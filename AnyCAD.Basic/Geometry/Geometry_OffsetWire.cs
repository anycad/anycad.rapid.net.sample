using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_OffsetWire : TestCase
    {
        public TopoShape Test(double dBTop)
        {
            double dC = 1.4;

            double dATop = 3.0;
            double dCirPosY = dBTop * 0.5 - 1.5;
            double dCirPosZ = -0.1;
            double dFltSize = 0.15;

            var polyPt1 = new GPnt(-dATop * 0.5, dCirPosY, dCirPosZ);
            var polyPt2 = new GPnt(-dATop * 0.5, dC * 0.5 + dFltSize, dCirPosZ);
            var polyPt3 = new GPnt(-dATop * 0.5 + dFltSize, dC * 0.5, dCirPosZ);
            var polyPt4 = new GPnt(dATop * 0.5 - dFltSize, dC * 0.5, dCirPosZ);
            var polyPt5 = new GPnt(dATop * 0.5, dC * 0.5 + dFltSize, dCirPosZ);
            var polyPt6 = new GPnt(dATop * 0.5, dCirPosY, dCirPosZ);
            GPntList polyPts = new GPntList();
            polyPts.Add(polyPt1);
            polyPts.Add(polyPt2);
            polyPts.Add(polyPt3);
            polyPts.Add(polyPt4);
            polyPts.Add(polyPt5);
            polyPts.Add(polyPt6);

            var polyWire = SketchBuilder.MakePolygon(polyPts, false);

            var topLst = new TopoShapeList();
            //topLst.Add(SketchBuilder.MakeLine(polyPt1, polyPt2));
            //topLst.Add(SketchBuilder.MakeLine(polyPt2, polyPt3));
            //topLst.Add(SketchBuilder.MakeLine(polyPt3, polyPt4));
            //topLst.Add(SketchBuilder.MakeLine(polyPt4, polyPt5));
            //topLst.Add(SketchBuilder.MakeLine(polyPt5, polyPt6));
            //var polyWire = SketchBuilder.MakeWire(topLst);

            var midProf = FeatureTool.OffsetWire(polyWire, -dFltSize, dFltSize, EnumGeomJoinType.Tangent, true);
            topLst.Clear();
            topLst.Add(midProf);
            topLst.Add(polyWire);

            midProf = ShapeBuilder.MakeCompound(topLst);

            return midProf;
        }

        public void OffsetArc(IRenderView render)
        {
            var arc = SketchBuilder.MakeArcOfCircle(new GCirc(GP.XOY(), 10), System.Math.PI * 0.25, System.Math.PI*0.75);

            var arc2 = FeatureTool.OffsetWire(arc, 1, 0, EnumGeomJoinType.Arc, true);

            render.ShowShape(arc, ColorTable.AliceBlue);
            render.ShowShape(arc2, ColorTable.DarkRed);
        }

        public override void Run(IRenderView render)
        {
            //var sketch = SketchBuilder.MakeRectangle(GP.XOY(), 100, 50, 5, false);
            //render.ShowShape(sketch, ColorTable.Blue);

            //var types = new EnumGeomJoinType[]{ EnumGeomJoinType.Arc, EnumGeomJoinType.Intersection, EnumGeomJoinType.Tangent };

            //for (int ii=0; ii<3; ++ii)
            //{
            //    var offset = FeatureTool.OffsetWire(sketch, 2*ii + 2, 0, types[ii], false);
            //    render.ShowShape(offset, ColorTable.Green);
            //}

            var topShp = Test(7.8);
            var topShp2 = Test(15.6);
            render.ShowShape(topShp, ColorTable.Red);
            render.ShowShape(topShp2, ColorTable.Green);

            OffsetArc(render);
        }
    }
}
