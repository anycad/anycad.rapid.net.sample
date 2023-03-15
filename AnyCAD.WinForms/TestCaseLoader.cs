using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace AnyCAD.Demo
{
    public class TestCaseLoader
    {
        public static void Register(TreeView tv)
        {
            Dictionary<String, TreeNode> dictNodes = new Dictionary<string, TreeNode>();
            TestCase.ForEachCase((Type type, string name, string groupName) =>
            {
                TreeNode groupNode = null;
                if (!dictNodes.TryGetValue(groupName, out groupNode))
                {
                    groupNode = tv.Nodes.Add(groupName);
                    dictNodes[groupName] = groupNode;
                }

                var node = groupNode.Nodes.Add(name);
                node.Tag = type;
            }, Assembly.GetExecutingAssembly());
            tv.ExpandAll();
        }

        public static void Register(TreeView tv, Assembly assembly)
        {
            Dictionary<String, TreeNode> dictNodes = new Dictionary<string, TreeNode>();
            TestCase.ForEachCase((Type type, string name, string groupName) =>
            {
                TreeNode groupNode = null;
                if (!dictNodes.TryGetValue(groupName, out groupNode))
                {
                    groupNode = tv.Nodes.Add(groupName);
                    dictNodes[groupName] = groupNode;
                }

                var node = groupNode.Nodes.Add(name);
                node.Tag = type;
            }, assembly);
            tv.ExpandAll();
        }

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
