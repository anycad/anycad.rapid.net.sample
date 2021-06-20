using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyCAD.Demo
{
    public partial class NewForm3D : Form
    {
        RenderControl mRenderView;

        public delegate void UpdateTagHandler();
        public event UpdateTagHandler UpdateTagEvent;

        public NewForm3D()
        {
            InitializeComponent();

            mRenderView = new RenderControl(this.panel1);
        }

        private void NewForm3D_Load(object sender, EventArgs e)
        {
            var box = GeometryBuilder.CreateBox(100, 200, 300);
            var material = MeshPhongMaterial.Create("simple");
            box.SetMaterial(material);
            material.SetColor(Vector3.Red);
            var node = new PrimitiveSceneNode(box);

            mRenderView.ShowSceneNode(node);

            mRenderView.SetStandardView(EnumStandardView.DefaultView);
            mRenderView.ZoomAll();

            // 转发给事件处理
            mRenderView.SetAfterRenderingCallback(()=>{
                if (UpdateTagEvent != null)
                    UpdateTagEvent();
            });

            // 创建两个自定义标注
            var mTagCtl = new MyTagControl(mRenderView, new Vector3(200, 300, 400), Vector3.Zero);
            UpdateTagEvent += mTagCtl.UpdateLayout;

            var mTagCtl2 = new MyTagControl(mRenderView, new Vector3(-100, -200, 100), Vector3.Zero);
            UpdateTagEvent += mTagCtl2.UpdateLayout;
        }
    }
}
