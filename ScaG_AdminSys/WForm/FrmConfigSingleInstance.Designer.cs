namespace CNY_AdminSys.WForm
{
    partial class FrmConfigSingleInstance
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
            this.gcMac = new DevExpress.XtraGrid.GridControl();
            this.gvMac = new DevExpress.XtraGrid.Views.Grid.GridView();
        
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMac)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMac
            // 
            this.gcMac.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcMac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMac.Location = new System.Drawing.Point(0, 65);
            this.gcMac.MainView = this.gvMac;
            this.gcMac.Name = "gcMac";
            this.gcMac.Size = new System.Drawing.Size(941, 409);
            this.gcMac.TabIndex = 4;
            this.gcMac.UseEmbeddedNavigator = true;
            this.gcMac.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMac});
            // 
            // gvMac
            // 
            this.gvMac.GridControl = this.gcMac;
            this.gvMac.Name = "gvMac";
            // 
            // FrmConfigSingleInstance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 474);
            this.Controls.Add(this.gcMac);
            this.Name = "FrmConfigSingleInstance";
            this.Text = "Config Instance Program";
            this.Controls.SetChildIndex(this.gcMac, 0);
         
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMac)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMac;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMac;
    }
}