using AnyCAD.Demo;
using AnyCAD.Foundation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AnyCAD.AvaloniaApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var rootNodes = new ObservableCollection<TreeViewItem>();
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
                    rootNodes.Add(node);
                    node.IsExpanded = true;
                }

                var testNode = new TreeViewItem();
                testNode.Header = name;
                testNode.Tag = type;
                group.Add(testNode);
            });

            this.mTreeView.DataContext = rootNodes;            
        }

        public void OnOpen(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filters.Add(new FileDialogFilter() { Name = "Model Files", Extensions = { "step", "stp", "iges", "igs" } });

            var result = dlg.ShowAsync(this);
            if (result == null)
                return;

            string fileName = result.Result[0];

            var shape = ShapeIO.Open(fileName);
            if(shape != null)
            {
                var node = BrepSceneNode.Create(shape, null, null);
                var scene = mRenderView.ViewContext.GetScene();
                scene.AddNode(node);

                mRenderView.ViewContext.RequestUpdate(EnumUpdateFlags.Scene);
            }
        }
    }
}