using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_SweepByFrenet : TestCase
    {
        public override void Run(IRenderView render)
        {
            var path = ShapeIO.Open(GetResourcePath("sweep/path.igs"));
            var spline = ShapeIO.Open(GetResourcePath("sweep/spline.igs"));

            var shape = FeatureTool.SweepByFrenet(spline, path, EnumSweepTransitionMode.Transformed, false, false, false);

            render.ShowShape(shape, ColorTable.Red);

            var cylinder = ShapeIO.Open(GetResourcePath("sweep/Cylinder.igs"));

            render.ShowShape(cylinder, ColorTable.Blue);
        }
    }
}
