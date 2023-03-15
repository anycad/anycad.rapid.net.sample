using AnyCAD.Foundation;
using System.Collections.Generic;

namespace AnyCAD.Demo.Graphics
{
    class RayAnimation
    {
        //点的位置
        ParticleSceneNode mMotionTrail;
        //目标的点
        List<Vector3> mPoints = new List<Vector3>();

        //射线
        PrimitiveSceneNode mLineNode;


        //射线发出的位置
        Vector3 mStart = new Vector3(200, 0, 200);

        //构造矩阵，避免更新射线的几何
        Matrix4 MakeTransform(Vector3 start, Vector3 end)
        {
            Vector3 dir = end - start;
            float len = dir.length();
            dir.normalize();

            return Matrix4.makeTranslation(start) * Matrix4.makeRotation(Vector3.UNIT_X, dir) * Matrix4.makeScale(len, 1, 1);
        }
        //记录当前射向的点
        int mCurrentIdx = 0;
        //用于控制快慢
        float mTime = 0;

        public RayAnimation(Vector3 position)
        {
            mStart = position.clone();
        }

        public void Create(IRenderView render)
        {
            // 随便构造些点
            float offset = 10.0f;
            for (int ii = 0; ii < 10; ++ii)
            {
                for (int jj = 0; jj < ii; ++jj)
                {
                    mPoints.Add(new Vector3(jj * offset, 100, ii * offset));
                }
            }


            mMotionTrail = new ParticleSceneNode((uint)mPoints.Count, ColorTable.Green, 3.0f);

            mCurrentIdx = 0;

            render.ShowSceneNode(mMotionTrail);

            var lineMaterial = BasicMaterial.Create("myline");
            lineMaterial.SetColor(ColorTable.Hex(0xFF0000));
            var line = GeometryBuilder.CreateLine(Vector3.Zero, new Vector3(1, 0, 0));
            mLineNode = new PrimitiveSceneNode(line, lineMaterial);
            mLineNode.SetTransform(MakeTransform(mStart, mPoints[0]));
            mLineNode.RequstUpdate();
            render.ShowSceneNode(mLineNode);
        }

        public bool Play(IRenderView render, float time)
        {
            if (mCurrentIdx >= mPoints.Count)
            {
                mLineNode.SetVisible(false);

                return false;
            }



            mTime += time;
            if (mTime < 100) //距离上次更新不到100ms，就返回
                return true;
            mTime = 0;

            Vector3 target = mPoints[mCurrentIdx];

            mLineNode.SetTransform(MakeTransform(mStart, target));
            mLineNode.RequstUpdate();

            mMotionTrail.SetPosition((uint)mCurrentIdx, target);
            mMotionTrail.RequstUpdate();

            render.RequestDraw(EnumUpdateFlags.Scene);

            ++mCurrentIdx;

            return true;
        }
    }

    class Simulation_DynamicRay : TestCase
    {

        RigidAnimation mCome;
        RigidAnimation mGo;
        RayAnimation mWorking;
        Vector3 mWorkingPosition = new Vector3(200, 200, 0);

        PrimitiveSceneNode mDevice;
        public override void Run(IRenderView render)
        {

            // add a plane
            var mMaterial1 = MeshPhongMaterial.Create("phong.texture");
            mMaterial1.SetFaceSide(EnumFaceSide.DoubleSide);
            var texture = ImageTexture2D.Create(GetResourcePath("textures/bricks2.jpg"));
            mMaterial1.SetColorMap(texture);

            var plane = GeometryBuilder.CreatePlane(500, 500);
            var planeNode = new PrimitiveSceneNode(plane, mMaterial1);
            planeNode.SetTransform(Matrix4.makeTranslation(new Vector3(0, 0, -2.5f)));
            planeNode.SetPickable(false);

            render.ShowSceneNode(planeNode);



            mDevice = new PrimitiveSceneNode(GeometryBuilder.CreateSphere(5), null);
            render.ShowSceneNode(mDevice);

            mCome = new RigidAnimation();
            mCome.Add(new MoveAnimationClip(mDevice, mWorkingPosition, 0, 5));

            mGo = new RigidAnimation();
            mGo.Add(new MoveAnimationClip(mDevice, new Vector3(-200, -200, 0), 0, 5));

        }


        int step = 0;
        public override void Animation(IRenderView render, float time)
        {
            if(step == 0)
            {
                if(mCome.Play(time))
                {
                    render.RequestDraw(EnumUpdateFlags.Scene);
                }
                else
                {
                    step = 1;
                    if (mWorking == null)
                    {
                        mWorking = new RayAnimation(mWorkingPosition);
                        mWorking.Create(render);
                    }
                        
                }
            }
            else if(step == 1)
            {
                if(!mWorking.Play(render, time))
                {
                    step = 2;
                }
            }
            else if(step == 2)
            {
                if (mGo.Play(time))
                {
                    render.RequestDraw(EnumUpdateFlags.Scene);
                }
                else
                {
                    step = 3;
                }
            }
        }
     }
}
