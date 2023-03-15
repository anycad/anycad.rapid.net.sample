using AnyCAD.Foundation;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AnyCAD.AvaloniaApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();           
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