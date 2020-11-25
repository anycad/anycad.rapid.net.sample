using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Boolean : TestCase
    {
        public override void Run(RenderControl render)
        {
            var box = ShapeBuilder.MakeBox(GP.XOY(), 10, 10, 10);
            var sphere = ShapeBuilder.MakeSphere(GP.Origin(), 5);

            render.ShowShape(box, Vector3.Red);
            render.ShowShape(sphere, Vector3.Blue);

            var common = BooleanTool.Common(box, sphere);
            render.ShowShape(common, Matrix4.makeTranslation(-20,0,0));

            var cut = BooleanTool.Cut(box, sphere);
            render.ShowShape(cut, Matrix4.makeTranslation(0, 20, 0));

            var cut2 = BooleanTool.Cut(sphere, box);
            render.ShowShape(cut2, Matrix4.makeTranslation(0, -20,0));

            var fuse = BooleanTool.Fuse(box, sphere);
            render.ShowShape(fuse, Matrix4.makeTranslation(20, 0, 0));


        }
    }
}
