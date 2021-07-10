using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;


namespace AnyCAD.Demo.Geometry
{
    class Geometry_Hole : TestCase
    {
        public override void Run(RenderControl render)
        {
            string fileName = GetResourcePath("models/hole.STEP");
            var shape = StepIO.Open(fileName);
            if (shape == null)
                return;

            var face = shape.FindChild(EnumTopoShapeType.Topo_FACE, 7);
            var wireExp = new WireExplor(face);
            var wires = wireExp.GetInnerWires();
            foreach(var wire in wires)
            {
                render.ShowShape(wire, ColorTable.Red);
            }
            render.ShowShape(shape,  ColorTable.LightYellow);
        }
    }
}
