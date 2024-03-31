namespace AnyCAD.Math.Test
{
    /// <summary>
    /// GDir(三维方向向量)相关接口的单元测试集
    /// API参考文档：http://anycad.cn/api/2024/class_g_dir.html
    /// </summary>
    [TestClass]
    public class GDirTest
    {
        [TestMethod]
        public void GDir_DefaultConstructor_ShouldCreateDirectionAlongXAxis()
        {
            // Arrange
            var expectedX = 1.0;
            var expectedY = 0.0;
            var expectedZ = 0.0;

            // Act
            GDir testDirection = new GDir();

            // Assert
            Assert.AreEqual(expectedX, testDirection.X(), 1e-6);
            Assert.AreEqual(expectedY, testDirection.Y(), 1e-6);
            Assert.AreEqual(expectedZ, testDirection.Z(), 1e-6);
        }

        [TestMethod]
        public void ConstructorFromGVec_ShouldNormalizeVector()
        {
            // Arrange
            GVec testVector = new GVec(2.0, 2.0, 0.0);
            double vectorLength = System.Math.Sqrt(2.0 * 2.0 + 2.0 * 2.0 + 0.0);
            double expectedX = 2.0 / vectorLength;
            double expectedY = 2.0 / vectorLength;
            double expectedZ = 0.0 / vectorLength;

            // Act
            GDir testDirection = new GDir(testVector);

            // Assert
            Assert.AreEqual(expectedX, testDirection.X(), 1e-6);
            Assert.AreEqual(expectedY, testDirection.Y(), 1e-6);
            Assert.AreEqual(expectedZ, testDirection.Z(), 1e-6);
        }

        [TestMethod]
        public void ConstructorFromGXYZ_ShouldCreateDirectionFromCoordinates()
        {
            // Arrange
            GXYZ testCoordinates = new GXYZ(1.0, 1.0, 1.0);
            double vectorLength = System.Math.Sqrt(1.0 * 1.0 + 1.0 * 1.0 + 1.0 * 1.0);
            double expectedX = 1.0 / vectorLength;
            double expectedY = 1.0 / vectorLength;
            double expectedZ = 1.0 / vectorLength;

            // Act
            GDir testDirection = new GDir(testCoordinates);

            // Assert
            Assert.AreEqual(expectedX, testDirection.X(), 1e-6);
            Assert.AreEqual(expectedY, testDirection.Y(), 1e-6);
            Assert.AreEqual(expectedZ, testDirection.Z(), 1e-6);
        }

        [TestMethod]
        public void ConstructorFromCoordinates_ShouldCreateDirectionWithSmallCoordinates()
        {
            // Arrange
            double smallX = 0.000001;
            double smallY = 0.000001;
            double smallZ = 0.000001;
            double vectorLength = System.Math.Sqrt(smallX * smallX + smallY * smallY + smallZ * smallZ);

            // Act
            GDir testDirection = new GDir(smallX, smallY, smallZ);

            // Assert
            // 由于坐标非常小，预期单位向量的坐标也将非常接近于零，但仍需验证是否为单位向量
            Assert.IsTrue(System.Math.Abs(testDirection.X() - (smallX / vectorLength)) < 1e-6);
            Assert.IsTrue(System.Math.Abs(testDirection.Y() - (smallY / vectorLength)) < 1e-6);
            Assert.IsTrue(System.Math.Abs(testDirection.Z() - (smallZ / vectorLength)) < 1e-6);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Runtime.InteropServices.SEHException))]
        public void ConstructorFromCoordinates_NonUnitVector()
        {
            // Arrange
            double nonUnitX = -double.Epsilon; // 任何小于或等于分辨率限制的值都会导致ConstructionError
            double nonUnitY = 0.0;
            double nonUnitZ = 0.0;

            // Act
            GDir testDirection = new GDir(nonUnitX, nonUnitY, nonUnitZ);
        }

        // IsEqual 方法 - 两个相等的单位向量
        // 目的: 验证 IsEqual 方法是否能够正确识别两个方向相同的单位向量。
        [TestMethod]
        public void IsEqual_ShouldReturnTrueForIdenticalDirections()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(1.0, 0.0, 0.0);

            // Act
            bool areEqual = direction1.IsEqual(direction2, 1e-6);

            // Assert
            Assert.IsTrue(areEqual);
        }

        // IsEqual 方法 - 两个不同的单位向量
        // 目的: 验证 IsEqual 方法是否能够正确识别两个方向不同的单位向量。
        [TestMethod]
        public void IsEqual_DifferentDirections()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(0.0, 1.0, 0.0);

            // Act
            bool areEqual = direction1.IsEqual(direction2, 1e-6);

            // Assert
            Assert.IsFalse(areEqual);
        }

        // IsNormal 方法 - 两个正交的单位向量
        // 目的: 验证 IsNormal 方法是否能够正确识别两个正交（垂直）的单位向量。
        [TestMethod]
        public void IsNormal_OrthogonalDirections()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(0.0, 1.0, 0.0);

            // Act
            bool areNormal = direction1.IsNormal(direction2, 1e-6);

            // Assert
            Assert.IsTrue(areNormal);
        }

        // IsNormal 方法 - 两个非正交的单位向量
        // 目的: 验证 IsNormal 方法是否能够正确识别两个非正交（不垂直）的单位向量。
        [TestMethod]
        public void IsNormal_NonOrthogonalDirections()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(1.0, 1.0, 0.0);

            // Act
            bool areNormal = direction1.IsNormal(direction2, 1e-6);

            // Assert
            Assert.IsFalse(areNormal);
        }

        // IsOpposite 方法 - 两个相反的单位向量
        // 目的: 验证 IsOpposite 方法是否能够正确识别两个方向相反的单位向量。
        [TestMethod]
        public void IsOpposite_OppositeDirections()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(-1.0, 0.0, 0.0);

            // Act
            bool areOpposite = direction1.IsOpposite(direction2, 1e-6);

            // Assert
            Assert.IsTrue(areOpposite);
        }

        // IsOpposite 方法 - 两个非相反的单位向量
        // 目的: 验证 IsOpposite 方法是否能够正确识别两个方向不相反的单位向量。
        [TestMethod]
        public void IsOpposite_NonOppositeDirections()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(0.0, 1.0, 0.0);

            // Act
            bool areOpposite = direction1.IsOpposite(direction2, 1e-6);

            // Assert
            Assert.IsFalse(areOpposite);
        }

        // IsParallel 方法 - 两个平行的单位向量
        // 目的: 验证 IsParallel 方法是否能够正确识别两个方向相同的单位向量（平行）。
        [TestMethod]
        public void IsParallel_ParallelDirections()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(2.0, 0.0, 0.0);

            // Act
            bool areParallel = direction1.IsParallel(direction2, 1e-6);

            // Assert
            Assert.IsTrue(areParallel);
        }

        // IsParallel 方法 - 两个非平行的单位向量
        // 目的: 验证 IsParallel 方法是否能够正确识别两个方向不同的单位向量（不平行）。
        [TestMethod]
        public void IsParallel_NonParallelDirections()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(0.0, 1.0, 0.0);

            // Act
            bool areParallel = direction1.IsParallel(direction2, 1e-6);

            // Assert
            Assert.IsFalse(areParallel);
        }

        // Angle 方法 - 计算两个平行单位向量之间的角度
        // 目的: 验证 Angle 方法是否能够正确计算两个平行单位向量之间的角度，预期角度为0。
        [TestMethod]
        public void Angle_ParallelDirections()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(1.0, 0.0, 0.0);

            // Act
            double angle = direction1.Angle(direction2);

            // Assert
            Assert.AreEqual(0.0, angle, 1e-6);
        }

        // Angle 方法 - 计算两个垂直单位向量之间的角度
        // 目的: 验证 Angle 方法是否能够正确计算两个垂直单位向量之间的角度，预期角度为π/2弧度。
        [TestMethod]
        public void Angle_PerpendicularDirections()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(0.0, 1.0, 0.0);

            // Act
            double angle = direction1.Angle(direction2);

            // Assert
            Assert.AreEqual(System.Math.PI / 2, angle, 1e-6);
        }

        // AngleWithRef 方法 - 计算两个相反单位向量之间的角度，考虑参考方向
        // 目的: 验证 AngleWithRef 方法是否能够正确计算两个相反单位向量之间的角度，当参考方向与其中一个向量共线时，预期角度为π弧度。
        [TestMethod]
        public void AngleWithRef_OppositeDirectionsWithReference()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(-1.0, 0.0, 0.0);
            GDir referenceDirection = new GDir(0.0, 0.0, 1.0);

            // Act
            double angle = direction1.AngleWithRef(direction2, referenceDirection);

            // Assert
            Assert.AreEqual(System.Math.PI, angle, 1e-6);
        }

        // AngleWithRef 方法 - 计算两个共线单位向量之间的角度，考虑参考方向
        // 目的: 验证 AngleWithRef 方法是否能够正确计算两个共线单位向量之间的角度，当参考方向与其中一个向量共线时，预期角度为0。
        [TestMethod]
        public void AngleWithRef_CollinearDirectionsWithReference()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(2.0, 0.0, 0.0);
            GDir referenceDirection = new GDir(0.0, 0.0, 1.0);

            // Act
            double angle = direction1.AngleWithRef(direction2, referenceDirection);

            // Assert
            Assert.AreEqual(0.0, angle, 1e-6);
        }

        // AngleWithRef 方法 - 计算两个单位向量之间的角度，考虑参考方向
        // 目的: 验证 AngleWithRef 方法是否能够正确计算两个单位向量之间的角度，当参考方向与其中一个向量垂直时，预期角度为π/4弧度。
        [TestMethod]
        public void AngleWithRef_ReferencePerpendicularToDirection1()
        {
            // Arrange
            GDir direction1 = new GDir(1.0, 0.0, 0.0);
            GDir direction2 = new GDir(1.0, 1.0, 0.0); // 45度于X轴
            GDir referenceDirection = new GDir(0.0, 0.0, 1.0); // 垂直于direction1

            // Act
            double angle = direction1.AngleWithRef(direction2, referenceDirection);

            // Assert
            Assert.AreEqual(System.Math.PI / 4, angle, 1e-6);
        }

        // Cross 方法 - 计算两个单位向量的叉积
        // 目的: 验证 Cross 方法是否能够正确计算两个单位向量的叉积。
        [TestMethod]
        public void Cross_CalculatesCrossProduct()
        {
            // Arrange
            GDir vector1 = new GDir(1.0, 0.0, 0.0); // 沿着X轴
            GDir vector2 = new GDir(0.0, 1.0, 0.0); // 沿着Y轴
            GDir expectedCross = new GDir(0.0, 0.0, 1.0); // 预期的Z轴方向

            // Act
            vector1.Cross(vector2);

            // Assert
            Assert.AreEqual(expectedCross.X(), vector1.X(), 1e-6);
            Assert.AreEqual(expectedCross.Y(), vector1.Y(), 1e-6);
            Assert.AreEqual(expectedCross.Z(), vector1.Z(), 1e-6);
        }

        // Crossed 方法 - 计算两个单位向量的叉积并归一化
        // 目的: 验证 Crossed 方法是否能够正确计算两个单位向量的叉积并归一化。
        [TestMethod]
        public void Crossed_CrossProductNormalization()
        {
            // Arrange
            GDir vector1 = new GDir(1.0, 0.0, 0.0); // 沿着X轴
            GDir vector2 = new GDir(0.0, 1.0, 0.0); // 沿着Y轴

            // Act
            GDir crossedResult = vector1.Crossed(vector2);

            // Assert
            Assert.AreEqual(0.0, crossedResult.X(), 1e-6);
            Assert.AreEqual(0.0, crossedResult.Y(), 1e-6);
            Assert.AreEqual(1.0, crossedResult.Z(), 1e-6); // 预期的Z轴方向
        }

        // CrossCross 方法 - 计算两个向量的双重叉积
        // 目的: 验证 CrossCross 方法是否能够正确计算两个向量的双重叉积。
        [TestMethod]
        public void CrossCross_CalculatesDoubleCrossProduct()
        {
            // Arrange
            GDir vector1 = new GDir(0.0, 1.0, 0.0); // 沿着Y轴
            GDir vector2 = new GDir(0.0, 1.0, 0.0); // 沿着Y轴
            GDir vector3 = new GDir(0.0, 0.0, 1.0); // 沿着Z轴
            GDir expectedCrossCross = new GDir(0.0, 0.0, -1.0); // vector1 ^ (vector2 ^ vector3)

            // Act
            vector1.CrossCross(vector2, vector3);

            // Assert
            Assert.AreEqual(expectedCrossCross.X(), vector1.X(), 1e-6);
            Assert.AreEqual(expectedCrossCross.Y(), vector1.Y(), 1e-6);
            Assert.AreEqual(expectedCrossCross.Z(), vector1.Z(), 1e-6);
        }

        // CrossCrossed 方法 - 计算两个向量的双重叉积并归一化
        // 目的: 验证 CrossCrossed 方法是否能够正确计算两个向量的双重叉积并归一化。
        [TestMethod]
        public void CrossCrossed_TwoVectors()
        {
            // Arrange
            GDir vector1 = new GDir(0.0, 1.0, 0.0); // 沿着Y轴
            GDir vector2 = new GDir(0.0, 1.0, 0.0); // 沿着Y轴
            GDir vector3 = new GDir(0.0, 0.0, 1.0); // 沿着Z轴

            // Act
            GDir crossCrossedResult = vector1.CrossCrossed(vector2, vector3);

            // Assert
            Assert.AreEqual(0.0, crossCrossedResult.X(), 1e-6);
            Assert.AreEqual(0.0, crossCrossedResult.Y(), 1e-6);
            Assert.AreEqual(-1.0, crossCrossedResult.Z(), 1e-6); // vector1 ^ (vector2 ^ vector3)
        }

        // Dot 方法 - 计算两个单位向量的点积
        // 目的: 验证 Dot 方法是否能够正确计算两个单位向量的点积。
        [TestMethod]
        public void Dot_CalculatesDotProduct()
        {
            // Arrange
            GDir vector1 = new GDir(1.0, 0.0, 0.0); // 沿着X轴
            GDir vector2 = new GDir(0.0, 1.0, 0.0); // 沿着Y轴

            // Act
            double dotProduct = vector1.Dot(vector2);

            // Assert
            Assert.AreEqual(0.0, dotProduct, 1e-6); // 预期点积为0，因为它们垂直
        }


        // DotCross 方法 - 计算两个向量的三重点积
        // 目的: 验证 DotCross 方法是否能够正确计算两个向量的三重点积。
        [TestMethod]
        public void DotCross_CalculatesTripleProduct()
        {
            // Arrange
            GDir vector1 = new GDir(1.0, 0.0, 0.0); // 沿着X轴
            GDir vector2 = new GDir(0.0, 1.0, 0.0); // 沿着Y轴
            GDir vector3 = new GDir(0.0, 0.0, 1.0); // 沿着Z轴

            // Act
            double dotCrossProduct = vector1.DotCross(vector2, vector3);

            // Assert
            Assert.AreEqual(1.0, dotCrossProduct, 1e-6); // 预期三重点积为1
        }

        // Reverse 方法 - 反转单位向量的方向
        // 目的: 验证 Reverse 方法是否能够正确反转单位向量的方向。
        [TestMethod]
        public void Reverse_ReversesUnitVectorDirection()
        {
            // Arrange
            GDir originalDirection = new GDir(1.0, 0.0, 0.0); // 沿着X轴的单位向量

            // Act
            originalDirection.Reverse();

            // Assert
            Assert.AreEqual(-1.0, originalDirection.X(), 1e-6);
            Assert.AreEqual(0.0, originalDirection.Y(), 1e-6);
            Assert.AreEqual(0.0, originalDirection.Z(), 1e-6); // 反转后应沿相反的X轴方向
        }

        // Reversed 方法 - 获取单位向量的反向
        // 目的: 验证 Reversed 方法是否能够正确返回单位向量的反向。
        [TestMethod]
        public void Reversed_ReturnsReversedUnitVector()
        {
            // Arrange
            GDir originalDirection = new GDir(1.0, 0.0, 0.0); // 沿着X轴的单位向量

            // Act
            GDir reversedDirection = originalDirection.Reversed();

            // Assert
            Assert.AreEqual(-1.0, reversedDirection.X(), 1e-6);
            Assert.AreEqual(0.0, reversedDirection.Y(), 1e-6);
            Assert.AreEqual(0.0, reversedDirection.Z(), 1e-6); // 反转后应沿相反的X轴方向
        }

        // Mirror 方法 - 关于一个轴的镜像变换
        // 目的: 验证 Mirror 方法是否能够正确地对单位向量进行关于指定轴的镜像变换。
        [TestMethod]
        public void GDir_Mirror_ReflectsAboutAnAxis()
        {
            // Arrange
            GDir originalDirection = new GDir(0.0, 1.0, 0.0); // Y轴方向的单位向量
            GAx1 axisOfReflection = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // Z轴

            // Act
            GDir reflectedDirection = originalDirection.Mirrored(axisOfReflection);

            // Assert
            Assert.AreEqual(0.0, reflectedDirection.X(), 1e-6);
            Assert.AreEqual(-1.0, reflectedDirection.Y(), 1e-6);
            Assert.AreEqual(0.0, reflectedDirection.Z(), 1e-6); // 镜像变换后应沿Y轴的反方向
        }

        // Mirror 方法 - 关于一个平面的镜像变换
        // 目的: 验证 Mirror 方法是否能够正确地对单位向量进行关于指定平面的镜像变换。
        [TestMethod]
        public void Mirror_ReflectsUnitVectorAboutAPlane()
        {
            // Arrange
            GDir originalDirection = new GDir(0.0, 0.0, 1.0); // 沿着Z轴的单位向量
            GAx2 planeOfReflection = new GAx2(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // XY平面

            // Act
            originalDirection.Mirror(planeOfReflection);

            // Assert
            Assert.AreEqual(0.0, originalDirection.X(), 1e-6);
            Assert.AreEqual(0.0, originalDirection.Y(), 1e-6); 
            Assert.AreEqual(-1.0, originalDirection.Z(), 1e-6); // 镜像变换后应沿Z轴的反方向
        }

        // Rotated 方法 - 绕指定轴的旋转变换
        // 目的: 验证 Rotated 方法是否能够正确地返回绕指定轴旋转后的新单位向量。
        [TestMethod]
        public void GDir_Rotated_ReturnsRotatedUnitVectorAboutAnAxis()
        {
            // Arrange
            GDir originalDirection = new GDir(0.0, 1.0, 0.0); // Y轴方向的单位向量
            GAx1 axisOfRotation = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)); // Z轴
            double angleOfRotation = System.Math.PI / 4; // 45度（以弧度表示）

            // Act
            GDir rotatedDirection = originalDirection.Rotated(axisOfRotation, angleOfRotation);

            // Assert
            // 预期的旋转结果是Y轴上的单位向量绕Z轴旋转45度，新的方向应在-Y轴和Z轴之间
            double expectedX = -0.7071067811865475; // -cos(45度)
            double expectedY = 0.7071067811865475; // sin(45度)
            double expectedZ = 0.0;

            Assert.AreEqual(expectedX, rotatedDirection.X(), 1e-6);
            Assert.AreEqual(expectedY, rotatedDirection.Y(), 1e-6);
            Assert.AreEqual(expectedZ, rotatedDirection.Z(), 1e-6);
        }

        // Transformed 方法 - 应用几何变换到单位向量
        // 目的: 验证 Transformed 方法是否能够正确地应用指定的几何变换到单位向量上，并返回变换后的单位向量。
        [TestMethod]
        public void Transformed_AppliesTransformationToUnitVector()
        {
            // Arrange
            GDir originalDirection = new GDir(1.0, 0.0, 0.0); // 沿着X轴的单位向量
            GTrsf transformation = new GTrsf(); // 创建一个几何变换对象

            // 创建一个绕Z轴的旋转变换
            transformation.SetRotation(new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)), System.Math.PI / 2); // 旋转90度

            // Act
            GDir transformedDirection = originalDirection.Transformed(transformation);

            // Assert
            // 预期的变换结果是原始的X轴单位向量经过绕Z轴90度旋转后，应变为沿着Y轴的单位向量
            Assert.AreEqual(0.0, transformedDirection.X(), 1e-6);
            Assert.AreEqual(1.0, transformedDirection.Y(), 1e-6);
            Assert.AreEqual(0.0, transformedDirection.Z(), 1e-6);
        }
    }
}
