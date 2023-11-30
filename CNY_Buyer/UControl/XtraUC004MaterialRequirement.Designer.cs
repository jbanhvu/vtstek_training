namespace CNY_Buyer.UControl
{
    partial class XtraUC004MaterialRequirement
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.panelControlBottom = new DevExpress.XtraEditors.PanelControl();
            this.lblCurrentRecord = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtPageSize = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDisplayInfo = new DevExpress.XtraEditors.LabelControl();
            this.btnFirstPage = new DevExpress.XtraEditors.SimpleButton();
            this.btnNextPage = new DevExpress.XtraEditors.SimpleButton();
            this.btnPreviousPage = new DevExpress.XtraEditors.SimpleButton();
            this.btnLastPage = new DevExpress.XtraEditors.SimpleButton();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBottom)).BeginInit();
            this.panelControlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlBottom
            // 
            this.panelControlBottom.Controls.Add(this.lblCurrentRecord);
            this.panelControlBottom.Controls.Add(this.labelControl2);
            this.panelControlBottom.Controls.Add(this.txtPageSize);
            this.panelControlBottom.Controls.Add(this.lblDisplayInfo);
            this.panelControlBottom.Controls.Add(this.btnFirstPage);
            this.panelControlBottom.Controls.Add(this.btnNextPage);
            this.panelControlBottom.Controls.Add(this.btnPreviousPage);
            this.panelControlBottom.Controls.Add(this.btnLastPage);
            this.panelControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlBottom.Location = new System.Drawing.Point(0, 388);
            this.panelControlBottom.Name = "panelControlBottom";
            this.panelControlBottom.Size = new System.Drawing.Size(938, 23);
            this.panelControlBottom.TabIndex = 3;
            // 
            // lblCurrentRecord
            // 
            this.lblCurrentRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentRecord.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCurrentRecord.Location = new System.Drawing.Point(267, 6);
            this.lblCurrentRecord.Name = "lblCurrentRecord";
            this.lblCurrentRecord.Size = new System.Drawing.Size(96, 13);
            this.lblCurrentRecord.TabIndex = 15;
            this.lblCurrentRecord.Text = "lblCurrentRecord";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(723, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(43, 13);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "PageSize";
            // 
            // txtPageSize
            // 
            this.txtPageSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPageSize.Location = new System.Drawing.Point(771, 2);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Properties.AutoHeight = false;
            this.txtPageSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtPageSize.Size = new System.Drawing.Size(67, 19);
            this.txtPageSize.TabIndex = 8;
            // 
            // lblDisplayInfo
            // 
            this.lblDisplayInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDisplayInfo.Location = new System.Drawing.Point(501, 6);
            this.lblDisplayInfo.Name = "lblDisplayInfo";
            this.lblDisplayInfo.Size = new System.Drawing.Size(64, 13);
            this.lblDisplayInfo.TabIndex = 13;
            this.lblDisplayInfo.Text = "lblDisplayInfo";
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirstPage.Image = global::CNY_Buyer.Properties.Resources.Actions_go_first_view_icon;
            this.btnFirstPage.Location = new System.Drawing.Point(841, 2);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(23, 19);
            this.btnFirstPage.TabIndex = 9;
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextPage.Image = global::CNY_Buyer.Properties.Resources.Actions_go_next_view_icon;
            this.btnNextPage.Location = new System.Drawing.Point(889, 2);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(23, 19);
            this.btnNextPage.TabIndex = 11;
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviousPage.Image = global::CNY_Buyer.Properties.Resources.Actions_go_previous_view_icon;
            this.btnPreviousPage.Location = new System.Drawing.Point(865, 2);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(23, 19);
            this.btnPreviousPage.TabIndex = 10;
            // 
            // btnLastPage
            // 
            this.btnLastPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLastPage.Image = global::CNY_Buyer.Properties.Resources.Actions_go_last_view_icon;
            this.btnLastPage.Location = new System.Drawing.Point(913, 2);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(23, 19);
            this.btnLastPage.TabIndex = 12;
            this.btnLastPage.ToolTip = "Last Page";
            // 
            // gcMain
            // 
            this.gcMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gcMain.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(938, 388);
            this.gcMain.TabIndex = 4;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            // 
            // XtraUC004MaterialRequirement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panelControlBottom);
            this.Name = "XtraUC004MaterialRequirement";
            this.Size = new System.Drawing.Size(938, 411);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBottom)).EndInit();
            this.panelControlBottom.ResumeLayout(false);
            this.panelControlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlBottom;
        private DevExpress.XtraEditors.LabelControl lblCurrentRecord;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit txtPageSize;
        private DevExpress.XtraEditors.LabelControl lblDisplayInfo;
        private DevExpress.XtraEditors.SimpleButton btnNextPage;
        private DevExpress.XtraEditors.SimpleButton btnPreviousPage;
        private DevExpress.XtraEditors.SimpleButton btnLastPage;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.SimpleButton btnFirstPage;
    }
}
