using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_TopoExplor : TestCase
    {
        public override void Run(RenderControl render)
        {
           var fileName = GetResourcePath("models/YONGSHENGDA_9998.igs");
           var shape = ShapeIO.Open(fileName);
            if (shape == null)
                return;

            var face = shape.FindChild(EnumTopoShapeType.Topo_FACE, 12);
            var edges = face.GetChildren(EnumTopoShapeType.Topo_EDGE);

            var wire = SketchBuilder.MakeWire(edges);
            if (wire == null)
                return;

            

            var curve = new ParametricCurve();
            curve.Initialize(wire);
            var ulist = curve.SplitByUniformLength(1, 0.2);


            var node = new ParticleSceneNode((uint)ulist.Count, ColorTable.Red, 2);
            for (int ii=0; ii<ulist.Count; ++ii)
            {
                node.SetPosition((uint)ii, Vector3.From(curve.Value(ulist[ii])));
            }
            node.UpdateBoundingBox();

            render.ShowShape(wire, ColorTable.GoldEnrod);
            render.ShowSceneNode(node);
        }
    }
}
