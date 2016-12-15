using System;
using EmuCPU.Lang.Operands;
using EmuCPU.Lang.Attributes;

namespace EmuCPU.Lang.Instructions {

	/// <summary>
	/// Унарные операции регистра
	/// </summary>
	[AsmCompile("inc dec nul one neg abs sgn sqrt flr cel", new Type[]{
		typeof(RegisterOperand)
	})]
	public class UnaryInstruction : Instruction {

		/// <summary>
		/// Прыжок
		/// </summary>
		/// <param name="interpreter">Интерпретатор</param>
		/// <param name="instruction">Инструкция</param>
		/// <param name="operands">Операнды</param>
		public override void Execute(Interpreter interpreter, string instruction, Operand[] operands) {
			
			// Получение индекса регистра
			int idx = (operands[0] as RegisterOperand).Index;
			if (idx == 7) {
				throw new Exception("Попытка записи в регистр чтения IN");
			}
			
			switch (instruction) {

				case "inc":
					// Инкремент
					interpreter.Registers[idx] += 1f;
					break;
					
				case "dec":
					// Декремент
					interpreter.Registers[idx] -= 1f;
					break;

				case "nul":
					// Установка в 0
					interpreter.Registers[idx] = 0;
					break;

				case "one":
					// Установка в 1
					interpreter.Registers[idx] = 1f;
					break;

				case "neg":
					// Смена знака
					interpreter.Registers[idx] *= -1f;
					break;

				case "abs":
					// Модуль числа
					interpreter.Registers[idx] = Math.Abs(interpreter.Registers[idx]);
					break;

				case "sgn":
					// Знак числа
					interpreter.Registers[idx] = Math.Sign(interpreter.Registers[idx]);
					break;

				case "sqrt":
					// Извлечение квадратного корня
					interpreter.Registers[idx] = Math.Sqrt(interpreter.Registers[idx]);
					break;

				case "flr":
					// Округление вниз
					interpreter.Registers[idx] = Math.Floor(interpreter.Registers[idx]);
					break;

				case "cel":
					// Округление вверх
					interpreter.Registers[idx] = Math.Ceiling(interpreter.Registers[idx]);
					break;
			}
		}

	}
}
