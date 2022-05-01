using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_90000Sphere : TestCase
    {
        public override void Run(RenderControl renderer)
        {
            var shape = ShapeBuilder.MakeSphere(new GPnt(), 1);
            var bufferShape = new BufferShape(shape, null, null, 0.1);
            bufferShape.Build();

            float distance = 3.0f;
            int halfCount = 150;
            int kk = 0;
            var scene = renderer.GetScene();
            for (int ii = -halfCount; ii < halfCount; ++ii)
                for (int jj = -halfCount; jj < halfCount; ++jj)
                    //for (int kk = -halfCount; kk < halfCount; ++kk)
                    {
                        var node = new BrepSceneNode(bufferShape);
                        node.SetTransform(Matrix4.makeTranslation(ii * distance, jj * distance, kk * distance));
                        scene.AddNode(node);
                    }
            renderer.ZoomAll();

            Matrix4 m1 = new Matrix4(1);
            Matrix4 m2 = new Matrix4(1);
            Matrix4 x = m1 * m2;
        }
    }
}
