using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_BaseSys.Class;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_BaseSys.WForm
{
    public partial class FrmF4Grid : XtraForm
    {
       
        public DataTable DataSource { set; get; }
        public string Caption { set; get; }
        public string StrColE { set; get; }

        /// <summary>
        /// Return: (0: 1R1C ID, 1: mR1C ID, 2: 1RmC, 3: mRmC)
        /// </summary>
        public int iDL { get; set; }
        public event Cls001Para.FireEventTransferData FireEventData = null;

        public FrmF4Grid()
        {
            InitializeComponent();
            this.Load += FrmF4Grid_Load;

            btnSelect.Click += BtnSelect_Click;
            grvMain.DoubleClick += GrvMain_DoubleClick;
            grcMain.ProcessGridKey += GrcMain_ProcessGridKey;
        }

        private void CreateGrid(DataTable dt, GridView gridView1)
        {
            gridView1.Columns.Clear();
            string[] fields = new string[dt.Columns.Count];
            string[] captions = new string[dt.Columns.Count];
            for (int t = 0; t < dt.Columns.Count; t++)
            {
                try
                {
                    fields[t] = dt.Columns[t].ColumnName;
                    captions[t] = dt.Columns[t].Caption;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            for (int i = 0; i < fields.Length; i++)
            {
                GridColumn col = new GridColumn();
                col.FieldName = fields[i];
                col.Caption = captions[i];
                gridView1.Columns.Add(col);
                gridView1.Columns[i].Visible = true;
            }
        }
        private void SetCaptionColGridSelect(GridControl grd, GridView gridView1, string str_Caption, int CCol)
        {
            if (string.IsNullOrEmpty(str_Caption)) return;
            String[] str_col = str_Caption.Split(new Char[] { ',' });

            grd.ForceInitialize();
            gridView1.PopulateColumns();
            try
            {
                for (int i = 0; i <= CCol - 1; i++)
                {
                    gridView1.Columns[i].Caption = str_col[i];
                }
                gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                gridView1.OptionsView.AllowHtmlDrawHeaders = true;
                gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetFocusCol(GridView grid, int idxCol)
        {
            grid.ClearSelection();
            grid.FocusedColumn = grid.VisibleColumns[idxCol];
            grid.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;

            grid.ShowEditor();
        }
        private void FrmF4Grid_Load(object sender, EventArgs e)
        {
            grvMain.OptionsView.ShowAutoFilterRow = true;
            grvMain.OptionsBehavior.Editable = false;

            if (DataSource != null)
            {
                CreateGrid(DataSource, grvMain);
                grcMain.DataSource = DataSource;
                SetCaptionColGridSelect(grcMain, grvMain, StrColE, DataSource.Columns.Count);
                SetFocusCol(grvMain, 0);
            }
        }

        private string GetRowSelectedValuesinGrd(GridView view, int indexCol)
        {
            if (view.SelectedRowsCount == 0) return "";
            const string LineDelimiter = "|";
            string result = "";
            for (int i = view.SelectedRowsCount - 1; i >= 0; i--)
            {
                int row = view.GetSelectedRows()[i];
                for (int j = 0; j < view.VisibleColumns.Count; j++)
                {
                    if (j == indexCol)
                    {
                        result += view.GetRowCellDisplayText(row, view.VisibleColumns[j]);
                    }
                }
                if (i != 0)
                    result += LineDelimiter;
            }
            return result;
        }
        private string GetRowSelectedValuesAllColinGrd(GridView view)
        {
            if (view.SelectedRowsCount == 0) return "";
            const string LineDelimiter = "|";
            string result = "";
            for (int i = view.SelectedRowsCount - 1; i >= 0; i--)
            {
                int row = view.GetSelectedRows()[i];
                for (int j = 0; j < view.VisibleColumns.Count; j++)
                {
                    result += view.GetRowCellDisplayText(row, view.VisibleColumns[j]);
                    result += LineDelimiter;
                }
            }
            return result;
        }
        private string GetRowSelectedValuesNRowAllColinGrd(GridView view)
        {
            if (view.SelectedRowsCount == 0) return "";
            const string LineDelimiter = "|";
            const string LineDelimiter2 = "$";
            string result = "";
            for (int i = view.SelectedRowsCount - 1; i >= 0; i--)
            {
                int row = view.GetSelectedRows()[i];
                for (int j = 0; j < view.VisibleColumns.Count; j++)
                {
                    result += view.GetRowCellDisplayText(row, view.VisibleColumns[j]);
                    result += LineDelimiter;
                }
                if (i != 0)
                    result = result.Substring(0, result.Length - 1);
                result += LineDelimiter2;
            }
            return result;
        }

        private DataTable GetSelectedRow(GridControl gc, GridView gv)
        {
            DataTable dtSrc = (DataTable) gc.DataSource;
            DataTable dtSel = dtSrc.Clone();

            for (int i = gv.SelectedRowsCount - 1; i >= 0; i--)
            {
                int irow = gv.GetSelectedRows()[i];
                DataRow dri = gv.GetDataRow(irow);
                dtSel.ImportRow(dri);
            }

            return dtSel;
        }

        private void GrcMain_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Cursor.Current = Cursors.WaitCursor;
                    if (FireEventData != null)
                    {
                        if (iDL == 0 || iDL == 1)
                        {
                            FireEventData(this, new Cls001Para.TransferDataEventArgs { ID = GetRowSelectedValuesinGrd(grvMain, 0), DtSel = GetSelectedRow(grcMain, grvMain) });
                        }
                        else if (iDL == 2)
                        {
                            FireEventData(this, new Cls001Para.TransferDataEventArgs { ID = GetRowSelectedValuesAllColinGrd(grvMain), DtSel = GetSelectedRow(grcMain, grvMain) });
                        }
                        else if (iDL == 3)
                        {
                            FireEventData(this, new Cls001Para.TransferDataEventArgs { ID = GetRowSelectedValuesNRowAllColinGrd(grvMain), DtSel = GetSelectedRow(grcMain, grvMain) });
                        }
                    }
                    Cursor.Current = Cursors.Default;
                    this.Close();
                    break;
                case Keys.Home:
                    SetFocusCol(grvMain, 0);
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }
        private void GrvMain_DoubleClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (FireEventData != null)
            {
                if (iDL == 0 || iDL == 1)
                {
                    FireEventData(this, new Cls001Para.TransferDataEventArgs { ID = GetRowSelectedValuesinGrd(grvMain, 0), DtSel = GetSelectedRow(grcMain, grvMain) });
                }
                else if (iDL == 2)
                {
                    FireEventData(this, new Cls001Para.TransferDataEventArgs { ID = GetRowSelectedValuesAllColinGrd(grvMain), DtSel = GetSelectedRow(grcMain, grvMain) });
                }
                else if (iDL == 3)
                {
                    FireEventData(this, new Cls001Para.TransferDataEventArgs { ID = GetRowSelectedValuesNRowAllColinGrd(grvMain), DtSel = GetSelectedRow(grcMain, grvMain) });
                }
            }
            Cursor.Current = Cursors.Default;
            this.Close();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (FireEventData != null)
            {
                if (iDL == 0 || iDL == 1)
                {
                    FireEventData(this, new Cls001Para.TransferDataEventArgs { ID = GetRowSelectedValuesinGrd(grvMain, 0), DtSel = GetSelectedRow(grcMain, grvMain) });
                }
                else if (iDL == 2)
                {
                    FireEventData(this, new Cls001Para.TransferDataEventArgs { ID = GetRowSelectedValuesAllColinGrd(grvMain), DtSel = GetSelectedRow(grcMain, grvMain) });
                }
                else if (iDL == 3)
                {
                    FireEventData(this, new Cls001Para.TransferDataEventArgs { ID = GetRowSelectedValuesNRowAllColinGrd(grvMain), DtSel = GetSelectedRow(grcMain, grvMain) });
                }
            }
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
