using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_TangentLine : TestCase
    {
        public override void Run(IRenderView renderer)
        {
            var cirlce = SketchBuilder.MakeCircle(GP.Origin(), 30, GP.DZ());
            var pt = new GPnt(30, 1, 0);
            var line = SketchBuilder.MakeTangentLine(cirlce, pt, new GLin(pt, new GDir(1, 0, 0)), Math.PI / 2, 100);

            renderer.ShowShape(cirlce, ColorTable.Red);
            renderer.ShowShape(line, ColorTable.Green);
        }
    }
}
