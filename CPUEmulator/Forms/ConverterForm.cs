using CPUEmulator.Editing;
using CPUEmulator.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CPUEmulator.Forms {

	/// <summary>
	/// Форма для конвертации старых кодов в новые
	/// </summary>
	public partial class ConverterForm : Form {

		/// <summary>
		/// Скомпилированный исходный код
		/// </summary>
		public string Code {
			get;
			private set;
		}

		/// <summary>
		/// Скрытый редактор
		/// </summary>
		CodeEditor editor;

		/// <summary>
		/// Конструктор формы
		/// </summary>
		public ConverterForm() {
			InitializeComponent();
			editor = new CodeEditor(new AsmLexer(Compiler.GetInstructions(), Compiler.GetRegisters()), splitContainer.Panel2);
			editor.AllowEdit = false;
			editor.Base.UsePopup(false);
			editor.Base.BringToFront();
			Shown += ConverterForm_Shown;
		}

		/// <summary>
		/// Форма выведена
		/// </summary>
		void ConverterForm_Shown(object sender, EventArgs e) {
			codeBox.Focus();
		}

		/// <summary>
		/// Изменён код в редакторе
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void codeBox_TextChanged(object sender, EventArgs e) {
			editor.AllowEdit = true;
			editor.Text = Convert(codeBox.Text);;
			editor.AllowEdit = false;
		}

		
		/// <summary>
		/// Конвертация кода
		/// </summary>
		string Convert(string txt) {
			errorLabel.Text = "";

			// Удаляем переносы и пробелы
			txt = txt.Replace("\n", " ").Replace("\r", "");
			while (txt.Contains("  ")) {
				txt = txt.Replace("  ", " ");
			}

			// Разбиваем на команды
			string[] lines = txt.Split(' ');

			// Разбор кодов
			List<string[]> codes = new List<string[]>();
			List<int> labels = new List<int>();
			for (int i = 0; i < lines.Length; i++) {
				string sn = lines[i];
				if (sn != "") {
					if (!Regex.IsMatch(sn, @"^\d{3}$")) {
						// Неизвестный ввод
						codes.Add(new string[]{
						"; Неизвестный ввод: "+sn
					});
					} else {
						// Получение кодов
						byte a = (byte)char.GetNumericValue(sn, 0);
						byte b = (byte)char.GetNumericValue(sn, 1);
						byte c = (byte)char.GetNumericValue(sn, 2);

						if (a == 0 && b == 0 && c == 0) {
							// Конец программы
							codes.Add(new string[]{
							"end\t\t\t\t; Завершение программы"
						});
						} else if (b == 0 && a != 8 && a != 9 && a != 0 && c != 0) {
							if (a == c) {
								// Пересылка из IN в другой регистр
								codes.Add(new string[]{
								"mov IN, "+IndexToReg(a)+"\t\t; Перемещение из IN в "+IndexToReg(a)
							});
							} else {
								// Пересылка регистр-регистр
								codes.Add(new string[]{
								"mov "+IndexToReg(a)+", "+IndexToReg(c)+"\t\t; Перемещение из "+IndexToReg(a)+" в "+IndexToReg(c)
							});
							}
						} else if (c == 0 && a != 8 && a != 9 && a != 0 && b != 0) {
							if (a == b) {
								// Сравнение с нулём
								codes.Add(new string[]{
								"",
								"; Сравнение регистра "+IndexToReg(a)+" с нулём",
								"mov "+IndexToReg(a)+", "+IndexToReg(1)+"\t\t; Перемещение из "+IndexToReg(a)+" в R1",
								"push R2\t\t\t; Сохранение регистра R2 в стек",
								"nul R2\t\t\t; Обнуление R2",
								"leq R1, R2\t\t; Сохранение R1 с R2",
								"pop R2\t\t\t; Возвращение значения R2 из стека"
							});
							} else {
								// Сравнение с регистром
								codes.Add(new string[]{
								"",
								"; Сравнение регистра "+IndexToReg(a)+" с регистром "+IndexToReg(b),
								"push R2\t\t\t; Сохранение регистра R2 в стек",
								"mov "+IndexToReg(b)+", "+IndexToReg(2)+"\t\t; Перемещение из "+IndexToReg(b)+" в R2",
								"mov "+IndexToReg(a)+", "+IndexToReg(1)+"\t\t; Перемещение из "+IndexToReg(a)+" в R1",
								"lss R1, R2\t\t; Сохранение R1 с R2",
								"pop R2\t\t\t; Возвращение значения R2 из стека"
							});
							}
						} else if (a == 0) {
							// Условный переход
							string lname = "label" + labels.Count;
							labels.Add(Math.Max(b * 10 + c, 1) - 1);
							codes.Add(new string[]{
							"jif @"+lname+"\t\t; Переход к метке ."+lname
						});
						} else if (b == 0 && c == 0) {
							// Безусловный переход
							codes.Add(new string[]{
							"; Прыжок по регистру "+IndexToReg(a)+" не поддерживается"
						});
						} else if (a == 5) {
							// Условный переход по регистру
							codes.Add(new string[]{
							"; Условный прыжок по регистру "+IndexToReg(c)+" не поддерживается"
						});
						} else if (a == 1) {
							// Сложение
							codes.Add(new string[]{
							"add "+IndexToReg(b)+", "+IndexToReg(c)+"\t\t; Сложение "+IndexToReg(b)+" и "+IndexToReg(c)
						});
						} else if (a == 2) {
							// Вычитание
							if (b == c) {
								codes.Add(new string[]{
								"nul "+IndexToReg(b)+"\t\t\t; Обнуление "+IndexToReg(b)
							});
							} else {
								codes.Add(new string[]{
								"sub "+IndexToReg(b)+", "+IndexToReg(c)+"\t\t; Вычитание "+IndexToReg(c)+" из "+IndexToReg(b)
							});
							}
						} else if (a == 3) {
							// Умножение
							codes.Add(new string[]{
							"mul "+IndexToReg(b)+", "+IndexToReg(c)+"\t\t; Умножение "+IndexToReg(b)+" на "+IndexToReg(c)
						});
						} else if (a == 4) {
							// Деление
							if (b == c) {
								codes.Add(new string[]{
								"one "+IndexToReg(b)+"\t\t\t; Установка "+IndexToReg(b)+" в единицу"
							});
							} else {
								codes.Add(new string[]{
								"div "+IndexToReg(b)+", "+IndexToReg(c)+"\t\t; Деление "+IndexToReg(b)+" на "+IndexToReg(c)
							});
							}
						} else if (a == 8) {
							// Чтение по адресу
							codes.Add(new string[]{
							"read "+(b*10+c)+"\t\t\t; Чтение из памяти"
						});
						} else if (a == 6) {
							// Чтение по регистру
							codes.Add(new string[]{
							"peek "+IndexToReg(c)+"\t\t\t; Чтение из ячейки памяти "+(b*10+c),
							"mov "+IndexToReg(8)+", "+IndexToReg(b)+"\t\t; Перемещение в регистр",
						});
						} else if (a == 9) {
							// Запись по адресу
							codes.Add(new string[]{
							"write "+(b*10+c)+"\t\t\t; Запись в ячейку памяти "+(b*10+c),
						});
						} else if (a == 7) {
							// Чтение по регистру
							codes.Add(new string[]{
							"mov "+IndexToReg(b)+", "+IndexToReg(9)+"\t\t; Перемещение в регистр вывода",
							"poke "+IndexToReg(c)+"\t\t\t; Запись в память",
						});
						}
					}
				}
			}

			// Сборка строк
			List<string> outl = new List<string>();
			for (int i = 0; i < codes.Count; i++) {
				
				// Вставка метки
				if (labels.Contains(i)) {
					outl.Add(".label"+labels.IndexOf(i));
				}

				// Обработка строк
				outl.AddRange(codes[i]);
			}
			return string.Join("\n", outl.ToArray());
		}

		/// <summary>
		/// Преобразование номера регистра в его имя
		/// </summary>
		/// <param name="n">Номер</param>
		/// <returns>Имя регистра</returns>
		string IndexToReg(int n) {
			if (n == 8) {
				return "IN";
			} else if (n == 9) {
				return "OUT";
			}
			return "R" + n;
		}

		/// <summary>
		/// Сохранение конвертации
		/// </summary>
		private void convertButton_Click(object sender, EventArgs e) {
			DialogResult = System.Windows.Forms.DialogResult.OK;
			Code = editor.Text;
			Close();
		}

		/// <summary>
		/// Закрытие формы
		/// </summary>
		protected override void OnClosed(EventArgs e) {
			base.OnClosed(e);
			if (DialogResult != System.Windows.Forms.DialogResult.OK) {
				DialogResult = System.Windows.Forms.DialogResult.Cancel;
			}
		}
	}
}
