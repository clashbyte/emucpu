using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmuCPU.Lang {

	/// <summary>
	/// Интерпретатор
	/// </summary>
	public class Interpreter {

		/// <summary>
		/// Состояние интерпретатора
		/// </summary>
		public MachineState State {
			get;
			private set;
		}

		/// <summary>
		/// Индекс выполняемой команды
		/// </summary>
		public int Pointer {
			get;
			private set;
		}

		/// <summary>
		/// Расположение команды
		/// </summary>
		public int CommandPos {
			get;
			private set;
		}

		/// <summary>
		/// Длина текущей команды
		/// </summary>
		public int CommandLen {
			get;
			private set;
		}

		/// <summary>
		/// Регистры
		/// </summary>
		public NumberCollection Registers {
			get;
			private set;
		}

		/// <summary>
		/// Данные
		/// </summary>
		public NumberCollection Data {
			get;
			private set;
		}

		/// <summary>
		/// Стек
		/// </summary>
		public NumberStack Stack {
			get;
			private set;
		}

		/// <summary>
		/// Программа для выполнения
		/// </summary>
		public Compiler.CompiledCode Program {
			get;
			private set;
		}

		/// <summary>
		/// Изменилось состояние
		/// </summary>
		public event EventHandler StateChanged;

		/// <summary>
		/// Изменено содержимое регистра
		/// </summary>
		public event EventHandler<NumberCollection.NumberChangedArgs> RegisterChanged;

		/// <summary>
		/// Изменено содержимое ячейки данных
		/// </summary>
		public event EventHandler<NumberCollection.NumberChangedArgs> DataChanged;

		/// <summary>
		/// Изменено содержимое стека
		/// </summary>
		public event EventHandler StackChanged;

		/// <summary>
		/// Конструктор для интерпретатора
		/// </summary>
		public Interpreter() {
			Registers = new NumberCollection(9);
			Data = new NumberCollection(100);
			Stack = new NumberStack();
			Registers.Changed += Registers_Changed;
			Data.Changed += Data_Changed;
			Stack.Changed += Stack_Changed;
		}


		/// <summary>
		/// Загрузка кода и подготовка к запуску
		/// </summary>
		/// <param name="code">Скомпилированный код</param>
		public bool LoadProgram(string code) {
			if (State != MachineState.Stopped) {
				Stop();
			}
			Program = Compiler.Compile(code);
			return Program.Errors == null;
		}

		/// <summary>
		/// Запуск программы
		/// </summary>
		public void Start() {
			State = MachineState.Running;
			if (StateChanged != null) {
				StateChanged(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Пауза
		/// </summary>
		public void Pause() {
			State = MachineState.Paused;
			if (StateChanged != null) {
				StateChanged(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Остановка
		/// </summary>
		public void Stop() {
			State = MachineState.Stopped;
			if (StateChanged != null) {
				StateChanged(this, EventArgs.Empty);
			}
			Pointer = CommandLen = CommandPos = 0;
			for (int i = 0; i < 9; i++) {
				Registers[i] = 0;
			}
			Stack.Clear();
		}

		/// <summary>
		/// Один шаг вперед
		/// </summary>
		public Error StepForward() {
			if (State == MachineState.Paused) {
				return Update(1);
			}
			return null;
		}

		/// <summary>
		/// Обновление состояния интерпретатора
		/// </summary>
		/// <returns>Ошибка или null</returns>
		public Error Update() {
			if (State == MachineState.Running) {
				// По 4 команды на обновление
				return Update(4);
			}
			return null;
		}

		/// <summary>
		/// Переход к указанной команде
		/// </summary>
		/// <param name="pos">Индекс команды</param>
		public void Jump(int pos) {
			Pointer = pos-1;
		}

		/// <summary>
		/// Обновление машины
		/// </summary>
		Error Update(int invokeCount) {
			if (Program != null) {
				if (Program.Bytecode != null) {
					while (invokeCount > 0) {
						if (Pointer < Program.Bytecode.Length) {
							Compiler.Invocation inv = Program.Bytecode[Pointer];
							try {
								RunCommand(inv);
							} catch (Exception ex) {
								Pause();
								Pointer++;
								return new Error(ex.Message, inv.CallingInstruction.Position, inv.CallingInstruction.Length);
							}
							Pointer++;
							if (State != MachineState.Running) {
								break;
							}
						} else {
							Stop();
							return null;
						}
						invokeCount--;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Выполнение одной команды
		/// </summary>
		/// <param name="inv">Вызов</param>
		void RunCommand(Compiler.Invocation inv) {
			CommandPos = inv.CallingInstruction.Position;
			CommandLen = inv.CallingInstruction.Length;
			inv.CallingInstruction.Execute(this, inv.Name, inv.Operands);
		}

		/// <summary>
		/// Данные изменены
		/// </summary>
		void Data_Changed(object sender, Interpreter.NumberCollection.NumberChangedArgs e) {
			if (DataChanged != null) {
				DataChanged(this, e);
			}
		}

		/// <summary>
		/// Регистры изменены
		/// </summary>
		void Registers_Changed(object sender, Interpreter.NumberCollection.NumberChangedArgs e) {
			if (RegisterChanged != null) {
				RegisterChanged(this, e);
			}
		}

		/// <summary>
		/// Стек изменен
		/// </summary>
		void Stack_Changed(object sender, EventArgs e) {
			if (StackChanged != null) {
				StackChanged(this, e);
			}
		}

		/// <summary>
		/// Состояние интерпретатора
		/// </summary>
		public enum MachineState {
			/// <summary>
			/// Остановлена
			/// </summary>
			Stopped,
			/// <summary>
			/// На паузе
			/// </summary>
			Paused,
			/// <summary>
			/// Работает
			/// </summary>
			Running
		}

		/// <summary>
		/// Ошибка выполнения
		/// </summary>
		public class Error {
			
			/// <summary>
			/// Расположение ошибки
			/// </summary>
			public int Location {
				get;
				private set;
			}

			/// <summary>
			/// Длина ошибочного сегмента
			/// </summary>
			public int Length {
				get;
				private set;
			}

			/// <summary>
			/// Сообщение
			/// </summary>
			public string Message {
				get;
				private set;
			}

			/// <summary>
			/// Конструктор ошибки
			/// </summary>
			/// <param name="txt">Текст</param>
			/// <param name="pos">Расположение</param>
			/// <param name="len">Длина</param>
			public Error(string txt, int pos, int len) {
				Message = txt;
				Location = pos;
				Length = len;
			}
		}

		/// <summary>
		/// Класс-контейнер для чисел
		/// </summary>
		public class NumberCollection {

			/// <summary>
			/// Значения
			/// </summary>
			double[] values;

			/// <summary>
			/// Изменение номера
			/// </summary>
			public event EventHandler<NumberChangedArgs> Changed;

			/// <summary>
			/// Доступ к указанному полю
			/// </summary>
			/// <param name="idx">Индекс</param>
			/// <returns>Данные</returns>
			public double this[int idx] {
				get {
					if (idx >= 0 && idx < values.Length) {
						return values[idx];
					}
					return 0;
				}
				set {
					if (idx >= 0 && idx < values.Length) {
						values[idx] = value;
						if (Changed != null) {
							Changed(this, new NumberChangedArgs() {
								Index = idx
							});
						}
					}
				}
			}

			/// <summary>
			/// Конструктор коллекции
			/// </summary>
			/// <param name="cap">Размер</param>
			public NumberCollection(int cap) {
				values = new double[cap];
			}

			/// <summary>
			/// Событие изменения номера
			/// </summary>
			public class NumberChangedArgs : EventArgs {
				public int Index {
					get;
					set;
				}
			}
		}

		/// <summary>
		/// Класс-контейнер для чисел
		/// </summary>
		public class NumberStack {

			/// <summary>
			/// Значения
			/// </summary>
			Stack<double> values;

			/// <summary>
			/// Изменение номера
			/// </summary>
			public event EventHandler Changed;

			/// <summary>
			/// Преобразование в массив
			/// </summary>
			/// <returns>Массив</returns>
			public double[] ToArray() {
				return values.ToArray();
			}

			/// <summary>
			/// Конструктор стека
			/// </summary>
			public NumberStack() {
				values = new Stack<double>();
			}

			/// <summary>
			/// Очистка стека
			/// </summary>
			public void Clear() {
				values.Clear();
				if (Changed != null) {
					Changed(this, EventArgs.Empty);
				}
			}

			/// <summary>
			/// Добавление числа в стек
			/// </summary>
			/// <param name="d">Число для добавления</param>
			public void Push(double d) {
				if (values.Count == 16) {
					throw new Exception("Стек переполнен - все 16 ячеек стека заняты");
				}
				values.Push(d);
				if (Changed != null) {
					Changed(this, EventArgs.Empty);
				}
			}

			/// <summary>
			/// Извлечение числа из стека
			/// </summary>
			/// <returns>Число</returns>
			public double Pop() {
				if (values.Count == 0) {
					throw new Exception("Стек пуст - чтение невозможно");
				}
				double d = values.Pop();
				if (Changed != null) {
					Changed(this, EventArgs.Empty);
				}
				return d;
			}

			/// <summary>
			/// Событие изменения номера
			/// </summary>
			public class NumberChangedArgs : EventArgs {
				public int Index {
					get;
					set;
				}
			}
		}
	}
}
