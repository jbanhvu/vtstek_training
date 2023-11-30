namespace CNY_SI.UControl
{
    partial class XtraUC001BusinessTrip
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gcBusinessTrip = new DevExpress.XtraGrid.GridControl();
            this.gvBusinessTrip = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBusinessTrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBusinessTrip)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.gcBusinessTrip);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(885, 619);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // gcBusinessTrip
            // 
            this.gcBusinessTrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBusinessTrip.Location = new System.Drawing.Point(3, 18);
            this.gcBusinessTrip.MainView = this.gvBusinessTrip;
            this.gcBusinessTrip.Name = "gcBusinessTrip";
            this.gcBusinessTrip.Size = new System.Drawing.Size(879, 598);
            this.gcBusinessTrip.TabIndex = 0;
            this.gcBusinessTrip.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBusinessTrip});
            // 
            // gvBusinessTrip
            // 
            this.gvBusinessTrip.GridControl = this.gcBusinessTrip;
            this.gvBusinessTrip.Name = "gvBusinessTrip";
            // 
            // XtraUC001BusinessTrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "XtraUC001BusinessTrip";
            this.Size = new System.Drawing.Size(885, 619);
            this.Load += new System.EventHandler(this.XtraUC001BusinessTrip_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBusinessTrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBusinessTrip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl gcBusinessTrip;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBusinessTrip;
    }
}
