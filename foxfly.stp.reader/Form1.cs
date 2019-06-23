using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using foxfly;

namespace foxfly.stp.reader
{
    public partial class Form1 : Form
    {
        private XdeDocument doc;
        public Form1()
        {
            InitializeComponent();
        }

        private void AddNode(TreeNode node, XdeLabel label, vec3 color)
        {
            XdeLabelIterator itr = new XdeLabelIterator(label);
            for (; itr.More(); itr.Next())
            {
                var childLabel = itr.Value();
                var name = childLabel.GetName();
                if(name.Length == 0)
                {
                    name = String.Format("{0}",childLabel.GetTag());
                }

                var childColor = color;
                var shape =  doc.GetShape(childLabel);
                if(shape != null)
                {
                    var shapeType = shape.GetShapeType();
                    if (shapeType == EnumTopoShapeType.Topo_EDGE || shapeType == EnumTopoShapeType.Topo_WIRE)
                    {
                        childColor = doc.GetEdgeColor(shape, childColor);
                    }
                    else
                    {
                        childColor = doc.GetFaceColor(shape, childColor);
                    }

                    name = name + String.Format(" RGB({0}, {1}, {2})", childColor.x, childColor.y, childColor.z);
                }

                var childNode = node.Nodes.Add(name);

                if (childLabel.HasChild())
                    AddNode(childNode, childLabel, childColor);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "STEP File(*.stp;*.step)|*.stp;*.step|All Files(*.*)|*.*";

            if (DialogResult.OK != dlg.ShowDialog())
                return;

            doc = new XdeDocument();
            if (!doc.Open(dlg.FileName, EnumXdeDocType.STEP))
                return;

            var root = doc.GetMainLabel();
            if (root.IsNullLabel())
                return;
            var name = root.GetName();
            if (name.Length == 0)
                name = dlg.SafeFileName;
            var node = this.treeView1.Nodes.Add(name);
            vec3 color = new vec3(0.5f, 0.5f, 0.5f);
            AddNode(node, root, color);
        }
    }
}
