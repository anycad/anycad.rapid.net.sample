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
           var wires = CurveBuilder.ConnectEdgesToWires(shapes, 0.01, false);

            WireTreeBuilder wtb = new WireTreeBuilder();

            for(int i = 0; i<4 && i< wires.Count;  i++) 
            {
 
                wtb.AddWire(wires[i]);
            }
            wtb.Build();
            wtb.Normalize();

            var count = wtb.GetCount();
            for(uint ii=0; ii<count; ii++)
            {
               var wouter =  wtb.GetItem(ii);
                render.ShowShape(wouter, ColorTable.Red);
            }
        }
    }
}
