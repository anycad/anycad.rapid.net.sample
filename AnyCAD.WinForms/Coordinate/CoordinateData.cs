using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;
namespace AnyCAD.Demo.Coordinate
{

    internal class MyObject
    {
        public SceneNode Node = null;
        public GAx2 Coordinate = new GAx2();
        public Matrix4 LocalBiasTrf = new Matrix4(1);
        public AxisWidget Axis = AxisWidget.Create(1, new Vector3(5));

        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Z { get; set; } = 0;

        public double A { get; set; } = 0;
        public double B { get; set; } = 0;
        public double C { get; set; } = 0;

        public uint Id { get; set; } = 0;
        public MyObject()
        {

        }

        public void Show(RenderControl render)
        {
            render.ShowSceneNode(Node);
            render.ShowSceneNode(Axis);
        }

        public void UpdateUI(GAx2 toCoord)
        {
            GTrsf worldToReletive = new GTrsf();
            worldToReletive.SetTransformation(new GAx3(GP.XOY()), new GAx3(toCoord));

            var xyz = Coordinate.Location().Transformed(worldToReletive);
            X = xyz.X();
            Y = xyz.Y();
            Z = xyz.Z();

            GTrsf t = new GTrsf();
            t.SetTransformation(new GAx3(Coordinate), new GAx3(GP.XOY()));
            t.Multiply(worldToReletive);
            var quat = t.GetRotation();
            double a =0, b = 0, c = 0;
            quat.GetEulerAngles(GEulerSequence.gp_YawPitchRoll, ref a, ref b, ref c);

            A = 180 / Math.PI * a;
            B = 180 / Math.PI * b;
            C = 180 / Math.PI * c;
        }

        public void UpdateData(GAx2 toCoord)
        {
            // 
            GQuaternion rotate = new GQuaternion();
            rotate.SetEulerAngles(GEulerSequence.gp_YawPitchRoll, A * Math.PI/180, B * Math.PI / 180, C * Math.PI / 180);
            GTrsf current = new GTrsf();
            current.SetRotationPart(rotate);
            current.SetTranslationPart(new GVec(X, Y, Z));
            Coordinate = new GAx2();
            Coordinate.Transform(current);


            GTrsf trf = new GTrsf();
            trf.SetTransformation(new GAx3(toCoord), new GAx3(GP.XOY()));
            Coordinate.Transform(trf);

            UpdateWorldTransform();
        }

        protected void UpdateWorldTransform()
        {
            var trf = new GTrsf();
            trf.SetTransformation(new GAx3(Coordinate), new GAx3());
            var mat = Matrix4.makeTransform(trf);

            Axis.SetTransform(mat);
            Axis.RequstUpdate();

            Node.SetTransform(mat * LocalBiasTrf);
            Node.RequstUpdate();
        }
    }

    internal class MyPart : MyObject
    {
        public MyPart()
        {
            var shape = GeometryBuilder.CreateBox(5, 10, 15);
            var material = MeshPhongMaterial.Create("test");
            material.SetColor(ColorTable.BrulyWood);
            var node = new PrimitiveSceneNode(shape, material);
            Id = node.GetUuid();
            var sz = node.GetBoundingBox().getSize();
            var trf = Matrix4.makeTranslation(0, 0, sz.z * 0.5f);
            node.SetTransform(trf);

            Coordinate = new GAx2();
            Node = node;
            LocalBiasTrf = trf;

            X = Coordinate.Location().X();
            Y = Coordinate.Location().Y();
            Z = Coordinate.Location().Z();

            UpdateWorldTransform();
        }
    }

    internal class MyPlane : MyObject
    {
        public MyPlane()
        {
            Id = 200;

            var shape = GeometryBuilder.CreatePlane(200, 200);
            var material = MeshPhongMaterial.Create("test");
            material.SetColor(ColorTable.Gray);

            var node = new PrimitiveSceneNode(shape, material);
            Id = node.GetUuid();
            var sz = node.GetBoundingBox().getSize();
            LocalBiasTrf = new Matrix4(1);
 
            Coordinate = new GAx2(new GPnt(sz.x * 0.5f, sz.y * 0.5f, 0), GP.DZ());
            Node = node;
          
            X = Coordinate.Location().X();
            Y = Coordinate.Location().Y();
            Z = Coordinate.Location().Z();

            UpdateWorldTransform();
        }
    }

}
