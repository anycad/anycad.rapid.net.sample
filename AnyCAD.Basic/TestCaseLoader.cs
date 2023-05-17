using AnyCAD.Foundation;
using System.Collections.Generic;
using System.Reflection;

namespace AnyCAD.Demo
{
    public class TestCaseLoaderBase
    {
        public static void ForEachCase(AnyCAD.Demo.TestCase.TestCaseHandler handler)
        {
            TestCase.ForEachCase(handler, Assembly.GetExecutingAssembly());
        }


        static Dictionary<string, string> Names = new Dictionary<string, string>()
        {
            {"Graphics", "显示"},
            {"Geometry", "造型"},
            {"Analysis", "分析"},
            {"Interaction", "交互"},
            {"Simulation", "仿真"},
        };

        static public string GetUIName(string name)
        {
            if (Names.TryGetValue(name, out var val))
                return val;
            return name;
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
