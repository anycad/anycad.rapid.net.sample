using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Sweep : TestCase
    {
        public override void Run(RenderControl render)
        {
            var sektch = SketchBuilder.MakeEllipse(GP.Origin(), 10, 5, GP.DX(), GP.DZ());

            var path = SketchBuilder.MakeArcOfCircle(GP.Origin(), new GPnt(50, 0, 50), new GPnt(20, 0, 40));

            var feature = FeatureTool.Sweep(sektch, path, EnumGeomFillTrihedron.ConstantNormal);
            render.ShowShape(feature, Vector3.Blue);
        }
    }
}
