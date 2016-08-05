using System;
using System.Drawing;

namespace DB.FreeFoosballInspector
{
    public class ExhaustiveTemplateMatchingInspector: IFreeFoosballInspector
    {
        private bool _isPreviousFree = false;
        private const double SimilarityThreshold = 0.90;
        private double _lastSimilarity;
        private bool _isInitialEventSent = false;
        private Bitmap _image = null;
        
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
            string template = AppDomain.CurrentDomain.BaseDirectory + "/Images/template.jpg";

            var dirPath = "C:\\temp";

            var im2 = BitmapHelper.CropImage(_image, new Rectangle(725, 497, 117, 81));

            var similarity = ImageComparer.GetSimilarity(template, im2, dirPath);
            Console.WriteLine(similarity);
            return similarity;
        }
    }
}
