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
        private ImageSource _imageSource = new BitmapImage(new Uri("pack://application:,,,/FreeFoosball;component/Assets/foosball_busy.ico"));

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

        public Action CloseAction { get; set; }

        public ICommand CloseCommandProperty => new CloseCommand(CloseAction);

        private void OnInspectionAction(bool isFree)
        {
            BitmapImage image;
            string text;
             
            if (isFree)
            {
                image = new BitmapImage(new Uri("pack://application:,,,/FreeFoosball;component/Assets/foosball_available.ico"));
                text = Resources.Good;
            }
            else
            {
                image = new BitmapImage(new Uri("pack://application:,,,/FreeFoosball;component/Assets/foosball_busy.ico"));
                text = Resources.Bad;
            }

            image.Freeze();

            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                Text = text;
                IconSource = image;
            });
        }
    }
}
