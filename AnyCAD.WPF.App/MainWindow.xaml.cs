using AnyCAD.Foundation;
using AnyCAD.NX.Command;
using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace AnyCAD.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainViewModel(this.mRenderCtrl);
            this.DataContext = vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UICommandManager.Instance.Register(Assembly.GetExecutingAssembly());
        }

        private void Browser_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView treeView= (TreeView)sender;
            if(treeView == null)
                return;

            var node = treeView.SelectedItem as TreeViewItem;
            if (node == null)
                return;
            Demo.TestCase.CreateTest(node.Tag, this.mRenderCtrl);
        }

        private void RenderCtrl_ViewerReady()
        {
            var vm = this.DataContext as MainViewModel;
            if (vm == null)
                throw new Exception("DataContext is null!");

            vm.ViewReady();
        }


        private void VisitHelpWebsite(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "http://www.anycad.cn",
                UseShellExecute = true,
            });
        }

    }
}
