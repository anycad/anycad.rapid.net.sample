using AnyCAD.Foundation;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_Shape : TestCase
    {
        public override void Run(IRenderView renderer)
        {
            var fullPath = DialogUtil.OpenFileDialog("选择模型文件", new StringList(new string[] { "CAD Files (.igs .iges .stp .step .brep)", "*.igs *.iges *.stp *.step *.brep" }));
            if (fullPath.IsEmpty())
                return;

            var shape = ShapeIO.Open(fullPath);
            if (shape == null)
                return;

            var shapeExplor = new ShapeExplor();
            shapeExplor.AddShape(shape);
            shapeExplor.Build();

            var edgeCount = shapeExplor.GetEdgeCount();
            for(uint ii=0; ii<edgeCount; ++ii)
            {
                var edge = shapeExplor.GetEdge(ii);
                if(edge.GetCurveType() == EnumCurveType.CurveType_Line)
                    renderer.ShowShape(edge.GetShape(), ColorTable.Blue);
                else
                    renderer.ShowShape(edge.GetShape(), ColorTable.Red);
            }
        }
    }
}
