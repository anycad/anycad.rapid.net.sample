using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Linq;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_Curve : TestCase
    {
        public override void Run(RenderControl renderer)
        {
            var ellipse = SketchBuilder.MakeEllipse(GP.Origin(), 10, 5, GP.DX(), GP.DZ());
            renderer.ShowShape(ellipse, Vector3.Blue);

            ParametricCurve pc = new ParametricCurve(ellipse);
 
            var paramsList = pc.SplitByUniformLength(1, 0.01);

            uint itemCount = (uint)paramsList.Count;
            var points = new ParticleSceneNode(itemCount, Vector3.Green, 5.0f);
            var lines = new SegmentsSceneNode(itemCount, Vector3.Red, 2);

            Random random = new Random();
            for (int ii=0; ii< paramsList.Count; ++ii)
            {
                var value = pc.D1(paramsList[ii]);
                var pos = value.GetPoint();
                var dir = value.GetVectors()[0];
                var end = new GPnt(pos.XYZ().Added(dir.XYZ()));

                lines.SetPositions((uint)ii, Vector3.From(pos), Vector3.From(end));
                lines.SetColors((uint)ii, Vector3.Red, Vector3.From(random.NextDouble(), random.NextDouble(), random.NextDouble()));

                points.SetPosition((uint)ii, Vector3.From(pos));
            }

            renderer.ShowSceneNode(points);
            renderer.ShowSceneNode(lines);
        }
    }
}
