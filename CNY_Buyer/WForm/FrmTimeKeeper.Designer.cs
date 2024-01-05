namespace CNY_Buyer.WForm
{
    partial class FrmTimeKeeper
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
            this.gcTimeKeeper = new DevExpress.XtraGrid.GridControl();
            this.gvTimeKeeper = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtEncrollNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtRecord = new DevExpress.XtraEditors.TextEdit();
            this.txtDateTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTimeKeeper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTimeKeeper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEncrollNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTime.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gcTimeKeeper);
            this.groupBox2.Location = new System.Drawing.Point(12, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 398);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // gcTimeKeeper
            // 
            this.gcTimeKeeper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTimeKeeper.Location = new System.Drawing.Point(3, 19);
            this.gcTimeKeeper.MainView = this.gvTimeKeeper;
            this.gcTimeKeeper.Name = "gcTimeKeeper";
            this.gcTimeKeeper.Size = new System.Drawing.Size(770, 376);
            this.gcTimeKeeper.TabIndex = 0;
            this.gcTimeKeeper.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTimeKeeper});
            this.gcTimeKeeper.Click += new System.EventHandler(this.gcTimeKeeper_Click_1);
            // 
            // gvTimeKeeper
            // 
            this.gvTimeKeeper.GridControl = this.gcTimeKeeper;
            this.gvTimeKeeper.Name = "gvTimeKeeper";
            // 
            // txtEncrollNumber
            // 
            this.txtEncrollNumber.Location = new System.Drawing.Point(104, 12);
            this.txtEncrollNumber.Name = "txtEncrollNumber";
            this.txtEncrollNumber.Size = new System.Drawing.Size(116, 22);
            this.txtEncrollNumber.TabIndex = 2;
            // 
            // txtRecord
            // 
            this.txtRecord.Location = new System.Drawing.Point(288, 12);
            this.txtRecord.Name = "txtRecord";
            this.txtRecord.Size = new System.Drawing.Size(116, 22);
            this.txtRecord.TabIndex = 3;
            // 
            // txtDateTime
            // 
            this.txtDateTime.Location = new System.Drawing.Point(501, 12);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.Size = new System.Drawing.Size(116, 22);
            this.txtDateTime.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 16);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "EncrollNumber";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(242, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 16);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Record";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(440, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 16);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "DateTime";
            // 
            // FrmTimeKeeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtDateTime);
            this.Controls.Add(this.txtRecord);
            this.Controls.Add(this.txtEncrollNumber);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmTimeKeeper";
            this.Text = "FrmTimeKeeper";
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTimeKeeper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTimeKeeper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEncrollNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTime.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gcTimeKeeper;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTimeKeeper;
        private DevExpress.XtraEditors.TextEdit txtEncrollNumber;
        private DevExpress.XtraEditors.TextEdit txtRecord;
        private DevExpress.XtraEditors.TextEdit txtDateTime;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}