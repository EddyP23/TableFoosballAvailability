using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DB.FreeFoosballInspector;
using FreeFoosball.Commands;
using Prism.Mvvm;
using System.Windows.Threading;

namespace FreeFoosball.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private const string Good = "Quickly run!!! It is your chance now!";
        private const string Bad = "Sorry. It is not your time to leave your work.";

        private string _text = Bad;
        private ImageSource _imageSource = new BitmapImage(new Uri("pack://application:,,,/FreeFoosball;component/Assets/busy.ico"));
        private readonly FreeFoosballInspector<ExhaustiveTemplateMatchingInspector> _inspector = new FreeFoosballInspector<ExhaustiveTemplateMatchingInspector>();

        public MainWindowViewModel()
        {
            _inspector.TableStatusChangedEvent += (sender, eventArgs) =>
            {
                if (((TableStatusChangedEventArgs)eventArgs).IsFree)
                {
                    var bi = new BitmapImage(new Uri("pack://application:,,,/FreeFoosball;component/Assets/available.ico"));
                    bi.Freeze();

                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                        Text = Good;
                        IconSource = bi;
                    });
                }
                else
                {
                    var bi = new BitmapImage(new Uri("pack://application:,,,/FreeFoosball;component/Assets/busy.ico"));
                    bi.Freeze();

                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                        Text = Bad;
                        IconSource = bi;
                    });
                }
            };
            _inspector.Start();
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
    }
}
