namespace CNY_WH.WForm
{
    partial class Frm_SDR_AddNewItem
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
            this.btnUpDown = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownUp = new DevExpress.XtraEditors.SimpleButton();
            this.gcItemCode = new DevExpress.XtraGrid.GridControl();
            this.gvItemCode = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpDown
            // 
            this.btnUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpDown.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnUpDown.ImageOptions.Image = global::CNY_WH.Properties.Resources.Down;
            this.btnUpDown.Location = new System.Drawing.Point(868, 7);
            this.btnUpDown.Name = "btnUpDown";
            this.btnUpDown.Size = new System.Drawing.Size(83, 28);
            this.btnUpDown.TabIndex = 13;
            this.btnUpDown.Text = "OK";
            this.btnUpDown.ToolTip = "Press (Ctrl+Shift+C)";
            // 
            // btnDownUp
            // 
            this.btnDownUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownUp.ImageOptions.Image = global::CNY_WH.Properties.Resources.Up;
            this.btnDownUp.Location = new System.Drawing.Point(600, 7);
            this.btnDownUp.Name = "btnDownUp";
            this.btnDownUp.Size = new System.Drawing.Size(83, 28);
            this.btnDownUp.TabIndex = 12;
            this.btnDownUp.Text = "Down - Up";
            this.btnDownUp.Visible = false;
            // 
            // gcItemCode
            // 
            this.gcItemCode.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcItemCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcItemCode.Location = new System.Drawing.Point(0, 0);
            this.gcItemCode.MainView = this.gvItemCode;
            this.gcItemCode.Name = "gcItemCode";
            this.gcItemCode.Size = new System.Drawing.Size(962, 445);
            this.gcItemCode.TabIndex = 41;
            this.gcItemCode.UseEmbeddedNavigator = true;
            this.gcItemCode.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItemCode});
            // 
            // gvItemCode
            // 
            this.gvItemCode.GridControl = this.gcItemCode;
            this.gvItemCode.Name = "gvItemCode";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnUpDown);
            this.panelControl2.Controls.Add(this.btnDownUp);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 445);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(962, 40);
            this.panelControl2.TabIndex = 42;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = global::CNY_WH.Properties.Resources.cancel_16x16;
            this.btnCancel.Location = new System.Drawing.Point(779, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 28);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTip = "Press (Ctrl+Shift+C)";
            // 
            // Frm_SDR_AddNewItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(962, 485);
            this.Controls.Add(this.gcItemCode);
            this.Controls.Add(this.panelControl2);
            this.Name = "Frm_SDR_AddNewItem";
            this.Text = "SDR Add New Item";
            ((System.ComponentModel.ISupportInitialize)(this.gcItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnUpDown;
        private DevExpress.XtraEditors.SimpleButton btnDownUp;
        private DevExpress.XtraGrid.GridControl gcItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItemCode;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}