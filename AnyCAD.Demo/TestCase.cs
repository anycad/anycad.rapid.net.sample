using AnyCAD.Forms;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AnyCAD.Demo
{
    abstract class TestCase
    {
        private static TestCase mCurrentDemo; 

        public static string GetResourcePath(string fileName)
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"\..\..\data\" + fileName;
        }

        public static void Register(TreeView tv)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(TestCase)))
                {
                    var node = tv.Nodes.Add(type.Name);
                    node.Tag = type;
                }

            }
        }

        public static void CreateTest(object tag, RenderControl render)
        {
            var type = tag as Type;
            mCurrentDemo = Activator.CreateInstance(type) as TestCase;
            render.ClearAll();
            mCurrentDemo.Run(render);
            render.ZoomAll();
        }
        public abstract void Run(RenderControl render);

        public virtual void Animation(RenderControl render, float time)
        {

        }

        public static void RunAnimation(RenderControl render, float timer)
        {
            if (mCurrentDemo != null)
                mCurrentDemo.Animation(render, timer);
        }
    }

    class _Welcom_AnyCAD : TestCase
    {
        public override void Run(RenderControl render)
        {
            
        }
    }

}
