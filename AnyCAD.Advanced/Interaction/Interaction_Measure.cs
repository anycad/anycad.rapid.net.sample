using AnyCAD.Demo.Geometry;
using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Graphics
{
    class MyDistanceMeasureEditor : DistanceMeasureEditor
    {
        public MyDistanceMeasureEditor()
        {
            //SetRepeat(true);
        }
        public override void Apply(ViewContext ctx)
        {
            var node = GetNode();
            base.Apply(ctx);
        }
    }

    class Interaction_Measure : Analysis_Holes
    {
        public override void Run(IRenderView render)
        {
            base.Run(render);

            render.SetEditor(new MyDistanceMeasureEditor());
        }

        public override void Exit(IRenderView render)
        {
            render.SetEditor(null);
        }
    }
}
