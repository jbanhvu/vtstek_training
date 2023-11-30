namespace CNY_AdminSys.UControl
{
    partial class UCACF_Position
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
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.groupList = new DevExpress.XtraEditors.GroupControl();
            this.gcMainAE = new DevExpress.XtraGrid.GridControl();
            this.gvMainAE = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).BeginInit();
            this.groupInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupList)).BeginInit();
            this.groupList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMainAE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMainAE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupInfo
            // 
            this.groupInfo.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupInfo.AppearanceCaption.Options.UseFont = true;
            this.groupInfo.Controls.Add(this.txtDescription);
            this.groupInfo.Controls.Add(this.labelControl13);
            this.groupInfo.Controls.Add(this.labelControl2);
            this.groupInfo.Controls.Add(this.txtName);
            this.groupInfo.Controls.Add(this.labelControl1);
            this.groupInfo.Controls.Add(this.txtCode);
            this.groupInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupInfo.Location = new System.Drawing.Point(0, 0);
            this.groupInfo.Name = "groupInfo";
            this.groupInfo.ShowCaption = false;
            this.groupInfo.Size = new System.Drawing.Size(1314, 30);
            this.groupInfo.TabIndex = 5;
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(430, 8);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(53, 13);
            this.labelControl13.TabIndex = 18;
            this.labelControl13.Text = "Description";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(142, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(27, 13);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(175, 4);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtName.Properties.Appearance.Options.UseFont = true;
            this.txtName.Properties.Appearance.Options.UseForeColor = true;
            this.txtName.Properties.AutoHeight = false;
            this.txtName.Size = new System.Drawing.Size(249, 22);
            this.txtName.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(25, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Code";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(36, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtCode.Properties.Appearance.Options.UseFont = true;
            this.txtCode.Properties.Appearance.Options.UseForeColor = true;
            this.txtCode.Properties.AutoHeight = false;
            this.txtCode.Size = new System.Drawing.Size(100, 22);
            this.txtCode.TabIndex = 0;
            // 
            // groupList
            // 
            this.groupList.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupList.AppearanceCaption.Options.UseFont = true;
            this.groupList.Controls.Add(this.gcMainAE);
            this.groupList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupList.Location = new System.Drawing.Point(0, 30);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(1314, 658);
            this.groupList.TabIndex = 6;
            this.groupList.Text = "Listing";
            // 
            // gcMainAE
            // 
            this.gcMainAE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMainAE.Location = new System.Drawing.Point(2, 20);
            this.gcMainAE.MainView = this.gvMainAE;
            this.gcMainAE.Name = "gcMainAE";
            this.gcMainAE.Size = new System.Drawing.Size(1310, 636);
            this.gcMainAE.TabIndex = 17;
            this.gcMainAE.UseEmbeddedNavigator = true;
            this.gcMainAE.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMainAE});
            // 
            // gvMainAE
            // 
            this.gvMainAE.GridControl = this.gcMainAE;
            this.gvMainAE.Name = "gvMainAE";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(489, 4);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Properties.Appearance.Options.UseFont = true;
            this.txtDescription.Properties.Appearance.Options.UseForeColor = true;
            this.txtDescription.Properties.AutoHeight = false;
            this.txtDescription.Size = new System.Drawing.Size(249, 22);
            this.txtDescription.TabIndex = 19;
            // 
            // UCACF_Position
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupList);
            this.Controls.Add(this.groupInfo);
            this.Name = "UCACF_Position";
            this.Size = new System.Drawing.Size(1314, 688);
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).EndInit();
            this.groupInfo.ResumeLayout(false);
            this.groupInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupList)).EndInit();
            this.groupList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMainAE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMainAE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupInfo;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.GroupControl groupList;
        private DevExpress.XtraGrid.GridControl gcMainAE;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMainAE;
        private DevExpress.XtraEditors.TextEdit txtDescription;
    }
}
