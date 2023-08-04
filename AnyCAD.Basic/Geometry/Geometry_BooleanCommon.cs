using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_BoolenCommon : TestCase
    {
        public override void Run(IRenderView render)
        {
            TopoShapeList Group = new TopoShapeList();
            GPntList ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(19000, 28700, 8700));
            ST_P_Info.Add(new GPnt(19000, 30500, 8700));
            ST_P_Info.Add(new GPnt(21950, 30500, 8700));
            ST_P_Info.Add(new GPnt(21950, 28700, 8700));
            var face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group.Add(face);

            ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(19000, 28700, 8700));
            ST_P_Info.Add(new GPnt(19000, 30500, 8700));
            ST_P_Info.Add(new GPnt(19000, 30500, 8600));
            ST_P_Info.Add(new GPnt(19000, 28700, 8600));
            face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group.Add(face);

            ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(19000, 30500, 8700));
            ST_P_Info.Add(new GPnt(21950, 30500, 8700));
            ST_P_Info.Add(new GPnt(21950, 30500, 8600));
            ST_P_Info.Add(new GPnt(19000, 30500, 8600));
            face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group.Add(face);
            ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(21950, 30500, 8700));
            ST_P_Info.Add(new GPnt(21950, 28700, 8700));
            ST_P_Info.Add(new GPnt(21950, 28700, 8600));
            ST_P_Info.Add(new GPnt(21950, 30500, 8600));
            face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group.Add(face);
            ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(21950, 28700, 8700));
            ST_P_Info.Add(new GPnt(19000, 28700, 8700));
            ST_P_Info.Add(new GPnt(19000, 28700, 8600));
            ST_P_Info.Add(new GPnt(21950, 28700, 8600));
            face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group.Add(face);

            ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(19000, 28700, 8600));
            ST_P_Info.Add(new GPnt(19000, 30500, 8600));
            ST_P_Info.Add(new GPnt(21950, 30500, 8600));
            ST_P_Info.Add(new GPnt(21950, 28700, 8600));
            face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group.Add(face);


            TopoShape extrude_Cut = ShapeBuilder.MakeSolid(ShapeBuilder.MakeCompound(Group));


            TopoShapeList Group2 = new TopoShapeList();
            ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(19100, 30100, 8700));
            ST_P_Info.Add(new GPnt(18900, 30100, 8700));
            ST_P_Info.Add(new GPnt(18900, 29000, 8700));
            ST_P_Info.Add(new GPnt(19100, 29000, 8700));
            face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group2.Add(face);
            ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(19100, 30100, 8700));
            ST_P_Info.Add(new GPnt(18900, 30100, 8700));
            ST_P_Info.Add(new GPnt(18900, 30100, 5800));
            ST_P_Info.Add(new GPnt(19100, 30100, 5800));
            face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group2.Add(face);

            ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(18900, 30100, 8700));
            ST_P_Info.Add(new GPnt(18900, 29000, 8700));
            ST_P_Info.Add(new GPnt(18900, 29000, 5800));
            ST_P_Info.Add(new GPnt(18900, 30100, 5800));
            face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group2.Add(face);
            ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(18900, 29000, 8700));
            ST_P_Info.Add(new GPnt(19100, 29000, 8700));
            ST_P_Info.Add(new GPnt(19100, 29000, 5800));
            ST_P_Info.Add(new GPnt(18900, 29000, 5800));
            face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group2.Add(face);
            ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(19100, 29000, 8700));
            ST_P_Info.Add(new GPnt(19100, 30100, 8700));
            ST_P_Info.Add(new GPnt(19100, 30100, 5800));
            ST_P_Info.Add(new GPnt(19100, 29000, 5800));
            face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group2.Add(face);

            ST_P_Info = new GPntList();
            ST_P_Info.Add(new GPnt(19100, 30100, 5800));
            ST_P_Info.Add(new GPnt(18900, 30100, 5800));
            ST_P_Info.Add(new GPnt(18900, 29000, 5800));
            ST_P_Info.Add(new GPnt(19100, 29000, 5800));
            face = SketchBuilder.MakePlanarFace(SketchBuilder.MakePolygon(ST_P_Info, true));
            Group2.Add(face);

            TopoShape extrude = ShapeBuilder.MakeSolid(ShapeBuilder.MakeCompound(Group2));

            extrude = BooleanTool.Unify(extrude, true, true, false);
            extrude_Cut = BooleanTool.Unify(extrude_Cut, true, true, false);

            extrude = FixShapeTool.FixSolid(extrude);
            extrude_Cut = FixShapeTool.FixSolid(extrude_Cut);

            TopoShape boolCommon = BooleanTool.Common(extrude_Cut, extrude);
            var m0 = MeshStandardMaterial.Create("xx");
            m0.SetColor(ColorTable.Red);
            var node0 = BrepSceneNode.Create(boolCommon, m0, null);
            render.ShowSceneNode(node0);


            var m1 = MeshStandardMaterial.Create("xx");
            m1.SetTransparent(true);
            m1.SetOpacity(0.5f);
            m1.SetColor(ColorTable.Green);
            var node = BrepSceneNode.Create(extrude, m1, null);
            render.ShowSceneNode(node);


            var m2 = MeshStandardMaterial.Create("xx");
            m2.SetTransparent(true);
            m2.SetOpacity(0.5f);
            m2.SetColor(ColorTable.BlueViolet);
            var node2 = BrepSceneNode.Create(extrude_Cut, m2, null);
            render.ShowSceneNode(node2);
        }
    }
}
