namespace CNY_Buyer.WForm
{
    partial class Frm001PurchaseRequisitionSelectItem
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnNextFinish = new DevExpress.XtraEditors.SimpleButton();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.pcButton = new DevExpress.XtraEditors.PanelControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pageCategory = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.tlItem = new DevExpress.XtraTreeList.TreeList();
            this.pageSO = new DevExpress.XtraTab.XtraTabPage();
            this.grpTop = new DevExpress.XtraEditors.GroupControl();
            this.tlSO = new DevExpress.XtraTreeList.TreeList();
            this.panelHLine = new DevExpress.XtraEditors.PanelControl();
            this.pcTopGI = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabMain = new DevExpress.XtraTab.XtraTabControl();
            this.chkCheckRequest = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.chkItem = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pcButton)).BeginInit();
            this.pcButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.pageCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlItem)).BeginInit();
            this.pageSO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpTop)).BeginInit();
            this.grpTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlSO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelHLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTopGI)).BeginInit();
            this.pcTopGI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabMain)).BeginInit();
            this.xtraTabMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckRequest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkItem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = global::CNY_Buyer.Properties.Resources.cancel_24x24_W;
            this.btnCancel.Location = new System.Drawing.Point(742, 45);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTip = "Press (Ctrl+Shift+C)";
            // 
            // btnNextFinish
            // 
            this.btnNextFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextFinish.ImageOptions.Image = global::CNY_Buyer.Properties.Resources.forward_24x24_W;
            this.btnNextFinish.Location = new System.Drawing.Point(928, 45);
            this.btnNextFinish.Name = "btnNextFinish";
            this.btnNextFinish.Size = new System.Drawing.Size(75, 28);
            this.btnNextFinish.TabIndex = 10;
            this.btnNextFinish.Text = "Next";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.ImageOptions.Image = global::CNY_Buyer.Properties.Resources.backward_24x24_W;
            this.btnBack.Location = new System.Drawing.Point(847, 45);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 28);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "Back";
            this.btnBack.ToolTip = "Press (Ctrl+Shift+B)";
            // 
            // pcButton
            // 
            this.pcButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcButton.Controls.Add(this.btnBack);
            this.pcButton.Controls.Add(this.btnCancel);
            this.pcButton.Controls.Add(this.btnNextFinish);
            this.pcButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pcButton.Location = new System.Drawing.Point(0, 486);
            this.pcButton.Name = "pcButton";
            this.pcButton.Size = new System.Drawing.Size(1007, 76);
            this.pcButton.TabIndex = 13;
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // pageCategory
            // 
            this.pageCategory.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageCategory.Appearance.Header.Options.UseFont = true;
            this.pageCategory.Controls.Add(this.groupControl1);
            this.pageCategory.ImageOptions.Image = global::CNY_Buyer.Properties.Resources.Number_2_icon;
            this.pageCategory.Name = "pageCategory";
            this.pageCategory.Size = new System.Drawing.Size(1005, 458);
            toolTipTitleItem1.Appearance.Image = global::CNY_Buyer.Properties.Resources.info_16x16;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.ImageOptions.Image = global::CNY_Buyer.Properties.Resources.info_16x16;
            toolTipTitleItem1.Text = "Information";
            toolTipItem1.Appearance.Image = global::CNY_Buyer.Properties.Resources.feature_16x16;
            toolTipItem1.Appearance.Options.UseImage = true;
            toolTipItem1.ImageOptions.Image = global::CNY_Buyer.Properties.Resources.feature_16x16;
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Mục tiêu: Chọn vật tư cần mua";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.pageCategory.SuperTip = superToolTip1;
            this.pageCategory.Tag = "2";
            this.pageCategory.Text = "Chọn vật tư";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Crimson;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.tlItem);
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1005, 458);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Mục tiêu: Chọn vật tư cần mua";
            // 
            // tlItem
            // 
            this.tlItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlItem.Location = new System.Drawing.Point(2, 57);
            this.tlItem.Name = "tlItem";
            this.tlItem.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlItem.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlItem.Size = new System.Drawing.Size(1001, 399);
            this.tlItem.TabIndex = 29;
            // 
            // pageSO
            // 
            this.pageSO.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageSO.Appearance.Header.Options.UseFont = true;
            this.pageSO.Controls.Add(this.grpTop);
            this.pageSO.ImageOptions.Image = global::CNY_Buyer.Properties.Resources.Number_1_icon;
            this.pageSO.Name = "pageSO";
            this.pageSO.Size = new System.Drawing.Size(1005, 458);
            toolTipTitleItem2.Appearance.Image = global::CNY_Buyer.Properties.Resources.info_16x16;
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.ImageOptions.Image = global::CNY_Buyer.Properties.Resources.info_16x16;
            toolTipTitleItem2.Text = "Information";
            toolTipItem2.Appearance.Image = global::CNY_Buyer.Properties.Resources.feature_16x16;
            toolTipItem2.Appearance.Options.UseImage = true;
            toolTipItem2.ImageOptions.Image = global::CNY_Buyer.Properties.Resources.feature_16x16;
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Mục tiêu: Chọn ít nhất một yêu cầu";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.pageSO.SuperTip = superToolTip2;
            this.pageSO.Tag = "1";
            this.pageSO.Text = "Chọn yêu cầu";
            // 
            // grpTop
            // 
            this.grpTop.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTop.AppearanceCaption.ForeColor = System.Drawing.Color.Crimson;
            this.grpTop.AppearanceCaption.Options.UseFont = true;
            this.grpTop.AppearanceCaption.Options.UseForeColor = true;
            this.grpTop.Controls.Add(this.tlSO);
            this.grpTop.Controls.Add(this.panelHLine);
            this.grpTop.Controls.Add(this.pcTopGI);
            this.grpTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTop.Location = new System.Drawing.Point(0, 0);
            this.grpTop.Name = "grpTop";
            this.grpTop.Size = new System.Drawing.Size(1005, 458);
            this.grpTop.TabIndex = 1;
            this.grpTop.Text = "Mục tiêu: Chọn ít nhất một yêu cầu";
            // 
            // tlSO
            // 
            this.tlSO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlSO.Location = new System.Drawing.Point(2, 57);
            this.tlSO.Name = "tlSO";
            this.tlSO.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlSO.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlSO.Size = new System.Drawing.Size(1001, 399);
            this.tlSO.TabIndex = 28;
            // 
            // panelHLine
            // 
            this.panelHLine.AllowDrop = true;
            this.panelHLine.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelHLine.ContentImage = global::CNY_Buyer.Properties.Resources.HPermissionLine;
            this.panelHLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHLine.Location = new System.Drawing.Point(2, 48);
            this.panelHLine.Name = "panelHLine";
            this.panelHLine.Size = new System.Drawing.Size(1001, 9);
            this.panelHLine.TabIndex = 503;
            // 
            // pcTopGI
            // 
            this.pcTopGI.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcTopGI.Controls.Add(this.chkCheckRequest);
            this.pcTopGI.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcTopGI.Location = new System.Drawing.Point(2, 23);
            this.pcTopGI.Name = "pcTopGI";
            this.pcTopGI.Size = new System.Drawing.Size(1001, 25);
            this.pcTopGI.TabIndex = 504;
            // 
            // xtraTabMain
            // 
            this.xtraTabMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabMain.Appearance.Options.UseBackColor = true;
            this.xtraTabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabMain.Location = new System.Drawing.Point(0, 0);
            this.xtraTabMain.Name = "xtraTabMain";
            this.xtraTabMain.SelectedTabPage = this.pageSO;
            this.xtraTabMain.Size = new System.Drawing.Size(1007, 486);
            this.xtraTabMain.TabIndex = 29;
            this.xtraTabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageSO,
            this.pageCategory});
            // 
            // chkCheckRequest
            // 
            this.chkCheckRequest.Location = new System.Drawing.Point(10, 4);
            this.chkCheckRequest.Name = "chkCheckRequest";
            this.chkCheckRequest.Properties.Caption = "Chọn tất cả";
            this.chkCheckRequest.Size = new System.Drawing.Size(147, 20);
            this.chkCheckRequest.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.AllowDrop = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.ContentImage = global::CNY_Buyer.Properties.Resources.HPermissionLine;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 48);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1001, 9);
            this.panelControl1.TabIndex = 505;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.chkItem);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 23);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1001, 25);
            this.panelControl2.TabIndex = 506;
            // 
            // chkItem
            // 
            this.chkItem.Location = new System.Drawing.Point(10, 4);
            this.chkItem.Name = "chkItem";
            this.chkItem.Properties.Caption = "Chọn tất cả vật tư";
            this.chkItem.Size = new System.Drawing.Size(147, 20);
            this.chkItem.TabIndex = 0;
            // 
            // Frm001PurchaseRequisitionSelectItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1007, 562);
            this.Controls.Add(this.xtraTabMain);
            this.Controls.Add(this.pcButton);
            this.Name = "Frm001PurchaseRequisitionSelectItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PO Wizard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pcButton)).EndInit();
            this.pcButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.pageCategory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlItem)).EndInit();
            this.pageSO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpTop)).EndInit();
            this.grpTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlSO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelHLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTopGI)).EndInit();
            this.pcTopGI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabMain)).EndInit();
            this.xtraTabMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckRequest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkItem.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnNextFinish;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.PanelControl pcButton;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraTab.XtraTabPage pageCategory;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraTreeList.TreeList tlItem;
        private DevExpress.XtraTab.XtraTabPage pageSO;
        private DevExpress.XtraEditors.GroupControl grpTop;
        private DevExpress.XtraTreeList.TreeList tlSO;
        private DevExpress.XtraEditors.PanelControl panelHLine;
        private DevExpress.XtraEditors.PanelControl pcTopGI;
        private DevExpress.XtraTab.XtraTabControl xtraTabMain;
        private DevExpress.XtraEditors.CheckEdit chkCheckRequest;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CheckEdit chkItem;
    }
}