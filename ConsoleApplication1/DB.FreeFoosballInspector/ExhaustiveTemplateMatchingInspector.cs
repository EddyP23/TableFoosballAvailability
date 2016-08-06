using System;
using System.Drawing;
using System.IO;

namespace DB.FreeFoosballInspector
{
    public class ExhaustiveTemplateMatchingInspector : IFreeFoosballInspector
    {
        private const double SimilarityThreshold = 0.80;
        private static readonly string TemplateFile = AppDomain.CurrentDomain.BaseDirectory + "/Images/template.jpg";
        private static readonly Rectangle CropRctangle = new Rectangle(725, 497, 117, 81);

        private bool _isPreviousFree;
        private double _lastSimilarity;
        private bool _isInitialEventSent;
        private Bitmap _image;

        public ExhaustiveTemplateMatchingInspector()
        {
            var imageRetriever = new ImageRetriever();
            imageRetriever.ImageRetrievedEvent += (sender, args) =>
            {
                _image = ((ImageRetrievedEventArgs)args).Image;

                _lastSimilarity = GetSimilarity();

            };
            imageRetriever.Start();
        }

        public bool HasChangedToFree()
        {
            if (IsFree() && (!_isPreviousFree || !_isInitialEventSent) && _image != null)
            {
                _isInitialEventSent = true;
                _isPreviousFree = true;
                 return true;
            }
            return false;
        }

        public bool HasChangedToOccupied()
        {
            if (!IsFree() && (_isPreviousFree || !_isInitialEventSent) && _image != null)
            {
                _isInitialEventSent = true;

                _isPreviousFree = false;
                return true;
            }
            return false;
        }

        private bool IsFree()
        {
            return _lastSimilarity > SimilarityThreshold;
        }
        
        private double GetSimilarity()
        {
            var dirPath = Path.GetTempPath();
            var im2 = _image.Crop(CropRctangle);

            var similarity = ImageComparer.GetSimilarity(TemplateFile, im2, dirPath);
            Console.WriteLine(similarity);
            return similarity;
        }
    }
}
