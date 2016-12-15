using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmuCPU.Lang.Operands {

	/// <summary>
	/// Операнд-адрес
	/// </summary>
	public class AddressOperand : Operand {

		/// <summary>
		/// Указанный адрес
		/// </summary>
		public int Index { get; private set; }

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="idx">Индекс команды для прыжка</param>
		public AddressOperand(int idx) {
			Index = idx;
		}

	}
}
