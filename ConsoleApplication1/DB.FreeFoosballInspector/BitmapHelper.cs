using System.Drawing;
using System.Net;

namespace DB.FreeFoosballInspector
{
    public static class BitmapHelper
    {
        public static Bitmap FromUrl(string url)
        {
            var request = WebRequest.CreateHttp("http://10.32.244.12/record/current.jpg?rand=272954");
            
            request.UserAgent =
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                return (Bitmap)Image.FromStream(stream);
            }
        }

        public static Bitmap Crop(this Bitmap img, Rectangle cropArea)
        {
            var bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

    }
}
