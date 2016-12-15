using System;
using EmuCPU.Lang.Operands;
using EmuCPU.Lang.Attributes;

namespace EmuCPU.Lang.Instructions {

	/// <summary>
	/// Инструкции сравнения
	/// </summary>
	[AsmCompile("eq neq grt lss geq leq and or", new Type[]{ 
		typeof(RegisterOperand),
		typeof(RegisterOperand)
	})]
	public class CompareInstruction : Instruction {

		/// <summary>
		/// Инструкции сравнения
		/// </summary>
		/// <param name="interpreter">Интерпретатор</param>
		/// <param name="instruction">Инструкция</param>
		/// <param name="operands">Операнды</param>
		public override void Execute(Interpreter interpreter, string instruction, Operand[] operands) {

			// Получение значений
			double val1 = interpreter.Registers[(operands[0] as RegisterOperand).Index];
			double val2 = interpreter.Registers[(operands[1] as RegisterOperand).Index];
			bool state = false;

			switch (instruction) {

				case "eq":
					// Равно
					state = val1 == val2;
					break;

				case "neq":
					// Не равно
					state = val1 != val2;
					break;

				case "grt":
					// Больше
					state = val1 > val2;
					break;

				case "lss":
					// Меньше
					state = val1 < val2;
					break;

				case "geq":
					// Больше или равно
					state = val1 >= val2;
					break;

				case "leq":
					// Меньше или равно
					state = val1 <= val2;
					break;

				case "and":
					// И
					state = (val1 > 0) && (val2 > 0);
					break;

				case "or":
					// Или
					state = (val1 > 0) || (val2 > 0);
					break;
			}

			// Запись в регистр
			interpreter.Registers[0] = state ? 1 : 0;
		}

	}
}
