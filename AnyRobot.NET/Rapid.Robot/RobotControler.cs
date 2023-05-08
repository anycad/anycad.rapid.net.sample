
using AnyCAD.Foundation;

namespace AnyCAD.Robot
{
    public class RobotControler
    {
        RobotBody mRobot;
        ParticleSceneNode mTrajectory = new ParticleSceneNode(1000, Vector3.Red, 5);
        uint nSeedIdx = 0;

        public RobotBody Robot { get { return mRobot; } }
        public ParticleSceneNode Trajectory { get {return mTrajectory;} }

        public RobotControler(RobotBody robot)
        {
            mRobot = robot;
            mTrajectory.SetVisible(true);
        }

        public void Reset()
        {
            mRobot.Reset();
        }
        public void ShowTracking(bool bShow)
        {
            mTrajectory.SetVisible(bShow);
        }
        public void AddTrackingPoint(Vector3 position)
        {
            mTrajectory.SetPosition(nSeedIdx++, position);
            mTrajectory.UpdateBoundingBox();
            mTrajectory.RequestUpdate();
        }

        public double GetVariable(uint armIdx, uint idx)
        {
            return mRobot.GetValue(armIdx, idx);
        }

        public Matrix4d GetFinalPosition(uint armIdx)
        {
            return mRobot.GetFinalFrame(armIdx);
        }
        public double AddVariable(uint armIdx, uint idx, double step)
        {
            double val = mRobot.GetValue(armIdx, idx);
            val += step;
            mRobot.SetValue(armIdx, idx, val);
         
            mRobot.UpdateFrames();

            return val;
        }
    }
}
