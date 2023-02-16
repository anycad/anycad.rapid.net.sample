using AnyCAD.Forms;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_Coordinate : TestCase
    {
        public override void Run(RenderControl render)
        {
            MultiCoordinateSystemForm dlg = new MultiCoordinateSystemForm();
            dlg.ShowDialog();
            dlg = null;
        }
    }
}
