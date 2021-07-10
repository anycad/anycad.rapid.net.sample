using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;



namespace AnyCAD.Demo.Analysis
{
    class Analysis_Points : TestCase
    {
        static Dictionary<uint, Dictionary<uint, float>> mData = null;
        static uint mMinX = uint.MaxValue;
        static uint mMaxX = uint.MinValue;
        static uint mMinY = uint.MaxValue;
        static uint mMaxY = uint.MinValue;
        bool ReadData()
        {
            if (mData != null)
                return true;

            string fileName = GetResourcePath("data/XYZ.txt");
            using (StreamReader reader = File.OpenText(fileName))
            {
                mData = new Dictionary<uint, Dictionary<uint, float>>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var items = line.Split('\t');
                    if (items.Length != 3)
                        continue;

                    uint x = uint.Parse(items[0]);
                    uint y = uint.Parse(items[1]);
                    float z = float.Parse(items[2]);

                    if (mMinX > x) mMinX = x;
                    if (mMaxX < x) mMaxX = x;
                    if (mMinY > y) mMinY = y;
                    if (mMaxY < y) mMaxY = y;

                    Dictionary<uint, float> yData = null;
                    if(!mData.TryGetValue(x, out yData))
                    {
                        yData = new Dictionary<uint, float>();
                        mData[x] = yData;
                    }

                    yData[y] = z;
                }
            }

            return true;
        }
        public override void Run(RenderControl render)
        {
            if (!ReadData())
                return;

            var matplot = Matplot.Create("MyMatlab 2020");

            var xRange = new PlotRange(mMinX,  mMaxX-1, 1);
            var yRange = new PlotRange(mMinY, mMaxY-1, 1);
            matplot.AddSurface(xRange, yRange, (idxU, idxV, u, v) =>
            {
                double x = u;
                double y = v;
                double z = mData[idxU+1][idxV+1];

                return new GPnt(x, y, z);
            });

            var node = matplot.Build(ColorMapKeyword.Create(EnumSystemColorMap.Cooltowarm));
            node.SetPickable(false);

            var pw = new PaletteWidget();
            pw.Update(matplot.GetColorTable());

            render.ShowSceneNode(pw);
            render.ShowSceneNode(node);

            var material = MeshPhongMaterial.Create("font-x");
            material.SetColor(ColorTable.Red);
            var shape = FontManager.Instance().CreateMesh("Create a better world!");
            shape.SetMaterial(material);
            var text = new TextSceneNode(shape, 24, true);
            //var text = new TextSceneNode("Wow", 24, new Vector3(1, 1, 0), ColorTable.Red, false);
            var tag = TagNode2D.Create(text, new Vector3(15), new Vector3(0));
            
            render.ShowSceneNode(tag);
        }
    }
}
