using AnyCAD.Demo;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.AvaloniaApp
{
    internal class MainViewModel : ObservableObject
    {

        public MainViewModel() 
        {
            FillTestCase();
        }  

        public ObservableCollection<TreeViewItem> Items { get; set; } = new ObservableCollection<TreeViewItem>();
        void FillTestCase()
        {

            Dictionary<string, ObservableCollection<TreeViewItem>> groupDict = new Dictionary<string, ObservableCollection<TreeViewItem>>();
            TestCaseLoaderBase.ForEachCase((Type type, string name, string groupName) =>
            {
                ObservableCollection<TreeViewItem> group = null;
                if (!groupDict.TryGetValue(groupName, out group))
                {
                    group = new ObservableCollection<TreeViewItem>();
                    groupDict[groupName] = group;

                    var node = new TreeViewItem();
                    node.Items = group;
                    node.Header = groupName;
                    node.IsExpanded = true;

                    Items.Add(node);
                }

                var testNode = new TreeViewItem();
                testNode.Header = name;
                testNode.Tag = type;
                group.Add(testNode);
            });
        }
    }
}
