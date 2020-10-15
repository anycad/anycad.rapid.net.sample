using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_Robot2 : TestCase
    {
        RobotModel mRobot = new RobotModel();
        public override void Run(RenderControl render)
        {
            double d1 = 40;
            double d2 = 0;
            double d4 = 5;
            double a1 = 50;
            double a2 = 25;

            // base (0,0,0)

            // Joint 1
            var s1 = SketchBuilder.MakeLine(GP.Origin(), new GPnt(0, 0, d1));
            var n1 = BrepSceneNode.Create(s1, null, null);
            var j1 = new RobotPart(EnumRobotJointType.Revolute, 0, 0, d1, 0);
            j1.SetVisualNode(n1);
            mRobot.AddPart(j1);

            // Joint 2
            var s2 = SketchBuilder.MakeLine(new GPnt(0, 0, d1), new GPnt(a1, 0, d1));
            var n2 = BrepSceneNode.Create(s2, null, null);
            var j2 = new RobotPart(EnumRobotJointType.Revolute, 0, a1, d2, 0);
            j2.SetVisualNode(n2);
            j2.SetPosition(Vector3.From(0, 0, d1));
            mRobot.AddPart(j2);

            // Joint 3
            var s3 = SketchBuilder.MakeLine(new GPnt(a1, 0, d1 + d2), new GPnt(a1 + a2, 0, d1 + d2));
            var n3 = BrepSceneNode.Create(s3, null, null);
            var j3 = new RobotPart(EnumRobotJointType.Revolute, 0, a2, 0, 0);
            j3.SetVisualNode(n3);
            j3.SetPosition(Vector3.From(a1, 0, d1 +d2));
            mRobot.AddPart(j3);

            // Joint 4
            var s4 = SketchBuilder.MakeLine(new GPnt(a1 + a2, 0, d1 + d2), new GPnt(a1 + a2, 0, d1 + d2 + d4 ));
            var n4 = BrepSceneNode.Create(s4, null, null);
            var j4 = new RobotPart(EnumRobotJointType.Revolute, 0, 0, d4, 0);
            j4.SetVisualNode(n4);
            j4.SetPosition(Vector3.From(a1 + a2, 0, d1 + d2));
            mRobot.AddPart(j4);

            render.ShowSceneNode(mRobot);
        }

        float mTheta = 0;
        float mD = 10;
        float mSign = 1;
        public override void Animation(RenderControl render, float time)
        {
            mTheta += 0.5f;
            mRobot.SetVariable(0, mTheta);
            mRobot.SetVariable(1, mTheta * 2);
            mRobot.SetVariable(2, mTheta * 3);
            mRobot.SetVariable(3, mTheta * 6);
            if (mD > 30 || mD < 10)
                mSign *= -1;
            mD += mSign * 0.2f;

            mRobot.UpdateFrames();

            render.UpdateWorld();
        }
     }
}
