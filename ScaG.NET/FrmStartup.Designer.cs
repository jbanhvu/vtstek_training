namespace CNY_Main
{
    partial class FrmStartup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStartup));
            this.panelBackGround = new DevExpress.XtraEditors.PanelControl();
            this.mqSplash = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.lblCopyRight = new DevExpress.XtraEditors.LabelControl();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.lblPlatform = new DevExpress.XtraEditors.LabelControl();
            this.lblSofName = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelBackGround)).BeginInit();
            this.panelBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mqSplash.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBackGround
            // 
            this.panelBackGround.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelBackGround.ContentImage = global::CNY_Main.Properties.Resources.backgroundFinalSFFFull;
            this.panelBackGround.Controls.Add(this.mqSplash);
            this.panelBackGround.Controls.Add(this.pictureLogo);
            this.panelBackGround.Controls.Add(this.lblCopyRight);
            this.panelBackGround.Controls.Add(this.lblVersion);
            this.panelBackGround.Controls.Add(this.lblPlatform);
            this.panelBackGround.Controls.Add(this.lblSofName);
            this.panelBackGround.Controls.Add(this.pictureBox1);
            this.panelBackGround.Controls.Add(this.progressBarControl1);
            this.panelBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBackGround.Location = new System.Drawing.Point(0, 0);
            this.panelBackGround.Name = "panelBackGround";
            this.panelBackGround.Size = new System.Drawing.Size(500, 343);
            this.panelBackGround.TabIndex = 2;
            // 
            // mqSplash
            // 
            this.mqSplash.EditValue = 0;
            this.mqSplash.Location = new System.Drawing.Point(5, 5);
            this.mqSplash.Name = "mqSplash";
            this.mqSplash.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.mqSplash.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mqSplash.Size = new System.Drawing.Size(489, 10);
            this.mqSplash.TabIndex = 8;
            // 
            // pictureLogo
            // 
            this.pictureLogo.BackColor = System.Drawing.Color.Transparent;
            this.pictureLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureLogo.Image")));
            this.pictureLogo.Location = new System.Drawing.Point(362, 276);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(131, 60);
            this.pictureLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureLogo.TabIndex = 7;
            this.pictureLogo.TabStop = false;
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.AllowHtmlString = true;
            this.lblCopyRight.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyRight.Appearance.Options.UseFont = true;
            this.lblCopyRight.Location = new System.Drawing.Point(29, 296);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(47, 16);
            this.lblCopyRight.TabIndex = 6;
            this.lblCopyRight.Text = "Copyright";
            // 
            // lblVersion
            // 
            this.lblVersion.Appearance.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Appearance.Options.UseFont = true;
            this.lblVersion.Location = new System.Drawing.Point(29, 91);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(36, 16);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "Version";
            // 
            // lblPlatform
            // 
            this.lblPlatform.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblPlatform.Appearance.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlatform.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblPlatform.Appearance.Options.UseBackColor = true;
            this.lblPlatform.Appearance.Options.UseFont = true;
            this.lblPlatform.Appearance.Options.UseForeColor = true;
            this.lblPlatform.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPlatform.Location = new System.Drawing.Point(29, 60);
            this.lblPlatform.Name = "lblPlatform";
            this.lblPlatform.Size = new System.Drawing.Size(231, 24);
            this.lblPlatform.TabIndex = 4;
            this.lblPlatform.Text = "PLATFORM FOR WINDOWS ";
            // 
            // lblSofName
            // 
            this.lblSofName.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblSofName.Appearance.Font = new System.Drawing.Font("Arial Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSofName.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblSofName.Appearance.Options.UseBackColor = true;
            this.lblSofName.Appearance.Options.UseFont = true;
            this.lblSofName.Appearance.Options.UseForeColor = true;
            this.lblSofName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSofName.Location = new System.Drawing.Point(29, 27);
            this.lblSofName.Name = "lblSofName";
            this.lblSofName.Size = new System.Drawing.Size(404, 32);
            this.lblSofName.TabIndex = 2;
            this.lblSofName.Text = "VTSTek Management System";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CNY_Main.Properties.Resources.backgroundBusiness;
            this.pictureBox1.Location = new System.Drawing.Point(5, 120);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(489, 154);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(5, 5);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressBarControl1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.progressBarControl1.Size = new System.Drawing.Size(489, 10);
            this.progressBarControl1.TabIndex = 0;
            // 
            // FrmStartup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 343);
            this.Controls.Add(this.panelBackGround);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("FrmStartup.IconOptions.Icon")));
            this.IconOptions.Image = global::CNY_Main.Properties.Resources.Increase_icon;
            this.Name = "FrmStartup";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmStartup";
            this.Load += new System.EventHandler(this.FrmStartup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelBackGround)).EndInit();
            this.panelBackGround.ResumeLayout(false);
            this.panelBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mqSplash.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.PanelControl panelBackGround;
        private DevExpress.XtraEditors.LabelControl lblSofName;
        private DevExpress.XtraEditors.LabelControl lblPlatform;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private DevExpress.XtraEditors.LabelControl lblCopyRight;
        private System.Windows.Forms.PictureBox pictureLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.MarqueeProgressBarControl mqSplash;
    }
}