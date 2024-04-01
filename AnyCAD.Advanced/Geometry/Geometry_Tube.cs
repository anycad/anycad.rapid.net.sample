using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Tube : TestCase
    {
        public override void Run(IRenderView render)
        {
            // 圆管
            var tube = AdvShapeBuilder.MakeTube(new GAx2(), 8, 1, 100);

            // 矩形管
            var rectTube = AdvShapeBuilder.MakeRectTube(new GAx2(new GPnt(40, 0, 0), new GDir(0, 0, 1)), 15, 20, 2, 2, 100);

            // 实心
            var rectSolid = AdvShapeBuilder.MakeRectSolid(new GAx2(new GPnt(40, 40, 0), new GDir(0, 0, 1)), 15, 20, 2, 100);


            // 腰管
            var waistTube = AdvShapeBuilder.MakeWaistTube(new GAx2(new GPnt(80, 0, 0), new GDir(0, 0, 1)), 20, 5, 1, 100);

            var waistSolid = AdvShapeBuilder.MakeWaistSolid(new GAx2(new GPnt(80, 40, 0), new GDir(0, 0, 1)), 20, 5, 100);


            var edgeMaterial = LineDashedMaterial.Create("tube.material.edge");
            edgeMaterial.SetColor(ColorTable.Wheat);

            // 显示
            render.ShowSceneNode(BrepSceneNode.Create(tube, MaterialStore.Chrome, edgeMaterial));
            
            render.ShowSceneNode(BrepSceneNode.Create(rectTube, MaterialStore.Copper, edgeMaterial));
            render.ShowSceneNode(BrepSceneNode.Create(rectSolid, MaterialStore.Brass, edgeMaterial));

            render.ShowSceneNode(BrepSceneNode.Create(waistTube, MaterialStore.YellowPlastic, edgeMaterial));
            render.ShowSceneNode(BrepSceneNode.Create(waistSolid, MaterialStore.YellowRubber, edgeMaterial));
        }
    }
}
