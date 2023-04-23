using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Transform : TestCase
    {
        void CustumTransform(IRenderView mRenderView)
        {
            TopoShapeList topoShapes = new TopoShapeList();
            var Line1 = SketchBuilder.MakeLine(new GPnt(-0.15, 0.4, 0), new GPnt(0.15, 0.4, 0));
            var Line2 = SketchBuilder.MakeLine(new GPnt(0.15, 0.38, 0), new GPnt(0.0154999999999999, 0.379999999999998, 0));
            var Line3 = SketchBuilder.MakeLine(new GPnt(-0.15, 0.38, 0), new GPnt(-0.0155000000000002, 0.380000000000002, 0));
            var Line4 = SketchBuilder.MakeLine(new GPnt(-0.15, -0.38, 0), new GPnt(-0.0155000000000002, -0.380000000000005, 0));
            var Line5 = SketchBuilder.MakeLine(new GPnt(0.15, -0.38, 0), new GPnt(0.0155, -0.380000000000002, 0));
            var Line6 = SketchBuilder.MakeLine(new GPnt(-0.15, -0.4, 0), new GPnt(0.15, -0.4, 0));
            var Line7 = SketchBuilder.MakeLine(new GPnt(-0.0075, 0.372, 0), new GPnt(-0.0075, -0.372, 0));
            var Line8 = SketchBuilder.MakeLine(new GPnt(0.00749999999999999, 0.372, 0), new GPnt(0.00749999999999998, -0.372, 0));
            var Line9 = SketchBuilder.MakeLine(new GPnt(-0.15, 0.4, 0), new GPnt(-0.15, 0.38, 0));
            var Line10 = SketchBuilder.MakeLine(new GPnt(0.15, 0.4, 0), new GPnt(0.15, 0.38, 0));
            var Line11 = SketchBuilder.MakeLine(new GPnt(-0.15, -0.4, 0), new GPnt(-0.15, -0.38, 0));
            var Line12 = SketchBuilder.MakeLine(new GPnt(0.15, -0.4, 0), new GPnt(0.15, -0.38, 0));
            GCirc Cir1 = new GCirc(new GAx2(new GPnt(-0.0155000000000001, 0.372, 0), new GDir(0, 0, 1)), 0.008);
            var Arc1 = SketchBuilder.MakeArcOfCircle(Cir1, new GPnt(-0.00750000000000006, 0.372, 0), new GPnt(-0.0155000000000001, 0.38, 0));
            GCirc Cir2 = new GCirc(new GAx2(new GPnt(0.0154999999999999, 0.372, 0), new GDir(0, 0, 1)), 0.008);
            var Arc2 = SketchBuilder.MakeArcOfCircle(Cir2, new GPnt(0.0154999999999999, 0.38, 0), new GPnt(0.00749999999999993, 0.372, 0));
            GCirc Cir3 = new GCirc(new GAx2(new GPnt(-0.0155000000000001, -0.372, 0), new GDir(0, 0, 1)), 0.008);
            var Arc3 = SketchBuilder.MakeArcOfCircle(Cir3, new GPnt(-0.0155000000000001, -0.38, 0), new GPnt(-0.00750000000000006, -0.372, 0));
            GCirc Cir4 = new GCirc(new GAx2(new GPnt(0.0154999999999999, -0.372, 0), new GDir(0, 0, 1)), 0.008);
            var Arc4 = SketchBuilder.MakeArcOfCircle(Cir4, new GPnt(0.00749999999999993, -0.372, 0), new GPnt(0.0154999999999999, -0.38, 0));
            topoShapes.Add(Line1);
            topoShapes.Add(Line10);
            topoShapes.Add(Line2);
            topoShapes.Add(Arc2);
            topoShapes.Add(Line8);
            topoShapes.Add(Arc4);
            topoShapes.Add(Line5);
            topoShapes.Add(Line12);
            topoShapes.Add(Line6);
            topoShapes.Add(Line11);
            topoShapes.Add(Line4);
            topoShapes.Add(Arc3);
            topoShapes.Add(Line7);
            topoShapes.Add(Arc1);
            topoShapes.Add(Line3);
            topoShapes.Add(Line9);
            var Shape = ShapeBuilder.MakeCompound(topoShapes);
            Shape = SketchBuilder.MakeWire(topoShapes);
            var View = SketchBuilder.MakePlanarFace(Shape);

            var View1 = SketchBuilder.MakePlanarFace(Shape);
            View1 = TransformTool.Rotation(View1, new GAx1(new GPnt(0, 0, 0), new GDir(0, 1, 0)), Math.PI / 2);
            View1 = TransformTool.Rotation(View1, new GAx1(new GPnt(0, 0, 0), new GDir(1, 0, 0)), Math.PI / 2);
            mRenderView.ShowShape(View1, Vector3.Green);
            var widget = AxisWidget.Create(0.01f, new Vector3(0.1f));
            mRenderView.ShowShape(View, Vector3.Red);
            GAx3 gAx31 = new GAx3(new GAx2(new GPnt(0, 0, 0), new GDir(0, 0, 1), new GDir(1, 0, 0)));
            GAx3 gAx32 = new GAx3(new GAx2(new GPnt(0, 0, 0), new GDir(1, 0, 0), new GDir(0, 1, 0)));
            var dsad = gAx32.Direction().XYZ().X();
            GTrsf gTrsf = new GTrsf();
            gTrsf.SetTransformation(gAx32, gAx31);
            View = TransformTool.Transform(View, gTrsf);

            mRenderView.ShowShape(View, Vector3.Blue);
        }
        public override void Run(IRenderView render)
        {
            //render.SetViewCube(EnumViewCoordinateType.Axis);
            //CustumTransform(render);


            MeshStandardMaterial material = MeshStandardMaterial.Create("workpiece");
            material.SetColor(new Vector3(0.9f));
            material.SetFaceSide(EnumFaceSide.DoubleSide);
            material.SetTransparent(true);
            material.SetOpacity(0.5f);

            var box = ShapeBuilder.MakeBox(GP.XOY(), 10, 10, 10); //源toposhape
            var sphere = ShapeBuilder.MakeSphere(GP.Origin(), 5);

            //下面几行目的是把源toposhape往X轴移动一下，然后获取移动后的toposhape
            BrepSceneNode node = BrepSceneNode.Create(box, material, null);
            var trfCT2 = Matrix4.makeTranslation(4, 0, 0);
            node.SetTransform(trfCT2);
            node.RequestUpdate();
            var tp = TransformTool.TransformByMatrix(node.GetTopoShape(), node.GetTransform());

            //用移动后的toposhape做cut运算
            var cut = BooleanTool.Cut(tp, sphere);
            render.ShowShape(cut, ColorTable.BlanchedAlmond);

            //render.ShowShape(sphere, ColorTable.BrulyWood);      
            render.ShowSceneNode(node);
        }
    }
}
