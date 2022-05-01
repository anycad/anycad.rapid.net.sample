
using System.Collections.Generic;

namespace AnyCAD.Robot
{
    public class Arm
    {
        public string Name { get; set; }
        /// <summary>
        /// 关节参数列表
        /// </summary>
        public List<Joint> Joints { get; set; }
        public Arm()
        {
            Name = "";
            Joints = new List<Joint>();
        }
    }
}
