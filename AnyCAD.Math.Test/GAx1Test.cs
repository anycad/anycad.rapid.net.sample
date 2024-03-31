namespace AnyCAD.Math.Test
{
    /// <summary>
    /// GAx1(轴)相关接口的单元测试集
    /// API参考文档：http://anycad.cn/api/2024/class_g_ax1.html
    /// 轴由两个主要部分组成：起点（也称为位置点或原点）和方向（是一个单位向量，也称为主方向）
    /// 轴在三维几何中用于多种目的，包括描述几何实体的对称轴、旋转轴，或者作为几何变换的基础，如对称变换和旋转变换
    /// </summary>
    [TestClass]
    public class GAx1Test
    {
        // 默认构造函数
        // 目的: 验证默认构造函数是否正确创建了一个坐标为(0, 0, 0)的GAx1对象。
        [TestMethod]
        public void DefaultConstructor_ShouldCreateGAx1WithOrigin()
        {
            // Arrange & Act
            GAx1 axis = new GAx1();

            // Assert
            Assert.AreEqual(0.0, axis.Location().X(), 1e-6);
            Assert.AreEqual(0.0, axis.Location().Y(), 1e-6);
            Assert.AreEqual(0.0, axis.Location().Z(), 1e-6);
            Assert.AreEqual(0.0, axis.Direction().X(), 1e-6);
            Assert.AreEqual(0.0, axis.Direction().Y(), 1e-6);
            Assert.AreEqual(1.0, axis.Direction().Z(), 1e-6); // 假设默认方向为Z轴
        }

        // 指定位置点和方向的构造函数
        // 目的: 验证指定位置点和方向的构造函数是否正确创建了一个具有给定位置点和方向的GAx1对象。
        [TestMethod]
        public void ConstructorWithLocationAndDirection_ShouldCreateGAx1WithSpecifiedValues()
        {
            // Arrange
            GPnt expectedLocation = new GPnt(1.0, 2.0, 3.0);
            GDir expectedDirection = new GDir(0.0, 1.0, 0.0); // 假设方向为Y轴

            // Act
            GAx1 axis = new GAx1(expectedLocation, expectedDirection);

            // Assert
            Assert.AreEqual(expectedLocation.X(), axis.Location().X(), 1e-6);
            Assert.AreEqual(expectedLocation.Y(), axis.Location().Y(), 1e-6);
            Assert.AreEqual(expectedLocation.Z(), axis.Location().Z(), 1e-6);
            Assert.AreEqual(expectedDirection.X(), axis.Direction().X(), 1e-6);
            Assert.AreEqual(expectedDirection.Y(), axis.Direction().Y(), 1e-6);
            Assert.AreEqual(expectedDirection.Z(), axis.Direction().Z(), 1e-6);
        }

        // SetDirection方法
        // 目的: 验证SetDirection方法是否正确设置轴线的方向。
        [TestMethod]
        public void SetDirection_ShouldSetTheDirectionOfTheGAx1()
        {
            // Arrange
            GAx1 axis = new GAx1(); // 使用默认构造函数创建轴线
            GDir newDirection = new GDir(1.0, 0.0, 0.0); // 期望设置的方向为X轴

            // Act
            axis.SetDirection(newDirection);

            // Assert
            Assert.AreEqual(newDirection.X(), axis.Direction().X(), 1e-6);
            Assert.AreEqual(newDirection.Y(), axis.Direction().Y(), 1e-6);
            Assert.AreEqual(newDirection.Z(), axis.Direction().Z(), 1e-6);
        }

        // SetLocation方法
        // 目的: 验证SetLocation方法是否正确设置轴线的位置点。
        [TestMethod]
        public void SetLocation_ShouldSetTheLocationOfTheGAx1()
        {
            // Arrange
            GAx1 axis = new GAx1(); // 使用默认构造函数创建轴线
            GPnt newLocation = new GPnt(-1.0, -2.0, -3.0); // 期望设置的位置点

            // Act
            axis.SetLocation(newLocation);

            // Assert
            Assert.AreEqual(newLocation.X(), axis.Location().X(), 1e-6);
            Assert.AreEqual(newLocation.Y(), axis.Location().Y(), 1e-6);
            Assert.AreEqual(newLocation.Z(), axis.Location().Z(), 1e-6);
        }

        // IsCoaxial方法
        // 目的: 验证IsCoaxial方法是否正确判断两条轴线是否共轴。
        [TestMethod]
        public void IsCoaxial_ShouldCorrectlyIdentifyCoaxialAxes()
        {
            // Arrange
            GAx1 axis1 = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // Z轴
            GAx1 axis2 = new GAx1(new GPnt(0.0, 0.0, 1.0), new GDir(0.0, 0.0, 1.0)); // 与axis1共轴，但位置点不同

            // Act
            bool areCoaxial = axis1.IsCoaxial(axis2, 1e-6, 1e-6);

            // Assert
            Assert.IsTrue(areCoaxial);
        }

        // IsParallel方法
        // 目的: 验证IsParallel方法是否正确判断两条轴线是否平行。
        [TestMethod]
        public void IsParallel_ShouldCorrectlyIdentifyParallelAxes()
        {
            // Arrange
            GAx1 axis1 = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 1.0, 0.0)); // Y轴
            GAx1 axis3 = new GAx1(new GPnt(1.0, 0.0, 0.0), new GDir(0.0, 1.0, 0.0)); // 与axis1平行

            // Act
            bool areParallel = axis1.IsParallel(axis3, 1e-6);

            // Assert
            Assert.IsTrue(areParallel);
        }

        // Angle方法
        // 目的: 验证Angle方法是否正确计算两条轴线之间的角度。
        [TestMethod]
        public void Angle_ShouldCorrectlyCalculateAngleBetweenAxes()
        {
            // Arrange
            GAx1 axis1 = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // Z轴
            GAx1 axis2 = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(1.0, 0.0, 0.0)); // X轴

            // Act
            double angle = axis1.Angle(axis2);

            // Assert
            Assert.AreEqual(System.Math.PI / 2.0, angle, 1e-6);
        }

        // Reverse方法
        // 目的: 验证Reverse方法是否正确反转轴线的方向。
        [TestMethod]
        public void Reverse_ShouldReverseTheDirectionOfTheAxis()
        {
            // Arrange
            GAx1 axis = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 1.0, 0.0)); // Y轴

            // Act
            axis.Reverse();

            // Assert
            GDir reversedDirection = axis.Direction();
            Assert.AreEqual(0.0, reversedDirection.X(), 1e-6);
            Assert.AreEqual(-1.0, reversedDirection.Y(), 1e-6);
            Assert.AreEqual(0.0, reversedDirection.Z(), 1e-6);
        }

        // Mirror方法
        // 目的: 验证Mirror方法是否根据给定的对称点正确镜像轴线。
        [TestMethod]
        public void Mirror_ShouldMirrorTheAxisWithRespectToAPoint()
        {
            // Arrange
            GAx1 axis = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 1.0, 0.0)); // Y轴
            GPnt mirrorPoint = new GPnt(0.0, 0.0, 1.0); // 镜像点在Z轴上

            // Act
            axis.Mirror(mirrorPoint);

            // Assert
            GDir mirroredDirection = axis.Direction();
            Assert.AreEqual(0.0, mirroredDirection.X(), 1e-6);
            Assert.AreEqual(-1.0, mirroredDirection.Y(), 1e-6);
            Assert.AreEqual(0.0, mirroredDirection.Z(), 1e-6);
        }

        // Rotate方法
        // 目的: 验证Rotate方法是否根据给定的轴和角度正确旋转轴线。
        [TestMethod]
        public void Rotate_ShouldRotateTheAxisByTheGivenAngle()
        {
            // Arrange
            GAx1 axis = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 1.0, 0.0)); // Y轴
            GAx1 rotationAxis = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // Z轴
            double angle = System.Math.PI / 4; // 旋转角度
            GDir expectedDirection = new GDir(-System.Math.Sin(angle), System.Math.Cos(angle), 0.0); // 预期的旋转后方向

            // Act
            axis.Rotate(rotationAxis, angle);

            // Assert
            GDir rotatedDirection = axis.Direction();
            Assert.AreEqual(expectedDirection.X(), rotatedDirection.X(), 1e-6);
            Assert.AreEqual(expectedDirection.Y(), rotatedDirection.Y(), 1e-6);
            Assert.AreEqual(expectedDirection.Z(), rotatedDirection.Z(), 1e-6);
        }

        // Scale方法
        // 目的: 验证Scale方法是否根据给定的中心点和缩放因子正确缩放轴线。
        [TestMethod]
        public void Scale_ShouldScaleTheAxisWithTheGivenScaleFactor()
        {
            // Arrange
            GAx1 axis = new GAx1(new GPnt(1.0, 0.0, 0.0), new GDir(0.0, 1.0, 0.0)); // Y轴，位置点为(1, 0, 0)
            GPnt scaleCenter = new GPnt(0.0, 0.0, 0.0); // 缩放中心在原点
            double scaleFactor = 2.0; // 缩放因子

            // Act
            axis.Scale(scaleCenter, scaleFactor);

            // Assert
            GPnt scaledLocation = axis.Location();
            GDir scaledDirection = axis.Direction();
            Assert.AreEqual(2.0, scaledLocation.X(), 1e-6); // 位置点X分量应缩放
            Assert.AreEqual(0.0, scaledLocation.Y(), 1e-6);
            Assert.AreEqual(0.0, scaledLocation.Z(), 1e-6);
            Assert.AreEqual(scaledDirection.X(), 0.0, 1e-6); // 方向应保持不变
            Assert.AreEqual(scaledDirection.Y(), 1.0, 1e-6);
            Assert.AreEqual(scaledDirection.Z(), 0.0, 1e-6);
        }

        // Translate方法
        // 目的: 验证Translate方法是否根据给定的向量正确平移轴线。
        [TestMethod]
        public void Translate_ShouldTranslateTheAxisByTheGivenVector()
        {
            // Arrange
            GAx1 axis = new GAx1(new GPnt(1.0, 2.0, 3.0), new GDir(0.0, 1.0, 0.0)); // 轴线沿Y轴，位置点为(1, 2, 3)
            GVec translationVector = new GVec(2.0, 3.0, 4.0); // 向量(2, 3, 4)

            // Act
            GAx1 translatedAxis = axis.Translated(translationVector);

            // Assert
            GPnt expectedLocation = translatedAxis.Location(); // 变换后的位置点
            GDir expectedDirection = translatedAxis.Direction(); // 变换后的方向

            // 计算预期的位置点
            GPnt calculatedLocation = new GPnt(axis.Location().X() + translationVector.X(),
                                         axis.Location().Y() + translationVector.Y(),
                                         axis.Location().Z() + translationVector.Z());

            Assert.AreEqual(calculatedLocation.X(), expectedLocation.X(), 1e-6);
            Assert.AreEqual(calculatedLocation.Y(), expectedLocation.Y(), 1e-6);
            Assert.AreEqual(calculatedLocation.Z(), expectedLocation.Z(), 1e-6);
            Assert.IsTrue(axis.Direction().IsEqual(expectedDirection, 1e-6)); // 方向应该保持不变
        }

        // Transform方法
        // 目的: 验证Transform方法是否根据给定的组合变换矩阵正确变换轴线。
        [TestMethod]
        public void Transform_ShouldTransformTheAxisWithTheGivenTransformation()
        {
            // Arrange
            GAx1 originalAxis = new GAx1(new GPnt(1.0, 0.0, 0.0), new GDir(0.0, 1.0, 0.0)); // 原始轴线沿Y轴，位置点为(1, 0, 0)

            GTrsf scaleTransformation = new GTrsf(); // 创建缩放变换
            double scaleFactor = 2.0;
            scaleTransformation.SetScaleFactor(scaleFactor);

            GTrsf rotationTransformation = new GTrsf(); // 创建旋转变换
            GAx1 rotationAxis = new GAx1();
            double rotationAngle = System.Math.PI / 4;
            rotationTransformation.SetRotation(rotationAxis, rotationAngle);

            GTrsf translationTransformation = new GTrsf(); // 创建平移变换
            GVec translation = new GVec(1, 2, 3);
            translationTransformation.SetTranslation(translation);

            // 组合变换
            GTrsf combinedTransformation = translationTransformation.Multiplied(rotationTransformation.Multiplied(scaleTransformation)); // T * R * S

            // Act
            GAx1 transformedAxis = originalAxis.Transformed(combinedTransformation);

            // Assert
            GPnt expectedLocation = transformedAxis.Location(); // 变换后的位置点，依次缩放、旋转、平移
            expectedLocation.Scale(new GPnt(), scaleFactor);
            expectedLocation.Rotate(rotationAxis, rotationAngle);
            expectedLocation.Translate(translation);

            GDir expectedDirection = transformedAxis.Direction(); // 变换后的方向，只需要旋转
            expectedDirection.Rotate(rotationAxis, rotationAngle);

            // 根据变换矩阵计算预期的位置点和方向
            GPnt calculatedLocation = transformedAxis.Location();
            GDir calculatedDirection = transformedAxis.Direction();

            Assert.AreEqual(calculatedLocation.X(), expectedLocation.X(), 1e-6);
            Assert.AreEqual(calculatedLocation.Y(), expectedLocation.Y(), 1e-6);
            Assert.AreEqual(calculatedLocation.Z(), expectedLocation.Z(), 1e-6);
            Assert.AreEqual(calculatedDirection.X(), expectedDirection.X(), 1e-6);
            Assert.AreEqual(calculatedDirection.Y(), expectedDirection.Y(), 1e-6);
            Assert.AreEqual(calculatedDirection.Z(), expectedDirection.Z(), 1e-6);
        }
    }
}