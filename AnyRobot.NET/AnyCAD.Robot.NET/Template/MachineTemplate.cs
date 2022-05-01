using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace AnyCAD.Robot
{
    public class MachineTemplate
    {
        #region Serialize
        public string Author{ get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 模型文件相对于.robot文件的相对位置
        /// </summary>
        public string ModelPath { get; set; }
        /// <summary>
        /// 模型文件的类型。BREP: 表示的是.brep, .step, .iges格式; Mesh: 表示 stl, obj, 3ds等
        /// </summary>
        public string ModelType {
            get { return mModelType.ToString(); }
            set { mModelType = GetModelType(value); }
        }
        /// <summary>
        /// 机械臂列表
        /// </summary>
        public List<Arm> Arms { get; set; }
        /// <summary>
        /// 设置整体的位置、大小信息
        /// </summary>
        public Transform Transform { get; set; }

        /// <summary>
        /// 关节处轴的显示大小
        /// </summary>
        public float AxisSize { get; set; }
        #endregion

        private EnumModelType mModelType = EnumModelType.BREP;

        static public readonly string Filter = "AnyCAD Robot(*.robot)|*.robot";
        public MachineTemplate()
        {
            Author = "AnyCAD";
            Name = "Robot";
            
            Description = "";
            ModelPath = ".";
            AxisSize = 100;
            mModelType = EnumModelType.BREP;
            Arms = new List<Arm>();
            Transform = new Transform();
        }
        public EnumModelType GetModelType()
        {
            return mModelType;
        }
        private EnumModelType GetModelType(string type)
        {
            if (type == "BREP")
                return EnumModelType.BREP;
            return EnumModelType.Mesh;
        }

        public bool Save(string fileName)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var data = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, data);
            return true;
        }
        /// <summary>
        /// 生成一个6R机器人的模板文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public static bool SaveSample(string fileName)
        {
            MachineTemplate template = new MachineTemplate();
            template.Description = "Sample project.";
            template.Name = "XYZ";

            var arm = new Arm();
            arm.Joints.Add(new Joint { Name = "Base", Type= EnumRobotJointType.Fixed.ToString() });
            for (int ii=1; ii<7; ++ii)
            {
                arm.Joints.Add(new Joint {
                    Name= string.Format("AXIS{0}", ii),
                    Type = EnumRobotJointType.Revolute.ToString()
                });
            }

            template.Arms.Add(arm);

            return template.Save(fileName);
        }

        public delegate void LoadProgressCallback(int progress);
        public static MachineTemplate Load(string fileName, LoadProgressCallback callback)
        {
            callback(0);
            MachineTemplate template;
            using (StreamReader reader = new StreamReader(fileName))
            {
                var data = reader.ReadToEnd();
                template = JsonSerializer.Deserialize<MachineTemplate>(data);
            }

            callback(10);
            template.ModelPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(fileName),
                template.ModelPath);

            int progress = 10;
            int step = 90 / template.Arms.Count;

            if (template.GetModelType() == EnumModelType.BREP)
            {
                MeshStandardMaterial mMaterial = MeshStandardMaterial.Create("robot");
                mMaterial.SetColor(new Vector3(0.9f, 0.8f, 0.2f));
                mMaterial.SetMetalness(0.2f);
                mMaterial.SetRoughness(0.4f);

                foreach(var arm in template.Arms)
                {
                    int childStep = step / arm.Joints.Count;
                    foreach (var joint in arm.Joints)
                    {
                        var jointFileName = System.IO.Path.Combine(template.ModelPath, joint.Model);
                        var shape = ShapeIO.Open(jointFileName);
                        if (shape == null)
                        {
                            throw new InvalidDataException(String.Format("Failed to open {0}", jointFileName));
                        }

                        var node = BrepSceneNode.Create(shape, mMaterial, null, 1);
                        node.SetSubShapePickable(false);

                        joint.Visual = node;

                        progress += childStep;
                        callback(progress);
                    }
                }

            }
            else
            {
                foreach (var arm in template.Arms)
                {
                    int childStep = step / arm.Joints.Count;
                    foreach (var joint in arm.Joints)
                    {
                        var jointFileName = System.IO.Path.Combine(template.ModelPath, joint.Model);
                        var node = SceneIO.Load(jointFileName);
                        if (node == null)
                        {
                            throw new InvalidDataException(String.Format("Failed to open {0}", jointFileName));
                        }

                        joint.Visual = node;

                        progress += childStep;
                        callback(progress);
                    }
                }
            }
            callback(100);
            return template;
        }

        public RobotBody CreateInstance()
        {
            var robot = new RobotBody();
            foreach(var arm in Arms)
            {
                var robotArm = new RobotArm();
                robotArm.SetName(arm.Name);
                robotArm.SetAxisSize(AxisSize);
                for (int ii = 0; ii < arm.Joints.Count; ++ii)
                {
                    var joint = arm.Joints[ii];

                    robotArm.AddJoint(joint.GetJointType(), joint.DH.Alpha, joint.DH.A, joint.DH.D, joint.DH.Theta);
                    robotArm.AddLink((uint)ii, joint.Visual.Clone());
                }
                robotArm.ApplyDH();
                robot.AddArm(robotArm);
            }

            robot.Reset();

            return robot;
        }
    }
}
