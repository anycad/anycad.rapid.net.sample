namespace AnyCAD.Demo
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AnyCAD.Foundation.GlobalInstance.Initialize();
            AnyCAD.Foundation.DocumentIO.Initialize();
            Application.Run(new MainForm());
            AnyCAD.Foundation.GlobalInstance.Destroy();
        }
    }
}