using AnyCAD.Forms;
using AnyCAD.Foundation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_CAE : TestCase
    {
        static Float32Buffer mPositions;
        static Float32Buffer mPressures;
        static Float32Buffer mColors;
        static ColorLookupTable mColorTable;
        bool ReadData()
        {
            if (mPositions != null)
                return true;

            string fileName = GetResourcePath("pressure.json");
            using (StreamReader reader = File.OpenText(fileName))
            {
                JObject obj = JObject.Parse(reader.ReadToEnd());
                var postionsToken = obj["data"]["attributes"]["position"]["array"].Children().ToList();
                mPositions = new Float32Buffer(0);

                mPositions.Reserve((uint)postionsToken.Count);
                foreach (var token in postionsToken)
                {
                    float val = token.ToObject<float>();
                    mPositions.Append(val);
                }

                mColorTable = new ColorLookupTable();
                mColorTable.SetMaxValue(2000);
                mColorTable.SetColorMap(ColorMapKeyword.Create(EnumSystemColorMap.Rainbow));
                mColors = new Float32Buffer((uint)postionsToken.Count);

                var pressureToken = obj["data"]["attributes"]["pressure"]["array"].Children().ToList();
                mPressures = new Float32Buffer(0);
                mPressures.Reserve((uint)pressureToken.Count);
                uint idx = 0;
                foreach(var token in pressureToken)
                {
                    float val = token.ToObject<float>();
                    mPressures.Append(val);

                    var clr = mColorTable.GetColor(val);
                    mColors.SetValue(idx * 3, clr);
                    ++idx;
                }
            }

            return true;
        }
        public override void Run(RenderControl render)
        {
            if (!ReadData())
                return;

            var material = MeshPhongMaterial.Create("cae-material");
            material.GetTemplate().SetVertexColors(true);
            material.SetFaceSide(EnumFaceSide.DoubleSide);


            var position = BufferAttribute.Create(EnumAttributeSemantic.Position, EnumAttributeComponents.Three, mPositions);
            var color = BufferAttribute.Create(EnumAttributeSemantic.Color, EnumAttributeComponents.Three, mColors);

            BufferGeometry geometry = new BufferGeometry();
            geometry.AddAttribute(position);
            geometry.AddAttribute(color);

            NormalCalculator.ComputeVertexNormals(geometry);

            var node = new PrimitiveSceneNode(geometry, EnumPrimitiveType.TRIANGLES, material);

            node.SetPickable(false);

            PaletteWidget pw = new PaletteWidget();
            pw.Update(mColorTable);

            render.ShowSceneNode(pw);

            render.ShowSceneNode(node);
        }
    }
}
