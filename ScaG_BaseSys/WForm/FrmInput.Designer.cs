namespace CNY_BaseSys.WForm
{
    partial class FrmInput
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
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.lblInput = new DevExpress.XtraEditors.LabelControl();
            this.txtInput = new DevExpress.XtraEditors.TextEdit();
            this.pcLeft = new DevExpress.XtraEditors.PanelControl();
            this.pcRight = new DevExpress.XtraEditors.PanelControl();
            this.pcCenter = new DevExpress.XtraEditors.PanelControl();
            this.pcTop = new DevExpress.XtraEditors.PanelControl();
            this.pcTopAfter = new DevExpress.XtraEditors.PanelControl();
            this.pcTopAfterLeft = new DevExpress.XtraEditors.PanelControl();
            this.pcTopAfterRight = new DevExpress.XtraEditors.PanelControl();
            this.pcBottom = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcCenter)).BeginInit();
            this.pcCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTopAfter)).BeginInit();
            this.pcTopAfter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcTopAfterLeft)).BeginInit();
            this.pcTopAfterLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcTopAfterRight)).BeginInit();
            this.pcTopAfterRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBottom)).BeginInit();
            this.pcBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = global::CNY_BaseSys.Properties.Resources.cancel_16x16;
            this.btnClose.Location = new System.Drawing.Point(208, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(55, 24);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::CNY_BaseSys.Properties.Resources.apply_16x16;
            this.btnOK.Location = new System.Drawing.Point(150, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(55, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Ok";
            // 
            // lblInput
            // 
            this.lblInput.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInput.Location = new System.Drawing.Point(0, 0);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(103, 24);
            this.lblInput.TabIndex = 113;
            this.lblInput.Text = "lblInput";
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.EditValue = "";
            this.txtInput.Location = new System.Drawing.Point(0, 0);
            this.txtInput.Name = "txtInput";
            this.txtInput.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtInput.Properties.Appearance.Options.UseForeColor = true;
            this.txtInput.Properties.AutoHeight = false;
            this.txtInput.Size = new System.Drawing.Size(161, 24);
            this.txtInput.TabIndex = 0;
            // 
            // pcLeft
            // 
            this.pcLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcLeft.Location = new System.Drawing.Point(0, 0);
            this.pcLeft.Name = "pcLeft";
            this.pcLeft.Size = new System.Drawing.Size(10, 70);
            this.pcLeft.TabIndex = 119;
            // 
            // pcRight
            // 
            this.pcRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pcRight.Location = new System.Drawing.Point(274, 0);
            this.pcRight.Name = "pcRight";
            this.pcRight.Size = new System.Drawing.Size(10, 70);
            this.pcRight.TabIndex = 120;
            // 
            // pcCenter
            // 
            this.pcCenter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcCenter.Controls.Add(this.pcBottom);
            this.pcCenter.Controls.Add(this.pcTopAfter);
            this.pcCenter.Controls.Add(this.pcTop);
            this.pcCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcCenter.Location = new System.Drawing.Point(10, 0);
            this.pcCenter.Name = "pcCenter";
            this.pcCenter.Size = new System.Drawing.Size(264, 70);
            this.pcCenter.TabIndex = 121;
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
            // pcTopAfter
            // 
            this.pcTopAfter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcTopAfter.Controls.Add(this.pcTopAfterRight);
            this.pcTopAfter.Controls.Add(this.pcTopAfterLeft);
            this.pcTopAfter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcTopAfter.Location = new System.Drawing.Point(0, 10);
            this.pcTopAfter.Name = "pcTopAfter";
            this.pcTopAfter.Size = new System.Drawing.Size(264, 24);
            this.pcTopAfter.TabIndex = 120;
            // 
            // pcTopAfterLeft
            // 
            this.pcTopAfterLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcTopAfterLeft.Controls.Add(this.lblInput);
            this.pcTopAfterLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcTopAfterLeft.Location = new System.Drawing.Point(0, 0);
            this.pcTopAfterLeft.Name = "pcTopAfterLeft";
            this.pcTopAfterLeft.Size = new System.Drawing.Size(103, 24);
            this.pcTopAfterLeft.TabIndex = 120;
            // 
            // pcTopAfterRight
            // 
            this.pcTopAfterRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcTopAfterRight.Controls.Add(this.txtInput);
            this.pcTopAfterRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcTopAfterRight.Location = new System.Drawing.Point(103, 0);
            this.pcTopAfterRight.Name = "pcTopAfterRight";
            this.pcTopAfterRight.Size = new System.Drawing.Size(161, 24);
            this.pcTopAfterRight.TabIndex = 121;
            // 
            // pcBottom
            // 
            this.pcBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcBottom.Controls.Add(this.btnClose);
            this.pcBottom.Controls.Add(this.btnOK);
            this.pcBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcBottom.Location = new System.Drawing.Point(0, 34);
            this.pcBottom.Name = "pcBottom";
            this.pcBottom.Size = new System.Drawing.Size(264, 36);
            this.pcBottom.TabIndex = 121;
            // 
            // FrmInput
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(284, 70);
            this.Controls.Add(this.pcCenter);
            this.Controls.Add(this.pcRight);
            this.Controls.Add(this.pcLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmInput";
            ((System.ComponentModel.ISupportInitialize)(this.txtInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcCenter)).EndInit();
            this.pcCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTopAfter)).EndInit();
            this.pcTopAfter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcTopAfterLeft)).EndInit();
            this.pcTopAfterLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcTopAfterRight)).EndInit();
            this.pcTopAfterRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcBottom)).EndInit();
            this.pcBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl lblInput;
        private DevExpress.XtraEditors.TextEdit txtInput;
        private DevExpress.XtraEditors.PanelControl pcLeft;
        private DevExpress.XtraEditors.PanelControl pcRight;
        private DevExpress.XtraEditors.PanelControl pcCenter;
        private DevExpress.XtraEditors.PanelControl pcBottom;
        private DevExpress.XtraEditors.PanelControl pcTopAfter;
        private DevExpress.XtraEditors.PanelControl pcTopAfterRight;
        private DevExpress.XtraEditors.PanelControl pcTopAfterLeft;
        private DevExpress.XtraEditors.PanelControl pcTop;
    }
}