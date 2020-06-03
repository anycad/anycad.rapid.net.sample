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
            GAx2 coord = new GAx2(new GPnt(startPt.Added(new GVec(-25, -25, 0)).XYZ()), GP.DZ());
            TopoShape section = SketchBuilder.MakeRectangle(coord, 50, 50, 10, false);

            render.ShowShape(section, Vector3.Red);
            render.ShowShape(path, Vector3.Green);
            
            TopoShape pipe = FeatureTool.Evolved(section, path, EnumGeomJoinType.Arc, true);

            render.ShowShape(pipe, Vector3.Blue);
        }
    }
}
