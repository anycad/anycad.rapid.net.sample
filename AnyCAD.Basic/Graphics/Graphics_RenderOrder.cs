using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    /// <summary>
    /// 透明物体绘制顺序
    /// </summary>
    class Graphics_RenderOrder : TestCase
    {
        public override void Run(IRenderView render)
        {
            var materialCore = MeshStandardMaterial.Create("op");
            materialCore.SetColor(ColorTable.MediumVioletRed);

            var materialSmall = MeshStandardMaterial.Create("transparent1");
            materialSmall.SetTransparent(true);
            materialSmall.SetOpacity(0.5f);
            materialSmall.SetColor(ColorTable.GreenYellow);

            var materialBig = MeshStandardMaterial.Create("transparent2");
            materialBig.SetTransparent(true);
            materialBig.SetOpacity(0.2f);
            materialBig.SetColor(ColorTable.SkyBlue);

            var bigBox = GeometryBuilder.CreateBox(100, 100, 100);
            materialBig.SetRenderOrder(bigBox.GetSafeBoudingBox().volume());
            var node = PrimitiveSceneNode.Create(bigBox, materialBig);

            var smallBox = GeometryBuilder.CreateBox(50, 50, 90);
            materialSmall.SetRenderOrder(smallBox.GetSafeBoudingBox().volume());
            var node2 = PrimitiveSceneNode.Create(smallBox, materialSmall);

            var core = GeometryBuilder.CreateSphere(10, 32, 32);
            var node3 = PrimitiveSceneNode.Create(core, materialCore);

            render.ShowSceneNode(node);
            render.ShowSceneNode(node2);
            render.ShowSceneNode(node3);
        }
    }
}
