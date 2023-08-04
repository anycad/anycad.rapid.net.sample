using AnyCAD.Foundation;
using System.IO;

namespace AnyCAD.Demo.Graphics
{
    class PointDataNormalColor
    {
        public Float32Buffer Points;
        public Float32Buffer Normals;
        public Float32Buffer Colors;
    }

    class Graphics_PointCloudWithNormal : TestCase
    {
        PointDataNormalColor ReadData(string fileName, Vector3 color)
        {
            using (StreamReader reader = File.OpenText(fileName))
            {
                PointDataNormalColor pointData = new PointDataNormalColor();

                pointData.Points = new Float32Buffer(0);
                pointData.Colors = new Float32Buffer(0);
                pointData.Normals = new Float32Buffer(0);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var items = line.Split(' ');
                    if (items.Length != 6)
                        continue;

                    for (int ii = 0; ii < 3; ++ii)
                    {
                        pointData.Points.Append(float.Parse(items[ii]));

                    }

                    for (int ii = 3; ii < 6; ++ii)
                    {
                        pointData.Normals.Append(float.Parse(items[ii]));
                    }

                    pointData.Colors.Append3(color);
                }
                return pointData;
            }
        }

        public override void Run(IRenderView render)
        {
            var points1 = ReadData(GetResourcePath(@"Points\xiaomianpian.txt"), ColorTable.Gray);
            if (points1 == null)
                return;

            var geom = GeometryBuilder.CreatePoints(new Float32Array(points1.Points),
                new Float32Array(points1.Colors), new Float32Array(points1.Normals));

            var mat = MeshPhongMaterial.Create("pt.normal");

            //var node = new PrimitiveSceneNode(geom, mat);
            //render.ShowSceneNode(node);
            PointCloud mPointCloud1 = PointCloud.Create(points1.Points, points1.Colors, points1.Normals, 3);

            render.ShowSceneNode(mPointCloud1);
        }

    }
}
