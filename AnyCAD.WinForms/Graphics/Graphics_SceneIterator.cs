using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Forms;
using AnyCAD.Foundation;
using System.IO.Compression;
using System.IO;
using System.Windows.Forms;

namespace AnyCAD.Demo.Graphics
{
    class Graphics_SceneIterator : AnyCAD.Demo.Geometry.Geometry_Boolean
    {
   
        public override void Run(RenderControl render)
        {
            base.Run(render);

            try
            {
                // Let's iterate the scene
                using (FileStream fs = new FileStream("d:/myZip.zip", FileMode.Create))
                {
                    using (ZipArchive zipArchive = new ZipArchive(fs, ZipArchiveMode.Create))
                    {
                        for (var itr = render.GetScene().CreateIterator(); itr.More(); itr.Next())
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
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("d:/myZip.zip");
        }
    }
}
