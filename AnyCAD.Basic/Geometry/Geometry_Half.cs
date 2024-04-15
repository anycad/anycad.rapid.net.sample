using AnyCAD.Foundation;
using System;
using System.IO;
using System.Text;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_HalfSpace : TestCase
    {
        public override void Run(IRenderView render)
        {
            GPntList points = new GPntList();
            for (int ii = 0; ii < 5; ++ii)
                for (int jj = 0; jj < 5; ++jj)
                {
                    double c = ii + jj - 2;
                    points.Add(new GPnt(ii, jj, c));
                }

            var surf = SurfaceBuilder.PointsToBSplineSurface(points, 5);
            var halfSpace = ShapeBuilder.MakeHalfSpace(new GPnt(), new GDir(0, 0, 1));
            surf = BooleanTool.Cut(surf, halfSpace);

            render.ShowShape(surf, ColorTable.Red);

        }
    }
}
