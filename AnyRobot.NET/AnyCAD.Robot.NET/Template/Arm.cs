
using System.Collections.Generic;

namespace AnyCAD.Robot.Template
{
    /// <summary>
    /// 机械臂
    /// </summary>
    internal class Arm
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 关节参数列表
        /// </summary>
        public List<Link> Links { get; set; } = new List<Link>();
        public Arm()
        {

        }
    }
}
