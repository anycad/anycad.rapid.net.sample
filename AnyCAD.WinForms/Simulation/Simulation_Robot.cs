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
        RobotModel mRobot = new RobotModel();
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

            double scale = 1;
            mRobot.AddJoint(EnumRobotJointType.Fixed, 0, 0, 200 * scale, 0);
            mRobot.AddJoint(EnumRobotJointType.Revolute, 0, 0, 130 * scale, 0);// Link1

            mRobot.AddJoint(EnumRobotJointType.Revolute, 90, 30, 0, 180);// Link2
            mRobot.AddJoint(EnumRobotJointType.Fixed, 90, 0, 480 * scale, 0);

            mRobot.AddJoint(EnumRobotJointType.Revolute, 90, 0, 0, 270);// Link3
            mRobot.AddJoint(EnumRobotJointType.Fixed, 90, 0, 100 * scale, 0);

            mRobot.AddJoint(EnumRobotJointType.Revolute, 0, 0, 380 * scale, 0); // Link4
            mRobot.AddJoint(EnumRobotJointType.Revolute, 270, 0, 0, 0); // Link5 
            mRobot.AddJoint(EnumRobotJointType.Revolute, 90, 0, 100 * scale, 0); // Link6 

            double acc = 1;

            mRobot.AddLink(0, BrepSceneNode.Create(BrepIO.Open(rootPath + files[0]), material, null, acc));
            //Link1
            mRobot.AddLink(1, BrepSceneNode.Create(BrepIO.Open(rootPath + files[1]), material, null, acc));
            //Link2
            mRobot.AddLink(3, BrepSceneNode.Create(BrepIO.Open(rootPath + files[2]), material, null, acc));
            //Link3
            mRobot.AddLink(5, BrepSceneNode.Create(BrepIO.Open(rootPath + files[3]), material, null, acc));
            //Link4
            mRobot.AddLink(6, BrepSceneNode.Create(BrepIO.Open(rootPath + files[4]), material, null, acc));
            //Link5
            mRobot.AddLink(7, BrepSceneNode.Create(BrepIO.Open(rootPath + files[5]), material, null, acc));
            //Link6
            mRobot.AddLink(8, BrepSceneNode.Create(BrepIO.Open(rootPath + files[6]), material, null, acc));

            render.ShowSceneNode(mRobot);

            render.ShowSceneNode(mMotionTrail);

            mRobot.ResetInitialState();
        }

        float mTheta = 0;
        float mD = 10;
        float mSign = 1;
        uint mCount = 0;
        public override void Animation(RenderControl render, float time)
        {
            mTheta += 0.5f;

            mRobot.SetVariable(1, mTheta * 2);
            mRobot.SetVariable(2, 120);
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
            mMotionTrail.UpdateBoundingBox();
            ++mCount;

            render.RequestDraw(EnumUpdateFlags.Scene);
        }
    }
}
