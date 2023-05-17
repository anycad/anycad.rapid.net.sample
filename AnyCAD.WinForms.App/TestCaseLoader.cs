using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace AnyCAD.Demo
{
    public class TestCaseLoader : TestCaseLoaderBase
    {

        public static void Register(TreeView tv)
        {
            var node = tv.Nodes.Add("窗体示例");
            TestCaseLoader.Register(node, Assembly.GetExecutingAssembly());

            node = tv.Nodes.Add("基础功能");
            Register(node);

            node = tv.Nodes.Add("高级功能");
            RegisterAdv(node);
        }


        static void Register(TreeNode tv)
        {
            Dictionary<String, TreeNode> dictNodes = new Dictionary<string, TreeNode>();
            ForEachCase((Type type, string name, string groupName) =>
            {
                TreeNode groupNode = null;
                if (!dictNodes.TryGetValue(groupName, out groupNode))
                {
                    groupNode = tv.Nodes.Add(GetUIName(groupName));
                    dictNodes[groupName] = groupNode;
                }

                var node = groupNode.Nodes.Add(name);
                node.Tag = type;
            });

            tv.ExpandAll();
        }

        static void RegisterAdv(TreeNode tv)
        {
            Dictionary<String, TreeNode> dictNodes = new Dictionary<string, TreeNode>();
            TestCaseLoaderAdv.ForEachCase((Type type, string name, string groupName) =>
            {
                TreeNode groupNode = null;
                if (!dictNodes.TryGetValue(groupName, out groupNode))
                {
                    groupNode = tv.Nodes.Add(GetUIName(groupName));
                    dictNodes[groupName] = groupNode;
                }

                var node = groupNode.Nodes.Add(name);
                node.Tag = type;
            });

            tv.ExpandAll();
        }

        static void Register(TreeNode tv, Assembly assembly)
        {
            Dictionary<String, TreeNode> dictNodes = new Dictionary<string, TreeNode>();
            TestCase.ForEachCase((Type type, string name, string groupName) =>
            {
                TreeNode groupNode = null;
                if (!dictNodes.TryGetValue(groupName, out groupNode))
                {
                    groupNode = tv.Nodes.Add(GetUIName(groupName));
                    dictNodes[groupName] = groupNode;
                }

                var node = groupNode.Nodes.Add(name);
                node.Tag = type;
            }, assembly);
            tv.ExpandAll();
        }
    }
}
