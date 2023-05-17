using AnyCAD.Demo;
using Avalonia.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace AnyCAD.AvaloniaApp
{
    internal class TestCaseLoader
    {
        ObservableCollection<TreeViewItem> rootNodes = new ();
        Dictionary<string, ObservableCollection<TreeViewItem>> groupDict = new ();

        void OnLoaded(Type type, string name, string groupName)
        {
            ObservableCollection<TreeViewItem> group = null;
            if (!groupDict.TryGetValue(groupName, out group))
            {
                group = new ObservableCollection<TreeViewItem>();
                groupDict[groupName] = group;

                var node = new TreeViewItem();
                node.Items = group;
                node.Header = TestCaseLoaderBase.GetUIName(groupName);
                node.IsExpanded = true;

                rootNodes.Add(node);
            }

            var testNode = new TreeViewItem();
            testNode.Header = name;
            testNode.Tag = type;
            group.Add(testNode);
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
