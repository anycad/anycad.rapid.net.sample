namespace AnyCAD.WinForms.App.Dialog
{
    partial class ModelFileDialogControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel3d = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel3d
            // 
            this.panel3d.Location = new System.Drawing.Point(3, 3);
            this.panel3d.Name = "panel3d";
            this.panel3d.Size = new System.Drawing.Size(1207, 623);
            this.panel3d.TabIndex = 0;
            // 
            // ModelFileDialogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3d);
            this.Name = "ModelFileDialogControl";
            this.Size = new System.Drawing.Size(1225, 823);
            this.Load += new System.EventHandler(this.ModelFileDialogControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel3d;
    }
}
