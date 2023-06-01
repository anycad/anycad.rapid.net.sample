using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_PointPlane : TestCase
    {
        public override void Run(IRenderView renderer)
        {
            var pt = new GPnt();
            var plane = SketchBuilder.MakePlanarFace(new GPln(new GPnt(200, 0, 0), new GDir(1, 0, 0)), -100, 100, -100, 100);

            var etrema = new ExtremaPointSurface();
            if(etrema.Initialize(plane, pt))
            {
                var dist = etrema.GetSquareDistance(0);
                DialogUtil.ShowMessageBox("距离", dist.ToString());
            }
        }
    }
}
