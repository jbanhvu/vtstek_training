namespace CNY_BaseSys.UControl
{
 
       partial class XtraUCPanelControlNumberBetween
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
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumberValueEnd = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumberValueStart = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlNumberBetween)).BeginInit();
            this.panelControlNumberBetween.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberValueEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberValueStart.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlNumberBetween
            // 
            this.panelControlNumberBetween.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlNumberBetween.Controls.Add(this.labelControl6);
            this.panelControlNumberBetween.Controls.Add(this.txtNumberValueEnd);
            this.panelControlNumberBetween.Controls.Add(this.labelControl5);
            this.panelControlNumberBetween.Controls.Add(this.txtNumberValueStart);
            this.panelControlNumberBetween.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlNumberBetween.Location = new System.Drawing.Point(0, 0);
            this.panelControlNumberBetween.Name = "panelControlNumberBetween";
            this.panelControlNumberBetween.Size = new System.Drawing.Size(360, 47);
            this.panelControlNumberBetween.TabIndex = 19;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Location = new System.Drawing.Point(194, 3);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(51, 13);
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "To Value ";
            // 
            // txtNumberValueEnd
            // 
            this.txtNumberValueEnd.Location = new System.Drawing.Point(250, 0);
            this.txtNumberValueEnd.Name = "txtNumberValueEnd";
            this.txtNumberValueEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNumberValueEnd.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNumberValueEnd.Size = new System.Drawing.Size(109, 20);
            this.txtNumberValueEnd.TabIndex = 15;
            this.txtNumberValueEnd.TabStop = false;
            this.txtNumberValueEnd.Leave += new System.EventHandler(this.txtNumberValueEnd_Leave);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Location = new System.Drawing.Point(3, 3);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(63, 13);
            this.labelControl5.TabIndex = 14;
            this.labelControl5.Text = "From Value";
            // 
            // txtNumberValueStart
            // 
            this.txtNumberValueStart.Location = new System.Drawing.Point(71, 1);
            this.txtNumberValueStart.Name = "txtNumberValueStart";
            this.txtNumberValueStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNumberValueStart.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNumberValueStart.Size = new System.Drawing.Size(109, 20);
            this.txtNumberValueStart.TabIndex = 13;
            this.txtNumberValueStart.TabStop = false;
            this.txtNumberValueStart.Leave += new System.EventHandler(this.txtNumberValueStart_Leave);
            // 
            // XtraUCPanelControlNumberBetween
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlNumberBetween);
            this.Name = "XtraUCPanelControlNumberBetween";
            this.Size = new System.Drawing.Size(360, 47);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlNumberBetween)).EndInit();
            this.panelControlNumberBetween.ResumeLayout(false);
            this.panelControlNumberBetween.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberValueEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberValueStart.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlNumberBetween;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtNumberValueEnd;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtNumberValueStart;
    }
}
