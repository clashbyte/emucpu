using System;
using CPUEmulator.Lang.Operands;
using CPUEmulator.Lang.Attributes;

namespace CPUEmulator.Lang.Instructions {

	/// <summary>
	/// Инструкции управления выполнением
	/// </summary>
	[AsmCompile("brk end", new Type[]{  })]
	public class ControlInstruction : Instruction {

		/// <summary>
		/// Инструкции управления выполнением
		/// </summary>
		/// <param name="interpreter">Интерпретатор</param>
		/// <param name="instruction">Инструкция</param>
		/// <param name="operands">Операнды</param>
		public override void Execute(Interpreter interpreter, string instruction, Operand[] operands) {

			if (instruction == "brk") {
				// Остановка программы
				interpreter.Pause();
			} else {
				// Завершение программы
				interpreter.Stop();
			}

		}

	}
}
