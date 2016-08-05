using System;
using FreeFoosball.ViewModels;

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
            DataContext = viewModel;
        }
    }
}
