using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CPUEmulator.Lang.Attributes;
using CPUEmulator.Lang.Instructions;
using CPUEmulator.Lang.Operands;

namespace CPUEmulator.Lang {

	/// <summary>
	/// Компилятор
	/// </summary>
	public static class Compiler {

		/// <summary>
		/// Разрешить ли пропуск запятой при вызове
		/// </summary>
		const bool ALLOW_COMMA_SKIP = false;

		/// <summary>
		/// Правила для компиляции 
		/// </summary>
		static CompileRule[] rules;

		/// <summary>
		/// Список всех доступных имён
		/// </summary>
		static string[] names;

		/// <summary>
		/// Список всех доступных регистров
		/// </summary>
		static string[][] registers;

		/// <summary>
		/// Сборка всех классов инструкций через reflection
		/// </summary>
		public static void SeekInstructions() {

			// Проход по типам
			List<CompileRule> rl = new List<CompileRule>();
			List<string> nm = new List<string>();
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				foreach (Type type in assembly.GetTypes()) {
					object[] attribs = type.GetCustomAttributes(typeof(AsmCompileAttribute), false);
					if (attribs != null && attribs.Length > 0) {
						AsmCompileAttribute a = (AsmCompileAttribute)attribs[0];
						rl.Add(new CompileRule() {
							instruction = type,
							names = a.Keys,
							operands = a.Operands
						});
						nm.AddRange(a.Keys);
					}
				}
			}

			// Правила и названия инструкций
			rules = rl.ToArray();
			names = nm.ToArray();
			
			// Сборка регистров
			registers = new string[][]{
				new string[] { "r1", "eax", "reg1" },	// Первый
				new string[] { "r2", "ebx", "reg2" },	// Второй
				new string[] { "r3", "ecx", "reg3" },	// Третий
				new string[] { "r4", "edx", "reg4" },	// Четвертый
				new string[] { "r5", "eex", "reg5" },	// Пятый
				new string[] { "r6", "efx", "reg6" },	// Шестой
				new string[] { "r7", "egx", "reg7" },	// Седьмой
				new string[] { "r8", "in",  "reg8" },	// Восьмой (входной)
				new string[] { "r9", "out", "reg9" },	// Девятый (выходной)
			};
			
		}

		/// <summary>
		/// Список всех инструкций в одной строке
		/// </summary>
		public static string GetInstructions() {
			return string.Join(" ", names);
		}

		/// <summary>
		/// Список всех регистров в одной строке
		/// </summary>
		public static string GetRegisters() {
			List<string> regs = new List<string>();
			foreach (string[] s in registers) {
				regs.AddRange(s);
			}
			return string.Join(" ", regs);
		}

