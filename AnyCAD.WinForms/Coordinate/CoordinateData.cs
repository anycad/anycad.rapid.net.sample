using AnyCAD.Foundation;
using System;
namespace AnyCAD.Demo.Coordinate
{

    internal class MyObject
    {
        #region Model
        // 标识
        public uint Id { get; set; } = 0;
        //物体的世界坐标系：用来保存物体的位置和角度
        public GAx2 WorldCoordinate = new GAx2();
        //局部调整矩阵，对原是模型做调整
        public Matrix4 LocalBiasMatrix = new Matrix4(1);
        #endregion

        #region VisualModel
        //展示表达
        public SceneNode VisualNode = null;
        //坐标系可视化
        public AxisWidget VisualAxis = AxisWidget.Create(1, new Vector3(5));
        #endregion

        // 展示在界面的属性
        #region ViewModel
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Z { get; set; } = 0;

        public double A { get; set; } = 0;
        public double B { get; set; } = 0;
        public double C { get; set; } = 0;
        #endregion


        public MyObject()
        {

        }

        /// <summary>
        /// 更新对象在相对坐标系下展示的数据
        /// </summary>
        /// <param name="toCoord">目标相对坐标系</param>
        public void UpdateViewModel(GAx2 toCoord)
        {
            GTrsf worldToReletive = new GTrsf();
            worldToReletive.SetTransformation(new GAx3(GP.XOY()), new GAx3(toCoord));

            // 1. 计算相对位置
            // 1.1 WorldCoordinate.Location()为对象在世界坐标系下的位置
            var xyz = WorldCoordinate.Location();
            // 1.2 变换到相对坐标系下
            xyz = xyz.Transformed(worldToReletive);
            X = xyz.X();
            Y = xyz.Y();
            Z = xyz.Z();

            //2. 计算旋转矩阵
            //2.1 计算世界坐标系下的旋转矩阵
            GTrsf t = new GTrsf();
            t.SetTransformation(new GAx3(WorldCoordinate), new GAx3(GP.XOY()));
            //2.2 旋转矩阵变换到相对坐标系
            t.Multiply(worldToReletive);
            //2.3 获取欧拉角
            var quat = t.GetRotation();
            double a =0, b = 0, c = 0;
            quat.GetEulerAngles(GEulerSequence.gp_YawPitchRoll, ref a, ref b, ref c);
            //2.4 弧度转换到角度
            A = 180 / Math.PI * a;
            B = 180 / Math.PI * b;
            C = 180 / Math.PI * c;
        }

        /// <summary>
        /// 在相对坐标系下修改模型
        /// </summary>
        /// <param name="toCoord"></param>
        public void UpdateModel(GAx2 toCoord)
        {
            // 1 计算当前的局部变换
            // 1.1 角度变换
            GQuaternion rotate = new GQuaternion();
            rotate.SetEulerAngles(GEulerSequence.gp_YawPitchRoll, A * Math.PI/180, B * Math.PI / 180, C * Math.PI / 180);
            GTrsf current = new GTrsf();
            current.SetRotationPart(rotate);
            // 1.2 位移变换
            current.SetTranslationPart(new GVec(X, Y, Z));

            // 2. 基于变换构造局部坐标系
            WorldCoordinate = new GAx2();
            WorldCoordinate.Transform(current);

            // 3. 计算相对坐标系到世界坐标系的变换
            GTrsf trf = new GTrsf();
            trf.SetTransformation(new GAx3(toCoord), new GAx3(GP.XOY()));
            // 4. 局部坐标系变换到世界坐标系
            // 模型保存的是世界坐标系
            WorldCoordinate.Transform(trf);

            // 5. 更新可视化数据
            UpdateVisual();
        }

        /// <summary>
        /// 更新可视化数据
        /// </summary>
        protected void UpdateVisual()
        {
            // 计算世界坐标系变换到绝对坐标系下的变换
            var trf = new GTrsf();
            trf.SetTransformation(new GAx3(WorldCoordinate), new GAx3());
            var mat = Matrix4.makeTransform(trf);

            // 更新坐标轴位置
            VisualAxis.SetTransform(mat);
            VisualAxis.RequstUpdate();

            // 更新可视化数据
            // LocalBiasMatrix是对原始模型的调整，这里需要乘上
            VisualNode.SetTransform(mat * LocalBiasMatrix);
            VisualNode.RequstUpdate();
        }

        /// <summary>
        /// 显示物体
        /// </summary>
        /// <param name="render"></param>
        public void Show(IRenderView render)
        {
            render.ShowSceneNode(VisualNode);
            render.ShowSceneNode(VisualAxis);
        }

    }

    internal class MyPart : MyObject
    {
        public MyPart()
        {
            const float height = 20;
            // 构造一个Box，中心点在（0，0，0）位置
            var shape = GeometryBuilder.CreateBox(5, 10, height);
            var material = MeshPhongMaterial.Create("test");
            material.SetColor(ColorTable.BrulyWood);
            VisualNode = new PrimitiveSceneNode(shape, material);
            Id = VisualNode.GetUuid();

            // 默认放置在绝对坐标系的原点处
            WorldCoordinate = new GAx2();
            X = WorldCoordinate.Location().X();
            Y = WorldCoordinate.Location().Y();
            Z = WorldCoordinate.Location().Z();

            // 调整一下模型的位置，让Box下底面中心与(X,Y,Z)对齐
            LocalBiasMatrix = Matrix4.makeTranslation(0, 0, height * 0.5f);
            
            // 更新可视化对象
            UpdateVisual();               
        }
    }

    internal class MyPlane : MyObject
    {
        public MyPlane()
        {
            // 构造个平面。平面的中心在原点
            const float length = 200, width = 200;
            var shape = GeometryBuilder.CreatePlane(length, width);
            var material = MeshPhongMaterial.Create("test");
            material.SetColor(ColorTable.Gray);
            VisualNode = new PrimitiveSceneNode(shape, material);
            Id = VisualNode.GetUuid();
            
            // 世界坐标系调整到（100，100）处
            WorldCoordinate = new GAx2(new GPnt(length * 0.5f, width * 0.5f, 0), GP.DZ());
            X = WorldCoordinate.Location().X();
            Y = WorldCoordinate.Location().Y();
            Z = WorldCoordinate.Location().Z();

            // 仍旧保持平面的中心为原点，故设置为单位阵
            LocalBiasMatrix = new Matrix4(1);

            // 更新可视化对象
            UpdateVisual();
        }
    }

}
