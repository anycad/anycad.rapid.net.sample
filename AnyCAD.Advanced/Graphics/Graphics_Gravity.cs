using AnyCAD.Foundation;
using System;
using System.Collections.Generic;

namespace AnyCAD.Demo.Graphics
{
    /// <summary>
    /// 重力弹跳小球模拟
    /// 模拟 10 个有弹力的小球从不同高度做自由落体运动，在地面弹起、落下，直至静止
    /// </summary>
    class Graphics_Gravity : TestCase
    {
        // 小球数据结构
        class Ball
        {
           public PrimitiveSceneNode Node;          // 场景节点
           public Vector3 Position;            // 当前位置
           public Vector3 Velocity;            // 当前速度
           public float Radius;                // 半径
           public float Mass;                  // 质量
           public float Restitution;           // 弹性系数 (0-1)
            
           public Ball(PrimitiveSceneNode node, Vector3 pos, float radius, float restitution)
            {
                Node = node;
                Position = pos;
                Radius = radius;
                Velocity = new Vector3(0, 0, 0);
                Mass = radius * 0.1f; // 质量与半径成正比
                Restitution = restitution;
            }
        }
        
        List<Ball> mBalls = new List<Ball>();
        PrimitiveSceneNode mGroundNode;
        
        // 物理参数
        const float GRAVITY = -9.8f * 10;       // 重力加速度 (m/s²)，放大 10 倍以适应场景
        const float DAMPING = 0.995f;           // 空气阻尼
        const float GROUND_Z = 0;               // 地面高度（Z 方向）

        GroupSceneNode mGroup;                       // 用于分组管理所有小球
        
       public override void Run(IRenderView render)
        {
            // 创建地面
            CreateGround(render);
            
            // 创建小球组
            mGroup = new GroupSceneNode();
            render.ShowSceneNode(mGroup);
            
            // 初始化小球
            CreateBalls(render);
            
            // 启用动画
            render.EnableAnimation(true);
        }
        
        /// <summary>
        /// 创建地面
        /// </summary>
        void CreateGround(IRenderView render)
        {
            var groundMaterial = MeshPhongMaterial.Create("ground.material");
            groundMaterial.SetColor(ColorTable.LightGray);
            groundMaterial.SetFaceSide(EnumFaceSide.DoubleSide);
            
            // 创建一个平面作为地面（XY 平面，Z=0）
            var groundGeom = GeometryBuilder.CreatePlane(200, 200);
            mGroundNode = PrimitiveSceneNode.Create(groundGeom, groundMaterial);
            mGroundNode.SetTransform(Matrix4d.makeTranslation(0, 0, GROUND_Z));
            mGroundNode.SetPickable(false);
            
            render.ShowSceneNode(mGroundNode);
        }
        
        /// <summary>
        /// 创建 10 个不同高度和属性的小球
        /// </summary>
        void CreateBalls(IRenderView render)
        {
            Random random = new Random();
            
            for (int i = 0; i < 10; i++)
            {
                // 随机生成小球参数
                float radius = 3.0f + (float)random.NextDouble() * 4.0f; // 半径 3-7
                float x = -60 + (float)(i % 5) * 30 + (float)random.NextDouble() * 10; // X 方向分布
                float y = -30 + (float)random.NextDouble() * 60; // Y 方向随机
                float height = 50 + (float)i * 10 + (float)random.NextDouble() * 20; // 高度（Z 方向）50-170
                
                // 创建球体几何
                var sphereGeom = GeometryBuilder.CreateSphere(radius, 32, 32);
                
                // 创建材质（不同颜色）
                var material = MeshPhongMaterial.Create($"ball{i}.material");
                var color = GetRandomColor(i);
                material.SetColor(color);
                material.SetSpecular(new Vector3(0.3f, 0.3f, 0.3f));
                material.SetShininess(50);
                
                // 创建场景节点
                var ballNode = PrimitiveSceneNode.Create(sphereGeom, material);
                ballNode.SetPickable(false);
                
                // 设置初始位置（X, Y, Z），Z 为高度
                var position = new Vector3(x, y, height);
                ballNode.SetTransform(Matrix4d.makeTranslation(position.x, position.y, position.z));
                ballNode.RequestUpdate();
                
                // 添加到组
                mGroup.AddNode(ballNode);
                
                // 创建小球对象（弹性系数 0.6-0.85）
                float restitution = 0.6f + (float)random.NextDouble() * 0.25f;
                var ball = new Ball(ballNode, position, radius, restitution);
                mBalls.Add(ball);
            }
        }
        
        /// <summary>
        /// 根据索引生成不同的颜色
        /// </summary>
        Vector3 GetRandomColor(int index)
        {
            var colors = new Vector3[]
            {
                ColorTable.Red,
                ColorTable.Green,
                ColorTable.Blue,
                ColorTable.Yellow,
                ColorTable.Cyan,
                ColorTable.Magenta,
                ColorTable.Orange,
                ColorTable.Purple,
                ColorTable.Pink,
                ColorTable.LightBlue
            };
            
            return colors[index % colors.Length];
        }
        
        /// <summary>
        /// 更新单个小球的物理状态
        /// </summary>
        void UpdateBall(Ball ball, float deltaTime)
        {
            // 应用重力（Z 方向向下）
            ball.Velocity.z += GRAVITY * deltaTime;
            
            // 应用空气阻尼
            ball.Velocity *= DAMPING;
            
            // 更新位置
            ball.Position += ball.Velocity * deltaTime;
            
            // 检测地面碰撞（Z=0 平面）
            if (ball.Position.z - ball.Radius <= GROUND_Z)
            {
                // 修正位置到地面上
                ball.Position.z = GROUND_Z + ball.Radius;
                
                // 速度反向并衰减（弹性碰撞）
                if (Math.Abs(ball.Velocity.z) > 0.1f)
                {
                    ball.Velocity.z = -ball.Velocity.z * ball.Restitution;
                }
                else
                {
                    // 速度很小时直接设为 0，避免微小抖动
                    ball.Velocity.z = 0;
                }
            }
            
            // 更新场景节点的变换
            ball.Node.SetTransform(Matrix4d.makeTranslation(ball.Position.x, ball.Position.y, ball.Position.z));
            ball.Node.RequestUpdate();
        }
        
        /// <summary>
        /// 检查所有小球是否基本静止
        /// </summary>
        bool IsAllBallsResting()
        {
            foreach (var ball in mBalls)
            {
                // 如果任何小球的速度还比较大，或者还在空中，就返回 false
                if (Math.Abs(ball.Velocity.z) > 0.05f || ball.Position.z > ball.Radius + 0.1f)
                {
                    return false;
                }
            }
            return true;
        }
        
       public override void Animation(IRenderView render, float time)
        {
            // 时间步长（秒）
            float deltaTime = time / 1000.0f * 2.0f; // 加速 2 倍以便更快看到效果
            
            // 更新所有小球
            foreach (var ball in mBalls)
            {
                UpdateBall(ball, deltaTime);
            }
            
            // 请求重绘
            render.RequestDraw(EnumUpdateFlags.Scene);
        }
        
       public override void Exit(IRenderView render)
        {
            render.EnableAnimation(false);
        }
    }
}
