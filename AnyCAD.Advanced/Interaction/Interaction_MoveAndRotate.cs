using AnyCAD.Demo.Geometry;
using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Graphics
{
    class Interaction_MoveAndRotate : TestCase
    {
        public override void Run(IRenderView render)
        {
            var shape = GeometryBuilder.CreateBox(10, 100, 200);
            var node = PrimitiveSceneNode.Create(shape, null);
            render.ShowSceneNode(node);

            var editor = new NodeFrameEidtor(node, TransformWidget.Create(20), "MyNodeEditor");
            render.SetEditor(editor);
        }

        public override void Exit(IRenderView render)
        {
            render.SetEditor(null);
        }
    }
}
