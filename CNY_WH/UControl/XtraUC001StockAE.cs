using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Security;
using System.Windows.Forms;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_WH.Class;
using CNY_WH.Info;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_WH.UControl
{
    public partial class XtraUC001StockAE : UserControl
    {
        #region Properties
        Inf_001Stock _inf = new Inf_001Stock();
        Cls_001Stock _cls = new Cls_001Stock();
        List<Tuple<Control, int>> _list = new List<Tuple<Control, int>>();
        int _pk;
        string _otp;
        private SystemGetCodeRule _sysCode;
        OpenFileDialog op = new OpenFileDialog();
        #endregion

        #region Contractor
        public XtraUC001StockAE(int pk, string otp)
        {
            InitializeComponent();
            _pk = pk;
            _otp = otp;
            Format();
            GenerateEvent();
            LoadData();

        }
      #endregion

        #region Format
        public void Format()
        {
            FormatHeader();
            FormatGridview(gcCode, gvCode);
        }
        #region FormatHeader
        public void FormatHeader()
        {
            txtCode.Properties.ReadOnly = true;
            if (_otp == "Add" || _otp == "Copy")
            {

            }
            else
            {
                LockHeader();
            }
        }
        void LockHeader()
        {

        }

        #endregion

        #region FormatGridview

        private void FormatGridview(GridControl gcMain, GridView gvMain)
        {
            FormatGridView.InitAE(gcMain, gvMain);
            InitColumGridview();
            LockColumnGridview( gvMain);
            GenerateEventGridview();
        }

        #region InitColumGridview
        private void InitColumGridview()
        {
            FormatGridView.CreateColumnOnGridview(gvCode, HorzAlignment.Default, "Mô tả", "Description", 1);
            FormatGridView.CreateColumnOnGridview(gvCode, HorzAlignment.Center, "Giá trị", "Value", 2);
            FormatGridView.CreateColumnOnGridview(gvCode, HorzAlignment.Center, "Độ dài", "Lenghth", 3);
        }
        #endregion

        #region Gridview Format Event 

        void LockColumnGridview(GridView gvMain)
        {
            foreach (GridColumn column in gvMain.Columns)
            {
                column.OptionsColumn.AllowEdit = false;
            }
        }
        public void GenerateEventGridview()
        {
            gvCode.RowCellStyle += gvCode_RowCellStyle;
            gvCode.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;
        }
        private void gvCode_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.OptionsColumn.AllowEdit == false)
            {
                // Đặt màu cho ô khi cột bị khóa
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else
            {
                // Đặt màu cho các ô khác (các cột không bị khóa)
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
            }
        }
        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;

            if (gv.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkMagenta;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }
            e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());
        }
        #endregion
        #endregion



        #endregion

        #region LoadData

        public void LoadData()
        {
            LoadDataToSlue();


            if (_pk == -1)
            {
                DisplayDataForAdding();
            }
            else
            {
                DisplayDataForEditing();
            }
            //Kiểm tra nếu đây là sao chép từ phiếu cũ thì PK=-1
            if (_otp == "Copy")
            {
                _pk = -1;
            }
        }

        #region Load Data To Search lookup Edit
        public void LoadDataToSlue()
        {
            //Type
            slueType.Properties.DataSource = _inf.Excute("SELECT PK [Code], [Name] FROM dbo.StockType where PK<4");
            slueType.Properties.DisplayMember = "Code";
            slueType.Properties.ValueMember = "Code";
            slueType.Properties.NullText = null;

            //Group
            slueGroup.Properties.DataSource = _inf.Excute("SELECT PK [Code], [Name] FROM dbo.StockGroup where PK<4");
            slueGroup.Properties.DisplayMember = "Code";
            slueGroup.Properties.ValueMember = "Code";
            slueGroup.Properties.NullText = null;

            //Scope
            slueScope.Properties.DataSource = _inf.Excute("SELECT PK [Code], [Name] FROM dbo.StockScope where PK<3");
            slueScope.Properties.DisplayMember = "Code";
            slueScope.Properties.ValueMember = "Code";
            slueScope.Properties.NullText = null;

            //Unit
            slueUnit.Properties.DataSource = _inf.Excute("SELECT PK [Code], [Name] FROM dbo.StockUnit");
            slueUnit.Properties.DisplayMember = "Code";
            slueUnit.Properties.ValueMember = "Code";
            slueUnit.Properties.NullText = null;

            //Manufacturer
            slueManufacturer.Properties.DataSource = _inf.Excute("SELECT PK [Code], [Name] FROM dbo.Manufacturer");
            slueManufacturer.Properties.DisplayMember = "Code";
            slueManufacturer.Properties.ValueMember = "Code";
            slueManufacturer.Properties.NullText = null;

            //Origin
            slueOrigin.Properties.DataSource = _inf.Excute("SELECT PK [Code],RTRIM(CNY002) [Name] FROM dbo.CNY00007");
            slueOrigin.Properties.DisplayMember = "Code";
            slueOrigin.Properties.ValueMember = "Code";
            slueOrigin.Properties.NullText = null;

        }
        #endregion

        #region LoadDataGridViewCode
        public void LoadDataGridViewCode()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("Lenghth", typeof(string));
            dt.Rows.Add("Mã loại vật tư", ProcessGeneral.GetSafeString(slueType.EditValue), "1");
            dt.Rows.Add("Mã nhóm vật tư", ProcessGeneral.GetSafeString(slueGroup.EditValue), "1");
            dt.Rows.Add("Mã Phạm vi vật tư", ProcessGeneral.GetSafeString(slueScope.EditValue), "1");
            if (_otp == "Edit")
                dt.Rows.Add("Số serial (Tự tăng)", ProcessGeneral.RightString(ProcessGeneral.GetSafeString(txtCode.EditValue), 4), "4");
            else
                dt.Rows.Add("Số serial (Tự tăng)", "");
            gcCode.DataSource = dt;
            gvCode.BestFitColumns();
        }

        #endregion

        #region Display Data For Editing
        public void DisplayDataForEditing()
        {
            DataTable dt = _inf.sp_Stock_Select(_pk);
            slueGroup.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["StockGroup"]);
            slueScope.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["StockScope"]);
            slueType.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["StockType"]);
            if(_otp=="Edit")
                txtCode.EditValue= ProcessGeneral.GetSafeString(dt.Rows[0]["Code"]);
            txtName.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Name"]);
            slueUnit.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["Unit"]);
            textEdit1.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Certificate"]);
            slueManufacturer.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["Manufacturer"]);
            slueOrigin.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["Origin"]);
            spinMin.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["MinStock"]);
            spinMax.EditValue = ProcessGeneral.GetSafeInt(dt.Rows[0]["MaxStock"]);
            LoadDataGridViewCode();
        }
        #endregion

        #region Display Data For Adding
        public void DisplayDataForAdding()
        {

        }

        private string CreateCustomerCode()
        {
            //DataTable tb = new DataTable();
            //string code = "";
            //string a = string.Format("select top(1) RIGHT('A00' + ltrim(rtrim(str((convert(int, right(CNY001, 2)) + 1)))), 2) as Number " +
            //    " from CNY00011 where SUBSTRING(CNY001,2,2) ='{0}' order by CNY001 desc", DateTime.Now.ToString("yy"));

            //tb = _inf.Excute(a);
            //if (tb.Rows.Count > 0)
            //{
            //    code = "C" + DateTime.Now.ToString("yy") + tb.Rows[0][0].ToString();
            //}
            //else
            //{
            //    code = "C" + DateTime.Now.ToString("yy") + "01";
            //}
            //return code;
            _sysCode = new SystemGetCodeRule(19, "CNY00011");
            _sysCode.CreateCodeString();
            return _sysCode.CodeRuleData.StrCode;
        }

        #endregion

        #endregion

        #region Generate Event
        public void GenerateEvent()
        {
            GenerateEventSearchlookupEdit();

        }

        #region Generate Event Search lookup Edit
        public void GenerateEventSearchlookupEdit()
        {
            slueGroup.EditValueChanged += slue_EditValueChanged;
            slueGroup.Popup += slue_Popup;
            slueType.EditValueChanged += slue_EditValueChanged;
            slueType.Popup += slue_Popup;
            slueScope.EditValueChanged += slue_EditValueChanged;
            slueScope.Popup += slue_Popup;
            slueManufacturer.EditValueChanged += slue_EditValueChanged;
            slueManufacturer.Popup += slue_Popup;
            slueUnit.EditValueChanged += slue_EditValueChanged;
            slueUnit.Popup += slue_Popup;
            slueOrigin.EditValueChanged += slue_EditValueChanged;
            slueOrigin.Popup += slue_Popup;
        }

        private void slue_Popup(object sender, EventArgs e)
        {
            var slue = sender as SearchLookUpEdit;
            GridView a = slue.Properties.View;
            a.BestFitColumns();
        }

        void slue_EditValueChanged(object sender, EventArgs e)
        {
            var slue = sender as SearchLookUpEdit;
            SetDescriptionText(slue);
        }
        public void SetDescriptionText(SearchLookUpEdit slue)
        {

            DataTable dtsource = slue.Properties.DataSource as DataTable;
            TextEdit desc = this.Controls.Find("txt" + slue.Name.Substring(4, slue.Name.Length - 4) + "Desc", true).FirstOrDefault() as TextEdit;
            if (dtsource == null)
            {
                desc.EditValue = "";
                return;
            }
            var drv = slue.Properties.GetRowByKeyValue(slue.EditValue) as DataRowView;
            if (drv != null)
            {
                string a = ProcessGeneral.GetSafeString(drv.Row["Name"]);

                desc.EditValue = a;
            }
            else
            {
                desc.EditValue = "";
            }
        }
        #endregion

        #region Save
        public void Save()
        {
            #region Check input

            #endregion

            #region Get Info
            _cls.PK = _pk;
            _cls.Code =ProcessGeneral.GetSafeString(txtCode.EditValue);
            _cls.Name = ProcessGeneral.GetSafeString(txtName.EditValue);
            _cls.StockType = ProcessGeneral.GetSafeInt(slueType.EditValue);
            _cls.StockGroup = ProcessGeneral.GetSafeInt(slueGroup.EditValue);
            _cls.StockScope = ProcessGeneral.GetSafeInt(slueScope.EditValue);
            _cls.Unit = ProcessGeneral.GetSafeInt(slueUnit.EditValue);
            _cls.MinStock = ProcessGeneral.GetSafeInt(spinMin.EditValue);
            _cls.MaxStock = ProcessGeneral.GetSafeInt(spinMax.EditValue);
            _cls.Created_By = DeclareSystem.SysUserName;
            _cls.Created_Date = DateTime.Now;
            _cls.Updated_By = DeclareSystem.SysUserName;
            _cls.Updated_Date = DateTime.Now;
            _cls.Origin = ProcessGeneral.GetSafeInt(slueOrigin.EditValue);
            _cls.Certificate = ProcessGeneral.GetSafeString(textEdit1.EditValue);
            _cls.Manufacturer = ProcessGeneral.GetSafeInt(slueManufacturer.EditValue);

            #endregion

            #region Save and show message
            //Update data
            DataTable dtSaveResult = _inf.sp_Stock_Update(_cls);
            //Get result info
            string msg = ProcessGeneral.GetSafeString(dtSaveResult.Rows[0]["ErrMsg"]);
            //Set new ID
            if (_cls.PK == -1)
            {
                //Update new ID
                _cls.PK = ProcessGeneral.GetSafeInt(dtSaveResult.Rows[0]["IDENTITY"]);
                _pk = _cls.PK;
            }

            XtraMessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DisplayDataForEditing();
            #endregion
        }

        #endregion

        #endregion

    }
}
