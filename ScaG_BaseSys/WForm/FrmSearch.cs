using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraRichEdit;
using CNY_BaseSys.Common;
using CNY_BaseSys.UControl;
namespace CNY_BaseSys.WForm
{
    public partial class FrmSearch : DevExpress.XtraEditors.XtraForm
    {
        
         #region "Property"
        public event SearchHandler searchEvent= null;
        readonly DataTable _dtFiled;
        private string _fielddisplay = "";
        private string _fieldvalue = "";
        private string _fieldtype = "";
        readonly XtraUCPanelControlValueString _panelControlValueString;
        readonly XtraUCPanelControlNumber _panelControlNumBer;
        readonly XtraUCPanelControlNumberBetween _panelControlNumberBetween;
        readonly XtraUCPanelControlDateTime _panelControlDateTime;
        readonly XtraUCPanelControlDateTimeBetween _panelControlDateTimeBetween;
        readonly XtraUCPanelControlBoolean _panelControlBool;
        private string _fillterDisplayAdd = "";
        private string _fillterValueAdd = "";
        private string _strTitileForm = "";
        public string TitileForm { get { return this._strTitileForm; } set { this._strTitileForm = value; } }
        private MemoEdit txtStringValue;
        private TextEdit txtNumberValue;
        private TextEdit txtNumberValueStart;
        private TextEdit txtNumberValueEnd;
        private DateEdit txtdatetimeValue;
        private DateEdit txtdatetimeValueStart;
        private DateEdit txtdatetimeValueEnd;
        private CheckEdit chkCheckValue;

        private bool _enablePanelOperator = true;
        public bool EnablePanelOperator
        {
            get { return _enablePanelOperator; }
            set { _enablePanelOperator = value; }
        }
        #endregion

        #region "contructor"

        /// <summary>
        ///     Parameter DatatTable have to structure includes 3 column and sort by number as follows: FieldValue ,FieldDisplay ,FieldType. 
        ///     FiledValue is column in the database
        ///     FieldDisplay is column display search
        ///     FieldType is column data type (includes 4 type(string, datetime, number,bool))
        /// </summary>
        /// <param name="dt" type="System.Data.DataTable">
        ///     <para>
        ///      
        ///     </para>
        /// </param>
        public FrmSearch(DataTable dt)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;         
            this.cbOperator.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            _panelControlValueString = new XtraUCPanelControlValueString()
            {
              Dock = DockStyle.Fill,
              Name = "panelControlValueString",
            };
            panelControlAdd.Controls.Add(_panelControlValueString);
            txtStringValue = _panelControlValueString.txtStringValues;
            //
            _panelControlNumBer = new XtraUCPanelControlNumber()
            {
                Dock = DockStyle.Fill,
                Name = "panelControlNumBer",
            };
            panelControlAdd.Controls.Add(_panelControlNumBer);
            txtNumberValue = _panelControlNumBer.txtNumberValueP;
            //
            _panelControlNumberBetween = new XtraUCPanelControlNumberBetween()
            {
              Dock = DockStyle.Fill,
              Name = "panelControlNumberBetween",
            };
            panelControlAdd.Controls.Add(_panelControlNumberBetween);
            txtNumberValueStart = _panelControlNumberBetween.txtNumberValueStartP;
            txtNumberValueEnd = _panelControlNumberBetween.txtNumberValueEndP;
            //
            _panelControlDateTime = new XtraUCPanelControlDateTime()
            {
              Dock = DockStyle.Fill,
              Name = "panelControlDateTime",
            };
            panelControlAdd.Controls.Add(_panelControlDateTime);
            txtdatetimeValue = _panelControlDateTime.txtdatetimeValueP;
            //
            _panelControlDateTimeBetween = new XtraUCPanelControlDateTimeBetween()
            {
              Dock = DockStyle.Fill,
              Name = "panelControlDateTimeBetween",
            };
            panelControlAdd.Controls.Add(_panelControlDateTimeBetween);
            txtdatetimeValueStart = _panelControlDateTimeBetween.txtdatetimeValueStartP;
            txtdatetimeValueEnd = _panelControlDateTimeBetween.txtdatetimeValueEndP;
            //
            _panelControlBool = new XtraUCPanelControlBoolean()
            {
               Dock = DockStyle.Fill,
               Name = "panelControlBool",
            };
            panelControlAdd.Controls.Add(_panelControlBool);
            chkCheckValue = _panelControlBool.chkCheckValueP;
            //
            this._dtFiled = dt;
            GridViewCustomInit();
            searchLookUpFiled.Select();
            UsePanelOpertorAndCommandButton();
            searchLookUpFiled.KeyDown += searchLookUpFiled_KeyDown; ;
            cbOperator.KeyDown += cbOperator_KeyDown;
            txtStringValue.KeyDown += txtStringValue_KeyDown;
            chkAnd.KeyDown += chkAnd_KeyDown;
            chkOr.KeyDown += chkOr_KeyDown;
            txtNumberValue.KeyDown += txtNumberValue_KeyDown;
            txtNumberValueStart.KeyDown += txtNumberValueStart_KeyDown;
            txtNumberValueEnd.KeyDown += txtNumberValueEnd_KeyDown;
            txtdatetimeValue.KeyDown += txtdatetimeValue_KeyDown;
            txtdatetimeValueStart.KeyDown += txtdatetimeValueStart_KeyDown;
            txtdatetimeValueEnd.KeyDown += txtdatetimeValueEnd_KeyDown;
            chkCheckValue.KeyDown += chkCheckValue_KeyDown;
        }

      

    

