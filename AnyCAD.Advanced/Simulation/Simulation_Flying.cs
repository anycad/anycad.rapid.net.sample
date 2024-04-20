using AnyCAD.Foundation;
using System;
using System.Collections.Generic;

namespace AnyCAD.Demo.Graphics
{
    class DeviceObject
    {
        public SceneNode mNode;
        public List<Vector3> mBasePoints;
        public List<Vector3> mCurrentPoints = new List<Vector3>();
        public Vector3 mPosition;

        public DeviceObject()
        {

        }

        public void Create(GRepShape shape, Vector3 position, List<Vector3> points)
        {
            mPosition = position;
            mBasePoints = points;

            mNode = new BrepSceneNode(shape);
            mNode.SetTransform(Matrix4.makeTranslation(mPosition));
            mNode.RequestUpdate();
        }

        public void Transform(Matrix4 trf)
        {
            var globalTrf = Matrix4.makeTranslation(mPosition) * trf;
            mNode.SetTransform(globalTrf);
            mNode.RequestUpdate();

            mCurrentPoints.Clear();

            foreach (var pt in mBasePoints)
            {
                mCurrentPoints.Add(pt * globalTrf);
            }
        }

        public void MoveByZ(float a, float b, float c)
        {
            var offsetB = b - a;

        }
    }

    class DeviceObjectGroup
    {
        public List<DeviceObject> mDevices = new List<DeviceObject>();
        public float mZ = 10;
        public float mStepZ = 0.1f;
        public float mStepAngle = 0.02f;
        public float mAngle = 0;
        public void Create(IRenderView render, float radius)
        {
            var triRadius = 1;
            var points = new GPntList();

            points.Add(new GPnt(1, 0, 0));
            double a1 = 120.0 / 180.0 * Math.PI;
            points.Add(new GPnt(Math.Cos(a1), Math.Sin(a1), 0));
            double a2 = 240.0 / 180.0 * Math.PI;
            points.Add(new GPnt(Math.Cos(a2), Math.Sin(a2), 0));

            TopoShape shape = SketchBuilder.MakePolygonFace(points);

            List<Vector3> basePoints = new List<Vector3>();
            basePoints.Add(new Vector3(-1, -1, 0));
            basePoints.Add(new Vector3(1, -1, 0));
            basePoints.Add(new Vector3(0, triRadius, 0));


            var material = MeshPhongMaterial.Create("tube.color");
            material.SetFaceSide(EnumFaceSide.DoubleSide);
            material.SetUniform("diffuse", new Vector3(1, 0, 1));

            var bs = GRepShape.Create(shape, material, null, 0.1, false);
            bs.Build();

            int nCount = 20;
            List<Vector3> positions = new List<Vector3>();
            double step = Math.PI * 2 / nCount;
            for (int ii = 0; ii < nCount; ++ii)
            {
                Vector3 pt = new Vector3((float)(radius * Math.Cos(ii * step)),
                    (float)(radius * Math.Sin(ii * step)), 0);
                positions.Add(pt);

                var obj = new DeviceObject();
                obj.Create(bs, pt, basePoints);
                mDevices.Add(obj);
            }
        }

        public void Show(IRenderView render)
        {
            foreach (var device in mDevices)
            {
                render.ShowSceneNode(device.mNode);
            }

            mOffsetZ.x += mRandom.Next(1, 10) / 10.0f;
            mOffsetZ.y += mRandom.Next(1, 10) / 10.0f;
            mOffsetZ.z += mRandom.Next(1, 10) / 10.0f;

            //foreach (var device in mDevices)
            //{
            //    mDevices[0].MoveByZ(mOffsetZ.x, mOffsetZ.y, mOffsetZ.z);
            //}
        }

        float rotateAngleZ = 0;

        Vector3 mOffsetZ = new Vector3(1, 1.2f, 1.5f);
        Random mRandom = new Random();
        public void Animation()
        {

            if (mAngle > Math.PI / 6 || mAngle < -Math.PI / 6)
            {
                mStepAngle = -mStepAngle;
            }
            mAngle += mStepAngle / 12;


            rotateAngleZ += Math.Abs(mStepAngle);

            var rotateZ = Matrix4.makeRotationAxis(Vector3.UNIT_Z, rotateAngleZ);
            var dirX = Vector3.UNIT_X * rotateZ;
            var axis = dirX.cross(Vector3.UNIT_Z);

            var rotateX = Matrix4.makeRotationAxis(axis, mAngle);

            var dir = Vector3.UNIT_Z * rotateX;

            var plane = new PlaneF(dir.normalized(), new Vector3(0, 0, 0));

            foreach (var device in mDevices)
            {
                var pt = plane.projectVector(device.mPosition);
                var trf = Matrix4.makeTranslation(0, 0, pt.z + mZ);
                device.Transform(trf * rotateX);
            }
        }
    }

    class Simulation_Flying : TestCase
    {

        List<DeviceObjectGroup> mDeviceGroup = new List<DeviceObjectGroup>();
        public override void Run(IRenderView render)
        {
            if (mDeviceGroup.Count == 0)
            {
                var group1 = new DeviceObjectGroup();
                group1.Create(render, 10);
                mDeviceGroup.Add(group1);

                var group2 = new DeviceObjectGroup();
                group2.Create(render, 15);
                group2.mStepAngle = -group2.mStepAngle;

                mDeviceGroup.Add(group2);
            }

            foreach (var group in mDeviceGroup)
            {
                group.Show(render);
            }

            render.EnableAnimation(true);
            //var p1 = SketchBuilder.MakePlanarFace(new GPln(new GPnt(0, 0, 1.2), GP.DZ()), -10, 20, -10, 20);
            //render.ShowShape(p1, Vector3.LightGray);
            //var p2 = SketchBuilder.MakePlanarFace(new GPln(new GPnt(0, 0, 1.5), GP.DZ()), -10, 20, -10, 20);
            //render.ShowShape(p2, Vector3.LightGray);

            render.RequestDraw(EnumUpdateFlags.Scene);
        }

        public override void Animation(IRenderView render, float time)
        {

            foreach (var group in mDeviceGroup)
            {
                group.Animation();
            }

            render.RequestDraw(EnumUpdateFlags.Scene);
        }

        public override void Exit(IRenderView render)
        {
            render.EnableAnimation(false);
        }
    }
}
