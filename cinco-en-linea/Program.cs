﻿using System;
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
            if (PerformanceCounterCategory.Exists("iglosiggio.dbg"))
            {
                Logica.Explorador.Count = new PerformanceCounter();
                Logica.Explorador.Count.CategoryName = "iglosiggio.dbg";
                Logica.Explorador.Count.CounterName = "obj.count";
                Logica.Explorador.Count.InstanceName = String.Format("{0}/cinco-en-linea/Exploradores", Process.GetCurrentProcess().Id);
                Logica.Explorador.Count.ReadOnly = false;
                Logica.Explorador.Count.BeginInit();
                Logica.Explorador.Count.RawValue = 0;
                Logica.Explorador.Count.EndInit();
            }
			Application.Run (new Form1 ());
            Logica.Explorador.Count.Close();
		}
	}
}
