using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Form2010
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AnyCAD.Foundation.GlobalInstance.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            AnyCAD.Foundation.GlobalInstance.Destroy();
        }
    }
}
