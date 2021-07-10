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
        ParticleSceneNode mMotionTrail = new ParticleSceneNode(1000, ColorTable.Red, 3.0f);
        RigidAnimation mAnimation;

        public override void Run(RenderControl render)
        {
            var material = MeshStandardMaterial.Create("workpiece");
            material.SetColor(new Vector3(0.9f));
            material.SetFaceSide(EnumFaceSide.DoubleSide);

            var shape = ShapeBuilder.MakeCylinder(GP.YOZ(), 5, 50, 0);
            mWorkpiece = BrepSceneNode.Create(shape, material, null);

            render.ShowSceneNode(mWorkpiece);

            var coord = new GAx2(new GPnt(0, 0, 5), GP.DZ());
            var toolShape = ShapeBuilder.MakeCone(coord, 0, 2, 5, 0);
            var toolNode = render.ShowShape(toolShape, ColorTable.Blue);


            render.ShowSceneNode(mMotionTrail);


            // Initialize Animation
            mAnimation = new RigidAnimation();

            var rotation = Matrix4.makeRotationAxis(new Vector3(1, 0, 0), (float)Math.PI);
            var trf = Matrix4.makeTranslation(-50, 0, 0) * rotation;

            mAnimation.Add(new MatrixAnimationClip(mWorkpiece, mWorkpiece.GetTransform(), trf, 0, 10));
            mAnimation.Add(new MatrixAnimationClip(toolNode, toolNode.GetTransform(), trf, 10, 15));
            mAnimation.Add(new RotateAnimationClip(toolNode, Vector3.UNIT_Z, (float)Math.PI*4, 16, 20));
        }
        
        uint mCount = 0;
        public override void Animation(RenderControl render, float time)
        {
            if(mAnimation.Play(time))
            {
                var trf = mWorkpiece.GetTransform();
                Vector3 tailPostion = new Vector3(0, 0, 5);
                tailPostion.applyMatrix4(trf);
                mMotionTrail.SetPosition(mCount++, tailPostion);
                mMotionTrail.RequstUpdate();

                render.RequestDraw(EnumUpdateFlags.Scene);
            }


        }
     }
}
