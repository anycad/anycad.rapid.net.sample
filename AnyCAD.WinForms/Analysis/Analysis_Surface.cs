using AnyCAD.Forms;
using AnyCAD.Foundation;
using System.Windows.Forms;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_Surface : TestCase
    {
        public override void Run(RenderControl renderer)
        {
            string fileName = GetResourcePath("Holes.stp");
            var shape = StepIO.Open(fileName);
            if (shape == null)
                return;


            var face = shape.FindChild(EnumTopoShapeType.Topo_FACE, 148);
            var surface = new ParametricSurface(face);

            var wireExp = new WireExplor(face);
            var wires = wireExp.GetInnerWires();
            foreach (var wire in wires)
            {
                // Show wire
                renderer.ShowShape(wire, Vector3.Red);

                var curve = new ParametricCurve(wire);
                var paramList = curve.SplitByUniformLength(1, 0.01);

                foreach (var p in paramList)
                {
                    var pt = curve.Value(p);
                    var pointSur = new ExtremaPointSurface();
                    if (pointSur.Initialize(surface, pt, GP.Resolution(), GP.Resolution()))
                    {
                        var uv = pointSur.GetParameter(0);
                        var normal = surface.GetNormal(uv.X(), uv.Y());
                        // show normal
                        var dirShape = SketchBuilder.MakeLine(pt, new GPnt(pt.XYZ().Added(normal.XYZ())));
                        renderer.ShowShape(dirShape, Vector3.Green);
                    }

                }

            }

            // Show face
            var faceMaterial = MeshStandardMaterial.Create("pbr.face");
            faceMaterial.SetColor(Vector3.LightGray);
            faceMaterial.SetFaceSide(EnumFaceSide.DoubleSide);
            var faceNode = BrepSceneNode.Create(face, faceMaterial, null);
            faceNode.SetDisplayFilter(EnumShapeFilter.Face);
            renderer.ShowSceneNode(faceNode);
        }
    }
}
