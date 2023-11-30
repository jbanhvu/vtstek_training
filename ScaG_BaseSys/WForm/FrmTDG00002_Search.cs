using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_BaseSys.Properties;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_BaseSys.WForm
{
    public partial class FrmTDG00002_Search : DevExpress.XtraEditors.XtraForm
    {
        #region "Property & Field"
        public event SearchHandlerCode searchEvent = null;


        private string _fieldNameAttPk = "i.CNY00008PK";

        public string FieldNameAttPk
        {
            get { return this._fieldNameAttPk; }
            set { this._fieldNameAttPk = value; }
        }

        private string _fieldNameAttValue = "i.AttributeValue";
        public string FieldNameAttValue
        {
            get { return this._fieldNameAttValue; }
            set { this._fieldNameAttValue = value; }
        }




        private WaitDialogForm _dlg = null;



        private readonly RepositoryItemComboBox _reposItemComboNoType;
        private readonly RepositoryItemComboBox _reposItemComboString;
        private readonly RepositoryItemComboBox _reposItemComboNumberDateTime;
        private readonly RepositoryItemComboBox _reposItemComboBool;

        private readonly RepositoryItemComboBox _reposItemComboOperator;
        private readonly RepositoryItemTextEdit _repositoryText;
        private readonly RepositoryItemSpinEdit _repositorySpin;
        private readonly RepositoryItemCheckEdit _repositoryCheck;
        private readonly RepositoryItemTextEdit _repositoryTextDateTime;


        private readonly DataTable _dtFiled;
        #endregion

        #region "Contructor"

        public FrmTDG00002_Search(DataTable dtFiled)
        {
            InitializeComponent();
            this._dtFiled = dtFiled;

            _repositoryText = new RepositoryItemTextEdit { AutoHeight = false, CharacterCasing = CharacterCasing.Upper };
            _repositorySpin = new RepositoryItemSpinEdit
            {
                AutoHeight = false,
                MinValue = decimal.MinValue,
                MaxValue = decimal.MaxValue,
                AllowMouseWheel = false,
            };
            _repositorySpin.Buttons.Clear();



            _repositoryCheck = new RepositoryItemCheckEdit
            {
                AutoHeight = false,
            };

            _repositoryTextDateTime = new RepositoryItemTextEdit
            {
                AutoHeight = false,
                AllowNullInput = DefaultBoolean.True,
                AllowMouseWheel = false,
            };
            _repositoryTextDateTime.Mask.MaskType = MaskType.DateTime;
            _repositoryTextDateTime.Mask.EditMask = ConstSystem.SysDateFormat;
            _repositoryTextDateTime.Mask.UseMaskAsDisplayFormat = true;

            _reposItemComboOperator = new RepositoryItemComboBox
            {
                AutoHeight = false,
                TextEditStyle = TextEditStyles.DisableTextEditor,
            };
            _reposItemComboOperator.Items.AddRange(new object[] { "AND", "OR", });




            _reposItemComboNoType = new RepositoryItemComboBox
            {
                AutoHeight = false,
                TextEditStyle = TextEditStyles.DisableTextEditor,
            };
            _reposItemComboNoType.Items.AddRange(new object[] { "", });


            _reposItemComboString = new RepositoryItemComboBox
            {
                AutoHeight = false,
                TextEditStyle = TextEditStyles.DisableTextEditor,
            };
            _reposItemComboString.Items.AddRange(new object[] { "%%", "_%", "%_", "=", "!=", });


            _reposItemComboNumberDateTime = new RepositoryItemComboBox
            {
                AutoHeight = false,
                TextEditStyle = TextEditStyles.DisableTextEditor,
            };
            _reposItemComboNumberDateTime.Items.AddRange(new object[] { "=", "!=", ">", ">=", "<", "<=", "<-->", });


            _reposItemComboBool = new RepositoryItemComboBox
            {
                AutoHeight = false,
                TextEditStyle = TextEditStyles.DisableTextEditor,
            };
            _reposItemComboBool.Items.AddRange(new object[] { "=", });



            this.MinimizeBox = false;
            this.Load += FrmGenerate_Load;

            btnCancel.Click += btnCancel_Click;
            btnNextFinish.Click += btnNextFinish_Click;

            btnRemoveAll.Click += btnRemoveAll_Click;
            btnRemoveOneRow.Click += btnRemoveOneRow_Click;
            btnSelectAll.Click += btnSelectAll_Click;
            btnSelectOneRow.Click += btnSelectOneRow_Click;



        }


        #endregion








        #region "Form Event"

        private void FrmGenerate_Load(object sender, EventArgs e)
        {
            _dlg = new WaitDialogForm();



            GridViewAttributeInit();
            GridViewValueCustomInit();






            _dlg.Close();



        }
        #endregion

        #region "Button Click Event"


        private string GetExpressionFinal(string type, string fieldName, string expression, string value1, string value2)
        {
            string str = "";
            switch (expression)
            {
                case "%%":
                    str = String.Format(@"(UPPER({0}) like '%{1}%')", fieldName.Trim(), value1.Trim().ToUpper());
                    break;
                case "_%":
                    str = String.Format(@"(UPPER({0}) like '{1}%')", fieldName.Trim(), value1.Trim().ToUpper());
                    break;
                case "%_":
                    str = String.Format(@"(UPPER({0}) like '%{1}')", fieldName.Trim(), value1.Trim().ToUpper());
                    break;
                case "=":
                case "!=":
                    {
                        switch (type)
                        {
                            case "string":
                                str = String.Format(@"(UPPER({0}) {2} '{1}')", fieldName.Trim(), value1.Trim().ToUpper(), expression);
                                break;
                            case "datetime":
                                if (!string.IsNullOrEmpty(value1))
                                {
                                    str = String.Format(@"({0} {2} '{1}')", fieldName.Trim(), value1.Trim().ToUpper(), expression);
                                }

                                break;
                            case "number":
                            case "boolean":
                                if (!string.IsNullOrEmpty(value1))
                                {
                                    str = String.Format(@"({0} {2} {1})", fieldName.Trim(), value1.Trim().ToUpper(), expression);
                                }
                                break;
                        }

                    }
                    break;
                case ">":
                case ">=":
                case "<":
                case "<=":
                    if (!string.IsNullOrEmpty(value1))
                    {
                        switch (type)
                        {
                            case "datetime":
                                str = String.Format(@"({0} {2} '{1}')", fieldName.Trim(), value1.Trim().ToUpper(), expression);
                                break;
                            case "number":
                                str = String.Format(@"({0} {2} {1})", fieldName.Trim(), value1.Trim().ToUpper(), expression);
                                break;
                        }
                    }

                    break;


                case "<-->":
                    if (!string.IsNullOrEmpty(value1) && !string.IsNullOrEmpty(value2))
                    {
                        switch (type)
                        {
                            case "datetime":
                                str = String.Format(@"({0} BETWEEN '{1}' and '{2}')", fieldName.Trim(), value1.Trim().ToUpper(), value2.Trim().ToUpper());
                                break;
                            case "number":
                                str = String.Format(@"({0} BETWEEN {1} and {2})", fieldName.Trim(), value1.Trim().ToUpper(), value2.Trim().ToUpper());
                                break;
                        }
                    }

                    break;
            }
            return str;




        }



        private string GetExpressionFinal(string type, string fieldName, string expression, string value1, string value2, bool isAttribute, Int64 pkAttribute)
        {
            string str = "";
            switch (expression)
            {
                case "%%":
                    if (!isAttribute)
                    {
                        str = String.Format(@"(UPPER({0}) like '%{1}%')", fieldName.Trim(), value1.Trim().ToUpper());
                    }
                    else
                    {
                        str = String.Format(@"(({0}={1}) and ( UPPER({2}) like '%{3}%'))", _fieldNameAttPk, pkAttribute, _fieldNameAttValue, value1.Trim().ToUpper());
                    }
                    break;
                case "_%":
                    if (!isAttribute)
                    {
                        str = String.Format(@"(UPPER({0}) like '{1}%')", fieldName.Trim(), value1.Trim().ToUpper());
                    }
                    else
                    {
                        str = String.Format(@"(({0}={1}) and ( UPPER({2}) like '{3}%'))", _fieldNameAttPk, pkAttribute, _fieldNameAttValue, value1.Trim().ToUpper());
                    }
                    break;
                case "%_":
                    if (!isAttribute)
                    {
                        str = String.Format(@"(UPPER({0}) like '%{1}')", fieldName.Trim(), value1.Trim().ToUpper());
                    }
                    else
                    {
                        str = String.Format(@"(({0}={1}) and ( UPPER({2}) like '%{3}'))", _fieldNameAttPk, pkAttribute, _fieldNameAttValue, value1.Trim().ToUpper());
                    }
                    break;
                case "=":
                case "!=":
                    if (!isAttribute)
                    {
                        switch (type)
                        {
                            case "string":
                                str = String.Format(@"(UPPER({0}) {2} '{1}')", fieldName.Trim(), value1.Trim().ToUpper(), expression);
                                break;
                            case "datetime":
                                if (!string.IsNullOrEmpty(value1))
                                {
                                    str = String.Format(@"({0} {2} '{1}')", fieldName.Trim(), value1.Trim().ToUpper(), expression);
                                }

                                break;
                            case "number":
                            case "boolean":
                                if (!string.IsNullOrEmpty(value1))
                                {
                                    str = String.Format(@"({0} {2} {1})", fieldName.Trim(), value1.Trim().ToUpper(), expression);
                                }
                                break;
                        }

                    }
                    else
                    {
                        switch (type)
                        {
                            case "string":
                                str = String.Format(@"(({0}={1}) and ( UPPER({2}) {4} '{3}'))", _fieldNameAttPk, pkAttribute, _fieldNameAttValue, value1.Trim().ToUpper(), expression);
                                break;
                            case "datetime":
                                if (!string.IsNullOrEmpty(value1))
                                {
                                    str = String.Format(@"(({0}={1}) and ( {2} {4} '{3}'))", _fieldNameAttPk,
                                        pkAttribute, _fieldNameAttValue, value1.Trim().ToUpper(), expression);
                                }
                                break;
                            case "number":
                            case "boolean":
                                if (!string.IsNullOrEmpty(value1))
                                {
                                    str = String.Format(@"(({0}={1}) AND (ISNUMERIC(LTRIM(RTRIM({2})))=1) and (CAST(LTRIM(RTRIM({2})) AS FLOAT) {4} {3}))",
                                        _fieldNameAttPk, pkAttribute, _fieldNameAttValue, value1.Trim().ToUpper(), expression);
                                }
                                break;
                        }
                    }
                    break;
                case ">":
                case ">=":
                case "<":
                case "<=":
                    if (!string.IsNullOrEmpty(value1))
                    {
                        if (!isAttribute)
                        {
                            switch (type)
                            {
                                case "datetime":
                                    str = String.Format(@"({0} {2} '{1}')", fieldName.Trim(), value1.Trim().ToUpper(), expression);
                                    break;
                                case "number":
                                    str = String.Format(@"({0} {2} {1})", fieldName.Trim(), value1.Trim().ToUpper(), expression);
                                    break;
                            }

                        }
                        else
                        {
                            switch (type)
                            {
                                case "datetime":
                                    str = String.Format(@"(({0}={1}) AND (ISDATE(LTRIM(RTRIM({2})))=1) and (CONVERT(DATETIME,LTRIM(RTRIM({2})),103) {4} '{3}'))", _fieldNameAttPk, pkAttribute,
                                        _fieldNameAttValue, value1.Trim().ToUpper(), expression);
                                    break;
                                case "number":
                                    str = String.Format(@"(({0}={1}) AND (ISNUMERIC(LTRIM(RTRIM({2})))=1) and (CAST(LTRIM(RTRIM({2})) AS FLOAT) {4} {3}))", _fieldNameAttPk, pkAttribute,
                                        _fieldNameAttValue, value1.Trim().ToUpper(), expression);
                                    break;
                            }
                        }
                    }

                    break;


                case "<-->":
                    if (!string.IsNullOrEmpty(value1) && !string.IsNullOrEmpty(value2))
                    {
                        if (!isAttribute)
                        {
                            switch (type)
                            {
                                case "datetime":
                                    str = String.Format(@"({0} BETWEEN '{1}' and '{2}')", fieldName.Trim(), value1.Trim().ToUpper(), value2.Trim().ToUpper());
                                    break;
                                case "number":
                                    str = String.Format(@"({0} BETWEEN {1} and {2})", fieldName.Trim(), value1.Trim().ToUpper(), value2.Trim().ToUpper());
                                    break;
                            }

                        }
                        else
                        {
                            switch (type)
                            {
                                case "datetime":
                                    str = String.Format(@"(({0}={1}) AND (ISDATE(LTRIM(RTRIM({2})))=1) and (CONVERT(DATETIME,LTRIM(RTRIM({2})),103) BETWEEN '{3}' and '{4}'))",
                                        _fieldNameAttPk, pkAttribute, _fieldNameAttValue, value1.Trim().ToUpper(), value2.Trim().ToUpper());
                                    break;
                                case "number":
                                    str = String.Format(@"(({0}={1}) AND (ISNUMERIC(LTRIM(RTRIM({2})))=1) and (CAST(LTRIM(RTRIM({2})) AS FLOAT) BETWEEN {3} and {4}))",
                                        _fieldNameAttPk, pkAttribute, _fieldNameAttValue, value1.Trim().ToUpper(), value2.Trim().ToUpper());
                                    break;
                            }
                        }
                    }

                    break;
            }
            return str;




        }


        private void btnNextFinish_Click(object sender, EventArgs e)
        {
            if (gvValue.RowCount == 0)
            {
                this.Close();
                return;
            }
            if (!txtFocus.Focused)
            {
                txtFocus.Focus();
            }
            var q1 = Enumerable.Range(0, gvValue.RowCount).Select(p => new
            {
                Type = ProcessGeneral.GetSafeString(gvValue.GetRowCellValue(p, "Type")),
                Code = ProcessGeneral.GetSafeString(gvValue.GetRowCellValue(p, "Code")),
                Name = ProcessGeneral.GetSafeString(gvValue.GetRowCellValue(p, "Name")),
                Expression = ProcessGeneral.GetSafeString(gvValue.GetRowCellValue(p, "Expression")),
                Value1 = ProcessGeneral.GetSafeString(gvValue.GetRowCellValue(p, "Value1")),
                Value2 = ProcessGeneral.GetSafeString(gvValue.GetRowCellValue(p, "Value2")),
                Operator = ProcessGeneral.GetSafeString(gvValue.GetRowCellValue(p, "Operator")),
                PKA = ProcessGeneral.GetSafeInt64(gvValue.GetRowCellValue(p, "PKA")),
                FielName = ProcessGeneral.GetSafeString(gvValue.GetRowCellValue(p, "FielName")),
                IsAttribute = ProcessGeneral.GetSafeBool(gvValue.GetRowCellValue(p, "IsAttribute")),
                RowIndex = p,
            }).ToList();

            var qAtt = q1.Where(p => p.IsAttribute).ToList();
            bool isAtt = false;
            List<String> lOr = new List<string>();
            List<String> lAnd = new List<string>();

            if (qAtt.Any())
            {
                isAtt = true;
                foreach (var iTemAtt in qAtt)
                {
                    int rIndex = iTemAtt.RowIndex;
                    if (rIndex == 0)
                    {
                        lAnd.Add(GetExpressionFinal(iTemAtt.Type, iTemAtt.FielName, iTemAtt.Expression, iTemAtt.Value1,
                            iTemAtt.Value2, iTemAtt.IsAttribute, iTemAtt.PKA));
                    }
                    else
                    {
                        string operBefor = ProcessGeneral.GetSafeString(gvValue.GetRowCellValue(rIndex - 1, "Operator"));
                        if (operBefor.ToUpper() == "OR")
                        {
                            lOr.Add(GetExpressionFinal(iTemAtt.Type, iTemAtt.FielName, iTemAtt.Expression, iTemAtt.Value1,
                                iTemAtt.Value2, iTemAtt.IsAttribute, iTemAtt.PKA));

                        }
                        else
                        {
                            lAnd.Add(GetExpressionFinal(iTemAtt.Type, iTemAtt.FielName, iTemAtt.Expression, iTemAtt.Value1,
                                iTemAtt.Value2, iTemAtt.IsAttribute, iTemAtt.PKA));
                        }
                    }
                }
            }






            string strOr = "";
            if (lOr.Any())
            {
                strOr = string.Join(" OR ", lOr.Distinct());
            }


            var qNotAtt = q1.Where(p => !p.IsAttribute).ToList();
            string strNormal = "";
            if (qNotAtt.Any())
            {
                int cNotAtt = qNotAtt.Max(p => p.RowIndex);
                var qNormal = qNotAtt.Select(p => string.Format("{0} {1}", GetExpressionFinal(p.Type, p.FielName, p.Expression, p.Value1, p.Value2),
                   p.RowIndex == cNotAtt ? "" : p.Operator).Trim()).Distinct().ToList();
                strNormal = string.Join(" ", qNormal);
            }


            var qAnd = lAnd.Distinct().ToList();
            int countAnd = qAnd.Count;
            string strAnd = "";
            if (countAnd > 0)
            {
                strAnd = string.Join(" OR ", qAnd).Trim();
            }


            searchEvent?.Invoke(this, new SearchCodeEventArgs
            {
                StrNormal = strNormal,
                StrAnd = strAnd,
                StrOr = strOr,
                CountAnd = countAnd,
                IsAtt = isAtt
            });
            this.Close();


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        #endregion

        #region "Process Selected Value"

        #region "Init gridview"



        private void GridViewAttributeInit()
        {
            gcAttribute.AllowDrop = true;
            gcAttribute.UseEmbeddedNavigator = true;

            gcAttribute.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcAttribute.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcAttribute.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcAttribute.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcAttribute.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvAttribute.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            //   gvRM.OptionsBehavior.AutoPopulateColumns = false;
            gvAttribute.OptionsBehavior.Editable = false;
            gvAttribute.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvAttribute.OptionsCustomization.AllowColumnMoving = false;
            gvAttribute.OptionsCustomization.AllowQuickHideColumns = true;
            gvAttribute.OptionsCustomization.AllowSort = true;
            gvAttribute.OptionsCustomization.AllowFilter = true;
            gvAttribute.HorzScrollVisibility = ScrollVisibility.Auto;
            gvAttribute.OptionsView.ColumnAutoWidth = false;
            gvAttribute.OptionsCustomization.AllowColumnResizing = true;
            gvAttribute.OptionsView.ShowGroupPanel = false;
            gvAttribute.OptionsView.ShowIndicator = true;
            gvAttribute.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvAttribute.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvAttribute.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvAttribute.OptionsView.ShowAutoFilterRow = true;
            gvAttribute.OptionsView.AllowCellMerge = false;
            // gvRM.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvAttribute.OptionsNavigation.AutoFocusNewRow = true;
            gvAttribute.OptionsNavigation.UseTabKey = true;

            gvAttribute.FocusRectStyle = DrawFocusRectStyle.CellFocus;

            gvAttribute.OptionsSelection.MultiSelect = true;
            gvAttribute.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvAttribute.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvAttribute.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvAttribute.OptionsView.EnableAppearanceEvenRow = false;
            gvAttribute.OptionsView.EnableAppearanceOddRow = false;

            gvAttribute.OptionsView.ShowFooter = false;


            //   gvRM.RowHeight = 25;

            gvAttribute.OptionsFind.AllowFindPanel = true;
            //gvRM.OptionsFind.AlwaysVisible = true;//==>false==>gvRM.OptionsFind.ShowCloseButton = true;
            gvAttribute.OptionsFind.AlwaysVisible = false;
            gvAttribute.OptionsFind.ShowCloseButton = true;
            gvAttribute.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvAttribute)
            {
                IsPerFormEvent = true,
                AllowSort = true,
                IsDrawFilter = true,
                IsBestFitDoubleClick = false
            };

            GridColumn[] arrGridCol = new GridColumn[6];

            #region "Init Column"

            arrGridCol[0] = new GridColumn
            {
                Caption = @"Code",
                FieldName = "Code",
                Name = "Code",
                Visible = true,
                VisibleIndex = 0,
                Fixed = FixedStyle.Left,
                //  ColumnEdit = _chkedit,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Total :", },
            };


            arrGridCol[1] = new GridColumn
            {
                Caption = @"Name",
                FieldName = "Name",
                Name = "Name",
                Visible = true,
                VisibleIndex = 1,
                Fixed = FixedStyle.None,
                //  ColumnEdit = _chkedit,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //  SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Total :", },
            };

            arrGridCol[2] = new GridColumn
            {
                Caption = @"PKA",
                FieldName = "PKA",
                Name = "PKA",
                Visible = true,
                VisibleIndex = 2,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryText,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Total :", },
            };

            arrGridCol[3] = new GridColumn
            {
                Caption = @"FielName",
                FieldName = "FielName",
                Name = "FielName",
                Visible = true,
                VisibleIndex = 3,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryText,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Total :", },
            };

            arrGridCol[4] = new GridColumn
            {
                Caption = @"IsAttribute",
                FieldName = "IsAttribute",
                Name = "IsAttribute",
                Visible = true,
                VisibleIndex = 4,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryText,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Total :", },
            };

            arrGridCol[5] = new GridColumn
            {
                Caption = @"Type",
                FieldName = "Type",
                Name = "Type",
                Visible = true,
                VisibleIndex = 5,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryText,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                OptionsColumn = { AllowSort = DefaultBoolean.True },
                OptionsFilter = { AutoFilterCondition = AutoFilterCondition.Contains },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Total :", },
            };


            #endregion

            gvAttribute.Columns.AddRange(arrGridCol);






            gvAttribute.RowCountChanged += gvAttribute_RowCountChanged;
            gvAttribute.CustomDrawRowIndicator += gvAttribute_CustomDrawRowIndicator;
            gvAttribute.RowStyle += gvAttribute_RowStyle;

            gcAttribute.DataSource = _dtFiled;
            gvAttribute.BestFitColumns();
            ProcessGeneral.HideVisibleColumnsGridView(gvAttribute, false, "PKA", "FielName", "IsAttribute", "Type");
            gcAttribute.ForceInitialize();

        }



        #endregion



        #region "gridview event"



        private void gvAttribute_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsRowSelected(e.RowHandle)) return;
            e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
            e.HighPriority = true;
            e.Appearance.BackColor = GridCellColor.BackColorSelectedRow;
            e.Appearance.BackColor2 = GridCellColor.BackColor2ShowEditor;
            e.Appearance.GradientMode = LinearGradientMode.Horizontal;
        }


        private void gvAttribute_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

            if (ProcessGeneral.GetSafeBool(gv.GetRowCellValue(e.RowHandle, "IsAttribute")))
            {
                Rectangle rect = e.Bounds;
                Brush brush = new LinearGradientBrush(rect, Color.GreenYellow, Color.Azure, 90);
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
            }
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

        private void gvAttribute_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            // if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }



        #endregion







        #region "button click"



        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            DataTable dtS = gcValue.DataSource as DataTable;
            if (dtS == null) return;
            dtS.Rows.Clear();
            dtS.AcceptChanges();
            BestFitColGridValue();



        }


        private void btnRemoveOneRow_Click(object sender, EventArgs e)
        {
            if (gvValue.SelectedRowsCount > 0)
            {
                ProcessGeneral.DeleteSelectedRows(gvValue);
                ((DataTable)gcValue.DataSource).AcceptChanges();
                BestFitColGridValue();
                UpdateOperatorWhenAdd();
            }



        }

        private void btnSelectOneRow_Click(object sender, EventArgs e)
        {
            if (gvAttribute.SelectedRowsCount > 0)
            {
                AddRow(gvAttribute.GetSelectedRows());
            }

        }



        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (gvAttribute.RowCount > 0)
            {
                AddRow(Enumerable.Range(0, gvAttribute.RowCount).ToArray());
            }


        }

        private int GetMinRowAttribute()
        {
            for (int i = 0; i < gvValue.RowCount; i++)
            {
                if (ProcessGeneral.GetSafeBool(gvValue.GetRowCellValue(i, "IsAttribute")))
                    return i;
            }
            return -1;
        }

        private void AddRow(params int[] rows)
        {
            if (rows.Length <= 0) return;

            DataTable dtS = gcValue.DataSource as DataTable;
            if (dtS == null) return;
            dtS.AcceptChanges();

            int minRow = GetMinRowAttribute();
            int beginRow = minRow;

            foreach (int rH in rows)
            {
                bool isAttribute = ProcessGeneral.GetSafeBool(gvAttribute.GetRowCellValue(rH, "IsAttribute"));
                DataRow dr = dtS.NewRow();
                dr["Type"] = ProcessGeneral.GetSafeString(gvAttribute.GetRowCellValue(rH, "Type"));
                dr["Code"] = ProcessGeneral.GetSafeString(gvAttribute.GetRowCellValue(rH, "Code"));
                dr["Name"] = ProcessGeneral.GetSafeString(gvAttribute.GetRowCellValue(rH, "Name"));

                dr["Expression"] = "";
                dr["Value1"] = "";
                dr["Value2"] = "";


                dr["Operator"] = "";


                dr["PKA"] = ProcessGeneral.GetSafeInt64(gvAttribute.GetRowCellValue(rH, "PKA"));

                dr["FielName"] = ProcessGeneral.GetSafeString(gvAttribute.GetRowCellValue(rH, "FielName"));
                dr["IsAttribute"] = isAttribute;

                if (isAttribute)
                {
                    dtS.Rows.Add(dr);

                }
                else
                {
                    if (minRow == -1)
                    {
                        dtS.Rows.Add(dr);
                    }
                    else
                    {
                        dtS.Rows.InsertAt(dr, beginRow);
                        beginRow++;
                    }

                }






            }
            UpdateOperatorWhenAdd();
            dtS.AcceptChanges();


            BestFitColGridValue();
        }




        private void UpdateOperatorWhenAdd()
        {
            for (int i = 0; i < gvValue.RowCount; i++)
            {
                if (i == gvValue.RowCount - 1)
                {
                    gvValue.SetRowCellValue(i, "Operator", "");

                }
                else
                {
                    string oper = ProcessGeneral.GetSafeString(gvValue.GetRowCellValue(i, "Operator"));
                    if (!string.IsNullOrEmpty(oper)) continue;
                    gvValue.SetRowCellValue(i, "Operator", "AND");
                }


            }
        }





        #endregion






        #endregion


        #region "Process GridView"

        private void BestFitColGridValue()
        {
            gvValue.BestFitColumns();

            for (int i = 0; i < gvValue.VisibleColumns.Count; i++)
            {
                GridColumn gCol = gvValue.VisibleColumns[i];
                string fieldName = gCol.FieldName;
                switch (fieldName)
                {
                    case "Value1":
                    case "Value2":
                        gCol.Width += 40;
                        break;
                    default:
                        gCol.Width += 10;
                        break;

                }

            }

        }
        private DataTable TableValueTemp()
        {
            var dt = new DataTable();
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Expression", typeof(string));
            dt.Columns.Add("Value1", typeof(string));
            dt.Columns.Add("Value2", typeof(string));
            dt.Columns.Add("Operator", typeof(string));
            dt.Columns.Add("PKA", typeof(Int64));
            dt.Columns.Add("FielName", typeof(string));
            dt.Columns.Add("IsAttribute", typeof(bool));
            return dt;
        }

        private void GridViewValueCustomInit()
        {


            gcValue.UseEmbeddedNavigator = true;

            gcValue.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcValue.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcValue.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcValue.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcValue.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvValue.OptionsBehavior.Editable = true;
            gvValue.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvValue.OptionsCustomization.AllowColumnMoving = false;
            gvValue.OptionsCustomization.AllowQuickHideColumns = true;
            gvValue.OptionsCustomization.AllowSort = false;
            gvValue.OptionsCustomization.AllowFilter = false;

            //     gvACQ.OptionsHint.ShowCellHints = true;
            gvValue.OptionsView.ColumnAutoWidth = false;
            gvValue.OptionsView.ShowGroupPanel = false;
            gvValue.OptionsView.ShowIndicator = true;
            gvValue.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvValue.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvValue.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvValue.OptionsView.ShowAutoFilterRow = false;
            gvValue.OptionsView.AllowCellMerge = false;
            gvValue.HorzScrollVisibility = ScrollVisibility.Auto;

            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvValue.OptionsNavigation.AutoFocusNewRow = true;
            gvValue.OptionsNavigation.UseTabKey = true;

            gvValue.OptionsSelection.MultiSelect = true;
            gvValue.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvValue.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvValue.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvValue.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvValue.OptionsView.EnableAppearanceEvenRow = false;
            gvValue.OptionsView.EnableAppearanceOddRow = false;

            gvValue.OptionsView.ShowFooter = false;

            gvValue.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            //   gridView1.RowHeight = 25;

            gvValue.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvValue.OptionsFind.AlwaysVisible = false;
            gvValue.OptionsFind.ShowCloseButton = true;
            gvValue.OptionsFind.HighlightFindResults = true;

            gvValue.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new MyFindPanelFilterHelper(gvValue)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsDrawFilter = true,
                IsBestFitDoubleClick = true,
            };

            gvValue.OptionsMenu.EnableFooterMenu = false;



            GridColumn[] arrGridCol = new GridColumn[10];

            #region "Init Column"

            arrGridCol[0] = new GridColumn
            {
                Caption = @"Type",
                FieldName = "Type",
                Name = "Type",
                Visible = true,
                VisibleIndex = 0,
                Fixed = FixedStyle.Left,
                //     ColumnEdit = repositorySpinIndex,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                // SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Total :", },
                // ImageIndex = 0,
                //  ImageAlignment = StringAlignment.Near
            };

            arrGridCol[1] = new GridColumn
            {
                Caption = @"Code",
                FieldName = "Code",
                Name = "Code",
                Visible = true,
                VisibleIndex = 1,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositorySpin0,
                //  DisplayFormat = { FormatString = @"N0", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //   SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };

            arrGridCol[2] = new GridColumn
            {
                Caption = @"Name",
                FieldName = "Name",
                Name = "Name",
                Visible = true,
                VisibleIndex = 2,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
                //ImageIndex = 0,
                //ImageAlignment = StringAlignment.Near
            };

            arrGridCol[3] = new GridColumn
            {
                Caption = @"Expression",
                FieldName = "Expression",
                Name = "Expression",
                Visible = true,
                VisibleIndex = 3,
                Fixed = FixedStyle.None,
                // ColumnEdit = _repositoryText,
                //DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                // SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
                //ImageIndex = 0,
                //ImageAlignment = StringAlignment.Near
            };
            arrGridCol[4] = new GridColumn
            {
                Caption = @"Value 1",
                FieldName = "Value1",
                Name = "Value1",
                Visible = true,
                VisibleIndex = 4,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryCheck,
                //  DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                // SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
                //ImageIndex = 0,
                //ImageAlignment = StringAlignment.Near
            };
            arrGridCol[5] = new GridColumn
            {
                Caption = @"Value 2",
                FieldName = "Value2",
                Name = "Value2",
                Visible = true,
                VisibleIndex = 5,
                Fixed = FixedStyle.None,
                //  ColumnEdit = riMaskedTextEditDateTime,
                //  DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //  SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
                //ImageIndex = 0,
                //ImageAlignment = StringAlignment.Near
            };



            arrGridCol[6] = new GridColumn
            {
                Caption = @"Operator",
                FieldName = "Operator",
                Name = "Operator",
                Visible = true,
                VisibleIndex = 6,
                Fixed = FixedStyle.None,
                //   ColumnEdit = _repositorySpin,
                //  DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem =
                //{
                //    SummaryType = SummaryItemType.Custom,
                //    //DisplayFormat = @"Sum :",

                //},
                //ImageIndex = 0,
                //ImageAlignment = StringAlignment.Near
            };


            arrGridCol[7] = new GridColumn
            {
                Caption = @"PKA",
                FieldName = "PKA",
                Name = "PKA",
                Visible = true,
                VisibleIndex = 7,
                Fixed = FixedStyle.None,
                //ColumnEdit = _repositorySpin,
                //  DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem =
                //{
                //    SummaryType = SummaryItemType.Custom,
                //    //DisplayFormat = @"Sum :",

                //},
                //ImageIndex = 0,
                //ImageAlignment = StringAlignment.Near
            };


            arrGridCol[8] = new GridColumn
            {
                Caption = @"FielName",
                FieldName = "FielName",
                Name = "FielName",
                Visible = true,
                VisibleIndex = 8,
                Fixed = FixedStyle.None,
                //ColumnEdit = _repositorySpin,
                //  DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem =
                //{
                //    SummaryType = SummaryItemType.Custom,
                //    //DisplayFormat = @"Sum :",

                //},
                //ImageIndex = 0,
                //ImageAlignment = StringAlignment.Near
            };

            arrGridCol[9] = new GridColumn
            {
                Caption = @"IsAttribute",
                FieldName = "IsAttribute",
                Name = "IsAttribute",
                Visible = true,
                VisibleIndex = 9,
                Fixed = FixedStyle.None,
                //ColumnEdit = _repositorySpin,
                //  DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem =
                //{
                //    SummaryType = SummaryItemType.Custom,
                //    //DisplayFormat = @"Sum :",

                //},
                //ImageIndex = 0,
                //ImageAlignment = StringAlignment.Near
            };
            #endregion



            gvValue.Columns.AddRange(arrGridCol);









            gvValue.ShowingEditor += gvValue_ShowingEditor;
            gcValue.Paint += gcValue_Paint;

            gvValue.RowCellStyle += gvValue_RowCellStyle;
            gvValue.CellValueChanged += gvValue_CellValueChanged;
            gvValue.LeftCoordChanged += gvValue_LeftCoordChanged;
            gvValue.TopRowChanged += gvValue_TopRowChanged;
            gvValue.MouseMove += gvValue_MouseMove;
            gvValue.FocusedColumnChanged += gvValue_FocusedColumnChanged;
            gvValue.FocusedRowChanged += gvValue_FocusedRowChanged;
            gvValue.RowCountChanged += gvValue_RowCountChanged;
            gvValue.CustomDrawRowIndicator += gvValue_CustomDrawRowIndicator;


            gvValue.CustomRowCellEdit += gvValue_CustomRowCellEdit;


            gcValue.DataSource = TableValueTemp();
            ProcessGeneral.HideVisibleColumnsGridView(gvValue, false, "PKA", "Type", "Code", "FielName", "IsAttribute"
                );




            gcValue.ForceInitialize();
            BestFitColGridValue();
            //Minute_B_Analysis Minute_Aft_Analysis Minute_Factory By_Hand
        }












        #region"GridView Event"
        private void gvValue_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {

            var gv = sender as GridView;
            if (gv == null) return;
            string fieldName = e.Column.FieldName;
            switch (fieldName)
            {

                case "Expression":
                    {
                        string type = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, "Type")).ToLower();
                        switch (type)
                        {
                            case "string":
                                e.RepositoryItem = _reposItemComboString;
                                break;
                            case "number":
                            case "datetime":
                                e.RepositoryItem = _reposItemComboNumberDateTime;
                                break;
                            case "boolean":
                                e.RepositoryItem = _reposItemComboBool;
                                break;
                            default:
                                e.RepositoryItem = _reposItemComboNoType;
                                break;
                        }
                    }

                    break;
                case "Value1":
                case "Value2":
                    {
                        string type = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, "Type")).ToLower();
                        switch (type)
                        {
                            case "string":
                                e.RepositoryItem = _repositoryText;
                                break;
                            case "number":
                                e.RepositoryItem = _repositorySpin;
                                break;
                            case "datetime":
                                e.RepositoryItem = _repositoryTextDateTime;
                                break;
                            case "boolean":
                                e.RepositoryItem = _repositoryCheck;
                                break;
                            default:
                                e.RepositoryItem = _repositoryText;
                                break;
                        }
                    }
                    break;
                case "Operator":
                    e.RepositoryItem = _reposItemComboOperator;
                    break;
            }

        }



        private void gvValue_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            // if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvValue_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

            if (ProcessGeneral.GetSafeBool(gv.GetRowCellValue(e.RowHandle, "IsAttribute")))
            {
                Rectangle rect = e.Bounds;
                Brush brush = new LinearGradientBrush(rect, Color.GreenYellow, Color.Azure, 90);
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
            }
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






        private void gvValue_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            switch (e.Column.FieldName)
            {

                case "Expression":
                    {
                        if (ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, "Expression")).ToLower() != "<-->")
                        {
                            gv.SetRowCellValue(e.RowHandle, "Value2", "");
                        }
                    }
                    break;

            }
        }

        private void gvValue_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            int rH = gv.FocusedRowHandle;
            string fieldName = gv.FocusedColumn.FieldName;
            switch (fieldName)
            {
                case "Expression":
                    e.Cancel = false;
                    break;
                case "Value1":
                    e.Cancel = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "Expression")).ToLower() == "";
                    break;
                case "Value2":
                    e.Cancel = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "Expression")).ToLower() != "<-->";

                    break;
                case "Operator":
                    e.Cancel = gv.FocusedRowHandle == gv.RowCount - 1;

                    break;
                default:
                    e.Cancel = true;
                    break;
            }

        }







        private void gvValue_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (gv.FocusedRowHandle == e.RowHandle && gv.FocusedColumn.FieldName == e.Column.FieldName)
            {

                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellFocused;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }
            if (gv.IsCellSelected(e.RowHandle, e.Column))
            {
                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellSelected;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }


            switch (e.Column.FieldName)
            {

                case "Expression":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                    }
                    break;
                case "Value1":
                    {
                        if (ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, "Expression")).ToLower() == "")
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        else
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                        }

                    }
                    break;
                case "Value2":
                    if (ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, "Expression")).ToLower() != "<-->")
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                    }
                    break;
                case "Operator":
                    if (e.RowHandle == gv.RowCount - 1)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;

                    }


                    break;

                default:
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
            }







        }

        private void gvValue_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvValue_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {

            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvValue_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvValue_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }
        private void gvValue_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gcValue_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            DrawRectangleSelection.PaintGridViewSelectionRect((GridControl)sender, e);
        }
        #endregion











        #endregion


    }
}