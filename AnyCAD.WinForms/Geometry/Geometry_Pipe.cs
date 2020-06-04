using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Pipe : TestCase
    {

        public override void Run(RenderControl render)
        {
            var startPt = new GVec(0, 100, 0);
            var points = new GPntList();
            points.Add(new GPnt(startPt.XYZ()));
            points.Add(new GPnt(startPt.Added(new GVec(0, 0, 150)).XYZ()));
            points.Add(new GPnt(startPt.Added(new GVec(0, 100, 150)).XYZ()));
            points.Add(new GPnt(startPt.Added(new GVec(-100, 100, 150)).XYZ()));
            points.Add(new GPnt(startPt.Added(new GVec(-100, 300, 150)).XYZ()));
            points.Add(new GPnt(startPt.Added(new GVec(100, 300, 150)).XYZ()));
            TopoShape path = SketchBuilder.MakePolygon(points, false);

            var sectionList = new TopoShapeList();

            GAx2 coord1 = new GAx2(new GPnt(startPt.Added(new GVec(- 25, - 25, 0)).XYZ()), GP.DY());
            TopoShape section1 = SketchBuilder.MakeRectangle(coord1, 50, 50, 10, false);

            render.ShowShape(section1, Vector3.Red);
            render.ShowShape(path, Vector3.Green);

            TopoShape pipe = FeatureTool.SweepByFrenet(section1, path, EnumSweepTransitionMode.RoundCorner,
                false);

            render.ShowShape(pipe, Vector3.Blue);
        }
    }
}
