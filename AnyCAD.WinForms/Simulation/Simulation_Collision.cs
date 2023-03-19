using AnyCAD.Foundation;
using System.Collections.Generic;

namespace AnyCAD.Demo.Graphics
{
    class Simulation_Collision : TestCase
    {
        BrepSceneNode mObject1;
        BrepSceneNode mObject2;
        MaterialInstance mMaterial;
        MaterialInstance mMaterialWarning;
        public override void Run(IRenderView render)
        {
            mMaterial = MeshPhongMaterial.Create("face");
            mMaterial.SetColor(ColorTable.查特酒绿);
            mMaterial.SetFaceSide(EnumFaceSide.DoubleSide);

            mMaterialWarning = MeshPhongMaterial.Create("face");
            mMaterialWarning.SetColor(ColorTable.浅粉红);
            mMaterialWarning.SetFaceSide(EnumFaceSide.DoubleSide);

            var shape1 = ShapeBuilder.MakeCone(GP.XOY(), 10, 5, 10, System.Math.PI/2);
            mObject1 = BrepSceneNode.Create(shape1, null, null);
            mObject1.SetTransform(Matrix4.makeTranslation(100, 100, 0));

            var shape2 = ShapeBuilder.MakeCylinder(GP.XOY(), 20, 20, 0);
            mObject2 = BrepSceneNode.Create(shape2, null, null);

            render.ShowSceneNode(mObject1);
            render.ShowSceneNode(mObject2);
        }

        float mDistance = 100;
        float mStep = -1;
        public override void Animation(IRenderView render, float time)
        {
            var collision = new NodeCollisionDetector(mObject1);
            if(collision.Test(mObject2))
            {
                mObject1.SetFaceMaterial(mMaterialWarning);
            }
            else
            {
                mObject1.SetFaceMaterial(mMaterial);
            }

            if (mDistance < -100)
                mStep = 1;
            else if(mDistance > 100)
            {
                mStep = -1;
            }

            mDistance += mStep;
            mObject1.AddTransform(Matrix4.makeTranslation(mStep, mStep, 0));
            mObject1.RequstUpdate();

            render.RequestDraw(EnumUpdateFlags.Scene);
        }
     }
}
