using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    /// <summary>
    /// 使用鼠标在曲面上绘制曲线
    /// </summary>
    class DrawCurveOnSurfaceEditor : Editor
    {        
        public DrawCurveOnSurfaceEditor()
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

            var tmpCtx = ctx.GetTempContext();
            tmpCtx.Start();
            tmpCtx.SetPickFilter(EnumShapeFilter.Face);
            tmpCtx.AddNode(mNode);

            return base.Start(ctx);
        }

        /// <summary>
        /// 更新曲线
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="endPt"></param>
        void Update(ViewContext ctx, GPnt2d endPt)
        {
            if (_Points.Count == 0 || _Surface == null)
                return;

            if (_Points[_Points.Count-1].Distance(endPt) < 0.0001)
                return;

            TopoShape shape;
            if(_Points.Count == 1)
            {
                shape = Sketch2dBuilder.MakeLine(_Points[0], endPt);
            }
            else
            {
                GPnt2dList pts = new GPnt2dList();
                foreach(var pt in _Points)
                {
                    pts.Add(pt);
                }
                pts.Add(endPt);

                shape = Sketch2dBuilder.MakeBSpline(pts, false, GP.Resolution());
            }

            if (shape == null)
                return;

            var curve = SketchBuilder.MakeCurveOnSurface(shape, _Surface);
            if(curve == null)
                return;

            var bs = new BufferShape(curve, null, mLineMaterial, 0);
            bs.Build();

            mNode.SetShape(bs);
            mNode.SetVisible(true);
            mNode.RequestUpdate();

            ctx.RequestUpdate(EnumUpdateFlags.Dynamic);
        }

        public override void Finish(ViewContext ctx)
        {
            var tmpCtx = ctx.GetTempContext();
            tmpCtx.Clear();
            base.Finish(ctx);
        }

        GPnt2dList _Points = new GPnt2dList();
        TopoShape _Surface;

        /// <summary>
        /// 获取鼠标位置下的点
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="evt"></param>
        /// <returns></returns>
        GPnt2d GetUV(ViewContext ctx, InputEvent evt)
        {
            var engine = SnapShapeEngine.Get(ctx);
            var pick = engine.PickFace(ctx, evt.GetX(), evt.GetY());
            if (!pick.IsNull())
            {
                ctx.ClearSelection();
                var face = engine.ParseSubShape(ctx, pick);
                if (face != null)
                {
                    var ps = new ParametricSurface();
                    if (ps.Initialize(face))
                    {
                        var uv = ps.ComputeClosestPoint(pick.GetPosition().ToPnt(), GP.Resolution(), GP.Resolution());
                        if(_Surface == null)
                            _Surface = face;
                        return new GPnt2d(uv);
                    }
                }
            }
            return null;
        }

        public override EnumEditorCode OnMouseMove(ViewContext ctx, InputEvent evt)
        {
            if(evt.GetButtons() == EnumMouseButton.Zero)
            {
                var pt = GetUV(ctx, evt);
                if (pt != null)
                {
                    Update(ctx, pt);
                    return EnumEditorCode.Processed;
                }
            }

            return base.OnMouseMove(ctx, evt);
        }

        public override EnumEditorCode OnMouseUp(ViewContext ctx, InputEvent evt)
        {
            if (evt.GetButtons() == EnumMouseButton.Left)
            {
                var pt = GetUV(ctx, evt);
                if (pt != null)
                {
                    _Points.Add(pt);
                    return EnumEditorCode.Processed;
                }
            }

            return base.OnMouseUp(ctx, evt);
        }

        public override EnumEditorCode OnKeyUp(ViewContext ctx, InputEvent evt)
        {
            if((EnumKeyCode)evt.GetKey() == EnumKeyCode.KEY_SPACE)
            {
                _Points.Clear();
                _Surface = null; 
            }
            return base.OnKeyUp(ctx, evt);
        }
    }

    class Interaction_CurveOnSurface : TestCase
    {
        public override void Run(IRenderView render)
        {
            // 启用Shape捕捉功能。在程序启动的时候设置一次即可。
            ModelingEngine.EnableSnapShape();

            // 设置自定义的编辑器
            var mEditor = new DrawCurveOnSurfaceEditor();
            render.SetEditor(mEditor);

            // 创建个球，用来在上面绘制曲线
            var sphere = ShapeBuilder.MakeSphere(new GPnt(), 10);
            render.ShowShape(sphere, ColorTable.LightPink);
        }

        public override void Exit(IRenderView render)
        {
            render.Viewer.ClearEditor();
        }
    }
}
