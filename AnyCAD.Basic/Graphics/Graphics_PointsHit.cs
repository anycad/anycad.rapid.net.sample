using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_PointsHit : TestCase
    {
        public override void Run(IRenderView render)
        {
            var pos = new Vector3(1);
            var pt = GeometryBuilder.CreatePoint(new Vector3(1));
            var node = PrimitiveSceneNode.Create(pt, null);
            node.SetBoundingBox(new AABox(pos - new Vector3(10), pos + new Vector3(10)));
            render.ShowSceneNode(node);

            var pt2 = GeometryBuilder.CreatePoint(new Vector3(100, 1000, 0));
            var node2 = PrimitiveSceneNode.Create(pt2, null);
            render.ShowSceneNode(node2);
        }
    }
}
