using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Interaction_PickEdge : TestCase
    {
        uint _PickFilter = 0;
        public override void Run(IRenderView render)
        {
            string fileName = GetResourcePath("ST038.stp");
            var shape = StepIO.Open(fileName);
            if (shape == null)
                return;

            var material = MeshPhongMaterial.Create("mesh.transparent");
            material.SetColor(ColorTable.BlanchedAlmond);
            material.SetTransparent(true);
            material.SetOpacity(0.5f);

            var node = BrepSceneNode.Create(shape, material, null);
            render.ShowSceneNode(node);

            _PickFilter = render.ViewContext.GetPickFilter();
            render.ViewContext.SetPickFilter((uint)EnumShapeFilter.Edge);
        }


        public override void OnSelectionChanged(IRenderView render, PickedResult result)
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
                    var node = new ParticleSceneNode((uint)parameters.Count+1, ColorTable.ForestGreen, 10);
                    
                    for (uint ii=0; ii< parameters.Count; ++ii)
                    {                       
                         node.SetPosition(ii, Vector3.From(curve.Value(parameters[(int)ii])));
                    }
                    node.SetPosition((uint)parameters.Count, Vector3.From(curve.LastPoint()));
                    node.UpdateBoundingBox();
                    render.ShowSceneNode(node);

                    render.RequestDraw();
                }
            }

            render.SelectionManager.Clear(false);
            render.RequestDraw(EnumUpdateFlags.Selection);
        }

        public override void Exit(IRenderView render)
        {
            render.ViewContext.SetPickFilter(_PickFilter);
            base.Exit(render);
        }
    }
}
