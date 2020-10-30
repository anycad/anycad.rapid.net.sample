using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo.Graphics
{
    class Simulation_MotionTrail : TestCase
    {
        BrepSceneNode mWorkpiece = null;
        ParticleSceneNode mMotionTrail = new ParticleSceneNode(1000, Vector3.Red, 3.0f);
        public override void Run(RenderControl render)
        {
            var material = MeshStandardMaterial.Create("workpiece");
            material.SetColor(new Vector3(0.9f));
            material.SetFaceSide(EnumFaceSide.DoubleSide);

            var shape = ShapeBuilder.MakeCylinder(GP.YOZ(), 5, 50, 0);
            mWorkpiece = BrepSceneNode.Create(shape, material, null);

            render.ShowSceneNode(mWorkpiece);

            {
                var coord = new GAx2(new GPnt(0, 0, 5), GP.DZ());
                var toolShape = ShapeBuilder.MakeCone(coord, 0, 2, 5, 0);
                render.ShowShape(toolShape, Vector3.Blue);
            }
            

            render.ShowSceneNode(mMotionTrail);

            mLength = 0;
            mTheta = 0;
        }

        float mTheta = 0;
        float mLength = 0;
        
        uint mCount = 0;
        public override void Animation(RenderControl render, float time)
        {
            if (mLength > 50)
                return;
  
            mTheta += 0.02f;
            var rotation = Matrix4.makeRotationAxis(new Vector3(1, 0, 0), mTheta);
            mLength += 0.1f;

            var trf = Matrix4.makeTranslation(-mLength, 0, 0) * rotation;

            mWorkpiece.SetTransform(trf);
            mWorkpiece.RequstUpdate();

            Vector3 tailPostion = new Vector3(0, 0, 5);
            tailPostion.applyMatrix4(trf);
            mMotionTrail.SetPosition(mCount++, tailPostion);
            mMotionTrail.RequstUpdate();

            render.RequestDraw(EnumUpdateFlags.Scene);
        }
     }
}
