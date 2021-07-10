using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Transform : TestCase
    {
        public override void Run(RenderControl render)
        {
            var box = ShapeBuilder.MakeBox(GP.XOY(), 10, 20, 30);
            render.ShowShape(box, ColorTable.Green);

            var trans = TransformTool.Translate(box, new GVec(-20, 0, 0));
            render.ShowShape(trans, new Vector3(105.0f/256.0f));

            var scale = TransformTool.Scale(box, GP.Origin(), 0.5);
            render.ShowShape(scale, ColorTable.Blue);

            var rotate = TransformTool.Rotation(box, GP.OX(), 45);
            render.ShowShape(rotate, ColorTable.Red);

        }
    }
}
