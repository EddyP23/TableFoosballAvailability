using FreeFoosball.ViewModels;
using Resource = FreeFoosball.Properties.Resources;

namespace FreeFoosball
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            viewModel.CloseAction = Close;
            viewModel.NotificationAction = ShowNotificationBallooon;
            DataContext = viewModel;
        }

        private void ShowNotificationBallooon(bool bad)
        {
            TaskBarIconControl.ShowBalloonTip(
                Resource.BestApplication, bad ? Resource.Bad : Resource.Good, Resource.foosball);
        }
    }
}
