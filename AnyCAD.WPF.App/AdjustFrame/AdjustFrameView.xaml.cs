using AnyCAD.NX.Command;
using AnyCAD.NX.Controls;
using AnyCAD.NX.View;
using AnyCAD.NX.ViewModel;

namespace AnyCAD.WPF.AdjustFrame
{
    /// <summary>
    /// AdjustLocationView.xaml 的交互逻辑
    /// </summary>
    public partial class AdjustFrameView : AuSubView
    {
        public AdjustFrameView()
        {
            InitializeComponent();
        }
    }


    class AdjustFrameCommand : TransientCommand
    {
        public AdjustFrameCommand()
        {

        }
        public override string Name { get => "My.AdjustFrame"; }

        public override string GetUiName()
        {
            return "姿态调整";
        }
        public override TransientViewModel CreateViewModel(object obj)
        {
            return new AdjustFrameViewModel();
        }

        public override void CreateView(ICommandView view, TransientViewModel vm)
        {
            view.AddView(new AdjustFrameView());
        }
    }
}
