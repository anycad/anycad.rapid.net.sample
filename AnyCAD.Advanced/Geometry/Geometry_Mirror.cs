using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Mirror : TestCase
    {
        public override void Run(IRenderView render)
        {
            double TCD = 30;
            double BCD = 50;
            double Ht = 70;
            double Bow = 10;
            double x1 = BCD / 2.0;
            double x2 = TCD / 2.0;
            double x3 = (BCD + TCD) / 4.0 + Bow;
            double y2 = Ht;
            double y3 = Ht / 2.0;
            double a, b, R;
            double Z = 0;
            GetCircleParam(x1, 0, x2, y2, x3, y3, out a, out b, out R);
            GPnt centerR = new GPnt(a, 0, b + Z);
            var bowCR = SketchBuilder.MakeCircle(centerR, R, GP.DY());
            bowCR = SketchBuilder.MakePlanarFace(bowCR);

            GPntList pntsT = new GPntList();
            pntsT.Add(new GPnt(TCD / 2.0, 0, Z + Ht));
            pntsT.Add(new GPnt(-TCD / 2.0, 0, Z + Ht));
            pntsT.Add(new GPnt(-BCD / 2.0, 0, Z));
            pntsT.Add(new GPnt(BCD / 2.0, 0, Z));
            var tra = SketchBuilder.MakePolygon(pntsT, true);
            tra = SketchBuilder.MakePlanarFace(tra);
            if (Bow < 0)
            {
                var bowCL = TransformTool.Mirror(bowCR, new GAx1(new GPnt(0, 0, Z + Ht), GP.DZ()));
                tra = BooleanTool.Cut(tra, bowCR);
                tra = BooleanTool.Cut(tra, bowCL);
            }
            else
            {
                GPntList pntRT = new GPntList();
                pntRT.Add(new GPnt(TCD / 2.0, 0, Z + Ht));
                pntRT.Add(new GPnt(TCD / 2.0 + BCD, 0, Z + Ht));
                pntRT.Add(new GPnt(BCD / 2.0 + TCD, 0, Z));
                pntRT.Add(new GPnt(BCD / 2.0, 0, Z));
                var traR = SketchBuilder.MakePolygon(pntRT, true);
                traR = SketchBuilder.MakePlanarFace(traR);
                bowCR = BooleanTool.Common(traR, bowCR);
                var bowCL = TransformTool.Mirror(bowCR, new GAx1(new GPnt(0, 0, Z + Ht), GP.DZ()));

                tra = BooleanTool.Sewing(tra, bowCR);
                tra = BooleanTool.Sewing(tra, bowCL);
                tra = BooleanTool.Unify(tra, true, true, true);
            }
           
            tra = TransformTool.Translate(tra, new GVec(0, (-100) / 2.0, 0));
            tra = FeatureTool.Extrude(tra, 100, GP.DY());
            render.ShowShape(tra, Vector3.ColorFromHex(shapeColor));

            var offset = FeatureTool.OffsetShape(tra, 5, EnumGeomJoinType.Intersection);

            if(offset != null)
            {
                var material = MeshPhongMaterial.Create("transparent");
                material.SetOpacity(0.5f);
                material.SetTransparent(true);

                var node = BrepSceneNode.Create(offset, material, null);
                render.ShowSceneNode(node);
            }
        }

        private uint shapeColor = 0x1d953f;
        public static void GetCircleParam(double x1, double y1, double x2, double y2, double x3, double y3, out double a, out double b, out double r)
        {
            double a_deno = ((y1 - y3) * (x1 - x2) - (y1 - y2) * (x1 - x3)) * 2;
            double b_deno = ((y1 - y2) * (x1 - x3) - (y1 - y3) * (x1 - x2)) * 2;
            if (a_deno == 0 || b_deno == 0)
            {
                a = 0;
                b = 0;
                r = 0;
                return;
            }
            double a_num = (x1 + x2) * (x1 - x2) * (y1 - y3) + (y1 + y2) * (y1 - y2) * (y1 - y3) - (x1 + x3) * (x1 - x3) * (y1 - y2) - (y1 + y3) * (y1 - y3) * (y1 - y2);
            a = a_num / a_deno;

            double b_num = (x1 + x2) * (x1 - x2) * (x1 - x3) + (y1 + y2) * (y1 - y2) * (x1 - x3) - (x1 + x3) * (x1 - x3) * (x1 - x2) - (y1 + y3) * (y1 - y3) * (x1 - x2);
            b = b_num / b_deno;

            r = Math.Sqrt(Math.Pow(x1 - a, 2) + Math.Pow(y1 - b, 2));
        }

    }
}
