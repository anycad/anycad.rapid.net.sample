namespace AnyCAD.Demo
{
    partial class MultiCoordinateSystemForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.upDownX = new System.Windows.Forms.NumericUpDown();
            this.upDownY = new System.Windows.Forms.NumericUpDown();
            this.upDownZ = new System.Windows.Forms.NumericUpDown();
            this.upDownA = new System.Windows.Forms.NumericUpDown();
            this.upDownB = new System.Windows.Forms.NumericUpDown();
            this.upDownC = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownC)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1552, 993);
            this.splitContainer1.SplitterDistance = 287;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.upDownZ);
            this.groupBox1.Controls.Add(this.upDownY);
            this.groupBox1.Controls.Add(this.upDownX);
            this.groupBox1.Location = new System.Drawing.Point(25, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 161);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "位置：";
            // 
            // comboBox1
            // 
            this.comboBox1.AllowDrop = true;
            this.comboBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "世界坐标系",
            "局部坐标系"});
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(121, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(130, 29);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.MultiCoordinateSystemForm_ChangeCS);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "坐标系：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.upDownC);
            this.groupBox2.Controls.Add(this.upDownB);
            this.groupBox2.Controls.Add(this.upDownA);
            this.groupBox2.Location = new System.Drawing.Point(25, 266);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 172);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "角度";
            // 
            // upDownX
            // 
            this.upDownX.Location = new System.Drawing.Point(70, 30);
            this.upDownX.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.upDownX.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.upDownX.Name = "upDownX";
            this.upDownX.Size = new System.Drawing.Size(120, 31);
            this.upDownX.TabIndex = 5;
            this.upDownX.ValueChanged += new System.EventHandler(this.upDownX_ValueChanged);
            // 
            // upDownY
            // 
            this.upDownY.Location = new System.Drawing.Point(70, 67);
            this.upDownY.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.upDownY.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.upDownY.Name = "upDownY";
            this.upDownY.Size = new System.Drawing.Size(120, 31);
            this.upDownY.TabIndex = 5;
            this.upDownY.ValueChanged += new System.EventHandler(this.upDownX_ValueChanged);
            // 
            // upDownZ
            // 
            this.upDownZ.Location = new System.Drawing.Point(70, 104);
            this.upDownZ.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.upDownZ.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.upDownZ.Name = "upDownZ";
            this.upDownZ.Size = new System.Drawing.Size(120, 31);
            this.upDownZ.TabIndex = 5;
            this.upDownZ.ValueChanged += new System.EventHandler(this.upDownX_ValueChanged);
            // 
            // upDownA
            // 
            this.upDownA.Location = new System.Drawing.Point(70, 30);
            this.upDownA.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.upDownA.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.upDownA.Name = "upDownA";
            this.upDownA.Size = new System.Drawing.Size(120, 31);
            this.upDownA.TabIndex = 5;
            this.upDownA.ValueChanged += new System.EventHandler(this.upDownX_ValueChanged);
            // 
            // upDownB
            // 
            this.upDownB.Location = new System.Drawing.Point(70, 67);
            this.upDownB.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.upDownB.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.upDownB.Name = "upDownB";
            this.upDownB.Size = new System.Drawing.Size(120, 31);
            this.upDownB.TabIndex = 5;
            this.upDownB.ValueChanged += new System.EventHandler(this.upDownX_ValueChanged);
            // 
            // upDownC
            // 
            this.upDownC.Location = new System.Drawing.Point(70, 104);
            this.upDownC.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.upDownC.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.upDownC.Name = "upDownC";
            this.upDownC.Size = new System.Drawing.Size(120, 31);
            this.upDownC.TabIndex = 5;
            this.upDownC.ValueChanged += new System.EventHandler(this.upDownX_ValueChanged);
            // 
            // MultiCoordinateSystemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1552, 993);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MultiCoordinateSystemForm";
            this.Text = "多坐标系";
            this.Load += new System.EventHandler(this.MultiCoordinateSystemForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown upDownZ;
        private System.Windows.Forms.NumericUpDown upDownY;
        private System.Windows.Forms.NumericUpDown upDownX;
        private System.Windows.Forms.NumericUpDown upDownC;
        private System.Windows.Forms.NumericUpDown upDownB;
        private System.Windows.Forms.NumericUpDown upDownA;
    }
}