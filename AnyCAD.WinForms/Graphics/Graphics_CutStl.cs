using AnyCAD.Foundation;
using System;
using System.Windows.Forms;

namespace AnyCAD.Demo.Graphics
{
    /// <summary>
    /// 高效、快速的剖切算法
    /// </summary>
    class Graphics_CutStl : TestCase
    {
        public override void Run(IRenderView render)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "STL (*.stl;*.stb)|*.stl;*.stb";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var node = SceneIO.Load(dialog.FileName);
            if (node == null)
                return;
            render.ShowSceneNode(node);
            // 获取Mesh
            var nodeSS = ShapeSceneNode.Cast(node);
            PrimitiveShape mesh = nodeSS.GetPrimitive(0);
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
