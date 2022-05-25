using AnyCAD.Forms;
using AnyCAD.Foundation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyCAD.Demo.Graphics
{
    class Interaction_Transform : TestCase
    {
        public override void Run(RenderControl render)
        {
            var dlg = new TransformDlg();
            dlg.ShowDialog();
        }
    }
}
