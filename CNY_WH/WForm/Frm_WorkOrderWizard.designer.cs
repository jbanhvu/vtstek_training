namespace CNY_WH.WForm
{
    partial class Frm_WorkOrderWizard
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
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnNextFinish = new DevExpress.XtraEditors.SimpleButton();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.pcButton = new DevExpress.XtraEditors.PanelControl();
            this.txtFocus = new DevExpress.XtraEditors.TextEdit();
            this.tlMain = new DevExpress.XtraTreeList.TreeList();
            this.xtraTabMain = new DevExpress.XtraTab.XtraTabControl();
            this.pageSO = new DevExpress.XtraTab.XtraTabPage();
            this.pageCategory = new DevExpress.XtraTab.XtraTabPage();
            this.tlItemCode = new DevExpress.XtraTreeList.TreeList();
            this.pageRM = new DevExpress.XtraTab.XtraTabPage();
            this.tlFinal = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.pcButton)).BeginInit();
            this.pcButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFocus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabMain)).BeginInit();
            this.xtraTabMain.SuspendLayout();
            this.pageSO.SuspendLayout();
            this.pageCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlItemCode)).BeginInit();
            this.pageRM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlFinal)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = global::CNY_WH.Properties.Resources.cancel_24x24_W;
            this.btnCancel.Location = new System.Drawing.Point(742, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTip = "Press (Ctrl+Shift+C)";
            // 
            // btnNextFinish
            // 
            this.btnNextFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextFinish.ImageOptions.Image = global::CNY_WH.Properties.Resources.forward_24x24_W;
            this.btnNextFinish.Location = new System.Drawing.Point(928, 1);
            this.btnNextFinish.Name = "btnNextFinish";
            this.btnNextFinish.Size = new System.Drawing.Size(75, 28);
            this.btnNextFinish.TabIndex = 10;
            this.btnNextFinish.Text = "Next";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.ImageOptions.Image = global::CNY_WH.Properties.Resources.backward_24x24_W;
            this.btnBack.Location = new System.Drawing.Point(847, 1);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 28);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "Back";
            this.btnBack.ToolTip = "Press (Ctrl+Shift+B)";
            // 
            // pcButton
            // 
            this.pcButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcButton.Controls.Add(this.txtFocus);
            this.pcButton.Controls.Add(this.btnBack);
            this.pcButton.Controls.Add(this.btnCancel);
            this.pcButton.Controls.Add(this.btnNextFinish);
            this.pcButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pcButton.Location = new System.Drawing.Point(0, 530);
            this.pcButton.Name = "pcButton";
            this.pcButton.Size = new System.Drawing.Size(1007, 32);
            this.pcButton.TabIndex = 13;
            // 
            // txtFocus
            // 
            this.txtFocus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtFocus.Location = new System.Drawing.Point(497, 20);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.Properties.AutoHeight = false;
            this.txtFocus.Size = new System.Drawing.Size(10, 10);
            this.txtFocus.TabIndex = 16;
            // 
            // tlMain
            // 
            this.tlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlMain.Location = new System.Drawing.Point(0, 0);
            this.tlMain.Name = "tlMain";
            this.tlMain.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.Size = new System.Drawing.Size(978, 524);
            this.tlMain.TabIndex = 28;
            // 
            // xtraTabMain
            // 
            this.xtraTabMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraTabMain.Appearance.Options.UseBackColor = true;
            this.xtraTabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabMain.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabMain.Location = new System.Drawing.Point(0, 0);
            this.xtraTabMain.Name = "xtraTabMain";
            this.xtraTabMain.SelectedTabPage = this.pageSO;
            this.xtraTabMain.Size = new System.Drawing.Size(1007, 530);
            this.xtraTabMain.TabIndex = 29;
            this.xtraTabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageSO,
            this.pageCategory,
            this.pageRM});
            // 
            // pageSO
            // 
            this.pageSO.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageSO.Appearance.Header.Options.UseFont = true;
            this.pageSO.Controls.Add(this.tlMain);
            this.pageSO.ImageOptions.Image = global::CNY_WH.Properties.Resources.Number_1_icon_8_8;
            this.pageSO.Name = "pageSO";
            this.pageSO.Size = new System.Drawing.Size(978, 524);
            toolTipTitleItem1.Appearance.Image = global::CNY_WH.Properties.Resources.info_16x16;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.ImageOptions.Image = global::CNY_WH.Properties.Resources.info_16x16;
            toolTipTitleItem1.Text = "Information";
            toolTipItem1.Appearance.Image = global::CNY_WH.Properties.Resources.feature_16x16;
            toolTipItem1.Appearance.Options.UseImage = true;
            toolTipItem1.ImageOptions.Image = global::CNY_WH.Properties.Resources.feature_16x16;
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "SO Info (Find SO + Select SO)";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.pageSO.SuperTip = superToolTip1;
            this.pageSO.Tag = "1";
            this.pageSO.Text = "Select";
            // 
            // pageCategory
            // 
            this.pageCategory.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageCategory.Appearance.Header.Options.UseFont = true;
            this.pageCategory.Controls.Add(this.tlItemCode);
            this.pageCategory.ImageOptions.Image = global::CNY_WH.Properties.Resources.Number_2_icon_8_8;
            this.pageCategory.Name = "pageCategory";
            this.pageCategory.Size = new System.Drawing.Size(978, 524);
            toolTipTitleItem2.Appearance.Image = global::CNY_WH.Properties.Resources.info_16x16;
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.ImageOptions.Image = global::CNY_WH.Properties.Resources.info_16x16;
            toolTipTitleItem2.Text = "Information";
            toolTipItem2.Appearance.Image = global::CNY_WH.Properties.Resources.feature_16x16;
            toolTipItem2.Appearance.Options.UseImage = true;
            toolTipItem2.ImageOptions.Image = global::CNY_WH.Properties.Resources.feature_16x16;
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Select Item Code";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.pageCategory.SuperTip = superToolTip2;
            this.pageCategory.Tag = "2";
            this.pageCategory.Text = "Select Item";
            // 
            // tlItemCode
            // 
            this.tlItemCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlItemCode.Location = new System.Drawing.Point(0, 0);
            this.tlItemCode.Name = "tlItemCode";
            this.tlItemCode.Size = new System.Drawing.Size(978, 524);
            this.tlItemCode.TabIndex = 0;
            // 
            // pageRM
            // 
            this.pageRM.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageRM.Appearance.Header.Options.UseFont = true;
            this.pageRM.Controls.Add(this.tlFinal);
            this.pageRM.ImageOptions.Image = global::CNY_WH.Properties.Resources.Number_3_icon_8_8;
            this.pageRM.Name = "pageRM";
            this.pageRM.Size = new System.Drawing.Size(978, 524);
            toolTipTitleItem3.Appearance.Image = global::CNY_WH.Properties.Resources.info_16x16;
            toolTipTitleItem3.Appearance.Options.UseImage = true;
            toolTipTitleItem3.ImageOptions.Image = global::CNY_WH.Properties.Resources.info_16x16;
            toolTipTitleItem3.Text = "Information";
            toolTipItem3.Appearance.Image = global::CNY_WH.Properties.Resources.feature_16x16;
            toolTipItem3.Appearance.Options.UseImage = true;
            toolTipItem3.ImageOptions.Image = global::CNY_WH.Properties.Resources.feature_16x16;
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Review Item Selected";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.pageRM.SuperTip = superToolTip3;
            this.pageRM.Tag = "3";
            this.pageRM.Text = "Review Item Selected";
            // 
            // tlFinal
            // 
            this.tlFinal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlFinal.Location = new System.Drawing.Point(0, 0);
            this.tlFinal.Name = "tlFinal";
            this.tlFinal.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlFinal.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlFinal.Size = new System.Drawing.Size(978, 524);
            this.tlFinal.TabIndex = 30;
            // 
            // Frm_WorkOrderWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1007, 562);
            this.Controls.Add(this.xtraTabMain);
            this.Controls.Add(this.pcButton);
            this.Name = "Frm_WorkOrderWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Work Order Wizard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pcButton)).EndInit();
            this.pcButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFocus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabMain)).EndInit();
            this.xtraTabMain.ResumeLayout(false);
            this.pageSO.ResumeLayout(false);
            this.pageCategory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlItemCode)).EndInit();
            this.pageRM.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlFinal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnNextFinish;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.PanelControl pcButton;
        private DevExpress.XtraEditors.TextEdit txtFocus;
        private DevExpress.XtraTreeList.TreeList tlMain;
        private DevExpress.XtraTab.XtraTabControl xtraTabMain;
        private DevExpress.XtraTab.XtraTabPage pageSO;
        private DevExpress.XtraTab.XtraTabPage pageCategory;
        private DevExpress.XtraTab.XtraTabPage pageRM;
        private DevExpress.XtraTreeList.TreeList tlFinal;
        private DevExpress.XtraTreeList.TreeList tlItemCode;
    }
}