		/// <summary>
		/// Индекс 
		/// </summary>
		/// <param name="name">Имя регистра</param>
		/// <returns>Индекс регистра</returns>
		public static int RegisterNameToIndex(string name) {
			name = name.ToLower();
			for (int i = 0; i < registers.Length; i++) {
				if (registers[i].Contains(name)) {
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Компиляция строки текста в байткод
		/// </summary>
		/// <param name="code">Текст</param>
		/// <returns>Программа</returns>
		public static CompiledCode Compile(string code) {

			// Списки всех данных
			List<Error> errors = new List<Error>();
			List<Label> labels = new List<Label>();
			List<Invocation> invocs = new List<Invocation>();
			HashSet<string> labelNames = new HashSet<string>(); 

			// Токенизация
			TokenCollection tokens = Tokenize(code);

			// Компилятор двухпроходной, подробнее
			// описано в разделе помощи
			for (int bounce = 0; bounce < 2; bounce++) {

				// Сброс токенизатора
				tokens.Reset();
				
				// Проход по токенам
				while (tokens.TokensAvail) {

					// Получение токена и его разбор
					Token t = tokens.Next();
					switch (t.Type) {

						case Token.TokenType.Instruction:
							// Инструкции - разбираются во втором проходе
							string cname = t.Content.ToLower();
							CompileRule cr = null;
							List<Operand> ops = new List<Operand>();

							// Поиск правила компиляции для оператора
							foreach (CompileRule rule in rules) {
								if (rule.names.Contains(cname)) {
									cr = rule;
									break;
								}
							}

							if (cr != null) {
								// Разбор данных
								int numOps = cr.operands.Length;
								bool save = true;

								// Поиск операндов
								for (int i = 0; i < numOps; i++) {

									Token tt = tokens.Peek();

									// Проверка на пустоту токена
									if (tt == null) {
										if (bounce == 1) {
											errors.Add(new Error("Неожиданный конец определения аргументов: " + t.Content, t.Start, t.Length));
										}
										save = false;
										break;
									}

									// Сравнение типов
									Type opType = cr.operands[i];
									Type metType = null;

									if (tt.Type == Token.TokenType.Number) {
										metType = typeof(NumberOperand);
									} else if (tt.Type == Token.TokenType.Register) {
										metType = typeof(RegisterOperand);
									} else if (tt.Type == Token.TokenType.Address) {
										metType = typeof(AddressOperand);
									} else {
										if (bounce == 1) {
											// Неожиданный токен
											switch (tt.Type) {
												case Token.TokenType.Unknown:
													errors.Add(new Error("Неожиданный литерал: " + tt.Content, tt.Start, tt.Length));
													break;
												case Token.TokenType.Instruction:
													errors.Add(new Error("Неожиданная инструкция: " + tt.Content, tt.Start, tt.Length));
													break;
												case Token.TokenType.Label:
													errors.Add(new Error("Неожиданная метка: " + tt.Content, tt.Start, tt.Length));
													break;
												case Token.TokenType.Comma:
													errors.Add(new Error("Неожиданный, возможно дублированный разделитель", tt.Start, tt.Length));
													break;
											}
										}
										tokens.Skip();
										save = false;
										break;
									}

									// Проверка на совпадение параметров
									if (metType != opType) {

										if (bounce == 1) {
											string error = "";

											// Исходный тип
											if (metType == typeof(NumberOperand)) {
												error = "Получена числовая константа, ";
											} else if (metType == typeof(RegisterOperand)) {
												error = "Получен регистр, ";
											} else if (metType == typeof(AddressOperand)) {
												error = "Получена ссылка на метку, ";
											} else {
												error = "Получен неизвестный тип, ";
											}

											// Требуемый тип
											if (opType == typeof(NumberOperand)) {
												error += "требовалась числовая константа";
											} else if (opType == typeof(RegisterOperand)) {
												error += "требовался регистр";
											} else if (opType == typeof(AddressOperand)) {
												error += "требовалась ссылка на метку";
											} else {
												error += "требовался неизвестный тип (што?)";
											}

											// Сохранение ошибки
											errors.Add(new Error(error, tt.Start, tt.Length));
										}
										
										tokens.Skip();
										save = false;
										break;
									}

									// Сохранение операнда для вызова
									if (bounce == 1) {
										if (opType == typeof(NumberOperand)) {
											// Сохранение числа
											ops.Add(new NumberOperand(tt.Content));
										} else if (opType == typeof(RegisterOperand)) {
											// Поиск регистра
											ops.Add(new RegisterOperand(RegisterNameToIndex(tt.Content)));
										} else if (opType == typeof(AddressOperand)) {
											// Поиск адреса для прыжка
											int addr = -1;
											foreach (Label l in labels) {
												if (l.Name == tt.Content) {
													addr = l.Location;
													break;
												}
											}
											if (addr >= 0) {
												ops.Add(new AddressOperand(addr));
											} else {
												errors.Add(new Error("Не найдена указанная метка: "+tt.Content, tt.Start, tt.Length));
											}
										}
									}

									// Пропуск до следующего токена
									tokens.Skip();

									// Операнд сохранён, можно искать запятую
									if (i < numOps - 1) {
										tt = tokens.Peek();
										if (tt == null || tt.Type != Token.TokenType.Comma && !ALLOW_COMMA_SKIP) {
											if (bounce == 1) {
												errors.Add(new Error("Не все аргументы объявлены (получено "+(i+1)+", ожидалось "+numOps+")", t.Start, t.Length));
												save = false;
												tokens.Skip();
												break;
											}
										}
										tokens.Skip();
									}

								}

								// Сохранение вызова
								if (save && bounce == 1) {
									Instruction inst = (Instruction)Activator.CreateInstance(cr.instruction);
									inst.Position = t.Start;
									inst.Length = t.Length;
									invocs.Add(new Invocation(cname, inst, ops.ToArray()));
								}
							} else {
								if (bounce == 1) {
									errors.Add(new Error("Правило компиляции не найдено: " + t.Content, t.Start, t.Length));
								}
							}
							break;

						case Token.TokenType.Label:
							// Метки - разбираются в обоих проходах
							if (t.Content.Length > 0) {
								string lname = t.Content.ToLower();
								if (!labelNames.Contains(lname) || bounce == 1) {
									if (bounce == 1) {
										labels.Add(new Label(lname, invocs.Count));
									} else {
										labelNames.Add(lname);
									}
								} else {
									// Повторяющаяся метка
									errors.Add(new Error("Повторное использование метки: " + t.Content, t.Start, t.Length));
								}
							} else {
								// Метка пуста
								if (bounce == 0) {
									errors.Add(new Error("Пропущено имя метки", t.Start, t.Length));
								}
							}
							break;

						case Token.TokenType.Unknown:
							// Неизвестный токен
							if (bounce == 0) {
								errors.Add(new Error("Неизвестный литерал: " + t.Content, t.Start, t.Length));
							}
							break;
						
						case Token.TokenType.Register:
							// Неизвестный регистр
							if (bounce == 0) {
								errors.Add(new Error("Неожиданный вызов регистра: " + t.Content.ToUpper(), t.Start, t.Length));
							}
							break;

						case Token.TokenType.Number:
							// Номерная константа
							if (bounce == 0) {
								errors.Add(new Error("Неожиданная числовая константа: " + t.Content, t.Start, t.Length));
							}
							break;

						case Token.TokenType.Address:
							// Адрес
							if (bounce == 0) {
								errors.Add(new Error("Неожиданная ссылка: @" + t.Content, t.Start, t.Length));
							}
							break;

						case Token.TokenType.Comma:
							// Адрес
							if (bounce == 0) {
								errors.Add(new Error("Неожиданная разделительная запятая", t.Start, t.Length));
							}
							break;
					}

				}
			}

			// Создание объекта кода
			if (errors.Count > 0) {
				return new CompiledCode(errors.ToArray());
			}
			return new CompiledCode(invocs.ToArray(), labels.ToArray());
		}

		/// <summary>
		/// Токенизация кода
		/// </summary>
		/// <param name="text">Текст</param>
		/// <returns>Коллекция токенов</returns>
		static TokenCollection Tokenize(string text) {

			// Небольшой костыль
			text += " ";

			// Список токенов
			List<Token> tokens = new List<Token>();
			HashSet<string> keys = new HashSet<string>(GetInstructions().ToLower().Split(' '));
			HashSet<string> regs = new HashSet<string>(GetRegisters().ToLower().Split(' '));

			// Переменные, как и в лексере
			int p = 0;
			int max = text.Length;
			bool next = true;
			char c;
			int start = 0, length = 0;
			TokenizerState state = TokenizerState.Unknown;

			// Проход по токенам
			while (p < max) {

				// Получение символа
				c = text[p];

				switch (state) {
					case TokenizerState.Unknown:
						// Неизвестное состояние
						if (!Char.IsWhiteSpace(c)) {
							next = false;
							start = p;
							if (c == '@') {
								// Получена переменная
								state = TokenizerState.Address;
								next = true;
								length++;
							} else if (c == '.') {
								// Получен лейбл
								state = TokenizerState.Label;
								next = true;
								length++;
							} else if (c == ',') {
								// Запятая
								tokens.Add(new Token() {
									Content = c.ToString(),
									Start = p,
									Length = 1,
									Type = Token.TokenType.Comma
								});
								next = true;
							} else if (c == ';' || c == '#') {
								// Комментарий
								state = TokenizerState.Comment;
							} else if (Char.IsDigit(c) || c == '-') {
								// Получена цифра
								state = TokenizerState.Number;
								next = true;
								length = 1;
							} else if (Char.IsLetter(c)) {
								// Получена буква
								state = TokenizerState.Identifier;
							} else {
								// Неизестный токен
								tokens.Add(new Token() {
									Content = c.ToString(),
									Start = p,
									Length = 1,
									Type = Token.TokenType.Unknown
								});
								next = true;
							}
						}
						break;

					case TokenizerState.Identifier:
					case TokenizerState.Address:
					case TokenizerState.Label:
						// Любое символьное обозначение
						if (Char.IsLetterOrDigit(c)) {
							length++;
						} else {
							string word = text.Substring(start, length).ToLower();
							if (state == TokenizerState.Address) {
								// Адрес
								tokens.Add(new Token() {
									Content = word.Substring(1),
									Start = start,
									Length = length,
									Type = Token.TokenType.Address
								});
							} else if (state == TokenizerState.Label) {
								// Метка
								tokens.Add(new Token() {
									Content = word.Substring(1),
									Start = start,
									Length = length,
									Type = Token.TokenType.Label
								});
							} else if (keys.Contains(word)) {
								// Инструкция
								tokens.Add(new Token() {
									Content = word,
									Start = start,
									Length = length,
									Type = Token.TokenType.Instruction
								});
							} else if (regs.Contains(word)) {
								// Регистр
								tokens.Add(new Token() {
									Content = word,
									Start = start,
									Length = length,
									Type = Token.TokenType.Register
								});
							} else {
								// Неизвестный токен
								tokens.Add(new Token() {
									Content = word,
									Start = start,
									Length = length,
									Type = Token.TokenType.Unknown
								});
							}
							length = 0;
							state = TokenizerState.Unknown;
							next = false;
						}
						break;

					case TokenizerState.Number:
						// Число
						if (Char.IsDigit(c)) {
							length++;
						} else {
							tokens.Add(new Token() {
								Content = text.Substring(start, length),
								Start = start,
								Length = length,
								Type = Token.TokenType.Number
							});
							length = 0;
							state = TokenizerState.Unknown;
							next = false;
						}
						break;

					case TokenizerState.Comment:
						// Комментарий
						if (c == 10 || c == 13) {
							state = TokenizerState.Unknown;
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

				case TokenizerState.Identifier:
				case TokenizerState.Address:
				case TokenizerState.Label:
					// Идентифиер
					string word = text.Substring(start, length).ToLower();
					if (state == TokenizerState.Address) {
						// Адрес
						tokens.Add(new Token() {
							Content = word,
							Start = start,
							Length = length,
							Type = Token.TokenType.Address
						});
					} else if (state == TokenizerState.Label) {
						// Метка
						tokens.Add(new Token() {
							Content = word,
							Start = start,
							Length = length,
							Type = Token.TokenType.Label
						});
					} else if (keys.Contains(word)) {
						// Инструкция
						tokens.Add(new Token() {
							Content = word,
							Start = start,
							Length = length,
							Type = Token.TokenType.Instruction
						});
					} else if (regs.Contains(word)) {
						// Регистр
						tokens.Add(new Token() {
							Content = word,
							Start = start,
							Length = length,
							Type = Token.TokenType.Register
						});
					} else {
						// Неизвестный токен
						tokens.Add(new Token() {
							Content = word,
							Start = start,
							Length = length,
							Type = Token.TokenType.Unknown
						});
					}
					break;

				case TokenizerState.Number:
					// Цифра
					tokens.Add(new Token() {
						Content = text.Substring(start, length),
						Start = start,
						Length = length,
						Type = Token.TokenType.Number
					});
					break;
			}
			return new TokenCollection(tokens.ToArray());
		}

		/// <summary>
		/// Правило для компиляции
		/// </summary>
		class CompileRule {

			/// <summary>
			/// Название для инструкции
			/// </summary>
			public string[] names;

			/// <summary>
			/// Тип инструкции
			/// </summary>
			public Type instruction;

			/// <summary>
			/// Типы операндов
			/// </summary>
			public Type[] operands;
		}

		/// <summary>
		/// Один из элементов
		/// </summary>
		class Token {

			/// <summary>
			/// Содержимое токена
			/// </summary>
			public string Content;

			/// <summary>
			/// Тип токена
			/// </summary>
			public TokenType Type;

			/// <summary>
			/// Начало данных
			/// </summary>
			public int Start;

			/// <summary>
			/// Длина данных
			/// </summary>
			public int Length;

			/// <summary>
			/// Тип токена
			/// </summary>
			public enum TokenType {
				Unknown,
				Instruction,
				Register,
				Number,
				Address,
				Label,
				Comma
			}
		}

		/// <summary>
		/// Коллекция токенов для парсинга
		/// </summary>
		class TokenCollection {

			/// <summary>
			/// Список токенов
			/// </summary>
			Token[] tokens;

			/// <summary>
			/// Текущая позиция в списке токенов
			/// </summary>
			int tokenPointer;

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="ts">Список токенов</param>
			public TokenCollection(Token[] ts) {
				tokens = ts;
				tokenPointer = 0;
			}

			/// <summary>
			/// Доступны ли токены
			/// </summary>
			public bool TokensAvail {
				get {
					if (tokens != null) {
						return tokens.Length > tokenPointer;
					}
					return false;
				}
			}

			/// <summary>
			/// Получение следующего токена из списка
			/// </summary>
			/// <returns>Токен или null</returns>
			public Token Next() {
				Token t = null;
				if (tokens != null) {
					if (tokenPointer < tokens.Length) {
						t = tokens[tokenPointer];
						tokenPointer++;
					}
				}
				return t;
			}

			/// <summary>
			/// Получение следующего токена без увеличения счётчика
			/// </summary>
			/// <returns>Токен или null</returns>
			public Token Peek() {
				Token t = null;
				if (tokens != null) {
					if (tokenPointer < tokens.Length) {
						t = tokens[tokenPointer];
					}
				}
				return t;
			}

			/// <summary>
			/// Пропуск токена
			/// </summary>
			public void Skip() {
				if (tokens != null) {
					if (tokenPointer < tokens.Length) {
						tokenPointer++;
					}
				}
			}

			/// <summary>
			/// Сброс счётчика токенов
			/// </summary>
			public void Reset() {
				tokenPointer = 0;
			}
		}

		/// <summary>
		/// Состояние токенизера
		/// </summary>
		private enum TokenizerState {
			Unknown,
			Identifier,
			Number,
			Address,
			Label,
			Comment
		}

		/// <summary>
		/// Скомпилированный код
		/// </summary>
		public class CompiledCode {

			/// <summary>
			/// Компиляция байткода
			/// </summary>
			public Invocation[] Bytecode {
				get;
				private set;
			}

			/// <summary>
			/// Метки для перехода
			/// </summary>
			public Label[] Labels {
				get;
				private set;
			}

			/// <summary>
			/// Ошибки при компиляции
			/// </summary>
			public Error[] Errors {
				get;
				private set;
			}

			/// <summary>
			/// Создание успешно скомпилированного байткода
			/// </summary>
			/// <param name="invocations">Вызовы</param>
			/// <param name="labels">Метки</param>
			public CompiledCode(Invocation[] invocations, Label[] labels) {
				Bytecode = invocations;
				Labels = labels;
			}

			/// <summary>
			/// Создание байткода с ошибками
			/// </summary>
			/// <param name="errors">Список ошибок</param>
			public CompiledCode(Error[] errors) {
				Errors = errors;
			}
		}

		/// <summary>
		/// Метка для перехода
		/// </summary>
		public class Label {

			/// <summary>
			/// Имя метки
			/// </summary>
			public string Name { get; private set; }

			/// <summary>
			/// Индекс команды для прыжка
			/// </summary>
			public int Location { get; private set; }

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="name">Имя метки</param>
			/// <param name="loc">Индекс команды для перехода</param>
			public Label(string name, int loc) {
				Name = name;
				Location = loc;
			}
		}

		/// <summary>
		/// Вызов функции
		/// </summary>
		public class Invocation {

			/// <summary>
			/// Имя вызываемой команды
			/// </summary>
			public string Name {
				get;
				private set;
			}

			/// <summary>
			/// Вызов инструкции
			/// </summary>
			public Instruction CallingInstruction {
				get;
				private set;
			}

			/// <summary>
			/// Операнды
			/// </summary>
			public Operand[] Operands {
				get;
				private set;
			}

			/// <summary>
			/// Конструкция вызова процедуры
			/// </summary>
			/// <param name="name">Имя вызываемой конструкции</param>
			/// <param name="inst">Инструкция</param>
			/// <param name="ops">Операнды</param>
			public Invocation(string name, Instruction inst, Operand[] ops) {
				Name = name;
				CallingInstruction = inst;
				Operands = ops;
			}
		}

		/// <summary>
		/// Ошибка при компиляции байткода
		/// </summary>
		public class Error {

			/// <summary>
			/// Начальная позиция
			/// </summary>
			public int StartPosition {
				get;
				private set;
			}

			/// <summary>
			/// Длина
			/// </summary>
			public int Length {
				get;
				private set;
			}

			/// <summary>
			/// Текст ошибки
			/// </summary>
			public string Message {
				get;
				private set;
			}

			/// <summary>
			/// Конструктор ошибки
			/// </summary>
			/// <param name="msg">Сообщение</param>
			/// <param name="start">Начало</param>
			/// <param name="len">Длина</param>
			public Error(string msg, int start, int len) {
				StartPosition = start;
				Length = len;
				Message = msg;
			}
		}
	}
}
