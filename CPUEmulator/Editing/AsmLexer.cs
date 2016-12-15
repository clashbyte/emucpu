using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CPUEmulator.Editing {

	/// <summary>
	/// Лексер для примитивного ассемблера
	/// </summary>
	public class AsmLexer {

		// Константы для Scintilla
		public const int StyleDefault		= 0;
		public const int StyleInstruction	= 1;
		public const int StyleRegister		= 2;
		public const int StyleNumber		= 3;
		public const int StyleAddress		= 4;
		public const int StyleLabel			= 5;
		public const int StyleComment		= 6;

		// Хеш-сеты для инструкций и для регистров
		HashSet<string> instructions;
		HashSet<string> registers;

		/// <summary>
		/// Стайлинг строки
		/// </summary>
		/// <param name="scintilla">Scintilla</param>
		/// <param name="startPos">Начальная позиция</param>
		/// <param name="endPos">Конечная позиция</param>
		public void Style(Scintilla scintilla, int startPos, int endPos) {
			
			// Возвращаемся в начало строки
			int line = scintilla.LineFromPosition(startPos);
			startPos = scintilla.Lines[line].Position;

			// Начинаем стайлить
			int length = 0;
			int start = 0;
			string txt = scintilla.Text.ToLower();
			ParserState state = ParserState.Unknown;
			scintilla.StartStyling(startPos);
			
			// Цикл стайлинга
			bool next = true;
			int p = startPos;
			while (p < endPos) {
				
				// Получение символа
				char c = (char)scintilla.GetCharAt(p);

				switch (state) {
					case ParserState.Unknown:
						// Неизвестное состояние
						next = false;
						start = p;
						if (c == '@') {
							// Получена переменная
							state = ParserState.Address;
							next = true;
							length++;
						} else if (c == '.') {
							// Получен лейбл
							state = ParserState.Label;
							next = true;
							length++;
						} else if (c == ';' || c == '#') {
							// Комментарий
							state = ParserState.Comment;
						} else if (Char.IsDigit(c)) {
							// Получена цифра
							state = ParserState.Number;
						} else if (Char.IsLetter(c)) {
							// Получена буква
							state = ParserState.Identifier;
						} else {
							// Неизестный токен
							scintilla.SetStyling(1, StyleDefault);
							next = true;
						}
						break;

					case ParserState.Identifier:
					case ParserState.Address:
					case ParserState.Label:
						// Любое символьное обозначение
						if (Char.IsLetterOrDigit(c)) {
							length++;
						} else {
							string word = txt.Substring(start, length);
							if (state == ParserState.Address) {
								scintilla.SetStyling(length, StyleAddress);
							} else if (state == ParserState.Label) {
								scintilla.SetStyling(length, StyleLabel);
							} else if (instructions.Contains(word)) {
								scintilla.SetStyling(length, StyleInstruction);
							} else if (registers.Contains(word)) {
								scintilla.SetStyling(length, StyleRegister);
							} else {
								scintilla.SetStyling(length, StyleDefault);
							}
							length = 0;
							state = ParserState.Unknown;
							next = false;
						}
						break;

					case ParserState.Number:
						// Число
						if (Char.IsDigit(c)) {
							length++;
						} else {
							scintilla.SetStyling(length, StyleNumber);
							length = 0;
							state = ParserState.Unknown;
							next = false;
						}
						break;

					case ParserState.Comment:
						// Комментарий
						if (c != 10 && c != 13) {
							length++;
						} else {
							scintilla.SetStyling(length, StyleComment);
							length = 0;
							state = ParserState.Unknown;
							next = false;
						}
						break;
				}

				// Инкремент на следующий символ
				if (next) {
					p++;
				}
				next = true;
			}

			// Пост-обработка, если парсер не завершился
			switch (state) {

				case ParserState.Identifier:
				case ParserState.Address:
				case ParserState.Label:
					// Идентифиер
					string word = txt.Substring(start, length).ToLower();
					if (state == ParserState.Address) {
						scintilla.SetStyling(length, StyleAddress);
					} else if (state == ParserState.Label) {
						scintilla.SetStyling(length, StyleLabel);
					} else if (instructions.Contains(word)) {
						scintilla.SetStyling(length, StyleInstruction);
					} else if (registers.Contains(word)) {
						scintilla.SetStyling(length, StyleRegister);
					} else {
						scintilla.SetStyling(length, StyleDefault);
					}
					break;

				case ParserState.Number:
					// Цифра
					scintilla.SetStyling(length, StyleNumber);
					break;

				case ParserState.Comment:
					// Комментарий
					scintilla.SetStyling(length, StyleComment);
					break;
			}

		}

		/// <summary>
		/// Конструктор для лексера
		/// </summary>
		/// <param name="instructionList">Список инструкций</param>
		/// <param name="registerList">Список регистров</param>
		public AsmLexer(string instructionList, string registerList) {
			instructions = new HashSet<string>(Regex.Split(instructionList.ToLower() ?? string.Empty, @"\s+").Where(l => !string.IsNullOrEmpty(l)));
			registers = new HashSet<string>(Regex.Split(registerList.ToLower() ?? string.Empty, @"\s+").Where(l => !string.IsNullOrEmpty(l)));
		}
		
		/// <summary>
		/// Состояния парсера
		/// </summary>
		private enum ParserState {
			Unknown,
			Identifier,
			Number,
			Address,
			Label,
			Comment
		}
	}
}
