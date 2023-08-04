using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_SweepLoft : TestCase
    {
        public void SweepBySections1(IRenderView render)
        {
            var path = SketchBuilder.MakeArcOfCircle(new GPnt(0, 0, 0), new GPnt(10, 0, 10), new GPnt(5, 0, 8));
            var baseSketch = SketchBuilder.MakeRectangle(new GAx2(new GPnt(-5, -10, 0), GP.DZ(), GP.DX()), 10, 20, 2, false);
            var topSketch = SketchBuilder.MakeCircle(new GPnt(10, 0, 10), 5, GP.DX());

            var shapeList = new TopoShapeList();
            shapeList.Add(baseSketch);
            shapeList.Add(topSketch);
            var shape = FeatureTool.SweepBySections(shapeList, path, EnumSweepTransitionMode.RoundCorner, true);

            render.ShowShape(shape, ColorTable.Beige);
        }

        public void SweepBySections2(IRenderView render)
        {
            var baseWire = SketchBuilder.MakeRectangle(new GAx2(new GPnt(35, -60, 0), GP.DZ()), 30, 20, 9, false);
            var topWire = FeatureTool.OffsetWire(baseWire, 6, 30, EnumGeomJoinType.Arc, false);
            var midWire = FeatureTool.OffsetWire(baseWire, 2, 15, EnumGeomJoinType.Arc, false);

            TopoShapeList topLst = new TopoShapeList();
            topLst.Add(baseWire);
            topLst.Add(midWire);
            topLst.Add(topWire);
            var path2 = SketchBuilder.MakeLine(new GPnt(35 + 15, -60 + 10, 0), new GPnt(35 + 15, -60 + 10, 30));

            render.ShowShape(baseWire, ColorTable.Violet);
            render.ShowShape(midWire, ColorTable.Violet);
            render.ShowShape(topWire, ColorTable.Violet);

            render.ShowShape(path2, ColorTable.Violet);

            var sweepFrenet = FeatureTool.SweepBySections(topLst, path2, EnumSweepTransitionMode.Transformed, true);
            render.ShowShape(sweepFrenet, ColorTable.LightYellow);
        }

        public override void Run(IRenderView render)
        {
            SweepBySections1(render);
            SweepBySections2(render);
        }
    }
}
