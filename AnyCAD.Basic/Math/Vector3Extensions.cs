
namespace AnyCAD.Foundation
{
    public static class Vector3Extensions
    {
        public static double X(this Vector3 v) => v.x;
        public static void X(this Vector3 v, double value) => v.x = (float)value;
    }
}
