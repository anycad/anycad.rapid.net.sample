using AnyCAD.Exchange;
using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Windows.Forms;

namespace AnyCAD.Demo
{
    public partial class MainForm : Form
    {
        RenderControl mRenderView;
        public MainForm()
        {
            InitializeComponent();

            mRenderView = new RenderControl();
            this.splitContainer1.Panel2.Controls.Add(mRenderView);
            mRenderView.Dock = DockStyle.Fill;
            mRenderView.TabIndex = 1;

            TestCase.Register(this.treeView1);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mRenderView.SetSelectCallback((PickedItem item) =>
            {
                if (item.IsNull())
                {
                    this.toolStripStatusLabel1.Text = "www.anycad.cn";
                }
                else
                {
                    this.toolStripStatusLabel1.Text = String.Format("<{0}>: {1}", item.GetNodeId(), item.GetPosition().ToString());
                }
            });

            mRenderView.SetAnimationCallback((float timer)=>{
                TestCase.RunAnimation(mRenderView, timer);
            });
        }
        private void sTEPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "STEP (*.stp;*.step)|*.stp;*.step|IGES (*.igs;*.iges)|*.igs;*.iges";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            StepReader doc = new StepReader();
            doc.Open(dialog.FileName, (XdeNode xn, TopoShape shape, Vector3 color) =>
            {
                mRenderView.ShowShape(shape, color);
            });

            mRenderView.ZoomAll();
        }

        private void iGESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "IGES (*.igs;*.iges)|*.igs;*.iges|STEP (*.stp;*.step)|*.stp;*.step";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            StepReader doc = new StepReader();
            doc.Open(dialog.FileName, (XdeNode xn, TopoShape shape, Vector3 color) =>
            {
                mRenderView.ShowShape(shape, color);
            });

            mRenderView.ZoomAll();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.ClearAll();
        }

        private void captureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Bitmap (*.bmp)|*.bmp";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            mRenderView.CaptureScreenShot(dialog.FileName);
        }

        private void stepToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "STEP (*.stp;*.step)|*.stp;*.step";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var shape = StepIO.Open(dialog.FileName);
            if (shape == null)
                return;

            mRenderView.ShowShape(shape, new Vector3(0.8f));

            mRenderView.ZoomAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = this.treeView1.SelectedNode;
            if (node == null)
                return;
            TestCase.CreateTest(node.Tag, mRenderView);
        }

        private void zoomAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.ZoomAll();
        }

        private void projectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SwitchProjectionType();
        }
    }
}
