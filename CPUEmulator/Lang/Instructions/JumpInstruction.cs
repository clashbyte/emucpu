using System;
using CPUEmulator.Lang.Operands;
using CPUEmulator.Lang.Attributes;

namespace CPUEmulator.Lang.Instructions {

	/// <summary>
	/// Инструкция перемещения указателя выполнения
	/// </summary>
	[AsmCompile("jmp jif", new Type[]{
		typeof(AddressOperand)
	})]
	public class JumpInstruction : Instruction {

		/// <summary>
		/// Прыжок
		/// </summary>
		/// <param name="interpreter">Интерпретатор</param>
		/// <param name="instruction">Инструкция</param>
		/// <param name="operands">Операнды</param>
		public override void Execute(Interpreter interpreter, string instruction, Operand[] operands) {
			bool j = true;
			if (instruction == "jif") {
				j = interpreter.Registers[0] > 0;
			}
			if (j) {
				interpreter.Jump((operands[0] as AddressOperand).Index);
			}
		}

	}
}
