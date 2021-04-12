using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Pyramid : TestCase
    {

        public override void Run(RenderControl render)
        {
            var material = MeshStandardMaterial.Create("my-material");
            material.SetRoughness(0.75f);
            material.SetMetalness(0.1f);
            material.SetColor(Vector3.ColorFromHex(0xFFC107));

            for (uint ii = 0; ii < 10; ++ii)
            {
                TopoShape shape = ShapeBuilder.MakePyramid(new GAx2(new GPnt(ii * 11, 0, 0), GP.DZ()), ii + 3, 5, 5 + ii);
                var node = BrepSceneNode.Create(shape, material, null);
                render.ShowSceneNode(node);
            }

            //var bottom = SketchBuilder.MakeRectangle(GP.XOY(), 10, 20, 2, false);
            //var shape2 = FeatureTool.Loft(bottom, new GPnt(5, 10, 10));

            //render.ShowShape(shape2, Vector3.Blue);

            //var shape3 =  ShapeBuilder.MakeCone(GP.XOY(), 10, 0, 10, 0);
            //render.ShowShape(shape3, Vector3.Blue);
        }
    }
}
