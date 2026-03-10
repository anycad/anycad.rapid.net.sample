# AnyCAD Rapid.NET Sample - AI 开发指南

## 📚 项目概述

本项目是 AnyCAD Rapid.NET 框架的示例代码库，展示了三维建模、运动仿真和高级几何处理的核心功能。

### 技术栈
- **语言**: C# .NET (.NET 6.0/8.0/10.0)
- **UI 框架**: WinForms / WPF / Avalonia (跨平台)
- **核心库**: AnyCAD.Foundation, AnyCAD.Simulate, AnyCAD.QuickSolid
- **坐标系**: 右手坐标系，Z 轴向上

---

## 🏗️ 项目结构

```
anycad.rapid.net.sample/
├── AnyCAD.Basic/              # 基础功能示例
│   ├── Analysis/             # 几何分析（曲线、曲面、距离等）
│   ├── Geometry/             # 几何造型（布尔运算、拉伸、旋转等）
│   ├── Graphics/             # 图形显示（材质、纹理、粒子等）
│   ├── Interaction/          # 交互操作（拾取、测量等）
│   └── TestCaseLoader.cs     # 测试用例加载器
├── AnyCAD.Advanced/           # 高级功能示例
│   ├── Analysis/             # 高级分析
│   ├── Graphics/             # 高级图形（CAE、点云等）
│   ├── Interaction/          # 复杂交互
│   ├── Interop/              # 数据交换（DXF/DWG 导入）
│   ├── Simulation/           # 物理仿真（碰撞、光线等）
│   └── TestCaseLoader.cs
├── AnyCAD.QuickSolid/         # 快速建模工具
│   ├── Analysis/             # 曲率分析、孔洞检测
│   ├── Curve/                # 曲线处理
│   └── Geometry/             # 特殊造型（展开、投影等）
├── AnyCAD.WPF.App/            # WPF 应用程序入口
├── AnyCAD.WinForms.App/       # WinForms 应用程序入口
├── AnyCAD.AvaloniaApp/        # Avalonia 跨平台应用入口
└── data/                      # 资源数据文件
```

---

## 🎯 核心概念

### 1. TestCase 基类

所有示例都继承自 `TestCase` 抽象类，提供以下生命周期方法：

```csharp
class MyTestCase : TestCase
{
    // 初始化：创建场景、几何体、材质等
   public override void Run(IRenderView render)
    {
        // 在此处创建和显示几何体
    }
    
    // 动画回调（如果启用动画）
   public override void Animation(IRenderView render, float time)
    {
        // time: 毫秒，从 0 开始递增
        // 用于更新物体位置、变形等
    }
    
    // 清理资源
   public override void Exit(IRenderView render)
    {
        // 禁用动画、释放资源等
    }
    
    // 选择变化回调
   public override void OnSelectionChanged(IRenderView render, PickedResult result)
    {
        // 处理用户选择的物体
    }
}
```

### 2. 场景图结构

```
Scene
├── GroupSceneNode          # 组节点（可包含多个子节点）
├── BrepSceneNode           # 边界表示节点（精确几何体）
├── PrimitiveSceneNode      # 基本几何节点（网格、线、点）
├── ParticleSceneNode       # 粒子系统节点
└── SegmentsSceneNode       # 线段节点
```

### 3. 几何创建方式

#### 方式 1: GeometryBuilder (直接创建网格)
```csharp
// 创建球体
var sphere = GeometryBuilder.CreateSphere(radius, segmentsW, segmentsH);

// 创建平面
var plane = GeometryBuilder.CreatePlane(width, height);

// 创建线条
var line = GeometryBuilder.CreateLine(startPoint, endPoint);

// 创建点
var points = GeometryBuilder.CreatePoints(positions, colors, normals);
```

#### 方式 2: ShapeBuilder + BrepSceneNode (精确 CAD 几何)
```csharp
// 创建圆柱
var shape = ShapeBuilder.MakeCylinder(axis, radius, height, angle);

// 创建球体
var shape = ShapeBuilder.MakeSphere(center, radius);

// 创建立方体
var shape = ShapeBuilder.MakeBox(axis, length, width, height);

// 显示
var node = BrepSceneNode.Create(shape, material, edgeMaterial);
render.ShowSceneNode(node);
```

#### 方式 3: SketchBuilder (草图曲线)
```csharp
// 创建直线
var line = SketchBuilder.MakeLine(point1, point2);

// 创建圆
var circle = SketchBuilder.MakeCircle(center, radius, direction);

// 创建矩形
var rect = SketchBuilder.MakeRectangle(axis, length, width, cornerRadius, closed);

// 创建椭圆
var ellipse = SketchBuilder.MakeEllipse(center, majorRadius, minorRadius, xAxis, zAxis);
```

