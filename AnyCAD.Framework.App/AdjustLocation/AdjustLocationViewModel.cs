using AnyCAD.Foundation;
using AnyCAD.NX.ViewModel;
using AnyCAD.SourceGenerator.Common;

namespace AnyCAD.WPF.AdjustLocation
{
    enum EnumAdjustLocationStep
    {
        None,
        SourceObject,
        SourcePoint,
        TargetPoint,
    }

    internal partial class AdjustLocationViewModel : TransientViewModel
    {
        public AdjustLocationViewModel()
        {
            
        }

        public override void Initialize()
        {
            SetCurrentStep(nameof(EnumAdjustLocationStep.SourceObject));

            ViewStateGuard.SetPickFilter(EnumShapeFilter.Object);
        }

        SceneNode? _SourceObject = null;

        [StepStateBinding]
        public EnumAdjustLocationStep _StepIndex = EnumAdjustLocationStep.None;

        GPnt _SourcePoint = new GPnt();
        [ExpandXYZ]
        public GPnt SourcePoint
        {
            get => _SourcePoint;
            set
            {
                _SourcePoint.SetXYZ(value.XYZ());
                OnPropertyChanged(nameof(SourcePoint));
                OnPropertyChanged(nameof(SourcePointX));
                OnPropertyChanged(nameof(SourcePointY));
                OnPropertyChanged(nameof(SourcePointZ));
            }
        }

        GPnt _TargetPoint = new GPnt();
        [ExpandXYZ]
        public GPnt TargetPoint
        {
            get => _TargetPoint;
            set
            {
                _TargetPoint.SetXYZ(value.XYZ());
                OnPropertyChanged(nameof(TargetPoint));
                OnPropertyChanged(nameof(TargetPointX));
                OnPropertyChanged(nameof(TargetPointY));
                OnPropertyChanged(nameof(TargetPointZ));

                UpdateObject();
            }
        }

        void UpdateObject()
        {
            if(_SourceObject == null)
            {
                return;
            }

            var vec = new GVec(_SourcePoint, _TargetPoint);
            var trf = Matrix4.makeTranslation(Vector3.From(vec));

            _SourceObject.SetTransform(trf);
            _SourceObject.RequestUpdate();

            ViewContext.RequestUpdate(EnumUpdateFlags.Scene);
        }

        public override void OnViewSelectionChanged(PickedResult pr, GPnt? pt)
        {
            switch(StepIndex)
            {
                case EnumAdjustLocationStep.SourceObject:
                    {
                        if (pr.IsEmpty())
                            return;

                        _SourceObject = pr.GetItem().GetNode();

                        StepIndex = EnumAdjustLocationStep.SourcePoint;
                        ViewStateGuard.SetPickFilter(EnumShapeFilter.VertexEdgeFace);
                    }
                    break;
                case EnumAdjustLocationStep.SourcePoint:
                    {
                        if (pt != null)
                        {
                            SourcePoint = pt;
                        }
                        else
                        {
                            if (pr.IsEmpty())
                                return;

                            SourcePoint = pr.GetItem().GetPosition().ToPnt();
                        }
                        SetStepState(nameof(SourcePoint), true);

                        TargetPoint = SourcePoint;
                        StepIndex = EnumAdjustLocationStep.TargetPoint;
                        ViewStateGuard.SetPickFilter(EnumShapeFilter.VertexEdgeFace);
                    }
                    break;
                case EnumAdjustLocationStep.TargetPoint:
                    {
                        if (pt != null)
                        {
                            TargetPoint = pt;
                        }
                        else
                        {
                            if (pr.IsEmpty())
                                return;

                            TargetPoint = pr.GetItem().GetPosition().ToPnt();
                        }
                    }
                    break;
            }

        }
    }
}
