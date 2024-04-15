using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_HLR : TestCase
    {
        public override void Run(IRenderView render)
        {
            var fileName = GetResourcePath("ST038.stp");
            var model = ShapeIO.Open(fileName);
            if(model == null) return;

            ShapeHLRBuilder builder = new ShapeHLRBuilder();
            builder.AddShape(model);
            var bbox = model.GetBBox();

            {
                var ax = new GAx2(bbox.CornerMax().Translated(new GVec(10, 0, 0)), new GDir(new GVec(-1, 0, 0)));
                if (builder.Compute(ax))
                {
                    var edges = builder.GetVisibleOutlines();
                    var group = ShapeBuilder.MakeCompound(edges);
                    var node = render.ShowShape(group, ColorTable.LightGreen);
                    var trf = TransformTool.ToMatrix4(ax);
                    node.SetTransform(trf);
                }
            }
            {
                var ax = new GAx2(bbox.CornerMax().Translated(new GVec(0, 10, 0)), new GDir(new GVec(0, -1, 0)));
                if (builder.Compute(ax))
                {
                    var edges = builder.GetVisibleOutlines();
                    var group = ShapeBuilder.MakeCompound(edges);
                    var node = render.ShowShape(group, ColorTable.LightPink);
                    var trf = TransformTool.ToMatrix4(ax);
                    node.SetTransform(trf);
                }
            }
            {
                var ax = new GAx2(bbox.CornerMax().Translated(new GVec(0, 0, 10)), new GDir(new GVec(0, 0, -1)));
                if (builder.Compute(ax))
                {
                    var edges = builder.GetVisibleOutlines();
                    var group = ShapeBuilder.MakeCompound(edges);
                    var node = render.ShowShape(group, ColorTable.GreenYellow);
                    var trf = TransformTool.ToMatrix4(ax);
                    node.SetTransform(trf);
                }
            }

            render.ShowShape(model, ColorTable.LightGray);
        }
    }
}
