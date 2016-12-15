using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPUEmulator.Lang.Operands;

namespace CPUEmulator.Lang.Instructions {

	/// <summary>
	/// Абстракная инструкция
	/// </summary>
	public abstract class Instruction {

		/// <summary>
		/// Индекс линии
		/// </summary>
		public int Line;

		/// <summary>
		/// Начальная позиция в тексте
		/// </summary>
		public int Position;

		/// <summary>
		/// Конечная позиция в тексте
		/// </summary>
		public int Length;

		/// <summary>
		/// Выполнение инструкции
		/// </summary>
		/// <param name="interpreter">Интерпретатор</param>
		/// <param name="instruction">Инструкция</param>
		/// <param name="operands">Операнды</param>
		public abstract void Execute(Interpreter interpreter, string instruction, Operand[] operands);

	}
}
