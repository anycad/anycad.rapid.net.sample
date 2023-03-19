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
    }
}
