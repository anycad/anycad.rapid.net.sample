using AnyCAD.Foundation;
using System;
using System.Reflection;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_PosAxis : TestCase
    {
        public override void Run(IRenderView renderer)
        {
            var origonNode = AxisWidget.Create(0.1f, new Vector3(1));

            var angle = (float)Math.PI / 4;
            var trf = Matrix4.makeTranslation(10, 11, 12) * Matrix4.fromEulerAngleXYZ(angle, angle, angle);
            origonNode.SetTransform(trf);

            var dem = trf.decomposeTRS();

            var angels = dem.rotation.conjugate().eulerAngles();

            var aa = angle * 180 / Math.PI;
            var bb = angels.x * 180 / Math.PI;

            var cc = trf.extractEulerAngleXYZ();

            var trf2 = Matrix4.makeTranslation(dem.translation) * dem.rotation.toMatrix4();
            var origonNode2 = AxisWidget.Create(0.1f, new Vector3(1));
            origonNode2.SetTransform(trf2);
            renderer.ShowSceneNode(origonNode2);

            var box = ShapeBuilder.MakeBox(GP.XOY(), 5, 10, 10);
            var plane = SketchBuilder.MakePlanarFace(new GPln(new GPnt(0, 0, 2), GP.DZ()), -5, 20, -5, 20);
            var section = BooleanTool.Section(box, plane);
            var edges = section.GetChildren(EnumTopoShapeType.Topo_EDGE);
            var wire = SketchBuilder.MakeWire(edges);

            var material = MeshPhongMaterial.Create("solid");
            material.SetTransparent(true);
            material.SetOpacity(0.5f);
            material.SetColor(ColorTable.AliceBlue);

            renderer.ShowSceneNode(BrepSceneNode.Create(box, material, null));

            var curve = new ParametricCurve(wire);
            var parameters = curve.SplitByUniformLength(1, 0.001f);

            foreach (var p in parameters)
            {
                var v = curve.D2(p);
                var pos = v.GetPoint();
                var dirs = v.GetVectors();
                var dd = dirs[0].Normalized();
                var ax = new GAx3(pos, GP.DZ(), new GDir(dd.XYZ()));
                var axw = AxisWidget.Create(0.05f, new Vector3(0.5f));
                axw.SetTransform(Matrix4.makeFromAx3(ax));
                renderer.ShowSceneNode(axw);
            }

            renderer.ShowShape(wire, ColorTable.DarkRed);
        }
    }
}
