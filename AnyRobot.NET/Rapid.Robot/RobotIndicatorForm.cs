using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
            position.applyMatrix4d(trf);


            labelX.Text = String.Format("{0:N}", position.x);
            labelY.Text = String.Format("{0:N}", position.y);
            labelZ.Text = String.Format("{0:N}", position.z);

            numericX.Value = (decimal)position.x;
            numericY.Value = (decimal)position.y;
            numericZ.Value = (decimal)position.z;

            mRobot.AddTrackingPoint(position);


            for(int ii=0; ii< mLabelA.Count; ++ii)
            {
                mLabelA[ii].Text = String.Format("{0:N}", mRobot.GetVariable(0, (uint)ii+2));
            }

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
            var v = mRobot.AddVariable(armIdx, mButtonIndex + 1, mMoveSign * (double)numericUpDownSpeed.Value);
            UpdateAll();
        }

        private void numeric_ValueChanged(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mSavedFrame == null)
                return;

            var arm = mRobot.Robot.GetArm(0);
            var solver = IkSolverFactory.Instance().Create("LMA");
            if (!solver.Initialize(arm))
                return;


            solver.MoveToFrame(mSavedFrame);
            {

                for(uint ii=0; ii < arm.GetJointCount(); ++ii)
                {
                    var v = solver.GetValue(ii);
                    arm.SetValue(ii, v * 180/Math.PI);
                }

                mRobot.Robot.UpdateFrames();

                UpdateAll();
            }            
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            var arm = mRobot.Robot.GetArm(0);
            for (uint ii = 0; ii < arm.GetJointCount(); ++ii)
            {
                arm.SetValue(ii, 0);
            }

            mRobot.Robot.UpdateFrames();

            UpdateAll();
        }

        Matrix4d mSavedFrame;
        private void buttonSave_Click(object sender, EventArgs e)
        {
            mSavedFrame = mRobot.GetFinalPosition(0).clone();
        }
    }
}
