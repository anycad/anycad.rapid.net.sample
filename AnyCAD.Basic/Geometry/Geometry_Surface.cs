using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Surface : TestCase
    {
        public override void Run(IRenderView render)
        {
            Random rand = new Random();
            GPntList points = new GPntList();
            for (int ii = 0; ii < 5; ++ii)
                for (int jj = 0; jj < 5; ++jj)
                {
                    points.Add(new GPnt(ii, jj, rand.NextDouble()));
                }

            var surf = SurfaceBuilder.PointsToBSplineSurface(points, 5);

            var material = MeshStandardMaterial.Create("plastic");
            material.SetRoughness(0.1f);
            material.SetMetalness(0.4f);
            material.SetColor(new Vector3(0.98f, 0.55f, 0.33f));
            material.SetFaceSide(EnumFaceSide.DoubleSide);
            var node = BrepSceneNode.Create(surf, material, null);
            render.ShowSceneNode(node);

        }
    }
}
