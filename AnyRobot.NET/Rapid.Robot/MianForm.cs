using AnyCAD.Forms;
using AnyCAD.Foundation;
using AnyCAD.Robot;
using Rapid.Robot;
using System;
using System.Windows.Forms;

namespace RapidRobot
{
    public partial class MainForm : Form
    {
        MachineFactory mMachineFactory = new ();
        RenderControl mRenderView;
        AnyCAD.Robot.RobotControler mRobotControler;
        public MainForm()
        {
            InitializeComponent();
            mRenderView = new RenderControl(this.panel1);

            this.newToolStripMenuItem.Click += NewProject;
            this.openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            this.indicatorToolStripMenuItem.Click += IndicatorToolStripMenuItem_Click;
            this.saveTemplateToolStripMenuItem.Click += SaveTemplateToolStripMenuItem_Click;
            this.showLinktoolStripMenuItem1.Click += ShowLinktoolStripMenuItem1_Click;
        }

        private void SaveTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = MachineFactory.Filter;
            if (save.ShowDialog() != DialogResult.OK)
                return;
            mMachineFactory.SaveSample(save.FileName);
        }

        private void NewProject(object sender, EventArgs e)
        {
            mRenderView.ClearAll();
        }

        private void OnUpdateModel()
        {
            mRenderView.RequestDraw(EnumUpdateFlags.Scene);
        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = MachineFactory.Filter;
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            this.progressBar1.Visible = true;
           var name = mMachineFactory.Load(dlg.FileName, (int progress)=>
           {
              this.progressBar1.Value = progress;
           });
           this.progressBar1.Visible = false;

            if(name.Length == 0)
            {
                MessageBox.Show("打开配置文件失败");
                return;
            }

            var robot = mMachineFactory.CreateInstance(name);
            mRenderView.ShowSceneNode(robot);


            mRobotControler = new AnyCAD.Robot.RobotControler(robot);
            mRenderView.ShowSceneNode(mRobotControler.Trajectory);

            mRenderView.Viewer.SetStandardView(EnumStandardView.DefaultView, false);
            mRenderView.ZoomAll();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mRenderView.GetCamera().SetProjectionType(EnumProjectionType.Orthographic);
            mRenderView.RequestDraw(EnumUpdateFlags.Camera);

            mRenderView.ShowCoordinateGrid(true);
        }

        RobotIndicatorForm mIndicatorUI;
        private void IndicatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mRobotControler == null)
                return;

            mIndicatorUI = new RobotIndicatorForm(mRobotControler);
            mIndicatorUI.Owner = this;
            mIndicatorUI.OnUpdate = OnUpdateModel;
            mIndicatorUI.Show();
        }

        bool bShowLink = true;
        private void ShowLinktoolStripMenuItem1_Click (object sender, EventArgs e)
        {
            bShowLink = !bShowLink;
            mRobotControler.Robot.ShowLinks(bShowLink);
            mRenderView.RequestDraw();
        }


        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var arm = mRobotControler.Robot.GetArm(0);
            var target = arm.GetJoint(arm.GetJointCount() - 1).GetAxisNode();
            var editor = new RobotTargetEditor(target, arm);

            mRenderView.SetEditor(editor);
        }

        private void byNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.ViewContext.SetPickFilter((uint)(EnumShapeFilter.RootNode | EnumShapeFilter.VertexEdgeFace));
        }

        private void bySubNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.ViewContext.SetPickFilter((uint)(EnumShapeFilter.LeafNode | EnumShapeFilter.VertexEdgeFace));
        }
    }
}
