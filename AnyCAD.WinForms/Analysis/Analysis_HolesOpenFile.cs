using AnyCAD.Forms;
using AnyCAD.Foundation;
using System.Windows.Forms;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_HolesOpenFile : TestCase
    {
        public override void Run(RenderControl renderer)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "IGES (*.igs;*.iges)|*.igs;*.iges|STEP (*.stp;*.step)|*.stp;*.step";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var shape = ShapeIO.Open(dialog.FileName);
            if (shape == null)
                return;

            var edgeMaterial = BasicMaterial.Create("hole-edge");
            edgeMaterial.SetColor(Vector3.ColorFromHex(0xFF0000));
            edgeMaterial.SetLineWidth(2);


            // 1. Find the exterial holes
            var holeExp = new HoleExplor();
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
            var material = MeshNormalMaterial.Create("hole-face");
            material.SetFaceSide(EnumFaceSide.DoubleSide);

            var shapeNode = BrepSceneNode.Create(shape, material, null);
            shapeNode.SetDisplayFilter(EnumShapeFilter.Face);
            renderer.ShowSceneNode(shapeNode);
        }
    }
}
