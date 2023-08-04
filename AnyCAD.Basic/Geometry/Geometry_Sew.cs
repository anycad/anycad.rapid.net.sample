using AnyCAD.Foundation;
using System;
using System.IO;
using System.Text;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Sew : TestCase
    {
        public override void Run(IRenderView render)
        {
            var face1 = ShapeIO.Open(GetResourcePath("boolean/face1.igs"));
            var face2 = ShapeIO.Open(GetResourcePath("boolean/face2.igs"));

            var face = BooleanTool.Fuse(face1, face2);

            render.ShowShape(face, ColorTable.LightBlue);
        }
    }
}
