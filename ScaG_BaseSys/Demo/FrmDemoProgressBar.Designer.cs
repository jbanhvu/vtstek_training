namespace CNY_BaseSys.Demo
{
    partial class FrmDemoProgressBar
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
            this.pcTop = new DevExpress.XtraEditors.PanelControl();
            this.btnAllEffects = new DevExpress.XtraEditors.SimpleButton();
            this.btnGrayScale = new DevExpress.XtraEditors.SimpleButton();
            this.btnNegative = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.pcContain = new DevExpress.XtraEditors.PanelControl();
            this.pct = new System.Windows.Forms.PictureBox();
            this.dlg = new System.Windows.Forms.OpenFileDialog();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pcTop)).BeginInit();
            this.pcTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcContain)).BeginInit();
            this.pcContain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pct)).BeginInit();
            this.SuspendLayout();
            // 
            // pcTop
            // 
            this.pcTop.Controls.Add(this.btnClose);
            this.pcTop.Controls.Add(this.btnAllEffects);
            this.pcTop.Controls.Add(this.btnGrayScale);
            this.pcTop.Controls.Add(this.btnNegative);
            this.pcTop.Controls.Add(this.btnOpenFile);
            this.pcTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcTop.Location = new System.Drawing.Point(0, 0);
            this.pcTop.Name = "pcTop";
            this.pcTop.Size = new System.Drawing.Size(784, 34);
            this.pcTop.TabIndex = 0;
            // 
            // btnAllEffects
            // 
            this.btnAllEffects.Location = new System.Drawing.Point(234, 5);
            this.btnAllEffects.Name = "btnAllEffects";
            this.btnAllEffects.Size = new System.Drawing.Size(75, 23);
            this.btnAllEffects.TabIndex = 3;
            this.btnAllEffects.Text = "&All effects";
            this.btnAllEffects.Click += new System.EventHandler(this.btnAllEffects_Click);
            // 
            // btnGrayScale
            // 
            this.btnGrayScale.Location = new System.Drawing.Point(156, 5);
            this.btnGrayScale.Name = "btnGrayScale";
            this.btnGrayScale.Size = new System.Drawing.Size(75, 23);
            this.btnGrayScale.TabIndex = 2;
            this.btnGrayScale.Text = "&Gray scale";
            this.btnGrayScale.Click += new System.EventHandler(this.btnGrayScale_Click);
            // 
            // btnNegative
            // 
            this.btnNegative.Location = new System.Drawing.Point(79, 5);
            this.btnNegative.Name = "btnNegative";
            this.btnNegative.Size = new System.Drawing.Size(75, 23);
            this.btnNegative.TabIndex = 1;
            this.btnNegative.Text = "&Negative";
            this.btnNegative.Click += new System.EventHandler(this.btnNegative_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(2, 5);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "&Open File";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // pcContain
            // 
            this.pcContain.Controls.Add(this.pct);
            this.pcContain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcContain.Location = new System.Drawing.Point(0, 34);
            this.pcContain.Name = "pcContain";
            this.pcContain.Size = new System.Drawing.Size(784, 528);
            this.pcContain.TabIndex = 1;
            // 
            // pct
            // 
            this.pct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pct.Location = new System.Drawing.Point(2, 2);
            this.pct.Name = "pct";
            this.pct.Size = new System.Drawing.Size(780, 524);
            this.pct.TabIndex = 2;
            this.pct.TabStop = false;
            // 
            // dlg
            // 
            this.dlg.FileName = "openFileDialog1";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(311, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmDemoProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pcContain);
            this.Controls.Add(this.pcTop);
            this.Name = "FrmDemoProgressBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Demo Progress Bar";
            ((System.ComponentModel.ISupportInitialize)(this.pcTop)).EndInit();
            this.pcTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcContain)).EndInit();
            this.pcContain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pcTop;
        private DevExpress.XtraEditors.SimpleButton btnAllEffects;
        private DevExpress.XtraEditors.SimpleButton btnGrayScale;
        private DevExpress.XtraEditors.SimpleButton btnNegative;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
        private DevExpress.XtraEditors.PanelControl pcContain;
        private System.Windows.Forms.PictureBox pct;
        private System.Windows.Forms.OpenFileDialog dlg;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}