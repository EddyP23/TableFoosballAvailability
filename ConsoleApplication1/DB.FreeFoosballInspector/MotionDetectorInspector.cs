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
        private const double MotionPercentageThreshold = 0.001;
        private double _lastMotionPercentage;
        private Bitmap _image = null;
        private readonly MotionDetector _motionDetector;
        private readonly BlobCountingObjectsProcessing _objectsProcessing;
        private bool _isInitialEventSent = false;

        private const int CounterToPublishEvent = 5;
        private int _freeInARow = 0;
        private int _occupiedInARow = 0;

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
            if (IsFree())
            {
                _occupiedInARow = 0;
                if (_image != null)
                {
                    if (_freeInARow >= CounterToPublishEvent)
                    {
                        if (!_isPreviousFree || !_isInitialEventSent)
                        {
                            _isPreviousFree = true;
                            _isInitialEventSent = true;
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        public bool HasChangedToOccupied()
        {
            if (!IsFree())
            {
                _freeInARow = 0;
                if (_image != null)
                {
                    if (_occupiedInARow >= CounterToPublishEvent)
                    {
                        if (_isPreviousFree || !_isInitialEventSent)
                        {
                            _isPreviousFree = false;
                            _isInitialEventSent = true;
                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        private bool IsFree()
        {
            return _lastMotionPercentage <= MotionPercentageThreshold;
        }
        //int _i = 0;
        private void OnImageRetrieved(object sender, EventArgs args)
        {
            //var fakeProcessorResults = new int[]
            //{0, 0, 0, 0, 0, 0, 3,3,3,3,3,3,3,2,2,0,1,1,0,1,1,0,0,0,1};

            if (IsFree())
            {
                _freeInARow++;
            }
            if(!IsFree())
            {
                _occupiedInARow++;
            }
            Console.WriteLine($"f {_freeInARow} o {_occupiedInARow}");
            
            _image = ((ImageRetrievedEventArgs)args).Image;
            _image = _image.Crop(new Rectangle(725, 497, 117, 81));

            //_lastMotionPercentage = fakeProcessorResults[_i];
            //_i++;
            //if (_i > fakeProcessorResults.Length-1)
            //    _i = 0;

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
                }
            }

            new Bitmap(_image).Save(Path.Combine(Path.GetTempPath(), "temp.bmp"));

            Console.WriteLine(_lastMotionPercentage);
        }
    }
}
