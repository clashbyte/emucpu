using System;
using EmuCPU.Lang.Operands;
using EmuCPU.Lang.Attributes;

namespace EmuCPU.Lang.Instructions {

	/// <summary>
	/// Доступ к указанной ячейке памяти
	/// </summary>
	[AsmCompile("write read", new Type[]{
		typeof(NumberOperand)
	})]
	public class MemoryNumInstruction : Instruction {

		/// <summary>
		/// Доступ к указанной ячейке памяти
		/// </summary>
		/// <param name="interpreter">Интерпретатор</param>
		/// <param name="instruction">Инструкция</param>
		/// <param name="operands">Операнды</param>
		public override void Execute(Interpreter interpreter, string instruction, Operand[] operands) {

			int mem = (operands[0] as NumberOperand).Number;
			if (mem < 1 || mem > 100) {
				throw new Exception("Обращение к несуществующей ячейке памяти: "+mem);
			}
			mem--;

			if (instruction == "read") {
				interpreter.Registers[7] = interpreter.Data[mem];
			} else {
				interpreter.Data[mem] = interpreter.Registers[8];
			}
		}

	}
}
