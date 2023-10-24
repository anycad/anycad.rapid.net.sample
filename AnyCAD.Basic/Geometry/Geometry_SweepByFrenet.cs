using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_SweepByFrenet : TestCase
    {
        public void Sweep(IRenderView render)
        {
            var path2 = ShapeIO.Open(GetResourcePath("sweep/path_pl.igs"));
            render.ShowShape(path2, ColorTable.Red);
            var rectShp = SketchBuilder.MakeRectangle(GP.YOZ(), 0.2, 0.1, 0, false);
            rectShp = TransformTool.Translate(rectShp, new GVec(0.0, 5.0, 0.0));
            render.ShowShape(rectShp, ColorTable.Blue);
            //var topshp3 = FeatureTool.SweepByFrenet(rectShp, path2, EnumSweepTransitionMode.RoundCorner, true, false, false);
            //render.ShowShape(topshp3, ColorTable.Red);
        }

        public override void Run(IRenderView render)
        {
            Sweep(render);
            return;
            var path = ShapeIO.Open(GetResourcePath("sweep/path.igs"));
            var spline = ShapeIO.Open(GetResourcePath("sweep/spline.igs"));

            var shape = FeatureTool.SweepByFrenet(spline, path, EnumSweepTransitionMode.Transformed, false, false, false);

            render.ShowShape(shape, ColorTable.Red);

            var cylinder = ShapeIO.Open(GetResourcePath("sweep/Cylinder.igs"));

            render.ShowShape(cylinder, ColorTable.Blue);
        }
    }
}
