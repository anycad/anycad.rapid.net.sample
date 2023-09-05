using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_OffsetSolid : TestCase
    {
        public override void Run(IRenderView render)
        {
            var solid = ShapeBuilder.MakeCylinder(new GAx2(), 10, 10, System.Math.PI/4 * 5);

            {
                var ss = FeatureTool.OffsetShape(solid, 3, EnumGeomJoinType.Intersection);
                if (ss == null)
                    return;

                var material = MeshPhongMaterial.Create("tansparent");
                material.SetOpacity(0.5f);
                material.SetTransparent(true);
                material.SetFaceSide(EnumFaceSide.DoubleSide);
                material.SetColor(ColorTable.LightYellow);

                var node = BrepSceneNode.Create(ss, material, null);
                render.ShowSceneNode(node);
            }
            render.ShowShape(solid, ColorTable.Red);
        }
    }
}
