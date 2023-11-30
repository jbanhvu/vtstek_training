namespace CNY_AdminSys.WForm
{
    partial class FrmMangeUpdate
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
            this.xtraTabMain = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageUpd = new DevExpress.XtraTab.XtraTabPage();
            this.txtFocusUpd = new DevExpress.XtraEditors.TextEdit();
            this.gcUpd = new DevExpress.XtraGrid.GridControl();
            this.gvUpd = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPageCom = new DevExpress.XtraTab.XtraTabPage();
            this.txtFocusCom = new DevExpress.XtraEditors.TextEdit();
            this.gcCom = new DevExpress.XtraGrid.GridControl();
            this.gvCom = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPageRep = new DevExpress.XtraTab.XtraTabPage();
            this.txtFocusRep = new DevExpress.XtraEditors.TextEdit();
            this.gcRep = new DevExpress.XtraGrid.GridControl();
            this.gvRep = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPageMac = new DevExpress.XtraTab.XtraTabPage();
            this.txtFocusMac = new DevExpress.XtraEditors.TextEdit();
            this.gcMac = new DevExpress.XtraGrid.GridControl();
            this.gvMac = new DevExpress.XtraGrid.Views.Grid.GridView();
         
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlAdd)).BeginInit();
            this.panelControlAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabMain)).BeginInit();
            this.xtraTabMain.SuspendLayout();
            this.xtraTabPageUpd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFocusUpd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcUpd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUpd)).BeginInit();
            this.xtraTabPageCom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFocusCom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCom)).BeginInit();
            this.xtraTabPageRep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFocusRep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRep)).BeginInit();
            this.xtraTabPageMac.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFocusMac.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMac)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlAdd
            // 
            this.panelControlAdd.Controls.Add(this.xtraTabMain);
            this.panelControlAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlAdd.Location = new System.Drawing.Point(0, 65);
            this.panelControlAdd.Name = "panelControlAdd";
            this.panelControlAdd.Size = new System.Drawing.Size(938, 409);
            this.panelControlAdd.TabIndex = 4;
            // 
            // xtraTabMain
            // 
            this.xtraTabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabMain.Location = new System.Drawing.Point(2, 2);
            this.xtraTabMain.Name = "xtraTabMain";
            this.xtraTabMain.SelectedTabPage = this.xtraTabPageUpd;
            this.xtraTabMain.Size = new System.Drawing.Size(934, 405);
            this.xtraTabMain.TabIndex = 0;
            this.xtraTabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageUpd,
            this.xtraTabPageCom,
            this.xtraTabPageRep,
            this.xtraTabPageMac});
            this.xtraTabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabMain_SelectedPageChanged);
            // 
            // xtraTabPageUpd
            // 
            this.xtraTabPageUpd.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabPageUpd.Appearance.Header.Options.UseFont = true;
            this.xtraTabPageUpd.Controls.Add(this.txtFocusUpd);
            this.xtraTabPageUpd.Controls.Add(this.gcUpd);
            this.xtraTabPageUpd.Image = global::CNY_AdminSys.Properties.Resources.ide_16x16;
            this.xtraTabPageUpd.Name = "xtraTabPageUpd";
            this.xtraTabPageUpd.Size = new System.Drawing.Size(928, 374);
            this.xtraTabPageUpd.Text = "Setting Update";
            // 
            // txtFocusUpd
            // 
            this.txtFocusUpd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFocusUpd.Location = new System.Drawing.Point(903, 360);
            this.txtFocusUpd.Name = "txtFocusUpd";
            this.txtFocusUpd.Properties.AutoHeight = false;
            this.txtFocusUpd.Size = new System.Drawing.Size(10, 10);
            this.txtFocusUpd.TabIndex = 5;
            // 
            // gcUpd
            // 
            this.gcUpd.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcUpd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcUpd.Location = new System.Drawing.Point(0, 0);
            this.gcUpd.MainView = this.gvUpd;
            this.gcUpd.Name = "gcUpd";
            this.gcUpd.Size = new System.Drawing.Size(928, 374);
            this.gcUpd.TabIndex = 0;
            this.gcUpd.UseEmbeddedNavigator = true;
            this.gcUpd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUpd});
            // 
            // gvUpd
            // 
            this.gvUpd.GridControl = this.gcUpd;
            this.gvUpd.Name = "gvUpd";
            // 
            // xtraTabPageCom
            // 
            this.xtraTabPageCom.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabPageCom.Appearance.Header.Options.UseFont = true;
            this.xtraTabPageCom.Controls.Add(this.txtFocusCom);
            this.xtraTabPageCom.Controls.Add(this.gcCom);
            this.xtraTabPageCom.Image = global::CNY_AdminSys.Properties.Resources.newtask_16x16;
            this.xtraTabPageCom.Name = "xtraTabPageCom";
            this.xtraTabPageCom.Size = new System.Drawing.Size(928, 374);
            this.xtraTabPageCom.Text = "Choose Component ";
            // 
            // txtFocusCom
            // 
            this.txtFocusCom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFocusCom.Location = new System.Drawing.Point(905, 360);
            this.txtFocusCom.Name = "txtFocusCom";
            this.txtFocusCom.Properties.AutoHeight = false;
            this.txtFocusCom.Size = new System.Drawing.Size(10, 10);
            this.txtFocusCom.TabIndex = 6;
            // 
            // gcCom
            // 
            this.gcCom.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcCom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCom.Location = new System.Drawing.Point(0, 0);
            this.gcCom.MainView = this.gvCom;
            this.gcCom.Name = "gcCom";
            this.gcCom.Size = new System.Drawing.Size(928, 374);
            this.gcCom.TabIndex = 1;
            this.gcCom.UseEmbeddedNavigator = true;
            this.gcCom.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCom});
            // 
            // gvCom
            // 
            this.gvCom.GridControl = this.gcCom;
            this.gvCom.Name = "gvCom";
            // 
            // xtraTabPageRep
            // 
            this.xtraTabPageRep.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabPageRep.Appearance.Header.Options.UseFont = true;
            this.xtraTabPageRep.Controls.Add(this.txtFocusRep);
            this.xtraTabPageRep.Controls.Add(this.gcRep);
            this.xtraTabPageRep.Image = global::CNY_AdminSys.Properties.Resources.addgroupfooter_16x16;
            this.xtraTabPageRep.Name = "xtraTabPageRep";
            this.xtraTabPageRep.Size = new System.Drawing.Size(928, 374);
            this.xtraTabPageRep.Text = "Choose Report";
            // 
            // txtFocusRep
            // 
            this.txtFocusRep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFocusRep.Location = new System.Drawing.Point(905, 360);
            this.txtFocusRep.Name = "txtFocusRep";
            this.txtFocusRep.Properties.AutoHeight = false;
            this.txtFocusRep.Size = new System.Drawing.Size(10, 10);
            this.txtFocusRep.TabIndex = 6;
            // 
            // gcRep
            // 
            this.gcRep.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcRep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRep.Location = new System.Drawing.Point(0, 0);
            this.gcRep.MainView = this.gvRep;
            this.gcRep.Name = "gcRep";
            this.gcRep.Size = new System.Drawing.Size(928, 374);
            this.gcRep.TabIndex = 1;
            this.gcRep.UseEmbeddedNavigator = true;
            this.gcRep.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRep});
            // 
            // gvRep
            // 
            this.gvRep.GridControl = this.gcRep;
            this.gvRep.Name = "gvRep";
            // 
            // xtraTabPageMac
            // 
            this.xtraTabPageMac.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabPageMac.Appearance.Header.Options.UseFont = true;
            this.xtraTabPageMac.Controls.Add(this.txtFocusMac);
            this.xtraTabPageMac.Controls.Add(this.gcMac);
            this.xtraTabPageMac.Image = global::CNY_AdminSys.Properties.Resources.navigationbar_16x16;
            this.xtraTabPageMac.Name = "xtraTabPageMac";
            this.xtraTabPageMac.Size = new System.Drawing.Size(928, 374);
            this.xtraTabPageMac.Text = "Choose Machine";
            // 
            // txtFocusMac
            // 
            this.txtFocusMac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFocusMac.Location = new System.Drawing.Point(905, 360);
            this.txtFocusMac.Name = "txtFocusMac";
            this.txtFocusMac.Properties.AutoHeight = false;
            this.txtFocusMac.Size = new System.Drawing.Size(10, 10);
            this.txtFocusMac.TabIndex = 7;
            // 
            // gcMac
            // 
            this.gcMac.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcMac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMac.Location = new System.Drawing.Point(0, 0);
            this.gcMac.MainView = this.gvMac;
            this.gcMac.Name = "gcMac";
            this.gcMac.Size = new System.Drawing.Size(928, 374);
            this.gcMac.TabIndex = 1;
            this.gcMac.UseEmbeddedNavigator = true;
            this.gcMac.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMac});
            // 
            // gvMac
            // 
            this.gvMac.GridControl = this.gcMac;
            this.gvMac.Name = "gvMac";
            // 
            // FrmMangeUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 474);
            this.Controls.Add(this.panelControlAdd);
            this.Name = "FrmMangeUpdate";
            this.Text = "Manage Update";
            this.Controls.SetChildIndex(this.panelControlAdd, 0);
         
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlAdd)).EndInit();
            this.panelControlAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabMain)).EndInit();
            this.xtraTabMain.ResumeLayout(false);
            this.xtraTabPageUpd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFocusUpd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcUpd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUpd)).EndInit();
            this.xtraTabPageCom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFocusCom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCom)).EndInit();
            this.xtraTabPageRep.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFocusRep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRep)).EndInit();
            this.xtraTabPageMac.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFocusMac.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMac)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlAdd;
        private DevExpress.XtraTab.XtraTabControl xtraTabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageUpd;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageCom;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageRep;
        private DevExpress.XtraGrid.GridControl gcUpd;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUpd;
        private DevExpress.XtraGrid.GridControl gcCom;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCom;
        private DevExpress.XtraGrid.GridControl gcRep;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRep;
        private DevExpress.XtraEditors.TextEdit txtFocusUpd;
        private DevExpress.XtraEditors.TextEdit txtFocusCom;
        private DevExpress.XtraEditors.TextEdit txtFocusRep;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageMac;
        private DevExpress.XtraEditors.TextEdit txtFocusMac;
        private DevExpress.XtraGrid.GridControl gcMac;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMac;

    }
}