### 4. 材质系统

```csharp
// 基础材质（用于线框、点）
var basicMat = BasicMaterial.Create("name");
basicMat.SetColor(ColorTable.Red);
basicMat.SetLineWidth(2.0f);

// Phong 材质（光滑表面）
var phongMat = MeshPhongMaterial.Create("name");
phongMat.SetColor(ColorTable.Blue);
phongMat.SetSpecular(new Vector3(0.5f, 0.5f, 0.5f));
phongMat.SetShininess(50);

// 标准材质（PBR 渲染）
var standardMat = MeshStandardMaterial.Create("name");
standardMat.SetColor(ColorTable.Green);
standardMat.SetMetalness(0.5f);
standardMat.SetRoughness(0.5f);

// 点材质
var pointsMat = PointsMaterial.Create("name");
pointsMat.SetPointSize(10.0f);
pointsMat.SetSizeAttenuation(true);

// 虚线材质
var dashedMat = LineDashedMaterial.Create("name");
dashedMat.SetDashSize(1.0f);
dashedMat.SetGapSize(0.5f);

// 透明材质
material.SetTransparent(true);
material.SetOpacity(0.5f);

// 纹理贴图
var texture = ImageTexture2D.Create(GetResourcePath("textures/image.jpg"));
material.SetColorMap(texture);
```

---

## 📖 示例分类与模板

### 类型 1: 静态几何显示

**文件位置**: `AnyCAD.Basic/Graphics/` 或 `AnyCAD.Basic/Geometry/`

```csharp
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_MyExample: TestCase
    {
       public override void Run(IRenderView render)
        {
            // 1. 创建几何体
            var shape = ShapeBuilder.MakeCylinder(GP.XOY(), 10, 50, Math.PI * 2);
            
            // 2. 创建材质
            var material = MeshPhongMaterial.Create("my-material");
            material.SetColor(ColorTable.Blue);
            
            // 3. 创建场景节点并显示
            var node = BrepSceneNode.Create(shape, material, null);
            render.ShowSceneNode(node);
        }
    }
}
```

### 类型 2: 动画模拟

**文件位置**: `AnyCAD.Advanced/Simulation/` 或 `AnyCAD.Basic/Graphics/`

```csharp
using AnyCAD.Foundation;
using System.Collections.Generic;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_AnimationExample : TestCase
    {
        List<BrepSceneNode> mObjects = new List<BrepSceneNode>();
        
       public override void Run(IRenderView render)
        {
            // 创建初始物体
            var shape = GeometryBuilder.CreateSphere(5, 32, 32);
            var material = MeshPhongMaterial.Create("sphere.mat");
            material.SetColor(ColorTable.Red);
            
            var node = new PrimitiveSceneNode(shape, material);
            render.ShowSceneNode(node);
            mObjects.Add(node);
            
            // 启用动画
            render.EnableAnimation(true);
        }
        
       public override void Animation(IRenderView render, float time)
        {
            // time 单位：毫秒
            foreach (var obj in mObjects)
            {
                // 创建变换矩阵
                var rotation = Matrix4d.makeRotationAxis(Vector3d.UNIT_Y, time * 0.001);
                var translation = Matrix4d.makeTranslation(0, Math.Sin(time * 0.002) * 20, 0);
                
                // 应用变换
                obj.SetTransform(rotation * translation);
                obj.RequestUpdate();
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
```

### 类型 3: 物理仿真

**文件位置**: `AnyCAD.Advanced/Simulation/`

