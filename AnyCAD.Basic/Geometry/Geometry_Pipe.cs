using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Pipe : TestCase
    {

        public override void Run(IRenderView render)
        {
            var material = MeshStandardMaterial.Create("metal-double");
            material.SetColor(new Vector3(1.0f, 0.8f, 0.0f));
            material.SetMetalness(0.8f);
            material.SetRoughness(0.5f);
            material.SetFaceSide(EnumFaceSide.DoubleSide);


            var startPt = new GVec(0, 100, 0);
            var points = new GPntList();
            points.Add(new GPnt(startPt.XYZ()));
            points.Add(new GPnt(startPt.Added(new GVec(0, 0, 150)).XYZ()));
            points.Add(new GPnt(startPt.Added(new GVec(0, 100, 150)).XYZ()));
            points.Add(new GPnt(startPt.Added(new GVec(-100, 100, 150)).XYZ()));
            points.Add(new GPnt(startPt.Added(new GVec(-100, 300, 150)).XYZ()));
            points.Add(new GPnt(startPt.Added(new GVec(100, 300, 150)).XYZ()));
            TopoShape path = SketchBuilder.MakePolygon(points, false);

            var sectionList = new TopoShapeList();

            GAx2 coord1 = new GAx2(new GPnt(startPt.Added(new GVec(- 25, - 25, 0)).XYZ()), GP.DY());
            TopoShape section1 = SketchBuilder.MakeRectangle(coord1, 50, 50, 10, false);

            render.ShowShape(section1, ColorTable.Red);
            render.ShowShape(path, ColorTable.Green);

            TopoShape pipe = FeatureTool.SweepByFrenet(section1, path, EnumSweepTransitionMode.RoundCorner,
                false);
            BufferShape bs = new BufferShape(pipe, material, null, 0.1f);
            bs.Build();
            var node = new BrepSceneNode(bs);

            render.ShowSceneNode(node);
        }
    }
}
