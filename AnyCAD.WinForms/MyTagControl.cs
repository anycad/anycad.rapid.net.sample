using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyCAD.Demo
{
    public partial class MyTagControl : UserControl
    {
        public TagNode2D TagNode;
        public MyTagControl(RenderControl renderView, Vector3 worldPosition, Vector3 targetWorld)
        {
            InitializeComponent();

            TagNode = TagNode2D.Create(null, worldPosition, targetWorld);
            renderView.ShowSceneNode(TagNode);

            renderView.Controls.Add(this);
            this.BringToFront();
        }

        /// <summary>
        /// 根据需要调整位置
        /// </summary>
        public void UpdateLayout()
        {
            var position = TagNode.GetViewportPosition();
            var size = this.Parent.Size;
            this.Location = new Point(size.Width / 2 + (int)position.x - 20, size.Height / 2 - (int)position.y - 20);
        }
    }
}
