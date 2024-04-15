namespace AnyCAD.Math.Test
{
    /// <summary>
    /// GXYZ(三元数、笛卡尔坐标)相关接口的单元测试集
    /// API参考文档：http://anycad.cn/api/2024/class_g_x_y_z.html
    /// </summary>
    [TestClass]
    public class GXYZTest
    {
        // 默认构造函数
        // 目的: 验证默认构造函数是否正确创建了一个坐标为(0, 0, 0)的GXYZ对象。
        [TestMethod]
        public void DefaultConstructor_ShouldCreateOrigin()
        {
            // Arrange & Act
            GXYZ xyz = new GXYZ();

            // Assert
            Assert.AreEqual(0.0, xyz.X(), 1e-6);
            Assert.AreEqual(0.0, xyz.Y(), 1e-6);
            Assert.AreEqual(0.0, xyz.Z(), 1e-6);
        }

        // 指定坐标的构造函数
        // 目的: 验证指定坐标的构造函数是否正确创建了一个具有给定坐标的GXYZ对象。
        [TestMethod]
        public void ConstructorWithCoordinates_ShouldCreatePointWithSpecifiedCoordinates()
        {
            // Arrange
            double expectedX = 1.0;
            double expectedY = 2.0;
            double expectedZ = 3.0;

            // Act
            GXYZ xyz = new GXYZ(expectedX, expectedY, expectedZ);

            // Assert
            Assert.AreEqual(expectedX, xyz.X(), 1e-6);
            Assert.AreEqual(expectedY, xyz.Y(), 1e-6);
            Assert.AreEqual(expectedZ, xyz.Z(), 1e-6);
        }

        // Modulus 方法
        // 目的: 验证 Modulus 方法是否正确计算了 GXYZ 对象的模（长度）。
        [TestMethod]
        public void Modulus_ShouldCalculateTheLengthOfTheVector()
        {
            // Arrange
            GXYZ xyz = new GXYZ(3.0, 4.0, 0.0); // 向量在X轴和Y轴上的分量

            // Act
            double actualModulus = xyz.Modulus();

            // Assert
            double expectedModulus = System.Math.Sqrt(3.0 * 3.0 + 4.0 * 4.0); // 使用勾股定理计算预期模值
            Assert.AreEqual(expectedModulus, actualModulus, 1e-6);
        }

        // SquareModulus 方法
        // 目的: 验证 SquareModulus 方法是否正确计算了 GXYZ 对象的模的平方。
        [TestMethod]
        public void SquareModulus_ShouldCalculateTheSquareOfTheLength()
        {
            // Arrange
            GXYZ xyz = new GXYZ(3.0, 4.0, 0.0); // 向量在X轴和Y轴上的分量

            // Act
            double actualSquareModulus = xyz.SquareModulus();

            // Assert
            double expectedSquareModulus = (3.0 * 3.0 + 4.0 * 4.0); // 计算预期模的平方
            Assert.AreEqual(expectedSquareModulus, actualSquareModulus, 1e-6);
        }

        // IsEqual 方法 - 比较两个相等的点
        // 目的: 验证 IsEqual 方法是否能够正确判断两个具有相同坐标的 GXYZ 对象是否相等。
        [TestMethod]
        public void IsEqual_ShouldReturnTrueForEqualPoints()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 2.0, 3.0);
            GXYZ xyz2 = new GXYZ(1.0, 2.0, 3.0);

            // Act
            bool areEqual = xyz1.IsEqual(xyz2, 1e-6);

            // Assert
            Assert.IsTrue(areEqual);
        }

        // IsEqual 方法 - 比较两个不同的点
        // 目的: 验证 IsEqual 方法是否能够正确判断两个具有不同坐标的 GXYZ 对象是否不相等。
        [TestMethod]
        public void IsEqual_ShouldReturnFalseForDifferentPoints()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 2.0, 3.0);
            GXYZ xyz2 = new GXYZ(2.0, 3.0, 4.0);

            // Act
            bool areEqual = xyz1.IsEqual(xyz2, 1e-6);

            // Assert
            Assert.IsFalse(areEqual);
        }

        // IsEqual 方法 - 边界情况 - 比较接近的点
        // 目的: 验证 IsEqual 方法是否能够在指定容差范围内正确判断两个接近的点是否相等。
        [TestMethod]
        public void IsEqual_ShouldReturnTrueForClosePointsWithinTolerance()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 2.0, 3.0);
            GXYZ xyz2 = new GXYZ(1.000001, 2.000001, 3.000001);
            double tolerance = 1e-3;

            // Act
            bool areEqual = xyz1.IsEqual(xyz2, tolerance);

            // Assert
            Assert.IsTrue(areEqual);
        }

        // IsEqual 方法 - 边界情况 - 比较超过容差范围的点
        // 目的: 验证 IsEqual 方法是否能够在指定容差范围外正确判断两个点是否不相等。
        [TestMethod]
        public void IsEqual_ShouldReturnFalseForPointsBeyondTolerance()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 2.0, 3.0);
            GXYZ xyz2 = new GXYZ(1.00001, 2.00001, 3.00001);
            double tolerance = 1e-5;

            // Act
            bool areEqual = xyz1.IsEqual(xyz2, tolerance);

            // Assert
            Assert.IsFalse(areEqual);
        }

        // Added 方法 - 向量加法
        // 目的: 验证 Added 方法是否正确执行了两个向量的加法。
        [TestMethod]
        public void Added_ShouldPerformVectorAddition()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 2.0, 3.0);
            GXYZ xyz2 = new GXYZ(3.0, 4.0, 5.0);

            // Act
            GXYZ addedResult = xyz1.Added(xyz2);

            // Assert
            Assert.AreEqual(4.0, addedResult.X(), 1e-6);
            Assert.AreEqual(6.0, addedResult.Y(), 1e-6);
            Assert.AreEqual(8.0, addedResult.Z(), 1e-6);
        }

        // Subtracted 方法 - 向量减法
        // 目的: 验证 Subtracted 方法是否正确执行了两个向量的减法。
        [TestMethod]
        public void Subtracted_ShouldPerformVectorSubtraction()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(5.0, 6.0, 7.0);
            GXYZ xyz2 = new GXYZ(1.0, 2.0, 3.0);

            // Act
            GXYZ subtractedResult = xyz1.Subtracted(xyz2);

            // Assert
            Assert.AreEqual(4.0, subtractedResult.X(), 1e-6);
            Assert.AreEqual(4.0, subtractedResult.Y(), 1e-6);
            Assert.AreEqual(4.0, subtractedResult.Z(), 1e-6);
        }

        // Multiplied 方法 - 标量乘法
        // 目的: 验证 Multiplied 方法是否正确执行了向量与标量的乘法。
        [TestMethod]
        public void Multiplied_ShouldPerformScalarMultiplication()
        {
            // Arrange
            GXYZ xyz = new GXYZ(1.0, 2.0, 3.0);
            double scalar = 2.0;

            // Act
            GXYZ multipliedResult = xyz.Multiplied(scalar);

            // Assert
            Assert.AreEqual(2.0, multipliedResult.X(), 1e-6);
            Assert.AreEqual(4.0, multipliedResult.Y(), 1e-6);
            Assert.AreEqual(6.0, multipliedResult.Z(), 1e-6);
        }

        // Multiplied 方法 - 向量逐分量乘法
        // 目的: 验证 Multiplied 方法是否正确执行了两个向量的逐分量乘法。
        [TestMethod]
        public void Multiplied_ShouldPerformComponentwiseMultiplication()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 2.0, 3.0);
            GXYZ xyz2 = new GXYZ(4.0, 5.0, 6.0);

            // Act
            GXYZ multipliedResult = xyz1.Multiplied(xyz2);

            // Assert
            Assert.AreEqual(4.0, multipliedResult.X(), 1e-6); // 1 * 4
            Assert.AreEqual(10.0, multipliedResult.Y(), 1e-6); // 2 * 5
            Assert.AreEqual(18.0, multipliedResult.Z(), 1e-6); // 3 * 6
        }

        // Multiplied 方法 - 矩阵乘法
        // 目的: 验证 Multiplied 方法是否正确执行了向量与矩阵的乘法。
        [TestMethod]
        public void Multiplied_ShouldPerformMatrixMultiplication()
        {
            // Arrange
            GXYZ xyz = new GXYZ(1.0, 2.0, 3.0);
            GMat matrix = new GMat(
                1.0, 0.0, 3.0, // row1
                2.0, 1.0, 0.0, // row2
                0.0, 2.0, 1.0  // row3
            );

            // Act
            GXYZ multipliedResult = xyz.Multiplied(matrix); // matrix * xyz

            // Assert
            Assert.AreEqual(10.0, multipliedResult.X(), 1e-6); // (1*1 + 0*2 + 3*3)
            Assert.AreEqual(4.0, multipliedResult.Y(), 1e-6); // (2*1 + 1*2 + 0*3)
            Assert.AreEqual(7.0, multipliedResult.Z(), 1e-6); // (0*1 + 2*2 + 1*3)
        }

        // Divided 方法 - 标量除法
        // 目的: 验证 Divided 方法是否正确执行了向量与标量的除法。
        [TestMethod]
        public void Divided_ShouldPerformScalarDivision()
        {
            // Arrange
            GXYZ xyz = new GXYZ(4.0, 8.0, 12.0);
            double scalar = 2.0;

            // Act
            GXYZ dividedResult = xyz.Divided(scalar);

            // Assert
            Assert.AreEqual(2.0, dividedResult.X(), 1e-6);
            Assert.AreEqual(4.0, dividedResult.Y(), 1e-6);
            Assert.AreEqual(6.0, dividedResult.Z(), 1e-6);
        }

        // Crossed 方法 - 计算两个向量的叉积
        // 目的: 验证 Crossed 方法是否正确计算了两个向量的叉积，并返回新的向量。
        [TestMethod]
        public void Crossed_ShouldCalculateCrossProduct()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 0.0, 0.0); // 沿着X轴
            GXYZ xyz2 = new GXYZ(0.0, 1.0, 0.0); // 沿着Y轴

            // Act
            GXYZ crossProduct = xyz1.Crossed(xyz2);

            // Assert
            Assert.AreEqual(0.0, crossProduct.X(), 1e-6);
            Assert.AreEqual(0.0, crossProduct.Y(), 1e-6);
            Assert.AreEqual(1.0, crossProduct.Z(), 1e-6); // 预期的Z轴方向
        }

        // CrossMagnitude 方法 - 计算叉积的模
        // 目的: 验证 CrossMagnitude 方法是否正确计算了两个向量叉积的模。
        [TestMethod]
        public void CrossMagnitude_ShouldCalculateCrossProductMagnitude()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 0.0, 0.0); // 沿着X轴
            GXYZ xyz2 = new GXYZ(0.0, 1.0, 0.0); // 沿着Y轴

            // Act
            double crossMagnitude = xyz1.CrossMagnitude(xyz2);

            // Assert
            Assert.AreEqual(1.0, crossMagnitude, 1e-6); // 叉积的模是1
        }

        // CrossSquareMagnitude 方法 - 计算叉积模的平方
        // 目的: 验证 CrossSquareMagnitude 方法是否正确计算了两个向量叉积模的平方。
        [TestMethod]
        public void CrossSquareMagnitude_ShouldCalculateCrossProductSquareMagnitude()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 0.0, 0.0); // 沿着X轴
            GXYZ xyz2 = new GXYZ(0.0, 1.0, 0.0); // 沿着Y轴

            // Act
            double crossSquareMagnitude = xyz1.CrossSquareMagnitude(xyz2);

            // Assert
            Assert.AreEqual(1.0, crossSquareMagnitude, 1e-6); // 叉积模的平方是1
        }

        // CrossCrossed 方法 - 计算两个向量的三重叉积
        // 目的: 验证 CrossCrossed 方法是否正确计算了两个向量的三重叉积，并返回新的向量。
        [TestMethod]
        public void CrossCrossed_ShouldCalculateTripleCrossProduct()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(0.0, 1.0, 0.0); // 沿着Y轴
            GXYZ xyz2 = new GXYZ(0.0, 1.0, 0.0); // 沿着Y轴
            GXYZ xyz3 = new GXYZ(0.0, 0.0, 1.0); // 沿着Z轴

            // Act
            GXYZ crossCrossProduct = xyz1.CrossCrossed(xyz2, xyz3); // xyz1 x (xyz2 x xyz3)

            // Assert
            Assert.AreEqual(0.0, crossCrossProduct.X(), 1e-6);
            Assert.AreEqual(0.0, crossCrossProduct.Y(), 1e-6);
            Assert.AreEqual(-1.0, crossCrossProduct.Z(), 1e-6); // 预期的反Z轴方向
        }

        // GXYZ 类 - Dot 方法 - 计算两个向量的点积
        // 目的: 验证 Dot 方法是否正确计算了两个向量的点积。
        [TestMethod]
        public void Dot_ShouldCalculateDotProduct()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 2.0, 3.0);
            GXYZ xyz2 = new GXYZ(4.0, 5.0, 6.0);

            // Act
            double dotProduct = xyz1.Dot(xyz2);

            // Assert
            Assert.AreEqual(32.0, dotProduct, 1e-6); // 1*4 + 2*5 + 3*6
        }

        // GXYZ 类 - DotCross 方法 - 计算两个向量的三重点积
        // 目的: 验证 DotCross 方法是否正确计算了两个向量的三重点积。
        [TestMethod]
        public void DotCross_ShouldCalculateTripleProduct()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 0.0, 0.0); // 沿着X轴
            GXYZ xyz2 = new GXYZ(0.0, 1.0, 0.0); // 沿着Y轴
            GXYZ xyz3 = new GXYZ(0.0, 0.0, 1.0); // 沿着Z轴

            // Act
            double dotCrossProduct = xyz1.DotCross(xyz2, xyz3);

            // Assert
            Assert.AreEqual(1.0, dotCrossProduct, 1e-6); // xyz1 * (xyz2 x xyz3)
        }

        // Normalized 方法 - 计算非零向量的单位向量
        // 目的: 验证 Normalize 方法是否正确计算了非零向量的单位向量。
        [TestMethod]
        public void Normalized_ShouldCalculateUnitVectorForNonZeroVector()
        {
            // Arrange
            GXYZ xyz = new GXYZ(3.0, 4.0, 0.0);

            // Act
            GXYZ normalizedVector = xyz.Normalized();

            // Assert
            double expectedModulus = System.Math.Sqrt(3.0 * 3.0 + 4.0 * 4.0);
            Assert.AreEqual(1.0, normalizedVector.Modulus(), 1e-6, "The modulus of the normalized vector should be 1.");
            Assert.AreEqual(xyz.X() / expectedModulus, normalizedVector.X(), 1e-6);
            Assert.AreEqual(xyz.Y() / expectedModulus, normalizedVector.Y(), 1e-6);
            Assert.AreEqual(xyz.Z() / expectedModulus, normalizedVector.Z(), 1e-6);
        }

        // Normalized 方法 - 计算零向量的单位向量（边界情况）
        // 目的: 验证 Normalize 方法在处理零向量时是否抛出异常。
        [TestMethod]
        [ExpectedException(typeof(System.Runtime.InteropServices.SEHException))]
        public void Normalized_ShouldThrowExceptionForZeroVector()
        {
            // Arrange
            GXYZ zeroVector = new GXYZ(0.0, 0.0, 0.0);

            // Act
            GXYZ normalizedVector = zeroVector.Normalized();

            // Assert (异常预期在Act部分抛出)
        }

        // SetLinearForm 方法 - 设置线性线性变换后的坐标（theXYZ1 + theXYZ2）
        // 目的: 验证 SetLinearForm 方法是否正确设置线性变换后的坐标。
        [TestMethod]
        public void SetLinearForm_ShouldSetCoordinatesFromTwoVectors()
        {
            // Arrange
            GXYZ xyz1 = new GXYZ(1.0, 0.0, 0.0);
            GXYZ xyz2 = new GXYZ(0.0, 1.0, 0.0);
            GXYZ xyz = new GXYZ();

            // Act
            xyz.SetLinearForm(xyz1, xyz2);

            // Assert
            Assert.AreEqual(1.0, xyz.X(), 1e-6);
            Assert.AreEqual(1.0, xyz.Y(), 1e-6);
            Assert.AreEqual(0.0, xyz.Z(), 1e-6);
        }

        // SetLinearForm 方法 - 设置线性线性变换后的坐标（theA1 * theXYZ1 + theXYZ2）
        // 目的: 验证 SetLinearForm 方法是否正确设置线性变换后的坐标。
        [TestMethod]
        public void SetLinearForm_ShouldSetCoordinatesFromScalarAndTwoVector()
        {
            // Arrange
            double a1 = 2.0;
            GXYZ xyz1 = new GXYZ(1.0, 2.0, 3.0);
            GXYZ xyz2 = new GXYZ(2.0, 3.0, 4.0);
            GXYZ xyz = new GXYZ();

            // Act
            xyz.SetLinearForm(a1, xyz1, xyz2);

            // Assert
            Assert.AreEqual(4.0, xyz.X(), 1e-6);
            Assert.AreEqual(7.0, xyz.Y(), 1e-6);
            Assert.AreEqual(10.0, xyz.Z(), 1e-6);
        }

        // SetLinearForm 方法 - 设置线性线性变换后的坐标（theA1 * theXYZ1 + theA2 * theXYZ2 ）
        // 目的: 验证 SetLinearForm 方法是否正确设置线性变换后的坐标。
        [TestMethod]
        public void SetLinearForm_ShouldSetCoordinatesFromTwoScalarsAndTwoVectors()
        {
            // Arrange
            double a1 = 1.0;
            double a2 = 2.0;
            GXYZ xyz1 = new GXYZ(1.0, 0.0, 0.0);
            GXYZ xyz2 = new GXYZ(0.0, 1.0, 0.0);
            GXYZ xyz = new GXYZ();

            // Act
            xyz.SetLinearForm(a1, xyz1, a2, xyz2);

            // Assert
            Assert.AreEqual(1.0, xyz.X(), 1e-6);
            Assert.AreEqual(2.0, xyz.Y(), 1e-6);
            Assert.AreEqual(0.0, xyz.Z(), 1e-6);
        }

        // SetLinearForm 方法 - 设置线性线性变换后的坐标（theA1 * theXYZ1 + theA2 * theXYZ2 + theXYZ3）
        // 目的: 验证 SetLinearForm 方法是否正确设置线性变换后的坐标。
        [TestMethod]
        public void SetLinearForm_ShouldSetCoordinatesFromTwoScalarsAndThreeVectors()
        {
            // Arrange
            double a1 = 1.0;
            double a2 = 2.0;
            GXYZ xyz1 = new GXYZ(1.0, 0.0, 0.0);
            GXYZ xyz2 = new GXYZ(0.0, 1.0, 0.0);
            GXYZ xyz3 = new GXYZ(0.0, 0.0, 1.0);
            GXYZ xyz = new GXYZ();

            // Act
            xyz.SetLinearForm(a1, xyz1, a2, xyz2, xyz3);

            // Assert
            Assert.AreEqual(1.0, xyz.X(), 1e-6);
            Assert.AreEqual(2.0, xyz.Y(), 1e-6);
            Assert.AreEqual(1.0, xyz.Z(), 1e-6);
        }

        // SetLinearForm 方法 - 设置线性线性变换后的坐标（theA1 * theXYZ1 + theA2 * theXYZ2 + theA3 * theXYZ3）
        // 目的: 验证 SetLinearForm 方法是否正确设置线性变换后的坐标。
        [TestMethod]
        public void SetLinearForm_ShouldSetCoordinatesFromThreeScalarsAndThreeVectors()
        {
            // Arrange
            double a1 = 1.0;
            double a2 = 2.0;
            double a3 = 3.0;
            GXYZ xyz1 = new GXYZ(1.0, 0.0, 0.0);
            GXYZ xyz2 = new GXYZ(0.0, 1.0, 0.0);
            GXYZ xyz3 = new GXYZ(0.0, 0.0, 1.0);
            GXYZ xyz = new GXYZ();

            // Act
            xyz.SetLinearForm(a1, xyz1, a2, xyz2, a3, xyz3);

            // Assert
            Assert.AreEqual(1.0, xyz.X(), 1e-6);
            Assert.AreEqual(2.0, xyz.Y(), 1e-6);
            Assert.AreEqual(3.0, xyz.Z(), 1e-6);
        }

        // SetLinearForm 方法 - 设置线性线性变换后的坐标（theA1 * theXYZ1 + theA2 * theXYZ2 + theA3 * theXYZ3 + theXYZ4）
        // 目的: 验证 SetLinearForm 方法是否正确设置线性变换后的坐标。
        [TestMethod]
        public void SetLinearForm_ShouldSetCoordinatesFromThreeScalarsAndFourVectors()
        {
            // Arrange
            double a1 = 1.0;
            double a2 = 2.0;
            double a3 = 3.0;
            GXYZ xyz1 = new GXYZ(1.0, 0.0, 0.0);
            GXYZ xyz2 = new GXYZ(0.0, 1.0, 0.0);
            GXYZ xyz3 = new GXYZ(0.0, 0.0, 1.0);
            GXYZ xyz4 = new GXYZ(1.0, 2.0, 3.0);
            GXYZ xyz = new GXYZ();

            // Act
            xyz.SetLinearForm(a1, xyz1, a2, xyz2, a3, xyz3, xyz4);

            // Assert
            Assert.AreEqual(2.0, xyz.X(), 1e-6);
            Assert.AreEqual(4.0, xyz.Y(), 1e-6);
            Assert.AreEqual(6.0, xyz.Z(), 1e-6);
        }
    }
}
