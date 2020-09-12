using AnyCAD.Forms;
using AnyCAD.Foundation;
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
            var buffer = new Float32Buffer((uint)paramsList.Count * 3);
            for(int ii=0; ii< paramsList.Count; ++ii)
            {
                var value = pc.D1(paramsList[ii]);
                var pos = value.GetPoint();
                var dir = value.GetVectors()[0];

                var dirShape = SketchBuilder.MakeLine(pos, new GPnt(pos.XYZ().Added(dir.XYZ())));
                renderer.ShowShape(dirShape, Vector3.Red);

                buffer.SetValue((uint)ii * 3, Vector3.From(pos));
            }

            var primitive =  GeometryBuilder.CreatePoints(new Float32Array(buffer), null);
            var node = new PrimitiveSceneNode(primitive);
            var material = PointsMaterial.Create("pts");
            material.SetColor(Vector3.Green);
            material.SetPointSize(5.0f);
            node.SetMaterial(material);
            renderer.ShowSceneNode(node);
        }
    }
}
