using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Simulation_Links : TestCase
    {

        ParticleSceneNode mMotionTrail = new ParticleSceneNode(1000, ColorTable.Red, 3.0f);

        RobotArm mArmRRR;
        RobotArm mArmPPP;
        //RobotArm mArmRR;
        MaterialInstance mLinkMaterial;
        MaterialInstance mLinkMaterial2;
        SceneNode CreateLine(Vector3 start, Vector3 end, MaterialInstance material)
        {
            var line = GeometryBuilder.CreateLine(start, end);
            return new PrimitiveSceneNode(line, material);
        }
        public override void Run(RenderControl render)
        {
            mLinkMaterial = BasicMaterial.Create("link");
            mLinkMaterial.SetColor(ColorTable.Indigo);
            mLinkMaterial.SetLineWidth(5);
            mLinkMaterial2 = BasicMaterial.Create("link");
            mLinkMaterial2.SetColor(ColorTable.Auqamarin);
            mLinkMaterial2.SetLineWidth(5);


            mArmRRR = new RobotArm();
            mArmRRR.SetComputeMethod(EnumDHComputeMethod.Modified_DH);
            // base
            var idx = mArmRRR.AddJoint(EnumRobotJointType.Fixed, 0, 0, 0, 0);
            mArmRRR.AddLink(idx, CreateLine(new Vector3(0, 0, 0), new Vector3(0, 0, 10), mLinkMaterial));
            // L1
            idx = mArmRRR.AddJoint(EnumRobotJointType.Revolute, 90, 0, 100, 0);
            mArmRRR.AddLink(idx, CreateLine(new Vector3(0, 0, 10), new Vector3(0, 0, 100), mLinkMaterial));
            // L2
            idx = mArmRRR.AddJoint(EnumRobotJointType.Revolute, -90, 50, 0, 0);
            mArmRRR.AddLink(idx, CreateLine(new Vector3(0, 0, 100), new Vector3(50, 0, 100), mLinkMaterial));
            // L3
            idx = mArmRRR.AddJoint(EnumRobotJointType.Revolute, 0, 0, 20, 0);
            mArmRRR.AddLink(idx, CreateLine(new Vector3(50, 0, 100), new Vector3(50, 0, 120), mLinkMaterial));
            // Tool
            mArmRRR.AddTool( 0, 0, 20, 0, CreateLine(new Vector3(0, 0, -20), new Vector3(0, 0, 0), mLinkMaterial2));
     

            mArmRRR.ApplyDH();
            mArmRRR.UpdateFrames();

            //render.ShowSceneNode(mArmRRR);


            mArmPPP = new RobotArm();
            mArmPPP.SetComputeMethod(EnumDHComputeMethod.Modified_DH);

            idx = mArmPPP.AddJoint(EnumRobotJointType.Fixed, 0, 0, 0, 0);
            mArmPPP.AddLink(idx, CreateLine(new Vector3(0, 0, 0), new Vector3(0, 0, 350), mLinkMaterial2));
            //L1
            idx = mArmPPP.AddJoint(EnumRobotJointType.Prismatic, -90, 0, 200, 90);
            mArmPPP.AddLink(idx, CreateLine(new Vector3(0, 0, 200), new Vector3(100, 0, 200), mLinkMaterial2));
            //L2
            idx = mArmPPP.AddJoint(EnumRobotJointType.Prismatic, -90, 100, -100, 0);
            mArmPPP.AddLink(idx, CreateLine(new Vector3(100, 0, 200), new Vector3(100, 100, 200), mLinkMaterial2));


            mArmPPP.AddTool(0, 0, 0, 0, mArmRRR);

            mArmPPP.ApplyDH();
            mArmPPP.UpdateFrames();
     
            render.ShowSceneNode(mArmPPP);
            render.ShowSceneNode(mMotionTrail);
        }


        double step = 0;
        double sign = 0.5;
        uint pointIdx = 0;
        public override void Animation(RenderControl render, float time)
        {
            step += sign;
            if (step > 100)
            {
                sign = -0.5;
            }
            if (step < 0)
            {
                sign = 0.5;
            }

            for (uint ii = 1; ii < mArmPPP.GetJointCount(); ++ii)
            {
                mArmPPP.SetValue(ii, step);
            }
            mArmPPP.UpdateFrames();

            for (uint ii = 1; ii < mArmRRR.GetJointCount(); ++ii)
            {
                mArmRRR.SetValue(ii, step * 3 / ii);
            }
            mArmRRR.UpdateFrames();


            Vector3 endPt = new Vector3();
            var trf = mArmRRR.GetFinalTransform();
            endPt.applyMatrix4(mArmRRR.GetWorldTransform() * trf);

            mMotionTrail.SetPosition(++pointIdx, endPt);
            mMotionTrail.UpdateBoundingBox();

            render.RequestDraw(EnumUpdateFlags.Scene);
        }
     }
}
