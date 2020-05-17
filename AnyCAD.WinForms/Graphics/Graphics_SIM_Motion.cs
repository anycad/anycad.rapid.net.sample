using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;


namespace AnyCAD.Demo.Graphics
{
    class Graphics_SIM_Motion : TestCase
    {
        ShapeSceneNode TubeNode;
        ShapeSceneNode CylinderNode;
        MaterialInstance RedMaterial;
        MaterialInstance GrayMaterial;
        float nStep = 1;
        float nCurrentHeight = 0;
        float nDirection = 1;
        float nScale = 10;

        ShapeSceneNode ConeNode1;
        ShapeSceneNode ConeNode2;
        public override void Run(RenderControl render)
        {
            var mmgr = render.GetMaterialManager();
            RedMaterial = mmgr.Create("tube.color", "phong");
            RedMaterial.SetUniform("diffuse", Uniform.Create(new Vector3(1, 0, 1)));

            GrayMaterial = mmgr.Create("cylinder.color", "phong");
            GrayMaterial.SetUniform("diffuse", Uniform.Create(new Vector3(0.8f)));

            var tube = ShapeBuilder.MakeTube(new GPnt(0,0,5), GP.DZ(), 10, 2, 50);
            TubeNode = ShapeSceneNode.Create(tube);
            TubeNode.SetMaterial(RedMaterial);

            var cylinder = ShapeBuilder.MakeCylinder(GP.XOY(), 10, 60, 0);
            CylinderNode = ShapeSceneNode.Create(cylinder);
            CylinderNode.SetMaterial(GrayMaterial);

            render.ShowSceneNode(TubeNode);
            render.ShowSceneNode(CylinderNode);


            var cone = ShapeBuilder.MakeCone(GP.YOZ(), 5, 0, 10, 0);
            var bs = new BufferShape(cone);
            bs.Build();
            ConeNode1 = new ShapeSceneNode(bs);
            ConeNode1.SetMaterial(RedMaterial);
            
            ConeNode2 = new ShapeSceneNode(bs);
            ConeNode2.SetMaterial(GrayMaterial);

            render.ShowSceneNode(ConeNode1);
            render.ShowSceneNode(ConeNode2);
        }

        public override void Animation(RenderControl render, float time)
        {
            if (nCurrentHeight > 50)
            {
                CylinderNode.SetMaterial(GrayMaterial);
                TubeNode.SetMaterial(RedMaterial);
                nDirection = -1;
            }
            else if (nCurrentHeight < 0)
            {
                nDirection = 1;
                CylinderNode.SetMaterial(RedMaterial);
                TubeNode.SetMaterial(GrayMaterial);
            }

            {
                var matrixR = Matrix4.makeRotationAxis(new Vector3(0, 0, 1), time);
                var matrixT = Matrix4.makeTranslation(-50, 0, 0);
                ConeNode1.SetTransform(matrixR * matrixT);

                nScale *= 1.01f;
                if (nScale > 5)
                    nScale = 1;
                ConeNode2.SetTransform(matrixT * matrixR * Matrix4.makeScale(1, nScale, nScale));
            }
                

            nCurrentHeight += nStep * nDirection;
            CylinderNode.SetTransform(Matrix4.makeTranslation(0, 0, nCurrentHeight));


            render.GetContext().UpdateWorldBox();
            render.RequestDraw();
        }
    }
}
