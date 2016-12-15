using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmuCPU.Lang.Operands {

	/// <summary>
	/// Операнд-число
	/// </summary>
	public class NumberOperand : Operand {

		/// <summary>
		/// Содержимое
		/// </summary>
		public int Number { get; private set; }

		/// <summary>
		/// Конструктор операнда
		/// </summary>
		/// <param name="n">Число для записи</param>
		public NumberOperand(int n) {
			Number = n;
		}

		/// <summary>
		/// Конструктор с парсером из строки
		/// </summary>
		/// <param name="pn">Строка-число</param>
		public NumberOperand(string pn) {
			int n = 0;
			int.TryParse(pn, out n);
			Number = n;
		}

	}
}
