using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Transform : TestCase
    {
        void CustumTransform(RenderControl render)
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
            render.ShowShape(View, Vector3.Red);
            GAx3 gAx31 = new GAx3(new GAx2(new GPnt(0, 0, 0), new GDir(0, 0, 1), new GDir(1, 0, 0)));
            GAx3 gAx32 = new GAx3(new GAx2(new GPnt(0, 0, 0), new GDir(0, 0, 1), new GDir(0, 1, 0)));
            GTrsf gTrsf = new GTrsf();
            gTrsf.SetTransformation(gAx31, gAx32);
            View = TransformTool.Transform(View, gTrsf);

            render.ShowShape(View, Vector3.Blue);
        }
        public override void Run(RenderControl render)
        {
            //var box = ShapeBuilder.MakeBox(GP.XOY(), 10, 20, 30);
            //render.ShowShape(box, ColorTable.Green);

            //var trans = TransformTool.Translate(box, new GVec(-20, 0, 0));
            //render.ShowShape(trans, new Vector3(105.0f / 256.0f));

            //var scale = TransformTool.Scale(box, GP.Origin(), 0.5);
            //render.ShowShape(scale, ColorTable.Blue);

            //var rotate = TransformTool.Rotation(box, GP.OX(), 45);
            //render.ShowShape(rotate, ColorTable.Red);


            CustumTransform(render);
        }
    }
}
