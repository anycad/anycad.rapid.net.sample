using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_MiddlePath : TestCase
    {
        public override void Run(IRenderView renderer)
        {
            var steelLine = ShapeIO.Open(GetResourcePath("models/SteelLine.STEP"));

            GPnt startPoint = null;
            double radius = 0; 
            var baseList = new TopoShapeList(); 
            var faceList = steelLine.GetChildren(EnumTopoShapeType.Topo_FACE);
            foreach(var face in faceList)
            {
                ParametricSurface surf = new ParametricSurface();
                surf.Initialize(face);
                if(surf.GetSurfaceType() == EnumSurfaceType.SurfaceType_Plane)
                {
                    baseList.Add(face);

                    if (radius > 0)
                        continue;

                    var edges2 = face.GetChildren(EnumTopoShapeType.Topo_EDGE);
                    foreach(var edge in edges2)
                    {
                        ParametricCurve curve = new ParametricCurve();
                        curve.Initialize(edge);
                        
                        if(curve.GetCurveType() == EnumCurveType.CurveType_Circle)
                        {
                            var circle = curve.TryCircle();
                            radius = circle.Radius();
                            startPoint = circle.Axis().Location();
                            break;
                        }
                    }
                }
            }

            var sectionEdges = new TopoShapeList();
            var edges = steelLine.GetChildren(EnumTopoShapeType.Topo_EDGE);
            foreach (var edge in edges)
            {
                ParametricCurve curve = new ParametricCurve();
                curve.Initialize(edge);

                if (curve.GetCurveType() == EnumCurveType.CurveType_Circle)
                {
                    var circle = curve.TryCircle();
                    if (circle.Radius() <= radius)
                    {
                        sectionEdges.Add(edge);
                    }
                    else
                    {

                    }
                }
            }


            foreach(var edge in sectionEdges)
            {
                renderer.ShowShape(edge, ColorTable.Bisque);
            }
            //if(baseList.Count > 1)
            //{
            //    WireExplor we = new WireExplor();
            //    we.Initialize(baseList[0]);

            //    WireExplor we2 = new WireExplor();
            //    we2.Initialize(baseList[1]);
            //    var path = FeatureTool.MiddlePath(steelLine, we.GetOuterWire(), we2.GetOuterWire());
            //    renderer.ShowShape(path, ColorTable.Bisque);
            //}
        }
    }
}
