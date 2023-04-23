using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnyCAD.Forms;
using AnyCAD.Foundation;

namespace AnyCAD.Demo
{
    public partial class TransformDlg : Form
    {
        private RenderControl renderView;

        public TransformDlg()
        {
            InitializeComponent();

            renderView = new RenderControl(this.splitContainer1.Panel2);
        }

        private void PreviewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            renderView.DestroyRenderer();
            renderView = null;
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            var shape = SketchBuilder.MakeLine(new GPnt(), new GPnt(100, 100, 0));
            var node1 = BrepSceneNode.Create(shape, null, null);

            shape = SketchBuilder.MakeLine(new GPnt(), new GPnt(100, 100, 100));
            var node2 = BrepSceneNode.Create(shape, null, null);

            var group = new GroupSceneNode();
            group.AddNode(node1);
            group.AddNode(node2);

            renderView.ShowSceneNode(group);

            renderView.ZoomAll();

            var root = treeView1.Nodes.Add("Group");
            root.Tag = group;

            var n1 = root.Nodes.Add("L1");
            n1.Tag = node1;

            var n2 = root.Nodes.Add("L2");
            n2.Tag = node2;

            treeView1.ExpandAll();

            renderView.ShowCoordinateGrid(true);
        }

        private void 移动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("Move");
        }

        private void 旋转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("Rotate");
        }
        private void 选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("Pick");
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treeView1.SelectedNode == null)
                return;

            var node =  treeView1.SelectedNode.Tag as SceneNode;

            var pick = new PickedItem(node, new IntersectPoint(EnumShapeFilter.Zero, 0));
            var ss = renderView.ViewContext.GetSelectionManager().GetSelection();
            ss.Clear();
            ss.Add(pick, true);
            renderView.RequestDraw();
        }


    }
}
