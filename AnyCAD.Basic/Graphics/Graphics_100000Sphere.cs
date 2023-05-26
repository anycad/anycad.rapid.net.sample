using AnyCAD.Foundation;
using System;
using System.Collections.Generic;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_100000Sphere : TestCase
    {
        public void RunBox(IRenderView renderer)
        {
            var shape = ShapeBuilder.MakeBox(GP.XOY(), 1, 1, 1);
            var bufferShape = GRepShape.Create(shape, null, null, 0.5, true);
            bufferShape.Build();

            //100 x 100 x 5;
            float distance = 3.0f;
            int halfCount = 50;
            int height = 5;
            var scene = renderer.Scene;
            for (int ii = -halfCount; ii < halfCount; ++ii)
            {
                if (ii == 0)
                    continue;

                for (int jj = -halfCount; jj < halfCount; ++jj)
                {
                    if(jj ==0)
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

        public override void Run(IRenderView renderer)
        {
            var shape = GeometryBuilder.CreateSphere(1);
            Random random= new Random();
            List<MeshPhongMaterial> materials = new List<MeshPhongMaterial>();
            for(int ii=0; ii<10; ++ii)
            {
                var color = Vector3.From(random.NextDouble(), random.NextDouble(), random.NextDouble());
                var material = MeshPhongMaterial.Create("mesh.material");
                material.SetColor(color);
                materials.Add(material);
            }

            //100 x 100 x 5;
            float distance = 3.0f;
            int halfCount = 50;
            int height = 10;
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
                        var material = materials[random.Next(10)];
                        var node = PrimitiveSceneNode.Create(shape, material);
                        node.SetTransform(Matrix4.makeTranslation(ii * distance, jj * distance, kk * distance));
                        scene.AddNode(node);
                    }
                }
            }

            renderer.ZoomAll();
        }
    }
}
