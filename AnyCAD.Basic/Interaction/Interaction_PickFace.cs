using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Interaction_PickFace : TestCase
    {
        ArrowWidget mArrow;
        uint _PickFilter = 0;
        public override void Run(IRenderView render)
        {
            var arrowMaterial = MeshPhongMaterial.Create("arrow");
            arrowMaterial.SetColor(ColorTable.Red);
            mArrow = ArrowWidget.Create(1, 10, arrowMaterial);
            mArrow.SetPickable(false);
            render.ShowSceneNode(mArrow);


            var shape = ShapeBuilder.MakeBox(GP.XOY(), 10, 20, 30);
            var mat = MeshStandardMaterial.Create("ss");
            mat.SetColor(ColorTable.LightGrey);

            var node = BrepSceneNode.Create(shape, mat, null);
            render.ShowSceneNode(node);

            _PickFilter = render.ViewContext.GetPickFilter();
            render.ViewContext.SetPickFilter((uint)EnumShapeFilter.Face);
        }


        public override void OnSelectionChanged(IRenderView render, PickedResult result)
        {
            var item = result.GetItem();
            if (item.GetNode() == null)
                return;
            var ssn = BrepSceneNode.Cast(item.GetNode());
            if (ssn == null)
                return;

            if (item.GetShapeType() == EnumShapeFilter.Face)
            {
                var face = ssn.GetTopoShape().FindChild(EnumTopoShapeType.Topo_FACE, (int)item.GetTopoShapeId());
                if (face != null)
                {
                    var pt = item.GetPoint().GetPosition();

                    var trf = ssn.GetWorldTransform().ToTrsf();
                    trf.Invert();
                    var pointOnShape = pt.ToPnt().Transformed(trf);

                    var surface = new ParametricSurface(face);
        
                    var param = surface.ComputeClosestPoint(pointOnShape, GP.Resolution(), GP.Resolution());

                    var values = surface.D1(param.X(), param.Y());
                    var postion = Vector3.From(values.GetPoint());
                    var vecs = values.GetVectors();

                    var dir = Vector3.From(vecs[0].Crossed(vecs[1]));
                    dir.normalize();

                    if(face.GetOrientation() == EnumTopoOrientation.REVERSED)
                    {
                        dir = dir * -1;
                    }

                    mArrow.SetLocation(postion, dir);
                    mArrow.RequestUpdate();
                    mArrow.Update();

                    render.RequestDraw(EnumUpdateFlags.Scene);
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
