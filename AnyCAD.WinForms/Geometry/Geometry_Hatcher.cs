using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Hatcher : TestCase
    {
        public override void Run(RenderControl render)
        {
            // 1. Make face with a hole.
            var c1 = SketchBuilder.MakeCircle(GP.Origin(), 30, GP.DZ());
            var c2 = SketchBuilder.MakeCircle(GP.Origin(), 100, GP.DZ());

            var face = SketchBuilder.MakePlanarFace(c2);
            var face2 = SketchBuilder.MakePlanarFace(c1);
            var shape = BooleanTool.Cut(face, face2);
            //render.ShowShape(shape, Vector3.Blue);

            var faces = shape.GetChildren(EnumTopoShapeType.Topo_FACE);

            // 2. Hatch the face.
            foreach (var item in faces)
            {
                HatchHatcher hh = new HatchHatcher(item);
                hh.Build();
                var material = BasicMaterial.Create("line");
                material.SetColor(new Vector3(0.5f, 0.5f, 1.0f));
                var node = hh.Create(material);

                // 3. Show the hatching lines.
                render.ShowSceneNode(node);
            }

        }
    }
}
