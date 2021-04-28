using AnyCAD.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnyCAD.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
  
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // List all test cases
            var rootNodes = new ObservableCollection<TreeViewItem>();
            Dictionary<string, int> groupDict = new Dictionary<string, int>();
            AnyCAD.Demo.TestCase.ForEachCase((Type type, string name, string groupName) =>
            {
                int groupId = 0;
                if(!groupDict.TryGetValue(groupName, out groupId))
                {
                    groupId = rootNodes.Count;
                    groupDict[groupName] = groupId;
                    var node = new TreeViewItem();
                    node.Header = groupName;
                    rootNodes.Add(node);
                    node.IsExpanded = true;
                }

                var testNode = new TreeViewItem();
                testNode.Header = name;
                testNode.Tag = type;
                rootNodes[groupId].Items.Add(testNode);
            });

            projectBrowser.ItemsSource = rootNodes;


            // Enable animation.
            this.mRenderCtrl.ViewerReady += () =>
            {
                this.mRenderCtrl.View3D.SetAnimationCallback((float timer) =>
                {
                    Demo.TestCase.RunAnimation(this.mRenderCtrl.View3D, timer);
                });
            };
        }

        private void projectBrowser_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = projectBrowser.SelectedItem as TreeViewItem;
            if (node == null)
                return;
            Demo.TestCase.CreateTest(node.Tag, this.mRenderCtrl.View3D);
        }

    }
}