```csharp
using AnyCAD.Foundation;
using AnyCAD.Simulate;

namespace AnyCAD.Demo.Simulation
{
    class Simulation_PhysicsExample: TestCase
    {
        class PhysicsObject
        {
           public BrepSceneNode Node;
           public Vector3 Position;
           public Vector3 Velocity;
           public float Mass;
        }
        
        const float GRAVITY = -9.8f;
        List<PhysicsObject> mObjects = new List<PhysicsObject>();
        
       public override void Run(IRenderView render)
        {
            // 创建地面
            var ground = GeometryBuilder.CreatePlane(100, 100);
            var groundMat = MeshPhongMaterial.Create("ground.mat");
            groundMat.SetColor(ColorTable.LightGray);
            var groundNode = new PrimitiveSceneNode(ground, groundMat);
            render.ShowSceneNode(groundNode);
            
            // 创建下落物体
            for (int i = 0; i < 5; i++)
            {
                var shape = GeometryBuilder.CreateSphere(2 + i, 32, 32);
                var material = MeshPhongMaterial.Create($"ball{i}.mat");
                material.SetColor(new Vector3(i * 0.2f, 0.5f, 1.0f - i * 0.2f));
                
                var node = new PrimitiveSceneNode(shape, material);
                var position = new Vector3(-20 + i * 10, 50, 0);
                node.SetTransform(Matrix4d.makeTranslation(position.x, position.y, position.z));
                render.ShowSceneNode(node);
                
                mObjects.Add(new PhysicsObject
                {
                    Node = node,
                    Position = position,
                    Velocity = new Vector3(0, 0, 0),
                    Mass = 1.0f
                });
            }
            
            render.EnableAnimation(true);
        }
        
       public override void Animation(IRenderView render, float time)
        {
            float deltaTime = time / 1000.0f; // 转换为秒
            
            foreach (var obj in mObjects)
            {
                // 应用重力
                obj.Velocity.y += GRAVITY * deltaTime;
                
                // 更新位置
                obj.Position += obj.Velocity * deltaTime;
                
                // 地面碰撞检测
                if (obj.Position.y <= 0)
                {
                    obj.Position.y = 0;
                    obj.Velocity.y *= -0.5f; // 弹性碰撞
                }
                
                // 更新节点
                obj.Node.SetTransform(Matrix4d.makeTranslation(obj.Position));
                obj.Node.RequestUpdate();
            }
            
            render.RequestDraw(EnumUpdateFlags.Scene);
        }
        
       public override void Exit(IRenderView render)
        {
            render.EnableAnimation(false);
        }
    }
}
```

### 类型 4: 交互操作

**文件位置**: `AnyCAD.Advanced/Interaction/` 或 `AnyCAD.Basic/Interaction/`

```csharp
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Interaction
{
    class Interaction_MyEditor : BaseEditor
    {
       public override EnumEditorCode OnMouseDown(ViewContext ctx, InputEvent evt)
        {
            if (evt.GetButtons() == EnumMouseButton.Left)
            {
                // 获取点击位置的坐标
                var pick = ctx.SnapPoint(evt.GetX(), evt.GetY());
                var point = pick.GetPosition();
                
                // 执行操作
                DoSomething(point);
                
                return EnumEditorCode.Processed;
            }
            return base.OnMouseDown(ctx, evt);
        }
    }
    
    class Interaction_Example : TestCase
    {
       public override void Run(IRenderView render)
        {
            // 设置自定义编辑器
            var editor = new Interaction_MyEditor();
            render.SetEditor(editor);
            
            // 创建场景内容
            var shape = ShapeBuilder.MakeBox(GP.XOY(), 50, 50, 10);
            render.ShowShape(shape, ColorTable.Blue);
        }
        
       public override void Exit(IRenderView render)
        {
            render.SetEditor(null);
        }
    }
}
```

---

## 🔧 常用 API 参考

### 几何造型 (ShapeBuilder)

```csharp
// 基础几何体
ShapeBuilder.MakeBox(axis, lx, ly, lz)           // 立方体
ShapeBuilder.MakeSphere(center, radius)           // 球体
ShapeBuilder.MakeCylinder(axis, radius, height, angle)  // 圆柱
ShapeBuilder.MakeCone(axis, r1, r2, height, angle)     // 圆锥
ShapeBuilder.MakeTorus(axis, r1, r2, angle1, angle2)   // 圆环

// 扫描和放样
FeatureTool.Extrude(shape, distance, direction)   // 拉伸
FeatureTool.Revolve(shape, axis, angle)           // 旋转
FeatureTool.Loft(wire1, wire2, solid)             // 放样
FeatureTool.Sweep(profile, path, mode)            // 扫掠

// 布尔运算
BooleanTool.Unify(shape1, shape2, glue, fuse)     // 并集
BooleanTool.Cut(shape1, shape2)                   // 差集
BooleanTool.Intersect(shape1, shape2)             // 交集

// 特征操作
FeatureTool.Fillet(shape, edges, radius)          // 倒圆角
FeatureTool.Chamfer(shape, edges, distance)       // 倒角
FeatureTool.Shell(shape, faces, thickness)        // 抽壳
```

### 草图绘制 (SketchBuilder)

