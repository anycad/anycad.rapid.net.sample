using AnyCAD.Forms;
using AnyCAD.Foundation;
using System.Windows.Forms;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_Holes : TestCase
    {
        public override void Run(RenderControl renderer)
        {
            var shape = ShapeIO.Open(GetResourcePath("hole/30-30.IGS"));
            if (shape == null)
                return;

            var edgeMaterial = BasicMaterial.Create("hole-edge");
            edgeMaterial.SetColor(Vector3.ColorFromHex(0xFF0000));
            edgeMaterial.SetLineWidth(2);


            // 1. Find the exterial holes
            var holeExp = new HoleExplorLegacy();
            if (!holeExp.Initialize(shape))
                return;
            var holeNumber = holeExp.ComputeExteriorHoles();

            for(int ii=0; ii<holeNumber; ++ii)
            {
                var wire = holeExp.GetExteriorHoleWire(ii);
                var wireNode = BrepSceneNode.Create(wire, null, edgeMaterial);

                renderer.ShowSceneNode(wireNode);
            }

            // 2. Show the faces
            var material = MeshStandardMaterial.Create("hole-face");
            material.SetColor(Vector3.ColorFromHex(0xBBAA33));
            material.SetRoughness(0.8f);
            material.SetFaceSide(EnumFaceSide.DoubleSide);

            var shapeNode = BrepSceneNode.Create(shape, material, null);
            shapeNode.SetDisplayFilter(EnumShapeFilter.Face);
            renderer.ShowSceneNode(shapeNode);
        }
    }
}
