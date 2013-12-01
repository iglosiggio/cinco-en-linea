using System;
using System.Diagnostics;

namespace PerfcountInstaller
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (!PerformanceCounterCategory.Exists ("iglosiggio.dbg")) {
				CounterCreationDataCollection Data = new CounterCreationDataCollection ();
				Data.Add (
					new CounterCreationData (
					"obj.count",
					"Indica la cantidad de objetos de cierto tipo",
					PerformanceCounterType.NumberOfItems32
				)
				);
				PerformanceCounterCategory.Create (
					"iglosiggio.dbg",
					"Datos varios para mis programas",
					PerformanceCounterCategoryType.MultiInstance,
					Data
				);
				Console.WriteLine("Hola mundo");
			} else {
				PerformanceCounterCategory.Delete ("iglosiggio.dbg");
				Console.WriteLine("Chau chau adios (8)");
			}
            Console.ReadKey();
		}
	}
}
