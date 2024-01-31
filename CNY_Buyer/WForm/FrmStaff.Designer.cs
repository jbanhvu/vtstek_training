namespace CNY_Buyer.WForm
{
    partial class FrmStaff
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
            this.panelControlAdd = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlAdd
            // 
            this.panelControlAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlAdd.Location = new System.Drawing.Point(0, 0);
            this.panelControlAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelControlAdd.Name = "panelControlAdd";
            this.panelControlAdd.Size = new System.Drawing.Size(800, 450);
            this.panelControlAdd.TabIndex = 0;
            // 
            // FrmStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelControlAdd);
            this.Name = "FrmStaff";
            this.Text = "FrmStaff";
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlAdd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlAdd;
    }
}