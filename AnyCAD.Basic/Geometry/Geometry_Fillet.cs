using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Fillet : TestCase
    {
        public override void Run(IRenderView render)
        {
            var shape1 = ShapeBuilder.MakeBox(GP.XOY(), 100, 100, 100);

            var fillet = FeatureTool.Fillet(shape1, new Uint32List(new uint[] { 5, 4 }), new DoubleList(new double[] { 5, 5 }));

            render.ShowShape(fillet, ColorTable.Aqua);
        }
    }
}
