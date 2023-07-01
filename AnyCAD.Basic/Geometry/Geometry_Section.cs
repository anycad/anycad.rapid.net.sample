using AnyCAD.Foundation;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AnyCAD.Demo.Geometry
{
    class Geometry_Section : TestCase
    {

        GDir _Dir = GP.DZ();
        TopoShape _Face;
        double _StartZ = 0;
        int _BatchSize = 2000;
        Task<TopoShapeList> CreateTask(int start, double step)
        {
            return Task.Factory.StartNew(
                    () =>
                    {
                        var pt = new GPnt(0, 0, _StartZ + start *_BatchSize * step);
                        var list = new TopoShapeList();
                        for (int jj = 0; jj < _BatchSize; ++jj, pt.z += step)
                        {
                            var plane = ShapeBuilder.MakeHalfSpace(pt, _Dir);
                            var wire = BooleanTool.Section(_Face, plane);
                            if (wire != null)
                            {
                                list.Add(wire);
                            }
                            else
                            {
                                break;
                            }
                        }

                        return list;
                    }
                    );
        }

        public override void Run(IRenderView render)
        {
            var shape = ShapeIO.Open(GetResourcePath("models/bottle.brep"));
            if (shape == null)
            {
                return;
            }

            _Face = shape.FindChild(EnumTopoShapeType.Topo_FACE, 15);
            var box = _Face.GetOptimalBBox();
            var range = box.CornerMax().z - box.CornerMin().z;
            var step = 0.01;
            var count = range / step;
            var batch = count / _BatchSize;
            _StartZ = box.CornerMin().z;

            List<Task<TopoShapeList>> taskList = new List<Task<TopoShapeList>>();
            for (int ii = 0; ii < batch; ii++)
            {
                taskList.Add(CreateTask(ii, step));
            }
            Task.WhenAll(taskList).Wait();

            foreach(var task in taskList)
            {
                var items = task.GetAwaiter().GetResult();
                foreach(var item in items)
                {
                    render.ShowShape(item, ColorTable.GreenYellow);
                }
            }

            render.ShowShape(shape, ColorTable.BrulyWood);
        }
    }
}
