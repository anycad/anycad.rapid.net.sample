﻿using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_CurveOnSurface : TestCase
    {
        public override void Run(RenderControl render)
        {
            var paramCurve = Sketch2dBuilder.MakeLine(new GPnt2d(0, 0), new GPnt2d(Math.PI*20, 20));

            // curve one cone
            var cone = ShapeBuilder.MakeCone(GP.XOY(), 1, 10, 20, 0);
            var coneFace = cone.FindChild(EnumTopoShapeType.Topo_FACE, 0);
            var curveOnConeSurface = SketchBuilder.MakeCurveOnSurface(paramCurve, coneFace);
            render.ShowShape(curveOnConeSurface, Vector3.Blue);

            // curve one cylinder
            var cylinder = ShapeBuilder.MakeCylinder(GP.XOY(), 2, 20, 0);
            var cylinderFace = cylinder.FindChild(EnumTopoShapeType.Topo_FACE, 0);

            var curveOnCylinderSurface = SketchBuilder.MakeCurveOnSurface(paramCurve, cylinderFace);
            render.ShowShape(curveOnCylinderSurface, Vector3.Green);
        }
    }
}