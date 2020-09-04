using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;



namespace AnyCAD.Demo.Graphics
{
    class Graphics_Points : TestCase
    {
        public override void Run(RenderControl render)
        {
            // prepare points data
            const int COUNT = 300;
            var buffer = new Float32Buffer(0);
            buffer.Reserve(COUNT * 3);

            var colors = new Float32Buffer(0);

            Random random = new Random();
            for (int i = 0; i < COUNT; i++)
            {

                float x = 2000 * (float)random.NextDouble() - 1000;
                float y = 2000 * (float)random.NextDouble() - 1000;
                float z = 2000 * (float)random.NextDouble() - 1000;

                buffer.Append( x,y,z);
                colors.Append( (float)random.NextDouble(), (float)random.NextDouble() , (float)random.NextDouble() );
            }

            var primitive = GeometryBuilder.CreatePoints(new Float32Array(buffer), new Float32Array(colors));

            // prepare point material
            var material = PointsMaterial.Create("points-material");
            material.SetSizeAttenuation(false);
            material.SetPointSize(15.0f);
            material.SetColorMap(ImageTexture2D.Create(GetResourcePath("textures/snowflake7_alpha.png")));
            material.SetTransparent(true);

            // enable vertex color
            material.GetTemplate().SetVertexColors(true);


            // add to scene
            var node = new PrimitiveSceneNode(primitive, material);
            node.SetPickable(false);
            render.ShowSceneNode(node);
        }
    }
}
