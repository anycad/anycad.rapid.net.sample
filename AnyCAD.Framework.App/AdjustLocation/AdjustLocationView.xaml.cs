using AnyCAD.NX.Command;
using AnyCAD.NX.Controls;
using AnyCAD.NX.View;
using AnyCAD.NX.ViewModel;

namespace AnyCAD.WPF.AdjustLocation
{
    /// <summary>
    /// AdjustLocationView.xaml 的交互逻辑
    /// </summary>
    public partial class AdjustLocationView : AuSubView
    {
        public AdjustLocationView()
        {
            InitializeComponent();
        }
    }


    class AdjustLocationCommand : TransientCommand
    {
        public AdjustLocationCommand()
        {

        }
        public override string Name { get => "My.AdjustLocation"; }

        public override string GetUiName()
        {
            return "位置调整";
        }
        public override TransientViewModel CreateViewModel(object obj)
        {
            return new AdjustLocationViewModel();
        }

        public override void CreateView(ICommandView view, TransientViewModel vm)
        {
            view.AddView(new AdjustLocationView());
        }
    }
}
