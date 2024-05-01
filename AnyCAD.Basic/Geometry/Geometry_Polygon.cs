using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Polygon : TestCase
    {
        BrepSceneNode polygon;
        public override void Run(IRenderView render)
        {
            TopoShape plygon = CreatePolygon3D("Polygon3D;Vertices 4;-9304.4420,-15618.4244,-6850.9019;-17648.5478,-2404.9367,-7268.8830;4273.9075,11415.8433,-7992.3386;12618.0134,-1797.6443,-7574.3575;Center -2515.2672,-2101.2905,-7421.6202;");

            MeshStandardMaterial mat = MeshStandardMaterial.Create("my-material");
            mat.SetFaceSide(EnumFaceSide.DoubleSide);
            mat.SetOpacity(0.75f);
            mat.SetRoughness(0.5f);
            mat.SetTransparent(true);
            mat.SetMetalness(0.2f);
            mat.SetColor(ColorTable.Green);
            polygon = BrepSceneNode.Create(plygon, mat, mat);
            render.ShowSceneNode(polygon);

        }

        private TopoShape CreatePolygon3D(string FormattedString)
        {
            string[] splited = FormattedString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (splited.Length < 5) throw new Exception("Incorrect formatted string. At Polygon3D.CreatePolygon3D");
            int edgeCount = int.Parse(splited[1].Split(' ')[1]);
            GPntList verts = new GPntList();
            for (int i = 0; i < edgeCount; i++)
            {
                string[] coord = splited[i + 2].Split(',');
                float x = float.Parse(coord[0]);
                float y = float.Parse(coord[1]);
                float z = float.Parse(coord[2]);
                verts.Add(new GPnt(x, y, z));
            }
            return SketchBuilder.MakePolygonFace(verts);
        }

        public override void OnRunCommnad(IRenderView render)
        {
            render.ViewContext.GetSelectionManager().Clear();

            TopoShape shape = CreatePolygon3D("Polygon3D;Vertices 29;11763.0156,-2273.7361,-7548.5039;11598.7148,-2390.7390,-7542.5781;11270.8281,-2606.7117,-7531.4102;6156.4565,-5856.8823,-7361.6616;-8665.2275,-14314.6650,-6905.8032;-8858.6475,-14419.3203,-6900.0688;-9030.3350,-14384.4502,-6899.7739;-10551.6045,-13501.0205,-6918.7217;-10943.1914,-12897.0117,-6937.7334;-12387.6025,-10622.6172,-7009.6025;-15710.8936,-5359.6982,-7176.0854;-15968.1709,-2449.2700,-7282.9136;-15940.9082,-2342.3318,-7287.1816;-15903.9150,-2235.5625,-7291.5342;-15710.1064,-2066.0266,-7299.7070;-15316.9814,-1748.6245,-7315.2910;-12879.4395,86.7391,-7406.9399;-10173.6445,2072.7390,-7506.7476;-6596.3569,4380.3730,-7626.7661;41.9929,8552.3760,-7845.3452;4530.5542,9515.8262,-7923.4263;5014.0825,9311.2305,-7920.2637;5434.3643,9114.3574,-7916.8003;5786.1353,8933.9434,-7913.3149;5898.9507,8862.0928,-7911.6719;6206.1509,8375.9561,-7896.2959;7008.2178,7063.3589,-7854.5239;10894.9326,475.3387,-7643.5713;11778.1230,-2132.4121,-7553.9492;Center -2401.3079,-2113.7202,-7422.2181;");

            MeshStandardMaterial mat = MeshStandardMaterial.Create("my-material");
            mat.SetFaceSide(EnumFaceSide.DoubleSide);
            mat.SetOpacity(0.75f);
            mat.SetRoughness(0.5f);
            mat.SetTransparent(true);
            mat.SetMetalness(0.2f);
            mat.SetColor(ColorTable.Green);
            var buff = GRepShape.Create(shape, mat, null, 0.001, true);
            polygon.SetShape(buff);
            polygon.SetFaceMaterial(mat);
            polygon.RequestUpdate();

            render.RequestDraw(EnumUpdateFlags.Scene);
        }

    }
}
