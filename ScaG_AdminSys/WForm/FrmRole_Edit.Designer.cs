namespace CNY_AdminSys.WForm
{
    partial class FrmRole_Edit
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
            this.tlMain = new DevExpress.XtraTreeList.TreeList();
            this.panelVLine = new DevExpress.XtraEditors.PanelControl();
            this.groupTop = new DevExpress.XtraEditors.GroupControl();
            this.btnView = new DevExpress.XtraEditors.SimpleButton();
            this.txtFocus = new DevExpress.XtraEditors.TextEdit();
            this.gcUserInRole = new DevExpress.XtraGrid.GridControl();
            this.gvUserInRole = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitCCMain = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelVLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTop)).BeginInit();
            this.groupTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFocus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcUserInRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserInRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitCCMain)).BeginInit();
            this.splitCCMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlMain
            // 
            this.tlMain.DataSource = null;
            this.tlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlMain.Location = new System.Drawing.Point(10, 0);
            this.tlMain.Name = "tlMain";
            this.tlMain.Size = new System.Drawing.Size(573, 474);
            this.tlMain.TabIndex = 0;
            // 
            // panelVLine
            // 
            this.panelVLine.AllowDrop = true;
            this.panelVLine.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelVLine.ContentImage = global::CNY_AdminSys.Properties.Resources.VPermissionLine;
            this.panelVLine.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelVLine.Location = new System.Drawing.Point(0, 0);
            this.panelVLine.Name = "panelVLine";
            this.panelVLine.Size = new System.Drawing.Size(10, 474);
            this.panelVLine.TabIndex = 1;
            // 
            // groupTop
            // 
            this.groupTop.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupTop.AppearanceCaption.Options.UseFont = true;
            this.groupTop.Controls.Add(this.btnView);
            this.groupTop.Controls.Add(this.txtFocus);
            this.groupTop.Controls.Add(this.gcUserInRole);
            this.groupTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupTop.Location = new System.Drawing.Point(0, 0);
            this.groupTop.Name = "groupTop";
            this.groupTop.Size = new System.Drawing.Size(350, 474);
            this.groupTop.TabIndex = 3;
            this.groupTop.Text = "User Listing";
            // 
            // btnView
            // 
            this.btnView.ImageOptions.Image = global::CNY_AdminSys.Properties.Resources.showall_16x16;
            this.btnView.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnView.Location = new System.Drawing.Point(80, 0);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(22, 20);
            this.btnView.TabIndex = 497;
            this.btnView.ToolTip = "View Tree Menu";
            // 
            // txtFocus
            // 
            this.txtFocus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFocus.Location = new System.Drawing.Point(326, 459);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.Properties.AutoHeight = false;
            this.txtFocus.Size = new System.Drawing.Size(10, 10);
            this.txtFocus.TabIndex = 5;
            // 
            // gcUserInRole
            // 
            this.gcUserInRole.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcUserInRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcUserInRole.Location = new System.Drawing.Point(2, 20);
            this.gcUserInRole.MainView = this.gvUserInRole;
            this.gcUserInRole.Name = "gcUserInRole";
            this.gcUserInRole.Size = new System.Drawing.Size(346, 452);
            this.gcUserInRole.TabIndex = 1;
            this.gcUserInRole.UseEmbeddedNavigator = true;
            this.gcUserInRole.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUserInRole});
            // 
            // gvUserInRole
            // 
            this.gvUserInRole.GridControl = this.gcUserInRole;
            this.gvUserInRole.Name = "gvUserInRole";
            // 
            // splitCCMain
            // 
            this.splitCCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCCMain.Location = new System.Drawing.Point(0, 0);
            this.splitCCMain.Name = "splitCCMain";
            this.splitCCMain.Panel1.Controls.Add(this.groupTop);
            this.splitCCMain.Panel1.Text = "Panel1";
            this.splitCCMain.Panel2.Controls.Add(this.tlMain);
            this.splitCCMain.Panel2.Controls.Add(this.panelVLine);
            this.splitCCMain.Panel2.Text = "Panel2";
            this.splitCCMain.Size = new System.Drawing.Size(938, 474);
            this.splitCCMain.SplitterPosition = 350;
            this.splitCCMain.TabIndex = 2;
            this.splitCCMain.Text = "splitContainerControl1";
            // 
            // FrmRole_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 474);
            this.Controls.Add(this.splitCCMain);
            this.Name = "FrmRole_Edit";
            this.Text = "Edit Permission On User";
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelVLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTop)).EndInit();
            this.groupTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFocus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcUserInRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserInRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitCCMain)).EndInit();
            this.splitCCMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelVLine;
        private DevExpress.XtraTreeList.TreeList tlMain;
        private DevExpress.XtraEditors.GroupControl groupTop;
        private DevExpress.XtraGrid.GridControl gcUserInRole;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUserInRole;
        private DevExpress.XtraEditors.TextEdit txtFocus;
        private DevExpress.XtraEditors.SplitContainerControl splitCCMain;
        private DevExpress.XtraEditors.SimpleButton btnView;
    }
}