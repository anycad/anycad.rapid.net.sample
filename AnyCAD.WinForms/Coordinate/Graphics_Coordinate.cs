using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_Coordinate : TestCase
    {
        public override void Run(IRenderView render)
        {
            MultiCoordinateSystemForm dlg = new MultiCoordinateSystemForm();
            dlg.ShowDialog();
            dlg = null;
        }
    }
}
