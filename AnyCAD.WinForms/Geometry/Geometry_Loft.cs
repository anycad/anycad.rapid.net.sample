using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Loft : TestCase
    {
        public override void Run(RenderControl render)
        {
            var baseSketch = SketchBuilder.MakeRectangle(GP.XOY(), 10, 20, 2, false);
            
            // project to plane
            var topSketch = ProjectionTool.ProjectOnPlane(baseSketch, new GPnt(0, 0, 30), new GDir(0,-0.5,  0.5), GP.DZ());

            // make loft
            var shape =  FeatureTool.Loft(baseSketch, topSketch, true);

            render.ShowShape(shape, Vector3.Green);
        }
    }
}
