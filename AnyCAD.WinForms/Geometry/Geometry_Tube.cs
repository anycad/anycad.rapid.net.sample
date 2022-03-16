using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Tube : TestCase
    {
        public TopoShape Generate(double innerLength, double innerWidth, double outerLength, double outerWidth, double radius, double length)
        { 
            double outerStartX = outerLength / 2;
            double outerStartZ = outerLength / 2; 
            double innerStartX = innerLength / 2; 
            double innerStartZ = innerLength / 2;
            var outerStartPoint = new GPnt(-outerStartX, 0, -outerStartZ); 
            var innerStartPoint = new GPnt(-innerStartX, 0, -innerStartZ);
            var innerSide = SketchBuilder.MakeRectangle(new GAx2(innerStartPoint, new GDir(0, 1, 0)), innerLength, innerWidth, radius, false);
            var outerSide = SketchBuilder.MakeRectangle(new GAx2(outerStartPoint, new GDir(0, 1, 0)), outerLength, outerWidth, radius, false);

            var face1 = SketchBuilder.MakePlanarFace(innerSide);
            var face2 = SketchBuilder.MakePlanarFace(outerSide);

            var cut = BooleanTool.Cut(face2, face1);

            var tube = FeatureTool.Extrude(cut, length, new GDir(0, 1, 0));
            return tube;
        }
        public override void Run(RenderControl render)
        {
            var tube = Generate(100, 100, 150, 150, 5, 100);
            render.ShowShape(tube, ColorTable.Beige);
            return;

        }
    }
}
