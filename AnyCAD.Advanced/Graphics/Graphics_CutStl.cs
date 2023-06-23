using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    /// <summary>
    /// 高效、快速的剖切算法
    /// </summary>
    class Graphics_CutStl : TestCase
    {
        public override void Run(IRenderView render)
        {
            var fullPath = DialogUtil.OpenFileDialog("STL", new StringList(new string[] { "3D Files (.stl .stb)", "*.stl *.stb" }));
            if (fullPath.IsEmpty())
                return;

            var node = SceneIO.Load(fullPath.GetString());
            if (node == null)
                return;
            render.ShowSceneNode(node);
            PrimitiveShape mesh = null;
            // 获取Mesh
            var nodeSS = ShapeSceneNode.Cast(node);
            if(nodeSS !=null)
            {
                mesh = nodeSS.GetPrimitive(0);
            }
            else
            {
                var nodePS = PrimitiveSceneNode.Cast(node);
                if (nodePS != null)
                {
                    mesh = nodePS.GetPrimitive();
                }
            }
            if (mesh == null)
                return;

            var geom = mesh.GetGeometry();

            //在中间砍一刀
            var box = node.GetBoundingBox();
            var mid = (box.getMaximum().z + box.getMinimum().z)/2;
            var size = box.getSize();
            var section = MeshTool.ComputeSection(geom, mid, 0.001f, 2.0f);
            if (section == null)
                return;

            // 加粗显示，这样更明显
            var material = BasicMaterial.Create("line.m");
            material.SetColor(ColorTable.李子);
            material.SetLineWidth(15);
            var shape = new PrimitiveSceneNode(section, material);
            shape.SetCulling(false);
            render.ShowSceneNode(shape);
        }
    }
}
