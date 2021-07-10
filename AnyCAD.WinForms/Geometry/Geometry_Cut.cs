using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Cut : TestCase
    {
        public override void Run(RenderControl render)
        {
            string filepathp = GetResourcePath(@"data\blade_p.dat");    // p侧字符型数据
            string[] data_p = File.ReadAllLines(filepathp, Encoding.Default);  // 1行    2401列，
            string filepaths = GetResourcePath(@"data\blade_s.dat");    // s侧字符型数据
            string[] data_s = File.ReadAllLines(filepaths, Encoding.Default);
            //
            // p侧的曲线
            double[,] curvee = new double[(data_p.GetUpperBound(data_p.Rank - 1) + 1), 3];   //建立数组，n行3列，
            for (int i = 0; i < (data_p.GetUpperBound(data_p.Rank - 1) + 1); i++)//
            {
                data_p[i].Split();
                string[] split = data_p[i].Split(new Char[] { ' ' });
                curvee[i, 0] = double.Parse(split[0]);
                curvee[i, 1] = double.Parse(split[1]);
                curvee[i, 2] = double.Parse(split[2]);
            }
            //
            // s侧的曲线，
            double[,] curvee1 = new double[(data_p.GetUpperBound(data_p.Rank - 1) + 1), 3];   //建立数组，n行3列，
            for (int i = 0; i < (data_p.GetUpperBound(data_p.Rank - 1) + 1); i++)//
            {
                data_s[i].Split();
                string[] split = data_s[i].Split(new Char[] { ' ' });
                curvee1[i, 0] = double.Parse(split[0]);
                curvee1[i, 1] = double.Parse(split[1]);
                curvee1[i, 2] = double.Parse(split[2]);
            }
            int n_jie = (int)curvee1[0, 1]; // 截面数
            int n_points = (int)curvee1[0, 0]; // 点数
            // topolist
            TopoShapeList spline = new TopoShapeList(); //进行拓扑list的生成，
            for (int nn = 0; nn < n_jie; nn++)   // 循环截面数，
            {
                //int nn = 0;
                GPntList points = new GPntList();
                // s侧
                for (int mm = nn * n_points + 1; mm < (nn + 1) * n_points; mm++) // 循环截面所对应的点数范围，
                {
                    GPnt P0 = new GPnt(curvee[mm, 0], curvee[mm, 1], curvee[mm, 2]);//   需要进行添加的点，
                    points.Add(P0);
                }
                // p侧
                for (int mm = (nn + 1) * n_points; mm > nn * n_points + 1; mm--) // 循环截面所对应的点数范围，
                {
                    GPnt P0 = new GPnt(curvee1[mm, 0], curvee1[mm, 1], curvee1[mm, 2]);//   需要进行添加的点，
                    points.Add(P0);
                }
                // var quxian = SketchBuilder.MakePolygon(points, true);  //生成拓扑，
                var quxian = SketchBuilder.MakeBSpline(points, true);
                //mRenderView.ShowShape(quxian, ColorTable.Red);
                spline.Add(quxian);
            }
            var shape = FeatureTool.Loft(spline, true, false);

            //创建一个体进行布尔运算
            GPntList points_shroud = new GPntList();
            GPnt P10 = new GPnt(170, 0, 0);
            GPnt P11 = new GPnt(170, 0, 200);
            GPnt P12 = new GPnt(250, 0, 200);
            GPnt P13 = new GPnt(250, 0, 0);
            points_shroud.Add(P10);
            points_shroud.Add(P11);
            points_shroud.Add(P12);
            points_shroud.Add(P13);
            var quxian1 = SketchBuilder.MakePolygon(points_shroud, true);
            var face = SketchBuilder.MakePlanarFace(quxian1);
            var feature = FeatureTool.Revol(face, new GAx1(new GPnt(0, 0, 0), GP.DZ()), 0);

            var cut = BooleanTool.Cut(shape, feature);   // 求差  保留前面的一个
            if (cut != null)
                render.ShowShape(cut, ColorTable.Green);

            //var cut2 = BooleanTool.Cut(feature, shape);
            //if (cut2 != null)
            //    render.ShowShape(cut2, ColorTable.Blue);
        }
    }
}
