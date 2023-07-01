using AnyCAD.Foundation;
using Avalonia.Controls;

namespace AnyCAD.AvaloniaApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();

            this.mRenderView.SetAnimationCallback((ViewerListener.AnimationHandler)((float timer) =>
            {
                Demo.TestCase.RunAnimation(mRenderView, timer);
            }));

            this.mRenderView.SetSelectCallback((ViewerListener.AfterSelectHandler)((PickedResult result) =>
            {
                Demo.TestCase.SelectionChanged(mRenderView, result);
            }));
        }

        public void TreeView_SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            var treeView =  e.Source as TreeView;
            var node = treeView.SelectedItem as TreeViewItem;
            if (node == null)
                return;
            Demo.TestCase.CreateTest(node.Tag, mRenderView);
        }
    }
}