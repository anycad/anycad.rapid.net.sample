
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;

namespace AnyCAD.Robot.Template
{
    /// <summary>
    /// 关节
    /// </summary>
    internal class Link
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { 
            get { return mJointType.ToString();  }
            set { mJointType = GetJointType(value); }
        }
        /// <summary>
        /// 几何模型
        /// </summary>
        public List<Model> Model { get; set; } = new List<Model>();
        /// <summary>
        /// DH参数
        /// </summary>
        public JointDH DH { get; set; } = new JointDH();

        private EnumRobotJointType mJointType = EnumRobotJointType.Fixed;

        [NonSerialized]
        public SceneNode Visual;
        public Link()
        {
        }

        public EnumRobotJointType GetJointType()
        {
            return mJointType;
        }
        private EnumRobotJointType GetJointType(string val)
        {
            if (val == "Fixed")
                return EnumRobotJointType.Fixed;
            if(val == "Revolute")
                return EnumRobotJointType.Revolute;
            if(val == "Prismatic")
                return EnumRobotJointType.Prismatic;

            return EnumRobotJointType.Fixed;
        }  
        
        public void SetJointType(EnumRobotJointType type)
        {
            Type = type.ToString();
        }
    }
}
