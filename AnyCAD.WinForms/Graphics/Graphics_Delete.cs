using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_Delete : TestCase
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

            var group = new GroupSceneNode();

            var box = GeometryBuilder.CreateBox(100, 100, 200);
            var boxNode = new PrimitiveSceneNode(box, mMaterial1);
            group.AddNode(boxNode);

            var sphere = GeometryBuilder.CreateSphere(50, 32, 32);
            var sphereNode = new PrimitiveSceneNode(sphere, mMaterial1);
            sphereNode.SetTransform(Matrix4.makeTranslation(new Vector3(0, 0, 150)));

            group.AddNode(sphereNode);

            render.ShowSceneNode(group);

           
            // 根据ID删除Node
            
            //注意：加在Group中的Node，只能从Group中删除
            // render.GetScene().RemoveNode(sphereNode.GetUuid()); //不工作
            
            
            // 从Group中删除
            group.RemoveNode(sphereNode.GetUuid());
            group.RequstUpdate();

            render.ViewContext.RequestUpdate(EnumUpdateFlags.Scene);
        }

    }
}
