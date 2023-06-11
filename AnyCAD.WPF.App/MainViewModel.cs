using AnyCAD.Drawing;
using AnyCAD.Foundation;
using AnyCAD.NX.Command;
using AnyCAD.NX.Controls;
using AnyCAD.NX.Settings;
using AnyCAD.NX.View;
using AnyCAD.NX.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace AnyCAD.WPF
{
    internal partial class MainViewModel : RenderViewModel
    {
        [ObservableProperty]
        ObservableCollection<TreeViewItem> _BasicSamples;

        [ObservableProperty]
        ObservableCollection<TreeViewItem> _AdvSamples;

        [ObservableProperty]
        string _MousePosition = string.Empty;

        [ObservableProperty]
        string _SelectionInfo = string.Empty;
        public MainViewModel(IRenderView view):base(view)
        { 
            _BasicSamples = TestCaseLoader.LoadBasic();
            _AdvSamples = TestCaseLoader.LoadAdv();
            PropertyChanged += ViewModel_PropertyChanged;            
        }

        public void ViewReady()
        {
            mRenderView.SetHilightingCallback((PickedResult pr) =>
            {
                var pt = pr.GetItem().GetPosition();
                MousePosition = $"{pt.x} {pt.y} {pt.z}";
                return true;
            });

            mRenderView.SetAnimationCallback((ViewerListener.AnimationHandler)((float timer) =>
            {
                Demo.TestCase.RunAnimation(mRenderView, timer);
            }));

            DoInitialize();
        }

        protected override void OnViewSelectionChanged(PickedResult result)
        {
            Demo.TestCase.SelectionChanged(mRenderView, result);
            UpdateSelectionInfo(result);
        }

        protected override ICommandView OnCreateCommandView(CommandContext ctx, string title)
        {
            return new AuCommandView(ctx, title);
        }

        public void UpdateSelectionInfo(PickedResult pr)
        {
            var ss = mRenderView.SelectionManager.GetSelection();
            var count = ss.GetCount();
            var msg = $"Count: {count}";
            if(count == 1)
            {
                for(var itr = ss.CreateIterator();itr.More(); itr.Next())
                {
                    var item = itr.Current();
                    msg += $"\nNodeId: {item.GetNodeId()}";
                    msg += $"\nUserId: {item.GetUserId()}";
                    msg += $"\nShapeId: {item.GetShapeIndex()}";
                    msg += $"\nPrimitiveId: {item.GetPrimitiveIndex()}";
                    msg += $"\nType: {item.GetShapeType().ToString()}";
                    msg += $"\nTopoShapeId: {item.GetTopoShapeId().ToString()}";
                    var pt = item.GetPosition();
                    msg += $"\nPosition: {pt.x} {pt.y} {pt.z}";

                    var shapeNode = BrepSceneNode.Cast(item.GetNode());
                    if(shapeNode != null)
                    {
                        var trf = shapeNode.GetWorldTransform().ToTrsf();
                        trf.Invert();
                        var pointOnShape = pt.ToPnt().Transformed(trf);
                        switch (item.GetShapeType())
                        {
                            case EnumShapeFilter.Edge:
                                {
                                   var edge = shapeNode.GetTopoShape().FindChild(EnumTopoShapeType.Topo_EDGE, (int)item.GetTopoShapeId());
                                    if(edge != null)
                                    {
                                        var pc = new ParametricCurve(edge);
                                        msg += $"\nEdge: {pc.GetCurveType().ToString()}";

                                        var epc = new ExtremaPointCurve();
                                        if(epc.Initialize(edge, pointOnShape) && epc.GetPointCount() > 0)
                                        {
                                            var param = epc.GetParameter(0);
                                            msg += $"\nU: {param}";
                                        }
                                    }
                                }
                                break;
                            case EnumShapeFilter.Face:
                                {
                                    var face = shapeNode.GetTopoShape().FindChild(EnumTopoShapeType.Topo_FACE, (int)item.GetTopoShapeId());
                                    if(face != null)
                                    {
                                        var ps = new ParametricSurface(face);
                                        msg += $"\nFace: {ps.GetSurfaceType().ToString()}";

                                        var uv = ps.ComputeClosestPoint(pointOnShape, GP.Resolution(), GP.Resolution());
                                        msg += $"\nUV: {uv.x} {uv.y}";
                                    }
                                }
                                break;
                            default:
                                {
                                    msg += $"\n^_^";
                                }
                                break;
                        }
                        
                    }
                }                
            }
            
            SelectionInfo= msg ;
        }

        [RelayCommand]
        void OnViewSetting()
        {
            ViewSettings settings = new();
            settings.Initialize(RenderView);

            //settings.Save("d:/ViewSetting.json");

            ViewSettingsView settingView = new(settings, RenderView);
            settingView.ShowDialog();
        }

        [RelayCommand]
        void OnNewScene()
        {
            RenderView.ClearAll();
        }

        [RelayCommand]
        void OnOpenModel()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".stp",
                Filter = "Models (*.igs;*.iges;*.stp;*.step;*.brep;*.stl)|*.igs;*.iges;*.stp;*.step;*.brep;*.stl"
            };
            if (dlg.ShowDialog() != true)
                return;

            SceneNode? node = null;
            ProgressView pv = new ProgressView(() =>
            {
                var shape = ShapeIO.Open(dlg.FileName);
                if (shape == null)
                    return;
                node = BrepSceneNode.Create(shape, null, null, 0, false);
            });
            pv.ShowDialog();

            if (node == null)
                return;

            mRenderView.ShowSceneNode(node);
            mRenderView.ZoomAll();
        }

        [RelayCommand]
        void OnOpenMesh()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".obj",
                Filter = SceneIO.FormatFilters()
            };
            if (dlg.ShowDialog() != true)
                return;

            SceneNode? node = null;
            ProgressView pv = new ProgressView(() =>
            {
                node = SceneIO.Load(dlg.FileName);
            });
            pv.ShowDialog();

            if (node == null)
                return;

            mRenderView.ShowSceneNode(node);
            mRenderView.ZoomAll();
        }

        [RelayCommand]
        void OnOpenDrawing()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".json",
                Filter = "Drawing (*.json)|*.json"
            };
            if (dlg.ShowDialog() != true)
                return;

            var drawing = DrawingDb.Load(dlg.FileName);
            if(drawing != null)
            {
                drawing.Show(RenderView);
            }
        }


        ConfirmInputView? _ConfirmInputView = null;
        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.ConfirmInputVisible))
            {
                if (ConfirmInputVisible)
                {
                    if (_ConfirmInputView != null)
                    {
                        _ConfirmInputView.Close();
                    }
                    _ConfirmInputView = new ConfirmInputView(this);
                    _ConfirmInputView.Show();
                }
                else
                {
                    if (_ConfirmInputView != null)
                    {
                        _ConfirmInputView.Close();
                        _ConfirmInputView = null;
                    }

                }
            }
        }
    }
}
