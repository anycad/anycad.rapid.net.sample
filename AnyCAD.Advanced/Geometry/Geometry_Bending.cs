using AnyCAD.Foundation;
using System;
using System.Collections.Generic;

namespace AnyCAD.Demo.Geometry
{
    /// <summary>
    /// 折弯状态和矩阵状态
    /// </summary>
    class BendingInfo
    {
        /// <summary>
        /// 保存折弯形变B
        /// </summary>
        public GRepShape BendingShape; 
        /// <summary>
        /// 折弯左半边的矩阵变换D
        /// </summary>
        public Matrix4 LeftHandleTransform;
        /// <summary>
        /// 折弯右半部分的矩阵变换C
        /// </summary>
        public Matrix4 RightHandleTransform;
        /// <summary>
        /// 全局位移变换
        /// </summary>
        public Matrix4 GlobalOffset;
    }

    class Geometry_Bending : TestCase
    {
        double mHeight = 5; //构件的高度
        GCirc mCircle = new GCirc(GP.XOY(), 10); //折弯内圆
        GCirc mCircle2 = new GCirc(GP.XOY(), 14);// 折弯外圆
        double mAngleOffset = 0.1; // 半角
        double mMiddleAngle = Math.PI / 2; //折弯中心角度
        float mDistance = 0; //位移

        BrepSceneNode mBendingNode; // 折弯显示对象B
        BrepSceneNode mMasterNode; // 圆柱A
        BrepSceneNode mLeftHandleNode; //左边部分C
        BrepSceneNode mRightHandleNode; //右边部分D

        //缓存
        Dictionary<double, BendingInfo> mCache = new Dictionary<double, BendingInfo>();

        /// <summary>
        /// 获取当前的状态
        /// </summary>
        /// <returns>系统状态</returns>
        BendingInfo GetCurrentState()
        {
            BendingInfo info = null;
            if (mCache.TryGetValue(mAngleOffset, out info))
                return info;
            info = new BendingInfo();

            // 构造折弯部分的几何
            var startAngle = mMiddleAngle - mAngleOffset;
            var endAngle = mMiddleAngle + mAngleOffset;

            var arc = SketchBuilder.MakeArcOfCircle(mCircle, startAngle, endAngle);
            var arc2 = SketchBuilder.MakeArcOfCircle(mCircle2, startAngle, endAngle);

            var face = FeatureTool.Loft(arc, arc2, false);
            var solid = FeatureTool.Extrude(face, mHeight, GP.DZ());


            // GRepShape即缓存几何对象，又缓存显示对象
            info.BendingShape = GRepShape.Create(solid, null, null, 0.1, false);
            info.BendingShape.Build();

            mCache[mAngleOffset] = info;

            //左边的矩阵变换
            info.LeftHandleTransform = Matrix4.makeRotationAxis(Vector3.UNIT_Z, (float)startAngle);
            var dir = info.LeftHandleTransform * Vector3.UNIT_X;
            info.LeftHandleTransform = Matrix4.makeTranslation(dir * 10) * info.LeftHandleTransform * Matrix4.makeTranslation(new Vector3(0,-12,0));

            //右边的矩阵变换
            info.RightHandleTransform = Matrix4.makeRotationAxis(Vector3.UNIT_Z, (float)endAngle);
            var dir2 = info.RightHandleTransform * Vector3.UNIT_X;
            info.RightHandleTransform = Matrix4.makeTranslation(dir2 * 10) * info.RightHandleTransform;

            // 全局变换
            info.GlobalOffset = Matrix4.makeTranslation(new Vector3(0, mDistance, 0));
            info.LeftHandleTransform = info.GlobalOffset * info.LeftHandleTransform;
            info.RightHandleTransform = info.GlobalOffset * info.RightHandleTransform;

            return info;
        }

        public override void Run(IRenderView render)
        {
            // 构造初始状态
            var info = GetCurrentState();
            mBendingNode = new BrepSceneNode();
            mBendingNode.SetShape(info.BendingShape);
            render.ShowSceneNode(mBendingNode);

            var shape = ShapeBuilder.MakeCylinder(GP.XOY(), 10, 5, 0);
            mMasterNode = BrepSceneNode.Create(shape, null, null);
            render.ShowSceneNode(mMasterNode);

            var box = ShapeBuilder.MakeBox(GP.XOY(), 4, 12, mHeight);
            mLeftHandleNode = BrepSceneNode.Create(box, null, null);
            mLeftHandleNode.SetTransform(info.LeftHandleTransform);
            render.ShowSceneNode(mLeftHandleNode);

            mRightHandleNode = BrepSceneNode.Create(box, null, null);
            mRightHandleNode.SetTransform(info.RightHandleTransform);
            render.ShowSceneNode(mRightHandleNode);

            render.EnableAnimation(true);
        }
        public override void Exit(IRenderView render)
        { 
            render.EnableAnimation(false);
        }

        float nLastTime = 0;
        public override void Animation(IRenderView render, float time)
        {
            nLastTime += time;
            if (nLastTime < 100)
                return;
            nLastTime = 0;

            mDistance += 1;
            mAngleOffset += 0.1;
            if (mAngleOffset > Math.PI / 5)
            {// 若角度太大，就恢复到初始状态
                mAngleOffset = 0.1;
                mDistance = 0;
            }
                
            var info = GetCurrentState();
            mBendingNode.SetShape(info.BendingShape);
            mBendingNode.SetTransform(info.GlobalOffset);
            mBendingNode.RequestUpdate();

            mLeftHandleNode.SetTransform(info.LeftHandleTransform);
            mLeftHandleNode.RequestUpdate();

            mRightHandleNode.SetTransform(info.RightHandleTransform);
            mRightHandleNode.RequestUpdate();

            mMasterNode.SetTransform(info.GlobalOffset);
            mMasterNode.RequestUpdate();

            // 更新场景
            render.RequestDraw(EnumUpdateFlags.Scene);
        }
    }
}
