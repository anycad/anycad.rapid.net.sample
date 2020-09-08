using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_Texture : TestCase
    {
        public override void Run(RenderControl render)
        {
            var material = MeshPhongMaterial.Create("phong.texture");
            material.SetUniform("diffuse", Uniform.Create(new Vector3(1, 0, 1)));

            var texture = ImageTexture2D.Create(GetResourcePath("textures/bricks2.jpg"));
            texture.SetRepeat(new Vector2(2.0f, 2.0f));
            texture.UpdateTransform();

            material.SetColorMap(texture);

            var shape = ShapeBuilder.MakeBox(GP.XOY(), 4,4,8);
            var node = BrepSceneNode.Create(shape, material, null);

            render.ShowSceneNode(node);
        }
    }
}
