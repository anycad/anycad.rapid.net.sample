using Avalonia;
using System;

namespace AnyCAD.AvaloniaApp
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            AnyCAD.Foundation.GlobalInstance.Initialize();

            BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

            AnyCAD.Foundation.GlobalInstance.Destroy();
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}