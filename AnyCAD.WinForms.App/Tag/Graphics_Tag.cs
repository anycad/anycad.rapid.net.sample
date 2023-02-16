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
    class Graphics_Tag : TestCase
    {
        public override void Run(RenderControl render)
        {
            NewForm3D dlg = new NewForm3D(render.GetScene());
            dlg.ShowDialog();
            dlg = null;
        }
    }
}
