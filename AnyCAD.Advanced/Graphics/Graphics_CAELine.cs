using AnyCAD.Foundation;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_CAELine : TestCase
    {
        public override void Run(IRenderView render)
        {
            PrimitiveSceneNode CAEnode;
            PaletteWidget Palette;
            Float32Buffer mPositions;
            Float32Buffer mColors;
            ColorLookupTable mColorTable;

            // 定义点
            mPositions = new Float32Buffer(0);
            mPositions.Append(0);
            mPositions.Append(0);
            mPositions.Append(0);
            mPositions.Append(100);
            mPositions.Append(100);
            mPositions.Append(100);
            mPositions.Append(100);
            mPositions.Append(100);
            mPositions.Append(0);

            // 定义边，两个点连成一条边，共三条边
            Uint32Buffer edges = new Uint32Buffer(2 * 3);
            edges.Set(0, 0);
            edges.Set(1, 1);
            edges.Set(2, 1);
            edges.Set(3, 2);
            edges.Set(4, 2);
            edges.Set(5, 0);

            // 定义颜色
            mColorTable = new ColorLookupTable();
            mColorTable.SetColorMap(ColorMapKeyword.Create(EnumSystemColorMap.Rainbow));
            mColorTable.SetMaxValue(100); //设置最大值

            mColors = new Float32Buffer(9);
            var clr1 = mColorTable.GetColor(5);
            mColors.SetValue(0, clr1);
            var clr2 = mColorTable.GetColor(50);
            mColors.SetValue(3, clr2);
            var clr3 = mColorTable.GetColor(90);
            mColors.SetValue(6, clr3);


            // 组装几何对象
            BufferGeometry geometry = new BufferGeometry(EnumPrimitiveType.LINES);
            geometry.AddAttribute(
                EnumAttributeSemantic.Position,
                EnumAttributeComponents.Three,
                mPositions
            );
            geometry.SetIndex(edges);
            geometry.AddAttribute(
                EnumAttributeSemantic.Color,
                EnumAttributeComponents.Three,
                mColors
            );

            BasicMaterial CAEMaterial = BasicMaterial.Create("cae-material");
            CAEMaterial.GetTemplate().SetVertexColors(true);
            CAEMaterial.SetLineWidth(5);

            CAEnode = new PrimitiveSceneNode(geometry, CAEMaterial);
            CAEnode.SetPickable(false);
            Palette = new PaletteWidget();
            Palette.Update(mColorTable);

            render.ShowSceneNode(CAEnode);
            render.ShowSceneNode(Palette);
        }
    }
}
