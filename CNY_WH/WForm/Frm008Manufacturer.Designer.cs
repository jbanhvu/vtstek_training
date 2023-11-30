
namespace CNY_WH.WForm
{
    partial class Frm008Manufacturer
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txt_manufacuturer_code = new DevExpress.XtraEditors.TextEdit();
            this.Txt_manufacturer_name = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcManufacturer = new DevExpress.XtraGrid.GridControl();
            this.gvManufacturer = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_manufacuturer_code.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_manufacturer_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcManufacturer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvManufacturer)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txt_manufacuturer_code);
            this.groupControl1.Controls.Add(this.Txt_manufacturer_name);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(703, 142);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Chi tiết";
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // txt_manufacuturer_code
            // 
            this.txt_manufacuturer_code.EditValue = "";
            this.txt_manufacuturer_code.Location = new System.Drawing.Point(143, 65);
            this.txt_manufacuturer_code.Margin = new System.Windows.Forms.Padding(7, 10, 7, 10);
            this.txt_manufacuturer_code.Name = "txt_manufacuturer_code";
            this.txt_manufacuturer_code.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txt_manufacuturer_code.Properties.Appearance.Options.UseForeColor = true;
            this.txt_manufacuturer_code.Properties.AutoHeight = false;
            this.txt_manufacuturer_code.Size = new System.Drawing.Size(128, 39);
            this.txt_manufacuturer_code.TabIndex = 319;
            this.txt_manufacuturer_code.Tag = "";
            // 
            // Txt_manufacturer_name
            // 
            this.Txt_manufacturer_name.EditValue = "";
            this.Txt_manufacturer_name.Location = new System.Drawing.Point(472, 65);
            this.Txt_manufacturer_name.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Txt_manufacturer_name.Name = "Txt_manufacturer_name";
            this.Txt_manufacturer_name.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Txt_manufacturer_name.Properties.Appearance.Options.UseForeColor = true;
            this.Txt_manufacturer_name.Properties.AutoHeight = false;
            this.Txt_manufacturer_name.Size = new System.Drawing.Size(132, 39);
            this.Txt_manufacturer_name.TabIndex = 319;
            this.Txt_manufacturer_name.Tag = "";
            this.Txt_manufacturer_name.EditValueChanged += new System.EventHandler(this.txtTypeDesc_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(365, 77);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(99, 16);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Tên nhà sản xuất";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(38, 77);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(94, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mã nhà sản xuất";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gcManufacturer);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 142);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(703, 348);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Danh sách";
            this.groupControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl2_Paint);
            // 
            // gcManufacturer
            // 
            this.gcManufacturer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcManufacturer.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcManufacturer.Location = new System.Drawing.Point(2, 28);
            this.gcManufacturer.MainView = this.gvManufacturer;
            this.gcManufacturer.Margin = new System.Windows.Forms.Padding(4);
            this.gcManufacturer.Name = "gcManufacturer";
            this.gcManufacturer.Size = new System.Drawing.Size(699, 318);
            this.gcManufacturer.TabIndex = 0;
            this.gcManufacturer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvManufacturer});
            // 
            // gvManufacturer
            // 
            this.gvManufacturer.DetailHeight = 279;
            this.gvManufacturer.GridControl = this.gcManufacturer;
            this.gvManufacturer.Name = "gvManufacturer";
            // 
            // Frm008Manufacturer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 490);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm008Manufacturer";
            this.Text = "Frm008Manufacturer";
            this.Load += new System.EventHandler(this.Frm008Manufacturer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_manufacuturer_code.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_manufacturer_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcManufacturer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvManufacturer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_manufacuturer_code;
        private DevExpress.XtraEditors.TextEdit Txt_manufacturer_name;
        private DevExpress.XtraGrid.GridControl gcManufacturer;
        private DevExpress.XtraGrid.Views.Grid.GridView gvManufacturer;
    }
}