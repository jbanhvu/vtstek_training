namespace CNY_Report
{
    partial class Frm003ProductionOrder
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.tlMain = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(300, 125);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "simpleButton1";
            // 
            // tlMain
            // 
            this.tlMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlMain.Location = new System.Drawing.Point(0, 378);
            this.tlMain.Name = "tlMain";
            this.tlMain.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.Size = new System.Drawing.Size(868, 291);
            this.tlMain.TabIndex = 265;
            // 
            // Frm003ProductionOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 669);
            this.Controls.Add(this.tlMain);
            this.Controls.Add(this.simpleButton1);
            this.Name = "Frm003ProductionOrder";
            this.Text = "Frm003ProductionOrder";
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraTreeList.TreeList tlMain;
    }
}