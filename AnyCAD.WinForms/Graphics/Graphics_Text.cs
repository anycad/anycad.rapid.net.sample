using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;



namespace AnyCAD.Demo.Graphics
{
    class Graphics_Text : TestCase
    {
        static bool mLoaded = false;
        public override void Run(RenderControl render)
        {
            if(!mLoaded)
            {
                mLoaded = true;

                FontManager.Instance().AddFont("LS", @"C:\Windows\Fonts\STLITI.TTF");
            }    

            {
                var fontMaterial = FontMaterial.Create("font-texture-1");
                fontMaterial.SetFaceSide(EnumFaceSide.DoubleSide);
                fontMaterial.SetColor(new Vector3(1, 1, 0));
                fontMaterial.SetBackground(new Vector3(0, 0, 1));
                fontMaterial.SetBillboard(true);

                var dim = fontMaterial.SetText("Hello 世界!", 128, "LS");
                var shape = GeometryBuilder.CreatePlane(dim.x * 0.1f, dim.y * 0.1f);

                var node = new PrimitiveSceneNode(shape, fontMaterial);
                node.SetPickable(false);

                render.ShowSceneNode(node);
            }
            {
                var fixedSizeMaterial = SpriteMaterial.Create("font-mesh-material");
                fixedSizeMaterial.SetSizeAttenuation(false);
                fixedSizeMaterial.SetColor(ColorTable.OrangeRed);

                var mesh = FontManager.Instance().CreateMesh("为中华之崛起而代码！");
                var node = new PrimitiveSceneNode(mesh, fixedSizeMaterial);
                var scale = 1 / 22.0f;
                node.SetTransform(Matrix4.makeTranslation(0, 10, 10) * Matrix4.makeScale(scale, scale, scale));
                node.SetPickable(false);

                render.ShowSceneNode(node);
            }
        }
    }
}
