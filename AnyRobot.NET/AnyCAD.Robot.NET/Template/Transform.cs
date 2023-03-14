
using AnyCAD.Foundation;

namespace AnyCAD.Robot
{
    public class Transform
    {
        public double[] Location { get; set; }
        public double Angle { get; set; }
        public double Scale { get; set; }

        public Transform()
        {
            Location = new double[3];
            Location[0] = Location[1] = Location[2] = 0;
            Angle = 0;
            Scale = 0;
        }

        public Matrix4 ToMatrix()
        {
            return Matrix4.makeTranslation((float)Location[0], (float)Location[1], (float)Location[2]);
        }
    }
}
