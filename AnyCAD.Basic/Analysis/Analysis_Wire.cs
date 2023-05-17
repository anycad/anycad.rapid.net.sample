using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_Wire : TestCase
    {
        public override void Run(IRenderView renderer)
        {
            var wire = BrepIO.Open(GetResourcePath("models/wire.brep"));
            if (wire == null)
                return;

            ParametricCurve pc = new ParametricCurve(wire);
 
            var paramsList = pc.SplitByUniformLength(0.1, 0.01);

            uint itemCount = (uint)paramsList.Count;
            var points = new ParticleSceneNode(itemCount, ColorTable.Green, 5.0f);
            var lines = new SegmentsSceneNode(itemCount, ColorTable.Red, 2);

            Random random = new Random();
            for (int ii=0; ii< paramsList.Count; ++ii)
            {
                var value = pc.D1(paramsList[ii]);
                var pos = value.GetPoint();
                var dir = value.GetVectors()[0];
                var end = new GPnt(pos.XYZ().Added(dir.XYZ()));

                lines.SetPositions((uint)ii, Vector3.From(pos), Vector3.From(end));
                lines.SetColors((uint)ii, ColorTable.Red, Vector3.From(random.NextDouble(), random.NextDouble(), random.NextDouble()));

                points.SetPosition((uint)ii, Vector3.From(pos));
            }
            points.UpdateBoundingBox();
            renderer.ShowSceneNode(points);
            renderer.ShowSceneNode(lines);
            renderer.ShowShape(wire, ColorTable.Honeydew);
        }
    }
}
