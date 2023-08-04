namespace CNY_Report
{
    partial class Frm001SOReport
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
            this.txtPK = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.tlServices = new DevExpress.XtraTreeList.TreeList();
            this.tlMain = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.txtPK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlServices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPK
            // 
            this.txtPK.Location = new System.Drawing.Point(22, 12);
            this.txtPK.Name = "txtPK";
            this.txtPK.Size = new System.Drawing.Size(100, 20);
            this.txtPK.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(128, 9);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(119, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(128, 38);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(119, 23);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "Preview Sub-report";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // gcMain
            // 
            this.gcMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcMain.Location = new System.Drawing.Point(253, 15);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(287, 190);
            this.gcMain.TabIndex = 263;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            // 
            // tlServices
            // 
            this.tlServices.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlServices.Location = new System.Drawing.Point(424, 223);
            this.tlServices.Name = "tlServices";
            this.tlServices.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlServices.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlServices.Size = new System.Drawing.Size(345, 291);
            this.tlServices.TabIndex = 264;
            // 
            // tlMain
            // 
            this.tlMain.Location = new System.Drawing.Point(0, 223);
            this.tlMain.Name = "tlMain";
            this.tlMain.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.Size = new System.Drawing.Size(388, 291);
            this.tlMain.TabIndex = 265;
            // 
            // Frm001SOReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 514);
            this.Controls.Add(this.tlMain);
            this.Controls.Add(this.tlServices);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtPK);
            this.Name = "Frm001SOReport";
            this.Text = "FrmSOReport";
            ((System.ComponentModel.ISupportInitialize)(this.txtPK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlServices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtPK;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvMain;
        private DevExpress.XtraTreeList.TreeList tlServices;
        private DevExpress.XtraTreeList.TreeList tlMain;
    }
}