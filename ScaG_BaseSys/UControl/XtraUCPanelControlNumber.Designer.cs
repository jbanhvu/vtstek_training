namespace CNY_BaseSys.UControl
{
    partial class XtraUCPanelControlNumber
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
            this.panelControlNumBer = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumberValue = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlNumBer)).BeginInit();
            this.panelControlNumBer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlNumBer
            // 
            this.panelControlNumBer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlNumBer.Controls.Add(this.labelControl3);
            this.panelControlNumBer.Controls.Add(this.txtNumberValue);
            this.panelControlNumBer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlNumBer.Location = new System.Drawing.Point(0, 0);
            this.panelControlNumBer.Name = "panelControlNumBer";
            this.panelControlNumBer.Size = new System.Drawing.Size(360, 47);
            this.panelControlNumBer.TabIndex = 18;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Location = new System.Drawing.Point(3, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(31, 13);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "Value";
            // 
            // txtNumberValue
            // 
            this.txtNumberValue.Location = new System.Drawing.Point(38, 0);
            this.txtNumberValue.Name = "txtNumberValue";
            this.txtNumberValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNumberValue.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNumberValue.Size = new System.Drawing.Size(124, 20);
            this.txtNumberValue.TabIndex = 11;
            this.txtNumberValue.TabStop = false;
            // 
            // XtraUCPanelControlNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlNumBer);
            this.Name = "XtraUCPanelControlNumber";
            this.Size = new System.Drawing.Size(360, 47);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlNumBer)).EndInit();
            this.panelControlNumBer.ResumeLayout(false);
            this.panelControlNumBer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberValue.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlNumBer;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtNumberValue;
    }
}
