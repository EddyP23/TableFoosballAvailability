using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication1.Helpers;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string tempFilePath = AppDomain.CurrentDomain.BaseDirectory + "/Images/temp.bmp";

            string template = AppDomain.CurrentDomain.BaseDirectory + "/Images/template.jpg";
            
            var dirPath = "C:\\temp";

            int i = 1;
            while (true)
            {
                var gotImage = BitmapHelper.FromUrl("http://10.32.244.12/record/current.jpg");
                BitmapHelper.CropImage(gotImage, new Rectangle(725, 497, 117, 81)).Save(tempFilePath+i.ToString());

                var similarity = ImageComparer.GetSimilarity(template, tempFilePath+i.ToString(), dirPath);
                Console.WriteLine($"[{i}] Similarity is {similarity}");
                Thread.Sleep(1000);
                i++;
            }


            Console.WriteLine("End of comparison.");
            Console.ReadKey();
            return;
        }
    }
}
