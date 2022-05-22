using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Interaction_PickEdge : TestCase
    {
        public override void Run(RenderControl render)
        {
            string fileName = GetResourcePath("ST038.stp");
            var shape = StepIO.Open(fileName);
            if (shape == null)
                return;

            render.ShowShape(shape, ColorTable.Chocolate);
        }


        public override void OnSelectionChanged(RenderControl render, PickedResult result)
        {
            var item = result.GetItem();
            if (item.GetNode() == null)
                return;
            var ssn = BrepSceneNode.Cast(item.GetNode());
            if (ssn == null)
                return;

            if (item.GetShapeType() == EnumShapeFilter.Edge)
            {
                var edge = ssn.GetTopoShape().FindChild(EnumTopoShapeType.Topo_EDGE, (int)item.GetTopoShapeId());
                if (edge == null)
                    return;

                var curve = new ParametricCurve(); 
                if(curve.Initialize(edge))
                {
                  
                   var parameters =  curve.SplitByUniformAbscissa(20);
                    var node = new ParticleSceneNode((uint)parameters.Count+1, ColorTable.ForestGreen, 8);
                    for(uint ii=0; ii< parameters.Count; ++ii)
                    {                       
                         node.SetPosition(ii, Vector3.From(curve.Value(parameters[(int)ii])));
                    }
                    node.SetPosition((uint)parameters.Count, Vector3.From(curve.LastPoint()));
                    node.UpdateBoundingBox();
                    render.ShowSceneNode(node);

                    render.RequestDraw();
                }
            }
        }
    }
}
