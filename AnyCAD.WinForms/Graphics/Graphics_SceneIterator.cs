using AnyCAD.Foundation;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_SceneIterator : AnyCAD.Demo.Geometry.Geometry_Boolean
    {
   
        public override void Run(IRenderView render)
        {
            base.Run(render);

            try
            {
                // Let's iterate the scene
                using (FileStream fs = new FileStream("../../myZip.zip", FileMode.Create))
                {
                    using (ZipArchive zipArchive = new ZipArchive(fs, ZipArchiveMode.Create))
                    {
                        for (var itr = render.Scene.CreateIterator(); itr.More(); itr.Next())
                        {
                            var node = BrepSceneNode.Cast(itr.Current());
                            if (node == null)
                                continue;

                            ZipArchiveEntry entry = zipArchive.CreateEntry(String.Format("{0}.brep", node.GetUuid()));
                            using (StreamWriter writer = new StreamWriter(entry.Open(), Encoding.Default))
                            {
                                writer.Write(node.GetTopoShape().Write());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               DialogUtil.ShowMessageBox("Exception",ex.Message);
            }
        }
    }
}
