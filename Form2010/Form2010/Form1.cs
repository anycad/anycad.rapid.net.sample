using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnyCAD.Foundation;
using AnyCAD.Forms;

namespace Form2010
{
    public partial class Form1 : Form
    {
        RenderControl mRenderer;
        public Form1()
        {
            InitializeComponent();

            mRenderer = new RenderControl(this.panel1);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "STEP (*.stp;*.step)|*.stp;*.step|IGES (*.igs;*.IGES)|*.igs;*.IGES||";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var shape = ShapeIO.Open(dialog.FileName);
            mRenderer.ShowShape(shape, Vector3.Blue);
        }
    }
}
