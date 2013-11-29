using System;
using System.Diagnostics;

namespace PerfcountInstaller
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (!PerformanceCounterCategory.Exists ("dbg@iglosiggio")) {
				CounterCreationDataCollection Data = new CounterCreationDataCollection ();
				Data.Add (
					new CounterCreationData (
					"Object Count",
					"Indica la cantidad de objetos de cierto tipo",
					PerformanceCounterType.NumberOfItems64
				)
				);
				PerformanceCounterCategory.Create (
					"dbg@iglosiggio",
					"Datos varios para mis programas",
					PerformanceCounterCategoryType.MultiInstance,
					Data
				);
				Console.WriteLine("Hola mundo");
			} else {
				PerformanceCounterCategory.Delete ("dbg@iglosiggio");
				Console.WriteLine("Chau chau adios (8)");
			}
		}
	}
}
