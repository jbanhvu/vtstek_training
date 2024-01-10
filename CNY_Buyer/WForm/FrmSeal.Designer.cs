namespace CNY_Buyer.WForm
{
    partial class FrmSeal
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gcSeal = new DevExpress.XtraGrid.GridControl();
            this.gvSeal = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtUpdatedDate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtUpdatedBy = new DevExpress.XtraEditors.TextEdit();
            this.txtCreatedDate = new DevExpress.XtraEditors.TextEdit();
            this.txtCreatedBy = new DevExpress.XtraEditors.TextEdit();
            this.txtContent = new DevExpress.XtraEditors.TextEdit();
            this.txtSealDate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtStaffPK = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSeal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSeal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdatedDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdatedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSealDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStaffPK.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gcSeal);
            this.groupBox2.Location = new System.Drawing.Point(12, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 299);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // gcSeal
            // 
            this.gcSeal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSeal.Location = new System.Drawing.Point(3, 19);
            this.gcSeal.MainView = this.gvSeal;
            this.gcSeal.Name = "gcSeal";
            this.gcSeal.Size = new System.Drawing.Size(770, 277);
            this.gcSeal.TabIndex = 0;
            this.gcSeal.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSeal});
            this.gcSeal.Click += new System.EventHandler(this.gcSeal_Click);
            // 
            // gvSeal
            // 
            this.gvSeal.GridControl = this.gcSeal;
            this.gvSeal.Name = "gvSeal";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(559, 57);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(81, 16);
            this.labelControl7.TabIndex = 28;
            this.labelControl7.Text = "Ngày cập nhật";
            // 
            // txtUpdatedDate
            // 
            this.txtUpdatedDate.Location = new System.Drawing.Point(660, 54);
            this.txtUpdatedDate.Name = "txtUpdatedDate";
            this.txtUpdatedDate.Size = new System.Drawing.Size(125, 22);
            this.txtUpdatedDate.TabIndex = 27;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(559, 13);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(86, 16);
            this.labelControl6.TabIndex = 26;
            this.labelControl6.Text = "Người cập nhật";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(288, 57);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(50, 16);
            this.labelControl5.TabIndex = 25;
            this.labelControl5.Text = "Ngày tạo";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(288, 18);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(55, 16);
            this.labelControl4.TabIndex = 24;
            this.labelControl4.Text = "Người tạo";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 16);
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "Nội dung";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(81, 16);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "Ngày đóng gói";
            // 
            // txtUpdatedBy
            // 
            this.txtUpdatedBy.Location = new System.Drawing.Point(660, 10);
            this.txtUpdatedBy.Name = "txtUpdatedBy";
            this.txtUpdatedBy.Size = new System.Drawing.Size(125, 22);
            this.txtUpdatedBy.TabIndex = 20;
            // 
            // txtCreatedDate
            // 
            this.txtCreatedDate.Location = new System.Drawing.Point(356, 54);
            this.txtCreatedDate.Name = "txtCreatedDate";
            this.txtCreatedDate.Size = new System.Drawing.Size(125, 22);
            this.txtCreatedDate.TabIndex = 19;
            // 
            // txtCreatedBy
            // 
            this.txtCreatedBy.Location = new System.Drawing.Point(356, 15);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.Size = new System.Drawing.Size(125, 22);
            this.txtCreatedBy.TabIndex = 18;
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(111, 54);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(125, 22);
            this.txtContent.TabIndex = 16;
            // 
            // txtSealDate
            // 
            this.txtSealDate.Location = new System.Drawing.Point(111, 15);
            this.txtSealDate.Name = "txtSealDate";
            this.txtSealDate.Size = new System.Drawing.Size(125, 22);
            this.txtSealDate.TabIndex = 15;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 97);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(56, 16);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "Nhân viên";
            // 
            // txtStaffPK
            // 
            this.txtStaffPK.Location = new System.Drawing.Point(111, 94);
            this.txtStaffPK.Name = "txtStaffPK";
            this.txtStaffPK.Size = new System.Drawing.Size(125, 22);
            this.txtStaffPK.TabIndex = 29;
            // 
            // FrmSeal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtStaffPK);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.txtUpdatedDate);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtUpdatedBy);
            this.Controls.Add(this.txtCreatedDate);
            this.Controls.Add(this.txtCreatedBy);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtSealDate);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmSeal";
            this.Text = "FrmSeal";
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSeal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSeal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdatedDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdatedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSealDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStaffPK.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gcSeal;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSeal;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtUpdatedDate;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtUpdatedBy;
        private DevExpress.XtraEditors.TextEdit txtCreatedDate;
        private DevExpress.XtraEditors.TextEdit txtCreatedBy;
        private DevExpress.XtraEditors.TextEdit txtContent;
        private DevExpress.XtraEditors.TextEdit txtSealDate;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtStaffPK;
    }
}