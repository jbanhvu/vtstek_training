using CNY_BaseSys.Class;
using CNY_BaseSys.Interfaces;
using CNY_BaseSys.UControl;
using CNY_BaseSys.WForm;
using DevExpress.Data;
using DevExpress.Office;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.ViewInfo;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.Spreadsheet.Export;
using DevExpress.XtraBars;
using GridFixedStyle = DevExpress.XtraGrid.Columns.FixedStyle;
using TreeFixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;
using Worksheet = DevExpress.Spreadsheet.Worksheet;
using Application = System.Windows.Forms.Application;
using Button = System.Windows.Forms.Button;
using DataTable = System.Data.DataTable;
using Label = System.Windows.Forms.Label;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
using TextBox = System.Windows.Forms.TextBox;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Layout;

namespace CNY_BaseSys.Common
{
    public class PasteDataGridStd
    {
        public Int32 GridRow { get; set; }
        public DataRow Row { get; set; }
    }
    public class PasteFuncCheckInfo
    {
        public string FieldName { get; set; }
        public string FuncName { get; set; }
        public bool IsCheckFunction { get; set; }
        public bool PasteOnMatchType { get; set; }
        public List<object> LstParameter { get; set; }
        /*
             public Form1()
        {
            InitializeComponent();
            gvMain.OptionsBehavior.Editable = false;
            gvMain.OptionsSelection.MultiSelect = true;
            gvMain.OptionsSelection.MultiSelectMode=GridMultiSelectMode.CellSelect;var dt = new DataTable();
            dt.Columns.Add("Col1", typeof (string));
            dt.Columns.Add("Col2", typeof(string));
            dt.Columns.Add("Col3", typeof(bool));

            dt.Rows.Add("Row Col 1 - 1", "Row Col 2 - 1", true);
            dt.Rows.Add("Row Col 1 - 2", "Row Col 2 - 2", true);
            dt.Rows.Add("Row Col 1 - 3", "Row Col 2 - 3", false);
            dt.Rows.Add("Row Col 1 - 4", "Row Col 2 - 4", false);
            gcMain.DataSource = dt;
            gcMain.ProcessGridKey += new KeyEventHandler(gcMain_ProcessGridKey);
        }

        void gcMain_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Dictionary<PasteFuncCheckInfo, Func<GridView, int, List<object>, bool>> methodsDict = new Dictionary
                    <PasteFuncCheckInfo, Func<GridView, int, List<object>, bool>>
                {
                    {
                        new PasteFuncCheckInfo
                        {
                            FieldName = "Col1",
                            FuncName = "CheckPasteCol1",
                            IsCheckFunction = true,
                            PasteOnMatchType = true,
                            LstParameter = new List<object>(),
                        },
                        CheckPasteCol1
                    },
                    {
                        new PasteFuncCheckInfo
                        {
                            FieldName = "Col2",
                            FuncName = "CheckPasteCol2",
                            IsCheckFunction = true,
                            PasteOnMatchType = true,
                            LstParameter = new List<object>(),
                        },
                        CheckPasteCol2
                    }
                };
                ProcessGeneral.PasteDataStandardOnGridView(gvMain, methodsDict);
            }
        }

        private bool CheckPasteCol1(GridView gv, int rH, List<object> para)
        {
            bool check = (bool)gv.GetRowCellValue(rH, "Col3");
            return check;
        }

        private bool CheckPasteCol2(GridView gv, int rH, List<object> para)
        {
            bool check = (bool)gv.GetRowCellValue(rH, "Col3");
            return !check;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Methods met = new Methods();
            var dic = met.MethodsDict;
            foreach (var item in dic)
            {
                string a = met.Execute(item.Key, new string[] { "1", "2","3" });
                MessageBox.Show(a);}
        }
         */
    }
    public class CtrlDOrDelFuncCheckInfo
    {
        public string FieldName { get; set; }
        public string FuncName { get; set; }
        public bool IsCheckFunction { get; set; }
        public List<object> LstParameter { get; set; }
    }




    public class TreeListSortIndexAfterDrop
    {
        public TreeListNode Node { get; set; }
        public Int32 RowIndex { get; set; }
        public Int32 VisibleIndex { get; set; }
        public Int32 IsDropNode { get; set; }
    }

    public class AttValueReturnInfo
    {
        public object Value { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public bool IsSave { get; set; }
    }
    public class MemoValueReturnInfo
    {
        public string Value { get; set; }
        public bool IsSave { get; set; }
    }


    public class RichTextValueReturnInfo
    {
        public string Value { get; set; }
        public bool IsSave { get; set; }

        public string ValueHtml { get; set; }

        public string ValueRtf { get; set; }
    }

    public class PermissionFormInfo
    {
        public bool PerIns { get; set; }
        public bool PerUpd { get; set; }
        public bool PerDel { get; set; }
        public bool PerViw { get; set; }
        public string StrAdvanceFunction { get; set; }
        public string StrSpecialFunction { get; set; }
        public bool PerCheckAdvanceFunction { get; set; }

        public DataTable DtSpecialFunction { get; set; }

        public DataTable DtAdvanceFunc { get; set; }
        public string FormName { get; set; }
        public string MenuCode { get; set; }
    }




    public static class ProcessGeneral
    {   /// <summary>
        /// Load Search Lookup With Tooltip Error Event Mouse Move
        /// </summary>
        /// <param name="searchLookup"></param>
        /// <param name="dtpara"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        /// <param name="textError"></param>
        /// <param name="titleError"></param>
        /// <param name="bfm"></param>
        /// <param name="columnAutoWidth"></param>
        /// <param name="arrColumnHide"></param>
        public static void LoadSearchLookupWithTooltipError(SearchLookUpEdit searchLookup, DataTable dtpara, string displayMember, string valueMember, string textError, string titleError, BestFitMode bfm, bool columnAutoWidth, params string[] arrColumnHide)
        {
            searchLookup.Properties.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            searchLookup.Properties.View.OptionsSelection.EnableAppearanceFocusedRow = true;
            searchLookup.Properties.View.OptionsSelection.EnableAppearanceFocusedCell = true;
            searchLookup.Properties.View.OptionsView.EnableAppearanceEvenRow = true;
            searchLookup.Properties.View.OptionsView.EnableAppearanceOddRow = true;
            searchLookup.Properties.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            searchLookup.Properties.View.OptionsView.ShowAutoFilterRow = true;
            searchLookup.Properties.View.OptionsView.ColumnAutoWidth = columnAutoWidth;
            searchLookup.Properties.View.BestFitMaxRowCount = dtpara.Rows.Count;
            searchLookup.Properties.View.OptionsFind.AllowFindPanel = true;
            searchLookup.Properties.View.OptionsFind.AlwaysVisible = true;
            searchLookup.Properties.View.OptionsFind.HighlightFindResults = true;

            searchLookup.Properties.BestFitMode = bfm;

            new MyFindPanelFilterHelper(searchLookup.Properties.View)
            {
                IsPerFormEvent = true,
            };
            searchLookup.Properties.ShowClearButton = false;

            searchLookup.Properties.TextEditStyle = TextEditStyles.Standard;

            searchLookup.Properties.DataSource = dtpara;
            searchLookup.Properties.DisplayMember = displayMember;
            searchLookup.Properties.ValueMember = valueMember;
            searchLookup.Properties.PopulateViewColumns();

            if (arrColumnHide.Length > 0)
            {
                foreach (string s in arrColumnHide)
                {
                    searchLookup.Properties.View.Columns[s].Visible = false;
                }

            }

            SetTooltipControl(searchLookup, textError, titleError, GetImageList(), 0, new Size(16, 16), DefaultBoolean.True, true, true);

            searchLookup.KeyDown += (s, e) =>
            {
                SearchLookUpEdit searchLookUpEdit = s as SearchLookUpEdit;
                if (searchLookUpEdit == null) return;

                if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control && !string.IsNullOrEmpty(searchLookUpEdit.Text))
                    Clipboard.SetText(searchLookUpEdit.Text);
            };

            searchLookup.MouseMove += (s, e) =>
            {
                SearchLookUpEdit searchLookUpEdit = s as SearchLookUpEdit;
                if (searchLookUpEdit == null) return;
                if (ProcessGeneral.GetSafeString(searchLookUpEdit.EditValue) == string.Empty)
                {
                    ToolTipController.DefaultController.ShowHint(
                        ToolTipController.DefaultController.GetToolTip(searchLookUpEdit),
                        ToolTipController.DefaultController.GetTitle(searchLookUpEdit),
                        searchLookUpEdit.PointToScreen(e.Location));
                }
                else
                {
                    ToolTipController.DefaultController.HideHint();
                }
            };
        }
        public static bool IsCreatedDiffBoMOnProduct(string connectString)
        {
            string sql = "SELECT CNY004 FROM  dbo.CNYPA001 WHERE CNY001='BM001'";
            AccessData accData = new AccessData(connectString);
            DataTable dt = accData.TblReadDataSQL(sql, null);
            if (dt.Rows.Count <= 0) return false;
            return GetSafeInt(dt.Rows[0]["CNY004"]) == 1;
        }

        public static RepositoryItemSearchLookUpEdit CreateRepositoryItemSearchLookUpEdit(DataTable dtpara, string displayMember, string valueMember, DevExpress.XtraEditors.Controls.BestFitMode bfm = DevExpress.XtraEditors.Controls.BestFitMode.None,
            params string[] arrColumnHide)
        {
            RepositoryItemSearchLookUpEdit searchLookup = new RepositoryItemSearchLookUpEdit();
            searchLookup.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            searchLookup.View.OptionsSelection.EnableAppearanceFocusedRow = true;
            searchLookup.View.OptionsSelection.EnableAppearanceFocusedCell = true;
            searchLookup.View.OptionsView.EnableAppearanceEvenRow = true;
            searchLookup.View.OptionsView.EnableAppearanceOddRow = true;
            searchLookup.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            searchLookup.View.OptionsView.ShowAutoFilterRow = true;
            searchLookup.View.OptionsFind.AllowFindPanel = true;
            searchLookup.View.OptionsFind.AlwaysVisible = true;
            searchLookup.View.OptionsFind.HighlightFindResults = true;



            searchLookup.BestFitMode = bfm;



            new MyFindPanelFilterHelper(searchLookup.View)
            {
                IsPerFormEvent = true,
            };
            searchLookup.ShowClearButton = false;



            searchLookup.TextEditStyle = TextEditStyles.Standard;



            searchLookup.DataSource = dtpara;
            searchLookup.DisplayMember = displayMember;
            searchLookup.ValueMember = valueMember;
            searchLookup.PopulateViewColumns();



            if (arrColumnHide.Length > 0)
            {
                foreach (string s in arrColumnHide)
                {
                    searchLookup.View.Columns[s].Visible = false;
                }



            }



            searchLookup.KeyDown += (s, e) =>
            {
                SearchLookUpEdit searchLookUpEdit = s as SearchLookUpEdit;
                if (searchLookUpEdit == null) return;



                if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control && !string.IsNullOrEmpty(searchLookUpEdit.Text))
                    Clipboard.SetText(searchLookUpEdit.Text);
            };



            return searchLookup;
        }
        public static object GetValueFirstRowByFieldName(DataTable dtS, string fieldName)
        {
            if (dtS != null && dtS.Rows.Count > 0)
                return dtS.Rows[0][fieldName];
            return null;
        }

        public static List<int> GetAllGroupRowHandle(this GridView gv)
        {
            List<int> l = new List<int>();
            int rHg = 0;
            while (true)
            {
                if (rHg >= gv.RowCount) break;
                int rowHandle = gv.GetVisibleRowHandle(rHg);
                bool isGroup = gv.IsGroupRow(rowHandle);
                if (isGroup)
                {
                    l.Add(rowHandle);
                    int childCount = gv.GetChildRowCount(rowHandle);
                    

                    if (gv.GetRowExpanded(rowHandle))
                    {
                        rHg = childCount + rHg + 1;
                    }
                    else
                    {
                        rHg++;
                    }
                }
                else
                {
                    rHg++;
                }
            }
            return l;
        }
        public static List<int> GetAllRowHandleAllGroup(this GridView gv)
        {
            List<int> lChild = new List<int>();
            int rHg = 0;
            while (true)
            {
                if (rHg >= gv.RowCount) break;
                int rowHandle = gv.GetVisibleRowHandle(rHg);
                bool isGroup = gv.IsGroupRow(rowHandle);
                if (isGroup)
                {
                    int childCount = gv.GetChildRowCount(rowHandle);
                    for (int i = 0; i < childCount; i++)
                    {
                        int cRh = gv.GetChildRowHandle(rowHandle, i);
                        lChild.Add(cRh);
                    }

                    if (gv.GetRowExpanded(rowHandle))
                    {
                        rHg = childCount + rHg + 1;
                    }
                    else
                    {
                        rHg++;
                    }
                }
                else
                {
                    rHg++;
                }
            }
            return lChild;
        }

        public static DataRow GetDataRowByEditValueKey(GridLookUpEdit gridLookup)
        {
            PropertyInfo property = gridLookup.Properties.GetType().GetProperty("Controller", BindingFlags.Instance | BindingFlags.NonPublic);
            BaseGridController controller = (BaseGridController)property.GetValue(gridLookup.Properties, null);
            int index = controller.FindRowByValue(gridLookup.Properties.ValueMember, gridLookup.EditValue);
            if (index < 0)
                return null;
            DataRowView drView = controller.GetRow(index) as DataRowView;
            if (drView != null)
                return drView.Row;
            return null;
        }

        public static RepositoryItemGridLookUpEdit CreateRepositoryItemGridLookUpEdit(DataTable dtpara, string displayMember, string valueMember, DevExpress.XtraEditors.Controls.BestFitMode bfm = DevExpress.XtraEditors.Controls.BestFitMode.None,
    params string[] arrColumnHide)
        {
            RepositoryItemGridLookUpEdit gridLookup = new RepositoryItemGridLookUpEdit();
            gridLookup.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gridLookup.View.OptionsSelection.EnableAppearanceFocusedRow = true;
            gridLookup.View.OptionsSelection.EnableAppearanceFocusedCell = true;
            gridLookup.View.OptionsView.EnableAppearanceEvenRow = true;
            gridLookup.View.OptionsView.EnableAppearanceOddRow = true;
            gridLookup.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridLookup.View.OptionsView.ShowAutoFilterRow = true;
            gridLookup.View.OptionsFind.AllowFindPanel = true;
            gridLookup.View.OptionsFind.AlwaysVisible = true;
            gridLookup.View.OptionsFind.HighlightFindResults = true;
            gridLookup.View.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //gridLookup.SearchMode = GridLookUpSearchMode.AutoSuggest;
            gridLookup.AutoComplete = true;
            gridLookup.BestFitMode = bfm;



            gridLookup.DataSource = dtpara;
            gridLookup.DisplayMember = displayMember;
            gridLookup.ValueMember = valueMember;
            gridLookup.PopulateViewColumns();



            if (arrColumnHide.Length > 0)
            {
                foreach (string s in arrColumnHide)
                {
                    gridLookup.View.Columns[s].Visible = false;
                }



            }



            gridLookup.KeyDown += (s, e) =>
            {
                GridLookUpEdit gridLkEdit = s as GridLookUpEdit;
                if (gridLkEdit == null) return;
                gridLkEdit.KeyDown += (s1, e1) =>
                {
                    if (e1.KeyCode == Keys.C && e1.Modifiers == Keys.Control && !string.IsNullOrEmpty(gridLkEdit.Text))
                        Clipboard.SetText(gridLkEdit.Text);
                };
            };



            return gridLookup;
        }
        public static DataTable GetTableOnTreeList(this TreeList tl, params string [] arrColDispText)
        {
            DataTable dtTree = tl.DataSource as DataTable;
            if (dtTree == null)
                return null;
            DataTable dt = dtTree.Clone();
            if (dtTree.Rows.Count > 0)
            {

                Dictionary<string, bool> lCol = dtTree.Columns.Cast<DataColumn>().Select(p => new
                {
                    p.ColumnName,
                    IsDispText = arrColDispText.Contains(p.ColumnName)
                }).ToDictionary(p => p.ColumnName, p => p.IsDispText);
                AddRowTableFromTree(tl, ref dt, lCol);
            }
         
            return dt;
        }
        private static void AddRowTableFromTree(TreeList tl, ref DataTable dt, Dictionary<string, bool> lCol)
        {
            foreach (TreeListNode node in tl.Nodes)
            {
                DataRow dr = dt.NewRow();
                foreach (var item in lCol)
                {
                    if (item.Value)
                    {
                        dr[item.Key] = node.GetDisplayText(item.Key);
                    }
                    else
                    {
                        dr[item.Key] = node.GetValue(item.Key);
                    }
                
                }
                dt.Rows.Add(dr);
                if (node.Nodes.Count > 0)
                {
                    AddRowTableFromNode(node, ref dt, lCol);
                }

            }
        }

        private static void AddRowTableFromNode(TreeListNode node, ref DataTable dt, Dictionary<string, bool> lCol)
        {
            foreach (TreeListNode childNode in node.Nodes)
            {
                DataRow dr = dt.NewRow();
                foreach (var item in lCol)
                {
                    if (item.Value)
                    {
                        dr[item.Key] = childNode.GetDisplayText(item.Key);
                    }
                    else
                    {
                        dr[item.Key] = childNode.GetValue(item.Key);
                    }

                }
                dt.Rows.Add(dr);
                if (childNode.Nodes.Count > 0)
                {
                    AddRowTableFromNode(childNode, ref dt, lCol);
                }

            }
        }
        public static string RepeatString(this string s, int n)
        {
            return String.Concat(Enumerable.Repeat(s, n));
        }
        public static ActionDialog ShowYesNoDialogForm(string formCaption, string commandPromt, int btnFocused)
        {
            ActionDialog actionDialog = ActionDialog.None;
            FrmCustomDialog f = new FrmCustomDialog
            {
                Text = formCaption,
                CommandPromt = commandPromt,
                Tag = btnFocused

            };
            f.OnGetValue += (s, e) =>
            {
                actionDialog = e.DialogResult;
            };
            f.ShowDialog();
            return actionDialog;
        }
        private static readonly Int64[] iMangTang = { 0, 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 };
        public static decimal RoundedNumber(decimal NumInput, Int64 NumTang, Int32 roundedRules, Int32 roundedType)
        {
            decimal result = 0;

            switch (roundedType)
            {
                case 0: // làm tròn số thập phân theo hệ thống
                    result = Math.Round(NumInput, (Int32)NumTang);
                    break;
                case 1: // làm tròn theo bội số chục, trăm, ngàn,...
                    
                    Int64 LenNumTang = NumTang.ToString().Length;
                    Int64 divisor = iMangTang[LenNumTang];
                    Int64 currentNo = Convert.ToInt64((Int64)NumInput / divisor * divisor);
                    Int64 remainder = Convert.ToInt64((Int64)NumInput % divisor);
                    Int64 iNumTang = NumTang;
                    if (remainder > 0)
                    {
                        if (roundedRules == 2) // giảm
                            iNumTang = NumTang - divisor;
                    }
                    if (roundedRules == 2) // giảm
                    {
                        result = currentNo - iNumTang;

                    }
                    else
                    {
                        result = currentNo + iNumTang;
                    }

                    break;
            }

            return result;
        }

