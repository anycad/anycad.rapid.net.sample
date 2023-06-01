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
        public void Show(IRenderView render)
        {
            if(Type == "Polyline")
            {
                var shape = Sketch2dBuilder.MakeLine(new GPnt2d(Data[0], Data[1]), new GPnt2d(Data[2], Data[3]));
                render.ShowShape(shape, ColorTable.Black);
            }
            else if(Type == "Text")
            {
                
            }
            else if(Type == "Circle")
            {
                var pt = new GPnt2d(Data[0], Data[1]);
                var shape = Sketch2dBuilder.MakeCircle(new GCirc2d(new GAx2d(pt, new GDir2d(1, 0)), Data[2]));
                render.ShowShape(shape, ColorTable.DarkRed);
            }
        }
    }

    internal class Entity
    {
        public uint Id { get; set; } = 0;
        public uint LayerId { get; set; } = 0;

        public string Type { get; set; } = string.Empty;

        public double[] Extends { get; set; } = new double[4];

        public List<Entity> Children { get; set; } = new();

        public List<Shape> Geometry { get; set; } = new();

        public void Show(IRenderView render)
        {
            foreach (var shape in Children)
            {
                shape.Show(render);
            }

            foreach (var shape in Geometry)
            {
                shape.Show(render);
            }
        }
    }


    internal class DrawingDb
    { 
        public List<Entity> Entity { get; set; } = new();
   
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
            foreach (var shape in Entity)
            {
                shape.Show(render);
            }
        }
    }
}
