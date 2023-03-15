using AnyCAD.Foundation;
using System;
using System.IO;

namespace AnyCAD.Demo.Geometry
{
    class Analysis_Curvature : TestCase
    {
        public override void Run(IRenderView render)
        {
            //1. Create Shape
            TopoShapeList tg = new TopoShapeList();
            GPntList points = new GPntList();

            using (var sr = new StreamReader(GetResourcePath("data/Stage4_Rotor4_Profile.curve")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("# Profile"))
                    {
                        if(points.Count > 0)
                        {
                            var temp2 = SketchBuilder.MakeBSpline(points);
                            if (temp2 != null)
                                tg.Add(temp2);
                        }

                        points = new GPntList();
                    }
                    else
                    {
                        var temp = line.Split('\t');
                        points.Add(new GPnt(double.Parse(temp[0]), double.Parse(temp[1]), double.Parse(temp[2])));
                    }
                }
            }


            var shape = FeatureTool.Loft(tg, true, true);

            //2. Compute Curvature
            var material = BasicMaterial.Create("vertex-color");
            material.SetVertexColors(true);
            material.SetFaceSide(EnumFaceSide.DoubleSide);

            var bs = new BufferShape(shape, material, null, 0.01f);
            bs.Build();

            ColorLookupTable clt = new ColorLookupTable();
            clt.SetColorMap(ColorMapKeyword.Create(EnumSystemColorMap.Rainbow));

            float scale = 100;
            clt.SetMinValue(-0.2f * scale);
            clt.SetMaxValue(scale);

            for (uint ii = 0; ii < bs.GetFaceCount(); ++ii)
            {
                var sc = new SurfaceCurvature(bs);
                if (sc.Compute(ii, EnumCurvatureType.MeanCurvature))
                {
                    Console.WriteLine("{0}, {1}", sc.GetMinValue(), sc.GetMaxValue());                   
                    var colorBuffer = clt.ComputeColors(sc.GetValues(), scale);
                    bs.SetVertexColors(ii, colorBuffer);
                }

            }

            // 3. Show it!
            var node = new BrepSceneNode(bs);

            render.ShowSceneNode(node);
        }
    }
}
