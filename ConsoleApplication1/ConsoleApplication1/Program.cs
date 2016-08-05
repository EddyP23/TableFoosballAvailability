using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            //lala
=======
            // Filepath to the image directory
            const string dirpath = @"C:\Temp\";

            // The threshold is the minimal acceptable similarity between template candidate. 
            // Min (loose) is 0.0 Max (strict) is 1.0
            const float similarityThreshold = 0.50f;

            string testImageOne = AppDomain.CurrentDomain.BaseDirectory + "/Images/template.jpg";
            string testImageTwo = AppDomain.CurrentDomain.BaseDirectory + "/Images/test1.jpg";

            // Comparison level is initially set to 0.95
            // Increment loop in steps of .01
            for (var compareLevel = 0.5; compareLevel <= 1.00; compareLevel += 0.10)
            {
                // Run the tests
                var testTwo = ImageComparer.CompareImages(testImageOne, testImageTwo, compareLevel, dirpath, similarityThreshold);
                
                // Output the results
                Console.WriteLine("Test images for similarities at compareLevel: {0}", compareLevel);
                Console.WriteLine("Results for Image 1 compared to Image 2 - Expected: True : Actual {0}", testTwo);
               
            }

            Console.WriteLine("End of comparison.");
            Console.ReadKey();
>>>>>>> 9e1fc6d8da8389163b119f0622d4ef7cc270d871
        }
    }
}
