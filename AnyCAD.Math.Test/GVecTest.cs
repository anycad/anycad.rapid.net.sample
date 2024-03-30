
namespace AnyCAD.Math.Test
{
    [TestClass]
    public class GVecTest
    {
        [TestMethod]
        public void DefaultConstructor_ReturnsZeroVector()
        {
            // Arrange & Act
            GVec vec = new GVec();

            // Assert
            Assert.AreEqual(0.0, vec.X(), 1e-6);
            Assert.AreEqual(0.0, vec.Y(), 1e-6);
            Assert.AreEqual(0.0, vec.Z(), 1e-6);
        }

        [TestMethod]
        public void ConstructorFromGDir_ReturnsUnitVector()
        {
            // Arrange
            GDir dir = new GDir(1.0, 0.0, 0.0); // Assuming GDir(1, 0, 0) is a valid direction

            // Act
            GVec vec = new GVec(dir);

            // Assert
            Assert.AreEqual(1.0, vec.X(), 1e-6);
            Assert.AreEqual(0.0, vec.Y(), 1e-6);
            Assert.AreEqual(0.0, vec.Z(), 1e-6);
        }

        [TestMethod]
        public void ConstructorFromGXYZ_ReturnsVectorWithGivenCoordinates()
        {
            // Arrange
            GXYZ coord = new GXYZ(2.0, 3.0, 4.0);

            // Act
            GVec vec = new GVec(coord);

            // Assert
            Assert.AreEqual(2.0, vec.X(), 1e-6);
            Assert.AreEqual(3.0, vec.Y(), 1e-6);
            Assert.AreEqual(4.0, vec.Z(), 1e-6);
        }

        [TestMethod]
        public void ConstructorFromTwoPoints_ReturnsVectorWithCorrectLength()
        {
            // Arrange
            GPnt p1 = new GPnt(0.0, 0.0, 0.0);
            GPnt p2 = new GPnt(1.0, 2.0, 3.0);

            // Act
            GVec vec = new GVec(p1, p2);

            // Assert
            double expectedLength = System.Math.Sqrt(1.0 * 1.0 + 2.0 * 2.0 + 3.0 * 3.0);
            Assert.AreEqual(expectedLength, vec.Magnitude(), 1e-6);
        }

        [TestMethod]
        public void SetCoord_UpdatesCoordinatesCorrectly()
        {
            // Arrange
            GVec vec = new GVec();
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;

            // Act
            vec.SetCoord(x, y, z);

            // Assert
            Assert.AreEqual(x, vec.X(), 1e-6);
            Assert.AreEqual(y, vec.Y(), 1e-6);
            Assert.AreEqual(z, vec.Z(), 1e-6);
        }

        [TestMethod]
        public void SetX_UpdatesXCoordinate()
        {
            // Arrange
            GVec vec = new GVec();
            double newX = 10.0;

            // Act
            vec.SetX(newX);

            // Assert
            Assert.AreEqual(newX, vec.X(), 1e-6);
        }

        [TestMethod]
        public void SetY_UpdatesYCoordinate()
        {
            // Arrange
            GVec vec = new GVec();
            double newY = 20.0;

            // Act
            vec.SetY(newY);

            // Assert
            Assert.AreEqual(newY, vec.Y(), 1e-6);
        }

        [TestMethod]
        public void SetZ_UpdatesZCoordinate()
        {
            // Arrange
            GVec vec = new GVec();
            double newZ = 30.0;

            // Act
            vec.SetZ(newZ);

            // Assert
            Assert.AreEqual(newZ, vec.Z(), 1e-6);
        }

        [TestMethod]
        public void Added_ReturnsSumOfTwoVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(0.0, 1.0, 0.0);

            // Act
            GVec sum = vec1.Added(vec2);

            // Assert
            Assert.AreEqual(1.0, sum.X(), 1e-6);
            Assert.AreEqual(1.0, sum.Y(), 1e-6);
            Assert.AreEqual(0.0, sum.Z(), 1e-6);
        }

        [TestMethod]
        public void Subtracted_ReturnsDifferenceOfTwoVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(0.0, 1.0, 0.0);

            // Act
            GVec difference = vec1.Subtracted(vec2);

