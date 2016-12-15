using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPUEmulator.Lang.Attributes {

	/// <summary>
	/// Атрибут для компиляции
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class AsmCompileAttribute : Attribute {

		/// <summary>
		/// Ключи, связанные с данной инструкцией
		/// </summary>
		public string[] Keys {
			get;
			private set;
		}

		/// <summary>
		/// Операнды для инструкции
		/// </summary>
		public Type[] Operands {
			get;
			private set;
		}

		/// <summary>
		/// Конструктор аттрибута
		/// </summary>
		/// <param name="keys">Ключи для связи с данной инструкцией</param>
		/// <param name="operands">Операнды для данной инструкции</param>
		public AsmCompileAttribute(string keys, Type[] operands) {
			Keys = keys.Split(' ');
			Operands = operands;
		}
	}
}
