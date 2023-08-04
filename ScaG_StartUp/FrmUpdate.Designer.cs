namespace CNY_StartUp
{
    partial class FrmUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdate));
            this.lblWaiting = new DevExpress.XtraEditors.LabelControl();
            this.mqProcess = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.mqProcess.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWaiting
            // 
            this.lblWaiting.Location = new System.Drawing.Point(12, 12);
            this.lblWaiting.Name = "lblWaiting";
            this.lblWaiting.Size = new System.Drawing.Size(80, 13);
            this.lblWaiting.TabIndex = 0;
            this.lblWaiting.Text = "Please waiting...";
            // 
            // mqProcess
            // 
            this.mqProcess.EditValue = 0;
            this.mqProcess.Location = new System.Drawing.Point(12, 31);
            this.mqProcess.Name = "mqProcess";
            this.mqProcess.Size = new System.Drawing.Size(302, 29);
            this.mqProcess.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(239, 66);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            // 
            // FrmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 95);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.mqProcess);
            this.Controls.Add(this.lblWaiting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Processing";
            ((System.ComponentModel.ISupportInitialize)(this.mqProcess.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblWaiting;
        private DevExpress.XtraEditors.MarqueeProgressBarControl mqProcess;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}