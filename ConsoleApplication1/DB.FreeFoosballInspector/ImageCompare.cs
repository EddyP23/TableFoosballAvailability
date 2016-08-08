using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace DB.FreeFoosballInspector
{
    public static class ImageComparer
    {
        // The file extension for the generated Bitmap files
        private const string BitMapExtension = ".bmp";

        public static double GetSimilarity(string image, Bitmap targetImage, string filepath)
        {
            // Load images into bitmaps
            var imageOne = new Bitmap(image);
            var imageTwo = targetImage;
            var overlayImage = ChangePixelFormat(new Bitmap(imageOne));
            var template = ChangePixelFormat(new Bitmap(imageOne));
            var templFile = ChangePixelFormat(new Bitmap(imageTwo));

            var df = new ThresholdedDifference(90) { OverlayImage = overlayImage };

            var savedTemplate = SaveBitmapToFile(df.Apply(template), filepath, image, BitMapExtension);
            var savedTempFile = SaveBitmapToFile(
                df.Apply(templFile),
                filepath,
                Path.Combine(filepath, "temp.bmp"),
                BitMapExtension);

            // Setup the AForge library
            var tm = new ExhaustiveTemplateMatching(0);

            // Process the images
            var results = tm.ProcessImage(savedTemplate, savedTempFile);

            // Compare the results, 0 indicates no match so return false
            return results.Length <= 0 ? 0 : results[0].Similarity;
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
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var savePath = Path.Combine(filepath, Path.GetFileNameWithoutExtension(name)) + extension;

            image.Save(savePath, ImageFormat.Bmp);

            return image;
        }

        /// <summary>
        /// Change the pixel format of the bitmap image
        /// </summary>
        /// <param name="inputImage">Bitmapped image</param>
        /// <param name="newFormat">Bitmap format - 24bpp</param>
        /// <returns>Bitmap image</returns>
        private static Bitmap ChangePixelFormat(Bitmap inputImage)
        {
            var img = inputImage.Clone(new Rectangle(0, 0, inputImage.Width, inputImage.Height), inputImage.PixelFormat);
            var histogramEqualization = new HistogramEqualization();
            var smoothing = new Mean();
            
            histogramEqualization.ApplyInPlace(img);
            smoothing.ApplyInPlace(img);

            img = img.Clone(new Rectangle(0, 0, inputImage.Width, inputImage.Height), PixelFormat.Format24bppRgb);

            return img;
        }
    }

}
