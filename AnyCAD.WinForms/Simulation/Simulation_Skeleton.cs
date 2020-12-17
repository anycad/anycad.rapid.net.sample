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
            double scale = 0.5;
            mRobot.AddJoint(EnumRobotJointType.Fixed, 0, 0, 200 * scale, 0);
            mRobot.AddJoint(EnumRobotJointType.Revolute, 0, 0, 130 * scale, 0);// Link1
            mRobot.AddJoint(EnumRobotJointType.Revolute, 90, 30, 0, 180);// Link2
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
            if (this.State == 0)
                return;

            mTheta += 0.5f;
            mRobot.SetVariable(1, mTheta * 1);
            mRobot.SetVariable(2, mTheta * 2);
            mRobot.SetVariable(4, mTheta * 3);
            mRobot.SetVariable(6, mTheta * 4);
            mRobot.SetVariable(7, mTheta * 5);
            mRobot.SetVariable(8, mTheta * 6);
            if (mD > 30 || mD < 10)
                mSign *= -1;
            mD += mSign * 0.2f;

            mRobot.UpdateFrames();

            Vector3 pt = new Vector3(0);
            pt = pt * mRobot.GetFinalTransform();
            mMotionTrail.SetPosition(mCount, pt);
            mMotionTrail.UpdateBoundingBox();
            ++mCount;

            if (this.State == 1)
            {
                var position = pt - Vector3.UNIT_Y * 500;
                render.GetCamera().LookAt(position, pt, Vector3.UNIT_Z);
            }
            else if (this.State == 2)
            {
                var position = pt + Vector3.UNIT_Z * 500;
                render.GetCamera().LookAt(position, pt, Vector3.UNIT_Y);
            }
            else if (this.State == 3)
            {
                var camera = render.GetCamera();
                var postion = new Vector3(0, -500, 0);

                var trf = Matrix4.makeRotationAxis(Vector3.UNIT_Z, mTheta * 3.1415926f/180);
                postion.applyMatrix4(trf);
               
                camera.LookAt(postion, Vector3.Zero, Vector3.UNIT_Z);
            }

            if (this.State == 1 && mTheta > 360)
            {
                this.State = 2;
            }
            else if (this.State == 2 && mTheta > 720)
            {
                render.SetStandardView(EnumStandardView.Front);
                this.State = 3;
            }
                

            render.RequestDraw(EnumUpdateFlags.Scene);
        }
    }
}
