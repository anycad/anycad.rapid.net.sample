using AnyCAD.Foundation;
using System.Xml.Linq;

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

                {
                    var feature = FeatureTool.SweepByFrenet(sketch, path, EnumSweepTransitionMode.RoundCorner, false, true, true);
                    render.ShowShape(feature, ColorTable.Blue);
                }

                {
                    var feature = FeatureTool.SweepByFrenet(sketch, path, EnumSweepTransitionMode.RoundCorner, true, false, false);
                    var node = render.ShowShape(feature, ColorTable.RoyalBlue);
                    node.SetTransform(Matrix4.makeTranslation(50, 50, 0));
                    node.RequstUpdate();
                }
                {
                    var feature2 = FeatureTool.Sweep(sketch, path, EnumGeomFillTrihedron.ConstantNormal);
                    var node = render.ShowShape(feature2, ColorTable.Green);
                    node.SetTransform(Matrix4.makeTranslation(-50,-50,0));
                    node.RequstUpdate();
                }


                render.RequestDraw();
            }

            //// 2. Revol
            //{
            //    var feature = FeatureTool.Revolve(sketch, new GAx1(new GPnt(-20, 0, 0), GP.DY()), 90);

            //    render.ShowShape(feature, ColorTable.Green);
            //}
            //// 3. Loft
            //{
            //    var baseWire = SketchBuilder.MakeRectangle(new GAx2(new GPnt(50, -50, 0), GP.DZ()), 20, 20, 5, false);
            //    var topWire = SketchBuilder.MakeCircle(new GPnt(60, -40, 40), 5, GP.DZ());

            //    var loft = FeatureTool.Loft(baseWire, topWire, true);
            //    render.ShowShape(loft, ColorTable.Red);
            //}

        }
    }
}
