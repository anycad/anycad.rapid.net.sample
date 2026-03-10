using AnyCAD.Foundation;
using System;
using System.Collections.Generic;

namespace AnyCAD.Demo.Graphics
{
    /// <summary>
    /// 力导向图布局示例
    /// 使用物理模拟（弹簧力和斥力）来实现节点的自动布局
    /// </summary>
    class Graphics_ForceDiagram : TestCase
    {
        // 节点数据结构
        class ForceNode
        {
           public Vector3d Position;      // 当前位置
           public Vector3d Velocity;      // 当前速度
           public Vector3d Force;         // 累积的力
           public int Index;             // 节点索引
           public bool Fixed;            // 是否固定位置

           public ForceNode(int index, Vector3d pos)
            {
                Index = index;
                Position = pos;
                Velocity = new Vector3d(0, 0, 0);
                Force = new Vector3d(0, 0, 0);
                Fixed = false;
            }
        }

        // 边数据结构
        class ForceEdge
        {
           public int Node1;             // 起始节点索引
           public int Node2;             // 终止节点索引
           public double RestLength;     // 静止长度

           public ForceEdge(int n1, int n2, double length)
            {
                Node1 = n1;
                Node2 = n2;
                RestLength = length;
            }
        }

        List<ForceNode> mNodes = new List<ForceNode>();
        List<ForceEdge> mEdges = new List<ForceEdge>();
        
        // 物理参数
        const double SPRING_CONSTANT = 0.1;      // 弹簧弹性系数
        const double REPULSION_CONSTANT = 500;   // 斥力系数
        const double DAMPING = 0.85;             // 阻尼系数
        const double MIN_DISTANCE = 10.0;        // 最小距离
        
        GroupSceneNode mPointsGroup;
        GroupSceneNode mLinesGroup;
        PointsMaterial mPointMaterial;
        LineMaterial mLineMaterial;
       public override void Run(IRenderView render)
        {
            // 初始化节点和边
            InitializeGraph();

            // 创建点材质
            mPointMaterial = PointsMaterial.Create("force-points");
            mPointMaterial.SetSizeAttenuation(false);
            mPointMaterial.SetPointSize(15.0f);
            mPointMaterial.SetVertexColors(true);
            mPointMaterial.SetPointStyle(EnumPointStyle.Dot);

            // 创建线材质
            mLineMaterial = LineMaterial.Create("force-lines");
            mLineMaterial.SetColor(ColorTable.LightGray);
            mLineMaterial.SetVertexColors(true);
            mLineMaterial.SetLineWidth(1);
            
            // 创建场景节点组
            mPointsGroup = new GroupSceneNode();
            mLinesGroup = new GroupSceneNode();

            mPointsGroup.SetPickable(false);
            mLinesGroup.SetPickable(false);

            render.ShowSceneNode(mPointsGroup);
            render.ShowSceneNode(mLinesGroup);
            
            // 更新显示
            UpdateVisualization();
            
            // 启用动画
            render.EnableAnimation(true);
        }
        
        /// <summary>
        /// 初始化图结构 - 创建一个简单的网络拓扑
        /// </summary>
        void InitializeGraph()
        {
            mNodes.Clear();
            mEdges.Clear();
            
            Random random = new Random();
            
            // 创建节点 (随机分布在空间中)
            for (int i = 0; i < 50; i++)
            {
                float x = 100 * (float)random.NextDouble() - 50;
                float y = 100 * (float)random.NextDouble() - 50;
                float z = 100 * (float)random.NextDouble() - 50;
                
                var node = new ForceNode(i, new Vector3d(x, y, z));
                
                // 固定几个节点作为锚点
                if (i < 2)
                {
                    node.Fixed = true;
                    node.Position = new Vector3d(x, 50, z);
                }
                
                mNodes.Add(node);
            }
            
            // 创建边 (连接节点)
            for (int i = 0; i < mNodes.Count; i++)
            {
                // 每个节点连接到附近的几个节点
               int connections = 2 + random.Next(2);
                for (int j = 0; j < connections; j++)
                {
                   int target = random.Next(mNodes.Count);
                    if (target != i)
                    {
                        // 计算初始距离作为静止长度
                        var dist = (mNodes[i].Position - mNodes[target].Position).length();
                        mEdges.Add(new ForceEdge(i, target, dist));
                    }
                }
            }
        }
        
