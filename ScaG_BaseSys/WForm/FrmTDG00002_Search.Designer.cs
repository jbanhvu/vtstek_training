namespace CNY_BaseSys.WForm
{
    partial class FrmTDG00002_Search
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnNextFinish = new DevExpress.XtraEditors.SimpleButton();
            this.pcButton = new DevExpress.XtraEditors.PanelControl();
            this.txtFocus = new DevExpress.XtraEditors.TextEdit();
            this.splitCCMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupAvailable = new DevExpress.XtraEditors.GroupControl();
            this.gcAttribute = new DevExpress.XtraGrid.GridControl();
            this.gvAttribute = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupSelected = new DevExpress.XtraEditors.GroupControl();
            this.gcValue = new DevExpress.XtraGrid.GridControl();
            this.gvValue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.btnRemoveAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectOneRow = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveOneRow = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pcButton)).BeginInit();
            this.pcButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFocus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitCCMain)).BeginInit();
            this.splitCCMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupAvailable)).BeginInit();
            this.groupAvailable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAttribute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAttribute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupSelected)).BeginInit();
            this.groupSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::CNY_BaseSys.Properties.Resources.Exitimage;
            this.btnCancel.Location = new System.Drawing.Point(859, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTip = "Close Search Form";
            // 
            // btnNextFinish
            // 
            this.btnNextFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextFinish.Image = global::CNY_BaseSys.Properties.Resources.Search_image;
            this.btnNextFinish.Location = new System.Drawing.Point(783, 2);
            this.btnNextFinish.Name = "btnNextFinish";
            this.btnNextFinish.Size = new System.Drawing.Size(75, 28);
            this.btnNextFinish.TabIndex = 10;
            this.btnNextFinish.Text = "Find";
            this.btnNextFinish.ToolTip = "Search Data Form Database By Expression";
            // 
            // pcButton
            // 
            this.pcButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcButton.Controls.Add(this.txtFocus);
            this.pcButton.Controls.Add(this.btnCancel);
            this.pcButton.Controls.Add(this.btnNextFinish);
            this.pcButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pcButton.Location = new System.Drawing.Point(0, 530);
            this.pcButton.Name = "pcButton";
            this.pcButton.Size = new System.Drawing.Size(937, 32);
            this.pcButton.TabIndex = 13;
            // 
            // txtFocus
            // 
            this.txtFocus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtFocus.Location = new System.Drawing.Point(462, 20);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.Properties.AutoHeight = false;
            this.txtFocus.Size = new System.Drawing.Size(10, 10);
            this.txtFocus.TabIndex = 16;
            // 
            // splitCCMain
            // 
            this.splitCCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCCMain.Location = new System.Drawing.Point(0, 0);
            this.splitCCMain.Name = "splitCCMain";
            this.splitCCMain.Panel1.Controls.Add(this.groupAvailable);
            this.splitCCMain.Panel1.Text = "Panel1";
            this.splitCCMain.Panel2.Controls.Add(this.groupSelected);
            this.splitCCMain.Panel2.Controls.Add(this.panelControl5);
            this.splitCCMain.Panel2.Text = "Panel2";
            this.splitCCMain.Size = new System.Drawing.Size(937, 530);
            this.splitCCMain.SplitterPosition = 247;
            this.splitCCMain.TabIndex = 15;
            this.splitCCMain.Text = "splitContainerControl1";
            // 
            // groupAvailable
            // 
            this.groupAvailable.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupAvailable.AppearanceCaption.Options.UseFont = true;
            this.groupAvailable.Controls.Add(this.gcAttribute);
            this.groupAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAvailable.Location = new System.Drawing.Point(0, 0);
            this.groupAvailable.Name = "groupAvailable";
            this.groupAvailable.Size = new System.Drawing.Size(247, 530);
            this.groupAvailable.TabIndex = 19;
            this.groupAvailable.Text = "Available Reference";
            // 
            // gcAttribute
            // 
            this.gcAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAttribute.Location = new System.Drawing.Point(2, 20);
            this.gcAttribute.MainView = this.gvAttribute;
            this.gcAttribute.Name = "gcAttribute";
            this.gcAttribute.Size = new System.Drawing.Size(243, 508);
            this.gcAttribute.TabIndex = 3;
            this.gcAttribute.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAttribute});
            // 
            // gvAttribute
            // 
            this.gvAttribute.GridControl = this.gcAttribute;
            this.gvAttribute.Name = "gvAttribute";
            // 
            // groupSelected
            // 
            this.groupSelected.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupSelected.AppearanceCaption.Options.UseFont = true;
            this.groupSelected.Controls.Add(this.gcValue);
            this.groupSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSelected.Location = new System.Drawing.Point(46, 0);
            this.groupSelected.Name = "groupSelected";
            this.groupSelected.Size = new System.Drawing.Size(639, 530);
            this.groupSelected.TabIndex = 20;
            this.groupSelected.Text = "Selected Reference";
            // 
            // gcValue
            // 
            this.gcValue.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcValue.Location = new System.Drawing.Point(2, 20);
            this.gcValue.MainView = this.gvValue;
            this.gcValue.Name = "gcValue";
            this.gcValue.Size = new System.Drawing.Size(635, 508);
            this.gcValue.TabIndex = 8;
            this.gcValue.UseEmbeddedNavigator = true;
            this.gcValue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvValue});
            // 
            // gvValue
            // 
            this.gvValue.GridControl = this.gcValue;
            this.gvValue.Name = "gvValue";
            // 
            // panelControl5
            // 
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Controls.Add(this.btnRemoveAll);
            this.panelControl5.Controls.Add(this.btnSelectOneRow);
            this.panelControl5.Controls.Add(this.btnRemoveOneRow);
            this.panelControl5.Controls.Add(this.btnSelectAll);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl5.Location = new System.Drawing.Point(0, 0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(46, 530);
            this.panelControl5.TabIndex = 2;
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRemoveAll.Image = global::CNY_BaseSys.Properties.Resources.Actions_go_first_view_icon_32;
            this.btnRemoveAll.Location = new System.Drawing.Point(0, 387);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(43, 43);
            this.btnRemoveAll.TabIndex = 7;
            this.btnRemoveAll.ToolTip = "Remove All Rows Selected (Press Control+1 or Click button)";
            // 
            // btnSelectOneRow
            // 
            this.btnSelectOneRow.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSelectOneRow.Image = global::CNY_BaseSys.Properties.Resources.Actions_go_next_view_icon_32;
            this.btnSelectOneRow.Location = new System.Drawing.Point(0, 77);
            this.btnSelectOneRow.Name = "btnSelectOneRow";
            this.btnSelectOneRow.Size = new System.Drawing.Size(43, 43);
            this.btnSelectOneRow.TabIndex = 4;
            this.btnSelectOneRow.ToolTip = "Select Row Focus Available (Press Control+3 or Click button)";
            // 
            // btnRemoveOneRow
            // 
            this.btnRemoveOneRow.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRemoveOneRow.Image = global::CNY_BaseSys.Properties.Resources.Actions_go_previous_view_icon_32;
            this.btnRemoveOneRow.Location = new System.Drawing.Point(0, 341);
            this.btnRemoveOneRow.Name = "btnRemoveOneRow";
            this.btnRemoveOneRow.Size = new System.Drawing.Size(43, 43);
            this.btnRemoveOneRow.TabIndex = 6;
            this.btnRemoveOneRow.ToolTip = "Remove Row Focus Selected (Press Control+2 or Click button)";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSelectAll.Image = global::CNY_BaseSys.Properties.Resources.Actions_go_last_view_icon_32;
            this.btnSelectAll.Location = new System.Drawing.Point(0, 123);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(43, 43);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.ToolTip = "Select All RowsAvailable (Press Control+4 or Click button)";
            // 
            // FrmTDG00002_Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(937, 562);
            this.Controls.Add(this.splitCCMain);
            this.Controls.Add(this.pcButton);
            this.Name = "FrmTDG00002_Search";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search RM Code";
            ((System.ComponentModel.ISupportInitialize)(this.pcButton)).EndInit();
            this.pcButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFocus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitCCMain)).EndInit();
            this.splitCCMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupAvailable)).EndInit();
            this.groupAvailable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAttribute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAttribute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupSelected)).EndInit();
            this.groupSelected.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnNextFinish;
        private DevExpress.XtraEditors.PanelControl pcButton;
        private DevExpress.XtraEditors.TextEdit txtFocus;
        private DevExpress.XtraEditors.SplitContainerControl splitCCMain;
        private DevExpress.XtraEditors.GroupControl groupAvailable;
        private DevExpress.XtraGrid.GridControl gcAttribute;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAttribute;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.SimpleButton btnRemoveAll;
        private DevExpress.XtraEditors.SimpleButton btnSelectOneRow;
        private DevExpress.XtraEditors.SimpleButton btnRemoveOneRow;
        private DevExpress.XtraEditors.SimpleButton btnSelectAll;
        private DevExpress.XtraEditors.GroupControl groupSelected;
        private DevExpress.XtraGrid.GridControl gcValue;
        private DevExpress.XtraGrid.Views.Grid.GridView gvValue;

    }
}