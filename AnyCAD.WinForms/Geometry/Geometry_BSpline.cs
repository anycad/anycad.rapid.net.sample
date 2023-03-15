using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Geometry_BSpline : TestCase
    {
        public override void Run(IRenderView render)
        {
            var material = MeshPhongMaterial.Create("phong.bspline");
            material.SetUniform("diffuse", new Vector3(1, 0, 1));

            var shape = CreateShape();
            var node = BrepSceneNode.Create(shape, material, null);

            render.ShowSceneNode(node);
        }

        TopoShape CreateShape()
        {
            GPntList v3list = new GPntList();
            double[,] startpiont = new double[1, 3] { { 0, 0, 10 } };//起点
            double length = 10;//波长
            double width = 4;//波峰减去波谷差值
            int nums = 5;//支点个数
            double[,] list = GetVerticalCurveList(startpiont, length, width, nums);
            for (int i = 0; i < list.GetLength(0); i++)
            {
                GPnt P0 = new GPnt(list[i, 0], list[i, 1], list[i, 2]);//直井井室入口 起点
                v3list.Add(P0);
            }

            GVec aa = new GVec(); aa.SetXYZ(v3list[1].XYZ());
            GVec bb = new GVec(); bb.SetXYZ(v3list[0].XYZ());
            GDir dir = new GDir();
            dir.SetXYZ(aa.Subtracted(bb).XYZ());

            TopoShape section = SketchBuilder.MakeCircle(v3list[0], 0.1, dir);
            TopoShape line9 = SketchBuilder.MakeBSpline(v3list);

            return FeatureTool.Sweep(section, line9, EnumGeomFillTrihedron.CorrectedFrenet);
        }

        public static double[,] GetVerticalCurveList(double[,] startpiont, double length, double width, int nums)
        {
            int len = (nums - 1) * 4 + 1;
            double[,] ps = new double[len, 3];
            for (int i = 0; i < len - 1; i++)
            {
                ps[i, 0] = startpiont[0, 0] + i / 4 * length;
                ps[i, 1] = startpiont[0, 1];
                ps[i, 2] = startpiont[0, 2];

                ps[i + 1, 0] = startpiont[0, 0] + i / 4 * length + length / 4;
                ps[i + 1, 1] = startpiont[0, 1];
                ps[i + 1, 2] = startpiont[0, 2] - width / 2;

                ps[i + 2, 0] = startpiont[0, 0] + i / 4 * length + 2 * length / 4;
                ps[i + 2, 1] = startpiont[0, 1];
                ps[i + 2, 2] = startpiont[0, 2] - width;

                ps[i + 3, 0] = startpiont[0, 0] + i / 4 * length + 3 * length / 4;
                ps[i + 3, 1] = startpiont[0, 1];
                ps[i + 3, 2] = startpiont[0, 2] - width / 2;

                i += 3;

            }
            ps[len - 1, 0] = startpiont[0, 0] + (nums - 1) * length;
            ps[len - 1, 1] = startpiont[0, 1];
            ps[len - 1, 2] = startpiont[0, 2];
            return ps;
        }
    }
}
