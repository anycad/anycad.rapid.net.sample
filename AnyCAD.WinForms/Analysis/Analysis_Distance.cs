using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_Distance : TestCase
    {
        public override void Run(IRenderView renderer)
        {
            string fileName = GetResourcePath("Holes.stp");
            var shape = StepIO.Open(fileName);
            if (shape == null)
                return;
            renderer.ShowSceneNode(BrepSceneNode.CreateBatch(shape, null, null));

            var bbox = shape.GetBBox();

            var shape2 = ShapeBuilder.MakeBox(new GAx2(bbox.CornerMax(), GP.DZ()), 100, 100, 10);
            renderer.ShowShape(shape2, ColorTable.LightSlateGray);

            ExtremaShapeShape ess = new ExtremaShapeShape();
            if (!ess.Initialize(shape, shape2, 0.001))
                return;

            var pt1 = ess.GetPointOnShape1(0);
            var pt2 = ess.GetPointOnShape2(0);

            var line = SketchBuilder.MakeLine(pt1, pt2);

            
            renderer.ShowShape(line, ColorTable.Red);
        }
    }
}
