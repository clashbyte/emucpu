using System;
using CPUEmulator.Lang.Operands;
using CPUEmulator.Lang.Attributes;

namespace CPUEmulator.Lang.Instructions {

	/// <summary>
	/// Математические инструкции
	/// </summary>
	[AsmCompile("add sub mul div mod pow", new Type[]{ 
		typeof(RegisterOperand),
		typeof(RegisterOperand)
	})]
	public class MathInstruction : Instruction {

		/// <summary>
		/// Математические операции
		/// </summary>
		/// <param name="interpreter">Интерпретатор</param>
		/// <param name="instruction">Инструкция</param>
		/// <param name="operands">Операнды</param>
		public override void Execute(Interpreter interpreter, string instruction, Operand[] operands) {

			
			int idx = (operands[0] as RegisterOperand).Index;
			double val = interpreter.Registers[(operands[1] as RegisterOperand).Index];

			// Защита от записи в IN-регистр
			if (idx == 7) {
				throw new Exception("Попытка записи в регистр чтения IN");
			}

			switch (instruction) {

				case "add":
					// Сложение
					interpreter.Registers[idx] += val;
					break;

				case "sub":
					// Вычитание
					interpreter.Registers[idx] -= val;
					break;

				case "mul":
					// Умножение
					interpreter.Registers[idx] *= val;
					break;

				case "div":
					// Деление
					if (val == 0) {
						throw new Exception("Деление на ноль");
					}
					interpreter.Registers[idx] /= val;
					break;

				case "mod":
					// Остаток от деления
					if (val == 0) {
						throw new Exception("Деление на ноль");
					}
					interpreter.Registers[idx] %= val;
					break;

				case "pow":
					// Возведение в степень
					interpreter.Registers[idx] = Math.Pow(interpreter.Registers[idx], val);
					break;
			}

		}

	}
}
