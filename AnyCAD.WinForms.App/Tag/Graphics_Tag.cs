using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_Tag : TestCase
    {
        public override void Run(IRenderView render)
        {
            NewForm3D dlg = new NewForm3D(render.Scene);
            dlg.ShowDialog();
            dlg = null;
        }
    }
}
