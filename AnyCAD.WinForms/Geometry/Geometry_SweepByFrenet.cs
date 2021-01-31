using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_SweepByFrenet : TestCase
    {
        public override void Run(RenderControl render)
        {
            var path = ShapeIO.Open(GetResourcePath("sweep/path.igs"));
            var spline = ShapeIO.Open(GetResourcePath("sweep/spline.igs"));

            var shape = FeatureTool.SweepByFrenet(spline, path, EnumSweepTransitionMode.Transformed, false, false, false);

            render.ShowShape(shape, Vector3.Red);

            var cylinder = ShapeIO.Open(GetResourcePath("sweep/Cylinder.igs"));

            render.ShowShape(cylinder, Vector3.Blue);
        }
    }
}
