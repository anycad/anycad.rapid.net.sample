using AnyCAD.Foundation;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AnyCAD.Drawing
{

    internal class Shape
    {
        public string Type { get; set; } = string.Empty;
        public double[] Data { get; set; } = new double[4];
        public string Content { get; set; } = string.Empty;
        public void Show(IRenderView render, MaterialInstance material)
        {
            if(Type == "Polyline")
            {
                if(Data.Length == 4)
                {
                    var p1 = new GPnt2d(Data[0], Data[1]);
                    var p2 = new GPnt2d(Data[2], Data[3]);
                    if (p1.Distance(p2) < 0.01)
                        return;
                    var shape = Sketch2dBuilder.MakeLine(p1, p2);
                    var node = BrepSceneNode.Create(shape, material, material);
                    render.ShowSceneNode(node);
                }
                else if(Data.Length > 4)
                {
                    var pts = new GPntList();
                    for(int ii=0; ii<Data.Length/2; ++ii)
                    {
                        pts.Add(new GPnt(Data[ii*2], Data[ii*2+1], 0));
                    }
                    pts.Add(new GPnt(Data[0], Data[1], 0));
                    var shape = SketchBuilder.MakePolyline(pts);
                    var node = BrepSceneNode.Create(shape, material, material);
                    render.ShowSceneNode(node);
                }

            }
            else if(Type == "Text")
            {
                if(Content.Length > 0)
                {
                    var mesh = FontManager.Instance().CreateMesh(Content);
                    var node = PrimitiveSceneNode.Create(mesh, material);
                    var trf = Matrix4.makeTranslation((float)Data[0], (float)Data[1], 0);
                    float height = 0.002f * (float)Data[4];
                    float width = height / (float)Data[5];
                    var scale = Matrix4.makeScale(height, width, 1);
                    var rotate = Matrix4.makeRotation(Vector3.UNIT_X, new Vector3((float)Data[2], (float)Data[3], 0));
                    node.SetTransform(trf * rotate * scale);
                    render.ShowSceneNode(node);
                }
                
            }
            else if(Type == "Circle")
            {
                var pt = new GPnt2d(Data[0], Data[1]);
                var shape = Sketch2dBuilder.MakeCircle(new GCirc2d(new GAx2d(pt, new GDir2d(1, 0)), Data[2]));
                var node = BrepSceneNode.Create(shape, material, material);
                render.ShowSceneNode(node);
            }
            else if(Type == "CircArc")
            {
                var pt = new GPnt2d(Data[0], Data[1]);
                var radius = Data[2];
                var start = Data[3];
                var end = Data[4];
                var refVec = new GDir2d(Data[5], Data[6]);
                var shape = Sketch2dBuilder.MakeArc(new GCirc2d(new GAx2d(pt, refVec), radius), start, end);
                var node = BrepSceneNode.Create(shape, material, material);
                render.ShowSceneNode(node);
            }
        }
    }

    internal class Layer
    {
        public uint Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public float[] Color { get; set; } = new float[4];
    }

    internal class Entity
    {
        public uint Id { get; set; } = 0;
        public uint LayerId { get; set; } = 0;

        public string Type { get; set; } = string.Empty;

        public double[] Extends { get; set; } = new double[4];

        public float[] Color { get; set; } = new float[3];

        public List<Entity> Children { get; set; } = new();

        public List<Shape> Geometry { get; set; } = new();


        public Vector3 ToColor()
        {
            return new Vector3(Color[0], Color[1], Color[2]);
        }

        public void Show(IRenderView render, MaterialInstance material)
        {            
            foreach (var shape in Children)
            {
                shape.Show(render, material);
            }

            foreach (var shape in Geometry)
            {
                shape.Show(render, material);
            }
        }
    }


    internal class DrawingDb
    { 
        public List<Entity> Entity { get; set; } = new();
        public List<Layer> Layer { get; set; } = new();   

        static public DrawingDb? Load(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                var data = reader.ReadToEnd();
                return JsonSerializer.Deserialize<DrawingDb>(data);
            }
        }

        public void Show(IRenderView render)
        {
            var materials = new Dictionary<uint, MaterialInstance>();
            foreach(var layer in Layer)
            {
                var material = BasicMaterial.Create("cad");
                material.SetColor(new Vector3(layer.Color[0], layer.Color[1], layer.Color[2])); 

                materials.Add(layer.Id, material);
            }
            var defalutMaterial = BasicMaterial.Create("cad");
            defalutMaterial.SetColor(ColorTable.Purple);

            foreach (var shape in Entity)
            {
                materials.TryGetValue(shape.LayerId, out var material);
                if(material != null)
                    shape.Show(render, material);
                else
                    shape.Show(render, defalutMaterial);
            }
        }
    }
}