        /// <summary>
        /// 应用力导向布局算法的一步迭代
        /// </summary>
        void StepSimulation()
        {
            // 清除所有节点的力
            foreach (var node in mNodes)
            {
                if (!node.Fixed)
                {
                    node.Force = new Vector3d(0, 0, 0);
                }
            }
            
            // 1. 计算弹簧力 (沿边的吸引力)
            foreach (var edge in mEdges)
            {
                var node1 = mNodes[edge.Node1];
                var node2 = mNodes[edge.Node2];
                
                var delta = node2.Position - node1.Position;
                var distance = delta.length();
                
                if (distance < 0.01) distance = 0.01f;
                
                // 胡克定律：F = -k * x
                var displacement = distance - edge.RestLength;
                var springForce = (float)(SPRING_CONSTANT * displacement);
                
                var force = delta * (springForce / distance);
                
                if (!node1.Fixed)
                {
                    node1.Force += force;
                }
                if (!node2.Fixed)
                {
                    node2.Force -= force;
                }
            }
            
            // 2. 计算斥力 (节点之间的排斥力)
            for (int i = 0; i < mNodes.Count; i++)
            {
                for (int j = i + 1; j < mNodes.Count; j++)
                {
                    var node1 = mNodes[i];
                    var node2 = mNodes[j];
                    
                    var delta = node2.Position - node1.Position;
                    var distance = delta.length();
                    
                    if (distance < MIN_DISTANCE) distance = MIN_DISTANCE;
                    
                    // 库仑斥力：F = k / r^2
                    var repulsionForce = (float)(REPULSION_CONSTANT / (distance * distance));
                    
                    var force = delta * (repulsionForce / distance);
                    
                    if (!node1.Fixed)
                    {
                        node1.Force -= force;
                    }
                    if (!node2.Fixed)
                    {
                        node2.Force += force;
                    }
                }
            }
            
            // 3. 更新位置和速度
            foreach (var node in mNodes)
            {
                if (!node.Fixed)
                {
                    // 速度更新：v = v + F * dt
                    node.Velocity += node.Force;
                    
                    // 应用阻尼
                    node.Velocity *= (float)DAMPING;
                    
                    // 位置更新：p = p + v * dt
                    node.Position += node.Velocity;
                }
            }
        }
        
        /// <summary>
        /// 更新可视化显示
        /// </summary>
        void UpdateVisualization()
        {
            mPointsGroup.Clear();
            mLinesGroup.Clear();
            
            // 创建点
            var buffer = new Float32Buffer(0);
            var colors = new Float32Buffer(0);
            
            foreach (var node in mNodes)
            {
                buffer.Append((float)node.Position.x, (float)node.Position.y, (float)node.Position.z);
                
                // 根据节点状态设置颜色
                if (node.Fixed)
                {
                    colors.Append(0.0f, 0.5f, 1.0f); // 蓝色表示固定节点
                }
                else
                {
                    colors.Append(1.0f, 0f, 0.3f); 
                }
            }
            
            var pointsGeom = GeometryBuilder.CreatePoints(new Float32Array(buffer), new Float32Array(colors), null);
            var pointsNode = new PrimitiveSceneNode(pointsGeom, mPointMaterial);
            mPointsGroup.AddNode(pointsNode);
            
            // 创建线
            var lineBuffer = new Float32Buffer(0);
            var lineColors = new Float32Buffer(0);
            
            foreach (var edge in mEdges)
            {
                var node1 = mNodes[edge.Node1];
                var node2 = mNodes[edge.Node2];
                
                lineBuffer.Append((float)node1.Position.x, (float)node1.Position.y, (float)node1.Position.z);
                lineBuffer.Append((float)node2.Position.x, (float)node2.Position.y, (float)node2.Position.z);
                
                var color = new Vector3(0.7f, 0.7f, 0.7f);
                lineColors.Append(color.x, color.y, color.z);
                lineColors.Append(color.x, color.y, color.z);
            }
            
            var linesGeom = GeometryBuilder.CreateLines(new Float32Array(lineBuffer), null, new Float32Array(lineColors));
            var linesNode = new PrimitiveSceneNode(linesGeom, mLineMaterial);
            mLinesGroup.AddNode(linesNode);
        }
        
       public override void Animation(IRenderView render, float time)
        {
            // 执行多次迭代以获得更稳定的结果
            for (int i = 0; i < 3; i++)
            {
                StepSimulation();
            }

            
            UpdateVisualization();
            
            // 请求重绘
            render.RequestDraw(EnumUpdateFlags.Scene);
        }
        
       public override void Exit(IRenderView render)
        {
            render.EnableAnimation(false);
        }
    }
}
