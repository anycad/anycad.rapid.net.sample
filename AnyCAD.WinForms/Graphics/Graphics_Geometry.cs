using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_Geometry : TestCase
    {
        public override void Run(RenderControl render)
        {
            // Create material
            var mMaterial1 = MeshPhongMaterial.Create("phong.texture");
            mMaterial1.SetFaceSide(EnumFaceSide.DoubleSide);
            var texture = ImageTexture2D.Create(GetResourcePath("textures/bricks2.jpg"));
            mMaterial1.SetColorMap(texture);

            var plane = GeometryBuilder.CreatePlane(500, 500);
            var planeNode = new PrimitiveSceneNode(plane, mMaterial1);
            planeNode.SetTransform(Matrix4.makeTranslation(new Vector3(0, 0, -100)));
            render.ShowSceneNode(planeNode);

            var box = GeometryBuilder.CreateBox(100, 100, 200);
            var boxNode = new PrimitiveSceneNode(box, mMaterial1);
            render.ShowSceneNode(boxNode);

            var sphere = GeometryBuilder.CreateSphere(50, 32, 32);
            var sphereNode = new PrimitiveSceneNode(sphere, mMaterial1);
            sphereNode.SetTransform(Matrix4.makeTranslation(new Vector3(0, 0, 150)));

            render.ShowSceneNode(sphereNode);
        }

    }
}
