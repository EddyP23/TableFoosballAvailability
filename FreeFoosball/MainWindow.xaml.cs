using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FreeFoosball.ViewModels;
using Hardcodet.Wpf.TaskbarNotification;
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

            viewModel.NotificationAction = ShowNotificationBallooon;
            DataContext = viewModel;
        }


        private void ShowNotificationBallooon(bool bad)
        {
            TaskBarIconControl.ShowBalloonTip(
                Resource.BestApplication, bad ? Resource.Bad : Resource.Good, BalloonIcon.Info);
        }
    }
}
