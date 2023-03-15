using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_Clip : TestCase
    {

        ClipPlaneView mClipView = new ClipPlaneView();
        public override void Run(IRenderView render)
        {
            var shape = ShapeBuilder.MakeCylinder(GP.XOY(), 50, 100, Math.PI * 1.2);

            var dir = new Vector3(1, 1, 1);
            dir.normalize();

            var node = BrepSceneNode.Create(shape, null, null);
            render.ShowSceneNode(node);
            node.SetPickable(false);

            mClipView.SetStartPoint(new Vector3(0, 0, 50));
            mClipView.SetDirection(-dir);
            mClipView.Start(render.ViewContext); 

            var plane = BooleanTool.Slice(shape, new GPnt(0, 0, 50), new GDir(dir.x, dir.y, dir.z), 0.001);
            var planes = ShapeBuilder.MakeCompound(plane);
            var material = BasicMaterial.Create("clip.f");
            material.GetTemplate().SetClipping(false);
            material.SetColor(ColorTable.Auqamarin);

            var edgeMaterial = BasicMaterial.Create("clip.e");
            edgeMaterial.GetTemplate().SetClipping(false);
            edgeMaterial.SetColor(ColorTable.IndianRed);

            var planeNode = BrepSceneNode.Create(planes, material, edgeMaterial);
            render.ShowSceneNode(planeNode);
        }
        public override void Animation(IRenderView render, float time)
        {
            mClipView.Update(render.ViewContext);
        }
        public override void Exit(IRenderView render)
        {
            mClipView.Finish(render.ViewContext);
        }
    }
}
