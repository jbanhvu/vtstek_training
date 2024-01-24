namespace CNY_Buyer.WForm
{
    partial class FrmLeave
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNote = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.slueStaff = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtStaffNameDesc = new DevExpress.XtraEditors.TextEdit();
            this.deFromDate = new DevExpress.XtraEditors.DateEdit();
            this.teFromDate = new DevExpress.XtraEditors.TimeEdit();
            this.teToDate = new DevExpress.XtraEditors.TimeEdit();
            this.deToDate = new DevExpress.XtraEditors.DateEdit();
            this.gcLeave = new DevExpress.XtraGrid.GridControl();
            this.gvLeave = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtDepartment = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStaff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStaffNameDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLeave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLeave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(402, 55);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(84, 16);
            this.labelControl4.TabIndex = 25;
            this.labelControl4.Text = "Ngày đi làm lại";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(402, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 16);
            this.labelControl2.TabIndex = 23;
            this.labelControl2.Text = "Ngày nghỉ";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 16);
            this.labelControl1.TabIndex = 22;
            this.labelControl1.Text = "Nhân viên";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(87, 87);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(242, 22);
            this.txtNote.TabIndex = 18;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 90);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(42, 16);
            this.labelControl3.TabIndex = 24;
            this.labelControl3.Text = "Ghi chú";
            // 
            // slueStaff
            // 
            this.slueStaff.EditValue = "Nhân viên";
            this.slueStaff.Location = new System.Drawing.Point(87, 12);
            this.slueStaff.Name = "slueStaff";
            this.slueStaff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueStaff.Properties.PopupView = this.searchLookUpEdit1View;
            this.slueStaff.Size = new System.Drawing.Size(59, 22);
            this.slueStaff.TabIndex = 29;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtStaffNameDesc
            // 
            this.txtStaffNameDesc.Location = new System.Drawing.Point(154, 12);
            this.txtStaffNameDesc.Name = "txtStaffNameDesc";
            this.txtStaffNameDesc.Size = new System.Drawing.Size(175, 22);
            this.txtStaffNameDesc.TabIndex = 30;
            // 
            // deFromDate
            // 
            this.deFromDate.EditValue = null;
            this.deFromDate.Location = new System.Drawing.Point(614, 12);
            this.deFromDate.Name = "deFromDate";
            this.deFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFromDate.Size = new System.Drawing.Size(129, 22);
            this.deFromDate.TabIndex = 31;
            // 
            // teFromDate
            // 
            this.teFromDate.EditValue = new System.DateTime(2024, 1, 17, 0, 0, 0, 0);
            this.teFromDate.Location = new System.Drawing.Point(501, 10);
            this.teFromDate.Name = "teFromDate";
            this.teFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teFromDate.Size = new System.Drawing.Size(107, 24);
            this.teFromDate.TabIndex = 32;
            // 
            // teToDate
            // 
            this.teToDate.EditValue = new System.DateTime(2024, 1, 17, 0, 0, 0, 0);
            this.teToDate.Location = new System.Drawing.Point(501, 50);
            this.teToDate.Name = "teToDate";
            this.teToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teToDate.Size = new System.Drawing.Size(107, 24);
            this.teToDate.TabIndex = 34;
            // 
            // deToDate
            // 
            this.deToDate.EditValue = null;
            this.deToDate.Location = new System.Drawing.Point(614, 52);
            this.deToDate.Name = "deToDate";
            this.deToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deToDate.Size = new System.Drawing.Size(129, 22);
            this.deToDate.TabIndex = 33;
            // 
            // gcLeave
            // 
            this.gcLeave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLeave.Location = new System.Drawing.Point(3, 19);
            this.gcLeave.MainView = this.gvLeave;
            this.gcLeave.Name = "gcLeave";
            this.gcLeave.Size = new System.Drawing.Size(767, 288);
            this.gcLeave.TabIndex = 0;
            this.gcLeave.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLeave,
            this.gridView1});
            this.gcLeave.Click += new System.EventHandler(this.gcLeave_Click_1);
            // 
            // gvLeave
            // 
            this.gvLeave.GridControl = this.gcLeave;
            this.gvLeave.Name = "gvLeave";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gcLeave;
            this.gridView1.Name = "gridView1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gcLeave);
            this.groupBox1.Location = new System.Drawing.Point(15, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(773, 310);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 52);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 16);
            this.labelControl5.TabIndex = 37;
            this.labelControl5.Text = "Phòng ban";
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(87, 49);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(242, 22);
            this.txtDepartment.TabIndex = 36;
            // 
            // FrmLeave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.teToDate);
            this.Controls.Add(this.deToDate);
            this.Controls.Add(this.teFromDate);
            this.Controls.Add(this.deFromDate);
            this.Controls.Add(this.txtStaffNameDesc);
            this.Controls.Add(this.slueStaff);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtNote);
            this.Name = "FrmLeave";
            this.Text = "FrmLeave";
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStaff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStaffNameDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLeave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLeave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtNote;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SearchLookUpEdit slueStaff;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.TextEdit txtStaffNameDesc;
        private DevExpress.XtraEditors.DateEdit deFromDate;
        private DevExpress.XtraEditors.TimeEdit teFromDate;
        private DevExpress.XtraEditors.TimeEdit teToDate;
        private DevExpress.XtraEditors.DateEdit deToDate;
        private DevExpress.XtraGrid.GridControl gcLeave;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLeave;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtDepartment;
    }
}