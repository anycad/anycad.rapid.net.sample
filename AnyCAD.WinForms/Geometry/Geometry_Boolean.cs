using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Boolean : TestCase
    {
        public override void Run(IRenderView render)
        {
            var box = ShapeBuilder.MakeBox(GP.XOY(), 10, 10, 10);
            var sphere = ShapeBuilder.MakeSphere(GP.Origin(), 5);

            render.ShowShape(box, ColorTable.Red);
            render.ShowShape(sphere, ColorTable.Blue);

            var common = BooleanTool.Common(box, sphere);
            render.ShowShape(common, ColorTable.Green).SetTransform(Matrix4.makeTranslation(-20, 0, 0));

            var cut = BooleanTool.Cut(box, sphere);
            render.ShowShape(cut, ColorTable.Honeydew).SetTransform(Matrix4.makeTranslation(0, 20, 0));

            var cut2 = BooleanTool.Cut(sphere, box);
            render.ShowShape(cut2, ColorTable.Violet).SetTransform(Matrix4.makeTranslation(0, -20, 0));

            var fuse = BooleanTool.Fuse(box, sphere);
            render.ShowShape(fuse, ColorTable.LightYellow).SetTransform(Matrix4.makeTranslation(20, 0, 0));


        }
    }
}
