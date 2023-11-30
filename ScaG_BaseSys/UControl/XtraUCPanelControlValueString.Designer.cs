namespace CNY_BaseSys.UControl
{
    partial class XtraUCPanelControlValueString
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
            this.txtStringValue = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlNumberBetween)).BeginInit();
            this.panelControlNumberBetween.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStringValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlNumberBetween
            // 
            this.panelControlNumberBetween.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlNumberBetween.Controls.Add(this.txtStringValue);
            this.panelControlNumberBetween.Controls.Add(this.labelControl10);
            this.panelControlNumberBetween.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlNumberBetween.Location = new System.Drawing.Point(0, 0);
            this.panelControlNumberBetween.Name = "panelControlNumberBetween";
            this.panelControlNumberBetween.Size = new System.Drawing.Size(360, 47);
            this.panelControlNumberBetween.TabIndex = 19;
            // 
            // txtStringValue
            // 
            this.txtStringValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtStringValue.Location = new System.Drawing.Point(40, 0);
            this.txtStringValue.Name = "txtStringValue";
            this.txtStringValue.Size = new System.Drawing.Size(320, 47);
            this.txtStringValue.TabIndex = 13;
            this.txtStringValue.TabStop = false;
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
            // XtraUCPanelControlValueString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlNumberBetween);
            this.Name = "XtraUCPanelControlValueString";
            this.Size = new System.Drawing.Size(360, 47);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlNumberBetween)).EndInit();
            this.panelControlNumberBetween.ResumeLayout(false);
            this.panelControlNumberBetween.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStringValue.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlNumberBetween;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.MemoEdit txtStringValue;
    }
}
