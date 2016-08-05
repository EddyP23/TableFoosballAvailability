using System;
using System.Drawing;

namespace DB.FreeFoosballInspector
{
    public class FreeFoosballInspector
    {
        private bool _isFree = false;
        private const double SimilarityThreshold = 90;
        
        public event EventHandler TableStatusChangedEvent;

        public void Start()
        {
            var timer = new System.Timers.Timer(1000);

            timer.Elapsed += (sender, args) =>
            {
                CheckIfStatusChangedAndInvokeEvent();
            };
            OnStartUpInvokeEvent();
            timer.Start();
        }

        private void OnStartUpInvokeEvent()
        {
            var sim = GetSimilarity();
            if (sim > SimilarityThreshold)
            {
                _isFree = true;
                TableStatusChangedEvent?.Invoke(this, new TableStatusChangedEventArgs() { IsFree = _isFree });
            }
            if (sim < SimilarityThreshold)
            {
                _isFree = false;
                TableStatusChangedEvent?.Invoke(this, new TableStatusChangedEventArgs() { IsFree = _isFree });
            }
        }

        private void CheckIfStatusChangedAndInvokeEvent()
        {
            var sim = GetSimilarity();
            if (sim > SimilarityThreshold && !_isFree)
            {
                _isFree = true;
                TableStatusChangedEvent?.Invoke(this, new TableStatusChangedEventArgs() { IsFree = _isFree });
            }

            if (sim < SimilarityThreshold && _isFree)
            {
                _isFree = false;
                TableStatusChangedEvent?.Invoke(this, new TableStatusChangedEventArgs() { IsFree = _isFree });
            }
        }

        private double GetSimilarity()
        {
            string template = AppDomain.CurrentDomain.BaseDirectory + "/Images/template.jpg";

            var dirPath = "C:\\temp";

            var gotImage = BitmapHelper.FromUrl("http://10.32.244.12/record/current.jpg");
            var im2 = BitmapHelper.CropImage(gotImage, new Rectangle(725, 497, 117, 81));

            var similarity = ImageComparer.GetSimilarity(template, im2, dirPath);
            return similarity;
        }
    }
}
