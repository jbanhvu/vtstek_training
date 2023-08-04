namespace CNY_BaseSys.WForm
{
    partial class FrmCustomDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomDialog));
            this.pcCenter = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.pcTopAfter = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblInput = new DevExpress.XtraEditors.LabelControl();
            this.pcTop = new DevExpress.XtraEditors.PanelControl();
            this.pcRight = new DevExpress.XtraEditors.PanelControl();
            this.pcLeft = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pcCenter)).BeginInit();
            this.pcCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcTopAfter)).BeginInit();
            this.pcTopAfter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // pcCenter
            // 
            this.pcCenter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcCenter.Controls.Add(this.btnClose);
            this.pcCenter.Controls.Add(this.btnOK);
            this.pcCenter.Controls.Add(this.pcTopAfter);
            this.pcCenter.Controls.Add(this.pcTop);
            this.pcCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcCenter.Location = new System.Drawing.Point(10, 0);
            this.pcCenter.Name = "pcCenter";
            this.pcCenter.Size = new System.Drawing.Size(264, 99);
            this.pcCenter.TabIndex = 124;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageOptions.Image = global::CNY_BaseSys.Properties.Resources.cancel_16x16;
            this.btnClose.Location = new System.Drawing.Point(207, 71);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(55, 24);
            this.btnClose.TabIndex = 122;
            this.btnClose.Text = "No";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ImageOptions.Image = global::CNY_BaseSys.Properties.Resources.apply_16x16;
            this.btnOK.Location = new System.Drawing.Point(149, 71);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(55, 24);
            this.btnOK.TabIndex = 121;
            this.btnOK.Text = "Yes";
            // 
            // pcTopAfter
            // 
            this.pcTopAfter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcTopAfter.Controls.Add(this.labelControl1);
            this.pcTopAfter.Controls.Add(this.lblInput);
            this.pcTopAfter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcTopAfter.Location = new System.Drawing.Point(0, 10);
            this.pcTopAfter.Name = "pcTopAfter";
            this.pcTopAfter.Size = new System.Drawing.Size(264, 60);
            this.pcTopAfter.TabIndex = 120;
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl1.ImageOptions.Image = global::CNY_BaseSys.Properties.Resources.question_32x32;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(34, 60);
            this.labelControl1.TabIndex = 115;
            // 
            // lblInput
            // 
            this.lblInput.Appearance.Options.UseTextOptions = true;
            this.lblInput.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblInput.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblInput.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblInput.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblInput.Location = new System.Drawing.Point(34, 0);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(230, 60);
            this.lblInput.TabIndex = 114;
            this.lblInput.Text = "lblInput";
            // 
            // pcTop
            // 
            this.pcTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcTop.Location = new System.Drawing.Point(0, 0);
            this.pcTop.Name = "pcTop";
            this.pcTop.Size = new System.Drawing.Size(264, 10);
            this.pcTop.TabIndex = 119;
            // 
            // pcRight
            // 
            this.pcRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pcRight.Location = new System.Drawing.Point(274, 0);
            this.pcRight.Name = "pcRight";
            this.pcRight.Size = new System.Drawing.Size(10, 99);
            this.pcRight.TabIndex = 123;
            // 
            // pcLeft
            // 
            this.pcLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcLeft.Location = new System.Drawing.Point(0, 0);
            this.pcLeft.Name = "pcLeft";
            this.pcLeft.Size = new System.Drawing.Size(10, 99);
            this.pcLeft.TabIndex = 122;
            // 
            // FrmCustomDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 99);
            this.Controls.Add(this.pcCenter);
            this.Controls.Add(this.pcRight);
            this.Controls.Add(this.pcLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCustomDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCustomDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pcCenter)).EndInit();
            this.pcCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcTopAfter)).EndInit();
            this.pcTopAfter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcLeft)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pcCenter;
        private DevExpress.XtraEditors.PanelControl pcTopAfter;
        private DevExpress.XtraEditors.PanelControl pcTop;
        private DevExpress.XtraEditors.PanelControl pcRight;
        private DevExpress.XtraEditors.PanelControl pcLeft;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl lblInput;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}