```csharp
// 曲线
SketchBuilder.MakeLine(p1, p2)                    // 直线
SketchBuilder.MakeCircle(center, radius, dir)     // 圆
SketchBuilder.MakeArc(p1, p2, p3)                 // 圆弧
SketchBuilder.MakeEllipse(center, r1, r2, xDir, zDir)  // 椭圆
SketchBuilder.MakeBSpline(points, weights, knots) // B 样条

// 面
SketchBuilder.MakePlanarFace(wire)                // 平面
SketchBuilder.MakeRectangle(axis, lx, ly, r, closed)  // 矩形

// 特征
SketchBuilder.MakeVertex(point)                   // 顶点
```

### 变换操作

```csharp
// 平移
Matrix4d.makeTranslation(x, y, z)
Matrix4d.makeTranslation(vector)

// 旋转
Matrix4d.makeRotationAxis(axis, angle)            // 绕轴旋转
Matrix4d.makeRotationX(angle)
Matrix4d.makeRotationY(angle)
Matrix4d.makeRotationZ(angle)

// 缩放
Matrix4d.makeScale(sx, sy, sz)
Matrix4d.makeScale(factor)                        // 均匀缩放

// 组合变换
var transform = Matrix4d.makeTranslation(10, 0, 0) 
              * Matrix4d.makeRotationZ(Math.PI / 4)
              * Matrix4d.makeScale(2);
```

### 颜色和材质

```csharp
// 预定义颜色
ColorTable.Red, ColorTable.Green, ColorTable.Blue
ColorTable.Yellow, ColorTable.Cyan, ColorTable.Magenta
ColorTable.Orange, ColorTable.Purple, ColorTable.Pink
ColorTable.LightGray, ColorTable.DarkGray
ColorTable.White, ColorTable.Black

// 自定义颜色
new Vector3(r, g, b)                              // RGB (0-1)
ColorTable.Hex(0xFF5733)                          // 十六进制

// 材质属性
material.SetColor(color)                          // 基础色
material.SetSpecular(color)                       // 高光色
material.SetShininess(value)                      // 光泽度 (0-100)
material.SetTransparent(true)                     // 透明
material.SetOpacity(0.5f)                         // 不透明度
material.SetFaceSide(EnumFaceSide.DoubleSide)     // 双面渲染
```

### 资源路径

```csharp
// 获取资源文件路径（相对于项目根目录）
string path = GetResourcePath("textures/image.jpg");
string path = GetResourcePath("models/part.step");
string path = GetResourcePath("data/file.json");
```

---

## 🎨 最佳实践

### 1. 性能优化

```csharp
// ✅ 好的做法：使用 GroupSceneNode 管理多个物体
var group = new GroupSceneNode();
for (int i = 0; i < 100; i++)
{
    var node = CreateNode(i);
    group.AddNode(node);
}
render.ShowSceneNode(group);

// ❌ 避免：创建过多独立场景节点
for (int i = 0; i < 100; i++)
{
    var node = CreateNode(i);
    render.ShowSceneNode(node);  // 降低性能
}
```

### 2. 动画优化

```csharp
// ✅ 好的做法：限制更新频率
float mLastTime = 0;
public override void Animation(IRenderView render, float time)
{
    if (time - mLastTime < 16) return;  // ~60fps
    mLastTime = time;
    
    UpdateScene();
    render.RequestDraw(EnumUpdateFlags.Scene);
}

// ✅ 使用 RequestUpdate 标记需要更新的节点
node.SetTransform(transform);
node.RequestUpdate();  // 只更新变化的节点
```

### 3. 内存管理

```csharp
// ✅ 好的做法：缓存材质和几何体
MaterialInstance mCachedMaterial;

public override void Run(IRenderView render)
{
    mCachedMaterial = MeshPhongMaterial.Create("shared.mat");
    mCachedMaterial.SetColor(ColorTable.Blue);
}

for (int i = 0; i < 100; i++)
{
    // 重复使用同一材质
    var node = CreateNode(mCachedMaterial);
}

// ❌ 避免：为每个物体创建新材质
for (int i = 0; i < 100; i++)
{
    var mat = MeshPhongMaterial.Create($"mat{i}");  // 浪费内存
    var node = CreateNode(mat);
}
```

### 4. 代码组织

```csharp
// ✅ 清晰的结构
class MyExample : TestCase
{
    // 成员变量
    private List<BrepSceneNode> mObjects = new List<BrepSceneNode>();
    private MaterialInstance mMaterial;
    
    // 辅助方法
    void CreateGround(IRenderView render) { ... }
    void CreateBalls(IRenderView render) { ... }
    void UpdatePhysics(float deltaTime) { ... }
    
    // TestCase 接口
   public override void Run(IRenderView render) { ... }
   public override void Animation(IRenderView render, float time) { ... }
   public override void Exit(IRenderView render) { ... }
}
```

