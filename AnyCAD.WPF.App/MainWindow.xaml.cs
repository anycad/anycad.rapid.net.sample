using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using AnyCAD.Demo;
using MahApps.Metro.Controls;

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
            this.DataContext = new MainViewModel(this.mRenderCtrl);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Enable animation.
            this.mRenderCtrl.ViewerReady += () =>
            {
                this.mRenderCtrl.View3D.SetAnimationCallback((ViewerListener.AnimationHandler)((float timer) =>
                {
                    Demo.TestCase.RunAnimation(this.mRenderCtrl.View3D, timer);
                }));

                this.mRenderCtrl.View3D.SetSelectCallback((ViewerListener.AfterSelectHandler)((PickedResult result) =>
                {
                    Demo.TestCase.SelectionChanged(mRenderCtrl.View3D, result);
                }));
            };
        }

        private void Browser_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView treeView= (TreeView)sender;
            if(treeView == null)
                return;

            var node = treeView.SelectedItem as TreeViewItem;
            if (node == null)
                return;
            Demo.TestCase.CreateTest(node.Tag, this.mRenderCtrl.View3D);
        }

    }
}
