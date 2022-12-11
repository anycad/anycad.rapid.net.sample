using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;

namespace AnyCAD.Demo.Graphics
{
    class Simulation_RayHit : TestCase
    {

        Raycaster mCaster;
        SceneNode mTargetNode;
        SegmentsSceneNode mLine;
        ParticleSceneNode mParticle;
        public override void Run(RenderControl render)
        {
            ColorLookupTable clt = new ColorLookupTable();
            clt.SetColorMap(ColorMapKeyword.Create(EnumSystemColorMap.Rainbow));
            clt.SetMinValue(-5);
            clt.SetMaxValue(5);
            var geometry = GeometryBuilder.CreateCylinder(5, 4, 10, 64, 20);

            mTargetNode = new PrimitiveSceneNode(geometry, null);

            var camera = Camera.CreateOrthographic(100, 100, 1, 0.1f, 1000);
            camera.LookAt(new Vector3(0, 100, 0), new Vector3(0), Vector3.UNIT_Z);

            var ray = new Ray(new Vector3(0, 100, 0), -Vector3.UNIT_Y);
            mCaster = new Raycaster(camera, (uint)EnumShapeFilter.Face, ray);

            mLine = new SegmentsSceneNode(1, ColorTable.Red, 1);
            render.ShowSceneNode(mLine);

            mParticle = new ParticleSceneNode(100, ColorTable.Green, 5);
            render.ShowSceneNode(mParticle);

            render.ShowSceneNode(mTargetNode);
        }

        float _Angle = -30;
        uint _PointIndex = 0;
        public override void Animation(RenderControl render, float time)
        {
            if (_Angle > 30)
            {
                _Angle = -30;
                mParticle.ResetPositions(Vector3.Zero);
            }
                
            _Angle += 0.5f;

            Vector3 dir = new Vector3(0, -1, 0);
            dir = dir * Matrix4.makeRotationAxis(Vector3.UNIT_Z, (float)(_Angle /180 * Math.PI));
            dir.normalize();

            Vector3 start = new Vector3(0, 15, 0);
            
            var ray = new Ray(start, dir);

            mCaster.Clear();
            mCaster.SetRay(ray);
            Vector3 end = start + dir * 50;
            if (mCaster.HitTest(mTargetNode) > 0)
            {
                end = mCaster.GetTopItem().GetPosition();

                mParticle.SetPosition(_PointIndex, end);                
                ++_PointIndex;
            }
            mLine.SetPositions(0, new Vector3(0, 100, 0), end);
            mLine.Update();

            render.RequestDraw();
        }
    }
}
