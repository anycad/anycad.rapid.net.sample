using AnyCAD.Foundation;
using System.Reflection;

namespace AnyCAD.Demo
{
    public class TestCaseLoaderAdv
    {
        public static void ForEachCase(AnyCAD.Demo.TestCase.TestCaseHandler handler)
        {
            ModelingEngine.EnableSnapShape();
            TestCase.ForEachCase(handler, Assembly.GetExecutingAssembly());
        }
    }

}
