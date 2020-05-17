using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Sketch : TestCase
    {
        public override void Run(RenderControl render)
        {
            var line = SketchBuilder.MakeLine(new GPnt(10, 10, 10), new GPnt(-10, -10, -10));
            render.ShowShape(line, new Vector3(1, 1, 0));
            
            var ellips = SketchBuilder.MakeEllipse(GP.Origin(), 10, 5, GP.DX(), GP.DZ());
            render.ShowShape(ellips, Vector3.Blue);

            var circle = SketchBuilder.MakeCircle(GP.Origin(), 4, new GDir(0, 1, 0));
            render.ShowShape(circle, Vector3.Green);

            var arc = SketchBuilder.MakeArcOfCircle(GP.Origin(), new GPnt(50, 0, 50), new GPnt(20, 0, 40));
            render.ShowShape(arc, Vector3.Red);

            var rect = SketchBuilder.MakeRectangle(GP.XOY(), 30, 40, 5, false);
            render.ShowShape(rect, Vector3.Green);
        }
    }
}
