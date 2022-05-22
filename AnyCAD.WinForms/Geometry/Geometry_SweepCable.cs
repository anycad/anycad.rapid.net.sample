using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_SweepCable : TestCase
    {
        public override void Run(RenderControl render)
        {
            GPntList points = new GPntList();
            string fileName = GetResourcePath("data/CableViewInfo.txt");
            using (StreamReader reader = File.OpenText(fileName))
            {
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var items = line.Split(',');
                    if (items.Length < 3)
                        continue;

                    double x = double.Parse(items[0]);
                    double y = double.Parse(items[1]);
                    double z = double.Parse(items[2]);

                    points.Add(new GPnt(x, y, z));
                   
                }

            }

            var path = SketchBuilder.MakeBSpline(points);
            render.ShowShape(path, ColorTable.Red);

            var curve = new ParametricCurve(path);
            var rt = curve.D1(curve.FirstParameter());

            var position = rt.GetPoint();
            var dir = new GDir(rt.GetVectors()[0]);
            var sketch = SketchBuilder.MakeCircle(position, 10, dir);
            render.ShowShape(sketch, new Vector3(0.8f));
            var pipe = FeatureTool.SweepByFrenet(sketch, path, EnumSweepTransitionMode.RightCorner, false);

            try
            {

                if (pipe != null)
                {
                    render.ShowShape(pipe, new Vector3(0.8f));
                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
