using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCAD.Foundation;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalInstance.Initialize();

            WindowCanvas canvas = new WindowCanvas("AnyCAD", false);
            canvas.Initialize(0, 600, 400);
            

            var box = ShapeBuilder.MakeBox(GP.XOY(), 10, 20, 30);
            var node = BrepSceneNode.Create(box, null, null, 0.01);
            var scene = canvas.GetContext().GetScene();
            scene.AddNode(node);
            //canvas.Run();
            scene.UpdateWorld();
            canvas.ZoomToExtend();
            canvas.Redraw(0);
            canvas.CaptureScreenShot("image.bmp");
            canvas.Destroy();

            GlobalInstance.Destroy();
        }
    }
}
