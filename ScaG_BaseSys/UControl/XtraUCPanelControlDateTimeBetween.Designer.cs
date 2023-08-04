namespace CNY_BaseSys.UControl
{
partial class XtraUCPanelControlDateTimeBetween
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
            this.panelControlDateTimeBetween = new DevExpress.XtraEditors.PanelControl();
            this.txtdatetimeValueEnd = new DevExpress.XtraEditors.DateEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtdatetimeValueStart = new DevExpress.XtraEditors.DateEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlDateTimeBetween)).BeginInit();
            this.panelControlDateTimeBetween.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatetimeValueEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatetimeValueEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatetimeValueStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatetimeValueStart.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlDateTimeBetween
            // 
            this.panelControlDateTimeBetween.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlDateTimeBetween.Controls.Add(this.txtdatetimeValueEnd);
            this.panelControlDateTimeBetween.Controls.Add(this.labelControl9);
            this.panelControlDateTimeBetween.Controls.Add(this.txtdatetimeValueStart);
            this.panelControlDateTimeBetween.Controls.Add(this.labelControl8);
            this.panelControlDateTimeBetween.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlDateTimeBetween.Location = new System.Drawing.Point(0, 0);
            this.panelControlDateTimeBetween.Name = "panelControlDateTimeBetween";
            this.panelControlDateTimeBetween.Size = new System.Drawing.Size(360, 47);
            this.panelControlDateTimeBetween.TabIndex = 21;
            // 
            // txtdatetimeValueEnd
            // 
            this.txtdatetimeValueEnd.EditValue = null;
            this.txtdatetimeValueEnd.Location = new System.Drawing.Point(251, 0);
            this.txtdatetimeValueEnd.Name = "txtdatetimeValueEnd";
            this.txtdatetimeValueEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdatetimeValueEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtdatetimeValueEnd.Size = new System.Drawing.Size(109, 20);
            this.txtdatetimeValueEnd.TabIndex = 19;
            this.txtdatetimeValueEnd.TabStop = false;
            this.txtdatetimeValueEnd.EditValueChanged += new System.EventHandler(this.txtdatetimeValueEnd_EditValueChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Location = new System.Drawing.Point(203, 3);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(44, 13);
            this.labelControl9.TabIndex = 18;
            this.labelControl9.Text = "To Date";
            // 
            // txtdatetimeValueStart
            // 
            this.txtdatetimeValueStart.EditValue = null;
            this.txtdatetimeValueStart.Location = new System.Drawing.Point(67, 0);
            this.txtdatetimeValueStart.Name = "txtdatetimeValueStart";
            this.txtdatetimeValueStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdatetimeValueStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtdatetimeValueStart.Size = new System.Drawing.Size(109, 20);
            this.txtdatetimeValueStart.TabIndex = 17;
            this.txtdatetimeValueStart.TabStop = false;
            this.txtdatetimeValueStart.EditValueChanged += new System.EventHandler(this.txtdatetimeValueStart_EditValueChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Location = new System.Drawing.Point(4, 3);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(59, 13);
            this.labelControl8.TabIndex = 16;
            this.labelControl8.Text = "From Date";
            // 
            // XtraUCPanelControlDateTimeBetween
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlDateTimeBetween);
            this.Name = "XtraUCPanelControlDateTimeBetween";
            this.Size = new System.Drawing.Size(360, 47);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlDateTimeBetween)).EndInit();
            this.panelControlDateTimeBetween.ResumeLayout(false);
            this.panelControlDateTimeBetween.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatetimeValueEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatetimeValueEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatetimeValueStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatetimeValueStart.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlDateTimeBetween;
        private DevExpress.XtraEditors.DateEdit txtdatetimeValueEnd;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.DateEdit txtdatetimeValueStart;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}
