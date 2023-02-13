using AnyCAD.Forms;
using AnyCAD.Foundation;
using System.Windows.Forms;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_Holes : TestCase
    {
        public override void Run(RenderControl renderer)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "IGES (*.igs;*.iges)|*.igs;*.iges|STEP (*.stp;*.step)|*.stp;*.step|Brep (*.brep)|*.brep";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            var shape = ShapeIO.Open(dialog.FileName);
            if (shape == null)
                return;

            var edgeMaterial = BasicMaterial.Create("hole-edge");
            edgeMaterial.SetColor(ColorTable.Hex(0xFF0000));
            edgeMaterial.SetLineWidth(2);


            // 1. Find the exterial holes
            var holeExp = new HoleDetector();
            if (!holeExp.Initialize(shape))
                return;
            var holeNumber = holeExp.GetHoleCount();

            for(uint ii=0; ii< holeNumber; ++ii)
            {
                var wire = holeExp.GetHoleExteriorWire(ii);
                var wireNode = BrepSceneNode.Create(wire, null, edgeMaterial);

                renderer.ShowSceneNode(wireNode);
            }

            // 2. Show the faces
            var material = MeshStandardMaterial.Create("hole-face");
            material.SetColor(ColorTable.Hex(0xBBAA33));
            material.SetRoughness(0.8f);
            material.SetFaceSide(EnumFaceSide.DoubleSide);

            var shapeNode = BrepSceneNode.Create(shape, material, null);
            shapeNode.SetDisplayFilter(EnumShapeFilter.Face);
            renderer.ShowSceneNode(shapeNode);
        }
    }
}
