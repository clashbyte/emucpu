using System;
using CPUEmulator.Lang.Operands;
using CPUEmulator.Lang.Attributes;

namespace CPUEmulator.Lang.Instructions {

	/// <summary>
	/// Доступ к указанной ячейке памяти
	/// </summary>
	[AsmCompile("poke peek", new Type[]{ 
		typeof(RegisterOperand)
	})]
	public class MemoryRegisterInstruction : Instruction {

		/// <summary>
		/// Доступ к указанной ячейке памяти
		/// </summary>
		/// <param name="interpreter">Интерпретатор</param>
		/// <param name="instruction">Инструкция</param>
		/// <param name="operands">Операнды</param>
		public override void Execute(Interpreter interpreter, string instruction, Operand[] operands) {

			int mem = (int)interpreter.Registers[(operands[0] as RegisterOperand).Index];
			if (mem < 1 || mem > 100) {
				throw new Exception("Обращение к несуществующей ячейке памяти: " + mem);
			}
			mem--;

			if (instruction == "peek") {
				interpreter.Registers[7] = interpreter.Data[mem];
			} else {
				interpreter.Data[mem] = interpreter.Registers[8];
			}
		}

	}
}
