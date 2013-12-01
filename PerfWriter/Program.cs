using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace PerfWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            PerformanceCounterCategory Cat = new PerformanceCounterCategory("iglosiggio.dbg");
            do
            {
                foreach (String Name in Cat.GetInstanceNames())
                {
                    PerformanceCounter Perf = Cat.GetCounters(Name)[0];
                    Console.WriteLine("{0} => {1}", Perf.InstanceName, Perf.RawValue);
                    System.Threading.Thread.Sleep(100);
                }
            } while (true);
        }
    }
}
