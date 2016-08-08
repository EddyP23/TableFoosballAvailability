using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AForge.Vision.Motion;

namespace DB.FreeFoosballInspector
{
    public class MotionDetectingInspector : IFreeFoosballInspector
    {
        private bool _isPreviousFree = false;
        private const double MotionPercentageThreshold = 0.005;
        private double _lastMotionPercentage;
        private bool _isInitialEventSent = false;
        private Bitmap _image = null;
        private readonly MotionDetector _motionDetector;
        readonly BlobCountingObjectsProcessing _objectsProcessing;

        public MotionDetectingInspector()
        {
            _objectsProcessing = new BlobCountingObjectsProcessing()
            {
                HighlightColor = Color.Red,
                HighlightMotionRegions = true,
                MinObjectsHeight = 5,
                MinObjectsWidth = 5
            };

            _motionDetector = new MotionDetector(new TwoFramesDifferenceDetector(true) { }, _objectsProcessing);

            var imageRetriever = new ImageRetriever();
            imageRetriever.ImageRetrievedEvent += OnImageRetrieved;

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
            return _lastMotionPercentage < MotionPercentageThreshold;
        }

        private void OnImageRetrieved(object sender, EventArgs args)
        {
            _image = ((ImageRetrievedEventArgs)args).Image;
            _image = _image.Crop(new Rectangle(725, 497, 117, 81));
            _lastMotionPercentage = _motionDetector.ProcessFrame(new Bitmap(_image).LockBits(new Rectangle(0, 0, _image.Width, _image.Height),
                ImageLockMode.ReadWrite, _image.PixelFormat));
            if (_objectsProcessing.ObjectsCount > 0)
            {
                using (Graphics g = Graphics.FromImage(_image))
                {
                    using (Pen p = new Pen(Color.Red))
                    {
                        g.DrawRectangles(p, _objectsProcessing.ObjectRectangles);
                    }
                };


            }

            new Bitmap(_image).Save(Path.Combine(Path.GetTempPath(), "temp.bmp"));

            Console.WriteLine(_lastMotionPercentage);
        }
    }
}
