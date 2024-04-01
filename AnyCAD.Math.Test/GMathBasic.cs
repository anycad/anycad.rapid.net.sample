namespace AnyCAD.Math.Test
{
    [TestClass]
    public class GMathBasic
    {
        /// <summary>
        /// GPnt GVec GDir的构造和基本使用
        /// </summary>
        [TestMethod]
        public void Construct()
        {
            // 1. 点
            // 构造两个点
            var pt1 = new GPnt(10, 10, 0);
            var pt2 = new GPnt(10, 10, 10);

            // 2 向量
            // 两个点构造向量：从pt1指向pt2，即 pt2 - pt1
            var vec1 = new GVec(pt1, pt2);

            Assert.IsTrue(vec1.IsEqual(new GVec(0, 0, 10), 0, 0));

            // GPnt、GVec、GDir都可以通过其XYZ()方法得到GXYZ值
            // 通过GXYZ值构造向量
            var vec2 = new GVec(pt1.XYZ());

            Assert.IsTrue(vec2.XYZ().IsEqual(pt1.XYZ(), 0));

            // 叉乘
            GVec cross = vec1.Crossed(vec2);
            // 点乘
            double dot = vec1.Dot(vec2);
            // 向量夹角，弧度
            double angle = vec1.Angle(vec2);
            // 模，长度
            double mod = vec1.Magnitude();
            // 单位化
            GVec dw = vec2.Normalized();

            // 3 方向
            // 通过向量构造方向
            var dir1 = new GDir(vec1);
            // 方向变成单位向量
            Assert.IsTrue(dir1.IsEqual(new GDir(0, 0, 1), 0));

        }

        /// <summary>
        /// Vector3d的构造和基本操作
        /// </summary>
        [TestMethod]
        public void VectorBasic()
        {
            var v1 = new Vector3d(10, 10, 0);
            var v2 = new Vector3d(10, 10, 10);

            // 相减
            var v3 = v2 - v1;
            // 相加
            var v4 = v2 + v1;
            // 叉乘
            var v5 = v1.cross(v2);
            // 点乘
            var dot = v1.dot(v3);
            // 夹角，弧度
            var angle = v1.angleTo(v4);

            // 单位化
            var d1 = v1.normalized();
        }

        /// <summary>
        /// Vector3d与GPnt/GVec/GDir的互相转换
        /// </summary>
        [TestMethod]
        public void Convert()
        {
            var v1 = new Vector3d(10, 10, 0);

            // Vector3d转换成GPnt/GVec/GDir
            var pt = new GPnt(v1);
            var v2 = new GVec(v1);
            var v3 = v1.normalized();
            var d1 = new GDir(v3);
            var xyz = new GXYZ(v1);

            // GPnt/GVec/GDir转换成Vector3d
            var w1 = new Vector3d(pt);
            var w2 = new Vector3d(v2);
            var w3 = new Vector3d(d1);
            var w4 = new Vector3d(xyz);
        }


        /// <summary>
        /// GTrsf的构造和基本使用
        /// </summary>
        public void ConstructGTrsf()
        {
            var trsf = new GTrsf();
            // 构造一个平移变换，x轴平移10个单位
            trsf.SetTranslation(new GVec(10,0,0));

            var shape = ShapeBuilder.MakeBox(new GAx2(), 10, 10, 10);
            var newShape = TransformTool.Transform(shape, trsf);


            var rot = new GTrsf();
            // 绕着Z轴旋转90度
            trsf.SetRotation(new GAx1(new GPnt(0, 0, 0), new GDir(0, 0, 1)), 90.0 / 180.0 * System.Math.PI);

            // 变换累加
            // trsf * rot
            var m = trsf.Multiplied(rot);

            // 使用四元数表示的旋转变换
            GQuaternion rotation = trsf.GetRotation();

            // 从四元数中提取欧拉角
            double u = 0, v = 0, w = 0;
            rotation.GetEulerAngles(GEulerSequence.gp_Extrinsic_XYX, ref u, ref v, ref w);
            
        }

        /// <summary>
        /// Matrix4的构造和基本使用，Matrix4d的使用方法一样
        /// </summary>
        public void ConstructMatrix4()
        {
            // 构造平移矩阵，沿着x轴移动10个单位
            var trans = Matrix4.makeTranslation(10,0,0);

            // 构造旋转矩阵，从一个方向变换到另外个方向的旋转矩阵
            var rot = Matrix4.makeRotation(new Vector3(1,0,0), new Vector3(1,1,1).normalized());

            // 绕着Z轴转45度
            var rot2 = Matrix4.makeRotationAxis(new Vector3(0, 0, 1), 45.0f / 180.0f * System.MathF.PI);

            // 先旋转，在平移
            var mat = trans * rot2;
        }

        [TestMethod]
        public void Transform()
        {
            var trsf = new GTrsf();
            // 构造一个平移变换，x轴平移10个单位
            trsf.SetTranslation(new GVec(10, 0, 0));

            var pt = new GPnt();

            // 点沿着X轴平移了10个单位
            pt.Transform(trsf);
            Assert.IsTrue(pt.IsEqual(new GPnt(10, 0, 0), 0));

            var vec = new GVec(0, 0, 1);
            // 向量沿着X轴平移了10个单位，大小不变
            vec.Transform(trsf);
            Assert.IsTrue(vec.IsEqual(new GVec(0, 0, 1), 0, 0));


            var dir = new GDir(0, 0, 1);
            // 方向沿着X轴平移N个单位，没有变化
            dir.Transform(trsf);
            Assert.IsTrue(dir.IsEqual(new GDir(0, 0, 1), 0));
        }


        public void ConstructAx()
        {
            //构造一个在指定位置和方向的轴
            var ax1 = new GAx1(new GPnt(10,10,0), new GDir(1,0,0));

            // 构造右手坐标系，指定Z和X方向
            var ax2 = new GAx2(new GPnt(10, 10, 0), new GDir(0, 0, 1), new GDir(1, 0, 0));

            // 使用右手坐标系来构造GAx3坐标系
            var ax3 = new GAx3(ax2);

            // 绕着ax1转45度
            ax3.Rotate(ax1, System.Math.PI / 4);
            // 沿着X方向移动10个单位
            ax3.Translate(new GVec(10, 0, 0));

            // 转换成变换
            //var trsf = GTrsf.From(ax3);
        }
    }
}
