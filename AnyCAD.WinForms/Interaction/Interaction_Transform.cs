using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Interaction_Transform : TestCase
    {
        public override void Run(IRenderView render)
        {
            var dlg = new TransformDlg();
            dlg.ShowDialog();
        }
    }
}
