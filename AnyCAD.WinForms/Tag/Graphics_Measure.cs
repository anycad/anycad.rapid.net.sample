using AnyCAD.Forms;
using AnyCAD.Foundation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyCAD.Demo.Graphics
{
    class MyEditorListner : EditorListener
    {
        public override void OnChanging(string name, ViewContext ctx, SceneNode pNode)
        {

        }
        public override void OnChanged(string name, ViewContext ctx, SceneNode pNode)
        {
            if (name == "MeasureTwoPoints")
            {
                var mdn = MeasureDistanceNode.Cast(pNode);
                if(mdn != null)
                {
                    var start = mdn.GetStart().GetPosition();
                    var end = mdn.GetEnd().GetPosition();
                    MessageBox.Show(String.Format("Measure Info: Distance: {6} \n{0},{1},{2} --> {3},{4},{5}", 
                        start.x, start.y, start.z, end.x, end.y, end.z, start.distanceTo(end)));
                }
               
            }
        }
    }

    class Graphics_Measure : Graphics_PointCloud
    {
        MyEditorListner mEditorListener;
        public override void Run(RenderControl render)
        {
            base.Run(render);

            if(mEditorListener == null)
            {
                mEditorListener = new MyEditorListner();
                EditorEvent.Instance().AddListener(mEditorListener);
            }

            render.ExecuteCommand("MeasureTwoPoints");
        }
    }
}
