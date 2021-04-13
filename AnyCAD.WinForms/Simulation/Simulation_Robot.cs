using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Simulation_Robot : TestCase
    {
        RobotArm mRobot = new RobotArm();
        ParticleSceneNode mMotionTrail = new ParticleSceneNode(1000, Vector3.Red, 3.0f);
        public override void Run(RenderControl render)
        {
            var material = MeshStandardMaterial.Create("robot");
            material.SetColor(new Vector3(0.9f));
            material.SetFaceSide(EnumFaceSide.DoubleSide);
            material.SetOpacity(0.5f);
            material.SetTransparent(true);

            List<string> files = new List<string>();
            files.Add("Base.brep");
            files.Add("AXIS1.brep");
            files.Add("AXIS2.brep");
            files.Add("AXIS3.brep");
            files.Add("AXIS4.brep");
            files.Add("AXIS5.brep");
            files.Add("AXIS6.brep");

            var rootPath = GetResourcePath(@"models\6R\");

            List<BrepSceneNode> links = new List<BrepSceneNode>();
            for(int ii=0; ii<files.Count; ++ii)
            {
                links.Add(BrepSceneNode.CreateBatch(ShapeIO.Open(rootPath + files[ii]), material, null, 0.1));
            }

            mRobot.AddJoint(EnumRobotJointType.Fixed, 0, 0, 200 , 0);
            mRobot.AddJoint(EnumRobotJointType.Revolute, 0, 0, 130 , 0);// Link1
            mRobot.AddJoint(EnumRobotJointType.Revolute, 90, 30, 0, 180, new RobotDH(90, 0, 480 , 0));// Link2
            mRobot.AddJoint(EnumRobotJointType.Revolute, 90, 0, 0, 270, new RobotDH(90, 0, 100 , 0));// Link3
            mRobot.AddJoint(EnumRobotJointType.Revolute, 0, 0, 380 , 0); // Link4
            mRobot.AddJoint(EnumRobotJointType.Revolute, 270, 0, 0, 0); // Link5 
            mRobot.AddJoint(EnumRobotJointType.Revolute, 90, 0, 100 , 0); // Link6 

            for(int ii=0; ii<7; ++ii)
            {
                mRobot.AddLink((uint)ii, links[ii]);
            }


            mRobot.ResetInitialState();

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
            mRobot.SetVariable(3, mTheta * 2);
            mRobot.SetVariable(4, mTheta * 2);
            mRobot.SetVariable(5, mTheta * 1);
            mRobot.SetVariable(6, mTheta * 1);

            if (mD > 30 || mD < 10)
                mSign *= -1;
            mD += mSign * 0.2f;

            mRobot.UpdateFrames();

            Vector3 pt = new Vector3(0);
            pt = pt * mRobot.GetFinalTransform();
            mMotionTrail.SetPosition(mCount, pt);
            mMotionTrail.UpdateBoundingBox();
            ++mCount;

            render.RequestDraw(EnumUpdateFlags.Scene);
        }
    }
}
