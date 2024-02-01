using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_SpoineToArcs : TestCase
    {
        public override void Run(IRenderView render)
        {
            var face = ShapeIO.Open(GetResourcePath("Split/face.brep"));
            var edges = face.GetChildren(EnumTopoShapeType.Topo_EDGE);
            foreach(var edge in edges)
            {
                ParametricCurve pc = new ParametricCurve();
                if (!pc.Initialize(edge))
                    continue;

                if(pc.GetCurveType() == EnumCurveType.CurveType_BSplineCurve)
                {
                    var simpleCurve = AdvCurveBuilder.SplineToArcs(edge, 0.01);
                    foreach(var curve in simpleCurve)
                    {
                        render.ShowShape(curve, ColorTable.Red);
                    }

                    var node = render.ShowShape(edge, ColorTable.Green);
                    node.SetTransform(Matrix4.makeTranslate(0,0,1));
                }
            }
        }
    }
}
