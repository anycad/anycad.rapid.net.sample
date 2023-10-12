namespace AnyCAD.Demo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openFastToolStripMenuItem = new ToolStripMenuItem();
            openSTEPToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            openAdvToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            clearToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            captureToolStripMenuItem = new ToolStripMenuItem();
            zoomAllToolStripMenuItem = new ToolStripMenuItem();
            projectionToolStripMenuItem = new ToolStripMenuItem();
            standardViewToolStripMenuItem = new ToolStripMenuItem();
            frontToolStripMenuItem = new ToolStripMenuItem();
            backToolStripMenuItem = new ToolStripMenuItem();
            topToolStripMenuItem = new ToolStripMenuItem();
            bottomToolStripMenuItem = new ToolStripMenuItem();
            rightToolStripMenuItem = new ToolStripMenuItem();
            leftToolStripMenuItem = new ToolStripMenuItem();
            dToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            backgroundColorToolStripMenuItem = new ToolStripMenuItem();
            backgroundImageToolStripMenuItem = new ToolStripMenuItem();
            backgroundSkyBoxToolStripMenuItem = new ToolStripMenuItem();
            coordinateGridToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripSeparator();
            useViewAixsToolStripMenuItem = new ToolStripMenuItem();
            useViewCubeToolStripMenuItem = new ToolStripMenuItem();
            noneCoordinateToolStripMenuItem = new ToolStripMenuItem();
            pickToolStripMenuItem = new ToolStripMenuItem();
            框选ToolStripMenuItem = new ToolStripMenuItem();
            单选ToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripSeparator();
            filterEdgeToolStripMenuItem = new ToolStripMenuItem();
            filterFaceToolStripMenuItem = new ToolStripMenuItem();
            filterResetToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            moveToolStripMenuItem = new ToolStripMenuItem();
            rotateToolStripMenuItem = new ToolStripMenuItem();
            commandToolStripMenuItem = new ToolStripMenuItem();
            clipBoxToolStripMenuItem = new ToolStripMenuItem();
            rectZoomToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            mouseToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            toolTipToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripSeparator();
            selectionToolStripMenuItem = new ToolStripMenuItem();
            depthTestToolStripMenuItem = new ToolStripMenuItem();
            orbitCenterToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripSeparator();
            explosureToolStripMenuItem = new ToolStripMenuItem();
            contactShadowToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            splitContainer2 = new SplitContainer();
            treeView1 = new TreeView();
            listBox1 = new ListBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(36, 36);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem, pickToolStripMenuItem, editToolStripMenuItem, commandToolStripMenuItem, settingsToolStripMenuItem, toolStripMenuItem6 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(2298, 49);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openFastToolStripMenuItem, openSTEPToolStripMenuItem, importToolStripMenuItem, openAdvToolStripMenuItem, saveToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(80, 43);
            fileToolStripMenuItem.Text = "File";
            // 
            // openFastToolStripMenuItem
            // 
            openFastToolStripMenuItem.Name = "openFastToolStripMenuItem";
            openFastToolStripMenuItem.Size = new Size(512, 48);
            openFastToolStripMenuItem.Text = "Open STEP/IGES";
            openFastToolStripMenuItem.Click += openFastToolStripMenuItem_Click;
            // 
            // openSTEPToolStripMenuItem
            // 
            openSTEPToolStripMenuItem.Name = "openSTEPToolStripMenuItem";
            openSTEPToolStripMenuItem.Size = new Size(512, 48);
            openSTEPToolStripMenuItem.Text = "Open STEP/IGES with color";
            openSTEPToolStripMenuItem.Click += openSTEPToolStripMenuItem_Click;
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new Size(512, 48);
            importToolStripMenuItem.Text = "Open STL";
            importToolStripMenuItem.Click += importToolStripMenuItem_Click;
            // 
            // openAdvToolStripMenuItem
            // 
            openAdvToolStripMenuItem.Name = "openAdvToolStripMenuItem";
            openAdvToolStripMenuItem.Size = new Size(512, 48);
            openAdvToolStripMenuItem.Text = "Open Adv...";
            openAdvToolStripMenuItem.Click += openAdvToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(512, 48);
            saveToolStripMenuItem.Text = "Save ...";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clearToolStripMenuItem, toolStripMenuItem1, captureToolStripMenuItem, zoomAllToolStripMenuItem, projectionToolStripMenuItem, standardViewToolStripMenuItem, toolStripMenuItem2, backgroundColorToolStripMenuItem, backgroundImageToolStripMenuItem, backgroundSkyBoxToolStripMenuItem, coordinateGridToolStripMenuItem, toolStripMenuItem4, useViewAixsToolStripMenuItem, useViewCubeToolStripMenuItem, noneCoordinateToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(98, 43);
            viewToolStripMenuItem.Text = "View";
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(420, 48);
            clearToolStripMenuItem.Text = "Clear";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(417, 6);
            // 
            // captureToolStripMenuItem
            // 
            captureToolStripMenuItem.Name = "captureToolStripMenuItem";
            captureToolStripMenuItem.Size = new Size(420, 48);
            captureToolStripMenuItem.Text = "Capture";
            captureToolStripMenuItem.Click += captureToolStripMenuItem_Click;
            // 
            // zoomAllToolStripMenuItem
            // 
            zoomAllToolStripMenuItem.Name = "zoomAllToolStripMenuItem";
            zoomAllToolStripMenuItem.Size = new Size(420, 48);
            zoomAllToolStripMenuItem.Text = "Zoom All";
            zoomAllToolStripMenuItem.Click += zoomAllToolStripMenuItem_Click;
            // 
            // projectionToolStripMenuItem
            // 
            projectionToolStripMenuItem.Name = "projectionToolStripMenuItem";
            projectionToolStripMenuItem.Size = new Size(420, 48);
            projectionToolStripMenuItem.Text = "Projection";
            projectionToolStripMenuItem.Click += projectionToolStripMenuItem_Click;
            // 
            // standardViewToolStripMenuItem
            // 
            standardViewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { frontToolStripMenuItem, backToolStripMenuItem, topToolStripMenuItem, bottomToolStripMenuItem, rightToolStripMenuItem, leftToolStripMenuItem, dToolStripMenuItem });
            standardViewToolStripMenuItem.Name = "standardViewToolStripMenuItem";
            standardViewToolStripMenuItem.Size = new Size(420, 48);
            standardViewToolStripMenuItem.Text = "StandardView";
            // 
            // frontToolStripMenuItem
            // 
            frontToolStripMenuItem.Name = "frontToolStripMenuItem";
            frontToolStripMenuItem.Size = new Size(259, 48);
            frontToolStripMenuItem.Text = "Front";
            frontToolStripMenuItem.Click += frontToolStripMenuItem_Click;
            // 
            // backToolStripMenuItem
            // 
            backToolStripMenuItem.Name = "backToolStripMenuItem";
            backToolStripMenuItem.Size = new Size(259, 48);
            backToolStripMenuItem.Text = "Back";
            backToolStripMenuItem.Click += backToolStripMenuItem_Click;
            // 
            // topToolStripMenuItem
            // 
            topToolStripMenuItem.Name = "topToolStripMenuItem";
            topToolStripMenuItem.Size = new Size(259, 48);
            topToolStripMenuItem.Text = "Top";
            topToolStripMenuItem.Click += topToolStripMenuItem_Click;
            // 
            // bottomToolStripMenuItem
            // 
            bottomToolStripMenuItem.Name = "bottomToolStripMenuItem";
            bottomToolStripMenuItem.Size = new Size(259, 48);
            bottomToolStripMenuItem.Text = "Bottom";
            bottomToolStripMenuItem.Click += bottomToolStripMenuItem_Click;
            // 
            // rightToolStripMenuItem
            // 
            rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            rightToolStripMenuItem.Size = new Size(259, 48);
            rightToolStripMenuItem.Text = "Right";
            rightToolStripMenuItem.Click += rightToolStripMenuItem_Click;
            // 
            // leftToolStripMenuItem
            // 
            leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            leftToolStripMenuItem.Size = new Size(259, 48);
            leftToolStripMenuItem.Text = "Left";
            leftToolStripMenuItem.Click += leftToolStripMenuItem_Click;
            // 
            // dToolStripMenuItem
            // 
            dToolStripMenuItem.Name = "dToolStripMenuItem";
            dToolStripMenuItem.Size = new Size(259, 48);
            dToolStripMenuItem.Text = "3D";
            dToolStripMenuItem.Click += dToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(417, 6);
            // 
            // backgroundColorToolStripMenuItem
            // 
            backgroundColorToolStripMenuItem.Name = "backgroundColorToolStripMenuItem";
            backgroundColorToolStripMenuItem.Size = new Size(420, 48);
            backgroundColorToolStripMenuItem.Text = "Background Color";
            backgroundColorToolStripMenuItem.Click += backgroundColorToolStripMenuItem_Click;
            // 
            // backgroundImageToolStripMenuItem
            // 
            backgroundImageToolStripMenuItem.Name = "backgroundImageToolStripMenuItem";
            backgroundImageToolStripMenuItem.Size = new Size(420, 48);
            backgroundImageToolStripMenuItem.Text = "Background Image";
            backgroundImageToolStripMenuItem.Click += backgroundImageToolStripMenuItem_Click;
            // 
            // backgroundSkyBoxToolStripMenuItem
            // 
            backgroundSkyBoxToolStripMenuItem.Name = "backgroundSkyBoxToolStripMenuItem";
            backgroundSkyBoxToolStripMenuItem.Size = new Size(420, 48);
            backgroundSkyBoxToolStripMenuItem.Text = "Background SkyBox";
            backgroundSkyBoxToolStripMenuItem.Click += backgroundSkyBoxToolStripMenuItem_Click;
            // 
            // coordinateGridToolStripMenuItem
            // 
            coordinateGridToolStripMenuItem.Name = "coordinateGridToolStripMenuItem";
            coordinateGridToolStripMenuItem.Size = new Size(420, 48);
            coordinateGridToolStripMenuItem.Text = "Coordinate Grid";
            coordinateGridToolStripMenuItem.Click += coordinateGridToolStripMenuItem_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(417, 6);
            // 
            // useViewAixsToolStripMenuItem
            // 
            useViewAixsToolStripMenuItem.Name = "useViewAixsToolStripMenuItem";
            useViewAixsToolStripMenuItem.Size = new Size(420, 48);
            useViewAixsToolStripMenuItem.Text = "Use ViewAixs";
            useViewAixsToolStripMenuItem.Click += useViewAixsToolStripMenuItem_Click;
            // 
            // useViewCubeToolStripMenuItem
            // 
            useViewCubeToolStripMenuItem.Name = "useViewCubeToolStripMenuItem";
            useViewCubeToolStripMenuItem.Size = new Size(420, 48);
            useViewCubeToolStripMenuItem.Text = "Use ViewCube";
            useViewCubeToolStripMenuItem.Click += useViewCubeToolStripMenuItem_Click;
            // 
            // noneCoordinateToolStripMenuItem
            // 
            noneCoordinateToolStripMenuItem.Name = "noneCoordinateToolStripMenuItem";
            noneCoordinateToolStripMenuItem.Size = new Size(420, 48);
            noneCoordinateToolStripMenuItem.Text = "None Coordinate";
            noneCoordinateToolStripMenuItem.Click += noneCoordinateToolStripMenuItem_Click;
            // 
            // pickToolStripMenuItem
            // 
            pickToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 框选ToolStripMenuItem, 单选ToolStripMenuItem, toolStripMenuItem7, filterEdgeToolStripMenuItem, filterFaceToolStripMenuItem, filterResetToolStripMenuItem });
            pickToolStripMenuItem.Name = "pickToolStripMenuItem";
            pickToolStripMenuItem.Size = new Size(90, 43);
            pickToolStripMenuItem.Text = "Pick";
            // 
            // 框选ToolStripMenuItem
            // 
            框选ToolStripMenuItem.Name = "框选ToolStripMenuItem";
            框选ToolStripMenuItem.Size = new Size(305, 48);
            框选ToolStripMenuItem.Text = "框选";
            框选ToolStripMenuItem.Click += 框选ToolStripMenuItem_Click;
            // 
            // 单选ToolStripMenuItem
            // 
            单选ToolStripMenuItem.Name = "单选ToolStripMenuItem";
            单选ToolStripMenuItem.Size = new Size(305, 48);
            单选ToolStripMenuItem.Text = "单选";
            单选ToolStripMenuItem.Click += 单选ToolStripMenuItem_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(302, 6);
            // 
            // filterEdgeToolStripMenuItem
            // 
            filterEdgeToolStripMenuItem.Name = "filterEdgeToolStripMenuItem";
            filterEdgeToolStripMenuItem.Size = new Size(305, 48);
            filterEdgeToolStripMenuItem.Text = "Filter Edge";
            // 
            // filterFaceToolStripMenuItem
            // 
            filterFaceToolStripMenuItem.Name = "filterFaceToolStripMenuItem";
            filterFaceToolStripMenuItem.Size = new Size(305, 48);
            filterFaceToolStripMenuItem.Text = "Filter Face";
            // 
            // filterResetToolStripMenuItem
            // 
            filterResetToolStripMenuItem.Name = "filterResetToolStripMenuItem";
            filterResetToolStripMenuItem.Size = new Size(305, 48);
            filterResetToolStripMenuItem.Text = "Filter Reset";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { deleteToolStripMenuItem, moveToolStripMenuItem, rotateToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(86, 43);
            editToolStripMenuItem.Text = "Edit";
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(248, 48);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // moveToolStripMenuItem
            // 
            moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            moveToolStripMenuItem.Size = new Size(248, 48);
            moveToolStripMenuItem.Text = "Move";
            moveToolStripMenuItem.Click += moveToolStripMenuItem_Click;
            // 
            // rotateToolStripMenuItem
            // 
            rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            rotateToolStripMenuItem.Size = new Size(248, 48);
            rotateToolStripMenuItem.Text = "Rotate";
            rotateToolStripMenuItem.Click += rotateToolStripMenuItem_Click;
            // 
            // commandToolStripMenuItem
            // 
            commandToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clipBoxToolStripMenuItem, rectZoomToolStripMenuItem });
            commandToolStripMenuItem.Name = "commandToolStripMenuItem";
            commandToolStripMenuItem.Size = new Size(171, 43);
            commandToolStripMenuItem.Text = "Command";
            // 
            // clipBoxToolStripMenuItem
            // 
            clipBoxToolStripMenuItem.Name = "clipBoxToolStripMenuItem";
            clipBoxToolStripMenuItem.Size = new Size(296, 48);
            clipBoxToolStripMenuItem.Text = "ClipBox";
            clipBoxToolStripMenuItem.Click += clipBoxToolStripMenuItem_Click;
            // 
            // rectZoomToolStripMenuItem
            // 
            rectZoomToolStripMenuItem.Name = "rectZoomToolStripMenuItem";
            rectZoomToolStripMenuItem.Size = new Size(296, 48);
            rectZoomToolStripMenuItem.Text = "RectZoom";
            rectZoomToolStripMenuItem.Click += rectZoomToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mouseToolStripMenuItem, toolStripMenuItem3, toolTipToolStripMenuItem, toolStripMenuItem5, selectionToolStripMenuItem, orbitCenterToolStripMenuItem, toolStripMenuItem8, explosureToolStripMenuItem, contactShadowToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(142, 43);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // mouseToolStripMenuItem
            // 
            mouseToolStripMenuItem.Name = "mouseToolStripMenuItem";
            mouseToolStripMenuItem.Size = new Size(375, 48);
            mouseToolStripMenuItem.Text = "Mouse";
            mouseToolStripMenuItem.Click += mouseToolStripMenuItem_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(372, 6);
            // 
            // toolTipToolStripMenuItem
            // 
            toolTipToolStripMenuItem.Name = "toolTipToolStripMenuItem";
            toolTipToolStripMenuItem.Size = new Size(375, 48);
            toolTipToolStripMenuItem.Text = "ToolTip";
            toolTipToolStripMenuItem.Click += toolTipToolStripMenuItem_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(372, 6);
            // 
            // selectionToolStripMenuItem
            // 
            selectionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { depthTestToolStripMenuItem });
            selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
            selectionToolStripMenuItem.Size = new Size(375, 48);
            selectionToolStripMenuItem.Text = "Selection";
            // 
            // depthTestToolStripMenuItem
            // 
            depthTestToolStripMenuItem.Name = "depthTestToolStripMenuItem";
            depthTestToolStripMenuItem.Size = new Size(296, 48);
            depthTestToolStripMenuItem.Text = "DepthTest";
            depthTestToolStripMenuItem.Click += depthTestToolStripMenuItem_Click;
            // 
            // orbitCenterToolStripMenuItem
            // 
            orbitCenterToolStripMenuItem.Name = "orbitCenterToolStripMenuItem";
            orbitCenterToolStripMenuItem.Size = new Size(375, 48);
            orbitCenterToolStripMenuItem.Text = "Orbit Center";
            orbitCenterToolStripMenuItem.Click += orbitCenterToolStripMenuItem_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(372, 6);
            // 
            // explosureToolStripMenuItem
            // 
            explosureToolStripMenuItem.Name = "explosureToolStripMenuItem";
            explosureToolStripMenuItem.Size = new Size(375, 48);
            explosureToolStripMenuItem.Text = "Exposure";
            explosureToolStripMenuItem.Click += explosureToolStripMenuItem_Click;
            // 
            // contactShadowToolStripMenuItem
            // 
            contactShadowToolStripMenuItem.Name = "contactShadowToolStripMenuItem";
            contactShadowToolStripMenuItem.Size = new Size(375, 48);
            contactShadowToolStripMenuItem.Text = "Contact Shadow";
            contactShadowToolStripMenuItem.Click += contactShadowToolStripMenuItem_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(57, 43);
            toolStripMenuItem6.Text = "+";
            toolStripMenuItem6.Click += toolStripMenuItem6_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 49);
            splitContainer1.Margin = new Padding(7, 8, 7, 8);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(statusStrip1);
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(2298, 1422);
            splitContainer1.SplitterDistance = 423;
            splitContainer1.SplitterWidth = 10;
            splitContainer1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(36, 36);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 1400);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 38, 0);
            statusStrip1.Size = new Size(423, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.IsLink = true;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 11);
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(7, 8, 7, 8);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(treeView1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(listBox1);
            splitContainer2.Size = new Size(423, 1422);
            splitContainer2.SplitterDistance = 1136;
            splitContainer2.SplitterWidth = 12;
            splitContainer2.TabIndex = 1;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 0);
            treeView1.Margin = new Padding(7, 8, 7, 8);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(423, 1136);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // listBox1
            // 
            listBox1.Dock = DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 35;
            listBox1.Location = new Point(0, 0);
            listBox1.Margin = new Padding(7, 8, 7, 8);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(423, 274);
            listBox1.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(16F, 35F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2298, 1471);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(7, 8, 7, 8);
            Name = "MainForm";
            Text = "AnyCAD三维图形平台：功能展示";
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openSTEPToolStripMenuItem;
        private SplitContainer splitContainer1;
        private TreeView treeView1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem clearToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem captureToolStripMenuItem;
        private ToolStripMenuItem openFastToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripMenuItem zoomAllToolStripMenuItem;
        private ToolStripMenuItem projectionToolStripMenuItem;
        private SplitContainer splitContainer2;
        private ListBox listBox1;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem standardViewToolStripMenuItem;
        private ToolStripMenuItem frontToolStripMenuItem;
        private ToolStripMenuItem backToolStripMenuItem;
        private ToolStripMenuItem topToolStripMenuItem;
        private ToolStripMenuItem bottomToolStripMenuItem;
        private ToolStripMenuItem rightToolStripMenuItem;
        private ToolStripMenuItem leftToolStripMenuItem;
        private ToolStripMenuItem dToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem backgroundColorToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem mouseToolStripMenuItem;
        private ToolStripMenuItem backgroundImageToolStripMenuItem;
        private ToolStripMenuItem backgroundSkyBoxToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem toolTipToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem coordinateGridToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem selectionToolStripMenuItem;
        private ToolStripMenuItem depthTestToolStripMenuItem;
        private ToolStripMenuItem commandToolStripMenuItem;
        private ToolStripMenuItem clipBoxToolStripMenuItem;
        private ToolStripMenuItem orbitCenterToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem useViewAixsToolStripMenuItem;
        private ToolStripMenuItem useViewCubeToolStripMenuItem;
        private ToolStripMenuItem noneCoordinateToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem8;
        private ToolStripMenuItem explosureToolStripMenuItem;
        private ToolStripMenuItem contactShadowToolStripMenuItem;
        private ToolStripMenuItem moveToolStripMenuItem;
        private ToolStripMenuItem rotateToolStripMenuItem;
        private ToolStripMenuItem rectZoomToolStripMenuItem;
        private ToolStripMenuItem pickToolStripMenuItem;
        private ToolStripMenuItem 框选ToolStripMenuItem;
        private ToolStripMenuItem 单选ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem7;
        private ToolStripMenuItem filterEdgeToolStripMenuItem;
        private ToolStripMenuItem filterFaceToolStripMenuItem;
        private ToolStripMenuItem filterResetToolStripMenuItem;
        private ToolStripMenuItem openAdvToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
    }
}

