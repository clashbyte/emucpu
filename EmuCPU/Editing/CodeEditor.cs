using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNET;
using System.Windows.Forms;
using System.Drawing;
using EmuCPU.Lang;

namespace EmuCPU.Editing {

	/// <summary>
	/// Редактор кода
	/// </summary>
	public class CodeEditor {

		/// <summary>
		/// Получение или установка текста
		/// </summary>
		public string Text {
			get {
				return editor.Text;
			}
			set {
				editor.Text = value;
				if (TextChanged != null) {
					TextChanged(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Режим документа
		/// </summary>
		public bool AllowEdit {
			get {
				return allowEdit;
			}
			set {
				allowEdit = value;
				editor.ReadOnly = !allowEdit;
			}
		}

		/// <summary>
		/// Ошибки
		/// </summary>
		public Compiler.Error[] Errors {
			get;
			private set;
		}

		/// <summary>
		/// Получение непосредственно редактора
		/// </summary>
		public Scintilla Base {
			get {
				return editor;
			}
		}

		/// <summary>
		/// Событие компиляции
		/// </summary>
		public event EventHandler RuntimeCompiled;

		/// <summary>
		/// Событие изменения текста
		/// </summary>
		public event EventHandler TextChanged;

		/// <summary>
		/// Ссылка на редактор
		/// </summary>
		Scintilla editor;

		/// <summary>
		/// Лексер для языка
		/// </summary>
		AsmLexer lexer;

		/// <summary>
		/// Таймер для прохода по документу
		/// </summary>
		Timer parseTimer;

		/// <summary>
		/// Скомпилированный код
		/// </summary>
		Compiler.CompiledCode compiled;

		/// <summary>
		/// Разрешать ли редактирование
		/// </summary>
		bool allowEdit;

		/// <summary>
		/// Полная очистка документа
		/// </summary>
		public void ResetWithText(string txt) {
			editor.ReadOnly = false;
			editor.Text = txt;
			editor.EmptyUndoBuffer();
			editor.SetSavePoint();
		}

		/// <summary>
		/// "Сохранение" файла
		/// </summary>
		public void SaveText() {
			editor.SetSavePoint();
		}

		/// <summary>
		/// Поиск строки по индексу символа
		/// </summary>
		public int LineFromPosition(int pos) {
			return editor.LineFromPosition(pos);
		}

		/// <summary>
		/// Пометка места выполнения
		/// </summary>
		/// <param name="pos">Индекс символа</param>
		/// <param name="len">Длина</param>
		public void SetExecutionMark(int pos, int len) {
			editor.IndicatorCurrent = 1;
			editor.IndicatorClearRange(0, editor.TextLength);
			if (len > 0) {
				editor.IndicatorFillRange(pos, len);
			}
		}

		/// <summary>
		/// Конструктор редактора
		/// </summary>
		/// <param name="asmLexer">Лексер для парсинга</param>
		/// <param name="parent">Родительский контейнер</param>
		public CodeEditor(AsmLexer asmLexer, Control parent) {
			
			// Инициализация таймера
			parseTimer = new Timer();
			parseTimer.Interval = 200;
			parseTimer.Tick += parseTimer_Tick;

			// Сохранение лексера
			lexer = asmLexer;

			// Инициализация редактора
			editor = new Scintilla();
			editor.Dock = DockStyle.Fill;
			editor.BorderStyle = BorderStyle.Fixed3D;
			editor.MouseDwellTime = 400;
			parent.Controls.Add(editor);

			// Настройка параметров редактора
			editor.BufferedDraw = true;
			editor.WrapMode = WrapMode.None;
			editor.IndentationGuides = IndentView.LookBoth;
			editor.Lexer = Lexer.Container;
			editor.UseTabs = true;

			// Стили
			editor.Styles[Style.Default].Font = "Consolas";
			editor.Styles[Style.Default].Size = 11;
			editor.Styles[AsmLexer.StyleDefault].Font = "Consolas";
			editor.Styles[AsmLexer.StyleDefault].Size = 11;
			editor.StyleClearAll();
			editor.Styles[Style.LineNumber].BackColor = Color.FromArgb(245, 245, 245);
			editor.Styles[Style.LineNumber].ForeColor = Color.FromArgb(120, 120, 120);
			editor.Styles[Style.IndentGuide].BackColor = Color.FromArgb(235, 235, 235);
			editor.Styles[Style.IndentGuide].ForeColor = Color.FromArgb(120, 120, 120);

			Margin numberMargin = editor.Margins[1];
			numberMargin.Width = 30;
			numberMargin.Type = MarginType.Number;
			numberMargin.Sensitive = true;
			numberMargin.Mask = 0;

			Margin runMargin = editor.Margins[2];
			runMargin.Width = 20;
			runMargin.Sensitive = true;
			runMargin.Type = MarginType.Symbol;
			runMargin.Mask = 4;
			runMargin.Cursor = MarginCursor.Arrow;

			Marker runMarker = editor.Markers[2];
			runMarker.Symbol = MarkerSymbol.Arrow;
			runMarker.SetBackColor(Color.Yellow);
			runMarker.SetForeColor(Color.Black);
			runMarker.SetAlpha(100);

			// Оформление
			editor.Styles[AsmLexer.StyleInstruction].ForeColor = Color.FromArgb(0, 0, 120);
			editor.Styles[AsmLexer.StyleInstruction].Bold = true;
			editor.Styles[AsmLexer.StyleComment].ForeColor = Color.FromArgb(100, 100, 0);
			editor.Styles[AsmLexer.StyleComment].Italic = true;
			editor.Styles[AsmLexer.StyleNumber].ForeColor = Color.FromArgb(0, 150, 150);
			editor.Styles[AsmLexer.StyleRegister].ForeColor = Color.FromArgb(0, 100, 0);
			editor.Styles[AsmLexer.StyleRegister].Case = StyleCase.Upper;
			editor.Styles[AsmLexer.StyleAddress].ForeColor = Color.FromArgb(100, 20, 20);
			editor.Styles[AsmLexer.StyleLabel].ForeColor = Color.FromArgb(150, 50, 0);
			editor.Styles[AsmLexer.StyleLabel].Weight = 32;

			// Индикатор выполнения
			editor.Indicators[1].Style = IndicatorStyle.RoundBox;
			editor.Indicators[1].OutlineAlpha = 250;
			editor.Indicators[1].Alpha = 30;
			editor.Indicators[1].ForeColor = Color.DarkOrange;

			// Индикатор ошибки
			editor.Indicators[2].Style = IndicatorStyle.Squiggle;
			editor.Indicators[2].Under = true;
			editor.Indicators[2].ForeColor = Color.Red;
			editor.Indicators[2].Alpha = 30;

			// События
			editor.StyleNeeded += codeEditor_StyleNeeded;
			editor.TextChanged += codeEditor_TextChanged;
			editor.DwellStart += codeEditor_DwellStart;
			editor.DwellEnd += codeEditor_DwellEnd;
		}

		/// <summary>
		/// Мышь наведена
		/// </summary>
		void codeEditor_DwellStart(object sender, DwellEventArgs e) {
			if (compiled != null && compiled.Errors != null) {
				foreach (Compiler.Error er in compiled.Errors) {
					if (er.StartPosition <= e.Position && er.StartPosition + er.Length > e.Position) {
						editor.CallTipShow(er.StartPosition, "Ошибка: \n" + er.Message);
						editor.CallTipSetForeHlt(Color.DarkRed);
						editor.CallTipSetHlt(0, 7);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Наведение мыши закончилось
		/// </summary>
		void codeEditor_DwellEnd(object sender, DwellEventArgs e) {
			if (editor.CallTipActive) {
				editor.CallTipCancel();
			}
		}

		/// <summary>
		/// Время парсить на ошибки
		/// </summary>
		void parseTimer_Tick(object sender, EventArgs e) {
			parseTimer.Stop();
			editor.IndicatorCurrent = 2;
			editor.IndicatorClearRange(0, editor.TextLength);
			compiled = Compiler.Compile(editor.Text);
			Errors = compiled.Errors;
			if (compiled.Errors != null) {
				foreach (Compiler.Error err in compiled.Errors) {
					editor.IndicatorFillRange(err.StartPosition, err.Length);
				}
			}
			if (RuntimeCompiled != null) {
				RuntimeCompiled(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Изменение кода в редакторе
		/// </summary>
		void codeEditor_TextChanged(object sender, EventArgs e) {
			if (parseTimer.Enabled) {
				parseTimer.Stop();
			}
			parseTimer.Start();
			if (TextChanged != null) {
				TextChanged(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Стилизация строки
		/// </summary>
		void codeEditor_StyleNeeded(object sender, StyleNeededEventArgs e) {
			lexer.Style(editor, editor.GetEndStyled(), e.Position);
		}
	}
}
