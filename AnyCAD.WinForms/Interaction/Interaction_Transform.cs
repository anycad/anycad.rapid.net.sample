using AnyCAD.Forms;

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
