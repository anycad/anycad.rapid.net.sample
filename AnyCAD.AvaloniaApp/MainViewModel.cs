using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AnyCAD.AvaloniaApp
{
    internal class MainViewModel : ObservableObject
    {
        public ObservableCollection<TreeViewItem> BasicItems { get; set; }
        public ObservableCollection<TreeViewItem> AdvItems { get; set; }
        public MainViewModel() 
        {
            BasicItems = TestCaseLoader.LoadBasic();
            AdvItems = TestCaseLoader.LoadAdv();
        }  
    }
}
