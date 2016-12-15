using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CPUEmulator.Lang;

namespace CPUEmulator {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Compiler.SeekInstructions();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Forms.MainForm());
		}
	}
}
