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
            dialog.Filter = "IGES (*.igs;*.iges)|*.igs;*.iges|STEP (*.stp;*.step)|*.stp;*.step|Brep (*.brep)|*.brep";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var shape = ShapeIO.Open(dialog.FileName);
            if (shape == null)
                return;

            var bs = new BufferShape(shape, null, null, 0);
            bs.Build();

            // 1. Compute
            var holeExp = new HoleExplor();
            if (!holeExp.Initialize(bs))
                return;
            var dir = holeExp.ComputeDirection();
            holeExp.Compute(dir);

            var defaultMaterial = MeshPhongMaterial.Create("face-x");
            defaultMaterial.SetFaceSide(EnumFaceSide.DoubleSide);
            bs.SetFaceMaterial(defaultMaterial);
           

            // 2. Set hole faces with red color
            var material = BasicMaterial.Create("hole-face");
            material.SetFaceSide(EnumFaceSide.DoubleSide);
            material.SetColor(new Vector3(0.5f, 0, 0));
            var holeNumber = holeExp.GetHoleCount();
            for(uint ii=0; ii< holeNumber; ++ii)
            {
                var faceIDs = holeExp.GetHoleFaces(ii);
                foreach(var faceIdx in faceIDs)
                    bs.SetFaceMaterial(faceIdx, material);
            }

            // 2. Show the faces
            var shapeNode = new BrepSceneNode(bs);
            renderer.ShowSceneNode(shapeNode);
        }
    }
}
