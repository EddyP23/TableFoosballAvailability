using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DB.FreeFoosballInspector;
using FreeFoosball.Commands;
using Prism.Mvvm;
using System.Windows.Threading;
using FreeFoosball.Properties;

namespace FreeFoosball.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _text = Resources.Bad;
        private ImageSource _imageSource = new BitmapImage(new Uri("pack://application:,,,/FreeFoosball;component/Assets/foosball_busy2.ico"));

        public MainWindowViewModel(IFreeFoosballInspectionManager inspectionManager)
        {
            inspectionManager.Configure(OnInspectionAction).StartInspection();
        }

        public ImageSource IconSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public string Title => Resources.BestApplication;

        public Action CloseAction { get; set; }

        public Action<bool> NotificationAction { get; set; }

        public ICommand CloseCommandProperty => new CloseCommand(CloseAction);

        private void OnInspectionAction(bool isFree)
        {
            BitmapImage image;
            string text;
             
            if (isFree)
            {
                image = new BitmapImage(new Uri("pack://application:,,,/FreeFoosball;component/Assets/foosball_available2.ico"));
                text = Resources.Available;
            }
            else
            {
                image = new BitmapImage(new Uri("pack://application:,,,/FreeFoosball;component/Assets/foosball_busy2.ico"));
                text = Resources.Busy;
            }

            image.Freeze();

            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                Text = text;
                IconSource = image;
            });

            NotificationAction?.Invoke(!isFree);
        }
    }
}
