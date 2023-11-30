namespace CNY_BaseSys.WForm
{
    partial class FrmF4Grid
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.grcMain = new DevExpress.XtraGrid.GridControl();
            this.grvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.chkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.grcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.grvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.chkAll);
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 10);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::CNY_BaseSys.Properties.Resources.cancel_24x24_W;
            this.btnCancel.Location = new System.Drawing.Point(314, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 36);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkAll
            // 
            this.chkAll.Location = new System.Drawing.Point(3, 10);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Caption = "All";
            this.chkAll.Size = new System.Drawing.Size(70, 19);
            this.chkAll.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Image = global::CNY_BaseSys.Properties.Resources.System_Add_32x32;
            this.btnSelect.Location = new System.Drawing.Point(233, 10);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 36);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Accept";
            // 
            // grcMain
            // 
            this.grcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcMain.Location = new System.Drawing.Point(0, 10);
            this.grcMain.MainView = this.grvMain;
            this.grcMain.Name = "grcMain";
            this.grcMain.Size = new System.Drawing.Size(403, 343);
            this.grcMain.TabIndex = 0;
            this.grcMain.UseEmbeddedNavigator = true;
            this.grcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[]
            {
                this.grvMain
            });
            // 
            // grvMain
            // 
            this.grvMain.GridControl = this.grcMain;
            this.grvMain.Name = "grvMain";
            this.grvMain.OptionsSelection.MultiSelect = true;
            // 
            // FrmF4Grid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 353);
            this.Controls.Add(this.grcMain);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "FrmF4Grid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose Data";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.chkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.grcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.grvMain)).EndInit();
            this.ResumeLayout(false);
        }


        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.CheckEdit chkAll;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraGrid.GridControl grcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView grvMain;
    }

}