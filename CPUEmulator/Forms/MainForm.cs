using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CPUEmulator.Editing;
using ScintillaNET;
using CPUEmulator.Lang;
using System.Media;
using System.Reflection;

namespace CPUEmulator.Forms {

	/// <summary>
	/// Основная форма программы
	/// </summary>
	public partial class MainForm : Form {

		/// <summary>
		/// Редактор кода
		/// </summary>
		CodeEditor editor;

		/// <summary>
		/// Поле ввода для редактирования данных
		/// </summary>
		TextBox itemEditBox;

		/// <summary>
		/// Заголовок редактора
		/// </summary>
		string appTitle = "EmuCPU";

		/// <summary>
		/// Сохранён ли код
		/// </summary>
		bool documentSaved;

		/// <summary>
		/// Ссылка на файл
		/// </summary>
		string documentPath;

		/// <summary>
		/// Таймер обновления программы
		/// </summary>
		Timer updateTimer;

		/// <summary>
		/// Интерпретатор
		/// </summary>
		Interpreter interpreter;

		/// <summary>
		/// Базовый конструктор
		/// </summary>
		public MainForm() {
			InitializeComponent();

			// Инициализация полей данных
			typeof(ListView).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, dataList, new object[] { true }); 
			dataList.SuspendLayout();
			for (int i = 1; i < 100; i++) {
				dataList.Items.Add(new ListViewItem(new string[]{
					i.ToString(), "0"
				}));
			}
			dataList.ResumeLayout();

			// Инициализация регистров
			string[] rgs = new string[] {
				"R1", "R2", "R3", "R4", "R5", "R6", "R7", "IN", "OUT"
			};
			typeof(ListView).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, registerList, new object[] { true }); 
			registerList.SuspendLayout();
			foreach (string rg in rgs) {
				registerList.Items.Add(new ListViewItem(new string[]{
					rg, "0"
				}));
			}
			registerList.ResumeLayout();

