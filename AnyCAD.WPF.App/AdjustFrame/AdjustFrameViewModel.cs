using AnyCAD.Foundation;
using AnyCAD.NX.ViewModel;
using AnyCAD.SourceGenerator.Common;

namespace AnyCAD.WPF.AdjustFrame
{
    enum EnumAdjustFrameStep
    {
        None,
        SourceObject,
        
        SourceFrameOrigin,
        SourceFrameXdir,
        SourceFrameZdir,

        TargetFrameOrigin,
        TargetFrameXdir,
        TargetFrameZdir,
    }

    internal partial class AdjustFrameViewModel : TransientViewModel
    {
        public AdjustFrameViewModel()
        {
            
        }

        public override void Initialize()
        {
            SetCurrentStep(nameof(EnumAdjustFrameStep.SourceObject));

            
        }

        new bool IsApplyVisible => true;

        SceneNode? _SourceObject = null;

        [StepStateBinding]
        public EnumAdjustFrameStep _StepIndex = EnumAdjustFrameStep.None;

        partial void OnStepIndexChanged(EnumAdjustFrameStep value)
        {
            switch(value)
            {
                case EnumAdjustFrameStep.SourceObject:
                    ViewStateGuard.SetPickFilter(EnumShapeFilter.Object);
                    break;
                default:
                    ViewStateGuard.SetPickFilter(EnumShapeFilter.VertexEdgeFace);
                    break;
            }
        }


        GPnt _SourceFrameOrigin = new GPnt();
        [ExpandXYZ]
        public GPnt SourceFrameOrigin
        {
            get => _SourceFrameOrigin;
            set
            {
                _SourceFrameOrigin.SetXYZ(value.XYZ());
                OnPropertyChanged(nameof(SourceFrameOrigin), 
                    nameof(SourceFrameOriginX), nameof(SourceFrameOriginY), nameof(SourceFrameOriginZ));
            }
        }

        GPnt _SourceFrameXdir = new GPnt();
        [ExpandXYZ]
        public GPnt SourceFrameXdir
        {
            get => _SourceFrameXdir;
            set
            {
                _SourceFrameXdir.SetXYZ(value.XYZ());
                OnPropertyChanged(nameof(SourceFrameXdir),
                    nameof(SourceFrameXdirX), nameof(SourceFrameXdirY), nameof(SourceFrameXdirZ));
            }
        }

        GPnt _SourceFrameZdir = new GPnt();
        [ExpandXYZ]
        public GPnt SourceFrameZdir
        {
            get => _SourceFrameZdir;
            set
            {
                _SourceFrameZdir.SetXYZ(value.XYZ());
                OnPropertyChanged(nameof(SourceFrameZdir),
                    nameof(SourceFrameZdirX), nameof(SourceFrameZdirY), nameof(SourceFrameZdirZ));
            }
        }

        GPnt _TargetFrameOrigin = new GPnt();
        [ExpandXYZ]
        public GPnt TargetFrameOrigin
        {
            get => _TargetFrameOrigin;
            set
            {
                _TargetFrameOrigin.SetXYZ(value.XYZ());
                OnPropertyChanged(nameof(TargetFrameOrigin), nameof(TargetFrameOriginX), nameof(TargetFrameOriginY), nameof(TargetFrameOriginZ));
            }
        }

        GPnt _TargetFrameXdir = new GPnt();
        [ExpandXYZ]
        public GPnt TargetFrameXdir
        {
            get => _TargetFrameXdir;
            set
            {
                _TargetFrameXdir.SetXYZ(value.XYZ());
                OnPropertyChanged(nameof(TargetFrameXdir),
                    nameof(TargetFrameXdirX), nameof(TargetFrameXdirY), nameof(TargetFrameXdirZ));
            }
        }

        GPnt _TargetFrameZdir = new GPnt();
        [ExpandXYZ]
        public GPnt TargetFrameZdir
        {
            get => _TargetFrameZdir;
            set
            {
                _TargetFrameZdir.SetXYZ(value.XYZ());
                OnPropertyChanged(nameof(TargetFrameZdir),
                    nameof(TargetFrameZdirX), nameof(TargetFrameZdirY), nameof(TargetFrameZdirZ));
            }
        }

