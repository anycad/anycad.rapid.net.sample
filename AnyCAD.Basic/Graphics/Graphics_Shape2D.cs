using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_Shape2D : TestCase
    {

        public override void Run(IRenderView render)
        {
            var cir = GeometryBuilder.CreateArc(new Vector3(0), new Vector3(100, 0, 0), new Vector3(0, 100, 0), 0);
            var node = PrimitiveSceneNode.Create(cir, null);

            var text = TextSceneNode.Create("圆弧", ColorTable.绿宝石, 50, true);

            var group = new GroupSceneNode();
            group.AddNode(node);
            group.AddNode(text);

            //相对窗口左上角
            var window = new WindowNode2D(group, 100, 100);
            render.ShowSceneNode(window);

            //三维也显示一下
            render.ShowSceneNode(group);
        }
    }
}
