using System.Windows;

namespace AnyCAD.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AnyCAD.Foundation.GlobalInstance.Initialize();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AnyCAD.Foundation.GlobalInstance.Destroy();
        }
    }
}
