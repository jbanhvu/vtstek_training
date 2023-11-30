using DevExpress.XtraTreeList;

namespace CNY_Report
{
    partial class Frm002BoMReport
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
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.tlMain = new DevExpress.XtraTreeList.TreeList();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl4 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcAmount = new DevExpress.XtraGrid.GridControl();
            this.gvAmount = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl5 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcUpdate = new DevExpress.XtraGrid.GridControl();
            this.gvUpdate = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcInsert = new DevExpress.XtraGrid.GridControl();
            this.gvInsert = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcPaint = new DevExpress.XtraGrid.GridControl();
            this.gvPaint = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gcAttribute = new DevExpress.XtraGrid.GridControl();
            this.gvAttribute = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl6 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcDelete = new DevExpress.XtraGrid.GridControl();
            this.gvDelete = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            this.splitContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).BeginInit();
            this.splitContainerControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl5)).BeginInit();
            this.splitContainerControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInsert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInsert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPaint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPaint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAttribute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAttribute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl6)).BeginInit();
            this.splitContainerControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Location = new System.Drawing.Point(829, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tlMain
            // 
            this.tlMain.Appearance.HorzLine.BorderColor = System.Drawing.Color.Black;
            this.tlMain.Appearance.HorzLine.Options.UseBorderColor = true;
            this.tlMain.Appearance.TreeLine.BackColor = System.Drawing.Color.Black;
            this.tlMain.Appearance.TreeLine.Options.UseBackColor = true;
            this.tlMain.Appearance.VertLine.BackColor = System.Drawing.Color.Black;
            this.tlMain.Appearance.VertLine.Options.UseBackColor = true;
            this.tlMain.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlMain.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.tlMain.AppearancePrint.Lines.BorderColor = System.Drawing.Color.Black;
            this.tlMain.AppearancePrint.Lines.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlMain.AppearancePrint.Lines.Options.UseBorderColor = true;
            this.tlMain.AppearancePrint.Lines.Options.UseFont = true;
            this.tlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlMain.Location = new System.Drawing.Point(0, 0);
            this.tlMain.Name = "tlMain";
            this.tlMain.Size = new System.Drawing.Size(907, 286);
            this.tlMain.TabIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 614);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(907, 30);
            this.panelControl1.TabIndex = 4;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainerControl3);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(907, 614);
            this.splitContainerControl1.SplitterPosition = 449;
            this.splitContainerControl1.TabIndex = 5;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // splitContainerControl3
            // 
            this.splitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl3.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl3.Horizontal = false;
            this.splitContainerControl3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl3.Name = "splitContainerControl3";
            this.splitContainerControl3.Panel1.Controls.Add(this.tlMain);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            this.splitContainerControl3.Panel2.Controls.Add(this.splitContainerControl4);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.splitContainerControl3.Size = new System.Drawing.Size(907, 450);
            this.splitContainerControl3.SplitterPosition = 159;
            this.splitContainerControl3.TabIndex = 4;
            this.splitContainerControl3.Text = "splitContainerControl3";
            // 
            // splitContainerControl4
            // 
            this.splitContainerControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl4.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl4.Name = "splitContainerControl4";
            this.splitContainerControl4.Panel1.Controls.Add(this.gcAmount);
            this.splitContainerControl4.Panel1.Text = "Panel1";
            this.splitContainerControl4.Panel2.Controls.Add(this.splitContainerControl5);
            this.splitContainerControl4.Panel2.Text = "Panel2";
            this.splitContainerControl4.Size = new System.Drawing.Size(907, 159);
            this.splitContainerControl4.SplitterPosition = 211;
            this.splitContainerControl4.TabIndex = 0;
            this.splitContainerControl4.Text = "splitContainerControl4";
            // 
            // gcAmount
            // 
            this.gcAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAmount.Location = new System.Drawing.Point(0, 0);
            this.gcAmount.MainView = this.gvAmount;
            this.gcAmount.Name = "gcAmount";
            this.gcAmount.Size = new System.Drawing.Size(211, 159);
            this.gcAmount.TabIndex = 7;
            this.gcAmount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAmount});
            // 
            // gvAmount
            // 
            this.gvAmount.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvAmount.AppearancePrint.GroupRow.Options.UseFont = true;
            this.gvAmount.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvAmount.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvAmount.AppearancePrint.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvAmount.AppearancePrint.Row.Options.UseFont = true;
            this.gvAmount.AppearancePrint.Row.Options.UseTextOptions = true;
            this.gvAmount.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvAmount.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvAmount.GridControl = this.gcAmount;
            this.gvAmount.Name = "gvAmount";
            this.gvAmount.OptionsPrint.PrintFooter = false;
            this.gvAmount.OptionsPrint.PrintGroupFooter = false;
            // 
            // splitContainerControl5
            // 
            this.splitContainerControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl5.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl5.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl5.Name = "splitContainerControl5";
            this.splitContainerControl5.Panel1.Controls.Add(this.gcUpdate);
            this.splitContainerControl5.Panel1.Text = "Panel1";
            this.splitContainerControl5.Panel2.Controls.Add(this.splitContainerControl6);
            this.splitContainerControl5.Panel2.Text = "Panel2";
            this.splitContainerControl5.Size = new System.Drawing.Size(691, 159);
            this.splitContainerControl5.SplitterPosition = 346;
            this.splitContainerControl5.TabIndex = 0;
            this.splitContainerControl5.Text = "splitContainerControl5";
            // 
            // gcUpdate
            // 
            this.gcUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcUpdate.Location = new System.Drawing.Point(0, 0);
            this.gcUpdate.MainView = this.gvUpdate;
            this.gcUpdate.Name = "gcUpdate";
            this.gcUpdate.Size = new System.Drawing.Size(346, 159);
            this.gcUpdate.TabIndex = 8;
            this.gcUpdate.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUpdate});
            // 
            // gvUpdate
            // 
            this.gvUpdate.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvUpdate.AppearancePrint.GroupRow.Options.UseFont = true;
            this.gvUpdate.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvUpdate.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvUpdate.AppearancePrint.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvUpdate.AppearancePrint.Row.Options.UseFont = true;
            this.gvUpdate.AppearancePrint.Row.Options.UseTextOptions = true;
            this.gvUpdate.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvUpdate.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvUpdate.GridControl = this.gcUpdate;
            this.gvUpdate.Name = "gvUpdate";
            // 
            // gcInsert
            // 
            this.gcInsert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcInsert.Location = new System.Drawing.Point(0, 0);
            this.gcInsert.MainView = this.gvInsert;
            this.gcInsert.Name = "gcInsert";
            this.gcInsert.Size = new System.Drawing.Size(170, 159);
            this.gcInsert.TabIndex = 9;
            this.gcInsert.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInsert});
            // 
            // gvInsert
            // 
            this.gvInsert.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvInsert.AppearancePrint.GroupRow.Options.UseFont = true;
            this.gvInsert.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvInsert.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvInsert.AppearancePrint.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvInsert.AppearancePrint.Row.Options.UseFont = true;
            this.gvInsert.AppearancePrint.Row.Options.UseTextOptions = true;
            this.gvInsert.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvInsert.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvInsert.GridControl = this.gcInsert;
            this.gvInsert.Name = "gvInsert";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.gcPaint);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.gcAttribute);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(907, 159);
            this.splitContainerControl2.SplitterPosition = 464;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // gcPaint
            // 
            this.gcPaint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPaint.Location = new System.Drawing.Point(0, 0);
            this.gcPaint.MainView = this.gvPaint;
            this.gcPaint.Name = "gcPaint";
            this.gcPaint.Size = new System.Drawing.Size(464, 159);
            this.gcPaint.TabIndex = 6;
            this.gcPaint.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPaint});
            // 
            // gvPaint
            // 
            this.gvPaint.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvPaint.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvPaint.AppearancePrint.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvPaint.AppearancePrint.Row.Options.UseFont = true;
            this.gvPaint.GridControl = this.gcPaint;
            this.gvPaint.Name = "gvPaint";
            // 
            // gcAttribute
            // 
            this.gcAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAttribute.Location = new System.Drawing.Point(0, 0);
            this.gcAttribute.MainView = this.gvAttribute;
            this.gcAttribute.Name = "gcAttribute";
            this.gcAttribute.Size = new System.Drawing.Size(438, 159);
            this.gcAttribute.TabIndex = 6;
            this.gcAttribute.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAttribute});
            // 
            // gvAttribute
            // 
            this.gvAttribute.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvAttribute.AppearancePrint.GroupRow.Options.UseFont = true;
            this.gvAttribute.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvAttribute.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvAttribute.AppearancePrint.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvAttribute.AppearancePrint.Row.Options.UseFont = true;
            this.gvAttribute.AppearancePrint.Row.Options.UseTextOptions = true;
            this.gvAttribute.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvAttribute.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvAttribute.GridControl = this.gcAttribute;
            this.gvAttribute.Name = "gvAttribute";
            // 
            // splitContainerControl6
            // 
            this.splitContainerControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl6.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl6.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl6.Name = "splitContainerControl6";
            this.splitContainerControl6.Panel1.Controls.Add(this.gcInsert);
            this.splitContainerControl6.Panel1.Text = "Panel1";
            this.splitContainerControl6.Panel2.Controls.Add(this.gcDelete);
            this.splitContainerControl6.Panel2.Text = "Panel2";
            this.splitContainerControl6.Size = new System.Drawing.Size(340, 159);
            this.splitContainerControl6.SplitterPosition = 170;
            this.splitContainerControl6.TabIndex = 0;
            this.splitContainerControl6.Text = "splitContainerControl6";
            // 
            // gcDelete
            // 
            this.gcDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDelete.Location = new System.Drawing.Point(0, 0);
            this.gcDelete.MainView = this.gvDelete;
            this.gcDelete.Name = "gcDelete";
            this.gcDelete.Size = new System.Drawing.Size(165, 159);
            this.gcDelete.TabIndex = 10;
            this.gcDelete.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDelete});
            // 
            // gvDelete
            // 
            this.gvDelete.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvDelete.AppearancePrint.GroupRow.Options.UseFont = true;
            this.gvDelete.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvDelete.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvDelete.AppearancePrint.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvDelete.AppearancePrint.Row.Options.UseFont = true;
            this.gvDelete.AppearancePrint.Row.Options.UseTextOptions = true;
            this.gvDelete.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvDelete.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvDelete.GridControl = this.gcDelete;
            this.gvDelete.Name = "gvDelete";
            // 
            // Frm002BoMReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 644);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "Frm002BoMReport";
            this.Text = "Frm002BoMReport";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.tlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).EndInit();
            this.splitContainerControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl5)).EndInit();
            this.splitContainerControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInsert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInsert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPaint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPaint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAttribute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAttribute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl6)).EndInit();
            this.splitContainerControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private TreeList  tlMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gcPaint;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvPaint;
        private DevExpress.XtraGrid.GridControl gcAttribute;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAttribute;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl3;
        private DevExpress.XtraGrid.GridControl gcAmount;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAmount;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl4;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl5;
        private DevExpress.XtraGrid.GridControl gcUpdate;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUpdate;
        private DevExpress.XtraGrid.GridControl gcInsert;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInsert;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl6;
        private DevExpress.XtraGrid.GridControl gcDelete;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDelete;
    }
}