namespace CNY_AdminSys.UControl
{
    partial class UCACF_Rule
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
            this.groupInfo = new DevExpress.XtraEditors.GroupControl();
            this.spinPriority = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDesc = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.groupList = new DevExpress.XtraEditors.GroupControl();
            this.gcMainAE = new DevExpress.XtraGrid.GridControl();
            this.gvMainAE = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).BeginInit();
            this.groupInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinPriority.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupList)).BeginInit();
            this.groupList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMainAE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMainAE)).BeginInit();
            this.SuspendLayout();
            // 
            // groupInfo
            // 
            this.groupInfo.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupInfo.AppearanceCaption.Options.UseFont = true;
            this.groupInfo.Controls.Add(this.spinPriority);
            this.groupInfo.Controls.Add(this.labelControl4);
            this.groupInfo.Controls.Add(this.labelControl3);
            this.groupInfo.Controls.Add(this.txtDesc);
            this.groupInfo.Controls.Add(this.labelControl2);
            this.groupInfo.Controls.Add(this.txtName);
            this.groupInfo.Controls.Add(this.labelControl1);
            this.groupInfo.Controls.Add(this.txtCode);
            this.groupInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupInfo.Location = new System.Drawing.Point(0, 0);
            this.groupInfo.Name = "groupInfo";
            this.groupInfo.ShowCaption = false;
            this.groupInfo.Size = new System.Drawing.Size(938, 28);
            this.groupInfo.TabIndex = 8;
            // 
            // spinPriority
            // 
            this.spinPriority.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinPriority.Location = new System.Drawing.Point(869, 3);
            this.spinPriority.Name = "spinPriority";
            this.spinPriority.Properties.AutoHeight = false;
            this.spinPriority.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinPriority.Properties.EditFormat.FormatString = "N0";
            this.spinPriority.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinPriority.Size = new System.Drawing.Size(66, 22);
            this.spinPriority.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(822, 7);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(41, 13);
            this.labelControl4.TabIndex = 18;
            this.labelControl4.Text = "Priority :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(469, 7);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 13);
            this.labelControl3.TabIndex = 17;
            this.labelControl3.Text = "Description :";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(534, 3);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtDesc.Properties.Appearance.Options.UseFont = true;
            this.txtDesc.Properties.Appearance.Options.UseForeColor = true;
            this.txtDesc.Properties.AutoHeight = false;
            this.txtDesc.Size = new System.Drawing.Size(280, 22);
            this.txtDesc.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(143, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(34, 13);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "Name :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(183, 3);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtName.Properties.Appearance.Options.UseFont = true;
            this.txtName.Properties.Appearance.Options.UseForeColor = true;
            this.txtName.Properties.AutoHeight = false;
            this.txtName.Size = new System.Drawing.Size(280, 22);
            this.txtName.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(4, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Code :";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(42, 3);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtCode.Properties.Appearance.Options.UseFont = true;
            this.txtCode.Properties.Appearance.Options.UseForeColor = true;
            this.txtCode.Properties.AutoHeight = false;
            this.txtCode.Size = new System.Drawing.Size(95, 22);
            this.txtCode.TabIndex = 0;
            // 
            // groupList
            // 
            this.groupList.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupList.AppearanceCaption.Options.UseFont = true;
            this.groupList.Controls.Add(this.gcMainAE);
            this.groupList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupList.Location = new System.Drawing.Point(0, 28);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(938, 551);
            this.groupList.TabIndex = 9;
            this.groupList.Text = "Listing";
            // 
            // gcMainAE
            // 
            this.gcMainAE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMainAE.Location = new System.Drawing.Point(2, 20);
            this.gcMainAE.MainView = this.gvMainAE;
            this.gcMainAE.Name = "gcMainAE";
            this.gcMainAE.Size = new System.Drawing.Size(934, 529);
            this.gcMainAE.TabIndex = 4;
            this.gcMainAE.UseEmbeddedNavigator = true;
            this.gcMainAE.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMainAE});
            // 
            // gvMainAE
            // 
            this.gvMainAE.GridControl = this.gcMainAE;
            this.gvMainAE.Name = "gvMainAE";
            // 
            // UCACF_Rule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupList);
            this.Controls.Add(this.groupInfo);
            this.Name = "UCACF_Rule";
            this.Size = new System.Drawing.Size(938, 579);
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).EndInit();
            this.groupInfo.ResumeLayout(false);
            this.groupInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinPriority.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupList)).EndInit();
            this.groupList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMainAE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMainAE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupInfo;
        private DevExpress.XtraEditors.SpinEdit spinPriority;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtDesc;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.GroupControl groupList;
        private DevExpress.XtraGrid.GridControl gcMainAE;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMainAE;
    }
}
