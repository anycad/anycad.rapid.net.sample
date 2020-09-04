using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;


namespace AnyCAD.Demo.Graphics
{
    class Graphics_Matlab : TestCase
    {
        public override void Run(RenderControl render)
        {
            var matplot = Matplot.Create("MyMatlab 2020");

            var xRange = new PlotRange(0, 3.14f * 2, 0.1f);
            var yRange = new PlotRange(0, 3.14f * 2, 0.1f);
            matplot.AddSurface(xRange, yRange, (idxU, idxV, u, v) =>
            {
                double x = u;
                double y = v;
                double z = Math.Sin(u) + Math.Cos(v);

                return new GPnt(x, y, z);
            });

            var node = matplot.Build(ColorMapKeyword.Create(EnumSystemColorMap.Rainbow));
            node.SetPickable(false);
            render.ShowSceneNode(node);
        }
    }
}
