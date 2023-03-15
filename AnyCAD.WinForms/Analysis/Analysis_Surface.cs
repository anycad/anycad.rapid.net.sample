using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_Surface : TestCase
    {
        public override void Run(IRenderView renderer)
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
                renderer.ShowShape(wire, ColorTable.Red);

                var curve = new ParametricCurve(wire);
                var paramList = curve.SplitByUniformLength(1, 0.01);

                var lines = new SegmentsSceneNode((uint)paramList.Count, ColorTable.Green, 2);
                uint idx = 0;
                foreach (var p in paramList)
                {
                    var pt = curve.Value(p);
                    var pointSur = new ExtremaPointSurface();
                    if (pointSur.Initialize(face, pt))
                    {
                        var uv = pointSur.GetParameter(0);
                        var normal = surface.GetNormal(uv.X(), uv.Y());

                        lines.SetPositions(idx++, Vector3.From(pt), Vector3.From(pt.XYZ().Added(normal.XYZ())));
                    }

                }
                lines.UpdateBoundingBox();
                renderer.ShowSceneNode(lines);

            }

            // Show face
            var faceMaterial = MeshStandardMaterial.Create("pbr.face");
            faceMaterial.SetColor(ColorTable.Gray);
            faceMaterial.SetFaceSide(EnumFaceSide.DoubleSide);
            var faceNode = BrepSceneNode.Create(face, faceMaterial, null);
            faceNode.SetDisplayFilter(EnumShapeFilter.Face);
            renderer.ShowSceneNode(faceNode);
        }
    }
}
