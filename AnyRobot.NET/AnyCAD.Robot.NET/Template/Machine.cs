using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace AnyCAD.Robot.Template
{
    internal class Machine
    {
        #region Serialize
        /// <summary>
        /// 作者
        /// </summary>
        public string Author{ get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
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

        #endregion

        private EnumModelType mModelType = EnumModelType.BREP;
        public Machine()
        {
            Author = "AnyCAD";
            Name = "Robot";
            
            Description = "";
            ModelPath = ".";
            mModelType = EnumModelType.BREP;
            Arms = new List<Arm>();
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

        public SceneNode LoadModel(string fileName, EnumModelType type, MaterialInstance material)
        {
            if(type == EnumModelType.Mesh)
            {
                STLReader reader = new STLReader();
                var node = reader.Load(fileName, material);
                if (node == null)
                {
                    node = SceneIO.Load(fileName);
                }

                return node;
            }
            else
            {
                var shape = ShapeIO.Open(fileName);
                if (shape == null)
                {
                    throw new InvalidDataException(String.Format("Failed to open {0}", fileName));
                }
                var node = BrepSceneNode.Create(shape, material, null, 1);
                node.SetSubShapePickable(false);
                return node;
            }
        }

        private Dictionary<string, MeshPhongMaterial> mMaterials = new Dictionary<string, MeshPhongMaterial>();
        public MeshPhongMaterial FindMaterial(Color clr)
        {
            var id = clr.ToString();
            mMaterials.TryGetValue(id, out MeshPhongMaterial material);
            if(material != null)
                return material;
            material = MeshPhongMaterial.Create("robot");
            material.SetColor(clr.To());
            material.SetShininess(200);
            material.SetSpecular(new Vector3(0.9f, 0.8f, 0.2f));

            mMaterials.Add(id, material);

            return material;
        }


        public RobotBody CreateInstance()
        {
            var robot = new RobotBody();
            foreach (var arm in Arms)
            {
                var robotArm = new RobotArm();
                robotArm.SetName(arm.Name);

                var builder = new RobotFrameBuilder();
                for (int ii = 0; ii < arm.Links.Count; ++ii)
                {
                    var joint = arm.Links[ii];
                    builder.Add(joint.GetJointType(), joint.DH.Alpha, joint.DH.A, joint.DH.D, joint.DH.Theta);
                }
                builder.Build(EnumDHComputeMethod.Modified_DH);

                for (uint ii = 0; ii < builder.GetCount(); ++ii)
                {
                    var idx = robotArm.AddJoint(builder.GetJointType(ii), builder.GetFrame(ii));
                    if (ii < arm.Links.Count)
                    {
                        var joint = arm.Links[(int)ii];
                        robotArm.AddLink(idx, joint.Visual.Clone());
                    }
                }
                robotArm.Apply();
                robot.AddArm(robotArm);
            }


            robot.Reset();

            return robot;
        }
    }
}
