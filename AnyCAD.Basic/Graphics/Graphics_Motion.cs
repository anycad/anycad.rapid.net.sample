using AnyCAD.Foundation;


namespace AnyCAD.Demo.Graphics
{
    class Graphics_Motion : TestCase
    {
        BrepSceneNode TubeNode;
        BrepSceneNode CylinderNode;
        MaterialInstance RedMaterial;
        MaterialInstance GrayMaterial;
        float nStep = 1;
        float nCurrentHeight = 0;
        float nDirection = 1;
        float nScale = 10;

        BrepSceneNode ConeNode1;
        BrepSceneNode ConeNode2;
        public override void Run(IRenderView render)
        {
            RedMaterial = MeshPhongMaterial.Create("phong.color");
            RedMaterial.SetColor(ColorTable.Red);

            GrayMaterial = MeshPhongMaterial.Create("phong.color");
            GrayMaterial.SetColor(ColorTable.小麦色);
            GrayMaterial.SetTransparent(true);
            GrayMaterial.SetOpacity(0.5f);

            var tube = ShapeBuilder.MakeTube(new GPnt(0,0,5), GP.DZ(), 10, 2, 50);
            TubeNode = BrepSceneNode.Create(tube, RedMaterial, null);

            var cylinder = ShapeBuilder.MakeCylinder(GP.XOY(), 10, 60, 0);
            CylinderNode = BrepSceneNode.Create(cylinder, GrayMaterial, null);

            render.ShowSceneNode(TubeNode);
            render.ShowSceneNode(CylinderNode);


            var cone = ShapeBuilder.MakeCone(GP.YOZ(), 5, 0, 10, 0);
            var bs = GRepShape.Create(cone, RedMaterial, null, 0.1, false);
            ConeNode1 = new BrepSceneNode(bs);
            ConeNode2 = new BrepSceneNode(bs);

            render.ShowSceneNode(ConeNode1);
            render.ShowSceneNode(ConeNode2);

            render.EnableAnimation(true);
        }

        public override void Exit(IRenderView render)
        {
            render.EnableAnimation(false);
        }

        public override void Animation(IRenderView render, float time)
        {
            if (nCurrentHeight > 50)
            {
                //CylinderNode.SetMaterial(GrayMaterial);
                //TubeNode.SetMaterial(RedMaterial);
                nDirection = -1;
            }
            else if (nCurrentHeight < 0)
            {
                nDirection = 1;
                //CylinderNode.SetMaterial(RedMaterial);
                //TubeNode.SetMaterial(GrayMaterial);
            }

            {
                var matrixR = Matrix4.makeRotationAxis(new Vector3(0, 0, 1), time);
                var matrixT = Matrix4.makeTranslation(-50, 0, 0);
                ConeNode1.SetTransform(matrixR * matrixT);
                ConeNode1.RequestUpdate();

                nScale *= 1.01f;
                if (nScale > 5)
                    nScale = 1;
                ConeNode2.SetTransform(matrixT * matrixR * Matrix4.makeScale(1, nScale, nScale));
                ConeNode2.RequestUpdate();
            }
                

            nCurrentHeight += nStep * nDirection;
            CylinderNode.SetTransform(Matrix4.makeTranslation(0, 0, nCurrentHeight));
            CylinderNode.RequestUpdate();

            render.RequestDraw(EnumUpdateFlags.Scene);
        }
    }
}
