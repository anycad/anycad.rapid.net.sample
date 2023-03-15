using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Sweep : TestCase
    {
        public override void Run(IRenderView render)
        {
            var sketch = SketchBuilder.MakeEllipse(GP.Origin(), 5, 3, GP.DX(), GP.DZ());

            // 1. Sweep
            {
                GPntList points = new GPntList { GP.Origin(), new GPnt(20, 10, 30), new GPnt(50, 50, 50), };
                var path = SketchBuilder.MakeBSpline(points);
                render.ShowShape(path, ColorTable.Green);

                var feature = FeatureTool.Sweep(sketch, path, EnumGeomFillTrihedron.ConstantNormal);
                render.ShowShape(feature, ColorTable.Blue);
            }

            // 2. Revol
            {
                var feature = FeatureTool.Revolve(sketch, new GAx1(new GPnt(-20, 0, 0), GP.DY()), 90);

                render.ShowShape(feature, ColorTable.Green);
            }
            // 3. Loft
            {
                var baseWire = SketchBuilder.MakeRectangle(new GAx2(new GPnt(50, -50, 0), GP.DZ()), 20, 20, 5, false);
                var topWire = SketchBuilder.MakeCircle(new GPnt(60, -40, 40), 5, GP.DZ());

                var loft = FeatureTool.Loft(baseWire, topWire, true);
                render.ShowShape(loft, ColorTable.Red);
            }
            
        }
    }
}
