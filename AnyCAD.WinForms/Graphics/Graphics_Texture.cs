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
        MeshPhongMaterial mMaterial1;
        MeshPhongMaterial mMaterial2;
        BrepSceneNode mNode;
        public override void Run(RenderControl render)
        {
            mMaterial1 = MeshPhongMaterial.Create("phong.texture");
            mMaterial1.SetUniform("diffuse", Uniform.Create(new Vector3(1, 0, 1)));

            var texture = ImageTexture2D.Create(GetResourcePath("textures/bricks2.jpg"));
            texture.SetRepeat(new Vector2(2.0f, 2.0f));
            texture.UpdateTransform();

            mMaterial1.SetColorMap(texture);

            var shape = ShapeBuilder.MakeBox(GP.XOY(), 4,4,8);
            mNode = BrepSceneNode.Create(shape, mMaterial1, null);

            mMaterial2 = MeshPhongMaterial.Create("phong.texture");
            var texture2 = ImageTexture2D.Create(GetResourcePath("textures/water.png"));
            mMaterial2.SetColorMap(texture2);


            render.ShowSceneNode(mNode);
        }

 
        public override void Animation(RenderControl render, float time)
        {
            int selector = (int)time % 2;

            mNode.GetShape().SetFaceMaterial(selector == 1 ? mMaterial1 : mMaterial2);

            render.RequestDraw();
        }
    }
}
