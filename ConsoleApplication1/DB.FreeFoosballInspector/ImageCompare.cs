using System;
using System.Drawing;
using System.IO;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace DB.FreeFoosballInspector
{

    public static class ImageComparer
    {
        // The file extension for the generated Bitmap files
        private const string BitMapExtension = ".bmp";
        public static int i = 0;
        private static bool _overrideTempFile = false;

        public static double GetSimilarity(string image, Bitmap targetImage, string filepath)
        {
            // Load images into bitmaps
            var imageOne = new Bitmap(image);

            var imageTwo = targetImage;

            var overlayImage = ChangePixelFormat(new Bitmap(imageOne), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            var template = ChangePixelFormat(new Bitmap(imageOne), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            var templFile = ChangePixelFormat(new Bitmap(imageTwo), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            var df = new ThresholdedDifference(90) {OverlayImage = overlayImage};

            var savedTemplate = SaveBitmapToFile(df.Apply(template), filepath, image, BitMapExtension);
            var savedTempFile = SaveBitmapToFile(df.Apply(templFile), filepath, "C:\\temp\\temp"+ (_overrideTempFile?i.ToString():"")+ ".bmp", BitMapExtension);
            i++;

            // Setup the AForge library
            var tm = new ExhaustiveTemplateMatching(0);

            // Process the images
            var results = tm.ProcessImage(savedTemplate, savedTempFile);

            // Compare the results, 0 indicates no match so return false
            if (results.Length <= 0)
            {
                return 0;
            }
            
            return results[0].Similarity;
        }

        /// <summary>
        /// Saves the bitmap automatic file.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="filepath">The filepath.</param>
        /// <param name="name">The name.</param>
        /// <param name="extension">The extension.</param>
        /// <returns>Bitmap image</returns>
        private static Bitmap SaveBitmapToFile(Bitmap image, string filepath, string name, string extension)
        {
            var savePath = string.Concat(filepath, "\\", Path.GetFileNameWithoutExtension(name), extension);

            image.Save(savePath, System.Drawing.Imaging.ImageFormat.Bmp);

            return image;
        }

        /// <summary>
        /// Change the pixel format of the bitmap image
        /// </summary>
        /// <param name="inputImage">Bitmapped image</param>
        /// <param name="newFormat">Bitmap format - 24bpp</param>
        /// <returns>Bitmap image</returns>
        private static Bitmap ChangePixelFormat(Bitmap inputImage, System.Drawing.Imaging.PixelFormat newFormat)
        {
            var img = inputImage.Clone(new Rectangle(0, 0, inputImage.Width, inputImage.Height), inputImage.PixelFormat);
            HistogramEqualization histogramEqualization = new HistogramEqualization();
            Mean smoothing = new Mean();
            
            histogramEqualization.ApplyInPlace(img);

            smoothing.ApplyInPlace(img);

            img = img.Clone(new Rectangle(0, 0, inputImage.Width, inputImage.Height), newFormat);

            return img;
        }
    }

}
