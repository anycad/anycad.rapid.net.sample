using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AnyCAD.Demo
{
    public class TestCaseLoader
    {
        public static void Register(TreeView tv)
        {
            TestCase.Register(tv, Assembly.GetExecutingAssembly());
        }

        public static void ForEachCase(AnyCAD.Demo.TestCase.TestCaseHandler handler)
        {
            AnyCAD.Demo.TestCase.ForEachCase(handler, Assembly.GetExecutingAssembly());
        }
    }

    class Welcome_AnyCAD : TestCase
    {
        public override void Run(RenderControl render)
        {
           var geometry =  FontManager.Instance().CreateMesh("AnyCAD!");
            var node = new PrimitiveSceneNode(geometry,  null);
            render.ShowSceneNode(node);
        }
    }

}
