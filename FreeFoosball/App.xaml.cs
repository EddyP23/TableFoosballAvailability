using System.Windows;
using DB.FreeFoosballInspector;
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

            container.RegisterType<IFreeFoosballInspectionManager, FreeFoosballInspectionManager<MotionDetectingInspector>>();
            container.RegisterType<MainWindowViewModel>();

            container.Resolve<MainWindow>().ShowDialog();
            container.Dispose();
        }
    }
}
