using System.Windows;
using FreeFoosball.ViewModels;
using Microsoft.Practices.Unity;

namespace FreeFoosball
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new UnityContainer();

            container.RegisterType<MainWindowViewModel>();

            container.Resolve<MainWindow>().ShowDialog();
            container.Dispose();
        }
    }
}
