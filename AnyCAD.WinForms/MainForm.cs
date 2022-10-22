using AnyCAD.Exchange;
using AnyCAD.Forms;
using AnyCAD.Foundation;
using System;
using System.Windows.Forms;

namespace AnyCAD.Demo
{
    public partial class MainForm : Form
    {
        RenderControl mRenderView;

        public MainForm()
        {
            InitializeComponent();

            mRenderView = new RenderControl(this.splitContainer1.Panel2);

            TestCase.Register(this.treeView1);
        }

        bool mEnableAnimation = true;
        uint mSelectedItm = 0;
        private void MainForm_Load(object sender, EventArgs e)
        {

            // Selection changed
            mRenderView.SetSelectCallback((PickedResult result) =>
            {
                mSelectedItm = 0;
                this.listBox1.Items.Clear();

                if (result.IsEmpty())
                    return;
                var item = result.GetItem();
                if (item.GetNode() == null)
                    return;

                TestCase.SelectionChanged(mRenderView, result);

                this.listBox1.Items.Add(item.GetNode().GetType().Name);
                mSelectedItm = item.GetNodeId();
                this.listBox1.Items.Add(String.Format("NodeId: {0}", item.GetNodeId()));
                this.listBox1.Items.Add(String.Format("UserId: {0}", item.GetUserId()));
                this.listBox1.Items.Add(item.GetPoint().GetPosition().ToString());
                this.listBox1.Items.Add(item.GetShapeType().ToString());
                this.listBox1.Items.Add(String.Format("SubIndex: {0}", item.GetShapeIndex()));
                this.listBox1.Items.Add(String.Format("PrimitiveIndex: {0}", item.GetPoint().GetPrimitiveIndex()));
                this.listBox1.Items.Add(String.Format("TopoShapeId: {0}", item.GetTopoShapeId()));

                // 获取圆弧信息的例子
                var node = BrepSceneNode.Cast(item.GetNode());
                if(node != null)
                {
                    var shape = node.GetTopoShape();

                    if(item.GetShapeType() == EnumShapeFilter.Edge)
                    {
                        var subShape = shape.FindChild(EnumTopoShapeType.Topo_EDGE, (int)item.GetTopoShapeId());
                        if(subShape != null)
                        {
                            var curve = new ParametricCurve(subShape);
                            if(curve.IsValidGeometry())
                            {
                                if(curve.GetCurveType() == EnumCurveType.CurveType_Circle)
                                {
                                    var circle = curve.TryCircle();
                                    var axis = circle.Axis();
                                    var center = axis.Location();
                                    var dir = axis.Direction();
                                    var radius = circle.Radius();
                                }
                            }
                        }
                    }
                    
                }
            });

            mRenderView.SetAnimationCallback((float timer) =>
            {
                if(mEnableAnimation)
                    TestCase.RunAnimation(mRenderView, timer);
            });

            
            //mRenderView.GetContext().GetSelection().SelectSubShape(mRenderView.GetScene(), nodeId, type, shapeIndex);

            //var itr = mRenderView.GetContext().GetSelection().GetSelection().CreateIterator();
            //for(;itr.More();itr.Next())
            //{
            //    var item = itr.Current();
            //}
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.ClearAll();
        }

        private void captureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Image (*.png)|*.png";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var ss = mRenderView.CreateScreenShot();
            ss.SaveFile(dialog.FileName);
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = this.treeView1.SelectedNode;
            if (node == null)
                return;
            TestCase.CreateTest(node.Tag, mRenderView);
        }

