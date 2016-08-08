using System.Drawing;
using System.Net;
using AForge.Imaging.Filters;

namespace DB.FreeFoosballInspector
{
    public static class BitmapHelper
    {
        public static Bitmap FromUrl(string url)
        {
            var request = WebRequest.CreateHttp(url);
            
            request.UserAgent =
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                return (Bitmap)Image.FromStream(stream);
            }
        }

        public static Bitmap Crop(this Bitmap bmp, Rectangle cropRectangle)
        {
            Crop filter = new Crop(cropRectangle);

            return filter.Apply(bmp);
        }

        public static Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

    }
}
