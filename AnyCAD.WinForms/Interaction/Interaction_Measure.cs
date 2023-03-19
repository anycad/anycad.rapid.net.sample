using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Graphics
{
    class MyEditorListner : EditorListener
    {
        public override void OnChanging(string name, ViewContext ctx, SceneNode pNode, ObjectIdSet ids)
        {

        }
        public override void OnChanged(string name, ViewContext ctx, SceneNode pNode, ObjectIdSet ids)
        {
            if (name == "MeasureTwoPoints")
            {
                var mdn = MeasureDistanceNode.Cast(pNode);
                if(mdn != null)
                {
                    var start = mdn.GetStart().GetPosition();
                    var end = mdn.GetEnd().GetPosition();
                    DialogUtil.ShowMessageBox("Info", String.Format("Measure Info: Distance: {6} \n{0},{1},{2} --> {3},{4},{5}", 
                        start.x, start.y, start.z, end.x, end.y, end.z, start.distanceTo(end)));
                }
               
            }
        }
    }

    class Interaction_Measure : Graphics_PointCloud
    {
        MyEditorListner mEditorListener;
        public override void Run(IRenderView render)
        {
            base.Run(render);

            if(mEditorListener == null)
            {
                mEditorListener = new MyEditorListner();
                EditorEvent.Instance().AddListener(mEditorListener);
            }

            AnyCAD.Foundation.Application.Instance().ExecuteCommand("MeasureTwoPoints");
        }
    }
}
