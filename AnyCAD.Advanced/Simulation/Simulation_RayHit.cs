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
        public override void Run(IRenderView render)
        {
            ColorLookupTable clt = new ColorLookupTable();
            clt.SetColorMap(ColorMapKeyword.Create(EnumSystemColorMap.Rainbow));
            clt.SetMinValue(-5);
            clt.SetMaxValue(5);
            var geometry = GeometryBuilder.CreateCylinder(5, 4, 10, 64, 20);

            mTargetNode = new PrimitiveSceneNode(geometry, null);

            var camera = new Camera(100, 100, new Vector3d(0, 100, 0), new Vector3d(0), Vector3d.UNIT_Z);
            //Camera.CreateOrthographic(100, 100, 1, 0.1f, 1000);
            camera.SetNear(0.1f);
            camera.SetFar(1000);
            camera.SetProjectionType(EnumProjectionType.Orthographic);
            camera.UpdateProjectionMatrix();

            var ray = new Ray(new Vector3d(0, 100, 0), -Vector3d.UNIT_Y);
            mCaster = new Raycaster(camera, (uint)EnumShapeFilter.Face, ray, 50,50);

            mLine = new SegmentsSceneNode(1, ColorTable.Red, 1);
            render.ShowSceneNode(mLine);

            mParticle = new ParticleSceneNode(100, ColorTable.Green, 5);
            render.ShowSceneNode(mParticle);

            render.ShowSceneNode(mTargetNode);

            render.EnableAnimation(true);
        }
        public override void Exit(IRenderView render)
        {
            render.EnableAnimation(false);
        }

        float _Angle = -30;
        uint _PointIndex = 0;
        public override void Animation(IRenderView render, float time)
        {
            if (_Angle > 30)
            {
                _Angle = -30;
                mParticle.ResetPositions(Vector3.Zero);
            }
                
            _Angle += 0.5f;

            Vector3d dir = new Vector3d(0, -1, 0);
            dir = dir * Matrix4d.makeRotationAxis(Vector3d.UNIT_Z,  _Angle /180 * Math.PI);
            dir.normalize();

            Vector3d start = new Vector3d(0, 15, 0);
            
            var ray = new Ray(start, dir);

            mCaster.Clear();
            mCaster.SetRay(ray);
            Vector3 end = Vector3.From(start + dir * 50);
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
