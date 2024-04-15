using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class MyDistanceMeasureEditor : DistanceMeasureEditor
    {
        public MyDistanceMeasureEditor()
        {
        }
        public override bool Apply(ViewContext ctx)
        {
            var node = GetNode();

            // 获取测量结果

            return base.Apply(ctx);
        }
    }

    class Interaction_Measure : TestCase
    {
        public override void Run(IRenderView render)
        {
            var shape = ShapeIO.Open(OpenModelFile());
            if (shape == null)
                return;
            render.ShowShape(shape, ColorTable.AliceBlue);

            render.SetEditor(new MyDistanceMeasureEditor());
        }

        public override void Exit(IRenderView render)
        {
            render.SetEditor(null);
        }
    }
}
