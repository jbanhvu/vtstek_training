namespace CNY_BaseSys.WForm
{
    partial class FrmViewImageProduct
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
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            this.idCol = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.splitCCTab3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.picProduct = new DevExpress.XtraEditors.PictureEdit();
            this.gcImage = new DevExpress.XtraGrid.GridControl();
            this.tvImage = new DevExpress.XtraGrid.Views.Tile.TileView();
            ((System.ComponentModel.ISupportInitialize)(this.splitCCTab3)).BeginInit();
            this.splitCCTab3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvImage)).BeginInit();
            this.SuspendLayout();
            // 
            // idCol
            // 
            this.idCol.Caption = "Id";
            this.idCol.FieldName = "Id";
            this.idCol.Name = "idCol";
            this.idCol.Visible = true;
            this.idCol.VisibleIndex = 0;
            // 
            // splitCCTab3
            // 
            this.splitCCTab3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCCTab3.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitCCTab3.Horizontal = false;
            this.splitCCTab3.Location = new System.Drawing.Point(0, 0);
            this.splitCCTab3.Name = "splitCCTab3";
            this.splitCCTab3.Panel1.Controls.Add(this.picProduct);
            this.splitCCTab3.Panel1.Text = "Panel1";
            this.splitCCTab3.Panel2.Controls.Add(this.gcImage);
            this.splitCCTab3.Panel2.Text = "Panel2";
            this.splitCCTab3.Size = new System.Drawing.Size(984, 562);
            this.splitCCTab3.SplitterPosition = 90;
            this.splitCCTab3.TabIndex = 60;
            this.splitCCTab3.Text = "splitContainerControl2";
            // 
            // picProduct
            // 
            this.picProduct.Cursor = System.Windows.Forms.Cursors.Default;
            this.picProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picProduct.Location = new System.Drawing.Point(0, 0);
            this.picProduct.Name = "picProduct";
            this.picProduct.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picProduct.Properties.ShowMenu = false;
            this.picProduct.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picProduct.Properties.ZoomAccelerationFactor = 1D;
            this.picProduct.Size = new System.Drawing.Size(984, 467);
            this.picProduct.TabIndex = 32;
            // 
            // gcImage
            // 
            this.gcImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcImage.Location = new System.Drawing.Point(0, 0);
            this.gcImage.MainView = this.tvImage;
            this.gcImage.Name = "gcImage";
            this.gcImage.Size = new System.Drawing.Size(984, 90);
            this.gcImage.TabIndex = 33;
            this.gcImage.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tvImage});
            // 
            // tvImage
            // 
            this.tvImage.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idCol});
            this.tvImage.GridControl = this.gcImage;
            this.tvImage.Name = "tvImage";
            this.tvImage.OptionsBehavior.AutoPopulateColumns = false;
            this.tvImage.OptionsFind.AllowFindPanel = false;
            this.tvImage.OptionsTiles.IndentBetweenGroups = 5;
            this.tvImage.OptionsTiles.IndentBetweenItems = 5;
            this.tvImage.OptionsTiles.ItemBackgroundImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Stretch;
            this.tvImage.OptionsTiles.ItemPadding = new System.Windows.Forms.Padding(0);
            this.tvImage.OptionsTiles.ItemSize = new System.Drawing.Size(70, 70);
            this.tvImage.OptionsTiles.Padding = new System.Windows.Forms.Padding(0);
            this.tvImage.OptionsTiles.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollButtons;
            this.tvImage.OptionsTiles.ShowGroupText = false;
            this.tvImage.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            tileViewItemElement1.Column = this.idCol;
            tileViewItemElement1.Text = "idCol";
            this.tvImage.TileTemplate.Add(tileViewItemElement1);
            // 
            // FrmViewImageProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.splitCCTab3);
            this.Name = "FrmViewImageProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Image";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.splitCCTab3)).EndInit();
            this.splitCCTab3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitCCTab3;
        private DevExpress.XtraEditors.PictureEdit picProduct;
        private DevExpress.XtraGrid.GridControl gcImage;
        private DevExpress.XtraGrid.Views.Tile.TileView tvImage;
        private DevExpress.XtraGrid.Columns.TileViewColumn idCol;
    }
}