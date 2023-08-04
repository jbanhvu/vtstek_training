namespace CNY_Report
{
    partial class FrmPOReport
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
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtPK = new DevExpress.XtraEditors.TextEdit();
            this.tlMain = new DevExpress.XtraTreeList.TreeList();
            this.tlServices = new DevExpress.XtraTreeList.TreeList();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlServices)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcMain.Location = new System.Drawing.Point(253, 11);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(287, 190);
            this.gcMain.TabIndex = 268;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(128, 34);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(119, 23);
            this.simpleButton2.TabIndex = 266;
            this.simpleButton2.Text = "Preview Sub-report";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(128, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(119, 23);
            this.simpleButton1.TabIndex = 267;
            this.simpleButton1.Text = "simpleButton1";
            // 
            // txtPK
            // 
            this.txtPK.Location = new System.Drawing.Point(22, 8);
            this.txtPK.Name = "txtPK";
            this.txtPK.Size = new System.Drawing.Size(100, 20);
            this.txtPK.TabIndex = 265;
            // 
            // tlMain
            // 
            this.tlMain.DataSource = null;
            this.tlMain.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlMain.Location = new System.Drawing.Point(0, 223);
            this.tlMain.Name = "tlMain";
            this.tlMain.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.Size = new System.Drawing.Size(787, 291);
            this.tlMain.TabIndex = 269;
            // 
            // tlServices
            // 
            this.tlServices.DataSource = null;
            this.tlServices.Location = new System.Drawing.Point(0, 119);
            this.tlServices.Name = "tlServices";
            this.tlServices.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlServices.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlServices.Size = new System.Drawing.Size(247, 72);
            this.tlServices.TabIndex = 270;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(68, 83);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 13);
            this.labelControl1.TabIndex = 271;
            this.labelControl1.Text = "labelControl1";
            // 
            // FrmPOReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 514);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.tlServices);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtPK);
            this.Controls.Add(this.tlMain);
            this.Name = "FrmPOReport";
            this.Text = "FrmPOReport";
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlServices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvMain;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txtPK;
        private DevExpress.XtraTreeList.TreeList tlMain;
        private DevExpress.XtraTreeList.TreeList tlServices;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}