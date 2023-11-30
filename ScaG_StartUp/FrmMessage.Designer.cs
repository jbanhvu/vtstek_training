namespace CNY_StartUp
{
    partial class FrmMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMessage));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbllabel1 = new System.Windows.Forms.Label();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.lblVersionServer = new DevExpress.XtraEditors.LabelControl();
            this.lblVersionReport = new DevExpress.XtraEditors.LabelControl();
            this.lblVersionReportServer = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnYes = new DevExpress.XtraEditors.SimpleButton();
            this.btnNo = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::CNY_StartUp.Properties.Resources.system_software_update;
            this.pictureBox1.Location = new System.Drawing.Point(6, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lbllabel1
            // 
            this.lbllabel1.AutoSize = true;
            this.lbllabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllabel1.Location = new System.Drawing.Point(149, 18);
            this.lbllabel1.Name = "lbllabel1";
            this.lbllabel1.Size = new System.Drawing.Size(182, 13);
            this.lbllabel1.TabIndex = 1;
            this.lbllabel1.Text = "A new version of CNY Available!";
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(152, 40);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "lblVersion";
            // 
            // lblVersionServer
            // 
            this.lblVersionServer.Location = new System.Drawing.Point(152, 59);
            this.lblVersionServer.Name = "lblVersionServer";
            this.lblVersionServer.Size = new System.Drawing.Size(77, 13);
            this.lblVersionServer.TabIndex = 3;
            this.lblVersionServer.Text = "lblVersionServer";
            // 
            // lblVersionReport
            // 
            this.lblVersionReport.Location = new System.Drawing.Point(152, 78);
            this.lblVersionReport.Name = "lblVersionReport";
            this.lblVersionReport.Size = new System.Drawing.Size(78, 13);
            this.lblVersionReport.TabIndex = 4;
            this.lblVersionReport.Text = "lblVersionReport";
            // 
            // lblVersionReportServer
            // 
            this.lblVersionReportServer.Location = new System.Drawing.Point(152, 97);
            this.lblVersionReportServer.Name = "lblVersionReportServer";
            this.lblVersionReportServer.Size = new System.Drawing.Size(110, 13);
            this.lblVersionReportServer.TabIndex = 5;
            this.lblVersionReportServer.Text = "lblVersionReportServer";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(152, 127);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(196, 13);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "Would you like to install the update now?";
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(206, 158);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(68, 23);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "Yes";
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNo.Location = new System.Drawing.Point(280, 158);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(68, 23);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "No";
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // FrmMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnNo;
            this.ClientSize = new System.Drawing.Size(431, 208);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.lblVersionReportServer);
            this.Controls.Add(this.lblVersionReport);
            this.Controls.Add(this.lblVersionServer);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lbllabel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CNY Update";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbllabel1;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private DevExpress.XtraEditors.LabelControl lblVersionServer;
        private DevExpress.XtraEditors.LabelControl lblVersionReport;
        private DevExpress.XtraEditors.LabelControl lblVersionReportServer;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnYes;
        private DevExpress.XtraEditors.SimpleButton btnNo;
    }
}