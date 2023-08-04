

namespace CNY_BaseSys.UControl
{
    partial class XtraUcTreePopupFi
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
            this.popupCCTree = new DevExpress.XtraEditors.PopupContainerControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.tlMain = new CNY_BaseSys.Common.PopUpFiTreeList();
            this.btnCheckAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnNextFinish = new DevExpress.XtraEditors.SimpleButton();
            this.ppEdit = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popupCCTree)).BeginInit();
            this.popupCCTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popupCCTree
            // 
            this.popupCCTree.Controls.Add(this.groupControl3);
            this.popupCCTree.Location = new System.Drawing.Point(101, 3);
            this.popupCCTree.Name = "popupCCTree";
            this.popupCCTree.Size = new System.Drawing.Size(600, 450);
            this.popupCCTree.TabIndex = 29;
            // 
            // groupControl3
            // 
            this.groupControl3.CaptionImageOptions.Image = global::CNY_BaseSys.Properties.Resources.ide_24x24;
            this.groupControl3.CaptionLocation = DevExpress.Utils.Locations.Right;
            this.groupControl3.Controls.Add(this.tlMain);
            this.groupControl3.Controls.Add(this.btnCheckAll);
            this.groupControl3.Controls.Add(this.btnNextFinish);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(600, 450);
            this.groupControl3.TabIndex = 15;
            // 
            // tlMain
            // 
            this.tlMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlMain.Location = new System.Drawing.Point(2, 1);
            this.tlMain.Name = "tlMain";
            this.tlMain.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.OptionsClipboard.CopyNodeHierarchy = DevExpress.Utils.DefaultBoolean.True;
            this.tlMain.OptionsFind.AlwaysVisible = true;
            this.tlMain.Size = new System.Drawing.Size(566, 447);
            this.tlMain.TabIndex = 27;
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckAll.Location = new System.Drawing.Point(569, 1);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(32, 32);
            this.btnCheckAll.TabIndex = 6;
            this.btnCheckAll.ToolTip = "Check All";
            // 
            // btnNextFinish
            // 
            this.btnNextFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextFinish.ImageOptions.Image = global::CNY_BaseSys.Properties.Resources.apply_32x32;
            this.btnNextFinish.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNextFinish.Location = new System.Drawing.Point(568, 417);
            this.btnNextFinish.Name = "btnNextFinish";
            this.btnNextFinish.Size = new System.Drawing.Size(32, 32);
            this.btnNextFinish.TabIndex = 10;
            this.btnNextFinish.ToolTip = "Selected Items";
            // 
            // ppEdit
            // 
            this.ppEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ppEdit.Location = new System.Drawing.Point(0, 0);
            this.ppEdit.Name = "ppEdit";
            this.ppEdit.Properties.AutoHeight = false;
            this.ppEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ppEdit.Properties.PopupControl = this.popupCCTree;
            this.ppEdit.Size = new System.Drawing.Size(712, 22);
            this.ppEdit.TabIndex = 30;
            // 
            // XtraUcTreePopupFi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupCCTree);
            this.Controls.Add(this.ppEdit);
            this.Name = "XtraUcTreePopupFi";
            this.Size = new System.Drawing.Size(712, 22);
            ((System.ComponentModel.ISupportInitialize)(this.popupCCTree)).EndInit();
            this.popupCCTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerControl popupCCTree;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private CNY_BaseSys.Common.PopUpFiTreeList tlMain;
        private DevExpress.XtraEditors.SimpleButton btnCheckAll;
        private DevExpress.XtraEditors.SimpleButton btnNextFinish;
        private DevExpress.XtraEditors.PopupContainerEdit ppEdit;
    }
}
