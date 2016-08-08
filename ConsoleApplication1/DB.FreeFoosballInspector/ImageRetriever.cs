using System;
using System.Drawing;
using System.Timers;

namespace DB.FreeFoosballInspector
{
    public class ImageRetriever
    {
        public event EventHandler ImageRetrievedEvent;

        public void Start()
        {
            var timer = new Timer(1000);
            timer.Elapsed += (sender, args) =>
            {
                var img = BitmapHelper.FromUrl("http://10.32.244.12/record/current.jpg");
                ImageRetrievedEvent?.Invoke(this, new ImageRetrievedEventArgs { Image = img });
            };
            timer.Start();
        }
    }

    public class ImageRetrievedEventArgs : EventArgs
    {
        public Bitmap Image { get; set; }
    }
}
