using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Interaction_PickFace : TestCase
    {
        ArrowWidget mArrow;

        public override void Run(RenderControl render)
        {
            var arrowMaterial = MeshPhongMaterial.Create("arrow");
            arrowMaterial.SetColor(ColorTable.Red);
            mArrow = ArrowWidget.Create(2, 10, arrowMaterial);
            mArrow.SetPickable(false);
            render.ShowSceneNode(mArrow);


            var shape = ShapeBuilder.MakeBox(GP.XOY(), 10, 20, 30);
            var mat = MeshStandardMaterial.Create("ss");
            mat.SetColor(Vector3.LightGray);

            var node = BrepSceneNode.Create(shape, mat, null);
            render.ShowSceneNode(node);
        }


        public override void OnSelectionChanged(RenderControl render, PickedResult result)
        {
            var item = result.GetItem();
            if (item.GetNode() == null)
                return;
            var ssn = BrepSceneNode.Cast(item.GetNode());
            if (ssn == null)
                return;

            if (item.GetShapeType() == EnumShapeFilter.Face)
            {
                var face = ssn.GetShape().GetShape().FindChild(EnumTopoShapeType.Topo_FACE, (int)item.GetShapeIndex());
                if (face != null)
                {
                    var surface = new ParametricSurface(face);
                    var pt = item.GetPoint().GetPosition();
                    var param = surface.ComputeClosestPoint(pt.ToPnt(), GP.Resolution(), GP.Resolution());

                    var values = surface.D1(param.X(), param.Y());
                    var postion = Vector3.From(values.GetPoint());
                    var vecs = values.GetVectors();

                    var dir = Vector3.From(vecs[1].Crossed(vecs[0]));
                    dir.normalize();
                    mArrow.SetLocation(postion, dir);
                    mArrow.RequstUpdate();
                    mArrow.Update();

                    render.GetContext().GetSelection().Clear();

                    render.RequestDraw(EnumUpdateFlags.Scene);
                }

            }
        }
    }
}
