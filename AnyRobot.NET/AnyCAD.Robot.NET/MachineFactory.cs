using AnyCAD.Foundation;
using AnyCAD.Robot.Template;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AnyCAD.Robot
{
    public class MachineFactory
    {

        static public readonly string Filter = "AnyCAD Machine(*.mson)|*.mson";

        Dictionary<string, Machine> mTemplates = new Dictionary<string, Machine>();
        public MachineFactory() 
        {
        }

        public delegate void LoadProgressCallback(int progress);

        public string Load(string fileName, LoadProgressCallback callback)
        {
            callback(0);
            Machine matchine;
            using (StreamReader reader = new StreamReader(fileName))
            {
                var data = reader.ReadToEnd();
                matchine = JsonSerializer.Deserialize<Machine>(data);
            }
            if(matchine == null)
                return string.Empty;

            callback(10);
            matchine.ModelPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(fileName), matchine.ModelPath);

            int progress = 10;
            int step = 90 / matchine.Arms.Count;

            foreach (var arm in matchine.Arms)
            {
                int childStep = step / arm.Links.Count;
                foreach (var link in arm.Links)
                {
                    if (link.Model.Count == 1)
                    {
                        var model = link.Model[0];
                        var modelFile = System.IO.Path.Combine(matchine.ModelPath, model.File);
                        link.Visual = matchine.LoadModel(modelFile, matchine.GetModelType(), matchine.FindMaterial(model.Color));
                    }
                    else
                    {
                        var group = new GroupSceneNode();
                        foreach (var model in link.Model)
                        {
                            var modelFile = System.IO.Path.Combine(matchine.ModelPath, model.File);
                            var shape = ShapeIO.Open(modelFile);
                            if (shape == null)
                            {
                                throw new InvalidDataException(String.Format("Failed to open {0}", modelFile));
                            }

                            var material = matchine.FindMaterial(model.Color);
                            var node = BrepSceneNode.Create(shape, material, null, 1);
                            node.SetSubShapePickable(false);
                        }
                        link.Visual = group;
                    }

                    progress += childStep;
                    callback(progress);
                }
            }


            callback(100);

            mTemplates.Add(matchine.Name, matchine);

            return matchine.Name;
        }

        public RobotBody CreateInstance(string name)
        {
            mTemplates.TryGetValue(name, out var matchine);
            if (matchine == null)
                return null;
            return matchine.CreateInstance();
        }

        /// <summary>
        /// 生成一个6R机器人的模板文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public bool SaveSample(string fileName)
        {
            Machine machine = new Machine();
            machine.Description = "Sample project.";
            machine.Name = "XYZ";

            var arm = new Arm();
            arm.Links.Add(new Link { Name = "Base", Type = EnumRobotJointType.Fixed.ToString() });
            for (int ii = 1; ii < 7; ++ii)
            {
                arm.Links.Add(new Link
                {
                    Name = string.Format("AXIS{0}", ii),
                    Type = EnumRobotJointType.Revolute.ToString()
                });
            }

            machine.Arms.Add(arm);

            return machine.Save(fileName);
        }
    }
}
