using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;



namespace AnyCAD.Demo.Graphics
{
    class Graphics_FontTexture : TestCase
    {
        public override void Run(RenderControl render)
        {
            var fontMaterial = FontMaterial.Create(render.GetMaterialManager(), "font-texture-1");
            fontMaterial.SetFaceSide(EnumFaceSide.DoubleSide);
            fontMaterial.SetColor(new Vector3(1, 1, 0));
            fontMaterial.SetBackground(new Vector3(0, 0, 1));
            fontMaterial.SetBillboard(true);

            var dim = fontMaterial.SetText("Hello 世界!", 128);
            var shape = GeometryBuilder.CreatePlane(dim.x * 0.1f, dim.y*0.1f);

            var node = new PrimitiveSceneNode(shape, fontMaterial);
            node.SetPickable(false);

            render.ShowSceneNode(node);
        }
    }
}
