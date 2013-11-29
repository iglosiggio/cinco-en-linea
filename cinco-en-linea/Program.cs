using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace cinco_en_linea
{
	static class Program
	{
		/// <summary>
		/// Punto de entrada principal para la aplicación.
		/// </summary>
		[STAThread]
		static void Main ()
		{
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault (false);
			if(PerformanceCounterCategory.Exists("dbg@iglosiggio"))
				Logica.Explorador.Count = new PerformanceCounter(
					"dbg@iglosiggio",
					"Object Count",
					String.Format ("{0}/cinco-en-linea/Exploradores", Process.GetCurrentProcess().Id),
					false
				);
			Application.Run (new Form1 ());
		}
	}
}
