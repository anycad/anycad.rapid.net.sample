using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;



namespace AnyCAD.Demo.Graphics
{
    class Graphics_PointCloud : TestCase
    {
        static Float32Buffer mPositions;
        static Float32Buffer mColors;
        bool ReadData()
        {
            if (mPositions != null)
                return true;

            string fileName = GetResourcePath("cloud.xyz");
            using (StreamReader reader = File.OpenText(fileName))
            {
                mPositions = new Float32Buffer(0);
                mColors = new Float32Buffer(0);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var items = line.Split(' ');
                    if (items.Length != 6)
                        continue;

                    for (int ii = 0; ii < 3; ++ii)
                    {
                        mPositions.Append(float.Parse(items[ii]));
                    }

                    for (int ii = 3; ii < 6; ++ii)
                    {
                        mColors.Append(float.Parse(items[ii]) / 255.0f);
                    }
                }
            }

            return true;
        }
        public override void Run(RenderControl render)
        {
            if (!ReadData())
                return;
       

            PointCloud node = PointCloud.Create(mPositions, mColors, 1);

            render.ShowSceneNode(node);
        }
    }
}
