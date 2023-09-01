using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    /// <summary>
    /// 透明物体绘制顺序
    /// </summary>
    class Graphics_OverrideMaterial : TestCase
    {
        public override void Run(IRenderView render)
        {
            var stl = new STLReader();          
            var face = MeshPhongMaterial.Create("stl");
            face.SetColor(ColorTable.LightYellow);
            face.SetOpacity(0.8f);
            face.SetDepthTest(false);
            face.SetTransparent(true);

            var node = stl.Load(GetResourcePath("stl/1234.stl"), false, face);
            render.ShowSceneNode(node);
        }
    }
}
