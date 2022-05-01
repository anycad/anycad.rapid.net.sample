using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyCAD.Forms;
using AnyCAD.Foundation;
namespace RapidRobot
{
    public partial class MainForm : Form
    {

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
            save.Filter = AnyCAD.Robot.MachineTemplate.Filter;
            if (save.ShowDialog() != DialogResult.OK)
                return;
            AnyCAD.Robot.MachineTemplate.SaveSample(save.FileName);
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
            dlg.Filter = AnyCAD.Robot.MachineTemplate.Filter;
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            this.progressBar1.Visible = true;
           var template = AnyCAD.Robot.MachineTemplate.Load(dlg.FileName, (int progress)=>
           {
               this.progressBar1.Value = progress;
           });
            this.progressBar1.Visible = false;

            if(template == null)
            {
                MessageBox.Show("打开配置文件失败");
                return;
            }

            var robot = template.CreateInstance();
            mRenderView.ShowSceneNode(robot);


            mRobotControler = new AnyCAD.Robot.RobotControler(robot);
            mRenderView.ShowSceneNode(mRobotControler.TrackingPath);

            mRenderView.SetStandardView(EnumStandardView.DefaultView);
            mRenderView.ZoomAll();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mRenderView.GetCamera().SetProjectionType(EnumProjectionType.Orthographic);
            mRenderView.RequestDraw(EnumUpdateFlags.Camera);
        }

        RobotIndicatorForm mIndicatorUI;
        private void IndicatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mIndicatorUI = new RobotIndicatorForm(mRobotControler);
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
            mRenderView.ExecuteCommand("Move");
        }
    }
}
