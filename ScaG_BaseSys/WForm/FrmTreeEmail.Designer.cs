namespace CNY_BaseSys.WForm
{
    partial class FrmTreeEmail
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
            this.grpCenter = new DevExpress.XtraEditors.GroupControl();
            this.tlMain = new DevExpress.XtraTreeList.TreeList();
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.pcButton = new DevExpress.XtraEditors.PanelControl();
            this.txtFocus = new DevExpress.XtraEditors.TextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnNextFinish = new DevExpress.XtraEditors.SimpleButton();
            this.btnCheckAll = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpCenter)).BeginInit();
            this.grpCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcButton)).BeginInit();
            this.pcButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFocus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpCenter
            // 
            this.grpCenter.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCenter.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grpCenter.AppearanceCaption.Options.UseFont = true;
            this.grpCenter.AppearanceCaption.Options.UseForeColor = true;
            this.grpCenter.Controls.Add(this.tlMain);
            this.grpCenter.Controls.Add(this.chkAll);
            this.grpCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCenter.Location = new System.Drawing.Point(0, 0);
            this.grpCenter.Name = "grpCenter";
            this.grpCenter.Size = new System.Drawing.Size(784, 621);
            this.grpCenter.TabIndex = 2;
            // 
            // tlMain
            // 
            this.tlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlMain.Location = new System.Drawing.Point(2, 20);
            this.tlMain.Name = "tlMain";
            this.tlMain.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.Size = new System.Drawing.Size(780, 599);
            this.tlMain.TabIndex = 27;
            // 
            // chkAll
            // 
            this.chkAll.Location = new System.Drawing.Point(3, 0);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAll.Properties.Appearance.ForeColor = System.Drawing.Color.DarkMagenta;
            this.chkAll.Properties.Appearance.Options.UseFont = true;
            this.chkAll.Properties.Appearance.Options.UseForeColor = true;
            this.chkAll.Properties.Caption = "All Department";
            this.chkAll.Size = new System.Drawing.Size(172, 19);
            this.chkAll.TabIndex = 29;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grpCenter);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.pcButton);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(784, 661);
            this.splitContainerControl1.SplitterPosition = 35;
            this.splitContainerControl1.TabIndex = 3;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // pcButton
            // 
            this.pcButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcButton.Controls.Add(this.btnCheckAll);
            this.pcButton.Controls.Add(this.txtFocus);
            this.pcButton.Controls.Add(this.btnCancel);
            this.pcButton.Controls.Add(this.btnNextFinish);
            this.pcButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcButton.Location = new System.Drawing.Point(0, 0);
            this.pcButton.Name = "pcButton";
            this.pcButton.Size = new System.Drawing.Size(784, 35);
            this.pcButton.TabIndex = 15;
            // 
            // txtFocus
            // 
            this.txtFocus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtFocus.Location = new System.Drawing.Point(386, 23);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.Properties.AutoHeight = false;
            this.txtFocus.Size = new System.Drawing.Size(10, 10);
            this.txtFocus.TabIndex = 16;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = global::CNY_BaseSys.Properties.Resources.cancel_24x24_W;
            this.btnCancel.Location = new System.Drawing.Point(626, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTip = "Press (Ctrl+Shift+C)";
            // 
            // btnNextFinish
            // 
            this.btnNextFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextFinish.ImageOptions.Image = global::CNY_BaseSys.Properties.Resources.apply_24x24_W;
            this.btnNextFinish.Location = new System.Drawing.Point(705, 4);
            this.btnNextFinish.Name = "btnNextFinish";
            this.btnNextFinish.Size = new System.Drawing.Size(75, 28);
            this.btnNextFinish.TabIndex = 10;
            this.btnNextFinish.Text = "OK";
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckAll.Location = new System.Drawing.Point(2, 1);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(32, 32);
            this.btnCheckAll.TabIndex = 18;
            this.btnCheckAll.ToolTip = "Check All";
            // 
            // FrmTreeEmail
            // 
            this.AcceptButton = this.btnNextFinish;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.splitContainerControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmTreeEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Listing";
            ((System.ComponentModel.ISupportInitialize)(this.grpCenter)).EndInit();
            this.grpCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcButton)).EndInit();
            this.pcButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFocus.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpCenter;
        private DevExpress.XtraEditors.CheckEdit chkAll;
        private DevExpress.XtraTreeList.TreeList tlMain;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl pcButton;
        private DevExpress.XtraEditors.TextEdit txtFocus;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnNextFinish;
        private DevExpress.XtraEditors.SimpleButton btnCheckAll;
    }
}