			// Стек
			typeof(ListView).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, stackList, new object[] { true }); 

			// Инициализация лексера и редактора
			AsmLexer lexer = new AsmLexer(
				Compiler.GetInstructions(),
				Compiler.GetRegisters()
			);
			editor = new CodeEditor(lexer, splitContainer.Panel1);
			editor.RuntimeCompiled += editor_RuntimeCompiled;
			editor.TextChanged += editor_TextChanged;
			editor.Base.SavePointLeft += Base_SavePointLeft;
			editor.Base.SavePointReached += Base_SavePointReached;

			// Таймер
			updateTimer = new Timer();
			updateTimer.Interval = 20;
			updateTimer.Tick += updateTimer_Tick;
			updateTimer.Start();

			// Открытие пустого файла
			documentSaved = true;
			CreateFile();
		}

		/// <summary>
		/// Обновление заголовка
		/// </summary>
		void UpdateTitle() {
			string nm = documentPath == "" ? "Новый файл" : System.IO.Path.GetFileNameWithoutExtension(documentPath);
			if (!documentSaved) {
				nm = "* " + nm;
			}
			Text = nm + " - " + appTitle;
		}

		/// <summary>
		/// Создание пустого файла
		/// </summary>
		void CreateFile() {
			
			// Сохранение файла
			if (!documentSaved) {
				System.Windows.Forms.DialogResult rs = MessageBox.Show("Имеются несохраненные изменения. Сохранить файл?", "Сохранить изменения", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (rs == System.Windows.Forms.DialogResult.Yes) {
					SaveFile();
				} else if (rs == System.Windows.Forms.DialogResult.Cancel) {
					return;
				}
			}

			// Создание интерпретатора
			ResetInterpreter();

			// Данные
			documentSaved = true;
			documentPath = "";
			editor.ResetWithText("");
			toolStripUndo.Enabled = false;
			toolStripRedo.Enabled = false;
			UpdateTitle();
		}

		/// <summary>
		/// Открытие файла
		/// </summary>
		void OpenFile() {
			
			// Файл не сохранён
			if (!documentSaved) {
				System.Windows.Forms.DialogResult rs = MessageBox.Show("Имеются несохраненные изменения. Сохранить файл?", "Сохранить изменения", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (rs == System.Windows.Forms.DialogResult.Yes) {
					SaveFile();
				}else if(rs == System.Windows.Forms.DialogResult.Cancel){
					return;
				}
			}

			// Открытие файла
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Файл кода (*.asm)|*.asm";
			ofd.FilterIndex = 0;
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				documentPath = ofd.FileName;
				ResetInterpreter();
				editor.ResetWithText(System.IO.File.ReadAllText(documentPath));
			} else {
				return;
			}
		}

		/// <summary>
		/// Сохранение файла
		/// </summary>
		/// <param name="force">Запросить ли имя</param>
		void SaveFile(bool force = false) {

			// Запрос файла
			string fname = documentPath;
			if (force || fname == "") {
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "Файл кода (*.asm)|*.asm";
				sfd.FilterIndex = 0;
				if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					fname = sfd.FileName;
				} else {
					return;
				}
			}

			// Сохранение файла
			if (fname != "") {
				System.IO.File.WriteAllText(fname, editor.Text, Encoding.UTF8);
				documentPath = fname;
				editor.SaveText();
			}
		}

		/// <summary>
		/// Пересоздание интерпретатора
		/// </summary>
		void ResetInterpreter() {
			if (interpreter != null) {
				interpreter.Stop();
			}
			interpreter = new Interpreter();
			interpreter.StateChanged += interpreter_StateChanged;
			interpreter.RegisterChanged += interpreter_RegisterChanged;
			interpreter.DataChanged += interpreter_DataChanged;
			interpreter.StackChanged += interpreter_StackChanged;
		}

		/// <summary>
		/// Отображение редактора для элемента
		/// </summary>
		/// <param name="item">Элемент</param>
		/// <param name="parent">Родитель</param>
		void ShowItemEditBox(ListViewItem item, ListView parent) {
			// Подгонка списка и выбор размера
			parent.EnsureVisible(parent.Items.IndexOf(item));
			Rectangle b = item.SubItems[1].Bounds;
			b.X += 6;
			b.Y += 1;
			b.Height -= 2;
			b.Width -= 12;

			// Создание редактора
			itemEditBox = new TextBox();
			itemEditBox.Bounds = b;
			itemEditBox.AutoSize = false;
			itemEditBox.BorderStyle = BorderStyle.None;
			itemEditBox.Tag = (object)item;
			itemEditBox.Text = item.SubItems[1].Text;
			itemEditBox.LostFocus += itemEditBox_LostFocus;
			itemEditBox.KeyPress += itemEditBox_KeyPress;
			itemEditBox.TabStop = false;
			itemEditBox.TabIndex = parent.TabIndex;
			parent.Controls.Add(itemEditBox);
			itemEditBox.Focus();
			itemEditBox.SelectAll();
		}


		// Потеря фокуса редактора строк
		void itemEditBox_LostFocus(object sender, EventArgs e) {
			if (itemEditBox.Parent != null) {
				Control p = itemEditBox.Parent;
				itemEditBox.Parent.Controls.Remove(itemEditBox);
				itemEditBox = null;
				p.Focus();
			}
		}

		// Нажатие кнопки в редакторе строк
		void itemEditBox_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == (char)Keys.Return) {
				double num = 0;
				if (!double.TryParse(itemEditBox.Text, out num)) {
					SystemSounds.Exclamation.Play();
				}
				ListViewItem itm = (ListViewItem)itemEditBox.Tag;
				if (itm != null) {
					itm.SubItems[1].Text = num.ToString();
				}
				
				if (itm.ListView == dataList) {
					interpreter.Data[itm.Index] = num;
				} else if (itm.ListView == registerList) {
					interpreter.Registers[itm.Index] = num;
				} else {
					
				}

				e.Handled = true;
				itemEditBox_LostFocus(sender, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Двойной клик по списку данных
		/// </summary>
		void dataList_MouseDoubleClick(object sender, MouseEventArgs e) {
			ListViewHitTestInfo hit = dataList.HitTest(e.Location);
			if (hit.Item != null && interpreter.State != Interpreter.MachineState.Running) {
				ShowItemEditBox(hit.Item, dataList);
			}
		}

		/// <summary>
		/// Двойной клик по списку регистров
		/// </summary>
		void registerList_MouseDoubleClick(object sender, MouseEventArgs e) {
			ListViewHitTestInfo hit = registerList.HitTest(e.Location);
			if (hit.Item != null && interpreter.State == Interpreter.MachineState.Paused) {
				ShowItemEditBox(hit.Item, registerList);
			}
		}

		/// <summary>
		/// Стирание значения в списке данных
		/// </summary>
		void dataList_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) {
				if (dataList.SelectedItems.Count > 0 && interpreter.State != Interpreter.MachineState.Running) {
					ListViewItem itm = dataList.SelectedItems[0];
					itm.SubItems[1].Text = "0";
					interpreter.Data[itm.Index] = 0;
				}
			}
		}

		/// <summary>
		/// Стирание значения в регистре
		/// </summary>
		void registerList_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) {
				if (registerList.SelectedItems.Count > 0 && interpreter.State == Interpreter.MachineState.Paused) {
					ListViewItem itm = registerList.SelectedItems[0];
					itm.SubItems[1].Text = "0";
					interpreter.Registers[itm.Index] = 0;
				}
			}
		}

		/// <summary>
		/// Запуск кода
		/// </summary>
		void toolStripPlay_Click(object sender, EventArgs e) {
			
			// Проверка кода на валидность
			if (interpreter.LoadProgram(editor.Text)) {
				interpreter.Start();
			} else {
				MessageBox.Show("В программе обнаружены синтаксические\nошибки, запуск невозможен.", "Проблема запуска", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// Возвращение к исполнению
		/// </summary>
		void toolStripContinue_Click(object sender, EventArgs e) {
			interpreter.Start();
		}

		/// <summary>
		/// Пауза
		/// </summary>
		void toolStripPause_Click(object sender, EventArgs e) {
			interpreter.Pause();
		}

		/// <summary>
		/// Один шаг вперед
		/// </summary>
		void toolStripAdvance_Click(object sender, EventArgs e) {
			Interpreter.Error err = interpreter.StepForward();
			if (err != null) {
				MessageBox.Show("Произошла ошибка выполнения:\n" + err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			editor.SetExecutionMark(interpreter.CommandPos, interpreter.CommandLen);
		}

		/// <summary>
		/// Остановка программы
		/// </summary>
		void toolStripStop_Click(object sender, EventArgs e) {
			interpreter.Stop();
		}

		/// <summary>
		/// Отмена действия
		/// </summary>
		void toolStripUndo_Click(object sender, EventArgs e) {
			editor.Base.Undo();
		}

		/// <summary>
		/// Повтор действия
		/// </summary>
		void toolStripRedo_Click(object sender, EventArgs e) {
			editor.Base.Redo();
		}

		/// <summary>
		/// Изменено состояние машины
		/// </summary>
		void interpreter_StateChanged(object sender, EventArgs e) {
			editor.AllowEdit = interpreter.State == Interpreter.MachineState.Stopped;
			toolStripPlay.Visible = menuPlay.Visible = interpreter.State == Interpreter.MachineState.Stopped;
			toolStripContinue.Visible = menuContinue.Visible = interpreter.State == Interpreter.MachineState.Paused;
			toolStripPause.Visible = menuPause.Visible = interpreter.State == Interpreter.MachineState.Running;
			toolStripAdvance.Visible = menuAdvance.Visible = interpreter.State == Interpreter.MachineState.Paused;
			toolStripStop.Visible = menuStop.Visible = interpreter.State != Interpreter.MachineState.Stopped;
			if (itemEditBox!=null) {
				itemEditBox_LostFocus(null, null);
			}
			if (interpreter.State == Interpreter.MachineState.Paused) {
				editor.SetExecutionMark(interpreter.CommandPos, interpreter.CommandLen);
			} else {
				editor.SetExecutionMark(0, 0);
			}
			toolStripUndo.Enabled = menuUndo.Enabled = editor.Base.CanUndo && interpreter.State == Interpreter.MachineState.Stopped;
			toolStripRedo.Enabled = menuRedo.Enabled = editor.Base.CanRedo && interpreter.State == Interpreter.MachineState.Stopped;
		}

		/// <summary>
		/// Изменение значения ячейки данных
		/// </summary>
		void interpreter_DataChanged(object sender, Interpreter.NumberCollection.NumberChangedArgs e) {
			dataList.Items[e.Index].SubItems[1].Text = interpreter.Data[e.Index].ToString();
		}

		/// <summary>
		/// Изменение значения регистра
		/// </summary>
		void interpreter_RegisterChanged(object sender, Interpreter.NumberCollection.NumberChangedArgs e) {
			registerList.Items[e.Index].SubItems[1].Text = interpreter.Registers[e.Index].ToString();
		}

		/// <summary>
		/// Изменено содержимое стека
		/// </summary>
		void interpreter_StackChanged(object sender, EventArgs e) {
			stackList.SuspendLayout();
			int sel = -1;
			if (stackList.SelectedIndices.Count > 0) {
				sel = stackList.SelectedIndices[0];
			}
			stackList.Items.Clear();
			double[] vals = interpreter.Stack.ToArray();
			for (int i = 0; i < vals.Length; i++) {
				stackList.Items.Add(new ListViewItem(new string[]{ (vals.Length-i).ToString(), vals[i].ToString() }));
			}
			if (sel>-1 && vals.Length > 0) {
				sel = vals.Length - 1 - sel;
				if (sel < 0) {
					sel = 0;
				}
				if(sel >= vals.Length){
					sel = vals.Length - 1;
				}
				stackList.SelectedIndices.Add(sel);
			}
			stackList.ResumeLayout();
		}

		/// <summary>
		/// Завершено тестовое компилирование
		/// </summary>
		void editor_RuntimeCompiled(object sender, EventArgs e) {
			if (editor.Errors != null) {
				Compiler.Error err = null;
				foreach (Compiler.Error rr in editor.Errors) {
					if (err == null || rr.StartPosition < err.StartPosition) {
						err = rr;
					}
				}
				bottomStatusLabel.ForeColor = Color.DarkRed;
				bottomStatusLabel.Text = err.Message + " (строка " + (editor.LineFromPosition(err.StartPosition) + 1) + ")";
			} else {
				bottomStatusLabel.ForeColor = Color.DarkGreen;
				bottomStatusLabel.Text = "Компиляция завершена";
			}
		}

		// Изменение текста в редакторе
		void editor_TextChanged(object sender, EventArgs e) {
			toolStripUndo.Enabled = menuUndo.Enabled = editor.Base.CanUndo;
			toolStripRedo.Enabled = menuRedo.Enabled = editor.Base.CanRedo;
		}

		/// <summary>
		/// Достигнута точка сохранения
		/// </summary>
		void Base_SavePointReached(object sender, EventArgs e) {
			documentSaved = true;
			UpdateTitle();
		}

		/// <summary>
		/// Точка сохранения потеряна
		/// </summary>
		void Base_SavePointLeft(object sender, EventArgs e) {
			documentSaved = false;
			UpdateTitle();
		}

		/// <summary>
		/// Обновление программы
		/// </summary>
		void updateTimer_Tick(object sender, EventArgs e) {
			if (interpreter != null) {
				if (interpreter.State == Interpreter.MachineState.Running) {
					Interpreter.Error err = interpreter.Update();
					if (err != null) {
						MessageBox.Show("Произошла ошибка выполнения:\n"+err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
		}

		/// <summary>
		/// Новый файл
		/// </summary>
		void toolStripNew_Click(object sender, EventArgs e) {
			CreateFile();
		}

		/// <summary>
		/// Открытие файла
		/// </summary>
		void toolStripOpen_Click(object sender, EventArgs e) {
			OpenFile();
		}

		/// <summary>
		/// Сохранение файла
		/// </summary>
		void toolStripSave_Click(object sender, EventArgs e) {
			SaveFile(false);
		}

		/// <summary>
		/// Сохранение файла как
		/// </summary>
		void toolStripSaveAs_Click(object sender, EventArgs e) {
			SaveFile(true);
		}

		/// <summary>
		/// Вывод окна конвертера
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripConverter_Click(object sender, EventArgs e) {
			ConverterForm frm = new ConverterForm();
			if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				editor.Text = frm.Code;
			}
		}

	}
}