            // Assert
            Assert.AreEqual(1.0, difference.X(), 1e-6);
            Assert.AreEqual(-1.0, difference.Y(), 1e-6);
            Assert.AreEqual(0.0, difference.Z(), 1e-6);
        }

        [TestMethod]
        public void Multiplied_ReturnsScaledVector()
        {
            // Arrange
            GVec vec = new GVec(1.0, 2.0, 3.0);
            double scalar = 2.0;

            // Act
            GVec scaled = vec.Multiplied(scalar);

            // Assert
            Assert.AreEqual(2.0, scaled.X(), 1e-6);
            Assert.AreEqual(4.0, scaled.Y(), 1e-6);
            Assert.AreEqual(6.0, scaled.Z(), 1e-6);
        }

        [TestMethod]
        public void Divided_ReturnsScaledVector()
        {
            // Arrange
            GVec vec = new GVec(2.0, 4.0, 6.0);
            double scalar = 2.0;

            // Act
            GVec scaled = vec.Divided(scalar);

            // Assert
            Assert.AreEqual(1.0, scaled.X(), 1e-6);
            Assert.AreEqual(2.0, scaled.Y(), 1e-6);
            Assert.AreEqual(3.0, scaled.Z(), 1e-6);
        }

        [TestMethod]
        public void Dot_ReturnsCorrectDotProduct()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 2.0, 3.0);
            GVec vec2 = new GVec(4.0, 5.0, 6.0);

            // Act
            double dotProduct = vec1.Dot(vec2);

            // Assert
            Assert.AreEqual(1.0 * 4.0 + 2.0 * 5.0 + 3.0 * 6.0, dotProduct, 1e-6);
        }

        [TestMethod]
        public void Crossed_ReturnsCorrectCrossedProduct()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(0.0, 1.0, 0.0);

            // Act
            GVec CrossedProduct = vec1.Crossed(vec2);

            // Assert
            Assert.AreEqual(0.0, CrossedProduct.X(), 1e-6);
            Assert.AreEqual(0.0, CrossedProduct.Y(), 1e-6);
            Assert.AreEqual(1.0, CrossedProduct.Z(), 1e-6);
        }

        [TestMethod]
        public void Normalized_ReturnsUnitVector()
        {
            // Arrange
            GVec vec = new GVec(2.0, 0.0, 0.0);

            // Act
            GVec Normalizedd = vec.Normalized();

            // Assert
            Assert.AreEqual(1.0, Normalizedd.X(), 1e-6);
            Assert.AreEqual(0.0, Normalizedd.Y(), 1e-6);
            Assert.AreEqual(0.0, Normalizedd.Z(), 1e-6);
        }

        [TestMethod]
        public void Magnitude_ReturnsCorrectMagnitude()
        {
            // Arrange
            GVec vec = new GVec(1.0, 2.0, 3.0);

            // Act
            double magnitude = vec.Magnitude();

            // Assert
            Assert.AreEqual(System.Math.Sqrt(1.0 * 1.0 + 2.0 * 2.0 + 3.0 * 3.0), magnitude, 1e-6);
        }

        [TestMethod]
        public void IsEqual_ReturnsTrueForEqualVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 2.0, 3.0);
            GVec vec2 = new GVec(1.0, 2.0, 3.0);

            // Act & Assert
            Assert.IsTrue(vec1.IsEqual(vec2, 0.0, 0.0));
        }

        [TestMethod]
        public void IsEqual_ReturnsFalseForVectorsWithDifferentMagnitudes()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 2.0, 3.0);
            GVec vec2 = new GVec(1.0, 2.0, 4.0);

            // Act & Assert
            Assert.IsFalse(vec1.IsEqual(vec2, 0.0, 0.0));
        }

        [TestMethod]
        public void IsEqual_ReturnsFalseForVectorsWithDifferentDirections()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(-1.0, 0.0, 0.0);

            // Act & Assert
            Assert.IsFalse(vec1.IsEqual(vec2, 0.0, 0.0));
        }

        [TestMethod]
        public void IsEqual_ReturnsFalseForVectorsWithDifferentComponents()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 1.0, 0.0);
            GVec vec2 = new GVec(1.0, 1.0, 1.0);

            // Act & Assert
            Assert.IsFalse(vec1.IsEqual(vec2, 0.0, 0.0));
        }

        [TestMethod]
        public void IsNormal_ReturnsTrueForOrthogonalVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(0.0, 1.0, 0.0);

            // Act & Assert
            Assert.IsTrue(vec1.IsNormal(vec2, 0.0));
        }

        [TestMethod]
        public void IsNormal_ReturnsFalseForParallelVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(2.0, 0.0, 0.0);

            // Act & Assert
            Assert.IsFalse(vec1.IsNormal(vec2, 0.0));
        }

        [TestMethod]
        public void IsOpposite_ReturnsTrueForAntiparallelVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(-1.0, 0.0, 0.0);

            // Act & Assert
            Assert.IsTrue(vec1.IsOpposite(vec2, 0.0));
        }

        [TestMethod]
        public void IsParallel_ReturnsTrueForParallelVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(2.0, 0.0, 0.0);

            // Act & Assert
            Assert.IsTrue(vec1.IsParallel(vec2, 0.0));
        }

        [TestMethod]
        public void IsParallel_ReturnsFalseForOrthogonalVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(0.0, 1.0, 0.0);

            // Act & Assert
            Assert.IsFalse(vec1.IsParallel(vec2, 0.0));
        }

        [TestMethod]
        public void Angle_ReturnsCorrectAngleBetweenVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(0.0, 1.0, 0.0);

            // Act
            double angle = vec1.Angle(vec2);

            // Assert
            Assert.AreEqual(System.Math.PI / 2, angle, 1e-6);
        }

        [TestMethod]
        public void AngleWithRef_ReturnsCorrectAngleWithReference()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(0.0, 1.0, 0.0);
            GVec refVec = new GVec(0.0, 0.0, 1.0);

            // Act
            double angle = vec1.AngleWithRef(vec2, refVec);

            // Assert
            Assert.AreEqual(System.Math.PI / 2, angle, 1e-6);
        }

        [TestMethod]
        public void Magnitude_ReturnsZeroForZeroVector()
        {
            // Arrange
            GVec zeroVec = new GVec(0.0, 0.0, 0.0);

            // Act
            double magnitude = zeroVec.Magnitude();

            // Assert
            Assert.AreEqual(0.0, magnitude, 1e-6);
        }

        [TestMethod]
        public void SquareMagnitude_ReturnsCorrectSquareMagnitude()
        {
            // Arrange
            GVec vec = new GVec(1.0, 2.0, 3.0);

            // Act
            double squareMagnitude = vec.SquareMagnitude();

            // Assert
            Assert.AreEqual(14.0, squareMagnitude, 1e-6);
        }

        [TestMethod]
        public void Reverse_ReturnsVectorWithOppositeDirection()
        {
            // Arrange
            GVec vec = new GVec(1.0, 2.0, 3.0);

            // Act
            GVec reversed = vec.Reversed();

            // Assert
            Assert.AreEqual(-1.0, reversed.X(), 1e-6);
            Assert.AreEqual(-2.0, reversed.Y(), 1e-6);
            Assert.AreEqual(-3.0, reversed.Z(), 1e-6);
        }

        [TestMethod]
        public void Mirror_ReturnsVectorReflectedOverVector()
        {
            // Arrange
            GVec vec = new GVec(1.0, 2.0, 3.0); // 原始向量
            GVec theV = new GVec(2.0, 3.0, 4.0); // 镜像中心方向向量

            // Act
            GVec mirrored = vec.Mirrored(theV); // 获取镜像向量

            // Assert
            double d = theV.Magnitude(); // 计算镜像中心方向向量的模
            double a = theV.X() / d; // 计算单位化后的镜像中心方向向量的X分量
            double b = theV.Y() / d; // 计算单位化后的镜像中心方向向量的Y分量
            double c = theV.Z() / d; // 计算单位化后的镜像中心方向向量的Z分量
            double m1 = 2.0 * a * b;
            double m2 = 2.0 * a * c;
            double m3 = 2.0 * b * c;
            double x = vec.X();
            double y = vec.Y();
            double z = vec.Z();
            double expectedX = ((2.0 * a * a) - 1.0) * x + m1 * y + m2 * z;
            double expectedY = m1 * x + ((2.0 * b * b) - 1.0) * y + m3 * z;
            double expectedZ = m2 * x + m3 * y + ((2.0 * c * c) - 1.0) * z;
            Assert.AreEqual(expectedX, mirrored.X(), 1e-6);
            Assert.AreEqual(expectedY, mirrored.Y(), 1e-6);
            Assert.AreEqual(expectedZ, mirrored.Z(), 1e-6);
        }

        [TestMethod]
        public void Mirror_ReturnsVectorReflectedOverAxis()
        {
            // Arrange
            GVec vec = new GVec(1.0, 2.0, 3.0);
            GAx1 axis = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 1.0, 0.0)); // Axis along the Y-axis

            // Act
            GVec mirrored = vec.Mirrored(axis);

            // Assert
            // The mirrored vector should have the same Y component and opposite X and Z components
            Assert.AreEqual(-vec.X(), mirrored.X(), 1e-6);
            Assert.AreEqual(vec.Y(), mirrored.Y(), 1e-6);
            Assert.AreEqual(-vec.Z(), mirrored.Z(), 1e-6);
        }


        [TestMethod]
        public void Rotated_ReturnsRotatedVector()
        {
            // Arrange
            GVec vec = new GVec(1.0, 0.0, 0.0); // 原始向量沿X轴
            GAx1 axis = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 1.0, 0.0)); // 旋转轴，沿Y轴
            double angle = -System.Math.PI / 4; // 旋转角度，逆时针旋转45°

            // Act
            GVec rotated = vec.Rotated(axis, angle); // 执行旋转

            // Assert
            // 预期的旋转后的向量坐标可以通过旋转矩阵计算得到
            double cosAngle = System.Math.Cos(angle);
            double sinAngle = System.Math.Sin(angle);
            double expectedX = 1.0 * cosAngle + 0.0 * sinAngle; // X坐标旋转后的值
            double expectedY = 0.0; // Y坐标保持不变
            double expectedZ = 0.0 * cosAngle - 1.0 * sinAngle; // Z坐标旋转后的值
            Assert.AreEqual(expectedX, rotated.X(), 1e-6);
            Assert.AreEqual(expectedY, rotated.Y(), 1e-6);
            Assert.AreEqual(expectedZ, rotated.Z(), 1e-6);
        }

        [TestMethod]
        public void Scaled_ReturnsScaledVector()
        {
            // Arrange
            GVec vec = new GVec(1.0, 2.0, 3.0); // 原始向量
            double scale = 2.0; // 缩放因子

            // Act
            GVec scaled = vec.Scaled(scale); // 执行缩放

            // Assert
            double expectedX = 1.0 * scale;
            double expectedY = 2.0 * scale;
            double expectedZ = 3.0 * scale;
            Assert.AreEqual(expectedX, scaled.X(), 1e-6);
            Assert.AreEqual(expectedY, scaled.Y(), 1e-6);
            Assert.AreEqual(expectedZ, scaled.Z(), 1e-6);
        }

        [TestMethod]
        public void Transformed_ReturnsOriginalVectorAfterTranslation()
        {
            // Arrange
            GVec vec = new GVec(1.0, 2.0, 3.0); // 原始向量
            GTrsf trsf = new GTrsf(); // 变换对象
            GVec translation = new GVec(2.0, 3.0, 4.0); // 平移向量
            trsf.SetTranslation(translation); // 设置变换为平移

            // Act
            GVec transformed = vec.Transformed(trsf); // 执行变换

            // Assert
            // 预期变换后的向量坐标与原始向量坐标相同，因为平移不改变向量的方向或长度
            Assert.AreEqual(vec.X(), transformed.X(), 1e-6);
            Assert.AreEqual(vec.Y(), transformed.Y(), 1e-6);
            Assert.AreEqual(vec.Z(), transformed.Z(), 1e-6);
        }

        [TestMethod]
        public void Transformed_ReturnsVectorAfterCompositeTransformation()
        {
            // Arrange
            GVec vec = new GVec(1.0, 0.0, 0.0); // 原始向量沿X轴

            // 创建平移变换
            GTrsf translationTrsf = new GTrsf();
            GVec translationVec = new GVec(2.0, 3.0, 4.0);
            translationTrsf.SetTranslation(translationVec);

            // 创建旋转变换
            GTrsf rotationTrsf = new GTrsf();
            GAx1 rotationAxis = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 1.0, 0.0)); // 绕Y轴
            double rotationAngle = System.Math.PI / 4; // 旋转角度
            rotationTrsf.SetRotation(rotationAxis, rotationAngle);

            // 创建缩放变换
            GTrsf scaleTrsf = new GTrsf();
            GPnt scaleCenter = new GPnt(1.0, 1.0, 1.0); // 缩放中心点
            double scale = 2.0; // 缩放因子
            scaleTrsf.SetScale(scaleCenter, scale);

            // 组合变换：首先缩放，然后旋转，最后平移
            GTrsf combinedTrsf = new GTrsf();
            combinedTrsf.Multiply(scaleTrsf);
            combinedTrsf.Multiply(rotationTrsf);
            combinedTrsf.Multiply(translationTrsf);

            // Act
            GVec transformed = vec.Transformed(combinedTrsf); // 执行复合变换

            // 构造expected
            GVec expected = vec.Scaled(2.0).Rotated(rotationAxis, rotationAngle);

            // Assert
            Assert.AreEqual(expected.X(), transformed.X(), 1e-6);
            Assert.AreEqual(expected.Y(), transformed.Y(), 1e-6);
            Assert.AreEqual(expected.Z(), transformed.Z(), 1e-6);
        }

        [TestMethod]
        public void Reversed_ReturnsReversedVector()
        {
            // Arrange
            GVec vec = new GVec(1.0, 2.0, 3.0); // 原始向量

            // Act
            GVec reversed = vec.Reversed(); // 执行反转

            // Assert
            double expectedX = -1.0;
            double expectedY = -2.0;
            double expectedZ = -3.0;
            Assert.AreEqual(expectedX, reversed.X(), 1e-6);
            Assert.AreEqual(expectedY, reversed.Y(), 1e-6);
            Assert.AreEqual(expectedZ, reversed.Z(), 1e-6);
        }

        [TestMethod]
        public void Add_UpdatesVectorWithSumOfTwoVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(0.0, 1.0, 0.0);

            // Act
            vec1.Add(vec2);

            // Assert
            Assert.AreEqual(1.0, vec1.X(), 1e-6);
            Assert.AreEqual(1.0, vec1.Y(), 1e-6);
            Assert.AreEqual(0.0, vec1.Z(), 1e-6);
        }


        [TestMethod]
        public void Subtract_UpdatesVectorWithDifferenceOfTwoVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(0.0, 1.0, 0.0);

            // Act
            vec1.Subtract(vec2);

            // Assert
            Assert.AreEqual(1.0, vec1.X(), 1e-6);
            Assert.AreEqual(-1.0, vec1.Y(), 1e-6);
            Assert.AreEqual(0.0, vec1.Z(), 1e-6);
        }

        [TestMethod]
        public void Multiply_UpdatesVectorWithScalarMultiplication()
        {
            // Arrange
            GVec vec = new GVec(1.0, 2.0, 3.0);
            double scalar = 2.0;

            // Act
            vec.Multiply(scalar);

            // Assert
            Assert.AreEqual(2.0, vec.X(), 1e-6);
            Assert.AreEqual(4.0, vec.Y(), 1e-6);
            Assert.AreEqual(6.0, vec.Z(), 1e-6);
        }

        [TestMethod]
        public void Divide_UpdatesVectorWithScalarDivision()
        {
            // Arrange
            GVec vec = new GVec(2.0, 4.0, 6.0);
            double scalar = 2.0;

            // Act
            vec.Divide(scalar);

            // Assert
            Assert.AreEqual(1.0, vec.X(), 1e-6);
            Assert.AreEqual(2.0, vec.Y(), 1e-6);
            Assert.AreEqual(3.0, vec.Z(), 1e-6);
        }

        [TestMethod]
        public void Cross_ComputesCrossProductOfTwoVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(0.0, 1.0, 0.0);

            // Act
            GVec crossProduct = vec1.Crossed(vec2);

            // Assert
            Assert.AreEqual(0.0, crossProduct.X(), 1e-6);
            Assert.AreEqual(0.0, crossProduct.Y(), 1e-6);
            Assert.AreEqual(1.0, crossProduct.Z(), 1e-6);
        }

        [TestMethod]
        public void Crossed_ComputesCrossProductOfTwoVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 0.0, 0.0);
            GVec vec2 = new GVec(0.0, 1.0, 0.0);

            // Act
            GVec crossProduct = vec1.Crossed(vec2);

            // Assert
            Assert.AreEqual(0.0, crossProduct.X(), 1e-6);
            Assert.AreEqual(0.0, crossProduct.Y(), 1e-6);
            Assert.AreEqual(1.0, crossProduct.Z(), 1e-6);
        }

        [TestMethod]
        public void Dot_ComputesDotProductOfTwoVectors()
        {
            // Arrange
            GVec vec1 = new GVec(1.0, 2.0, 3.0);
            GVec vec2 = new GVec(4.0, 5.0, 6.0);

            // Act
            double dotProduct = vec1.Dot(vec2);

            // Assert
            Assert.AreEqual(1.0 * 4.0 + 2.0 * 5.0 + 3.0 * 6.0, dotProduct, 1e-6);
        }

        [TestMethod]
        public void Normalize_ReturnsUnitVector()
        {
            // Arrange
            GVec vec = new GVec(2.0, 0.0, 0.0);

            // Act
            GVec normalized = vec.Normalized();

            // Assert
            Assert.AreEqual(1.0, normalized.X(), 1e-6);
            Assert.AreEqual(0.0, normalized.Y(), 1e-6);
            Assert.AreEqual(0.0, normalized.Z(), 1e-6);
        }
    }
}
