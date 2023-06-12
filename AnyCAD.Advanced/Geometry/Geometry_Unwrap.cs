using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Unwrap : TestCase
    {
        public override void Run(IRenderView render)
        {
            var fileName = OpenModelFile();
            if (fileName.IsEmpty())
                return;

            var shape = ShapeIO.Open(fileName);
            if(shape == null) 
                return;

            var faceList = shape.GetChildren(EnumTopoShapeType.Topo_FACE);

            int halfCount = faceList.Count / 2; int ii = 0;
            float x = 0, y = 0;
            float maxY = 0;
            foreach ( var face in faceList )
            {
                var wires = ShapeAnalysisTool.Unwrap(face);
                var plane = SketchBuilder.MakePolygonFace(wires);
                if(plane != null)
                {
                    var node = render.ShowShape(plane, ColorTable.GreenYellow);
                    node.SetTransform(Matrix4.makeTranslation(x, y, 0));
                    node.RequestUpdate();

                    var bbox = node.GetBoundingBox();
                    var sz = bbox.getSize();

                    if(ii == halfCount)
                    {
                        x = 0;
                        y = maxY;
                    }
                    else
                    {
                        x += sz.x;
                    }

                    if(ii < halfCount)
                    {

                        maxY = System.Math.Max(maxY, sz.y);
                    }
                    ++ii;
                }
            }

            render.ShowShape(shape, ColorTable.Honeydew);
        }
    }
}
