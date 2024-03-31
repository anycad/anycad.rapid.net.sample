namespace AnyCAD.Math.Test
{
    /// <summary>
    /// GAx2(右手坐标系)相关接口的单元测试集
    /// API参考文档：http://anycad.cn/api/2024/class_g_ax2.html
    /// Ax2类表示一个右手坐标系统，它在三维空间中定义了一个具有原点（也称为“位置点”）和三个正交单位向量的坐标系统
    /// 这三个单位向量分别称为“X方向”、“Y方向”和“方向”（也称为“主方向”）。主方向通常是坐标系统的“Z方向”
    /// </summary>
    [TestClass]
    public class GAx2Test
    {
        // 默认构造函数
        // 目的: 验证默认构造函数是否正确创建了一个对应于参考坐标系统(OXYZ)的GAx2对象。
        [TestMethod]
        public void DefaultConstructor()
        {
            // Arrange & Act
            GAx2 coordinateSystem = new GAx2();

            // Assert
            GPnt expectedOrigin = new GPnt(0.0, 0.0, 0.0); // 预期的原点坐标
            GDir expectedDirection = new GDir(0.0, 0.0, 1.0); // 预期的主方向为Z轴

            GPnt origin = coordinateSystem.Location();
            GDir direction = coordinateSystem.Direction();

            Assert.AreEqual(expectedOrigin.X(), origin.X(), 1e-6);
            Assert.AreEqual(expectedOrigin.Y(), origin.Y(), 1e-6);
            Assert.AreEqual(expectedOrigin.Z(), origin.Z(), 1e-6);
            Assert.AreEqual(expectedDirection.X(), direction.X(), 1e-6);
            Assert.AreEqual(expectedDirection.Y(), direction.Y(), 1e-6);
            Assert.AreEqual(expectedDirection.Z(), direction.Z(), 1e-6);
        }

        // 带三个参数的构造函数
        // 目的: 验证带三个参数的构造函数是否正确创建了一个具有给定原点和主(Z)方向、X方向的GAx2对象。
        [TestMethod]
        public void ConstructorWithOriginAndDirectionAndXDirection()
        {
            // Arrange
            GPnt expectedOrigin = new GPnt(1.0, 2.0, 3.0);
            GDir expectedDirection = new GDir(0.0, 0.0, 1.0); // 假设主方向为Z轴
            GDir expectedXDirection = new GDir(1.0, 0.0, 0.0); // 假设X方向为X轴

            // Act
            GAx2 coordinateSystem = new GAx2(expectedOrigin, expectedDirection, expectedXDirection);

            // Assert
            GPnt origin = coordinateSystem.Location();
            GDir direction = coordinateSystem.Direction();
            GDir xDirection = coordinateSystem.XDirection();

            Assert.AreEqual(expectedOrigin.X(), origin.X(), 1e-6);
            Assert.AreEqual(expectedOrigin.Y(), origin.Y(), 1e-6);
            Assert.AreEqual(expectedOrigin.Z(), origin.Z(), 1e-6);
            Assert.AreEqual(expectedDirection.X(), direction.X(), 1e-6);
            Assert.AreEqual(expectedDirection.Y(), direction.Y(), 1e-6);
            Assert.AreEqual(expectedDirection.Z(), direction.Z(), 1e-6);
            Assert.AreEqual(expectedXDirection.X(), xDirection.X(), 1e-6);
            Assert.AreEqual(expectedXDirection.Y(), xDirection.Y(), 1e-6);
            Assert.AreEqual(expectedXDirection.Z(), xDirection.Z(), 1e-6);
        }

        // 带两个参数的构造函数
        // 目的: 验证带两个参数的构造函数是否正确创建了一个具有给定原点和主(Z)方向的GAx2对象。
        [TestMethod]
        public void ConstructorWithOriginAndDirection()
        {
            // Arrange
            GPnt expectedOrigin = new GPnt(1.0, 2.0, 3.0);
            GDir expectedDirection = new GDir(1.0, 0.0, 0.0); // 假设主方向为X轴

            // Act
            GAx2 coordinateSystem = new GAx2(expectedOrigin, expectedDirection);

            // Assert
            GPnt origin = coordinateSystem.Location();
            GDir direction = coordinateSystem.Direction();

            Assert.AreEqual(expectedOrigin.X(), origin.X(), 1e-6);
            Assert.AreEqual(expectedOrigin.Y(), origin.Y(), 1e-6);
            Assert.AreEqual(expectedOrigin.Z(), origin.Z(), 1e-6);
            Assert.AreEqual(expectedDirection.X(), direction.X(), 1e-6);
            Assert.AreEqual(expectedDirection.Y(), direction.Y(), 1e-6);
            Assert.AreEqual(expectedDirection.Z(), direction.Z(), 1e-6);
        }

        // SetAxis方法
        // 目的：验证SetAxis方法是否正确设置GAx2对象的主轴和重新计算X方向和Y方向。
        [TestMethod]
        public void SetAxis_SetTheMainAxisAndRecomputeXAndYDirections()
        {
            // Arrange
            // 创建一个初始的GAx2对象，其X方向和Y方向已知
            GAx2 coordinateSystem = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0), new GDir(1.0, 0.0, 0.0)); // Z轴和X轴
                                                                                                                         // 创建一个新的GAx1对象，其方向将作为新的主轴
            GAx1 newAxis = new GAx1(new GPnt(1.0, 2.0, 3.0), new GDir(0.0, 1.0, 0.0)); // Y轴

            // Act
            coordinateSystem.SetAxis(newAxis); // 设置新的主轴并重新计算X和Y方向

            // Assert
            // 获取重新计算后的X方向和Y方向
            GDir newXDirection = coordinateSystem.XDirection();
            GDir newYDirection = coordinateSystem.YDirection();

            // 根据给定的重新计算规则，验证新的X方向是否正确
            // new "X Direction" = V1 ^ (previous "X Direction" ^ V)
            // 其中V1是newAxis的方向，previous "X Direction"是原来的X方向，V是newAxis的方向
            GDir expectedNewXDirection = newAxis.Direction().Crossed(coordinateSystem.XDirection().Crossed(coordinateSystem.Direction()));
            Assert.IsTrue(expectedNewXDirection.IsEqual(newXDirection, 1e-6), "X方向未按预期重新计算。");

            // 验证新的Y方向是否与新的主轴的X方向一致（因为新的主轴是Y轴）
            GDir expectedNewYDirection = newAxis.Direction().Crossed(expectedNewXDirection);
            Assert.IsTrue(expectedNewYDirection.IsEqual(newYDirection, 1e-6), "Y方向未正确设置为新轴的X方向。");
        }

        // SetDirection方法
        // 目的: 验证SetDirection方法是否正确改变了GAx2对象的主方向。
        [TestMethod]
        public void SetDirection_ShouldChangeMainDirection()
        {
            // Arrange
            GAx2 coordinateSystem = new GAx2(); // 创建一个默认的GAx2对象
            GDir newDirection = new GDir(0.0, 1.0, 0.0); // 新的主方向，沿Y轴

            // Act
            coordinateSystem.SetDirection(newDirection); // 改变主方向

            // Assert
            GDir actualDirection = coordinateSystem.Direction(); // 获取实际的主方向

            // 验证主方向是否已改变
            Assert.AreEqual(newDirection.X(), actualDirection.X(), "主方向的X坐标不匹配。");
            Assert.AreEqual(newDirection.Y(), actualDirection.Y(), "主方向的Y坐标不匹配。");
            Assert.AreEqual(newDirection.Z(), actualDirection.Z(), "主方向的Z坐标不匹配。");
        }

        // SetLocation方法
        // 目的: 验证SetLocation方法是否正确改变了GAx2对象的原点位置。
        [TestMethod]
        public void SetLocation_ShouldChangeTheOriginOfTheCoordinateSystem()
        {
            // Arrange
            GAx2 coordinateSystem = new GAx2(); // 创建一个默认的GAx2对象
            GPnt newLocation = new GPnt(10.0, 20.0, 30.0); // 新的原点位置

            // Act
            coordinateSystem.SetLocation(newLocation); // 改变原点位置

            // Assert
            GPnt actualLocation = coordinateSystem.Location(); // 获取实际的原点位置

            // 验证原点位置是否已改变
            Assert.AreEqual(newLocation.X(), actualLocation.X(), "原点的X坐标未正确设置。");
            Assert.AreEqual(newLocation.Y(), actualLocation.Y(), "原点的Y坐标未正确设置。");
            Assert.AreEqual(newLocation.Z(), actualLocation.Z(), "原点的Z坐标未正确设置。");
        }

        // SetXDirection 方法
        // 目的: 验证SetXDirection方法是否正确地改变了GAx2对象的X方向。同时，确保Y方向根据新的X方向和主方向进行相应的调整，且主方向保持不变。
        [TestMethod]
        public void SetXDirection_ShouldChangeTheXDirectionAndAdjustYDirectionAccordingly()
        {
            // Arrange
            GAx2 coordinateSystem = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // 主方向为Z轴
            GDir newXDirection = new GDir(-1.0, 0.0, 0.0); // 新的X方向，沿-X轴

            // Act
            coordinateSystem.SetXDirection(newXDirection); // 改变X方向

            // Assert
            GDir actualXDirection = coordinateSystem.XDirection(); // 获取实际的X方向
            GDir actualYDirection = coordinateSystem.YDirection(); // 获取实际的Y方向
            GDir actualDirection = coordinateSystem.Direction(); // 获取实际的主方向

            // 验证X方向是否已改变
            Assert.IsTrue(newXDirection.IsEqual(actualXDirection, 1e-6), "X方向未正确设置。");

            // 验证主方向是否保持不变
            Assert.IsTrue(new GDir(0.0, 0.0, 1.0).IsEqual(actualDirection, 1e-6), "主方向不应改变。");

            // 验证Y方向是否已根据新的X方向和主方向进行调整
            // 预期的Y方向应为实际主方向与新X方向的叉积
            GDir expectedYDirection = new GDir(0.0, 0.0, 1.0).Crossed(newXDirection);
            Assert.IsTrue(expectedYDirection.IsEqual(actualYDirection, 1e-6), "Y方向未按预期调整。");
        }

        // SetXDirection 方法
        // <Vx>与主方向不垂直时，能够正确地计算新的X方向
        [TestMethod]
        public void SetXDirection_ShouldChangeReComputeXDirectionAndAdjustYDirectionAccordingly()
        {
            // Arrange
            var zDirection = new GDir(0.0, 0.0, 1.0);
            GAx2 coordinateSystem = new GAx2(new GPnt(0.0, 0.0, 0.0), zDirection); // 主方向为Z轴
            GDir newXDirection = new GDir(1.0, 1.0, 1.0); // 新的X方向，不与主方向垂直

            // Act
            coordinateSystem.SetXDirection(newXDirection); // 改变X方向

            // Assert
            GDir actualXDirection = coordinateSystem.XDirection(); // 获取实际的X方向
            GDir actualYDirection = coordinateSystem.YDirection(); // 获取实际的Y方向
            GDir actualDirection = coordinateSystem.Direction(); // 获取实际的主方向

            // 验证X方向是否已重新计算：XDirection = Direction ^ (Vx ^ Direction)
            var expectedXDirection = zDirection.Crossed(newXDirection.Crossed(zDirection));
            Assert.IsTrue(expectedXDirection.IsEqual(actualXDirection, 1e-6), "X方向未正确设置。");

            // 验证主方向是否保持不变
            Assert.IsTrue(zDirection.IsEqual(actualDirection, 1e-6), "主方向不应改变。");

            // 验证Y方向是否已根据新的X方向和主方向进行调整
            // 预期的Y方向应为实际主方向与新X方向的叉积
            GDir expectedYDirection = zDirection.Crossed(newXDirection);
            Assert.IsTrue(expectedYDirection.IsEqual(actualYDirection, 1e-6), "Y方向未按预期调整。");
        }

        // SetXDirection 方法
        // 如果Vx与主方向平行，则应抛出异常。
        [TestMethod]
        [ExpectedException(typeof(System.Runtime.InteropServices.SEHException))]
        public void SetXDirection_ShouldRaiseExecptionWhenNewXDirectionParallelWithDirection()
        {
            // Arrange
            var zDirection = new GDir(0.0, 0.0, 1.0);
            GAx2 coordinateSystem = new GAx2(new GPnt(0.0, 0.0, 0.0), zDirection); // 主方向为Z轴
            GDir newXDirection = new GDir(0.0, 0.0, 1.0); // 新的X方向，与主方向平行

            // Act
            coordinateSystem.SetXDirection(newXDirection); // 改变X方向

            // Assert (异常预期在Act部分抛出)
        }

        // SetYDirection 方法
        // 目的: 验证SetYDirection方法是否正确地改变了GAx2对象的Y方向，并且在<Vy>与主方向不垂直时，能够正确地计算新的Y方向。同时，确保X方向根据新的Y方向和主方向进行相应的调整，且主方向保持不变。
        // 如果Vy与主方向平行，则应抛出Standard_ConstructionError异常。
        [TestMethod]
        [ExpectedException(typeof(System.Runtime.InteropServices.SEHException))]
        public void SetYDirection()
        {
            // Arrange (第一部分)
            var zDirection = new GDir(0.0, 0.0, 1.0);
            GAx2 coordinateSystem = new GAx2(new GPnt(0.0, 0.0, 0.0), zDirection); // 主方向为Z轴
            GDir newYDirection = new GDir(0.0, -1.0, 0.0); // 新的Y方向，沿-Y轴

            // Act
            coordinateSystem.SetYDirection(newYDirection); // 改变Y方向

            // Assert
            GDir actualXDirection = coordinateSystem.XDirection(); // 获取实际的X方向
            GDir actualYDirection = coordinateSystem.YDirection(); // 获取实际的Y方向
            GDir actualDirection = coordinateSystem.Direction(); // 获取实际的主方向

            // 验证Y方向是否已改变
            Assert.IsTrue(newYDirection.IsEqual(actualYDirection, 1e-6), "Y方向未正确设置。");

            // 验证主方向是否保持不变
            Assert.IsTrue(zDirection.IsEqual(actualDirection, 1e-6), "主方向不应改变。");

            // 验证X方向是否已根据新的Y方向和主方向进行调整
            // 预期的X方向应为新Y方向与实际主方向的叉积
            GDir expectedXDirection = newYDirection.Crossed(actualDirection);
            Assert.IsTrue(expectedXDirection.IsEqual(actualXDirection, 1e-6), "X方向未按预期调整。");

            // Arrange (第二部分)
            newYDirection = new GDir(1.0, 1.0, 1.0); // 新的Y方向，不与主方向垂直

            // Act
            coordinateSystem.SetYDirection(newYDirection); // 改变Y方向

            // Assert
            actualXDirection = coordinateSystem.XDirection(); // 获取实际的X方向
            actualYDirection = coordinateSystem.YDirection(); // 获取实际的Y方向
            actualDirection = coordinateSystem.Direction(); // 获取实际的主方向

            // 预期的Y方向
            GDir expectedYDirection = zDirection.Crossed(newYDirection.Crossed(zDirection));
            Assert.IsTrue(expectedYDirection.IsEqual(actualYDirection, 1e-6), "Y方向未正确设置。");

            // 验证X方向是否已根据新的Y方向和主方向进行调整
            // 预期的X方向应为新Y方向与实际主方向的叉积
            expectedXDirection = newYDirection.Crossed(actualDirection);
            Assert.IsTrue(expectedXDirection.IsEqual(actualXDirection, 1e-6), "X方向未按预期调整。");

            // Arrange (第三部分)
            newYDirection = new GDir(0.0, 0.0, 1.0); // 新的Y方向，与主方向平行

            // Act
            coordinateSystem.SetYDirection(newYDirection); // 改变Y方向

            // Assert (异常预期在Act部分抛出)
        }

        // Angle 方法
        // 目的: 验证Angle方法是否正确计算了两个GAx2对象之间的夹角。
        [TestMethod]
        public void Angle_ShouldCorrectlyCalculateAngleBetweenTwoCoordinateSystems()
        {
            // Arrange
            GAx2 cs1 = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // 主方向为Z轴
            GAx2 cs2 = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 1.0, 0.0)); // 主方向为Y轴

            // Act
            double angle = cs1.Angle(cs2);

            // Assert
            // 预期的夹角是90度，因为两个坐标系统共享一个原点，且它们的主方向垂直
            double expectedAngle = System.Math.PI / 2.0;
            Assert.AreEqual(expectedAngle, angle, 1e-6, "两个坐标系统之间的夹角计算不正确。");
        }

        // IsCoplanar 方法
        // 目的: 验证IsCoplanar方法是否正确判断两个GAx2对象是否共面。
        // 这里共面指的是两个GAx2对象的位置Location在主方向上重合(在给定的误差范围内)，且主方向相同(在给定的误差范围内)。
        // 即由两个坐标系的Location和Direction确定的两个平面重合
        [TestMethod]
        public void IsCoplanar_ShouldCorrectlyDetermineIfTwoCoordinateSystemsAreCoplanar()
        {
            // Arrange
            GAx2 cs1 = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // 主方向为Z轴
            GAx2 cs2 = new GAx2(new GPnt(2.0, 3.0, 1e-7), new GDir(0.0, 0.0, 1.0)); // 主方向为Z轴，位置在误差范围内重合

            // Act
            bool areCoplanar = cs1.IsCoplanar(cs2, 1e-6, 1e-10);

            // Assert
            Assert.IsTrue(areCoplanar, "两个坐标系统应共面，因为它们的主方向相同。");

            // Arrange (测试不共面的情况)
            GAx2 cs3 = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(1.0, 0.0, 0.0)); // 主方向为X轴

            // Act (测试不共面的情况)
            bool areCoplanarWithCS3 = cs1.IsCoplanar(cs3, 1e-6, 1e-10);

            // Assert (测试不共面的情况)
            Assert.IsFalse(areCoplanarWithCS3, "两个坐标系统不应共面，因为它们的主方向不同。");

            // Arrange (测试不共面的情况)
            GAx2 cs4 = new GAx2(new GPnt(0.0, 0.0, 0.1), new GDir(0.0, 0.0, 1.0)); // 主方向为X轴

            // Act (测试不共面的情况)
            bool areCoplanarWithCS4 = cs1.IsCoplanar(cs4, 1e-6, 1e-10);

            // Assert (测试不共面的情况)
            Assert.IsFalse(areCoplanarWithCS4, "两个坐标系统不应共面，因为它们的位置不同。");
        }

        // Mirrored 方法 (关于点)
        // 目的: 验证当关于一个点进行镜像变换时，是否正确地返回一个新的GAx2对象，该对象表示原始坐标系统关于该点的镜像。
        [TestMethod]
        public void Mirrored_AboutPoint_ShouldReturnCorrectMirroredCoordinateSystem()
        {
            // Arrange
            GAx2 originalCS = new GAx2(new GPnt(1.0, 2.0, 3.0), new GDir(0.0, 1.0, 0.0)); // 原始坐标系统
            GPnt mirrorPoint = new GPnt(0.0, 0.0, 0.0); // 镜像点，原点

            // Act
            GAx2 mirroredCS = originalCS.Mirrored(mirrorPoint); // 镜像变换

            // Assert
            // 验证镜像后的坐标系统的原点是否为原始坐标系统原点关于镜像点的对称点
            GPnt actualLocation = mirroredCS.Location();
            GDir actualDirection = mirroredCS.Direction();
            GDir actualXDirection = mirroredCS.XDirection();
            GDir actualYDirection = mirroredCS.YDirection();

            // 验证镜像后的坐标系X和Y方向与原坐标系反向
            GPnt expectedOrigin = originalCS.Location().Mirrored(mirrorPoint);
            Assert.IsTrue(expectedOrigin.IsEqual(actualLocation, 1e-6), "镜像后的坐标系统原点不正确。");

            GDir expectedXDirection = originalCS.XDirection().Reversed();
            Assert.IsTrue(expectedXDirection.IsEqual(actualXDirection, 1e-6), "镜像后的坐标系X方向不正确。");
            
            GDir expectedYDirection = originalCS.YDirection().Reversed();
            Assert.IsTrue(expectedYDirection.IsEqual(actualYDirection, 1e-6), "镜像后的坐标系Y方向不正确。");

            // 验证镜像后的坐标系统的方向保持不变
            Assert.IsTrue(originalCS.Direction().IsEqual(actualDirection, 1e-6), "镜像后的坐标系主方向不正确。");
        }

        // Mirrored 方法 (关于轴所表示的平面)
        // 目的: 验证当关于一个轴进行镜像变换时，是否正确地返回一个新的GAx2对象，该对象表示原始坐标系统关于该轴的镜像。
        [TestMethod]
        public void Mirrored_AboutAxis_ShouldReturnCorrectMirroredCoordinateSystem()
        {
            // Arrange
            GAx2 originalCS = new GAx2(new GPnt(1.0, 2.0, 3.0), new GDir(0.0, 1.0, 0.0)); // 原始坐标系统
            GAx1 mirrorAxis = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 1.0, 1.0)); // 镜像轴，表示XY平面

            // Act
            GAx2 mirroredCS = originalCS.Mirrored(mirrorAxis); // 镜像变换

            // Assert
            // 验证镜像后的坐标系统的原点是否为原始坐标系统原点关于镜像轴的对称点
            GPnt actualLocation = mirroredCS.Location();
            GDir actualDirection = mirroredCS.Direction();
            GDir actualXDirection = mirroredCS.XDirection();
            GDir actualYDirection = mirroredCS.YDirection();

            // 验证镜像后的坐标系X和Y方向关于mirroredCS表示的平面镜像
            GPnt expectedOrigin = originalCS.Location().Mirrored(mirrorAxis);
            Assert.IsTrue(expectedOrigin.IsEqual(actualLocation, 1e-6), "镜像后的坐标系统原点不正确。");

            GDir expectedXDirection = originalCS.XDirection().Mirrored(mirrorAxis);
            Assert.IsTrue(expectedXDirection.IsEqual(actualXDirection, 1e-6), "镜像后的坐标系X方向不正确。");

            GDir expectedYDirection = originalCS.YDirection().Mirrored(mirrorAxis);
            Assert.IsTrue(expectedYDirection.IsEqual(actualYDirection, 1e-6), "镜像后的坐标系Y方向不正确。");

            // 验证镜像后的坐标系主方向
            GDir expectedDirection = expectedXDirection.Crossed(expectedYDirection);
            Assert.IsTrue(expectedDirection.IsEqual(actualDirection, 1e-6), "镜像后的坐标系主方向不正确。");
        }

        // Mirrored 方法 (关于另一个坐标系统所表示的平面)
        // 目的: 验证当关于另一个坐标系统进行镜像变换时，是否正确地返回一个新的GAx2对象，该对象表示原始坐标系统关于另一个坐标系统的镜像。
        [TestMethod]
        public void Mirrored_AboutAnotherCoordinateSystem_ShouldReturnCorrectMirroredCoordinateSystem()
        {
            // Arrange
            GAx2 originalCS = new GAx2(new GPnt(1.0, 2.0, 3.0), new GDir(0.0, 1.0, 0.0)); // 原始坐标系统
            GAx2 mirrorCS = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // 镜像坐标系，表示XY平面

            // Act
            GAx2 mirroredCS = originalCS.Mirrored(mirrorCS); // 镜像变换

            // Assert
            // 验证镜像后的坐标系统的原点是否为原始坐标系统原点关于镜像坐标系的对称点
            GPnt actualLocation = mirroredCS.Location();
            GDir actualDirection = mirroredCS.Direction();
            GDir actualXDirection = mirroredCS.XDirection();
            GDir actualYDirection = mirroredCS.YDirection();

            // 验证镜像后的坐标系X和Y方向关于mirrorCS表示的平面镜像
            GPnt expectedOrigin = originalCS.Location().Mirrored(mirrorCS);
            Assert.IsTrue(expectedOrigin.IsEqual(actualLocation, 1e-6), "镜像后的坐标系统原点不正确。");

            GDir expectedXDirection = originalCS.XDirection().Mirrored(mirrorCS);
            Assert.IsTrue(expectedXDirection.IsEqual(actualXDirection, 1e-6), "镜像后的坐标系X方向不正确。");

            GDir expectedYDirection = originalCS.YDirection().Mirrored(mirrorCS);
            Assert.IsTrue(expectedYDirection.IsEqual(actualYDirection, 1e-6), "镜像后的坐标系Y方向不正确。");

            // 验证镜像后的坐标系主方向
            GDir expectedDirection = expectedXDirection.Crossed(expectedYDirection);
            Assert.IsTrue(expectedDirection.IsEqual(actualDirection, 1e-6), "镜像后的坐标系主方向不正确。");
        }

        // Rotated 方法
        // 目的: 验证Rotated方法是否正确地创建一个新的GAx2对象，表示原始坐标系统经过指定旋转角度后的变换。
        [TestMethod]
        public void Rotated_ShouldCreateCorrectlyRotatedCoordinateSystem()
        {
            // Arrange
            GAx2 originalCS = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // 原始坐标系统
            double rotationAngle = System.Math.PI / 4; // 旋转角度
            GAx1 rotationAxis = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // 旋转轴，Z轴

            // Act
            GAx2 rotatedCS = originalCS.Rotated(rotationAxis, rotationAngle); // 旋转后的坐标系统

            // Assert
            // 验证旋转后的坐标系统原点是否保持不变
            Assert.IsTrue(originalCS.Location().IsEqual(rotatedCS.Location(), 1e-6), "旋转后的坐标系统原点不正确。");

            // 验证旋转后的坐标系统的主方向是否已更新
            // 预期的主方向是原始主方向绕旋转轴旋转指定角度后的向量
            GDir expectedDirection = originalCS.Direction().Rotated(rotationAxis, rotationAngle);
            Assert.IsTrue(expectedDirection.IsEqual(rotatedCS.Direction(), 1e-6), "旋转后的坐标系统主方向不正确。");
        }

        // Scaled 方法
        // 目的: 验证Scaled方法是否正确地创建一个新的GAx2对象，表示原始坐标系统经过指定缩放因子后的变换。
        [TestMethod]
        public void Scaled_ShouldCreateCorrectlyScaledCoordinateSystem()
        {
            // Arrange
            GAx2 originalCS = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // 原始坐标系统
            GPnt scalePoint = new GPnt(1.0, 2.0, 3.0); // 缩放的基准点
            double scaleFactor = 2.0; // 缩放因子

            // Act
            GAx2 scaledCS = originalCS.Scaled(scalePoint, scaleFactor); // 缩放后的坐标系统

            // Assert
            // 验证缩放后的坐标系统原点是否关于给定的缩放点和缩放系数缩放
            GPnt expectedLocation = originalCS.Location().Scaled(scalePoint, scaleFactor);
            Assert.IsTrue(expectedLocation.IsEqual(scaledCS.Location(), 1e-6), "缩放后的坐标系统原点不正确。");

            // 验证缩放后的坐标系统的主方向是否不变
            GDir expectedDirection = originalCS.Direction();
            Assert.IsTrue(expectedDirection.IsEqual(scaledCS.Direction(), 1e-6), "缩放后的坐标系统主方向不正确。");
        }

        // Translated 方法
        // 目的: 验证Translated方法是否正确地创建一个新的GAx2对象，表示原始坐标系统经过指定平移向量后的变换。
        [TestMethod]
        public void Translated_ShouldCreateCorrectlyTranslatedCoordinateSystem()
        {
            // Arrange
            GAx2 originalCS = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // 原始坐标系统
            GVec translationVector = new GVec(1.0, 2.0, 3.0); // 平移向量

            // Act
            GAx2 translatedCS = originalCS.Translated(translationVector); // 平移后的坐标系统

            // Assert
            // 验证平移后的坐标系统原点是否已更新
            GPnt expectedOrigin = originalCS.Location().Translated(translationVector);
            Assert.IsTrue(expectedOrigin.IsEqual(translatedCS.Location(), 1e-6), "平移后的坐标系统原点不正确。");

            // 验证平移后的坐标系统的主方向是否保持不变
            Assert.IsTrue(originalCS.Direction().IsEqual(translatedCS.Direction(), 1e-6), "平移后的坐标系统主方向不正确。");
        }

        // Transformed 方法
        // 目的: 验证Transformed方法是否正确地创建一个新的GAx2对象，表示原始坐标系统经过组合的缩放、旋转和平移变换后的变换。
        [TestMethod]
        public void Transformed_ShouldCreateCorrectlyTransformedCoordinateSystem()
        {
            // Arrange
            GAx2 originalCS = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // 原始坐标系统
            GTrsf transformation = new GTrsf(); // 变换对象

            // 应用缩放变换
            double scaleFactor = 2.0;
            transformation.SetScaleFactor(scaleFactor);

            // 应用旋转变换
            GAx1 rotationAxis = new GAx1();
            double rotationAngle = System.Math.PI / 4; // 45度
            var quaternion = new GQuaternion(new GVec(rotationAxis.Direction()), rotationAngle);
            transformation.SetRotationPart(quaternion);

            // 应用平移变换
            GVec translationVector = new GVec(10.0, 5.0, 0.0);
            transformation.SetTranslationPart(translationVector);

            // Act
            GAx2 transformedCS = originalCS.Transformed(transformation); // 应用组合变换后的坐标系统

            // Assert
            // 验证变换后的坐标系统原点是否正确
            // 坐标系远点按缩放、旋转、平移的顺序变换
            GPnt expectedOrigin = originalCS.Location();
            expectedOrigin.Scaled(new GPnt(), scaleFactor);
            expectedOrigin.Rotate(rotationAxis, rotationAngle);
            expectedOrigin.Translate(translationVector);
            Assert.IsTrue(expectedOrigin.IsEqual(transformedCS.Location(), 1e-6), "变换后的坐标系统原点不正确。");

            // 坐标系方向只受旋转分量的影响
            // 验证变换后的坐标系统主方向是否正确
            GDir expectedDirection = originalCS.Direction().Rotated(rotationAxis, rotationAngle);
            Assert.IsTrue(expectedDirection.IsEqual(transformedCS.Direction(), 1e-6), "变换后的坐标系统主方向不正确。");

            // 验证变换后的坐标系统X方向和Y方向是否正确
            GDir expectedXDirection = originalCS.XDirection().Rotated(rotationAxis, rotationAngle);
            GDir expectedYDirection = originalCS.YDirection().Rotated(rotationAxis, rotationAngle);
            Assert.IsTrue(expectedXDirection.IsEqual(transformedCS.XDirection(), 1e-6), "变换后的坐标系统X方向不正确。");
            Assert.IsTrue(expectedYDirection.IsEqual(transformedCS.YDirection(), 1e-6), "变换后的坐标系统Y方向不正确。");
        }
    }
}
