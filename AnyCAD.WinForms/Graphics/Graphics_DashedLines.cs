using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;



namespace AnyCAD.Demo.Graphics
{
    class Graphics_DashedLines : TestCase
    {
        public override void Run(RenderControl render)
        {
            // prepare lines data
            const int COUNT = 300;
            var buffer = new Float32Buffer(0);
            buffer.Reserve(COUNT * 3);

            var colors = new Float32Buffer(0);

            Random random = new Random();
            for (int i = 0; i < COUNT; i++)
            {

                float x = 20 * (float)random.NextDouble() - 10;
                float y = 20 * (float)random.NextDouble() - 10;
                float z = 20 * (float)random.NextDouble() - 10;

                buffer.Append( x,y,z);
                colors.Append( (float)random.NextDouble(), (float)random.NextDouble() , (float)random.NextDouble() );
            }

            var primitive = GeometryBuilder.CreateLines(new Float32Array(buffer), null, new Float32Array(colors));
            GeometryBuilder.ComputeLineDistances(primitive);
            // prepare dashed line material
            var material = LineDashedMaterial.Create("dashed-line");
            // enable vertex color
            material.SetVertexColors(true);
            material.SetDashSize(1.0f);

            // add to scene
            var node = new PrimitiveSceneNode(primitive, material);
  
            render.ShowSceneNode(node);
        }
    }
}
