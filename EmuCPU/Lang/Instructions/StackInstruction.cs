using System;
using EmuCPU.Lang.Operands;
using EmuCPU.Lang.Attributes;

namespace EmuCPU.Lang.Instructions {

	/// <summary>
	/// Инструкция для работы со стеком
	/// </summary>
	[AsmCompile("push pop", new Type[]{
		typeof(RegisterOperand)
	})]
	public class StackInstruction : Instruction {

		/// <summary>
		/// Прыжок
		/// </summary>
		/// <param name="interpreter">Интерпретатор</param>
		/// <param name="instruction">Инструкция</param>
		/// <param name="operands">Операнды</param>
		public override void Execute(Interpreter interpreter, string instruction, Operand[] operands) {

			int idx = (operands[0] as RegisterOperand).Index;
			if (instruction == "push") {
				// Добавление в стек
				interpreter.Stack.Push(interpreter.Registers[idx]);
			} else {
				// Удаление из стека
				if (idx == 7) {
					throw new Exception("Попытка записи в регистр чтения IN");
				}
				interpreter.Registers[idx] = interpreter.Stack.Pop();
			}
		}

	}
}
