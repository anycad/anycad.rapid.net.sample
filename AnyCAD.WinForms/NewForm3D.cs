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
        public NewForm3D()
        {
            InitializeComponent();

            mRenderView = new RenderControl();
            this.panel1.Controls.Add(mRenderView);
            mRenderView.Dock = DockStyle.Fill;
            mRenderView.TabIndex = 1;
        }

        private void NewForm3D_Load(object sender, EventArgs e)
        {
            var axis = AxisWidget.Create(5, 20);

            mRenderView.ShowSceneNode(axis);

            mRenderView.ZoomAll();
        }
    }
}
