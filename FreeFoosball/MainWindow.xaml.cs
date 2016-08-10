﻿using System;
using System.ComponentModel;
using System.Windows;
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

            viewModel.CloseAction = Application.Current.Shutdown;
            viewModel.NotificationAction = ShowNotificationBallooon;
            DataContext = viewModel;
        }

        private void ShowNotificationBallooon(bool bad)
        {
            TaskBarIconControl.ShowBalloonTip(
                Resource.BestApplication, bad ? Resource.Bad : Resource.Good, BalloonIcon.Info);
        }

        // minimize to system tray when applicaiton is minimized
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized) this.Hide();

            base.OnStateChanged(e);
        }

        // minimize to system tray when applicaiton is closed
        protected override void OnClosing(CancelEventArgs e)
        {
            // setting cancel to true will cancel the close request
            // so the application is not closed
            e.Cancel = true;

            this.Hide();

            base.OnClosing(e);
        }

    }

}
