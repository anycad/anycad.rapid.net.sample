using AnyCAD.Forms;
using AnyCAD.Foundation;
using System.Windows.Forms;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_Shape : TestCase
    {
        public override void Run(RenderControl renderer)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "STEP (*.stp;*.step)|*.stp;*.step";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            var shape = StepIO.Open(dialog.FileName);
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
