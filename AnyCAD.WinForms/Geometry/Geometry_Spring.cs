using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Spring : TestCase
    {
        GPnt getOutterMultiSpingCoord(double t, double dS, double dR, double dr, double da1, double dw0, double dw1, double dPh)
        {
            var x = dR * Math.Cos(dw0 * t) + dr * Math.Cos(dw1 * t + dPh) * Math.Cos(dw0 * t) + dr * Math.Sin(da1) * Math.Sin(dw1 * t + dPh) * Math.Sin(dw0 * t);
            var y = dR * Math.Sin(dw0 * t) + dr * Math.Cos(dw1 * t + dPh) * Math.Sin(dw0 * t) - dr * Math.Sin(da1) * Math.Sin(dw1 * t + dPh) * Math.Cos(dw0 * t);
            var z = dS * dw0 * t / 2 / Math.PI + dr * Math.Cos(da1) * Math.Sin(dw1 * t + dPh);
            return new GPnt(x,y,z);
        }

        GPnt getIntterMultiSpingCoord(double t, double dS, double dR, double dw0)
        {
            var x = dR * Math.Cos(dw0 * t);
            var y = dR * Math.Sin(dw0 * t);
            var z = dS * dw0 * t / 2 / Math.PI;
            return new GPnt(x, y, z);
        }

        public override void Run(IRenderView render)
        {
            double dS = 19.0;//弹簧螺距mm
            double dR = 11.25;//1/2弹簧中径mm
            double dr1 = 0.9;//内层钢丝半径mm
            double dr2 = 0.9;//外层钢丝半径mm
            double dr = dr1 + dr2;//钢丝轴心分布半径mm
            double da1 = 0.29;//弹簧螺旋角rad
            double dw0 = 1;//卷簧轴转速rad/s
            double dw1 = 5;//拧索轴转速rad/s
            int nOutterCount = 5;//外层钢丝股数

            {
                // create the intter path
                GPntList pts = new GPntList();

                for (double t = 0; t < 10; t += 0.1)
                {
                    pts.Add(getIntterMultiSpingCoord(t, dS, dR, dw0));
                }
                var path = SketchBuilder.MakeBSpline(pts);

                // Get the sketch position and direction
                var curve = new ParametricCurve(path);
                var rt = curve.D1(curve.FirstParameter());

                var position = rt.GetPoint();
                var dir = new GDir(rt.GetVectors()[0]);
                var sketch = SketchBuilder.MakeCircle(position, dr1, dir);

                // Create pipe
                var pipe = FeatureTool.Sweep(sketch, path, EnumGeomFillTrihedron.CorrectedFrenet);

                render.ShowShape(pipe, new Vector3(0.8f));
            }


            Vector3[] colors = new Vector3[] { new Vector3(1.0f, 0, 0), 
                new Vector3(1, 1, 0), 
                new Vector3(0.5f, 1, 0), 
                new Vector3(0, 1, 64/255.0f),
                new Vector3(0, 1, 1) };


            // create the outter path
            for (int i = 0; i < nOutterCount; i++)
            {
                GPntList pts = new GPntList();
                for (double t = 0; t < 10; t += 0.1)
                {
                    pts.Add(getOutterMultiSpingCoord(t, dS, dR, dr, da1, dw0, dw1, i * 2 * Math.PI / nOutterCount));
  
                }
                var path = SketchBuilder.MakeBSpline(pts);

                // Get the sketch position and direction
                var curve = new ParametricCurve(path);
                var rt = curve.D1(curve.FirstParameter());

                var position = rt.GetPoint();
                var dir = new GDir(rt.GetVectors()[0]);

                var sketch = SketchBuilder.MakeCircle(position, dr2, dir);

                // Create pipe
                var pipe = FeatureTool.Sweep(sketch, path, EnumGeomFillTrihedron.CorrectedFrenet);

                render.ShowShape(pipe, colors[i]);
            }

        }
    }
}
