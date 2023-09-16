using AnyCAD.Foundation;
using System.Xml.Linq;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_SweepGear : TestCase
    {

        void Run1(IRenderView render)
        {
            var face = ShapeIO.Open(GetResourcePath("sweep/profile.brep"));
            var profile = face.GetChildren(EnumTopoShapeType.Topo_WIRE)[0];

            var trf = new GTrsf();
            trf.SetScale(GP.Origin(), 0.8);
            trf.SetTranslationPart(new GVec(0, 0, -6));
            var quat = new GQuaternion(new GVec(0, 0, 1), System.Math.PI / 12);
            trf.SetRotationPart(quat);
            var top = TransformTool.Transform(profile, trf);

 
            var section = new TopoShapeList();
            section.Add(profile);
            section.Add(top);
            var solid = FeatureTool.Loft(section, true, false);
            render.ShowShape(solid, ColorTable.BrulyWood);
        }

        void Run2(IRenderView render)
        {
            var profile = ShapeIO.Open(GetResourcePath("sweep/profile2.brep"));
            var path = ShapeIO.Open(GetResourcePath("sweep/path2.brep"));

            render.ShowShape(profile, ColorTable.Purple);
            render.ShowShape(path, ColorTable.Red);

            var solid = FeatureTool.SweepByFrenet(profile, path, EnumSweepTransitionMode.RoundCorner, true);
            render.ShowShape(solid, ColorTable.Gold);
        }

        void Run3(IRenderView render)
        {
            var path = ShapeIO.Open(GetResourcePath("sweep/path2.brep"));
            render.ShowShape(path, ColorTable.Green);

            TopoShape wire = ShapeIO.Open(GetResourcePath("sweep/AxialPro.brep"));
            ParametricCurve pc = new ParametricCurve(wire);

            GPnt p0 = pc.GetStartPoint();
            GPnt p1 = p0.Translated(new GVec(0, 10, 0));
            GPnt p3 = pc.GetEndPoint();
            GPnt p2 = p3.Translated(new GVec(0, 10, 0));

            TopoShape line0 = SketchBuilder.MakeLine(p1, p0);
            TopoShape line1 = SketchBuilder.MakeLine(p2, p1);
            TopoShape line2 = SketchBuilder.MakeLine(p3, p2);

            TopoShapeList listlines = new TopoShapeList();
            listlines.Add(wire);
            listlines.Add(line0);
            listlines.Add(line1);
            listlines.Add(line2);
            var FaceTeeth = SketchBuilder.MakeWire(listlines);

            render.ShowShape(FaceTeeth, ColorTable.Red);

            var TeethSolid = FeatureTool.SweepByFrenet(FaceTeeth, path, EnumSweepTransitionMode.RightCorner, true);

            render.ShowShape(TeethSolid, ColorTable.Gold);
        }
        public override void Run(IRenderView render)
        {
            Run3(render);
        }
    }
}
