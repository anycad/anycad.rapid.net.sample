using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AnyCAD.Demo
{
    public abstract class TestCase
    {
        private static TestCase mCurrentDemo; 
        public int State { get; set; }

        public static string GetResourcePath(string fileName)
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"\..\..\data\" + fileName;
        }

        public delegate void TestCaseHandler(Type type, string name, string groupName);
        public static void ForEachCase(TestCaseHandler handler)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(TestCase)))
                {
                    var items = type.Name.Split('_');
                    handler(type, items[1], items[0]);
                }

            }
        }
        public static void Register(TreeView tv)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            Dictionary<String, TreeNode> dictNodes = new Dictionary<string, TreeNode>();
            ForEachCase((Type type, string name, string groupName) =>
            {
                TreeNode groupNode = null;
                if (!dictNodes.TryGetValue(groupName, out groupNode))
                {
                    groupNode = tv.Nodes.Add(groupName);
                    dictNodes[groupName] = groupNode;
                }

                var node = groupNode.Nodes.Add(name);
                node.Tag = type;
            });
            tv.ExpandAll();
        }

        public static void CreateTest(object tag, RenderControl render)
        {
            if (tag == null)
                return; 
            var type = tag as Type;
            mCurrentDemo = Activator.CreateInstance(type) as TestCase;
            render.ClearAll();
            mCurrentDemo.State = 0;
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

        public static void IncreaseCounter(RenderControl render, int key)
        {
            if (mCurrentDemo != null)
                mCurrentDemo.State += 1 ;
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
