using System;
using EmuCPU.Lang.Operands;
using EmuCPU.Lang.Attributes;

namespace EmuCPU.Lang.Instructions {

	/// <summary>
	/// Инструкция передвижения из регистра в регистр
	/// </summary>
	[AsmCompile("mov", new Type[]{ 
		typeof(RegisterOperand),
		typeof(RegisterOperand)
	})]
	public class MoveInstruction : Instruction {

		/// <summary>
		/// Копирование из одного регистра в другой
		/// </summary>
		/// <param name="interpreter">Интерпретатор</param>
		/// <param name="instruction">Инструкция</param>
		/// <param name="operands">Операнды</param>
		public override void Execute(Interpreter interpreter, string instruction, Operand[] operands) {
			if ((operands[1] as RegisterOperand).Index == 7) {
				throw new Exception("Попытка записи в регистр чтения IN");
			}
			interpreter.Registers[(operands[1] as RegisterOperand).Index] = interpreter.Registers[(operands[0] as RegisterOperand).Index];
		}

	}
}
