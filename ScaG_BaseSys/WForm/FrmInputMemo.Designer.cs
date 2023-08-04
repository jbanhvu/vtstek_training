namespace CNY_BaseSys.WForm
{
    partial class FrmInputMemo
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
            this.btnNextFinish = new DevExpress.XtraEditors.SimpleButton();
            this.memoMain = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoMain.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNextFinish
            // 
            this.btnNextFinish.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextFinish.Image = global::CNY_BaseSys.Properties.Resources.apply_24x24_W;
            this.btnNextFinish.Location = new System.Drawing.Point(400, 0);
            this.btnNextFinish.Name = "btnNextFinish";
            this.btnNextFinish.Size = new System.Drawing.Size(50, 200);
            this.btnNextFinish.TabIndex = 1;
            this.btnNextFinish.Text = "OK";
            this.btnNextFinish.ToolTip = "(Press Ctrl+S Key)";
            // 
            // memoMain
            // 
            this.memoMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoMain.Location = new System.Drawing.Point(0, 0);
            this.memoMain.Name = "memoMain";
            this.memoMain.Size = new System.Drawing.Size(400, 200);
            this.memoMain.TabIndex = 2;
            // 
            // FrmInputMemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 200);
            this.Controls.Add(this.memoMain);
            this.Controls.Add(this.btnNextFinish);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmInputMemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Memo Input";
            ((System.ComponentModel.ISupportInitialize)(this.memoMain.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnNextFinish;
        private DevExpress.XtraEditors.MemoEdit memoMain;
    }
}