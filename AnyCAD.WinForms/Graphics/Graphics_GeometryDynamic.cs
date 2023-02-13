using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;


namespace AnyCAD.Demo.Graphics
{
    class Graphics_GeometryDynamic : TestCase
    {
        uint worldWidth = 128;
        uint worldDepth = 128;
        BufferGeometry mGeometry;
        public override void Run(RenderControl render)
        {
            mGeometry = GeometryBuilder.CreatePlane(20000, 20000, worldWidth - 1, worldDepth - 1);
            var position = mGeometry.GetAttribute(0);
            position.SetDataUsage(EnumBufferDataUsage.DYNAMIC_DRAW);
            var mPosition = new Float32Array(position.GetData());
            for (uint i = 0; i < position.GetCount()/3; i++)
            {

                float z = (float)(35 * Math.Sin(i / 2));
                mPosition.SetValue(i * 3 + 2, z);
            }

            var material = BasicMaterial.Create("basic-water");
            var img = FileImage.Create(GetResourcePath("textures/water.png"));
            var texture = new ImageTexture2D();
            texture.SetSource(img);
            var desc = texture.GetDesc();
            desc.SetWrapS(EnumTextureWrappingType.REPEAT);
            desc.SetWrapT(EnumTextureWrappingType.REPEAT);
            texture.SetRepeat(new Vector2(5, 5));

            material.AddTexture("map", texture);
            var color = ColorTable.Hex(0x0044ff);
            material.SetUniform("diffuse", color);
     

            var node = new PrimitiveSceneNode(mGeometry, material);
        
            node.SetPickable(false);
            node.SetCulling(false);

            render.ShowSceneNode(node);
        }

        public override void Animation(RenderControl render, float time)
        {
            var position = mGeometry.GetAttribute(0);
            position.SetDataUsage(EnumBufferDataUsage.DYNAMIC_DRAW);
            var mPosition = new Float32Array(position.GetData());
            var count = mPosition.GetItemCount() / 3;
            for (uint i = 0; i < count; i++)
            {

                var z = (float)(35 * Math.Sin(i / 5 + (time * 50 + i) / 7));
                mPosition.SetValue(i*3+2, z);
            }
            position.RequestUpdate();
            mGeometry.RequestUpdate();

            render.RequestDraw();
        }
    }
}