---

## 📝 命名约定

### 类名
- **Graphics_***: 图形显示相关
- **Geometry_***: 几何造型相关
- **Analysis_***: 分析计算相关
- **Simulation_***: 物理仿真相关
- **Interaction_***: 交互操作相关

### 变量名
- `mVariable`: 成员变量前缀
- `camelCase`: 局部变量和方法名
- `PascalCase`: 类名和方法名（公共）

### 文件组织
- 每个示例一个独立的 `.cs` 文件
- 文件名与类名一致
- 放在对应的功能目录下

---

## 🚀 快速开始

### 创建新示例的步骤

1. **确定分类**: 根据功能选择目录（Graphics/Geometry/Simulation 等）

2. **创建文件**: 在对应目录下创建新的 `.cs` 文件

3. **继承 TestCase**: 
```csharp
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_MyExample : TestCase
    {
       public override void Run(IRenderView render)
        {
            // 你的代码
        }
    }
}
```

4. **实现功能**: 参考现有示例编写代码

5. **测试运行**: 编译并运行对应的 App 项目

6. **添加文档**: 在类上方添加 XML 注释说明功能

---

## 🔍 调试技巧

### 1. 查看坐标系
```csharp
// 显示坐标轴
var axis = ArrowWidget.Create(0.1f, 10, null);
axis.SetLocation(Vector3.Zero, Vector3.UNIT_X);
render.ShowSceneNode(axis);
```

### 2. 输出信息
```csharp
// 使用控制台输出
System.Diagnostics.Debug.WriteLine($"Value: {value}");
Console.WriteLine($"Time: {time}");
```

### 3. 可视化调试
```csharp
// 显示点
var points = GeometryBuilder.CreatePoints(positions, colors, null);
render.ShowSceneNode(new PrimitiveSceneNode(points, null));

// 显示向量
var arrow = ArrowWidget.Create(0.2f, 5, material);
arrow.SetLocation(start, direction);
render.ShowSceneNode(arrow);
```

---

## 📦 依赖和环境

### 必需依赖
- Microsoft Visual C++ Runtime Library
- .NET SDK 6.0 / 8.0 / 10.0

### 可选依赖
- Avalonia UI (用于跨平台支持)

### 构建命令
```bash
dotnet build
dotnet run --project AnyCAD.WPF.App
```

---

## 🌐 参考资料

- [入门教程](http://www.anycad.cn/guide/)
- [API 文档](http://www.anycad.cn/api/classes.html)
- [更多示例](https://gitee.com/anycad/rapid.net.starter)
- [高级示例](https://gitee.com/anycad/RapidCAX)

---

## 💡 常见问题

### Q: 如何选择合适的几何创建方式？
**A**: 
- 需要精确 CAD 几何（布尔运算、工程图）→ 使用 `ShapeBuilder` + `BrepSceneNode`
- 只需要可视化（游戏、动画）→ 使用 `GeometryBuilder` + `PrimitiveSceneNode`
- 绘制曲线草图 → 使用 `SketchBuilder`

### Q: 动画时间单位是什么？
**A**: `Animation`方法的`time`参数单位是毫秒（ms），从 0 开始递增。

### Q: 如何实现物体跟随鼠标？
**A**: 继承 `BaseEditor` 类，重写 `OnMouseMove` 方法，使用`ctx.SnapPoint(x, y)`获取 3D 坐标。

### Q: 如何优化大量物体的渲染？
**A**: 
1. 使用 `GroupSceneNode` 统一管理
2. 共享材质实例
3. 使用实例化渲染（如果支持）
4. 减少 Draw Call（合并几何体）

---

## 🎓 学习路径

### 初级
1. 阅读 `AnyCAD.Basic/Graphics/` 下的简单示例
2. 学习基本几何体创建（球、立方体、圆柱）
3. 理解材质和颜色系统
4. 掌握坐标变换

### 中级
1. 学习 `AnyCAD.Basic/Geometry/` 中的造型示例
2. 掌握布尔运算和特征操作
3. 学习动画系统 (`Animation` 方法)
4. 理解场景图结构

### 高级
1. 研究 `AnyCAD.Advanced/Simulation/` 物理仿真
2. 学习自定义 Editor 和交互
3. 掌握 Shader 编程
4. 优化性能和内存管理

---

**最后更新**: 2026-03-10  
**维护者**: AnyCAD Team
