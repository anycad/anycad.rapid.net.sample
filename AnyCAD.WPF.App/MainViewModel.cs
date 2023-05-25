using AnyCAD.Foundation;
using AnyCAD.NX.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace AnyCAD.WPF
{
    internal partial class MainViewModel : RenderViewModel
    {
        [ObservableProperty]
        ObservableCollection<TreeViewItem> _BasicSamples;

        [ObservableProperty]
        ObservableCollection<TreeViewItem> _Advamples;

        public MainViewModel(IRenderView view):base(view)
        { 
            _BasicSamples = TestCaseLoader.LoadBasic();
            _Advamples = TestCaseLoader.LoadAdv();
        }
    }
}
