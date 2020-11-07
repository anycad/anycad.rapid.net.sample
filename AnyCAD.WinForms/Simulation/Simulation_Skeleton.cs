using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Simulation_Skeleton : TestCase
    {
        RobotModel mRobot = new RobotModel();
        ParticleSceneNode mMotionTrail = new ParticleSceneNode(1000, Vector3.Red, 3.0f);
        public override void Run(RenderControl render)
        {
            double d1 = 40;
            double d2 = 0;
            double d4 = 5;
            double a1 = 50;
            double a2 = 25;


            double scale = 0.5;
            mRobot.AddJoint(EnumRobotJointType.Fixed, 0, 0, 200 * scale, 0);
            mRobot.AddJoint(EnumRobotJointType.Revolute, 0, 0, 130 * scale, 0);// Link1
            var id = mRobot.AddJoint(EnumRobotJointType.Revolute, 90, 30, 0, 180);// Link2
            mRobot.AddJoint(EnumRobotJointType.Fixed, 90, 0, 480 * scale, 0); // Link3
            mRobot.AddJoint(EnumRobotJointType.Revolute, 90, 0, 0, 270);
            mRobot.AddJoint(EnumRobotJointType.Fixed, 90, 0, 100 * scale, 0); // Link4
            mRobot.AddJoint(EnumRobotJointType.Revolute, 0, 0, 380 * scale, 0);
            mRobot.AddJoint(EnumRobotJointType.Revolute, 270, 0, 0, 0); // Link5 
            mRobot.AddJoint(EnumRobotJointType.Revolute, 90, 0, 100 * scale, 0); // Link6 


            render.ShowSceneNode(mRobot);

            render.ShowSceneNode(mMotionTrail);
        }

        float mTheta = 0;
        float mD = 10;
        float mSign = 1;
        uint mCount = 0;
        public override void Animation(RenderControl render, float time)
        {
            mTheta += 0.5f;
            mRobot.SetVariable(1, mTheta * 2);
            mRobot.SetVariable(2, mTheta * 3);
            mRobot.SetVariable(4, mTheta * 6);
            mRobot.SetVariable(6, mTheta * 2);
            mRobot.SetVariable(7, mTheta * 6);
            mRobot.SetVariable(8, mTheta * 6);
            if (mD > 30 || mD < 10)
                mSign *= -1;
            mD += mSign * 0.2f;

            mRobot.UpdateFrames();

            Vector3 pt = new Vector3(0);
            pt = pt * mRobot.GetFinalTransform();
            mMotionTrail.SetPosition(mCount, pt);
            ++mCount;

            render.RequestDraw(EnumUpdateFlags.Scene);
        }
    }
}
