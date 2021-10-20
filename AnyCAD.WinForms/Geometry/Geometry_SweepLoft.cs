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
    class Geometry_SweepLoft : TestCase
    {
        public override void Run(RenderControl render)
        {
            var path = SketchBuilder.MakeArcOfCircle(new GPnt(0, 0, 0), new GPnt(10, 0, 10), new GPnt(5, 0, 8));
            var baseSketch = SketchBuilder.MakeRectangle(new GAx2(new GPnt(-5, -10, 0), GP.DZ(), GP.DX()), 10, 20, 2, false);
            var topSketch = SketchBuilder.MakeCircle(new GPnt(10, 0, 10), 5, GP.DX());

            var shapeList = new TopoShapeList();
            shapeList.Add(baseSketch);
            shapeList.Add(topSketch);
            var shape = FeatureTool.SweepByFrenet2(shapeList, path, EnumSweepTransitionMode.RoundCorner, true);

            render.ShowShape(shape, ColorTable.Beige);
        }
    }
}
