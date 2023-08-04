namespace CNY_BaseSys.WForm
{


    partial class FrmSearch
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
            this.labSelectItem = new DevExpress.XtraEditors.LabelControl();
            this.groCondition = new DevExpress.XtraEditors.GroupControl();
            this.panelControlAdd = new DevExpress.XtraEditors.PanelControl();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlSearch = new DevExpress.XtraGrid.GridControl();
            this.gvSearch = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControlOperator = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.chkOr = new DevExpress.XtraEditors.CheckEdit();
            this.chkAnd = new DevExpress.XtraEditors.CheckEdit();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbOperator = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.searchLookUpFiled = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.grisearchLookUpFiled = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnFind = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groCondition)).BeginInit();
            this.groCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlOperator)).BeginInit();
            this.panelControlOperator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkOr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbOperator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpFiled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grisearchLookUpFiled)).BeginInit();
            this.SuspendLayout();
            // 
            // labSelectItem
            // 
            this.labSelectItem.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labSelectItem.Location = new System.Drawing.Point(6, 7);
            this.labSelectItem.Name = "labSelectItem";
            this.labSelectItem.Size = new System.Drawing.Size(111, 13);
            this.labSelectItem.TabIndex = 0;
            this.labSelectItem.Text = "Select Item Needed";
            // 
            // groCondition
            // 
            this.groCondition.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groCondition.AppearanceCaption.ForeColor = System.Drawing.Color.DarkRed;
            this.groCondition.AppearanceCaption.Options.UseFont = true;
            this.groCondition.AppearanceCaption.Options.UseForeColor = true;
            this.groCondition.Controls.Add(this.panelControlAdd);
            this.groCondition.Controls.Add(this.btnRemove);
            this.groCondition.Controls.Add(this.btnAdd);
            this.groCondition.Controls.Add(this.gridControlSearch);
            this.groCondition.Controls.Add(this.panelControlOperator);
            this.groCondition.Controls.Add(this.pictureBox1);
            this.groCondition.Controls.Add(this.cbOperator);
            this.groCondition.Controls.Add(this.labelControl1);
            this.groCondition.Location = new System.Drawing.Point(3, 27);
            this.groCondition.Name = "groCondition";
            this.groCondition.Size = new System.Drawing.Size(623, 375);
            this.groCondition.TabIndex = 2;
            this.groCondition.Text = "Condition";
            // 
            // panelControlAdd
            // 
            this.panelControlAdd.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlAdd.Location = new System.Drawing.Point(259, 24);
            this.panelControlAdd.Name = "panelControlAdd";
            this.panelControlAdd.Size = new System.Drawing.Size(360, 47);
            this.panelControlAdd.TabIndex = 24;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Image = global::CNY_BaseSys.Properties.Resources._1400231837_Delete;
            this.btnRemove.Location = new System.Drawing.Point(541, 110);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(78, 28);
            this.btnRemove.TabIndex = 15;
            this.btnRemove.Text = "Remove";
            this.btnRemove.ToolTip = "Remove Row Filter Expression On Gridview\r\n (Alt+R)";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Image = global::CNY_BaseSys.Properties.Resources._1400231715_Add;
            this.btnAdd.Location = new System.Drawing.Point(541, 78);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(78, 28);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add";
            this.btnAdd.ToolTip = "Add Filter Expression Into Gridview\r\n (Alt+A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gridControlSearch
            // 
            this.gridControlSearch.Location = new System.Drawing.Point(2, 78);
            this.gridControlSearch.MainView = this.gvSearch;
            this.gridControlSearch.Name = "gridControlSearch";
            this.gridControlSearch.Size = new System.Drawing.Size(535, 295);
            this.gridControlSearch.TabIndex = 13;
            this.gridControlSearch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSearch});
            // 
            // gvSearch
            // 
            this.gvSearch.GridControl = this.gridControlSearch;
            this.gvSearch.Name = "gvSearch";
            // 
            // panelControlOperator
            // 
            this.panelControlOperator.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlOperator.Controls.Add(this.labelControl4);
            this.panelControlOperator.Controls.Add(this.chkOr);
            this.panelControlOperator.Controls.Add(this.chkAnd);
            this.panelControlOperator.Location = new System.Drawing.Point(2, 47);
            this.panelControlOperator.Name = "panelControlOperator";
            this.panelControlOperator.Size = new System.Drawing.Size(255, 24);
            this.panelControlOperator.TabIndex = 12;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Location = new System.Drawing.Point(6, 5);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(51, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Operator";
            // 
            // chkOr
            // 
            this.chkOr.Location = new System.Drawing.Point(125, 2);
            this.chkOr.Name = "chkOr";
            this.chkOr.Properties.Caption = "Or";
            this.chkOr.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkOr.Properties.RadioGroupIndex = 1;
            this.chkOr.Size = new System.Drawing.Size(39, 19);
            this.chkOr.TabIndex = 11;
            this.chkOr.TabStop = false;
            // 
            // chkAnd
            // 
            this.chkAnd.EditValue = true;
            this.chkAnd.Location = new System.Drawing.Point(77, 2);
            this.chkAnd.Name = "chkAnd";
            this.chkAnd.Properties.Caption = "And";
            this.chkAnd.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkAnd.Properties.RadioGroupIndex = 1;
            this.chkAnd.Size = new System.Drawing.Size(45, 19);
            this.chkAnd.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CNY_BaseSys.Properties.Resources.arrow_right_icon;
            this.pictureBox1.Location = new System.Drawing.Point(232, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 21);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // cbOperator
            // 
            this.cbOperator.Location = new System.Drawing.Point(75, 25);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbOperator.Size = new System.Drawing.Size(155, 20);
            this.cbOperator.TabIndex = 3;
            this.cbOperator.TabStop = false;
            this.cbOperator.EditValueChanged += new System.EventHandler(this.cbOperator_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(8, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Expression";
            // 
            // searchLookUpFiled
            // 
            this.searchLookUpFiled.Location = new System.Drawing.Point(125, 4);
            this.searchLookUpFiled.Name = "searchLookUpFiled";
            this.searchLookUpFiled.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpFiled.Properties.NullText = "";
            this.searchLookUpFiled.Properties.View = this.grisearchLookUpFiled;
            this.searchLookUpFiled.Size = new System.Drawing.Size(223, 20);
            this.searchLookUpFiled.TabIndex = 3;
            this.searchLookUpFiled.TabStop = false;
            this.searchLookUpFiled.Popup += new System.EventHandler(this.searchLookUpFiled_Popup);
            this.searchLookUpFiled.EditValueChanged += new System.EventHandler(this.searchLookUpFiled_EditValueChanged);
            // 
            // grisearchLookUpFiled
            // 
            this.grisearchLookUpFiled.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.grisearchLookUpFiled.Name = "grisearchLookUpFiled";
            this.grisearchLookUpFiled.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.grisearchLookUpFiled.OptionsView.ShowGroupPanel = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::CNY_BaseSys.Properties.Resources.Exitimage;
            this.btnCancel.Location = new System.Drawing.Point(541, 403);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 28);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTip = "Close Search Form";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFind.Image = global::CNY_BaseSys.Properties.Resources.Search_image;
            this.btnFind.Location = new System.Drawing.Point(458, 403);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(80, 28);
            this.btnFind.TabIndex = 18;
            this.btnFind.Text = "Find";
            this.btnFind.ToolTip = "Search Data Form Database By Expression Textbox Search (Alt+F)";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // FrmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(628, 432);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.searchLookUpFiled);
            this.Controls.Add(this.groCondition);
            this.Controls.Add(this.labSelectItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSearch";
            this.Load += new System.EventHandler(this.FrmSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groCondition)).EndInit();
            this.groCondition.ResumeLayout(false);
            this.groCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlOperator)).EndInit();
            this.panelControlOperator.ResumeLayout(false);
            this.panelControlOperator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkOr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbOperator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpFiled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grisearchLookUpFiled)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labSelectItem;
        private DevExpress.XtraEditors.GroupControl groCondition;
        private DevExpress.XtraEditors.ComboBoxEdit cbOperator;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpFiled;
        private DevExpress.XtraGrid.Views.Grid.GridView grisearchLookUpFiled;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckEdit chkOr;
        private DevExpress.XtraEditors.CheckEdit chkAnd;
        private DevExpress.XtraEditors.PanelControl panelControlOperator;
        private DevExpress.XtraGrid.GridControl gridControlSearch;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSearch;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnFind;
        private DevExpress.XtraEditors.PanelControl panelControlAdd;
    }
}