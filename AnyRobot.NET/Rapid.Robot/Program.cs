using System;
using System.Windows.Forms;

namespace RapidRobot
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AnyCAD.Foundation.GlobalInstance.Initialize();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            AnyCAD.Foundation.GlobalInstance.Destroy();
        }
    }
}