        private void zoomAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.ZoomAll(1.2f);
        }

        private void projectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SwitchProjectionType();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = SceneIO.FormatFilters();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            var node = SceneIO.Load(dlg.FileName);
            if (node == null)
                return;
            
            mRenderView.ShowSceneNode(node);
            mRenderView.ZoomAll();
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SetStandardView(EnumStandardView.DefaultView);
        }

        private void frontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SetStandardView(EnumStandardView.Front);
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SetStandardView(EnumStandardView.Back);
        }

        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SetStandardView(EnumStandardView.Top);
        }

        private void bottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SetStandardView(EnumStandardView.Bottom);
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SetStandardView(EnumStandardView.Right);
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SetStandardView(EnumStandardView.Left);
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            mRenderView.SetBackgroundColor(dlg.Color.R / 255.0f, dlg.Color.G / 255.0f, dlg.Color.B / 255.0f, 1);
        }
        private void backgroundImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            var texture = ImageTexture2D.Create(dlg.FileName);
            var background = new ImageBackground(texture);

            mRenderView.GetViewer().SetBackground(background);

        }

        private void backgroundSkyBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var background = SkyboxBackground.Create("cloudy");
            mRenderView.GetViewer().SetBackground(background);
        }
        private void mouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ctx = mRenderView.GetContext();
            // change orbit  to Left and middle buttons
            ctx.SetOrbitButton(EnumMouseButton.LeftMiddle);
            // change pan operation to right button
            ctx.SetPanButton(EnumMouseButton.Right);
        }

        bool bShowTooltip = false;
        private void toolTipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bShowTooltip)
            {
                mRenderView.SetHilightingCallback(null);
            }
            else
            {
                mRenderView.SetHilightingCallback((PickedResult result) =>
                {
                    var text = result.GetItem().GetPoint().GetPosition().ToString();
                    this.mRenderView.SetToolTip(text);
                    return true;
                });
            }

            bShowTooltip = !bShowTooltip;
        }

        private void filterEdgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ctx = mRenderView.GetContext();
            ctx.ClearDisplayFilter(EnumShapeFilter.VertexEdgeFace);
            ctx.AddDisplayFilter(EnumShapeFilter.Edge);
        }

        private void filterFaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ctx = mRenderView.GetContext();
            ctx.ClearDisplayFilter(EnumShapeFilter.VertexEdgeFace);
            ctx.AddDisplayFilter(EnumShapeFilter.Face);
        }

        private void filterResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ctx = mRenderView.GetContext();
            ctx.ResetDisplayFilters();
        }

        bool bShowCoordinateGrid = false;
        private void coordinateGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bShowCoordinateGrid = !bShowCoordinateGrid;
            mRenderView.ShowCoordinateGrid(bShowCoordinateGrid);
        }

        bool bEnableDepathTest = false;
        private void depthTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bEnableDepathTest = !bEnableDepathTest;
            mRenderView.GetContext().GetSelection().SetDepthTest(bEnableDepathTest);
            mRenderView.RequestDraw(EnumUpdateFlags.Camera);
        }

        private void clipBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.ExecuteCommand("ClipBox");
        }

        private void orbitCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.GetContext().SetUserOrbitPivot(Vector3.Zero);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            TestCase.IncreaseCounter(mRenderView, 0);
        }

        private void sTLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDlg = new FolderBrowserDialog();
            if (fbDlg.ShowDialog() != DialogResult.OK)
                return;
            string myDir = fbDlg.SelectedPath;
            foreach (string fileName in System.IO.Directory.GetFiles(myDir))
            {
                var shape = SceneIO.Load(fileName);
                mRenderView.ShowSceneNode(shape);
            }
        }

        private void useViewAixsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SetViewCube(EnumViewCoordinateType.Axis);
        }

        private void useViewCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SetViewCube(EnumViewCoordinateType.Cube);
        }

        private void noneCoordinateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.SetViewCube(EnumViewCoordinateType.Empty);
        }

        private void openSTEPToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "IGES (*.igs;*.iges)|*.igs;*.iges|STEP (*.stp;*.step)|*.stp;*.step";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            CADReader doc = new CADReader();
            doc.Open(dialog.FileName, (XdeNode xn, TopoShape shape, GTrsf trf, Vector3 color) =>
            {
                mRenderView.ShowShape(shape.Transformed(trf), color);
            });

            mRenderView.ZoomAll();
        }

        private void openFastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Models (*.igs;*.iges;*.stp;*.step;*.brep;*.stl)|*.igs;*.iges;*.stp;*.step;*.brep;*.stl";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var shape = ShapeIO.Open(dialog.FileName);
            if (shape == null)
                return;

            var solidList = shape.GetChildren(EnumTopoShapeType.Topo_SOLID);
            if(solidList.Count > 0)
            {
                foreach(var solid in solidList)
                {
                    mRenderView.ShowShape(solid, new Vector3(0.8f));
                }
            }
            else
            {
                mRenderView.ShowShape(shape, new Vector3(0.8f));
            }


            mRenderView.ZoomAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mSelectedItm < 1)
                return;
            mRenderView.GetScene().RemoveNode(mSelectedItm);
            mRenderView.GetContext().GetSelection().Clear();
            mRenderView.RequestDraw(EnumUpdateFlags.Scene);
        }

        private void explosureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add Exposure
            //var settings = mRenderView.GetContext().GetRenderSettings();
            //settings.SetToneMapping(EnumToneMapping.LinearToneMapping);
            //settings.SetToneMappingExposure(1.5f);

            //mRenderView.RequestDraw(EnumUpdateFlags.Material);
        }

        bool mContactShadow = false;
        private void contactShadowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mContactShadow = !mContactShadow;

            var settings = mRenderView.GetContext().GetRenderSettings();
            //settings.SetShadowMapEnabled(mContactShadow);
            settings.SetContactShadow(mContactShadow);
            settings.SetContactShadowBlur(5.0f);
            mRenderView.RequestDraw(EnumUpdateFlags.Light);
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.ExecuteCommand("Move");
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.ExecuteCommand("Rotate");
        }

        private void rectZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.ExecuteCommand("RectZoom");
        }

        private void 框选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.GetContext().SetRectPick(true);
        }

        private void 单选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRenderView.GetContext().SetRectPick(false);
        }
    }
}
