using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Sketch : TestCase
    {
        public override void Run(RenderControl render)
        {
            var line = SketchBuilder.MakeLine(new GPnt(10, 10, 10), new GPnt(-10, -10, -10));
            render.ShowShape(line, new Vector3(1, 1, 0));
            
            var ellips = SketchBuilder.MakeEllipse(GP.Origin(), 10, 5, GP.DX(), GP.DZ());
            render.ShowShape(ellips, ColorTable.Blue);

            var circle = SketchBuilder.MakeCircle(GP.Origin(), 4, new GDir(0, 1, 0));
            render.ShowShape(circle, ColorTable.Green);

            var arc = SketchBuilder.MakeArcOfCircle(GP.Origin(), new GPnt(50, 0, 50), new GPnt(20, 0, 40));
            render.ShowShape(arc, ColorTable.Red);

            var rect = SketchBuilder.MakeRectangle(GP.XOY(), 30, 40, 5, false);
            render.ShowShape(rect, ColorTable.Green);
        }
        public override void OnSelectionChanged(RenderControl render, PickedResult result)
        {
            var node = BrepSceneNode.Cast(result.GetItem().GetNode());
            if(node == null)
                return;

            var tool = SketchBuilder.MakeVertex(result.GetItem().GetPosition().ToPnt());
            var compound = BooleanTool.Split(node.GetTopoShape(), tool);
            if (compound == null)
                return;

            var edges = compound.GetChildren(EnumTopoShapeType.Topo_EDGE);
            foreach (var edge in edges)
            {
                render.ShowShape(edge, ColorTable.Red);
            }

            render.RemoveSceneNode(node.GetUuid());

            render.RequestDraw(EnumUpdateFlags.Scene);
        }
    }
}
