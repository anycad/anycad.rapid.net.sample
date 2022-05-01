
using AnyCAD.Foundation;

namespace AnyCAD.Robot
{
    public class RobotControler
    {
        RobotBody mRobot;
        ParticleSceneNode mTrackingNode = new ParticleSceneNode(1000, Vector3.Red, 5);
        uint nSeedIdx = 0;

        public RobotBody Robot { get { return mRobot; } }
        public ParticleSceneNode TrackingPath { get {return mTrackingNode;} }

        public RobotControler(RobotBody robot)
        {
            mRobot = robot;
            mTrackingNode.SetVisible(false);
        }

        public void Reset()
        {
            mRobot.Reset();
        }
        public void ShowTracking(bool bShow)
        {
            mTrackingNode.SetVisible(bShow);
        }
        public void AddTrackingPoint(Vector3 position)
        {
            mTrackingNode.SetPosition(nSeedIdx++, position);
            mTrackingNode.UpdateBoundingBox();
            mTrackingNode.RequstUpdate();
        }

        public double GetVariable(uint armIdx, uint idx)
        {
            return mRobot.GetValue(armIdx, idx);
        }

        public Matrix4 GetFinalPosition(uint armIdx)
        {
            return mRobot.GetFinalTransform(armIdx);
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
