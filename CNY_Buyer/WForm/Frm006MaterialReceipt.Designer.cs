namespace CNY_Buyer.WForm
{
    partial class Frm006MaterialReceipt
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
            this.panelControlAdd.Name = "panelControlAdd";
            this.panelControlAdd.Size = new System.Drawing.Size(941, 474);
            this.panelControlAdd.TabIndex = 8;
            // 
            // Frm006MaterialReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 474);
            this.Controls.Add(this.panelControlAdd);
            this.Name = "Frm006MaterialReceipt";
            this.Text = "Material Request";
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlAdd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlAdd;
    }
}