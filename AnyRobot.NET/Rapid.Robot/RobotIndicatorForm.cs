using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnyCAD.Foundation;
using AnyCAD;
namespace RapidRobot
{
    public partial class RobotIndicatorForm : Form
    {
        AnyCAD.Robot.RobotControler mRobot;
        List<Label> mLabelA = new List<Label>();
        double mMoveSign = 1;
        uint mButtonIndex = 0;
        public RobotIndicatorForm(AnyCAD.Robot.RobotControler robot)
        {
            mRobot = robot;
            InitializeComponent();

            mLabelA.Add(labelA1);
            mLabelA.Add(labelA2);
            mLabelA.Add(labelA3);
            mLabelA.Add(labelA4);
            mLabelA.Add(labelA5);
            mLabelA.Add(labelA6);

            labelA1.Text = robot.GetVariable(0, 0).ToString();
            labelA2.Text = robot.GetVariable(0, 1).ToString();
            labelA3.Text = robot.GetVariable(0, 2).ToString();
            labelA4.Text = robot.GetVariable(0, 3).ToString();
            labelA5.Text = robot.GetVariable(0, 4).ToString();
            labelA6.Text = robot.GetVariable(0, 5).ToString();

            uint nArmCount = robot.Robot.GetArmCount();
            for(uint ii=0; ii<nArmCount; ++ii)
            {
                var arm = robot.Robot.GetArm(ii);
                comboBox1.Items.Add(arm.GetName());
            }
            comboBox1.SelectedIndex = 0;
            

            UpdateAll();

            this.timer1.Enabled = false;
        }

        public delegate void UpdateView();

        public UpdateView OnUpdate;

        void UpdateAll()
        {
            var trf = mRobot.GetFinalPosition(0);

            var position = new Vector3(0);
            position.applyMatrix4(trf);

            labelX.Text = position.x.ToString();
            labelY.Text = position.y.ToString();
            labelZ.Text = position.z.ToString();

            mRobot.AddTrackingPoint(position);

            if (OnUpdate != null)
                OnUpdate();
        }
        private void buttonPositive_MouseDown(object sender, MouseEventArgs e)
        {
            Button c1 = (Button)sender;
            mButtonIndex = uint.Parse((string) c1.Tag);
            mMoveSign = 1;

            this.timer1.Enabled = true;
        }

        private void buttonNegitive_MouseDown(object sender, MouseEventArgs e)
        {
            Button c1 = (Button)sender;
            mButtonIndex = uint.Parse((string)c1.Tag);
            mMoveSign = -1;

            this.timer1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            mRobot.ShowTracking(c.Checked);
            if (OnUpdate != null)
                OnUpdate();
        }

        private void buttonA_MouseUp(object sender, MouseEventArgs e)
        {
            this.timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            uint armIdx = (uint)comboBox1.SelectedIndex;
            var v = mRobot.AddVariable(armIdx, mButtonIndex, mMoveSign * (double)numericUpDownSpeed.Value);
            if(mButtonIndex>0)
                mLabelA[(int)mButtonIndex - 1].Text = v.ToString();
            UpdateAll();
        }
    }
}