        #endregion

        #region "form event"
        private void FrmSearch_Load(object sender, EventArgs e)
        {
            this.Text = _strTitileForm;
            LoadSearchLookupEditField(_dtFiled);
            searchLookUpFiled.Select();
        }
        #endregion

        #region "Methold"


        /// <summary>
        ///     Hide or Visible Panel When Combobox Opertor Edit Values Changed
        /// </summary>
        /// <param name="statusString" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="statusNumBer" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="statusNumberBetween" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="statusDatetime" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="statusDateTimeBetween" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="statusBool" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        private void HideVisiblePanelValueType(bool statusString, bool statusNumBer, bool statusNumberBetween, bool statusDatetime, bool statusDateTimeBetween,bool statusBool)
        {
            _panelControlValueString.Visible = statusString;
            _panelControlNumBer.Visible = statusNumBer;
            _panelControlNumberBetween.Visible = statusNumberBetween;
            _panelControlDateTime.Visible = statusDatetime;
            _panelControlDateTimeBetween.Visible = statusDateTimeBetween;
            _panelControlBool.Visible = statusBool;
        }
        /// <summary>
        ///     Table Template Gridview condition search database
        /// </summary>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        private DataTable TableTemplateGridView()
        {
            var dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            dt.Columns.Add("FillterDisplay", typeof(string));
            dt.Columns.Add("FillterValue", typeof(string));
            dt.Columns.Add("Field", typeof(string));
            dt.Columns.Add("FieldValue", typeof(string));
            return dt;
        }

        /// <summary>
        ///     Khởi tạo cấu trúc của girdview
        /// </summary> 
        private void GridViewCustomInit()
        {

      
            

            gridControlSearch.UseEmbeddedNavigator = true;

            gridControlSearch.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gridControlSearch.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControlSearch.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControlSearch.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControlSearch.EmbeddedNavigator.Buttons.Remove.Visible = false;
            
            
        //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvSearch.OptionsBehavior.Editable = false;            
            gvSearch.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvSearch.OptionsBehavior.AllowDeleteRows = DefaultBoolean.True;
            gvSearch.OptionsCustomization.AllowColumnMoving = false;
            gvSearch.OptionsCustomization.AllowQuickHideColumns = true;
            gvSearch.OptionsCustomization.AllowSort = true;
            gvSearch.OptionsCustomization.AllowFilter = true;


            gvSearch.OptionsView.ShowGroupPanel = false;
            gvSearch.OptionsView.ShowIndicator = true;
            gvSearch.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvSearch.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvSearch.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvSearch.OptionsView.ShowAutoFilterRow = false;
            gvSearch.OptionsView.AllowCellMerge = false;
            // gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvSearch.OptionsNavigation.AutoFocusNewRow = true;
            gvSearch.OptionsNavigation.UseTabKey = true;

            gvSearch.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvSearch.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvSearch.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvSearch.OptionsView.EnableAppearanceEvenRow = true;
            gvSearch.OptionsView.EnableAppearanceOddRow = true;

            gvSearch.OptionsView.ShowFooter = false;
          
           

            //   gridView1.RowHeight = 25;

            gvSearch.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvSearch.OptionsFind.AlwaysVisible = false;
            gvSearch.OptionsFind.ShowCloseButton = true;
            gvSearch.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvSearch)
            {
                IsPerFormEvent = true,
            };
            var gridColumn1 = new GridColumn();
            gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            gridColumn1.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.Caption = @"No.";   
            gridColumn1.FieldName = "No";
            gridColumn1.Name = "No";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            gvSearch.Columns.Add(gridColumn1);

            
            var gridColumn2 = new GridColumn();
            gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            gridColumn2.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;         
            gridColumn2.Caption = @"Fillter Display";
            gridColumn2.FieldName = "FillterDisplay";
            gridColumn2.Name = "FillterDisplay";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            gvSearch.Columns.Add(gridColumn2);


