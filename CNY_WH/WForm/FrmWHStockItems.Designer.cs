﻿namespace CNY_WH.WForm
{
    partial class FrmWHStockItems
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
            this.panStockItems = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panStockItems)).BeginInit();
            this.SuspendLayout();
            // 
            // panStockItems
            // 
            this.panStockItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panStockItems.Location = new System.Drawing.Point(0, 0);
            this.panStockItems.Name = "panStockItems";
            this.panStockItems.Size = new System.Drawing.Size(561, 237);
            this.panStockItems.TabIndex = 0;
            // 
            // FrmWHStockItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 237);
            this.Controls.Add(this.panStockItems);
            this.Name = "FrmWHStockItems";
            this.Text = "FrmWHStockItems";
            ((System.ComponentModel.ISupportInitialize)(this.DtPerFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtSpecialFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panStockItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panStockItems;
    }
}