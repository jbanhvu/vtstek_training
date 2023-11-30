namespace CNY_BaseSys.WForm
{
    partial class FrmAttributeGenerate
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
            this.splitContainerMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupAvailable = new DevExpress.XtraEditors.GroupControl();
            this.gridControlLoadValue = new DevExpress.XtraGrid.GridControl();
            this.gvLoadValue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupSelected = new DevExpress.XtraEditors.GroupControl();
            this.gridControlSelectValue = new DevExpress.XtraGrid.GridControl();
            this.gvSelectValue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.btnRemoveAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectOneRow = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveOneRow = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pcButton)).BeginInit();
            this.pcButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFocus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupAvailable)).BeginInit();
            this.groupAvailable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLoadValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoadValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupSelected)).BeginInit();
            this.groupSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelectValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::CNY_BaseSys.Properties.Resources.cancel_24x24_W;
            this.btnCancel.Location = new System.Drawing.Point(595, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            // 
            // btnNextFinish
            // 
            this.btnNextFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextFinish.Image = global::CNY_BaseSys.Properties.Resources.apply_24x24_W;
            this.btnNextFinish.Location = new System.Drawing.Point(519, 2);
            this.btnNextFinish.Name = "btnNextFinish";
            this.btnNextFinish.Size = new System.Drawing.Size(75, 28);
            this.btnNextFinish.TabIndex = 10;
            this.btnNextFinish.Text = "Finish";
            // 
            // pcButton
            // 
            this.pcButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcButton.Controls.Add(this.txtFocus);
            this.pcButton.Controls.Add(this.btnCancel);
            this.pcButton.Controls.Add(this.btnNextFinish);
            this.pcButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pcButton.Location = new System.Drawing.Point(0, 420);
            this.pcButton.Name = "pcButton";
            this.pcButton.Size = new System.Drawing.Size(673, 32);
            this.pcButton.TabIndex = 13;
            // 
            // txtFocus
            // 
            this.txtFocus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtFocus.Location = new System.Drawing.Point(330, 20);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.Properties.AutoHeight = false;
            this.txtFocus.Size = new System.Drawing.Size(10, 10);
            this.txtFocus.TabIndex = 16;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Panel1.Controls.Add(this.groupAvailable);
            this.splitContainerMain.Panel1.Text = "Panel1";
            this.splitContainerMain.Panel2.Controls.Add(this.groupSelected);
            this.splitContainerMain.Panel2.Controls.Add(this.panelControl5);
            this.splitContainerMain.Panel2.Text = "Panel2";
            this.splitContainerMain.Size = new System.Drawing.Size(673, 420);
            this.splitContainerMain.SplitterPosition = 289;
            this.splitContainerMain.TabIndex = 0;
            this.splitContainerMain.Text = "splitContainerControl1";
            // 
            // groupAvailable
            // 
            this.groupAvailable.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupAvailable.AppearanceCaption.Options.UseFont = true;
            this.groupAvailable.Controls.Add(this.gridControlLoadValue);
            this.groupAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAvailable.Location = new System.Drawing.Point(0, 0);
            this.groupAvailable.Name = "groupAvailable";
            this.groupAvailable.Size = new System.Drawing.Size(289, 420);
            this.groupAvailable.TabIndex = 18;
            this.groupAvailable.Text = "Available Reference";
            // 
            // gridControlLoadValue
            // 
            this.gridControlLoadValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlLoadValue.Location = new System.Drawing.Point(2, 20);
            this.gridControlLoadValue.MainView = this.gvLoadValue;
            this.gridControlLoadValue.Name = "gridControlLoadValue";
            this.gridControlLoadValue.Size = new System.Drawing.Size(285, 398);
            this.gridControlLoadValue.TabIndex = 3;
            this.gridControlLoadValue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLoadValue});
            // 
            // gvLoadValue
            // 
            this.gvLoadValue.GridControl = this.gridControlLoadValue;
            this.gvLoadValue.Name = "gvLoadValue";
            // 
            // groupSelected
            // 
            this.groupSelected.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupSelected.AppearanceCaption.Options.UseFont = true;
            this.groupSelected.Controls.Add(this.gridControlSelectValue);
            this.groupSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSelected.Location = new System.Drawing.Point(46, 0);
            this.groupSelected.Name = "groupSelected";
            this.groupSelected.Size = new System.Drawing.Size(333, 420);
            this.groupSelected.TabIndex = 19;
            this.groupSelected.Text = "Selected Reference";
            // 
            // gridControlSelectValue
            // 
            this.gridControlSelectValue.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControlSelectValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSelectValue.Location = new System.Drawing.Point(2, 20);
            this.gridControlSelectValue.MainView = this.gvSelectValue;
            this.gridControlSelectValue.Name = "gridControlSelectValue";
            this.gridControlSelectValue.Size = new System.Drawing.Size(329, 398);
            this.gridControlSelectValue.TabIndex = 8;
            this.gridControlSelectValue.UseEmbeddedNavigator = true;
            this.gridControlSelectValue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSelectValue});
            // 
            // gvSelectValue
            // 
            this.gvSelectValue.GridControl = this.gridControlSelectValue;
            this.gvSelectValue.Name = "gvSelectValue";
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
            this.panelControl5.Size = new System.Drawing.Size(46, 420);
            this.panelControl5.TabIndex = 1;
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRemoveAll.Image = global::CNY_BaseSys.Properties.Resources.Actions_go_first_view_icon_32;
            this.btnRemoveAll.Location = new System.Drawing.Point(0, 301);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(43, 43);
            this.btnRemoveAll.TabIndex = 7;
            this.btnRemoveAll.ToolTip = "Remove All Rows Selected (Press Control+1 or Click button)";
            // 
            // btnSelectOneRow
            // 
            this.btnSelectOneRow.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSelectOneRow.Image = global::CNY_BaseSys.Properties.Resources.Actions_go_next_view_icon_32;
            this.btnSelectOneRow.Location = new System.Drawing.Point(0, 67);
            this.btnSelectOneRow.Name = "btnSelectOneRow";
            this.btnSelectOneRow.Size = new System.Drawing.Size(43, 43);
            this.btnSelectOneRow.TabIndex = 4;
            this.btnSelectOneRow.ToolTip = "Select Row Focus Available (Press Control+3 or Click button)";
            // 
            // btnRemoveOneRow
            // 
            this.btnRemoveOneRow.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRemoveOneRow.Image = global::CNY_BaseSys.Properties.Resources.Actions_go_previous_view_icon_32;
            this.btnRemoveOneRow.Location = new System.Drawing.Point(0, 255);
            this.btnRemoveOneRow.Name = "btnRemoveOneRow";
            this.btnRemoveOneRow.Size = new System.Drawing.Size(43, 43);
            this.btnRemoveOneRow.TabIndex = 6;
            this.btnRemoveOneRow.ToolTip = "Remove Row Focus Selected (Press Control+2 or Click button)";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSelectAll.Image = global::CNY_BaseSys.Properties.Resources.Actions_go_last_view_icon_32;
            this.btnSelectAll.Location = new System.Drawing.Point(0, 113);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(43, 43);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.ToolTip = "Select All RowsAvailable (Press Control+4 or Click button)";
            // 
            // FrmAttributeGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(673, 452);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.pcButton);
            this.Name = "FrmAttributeGenerate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attribute Wizard";
            ((System.ComponentModel.ISupportInitialize)(this.pcButton)).EndInit();
            this.pcButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFocus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupAvailable)).EndInit();
            this.groupAvailable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLoadValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoadValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupSelected)).EndInit();
            this.groupSelected.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelectValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnNextFinish;
        private DevExpress.XtraEditors.PanelControl pcButton;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerMain;
        private DevExpress.XtraEditors.GroupControl groupAvailable;
        private DevExpress.XtraGrid.GridControl gridControlLoadValue;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLoadValue;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.SimpleButton btnRemoveAll;
        private DevExpress.XtraEditors.SimpleButton btnSelectOneRow;
        private DevExpress.XtraEditors.SimpleButton btnRemoveOneRow;
        private DevExpress.XtraEditors.SimpleButton btnSelectAll;
        private DevExpress.XtraEditors.GroupControl groupSelected;
        private DevExpress.XtraGrid.GridControl gridControlSelectValue;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSelectValue;
        private DevExpress.XtraEditors.TextEdit txtFocus;
    }
}