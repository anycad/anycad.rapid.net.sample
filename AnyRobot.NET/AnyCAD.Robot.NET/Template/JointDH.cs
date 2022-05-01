
namespace AnyCAD.Robot
{
    public class JointDH
    {
        public double Alpha { get; set; }
        public double A { get; set; }
        public double D { get; set; }
        public double Theta { get; set; }

        public JointDH()
        {
            Alpha = A = D = Theta = 0;
        }
    }
}
