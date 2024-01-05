namespace CNY_Buyer.WForm
{
    partial class FrmVehicle
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gcVehicle = new DevExpress.XtraGrid.GridControl();
            this.gvVehicle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtRegistration = new DevExpress.XtraEditors.TextEdit();
            this.txtNote = new DevExpress.XtraEditors.TextEdit();
            this.txtUpdatedBy = new DevExpress.XtraEditors.TextEdit();
            this.txtCreatedDate = new DevExpress.XtraEditors.TextEdit();
            this.txtCreatedBy = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtUpdatedDate = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcVehicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVehicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegistration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdatedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdatedDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gcVehicle);
            this.groupBox1.Location = new System.Drawing.Point(12, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 315);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // gcVehicle
            // 
            this.gcVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcVehicle.Location = new System.Drawing.Point(3, 19);
            this.gcVehicle.MainView = this.gvVehicle;
            this.gcVehicle.Name = "gcVehicle";
            this.gcVehicle.Size = new System.Drawing.Size(770, 293);
            this.gcVehicle.TabIndex = 0;
            this.gcVehicle.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvVehicle});
            this.gcVehicle.Click += new System.EventHandler(this.gcVehicle_Click);
            // 
            // gvVehicle
            // 
            this.gvVehicle.GridControl = this.gcVehicle;
            this.gvVehicle.Name = "gvVehicle";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(82, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(125, 22);
            this.txtName.TabIndex = 1;
            // 
            // txtRegistration
            // 
            this.txtRegistration.Location = new System.Drawing.Point(359, 12);
            this.txtRegistration.Name = "txtRegistration";
            this.txtRegistration.Size = new System.Drawing.Size(125, 22);
            this.txtRegistration.TabIndex = 2;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(660, 12);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(125, 22);
            this.txtNote.TabIndex = 3;
            // 
            // txtUpdatedBy
            // 
            this.txtUpdatedBy.Location = new System.Drawing.Point(660, 51);
            this.txtUpdatedBy.Name = "txtUpdatedBy";
            this.txtUpdatedBy.Size = new System.Drawing.Size(125, 22);
            this.txtUpdatedBy.TabIndex = 6;
            // 
            // txtCreatedDate
            // 
            this.txtCreatedDate.Location = new System.Drawing.Point(359, 51);
            this.txtCreatedDate.Name = "txtCreatedDate";
            this.txtCreatedDate.Size = new System.Drawing.Size(125, 22);
            this.txtCreatedDate.TabIndex = 5;
            // 
            // txtCreatedBy
            // 
            this.txtCreatedBy.Location = new System.Drawing.Point(82, 51);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.Size = new System.Drawing.Size(125, 22);
            this.txtCreatedBy.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 16);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Tên";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(291, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(41, 16);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Biển số";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(559, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(42, 16);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "Ghi chú";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(15, 54);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(55, 16);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "Người tạo";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(291, 54);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(50, 16);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "Ngày tạo";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(559, 54);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(86, 16);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "Người cập nhật";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(559, 98);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(81, 16);
            this.labelControl7.TabIndex = 14;
            this.labelControl7.Text = "Ngày cập nhật";
            // 
            // txtUpdatedDate
            // 
            this.txtUpdatedDate.Location = new System.Drawing.Point(660, 95);
            this.txtUpdatedDate.Name = "txtUpdatedDate";
            this.txtUpdatedDate.Size = new System.Drawing.Size(125, 22);
            this.txtUpdatedDate.TabIndex = 13;
            // 
            // FrmVehicle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.txtUpdatedDate);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtUpdatedBy);
            this.Controls.Add(this.txtCreatedDate);
            this.Controls.Add(this.txtCreatedBy);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.txtRegistration);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmVehicle";
            this.Text = "FrmVehicle";
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcVehicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVehicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegistration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdatedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreatedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdatedDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl gcVehicle;
        private DevExpress.XtraGrid.Views.Grid.GridView gvVehicle;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtRegistration;
        private DevExpress.XtraEditors.TextEdit txtNote;
        private DevExpress.XtraEditors.TextEdit txtUpdatedBy;
        private DevExpress.XtraEditors.TextEdit txtCreatedDate;
        private DevExpress.XtraEditors.TextEdit txtCreatedBy;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtUpdatedDate;
    }
}