using System;
using System.Drawing;
using System.Threading;
using DB.FreeFoosballInspector;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var insp = new FreeFoosballInspector();
            insp.TableStatusChangedEvent += (sender, eventArgs) =>
            {
                if (((TableStatusChangedEventArgs) eventArgs).IsFree)
                {
                    Console.WriteLine("Table is free");
                }
                else
                {
                    Console.WriteLine("Table is ocuppied");
                }
            };
            insp.Start();


            Console.ReadKey();
            return;
        }
    }
}
