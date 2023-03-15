using AnyCAD.Foundation;
using System;
using System.IO;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Complex : TestCase
    {

        TopoShape CreateSpline(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                string line;
                GPntList points = new GPntList();
                while ((line = sr.ReadLine()) != null)
                {
                    var temp = line.Split(',');
                    points.Add(new GPnt(double.Parse(temp[0]), double.Parse(temp[1]), double.Parse(temp[2])));
                }
                if (points.Count > 1)
                {
                    return SketchBuilder.MakeBSpline(points);
                }
            }

            return null;
        }
        public override void Run(IRenderView render)
        {
            TopoShapeList auxCurves = new TopoShapeList();
            for(int ii=0; ii<4; ++ii)
            {
                char name = (char)((int)('A') + ii);
                string fileName = String.Format("data/complex/{0}.sldcrv", name);
                var curve = CreateSpline(GetResourcePath(fileName));
                if(curve!=null)
                {
                    render.ShowShape(curve, ColorTable.Hex(0xFF0000));
                    auxCurves.Add(curve);
                }
            }

            for(int ii=0; ii<12; ++ii)
            {
                string fileName = String.Format("data/complex/{0}.sldcrv", ii);
                var curve = CreateSpline(GetResourcePath(fileName));
                if (curve != null)
                {
                    render.ShowShape(curve, ColorTable.Hex(0xFF00FF));
                    auxCurves.Add(curve);
                }
            }
           
        }
    }
}
