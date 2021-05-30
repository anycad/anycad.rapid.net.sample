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
        TagNode2D mTag;

        MyTagControl mTagCtl;
        public NewForm3D()
        {
            InitializeComponent();

            mTagCtl = new MyTagControl();
            this.panel1.Controls.Add(mTagCtl);
           

            mRenderView = new RenderControl();
            this.panel1.Controls.Add(mRenderView);
            mRenderView.Dock = DockStyle.Fill;
            mRenderView.TabIndex = 1;
        }

        private void NewForm3D_Load(object sender, EventArgs e)
        {

            var box = GeometryBuilder.CreateBox(100, 200, 300);
            var node = new PrimitiveSceneNode(box);

            mRenderView.ShowSceneNode(node);


            mTag = TagNode2D.Create(null, new Vector3(200,300,400), Vector3.Zero);

            mRenderView.ShowSceneNode(mTag);

            mRenderView.SetStandardView(EnumStandardView.DefaultView);
            mRenderView.ZoomAll();

            mRenderView.SetAfterRenderingCallback(UpdateTagControls);
        }

        void UpdateTagControls()
        {
            var position = mTag.GetViewportPosition();


            mTagCtl.Location = new Point(Size.Width / 2 + (int)position.x - 20, Size.Height/2 - (int)position.y - 20);
        }
    }
}
