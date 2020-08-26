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

            var edge = shape.FindChild(EnumTopoShapeType.Topo_EDGE, 436);
            var face = shape.FindChild(EnumTopoShapeType.Topo_FACE, 148);

            var curve = new ParametricCurve(edge);
            curve.GetEndPoint();
            var surface = new ParametricSurface(face);

            var extrema = new ExtremaCurveSurface();
            extrema.SetSurface(face);
            if(extrema.Perform(curve))
            {
                int nCount = extrema.GetPointCount();
                MessageBox.Show(nCount.ToString());
            }

            renderer.ShowShape(edge, Vector3.Blue);
            renderer.ShowShape(face, Vector3.LightGray);
        }
    }
}