            var gridColumn3 = new GridColumn();
            gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            gridColumn3.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn3.Caption = @"Fillter Value";
            gridColumn3.FieldName = "FillterValue";
            gridColumn3.Name = "FillterValue";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex =2;
            gvSearch.Columns.Add(gridColumn3);



            var gridColumn4 = new GridColumn();
            gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            gridColumn4.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn4.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn4.Caption = @"Field";
            gridColumn4.FieldName = "Field";
            gridColumn4.Name = "Field";
            gridColumn4.Visible = true;
            gridColumn4.VisibleIndex = 3;
            gvSearch.Columns.Add(gridColumn4);


            var gridColumn5 = new GridColumn();
            gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            gridColumn5.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn5.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn5.Caption = @"FieldValue";
            gridColumn5.FieldName = "FieldValue";
            gridColumn5.Name = "FieldValue";
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 4;
            gvSearch.Columns.Add(gridColumn5);

            gvSearch.RowStyle += gvSearch_RowStyle;
            gvSearch.CustomDrawCell += gvSearch_CustomDrawCell;
            gvSearch.RowCountChanged += gvSearch_RowCountChanged;
            gvSearch.CustomDrawRowIndicator += gvSearch_CustomDrawRowIndicator;

            gridControlSearch.DataSource = TableTemplateGridView();
            ProcessGeneral.HideVisibleColumnsGridView(gvSearch, false, "No", "FillterValue", "Field", "FieldValue");
            gridControlSearch.ForceInitialize();
            BestFitColumnsGv();



        }

   

        private void BestFitColumnsGv()
        {
            gvSearch.BestFitColumns();
            gvSearch.Columns["FillterDisplay"].Width += 30;
        }
        /// <summary>
        ///     Use UsePanelOpertorAndCommandButton For And Or Opertor Select Combine Filter Expression And button Remove,Find,View
        /// </summary>
        private void UsePanelOpertorAndCommandButton()
        {
            if (gvSearch.RowCount > 0)
            {
                panelControlOperator.Enabled = _enablePanelOperator;
                btnRemove.Enabled = true;              
                btnFind.Enabled = true;
            }
            else
            {
                panelControlOperator.Enabled = false;
                btnRemove.Enabled = false;              
                btnFind.Enabled = false;
            }
        }

        /// <summary>
        /// Load Data LookupField
        /// </summary>
        /// <param name="dtpara"></param>
        private void LoadSearchLookupEditField(DataTable dtpara)
        {
            searchLookUpFiled.Properties.View.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            searchLookUpFiled.Properties.View.OptionsSelection.EnableAppearanceFocusedRow = true;
            searchLookUpFiled.Properties.View.OptionsSelection.EnableAppearanceFocusedCell = true;
            searchLookUpFiled.Properties.View.OptionsView.EnableAppearanceEvenRow = true;
            searchLookUpFiled.Properties.View.OptionsView.EnableAppearanceOddRow = true;
            searchLookUpFiled.Properties.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.ShowAlways;
            searchLookUpFiled.Properties.View.OptionsView.ShowAutoFilterRow = true;
            searchLookUpFiled.Properties.View.OptionsFind.AllowFindPanel = true;
            searchLookUpFiled.Properties.View.OptionsFind.AlwaysVisible = true;
            searchLookUpFiled.Properties.View.OptionsFind.HighlightFindResults = true;


            new MyFindPanelFilterHelper(searchLookUpFiled.Properties.View)
            {
                IsPerFormEvent = true,
            };
            searchLookUpFiled.Properties.ShowClearButton = false;



            searchLookUpFiled.Properties.DataSource = dtpara;
            searchLookUpFiled.Properties.DisplayMember = "FieldDisplay";
            searchLookUpFiled.Properties.ValueMember = "FieldValue";
            
            
            btnAdd.Enabled = false;
            HideVisiblePanelValueType(false, false, false, false, false,false);
           // searchLookUpFiled.EditValue = dtpara.Rows[0]["FieldValue"];
               

        }


        /// <summary>
        ///     Insert Item Into ComboboxOperator By Data Filed Type
        /// </summary>
        /// <param name="datatype" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>     
        private void LoadComBoOperator(string datatype)
        {
            cbOperator.Properties.Items.Clear();
            switch (datatype.Trim().ToLower())
            {
                case "":
                    cbOperator.Properties.Items.AddRange(new string[] { "",});
                    break;
                case "string":
                    cbOperator.Properties.Items.AddRange(new string[] { "", "Contains","Begins With","Ends With", "Is equal to", "Not equal to", });
                    break;
                case "number":
                case "datetime":
                    cbOperator.Properties.Items.AddRange(new string[] { "","Is equal to", "Not equal to", "Is greater than", 
                "Is greater than or equal to", "Is less than", "Is less than or equal to", "Between", });
                    break;
                case "bool":
                    cbOperator.Properties.Items.AddRange(new string[] { "", "Is equal to",});
                    break;
                default:
                    cbOperator.Properties.Items.AddRange(new string[] { "", });
                    break;
            }
         
            cbOperator.SelectedIndex = 0;
        }

        /// <summary>
        ///       Hide or Visible Panel When Combobox Opertor Edit Values Changed and ComboField DataType 
        /// </summary>
        /// <param name="parafieldtype" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fillterExpression" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        private void HideVisiblePanelValueTypeByDataType(string parafieldtype,string fillterExpression)
        {
            switch (parafieldtype.Trim().ToLower())
            {
                case "bool":
                    HideVisiblePanelValueType(false, false, false, false, false, true);
                    break;
                case "string":
                    HideVisiblePanelValueType(true,false,false,false,false,false);
                    break;
                case "number":      
                    if (fillterExpression == "Between")
                    {
                        HideVisiblePanelValueType(false, false, true, false, false, false);
                    }

                    else
                    {
                        HideVisiblePanelValueType(false, true, false, false, false, false);
                    }
                                                                              
                    break;
                case "datetime":
                    if (fillterExpression == "Between")
                    {
                        HideVisiblePanelValueType(false, false, false, false, true, false);
                    }
                    else
                    {
                        HideVisiblePanelValueType(false, false, false, true, false, false);
                    }
                                      
                    break;
                default:
                    HideVisiblePanelValueType(false,false,false,false,false,false);
                    break;
            }
        }

        /// <summary>
        ///      AddNew Row Into GridView From Expression Filter is builded
        /// </summary>
        /// <param name="strNo" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strFillterDisplay" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strFillterValue" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="field" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fieldV" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        private void AddRowIntoGridView(string strNo, string strFillterDisplay, string strFillterValue, string field, string fieldV)
        {
            gvSearch.AddNewRow();
            int newRowHandle = gvSearch.FocusedRowHandle;
            gvSearch.SetRowCellValue(newRowHandle, gvSearch.Columns["No"], strNo);
            gvSearch.SetRowCellValue(newRowHandle, gvSearch.Columns["FillterDisplay"], strFillterDisplay);
            gvSearch.SetRowCellValue(newRowHandle, gvSearch.Columns["FillterValue"], strFillterValue);
            gvSearch.SetRowCellValue(newRowHandle, gvSearch.Columns["Field"], field);
            gvSearch.SetRowCellValue(newRowHandle, gvSearch.Columns["FieldValue"], fieldV);
            gvSearch.UpdateCurrentRow();
        }

        /// <summary>
        ///     Get String Filter Expression On GridView (loop gridview row build append string)
        /// </summary>
        /// <returns>
        ///     A string value...
        /// </returns>
        private string GetStringFilterExpressionOnGridView()
        {
            string str = "";
            if (gvSearch.RowCount > 0)
            {
                str+= " Where ";
                for (int i = 0; i < gvSearch.RowCount; i++)
                {
                    str += String.Format("{0} ", ProcessGeneral.GetSafeString(gvSearch.GetRowCellValue(i, gvSearch.Columns["FillterValue"])));
                }
            }
            return str;
        }

        /// <summary>
        ///     Get Fillter Expression When Builded For Add GridView
        /// </summary>
        private void GetFillterDispLayAndFillterValue()
        {
            if (_fieldtype.Trim().ToLower() == "bool")
            {
                switch (ProcessGeneral.GetSafeString(cbOperator.EditValue))
                {
                    case "Is equal to":
                        _fillterDisplayAdd = String.Format(@"[{0}] = {1}", _fielddisplay, _panelControlBool.CheckDisplay);
                        _fillterValueAdd = String.Format(@"{0} = {1}", _fieldvalue, _panelControlBool.CheckValue);
                        break;                   
                    default:
                        _fillterDisplayAdd = string.Empty;
                        _fillterValueAdd = string.Empty;
                        break;
                }
            }
            if (_fieldtype.Trim().ToLower() == "string")
            {
                switch (ProcessGeneral.GetSafeString(cbOperator.EditValue))
                {
                    case "Contains":
                        _fillterDisplayAdd = String.Format(@"UPPER([{0}]) Contains '{1}'", _fielddisplay, _panelControlValueString.txtStringValues.Text.ToUpper());
                        _fillterValueAdd = String.Format(@"UPPER({0}) like N'%{1}%'", _fieldvalue, _panelControlValueString.txtStringValues.Text.ToUpper());
                        //   fillterValueAdd = String.Format(@"{0} like **********%{1}%**********", fieldvalue, panelControlValueString.txtStringValues.Text);
                        break;
                    case "Begins With":
                        _fillterDisplayAdd = String.Format(@"UPPER([{0}]) Begins With '{1}'", _fielddisplay, _panelControlValueString.txtStringValues.Text.ToUpper());
                        _fillterValueAdd = String.Format(@"UPPER({0}) like N'{1}%'", _fieldvalue, _panelControlValueString.txtStringValues.Text.ToUpper());
                        break;
                    case "Ends With":
                        _fillterDisplayAdd = String.Format(@"UPPER([{0}]) Ends With '{1}'", _fielddisplay, _panelControlValueString.txtStringValues.Text.ToUpper());
                        _fillterValueAdd = String.Format(@"UPPER({0}) like N'%{1}'", _fieldvalue, _panelControlValueString.txtStringValues.Text.ToUpper());
                        break;
                    case "Is equal to":
                        _fillterDisplayAdd = String.Format(@"UPPER([{0}]) = '{1}'", _fielddisplay, _panelControlValueString.txtStringValues.Text.ToUpper());
                        _fillterValueAdd = String.Format(@"UPPER({0}) = '{1}'", _fieldvalue, _panelControlValueString.txtStringValues.Text.ToUpper());
                        break;
                    case "Not equal to":
                        _fillterDisplayAdd = String.Format(@"UPPER([{0}]) <> '{1}'", _fielddisplay, _panelControlValueString.txtStringValues.Text.ToUpper());
                        _fillterValueAdd = String.Format(@"UPPER({0}) <> '{1}'", _fieldvalue, _panelControlValueString.txtStringValues.Text.ToUpper());
                        break;                         
                    default :
                        _fillterDisplayAdd = string.Empty;
                        _fillterValueAdd = string.Empty;
                        break;

                }
            }
            if (_fieldtype.Trim().ToLower() == "number")
            {
                switch (ProcessGeneral.GetSafeString(cbOperator.EditValue))
                {
                    case "Is equal to":
                        _fillterDisplayAdd = String.Format(@"[{0}] = {1}", _fielddisplay, _panelControlNumBer.txtNumberValueP.EditValue);
                        _fillterValueAdd = String.Format(@"{0} = {1}", _fieldvalue, _panelControlNumBer.txtNumberValueP.EditValue);
                        break;
                    case "Not equal to":
                        _fillterDisplayAdd = String.Format(@"[{0}] <> {1}", _fielddisplay, _panelControlNumBer.txtNumberValueP.EditValue);
                        _fillterValueAdd = String.Format(@"{0} <> {1}", _fieldvalue, _panelControlNumBer.txtNumberValueP.EditValue);
                        break;
                    case "Is greater than":
                        _fillterDisplayAdd = String.Format(@"[{0}] > {1}", _fielddisplay, _panelControlNumBer.txtNumberValueP.EditValue);
                        _fillterValueAdd = String.Format(@"{0} > {1}", _fieldvalue, _panelControlNumBer.txtNumberValueP.EditValue);
                        break;
                    case "Is greater than or equal to":
                        _fillterDisplayAdd = String.Format(@"[{0}] >= {1}", _fielddisplay, _panelControlNumBer.txtNumberValueP.EditValue);
                        _fillterValueAdd = String.Format(@"{0} >= {1}", _fieldvalue, _panelControlNumBer.txtNumberValueP.EditValue);
                        break;
                    case "Is less than":
                        _fillterDisplayAdd = String.Format(@"[{0}] < {1}", _fielddisplay, _panelControlNumBer.txtNumberValueP.EditValue);
                        _fillterValueAdd = String.Format(@"{0} < {1}", _fieldvalue, _panelControlNumBer.txtNumberValueP.EditValue);
                        break;
                    case "Is less than or equal to":
                        _fillterDisplayAdd = String.Format(@"[{0}] <= {1}", _fielddisplay, _panelControlNumBer.txtNumberValueP.EditValue);
                        _fillterValueAdd = String.Format(@"{0} <= {1}", _fieldvalue, _panelControlNumBer.txtNumberValueP.EditValue);
                        break;
                    case "Between":
                        _fillterDisplayAdd = String.Format(@"({1} <= [{0}] <= {2})", _fielddisplay, _panelControlNumberBetween.txtNumberValueStartP.EditValue, _panelControlNumberBetween.txtNumberValueEndP.EditValue);
                        _fillterValueAdd = String.Format(@"({0} between {1} and {2})", _fieldvalue, _panelControlNumberBetween.txtNumberValueStartP.EditValue, _panelControlNumberBetween.txtNumberValueEndP.EditValue);
                        break;
                    default:
                        _fillterDisplayAdd = string.Empty;
                        _fillterValueAdd = string.Empty;
                        break;

                }
            }

            if (_fieldtype.Trim().ToLower() == "datetime")
            {
                switch (ProcessGeneral.GetSafeString(cbOperator.EditValue))
                {
                    case "Is equal to":
                        _fillterDisplayAdd = String.Format(@"[{0}] = {1}", _fielddisplay, _panelControlDateTime.txtdatetimeValueP.Text);
                        _fillterValueAdd = String.Format(@"DATEDIFF(DD,{0},CONVERT(datetime,'{1}',103))=0" , _fieldvalue, _panelControlDateTime.txtdatetimeValueP.Text);
                        break;
                    case "Not equal to":
                        _fillterDisplayAdd = String.Format(@"[{0}] <> {1}", _fielddisplay, _panelControlDateTime.txtdatetimeValueP.Text);
                        _fillterValueAdd = String.Format(@"DATEDIFF(DD,{0},CONVERT(datetime,'{1}',103))<>0", _fieldvalue, _panelControlDateTime.txtdatetimeValueP.Text);
                        break;
                    case "Is greater than":
                        _fillterDisplayAdd = String.Format(@"[{0}] > {1}", _fielddisplay, _panelControlDateTime.txtdatetimeValueP.Text);
                        _fillterValueAdd = String.Format(@"DATEDIFF(DD,{0},CONVERT(datetime,'{1}',103))<0", _fieldvalue, _panelControlDateTime.txtdatetimeValueP.Text);
                        break;
                    case "Is greater than or equal to":
                        _fillterDisplayAdd = String.Format(@"[{0}] >= {1}", _fielddisplay, _panelControlDateTime.txtdatetimeValueP.Text);
                        _fillterValueAdd = String.Format(@"DATEDIFF(DD,{0},CONVERT(datetime,'{1}',103))<=0", _fieldvalue, _panelControlDateTime.txtdatetimeValueP.Text);
                        break;
                    case "Is less than":
                        _fillterDisplayAdd = String.Format(@"[{0}] < {1}", _fielddisplay, _panelControlDateTime.txtdatetimeValueP.Text);
                        _fillterValueAdd = String.Format(@"DATEDIFF(DD,{0},CONVERT(datetime,'{1}',103))>0", _fieldvalue, _panelControlDateTime.txtdatetimeValueP.Text);
                        break;
                    case "Is less than or equal to":
                        _fillterDisplayAdd = String.Format(@"[{0}] <= {1}", _fielddisplay, _panelControlDateTime.txtdatetimeValueP.Text);
                        _fillterValueAdd = String.Format(@"DATEDIFF(DD,{0},CONVERT(datetime,'{1}',103))>=0", _fieldvalue, _panelControlDateTime.txtdatetimeValueP.Text);
                        break;
                    case "Between":
                        _fillterDisplayAdd = String.Format(@"([{0}] From Date {1} To Date {2})", _fielddisplay, _panelControlDateTimeBetween.txtdatetimeValueStartP.Text, _panelControlDateTimeBetween.txtdatetimeValueEndP.Text);
                        _fillterValueAdd = String.Format(@"(convert(datetime,{0},103) between CONVERT(datetime,'{1} 00:00:00',103) and CONVERT(datetime,'{2} 23:59:59',103))", _fieldvalue, _panelControlDateTimeBetween.txtdatetimeValueStartP.Text, _panelControlDateTimeBetween.txtdatetimeValueEndP.Text);
                        break;
                    default:
                        _fillterDisplayAdd = string.Empty;
                        _fillterValueAdd = string.Empty;
                        break;

                }
            }
        }

        /// <summary>
        ///     Get Operator CheckBox Selected (And Or )
        /// </summary>
        /// <returns>
        ///     A string value...
        /// </returns>
        private string GetOpertorFilter()
        {
            if (chkAnd.Checked)
                return " And ";
            return " Or ";
        }

        /// <summary>
        ///     Set Empty TextBox and Datetime Picker
        /// </summary>
        private void SetNullControlValue()
        {
            _panelControlValueString.txtStringValues.EditValue = null;
            _panelControlNumBer.txtNumberValueP.EditValue = null;
            _panelControlDateTime.txtdatetimeValueP.EditValue = null;
            _panelControlNumberBetween.txtNumberValueStartP.EditValue = null;
            _panelControlNumberBetween.txtNumberValueEndP.EditValue = null;
            _panelControlDateTimeBetween.txtdatetimeValueStartP.EditValue = null;
            _panelControlDateTimeBetween.txtdatetimeValueEndP.EditValue = null;
        }
        #endregion

        #region "combobox event"

      
        private void searchLookUpFiled_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = searchLookUpFiled.Properties.View;
            _fielddisplay = ProcessGeneral.GetSafeString(view.GetFocusedRowCellValue("FieldDisplay"));
            _fieldvalue = ProcessGeneral.GetSafeString(view.GetFocusedRowCellValue("FieldValue"));
            _fieldtype = ProcessGeneral.GetSafeString(view.GetFocusedRowCellValue("FieldType"));
            LoadComBoOperator(_fieldtype);
          
        }

        private void cbOperator_EditValueChanged(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrEmpty(ProcessGeneral.GetSafeString(cbOperator.EditValue)))
            {
                btnAdd.Enabled = true;
                HideVisiblePanelValueTypeByDataType(_fieldtype, ProcessGeneral.GetSafeString(cbOperator.EditValue));
            }
            else
            {
                btnAdd.Enabled = false;
                HideVisiblePanelValueType(false,false,false,false,false,false);
            }

        }

        #endregion

        #region "button click event"
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (searchEvent != null)
            {
                searchEvent(this, new SearchEventArgs
                {
                    filterexpression = GetStringFilterExpressionOnGridView(),
                    DtSearch = gridControlSearch.DataSource as DataTable,
                });
            }
            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
              
            if (ProcessGeneral.GetSafeString(cbOperator.EditValue) != "")
            {
                if (_fieldtype.Trim().ToLower() == "number" && ProcessGeneral.GetSafeString(cbOperator.EditValue) != "Between" && _panelControlNumBer.txtNumberValueP.EditValue == null)
                {
                    XtraMessageBox.Show("Value Is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    return;
                }
                if (_fieldtype.Trim().ToLower() == "number" && ProcessGeneral.GetSafeString(cbOperator.EditValue) == "Between" && (_panelControlNumberBetween.txtNumberValueStartP.EditValue == null || _panelControlNumberBetween.txtNumberValueEndP.EditValue == null))
                {
                    XtraMessageBox.Show("Start Value or End Value Is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    return;
                }
                if (_fieldtype.Trim().ToLower() == "datetime" && ProcessGeneral.GetSafeString(cbOperator.EditValue) != "Between" && _panelControlDateTime.txtdatetimeValueP.EditValue == null)
                {
                    XtraMessageBox.Show("Date Is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    return;
                }
                if (_fieldtype.Trim().ToLower() == "datetime" && ProcessGeneral.GetSafeString(cbOperator.EditValue) == "Between" && (_panelControlDateTimeBetween.txtdatetimeValueStartP.EditValue == null || _panelControlDateTimeBetween.txtdatetimeValueEndP.EditValue == null))
                {
                    XtraMessageBox.Show("Start Date or End Date Is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    return;
                }
                GetFillterDispLayAndFillterValue();
                if (gvSearch.RowCount > 0)
                {
                    AddRowIntoGridView("", GetOpertorFilter() + _fillterDisplayAdd, GetOpertorFilter() + _fillterValueAdd, _fieldvalue, _fillterValueAdd); 
                }
                else
                {

                    AddRowIntoGridView("", _fillterDisplayAdd, _fillterValueAdd, _fieldvalue, _fillterValueAdd); 
                }
                UsePanelOpertorAndCommandButton();
                SetNullControlValue();
                BestFitColumnsGv();
                searchLookUpFiled.Select();
            }
             
            
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int delRowHandle = gvSearch.FocusedRowHandle;
            if (delRowHandle == 0)
            {
                gvSearch.DeleteRow(delRowHandle);
                if (gvSearch.RowCount > 0)
                {
                    string fillterDisplayRemove = ProcessGeneral.GetSafeString(gvSearch.GetRowCellValue(0, gvSearch.Columns["FillterDisplay"])).Trim();
                    string fillterValueRemove = ProcessGeneral.GetSafeString(gvSearch.GetRowCellValue(0, gvSearch.Columns["FillterValue"])).Trim();
                    gvSearch.SetRowCellValue(0, gvSearch.Columns["FillterDisplay"], fillterDisplayRemove.Substring(3));
                    gvSearch.SetRowCellValue(0, gvSearch.Columns["FillterValue"], fillterValueRemove.Substring(3));
                }
            }
            else
            {
                gvSearch.DeleteRow(delRowHandle);
            }       
            UsePanelOpertorAndCommandButton();
            BestFitColumnsGv();
            searchLookUpFiled.Select();
        }

        
        #endregion

        #region "gridview event"

        private void gvSearch_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvSearch_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }
        private void gvSearch_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex != 0) return;
            Image icon = CNY_BaseSys.Properties.Resources.database_search_icon;
            e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
            e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
            e.Handled = true;
        }

        private void gvSearch_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;


            }


        }

        private void searchLookUpFiled_Popup(object sender, EventArgs e)
        {
            var edit = sender as SearchLookUpEdit;
            GridView gv = edit.Properties.View;
            gv.Columns["FieldValue"].Visible = false;
        }
        #endregion

        #region "hotkey"
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {

                case Keys.Alt | Keys.A:
                    {

                        btnAdd_Click(null, null);
                        return true;
                    }
                case Keys.Alt | Keys.R:
                    {

                        btnRemove_Click(null, null);
                        return true;
                    }
                case Keys.Alt | Keys.F:
                    {
                        btnFind_Click(null, null);
                        return true;
                    }

            }
            return base.ProcessCmdKey(ref message, keys);
        }

        private void chkCheckValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (chkAnd.Enabled)
                {
                    chkAnd.Select();
                }
                else
                {

                    searchLookUpFiled.Select();
                }
            }
        }

        void txtdatetimeValueEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 )
            {
                if (chkAnd.Enabled)
                {
                    chkAnd.Select();
                }
                else
                {
                    
                    searchLookUpFiled.Select();
                }
             
            }
        }

        void txtdatetimeValueStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                txtdatetimeValueEnd.Select();
            }
        }

        private void txtdatetimeValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (chkAnd.Enabled)
                {
                    chkAnd.Select();
                }
                else
                {

                    searchLookUpFiled.Select();
                }
            }
        }

        private void txtNumberValueEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (chkAnd.Enabled)
                {
                    chkAnd.Select();
                }
                else
                {

                    searchLookUpFiled.Select();
                }
            }
        }

        private void txtNumberValueStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                txtNumberValueEnd.Select();
            }
        }

        private void txtNumberValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (chkAnd.Enabled)
                {
                    chkAnd.Select();
                }
                else
                {

                    searchLookUpFiled.Select();
                }
            }
        }

        private void chkOr_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F1)
            {
                searchLookUpFiled.Select();
            }
        }

        private void chkAnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                chkOr.Select();
            }
        }

        private void txtStringValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (chkAnd.Enabled)
                {
                    chkAnd.Select();
                }
                else
                {

                    searchLookUpFiled.Select();
                }
            }
        }

        private void cbOperator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)//Tab
            {
                if (_panelControlValueString.Visible)
                {
                    txtStringValue.Select();
                    return;
                }
                if (_panelControlNumBer.Visible)
                {
                    txtNumberValue.Select();
                    return;
                }
                if (_panelControlNumberBetween.Visible)
                {
                    txtNumberValueStart.Select();
                    return;
                }
                if (_panelControlDateTime.Visible)
                {
                    txtdatetimeValue.Select();
                    return;
                }
                if (_panelControlDateTimeBetween.Visible)
                {
                    txtdatetimeValueStart.Select();
                    return;
                }
                if (_panelControlBool.Visible)
                {
                    chkCheckValue.Select();
                    return;
                }
                searchLookUpFiled.Select();
                return;
            }
        }

        private void searchLookUpFiled_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                cbOperator.Select();
            }

        }

        #endregion

    

    }
}