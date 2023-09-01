using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class DrawLineEditor : Editor
    {
        
        public DrawLineEditor()
        {

        }

        BasicMaterial mLineMaterial;
        BrepSceneNode mNode = null;
        public override EnumEditorCode Start(ViewContext ctx)
        {
            mLineMaterial = BasicMaterial.Create("temp.line");
            mLineMaterial.SetColor(ColorTable.Green);
         
            var shape = SketchBuilder.MakeLine(new GPnt(), new GPnt(1, 1, 1));
            mNode = BrepSceneNode.Create(shape, null, mLineMaterial);

            mNode.SetVisible(false);

            ctx.GetScene().AddNode(mNode);

            return base.Start(ctx);
        }

        void Update(GPnt start, GPnt end)
        {
            if (start.Distance(end) < 0.0001)
                return;

            var shape = SketchBuilder.MakeLine(start, end);
            var bs = new BufferShape(shape, null, mLineMaterial, 0);
            bs.Build();

            mNode.SetShape(bs);
            mNode.SetVisible(true);
            mNode.RequestUpdate();
        }

        public override void Finish(ViewContext ctx)
        {
            ctx.GetScene().RemoveNode(mNode.GetUuid());
            ctx.RequestUpdate(EnumUpdateFlags.Scene);
            base.Finish(ctx);
        }

        Vector3 mStartPt = null;
        public override EnumEditorCode OnMouseDown(ViewContext ctx, InputEvent evt)
        {            
            if(mStartPt == null)
            {
                var pick = ctx.SnapPoint(evt.GetX(), evt.GetY());
                mStartPt = pick.GetPosition();
            }
            else
            {
                // 创建好了
                mNode.SetEdgeMaterial(null);


                //重新创建个临时对象
                mNode = BrepSceneNode.Create(mNode.GetTopoShape(), null, mLineMaterial);
                mNode.SetVisible(false);
                ctx.GetScene().AddNode(mNode);
                ctx.RequestUpdate(EnumUpdateFlags.Scene);


                mStartPt = null;
            }
            return base.OnMouseDown(ctx, evt);
        }

        public override EnumEditorCode OnMouseMove(ViewContext ctx, InputEvent evt)
        {
            if (mStartPt != null)
            {
                var pick = ctx.SnapPoint(evt.GetX(), evt.GetY());
                var pt = pick.GetPosition();
                Update(mStartPt.ToPnt(), pt.ToPnt());
                ctx.RequestUpdate(EnumUpdateFlags.Scene);
            }
            return base.OnMouseMove(ctx, evt);
        }

        public override EnumEditorCode OnMouseUp(ViewContext ctx, InputEvent evt)
        {
            return base.OnMouseUp(ctx, evt);
        }
    }

    class Interaction_DrawLine : TestCase
    {
        public override void Run(IRenderView render)
        {
            var mEditor = new DrawLineEditor();
            render.SetEditor(mEditor);
        }

        public override void Exit(IRenderView render)
        {
            render.Viewer.ClearEditor();
        }
    }
}
