using AnyCAD.Foundation;
using System;

namespace Rapid.Robot
{
    internal class RobotTargetEditor : NodeFrameEidtor
    {
        IkSolverLMA mSolver;
        RobotArm mRobotArm;
        public RobotTargetEditor(AxWidget target, RobotArm arm)
            : base(target, TransformWidget.Create(20), "Robot.MoveTarget")
        {

            mRobotArm = arm;

            mSolver = new IkSolverLMA();
            mSolver.Initialize(mRobotArm);
        }

        public override bool PreviewTransform(ViewContext ctx, Matrix4 trf)
        {
            if (!mSolver.MoveToFrame(trf.ToMatrix4d()))
                return false;


            for (uint ii = 0; ii < mRobotArm.GetJointCount(); ++ii)
            {
                var v = mSolver.GetValue(ii) * 180 / Math.PI;
                mRobotArm.SetValue(ii, v);
            }

            mRobotArm.UpdateFrames();

            ctx.RequestUpdate(EnumUpdateFlags.Scene);

            return base.PreviewTransform(ctx, trf);
        }
    }
}
