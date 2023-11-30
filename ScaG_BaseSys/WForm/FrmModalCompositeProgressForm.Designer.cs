namespace CNY_BaseSys.WForm
{
    partial class FrmModalCompositeProgressForm
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
            this.pgbOverallProgress = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblSubtitle = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblOverallProgress = new DevExpress.XtraEditors.LabelControl();
            this.pgbCurrentProgress = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblCurrentComponent = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pgbOverallProgress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgbCurrentProgress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pgbOverallProgress
            // 
            this.pgbOverallProgress.Location = new System.Drawing.Point(16, 125);
            this.pgbOverallProgress.Name = "pgbOverallProgress";
            this.pgbOverallProgress.Properties.ShowTitle = true;
            this.pgbOverallProgress.Size = new System.Drawing.Size(437, 22);
            this.pgbOverallProgress.TabIndex = 5;
            this.pgbOverallProgress.UseWaitCursor = true;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblSubtitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblSubtitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblSubtitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSubtitle.Location = new System.Drawing.Point(15, 63);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(438, 32);
            this.lblSubtitle.TabIndex = 4;
            this.lblSubtitle.Text = "Subtitle";
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Location = new System.Drawing.Point(15, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(438, 32);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Title";
            // 
            // lblOverallProgress
            // 
            this.lblOverallProgress.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblOverallProgress.Location = new System.Drawing.Point(20, 106);
            this.lblOverallProgress.Name = "lblOverallProgress";
            this.lblOverallProgress.Size = new System.Drawing.Size(76, 13);
            this.lblOverallProgress.TabIndex = 6;
            this.lblOverallProgress.Text = "Overall progress";
            // 
            // pgbCurrentProgress
            // 
            this.pgbCurrentProgress.Location = new System.Drawing.Point(16, 173);
            this.pgbCurrentProgress.Name = "pgbCurrentProgress";
            this.pgbCurrentProgress.Properties.ShowTitle = true;
            this.pgbCurrentProgress.Size = new System.Drawing.Size(437, 22);
            this.pgbCurrentProgress.TabIndex = 8;
            this.pgbCurrentProgress.UseWaitCursor = true;
            // 
            // lblCurrentComponent
            // 
            this.lblCurrentComponent.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCurrentComponent.Location = new System.Drawing.Point(20, 155);
            this.lblCurrentComponent.Name = "lblCurrentComponent";
            this.lblCurrentComponent.Size = new System.Drawing.Size(133, 13);
            this.lblCurrentComponent.TabIndex = 9;
            this.lblCurrentComponent.Text = "Current component progress";
            // 
            // frmModalCompositeProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 217);
            this.ControlBox = false;
            this.Controls.Add(this.lblCurrentComponent);
            this.Controls.Add(this.pgbCurrentProgress);
            this.Controls.Add(this.lblOverallProgress);
            this.Controls.Add(this.pgbOverallProgress);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmModalCompositeProgressForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmModalCompositeProgressForm";
            ((System.ComponentModel.ISupportInitialize)(this.pgbOverallProgress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgbCurrentProgress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl pgbOverallProgress;
        private DevExpress.XtraEditors.LabelControl lblSubtitle;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblOverallProgress;
        private DevExpress.XtraEditors.ProgressBarControl pgbCurrentProgress;
        private DevExpress.XtraEditors.LabelControl lblCurrentComponent;
    }
}