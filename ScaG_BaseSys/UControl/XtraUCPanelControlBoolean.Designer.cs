namespace CNY_BaseSys.UControl
{
    partial class XtraUCPanelControlBoolean
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControlNumberBetween = new DevExpress.XtraEditors.PanelControl();
            this.chkCheckValue = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlNumberBetween)).BeginInit();
            this.panelControlNumberBetween.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlNumberBetween
            // 
            this.panelControlNumberBetween.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlNumberBetween.Controls.Add(this.chkCheckValue);
            this.panelControlNumberBetween.Controls.Add(this.labelControl10);
            this.panelControlNumberBetween.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlNumberBetween.Location = new System.Drawing.Point(0, 0);
            this.panelControlNumberBetween.Name = "panelControlNumberBetween";
            this.panelControlNumberBetween.Size = new System.Drawing.Size(360, 47);
            this.panelControlNumberBetween.TabIndex = 21;
            // 
            // chkCheckValue
            // 
            this.chkCheckValue.Location = new System.Drawing.Point(40, -1);
            this.chkCheckValue.Name = "chkCheckValue";
            this.chkCheckValue.Properties.Caption = "";
            this.chkCheckValue.Size = new System.Drawing.Size(75, 19);
            this.chkCheckValue.TabIndex = 17;
            this.chkCheckValue.TabStop = false;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl10.Location = new System.Drawing.Point(4, 2);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(31, 13);
            this.labelControl10.TabIndex = 12;
            this.labelControl10.Text = "Value";
            // 
            // XtraUCPanelControlBoolean
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlNumberBetween);
            this.Name = "XtraUCPanelControlBoolean";
            this.Size = new System.Drawing.Size(360, 47);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlNumberBetween)).EndInit();
            this.panelControlNumberBetween.ResumeLayout(false);
            this.panelControlNumberBetween.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckValue.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlNumberBetween;
        private DevExpress.XtraEditors.CheckEdit chkCheckValue;
        private DevExpress.XtraEditors.LabelControl labelControl10;
    }
}
