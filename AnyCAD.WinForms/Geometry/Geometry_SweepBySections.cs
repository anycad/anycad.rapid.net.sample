using AnyCAD.Demo;
using AnyCAD.Foundation;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

class Geometry_SweepBySections : TestCase
{
    public override void Run(IRenderView render)
    {
        getPath2();
        for (int i = 0; i < listGData.Count; i++)
        {
            createFont(render, listGData[i]);
        }

        Sweep(render, mPointList, EnumSweepTransitionMode.RoundCorner, 1);
        Sweep(render, mPointList, EnumSweepTransitionMode.Transformed, 2.5f);
        Sweep(render, mPointList, EnumSweepTransitionMode.RightCorner, 4);

        render.RequestDraw();
    }//

    void Sweep(IRenderView render, GPntList points, EnumSweepTransitionMode mode, float offset)
    {
        var path = SketchBuilder.MakePolygon(points, false);

        var sketch =  SketchBuilder.MakeCircle(GP.XOY(), 0.1);

        var shape = FeatureTool.SweepByFrenet(sketch, path, mode, true);

        var node = render.ShowShape(shape, ColorTable.Aqua);
        node.SetTransform(Matrix4.makeTranslation(0, offset, 0));
        node.RequstUpdate();
    }

    private void createFont(IRenderView mRenderView, GPntList gPnts)
    {
        var path = SketchBuilder.MakeBSpline(gPnts);


        // Get the sketch position and direction
        var curve = new ParametricCurve(path);
        var bbb = curve.FirstParameter();
        var rt = curve.D1(curve.FirstParameter());

        var position = rt.GetPoint();
        var dir = new GDir(rt.GetVectors()[0]);
        var sketch1 = SketchBuilder.MakeCircle(position, 0.1, dir);


        var rt2 = curve.D1(curve.LastParameter());

        var position2 = rt2.GetPoint();
        var aaa = rt2.GetVectors();
        var dir2 = new GDir(rt2.GetVectors()[0]);
        var sketch2 = SketchBuilder.MakeCircle(position2, 0.1, dir2);



        var shapeList = new TopoShapeList();
        shapeList.Add(sketch1);
        shapeList.Add(sketch2);
        var shape = FeatureTool.SweepBySections(shapeList, path, EnumSweepTransitionMode.RoundCorner, true, false, false);
        //var shape = FeatureTool.SweepByFrenet(sketch1, path, EnumSweepTransitionMode.RoundCorner, true, true);

        mRenderView.ShowShape(shape, new Vector3(0.8f));

    }


    List<GPntList> listGData = new List<GPntList>();
    GPntList mPointList = new GPntList();
    private void getPath2()
    {
        listGData = new List<GPntList>();

        string path = GetResourcePath("/data/字符坐标/");
        var listX = Directory.GetFiles(path, "X*.txt", SearchOption.AllDirectories).ToList<string>();
        var listY = Directory.GetFiles(path, "Y*.txt", SearchOption.AllDirectories).ToList<string>();
        listX.Sort();
        listY.Sort();
        for (int i = 0; i < listY.Count; i++)
        {

            var listPointX = File.ReadAllLines(listX[i]);
            var listPointY = File.ReadAllLines(listY[i]);

            GPntList listPoint = new GPntList();

            for (int k = 0; k < listPointX.Length; k++)
            {
                double x = Math.Round(Convert.ToDouble(listPointX[k]), 5);
                double y = Math.Round(Convert.ToDouble(listPointY[k]), 5);

                listPoint.Add(new GPnt(x, y, 50));

                if (k == 0)
                {
                    mPointList.Add(new GPnt(x, y, 50));
                }                
            }

            listGData.Add(listPoint);
        }

    }

}