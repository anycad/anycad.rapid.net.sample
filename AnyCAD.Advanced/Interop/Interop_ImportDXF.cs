using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Interop_ImportDXF : TestCase
    {
        public override void Run(IRenderView render)
        {
           var fileName = DialogUtil.OpenFileDialog("DXF", new StringList { "DXF Files(.dxf)", "*.dxf"});
            if (fileName.IsEmpty())
                return;

           var shapes = DxfIO.Load(fileName.GetString());
           foreach (var shape in shapes)
            {
                render.ShowShape(shape, ColorTable.Red);
            }
        }
    }
}
