using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmuCPU.Lang.Operands {

	/// <summary>
	/// Операнд-регистр
	/// </summary>
	public class RegisterOperand : Operand {

		/// <summary>
		/// Индекс регистра
		/// </summary>
		public int Index { get; private set; }

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="idx">Индекс регистра</param>
		public RegisterOperand(int idx) {
			Index = idx;
		}

	}
}
