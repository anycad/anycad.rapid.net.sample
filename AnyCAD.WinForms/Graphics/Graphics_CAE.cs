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
        static MemoryImage mImage;
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

                var colorMap = ColorMapKeyword.Create(EnumSystemColorMap.Rainbow);
                var colorTable = new ColorLookupTable();
                colorTable.SetMaxValue(2000);
                colorTable.Update(colorMap);
                mColors = new Float32Buffer((uint)postionsToken.Count);

                var pressureToken = obj["data"]["attributes"]["pressure"]["array"].Children().ToList();
                mPressures = new Float32Buffer(0);
                mPressures.Reserve((uint)pressureToken.Count);
                uint idx = 0;
                foreach(var token in pressureToken)
                {
                    float val = token.ToObject<float>();
                    mPressures.Append(val);

                    var clr = colorTable.GetColor(val);
                    mColors.SetValue(idx * 3, clr);
                    ++idx;
                }

                mImage = colorTable.CreateImage(colorMap);
            }

            return true;
        }
        public override void Run(RenderControl render)
        {
            if (!ReadData())
                return;

            var materialManager = render.GetMaterialManager();
            var material = MeshPhongMaterial.Create(materialManager, "cae-material");
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


            var rainbowMaterial = BasicMaterial.Create(materialManager, "cae-rainbow");
            rainbowMaterial.SetUniform("diffuse", Uniform.Create(new Vector3(1)));

            var texture = new ImageTexture2D();
            texture.SetSource(mImage);

            rainbowMaterial.AddTexture("map", texture);


            var plane = GeometryBuilder.CreatePlane(25, 200);

            var rainbow = new PrimitiveSceneNode(plane, rainbowMaterial);

            var overlay = new SceneNode2D();
            overlay.SetNode(rainbow);
            overlay.SetOrigin(new ViewPosition(new Vector2(0.02f, 0.98f), EnumPositionType.Relative));
            overlay.SetSize(new ViewPosition(new Vector2(50, -200), EnumPositionType.Absolute));

            render.ShowSceneNode(overlay);

            render.ShowSceneNode(node);
        }
    }
}
