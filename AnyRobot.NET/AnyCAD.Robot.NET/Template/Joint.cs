
using AnyCAD.Foundation;
using System;

namespace AnyCAD.Robot
{
    public class Joint
    {
        public string Name { get; set; }
        public string Type { 
            get { return mJointType.ToString();  }
            set { mJointType = GetJointType(value); }
        }
        public string Model { get; set; }
        public JointDH DH { get; set; }
        public JointDH BiasDH { get; set; }

        private EnumRobotJointType mJointType = EnumRobotJointType.Fixed;

        [NonSerialized]
        public SceneNode Visual;
        public Joint()
        {
            Name = "";
            Model = "";
            DH = new JointDH();
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
