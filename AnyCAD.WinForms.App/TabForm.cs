using AnyCAD.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyCAD.WinForms.App
{
    public partial class TabForm : Form
    {
        RenderControl mRenderView;
        public TabForm()
        {
            InitializeComponent();
            mRenderView = new RenderControl(this.panel1);
        }

        private void TabForm_Load(object sender, EventArgs e)
        {

        }
    }
}
