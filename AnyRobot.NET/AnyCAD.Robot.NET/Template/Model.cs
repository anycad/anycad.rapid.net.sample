using AnyCAD.Foundation;

namespace AnyCAD.Robot.Template
{
    internal class Color
    {
        public int R { get; set; } = 128;
        public int G { get; set; } = 128;
        public int B { get; set; } = 128;

        public Vector3 To()
        {
            return new Vector3(R/255.0f, G / 255.0f, B / 255.0f);
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", R, G, B);
        }
    }

    internal class Model
    {
        public string File { get; set; } = string.Empty;
        public Color Color { get; set; } = new Color();
    }
}
