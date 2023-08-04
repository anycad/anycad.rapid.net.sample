using AnyCAD.Foundation;


namespace AnyCAD.Demo.Geometry
{
    class Geometry_PosAxis : TestCase
    {
        public override void Run(IRenderView render)
        {
            string fileName = GetResourcePath("feature/底封板.stp");
            var shape = StepIO.Open(fileName);
            if (shape == null)
                return;

            var fT = shape.GetShapeType();
            GlobalInstance.Log(fT.ToString());
            var faces = shape.GetChildren(EnumTopoShapeType.Topo_FACE);
            foreach( var face in faces )
            {
                var newShape = FixShapeTool.RemoveInternalWires(face, 100000);
                render.ShowShape(newShape, ColorTable.LightBlue);
            }

        }
    }
}
