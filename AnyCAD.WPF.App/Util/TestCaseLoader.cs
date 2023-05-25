using AnyCAD.Demo;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System;

namespace AnyCAD.WPF
{
    internal class TestCaseLoader
    {
        ObservableCollection<TreeViewItem> rootNodes = new ObservableCollection<TreeViewItem>();
        Dictionary<string, int> groupDict = new Dictionary<string, int>();

        void OnLoaded(Type type, string name, string groupName)
        {
            int groupId = 0;
            if (!groupDict.TryGetValue(groupName, out groupId))
            {
                groupId = rootNodes.Count;
                groupDict[groupName] = groupId;
                var node = new TreeViewItem();
                node.Header = TestCaseLoaderBase.GetUIName(groupName);
                rootNodes.Add(node);
                node.IsExpanded = true;
            }

            var testNode = new TreeViewItem();
            testNode.Header = name;
            testNode.Tag = type;
            rootNodes[groupId].Items.Add(testNode);
        }
        static public ObservableCollection<TreeViewItem> LoadBasic()
        {
            var loader = new TestCaseLoader();
            TestCaseLoaderBase.ForEachCase(loader.OnLoaded);
            return loader.rootNodes;
        }
        static public ObservableCollection<TreeViewItem> LoadAdv()
        {
            var loader = new TestCaseLoader();
            TestCaseLoaderAdv.ForEachCase(loader.OnLoaded);
            return loader.rootNodes;
        }
    }
}
