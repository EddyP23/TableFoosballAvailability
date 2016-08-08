using System;
using DB.FreeFoosballInspector;

namespace ConsoleApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var insp = new FreeFoosballInspectionManager<MotionDetectingInspector>();

            insp.Configure(s => Console.WriteLine(s ? "Table is free" : "Table is ocuppied")).StartInspection();

            Console.ReadKey();
        }
    }
}