        void UpdateObject()
        {
            if (_SourceObject == null)
                return;

            var xdir1 = new GVec(SourceFrameOrigin, SourceFrameXdir);
            var ydir1 = new GVec(SourceFrameOrigin, SourceFrameZdir);
            var zdir1 = xdir1.Crossed(ydir1);
            var ax1 = new GAx3(SourceFrameOrigin, new GDir(zdir1), new GDir(xdir1));

            var xdir2 = new GVec(TargetFrameOrigin, TargetFrameXdir);
            var ydir2 = new GVec(TargetFrameOrigin, TargetFrameZdir);
            var zdir2 = xdir2.Crossed(ydir2);
            var ax2 = new GAx3(TargetFrameOrigin, new GDir(zdir2), new GDir(xdir2));

            GTrsf trf = new GTrsf();
            trf.SetDisplacement(ax1, ax2);

            _SourceObject.AddTransform(Matrix4.makeTransform(trf));
            _SourceObject.RequestUpdate();

            ViewContext.RequestUpdate(EnumUpdateFlags.Scene);
        }

        GPnt? GetPickedPoint(PickedResult pr, GPnt? pt)
        {
            if (pt != null)
                return pt;

            if(pr.IsEmpty())
                return null;

            return pr.GetItem().GetPosition().ToPnt();
        }

        public override void OnViewSelectionChanged(PickedResult pr, GPnt? pt)
        {            
            switch(StepIndex)
            {
                case EnumAdjustFrameStep.SourceObject:
                    {
                        if (pr.IsEmpty())
                            return;
                        SetStepState(nameof(EnumAdjustFrameStep.SourceObject), true);
                        _SourceObject = pr.GetItem().GetNode(); 
                        StepIndex = EnumAdjustFrameStep.SourceFrameOrigin;
                    }
                    break;
                case EnumAdjustFrameStep.SourceFrameOrigin:
                    {
                        var p = GetPickedPoint(pr, pt);
                        if (p == null)
                            return;
                        SourceFrameOrigin = p;
                        SetStepState(nameof(SourceFrameOrigin), true);
                        StepIndex = EnumAdjustFrameStep.SourceFrameXdir;
                    }
                    break;
                case EnumAdjustFrameStep.SourceFrameXdir:
                    {
                        var p = GetPickedPoint(pr, pt);
                        if(p == null) 
                            return;

                        if (p.IsEqual(SourceFrameOrigin, GP.Resolution()))
                            return;

                        SourceFrameXdir = p;
                        SetStepState(nameof(SourceFrameXdir), true);
                        StepIndex = EnumAdjustFrameStep.SourceFrameZdir;
                    }
                    break;
                case EnumAdjustFrameStep.SourceFrameZdir:
                    {
                        var p = GetPickedPoint(pr, pt);
                        if (p == null)
                            return;
                        if (p.IsEqual(SourceFrameOrigin, GP.Resolution())
                            || p.IsEqual(SourceFrameXdir, GP.Resolution()))
                            return;

                        SourceFrameZdir = p;
                        SetStepState(nameof(SourceFrameZdir), true);
                        StepIndex = EnumAdjustFrameStep.TargetFrameOrigin;
                    }
                    break;
                case EnumAdjustFrameStep.TargetFrameOrigin:
                    {
                        var p = GetPickedPoint(pr, pt);
                        if(p == null) 
                            return;
                        TargetFrameOrigin = p;
                        SetStepState(nameof(TargetFrameOrigin), true);
                        StepIndex = EnumAdjustFrameStep.TargetFrameXdir;
                    }
                    break;
                case EnumAdjustFrameStep.TargetFrameXdir:
                    {
                        var p = GetPickedPoint(pr, pt);
                        if (p == null)
                            return;

                        if (p.IsEqual(TargetFrameOrigin, GP.Resolution()))
                            return;

                        TargetFrameXdir = p;
                        SetStepState(nameof(TargetFrameXdir), true);
                        StepIndex = EnumAdjustFrameStep.TargetFrameZdir;
                    }
                    break;
                case EnumAdjustFrameStep.TargetFrameZdir:
                    {
                        var p = GetPickedPoint(pr, pt);
                        if (p == null)
                            return;
                        if (p.IsEqual(TargetFrameOrigin, GP.Resolution())
                                || p.IsEqual(TargetFrameXdir, GP.Resolution()))
                            return;

                        TargetFrameZdir = p;
                        SetStepState(nameof(TargetFrameZdir), true);
                        StepIndex = EnumAdjustFrameStep.None;
                        UpdateObject();
                    }
                    break;
            }

        }
    }
}
