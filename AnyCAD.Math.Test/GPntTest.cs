namespace AnyCAD.Math.Test
{
    /// <summary>
    /// GPnt对象(三维坐标点)相关接口的单元测试集
    /// API参考文档：http://anycad.cn/api/2024/class_g_pnt.html
    /// </summary>
    [TestClass]
    public class GPntTest
    {
        [TestMethod]
        public void TestDefaultConstructor()
        {
            // Arrange & Act
            var point = new GPnt();

            // Assert
            Assert.AreEqual(0, point.X());
            Assert.AreEqual(0, point.Y());
            Assert.AreEqual(0, point.Z());
        }

        [TestMethod]
        public void TestConstructorWithParameters()
        {
            // Arrange
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;

            // Act
            var point = new GPnt(x, y, z);

            // Assert
            Assert.AreEqual(x, point.X());
            Assert.AreEqual(y, point.Y());
            Assert.AreEqual(z, point.Z());
        }


        [TestMethod]
        public void TestBaryCenter_MidPoint()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var otherPoint = new GPnt(2.0, 3.0, 4.0);
            double alpha = 1.0;
            double beta = 1.0;

            // Act
            point.BaryCenter(alpha, otherPoint, beta);

            // Assert
            Assert.AreEqual(1.5, point.X());
            Assert.AreEqual(2.5, point.Y());
            Assert.AreEqual(3.5, point.Z());
        }

        [TestMethod]
        public void TestBaryCenter_WeightedAverage()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var otherPoint = new GPnt(2.0, 3.0, 4.0);
            double alpha = 1.0;
            double beta = 2.0;

            // Act
            point.BaryCenter(alpha, otherPoint, beta);

            // Assert
            Assert.AreEqual(5.0 / 3.0, point.X());
            Assert.AreEqual(8.0 / 3.0, point.Y());
            Assert.AreEqual(11.0 / 3.0, point.Z());
        }

        [TestMethod]
        public void TestIsEqual_SamePoint()
        {
            // Arrange
            var point1 = new GPnt(1.0, 2.0, 3.0);
            var point2 = new GPnt(1.0, 2.0, 3.0);
            double tolerance = 0.1;

            // Act
            bool isEqual = point1.IsEqual(point2, tolerance);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void TestIsEqual_DifferentPointsWithinTolerance()
        {
            // Arrange
            var point1 = new GPnt(1.0, 2.0, 3.0);
            var point2 = new GPnt(1.01, 2.01, 3.01);
            double tolerance = 0.05;

            // Act
            bool isEqual = point1.IsEqual(point2, tolerance);

            // Assert
            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void TestIsEqual_DifferentPointsOutsideTolerance()
        {
            // Arrange
            var point1 = new GPnt(1.0, 2.0, 3.0);
            var point2 = new GPnt(2.0, 3.0, 4.0);
            double tolerance = 0.1;

            // Act
            bool isEqual = point1.IsEqual(point2, tolerance);

            // Assert
            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void TestDistance_SamePoint()
        {
            // Arrange
            var point1 = new GPnt(1.0, 2.0, 3.0);
            var point2 = new GPnt(1.0, 2.0, 3.0);

            // Act
            double distance = point1.Distance(point2);

            // Assert
            Assert.AreEqual(0, distance);
        }

        [TestMethod]
        public void TestDistance_DifferentPoints()
        {
            // Arrange
            var point1 = new GPnt(1.0, 2.0, 3.0);
            var point2 = new GPnt(4.0, 5.0, 6.0);

            // Act
            double distance = point1.Distance(point2);

            // Assert
            Assert.AreEqual(System.Math.Sqrt(27.0), distance);
        }

        [TestMethod]
        public void TestDistance_NegativeCoordinates()
        {
            // Arrange
            var point1 = new GPnt(-1.0, -2.0, -3.0);
            var point2 = new GPnt(-4.0, -5.0, -6.0);

            // Act
            double distance = point1.Distance(point2);

            // Assert
            Assert.AreEqual(System.Math.Sqrt(27.0), distance);
        }

        [TestMethod]
        public void TestSquareDistance_SamePoint()
        {
            // Arrange
            var point1 = new GPnt(1.0, 2.0, 3.0);
            var point2 = new GPnt(1.0, 2.0, 3.0);

            // Act
            double squareDistance = point1.SquareDistance(point2);

            // Assert
            Assert.AreEqual(0.0, squareDistance);
        }

        [TestMethod]
        public void TestSquareDistance_DifferentPoints()
        {
            // Arrange
            var point1 = new GPnt(1.0, 2.0, 3.0);
            var point2 = new GPnt(4.0, 5.0, 6.0);

            // Act
            double squareDistance = point1.SquareDistance(point2);

            // Assert
            Assert.AreEqual(27.0, squareDistance);
        }

        [TestMethod]
        public void TestMirror_Origin()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var mirrorPoint = new GPnt(0.0, 0.0, 0.0);

            // Act
            var mirroredPoint = point.Mirrored(mirrorPoint);
            point.Mirror(mirrorPoint);

            // Assert
            Assert.AreEqual(-1.0, mirroredPoint.X());
            Assert.AreEqual(-2.0, mirroredPoint.Y());
            Assert.AreEqual(-3.0, mirroredPoint.Z());

            Assert.AreEqual(-1.0, point.X());
            Assert.AreEqual(-2.0, point.Y());
            Assert.AreEqual(-3.0, point.Z());
        }

        [TestMethod]
        public void TestMirror_NonOrigin()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var mirrorPoint = new GPnt(1.0, 1.0, 1.0);

            // Act
            var mirroredPoint = point.Mirrored(mirrorPoint);
            point.Mirror(mirrorPoint);

            // Assert
            Assert.AreEqual(1.0, mirroredPoint.X());
            Assert.AreEqual(0.0, mirroredPoint.Y());
            Assert.AreEqual(-1.0, mirroredPoint.Z());

            Assert.AreEqual(1.0, point.X());
            Assert.AreEqual(0.0, point.Y());
            Assert.AreEqual(-1.0, point.Z());
        }

        [TestMethod]
        public void TestScale_Origin()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var scalePoint = new GPnt(0.0, 0.0, 0.0);
            double scaleValue = 2.0;

            // Act
            var scaledPoint = point.Scaled(scalePoint, scaleValue);
            point.Scale(scalePoint, scaleValue);

            // Assert
            Assert.AreEqual(2.0, point.X());
            Assert.AreEqual(4.0, point.Y());
            Assert.AreEqual(6.0, point.Z());
        }

        [TestMethod]
        public void TestScale_NonOrigin()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var scalePoint = new GPnt(1.0, 1.0, 1.0);
            double scaleValue = 2.0;

            // Act
            point.Scale(scalePoint, scaleValue);

            // Assert
            Assert.AreEqual(1.0, point.X());
            Assert.AreEqual(3.0, point.Y());
            Assert.AreEqual(5.0, point.Z());
        }

        [TestMethod]
        public void TestTranslate()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var point1 = new GPnt(1.0, 1.0, 1.0);
            var point2 = new GPnt(2.0, 2.0, 2.0);
            var expectedPoint = new GPnt(point.X() + point2.X() - point1.X(), point.Y() + point2.Y() - point1.Y(), point.Z() + point2.Z() - point1.Z());

            // Act
            point.Translate(point1, point2);

            // Assert
            Assert.AreEqual(expectedPoint.X(), point.X());
            Assert.AreEqual(expectedPoint.Y(), point.Y());
            Assert.AreEqual(expectedPoint.Z(), point.Z());
        }

        [TestMethod]
        public void TestTranslate_Vector()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var vector = new GVec(1.0, 1.0, 1.0);

            // Act
            point.Translate(vector);

            // Assert
            Assert.AreEqual(2.0, point.X());
            Assert.AreEqual(3.0, point.Y());
            Assert.AreEqual(4.0, point.Z());
        }

        [TestMethod]
        public void TestTranslate_NegativeVector()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var vector = new GVec(-1.0, -1.0, -1.0);

            // Act
            point.Translate(vector);

            // Assert
            Assert.AreEqual(0.0, point.X());
            Assert.AreEqual(1.0, point.Y());
            Assert.AreEqual(2.0, point.Z());
        }

        [TestMethod]
        public void TestRotate_ZeroAngle()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var axis = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0));
            double angle = 0.0;

            // Act
            point.Rotate(axis, angle);

            // Assert
            Assert.AreEqual(1.0, point.X());
            Assert.AreEqual(2.0, point.Y());
            Assert.AreEqual(3.0, point.Z());
        }

        [TestMethod]
        public void TestRotate_NonZeroAngle()
        {
            // Arrange
            var point = new GPnt(1.0, 0.0, 0.0);
            var axis = new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0));
            double angle = System.Math.PI / 2;  // 90 degrees

            // Act
            point.Rotate(axis, angle);

            // Assert
            Assert.AreEqual(0.0, point.X(), 1e-10);
            Assert.AreEqual(1.0, point.Y(), 1e-10);
            Assert.AreEqual(0.0, point.Z(), 1e-10);
        }

        [TestMethod]
        public void TestRotate_NonAlignedAxis()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var axis = new GAx1(new GPnt(1.0, 1.0, 1.0), new GDir(1.0, 1.0, 1.0));
            double angle = System.Math.PI / 2;  // 90 degrees

            // Act
            point.Rotate(axis, angle);

            // Assert
            Assert.AreEqual(2.5773502691896262, point.X(), 1e-10);
            Assert.AreEqual(0.84529946162074832, point.Y(), 1e-10);
            Assert.AreEqual(2.5773502691896257, point.Z(), 1e-10);
        }

        [TestMethod]
        public void TestTransform_Translation()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var transformation = new GTrsf();
            transformation.SetTranslation(new GVec(1.0, 1.0, 1.0));

            // Act
            point.Transform(transformation);

            // Assert
            Assert.AreEqual(2.0, point.X());
            Assert.AreEqual(3.0, point.Y());
            Assert.AreEqual(4.0, point.Z());
        }

        [TestMethod]
        public void TestTransform_Rotation()
        {
            // Arrange
            var point = new GPnt(1.0, 0.0, 0.0);
            var transformation = new GTrsf();
            transformation.SetRotation(new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)), System.Math.PI / 2);

            // Act
            point.Transform(transformation);

            // Assert
            Assert.AreEqual(0.0, point.X(), 1e-10);
            Assert.AreEqual(1.0, point.Y(), 1e-10);
            Assert.AreEqual(0.0, point.Z(), 1e-10);
        }

        [TestMethod]
        public void TestTransform_Scaling()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var transformation = new GTrsf();
            transformation.SetScale(new GPnt(0.0, 0.0, 0.0), 2.0);

            // Act
            point.Transform(transformation);

            // Assert
            Assert.AreEqual(2.0, point.X());
            Assert.AreEqual(4.0, point.Y());
            Assert.AreEqual(6.0, point.Z());
        }

        [TestMethod]
        public void TestTransform_ComplexTransformation()
        {
            // Arrange
            var point = new GPnt(1.0, 2.0, 3.0);
            var expectedPoint = new GPnt(point.X(), point.Y(), point.Z());
            var transformation = new GTrsf();
            transformation.SetScaleFactor(2.0);
            var quaternion = new GQuaternion(new GVec(0.0, 0.0, 1.0), System.Math.PI / 2);
            transformation.SetRotationPart(quaternion);
            transformation.SetTranslationPart(new GVec(1.0, 1.0, 1.0));

            // Apply the transformations to the expected point
            expectedPoint.Scale(new GPnt(0.0, 0.0, 0.0), 2.0);
            expectedPoint.Rotate(new GAx1(new GPnt(0.0, 0.0, 0.0), new GDir(0.0, 0.0, 1.0)), System.Math.PI / 2);
            expectedPoint.Translate(new GVec(1.0, 1.0, 1.0));

            // Act
            point.Transform(transformation);

            // Assert
            Assert.AreEqual(expectedPoint.X(), point.X(), 1e-10);
            Assert.AreEqual(expectedPoint.Y(), point.Y(), 1e-10);
            Assert.AreEqual(expectedPoint.Z(), point.Z(), 1e-10);
        }
    }
}