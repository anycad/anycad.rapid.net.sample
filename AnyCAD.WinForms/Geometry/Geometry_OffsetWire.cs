using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_OffsetWire : TestCase
    {
        public override void Run(RenderControl render)
        {
            var sketch = SketchBuilder.MakeRectangle(GP.XOY(), 100, 50, 5, false);
            render.ShowShape(sketch, Vector3.Blue);

            var types = new EnumGeomJoinType[]{ EnumGeomJoinType.Arc, EnumGeomJoinType.Intersection, EnumGeomJoinType.Tangent };

            for (int ii=0; ii<3; ++ii)
            {
                var offset = FeatureTool.OffsetWire(sketch, 2*ii + 2, 0, types[ii], false);
                render.ShowShape(offset, Vector3.Green);
            }

        }
    }
}
