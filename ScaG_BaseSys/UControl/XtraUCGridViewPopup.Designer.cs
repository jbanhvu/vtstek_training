using CNY_BaseSys;

namespace CNY_BaseSys.UControl
{
    partial class XtraUCGridViewPopup
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
            this.popupContainerGv = new DevExpress.XtraEditors.PopupContainerControl();
            this.splitContainerMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnFinish = new DevExpress.XtraEditors.SimpleButton();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new CNY_BaseSys.TransferDataGridView();
            this.ppEdit = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerGv)).BeginInit();
            this.popupContainerGv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerGv
            // 
            this.popupContainerGv.Controls.Add(this.splitContainerMain);
            this.popupContainerGv.Location = new System.Drawing.Point(101, 3);
            this.popupContainerGv.Name = "popupContainerGv";
            this.popupContainerGv.Size = new System.Drawing.Size(600, 450);
            this.popupContainerGv.TabIndex = 29;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Horizontal = false;
            this.splitContainerMain.IsSplitterFixed = true;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Panel1.Controls.Add(this.btnFinish);
            this.splitContainerMain.Panel1.Text = "Panel1";
            this.splitContainerMain.Panel2.Controls.Add(this.gcMain);
            this.splitContainerMain.Panel2.Text = "Panel2";
            this.splitContainerMain.Size = new System.Drawing.Size(600, 450);
            this.splitContainerMain.SplitterPosition = 31;
            this.splitContainerMain.TabIndex = 1;
            this.splitContainerMain.Text = "splitContainerControl1";
            // 
            // btnFinish
            // 
            this.btnFinish.ImageOptions.Image = global::CNY_BaseSys.Properties.Resources.apply_16x163;
            this.btnFinish.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnFinish.Location = new System.Drawing.Point(3, 3);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(63, 27);
            this.btnFinish.TabIndex = 2;
            this.btnFinish.Text = "Finish";
            // 
            // gcMain
            // 
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(600, 414);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            // 
            // ppEdit
            // 
            this.ppEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ppEdit.Location = new System.Drawing.Point(0, 0);
            this.ppEdit.Name = "ppEdit";
            this.ppEdit.Properties.AutoHeight = false;
            this.ppEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ppEdit.Properties.PopupControl = this.popupContainerGv;
            this.ppEdit.Size = new System.Drawing.Size(1097, 553);
            this.ppEdit.TabIndex = 30;
            // 
            // XtraUCGridViewPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerGv);
            this.Controls.Add(this.ppEdit);
            this.Name = "XtraUCGridViewPopup";
            this.Size = new System.Drawing.Size(1097, 553);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerGv)).EndInit();
            this.popupContainerGv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerControl popupContainerGv;
        private DevExpress.XtraEditors.PopupContainerEdit ppEdit;
        private DevExpress.XtraGrid.GridControl gcMain;
        private TransferDataGridView gvMain;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerMain;
        private DevExpress.XtraEditors.SimpleButton btnFinish;
    }
}
