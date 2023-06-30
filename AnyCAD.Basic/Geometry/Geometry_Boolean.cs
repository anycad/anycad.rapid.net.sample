using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Boolean : TestCase
    {
        public override void Run(IRenderView render)
        {
            var box = ShapeBuilder.MakeBox(GP.XOY(), 10, 10, 10);
            var sphere = ShapeBuilder.MakeBox(new GAx2(new GPnt(5,5,0), new GDir(0,0,1)), 10, 10, 10);

            render.ShowShape(box, ColorTable.Red);
            render.ShowShape(sphere, ColorTable.Blue);

            var common = BooleanTool.Common(box, sphere);
            var node = render.ShowShape(common, ColorTable.Green);
            node.SetTransform(Matrix4.makeTranslation(-20, 0, 0));
            node.RequestUpdate();

            var cut = BooleanTool.Cut(box, sphere);
            node = render.ShowShape(cut, ColorTable.Honeydew);
            node.SetTransform(Matrix4.makeTranslation(0, 20, 0));
            node.RequestUpdate();

            var cut2 = BooleanTool.Cut(sphere, box);
            node = render.ShowShape(cut2, ColorTable.Violet);
            node.SetTransform(Matrix4.makeTranslation(0, -20, 0));
            node.RequestUpdate();

            var fuse = BooleanTool.Fuse(box, sphere);
            fuse = BooleanTool.Unify(fuse, true, true, true);
            node = render.ShowShape(fuse, ColorTable.LightYellow);
            node.SetTransform(Matrix4.makeTranslation(20, 0, 0));
            node.RequestUpdate();
        }
    }
}
