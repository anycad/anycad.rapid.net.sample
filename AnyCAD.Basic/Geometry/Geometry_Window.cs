using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Window : TestCase
    {
        TopoShape CreateSketch(double width, double deep)
        {
            return SketchBuilder.MakeRectangle(GP.XOY(), width, deep, 0, false);
        }

        TopoShape Project(TopoShape baseShape, GPnt postion, GDir dir, GDir projDir)
        {
            return ProjectionTool.ProjectOnPlane(baseShape, postion, dir, projDir);
        }

        GroupSceneNode CreateFrames(double width, double height, double deep, double radius)
        {
            var baseSketch = CreateSketch(radius, deep);

            //TopLeft
            var s11 = Project(baseSketch, new GPnt(0,0,height), new GDir(-1, 0, -1), GP.DZ()); 
            //BottomLeft
            var s20 = Project(baseSketch, GP.Origin(), new GDir(-1, 0, 1), GP.DZ());
            //BottomRight
            var s12 = Project(s20, new GPnt(width,0,0), new GDir(1,0,1), GP.DX()); 
            //TopRight
            var s21 = Project(s11, new GPnt(width, 0, height), new GDir(-1, 0, 1), GP.DX());

            var frame1 = FeatureTool.Loft(s11, s20, false);
            var frame2 = FeatureTool.Loft(s20, s12, false);
            var frame3 = FeatureTool.Loft(s12, s21, false);
            var frame4 = FeatureTool.Loft(s21, s11, false);

            var frameMaterial = MeshStandardMaterial.Create("window-frame");
            frameMaterial.SetColor(ColorTable.LightGrey);
            frameMaterial.SetMetalness(0.5f);
            frameMaterial.SetFaceSide(EnumFaceSide.DoubleSide);

            var group = new GroupSceneNode();
            group.AddNode( BrepSceneNode.Create(frame1, frameMaterial, null));
            group.AddNode(BrepSceneNode.Create(frame2, frameMaterial, null));
            group.AddNode(BrepSceneNode.Create(frame3, frameMaterial, null));
            group.AddNode(BrepSceneNode.Create(frame4, frameMaterial, null));

            return group;
        }

        BrepSceneNode CreateGlass(GAx2 ax, double width, double height, double thickness)
        {
           var shape = ShapeBuilder.MakeBox(ax, width, thickness, height);

            var frameMaterial = MeshStandardMaterial.Create("window-glass");
            frameMaterial.SetColor(ColorTable.Hex(0xAAAAAA));
            frameMaterial.SetFaceSide(EnumFaceSide.DoubleSide);
            frameMaterial.SetTransparent(true);
            frameMaterial.SetOpacity(0.5f);

            return BrepSceneNode.Create(shape, frameMaterial, null);
        }

        SceneNode CreateAnnotation(Vector3 start, Vector3 end, Vector3 offset, String text)
        {
            var line =  GeometryBuilder.CreateLine(start + offset, end + offset);

            var mesh = FontManager.Instance().CreateMesh(text);
            var textNode = new PrimitiveSceneNode(mesh, null);

            float scale = 0.015f;
            textNode.SetTransform(Matrix4.makeRotationAxis(Vector3.UNIT_X, 3.14159f * 0.5f) *Matrix4.makeScale(scale, scale, 1));
            textNode.ComputeBoundingBox(Matrix4.Identity);
            float halfW = textNode.GetWorldBBox().getHalfSize().x;
            var dist = end.distanceTo(start);
            float ratio = (dist * 0.5f - halfW) / dist;
            textNode.SetTransform(Matrix4.makeTranslation((start + end) * ratio + offset) * textNode.GetTransform());


            var line2 = GeometryBuilder.CreateLine(start, start + offset + new Vector3(0,0,-2));
            var line3 = GeometryBuilder.CreateLine(end, end + offset + new Vector3(0, 0, -2));

            GroupSceneNode group = new GroupSceneNode();
            group.AddNode(new PrimitiveSceneNode(line, null));
            group.AddNode(new PrimitiveSceneNode(line2, null));
            group.AddNode(new PrimitiveSceneNode(line3, null));
            group.AddNode(textNode);

            return group;
        }
        public override void Run(IRenderView render)
        {
            var deep = 8.0f;
            var radius = 6;
            var width = 100;

            var frameNode = CreateFrames(width, 50, deep, 6);

            render.ShowSceneNode(frameNode);

            var ax = new GAx2(new GPnt(radius, (deep - 2) / 2, radius), GP.DZ());
            var glass = CreateGlass(ax, width - radius * 2, 50- radius * 2, 2);

            render.ShowSceneNode(glass);

            var dim = new AlignedDimensionNode(new Vector3(0, deep * 0.5f, 0), new Vector3(width, deep * 0.5f, 0),
                -10, "100");
            dim.SetAxisZ(new Vector3(0, -1, 0));
            dim.Update();
            render.ShowSceneNode(dim);
        }
    }
}
