namespace CNY_WH.WForm
{
    partial class Frm010BusinessTripDetail
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
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gcBusinessTripDetail = new DevExpress.XtraGrid.GridControl();
            this.gvBusinessTripDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBusinessTripDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBusinessTripDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1167, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(264, 42);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(100, 23);
            this.txtNote.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Note";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(78, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 23);
            this.txtName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gcBusinessTripDetail);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1167, 463);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List";
            // 
            // gcBusinessTripDetail
            // 
            this.gcBusinessTripDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBusinessTripDetail.Location = new System.Drawing.Point(3, 19);
            this.gcBusinessTripDetail.MainView = this.gvBusinessTripDetail;
            this.gcBusinessTripDetail.Name = "gcBusinessTripDetail";
            this.gcBusinessTripDetail.Size = new System.Drawing.Size(1161, 441);
            this.gcBusinessTripDetail.TabIndex = 0;
            this.gcBusinessTripDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBusinessTripDetail});
            // 
            // gvBusinessTripDetail
            // 
            this.gvBusinessTripDetail.GridControl = this.gcBusinessTripDetail;
            this.gvBusinessTripDetail.Name = "gvBusinessTripDetail";
            // 
            // Frm010BusinessTripDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.ClientSize = new System.Drawing.Size(1167, 583);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm010BusinessTripDetail";
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBusinessTripDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBusinessTripDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gcBusinessTripDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBusinessTripDetail;
    }
}
