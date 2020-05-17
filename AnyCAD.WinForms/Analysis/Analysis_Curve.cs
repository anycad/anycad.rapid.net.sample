using AnyCAD.Forms;
using AnyCAD.Foundation;


namespace AnyCAD.Demo.Geometry
{
    class Analysis_Curve : TestCase
    {
        public override void Run(RenderControl renderer)
        {
            var ellipse = SketchBuilder.MakeEllipse(GP.Origin(), 10, 5, GP.DX(), GP.DZ());
            renderer.ShowShape(ellipse, new Vector3(0.8f));

            var red = new Vector3(0.0f, 1.0f, 0.0f);
            ParametricCurve pc = new ParametricCurve(ellipse);
            for (double param = pc.FirstParameter(), endParam = pc.LastParameter(); param < endParam; param += 0.2)
            {
                var value = pc.D1(param);
                var pos = value.GetPoint();
                var dir = value.GetVectors()[0];

                var dirShape = SketchBuilder.MakeLine(pos, new GPnt(pos.XYZ().Added(dir.XYZ())));
                renderer.ShowShape(dirShape, red);
            }
        }
    }
}
