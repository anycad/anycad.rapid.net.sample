using AnyCAD.Foundation;
using System.Reflection;

namespace AnyCAD.Demo
{
    public class TestCaseLoaderBase
    {
        public static void ForEachCase(AnyCAD.Demo.TestCase.TestCaseHandler handler)
        {
            TestCase.ForEachCase(handler, Assembly.GetExecutingAssembly());
        }
    }

    class Welcome_AnyCAD : TestCase
    {
        public override void Run(IRenderView render)
        {
           var geometry =  FontManager.Instance().CreateMesh("AnyCAD!");
            var node = new PrimitiveSceneNode(geometry,  null);
            render.ShowSceneNode(node);
        }
    }

}
