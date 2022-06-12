using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Slice : TestCase
    {
        public override void Run(RenderControl render)
        {
            var dir = new GDir(0, 0, 1);

            var shape = ShapeBuilder.MakeCone(GP.XOY(), 100, 50, 200, 0);

            for(int ii=0; ii<10; ++ii)
            {
                var pos = new GPnt(0,0, 10 + ii * 20);

                var result = BooleanTool.Slice(shape, pos, dir, 0.01);
                foreach(var face in result)
                    render.ShowShape(face, ColorTable.BlanchedAlmond);
            }

            var material = MeshStandardMaterial.Create("cut-transparent");
            material.SetColor(ColorTable.Brown);
            material.SetTransparent(true);
            material.SetOpacity(0.6f);

            var node = BrepSceneNode.Create(shape, material, null);
            node.SetDisplayFilter(EnumShapeFilter.Face);
            render.ShowSceneNode(node);
        }
    }
}
