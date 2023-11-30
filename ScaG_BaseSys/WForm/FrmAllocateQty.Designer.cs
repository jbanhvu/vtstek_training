namespace CNY_BaseSys.WForm
{
    partial class FrmAllocateQty
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
            this.grpInfo = new DevExpress.XtraEditors.GroupControl();
            this.txtDemandQty = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtTotalDemand = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtStockQty = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new CNY_BaseSys.TransferDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo)).BeginInit();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDemandQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalDemand.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStockQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInfo
            // 
            this.grpInfo.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInfo.AppearanceCaption.Options.UseFont = true;
            this.grpInfo.Controls.Add(this.txtDemandQty);
            this.grpInfo.Controls.Add(this.labelControl1);
            this.grpInfo.Controls.Add(this.txtTotalDemand);
            this.grpInfo.Controls.Add(this.labelControl3);
            this.grpInfo.Controls.Add(this.txtStockQty);
            this.grpInfo.Controls.Add(this.labelControl17);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpInfo.Location = new System.Drawing.Point(0, 0);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(784, 46);
            this.grpInfo.TabIndex = 2;
            this.grpInfo.Text = "Quantity Info";
            // 
            // txtDemandQty
            // 
            this.txtDemandQty.EditValue = "";
            this.txtDemandQty.Location = new System.Drawing.Point(442, 21);
            this.txtDemandQty.Name = "txtDemandQty";
            this.txtDemandQty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDemandQty.Properties.Appearance.ForeColor = System.Drawing.Color.Purple;
            this.txtDemandQty.Properties.Appearance.Options.UseFont = true;
            this.txtDemandQty.Properties.Appearance.Options.UseForeColor = true;
            this.txtDemandQty.Properties.AutoHeight = false;
            this.txtDemandQty.Size = new System.Drawing.Size(120, 22);
            this.txtDemandQty.TabIndex = 637;
            this.txtDemandQty.Tag = "0";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(377, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 13);
            this.labelControl1.TabIndex = 638;
            this.labelControl1.Text = "Demand Qty";
            // 
            // txtTotalDemand
            // 
            this.txtTotalDemand.EditValue = "";
            this.txtTotalDemand.Location = new System.Drawing.Point(76, 21);
            this.txtTotalDemand.Name = "txtTotalDemand";
            this.txtTotalDemand.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDemand.Properties.Appearance.ForeColor = System.Drawing.Color.Purple;
            this.txtTotalDemand.Properties.Appearance.Options.UseFont = true;
            this.txtTotalDemand.Properties.Appearance.Options.UseForeColor = true;
            this.txtTotalDemand.Properties.AutoHeight = false;
            this.txtTotalDemand.Size = new System.Drawing.Size(120, 22);
            this.txtTotalDemand.TabIndex = 605;
            this.txtTotalDemand.Tag = "0";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 25);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(66, 13);
            this.labelControl3.TabIndex = 627;
            this.labelControl3.Text = "Total Demand";
            // 
            // txtStockQty
            // 
            this.txtStockQty.EditValue = "";
            this.txtStockQty.Location = new System.Drawing.Point(252, 21);
            this.txtStockQty.Name = "txtStockQty";
            this.txtStockQty.Properties.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.txtStockQty.Properties.Appearance.Options.UseForeColor = true;
            this.txtStockQty.Properties.AutoHeight = false;
            this.txtStockQty.Size = new System.Drawing.Size(120, 22);
            this.txtStockQty.TabIndex = 621;
            this.txtStockQty.Tag = "VersionCode";
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(201, 26);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(47, 13);
            this.labelControl17.TabIndex = 636;
            this.labelControl17.Text = "Stock Qty";
            // 
            // gcMain
            // 
            this.gcMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 46);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(784, 515);
            this.gcMain.TabIndex = 3;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            // 
            // FrmAllocateQty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.grpInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmAllocateQty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Allocate Quantity";
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo)).EndInit();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDemandQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalDemand.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStockQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpInfo;
        private DevExpress.XtraEditors.TextEdit txtTotalDemand;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtStockQty;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraGrid.GridControl gcMain;
        private TransferDataGridView gvMain;
        private DevExpress.XtraEditors.TextEdit txtDemandQty;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}