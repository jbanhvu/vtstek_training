using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_BaseSys.Properties;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_BaseSys.WForm
{
    public partial class FrmInputAttFinal : DevExpress.XtraEditors.XtraForm
    {

        #region "Property"
        private bool _checkKeyDown = false;
        private readonly DataAttType _dataType;
        private readonly string _strKey;
        private readonly DataTable _dtUnit;
        private readonly RepositoryItemDateEdit _repositoryTextDateTime;
        private readonly RepositoryItemTextEdit _repositoryTextNormal;
        private readonly RepositoryItemSpinEdit _repositorySpin;
        private readonly RepositoryItemCheckEdit _repositoryCheck;

        private readonly RepositoryItemTextEdit _repositoryTextUnit;
        private readonly string _defaultUnitCode;
        public event OnInputValueAttributeHandler OnInputValue = null;
        private readonly string[] _arrUnitName;
        private bool _isValid = true;
        private readonly bool _isAlphabet;
        #endregion

        #region "Contructor"
        public FrmInputAttFinal(DataAttType dataType, string strKey, DataTable dtUnit, string defaultUnitCode, bool isAlphabet)
        {
            InitializeComponent();
            this._defaultUnitCode = defaultUnitCode;
            this._isAlphabet = isAlphabet;
            _repositoryCheck = new RepositoryItemCheckEdit
            {
                AutoHeight = false,
                AllowMouseWheel = false,
                //ValueChecked = "1",
                //ValueUnchecked = "0"
                
            };
            _repositoryTextUnit = new RepositoryItemTextEdit
            {
                AutoHeight = false,
                AllowNullInput = DefaultBoolean.True,
             //   AllowMouseWheel = false,

            };
           
        
            _repositoryTextNormal = new RepositoryItemTextEdit
            {
                AutoHeight = false,
                AllowNullInput = DefaultBoolean.True,
                AllowMouseWheel = false,
                
            };


            _repositoryTextDateTime = new RepositoryItemDateEdit
            {
                AutoHeight = false,
                AllowNullInput = DefaultBoolean.True,
                AllowMouseWheel = false,
                EditMask = ConstSystem.SysDateFormat,
                Mask =
                {
                    MaskType = MaskType.DateTime,
                    UseMaskAsDisplayFormat = true,
                    EditMask =  ConstSystem.SysDateFormat,
                }
            };
            _repositoryTextDateTime.Buttons.Clear();
           

            _repositorySpin = new RepositoryItemSpinEdit
            {
                AutoHeight = false,
                MinValue = decimal.MinValue,
                MaxValue = decimal.MaxValue,
                AllowMouseWheel = false,
               // EditMask = "N3"
            };

            _repositorySpin.Buttons.Clear();
            _arrUnitName = dtUnit.AsEnumerable().Select(p => ProcessGeneral.GetSafeString(p["Description"]))
                .Where(p => !string.IsNullOrEmpty(p)).Distinct().ToArray();
            _dtUnit = dtUnit;
            
            _dataType = dataType;
            _strKey = strKey;

            
        
            GridViewCustomInit();
            this.Load += FrmInputAttFinal_Load;
            btnNextFinish.Click += BtnNextFinish_Click;
     
        }

  

        private void FrmInputAttFinal_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }
        
        protected override void WndProc(ref Message m)
        {
            const UInt32 WM_NCACTIVATE = 0x0086;

            if (m.Msg == WM_NCACTIVATE && m.WParam.ToInt32() == 0)
            {
                this.Close();
            }

            base.WndProc(ref m);
        }



        #endregion


        #region  "Button Click"
        

        private void BtnNextFinish_Click(object sender, EventArgs e)
        {



            if (!btnNextFinish.Focused)
            {
                string fieldName = "";
                GridColumn gCol = gvMain.FocusedColumn;
                if (gCol != null)
                {
                    fieldName = gCol.FieldName;
                }
                btnNextFinish.SelectNextControl(ActiveControl, true, true, true, true);
                btnNextFinish.Focus();

                if (!string.IsNullOrEmpty(fieldName))
                {
                    gcMain.SelectNextControl(ActiveControl, true, true, true, true);
                    gcMain.Focus();
                    gvMain.Focus();
                    ProcessGeneral.SetFocusedCellOnGrid(gvMain, 0, fieldName);
                  
                }
               
            }

            if (!_isValid)
            {
                
            


                if (!gcMain.Focused)
                {
                    gcMain.SelectNextControl(ActiveControl, true, true, true, true);
                    gcMain.Focus();
                    gvMain.Focus();
                    ProcessGeneral.SetFocusedCellOnGrid(gvMain, 0, 1);
                    gvMain.ShowEditor();
                }
                return;
            }

            string unitName = ProcessGeneral.GetSafeString(gvMain.GetRowCellDisplayText(0, "UnitName")).ToUpper();
            
            string unitCode = "";
            if (!string.IsNullOrEmpty(unitName))
            {
                var q1 = _dtUnit.AsEnumerable().Where(p => ProcessGeneral.GetSafeString(p["Description"]).ToUpper() == unitName).Select(p => new
                {
                    Code = ProcessGeneral.GetSafeString(p["Code"]),
                    Description = ProcessGeneral.GetSafeString(p["Description"]),
                }).First();
                if(q1 == null) return;
                unitName = q1.Description;
                unitCode = q1.Code;


            }


            object value = gvMain.GetRowCellValue(0, "Value");

            if (OnInputValue != null)
            {
             
                OnInputValue(this, new OnInputValueAttributeEventArgs
                {
                    Value = value,
                    UnitCode = unitCode,
                    UnitName = unitName
                });
            }
            this.Close();
        }

        #endregion

        #region "Process GridView"

        #region "Methold"

        private void LoadDataGridView()
        {
            DataTable dt = new DataTable();
            switch (_dataType)
            {
                case DataAttType.Boolean:
                    dt.Columns.Add("Value", typeof(bool));
                    break;
                case DataAttType.Datetime:
                    dt.Columns.Add("Value", typeof(DateTime));
                    break;
                case DataAttType.Number:
                    dt.Columns.Add("Value", typeof(float));
                    break;
                case DataAttType.String:
                    dt.Columns.Add("Value", typeof(string));
                    break;
            }
           
            dt.Columns.Add("UnitCode", typeof(string));

            dt.Columns.Add("UnitName", typeof(string));

            DataRow dr = dt.NewRow();



            object value;
            bool isSelectedText = false;

            int lenghKey = _strKey.Length;
            if (lenghKey == 0)
            {
                if (_dataType == DataAttType.Boolean)
                {
                    value = false;
                }
                else if (_dataType == DataAttType.Datetime)
                {
                    value = DateTime.Now;
                }
                else if (_dataType == DataAttType.Number)
                {
                    value = 0;
                }
                else
                {
                    value = "";
                }
            }
            else if (lenghKey == 1)
            {
                if (_dataType == DataAttType.Boolean)
                {
                    value = _strKey== "1";
                }
                else if (_dataType == DataAttType.Datetime)
                {
                    if (ProcessGeneral.IsNumber(_strKey))
                    {
                        int iKey = ProcessGeneral.GetSafeInt(_strKey);
                        int month = DateTime.Now.Month;
                        if (iKey > 3)
                        {
                            value = DateTime.Now;
                            
                        }
                        else if (iKey < 3)
                        {
                    

                            value = new DateTime(DateTime.Now.Year, month, ProcessGeneral.GetSafeInt(string.Format("{0}1", iKey)) );
                            isSelectedText = true;
                        }
                        else
                        {
                            if (month == 2)
                            {
                                value = DateTime.Now;
                            }
                            else
                            {

                                value = new DateTime(DateTime.Now.Year, month, ProcessGeneral.GetSafeInt(string.Format("{0}0", iKey)));
                                isSelectedText = true;
                            }

                        }
                    }
                    else
                    {
                        value = DateTime.Now;
                    }


                }
                else if (_dataType == DataAttType.Number)
                {
                    if (ProcessGeneral.IsNumber(_strKey))
                    {
                        double n1 = ProcessGeneral.GetSafeDouble(_strKey);
                        value = n1;
                        if (n1 > 0)
                            isSelectedText = true;
                    }
                    else
                    {
                        value = 0;
                    }

                }
                else
                {
                    value = _strKey;
                    isSelectedText = true;
                }
            }
            else
            {
                if (_dataType == DataAttType.Boolean)
                {
                    value = _strKey == "1";
                }
                else if (_dataType == DataAttType.Datetime)
                {

                    DateTime dateTime;
                    if (!DateTime.TryParseExact(_strKey, ConstSystem.SysDateFormat, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out dateTime))
                    {
                        dateTime = DateTime.Now;
                      
                    }
                    value = dateTime;

                }
                else if (_dataType == DataAttType.Number)
                {
                    value = ProcessGeneral.IsNumber(_strKey) ? ProcessGeneral.GetSafeDouble(_strKey) : 0;
                }
                else
                {
                    value = _strKey;
                }
            }
            dr["Value"] = value;


            

            dr["UnitCode"] = _defaultUnitCode;

            var q1 = _dtUnit.AsEnumerable().Where(p => p.Field<string>("Code") == _defaultUnitCode).Select(p => ProcessGeneral.GetSafeString(p["Description"])).ToList();
            if (q1.Any())
            {
                dr["UnitName"] = q1.First();
            }
            else
            {
                dr["UnitName"] = "";
            }
    
            dt.Rows.Add(dr);


            gcMain.DataSource = dt;
            gvMain.BestFitColumns();
            //foreach (GridColumn gCol in gvMain.Columns.Where(p => p.VisibleIndex >= 0))
            //{
            //    gCol.Width += 5;
            //}

            gvMain.Columns["Value"].Width = 90;
            gvMain.Columns["UnitName"].Width = 90;

          //  gcMain.SelectNextControl(ActiveControl, true, true, true, true);
            gcMain.Focus();
            gvMain.Focus();
            ProcessGeneral.SetFocusedCellOnGrid(gvMain, 0, 0);
            gvMain.ShowEditor();

            TextEdit editor = gvMain.ActiveEditor as TextEdit;
            if (editor != null)
            {
                if (!_isAlphabet)
                {
                    editor.SelectAll();
                }
                else if (isSelectedText)
                {

                    if (_dataType == DataAttType.Datetime)
                    {
                        editor.Select(1, 1);
                    }
                    else if (_dataType == DataAttType.Number)
                    {
                        editor.Select(editor.Text.Length - 1, 0);
                    }
                    else
                    {
                        editor.Select(editor.Text.Length, 0);
                    }

                }
               
              
            }
   

        }
     


        private void GridViewCustomInit()
        {


            gcMain.UseEmbeddedNavigator = false;

            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;

            gvMain.OptionsView.ShowColumnHeaders = false;
            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvMain.OptionsBehavior.Editable = true;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvMain.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;

            gvMain.OptionsCustomization.AllowSort = false;

            gvMain.OptionsCustomization.AllowFilter = false;


            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = false;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.HorzScrollVisibility = ScrollVisibility.Never;
            gvMain.VertScrollVisibility = ScrollVisibility.Never;
            gvMain.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;

            gvMain.OptionsSelection.MultiSelect = true;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvMain.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;
            gvMain.OptionsView.ShowFooter = false;

            gvMain.OptionsHint.ShowFooterHints = false;
            gvMain.OptionsHint.ShowCellHints = false;
            //   gridView1.RowHeight = 25;

            gvMain.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gvMain.OptionsFind.AllowFindPanel = false;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            //gvMain.OptionsFind.AlwaysVisible = false;
            //gvMain.OptionsFind.ShowCloseButton = true;
            //gvMain.OptionsFind.HighlightFindResults = true;

            gvMain.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);

            new MyFindPanelFilterHelper(gvMain)
            {
                AllowSort = false,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };


            GridColumn[] arrGridCol = new GridColumn[3];

            #region "Init Column"



            arrGridCol[0] = new GridColumn
            {
                Caption = @"Value",
                FieldName = "Value",
                Name = "Value",
                Visible = true,
                VisibleIndex = 0,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositorySpin0,
                //  DisplayFormat = { FormatString = @"N0", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //  SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };

            arrGridCol[1] = new GridColumn
            {
                Caption = @"UnitCode",
                FieldName = "UnitCode",
                Name = "UnitCode",
                Visible = true,
                VisibleIndex = 1,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
                //ImageIndex = 0,
                //ImageAlignment = StringAlignment.Near
            };
            arrGridCol[2] = new GridColumn
            {
                Caption = @"UnitName",
                FieldName = "UnitName",
                Name = "UnitName",
                Visible = true,
                VisibleIndex = 2,
                Fixed = FixedStyle.None,
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
                //ImageIndex = 0,
                //ImageAlignment = StringAlignment.Near
            };
           
            #endregion



            gvMain.Columns.AddRange(arrGridCol);

            ProcessGeneral.HideVisibleColumnsGridView(gvMain, false, "UnitCode");
    

          

            gvMain.RowCellStyle += gvMain_RowCellStyle;
          
            gvMain.LeftCoordChanged += gvMain_LeftCoordChanged;
            gvMain.MouseMove += gvMain_MouseMove;
            gvMain.TopRowChanged += gvMain_TopRowChanged;
            gvMain.FocusedColumnChanged += gvMain_FocusedColumnChanged;
            gvMain.FocusedRowChanged += gvMain_FocusedRowChanged;
            gcMain.Paint += gcMain_Paint;
            gcMain.EditorKeyDown += gcMain_EditorKeyDown;
            gcMain.KeyDown += gcMain_KeyDown;

            gvMain.ShowingEditor += GvMain_ShowingEditor;
            gvMain.ShownEditor += GvMain_ShownEditor;

            gvMain.DoubleClick += GvMain_DoubleClick;

            gvMain.CustomRowCellEdit += GvMain_CustomRowCellEdit;
            gvMain.ValidatingEditor += GvMain_ValidatingEditor;
            gvMain.InvalidValueException += GvMain_InvalidValueException;
            gcMain.ForceInitialize();



        }













        #endregion

        #region "GridView Event"
        private void GvMain_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
        }

       
     
        private void GvMain_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null)
            {
                _isValid = true;
                return;
            }
            GridColumn gCol = gv.FocusedColumn;
            if (gCol == null)
            {
                _isValid = true;
                return;
            }
            string fieldName = gCol.FieldName;
            if (fieldName == "Value")
            {
                _isValid = true;
                return;
            }
            string unitTest = ProcessGeneral.GetSafeString(e.Value).ToUpper();
            if (string.IsNullOrEmpty(unitTest))
            {
                _isValid = true;
                return;
            }
            if (_arrUnitName.Length <= 0)
            {
                _isValid = true;
                return;
            }

            if (!_arrUnitName.Any(p => p.Equals(unitTest, StringComparison.OrdinalIgnoreCase)))
            {
                _isValid = false;
                e.Valid = false;
                e.ErrorText = "Unit Invalid";

            }
            else
            {
                _isValid = true;
                e.Valid = true;
                e.ErrorText = "";
            }

           
        }
        private void GvMain_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GridColumn gCol = gv.FocusedColumn;
            if (gCol == null) return;
            string fieldName = gCol.FieldName;
            switch (fieldName)
            {
                case "Value":
                    e.Cancel = false;
                    break;
                default:
                    e.Cancel = _arrUnitName.Length <= 0;
                    break;
            }
        }
        private void GvMain_ShownEditor(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GridColumn gCol = gv.FocusedColumn;
            if (gCol == null) return;
            string fieldName = gCol.FieldName;
            if (fieldName == "Value") return;
            if(_arrUnitName.Length <= 0) return;
            TextEdit currentEditor = gv.ActiveEditor as TextEdit;
            if (currentEditor != null)
            {
                currentEditor.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                currentEditor.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;


                var source = new AutoCompleteStringCollection();
                source.AddRange(_arrUnitName);

                currentEditor.MaskBox.AutoCompleteCustomSource = source;
            }
        }

        private void GvMain_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {

            var gv = sender as GridView;
            if (gv == null) return;
            string fieldName = e.Column.FieldName;

            if (fieldName == "Value")
            {

                switch (_dataType)
                {
                    case DataAttType.Boolean:
                        e.RepositoryItem = _repositoryCheck;
                        break;
                    case DataAttType.Datetime:
                        e.RepositoryItem = _repositoryTextDateTime;
                        break;
                    case DataAttType.Number:
                        e.RepositoryItem = _repositorySpin;
                        break;
                    case DataAttType.String:
                        e.RepositoryItem = _repositoryTextNormal;
                        break;
                }
               
            }
            else
            {
                e.RepositoryItem = _repositoryTextUnit;
            }
        }
        private void GvMain_DoubleClick(object sender, EventArgs e)
        {
            BtnNextFinish_Click(null, null);
        }


     
      

        private void gvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
                case "Value":
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                    break;
                default:
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor =  Color.Honeydew; ;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                    break;
            }

        }

        private void gvMain_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvMain_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvMain_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvMain_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            var gv = sender as GridView;
            DrawRectangleSelection.RePaintGridView(gv);

        }
        private void gvMain_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var gv = sender as GridView;
            DrawRectangleSelection.RePaintGridView(gv);

        }

        private void gcMain_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            DrawRectangleSelection.PaintGridViewSelectionRect((GridControl)sender, e);
        }

        private void gcMain_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (!_checkKeyDown)
            {
                gcMain_KeyDown(sender, e);
            }
            _checkKeyDown = false;
        }

        private void gcMain_KeyDown(object sender, KeyEventArgs e)
        {
            var gc = sender as GridControl;
            if (gc == null) return;
            var gv = gc.FocusedView as GridView;
            if (gv == null) return;
            string fieldName = gv.FocusedColumn.FieldName;
         //   int rH = gv.FocusedRowHandle;
            _checkKeyDown = true;



            
            #region"F4 Key"
           

          
            if (e.KeyCode == Keys.Tab)
            {
                if (fieldName == "UnitName")
                {
                    btnNextFinish.Focus();
                }
                return;


            }
            #endregion

            if (fieldName == "Value" && _arrUnitName.Length > 0 &&  (_dataType == DataAttType.Number || _dataType == DataAttType.Datetime ||
                                         _dataType == DataAttType.Boolean) )
            {

                string letter = e.KeyCode.CheckIsAlphabeStr();
                if (string.IsNullOrEmpty(letter)) return;

                var qUnit = _arrUnitName.Where(p => p.StartsWith(letter, StringComparison.OrdinalIgnoreCase)).ToArray();
                int cUnit = qUnit.Length;
                bool isSelectText = cUnit > 0;
                // //unitName.Length <= 1 && 
                bool isSetValue = false;
                string unitName = ProcessGeneral.GetSafeString(gvMain.GetRowCellValue(0, "UnitName"));
                if (isSelectText )
                {
                    string unitSet = qUnit[0];
                    int lengthUnitSet = unitSet.Length;
                    if (unitName.Length <= 1 || !unitName.StartsWith(letter, StringComparison.OrdinalIgnoreCase))
                    {
                        if (cUnit == 1 && lengthUnitSet > 1)
                        {
                            gvMain.SetRowCellValue(0, "UnitName", unitSet);
                        }
                        else
                        {
                            gvMain.SetRowCellValue(0, "UnitName", letter);
                            isSetValue = true;
                        }
                      
                    }
                   
                }


              // 




                ProcessGeneral.SetFocusedCellOnGrid(gvMain, 0, "UnitName");
                gvMain.ShowEditor();
                if (isSelectText)
                {
                    TextEdit editor = gvMain.ActiveEditor as TextEdit;
                    if (editor == null) return;
                    string editText = editor.Text;
                    int editLength = editText.Length;
                    if (isSetValue)
                    {
                        editor.Select(editLength, 0);
                    }
                    else
                    {
                        // 12345
                        editor.Select(1, editLength - 1);
                    }
               
                }
           
              
            }
        }

       



        #endregion





        #endregion

        #region "hotkey"
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Escape:
                {
                    this.Close();
                    return true;
                }
                case Keys.Enter:
                {
                    BtnNextFinish_Click(null, null);
                    return true;
                }


            }
            return base.ProcessCmdKey(ref message, keys);
        }

        #endregion

    }
}
