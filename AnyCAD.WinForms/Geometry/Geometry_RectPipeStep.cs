using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_RectPipeStep : TestCase
    {
        public override void Run(RenderControl render)
        {
           var rect1 =  SketchBuilder.MakeRectangle(GP.XOY(), 10, 5, 1, true);
           var rect2 = SketchBuilder.MakeRectangle(new GAx2(new GPnt(1,1,0), GP.DZ(), GP.DX()), 8, 3, 1, true);

            var cut = BooleanTool.Cut(rect1, rect2);

            var extrude = FeatureTool.Extrude(cut, 100, GP.DZ());

            render.ShowShape(extrude, Vector3.Blue);
        }
    }
}
