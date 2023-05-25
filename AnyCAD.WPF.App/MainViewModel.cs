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
        ObservableCollection<TreeViewItem> _AdvSamples;

        [ObservableProperty]
        string _MousePosition = string.Empty;

        [ObservableProperty]
        string _SelectionInfo = string.Empty;
        public MainViewModel(IRenderView view):base(view)
        { 
            _BasicSamples = TestCaseLoader.LoadBasic();
            _AdvSamples = TestCaseLoader.LoadAdv();
        }

        public void ViewReady()
        {
            mRenderView.SetHilightingCallback((PickedResult pr) =>
            {
                var pt = pr.GetItem().GetPosition();
                MousePosition = $"{pt.x} {pt.y} {pt.z}";
                return true;
            });
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
    }
}