        public static string[] ArrCharacterVNese = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};

        public static bool CheckVNStringInput(this string s)
        {
            //string arrStr = s.
            //Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            //string temp = s.Normalize(NormalizationForm.FormD);
            //return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');



            return s.ToCharArray().Any(p => ArrCharacterVNese.Contains(p.ToString()));

      
        }
        public static string ConvertVNToNormalStr(this string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static Dictionary<string, Decimal> CalculateSumColumnsOnTable(this DataTable dt, params string[] arrCol)
        {
            Dictionary<string, Decimal> dicSum = new Dictionary<string, Decimal>();
            if (arrCol.Length <= 0) return dicSum;
            foreach (string col in arrCol)
            {
                decimal value = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    value += ProcessGeneral.GetSafeDecimal(dr[col]);
                }
                dicSum.Add(col, value);
            }

            return dicSum;
        }
        public static void SelectedNextCell(this GridView gv, int rH, int currentVisibleIndex)
        {
            if (gv.RowCount <= 0) return;
            if(rH == GridControl.InvalidRowHandle) return;
            if (gv.IsGroupRow(rH))
            {
                if (!gv.GetRowExpanded(rH))
                {
                    int rHN = rH - 1;
                    if(!gv.IsGroupRow(rHN)) return;

                    gv.ClearSelection();
                    gv.FocusedRowHandle = rHN;
                    gv.BeginSelection();
                    gv.SelectRow(rHN);
                    gv.EndSelection();


                    return;

                }


                GridColumn nextColumn = gv.VisibleColumns[0];
                if (nextColumn == null) return;
                int nextRow = gv.GetChildRowHandle(rH, 0);
                if (nextRow >= gv.DataRowCount) return;
                SetFocusedCellOnGridEnter(gv, nextRow, nextColumn.FieldName);
                return;
            }

            if (rH < 0) return;
            GridColumn tCol = gv.VisibleColumns[currentVisibleIndex + 1];


            if (tCol != null)
            {
                SetFocusedCellOnGridEnter(gv, rH, tCol.FieldName);
            }
            else
            {
                int nextRow;
                if (rH == GridControl.AutoFilterRowHandle)
                {
                    nextRow = 0;
                }
                else
                {
                    nextRow = rH + 1;
                    if (nextRow >= gv.DataRowCount) return;
                }

                GridColumn nextColumn = gv.VisibleColumns[0];
                if (nextColumn == null) return;
                SetFocusedCellOnGridEnter(gv, nextRow, nextColumn.FieldName);
            }

        }

        private static void SetFocusedCellOnGridEnter(GridView gv, int rH, string fieldName)
        {
            if (!gv.GridControl.Focused)
            {
                gv.Focus();
            }

            gv.ClearSelection();
            gv.FocusedRowHandle = rH;
            gv.FocusedColumn = gv.Columns[fieldName];
            gv.BeginSelection();
            gv.SelectCell(rH, gv.Columns[fieldName]);
            gv.EndSelection();


            while (gv.GetSelectedCells().Length > 1)
            {
                gv.BeginUpdate();
                gv.BeginSelection();
                gv.ClearSelection();
                gv.SelectCell(rH, gv.Columns[fieldName]);
                gv.EndSelection();
                gv.EndUpdate();
            }


        }



        public static void SelectedNextCell(this TreeList tl, TreeListNode node, int currentVisibleIndex)
        {
            if (node == null) return;
            TreeListColumn tCol = tl.VisibleColumns[currentVisibleIndex + 1];
            if (tCol != null)
            {
                SetFocusedCellOnTreeEnter(tl, node, tCol.FieldName);
            }
            else
            {
                int currentIndex = tl.GetVisibleIndexByNode(node);
                TreeListNode nextNode = tl.GetNodeByVisibleIndex(currentIndex + 1);
                // TreeListNode nextNode = node.NextNode;
                if (nextNode == null) return;
                TreeListColumn nextColumn = tl.VisibleColumns[0];
                if (nextColumn == null) return;
                SetFocusedCellOnTreeEnter(tl, nextNode, nextColumn.FieldName);
            }

        }
        private static void SetFocusedCellOnTreeEnter(TreeList tl, TreeListNode node, string fieldName)
        {
            if (!tl.Focused)
            {
                tl.Focus();
            }

            tl.Selection.Clear();
            tl.FocusedNode = node;
            tl.FocusedColumn = tl.Columns[fieldName];
            tl.BeginSelection();
            tl.SelectCell(node, tl.Columns[fieldName]);
            tl.EndSelection();


            while (tl.GetSelectedCells().Count > 1)
            {
                tl.BeginUpdate();
                tl.BeginSelection();
                tl.Selection.Clear();
                tl.SelectCell(node, tl.Columns[fieldName]);
                tl.EndSelection();
                tl.EndUpdate();
            }
           

        }

        public static void SetFirstFixColLeft(this TreeList tl, string fieldName)
        {

            foreach (TreeListColumn col in tl.VisibleColumns)
            {
                col.Fixed = TreeFixedStyle.Left;
                if (col.ParentBand != null)
                {
                    col.ParentBand.RootBand.Fixed = TreeFixedStyle.Left;
                }
                if (col.FieldName == fieldName)
                {
                    return;
                }
            }

        }
        public static string GetFirstFixColLeft(this TreeList tl)
        {
            var q1 = tl.VisibleColumns.Where(p => p.Fixed == TreeFixedStyle.Left).Select(p => p.FieldName).ToList();

            if (q1.Count <= 0) return string.Empty;
            return q1.Last();
        }

        public static DataTable GetAttColByAttValueDic(Dictionary<Int64, List<BoMInputAttInfo>> dicAttValue, string fieldName)
        {
            DataTable dtC = new DataTable();
            dtC.Columns.Add(fieldName, typeof(Int64));

            List<Int64> qDel = dicAttValue.SelectMany(p => p.Value.Select(t => t.AttibutePK), (m, n) => n).Distinct().ToList();


            foreach (Int64 key in qDel)
            {
                dtC.Rows.Add(key);

            }

            return dtC;


        }
        public static DataTable GetAttColByAttValueDic(Dictionary<string, List<BoMInputAttInfo>> dicAttValue, string fieldName)
        {
            DataTable dtC = new DataTable();
            dtC.Columns.Add(fieldName, typeof(Int64));

            List<Int64> qDel = dicAttValue.SelectMany(p => p.Value.Select(t => t.AttibutePK), (m, n) => n).Distinct().ToList();


            foreach (Int64 key in qDel)
            {
                dtC.Rows.Add(key);

            }

            return dtC;


        }
        public static RepositoryItemLookUpEdit CreateRepositoryItemLookUpEdit(DataTable dtpara, string displayMember, string valueMember, int searchColumnIndex, bool showHeader, bool setDropDownRow, SearchMode searchMode, params int[] hideCol)
        {
            RepositoryItemLookUpEdit lE = new RepositoryItemLookUpEdit();
            lE.DataSource = dtpara;
            lE.DisplayMember = displayMember;
            lE.ValueMember = valueMember;

            lE.ShowHeader = showHeader;


            LookUpColumnInfoCollection coll = lE.Columns;
            foreach (DataColumn colT in dtpara.Columns)
            {
                coll.Add(new LookUpColumnInfo(colT.ColumnName));
            }




            if (hideCol.Length > 0)
            {
                var qHide = hideCol.OrderByDescending(p => p).ToArray();
                foreach (int ind in qHide)
                {
                    LookUpColumnInfo col = lE.Columns[ind];
                    if (col == null) continue;
                    col.Visible = false;
                }
            }

            lE.ForceInitialize();
            lE.BestFitMode = BestFitMode.BestFitResizePopup;
            lE.TextEditStyle = TextEditStyles.Standard;
            lE.SearchMode = searchMode;
            lE.AutoSearchColumnIndex = searchColumnIndex;
            lE.AcceptEditorTextAsNewValue = DefaultBoolean.False;

            if (setDropDownRow)
            {
                lE.DropDownRows = dtpara.Rows.Count;
            }
            return lE;

        }
        public static void LoadLookupEdit(LookUpEdit lE, DataTable dtpara, string displayMember, string valueMember, int searchColumnIndex, bool showHeader, bool setDropDownRow, params int[] hideCol)
        {
            lE.Properties.DataSource = dtpara;
            lE.Properties.DisplayMember = displayMember;
            lE.Properties.ValueMember = valueMember;

            lE.Properties.ShowHeader = showHeader;


            LookUpColumnInfoCollection coll = lE.Properties.Columns;
            foreach (DataColumn colT in dtpara.Columns)
            {
                coll.Add(new LookUpColumnInfo(colT.ColumnName));
            }




            if (hideCol.Length > 0)
            {
                var qHide = hideCol.OrderByDescending(p => p).ToArray();
                foreach (int ind in qHide)
                {
                    LookUpColumnInfo col = lE.Properties.Columns[ind];
                    if (col == null) continue;
                    col.Visible = false;
                }
            }

            lE.Properties.ForceInitialize();
            lE.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            lE.Properties.TextEditStyle = TextEditStyles.Standard;
            lE.Properties.SearchMode = SearchMode.AutoFilter;
            lE.Properties.AutoSearchColumnIndex = searchColumnIndex;
            lE.Properties.AcceptEditorTextAsNewValue = DefaultBoolean.False;

            if (setDropDownRow)
            {
                lE.Properties.DropDownRows = dtpara.Rows.Count;
            }


        }


        public static PermissionFormInfo GetPermissionByFormCode(string formCode, string formName)
        {
            PermissionFormInfo info = new PermissionFormInfo
            {
                PerIns = false,
                PerDel = false,
                PerUpd = false,
                PerViw = false,
                PerCheckAdvanceFunction = true,
                StrAdvanceFunction = string.Empty,
                StrSpecialFunction = string.Empty,
                FormName = string.Empty,
                MenuCode = string.Empty,
            };


            // string formName = "";
            string menuCode = string.Empty;
            bool isAdmin = DeclareSystem.SysUserName.ToUpper() == "ADMIN";
            if (isAdmin)
            {
                info.PerIns = true;
                info.PerDel = true;
                info.PerUpd = true;
                info.PerViw = true;
                info.PerCheckAdvanceFunction = false;
                info.StrAdvanceFunction = "1,2,3,4,5";
                info.DtAdvanceFunc = GetAdvanceFunctionWhenOpenForm("1,2,3,4,5");
                info.StrSpecialFunction = string.Empty;
                info.FormName = string.Empty;
                info.MenuCode = menuCode;
                //   return info;
            }


            if (DeclareSystem.SysTablePermissionMainForm.Rows.Count <= 0 || string.IsNullOrEmpty(formCode))
            {
                if (!isAdmin)
                    info.DtAdvanceFunc = GetAdvanceFunctionWhenOpenForm(string.Empty);
                return info;
            }

            var q1 = DeclareSystem.SysTablePermissionMainForm.AsEnumerable().Where(p => GetSafeString(p["FormCode"]) == formCode && GetSafeString(p["FormName"]) == formName).ToList();
            if (q1.Count <= 0)
            {
                if (!isAdmin)
                    info.DtAdvanceFunc = GetAdvanceFunctionWhenOpenForm(string.Empty);
                return info;
            }

            DataRow dr = q1[0];
            if (!isAdmin)
            {
                info.PerIns = GetSafeBool(dr["RoleInsert"]);
                info.PerDel = GetSafeBool(dr["RoleDelete"]);
                info.PerUpd = GetSafeBool(dr["RoleUpdate"]);
                info.PerViw = GetSafeBool(dr["RoleView"]);
                info.PerCheckAdvanceFunction = GetSafeBool(dr["CheckAdvanceFunction"]);
                string advFunc = GetSafeString(dr["AdvanceFunction"]);
                info.StrAdvanceFunction = advFunc;
                info.DtAdvanceFunc = GetAdvanceFunctionWhenOpenForm(advFunc);
            }
            string strSpecialFunction = GetSafeString(dr["SpecialFunction"]);
            info.StrSpecialFunction = strSpecialFunction;
            info.DtSpecialFunction = GetSpecialFunctionWhenOpenForm(strSpecialFunction);
            info.FormName = GetSafeString(dr["FormName"]);
            info.MenuCode = GetSafeString(dr["MenuCode"]);
            return info;
        }
        public static PermissionFormInfo GetPermissionByFormCode(string formCode)
        {
            PermissionFormInfo info = new PermissionFormInfo
            {
                PerIns = false,
                PerDel = false,
                PerUpd = false,
                PerViw = false,
                PerCheckAdvanceFunction = true,
                StrAdvanceFunction = string.Empty,
                StrSpecialFunction = string.Empty,
                FormName = string.Empty,
                MenuCode = string.Empty,
            };


            // string formName = "";
            string menuCode = string.Empty;
            bool isAdmin = DeclareSystem.SysUserName.ToUpper() == "ADMIN";
            if (isAdmin)
            {
                info.PerIns = true;
                info.PerDel = true;
                info.PerUpd = true;
                info.PerViw = true;
                info.PerCheckAdvanceFunction = false;
                info.StrAdvanceFunction = "1,2,3,4,5";
                info.DtAdvanceFunc = GetAdvanceFunctionWhenOpenForm("1,2,3,4,5");
                info.StrSpecialFunction = string.Empty;
                info.FormName = string.Empty;
                info.MenuCode = menuCode;
                //   return info;
            }


            if (DeclareSystem.SysTablePermissionMainForm.Rows.Count <= 0 || string.IsNullOrEmpty(formCode))
            {
                if (!isAdmin)
                    info.DtAdvanceFunc = GetAdvanceFunctionWhenOpenForm(string.Empty);
                return info;
            }

            var q1 = DeclareSystem.SysTablePermissionMainForm.AsEnumerable().Where(p => GetSafeString(p["FormCode"]) == formCode).ToList();
            if (q1.Count <= 0)
            {
                if (!isAdmin)
                    info.DtAdvanceFunc = GetAdvanceFunctionWhenOpenForm(string.Empty);
                return info;
            }

            DataRow dr = q1[0];
            if (!isAdmin)
            {
                info.PerIns = GetSafeBool(dr["RoleInsert"]);
                info.PerDel = GetSafeBool(dr["RoleDelete"]);
                info.PerUpd = GetSafeBool(dr["RoleUpdate"]);
                info.PerViw = GetSafeBool(dr["RoleView"]);
                info.PerCheckAdvanceFunction = GetSafeBool(dr["CheckAdvanceFunction"]);
                string advFunc = GetSafeString(dr["AdvanceFunction"]);
                info.StrAdvanceFunction = advFunc;
                info.DtAdvanceFunc = GetAdvanceFunctionWhenOpenForm(advFunc);
            }
            string strSpecialFunction = GetSafeString(dr["SpecialFunction"]);
            info.StrSpecialFunction = strSpecialFunction;
            info.DtSpecialFunction = GetSpecialFunctionWhenOpenForm(strSpecialFunction);
            info.FormName = GetSafeString(dr["FormName"]);
            info.MenuCode = GetSafeString(dr["MenuCode"]);
            return info;
        }
       
        public static bool TryGetValueN<TKey, TValue>(this Dictionary<TKey, TValue> stack, TKey key, out TValue value)
        {

            value = default(TValue);
            try
            {
                return stack.TryGetValue(key, out value);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
        public static void AddN<TKey, TValue>(this Dictionary<TKey, TValue> stack, TKey key, TValue value)
        {
            if (stack.ContainsKey(key)) return;
            stack.Add(key, value);


        }


        public static Stack<T> CopyAnotherStack<T>(this Stack<T> stack)
        {

            if (stack.Count <= 0) return new Stack<T>();
            T[] arr = stack.Select((p, i) => new
            {
                Index = i,
                Item = p
            }).OrderByDescending(p => p.Index).Select(p => p.Item).ToArray();

            return new Stack<T>(arr);

        }
        public static int GetColumnPanelHeight(this TreeList tl)
        {
            TreeListViewInfo viewInfo = tl.ViewInfo;
            int height = 0;
            for (int i = 0; i < tl.VisibleColumns.Count; i++)
            {
                height = Math.Max(GetColumnBestHeight(viewInfo, tl.VisibleColumns[i]), height);
            }
            return height;

        }

        private static int GetColumnBestHeight(TreeListViewInfo viewInfo, TreeListColumn column)
        {
            ColumnInfo ex = viewInfo.ColumnsInfo[column];
            if (ex == null)
            {
                viewInfo.GInfo.AddGraphics(null);
                ex = new ColumnInfo(column);
                try
                {
                    ex.InnerElements.Add(new DrawElementInfo(new GlyphElementPainter(), new GlyphElementInfoArgs(viewInfo.TreeList.ColumnsImageList, 0, null), StringAlignment.Near));
                    ex.SetAppearance(viewInfo.PaintAppearance.HeaderPanel);
                    ex.Caption = column.Caption;
                    ex.CaptionRect = new Rectangle(0, 0, column.Width - 20, 17);
                }
                finally
                {
                    viewInfo.GInfo.ReleaseGraphics();
                }
            }
            GraphicsInfo grInfo = new GraphicsInfo();
            grInfo.AddGraphics(null);
            ex.Cache = grInfo.Cache;
            bool canDrawMore = true;
            Size captionSize = CalcCaptionTextSize(grInfo.Cache, ex as HeaderObjectInfoArgs, column.GetCaption());
            Size res = ex.InnerElements.CalcMinSize(grInfo.Graphics, ref canDrawMore);
            res.Height = Math.Max(res.Height, captionSize.Height);
            res.Width += captionSize.Width;
            grInfo.ReleaseGraphics();
            return res.Height;
        }

        private static Size CalcCaptionTextSize(GraphicsCache cache, HeaderObjectInfoArgs ee, string caption)
        {
            Size captionSize = ee.Appearance.CalcTextSize(cache, caption, ee.CaptionRect.Width).ToSize();
            captionSize.Height++;
            captionSize.Width++;
            return captionSize;
        }





        public static void VisibleAndSortGridColumn(this GridView gv, Dictionary<string, bool> dicCol, string afterColumn, Dictionary<Int64, AttributeHeaderInfo> dicAttHeader)
        {

            bool isFind = false;
            int index = -1;
            foreach (var item in dicCol)
            {
                string sCol = item.Key;
                bool visible = item.Value;
                GridColumn tColU = gv.Columns[sCol];
                if (!isFind)
                {
                    if (afterColumn == string.Empty)
                    {
                        isFind = true;
                        SortAttColtTemp(gv, dicAttHeader, ref index);
                    }
                    else if (afterColumn == sCol)
                    {
                        isFind = true;
                        if (tColU != null)
                        {
                            if (visible)
                            {
                                index++;
                                tColU.VisibleIndex = index;
                            }
                            else
                            {
                                tColU.Visible = false;
                            }


                        }
                        SortAttColtTemp(gv, dicAttHeader, ref index);
                        continue;

                    }
                }
                if (tColU == null) continue;

                if (visible)
                {
                    index++;
                    tColU.VisibleIndex = index;
                }
                else
                {
                    tColU.Visible = false;
                }


            }
            if (!isFind)
            {
                SortAttColtTemp(gv, dicAttHeader, ref index);
            }



        }


        private static void SortAttColtTemp(GridView gv, Dictionary<Int64, AttributeHeaderInfo> dicAttHeader, ref int index)
        {
            foreach (var itemA in dicAttHeader)
            {
                string colAtt = itemA.Value.AttibuteName;
                GridColumn tColA = gv.Columns[colAtt];
                if (tColA != null)
                {
                    index++;
                    tColA.VisibleIndex = index;
                }
            }
        }

        public static void VisibleAndSortGridColumn(this GridView gv, Dictionary<string, bool> dicCol)
        {


            var qUnVisible = dicCol.Where(p => !p.Value).Select(p => p.Key).ToList();
            var qVisible = dicCol.Where(p => p.Value).Select(p => p.Key).ToList();

            for (int i = 0; i < qVisible.Count; i++)
            {
                GridColumn tColV = gv.Columns[qVisible[i]];
                if (tColV == null) continue;
                tColV.VisibleIndex = i;
            }
            foreach (string s in qUnVisible)
            {
                GridColumn tColU = gv.Columns[s];
                if (tColU == null) continue;
                tColU.Visible = false;

            }

        }
        public static void VisibleAndSortTreeColumn(this TreeList tl, Dictionary<string, bool> dicCol)
        {


            var qUnVisible = dicCol.Where(p => !p.Value).Select(p => p.Key).ToList();
            var qVisible = dicCol.Where(p => p.Value).Select(p => p.Key).ToList();

            for (int i = 0; i < qVisible.Count; i++)
            {
                TreeListColumn tColV = tl.Columns[qVisible[i]];
                if (tColV == null) continue;
                tColV.VisibleIndex = i;
            }
            foreach (string s in qUnVisible)
            {
                TreeListColumn tColU = tl.Columns[s];
                if (tColU == null) continue;
                tColU.Visible = false;

            }

        }
        public static void SetTreeListColumnHeader(TreeListColumn tlCol, string caption, bool allowSort, HorzAlignment align,
            TreeFixedStyle fixedStyle, object tag, bool allowFilter = true)
        {

            tlCol.AppearanceCell.TextOptions.HAlignment = align;
            tlCol.AppearanceHeader.TextOptions.HAlignment = align;
            tlCol.Caption = caption;
            tlCol.Fixed = fixedStyle;
            tlCol.OptionsColumn.AllowMove = false;


         //   tlCol.OptionsColumn.AllowMove = true;

            tlCol.OptionsColumn.AllowSort = allowSort; //
            tlCol.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
            tlCol.Tag = tag;
            if (allowFilter)
            {
                tlCol.OptionsFilter.AllowFilter = true;
                tlCol.OptionsFilter.AllowAutoFilter = true;
                tlCol.OptionsFilter.AutoFilterCondition = DevExpress.XtraTreeList.Columns.AutoFilterCondition.Contains;
            }
            else
            {
                tlCol.OptionsFilter.AllowFilter = false;
                tlCol.OptionsFilter.AllowAutoFilter = false;
                //    tlCol.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Default;

            }


        }



        public static void ShowFindPanelTreeList(this TreeList tl, string findPanelText)
        {

            if (!tl.FindPanelVisible)
            {
                tl.ShowFindPanel();

                if (!string.IsNullOrEmpty(findPanelText))
                {
                    tl.ApplyFindFilter(findPanelText);
                }

            }

        }


        public static void SetGridColumnHeader(GridColumn tlCol, string caption, DefaultBoolean allowSort, HorzAlignment align,
            DevExpress.XtraGrid.Columns.FixedStyle fixedStyle, Int64 tag = 0, bool allowFilter = true, DefaultBoolean allowGroup = DefaultBoolean.True)
        {
            if (tlCol == null) return;
            tlCol.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
            tlCol.AppearanceCell.TextOptions.HAlignment = align;
            tlCol.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            tlCol.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
            tlCol.AppearanceHeader.TextOptions.HAlignment = align;
            tlCol.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            tlCol.Caption = caption;
            tlCol.Fixed = fixedStyle;
            tlCol.OptionsColumn.AllowMove = true;
            tlCol.OptionsColumn.AllowSort = allowSort; //
            tlCol.OptionsColumn.AllowGroup = allowGroup; //
            tlCol.Tag = tag;
            if (allowFilter)
            {
                tlCol.OptionsFilter.AllowFilter = true;
                tlCol.OptionsFilter.AllowAutoFilter = true;
                tlCol.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            }
            else
            {
                tlCol.OptionsFilter.AllowFilter = false;
                tlCol.OptionsFilter.AllowAutoFilter = false;
                //    tlCol.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Default;

            }


        }

        public static string GetRandomTableName()
        {
           
            return string.Format("{0}{1}{2:ddMMyyyyhhmmss}", DeclareSystem.SysUserName, Guid.NewGuid().ToString().Replace("-", string.Empty).Replace(" ", string.Empty).ToUpper().Trim(), DateTime.Now);
        }
        public static string GetRandomTableName(string projectCode, int index)
        {
            return string.Format("{0}{1}{2}{3:ddMMyyyyhhmmss}{4}", projectCode, DeclareSystem.SysUserName,
                Guid.NewGuid().ToString().Replace("-", string.Empty).Replace(" ", string.Empty).ToUpper().Trim(), DateTime.Now, index);
        }


        public static string CheckIsAlphabeStr(this Keys keyCode)
        {
            string str = string.Empty;
            switch (keyCode)
            {
                case Keys.A:
                case Keys.B:
                case Keys.C:
                case Keys.D:
                case Keys.E:
                case Keys.F:
                case Keys.G:
                case Keys.H:
                case Keys.I:
                case Keys.J:
                case Keys.K:
                case Keys.L:
                case Keys.M:
                case Keys.N:
                case Keys.O:
                case Keys.P:
                case Keys.Q:
                case Keys.R:
                case Keys.S:
                case Keys.T:
                case Keys.U:
                case Keys.V:
                case Keys.W:
                case Keys.X:
                case Keys.Y:
                case Keys.Z:
                    str = keyCode.GetKeyCodeStringValue();
                    break;
                default:
                    str = string.Empty;
                    break;
            }
            return str;
            //           
            //             

        }



        public static string CheckIsAlphabet(this Keys keyCode, out bool isAlphabet)
        {
            string str = string.Empty;
            switch (keyCode)
            {
                case Keys.A:
                case Keys.B:
                case Keys.C:
                case Keys.D:
                case Keys.E:
                case Keys.F:
                case Keys.G:
                case Keys.H:
                case Keys.I:
                case Keys.J:
                case Keys.K:
                case Keys.L:
                case Keys.M:
                case Keys.N:
                case Keys.O:
                case Keys.P:
                case Keys.Q:
                case Keys.R:
                case Keys.S:
                case Keys.T:
                case Keys.U:
                case Keys.V:
                case Keys.W:
                case Keys.X:
                case Keys.Y:
                case Keys.Z:
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                    str = keyCode.GetKeyCodeStringValue();
                    isAlphabet = true;
                    break;
                case Keys.F4:
                    str = "F4";
                    isAlphabet = false;
                    break;
                default:
                    str = string.Empty;
                    isAlphabet = false;
                    break;
            }
            return str;
            //           
            //             

        }



        public static string CheckIsKeyInputText(this char keyChar, out bool isInput)
        {
            string str = keyChar.ToString();

            if (char.IsLetter(keyChar) || char.IsDigit(keyChar))
            {
                isInput = true;
                return str;
            }
            switch (str)
            {
                case "!":
                case "@":
                case "#":
                case "$":
                case "%":
                case "^":
                case "&":
                case "*":
                case "(":
                case ")":
                case "-":
                case "_":
                case "+":
                case "=":
                case "{":
                case "[":
                case "}":
                case "]":
                case "|":
                case @"\":
                case ":":
                case ";":
                case "<":
                case ",":
                case ">":
                case ".":
                case "?":
                case "/":
                case "'":
                case "\"":
                    isInput = true;
                    return str;
                default:
                    isInput = false;
                    return string.Empty;

            }

        }


        [DllImport("user32.dll")]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);
        public static string GetKeyCodeStringValue(this Keys keyCode)
        {
            //KeysConverter kc = new KeysConverter();
            //return kc.ConvertToString(keyCode);

            byte[] keyboardState = new byte[255];
            bool keyboardStateStatus = GetKeyboardState(keyboardState);

            if (!keyboardStateStatus)
            {
                return string.Empty;
            }

            uint virtualKeyCode = (uint)keyCode;
            uint scanCode = MapVirtualKey(virtualKeyCode, 0);
            IntPtr inputLocaleIdentifier = GetKeyboardLayout(0);

            StringBuilder result = new StringBuilder();
            ToUnicodeEx(virtualKeyCode, scanCode, keyboardState, result, 5, (uint)0, inputLocaleIdentifier);

            return result.ToString();
        }


        public static string LeftString(this string s, int length)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            if (s.Length > length)
            {
                return s.Substring(0, length);
            }
            return s;
        }


        public static string GetAttributeStringValue(this DataAttType dataType, object value)
        {
            if (dataType == DataAttType.Boolean)
            {
                return GetSafeBoolString(value);
            }
            if (dataType == DataAttType.Datetime)
            {
                return GetSafeDatetimeNullableReturnMinDate(value).ToString(ConstSystem.SysDateFormat);
            }

            return GetSafeString(value);
        }


        public static DataTable StandardUnitAttTableBeforPrint(DataTable dt, string[] arrAttField, out Dictionary<string, string> dic)
        {
            dic = new Dictionary<string, string>();
            if (arrAttField.Length <= 0)
                return dt;
            if (dt.Rows.Count <= 0)
                return dt;
            try
            {

                foreach (string fieldName in arrAttField)
                {
                    var qFieldName = dt.AsEnumerable().Where(p => GetSafeString(p[fieldName]) != string.Empty).Select(p => new
                    {
                        Row = p,
                        Value = GetSafeString(p[fieldName]),
                        LastIndex = GetSafeString(p[fieldName]).LastIndexOf("-", StringComparison.Ordinal)
                    }).Select(p => new
                    {
                        p.Row,
                        p.Value,
                        p.LastIndex,
                        TempValue = p.LastIndex <= 0 ? p.Value : p.Value.SubStringNew(0, p.LastIndex),
                        TempUnit = p.LastIndex <= 0 ? string.Empty : p.Value.SubStringNew(p.LastIndex + 1, p.Value.Length - (p.LastIndex + 1)),
                        IndexDistinct = p.LastIndex <= 0 ? 0 : 1
                    }).ToList();
                    if (!qFieldName.Any()) continue;

                    int isDistinct = qFieldName.Select(p => p.TempUnit).Distinct().Count();
                    if (isDistinct == 1)
                    {
                        dic.Add(fieldName, qFieldName.First().TempUnit);
                        foreach (var item in qFieldName)
                        {
                            item.Row[fieldName] = item.TempValue;
                        }
                    }
                    else
                    {
                        // var q2 = qFieldName.Where(p => p.IndexDistinct == 1).ToList();
                        foreach (var itemQ2 in qFieldName)
                        {
                            if (itemQ2.IndexDistinct == 1)
                            {
                                itemQ2.Row[fieldName] = itemQ2.Value.Replace("-", string.Empty).Trim();
                            }

                        }
                    }

                }
                return dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dt;
            }



        }







        public static AttValueReturnInfo ShowFormInputAttributeOnGrid(GridView gv, Int32 rH, string fieldName, Form fCurr, DataAttType dataType, string strKey, DataTable dtUnit, string defaultUnit, bool isAlphabet)
        {


            AttValueReturnInfo rt = new AttValueReturnInfo { IsSave = false };


            GridCellInfo cellInfo = GetGridCellInfo(gv, rH, fieldName);
            if (cellInfo != null)
            {
                GridControl gc = gv.GridControl;
                FrmInputAttFinal f = new FrmInputAttFinal(dataType, strKey, dtUnit, defaultUnit, isAlphabet);
                Rectangle r = cellInfo.Bounds;

                Rectangle screen = Screen.FromControl(fCurr).WorkingArea;
                int rHeight = r.Height;
                int screenHeight = screen.Height;
                int screenWidth = screen.Width;
                Point point = gc.PointToScreen(new Point(r.Left, r.Top + rHeight));
                int x = point.X;
                int y = point.Y;
                int width = f.Width;
                int height = f.Height;
                int curWidth = x + width;

                int xNew = x;
                int yNew = y;

                if (curWidth > screenWidth)
                {
                    xNew = screenWidth - width;
                }
                int curHeight = y + height;

                if (curHeight > screenHeight)
                {
                    yNew = y - height - rHeight;
                }


                point.X = xNew;
                point.Y = yNew;




                f.Location = point;
                f.OnInputValue += (s, e) =>
                {
                    rt.IsSave = true;
                    rt.Value = e.Value;
                    rt.UnitCode = e.UnitCode;
                    rt.UnitName = e.UnitName;
                };
                f.ShowDialog();
            }
            return rt;

        }


        public static RichTextValueReturnInfo ShowFormInputRichTextOnTree(TreeList tl, TreeListNode node, int visibleIndex, Form fCurr, string data, DocumentTypeRichText typeText = DocumentTypeRichText.Normal,bool isInsertEnterKey = false)
        {
            RichTextValueReturnInfo rt = new RichTextValueReturnInfo { IsSave = false };
            DevExpress.XtraTreeList.ViewInfo.RowInfo rowInfo = tl.ViewInfo.RowsInfo[node];
            if (rowInfo == null || rowInfo.Cells.Count<=0) return rt;

            DevExpress.XtraTreeList.ViewInfo.CellInfo cellInfo = null;
            bool isFind = false;
            foreach (CellInfo cellLoop in rowInfo.Cells)
            {
                if (cellLoop.Column.VisibleIndex == visibleIndex)
                {
                    cellInfo = cellLoop;
                    isFind = true;
                    break;
                }
            }

            if (!isFind)
            {
                cellInfo = rowInfo.Cells[rowInfo.Cells.Count - 1];
            }

            // DevExpress.XtraTreeList.ViewInfo.CellInfo cellInfo = rowInfo.Cells[visibleIndex];
            if (cellInfo == null) return rt;
            FrmInputRichText f = new FrmInputRichText(data, typeText, isInsertEnterKey);
            Rectangle r = cellInfo.Bounds;

            Rectangle screen = Screen.FromControl(fCurr).WorkingArea;
            int rHeight = r.Height;
            int screenHeight = screen.Height;
            int screenWidth = screen.Width;

            Point point = tl.PointToScreen(new Point(r.Left, r.Top + rHeight));
            int x = point.X;
            int y = point.Y;
            int width = f.Width;
            int height = f.Height;
            int curWidth = x + width;

            int xNew = x;
            int yNew = y;
            DevExpress.XtraBars.BarDockStyle dockStyle = BarDockStyle.Top;
            


            if (curWidth > screenWidth)
            {
                xNew = screenWidth - width;
            }
            int curHeight = y + height;

            if (curHeight > screenHeight)
            {
                yNew = y - height - rHeight;
                dockStyle = BarDockStyle.Bottom;
                if (yNew < 0)
                {
                    yNew = 0;
                }
            }


            point.X = xNew;
            point.Y = yNew;




            f.Location = point;
            f.MenuDockStyle = dockStyle;


            f.OnInputValue += (s, e) =>
            {
                rt.IsSave = e.IsSave;
                rt.Value = e.Value;
                rt.ValueHtml = e.ValueHtml;
                rt.ValueRtf = e.ValueRtf;
            };

            f.ShowDialog();
            return rt;





        }



        public static MemoValueReturnInfo ShowFormInputMemoOnTree(TreeList tl, TreeListNode node, int visibleIndex, Form fCurr, string data)
        {

            MemoValueReturnInfo rt = new MemoValueReturnInfo { IsSave = false };
            DevExpress.XtraTreeList.ViewInfo.CellInfo cellInfo = tl.ViewInfo.RowsInfo[node].Cells[visibleIndex];
            if (cellInfo != null)
            {
                FrmInputMemo f = new FrmInputMemo(data);
                Rectangle r = cellInfo.Bounds;

                Rectangle screen = Screen.FromControl(fCurr).WorkingArea;
                int rHeight = r.Height;
                int screenHeight = screen.Height;
                int screenWidth = screen.Width;

                Point point = tl.PointToScreen(new Point(r.Left, r.Top + rHeight));
                int x = point.X;
                int y = point.Y;
                int width = f.Width;
                int height = f.Height;
                int curWidth = x + width;

                int xNew = x;
                int yNew = y;

                if (curWidth > screenWidth)
                {
                    xNew = screenWidth - width;
                }
                int curHeight = y + height;

                if (curHeight > screenHeight)
                {
                    yNew = y - height - rHeight;
                }


                point.X = xNew;
                point.Y = yNew;




                f.Location = point;
                f.OnInputValue += (s, e) =>
                {
                    rt.IsSave = e.IsSave;
                    rt.Value = e.Value;
                };
                f.ShowDialog();
            }
            return rt;

        }
        public static AttValueReturnInfo ShowFormInputAttributeOnTree(TreeList tl, TreeListNode node, int visibleIndex, Form fCurr, DataAttType dataType, string strKey, DataTable dtUnit, string defaultUnit, bool isAlphabet)
        {

            AttValueReturnInfo rt = new AttValueReturnInfo { IsSave = false };
            DevExpress.XtraTreeList.ViewInfo.RowInfo rowInfo = tl.ViewInfo.RowsInfo[node];
            if (rowInfo == null) return rt;
            List<CellInfo> cellsInfo = rowInfo.Cells;
            int visibleCount = cellsInfo.Count;
            if(visibleCount<=0) return rt;
            int indexFinal = visibleIndex >= visibleCount ? visibleCount - 1 : visibleIndex;
            DevExpress.XtraTreeList.ViewInfo.CellInfo cellInfo = cellsInfo[indexFinal];
            if (cellInfo != null)
            {
                FrmInputAttFinal f = new FrmInputAttFinal(dataType, strKey, dtUnit, defaultUnit, isAlphabet);
                Rectangle r = cellInfo.Bounds;

                Rectangle screen = Screen.FromControl(fCurr).WorkingArea;
                int rHeight = r.Height;
                int screenHeight = screen.Height;
                int screenWidth = screen.Width;

                Point point = tl.PointToScreen(new Point(r.Left, r.Top + rHeight));
                int x = point.X;
                int y = point.Y;
                int width = f.Width;
                int height = f.Height;
                int curWidth = x + width;

                int xNew = x;
                int yNew = y;

                if (curWidth > screenWidth)
                {
                    xNew = screenWidth - width;
                }
                int curHeight = y + height;

                if (curHeight > screenHeight)
                {
                    yNew = y - height - rHeight;
                }


                point.X = xNew;
                point.Y = yNew;




                f.Location = point;
                f.OnInputValue += (s, e) =>
                {
                    rt.IsSave = true;
                    rt.Value = e.Value;
                    rt.UnitCode = e.UnitCode;
                    rt.UnitName = e.UnitName;
                };
                f.ShowDialog();
            }
            return rt;

        }
        /*
        public static void UpdateGridViewInfo(GridView gv)
        {

            GridViewInfo gvInfo = gv.GetViewInfo() as GridViewInfo;
            if (gvInfo == null) return;
            for (int rowIndex = 0; rowIndex < gv.RowCount; rowIndex++)
            {
                GridDataRowInfo rowInfo = new GridDataRowInfo(gvInfo, rowIndex, 0);
                for (int colIndex = 0; colIndex < gv.VisibleColumns.Count; colIndex++)
                {
                    GridColumnInfoArgs args = new GridColumnInfoArgs(gv.VisibleColumns[colIndex]);
                    GridCellInfo cellInfo = new GridCellInfo(args, rowInfo, Rectangle.Empty);
                    gvInfo.UpdateCellAppearance(cellInfo);
                }
            }
        }
        */

        public static DataTable GetTableTypeColumnInt()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemIndex", typeof(int));
            return dt;
        }




        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr GetFocus();

        public static Control GetFocusedControl()
        {
            Control focusedControl = null;
            // To get hold of the focused control:
            IntPtr focusedHandle = GetFocus();
            if (focusedHandle != IntPtr.Zero)
                // Note that if the focused Control is not a .Net control, then this will return null.
                focusedControl = Control.FromHandle(focusedHandle);
            return focusedControl;
        }




        public static string GetSelectedText(this RadioGroup rg)
        {
            int index = rg.SelectedIndex;
            if (index < 0) return string.Empty;
            return rg.Properties.Items[index].Description;
        }
        public static void ExpandCollapseNodeSelected(this TreeList tl, bool expanded)
        {
            var q1 = tl.GetSelectedCells().Select(p => p.Node).Distinct().ToList();
            var q2 = GetParentNodesFormCollectionNodes(q1);

            foreach (TreeListNode nodeQ2 in q2)
            {
                if (expanded)
                {
                    nodeQ2.ExpandAll();
                }
                else
                {
                    nodeQ2.Expanded = false;
                }

            }
        }

        public static List<TreeListNode> GetParentNodesFormCollectionNodes(List<TreeListNode> lNode)
        {

            if (lNode.Count <= 1) return lNode;
            int i = 0, j = i + 1;
            List<TreeListNode> lNonParnet = new List<TreeListNode>();
            while (i != lNode.Count - 1)
            {
                TreeListNode nonParentNode = HasAsParent(lNode[i], lNode[j]);
                if (nonParentNode != null)
                    lNonParnet.Add(nonParentNode);
                if (j == lNode.Count - 1)
                {
                    i++; j = i + 1;
                }
                else
                    j++;
            }

            if (lNonParnet.Count == 0) return lNode;

            //foreach (TreeListNode node in lNonParnet)
            //    lNode.Remove(node);
            return lNode.Where(p => lNonParnet.All(t => t != p)).ToList();

        }
        public static TreeListNode HasAsParent(TreeListNode node1, TreeListNode node2)
        {
            if (node1.Level > node2.Level)
                return node1.HasAsParent(node2) ? node1 : null;
            if (node1.Level < node2.Level) return node2.HasAsParent(node1) ? node2 : null;
            return null;

        }

        public static List<TreeListSortIndexAfterDrop> GetListTreeNodeSameLevelAfterFormNodeS(TreeList tl, List<TreeListNode> lDrop)
        {
            List<TreeListSortIndexAfterDrop> lR = new List<TreeListSortIndexAfterDrop>();
            var lNode = tl.Nodes;

            int i = 0;
            foreach (TreeListNode node in lNode)
            {

                bool isDropNode = lDrop.Any(p => p == node);
                if (isDropNode)
                {
                    var item1 = new TreeListSortIndexAfterDrop
                    {
                        Node = node,
                        RowIndex = i,
                        VisibleIndex = tl.GetVisibleIndexByNode(node),
                        IsDropNode = 0,
                    };
                    lR.Add(item1);
                }
                else
                {
                    var item = new TreeListSortIndexAfterDrop
                    {
                        Node = node,
                        RowIndex = i,
                        VisibleIndex = tl.GetVisibleIndexByNode(node),
                        IsDropNode = 1,
                    };
                    lR.Add(item);
                }
                i++;

            }
            if (lR.Count <= 1)
                return lR;
            var q2 = lR.OrderByDescending(p => p.IsDropNode).ThenBy(p => p.RowIndex).Select((p, j) => new TreeListSortIndexAfterDrop
            {
                Node = p.Node,
                RowIndex = j,
                VisibleIndex = p.VisibleIndex,
                IsDropNode = p.IsDropNode,
            }).ToList();



            return q2;
        }
        public static List<TreeListSortIndexAfterDrop> GetListTreeNodeSameLevelAfterFormNodeS(TreeList tl, TreeListNode parentNode, TreeListNode formNode, List<TreeListNode> lDrop)
        {
            List<TreeListSortIndexAfterDrop> lR = new List<TreeListSortIndexAfterDrop>();
            var lNode = parentNode == null ? tl.Nodes : parentNode.Nodes;
            bool isFind = false;
            int i = 0;
            foreach (TreeListNode node in lNode)
            {
                bool isSame = node == formNode;
                bool isDropNode = lDrop.Any(p => p == node);

                if (isSame)
                {
                    isFind = true;
                }

                if (!isFind)
                {
                    if (isDropNode)
                    {
                        var item1 = new TreeListSortIndexAfterDrop
                        {
                            Node = node,
                            RowIndex = i,
                            VisibleIndex = tl.GetVisibleIndexByNode(node),
                            IsDropNode = 2,
                        };
                        lR.Add(item1);
                    }
                    else
                    {
                        var item = new TreeListSortIndexAfterDrop
                        {
                            Node = node,
                            RowIndex = i,
                            VisibleIndex = tl.GetVisibleIndexByNode(node),
                            IsDropNode = 4,
                        };
                        lR.Add(item);
                    }
                }
                else
                {
                    if (isSame)
                    {
                        var item2 = new TreeListSortIndexAfterDrop
                        {
                            Node = node,
                            RowIndex = i,
                            VisibleIndex = tl.GetVisibleIndexByNode(node),
                            IsDropNode = 3,
                        };
                        lR.Add(item2);
                    }
                    else
                    {
                        if (isDropNode)
                        {
                            var item3 = new TreeListSortIndexAfterDrop
                            {
                                Node = node,
                                RowIndex = i,
                                VisibleIndex = tl.GetVisibleIndexByNode(node),
                                IsDropNode = 1,
                            };
                            lR.Add(item3);
                        }
                        else
                        {
                            var item4 = new TreeListSortIndexAfterDrop
                            {
                                Node = node,
                                RowIndex = i,
                                VisibleIndex = tl.GetVisibleIndexByNode(node),
                                IsDropNode = 0,
                            };
                            lR.Add(item4);
                        }
                    }
                }
                i++;

            }
            if (lR.Count <= 1)
                return lR;
            var q2 = lR.OrderByDescending(p => p.IsDropNode).ThenBy(p => p.RowIndex).Select((p, j) => new TreeListSortIndexAfterDrop
            {
                Node = p.Node,
                RowIndex = j,
                VisibleIndex = p.VisibleIndex,
                IsDropNode = p.IsDropNode,
            }).ToList();
            return q2;
        }
        public static List<TreeListSortIndexAfterDrop> GetListTreeNodeSameLevelAfterFormNode(TreeList tl, TreeListNode parentNode, TreeListNode formNode, List<TreeListNode> lDrop)
        {
            List<TreeListSortIndexAfterDrop> lR = new List<TreeListSortIndexAfterDrop>();
            var lNode = parentNode == null ? tl.Nodes : parentNode.Nodes;
            bool isFind = false;
            int i = 0;
            foreach (TreeListNode node in lNode)
            {
                bool isSame = node == formNode;
                if (isSame)
                {
                    isFind = true;
                    var item = new TreeListSortIndexAfterDrop
                    {
                        Node = node,
                        RowIndex = i,
                        VisibleIndex = tl.GetVisibleIndexByNode(node),
                        IsDropNode = 2,
                    };
                    lR.Add(item);
                    i++;
                }
                if (isFind && !isSame)
                {

                    var q1 = lDrop.Any(p => p == node);

                    var item1 = new TreeListSortIndexAfterDrop
                    {
                        Node = node,
                        RowIndex = i,
                        VisibleIndex = tl.GetVisibleIndexByNode(node),
                        IsDropNode = q1 ? 1 : 0
                    };
                    lR.Add(item1);
                    i++;
                }
            }
            if (lR.Count <= 1)
                return lR;
            var q2 = lR.OrderByDescending(p => p.IsDropNode).ThenBy(p => p.RowIndex).Select((p, j) => new TreeListSortIndexAfterDrop
            {
                Node = p.Node,
                RowIndex = j,
                VisibleIndex = p.VisibleIndex,
                IsDropNode = p.IsDropNode,
            }).ToList();
            return q2;
        }

        public static List<TreeListNode> GetNodeSameLevelAfterFormNodeNew(TreeList tl, TreeListNode parentNode, TreeListNode formNode)
        {
            List<TreeListNode> lR = new List<TreeListNode>();
            if (formNode == null) return lR;

            var lNode = parentNode == null ? tl.Nodes : parentNode.Nodes;
            bool isFind = false;



            foreach (TreeListNode node in lNode)
            {
                bool isSame = node == formNode;
                if (isSame)
                {
                    isFind = true;
                }
                if (isFind && !isSame)
                {
                    lR.Add(node);

                }
            }



            return lR;
        }


        public static bool GetNodeSameLevelBeforFormNode(TreeList tl, TreeListNode parentNode, TreeListNode formNode)
        {

            if (formNode == null) return false;
            var lNode = parentNode == null ? tl.Nodes : parentNode.Nodes;

            if (lNode.Count > 0)
                return true;
            return false;
        }


        public static bool GetNodeSameLevelAfterFormNode(TreeList tl, TreeListNode parentNode, TreeListNode formNode)
        {

            if (formNode == null) return false;

            var lNode = parentNode == null ? tl.Nodes : parentNode.Nodes;
            bool isFind = false;



            foreach (TreeListNode node in lNode)
            {
                bool isSame = node == formNode;
                if (isSame)
                {
                    isFind = true;
                }
                if (isFind && !isSame)
                {
                    return true;

                }
            }



            return false;
        }




        public static List<TreeListSortIndexAfterDrop> GetListTreeNodeSameLevelAfterFormNode(TreeList tl, TreeListNode parentNode, TreeListNode formNode, List<TreeListNode> lDrop, string sortOrderColumn, bool isContainDrop)
        {
            List<TreeListSortIndexAfterDrop> lR = new List<TreeListSortIndexAfterDrop>();
            var lNode = parentNode == null ? tl.Nodes : parentNode.Nodes;
            bool isFind = false;

            int sortOrder = 0;
            int sortOrderDrop = 0;
            int countDrop = lDrop.Count;
            //10

            // 2

            foreach (TreeListNode node in lNode)
            {
                bool isSame = node == formNode;
                if (isSame)
                {
                    isFind = true;
                    int tempSort = GetSafeInt(node.GetValue(sortOrderColumn)) + 1;
                    sortOrder = tempSort + countDrop;
                    sortOrderDrop = tempSort;
                }
                if (isFind && !isSame)
                {
                    if (!isContainDrop || lDrop.All(p => p != node))
                    {
                        var item1 = new TreeListSortIndexAfterDrop
                        {
                            Node = node,
                            RowIndex = sortOrder,
                            VisibleIndex = tl.GetVisibleIndexByNode(node),
                            IsDropNode = 0
                        };
                        lR.Add(item1);
                        sortOrder++;
                    }

                }
            }



            foreach (var nodeD in lDrop)
            {
                var itemD = new TreeListSortIndexAfterDrop
                {
                    Node = nodeD,
                    RowIndex = sortOrderDrop,
                    VisibleIndex = tl.GetVisibleIndexByNode(nodeD),
                    IsDropNode = 1
                };
                lR.Add(itemD);
                sortOrderDrop++;
            }

            //sortOrderColumn
            if (lR.Count <= 1)
                return lR;
            var q2 = lR.OrderByDescending(p => p.IsDropNode).ThenBy(p => p.RowIndex).ToList();
            return q2;
        }


        public static List<TreeListSortIndexAfterDrop> GetListTreeNodeSameLevelBeforFormNode(TreeList tl, TreeListNode parentNode, TreeListNode formNode, List<TreeListNode> lDrop, string sortOrderColumn, bool isContainDrop)
        {
            List<TreeListSortIndexAfterDrop> lR = new List<TreeListSortIndexAfterDrop>();
            var lNode = parentNode == null ? tl.Nodes : parentNode.Nodes;
            bool isFind = false;

            int sortOrder = 0;
            int sortOrderDrop = 0;
            int countDrop = lDrop.Count;
            //10

            // 2

            foreach (TreeListNode node in lNode)
            {
                bool isSame = node == formNode;
                if (isSame)
                {
                    isFind = true;
                    int tempSort = GetSafeInt(node.GetValue(sortOrderColumn)) + 1;
                    sortOrder = tempSort + countDrop;
                    sortOrderDrop = tempSort;

                    var item0 = new TreeListSortIndexAfterDrop
                    {
                        Node = node,
                        RowIndex = sortOrder,
                        VisibleIndex = tl.GetVisibleIndexByNode(node),
                        IsDropNode = 0
                    };
                    lR.Add(item0);
                    sortOrder++;

                }
                if (isFind && !isSame)
                {
                    if (!isContainDrop || lDrop.All(p => p != node))
                    {
                        var item1 = new TreeListSortIndexAfterDrop
                        {
                            Node = node,
                            RowIndex = sortOrder,
                            VisibleIndex = tl.GetVisibleIndexByNode(node),
                            IsDropNode = 0
                        };
                        lR.Add(item1);
                        sortOrder++;
                    }

                }
            }



            foreach (var nodeD in lDrop)
            {
                var itemD = new TreeListSortIndexAfterDrop
                {
                    Node = nodeD,
                    RowIndex = sortOrderDrop,
                    VisibleIndex = tl.GetVisibleIndexByNode(nodeD),
                    IsDropNode = 1
                };
                lR.Add(itemD);
                sortOrderDrop++;
            }

            //sortOrderColumn
            if (lR.Count <= 1)
                return lR;
            var q2 = lR.OrderByDescending(p => p.IsDropNode).ThenBy(p => p.RowIndex).ToList();
            return q2;
        }





        public static List<TreeListSortIndexAfterDrop> GetListTreeNodeSameLevelBeforFormNodeS(TreeList tl, TreeListNode parentNode, TreeListNode formNode, List<TreeListNode> lDrop)
        {
            List<TreeListSortIndexAfterDrop> lR = new List<TreeListSortIndexAfterDrop>();
            var lNode = parentNode == null ? tl.Nodes : parentNode.Nodes;
            bool isFind = false;
            int i = 0;
            foreach (TreeListNode node in lNode)
            {
                bool isSame = node == formNode;
                bool isDropNode = lDrop.Any(p => p == node);

                if (isSame)
                {
                    isFind = true;
                }

                if (!isFind)
                {
                    if (isDropNode)
                    {
                        var item1 = new TreeListSortIndexAfterDrop
                        {
                            Node = node,
                            RowIndex = i,
                            VisibleIndex = tl.GetVisibleIndexByNode(node),
                            IsDropNode = 3,
                        };
                        lR.Add(item1);
                    }
                    else
                    {
                        var item = new TreeListSortIndexAfterDrop
                        {
                            Node = node,
                            RowIndex = i,
                            VisibleIndex = tl.GetVisibleIndexByNode(node),
                            IsDropNode = 4,
                        };
                        lR.Add(item);//
                    }
                }
                else
                {
                    if (isSame)
                    {
                        var item2 = new TreeListSortIndexAfterDrop
                        {
                            Node = node,
                            RowIndex = i,
                            VisibleIndex = tl.GetVisibleIndexByNode(node),
                            IsDropNode = 1,
                        };
                        lR.Add(item2);
                    }
                    else
                    {
                        if (isDropNode)
                        {
                            var item3 = new TreeListSortIndexAfterDrop
                            {
                                Node = node,
                                RowIndex = i,
                                VisibleIndex = tl.GetVisibleIndexByNode(node),
                                IsDropNode = 2,
                            };
                            lR.Add(item3);
                        }
                        else
                        {
                            var item4 = new TreeListSortIndexAfterDrop
                            {
                                Node = node,
                                RowIndex = i,
                                VisibleIndex = tl.GetVisibleIndexByNode(node),
                                IsDropNode = 0,
                            };
                            lR.Add(item4);
                        }
                    }
                }
                i++;

            }
            if (lR.Count <= 1)
                return lR;
            var q2 = lR.OrderByDescending(p => p.IsDropNode).ThenBy(p => p.RowIndex).Select((p, j) => new TreeListSortIndexAfterDrop
            {
                Node = p.Node,
                RowIndex = j,
                VisibleIndex = p.VisibleIndex,
                IsDropNode = p.IsDropNode,
            }).ToList();
            return q2;
        }
        public static List<TreeListSortIndexAfterDrop> GetListTreeNodeSameLevelBeforFormNode(TreeList tl, TreeListNode parentNode, TreeListNode formNode, List<TreeListNode> lDrop)
        {
            List<TreeListSortIndexAfterDrop> lR = new List<TreeListSortIndexAfterDrop>();
            var lNode = parentNode == null ? tl.Nodes : parentNode.Nodes;
            bool isFind = false;
            int i = 0;
            foreach (TreeListNode node in lNode)
            {
                bool isSame = node == formNode;
                if (isSame)
                {
                    isFind = true;
                    var item = new TreeListSortIndexAfterDrop
                    {
                        Node = node,
                        RowIndex = i,
                        VisibleIndex = tl.GetVisibleIndexByNode(node),
                        IsDropNode = 1,
                    };
                    lR.Add(item);
                    i++;
                }
                if (isFind && !isSame)
                {

                    var q1 = lDrop.Any(p => p == node);

                    var item1 = new TreeListSortIndexAfterDrop
                    {
                        Node = node,
                        RowIndex = i,
                        VisibleIndex = tl.GetVisibleIndexByNode(node),
                        IsDropNode = q1 ? 2 : 0
                    };
                    lR.Add(item1);
                    i++;
                }
            }
            if (lR.Count <= 1)
                return lR;
            var q2 = lR.OrderByDescending(p => p.IsDropNode).ThenBy(p => p.RowIndex).Select((p, j) => new TreeListSortIndexAfterDrop
            {
                Node = p.Node,
                RowIndex = j,
                VisibleIndex = p.VisibleIndex,
                IsDropNode = p.IsDropNode,
            }).ToList();
            return q2;
        }









        #region "Process Delete Key Grid Standard"
        public static void ProcessDeleteKeyFinalOnGrid(GridView gv, Dictionary<CtrlDOrDelFuncCheckInfo, Func<GridView, int, List<object>, bool>> methodsDict)
        {

            //Func<object[], int, string[]> extractMeth = (s, i) => new string[] {"1"};

            if (methodsDict.Count <= 0) return;
            var q1 = gv.GetSelectedCells().ToList();
            var q2Temp = q1.Select(p => new
            {
                p.Column.FieldName
            }).Distinct().ToList();
            var q2 = q2Temp.Join(methodsDict, p => p.FieldName, t => t.Key.FieldName, (p, t) => new
            {
                p.FieldName,
                t.Key.IsCheckFunction,
                t.Key.FuncName,
                t.Key.LstParameter,
                Methold = t.Value

            }).ToList();

            var tlCell = q1.Select(p => p.RowHandle).Distinct().ToList();
            if (!tlCell.Any()) return;



            foreach (var item in q2)
            {
                string fieldName = item.FieldName;

                GridColumn gCol = gv.Columns[fieldName];

                foreach (int rH in tlCell)
                {
                    bool isPaste;
                    if (!item.IsCheckFunction)
                    {
                        isPaste = true;
                    }
                    else
                    {
                        Func<GridView, int, List<object>, bool> method = item.Methold;
                        isPaste = method(gv, rH, item.LstParameter);
                    }
                    if (!isPaste) continue;
                    if (gCol.ColumnType == typeof(bool))
                    {
                        gv.SetRowCellValue(rH, fieldName, false);
                    }
                    else if (gCol.ColumnType == typeof(string))
                    {
                        gv.SetRowCellValue(rH, fieldName, string.Empty);
                    }
                    else if (gCol.ColumnType == typeof(DateTime))
                    {
                        gv.SetRowCellValue(rH, fieldName, new DateTime(1900, 1, 1));
                    }
                    else
                    {
                        gv.SetRowCellValue(rH, fieldName, 0);
                    }
                }
            }
        }
        #endregion

        #region "Control+D standard Grid"


        public static void ProcessControlDkeyFinalOnGrid(GridView gv, Dictionary<CtrlDOrDelFuncCheckInfo, Func<GridView, int, List<object>, bool>> methodsDict)
        {
            if (methodsDict.Count <= 0) return;

            var q1 = gv.GetSelectedCells().ToList();
            var arrColTemp = q1.Select(p => new { p.Column.FieldName }).Distinct().ToList();
            var arrCol = arrColTemp.Join(methodsDict, p => p.FieldName, t => t.Key.FieldName, (p, t) => new
            {
                p.FieldName,
                t.Key.IsCheckFunction,
                t.Key.FuncName,
                t.Key.LstParameter,
                Methold = t.Value

            }).ToList();



            if (arrCol.Count <= 0) return;
            var arrRow = q1.Select(p => p.RowHandle).Distinct().ToList();


            if (arrRow.Count <= 1) return;

            foreach (var item in arrCol)
            {
                string fieldName = item.FieldName;
                object firstValue = null;
                for (int i = 0; i < arrRow.Count; i++)
                {
                    int rH = arrRow[i];
                    if (i == 0)
                    {
                        firstValue = gv.GetRowCellValue(rH, fieldName);
                    }
                    else
                    {
                        bool isPaste;
                        if (!item.IsCheckFunction)
                        {
                            isPaste = true;
                        }
                        else
                        {
                            Func<GridView, int, List<object>, bool> method = item.Methold;
                            isPaste = method(gv, rH, item.LstParameter);
                        }
                        if (!isPaste) continue;
                        gv.SetRowCellValue(rH, fieldName, firstValue);

                    }
                }
            }
        }
        #endregion

        #region "Control + V Standard Grid"
        public static void PasteDataOnClipboardOnGridView(GridView gv, Dictionary<PasteFuncCheckInfo, Func<GridView, int, List<object>, bool>> methodsDict)
        {
            if (methodsDict.Count <= 0) return;

            var q10 = gv.GetSelectedCells().ToList();

            DataTable dt = GetDataTableFromClipBoard(PasteOptionEmpty.RemoveEmptyLast);
            if (dt.Rows.Count <= 0) return;
            var q10Temp = q10.Select(p => p.RowHandle).Where(p => p >= 0).ToList();
            if (!q10Temp.Any()) return;
            int minRow = q10Temp.Min();

            var lNode = Enumerable.Range(minRow, gv.RowCount - minRow).Select((p, i) => new
            {
                Index = i + 1,
                GridRow = p,
            }).ToList();



            int beginCol = q10.Select(p => p.Column.VisibleIndex).Where(p => p >= 0).Min();


            var q1 = gv.VisibleColumns.Select(p => new
            {
                p.VisibleIndex,
                p.FieldName,
            }).Where(p => p.VisibleIndex >= beginCol).Select((p, i) => new
            {
                p.VisibleIndex,
                p.FieldName,
                Index = i + 1
            }).ToList();


            int colLoop = dt.Columns.Count;
            if (q1.Count < colLoop)
            {
                colLoop = q1.Count;
            }

            int rowLoop = dt.Rows.Count;
            if (lNode.Count < rowLoop)
            {
                rowLoop = lNode.Count;
            }

            var qSourceOut = dt.AsEnumerable().Select((p, i) => new
            {
                Index = i + 1,
                Row = p,
            }).ToList().Where(p => p.Index <= rowLoop).ToList();

            var qSourceIn = lNode.Where(p => p.Index <= rowLoop).ToList();

            var qLoop = qSourceOut.Join(qSourceIn, p => p.Index, t => t.Index, (m, n) => new PasteDataGridStd
            {
                GridRow = n.GridRow,
                Row = m.Row,
            }).ToList();

            var qColTemp = q1.Where(p => p.Index <= colLoop).ToList();//Where(p => arrFieldName.Any(s => s == p.FieldName))
            var qCol = qColTemp.Join(methodsDict, p => p.FieldName, t => t.Key.FieldName, (p, t) => new
            {
                p.FieldName,
                p.Index,
                p.VisibleIndex,
                t.Key.IsCheckFunction,
                t.Key.FuncName,
                t.Key.PasteOnMatchType,
                t.Key.LstParameter,
                Methold = t.Value

            }).ToList();
            if (qLoop.Count <= 0) return;

            foreach (var item in qCol)
            {
                string fieldName = item.FieldName;
                int indexData = item.Index - 1;
                GridColumn gCol = gv.Columns[fieldName];
                bool pasteMatchType = item.PasteOnMatchType;
                foreach (var itemQr in qLoop)
                {
                    int rh = itemQr.GridRow;
                    DataRow dr = itemQr.Row;

                    bool isPaste;
                    if (!item.IsCheckFunction)
                    {
                        isPaste = true;
                    }
                    else
                    {
                        Func<GridView, int, List<object>, bool> method = item.Methold;
                        isPaste = method(gv, rh, item.LstParameter);
                    }
                    if (!isPaste) continue;

                    string strValue = GetSafeString(dr[indexData]);

                    if (gCol.ColumnType == typeof(bool))
                    {
                        bool boolValue;
                        bool isBoolType = Boolean.TryParse(strValue, out boolValue);
                        if (!isBoolType)
                        {
                            if (pasteMatchType) continue;
                            gv.SetRowCellValue(rh, fieldName, false);
                        }
                        else
                        {
                            gv.SetRowCellValue(rh, fieldName, boolValue);
                        }

                    }
                    else if (gCol.ColumnType == typeof(string))
                    {
                        gv.SetRowCellValue(rh, fieldName, strValue);
                    }
                    else if (gCol.ColumnType == typeof(DateTime))
                    {
                        DateTime dateValue;
                        bool isDateType = DateTime.TryParse(strValue, out dateValue);
                        if (!isDateType)
                        {
                            if (pasteMatchType) continue;
                            gv.SetRowCellValue(rh, fieldName, new DateTime(1900, 1, 1));
                        }
                        else
                        {
                            gv.SetRowCellValue(rh, fieldName, dateValue);
                        }

                    }
                    else
                    {
                        double doubleValue;
                        bool isDoubleType = double.TryParse(strValue, NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out doubleValue);
                        if (!isDoubleType)
                        {
                            if (pasteMatchType) continue;
                            gv.SetRowCellValue(rh, fieldName, 0);
                        }
                        else
                        {
                            gv.SetRowCellValue(rh, fieldName, doubleValue);
                        }

                    }
                }
            }
        }
        #endregion

        #region "Process Delete Key Grid Standard"
        public static void ClearInfoStandardInGridView(GridView gv, params string[] arrFieldName)
        {

            //Func<object[], int, string[]> extractMeth = (s, i) => new string[] {"1"};

            if (arrFieldName.Length <= 0) return;
            var q1 = gv.GetSelectedCells().ToList();
            var q2 = q1.Select(p => new
            {
                p.Column.FieldName
            }).Where(p => arrFieldName.Any(s => s == p.FieldName)).Distinct().ToList();
            var tlCell = q1.Select(p => p.RowHandle).Distinct().ToList();


            if (!tlCell.Any()) return;



            foreach (var item in q2)
            {
                string fieldName = item.FieldName;

                GridColumn gCol = gv.Columns[fieldName];
                if (gCol.ColumnType == typeof(bool))
                {
                    foreach (int rh1 in tlCell)
                    {
                        gv.SetRowCellValue(rh1, fieldName, false);
                    }
                }
                else if (gCol.ColumnType == typeof(string))
                {
                    foreach (int rh2 in tlCell)
                    {
                        gv.SetRowCellValue(rh2, fieldName, string.Empty);
                    }
                }
                else if (gCol.ColumnType == typeof(DateTime))
                {
                    foreach (int rh3 in tlCell)
                    {
                        gv.SetRowCellValue(rh3, fieldName, new DateTime(1900, 1, 1));
                    }
                }
                else
                {
                    foreach (int rh4 in tlCell)
                    {
                        gv.SetRowCellValue(rh4, fieldName, 0);
                    }
                }




            }




        }
        #endregion

        #region "Control + V Standard Grid"
        public static void PasteDataStandardOnGridView(GridView gv, params string[] arrFieldName)
        {
            if (arrFieldName.Length <= 0) return;
            var q10 = gv.GetSelectedCells().ToList();

            DataTable dt = GetDataTableFromClipBoard(PasteOptionEmpty.RemoveEmptyLast);
            if (dt.Rows.Count <= 0) return;
            var q10Temp = q10.Select(p => p.RowHandle).Where(p => p >= 0).ToList();
            if (!q10Temp.Any()) return;
            int minRow = q10Temp.Min();

            var lNode = Enumerable.Range(minRow, gv.RowCount - minRow).Select((p, i) => new
            {
                Index = i + 1,
                GridRow = p,
            }).ToList();



            int beginCol = q10.Select(p => p.Column.VisibleIndex).Where(p => p >= 0).Min();


            var q1 = gv.VisibleColumns.Select(p => new
            {
                p.VisibleIndex,
                p.FieldName,
            }).Where(p => p.VisibleIndex >= beginCol).Select((p, i) => new
            {
                p.VisibleIndex,
                p.FieldName,
                Index = i + 1
            }).ToList();


            int colLoop = dt.Columns.Count;
            if (q1.Count < colLoop)
            {
                colLoop = q1.Count;
            }

            int rowLoop = dt.Rows.Count;
            if (lNode.Count < rowLoop)
            {
                rowLoop = lNode.Count;
            }

            var qSourceOut = dt.AsEnumerable().Select((p, i) => new
            {
                Index = i + 1,
                Row = p,
            }).ToList().Where(p => p.Index <= rowLoop).ToList();

            var qSourceIn = lNode.Where(p => p.Index <= rowLoop).ToList();

            var qLoop = qSourceOut.Join(qSourceIn, p => p.Index, t => t.Index, (m, n) => new PasteDataGridStd
            {
                GridRow = n.GridRow,
                Row = m.Row,
            }).ToList();

            var qCol = q1.Where(p => p.Index <= colLoop).Where(p => arrFieldName.Any(s => s == p.FieldName)).ToList();

            if (qLoop.Count <= 0) return;

            foreach (var item in qCol)
            {
                string fieldName = item.FieldName;
                int indexData = item.Index - 1;
                GridColumn gCol = gv.Columns[fieldName];
                if (gCol.ColumnType == typeof(bool))
                {
                    foreach (var itemQ2 in qLoop)
                    {
                        int rhQ2 = itemQ2.GridRow;
                        DataRow drQ2 = itemQ2.Row;
                        string strValue = GetSafeString(drQ2[indexData]);
                        bool boolValue;
                        if (!Boolean.TryParse(strValue, out boolValue)) continue;
                        gv.SetRowCellValue(rhQ2, fieldName, boolValue);
                    }
                }
                else if (gCol.ColumnType == typeof(string))
                {
                    foreach (var itemQ5 in qLoop)
                    {
                        int rhQ5 = itemQ5.GridRow;
                        DataRow drQ5 = itemQ5.Row;
                        string strValue = GetSafeString(drQ5[indexData]);
                        gv.SetRowCellValue(rhQ5, fieldName, strValue);
                    }
                }
                else if (gCol.ColumnType == typeof(DateTime))
                {
                    foreach (var itemQ3 in qLoop)
                    {
                        int rhQ3 = itemQ3.GridRow;
                        DataRow drQ3 = itemQ3.Row;
                        string dateString = GetSafeString(drQ3[indexData]);
                        DateTime dResult;
                        if (!DateTime.TryParse(dateString, out dResult)) continue;
                        gv.SetRowCellValue(rhQ3, fieldName, dResult);
                    }
                }
                else
                {
                    foreach (var itemQ4 in qLoop)
                    {
                        int rhQ4 = itemQ4.GridRow;
                        DataRow drQ4 = itemQ4.Row;
                        double dValue4;
                        bool isDouble = double.TryParse(GetSafeString(drQ4[indexData]), NumberStyles.Any,
                            CultureInfo.InvariantCulture.NumberFormat, out dValue4);
                        if (!isDouble) continue;
                        gv.SetRowCellValue(rhQ4, fieldName, dValue4);
                    }
                }


            }
        }
        #endregion

        #region "Control+D standard Grid"


        public static void ProcessControlDkeyStandardGrid(GridView view, params string[] arrFieldName)
        {
            if (arrFieldName.Length <= 0) return;

            var q1 = view.GetSelectedCells().ToList();
            var arrCol = q1.Select(p => new { p.Column.FieldName }).Where(p => arrFieldName.Any(s => s == p.FieldName)).Distinct().ToList();
            var arrRow = q1.Select(p => p.RowHandle).Distinct().ToList();


            if (arrRow.Count <= 1) return;

            foreach (var item in arrCol)
            {
                string fieldName = item.FieldName;
                object firstValue = null;
                for (int i = 0; i < arrRow.Count; i++)
                {
                    int rH = arrRow[i];
                    if (i == 0)
                    {
                        firstValue = view.GetRowCellValue(rH, fieldName);
                    }
                    else
                    {
                        view.SetRowCellValue(rH, fieldName, firstValue);

                    }
                }
            }
        }
        #endregion





        public static int AccentInsensitiveIndexOf(this string source, string subStr)
        {
            return CultureInfo.InvariantCulture.CompareInfo.IndexOf(source, subStr, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
            //  return CultureInfo.InvariantCulture.CompareInfo.IndexOf(source, subStr, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace);

        }

        public static int GetTotalWidth(this GridView gv)
        {
            int totalWidth = gv.VisibleColumns.Select(p => p.Width).Sum();
            int scrollVerticalWidth = 0;

            GridViewInfo viewInfo = gv.GetViewInfo() as GridViewInfo;
            if (viewInfo != null && viewInfo.VScrollBarPresence == ScrollBarPresence.Visible)
            {
                FieldInfo fi = typeof(GridView).GetField("scrollInfo", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fi != null)
                {
                    DevExpress.XtraGrid.Scrolling.ScrollInfo sInfo = (DevExpress.XtraGrid.Scrolling.ScrollInfo)fi.GetValue(gv);
                    Rectangle rect = ((sInfo.VScroll as DevExpress.XtraEditors.IScrollBar).ViewInfo.ThumbButtonBounds);
                    scrollVerticalWidth = rect.Width;
                }

            }
            int toleranceWidth = 4;
            int indicatorWidth = 0;
            if (gv.OptionsView.ShowIndicator)
            {
                indicatorWidth = gv.IndicatorWidth;
            }
            int finalWidth = totalWidth + indicatorWidth + scrollVerticalWidth + toleranceWidth;
            return finalWidth;
        }

        public static void VisibleMenuParentForm(Form parent, bool visible = true)
        {
            if (parent != null)
            {
                var i = ((RibbonForm)parent).Ribbon.Pages.Count - 1;
                while (i-- > 1)
                    ((RibbonForm)parent).Ribbon.Pages[i].Visible = visible;
            }
        }

        public static string GetStringBeforCharacter(this string s, string strCharacter)
        {
            int lengthCharacter = strCharacter.Length;
            if (lengthCharacter <= 0) return s;
            int indexof = s.IndexOf(strCharacter, StringComparison.Ordinal);
            if (indexof < 0) return string.Empty;
            return s.Substring(0, indexof);
        }

        public static string GetStringAfterCharacter(this string s, string strCharacter)
        {
            int lengthCharacter = strCharacter.Length;
            if (lengthCharacter <= 0) return s;
            int indexof = s.IndexOf(strCharacter, StringComparison.Ordinal);
            if (indexof < 0) return string.Empty;
            return s.Substring(indexof + lengthCharacter, s.Length - (indexof + lengthCharacter));
        }

        public static DataTable TableTemplateGridViewSearchGeneral()
        {
            var dt = new DataTable();
            dt.Columns.Add("No", typeof(string));
            dt.Columns.Add("FillterDisplay", typeof(string));
            dt.Columns.Add("FillterValue", typeof(string));
            dt.Columns.Add("Field", typeof(string));
            dt.Columns.Add("FieldValue", typeof(string));
            return dt;
        }
        public static bool IsNodeVisible(TreeListNode node)
        {
            if (node.ParentNode == null)
            {
                foreach (TreeListColumn column in node.TreeList.VisibleColumns)
                {
                    object val = node[column.FieldName];
                    if (val != null && val.ToString().ToUpper().Contains(node.TreeList.FindFilterText.ToUpper()))
                        return true;
                }
                return false;
            }
            return IsNodeVisible(node.ParentNode);
        }




        public static void SetupDefaultMarginSettings(DevExpress.XtraRichEdit.API.Native.Document document, DocumentUnit unit, PaperKind pagerKind,
            bool landsCape, MarginWord margin)
        {
            document.Unit = unit;
            document.Sections[0].Page.PaperKind = pagerKind;
            document.Sections[0].Page.Landscape = landsCape;
            document.Sections[0].Margins.Left = margin.Left;
            document.Sections[0].Margins.Top = margin.Top;
            document.Sections[0].Margins.Bottom = margin.Bottom;
            document.Sections[0].Margins.Right = margin.Right;

        }
        public static void DeleteLastParagraph(DevExpress.XtraRichEdit.API.Native.Document document)
        {
            int count = document.Paragraphs.Count;
            if (count > 1)
            {
                document.AppendDocumentContent(document.Paragraphs[count - 2].Range);
                document.Delete(document.Paragraphs[count - 2].Range);
                document.Delete(document.Paragraphs[count - 2].Range);
            }
        }
        public static int FindRowInGridByColumn(this GridView gv, string searchText, string fieldName)
        {

            ColumnView cv = gv;
            GridColumn colSearch = cv.Columns[fieldName];
            if (colSearch != null)
            {
                int rhFound = cv.LocateByDisplayText(0, colSearch, searchText);
                if (rhFound != GridControl.InvalidRowHandle)
                {
                    return rhFound;
                }
            }
            return -1;

        }

        public static void SetFocusedCellTreeList(this TreeList tl, TreeListNode node, TreeListColumn col)
        {

            tl.FocusedNode = node;
            tl.FocusedColumn = col;
            tl.SelectCell(node, col);
        }


        public static List<TreeListColumn> GetSelectedColumnsTreeList(this TreeList tl)
        {
            List<TreeListColumn> l = tl.GetSelectedCells().Select(p => p.Column).Distinct().ToList();
            return l;
        }

        public static TreeListNode GetLastNodeVisible(this TreeList tl)
        {
            TreeListOperationCount countNodes = new TreeListOperationCount(tl.Nodes);
            tl.NodesIterator.DoOperation(countNodes);
            TreeListNode lastVisibleNode = tl.GetNodeByVisibleIndex(countNodes.Count - 1);
            return lastVisibleNode;

        }

        public static void ResetVisibleIndexOnTreeListNew(this TreeList tl, List<VisibleTreeGirdColumnInfo> lVCol, bool setAll = false)
        {
            if (setAll)
            {
                tl.BeginUpdate();
                foreach (var itemAll in lVCol)
                {
                    tl.Columns[itemAll.FieldName].Fixed = TreeFixedStyle.None;
                }
                foreach (var itemAll in lVCol)
                {
                    tl.Columns[itemAll.FieldName].VisibleIndex = itemAll.VisibleIndex;
                }
                tl.EndUpdate();
                return;
            }
            var q1 = tl.Columns.Where(p => p.VisibleIndex >= 0).Select(p => new
            {
                p.FieldName,
                p.VisibleIndex,
            }).ToList();

            var q2 = (q1.AsEnumerable()
                .Join(lVCol.AsEnumerable(), p => p.FieldName.ToUpper().Trim(), t => t.FieldName.ToUpper().Trim(),
                    (p, t) => new
                    {
                        t.FieldName,
                        t.VisibleIndex,
                    })).OrderBy(p => p.VisibleIndex).Select((p, i) => new
                    {
                        p.FieldName,
                        VisibleIndex = i
                    }).ToList();
            tl.BeginUpdate();
            foreach (var item in q2)
            {

                tl.Columns[item.FieldName].Fixed = TreeFixedStyle.None;
            }
            foreach (var item in q2)
            {
                tl.Columns[item.FieldName].VisibleIndex = item.VisibleIndex;
            }


            tl.EndUpdate();
        }
        public static void ResetVisibleIndexOnGridViewNew(this GridView gv)
        {
            var q1 = gv.Columns.AsEnumerable().Where(p => p.VisibleIndex >= 0).Select(p => new
            {
                p.FieldName,
                p.VisibleIndex,
            }).OrderBy(p => p.VisibleIndex).ToList();
            int index = 0;
            foreach (var item in q1)
            {
                gv.Columns[item.FieldName].VisibleIndex = index;
                index++;
            }

        }
        public static void ResetVisibleIndexOnGridViewNew(this GridView gv, List<VisibleTreeGirdColumnInfo> lVCol, bool setAll = false)
        {
            if (setAll)
            {
                foreach (var itemAll in lVCol)
                {
                    gv.Columns[itemAll.FieldName].Fixed = GridFixedStyle.None;
                }
                foreach (var itemAll in lVCol)
                {
                    gv.Columns[itemAll.FieldName].VisibleIndex = itemAll.VisibleIndex;
                }
                return;
            }
            var q1 = gv.Columns.AsEnumerable().Where(p => p.VisibleIndex >= 0).Select(p => new
            {
                p.FieldName,
                p.VisibleIndex,
            }).ToList();

            var q2 = (q1.AsEnumerable()
                .Join(lVCol.AsEnumerable(), p => p.FieldName.ToUpper().Trim(), t => t.FieldName.ToUpper().Trim(),
                    (p, t) => new
                    {
                        t.FieldName,
                        t.VisibleIndex,
                    })).ToList();
            foreach (var item in q2)
            {
                gv.Columns[item.FieldName].Fixed = GridFixedStyle.None;
            }
            foreach (var item in q2)
            {
                gv.Columns[item.FieldName].VisibleIndex = item.VisibleIndex;
            }
        }

        public static void CheckGridViewClick(MouseEventArgs e, GridView gv)
        {

            GridHitInfo hi = gv.CalcHitInfo(e.Location);
            if (!hi.InRowCell) return;
            GridViewInfo vi = gv.GetViewInfo() as GridViewInfo;
            if (vi == null) return;
            GridCellInfo cellInfo = vi.GetGridCellInfo(hi);
            if (cellInfo == null) return;
            CheckEditViewInfo cInfo = cellInfo.ViewInfo as CheckEditViewInfo;
            if (cInfo == null) return;
            Rectangle r = cInfo.CheckInfo.GlyphRect;
            r.Offset(cellInfo.Bounds.Location);
            if (!r.Contains(e.Location)) return;
            ProcessCheckClick(e, gv, hi);
        }
        private static void ProcessCheckClick(MouseEventArgs e, GridView gv, GridHitInfo hi)
        {
            int rH = hi.RowHandle;
            GridColumn gCol = hi.Column;
            SetFocusedCellOnGrid(gv, rH, gCol);

            gv.ShowEditor();
            CheckEdit edit = gv.ActiveEditor as CheckEdit;
            if (edit == null) return;
            edit.Toggle();
            DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }

        public static Control GetParentContrl(this Control control)
        {
            Control ctrl = control.Parent;
            if (ctrl == null)
            {
                return control;
            }
            return GetParentContrl(ctrl);
        }
        public static bool CheckHexColorRegex(string strInput, bool allowEmpty = true)
        {
            Regex myRegex = new Regex("^#(([0-9a-fA-F]{2}){3}|([0-9a-fA-F]){3})$");
            if (string.IsNullOrEmpty(strInput))
            {
                if (allowEmpty)
                    return true;
                return false;
            }

            return myRegex.IsMatch(strInput);
        }

        public static List<AlphaBetaValueSort> GetListAlphaBetaSort()
        {
            //   S  M L XL
            List<AlphaBetaValueSort> l = new List<AlphaBetaValueSort>
            {
                new AlphaBetaValueSort(".", 0),
                new AlphaBetaValueSort("A", 1),
                new AlphaBetaValueSort("B", 2),
                new AlphaBetaValueSort("C", 3),
                new AlphaBetaValueSort("D", 4),
                new AlphaBetaValueSort("E", 5),
                new AlphaBetaValueSort("F", 6),
                new AlphaBetaValueSort("G", 7),
                new AlphaBetaValueSort("H", 8),
                new AlphaBetaValueSort("I", 9),
                new AlphaBetaValueSort("J", 10),
                new AlphaBetaValueSort("K", 11),
                new AlphaBetaValueSort("L", 12),
                new AlphaBetaValueSort("M", 13),
                new AlphaBetaValueSort("N", 14),
                new AlphaBetaValueSort("O", 15),
                new AlphaBetaValueSort("P", 16),
                new AlphaBetaValueSort("Q", 17),
                new AlphaBetaValueSort("R", 18),
                new AlphaBetaValueSort("S", 19),
                new AlphaBetaValueSort("T", 20),
                new AlphaBetaValueSort("U", 21),
                new AlphaBetaValueSort("V", 22),
                new AlphaBetaValueSort("W", 23),
                new AlphaBetaValueSort("X", 24),
                new AlphaBetaValueSort("Y", 25),
                new AlphaBetaValueSort("Z", 26),
                new AlphaBetaValueSort("0", 27),
                new AlphaBetaValueSort("1", 28),
                new AlphaBetaValueSort("2", 29),
                new AlphaBetaValueSort("3", 30),
                new AlphaBetaValueSort("4", 31),
                new AlphaBetaValueSort("5", 32),
                new AlphaBetaValueSort("6", 33),
                new AlphaBetaValueSort("7", 34),
                new AlphaBetaValueSort("8", 35),
                new AlphaBetaValueSort("9", 36)
            };







            return l;
        }

        /// <summary>
        ///     Find Lost number in range {1,2,3,4,7,8} =>5,6
        /// </summary>
        /// <param name="arr" type="int[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="distanceNumber" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Collections.Generic.IEnumerable<int/> value...
        /// </returns>
        public static IEnumerable<Int32> FindNumberLostInArray(this int[] arr, int distanceNumber)
        {
            if (distanceNumber <= 0)
                yield break;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int number = arr[i];
                int nextNumber = arr[i + 1];
                int dis = nextNumber - number;
                if (dis > distanceNumber)
                {
                    int numReturn = number;
                    int numberCompare = nextNumber - distanceNumber;
                    while (numReturn < numberCompare)
                    {
                        numReturn = numReturn + distanceNumber;
                        yield return numReturn;
                    }
                }

            }

        }
        public static string SortSizeCup(string size, string cup)
        {
            if (!string.IsNullOrEmpty(cup)) return string.Empty;
            if (string.IsNullOrEmpty(size)) return string.Empty;
            if (IsNumber(size)) return string.Empty;
            var array = size.ToCharArray().Select(p => p.ToString()).ToList();

            var q1 = array.Where(IsNumber).ToList();
            int count = q1.Count;
            if (count == 1)
                return RightString(size, size.Length - 1) + " - " + q1.First();
            if (count == 0)
                return size;
            return string.Empty;

        }

        public static double StandardSortSizeCup(string size, string cup)
        {
            try
            {
                if (string.IsNullOrEmpty(size)) return 0;
                double sizeNumber;
                bool isNumberSize = double.TryParse(size, out sizeNumber);
                if (isNumberSize && string.IsNullOrEmpty(cup))
                {
                    const double level0Return = 100000000;
                    return level0Return + sizeNumber;
                }
                List<AlphaBetaValueSort> l = GetListAlphaBetaSort();
                if (!string.IsNullOrEmpty(size) && !string.IsNullOrEmpty(cup))
                {

                    string cupNew = cup;
                    double sizeInt = 0;
                    if (isNumberSize)
                    {
                        sizeInt = sizeNumber * 100;
                    }
                    else
                    {
                        cupNew = size + cup;
                    }
                    double valuetemp = 0;
                    var array = cupNew.ToCharArray().Select(p => p.ToString()).ToList();
                    double dec = 1;
                    foreach (string s in array)
                    {
                        double temp = 0;
                        var q2 = l.Where(p => p.TextValue == s).Select(p => p.IntValue).ToList();
                        if (q2.Any())
                        {
                            temp = q2.First() / dec;
                        }
                        valuetemp += temp;
                        dec = dec * 10;
                    }
                    const double level1Return = 200000000;
                    return level1Return + sizeInt + valuetemp;
                }

                int index1 = size.IndexOf("-", StringComparison.Ordinal);
                if (index1 >= 0)
                {
                    return ConvertSizeDualToDuoble(l, size, index1, false);
                }

                index1 = size.IndexOf("/", StringComparison.Ordinal);
                if (index1 >= 0)
                {
                    return ConvertSizeDualToDuoble(l, size, index1, true);
                }
                const double level2Return = 300000000;
                bool isSizeSmlXl;
                double valueSmlXl = StandardSizeSmlxl(l, level2Return, size, out isSizeSmlXl);

                if (isSizeSmlXl)
                {
                    return valueSmlXl;
                }

                return ConvertSizeLeft(l, size);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }



        }
        private static double ConvertSizeLeft(List<AlphaBetaValueSort> l, string size)
        {
            try
            {
                double valuetemp = 0;
                var array = size.ToCharArray().Select(p => p.ToString()).ToList();
                double dec = 1;
                foreach (string s in array)
                {
                    double temp = 0;
                    var q2 = l.Where(p => p.TextValue == s).Select(p => p.IntValue).ToList();
                    if (q2.Any())
                    {
                        temp = q2.First() / dec;
                    }
                    valuetemp += temp;
                    dec = dec * 10;
                }
                const double level4Return = 400000000;
                return level4Return + valuetemp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }

        }


        private static double ConvertSizeDualToDuoble(List<AlphaBetaValueSort> l, string size, int index1, bool maxLevel)
        {
            try
            {
                double constValue = maxLevel ? 999999999999 : 888888888888;
                string text = size.Substring(0, index1).Trim();
                double sizeNumber;
                bool isNumberSize = double.TryParse(text, out sizeNumber);
                if (isNumberSize)
                {
                    return constValue + sizeNumber * 100;
                }

                bool isSizeSmlXl;

                double returnValue = StandardSizeSmlxl(l, constValue, text, out isSizeSmlXl);
                return returnValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }




        }

        private static double StandardSizeSmlxl(List<AlphaBetaValueSort> l, double constValue, string size, out bool isSizeSmlXl)
        {
            try
            {
                List<AlphaBetaValueSort> l1 = GetListSmlxlSort();
                var q1 = l1.Select(p => new
                {
                    Index = size.IndexOf(p.TextValue, StringComparison.Ordinal),
                    p.IntValue
                }).Where(p => p.Index >= 0).ToList();
                if (!q1.Any())
                {
                    isSizeSmlXl = false;
                    return 0;
                }
                isSizeSmlXl = true;
                var q2 = q1.First();
                double value = q2.IntValue;
                int index = q2.Index;



                var array = size.ToCharArray().Select((p, i) => p.ToString()).ToList();

                double fristValue = 0;

                for (int j = 0; j < array.Count; j++)
                {
                    if (j >= index) break;
                    if (IsNumber(array[j]))
                    {
                        if (value <= 1000)
                        {
                            fristValue -= GetSafeDouble(array[j]);
                        }
                        else
                        {
                            fristValue += GetSafeDouble(array[j]);
                        }

                    }
                    else
                    {
                        if (value <= 1000)
                        {
                            fristValue -= 20;
                        }
                        else
                        {
                            fristValue += 20;
                        }

                    }

                }


                double lastValue = 0;
                double dec = 1;

                for (int k = index + 1; k < array.Count; k++)
                {
                    double temp = 0;
                    var q3 = l.Where(p => p.TextValue == array[k]).Select(p => p.IntValue).ToList();
                    if (q3.Any())
                    {
                        temp = q3.First() / dec;
                    }
                    lastValue += temp;
                    dec = dec * 10;

                }

                return lastValue + value + fristValue + constValue;
            }
            catch (Exception ex)
            {
                isSizeSmlXl = false;
                Console.WriteLine(ex.ToString());
                return 0;
            }





        }

        public static List<AlphaBetaValueSort> GetListSmlxlSort()
        {
            //   S  M L XL
            List<AlphaBetaValueSort> l = new List<AlphaBetaValueSort>
            {
                new AlphaBetaValueSort("S", 1000),
                new AlphaBetaValueSort("M", 2000),
                new AlphaBetaValueSort("L", 3000),
               // new AlphaBetaValueSort("X", 24),
            };
            return l;
        }

        /*
        public static void SetRowColVisibleUseRangeSpreadSheet(this SpreadsheetControl spMain)
        {
            DevExpress.Spreadsheet.Worksheet ws = spMain.ActiveWorksheet;
            DevExpress.Spreadsheet.Range usedRange = ws.GetUsedRange();
            spMain.WorksheetDisplayArea.SetSize(ws.Index, usedRange.ColumnCount, usedRange.RowCount);
        }
        */



        public static List<TreeListNode> GetListTreeListNodesFromNode(this TreeList tl, TreeListNode node, int count, bool getAll = false)
        {
            List<TreeListNode> l = new List<TreeListNode>();
            tl.NodesIterator.DoLocalOperation(new TreeListNodesFromNode(l, node, count, getAll), tl.Nodes);
            return l;
        }

        public static string ConvertColorToHexCode(this Color color)
        {

            return string.Format("#{0}{1}{2}", color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));
        }




        public static Color ConvertHexCodeToColor(this string hexCode)
        {

            Color color = ColorTranslator.FromHtml(hexCode);
            return color;
        }

        public static List<GridBand> GetAllBandGridViewSelectedColumn(this BandedGridView gv)
        {
            BandedGridColumn[] arrCol = gv.GetSelectedCells().Select(p => (BandedGridColumn)p.Column).ToArray();
            List<GridBand> lBand = arrCol.Where(p => p.OwnerBand != null).Select(p => p.OwnerBand).ToList();

            var q1 = lBand.Where(p => p.ParentBand != null).ToList();
            if (q1.Any())
            {
                lBand.AddRange(q1.SelectMany(gBand => gBand.GetAllParentBands()));
            }
            return lBand;
        }

        public static List<GridBand> GetAllParentBands(this GridBand childBand)
        {
            List<GridBand> result = new List<GridBand>();
            AddParentBands(childBand, result);
            return result;
        }

        private static void AddParentBands(GridBand childBand, List<GridBand> list)
        {
            GridBand parentBand = childBand.ParentBand;
            if (parentBand == null)
                return;
            list.Add(parentBand);
            AddParentBands(parentBand, list);
        }
        public static IEnumerable<GridBand> GetAllVisibleChildBandMinLevel(this GridBand parentBand)
        {
            foreach (GridBand band in parentBand.Children)
            {
                if (!band.HasChildren && band.Visible)
                {
                    yield return band;
                }

                if (band.HasChildren)
                {
                    foreach (GridBand childBand in band.GetAllVisibleChildBandMinLevel())
                    {
                        if (!childBand.HasChildren && childBand.Visible)
                        {
                            yield return childBand;
                        }
                    }
                }
            }
        }


        public static IEnumerable<BandedGridColumn> GetAllVisibleColumnInGridBandMinLevel(this GridBand parentBand)
        {
            return parentBand.GetAllVisibleChildBandMinLevel().Select(band => band.Columns[0]).Distinct().OrderBy(p => p.VisibleIndex);
        }

        public static IEnumerable<TreeListNode> GetAllChildNodeBoM(this TreeListNode tlNode, string fieldName, object value)
        {

            foreach (TreeListNode node in tlNode.Nodes.Where(p => p.GetValue(fieldName) == value))
            {
                yield return node;
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeListNode childNode in node.GetAllChildNodeBoM(fieldName, value))
                    {
                        yield return childNode;
                    }
                }
            }
        }


        public static TreeListNode GetParentNodeSameTypeBoM(this TreeListNode tlNode, string fieldName, object value)
        {

            TreeListNode parentNode = tlNode.ParentNode;
            if (parentNode == null) return null;

            object value1 = parentNode.GetValue(fieldName);
            if (value1.Equals(value))
            {
                return parentNode;

            }
            return parentNode.GetParentNodeSameTypeBoM(fieldName, value);
        }

        public static TreeListNode GetParentNodeSameTypeHBoM(this TreeListNode tlNode, string fieldName, string itemType)
        {

            if (tlNode == null)
                return null;
            if (GetSafeString(tlNode.GetValue(fieldName)) == itemType)
                return tlNode;
            TreeListNode parentNode = tlNode.ParentNode;
            if (parentNode == null) return null;

    
            if (GetSafeString(parentNode.GetValue(fieldName)) == itemType)
            {
                return parentNode;

            }
            return parentNode.GetParentNodeSameTypeHBoM(fieldName, itemType);
        }



        public static TreeListNode GetParentNodeDiffTypeBoM(this TreeListNode tlNode, string fieldName, object value)
        {

            TreeListNode parentNode = tlNode.ParentNode;
            if (parentNode == null) return null;

            object value1 = parentNode.GetValue(fieldName);
            if (!value1.Equals(value))
            {
                return parentNode;
            }


            return parentNode.GetParentNodeDiffTypeBoM(fieldName, value);
        }

        public static IEnumerable<TreeListNode> GetAllChildNode(this TreeListNode tlNode)
        {
            foreach (TreeListNode node in tlNode.Nodes)
            {
                yield return node;
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeListNode childNode in node.GetAllChildNode())
                    {
                        yield return childNode;
                    }
                }
            }
        }


        public static bool CheckTreeRemoveAttCol(this TreeList tl, string fieldName, bool valueCheck)
        {

            foreach (TreeListNode node in tl.Nodes)
            {



                if (!CheckTreeRemoveAttCol(node, fieldName, valueCheck))
                {
                    return false;
                }
            }
            return true;
        }


        private static bool CheckTreeRemoveAttCol(TreeListNode tlNode, string fieldName, bool valueCheck)
        {

            if (GetSafeBool(tlNode.GetValue(fieldName)) == valueCheck)
            {
                return false;
            }
            foreach (TreeListNode node in tlNode.Nodes)
            {
                bool valueReturn = CheckTreeRemoveAttCol(node, fieldName, valueCheck);
                if (!valueReturn)
                {
                    return false;
                }
            }
            return true;
        }








        public static List<TreeListNode> GetAllNodeTreeList(this TreeList tl)
        {
            List<TreeListNode> result = new List<TreeListNode>();
            foreach (TreeListNode node in tl.Nodes)
            {
                result.Add(node);
                var q1 = node.GetAllChildNode().ToArray();
                if (q1.Any())
                {
                    result.AddRange(q1);
                }
            }
            return result;
        }




        public static string[] GetArrayColumnInTable(this DataTable dt)
        {
            if (dt == null) return null;
            return dt.Columns.Cast<DataColumn>().Select(p => p.ColumnName).ToArray();

        }




        public static List<TreeListNode> GetAllParentNode(this TreeListNode tlNode)
        {
            List<TreeListNode> result = new List<TreeListNode>();
            AddParentNode(tlNode, result);
            return result;
        }

        private static void AddParentNode(TreeListNode node, List<TreeListNode> lNode)
        {
            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null)
                return;
            lNode.Add(parentNode);
            AddParentNode(parentNode, lNode);
        }



        public static List<TreeListNode> GetAllNodeTreeListNoChild(this TreeList tl)
        {
            List<TreeListNode> result = new List<TreeListNode>();
            foreach (TreeListNode node in tl.Nodes)
            {
                if (!node.HasChildren)
                {
                    result.Add(node);
                    continue;
                }

                var q1 = node.GetAllChildNodeNoChild().ToArray();
                if (q1.Any())
                {
                    result.AddRange(q1);
                }
            }
            return result;
        }

        private static IEnumerable<TreeListNode> GetAllChildNodeNoChild(this TreeListNode tlNode)
        {
            foreach (TreeListNode node in tlNode.Nodes)
            {
                if (!node.HasChildren)
                    yield return node;
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeListNode childNode in node.GetAllChildNodeNoChild())
                    {
                        if (!childNode.HasChildren)
                            yield return childNode;
                    }
                }
            }
        }

        public static void DisableColumnMove(TreeList tl)
        {



            for (int i = 0; i < tl.VisibleColumns.Count; i++)
            {
                tl.VisibleColumns[i].OptionsColumn.AllowMove = false;
            }
        }

        public static IEnumerable<int> AnalyzeArrayToReportMultiColumn(this int[] arrValue, int colNo)
        {
            List<int> l = new List<int>();
            int length = arrValue.Length;
            if (length <= 0)
                return l;
            if (colNo > length)
                return l;
            if (length % colNo != 0)
                return l;

            int minValue = arrValue.Min();
            var arrCheck = Enumerable.Range(minValue, length);


            var qCh = arrCheck.Except(arrValue).ToList();
            if (qCh.Any())
                return l;

            DataTable dtTemp = new DataTable();
            for (int i = 0; i < colNo; i++)
            {
                dtTemp.Columns.Add(i.ToString(), typeof(int));
            }

            int row = length / colNo;

            for (int j = 0; j < row; j++)
            {
                DataRow dr = dtTemp.NewRow();
                dr[0] = minValue;
                int temp = minValue;
                for (int k = 1; k < colNo; k++)
                {
                    temp = temp + row;
                    dr[k] = temp;
                }
                dtTemp.Rows.Add(dr);
                minValue++;


            }

            l = dtTemp.AsEnumerable().SelectMany(p => p.ItemArray).Select(t => (int)t).ToList();
            return l;

        }




        public static void LoadDataOnComboBox(ComboBoxEdit cb, TextEditStyles textTyles, int selectedIndex, params object[] arrValue)
        {
            cb.Properties.Items.Clear();
            foreach (var item in arrValue)
            {
                cb.Properties.Items.Add(item);
            }
            cb.Properties.TextEditStyle = textTyles;
            if (selectedIndex >= 0)
            {
                cb.SelectedIndex = selectedIndex;
            }

        }
        public static bool IsReloadDataTableFormDb(string connectString,DateTime d , string tableName)
        {
            AccessData accData = new AccessData(connectString);
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@date", SqlDbType.DateTime) { Value = d };
            arrpara[1] = new SqlParameter("@TableName", SqlDbType.NVarChar) { Value = tableName };
            return accData.TblReadDataSP("sp_GetTableLastUpdate", arrpara).Rows.Count > 0;
        }

        public static void LoadBomAssignInfo(string userName, string connectString)
        {
            AccessData accData = new AccessData(connectString);
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = userName };
            DeclareSystem.DtBoMAssignMain = accData.TblReadDataSP("sp_LoadUserAssignBOMWhenLogin", arrpara);
            DeclareSystem.DateGetBoMAssignMain = GetServerDate();
        }


        public static void OpenHelpForm(string menuCode, bool isGuideDocument)
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            string sql = string.Empty;
            if (isGuideDocument)
            {
                sql = string.Format("SELECT FormName,GuideDocument as FileData FROM dbo.ListMenu WHERE MenuCode = '{0}'", menuCode.Trim());
            }
            else
            {
                sql = string.Format("SELECT FormName,ProcessDocument as FileData FROM dbo.ListMenu WHERE MenuCode = '{0}'", menuCode.Trim());
            }
            DataTable dtCh = accData.TblReadDataSQL(sql, null);
            int r = dtCh.Rows.Count;
            if (r <= 0) return;
            if (dtCh.Rows[0]["FileData"] == DBNull.Value) return;
            byte[] fileData = (byte[])dtCh.Rows[0]["FileData"];
            if (fileData == null) return;
            string formName = GetSafeString(dtCh.Rows[0]["FormName"]);
            formName = string.Format(isGuideDocument ? "Guide Module : {0}" : "Process Module : {0}", formName);


            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => String.Equals(f.Name.Trim(), "FrmHelpFinal", StringComparison.CurrentCultureIgnoreCase)
                && String.Equals(f.Text.Trim(), formName.Trim(), StringComparison.CurrentCultureIgnoreCase));

            if (frm == null)
            {
                MemoryStream stream = new MemoryStream(fileData);
                var fNew = new FrmHelpFinal { Text = formName };
                fNew.LoadDocument(stream);
                fNew.Show();
            }
            else
            {
                frm.Activate();
            }



        }


        public static byte[] ConvertFileToByteArray(string path)
        {
            FileStream fStream = File.OpenRead(path);
            byte[] contents = new byte[fStream.Length];
            fStream.Read(contents, 0, (int)fStream.Length);
            fStream.Close();
            return contents;
        }

        /// <summary>
        ///     1,2,4,5,7 => {1,2},{4,5},{7,7}
        /// </summary>
        /// <param name="arrNumber" type="int[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Collections.Generic.Dictionary<int,int="" />
        ///     value...
        /// </returns>
        public static Dictionary<Int32, Int32> AnalyzeArrayToRange(params Int32[] arrNumber)
        {
            Dictionary<Int32, Int32> dic = new Dictionary<int, int>();

            if (arrNumber.Length <= 0)
                return dic;
            if (arrNumber.Length == 1)
            {
                dic.Add(arrNumber[0], arrNumber[0]);
                return dic;
            }

            Int32[] q1 = arrNumber.OrderBy(p => p).ToArray();
            int length = q1.Length - 1;
            int fristTemp = q1[0];
            int nextTemp = q1[0];
            for (int i = 0; i < length; i++)
            {
                int frist = q1[i];
                int next = q1[i + 1];
                if (i == length - 1)
                {
                    if (next - frist != 1)
                    {
                        dic.Add(fristTemp, nextTemp);
                        dic.Add(next, next);
                    }
                    else
                    {
                        dic.Add(fristTemp, next);

                    }
                }
                else
                {
                    if (next - frist != 1)
                    {
                        dic.Add(fristTemp, nextTemp);
                        fristTemp = next;
                        nextTemp = next;
                    }
                    else
                    {
                        nextTemp = next;
                    }
                }

            }
            return dic;
        }

        /// <summary>
        ///     Convert Number To Collection Object (190,100) => 100,90
        /// </summary>
        /// <param name="totalRecordInput"></param>
        /// <param name="pagingItem" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Collections.Generic.IEnumerable<int/> value...
        /// </returns>
        public static IEnumerable<Int32> SplitNumberToCollection(this int totalRecordInput, int pagingItem)
        {
            Int32 totalRecord = 0;
            if (totalRecordInput > 0)
            {
                totalRecord = totalRecordInput;
            }
            while (true)
            {
                var s = totalRecord - pagingItem;
                if (s > 0)
                {
                    yield return pagingItem;
                }
                else
                {
                    yield return totalRecord;
                    yield break;
                }
                totalRecord -= pagingItem;
            }
        }

        public static Boolean CheckStringInString(string strSource, string strCheck, string delimiterSource, string delimiterCheck, Boolean strSoruceEmpty = false)
        {
            if (strSource.ToUpper() == strCheck.ToUpper())
                return true;
            if (string.IsNullOrEmpty(strSource))
            {
                if (strSoruceEmpty)
                    return true;
                return false;
            }
            if (string.IsNullOrEmpty(strCheck))
                return false;
            var arrSource = strSource.Split(new[] { delimiterSource }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToUpper().Trim()).Distinct().ToList();
            var arrCheck = strCheck.Split(new[] { delimiterCheck }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToUpper().Trim()).Distinct().ToList();
            var q1 = arrCheck.Join(arrSource, p => p, t => t, (p, t) => p).ToList();
            return q1.Count == arrCheck.Count();
        }


        public static Int32 GetMaxRowSelectedOnGrid(this GridView gv)
        {
            var q1 = gv.GetSelectedRows();
            if (q1.Any())
                return q1.Max();
            return -1;
        }

        public static Int32 GetMinRowSelectedOnGrid(this GridView gv)
        {
            var q1 = gv.GetSelectedRows();
            if (q1.Any())
                return q1.Min();
            return -1;
        }


        public static bool CompareCollections<T>(this IEnumerable<T> lFrist, IEnumerable<T> lSecond)
        {
            var frist = lFrist as IList<T> ?? lFrist.ToList();
            var second = lSecond as IList<T> ?? lSecond.ToList();
            if (frist.Count != second.Count)
                return false;
            bool result = frist.Except(second).Union(second.Except(frist)).Any();
            return !result;
        }
        public static IEnumerable<String> GetAllColumnNameOfDataTable(this DataTable dt)
        {
            return Enumerable.Range(0, dt.Columns.Count).Select(p => dt.Columns[p].ColumnName.Trim());
        }

        public static DataTable LoadListGeneralInfoTableSY24(string compCode, string codeFile)
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            return accData.TblReadDataSQL(string.Format("SELECT RTrim(LTrim(SY24002)) AS Code,RTrim(Ltrim(SY24003)) AS Name FROM SY24{0}00 (NOLOCK) WHERE SY24001='{1}'  ORDER BY Code"
                , compCode, codeFile), null);
        }

        public static bool IsEditorFocusControl(Control control)
        {
            if (control is TreeList)
                return true;
            if (control is TextEdit)
                return true;
            if (control is GridControl)
                return true;
            if (control is LookUpEdit)
                return true;
            if (control is SearchLookUpEdit)
                return true;
            if (control is SpinEdit)
                return true;
            if (control is DateEdit)
                return true;
            if (control is ToggleSwitch)
                return true;
            if (control is CheckEdit)
                return true;
            if (control is XtraUCRichText)
                return true;
            if (control is MemoEdit)
                return true;
            return false;
        }

        /*
         
        public static IEnumerable<Int32> CreateArray(this Int32 number)
        {
            for (int i = 0; i < number; i++)
            {
                yield return i;
            }
         //
        }
        */


        public static void RemoveAllDataTableOnDataSet(this DataSet ds)
        {

            for (int i = ds.Tables.Count - 1; i >= 0; i--)
            {
                ds.Tables.RemoveAt(i);
            }
        }

        public static Bitmap DrawControlToBitmap(Control control)
        {
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle rect = control.RectangleToScreen(control.ClientRectangle);
            graphics.CopyFromScreen(rect.Location, System.Drawing.Point.Empty, control.Size);
            return bitmap;
        }


        /// <summary>
        ///     Function to test for Positive Integers.
        /// </summary>
        /// <param name="strNumber" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool IsNaturalNumber(String strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) && objNaturalPattern.IsMatch(strNumber);
        }
        /// <summary>
        ///     Function to test for Positive Integers with zero inclusive
        /// </summary>
        /// <param name="strNumber" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }
        /// <summary>
        ///     Function to Test for Integers both Positive & Negative
        /// </summary>
        /// <param name="strNumber" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool IsInteger(String strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
        }
        /// <summary>
        ///     Function to Test for Positive Number both Integer & Real
        /// </summary>
        /// <param name="strNumber" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool IsPositiveNumber(String strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(strNumber) && objPositivePattern.IsMatch(strNumber) && !objTwoDotPattern.IsMatch(strNumber);
        }
        /// <summary>
        ///     Function to test whether the string is valid number or not
        /// </summary>
        /// <param name="strNumber" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) && !objTwoDotPattern.IsMatch(strNumber) && !objTwoMinusPattern.IsMatch(strNumber) && objNumberPattern.IsMatch(strNumber);
        }
        /// <summary>
        ///     Function To test for Alphabets.
        /// </summary>
        /// <param name="strToCheck" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns> 
        public static bool IsAlpha(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }
        /// <summary>
        ///     Function to Check for AlphaNumeric.
        /// </summary>
        /// <param name="strToCheck" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool IsAlphaNumeric(String strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        #region "Progress bar diaglog"
        public static void StartProgressiveOperation(
           IProgressiveOperation operation,
           IWin32Window owner)
        {
            FrmModalProgressForm f =
                new FrmModalProgressForm(operation);

            f.ShowDialog(owner);
        }

        public static void StartProgressiveOperation(
        ICompositeProgressiveOperation operation,
        IWin32Window owner)
        {
            FrmModalCompositeProgressForm f =
                new FrmModalCompositeProgressForm(operation);

            f.ShowDialog(owner);
        }
        #endregion

        #region "Process Excel File"

        public static DateTime ConvertDateTimeLoadExcelFile(object value)
        {
            if (value == null)
                return new DateTime(1900, 1, 1);
            DateTime d = GetSafeDatetimeNullableReturnMinDate(value);
            if (d.Year == 1900)
                return new DateTime(1900, 1, 1);
            return d;
        }

        /// <summary>
        ///      Get Excel OLEDB connection string
        /// </summary>
        /// <param name="strSourcePath" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string GetConnectionStringExcel(string strSourcePath)
        {
            return string.Format(Path.GetExtension(strSourcePath).ToLower().Trim() == ".xls" ?
               "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;" :
               "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;",
               strSourcePath);
        }

        /// <summary>
        ///      Read Data from Excel File Into OleDBDataReader
        /// </summary>
        /// <param name="strPath" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="sheetname" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.OleDb.OleDbDataReader value...
        /// </returns>
        public static OleDbDataReader OleDbReadDataFromExcel(string strPath, string sheetname)
        {
            //string connectionstring = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", strPath);
            var connect = new OleDbConnection { ConnectionString = GetConnectionStringExcel(strPath) };
            var command = new OleDbCommand(String.Format("select * from [{0}]", sheetname), connect);
            try
            {
                connect.Open();
                OleDbDataReader oldbDr = command.ExecuteReader();
                connect.Close();
                return oldbDr;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            finally
            {
                if (connect.State != ConnectionState.Closed) { connect.Close(); }
                connect.Dispose();
                command.Dispose();
            }
            return null;
        }

        /// <summary>
        ///     Read Data from Excel File Into DataTable
        /// </summary>
        /// <param name="extension" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strPath" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="sheetname" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        public static DataTable TableReadDataFromExcel(string strPath, string sheetname)
        {
            var dtexcel = new DataTable();
            var connect = new OleDbConnection { ConnectionString = GetConnectionStringExcel(strPath) };
            var daexcel = new OleDbDataAdapter(String.Format("select * from [{0}]", sheetname), connect);
            try
            {

                connect.Open();
                daexcel.Fill(dtexcel);
                connect.Close();
                return dtexcel;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            finally
            {
                if (connect.State != ConnectionState.Closed) { connect.Close(); }
                connect.Dispose();
                daexcel.Dispose();
            }
            return null;

        }

        /// <summary>
        ///     Get Sheets Names On Excel File
        /// </summary>
        /// <param name="extension" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strPath" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string[] value...
        /// </returns>
        public static string[] GetExcelSheetNames(string strPath)
        {
            OleDbConnection objConn = null;
            DataTable dt = null;
            //  string sheetnames = "";
            try
            {
                objConn = new OleDbConnection(GetConnectionStringExcel(strPath));
                // Open connection with the database.
                objConn.Open();
                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                var excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                objConn.Close();
                return excelSheets;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            finally
            {
                if (objConn != null && objConn.State != ConnectionState.Closed)
                {
                    objConn.Close();
                    objConn.Dispose();
                    if (dt != null) dt.Dispose();
                }

            }
            return null;
        }


        public static DataSet ExtractImageFileToDataSet(string moduleName, string imagePath)
        {
            bool isError = false;
            string pathExcel = string.Format(@"{0}\{1}{2:ddMMyyyyhhmmss}.xls", Application.StartupPath, moduleName, DateTime.Now);
            DataSet ds = new DataSet("ExtractImageFile");
            try
            {

                TP_File file = TP_Stenography.DecodeImage((Bitmap)Image.FromFile(imagePath), string.Empty);
                file.Save(pathExcel);
                string[] excelSheets = GetExcelSheetNames(pathExcel);
                foreach (string sheetName in excelSheets)
                {
                    DataTable dt = TableReadDataFromExcel(pathExcel, sheetName);
                    dt.TableName = sheetName;
                    ds.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {
                isError = true;
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

            }
            finally
            {
                if (File.Exists(pathExcel))
                {
                    File.Delete(pathExcel);
                }
            }
            if (isError)
                return null;
            return ds;

        }

        public static DataSet ExtractImageFileToDataSet(string moduleName, string imagePath, SpreadsheetControl spMain)
        {
            bool isError = false;
            string pathExcel = string.Format(@"{0}\{1}{2:ddMMyyyyhhmmss}.xls", Application.StartupPath, moduleName, DateTime.Now);
            DataSet ds = new DataSet("ExtractImageFile");
            try
            {

                TP_File file = TP_Stenography.DecodeImage((Bitmap)Image.FromFile(imagePath), string.Empty);
                file.Save(pathExcel);


                string[] excelSheets = GetExcelSheetNames(pathExcel);
                foreach (string sheetName in excelSheets)
                {
                    DataTable dt = TableReadDataFromExcel(pathExcel, sheetName);
                    dt.TableName = sheetName;
                    ds.Tables.Add(dt);
                }
                spMain.LoadDocument(pathExcel);
            }
            catch (Exception ex)
            {
                isError = true;
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

            }
            finally
            {
                if (File.Exists(pathExcel))
                {
                    File.Delete(pathExcel);
                }
            }
            if (isError)
                return null;
            return ds;

        }

        public static DataTable ReadExcelFileToDatatable(string pathExcel)
        {

            try
            {
                SpreadsheetControl spMain = new SpreadsheetControl();
                spMain.LoadDocument(pathExcel);
                Worksheet ws = spMain.Document.Worksheets[0];
                var range = ws.GetUsedRange();
                DataTable dt = ws.CreateDataTable(range, true);
                DevExpress.Spreadsheet.Export.DataTableExporter exporter = ws.CreateDataTableExporter(range, dt, true);
                exporter.Export();
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static bool ExportExcelToImageFile(string imageName, string savePath, DataSet ds)
        {
            Int32 countTable = ds.Tables.Count;
            if (countTable <= 0)
            {
                XtraMessageBox.Show("Null Datasource...!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return false;
            }
            string pathExcel = string.Format(@"{0}\System{1:ddMMyyyyhhmmss}.xls", Application.StartupPath, DateTime.Now);
            string pathImage = string.Format(@"{0}\{1}", Application.StartupPath, imageName);
            if (!File.Exists(pathImage))
            {
                XtraMessageBox.Show("System Files could not detect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return false;
            }
            if (File.Exists(pathExcel))
            {
                File.Delete(pathExcel);
            }


            SpreadsheetControl spMain = new SpreadsheetControl();
            IWorkbook workbook = spMain.Document;


            for (int i = workbook.Worksheets.Count - 2; i >= 0; i--)
            {
                workbook.Worksheets.RemoveAt(i);
            }

            foreach (DataTable dt1 in ds.Tables)
            {
                string sheetName = dt1.TableName.Trim();
                workbook.Worksheets.Add(sheetName);
            }

            workbook.Worksheets.RemoveAt(0);
            for (int w = 0; w < ds.Tables.Count; w++)
            {
                workbook.Worksheets.ActiveWorksheet = workbook.Worksheets[w];
                Worksheet ws = workbook.Worksheets[w];
                DataTable dtExport = ds.Tables[w];

                ws.Import(dtExport, true, 0, 0);
                for (int i = 0; i < dtExport.Columns.Count; i++)
                {
                    ws.Columns[i].AutoFit();
                    ws.Columns[i].Width += 10;

                }
            }
            spMain.SaveDocument(pathExcel, DocumentFormat.Xls);

            bool result = true;
            try
            {




                Image originalImage = Image.FromFile(pathImage);


                Size scale = GetImageScale(originalImage, pathExcel);



                var image = new Bitmap(scale.Width, scale.Height);
                Graphics g = Graphics.FromImage(image);
                g.DrawImage(originalImage, 0, 0, scale.Width, scale.Height);

                TP_Stenography.EncodeImage(image, new TP_File(pathExcel, string.Empty), 1);
                image.Save(savePath, ImageFormat.Bmp);
                if (File.Exists(pathExcel))
                {
                    File.Delete(pathExcel);
                }
            }
            catch (FileTooLargeForImageException)
            {
                XtraMessageBox.Show("The image is not large enough to hold that file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }

            return result;
        }


        public static bool ExportExcelToImageFile(string imageName, string savePath, DataSet ds, object currentObject)
        {
            Int32 countTable = ds.Tables.Count;
            if (countTable <= 0)
            {
                XtraMessageBox.Show("Null Datasource...!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return false;
            }
            string pathExcel = string.Format(@"{0}\System{1:ddMMyyyyhhmmss}.xls", Application.StartupPath, DateTime.Now);
            string pathImage = string.Format(@"{0}\{1}", Application.StartupPath, imageName);
            if (!File.Exists(pathImage))
            {
                XtraMessageBox.Show("System Files could not detect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return false;
            }
            if (File.Exists(pathExcel))
            {
                File.Delete(pathExcel);
            }
            bool result = true;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            xlWorkbook.CheckCompatibility = false;
            //doi tuong Trống để thêm vào xlApp sau đó lưu lại sau
            object missValue = Missing.Value;
            xlApp.DisplayAlerts = false;
            xlApp.Visible = false;

            try
            {
                for (int v = countTable - 1; v >= 0; v--)
                {
                    DataTable dt = ds.Tables[v];
                    int rowNo = dt.Rows.Count;
                    int columnNo = dt.Columns.Count;
                    int colIndex = 0;

                    //Create Excel Sheets
                    var xlSheets = xlWorkbook.Sheets;
                    var xlWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);

                    xlWorksheet.Name = dt.TableName;

                    //Generate Field Names
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        colIndex++;
                        xlApp.Cells[1, colIndex] = dataColumn.ColumnName;
                    }

                    object[,] objData = new object[rowNo, columnNo];

                    //Convert DataSet to Cell Data
                    for (int row = 0; row < rowNo; row++)
                    {
                        for (int col = 0; col < columnNo; col++)
                        {
                            objData[row, col] = dt.Rows[row][col];
                        }

                    }


                    //Format Data Type of Columns 

                    colIndex = 0;
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        colIndex++;
                        string format = "@";
                        switch (dataColumn.DataType.Name)
                        {
                            case "Boolean":
                                break;
                            case "Byte":
                                break;
                            case "Char":
                                break;
                            case "DateTime":
                                format = "DD/MM/YYYY";
                                break;
                            case "Decimal":
                                format = "$* #,##0.00;[Red]-$* #,##0.00";
                                break;
                            case "Double":
                                break;
                            case "Int16":
                                format = "0";
                                break;
                            case "Int32":
                                format = "0";
                                break;
                            case "Int64":
                                format = "0";
                                break;
                            case "SByte":
                                break;
                            case "Single":
                                break;
                            case "TimeSpan":
                                break;
                            case "UInt16":
                                break;
                            case "UInt32":
                                break;
                            case "UInt64":
                                break;
                            default:
                                format = "@";
                                break;
                        }
                        //Format the Column accodring to Data Type
                        xlWorksheet.Range[xlApp.Cells[2, colIndex], xlApp.Cells[rowNo + 1, colIndex]].NumberFormat = format;

                    }



                    //Add the Data

                    Microsoft.Office.Interop.Excel.Range range = xlWorksheet.Range[xlApp.Cells[2, 1], xlApp.Cells[rowNo + 1, columnNo]];
                    range.Value2 = objData;

                    //format border
                    Microsoft.Office.Interop.Excel.Range rangeBroder = xlWorksheet.Range[xlApp.Cells[1, 1], xlApp.Cells[rowNo + 1, columnNo]];
                    rangeBroder.EntireColumn.AutoFit();
                    Microsoft.Office.Interop.Excel.Borders border = rangeBroder.Borders;
                    border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    border.Weight = 2d;


                    Microsoft.Office.Interop.Excel.Range rangeHeader = xlWorksheet.Range[xlApp.Cells[1, 1], xlApp.Cells[1, columnNo]];
                    FormattingExcelCells(rangeHeader, "#000099", Color.White, true);

                }

                //Remove the Default Worksheet
                ((Microsoft.Office.Interop.Excel.Worksheet)xlApp.ActiveWorkbook.Sheets[xlApp.ActiveWorkbook.Sheets.Count]).Delete();



                //save file
                xlWorkbook.SaveAs(pathExcel, XlFileFormat.xlWorkbookNormal, missValue, missValue, missValue, missValue, XlSaveAsAccessMode.xlExclusive, missValue, missValue, missValue, missValue, missValue);
                xlWorkbook.Close(true, missValue, missValue);
                xlApp.Quit();
                ReleaseObject(xlWorkbook);
                ReleaseObject(xlApp);



            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            finally
            {
                foreach (Process pro in Process.GetProcessesByName("EXCEL"))
                {
                    pro.Kill();
                }
            }
            GC.SuppressFinalize(currentObject);


            try
            {

                // Format the image for encoding
                Image originalImage = Image.FromFile(pathImage);
                // See if we need to resize the image
                Size scale = GetImageScale(originalImage, pathExcel);
                // Get a bitmap of the correct size
                var image = new Bitmap(scale.Width, scale.Height);
                Graphics g = Graphics.FromImage(image);
                g.DrawImage(originalImage, 0, 0, scale.Width, scale.Height);
                // Encode and save
                TP_Stenography.EncodeImage(image, new TP_File(pathExcel, string.Empty), 1);
                image.Save(savePath, ImageFormat.Bmp);
                if (File.Exists(pathExcel))
                {
                    File.Delete(pathExcel);
                }
            }
            catch (FileTooLargeForImageException)
            {
                XtraMessageBox.Show("The image is not large enough to hold that file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }

            return result;
        }




        /// <summary>
        /// FUNCTION FOR FORMATTING EXCEL CELLS
        /// </summary>
        /// <param name="range"></param>
        /// <param name="htmLcolorCode"></param>
        /// <param name="fontColor"></param>
        /// <param name="isFontbool"></param>
        private static void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string htmLcolorCode, Color fontColor, bool isFontbool)
        {
            range.Interior.Color = ColorTranslator.FromHtml(htmLcolorCode);
            range.Font.Color = ColorTranslator.ToOle(fontColor);
            if (isFontbool)
            {
                range.Font.Bold = true;
            }
        }
        private static void ReleaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occured while releasing object " + ex);
            }
            finally
            {
                GC.Collect();
            }
        }

        private static Size GetImageScale(Image image, string fileName)
        {
            double width = image.Width;
            double height = image.Height;
            double size = GetFileSize(fileName);
            double scale = height / width;
            double newWidth = Math.Sqrt((8.0 * size) / (3.0 * 1 * scale)) + 5.0; // add a 5 pixel buffer to be safe
            double newHeight = newWidth * scale;
            return new Size((int)newWidth, (int)newHeight);
        }
        private static long GetFileSize(string fileName)
        {
            if (File.Exists(fileName))
            {
                var info = new FileInfo(fileName);
                return info.Length;
            }
            return 0L;
        }

        #endregion

        public static void SetFocusedRowOnGrid(this GridView gv, int rH)
        {
            gv.ClearSelection();
            gv.FocusedRowHandle = rH;
            gv.FocusedColumn = gv.VisibleColumns[0];
            gv.SelectRow(rH);
        }

        public static void SetFocusedRowOnGrid(this GridView gv, int rH, GridColumn gCol)
        {
            gv.ClearSelection();
            gv.FocusedRowHandle = rH;
            gv.FocusedColumn = gCol;
            gv.SelectRow(rH);
        }

        public static void ExpandAllRowsMasterView(GridView gv)
        {
            for (int i = 0; i < gv.RowCount; i++)
            {
                gv.ExpandMasterRow(i);
            }
        }

        public static void SetFocusedRowOnLayoutView(this LayoutView lv, int rH)
        {
            lv.ClearSelection();
            lv.FocusedRowHandle = rH;
            lv.FocusedColumn = lv.VisibleColumns[0];
            lv.SelectRow(rH);
        }

        public static void SetFocusedRowOnLayoutView(this LayoutView lv, int rH, GridColumn gCol)
        {
            lv.ClearSelection();
            lv.FocusedRowHandle = rH;
            lv.FocusedColumn = gCol;
            lv.SelectRow(rH);
        }

        public static void SetFocusedRowOnTree(TreeList tl, TreeListNode node, string fieldName)
        {
            if (!tl.Focused)
            {
                tl.Focus();
            }


            tl.FocusedNode = node;
            tl.FocusedColumn = tl.Columns[fieldName];
            tl.Selection.Clear();
            tl.SelectNode(node);


        }

        public static void SetFocusedRowOnTree(TreeList tl, TreeListNode node, TreeListColumn col)
        {
            if (!tl.Focused)
            {
                tl.Focus();
            }
            tl.FocusedNode = node;
            tl.Selection.Clear();
          
            tl.FocusedColumn = col;

            tl.SelectNode(node);


        }
        public static void SetFocusedCellOnTree(TreeList tl, TreeListNode node, string fieldName)
        {
            if (!tl.Focused)
            {
                tl.Focus();
            }

            tl.FocusedNode = node;
            tl.Selection.Clear();

      
            tl.FocusedColumn = tl.Columns[fieldName];
            tl.SelectCell(node, tl.Columns[fieldName]);



        }
        public static void SetFocusedCellOnGrid(GridView gv, int rH, string fieldName)
        {
            gv.ClearSelection();
            gv.FocusedRowHandle = rH;
            gv.FocusedColumn = gv.Columns[fieldName];
            gv.SelectCell(rH, gv.Columns[fieldName]);
        }

        public static void SetFocusedCellOnGrid(GridView gv, int rH, int visibleIndex)
        {
            gv.ClearSelection();
            gv.FocusedRowHandle = rH;
            gv.FocusedColumn = gv.Columns[visibleIndex];
            gv.SelectCell(rH, gv.Columns[visibleIndex]);
        }

        public static void SetFocusedCellOnGrid(GridView gv, int rH, GridColumn gCol)
        {
            gv.ClearSelection();
            gv.FocusedRowHandle = rH;
            gv.FocusedColumn = gCol;
            gv.SelectCell(rH, gCol);
        }

        public static int FindRowHandleByRowObject(GridView gv, object row)
        {
            if (row != null)
                for (int i = 0; i < gv.DataRowCount; i++)
                    if (row.Equals(gv.GetRow(i)))
                        return i;
            return GridControl.InvalidRowHandle;

        }



        public static int FindRowHandleByDataRow(GridView gv, DataRow dr)
        {
            if (dr != null)
                for (int i = 0; i < gv.DataRowCount; i++)
                    if (gv.GetDataRow(i) == dr)
                        return i;
            return GridControl.InvalidRowHandle;

        }

        public static bool IsNumericType(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return Nullable.GetUnderlyingType(type).IsNumericType();
                        //return IsNumeric(Nullable.GetUnderlyingType(type));
                    }
                    return false;
                default:
                    return false;
            }
        }

        public static IEnumerable<T> FindAllChildrenByType<T>(this Control control)
        {
            IEnumerable<Control> controls = control.Controls.Cast<Control>();
            var enumerable = controls as IList<Control> ?? controls.ToList();
            return enumerable.OfType<T>().Concat<T>(enumerable.SelectMany(FindAllChildrenByType<T>));
        }




        public static Control FindControlParent<T>(this Control control)
        {
            Control ctrlParent = control;
            while ((ctrlParent = ctrlParent.Parent) != null)
            {
                if (ctrlParent.GetType() == typeof(T))
                {
                    return ctrlParent;
                }
            }
            return null;
        }


        public static List<System.Windows.Forms.Control> FindAllControlInForm(this Form form)

        {
            List<System.Windows.Forms.Control> lControl = new List<System.Windows.Forms.Control>();
            foreach (System.Windows.Forms.Control control in form.Controls)
            {
                lControl.Add(control);
                if (control.HasChildren)
                {
                    lControl.AddRange(control.FindAllChildrenControl());
                }
            }
            return lControl;
        }


        public static IEnumerable<System.Windows.Forms.Control> FindAllChildrenControl(this System.Windows.Forms.Control parentControl)
        {
            foreach (System.Windows.Forms.Control control in parentControl.Controls)
            {
                yield return control;
                if (control.HasChildren)
                {
                    foreach (System.Windows.Forms.Control childControl in control.FindAllChildrenControl())
                    {
                        yield return childControl;
                    }
                }
            }
        }

     
      
         
        public static void WriteDataTableToTextFile(string pathFile, DataTable dtExport, string compCode, string modImportName, string layOut, string connectString,
            bool isRemoveLastChar = true)
        {


            string strSql = string.Format(" Select * from SY41{0}00 Where SY41001='{1}{0}{2}' and SY41007 <> '0' ", compCode, modImportName, layOut);
            AccessData ac = new AccessData(connectString);
            DataTable dtWith = ac.TblReadDataSQL(strSql, null);
            StreamWriter str = new StreamWriter(pathFile, false, Encoding.Unicode);

            // String.Format("{0:ddMMyy}", dt); 


            foreach (DataRow datarow in dtExport.Rows)
            {

                string row = string.Empty;
                for (int i = 0; i < dtExport.Columns.Count; i++)
                {
                    string strResult = string.Empty;
                    if (dtExport.Columns[i].DataType == typeof(DateTime))
                    {
                        strResult = String.Format("{0:ddMMyy}", datarow[i]); ;
                    }
                    else
                    {
                        strResult = GetSafeString(datarow[i]);
                    }
                    int width = GetSafeInt(dtWith.Rows[i]["SY41008"]);
                    if (strResult.Length < width)
                    {
                        strResult = strResult + SetStringSpace(width - strResult.Length);
                    }
                    strResult = strResult.Substring(0, width);
                    row += strResult;

                }
                if (isRemoveLastChar)
                {
                    str.WriteLine(row.Remove(row.Length - 1, 1));
                }
                else
                {
                    str.WriteLine(row);
                }

            }

            str.Flush();
            str.Close();



        }


        static readonly string[] Months = { string.Empty, "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        static readonly string[] Days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        /// <summary>
        /// Get month (1 = January)
        /// </summary>
        public static string GetMonthString(int number)
        {
            return Months[number];
        }
        public static string GetMonthString(DateTime d)
        {
            return Months[d.Month];
        }
        /// <summary>
        /// Get day (0 = Sunday)
        /// </summary>
        public static string GetDayOfWeek(int number)
        {
            return Days[number];
        }
        public static string GetDayOfWeek(DateTime d)
        {
            return Days[(int)d.DayOfWeek];
        }

        public static List<String> GetDataFristColumnsOnClipBoard()
        {
            List<String> l = new List<string>();

            IDataObject data = Clipboard.GetDataObject();
            if (data == null) return l;
            string cData = data.GetData(DataFormats.Text).ToString();
            string[] rowsData = cData.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            l.AddRange(rowsData.Select(rowData => rowData.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries))
                               .Where(colsData => colsData.Length > 0).Select(colsData => colsData[0].Trim()));
            return l;
        }

        public static string[] GetArrayRowsFromClipBoard(PasteOptionEmpty pasteOption)
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data == null) return null;
            string cData = data.GetData(DataFormats.Text).ToString();
            string[] rowsData;
            if (pasteOption == PasteOptionEmpty.NotRemoveEmpty)
            {
                rowsData = cData.Split(new[] { "\r\n" }, StringSplitOptions.None);
            }
            else if (pasteOption == PasteOptionEmpty.RemoveAllEmpty)
            {
                rowsData = cData.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                rowsData = cData.Split(new[] { "\r\n" }, StringSplitOptions.None);
                var strCheck = GetSafeString(rowsData.Last());
                if (string.IsNullOrEmpty(strCheck))
                {
                    int length = rowsData.Length;
                    if (length > 0)
                    {
                        int selectItem = length - 1;
                        rowsData = rowsData.Select((p, i) => new
                        {
                            Index = i,
                            Value = p,
                        }).Where(p => p.Index < selectItem).Select(p => p.Value).ToArray();
                    }
                }

            }
            return rowsData;
        }

        public static DataTable GetDataTableFromClipBoard(PasteOptionEmpty pasteOption)
        {
            DataTable dt = new DataTable();
            string[] rowsData = GetArrayRowsFromClipBoard(pasteOption);
            if (rowsData == null || rowsData.Length <= 0) return dt;

            int count = rowsData.Select(p => new
            {
                CountItem = p.Split('\t').ToList().Count
            }).Max(p => p.CountItem);

            for (int i = 0; i < count; i++)
            {
                dt.Columns.Add(i.ToString(), typeof(string));
            }

            foreach (string rowData in rowsData)
            {
                DataRow dr = dt.NewRow();
                var arrCol = rowData.Split('\t').ToList();
                for (int j = 0; j < arrCol.Count; j++)
                {
                    dr[j] = arrCol[j];
                }
                dt.Rows.Add(dr);
            }
            return dt;


        }

        public static bool PasteValueCopyfileOnGird(GridView gv, string fieldName, int rH, bool checkDoubleType, bool isRound, int digit,
            PasteOptionEmpty pasteOption, params Color[] backColor)
        {

            IDataObject data = Clipboard.GetDataObject();

            if (data == null) return false;

            bool isSet = false;
            string cData = data.GetData(DataFormats.Text).ToString();
            string[] rowsData;
            if (pasteOption == PasteOptionEmpty.NotRemoveEmpty)
            {
                rowsData = cData.Split(new[] { "\r\n" }, StringSplitOptions.None);
            }
            else if (pasteOption == PasteOptionEmpty.RemoveAllEmpty)
            {
                rowsData = cData.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                rowsData = cData.Split(new[] { "\r\n" }, StringSplitOptions.None);
                int length = rowsData.Length;
                if (length > 0)
                {
                    int selectItem = length - 1;
                    rowsData = rowsData.Select((p, i) => new
                    {
                        Index = i,
                        Value = p,
                    }).Where(p => p.Index < selectItem).Select(p => p.Value).ToArray();
                }
            }



            int rGrid = gv.RowCount - 1;
            //int rH = gv.FocusedRowHandle;
            //string fieldName = gv.FocusedColumn.FieldName;


            foreach (var rowData in rowsData)
            {
                if (rH > rGrid)
                    break;
                string[] colsData = rowData.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                if (colsData.Length > 0)
                {
                    object value = null;
                    bool setValue = false;
                    if (checkDoubleType)
                    {
                        double dValue;
                        bool isDouble = Double.TryParse(colsData[0].Trim(), out dValue);
                        if (isDouble)
                        {
                            value = isRound ? Math.Round(dValue, digit) : dValue;
                            if (backColor.Length > 0)
                            {
                                GridCellInfo gColInfo = GetGridCellInfo(gv, rH, fieldName);
                                Color cellBackColor = gColInfo.Appearance.BackColor;
                                setValue = backColor.AsEnumerable().Any(p => p == cellBackColor);
                            }
                            else
                            {
                                setValue = true;
                            }
                        }
                        else
                        {
                            setValue = false;
                        }
                    }
                    else
                    {
                        setValue = true;
                        value = colsData[0].Trim();
                    }
                    if (setValue)
                    {
                        isSet = true;
                        gv.SetRowCellValue(rH, fieldName, value);
                    }
                }
                rH++;
            }
            return isSet;
        }


        public static bool CheckValueInArray<T>(T value, T[] arrFind)
        {
            return arrFind.AsEnumerable().Any(p => EqualityComparer<T>.Default.Equals(p, value));
        }
        public static void SetValueSearchLookUpEdit<T>(SearchLookUpEdit slE, T value)
        {
            int index = slE.Properties.GetIndexByKeyValue(value);

            if (index == GridControl.InvalidRowHandle)
            {
                // MessageBox.Show("GridView is not initialized yet! Key value not found");
                return;
            }
            slE.EditValue = value;
        }

        public static string GetDatabaseNameInConnectString(string strConn)
        {
            string[] arrConn = strConn.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            var q1 = arrConn.Where(p => p.ToUpper().Trim().Contains("INITIAL CATALOG")).Select(p => new { DatabaseName = p.Trim() }).ToList();
            if (q1.Any())
            {
                var strContainDbName = q1.Select(p => p.DatabaseName).FirstOrDefault();
                if (strContainDbName != null)
                {
                    int index = strContainDbName.IndexOf("=", StringComparison.Ordinal);
                    return strContainDbName.Substring(index + 1, strContainDbName.Length - (index + 1)).Trim();
                }
            }
            return string.Empty;
        }

        public static void CopyValueColumnInAnother(GridView gv, string columnCopy, string columnPaste, List<Int32> arRowCopy)
        {
            foreach (int t in arRowCopy)
            {
                gv.SetRowCellValue(t, columnPaste, gv.GetRowCellValue(t, columnCopy));
            }
        }
        /// <summary>
        ///     Set Cell Selected
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="gCol" type="DevExpress.XtraGrid.Columns.GridColumn">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void SetSelectedWhenGridViewDoubleClick(GridView gv, GridColumn gCol)
        {
            if (gv.RowCount == 0) return;
            gv.ClearSelection();
            gv.BeginSelection();
            gv.FocusedRowHandle = 0;
            gv.FocusedColumn = gCol;
            for (int i = 0; i < gv.RowCount; i++)
            {
                gv.SelectCell(i, gCol);
            }

            gv.EndSelection();
        }

        /// <summary>
        ///     Set Row Selected
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void SetSelectedWhenGridViewDoubleClick(GridView gv)
        {
            if (gv.RowCount == 0) return;
            gv.ClearSelection();
            gv.BeginSelection();
            gv.FocusedRowHandle = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                gv.SelectRow(i);
            }

            gv.EndSelection();
        }
        public static long DateDiff(DateInterval intervalType, DateTime beginDate, DateTime endDate)
        {
            switch (intervalType)
            {
                case DateInterval.Day:
                case DateInterval.DayOfYear:
                    TimeSpan spanForDays = endDate - beginDate;
                    return (long)spanForDays.TotalDays;
                case DateInterval.Hour:
                    TimeSpan spanForHours = endDate - beginDate;
                    return (long)spanForHours.TotalHours;
                case DateInterval.Minute:
                    TimeSpan spanForMinutes = endDate - beginDate;
                    return (long)spanForMinutes.TotalMinutes;
                case DateInterval.Month:
                    return ((endDate.Year - beginDate.Year) * 12) + (endDate.Month - beginDate.Month);
                case DateInterval.Quarter:
                    long dateOneQuarter = (long)Math.Ceiling(beginDate.Month / 3.0);
                    long dateTwoQuarter = (long)Math.Ceiling(endDate.Month / 3.0);
                    return (4 * (endDate.Year - beginDate.Year)) + dateTwoQuarter - dateOneQuarter;
                case DateInterval.Second:
                    TimeSpan spanForSeconds = endDate - beginDate;
                    return (long)spanForSeconds.TotalSeconds;
                case DateInterval.Weekday:
                    TimeSpan spanForWeekdays = endDate - beginDate;
                    return (long)(spanForWeekdays.TotalDays / 7.0);
                case DateInterval.WeekOfYear:
                    DateTime dateOneModified = beginDate;
                    DateTime dateTwoModified = endDate;
                    while (DateTimeFormatInfo.CurrentInfo != null && dateTwoModified.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                    {
                        dateTwoModified = dateTwoModified.AddDays(-1);
                    }
                    while (DateTimeFormatInfo.CurrentInfo != null && dateOneModified.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                    {
                        dateOneModified = dateOneModified.AddDays(-1);
                    }
                    TimeSpan spanForWeekOfYear = dateTwoModified - dateOneModified;
                    return (long)(spanForWeekOfYear.TotalDays / 7.0);
                case DateInterval.Year:
                    return endDate.Year - beginDate.Year;
                default:
                    return 0;
            }
        }
        /// <summary>
        /// Set All Column
        /// </summary>
        /// <param name="gv"></param>
        public static void SetContainsFilterInGridView(GridView gv)
        {
            foreach (GridColumn col in gv.Columns)
            {
                col.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            }
        }

        public static void SetContainsFilterInGridView(GridView gv, params string[] fieldName)
        {
            foreach (string str in fieldName)
            {
                if (string.IsNullOrEmpty(str)) continue;
                gv.Columns[str.Trim()].OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            }
        }

        public static void SetContainsFilterInGridView(GridView gv, params int[] columnIndex)
        {
            foreach (int index in columnIndex)
            {
                if (index < 0) continue;
                gv.Columns[index].OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            }
        }

        public static int GetIntGreaterEqualValue(double value)
        {
            int temp = (int)value;
            if (value - temp == 0)
                return temp;
            return temp + 1;
        }
        public static void StandardFormDateToDateInput(DateEdit dtF, DateEdit dtT)
        {
            if (dtF.IsModified)
            {
                dtT.Select();
                dtF.Select();
            }
            if (dtT.IsModified)
            {
                dtF.Select();
                dtT.Select();
            }
            string formDate = GetSafeString(dtF.EditValue);
            string toDate = GetSafeString(dtT.EditValue);
            if (string.IsNullOrEmpty(formDate) && string.IsNullOrEmpty(toDate))
            {
                return;
                //dtF.EditValue = DateTime.Now;
                //dtT.EditValue = DateTime.Now;
            }
            if (!string.IsNullOrEmpty(formDate) && !string.IsNullOrEmpty(toDate))
            {
                if (CompareDate(dtF.DateTime, dtT.DateTime)) return;
                dtF.EditValue = dtT.EditValue;
            }
            else
            {
                if (string.IsNullOrEmpty(formDate))
                {
                    dtF.EditValue = dtT.EditValue;
                }
                else
                {
                    dtT.EditValue = dtF.EditValue;
                }
            }
        }
        /// <summary>
        ///       print spreadsheet (paper kind auto setting)
        /// </summary>
        /// <param name="workbook" type="DevExpress.Spreadsheet.IWorkbook">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fitToPage" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fitToWidth" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fitToHeight" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void PrintSpreadSheet(IWorkbook workbook, PageOrientation pageOrien = PageOrientation.Default, bool fitToPage = true, int fitToWidth = 1, int fitToHeight = 0)
        {

            Worksheet worksheet = workbook.Worksheets.ActiveWorksheet;
            #region #WorksheetPrintOptions
            worksheet.ActiveView.Orientation = pageOrien;
            //  Display row and column headings.
            //  worksheet.ActiveView.ShowHeadings = false;
            // Access an object that contains print options.
            WorksheetPrintOptions printOptions = worksheet.PrintOptions;
            //  Print in black and white.
            printOptions.BlackAndWhite = false;
            //  Do not print gridlines.
            printOptions.PrintGridlines = false;
            //  Scale the print area to fit to two pages wide.
            printOptions.FitToPage = fitToPage;
            printOptions.FitToWidth = fitToWidth;
            printOptions.FitToHeight = fitToHeight;
            //  Print a dash instead of a cell error message.
            printOptions.ErrorsPrintMode = ErrorsPrintMode.Dash;





            //workbook.Unit = DevExpress.Office.DocumentUnit.Inch;

            //// Access page margins.
            //Margins pageMargins = workbook.Worksheets[0].ActiveView.Margins;

            //// Specify page margins.
            //pageMargins.Left = 1;
            //pageMargins.Top = 1.5F;
            //pageMargins.Right = 1;
            //pageMargins.Bottom = 0.8F;

            //// Specify header and footer margins.
            //pageMargins.Header = 1;
            //pageMargins.Footer = 0.4F;




            #endregion #WorksheetPrintOptions


            #region #PrintWorkbook
            // Invoke the Print Preview dialog for the workbook.
            using (PrintingSystem printingSystem = new PrintingSystem())
            {
                using (PrintableComponentLink link = new PrintableComponentLink(printingSystem))
                {
                    link.Component = workbook;
                    link.CreateDocument();
                    link.ShowPreviewDialog();
                }
            }
            #endregion
        }
        /// <summary>
        ///      print spreadsheet set paper kind befor printing (A4,A3,....)
        /// </summary>
        /// <param name="workbook" type="DevExpress.Spreadsheet.IWorkbook">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="paperKind" type="System.Drawing.Printing.PaperKind">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fitToPage" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fitToWidth" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fitToHeight" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void PrintSpreadSheet(IWorkbook workbook, PaperKind paperKind, PageOrientation pageOrien = PageOrientation.Default, bool fitToPage = true, int fitToWidth = 1, int fitToHeight = 0)
        {

            Worksheet worksheet = workbook.Worksheets.ActiveWorksheet;
            #region #WorksheetPrintOptions
            worksheet.ActiveView.Orientation = pageOrien;
            worksheet.ActiveView.PaperKind = paperKind;
            // Access an object that contains print options.
            WorksheetPrintOptions printOptions = worksheet.PrintOptions;
            //  Print in black and white.
            printOptions.BlackAndWhite = false;
            //  Do not print gridlines.
            printOptions.PrintGridlines = false;
            //  Scale the print area to fit to two pages wide.
            printOptions.FitToPage = fitToPage;
            printOptions.FitToWidth = fitToWidth;
            printOptions.FitToHeight = fitToHeight;
            //  Print a dash instead of a cell error message.
            printOptions.ErrorsPrintMode = ErrorsPrintMode.Dash;
            #endregion #WorksheetPrintOptions

            #region #PrintWorkbook
            // Invoke the Print Preview dialog for the workbook.
            using (PrintingSystem printingSystem = new PrintingSystem())
            {
                using (PrintableComponentLink link = new PrintableComponentLink(printingSystem))
                {
                    link.Component = workbook;
                    link.CreateDocument();
                    link.ShowPreviewDialog();
                }
            }
            #endregion #PrintWorkbook
        }
        public static DataTable GetTableExcelSheetName(string filename)
        {
            var dtSheet = new DataTable();
            dtSheet.Columns.Add("SheetName", typeof(string));
            string[] excelSheets = GetExcelSheetNames(filename);
            foreach (string t in excelSheets)
            {
                dtSheet.Rows.Add(t);
            }
            return dtSheet;
        }

        public static void UnCheckSelectedInGridView(GridView gv, string columnName)
        {
            for (int i = 0; i < gv.RowCount; i++)
            {
                gv.SetRowCellValue(i, columnName.Trim(), false);
            }
        }

        public static DataTable GetListLineWhenF4(string comp)
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            return accData.TblReadDataSQL(string.Format("SELECT ltrim(rtrim(AA26001)) AS Code,ltrim(rtrim(AA26002)) AS Description FROM AA26{0}00 ORDER BY Code", comp), null);
        }
        public static DataTable GetAllEmailAddressInTableListUser()
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            return accData.TblReadDataSP("usp_Scag_GetListEmailAddress", null);
        }

        public static string StringCumulativeRowsDeletedInTable(DataTable dtPara, string columnPk)
        {
            DataTable delRowsTable = dtPara.GetChanges(DataRowState.Deleted);
            string idDelete = string.Empty;
            if (delRowsTable != null)
            {
                idDelete = delRowsTable.Rows.Cast<DataRow>().Where(dr => GetSafeString(dr[columnPk.Trim(), DataRowVersion.Original]) != string.Empty).Aggregate(idDelete, (current, dr) => current + string.Format("{0},", GetSafeString(dr[columnPk.Trim(), DataRowVersion.Original])));
            }
            if (!string.IsNullOrEmpty(idDelete))
            {
                idDelete = idDelete.Substring(0, idDelete.Length - 1);
            }
            return idDelete;

        }

        public static string[] GetValueRowsDeletedInTable(DataTable dtPara, string columnPk)
        {
            DataTable delRowsTable = dtPara.GetChanges(DataRowState.Deleted);
            if (delRowsTable != null)
            {
                string[] lResult = delRowsTable.AsEnumerable().Select(dr => string.Format("{0},", dr[columnPk, DataRowVersion.Original])).Where(value => !string.IsNullOrEmpty(value)).ToArray();
                return lResult;
            }
            return null;
        }

        /// <summary>
        ///     User Info Include : UserName + Display Name, Email
        /// </summary>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        public static DataTable GetAllInforUserInDomain(bool isShowError = true)
        {
            var dtUser = new DataTable();
            dtUser.Columns.Add("UserName", typeof(string));
            dtUser.Columns.Add("DisplayName", typeof(string));
            dtUser.Columns.Add("Email", typeof(string));
            try
            {
                var searchRoot = new DirectoryEntry("LDAP://" + DeclareSystem.SysDomainName, DeclareSystem.SysUserInDomain, DeclareSystem.SysPasswordOfUserInDomain);
                var search = new DirectorySearcher(searchRoot) { Filter = "(&(objectClass=user)(objectCategory=person))" };
                search.PropertiesToLoad.Add("samaccountname");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("usergroup");
                search.PropertiesToLoad.Add("displayname");//first name
                SearchResultCollection resultCol = search.FindAll();
                for (int counter = 0; counter < resultCol.Count; counter++)
                {
                    SearchResult result = resultCol[counter];
                    if (result.Properties.Contains("samaccountname") && result.Properties.Contains("mail") && result.Properties.Contains("displayname"))
                    {
                        DataRow dr = dtUser.NewRow();
                        dr["UserName"] = GetSafeString(result.Properties["samaccountname"][0]);
                        dr["DisplayName"] = GetSafeString(result.Properties["displayname"][0]);
                        dr["Email"] = GetSafeString(result.Properties["mail"][0]);
                        dtUser.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                if (isShowError)
                {
                    XtraMessageBox.Show(string.Format("Error : {0}", ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return dtUser;
            }
            dtUser.DefaultView.Sort = "UserName ASC";
            return dtUser.DefaultView.ToTable();
        }



        public static DataTable GetDefaultTableStringPk()
        {

            DataTable dtDepWhere = new DataTable();
            dtDepWhere.Columns.Add("Code", typeof(string));
            return dtDepWhere;




        }
        public static DataTable GetListEmailUserByDepartmentPR(string connectString, out Dictionary<string, DataTable> dicUser)
        {




            AccessData accData = new AccessData(connectString);
            DataTable dtDepartment = accData.TblReadDataSP("sp_LoadListEmaiModuleDepartmentPR", null);
            var qWhere = dtDepartment.AsEnumerable().Select(p => new
            {
                Code = p.Field<string>("Code"),
            }).Distinct().ToList();

            DataTable dtWhere = qWhere.Count > 0 ? qWhere.CopyToDataTableNew() : ProcessGeneral.GetDefaultTableStringPk();

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtWhere };
            DataTable dtUser = accData.TblReadDataSP("sp_LoadListEmaiModuleUserPR", arrpara);
            dicUser = dtUser.AsEnumerable().GroupBy(p => p.Field<string>("DepartmentCode")).Select(s => new
            {
                s.Key,
                Value = s.Select(t => new
                {
                    UserName = t.Field<string>("UserName"),
                    FullName = t.Field<string>("FullName"),
                    Email = t.Field<string>("Email"),
                    Position = t.Field<string>("Position"),
                    FristName = t.Field<string>("FristName"),
                    DepartmentCode = t.Field<string>("DepartmentCode"),
                    DepartmentName = t.Field<string>("DepartmentName"),
                }).CopyToDataTableNew()
            }).ToDictionary(s => s.Key, s => s.Value);
            return dtDepartment;

        }
        public static DataTable TableUserModuleTemp()
        {
            var dt = new DataTable();
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Position", typeof(string));
            dt.Columns.Add("FristName", typeof(string));
            dt.Columns.Add("DepartmentCode", typeof(string));
            dt.Columns.Add("DepartmentName", typeof(string));
            return dt;
        }
        public static DataTable GetListEmailByDepartmentTree(string connectString,DataTable dtDepWhere,params string[] arrMailTo)
        {




            AccessData accData = new AccessData(connectString);
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtDepWhere };
            DataTable dtChild = accData.TblReadDataSP("sp_GetEmailInfoByDeparment", arrpara);
            var qChild = dtChild.AsEnumerable().Where(p => !arrMailTo.Contains(p.Field<string>("Email"))).Select(p =>
                new
                {
                    Code = p.Field<string>("Code"),
                    Name = p.Field<string>("Name"),
                    Email = p.Field<string>("Email"),
                    ChildPK = p.Field<Int64>("ChildPK"),
                    ParentPK = p.Field<Int64>("ParentPK"),
                    Position = p.Field<string>("Position"),
                    DepartmentCode = p.Field<string>("DepartmentCode"),
                }).ToList();

          
        
      
            if (qChild.Count <= 0)
            {
                DataTable dtReturn = dtChild.Clone();
                dtReturn.Columns.Remove("DepartmentCode");
                dtReturn.AcceptChanges();
                return dtReturn;
            }


            DataTable dtDepWhereNew = qChild.Select(p => new
            {
                p.DepartmentCode
            }).Distinct().CopyToDataTableNew();

            Int64 maxPk = qChild.Max(p => p.ChildPK) + 1;
            arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@MaxPK", SqlDbType.BigInt) { Value = maxPk };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtDepWhereNew };
            DataTable dtParent = accData.TblReadDataSP("sp_GetEmailInfoByDeparmentParent", arrpara);

            var qFinal = qChild.Join(dtParent.AsEnumerable(), p => p.DepartmentCode,
                t => t.Field<string>("Code"), (p, t) => new
                {
                    p.Code,
                    p.Name ,
                    p.Email ,
                    p.ChildPK ,
                    ParentPK = t.Field<Int64>("ChildPK"),
                    p.Position ,
                }).ToList();

            foreach (var itemFinal in qFinal)
            {
                DataRow drChild = dtParent.NewRow();

                drChild["Code"] = itemFinal.Code;
                drChild["Name"] = itemFinal.Name;
                drChild["Email"] = itemFinal.Email;
                drChild["ChildPK"] = itemFinal.ChildPK;
                drChild["ParentPK"] = itemFinal.ParentPK;

                drChild["Position"] = itemFinal.Position;
                dtParent.Rows.Add(drChild);
            }
            dtParent.AcceptChanges();
            return dtParent;

        }

        public static string FindMaxValueInStringNumber(string str, char delimiter)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            string[] arrNumber = str.Split(delimiter);
            return arrNumber.Where(s => !string.IsNullOrEmpty(s)).OrderByDescending(GetSafeInt).First();
        }
        public static string FindMinValueInStringNumber(string str, char delimiter)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            string[] arrNumber = str.Split(delimiter);
            return arrNumber.Where(s => !string.IsNullOrEmpty(s)).OrderBy(GetSafeInt).First();
        }

        public static void EnableFormWhenEndEdit(string fNameActive, string fTextActive)
        {
            foreach (Form frm in Application.OpenForms)
            {
                frm.Enabled = true;
                if (frm.Name == fNameActive && frm.Text.Trim() == fTextActive)
                {
                    frm.Activate();
                }
            }
        }

        public static void EnableFormWhenEndEdit(string fNameActive)
        {
            foreach (Form frm in Application.OpenForms)
            {
                frm.Enabled = true;
                if (frm.Name == fNameActive)
                {
                    frm.Activate();
                }
            }
        }

        public static void DisableFormWhenEditing(Form fParent, Form fChild)
        {
            foreach (Form frm in fParent.MdiChildren)
            {
                if (frm.GetType() == fChild.GetType() && frm.Name == fChild.Name && frm.Text.Trim() == fChild.Text.Trim())
                {
                    frm.Enabled = true;
                    frm.Activate();
                }
                else
                {
                    frm.Enabled = false;
                }
            }

        }


        public static TypeShowForm GetTypeShowForm(string type)
        {
            if (type.ToUpper().Trim() == TypeShowForm.ShowNormal.ToString().ToUpper().Trim())
                return TypeShowForm.ShowNormal;
            if (type.ToUpper().Trim() == TypeShowForm.ShowDialog.ToString().ToUpper().Trim())
                return TypeShowForm.ShowDialog;
            if (type.ToUpper().Trim() == TypeShowForm.ShowHide.ToString().ToUpper().Trim())
                return TypeShowForm.ShowHide;
            return TypeShowForm.ShowMdi;


        }


        public static DataAttType GetDataAttType(string type)
        {
            if (type.ToUpper().Trim() == DataAttType.Number.ToString().ToUpper().Trim())
                return DataAttType.Number;
            if (type.ToUpper().Trim() == DataAttType.Boolean.ToString().ToUpper().Trim())
                return DataAttType.Boolean;
            if (type.ToUpper().Trim() == DataAttType.Datetime.ToString().ToUpper().Trim())
                return DataAttType.Datetime;
            return DataAttType.String;
        }
        public static DataStatus GetDataStatus(string rowState)
        {
            if (rowState == DataStatus.Insert.ToString())
                return DataStatus.Insert;
            if (rowState == DataStatus.Update.ToString())
                return DataStatus.Update;
            return DataStatus.Unchange;
        }
        public static int GetDataStatusPriority(string rowState)
        {
            if (rowState == DataStatus.Insert.ToString())
                return 2;
            if (rowState == DataStatus.Update.ToString())
                return 0;
            return 1;
        }

        /// <summary>
        ///     Get ID From AACU0000 By Table Name
        /// </summary>
        /// <param name="tableName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A long value...
        /// </returns>
        public static Int64 GetNextId(string tableName)
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@TableName", SqlDbType.NVarChar) { Value = tableName.Trim() };
            DataTable dt = accData.TblReadDataSP("usp_Scag_GetNextID", arrpara);
            return dt.Rows.Count > 0 ? GetSafeInt64(dt.Rows[0]["ID"]) : 0;
        }
        /// <summary>
        ///     Get Min ID In Table For Loop PK
        /// </summary>
        /// <param name="tableName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="row" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A long value...
        /// </returns>
        public static Int64 GetNextId(string tableName, int row)
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@TableName", SqlDbType.NVarChar) { Value = tableName.Trim() };
            arrpara[1] = new SqlParameter("@Row", SqlDbType.Int) { Value = row };
            DataTable dt = accData.TblReadDataSP("usp_Scag_GetNextID_ByRow", arrpara);
            return dt.Rows.Count > 0 ? GetSafeInt64(dt.Rows[0]["ID"]) : 0;
        }

        /// <summary>
        ///     Get ID From AAC0 By TableName and CompanyCode
        /// </summary>
        /// <param name="tableName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="compCode" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A long value...
        /// </returns>
        public static Int64 GetNextId(string tableName, string compCode)
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@TableName", SqlDbType.NVarChar) { Value = tableName.Trim() };
            arrpara[1] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = compCode.Trim() };
            DataTable dt = accData.TblReadDataSP("usp_Scag_GetNextID_ByCompCode", arrpara);
            return dt.Rows.Count > 0 ? GetSafeInt64(dt.Rows[0]["ID"]) : 0;
        }

        /// <summary>
        ///      Get ID,AACO003 From AAC0 By TableName and CompanyCode
        /// </summary>
        /// <param name="tableName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="compCode" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        public static DataTable GetNextIdAaco003(string tableName, string compCode)
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@TableName", SqlDbType.NVarChar) { Value = tableName.Trim() };
            arrpara[1] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = compCode.Trim() };
            return accData.TblReadDataSP("usp_Scag_GetNextID_AACO003_ByCompCode", arrpara);
        }

        /// <summary>
        ///     Get Min ID In Table For Loop PK  
        /// </summary>
        /// <param name="tableName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="compCode" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="row" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A long value...
        /// </returns>
        public static Int64 GetNextId(string tableName, string compCode, int row)
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@TableName", SqlDbType.NVarChar) { Value = tableName.Trim() };
            arrpara[1] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = compCode.Trim() };
            arrpara[2] = new SqlParameter("@Row", SqlDbType.Int) { Value = row };
            DataTable dt = accData.TblReadDataSP("usp_Scag_GetNextID_ByCompCodeAndRow", arrpara);
            return dt.Rows.Count > 0 ? GetSafeInt64(dt.Rows[0]["ID"]) : 0;
        }
        /// <summary>
        ///   Get ID From Any Table By Condition Parameter filter source table  
        /// </summary>
        /// <param name="sourceTable" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fieldSelectd" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fieldWhere" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="valueWhere" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A long value...
        /// </returns>
        public static Int64 GetNextId(string sourceTable, string fieldSelectd, string fieldWhere, string valueWhere)
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@SourceTable", SqlDbType.NVarChar) { Value = sourceTable.Trim() };
            arrpara[1] = new SqlParameter("@FieldSelected", SqlDbType.NVarChar) { Value = fieldSelectd.Trim() };
            arrpara[2] = new SqlParameter("@FieldWhere", SqlDbType.NVarChar) { Value = fieldWhere.Trim() };
            arrpara[3] = new SqlParameter("@ValueWhere", SqlDbType.NVarChar) { Value = valueWhere.Trim() };
            DataTable dt = accData.TblReadDataSP("usp_Scag_GetNextID_General", arrpara);
            return dt.Rows.Count > 0 ? GetSafeInt64(dt.Rows[0]["ID"]) : 0;
        }

        public static DateTime GetServerDate()
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            DataTable dt = accData.TblReadDataSQL("SELECT GETDATE() AS CurrentDate", null);
            return dt.Rows.Count > 0 ? GetSafeDatetimeNullable(dt.Rows[0]["CurrentDate"]) : DateTime.Now;
        }

        public static DataTable GetAdvanceFunctionWhenOpenForm(string strAdvanceFunction)
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@AdvanceFunction", SqlDbType.NVarChar) { Value = strAdvanceFunction };
            return accData.TblReadDataSP("usp_Scag_LoadPermissionWhenOpenForm", arrpara);
        }
        public static DataTable GetSpecialFunctionWhenOpenForm(string strSpecialFunction)
        {
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@SpecialFunction", SqlDbType.NVarChar) { Value = strSpecialFunction };
            return accData.TblReadDataSP("usp_Scag_LoadSpecialFunctionWhenOpenForm", arrpara);
        }
        public static void CloseAllChildForm(Form fParent)
        {
            foreach (Form frm in fParent.MdiChildren)
            {
                frm.Close();
            }
        }

        public static bool CheckFormShow(Form fParent, Form fCheck)
        {
            return fParent.MdiChildren.Any(frm => frm.GetType() == fCheck.GetType() && frm.Name.ToUpper().Trim() == fCheck.Name.ToUpper().Trim() && frm.Text.ToUpper().Trim() == fCheck.Text.ToUpper().Trim());
        }

        public static bool CheckFormShow(Form fParent, string strName)
        {
            return fParent.MdiChildren.Any(frm => frm.Name.ToUpper().Trim() == strName.ToUpper().Trim());
        }

        public static bool CheckFormShow(Form fParent, string strName, string strText)
        {
            return fParent.MdiChildren.Any(frm => frm.Name.ToUpper().Trim() == strName.ToUpper().Trim() && frm.Text.ToUpper().Trim() == strText.ToUpper().Trim());
        }

        public static void ShowMDIForm(Form fParent, Form fChild)
        {
            bool activeForm = false;
            int mdiFormIndex = -1;
            foreach (Form frm in fParent.MdiChildren)
            {
                mdiFormIndex++;
                if (frm.GetType() == fChild.GetType() && frm.Name == fChild.Name && frm.Text.Trim() == fChild.Text.Trim())
                {
                    activeForm = true;
                    break;
                }
            }
            if (activeForm)
            {
                fParent.MdiChildren[mdiFormIndex].Activate();
                //  fChild.Activate();
            }
            else
            {
                fChild.MdiParent = fParent;
                fChild.WindowState = FormWindowState.Normal;
                fChild.StartPosition = FormStartPosition.CenterScreen;
                fChild.Show();
            }
        }

        public static void ShowMDIForm(Form fParent, Form fChild, WaitDialogForm dlg)
        {
            bool activeForm = false;
            int mdiFormIndex = -1;
            foreach (Form frm in fParent.MdiChildren)
            {
                mdiFormIndex++;
                if (frm.GetType() == fChild.GetType() && frm.Name == fChild.Name && frm.Text.Trim() == fChild.Text.Trim())
                {
                    activeForm = true;
                    break;
                }
            }
            if (activeForm)
            {
                fParent.MdiChildren[mdiFormIndex].Activate();
                //  fChild.Activate();
            }
            else
            {
                try
                {
                    dlg = new WaitDialogForm(string.Empty);
                    fChild.MdiParent = fParent;
                    fChild.WindowState = FormWindowState.Normal;
                    fChild.StartPosition = FormStartPosition.CenterScreen;
                    fChild.Show();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                }
                finally
                {
                    dlg.Close();
                }

            }
        }

        public static int CalcBestWidthTreeList(TreeList treeList, TreeListColumn column)
        {
            var op = new TreeListOperationColumnBestWidth(treeList, column);
            treeList.ViewInfo.GInfo.AddGraphics(null);
            try
            {
                treeList.NodesIterator.DoOperation(op);
            }
            finally
            {
                treeList.ViewInfo.GInfo.ReleaseGraphics();
            }
            return op.BestWidth;
        }

        public static void InsertNewRowIntoGridViewAfterAnyPosition(DataTable dtSource, GridView gv, int rowHandle, DataRow dr)
        {
            DataRow drInsert = dtSource.NewRow();
            int i = 0;
            Array.ForEach(dr.ItemArray, items =>
            {
                drInsert[i] = items;
                i++;
            });
            int posInsert = gv.GetDataSourceRowIndex(rowHandle) + 1;
            dtSource.Rows.InsertAt(drInsert, posInsert);
            dtSource.AcceptChanges();
            gv.FocusedRowHandle = gv.GetRowHandle(posInsert);
        }

        public static Rectangle GetRectangleInGridView(GridView gv, int rowHandel, int beginColumn, int endColumn)
        {
            GridViewInfo vi = gv.GetViewInfo() as GridViewInfo;
            Rectangle rect = vi.GetGridCellInfo(rowHandel, gv.Columns[beginColumn]).Bounds;
            for (int i = beginColumn + 1; i <= endColumn; i++)
            {
                rect.Width += vi.GetGridCellInfo(rowHandel, gv.Columns[i]).Bounds.Width;
            }
            return rect;

        }

        public static GridCellInfo GetGridCellInfo(GridView gv, int rowHandle, int columnIndex)
        {
            if (gv.IsRowVisible(rowHandle) == RowVisibleState.Hidden)
            {
                gv.MakeRowVisible(rowHandle, false);
            }
            var vi = gv.GetViewInfo() as GridViewInfo;
            if (vi == null) return null;
            GridCellInfo gCellInfo = vi.GetGridCellInfo(rowHandle, gv.Columns[columnIndex]);
            vi.UpdateCellAppearance(gCellInfo);
            return gCellInfo;
        }

        public static GridCellInfo GetGridCellInfo(GridView gv, int rowHandle, string columnName)
        {
            if (gv.IsRowVisible(rowHandle) == RowVisibleState.Hidden)
            {
                gv.MakeRowVisible(rowHandle, false);
            }
            var vi = gv.GetViewInfo() as GridViewInfo;
            if (vi == null) return null;
            GridCellInfo gCellInfo = vi.GetGridCellInfo(rowHandle, gv.Columns[columnName.Trim()]);
            vi.UpdateCellAppearance(gCellInfo);
            return gCellInfo;
        }

        public static GridCellInfo GetGridCellInfo(GridView gv, int rowHandle, GridColumn gCol)
        {
            if (gv.IsRowVisible(rowHandle) == RowVisibleState.Hidden)
            {
                gv.MakeRowVisible(rowHandle, false);
            }

            var vi = gv.GetViewInfo() as GridViewInfo;
            if (vi == null) return null;
            GridCellInfo gCellInfo = vi.GetGridCellInfo(rowHandle, gCol);
            vi.UpdateCellAppearance(gCellInfo);
            return gCellInfo;
        }

        public static void UpdateGridViewInfo(GridView gv)
        {

            GridViewInfo gvInfo = gv.GetViewInfo() as GridViewInfo;
            if (gvInfo == null) return;
            for (int rowIndex = 0; rowIndex < gv.RowCount; rowIndex++)
            {
                GridDataRowInfo rowInfo = new GridDataRowInfo(gvInfo, rowIndex, 0);
                for (int colIndex = 0; colIndex < gv.VisibleColumns.Count; colIndex++)
                {
                    GridColumnInfoArgs args = new GridColumnInfoArgs(gv.VisibleColumns[colIndex]);
                    GridCellInfo cellInfo = new GridCellInfo(args, rowInfo, Rectangle.Empty);
                    gvInfo.UpdateCellAppearance(cellInfo);
                }
            }
        }


        public static int FindRowInGridViewByDisplayText(GridControl gc, string pkColumn, string searchText)
        {
            int rhFound = 0;
            var cv = (ColumnView)gc.FocusedView;
            GridColumn colSearch = cv.Columns[pkColumn.Trim()];
            if (colSearch != null)
            {
                rhFound = cv.LocateByDisplayText(0, colSearch, searchText);
                if (rhFound == GridControl.InvalidRowHandle)
                {
                    rhFound = -1;
                }
            }
            else
            {
                rhFound = -1;
            }
            return rhFound;

        }

        /// <summary>
        ///     Get Sum Total Current Column On GridView Pass Rowhandel
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="rowhandle" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetIntSumFocusColumnGridViewPassRowhandle(GridView gv, int rowhandle)
        {
            int result = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                if (i != rowhandle)
                {
                    result += GetSafeInt(gv.GetRowCellValue(i, gv.FocusedColumn));
                }
            }
            return result;
        }

        /// <summary>
        ///      Get Sum Total Current Column On GridView
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetIntSumFocusColumnGridView(GridView gv)
        {
            int result = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                result += GetSafeInt(gv.GetRowCellValue(i, gv.FocusedColumn));
            }
            return result;
        }

        /// <summary>
        ///     Get Sum Total Column On GridView Pass Rowhandel
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fieldName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="rowhandel" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetIntSumGridViewPassRowhandle(GridView gv, string fieldName, int rowhandle)
        {
            int result = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                if (i != rowhandle)
                {
                    result += GetSafeInt(gv.GetRowCellValue(i, gv.Columns[fieldName.Trim()]));
                }
            }
            return result;
        }
        /// <summary>
        ///     Get Sum Total Column On GridView
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fieldName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetIntSumGridView(GridView gv, string fieldName)
        {
            int result = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                result += GetSafeInt(gv.GetRowCellValue(i, gv.Columns[fieldName.Trim()]));
            }
            return result;
        }
        /// <summary>
        ///     Format number (0:#,#)
        /// </summary>
        /// <param name="number" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string FormatNumber(int number)
        {
            if (number == 0)
                return number.ToString();
            return string.Format(CultureInfo.InvariantCulture, "{0:#,#}", number);

        }
        public static string FormatDate(object date, string formatString)
        {
            if (date == null)
                return "";
            try
            {
                return GetSafeDatetimeNullableReturnMinDate(date ).ToString(formatString);
            }
            catch
            {
                return "";
            }
            

        }
        public static string NumberToText(Decimal total, bool suffix = true)
        {
            try
            {
                string rs = "";
                total = Math.Round(total, 0);
                string[] ch = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
                string[] rch = { "lẻ", "mốt", "", "", "", "lăm" };
                string[] u = { "", "mươi", "trăm", "ngàn", "", "", "triệu", "", "", "tỷ", "", "", "ngàn", "", "", "triệu" };
                string nstr = total.ToString();

                int[] n = new int[nstr.Length];
                int len = n.Length;
                for (int i = 0; i < len; i++)
                {
                    n[len - 1 - i] = Convert.ToInt32(nstr.Substring(i, 1));
                }

                for (int i = len - 1; i >= 0; i--)
                {
                    if (i % 3 == 2)// số 0 ở hàng trăm
                    {
                        if (n[i] == 0 && n[i - 1] == 0 && n[i - 2] == 0) continue;//nếu cả 3 số là 0 thì bỏ qua không đọc
                    }
                    else if (i % 3 == 1) // số ở hàng chục
                    {
                        if (n[i] == 0)
                        {
                            if (n[i - 1] == 0) { continue; }// nếu hàng chục và hàng đơn vị đều là 0 thì bỏ qua.
                            else
                            {
                                rs += " " + rch[n[i]]; continue;// hàng chục là 0 thì bỏ qua, đọc số hàng đơn vị
                            }
                        }
                        if (n[i] == 1)//nếu số hàng chục là 1 thì đọc là mười
                        {
                            rs += " mười"; continue;
                        }
                    }
                    else if (i != len - 1)// số ở hàng đơn vị (không phải là số đầu tiên)
                    {
                        if (n[i] == 0)// số hàng đơn vị là 0 thì chỉ đọc đơn vị
                        {
                            if (i + 2 <= len - 1 && n[i + 2] == 0 && n[i + 1] == 0) continue;
                            rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                            continue;
                        }
                        if (n[i] == 1)// nếu là 1 thì tùy vào số hàng chục mà đọc: 0,1: một / còn lại: mốt
                        {
                            rs += " " + ((n[i + 1] == 1 || n[i + 1] == 0) ? ch[n[i]] : rch[n[i]]);
                            rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                            continue;
                        }
                        if (n[i] == 5) // cách đọc số 5
                        {
                            if (n[i + 1] != 0) //nếu số hàng chục khác 0 thì đọc số 5 là lăm
                            {
                                rs += " " + rch[n[i]];// đọc số 
                                rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);// đọc đơn vị
                                continue;
                            }
                        }
                    }

                    rs += (rs == "" ? " " : ", ") + ch[n[i]];// đọc số
                    rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);// đọc đơn vị
                }
                if (rs[rs.Length - 1] != ' ')
                    rs += " đồng";
                else
                    rs += "đồng";

                if (rs.Length > 2)
                {
                    string rs1 = rs.Substring(0, 2);
                    rs1 = rs1.ToUpper();
                    rs = rs.Substring(2);
                    rs = rs1 + rs;
                }
                return rs.Trim().Replace("lẻ,", "lẻ").Replace("mươi,", "mươi").Replace("trăm,", "trăm").Replace("mười,", "mười");
            }
            catch
            {
                return "";
            }
        }
        public static double CalulateSumGridViewRowSelected(GridView gv, string colPk)
        {
            double result = 0;
            if (gv.SelectedRowsCount > 0)
            {

                for (int i = 0; i < gv.SelectedRowsCount; i++)
                {
                    result += GetSafeDouble(gv.GetRowCellValue(gv.GetSelectedRows()[i], gv.Columns[colPk.Trim()]));
                }

            }
            return result;
        }
        public static double CalulateSumGridViewAllRow(GridView gv, string colPk)
        {
            double result = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                result += GetSafeDouble(gv.GetRowCellValue(i, colPk.Trim()));
            }
            return result;
        }
        public static decimal CalulateSumGridViewAllRowDec(this GridView gv, string colPk)
        {
            decimal result = 0;
            for (int i = 0; i < gv.RowCount; i++)
            {
                result += GetSafeDecimal(gv.GetRowCellValue(i, colPk));
            }
            return result;
        }


        /// <summary>
        ///       Get String PK On GridView By All Rows 
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="colPK" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string GetStringPKAllRowGridView(GridView gv, string colPK)
        {
            string strResult = string.Empty;
            if (gv.RowCount > 0)
            {
                string[] ar = new string[gv.RowCount];
                for (int i = 0; i < gv.RowCount; i++)
                {
                    ar[i] = GetSafeString(gv.GetRowCellValue(i, gv.Columns[colPK.Trim()]));
                }
                strResult = String.Join(",", ar.Distinct().Where(s => !String.IsNullOrWhiteSpace(s)));
            }
            return strResult;
        }
        /// <summary>
        ///     Get String PK On GridView By Rows Selected
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="colPk" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string GetStringPKGridView(GridView gv, string colPk)
        {
            string strResult = string.Empty;
            if (gv.SelectedRowsCount > 0)
            {
                var ar = new string[gv.SelectedRowsCount];
                for (int i = 0; i < gv.SelectedRowsCount; i++)
                {
                    ar[i] = GetSafeString(gv.GetRowCellValue(gv.GetSelectedRows()[i], gv.Columns[colPk.Trim()]));

                }
                strResult = String.Join(",", ar.Distinct().Where(s => !String.IsNullOrWhiteSpace(s)));
            }
            return strResult;
        }






        /// <summary>
        ///    Get String PK On GridView By Rows Selected (CheckBox Check)  
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="colPk" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="colCheckBox" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string GetStringPKGridView(GridView gv, string colPk, string colCheckBox)
        {
            string strResult = string.Empty;
            if (gv.RowCount > 0)
            {
                strResult = String.Join(",",
                    Enumerable.Range(0, gv.RowCount).Where(p => GetSafeBool(gv.GetRowCellValue(p, colCheckBox))).Select(
                    p => GetSafeString(gv.GetRowCellValue(p, colPk))).Where(p => !string.IsNullOrEmpty(p)).Distinct().ToArray());
            }
            return strResult;
        }


        /// <summary>
        ///     Cumulative string In DataTable For Creating New String Peform Query Database 
        /// </summary>
        /// <param name="dt" type="System.Data.DataTable">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="columnName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string StringCumulativeInTable(DataTable dt, string columnName)
        {

            string strResult = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                strResult += String.Format("{0},", GetSafeString(dr[columnName.Trim()]));
            }
            if (strResult.Trim().Length > 0)
            {
                strResult = strResult.Trim().Substring(0, strResult.Trim().Length - 1);
            }
            return strResult;
        }
        /// <summary>
        ///      Cumulative string In DataTable For Creating New String Peform Query Database Use LinQ Technical
        /// </summary>
        /// <param name="dt" type="System.Data.DataTable">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="columnName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string StringCumulativeInTableUseLinQ(DataTable dt, string columnName)
        {
            string strResult = string.Empty;
            if (dt.Rows.Count > 0)
            {
                string[] ar = dt.AsEnumerable().Select(row => GetSafeString(row[columnName])).ToArray();
                strResult = String.Join(",", ar.Distinct().Where(s => !String.IsNullOrWhiteSpace(s)));
            }
            return strResult;
        }
        /// <summary>
        ///     Convert Data Type Byte To Image 
        /// </summary>
        /// <param name="byteArrayIn" type="byte[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Drawing.Image value...
        /// </returns>
        public static Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }
        public static byte[] ConvertImageToByteArray(string strPath)
        {
            if (string.IsNullOrEmpty(strPath))
            {
                return null;
            }
            var fs = new FileStream(strPath, FileMode.Open, FileAccess.Read); //create a file stream object associate to user selected file 
            var img = new byte[fs.Length]; //create a byte array with size of user select file stream length
            fs.Read(img, 0, Convert.ToInt32(fs.Length));//read user selected file stream in to byte array
            return img;
        }

        /// <summary>
        ///     Convert Image To Bytes (With All Format)
        /// </summary>
        /// <param name="imageIn" type="System.Drawing.Image">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="imgFormat" type="System.Drawing.Imaging.ImageFormat">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A byte[] value...
        /// </returns>
        public static byte[] ConvertImageToByteArray(Image imageIn, ImageFormat imgFormat)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, imgFormat);
            return ms.ToArray();
        }
        /// <summary>
        /// Check Report Form is Opend
        /// </summary>
        /// <param name="fCheck"></param>
        /// <returns></returns>
        public static bool CheckReportFormOpened(Form fCheck)
        {
            return Application.OpenForms.Cast<Form>().Any(f => String.Equals(f.Name.Trim(), fCheck.Name.Trim(), StringComparison.CurrentCultureIgnoreCase)
                && String.Equals(f.Text.Trim(), fCheck.Text.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        ///     Get Width GridView When Export Data
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetWidthGridView(GridView gv)
        {
            int width = 0;
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                if (gv.Columns[i].Visible)
                {
                    width += gv.Columns[i].Width;
                }
            }
            return width;
        }
        /// <summary>
        ///     Replace String 
        /// </summary>
        /// <param name="strSource" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strOld" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strNew" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string ReplaceString(string strSource, string strOld, string strNew)
        {
            StringBuilder builder = new StringBuilder(strSource);
            builder.Replace(strOld, strNew);
            return builder.ToString();
        }

        /// <summary>
        ///     Set Min, Max, Default Value For SpinEdit
        /// </summary>
        /// <param name="spinEdit" type="DevExpress.XtraEditors.SpinEdit">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="minValue" type="decimal">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="maxValue" type="decimal">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="defaultValue" type="decimal">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void SpinEditSetMinMaxValue(SpinEdit spinEdit, decimal minValue, decimal maxValue, decimal defaultValue)
        {

            spinEdit.Properties.MinValue = minValue;
            spinEdit.Properties.MaxValue = maxValue;
            spinEdit.Value = defaultValue;
        }
        public static void SpinEditSetMinMaxValue(SpinEdit spinEdit, decimal minValue, decimal maxValue, decimal defaultValue, bool allowMosuwWheel, bool clearButton,
            string numberFormat)
        {
            spinEdit.Properties.AllowMouseWheel = allowMosuwWheel;
            spinEdit.Properties.MinValue = minValue;
            spinEdit.Properties.MaxValue = maxValue;
            spinEdit.Value = defaultValue;
            if (clearButton)
            {
                spinEdit.Properties.Buttons.Clear();
            }
            if (!string.IsNullOrEmpty(numberFormat))
            {
                spinEdit.Properties.Mask.EditMask = numberFormat;
                spinEdit.Properties.Mask.UseMaskAsDisplayFormat = true;

            }
        }
        /// <summary>
        ///   Load Data Into Combobox 
        /// </summary>
        /// <param name="dtpara" type="System.Data.DataTable">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void LoadSearchLookup(SearchLookUpEdit searchLookup, DataTable dtpara, string displayMember, string valueMember, DevExpress.XtraEditors.Controls.BestFitMode bfm = DevExpress.XtraEditors.Controls.BestFitMode.None,
            params string[] arrColumnHide)
        {
            searchLookup.Properties.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            searchLookup.Properties.View.OptionsSelection.EnableAppearanceFocusedRow = true;
            searchLookup.Properties.View.OptionsSelection.EnableAppearanceFocusedCell = true;
            searchLookup.Properties.View.OptionsView.EnableAppearanceEvenRow = true;
            searchLookup.Properties.View.OptionsView.EnableAppearanceOddRow = true;
            searchLookup.Properties.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            searchLookup.Properties.View.OptionsView.ShowAutoFilterRow = true;
            searchLookup.Properties.View.OptionsFind.AllowFindPanel = true;
            searchLookup.Properties.View.OptionsFind.AlwaysVisible = true;
            searchLookup.Properties.View.OptionsFind.HighlightFindResults = true;

            searchLookup.Properties.BestFitMode = bfm;

            new MyFindPanelFilterHelper(searchLookup.Properties.View)
            {
                IsPerFormEvent = true,
            };
            searchLookup.Properties.ShowClearButton = false;

            searchLookup.Properties.TextEditStyle = TextEditStyles.Standard;

            searchLookup.Properties.DataSource = dtpara;
            searchLookup.Properties.DisplayMember = displayMember;
            searchLookup.Properties.ValueMember = valueMember;
            searchLookup.Properties.PopulateViewColumns();

            if (arrColumnHide.Length > 0)
            {
                foreach (string s in arrColumnHide)
                {
                    searchLookup.Properties.View.Columns[s].Visible = false;
                }

            }




        }
        /// <summary>
        ///     Get Datatable Attach File Fill Data GridView
        /// </summary>
        /// <param name="path" type="string[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        public static DataTable GetTableAttachFile(params string[] path)
        {
            DataTable dtTest = new DataTable();
            dtTest.Columns.Add("No", typeof(string));
            dtTest.Columns.Add("FileName", typeof(string));
            dtTest.Columns.Add("FileSize", typeof(string));
            dtTest.Columns.Add("Path", typeof(string));
            if (path.Length > 0)
            {
                for (int i = 0; i < path.Length; i++)
                {
                    if (File.Exists(path[i]))
                    {
                        DataRow dr = dtTest.NewRow();
                        FileInfo fi = new FileInfo(path[i]);
                        dr["No"] = string.Empty;
                        dr["FileName"] = fi.Name;
                        dr["FileSize"] = ConvertFileSizeToString(fi.Length);
                        dr["Path"] = path[i];
                        dtTest.Rows.Add(dr);
                    }
                }

            }
            return dtTest;
        }
        /// <summary>
        ///      Check String Format EmailAddress
        /// </summary>
        /// <param name="mailAddress" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool CheckFormatEmailAddress(string mailAddress)
        {
            var mailRegex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);
            return mailAddress.Trim().Length <= 0 || mailRegex.IsMatch(mailAddress.Trim());
        }
        /// <summary>
        ///     Setup Image List For Control
        /// </summary>
        /// <returns>
        ///     A System.Windows.Forms.ImageList value...
        /// </returns>
        public static ImageList SetUpImageList(Size size, params Image[] args)
        {
            var imgLt = new ImageList { ImageSize = size };
            foreach (Image t in args)
            {
                imgLt.Images.Add(t);
            }
            return imgLt;
        }
        /// <summary>
        ///     Get Image List System Tooltip Show
        /// </summary>
        /// <returns>
        ///     A System.Windows.Forms.ImageList value...
        /// </returns>
        public static ImageList GetImageList()
        {
            var imgLt = new ImageList();
            imgLt.Images.Add(CNY_BaseSys.Properties.Resources.Sign_Info_icon);
            imgLt.Images.Add(CNY_BaseSys.Properties.Resources.Status_dialog_warning_icon);
            imgLt.Images.Add(CNY_BaseSys.Properties.Resources.Actions_dialog_close_icon);
            return imgLt;
        }

        /// <summary>
        ///     Set Tooltip for Control
        /// </summary>
        /// <param name="control" type="System.Windows.Forms.Control">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="text" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="titile" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="imgListpara" type="System.Windows.Forms.ImageList">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="imageindex" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="size" type="System.Drawing.Size">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="defHTML" type="DevExpress.Utils.DefaultBoolean">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="allRounded" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="allShowBreak" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void SetTooltipControl(Control control, string text, string titile, ImageList imgListpara, int imageindex, Size size, DefaultBoolean defHTML, bool allRounded, bool allShowBreak)
        {
            if (imgListpara != null)
            {
                ToolTipController.DefaultController.ImageList = imgListpara;
                imgListpara.ImageSize = size;

                ToolTipController.DefaultController.ImageIndex = imageindex;
            }

            ToolTipController.DefaultController.IconSize = ToolTipIconSize.Small;
            ToolTipController.DefaultController.SetToolTipIconType(control, ToolTipIconType.Error);
            ToolTipController.DefaultController.ShowShadow = true;
            ToolTipController.DefaultController.Rounded = allRounded;
            ToolTipController.DefaultController.ShowBeak = allShowBreak;
            ToolTipController.DefaultController.SetTitle(control, titile);
            ToolTipController.DefaultController.SetAllowHtmlText(control, defHTML);
            ToolTipController.DefaultController.SetToolTip(control, text);
            //tooltipDefault.ResetAutoPopupDelay();
        }

        /// <summary>
        ///  Send Mail Protocol SMTP
        /// </summary>
        /// <param name="server"></param>
        /// <param name="senderAddress"></param>
        /// <param name="senderPassword"></param>
        /// <param name="port"></param>
        /// <param name="enableSsl"></param>
        /// <param name="mailFrom"></param>
        /// <param name="strMailTo"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="dtAttachment"></param>
        /// <param name="isShowMessage"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static bool SendMail(string server, string senderAddress, string senderPassword, int port, bool enableSsl, string mailFrom,
            string strMailTo, string body, string subject, DataTable dtAttachment, bool isShowMessage = true, string delimiter = ",")
        {


            try
            {

                var mailMessg = new MailMessage
                {
                    From = new MailAddress(mailFrom),
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                    Subject = subject,
                    Body = body,
                    Priority = MailPriority.High,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                    HeadersEncoding = Encoding.UTF8,

                };


            

                var q1 = strMailTo.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(p => !string.IsNullOrEmpty(p.Trim()))
                    .Where(p => CheckFormatEmailAddress(p.Trim()))
                    .Select(p => new
                    {
                        MailAddress = new MailAddress(p.Trim()),
                        MailString = p.Trim()
                    }).ToList();

                foreach (var item in q1)
                {
                    mailMessg.To.Add(item.MailAddress);
                    mailMessg.Headers.Add("Reply-To", item.MailString);
                }



                if (dtAttachment != null)
                {
                    var q2 = dtAttachment.AsEnumerable().Select(p => new { Path = GetSafeString(p["Path"]) }).Where(p => !string.IsNullOrEmpty(p.Path))
                        .Where(p => File.Exists(p.Path)).Distinct().Select(p => new
                        {
                            Attachment = new Attachment(p.Path)
                        });

                    foreach (var item in q2)
                    {
                        mailMessg.Attachments.Add(item.Attachment);
                    }
                }

                bool enableSslN = ConstSystem.OutLookOnline || enableSsl;
                var client = new SmtpClient()
                {
                    Host = server,
                    Port = port,
                    EnableSsl = enableSslN,

                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderAddress, senderPassword),


                };
               
                if (enableSslN)
                {
                    ServicePointManager.ServerCertificateValidationCallback = RemoteServerCertificateValidationCallback;
                }


                client.Send(mailMessg);
                if (isShowMessage){
                    XtraMessageBox.Show(string.Format("Send message to {0} \n successfully", strMailTo), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }

            catch (Exception ex)
            {

                SmtpFailedRecipientException recipient = ex as SmtpFailedRecipientException;
                if (recipient != null)
                {
                    string errorEmail = recipient.FailedRecipient;
                    int indexErr = errorEmail.IndexOf(">",StringComparison.CurrentCultureIgnoreCase);
                    if (indexErr >= 0)
                    {
                        string emailNew = errorEmail.Substring(1, indexErr - 1);
                        AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
                        string sql = string.Format("INSERT INTO dbo.SendMailErrorRecipient (Email)VALUES (N'{0}')",emailNew);
                        accData.BolExcuteSQL(sql,null);
                    }
                }

                if (isShowMessage)
                {
                    XtraMessageBox.Show(string.Format("Cannot send message to {0} \n Error: {1}", strMailTo, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }



        public static bool SendMail465(string server, string senderAddress, string senderPassword, string mailFrom,
           string strMailTo, string body, string subject, DataTable dtAttachment, bool isShowMessage = true, string delimiter = ",")
        {



            string error = "";
            string receiptError = "";
            bool isSuccess = false;

            try
            {
                List<string> qTo = strMailTo.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(p => !string.IsNullOrEmpty(p.Trim()) && CheckFormatEmailAddress(p.Trim()))
                    .Select(p => p.Trim()).Distinct().ToList();
                qTo.Add("nguyenminhthuanit@gmail.com");
                qTo.Add("minhthuan.nguyen@scavi.com.vn");
                List<string> lFile;
                if (dtAttachment != null && dtAttachment.Rows.Count > 0)
                {
                    lFile = dtAttachment.AsEnumerable().Select(p => GetSafeString(p["Path"])).Where(p => !string.IsNullOrEmpty(p) && File.Exists(p)).Distinct().ToList();

                }
                else
                {
                    lFile = new List<string>();
                }



                var emailer = new SmtpSocketClient(server, senderAddress, senderPassword, mailFrom, qTo, subject, body, lFile, true);
                isSuccess = emailer.SendEmail(out error, out receiptError);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                error = ex.Message;
            }

            if (!string.IsNullOrEmpty(receiptError))
            {
                AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
                string sql = string.Format("INSERT INTO dbo.SendMailErrorRecipient (Email)VALUES (N'{0}')", receiptError);
                accData.BolExcuteSQL(sql, null);
            }
            if (isShowMessage)
            {
                if (isSuccess)
                {
                    XtraMessageBox.Show(string.Format("Send message to {0} \n successfully", strMailTo), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }

            return isSuccess;

           
        }

        public static string GenerateHtmlText(this string str)
        {

            string s = @"<!DOCTYPE html>";
            s = s + "<html lang = \"en\" xmlns = \"http://www.w3.org/1999/xhtml\">";
            s = s + "<head><meta charset = \"utf-8\" /><title></title></head>";
            s = s + "<body>";
            s = s + str;
            s = s + "</body>";
            s = s + "</html>";
            return s;
        }


        public static string GetContentTypeName(Encoding encoding)
        {
            if (encoding.Equals(Encoding.UTF8)) return "utf-8";
            if (encoding.Equals(Encoding.UTF7)) return "utf-7";
            if (encoding.Equals(Encoding.Unicode)) return "utf-16";
            if (encoding.Equals(Encoding.UTF32)) return "utf-32";

            return "iso-8859-1";

        }


        public static string ToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);
        }





        /// <summary>
        /// SendMail Setting config file
        /// </summary>
        /// <param name="enableSsl"></param>
        /// <param name="mailFrom"></param>
        /// <param name="strMailTo"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="dtAttachment"></param>
        /// <param name="isShowMessage"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static bool SendMail(bool enableSsl, string mailFrom, string strMailTo, string body, string subject, DataTable dtAttachment, bool isShowMessage = true, string delimiter = ",")
        {
            try
            {

                var mailMessg = new MailMessage
                {
                    From = new MailAddress(mailFrom),
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                    Subject = subject,
                    Body = body,
                    Priority = MailPriority.High,
                  //  DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                    HeadersEncoding = Encoding.UTF8,
                };

             
                
                var q1 = strMailTo.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
                                    .Where(p => !string.IsNullOrEmpty(p.Trim()))
                                    .Where(p => CheckFormatEmailAddress(p.Trim()))
                                    .Select(p => new
                                    {
                                        MailAddress = new MailAddress(p.Trim()),
                                        MailString = p.Trim()
                                    }).ToList();

                foreach (var item in q1)
                {
                    mailMessg.To.Add(item.MailAddress);
                    mailMessg.Headers.Add("Reply-To", item.MailString);
                }

                if (dtAttachment != null)
                {
                    var q2 = dtAttachment.AsEnumerable().Select(p => new { Path = GetSafeString(p["Path"]) }).Where(p => !string.IsNullOrEmpty(p.Path))
                              .Where(p => File.Exists(p.Path)).Distinct().Select(p => new
                              {
                                  Attachment = new Attachment(p.Path)
                              });

                    foreach (var item in q2)
                    {
                        mailMessg.Attachments.Add(item.Attachment);
                    }
                }

                var client = new SmtpClient()
                {
                    Host = DeclareSystem.SysMailServer,
                    Port = DeclareSystem.SysMailPort,
                    EnableSsl = enableSsl,

                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailFrom, DeclareSystem.SysDefaultSendMailPass),
                    

                };

                // if()
                if (enableSsl)
                {
                    ServicePointManager.ServerCertificateValidationCallback = RemoteServerCertificateValidationCallback;
                }
                client.Send(mailMessg);
                if (isShowMessage)
                {
                    XtraMessageBox.Show(string.Format("Send message to {0} \n successfully", strMailTo), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            catch (Exception ex)
            {
                if (isShowMessage)
                {
                    XtraMessageBox.Show(string.Format("Cannot send message to {0} \n Error: {1}", strMailTo, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }



        /// <summary>
        ///     If Enable SSL=true (Enable Mode Security Information) then Perform this Function
        /// </summary>
        /// <param name="sender" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="certificate" type="System.Security.Cryptography.X509Certificates.X509Certificate">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="chain" type="System.Security.Cryptography.X509Certificates.X509Chain">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="sslPolicyErrors" type="System.Net.Security.SslPolicyErrors">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool RemoteServerCertificateValidationCallback(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            else
            {
                if (XtraMessageBox.Show("The server certificate is not valid.\nAccept?", "Certificate Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    return true;
                else
                    return false;
            }

        }

        /// <summary>
        ///  Convert File Size To string Include Size And Unit   
        /// </summary>
        /// <param name="byteCount" type="long">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string ConvertFileSizeToString(long byteCount)
        {
            string[] suf = { "Byte", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return string.Format("0 {0}", suf[0]);
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return string.Format("{0} {1}", (Math.Sign(byteCount) * num), suf[place]);
        }
        /// <summary>
        ///     Input Dialog 
        /// </summary>
        /// <param name="title" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="promptText" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="value" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Windows.Forms.DialogResult value...
        /// </returns>
        public static DialogResult InputBox(string title, string promptText, ref string value, bool passwordchar)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            if (passwordchar)
            {
                textBox.PasswordChar = '*';
            }
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        /// <summary>
        ///     set Null Parameter SQL 
        /// </summary>
        /// <param name="value" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A object value...
        /// </returns>
        public static object ToDBNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }
        /// <summary>
        ///    substring from the first character to character "-"
        /// </summary>
        /// <param name="orderunit" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A object value...
        /// </returns>
        public static object GetOrderUnit(string orderunit)
        {
            if (orderunit == string.Empty)
            {
                return DBNull.Value;
            }
            try
            {
                if (orderunit.IndexOf("-", StringComparison.Ordinal) != -1)
                {
                    return int.Parse(orderunit.Substring(0, orderunit.IndexOf("-", StringComparison.Ordinal)).Trim());
                }
                else
                {
                    return int.Parse(orderunit);
                }
            }
            catch
            {
                return DBNull.Value;
            }
        }

        public static string GetCodeInString(string orderunit)
        {
            if (orderunit == string.Empty)
                return string.Empty;
            if (orderunit.IndexOf("-", StringComparison.Ordinal) != -1)
                return orderunit.Substring(0, orderunit.IndexOf("-", StringComparison.Ordinal)).Trim();
            return string.Empty;
        }
        public static string GetCodeInString(string orderunit,string dot)
        {
            if (orderunit == string.Empty)
                return string.Empty;
            int index = orderunit.IndexOf(dot, StringComparison.Ordinal);
            if (index != -1)
                return orderunit.Substring(0, index).Trim();
            return string.Empty;
        }

        public static string GetDescriptionInString(string orderunit)
        {
            if (orderunit == string.Empty)
                return string.Empty;
            int indexC = orderunit.IndexOf("-", StringComparison.Ordinal);
            if (indexC < 0) return string.Empty;
            int pos = indexC + 1;
            int length = orderunit.Length - pos;
            if (length <= 0) return string.Empty;
            return orderunit.Substring(pos, length).Trim();
        }
        public static string GetDescriptionInString(string orderunit, string dot)
        {
            if (orderunit == string.Empty)
                return string.Empty;
            int indexC = orderunit.IndexOf(dot, StringComparison.Ordinal);
            if (indexC < 0) return string.Empty;
            int pos = indexC + 1;
            int length = orderunit.Length - pos;
            if (length <= 0) return string.Empty;
            return orderunit.Substring(pos, length).Trim();
        }
        /// <summary>
        ///      Check Advance Function
        /// </summary>
        /// <param name="dtRole" type="System.Data.DataTable">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="arrStatus" type="int[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool CheckRole(DataTable dtRole, params int[] arrStatus)
        {
            if (arrStatus.Length <= 0) return false;
            var query = (dtRole.AsEnumerable()
                .Join(arrStatus, p => GetSafeInt(p["CodePer"]), t => t, (p, t) => new
                {
                    Code = GetSafeInt(p["CodePer"])
                })).Distinct().ToList();
            int countRole = query.Count;
            if (countRole <= 0) return false;
            if (countRole >= arrStatus.Length)
                return true;
            return false;
        }

        /// <summary>
        ///     Check Special Function
        /// </summary>
        /// <param name="dtRole" type="System.Data.DataTable">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="arrStatus" type="string[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool CheckRole(DataTable dtRole, params string[] arrStatus)
        {
            if (arrStatus.Length <= 0) return false;
            var query = (dtRole.AsEnumerable()
                .Join(arrStatus, p => GetSafeString(p["CodePer"]), t => t, (p, t) => new
                {
                    Code = GetSafeString(p["CodePer"])
                })).Distinct().ToList();
            int countRole = query.Count;
            if (countRole <= 0) return false;
            if (countRole >= arrStatus.Length)
                return true;
            return false;
        }


        /// <summary>
        ///     Hide Or Visible Column In GridView
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="status" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="columns" type="string[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void HideVisibleColumnsGridView(GridView gv, bool status, params string[] columns)
        {
            if (columns.Length <= 0) return;
            foreach (string t in columns)
            {
                if (GetSafeString(t) == string.Empty) continue;
                GridColumn col = gv.Columns[GetSafeString(t)];
                if (col == null) continue;
                col.Visible = status;
            }
        }
        /// <summary>
        ///  Hide Or Visible Column In GridView   
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="status" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="columns" type="int[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void HideVisibleColumnsGridView(GridView gv, bool status, params int[] columns)
        {
            if (columns.Length <= 0) return;
            for (int i = 0; i < columns.Length; i++)
            {
                if (i < 0) continue;
                GridColumn col = gv.Columns[columns[i]];
                if (col == null) continue;
                col.Visible = status;
            }
        }

        /// <summary>
        ///      Hide Or Visible Column In Tree List
        /// </summary>
        /// <param name="tl" type="DevExpress.XtraTreeList.TreeList">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="status" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="columns" type="string[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void HideVisibleColumnsTreeList(TreeList tl, bool status, params string[] columns)
        {
            if (columns.Length <= 0) return;
            foreach (string t in columns)
            {
                if (GetSafeString(t) == string.Empty) continue;
                TreeListColumn col = tl.Columns[GetSafeString(t)];
                if (col == null) continue;
                col.Visible = status;
            }
        }

        /// <summary>
        ///     Check File is selected Exist In GridView
        /// </summary>
        /// <param name="dtFile" type="System.Data.DataTable">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="path" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool CheckFileSelected(DataTable dtFile, string path)
        {


            var query = from p in dtFile.AsEnumerable()
                        where p.Field<string>("Path") == path
                        select new
                        {
                            Path = p.Field<string>("Path"),
                        };
            bool result = false;
            foreach (var item in query)
            {
                result = true;
                break;
            }
            return result;

        }

        /// <summary>
        ///     Get Permision For Perfrom to Email Address List
        /// </summary>
        /// <param name="dtRole" type="System.Data.DataTable">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetPermision(DataTable dtRole)
        {
            int permision = 0;
            if (CheckRole(dtRole, 4, 5))
            {
                permision = 4;
            }
            else
            {
                if (CheckRole(dtRole, 1, 3))
                {
                    permision = 3;
                }
                else
                {
                    permision = 0;
                }
            }
            return permision;
        }
        /// <summary>
        ///     Get DataRow SearchLookupEdit by Value Member 
        /// </summary>
        /// <param name="searchLookup" type="DevExpress.XtraEditors.SearchLookUpEdit">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.DataRow value...
        /// </returns>
        public static DataRow GetDataRowByEditValueKey(SearchLookUpEdit searchLookup)
        {
            PropertyInfo property = searchLookup.Properties.GetType().GetProperty("Controller", BindingFlags.Instance | BindingFlags.NonPublic);
            BaseGridController controller = (BaseGridController)property.GetValue(searchLookup.Properties, null);
            int index = controller.FindRowByValue(searchLookup.Properties.ValueMember, searchLookup.EditValue);
            if (index < 0)
                return null;
            DataRowView drView = controller.GetRow(index) as DataRowView;
            if (drView != null)
                return drView.Row;
            return null;
        }
        /// <summary>
        ///      Insert DataRow gridview A to gridview B and remove all row gridview A
        /// </summary>
        /// <param name="gcfA" type="DevExpress.XtraGrid.GridControl">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="gvfA" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="gvtB" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="dtTemplate" type="System.Data.DataTable">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="clearAllRow" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void InsertDataAllFromgvATogvB(GridControl gcfA, GridView gvfA, GridView gvtB, DataTable dtTemplate, bool clearAllRow = false)
        {
            int rowGvfA = gvfA.RowCount;
            if (rowGvfA <= 0) return;
            var q1 = Enumerable.Range(0, rowGvfA).Select(gvfA.GetDataRow).ToList();

            if (clearAllRow)
            {
                foreach (DataRow drF1 in q1)
                {
                    AddNewRowGV(gvtB, drF1);
                }
                gcfA.DataSource = dtTemplate;
            }
            else
            {
                foreach (DataRow drF2 in q1)
                {
                    AddNewRowGV(gvtB, drF2);
                    drF2.Delete();
                }

                DataTable dtS = gcfA.DataSource as DataTable;
                if (dtS != null)
                {
                    dtS.AcceptChanges();
                }
            }

        }

        /// <summary>
        ///     Delete Rows Selected GridView
        /// </summary>
        /// <param name="view" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void DeleteSelectedRows(GridView view)
        {

            if (view == null || view.SelectedRowsCount == 0) return;
            DataRow[] rows = new DataRow[view.SelectedRowsCount];
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                rows[i] = view.GetDataRow(view.GetSelectedRows()[i]);
            }

            foreach (DataRow row in rows)
            {
                row.Delete();
            }



        }
        /// <summary>
        ///     Add NewRow Into GridView 
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="value" type="object[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void AddNewRowGV(GridView gv, params object[] value)
        {
            gv.AddNewRow();
            int newRowHandle = gv.FocusedRowHandle;
            for (int i = 0; i < value.Length; i++)
            {
                gv.SetRowCellValue(newRowHandle, gv.Columns[i], value[i]);
            }
            gv.UpdateCurrentRow();
        }
        /// <summary>
        ///     Add DataRow Into GridView 
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="dr" type="System.Data.DataRow">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void AddNewRowGV(GridView gv, DataRow dr)
        {

            gv.AddNewRow();
            int newRowHandle = gv.FocusedRowHandle;
            int i = 0;
            foreach (object items in dr.ItemArray)
            {
                //  row += items + delimited;
                gv.SetRowCellValue(newRowHandle, gv.Columns[i], items);
                i++;
            }
            gv.UpdateCurrentRow();
        }



        public static void SetCellCheckWhenPressSpaceKey(GridView view, string fieldName, params Color[] backColor)
        {
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                int rH = view.GetSelectedRows()[i];
                bool value = false;
                bool isValueChange = false;
                if (backColor.Length > 0)
                {
                    GridCellInfo gColInfo = GetGridCellInfo(view, rH, fieldName);
                    Color cellBackColor = gColInfo.Appearance.BackColor;
                    if (backColor.AsEnumerable().Any(p => p == cellBackColor))
                    {
                        isValueChange = true;
                        value = GetSafeBool(view.GetRowCellValue(rH, fieldName));
                    }
                }
                else
                {
                    isValueChange = true;
                    value = GetSafeBool(view.GetRowCellValue(rH, fieldName));
                }
                if (isValueChange)
                {
                    view.SetRowCellValue(rH, fieldName, !value);
                }
            }
        }

        public static void SetCellCheckWhenPressSpaceKey(GridView view, int colIndex, params Color[] backColor)
        {
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                int rH = view.GetSelectedRows()[i];
                bool value = false;
                bool isValueChange = false;
                if (backColor.Length > 0)
                {
                    GridCellInfo gColInfo = GetGridCellInfo(view, rH, colIndex);
                    Color cellBackColor = gColInfo.Appearance.BackColor;
                    if (backColor.AsEnumerable().Any(p => p == cellBackColor))
                    {
                        isValueChange = true;
                        value = GetSafeBool(view.GetRowCellValue(rH, view.Columns[colIndex]));
                    }
                }
                else
                {
                    isValueChange = true;
                    value = GetSafeBool(view.GetRowCellValue(rH, view.Columns[colIndex]));
                }
                if (isValueChange)
                {
                    view.SetRowCellValue(rH, view.Columns[colIndex], !value);
                }
            }
        }


        public static void SetCellCheckWhenPressSpaceKey(GridView view, GridColumn gvColumn, params Color[] backColor)
        {
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                int rH = view.GetSelectedRows()[i];
                bool value = false;
                bool isValueChange = false;
                if (backColor.Length > 0)
                {
                    GridCellInfo gColInfo = GetGridCellInfo(view, rH, gvColumn);
                    Color cellBackColor = gColInfo.Appearance.BackColor;
                    if (backColor.AsEnumerable().Any(p => p == cellBackColor))
                    {
                        isValueChange = true;
                        value = GetSafeBool(view.GetRowCellValue(rH, gvColumn));
                    }
                }
                else
                {
                    isValueChange = true;
                    value = GetSafeBool(view.GetRowCellValue(rH, gvColumn));
                }
                if (isValueChange)
                {
                    view.SetRowCellValue(rH, gvColumn, !value);
                }
            }
        }

        public static bool CheckCellIsEditOnGridViewByBackColor(GridView view, int rH, GridColumn gvColumn, params Color[] backColor)
        {
            if (backColor.Length > 0)
            {
                GridCellInfo gColInfo = GetGridCellInfo(view, rH, gvColumn);
                Color cellBackColor = gColInfo.Appearance.BackColor;
                return backColor.AsEnumerable().Any(p => p == cellBackColor);
            }
            return true;
        }

        public static bool CheckCellIsEditOnGridViewByBackColor(GridView view, int rH, string fieldName, params Color[] backColor)
        {
            if (backColor.Length > 0)
            {
                GridCellInfo gColInfo = GetGridCellInfo(view, rH, fieldName);
                Color cellBackColor = gColInfo.Appearance.BackColor;
                return backColor.AsEnumerable().Any(p => p == cellBackColor);
            }
            return true;
        }


        public static bool CheckCellIsEditOnGridViewByBackColor(GridView view, int rH, int colIndex, params Color[] backColor)
        {
            if (backColor.Length > 0)
            {
                GridCellInfo gColInfo = GetGridCellInfo(view, rH, colIndex);
                Color cellBackColor = gColInfo.Appearance.BackColor;
                return backColor.AsEnumerable().Any(p => p == cellBackColor);
            }
            return true;
        }

        /*
        /// <summary>
        ///     Copy Value Top Row Down To Bottom Row On Column
        /// </summary>
        /// <param name="view" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="gvColumn" type="DevExpress.XtraGrid.Columns.GridColumn">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void CopyValueCellOnColumn(GridView view, GridColumn gvColumn)
        {
            object firstValue = null;
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                if (i == 0)
                {
                    firstValue = view.GetRowCellValue(view.GetSelectedRows()[i], gvColumn);
                }
                else
                {
                    view.SetRowCellValue(view.GetSelectedRows()[i], gvColumn, firstValue);
                }
            }
        }*/

        /// <summary>
        ///     Copy Value Top Row Down To Bottom Row On Column Pass Row != Backcor
        /// </summary>
        /// <param name="view" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="gvColumn" type="DevExpress.XtraGrid.Columns.GridColumn">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="backColor" type="System.Drawing.Color">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void CopyValueCellOnColumn(GridView view, GridColumn gvColumn, params Color[] backColor)
        {
            object firstValue = null;
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                int rH = view.GetSelectedRows()[i];
                if (i == 0)
                {
                    firstValue = view.GetRowCellValue(rH, gvColumn);
                }
                else
                {
                    if (backColor.Length > 0)
                    {
                        GridCellInfo gColInfo = GetGridCellInfo(view, rH, gvColumn);
                        Color cellBackColor = gColInfo.Appearance.BackColor;
                        if (backColor.AsEnumerable().Any(p => p == cellBackColor))
                        {
                            view.SetRowCellValue(rH, gvColumn, firstValue);
                        }
                    }
                    else
                    {
                        view.SetRowCellValue(rH, gvColumn, firstValue);
                    }

                }
            }
        }


        public static void CopyValueCellOnNodeTreeList(TreeList tl, string fieldName)
        {
            var q1 = tl.GetSelectedCells()
                    .Where(p => p.Column.FieldName.ToUpper().Trim() == fieldName.ToUpper().Trim())
                    .Select(p => p.Node)
                    .Distinct()
                    .ToList();
            int count = q1.Count;
            if (q1.Count <= 1) return;

            object firstValue = null;
            for (int i = 0; i < count; i++)
            {
                TreeListNode node = q1[i];
                if (i == 0)
                {
                    firstValue = node.GetValue(fieldName);
                }
                else
                {
                    node.SetValue(fieldName, firstValue);
                }
            }
        }


        /// <summary>
        ///       Copy Value Top Row Down To Bottom Row On Column Pass Row != Backcor
        /// </summary>
        /// <param name="view" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="fieldName" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="backColor" type="System.Drawing.Color[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void CopyValueCellOnColumn(GridView view, string fieldName, params Color[] backColor)
        {
            object firstValue = null;
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                int rH = view.GetSelectedRows()[i];
                if (i == 0)
                {
                    firstValue = view.GetRowCellValue(rH, fieldName);
                }
                else
                {
                    if (backColor.Length > 0)
                    {
                        GridCellInfo gColInfo = GetGridCellInfo(view, rH, fieldName);
                        Color cellBackColor = gColInfo.Appearance.BackColor;
                        if (backColor.AsEnumerable().Any(p => p == cellBackColor))
                        {
                            view.SetRowCellValue(rH, fieldName, firstValue);
                        }
                    }
                    else
                    {
                        view.SetRowCellValue(rH, fieldName, firstValue);
                    }

                }
            }
        }

        /// <summary>
        ///       Copy Value Top Row Down To Bottom Row On Column Pass Row != Backcor
        /// </summary>
        /// <param name="view" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="colIndex" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="backColor" type="System.Drawing.Color[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void CopyValueCellOnColumn(GridView view, int colIndex, params Color[] backColor)
        {
            object firstValue = null;
            for (int i = 0; i < view.SelectedRowsCount; i++)
            {
                int rH = view.GetSelectedRows()[i];
                if (i == 0)
                {
                    firstValue = view.GetRowCellValue(rH, view.Columns[colIndex]);
                }
                else
                {
                    if (backColor.Length > 0)
                    {
                        GridCellInfo gColInfo = GetGridCellInfo(view, rH, view.Columns[colIndex]);
                        Color cellBackColor = gColInfo.Appearance.BackColor;
                        if (backColor.AsEnumerable().Any(p => p == cellBackColor))
                        {
                            view.SetRowCellValue(rH, view.Columns[colIndex], firstValue);
                        }
                    }
                    else
                    {
                        view.SetRowCellValue(rH, view.Columns[colIndex], firstValue);
                    }

                }
            }
        }




        /// <summary>
        ///     ReSort Row In GridView
        /// </summary>
        /// <param name="gv" type="DevExpress.XtraGrid.Views.Grid.GridView">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void SortRowInGridView(GridView gv, ColumnSortOrder sort)
        {
            gv.BeginSort();
            try
            {
                gv.ClearSorting();
                for (int i = 0; i < gv.Columns.Count; i++)
                {
                    gv.Columns[i].SortOrder = sort;
                    // gv.Columns[i].SortOrder = ColumnSortOrder.Descending;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                gv.EndSort();
            }

        }

        public static string GetStringPkDataTransferForm(List<DataRow> lDr, string fieldName, string seperator, bool isDistinct)
        {
            var q1 = (lDr.AsEnumerable().Where(p => GetSafeString(p[fieldName.Trim()]) != string.Empty).Select(p => new
            {
                FieldName = GetSafeString(p[fieldName.Trim()])
            })).ToList();
            if (!q1.Any()) return string.Empty;

            if (isDistinct)
                return q1.Distinct().Select(p => p.FieldName).Aggregate((a, b) => a + seperator + b);
            return q1.Select(p => p.FieldName).Aggregate((a, b) => a + seperator + b);
        }

        /// <summary>
        ///      Convert Linq query stament result to Datatable
        /// </summary>
        /// <param name="varlist" type="System.Collections.Generic.IEnumerable<T>">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        public static DataTable ConvertToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = rec.GetType().GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        /// <summary>
        ///     Compare Number (Start Value Is less than or equal to End Value) true else false
        /// </summary>
        /// <param name="startValue" type="double">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="endValue" type="double">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool CompareNumber(double startValue, double endValue)
        {
            if (endValue - startValue < 0)
                return false;
            return true;

        }
        /// <summary>
        ///     Compare Month Year (Start Date Is less than or equal to End Date) true else false
        /// </summary>
        /// <param name="formDate" type="System.DateTime">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="toDate" type="System.DateTime">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool CompareMonthYear(DateTime formDate, DateTime toDate)
        {
            if (toDate.Year > formDate.Year)
            {
                return true;
            }
            if (toDate.Year < formDate.Year)
            {
                return false;
            }
            return toDate.Month >= formDate.Month;
        }
        /// <summary>
        ///     Compare Date (Start Date Is less than or equal to End Date) true else false
        /// </summary>
        /// <param name="startDate" type="System.DateTime">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="endDate" type="System.DateTime">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool CompareDate(DateTime startDate, DateTime endDate)
        {

            TimeSpan ts = endDate.Date - startDate.Date;
            return ts.Days >= 0;
        }

        /// <summary>
        ///     Get Start Date Of Month
        /// </summary>
        /// <param name="dt" type="System.DateTime">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.DateTime value...
        /// </returns>
        public static DateTime GetStartDateOfMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        /// <summary>
        ///     Get End Date Of Month
        /// </summary>
        /// <param name="dt" type="System.DateTime">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.DateTime value...
        /// </returns>
        public static DateTime GetEndDateOfMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, MaxDayOfMonth(dt));
        }

        /// <summary>
        ///     Get Max Day Of Month 
        /// </summary>
        /// <param name="dt" type="System.DateTime">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int MaxDayOfMonth(DateTime dt)
        {
            if (dt.Month == 1 || dt.Month == 3 || dt.Month == 5 || dt.Month == 7 || dt.Month == 8 || dt.Month == 10 || dt.Month == 12)
            {
                return 31;
            }
            else
            {
                if (dt.Month == 4 || dt.Month == 6 || dt.Month == 9 || dt.Month == 11)
                {
                    return 30;
                }
                else
                {
                    if (dt.Year % 4 == 0)
                    {
                        return 29;
                    }
                    else
                    {
                        return 28;
                    }

                }
            }

        }

        /// <summary>
        ///     Get Right String Of String Parent
        /// </summary>
        /// <param name="value" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="length" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string RightString(string value, int length)
        {
            if (length <= 0)
                return String.Empty;
            return value.Substring(value.Length - length);
        }

        /// <summary>
        ///     Get Safe Double If Values is null or values not is number return 0
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>     A double value...
        /// </returns>
        public static double GetSafeDouble(object values)
        {
            if (values == null)
                return 0;
            else
            {
                try
                {
                    return Convert.ToDouble(values);
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        ///     Get Safe Float If Values is null or values not is number return 0
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A double value...
        /// </returns>
        public static float GetSafeFloat(object values)
        {
            if (values == null)
                return 0;
            else
            {
                try
                {
                    return Convert.ToSingle(values);
                }
                catch
                {
                    return 0;
                }
            }
        }



        public static int GetCountDecimal(object values)
        {
            string str = GetSafeDouble(values).ToString();
            int index = str.IndexOf(".", StringComparison.Ordinal);
            if (index < 0) return 0;
            return str.Length - (index + 1);
        }
        public static decimal GetMinDecimalNumberByRound(int decimalRow)
        {
            if (decimalRow <= 0) return 1;
            string str = "";
            for (int i = 1; i <= decimalRow; i++)
            {
                if (i == decimalRow)
                {
                    str += "1";
                }
                else
                {
                    str += "0";
                }
            }

            str = "0." + str;
            return GetSafeDecimal(str);
        }

        /// <summary>
        ///     Get Safe decimal If Values is null or values not is number return 0
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A double value...
        /// </returns>
        public static decimal GetSafeDecimal(object values)
        {
            if (values == null)
                return 0;
            else
            {
                try
                {
                    return Convert.ToDecimal(values);
                }
                catch
                {
                    return 0;
                }
            }
        }
        public static object GetSafeDecimalDBNull(object values)
        {
            if (values == null)
                return DBNull.Value;
            else
            {
                try
                {
                    return Convert.ToDecimal(values);
                }
                catch
                {
                    return DBNull.Value;
                }
            }
        }
        /// <summary>
        ///     Get Safe Double If Values is null or values not is number return 1
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A double value...
        /// </returns>
        public static double GetSafeDouble_1(object values)
        {
            if (values == null)
                return 0;
            else
            {
                try
                {
                    return Convert.ToDouble(values);
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        ///     Get Double Value if value null return DBNull Value
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A object value...
        /// </returns>
        public static object GetSafeDoubleDBNull(object values)
        {
            if (values == null)
                return DBNull.Value;
            else
            {
                try
                {
                    return Convert.ToDouble(values);
                }
                catch
                {
                    return DBNull.Value;
                }
            }
        }

        /// <summary>
        ///     Get Int Value if value null return DBNull Value
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A object value...
        /// </returns>
        public static object GetSafeIntDBNull(object values)
        {
            if (values == null)
                return DBNull.Value;
            else
            {
                try
                {
                    return Convert.ToInt32(values);
                }
                catch
                {
                    return DBNull.Value;
                }
            }
        }
        /// <summary>
        ///     Convert String To DateTime With Format String
        /// </summary>
        /// <param name="strDate" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strFormat" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.DateTime value...
        /// </returns>
        public static DateTime ConvertDateTimeWithFormat(string strDate, string strFormat)
        {

            if (string.IsNullOrEmpty(strDate) || string.IsNullOrEmpty(strFormat))
                return new DateTime(1900, 1, 1);
            try
            {
                return DateTime.ParseExact(strDate, strFormat, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new DateTime(1900, 1, 1);
            }
        }
        /// <summary>
        ///     Check DateTime DataType With Format String
        /// </summary>
        /// <param name="strDate" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="strFormat" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool CheckDateTimeTypeFormatString(string strDate, string strFormat)
        {
            DateTime dateTime;
            return DateTime.TryParseExact(strDate, strFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);

        }
        /// <summary>
        ///     Get Safe Int If Values is null or values not is number return 0
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetSafeInt(object values)
        {
            if (values == null)
                return 0;
            else
            {
                try
                {
                    return Convert.ToInt32(values);
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        ///   GetSafeBool if value = null return false  
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A bool value...
        /// </returns>
        public static bool GetSafeBool(object values)
        {
            if (values == null)
                return false;
            else
            {
                try
                {
                    return Convert.ToBoolean(values);
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        ///     GetSafeBoolInt if value = null return 0 else 1  
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetSafeBoolInt(object values)
        {
            if (values == null)
                return 0;
            try
            {
                if (Convert.ToBoolean(values))
                    return 1;
                return 0;
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        ///   GetSafeBoolString if value = null return 0 else 1    
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string GetSafeBoolString(object values)
        {
            if (values == null)
                return "0";
            try
            {
                if (Convert.ToBoolean(values))
                    return "1";
                return "0";
            }
            catch
            {
                return "0";
            }
        }

        /// <summary>
        ///    GetSafeIntSubtractOne If Values is null or values not is number return -1
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetSafeIntSubtractOne(object values)
        {
            if (values == null)
                return -1;
            else
            {
                try
                {
                    return Convert.ToInt32(values);
                }
                catch
                {
                    return -1;
                }
            }
        }
        /// <summary>
        ///     Get Safe Int64 If Values is null or values not is number return 0
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A long value...
        /// </returns>
        public static Int64 GetSafeInt64(object values)
        {
            if (values == null)
                return 0;
            else
            {
                try
                {
                    return Convert.ToInt64(values);
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        ///     GetSafeInt64SubtractOne If Values is null or values not is number return -1
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A long value...
        /// </returns>
        public static Int64 GetSafeInt64SubtractOne(object values)
        {
            if (values == null)
                return -1;
            else
            {
                try
                {
                    return Convert.ToInt64(values);
                }
                catch
                {
                    return -1;
                }
            }
        }
        /// <summary>
        ///     Get Safe String If String values is null return empty
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string GetSafeString(object values)
        {
            if (values == null)
                return string.Empty;
            return values.ToString().Trim();
        }
        /// <summary>
        ///      Get Safe DateTime If values is null return null else return datetime value
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A object value...
        /// </returns>
        public static object GetSafeDatetime(object values)
        {
            if (values == null)
                return null;
            else
            {
                try
                {
                    return Convert.ToDateTime(values);
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        ///     Get Safe DateTime if Value null return DateTime.Now
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.DateTime value...
        /// </returns>
        public static DateTime GetSafeDatetimeNullable(object values)
        {
            if (values == null)
                return DateTime.Now;
            else
            {
                try
                {
                    return Convert.ToDateTime(values);
                }
                catch
                {
                    return DateTime.Now;
                }
            }
        }
        /// <summary>
        ///     Get Safe DateTime if Value null return Min Date
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.DateTime value...
        /// </returns>
        public static DateTime GetSafeDatetimeNullableReturnMinDate(object values)
        {
            if (values == null)
                return new DateTime(1900, 1, 1);
            else
            {
                try
                {
                    return Convert.ToDateTime(values);
                }
                catch
                {
                    return new DateTime(1900, 1, 1);
                }
            }
        }



        /// <summary>
        ///     Get Safe DataTime DBNull if values null return dbNUll
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A object value...
        /// </returns>
        public static object GetSafeDatetimeDBNull(object values)
        {
            if (values == null)
                return DBNull.Value;
            else
            {
                try
                {
                    return Convert.ToDateTime(values);
                }
                catch
                {
                    return DBNull.Value;
                }
            }
        }
        /// <summary>
        ///     Get Safe DataTime Null if values null return null
        /// </summary>
        /// <param name="values" type="object">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A object value...
        /// </returns>
        public static object GetSafeDatetimeNull(object values)
        {
            if (values == null)
                return null;
            try
            {
                return Convert.ToDateTime(values);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime? GetSafeDatetimeOjectNull(object values)
        {
            if (values == null)
                return null;
            try
            {
                return Convert.ToDateTime(values);
            }
            catch
            {
                return null;
            }
        }

        public static string FormatNumberPatternFooter(int decimals, bool isN)
        {

            if (decimals <= 0)
                return "{0:N0}";
            if (isN)
            {
                return "{0:" + string.Format("N{0}", decimals) + "}";
            }



            string str = string.Empty;
            for (int i = 0; i < decimals; i++)
            {
                str += "#";
            }
            return "{0:" + string.Format("#,0.{0}", str.Trim()) + "}";
        }
        public static string FormatNumberPattern(int decimals, bool isN)
        {

            if (decimals <= 0)
                return "N0";
            if (isN)
            {
                return string.Format("N{0}", decimals);
            }

            string str = string.Empty;
            for (int i = 0; i < decimals; i++)
            {
                str += "#";
            }
            return string.Format("#,0.{0}", str.Trim());
        }

        public static string SetStringLeftSpace(this string s, int count)
        {
            string str = string.Empty;
            for (int i = 0; i < count; i++)
            {
                str += " ";
            }
            return string.Format("{0}{1}", str, s);
        }



        /// <summary>
        ///     Add Space String Into Parent String
        /// </summary>
        /// <param name="count" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string SetStringSpace(int count)
        {
            string str = string.Empty;
            for (int i = 0; i < count; i++)
            {
                str += " ";
            }
            return str;
        }
        public static string SetStringDuplicate(int count, string strInput)
        {
            string str = string.Empty;
            for (int i = 0; i < count; i++)
            {
                str += strInput;
            }
            return str;
        }


        public static Int64 GetMaxNumber(this int length)
        {
            if (length < 0) return 0;
            string str = string.Empty;
            for (int i = 0; i < length; i++)
            {
                str += "9";
            }
            return GetSafeInt64(str.Trim());
        }






        /// <summary>
        ///     Export DataTable To Text File
        /// </summary>
        /// <param name="datatable" type="System.Data.DataTable">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="delimited" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="exportcolumnsheader" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="file" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void ExportDataTabletoFile(DataTable datatable, string delimited, bool exportcolumnsheader, string file)
        {

            StreamWriter str = new StreamWriter(file, false, Encoding.Unicode);
            if (exportcolumnsheader)
            {

                string Columns = string.Empty;
                foreach (DataColumn column in datatable.Columns)
                {

                    Columns += column.ColumnName + delimited;

                }
                str.WriteLine(Columns.Remove(Columns.Length - 1, 1));
            }

            foreach (DataRow datarow in datatable.Rows)
            {

                string row = string.Empty;

                foreach (object items in datarow.ItemArray)
                {
                    row += items + delimited;

                }
                str.WriteLine(row.Remove(row.Length - 1, 1));

            }

            str.Flush();
            str.Close();

        }
        /// <summary>
        ///     Set Date Edit Format By Format String
        /// </summary>
        /// <param name="datePicker" type="DevExpress.XtraEditors.DateEdit">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="format" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="showClear" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="showWeek" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="defaultTime" type="DevExpress.Utils.DefaultBoolean">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="clearDropdown" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void SetDateEditFormat(DateEdit datePicker, string format, bool showClear, bool showWeek, DefaultBoolean defaultTime, bool clearDropdown = false)
        {
            datePicker.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            datePicker.Properties.Mask.EditMask = format;
            datePicker.Properties.Mask.UseMaskAsDisplayFormat = true;
            datePicker.Properties.ShowClear = showClear;
            datePicker.Properties.ShowWeekNumbers = showWeek;
            datePicker.Properties.VistaDisplayMode = defaultTime;
            datePicker.Properties.VistaEditTime = defaultTime;
            if (clearDropdown)
            {
                datePicker.Properties.Buttons.Clear();
            }
        }

        /// <summary>
        ///     Find Control In ConTrol Parent
        /// </summary>
        /// <param name="root" type="System.Windows.Forms.Control">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="target" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A System.Windows.Forms.Control value...
        /// </returns>
        public static Control FindControl(Control root, string target)
        {
            if (root.Name.Equals(target))
                return root;
            for (var i = 0; i < root.Controls.Count; ++i)
            {
                if (root.Controls[i].Name.Equals(target))
                    return root.Controls[i];
            }
            for (var i = 0; i < root.Controls.Count; ++i)
            {
                Control result;
                for (var k = 0; k < root.Controls[i].Controls.Count; ++k)
                {
                    result = FindControl(root.Controls[i].Controls[k], target);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        /// <summary>
        ///     Remove Control In ConTrol Parent
        /// </summary>
        /// <param name="root" type="System.Windows.Forms.Control">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public static void RemoveControl(Control root)
        {
            for (int i = root.Controls.Count - 1; i >= 0; i--)
            {
                root.Controls.RemoveAt(i);
            }
        }
        /// <summary>
        /// Get right string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(string value, int length)
        {
            return value.Substring(value.Length - length);
        }


        /// <summary>
        ///     
        /// </summary>
        /// <param name="values" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="pos" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="length" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string SubStringNew(this string values, int pos, int length)
        {
            int strLength = values.Length - pos;
            if (strLength < 0)
                return string.Empty;
            if (length > strLength)
                return values.Substring(pos, strLength).Trim();
            return values.Substring(pos, length).Trim();
        }

        public static DataTable CopyToDataTableNew<T>(this IEnumerable<T> source)
        {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        public static DataTable CopyToDataTableNew<T>(this IEnumerable<T> source,
                                                    DataTable table, LoadOption? options)
        {
            return new ObjectShredder<T>().Shred(source, table, options);
        }

    }



    public class MarginWord
    {
        private float _left = 0;
        private float _top = 0;
        private float _bottom = 0;
        private float _right = 0;

        public float Left
        {
            get { return this._left; }
            set { this._left = value; }
        }
        public float Top
        {
            get { return this._top; }
            set { this._top = value; }
        }
        public float Bottom
        {
            get { return this._bottom; }
            set { this._bottom = value; }
        }
        public float Right
        {
            get { return this._right; }
            set { this._right = value; }
        }

        public MarginWord()
        {

        }
        public MarginWord(float left, float top, float bottom, float right)
        {
            this._left = left;
            this._top = top;
            this._bottom = bottom;
            this._right = right;
        }
    }

    public class ObjectShredder<T>
    {
        private readonly FieldInfo[] _fi;
        private readonly PropertyInfo[] _pi;
        private readonly Dictionary<string, int> _ordinalMap;

        // ObjectShredder constructor.
        public ObjectShredder()
        {
            var type = typeof(T);
            _fi = type.GetFields();
            _pi = type.GetProperties();
            _ordinalMap = new Dictionary<string, int>();
        }

        /// <summary>
        /// Loads a DataTable from a sequence of objects.
        /// </summary>
        /// <param name="source">The sequence of objects to load into the DataTable.</param>
        /// <param name="table">The input table. The schema of the table must match that 
        /// the type T.  If the table is null, a new table is created with a schema 
        /// created from the public properties and fields of the type T.</param>
        /// <param name="options">Specifies how values from the source sequence will be applied to 
        /// existing rows in the table.</param>
        /// <returns>A DataTable created from the source sequence.</returns>
        public DataTable Shred(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Load the table from the scalar sequence if T is a primitive type.
            if (typeof(T).IsPrimitive)
            {
                return ShredPrimitive(source, table, options);
            }

            // Create a new table if the input table is null.
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            // Initialize the ordinal map and extend the table schema based on type T.
            table = ExtendTable(table, typeof(T));

            // Enumerate the source sequence and load the object values into rows.
            table.BeginLoadData();
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (options != null)
                    {
                        table.LoadDataRow(ShredObject(table, e.Current), (LoadOption)options);
                    }
                    else
                    {
                        table.LoadDataRow(ShredObject(table, e.Current), true);
                    }
                }
            }
            table.EndLoadData();

            // Return the table.
            return table;
        }

        public DataTable ShredPrimitive(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Create a new table if the input table is null.
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            if (!table.Columns.Contains("Value"))
            {
                table.Columns.Add("Value", typeof(T));
            }

            // Enumerate the source sequence and load the scalar values into rows.
            table.BeginLoadData();
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                Object[] values = new object[table.Columns.Count];
                while (e.MoveNext())
                {
                    values[table.Columns["Value"].Ordinal] = e.Current;

                    if (options != null)
                    {
                        table.LoadDataRow(values, (LoadOption)options);
                    }
                    else
                    {
                        table.LoadDataRow(values, true);
                    }
                }
            }
            table.EndLoadData();

            // Return the table.
            return table;
        }

        public object[] ShredObject(DataTable table, T instance)
        {

            FieldInfo[] fi = _fi;
            PropertyInfo[] pi = _pi;

            if (instance.GetType() != typeof(T))
            {
                // If the instance is derived from T, extend the table schema
                // and get the properties and fields.
                ExtendTable(table, instance.GetType());
                fi = instance.GetType().GetFields();
                pi = instance.GetType().GetProperties();
            }

            // Add the property and field values of the instance to an array.
            Object[] values = new object[table.Columns.Count];
            foreach (FieldInfo f in fi)
            {
                values[_ordinalMap[f.Name]] = f.GetValue(instance);
            }

            foreach (PropertyInfo p in pi)
            {
                values[_ordinalMap[p.Name]] = p.GetValue(instance, null);
            }

            // Return the property and field values of the instance.
            return values;
        }

        public DataTable ExtendTable(DataTable table, Type type)
        {
            // Extend the table schema if the input table was null or if the value 
            // in the sequence is derived from type T.            
            foreach (FieldInfo f in type.GetFields())
            {
                if (!_ordinalMap.ContainsKey(f.Name))
                {
                    // Add the field as a column in the table if it doesn't exist
                    // already.
                    DataColumn dc = table.Columns.Contains(f.Name) ? table.Columns[f.Name]
                        : table.Columns.Add(f.Name, f.FieldType);

                    // Add the field to the ordinal map.
                    _ordinalMap.Add(f.Name, dc.Ordinal);
                }
            }
            foreach (PropertyInfo p in type.GetProperties())
            {
                if (!_ordinalMap.ContainsKey(p.Name))
                {
                    // Add the property as a column in the table if it doesn't exist
                    // already.
                    DataColumn dc = table.Columns.Contains(p.Name) ? table.Columns[p.Name]
                        : table.Columns.Add(p.Name, p.PropertyType);

                    // Add the property to the ordinal map.
                    _ordinalMap.Add(p.Name, dc.Ordinal);
                }
            }

            // Return the table.
            return table;
        }
    }

    public class AlphaBetaValueSort
    {
        public string TextValue
        {
            get
            {
                return this._textValue;
            }
            set
            {
                this._textValue = value;

            }
        }
        public double IntValue
        {
            get
            {
                return this._intValue;
            }
            set
            {
                this._intValue = value;

            }
        }
        private string _textValue = string.Empty;
        private double _intValue = 0;
        public AlphaBetaValueSort()
        {

        }
        public AlphaBetaValueSort(string textValue, double intValue)
        {
            this._textValue = textValue;
            this._intValue = intValue;
        }
        
    }
}
    
