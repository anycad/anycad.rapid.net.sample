using AnyCAD.Foundation;
using System;
using System.Collections.Generic;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_100000Box : TestCase
    {
        public override void Run(IRenderView renderer)
        {
            var shape = ShapeBuilder.MakeBox(GP.XOY(), 1, 1, 1);
            var bufferShape = GRepShape.Create(shape, null, null, 0.5, true);
            bufferShape.Build();

            //100 x 100 x 5;
            float distance = 3.0f;
            int halfCount = 50;
            int height = 50;
            var scene = renderer.Scene;
            for (int ii = -halfCount; ii < halfCount; ++ii)
            {
                if (ii == 0)
                    continue;

                for (int jj = -halfCount; jj < halfCount; ++jj)
                {
                    if (jj == 0)
                        continue;

                    for (int kk = 0; kk < height; ++kk)
                    {
                        var node = new BrepSceneNode(bufferShape);
                        node.SetTransform(Matrix4.makeTranslation(ii * distance, jj * distance, kk * distance));
                        scene.AddNode(node);
                    }
                }
            }

            renderer.ZoomAll();
        }
    }
}
