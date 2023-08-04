using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_BaseSys.Common
{
    public class GridControlMasterDetailPaging
    {
        #region "Property"


        public int pageCount { get; set; }
        public int maxRec { get; set; }
        public int beginRec { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int recNo { get; set; }
        public GridControl gridcontrol { get; set; }
        public GridView gridview { get; set; }
        public LabelControl labelDisplay { get; set; }
        public LabelControl labelCurrentRecord { get; set; }
        public ComboBoxEdit combopageSize { get; set; }
        public SimpleButton btnLastPage { get; set; }
        public SimpleButton btnNextPage { get; set; }
        public SimpleButton btnPreviousPage { get; set; }
        public SimpleButton btnFirstPage { get; set; }
        public DataTable dtParent { get; set; }
        public List<MasterDetailDiagram> lDetail { get; set; }


        private readonly List<BestFitColumnPaging> _lColFit;

        private bool _conditionload = false;
        private readonly int _itemPerPage = 10;

        #endregion


        #region "Contructor"

        public GridControlMasterDetailPaging(GridControl gridControlData, GridView gvData,
            LabelControl labelDisplyPage, LabelControl labelCurrentRec,
            ComboBoxEdit droppageSize, SimpleButton btnFirst, SimpleButton btnPrevious, SimpleButton btnNext,
            SimpleButton btnLast, List<BestFitColumnPaging> lBfcol)
        {
            _itemPerPage = DeclareSystem.SysItemsPaging;
            gridcontrol = gridControlData;
            gridview = gvData;

            labelDisplay = labelDisplyPage;
            labelCurrentRecord = labelCurrentRec;
            combopageSize = droppageSize;
            btnFirstPage = btnFirst;
            btnLastPage = btnLast;
            btnPreviousPage = btnPrevious;
            btnNextPage = btnNext;
            _lColFit = lBfcol;
            gridview.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gridview.RowCountChanged += gridview_RowCountChanged;
            gridview.CustomDrawRowIndicator += gridview_CustomDrawRowIndicator;
            btnFirstPage.Click += btnFirstPage_Click;
            btnLast.Click += btnLast_Click;
            btnNextPage.Click += btnNextPage_Click;
            btnPreviousPage.Click += btnPreviousPage_Click;
            combopageSize.EditValueChanged += combopageSize_EditValueChanged;
        }

        #endregion


        #region "methold pagging"
        /// <summary>
        ///     Use Button Pagging (First, Last,Previous,Next)
        /// </summary>
        /// <param name="first" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="last" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="previous" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="next" type="bool">
        ///     <para>
        ///         
        ///     </para>
        /// </param> 
        public void UseButton(bool first, bool last, bool previous, bool next)
        {
            btnFirstPage.Enabled = first;
            btnLastPage.Enabled = last;
            btnPreviousPage.Enabled = previous;
            btnNextPage.Enabled = next;
        }

        /// <summary>
        ///     load girdview ,display current page, display page info
        /// </summary>
        public void LoadPage()
        {
            int startRec = recNo;
            if (recNo == 0 && maxRec == 0)
            {
                beginRec = 0;
            }
            else
            {
                beginRec = recNo + 1;
            }
            int endRec = 0;
            DataTable dtTempParent = dtParent.Clone();
            dtTempParent.TableName = ConstSystem.RootTableName;
            if (currentPage == pageCount)
            {
                endRec = maxRec;
            }
            else
            {
                endRec = pageSize * currentPage;
            }

            for (int i = startRec; i < endRec; i++)
            {
                if (i < dtParent.Rows.Count)
                {
                    dtTempParent.ImportRow(dtParent.Rows[i]);
                    recNo = recNo + 1;
                }
            }

            var ds = new DataSet("MasterDetailDS");
            ds.Tables.Add(dtTempParent);

            var q1 = lDetail.OrderBy(p => p.Level).ThenBy(p => p.Index);
            foreach (var itemQ1 in q1)
            {
                string tblParentName = string.IsNullOrEmpty(itemQ1.ParentTable.Trim()) ? ConstSystem.RootTableName : itemQ1.ParentTable.Trim();
                DataTable dtParentQ1 = ds.Tables[tblParentName];
                DataTable dtChildQ1 = itemQ1.Dt;
                DataTable dtTempChild = dtChildQ1.Clone();
                string relationName = itemQ1.RelationColName.Trim();
                var q2 = dtParentQ1.AsEnumerable().Join(dtChildQ1.AsEnumerable(), tParent => ProcessGeneral.GetSafeString(tParent[relationName]),
                   tChild => ProcessGeneral.GetSafeString(tChild[relationName]), (tParent, tChild) => tChild).ToList();
                if (q2.Any())
                {
                    dtTempChild = q2.CopyToDataTable();
                }
                string tblChildName = itemQ1.TableName.Trim();
                dtTempChild.TableName = tblChildName;
                ds.Tables.Add(dtTempChild);

                DataColumn parentColumn = ds.Tables[tblParentName].Columns[relationName];
                DataColumn childColumn = ds.Tables[tblChildName.Trim()].Columns[relationName];
                ds.Relations.Add(new DataRelation(relationName, parentColumn, childColumn));
            }

            gridcontrol.DataSource = ds;
            gridcontrol.DataMember = ConstSystem.RootTableName;
            DisplayPageInfo();
            LoadCurrentRecord();

            BestFitColumns();

        }

        /// <summary>
        ///  display page info   
        /// </summary>
        public void DisplayPageInfo()
        {
            labelDisplay.Text = String.Format("Page {0}/{1} - Record ({2}-{3})/{4}", currentPage, pageCount, beginRec, recNo, maxRec);
        }

        /// <summary>
        ///     display current record selected
        /// </summary>
        public void LoadCurrentRecord()
        {
            if (gridview.IsDataRow(gridview.FocusedRowHandle))
            {
                if (maxRec == 0)
                    labelCurrentRecord.Text = string.Empty;
                else
                    labelCurrentRecord.Text = string.Format("Current Record Selected: {0}", pageSize * (currentPage - 1) + (gridview.FocusedRowHandle + 1));
            }
            else
            {
                if (maxRec == 0)
                    labelCurrentRecord.Text = string.Empty;
                else
                    labelCurrentRecord.Text = string.Format("Current Record Selected: {0}", pageSize * (currentPage - 1) + 1);
            }
        }

        /// <summary>
        ///     Load Last Page Gridview
        /// </summary>
        public void LastPage()
        {
            UseButton(true, false, true, false);
            currentPage = pageCount;
            recNo = pageSize * (currentPage - 1);
            LoadPage();
        }

        /// <summary>
        ///     Load First Page Gridview
        /// </summary>
        public void FirstPage()
        {
            UseButton(false, true, false, true);
            if (currentPage == 1)
            {
                return;
            }
            currentPage = 1;
            recNo = 0;
            LoadPage();
        }

        /// <summary>
        ///     Load Previous Page
        /// </summary>
        public void PreviousPage()
        {

            if (currentPage == 1)
            {
                UseButton(false, true, false, true);
                return;
            }
            currentPage = currentPage - 1;
            if (currentPage < 1)
            {
                currentPage = 1;

            }
            else
            {
                recNo = pageSize * (currentPage - 1);

            }

            if (currentPage == 1)
            {
                UseButton(false, true, false, true);
            }
            else
            {
                UseButton(true, true, true, true);
            }

            LoadPage();
        }

        /// <summary>
        ///     Load Next Page GridView
        /// </summary>
        public void NextPage()
        {
            if (currentPage == pageCount)
            {
                UseButton(true, false, true, false);
                return;
            }
            currentPage = currentPage + 1;

            if (currentPage > pageCount)
            {
                currentPage = pageCount;
            }
            if (currentPage == pageCount)
            {
                UseButton(true, false, true, false);
            }
            else
            {
                UseButton(true, true, true, true);
            }
            LoadPage();
        }


        public void Innitial(DataTable tableParent, List<MasterDetailDiagram> l)
        {

            dtParent = tableParent;
            lDetail = l;
            LoadComboPageSize(dtParent.Rows.Count);
            LoadDataPaging();

           
        }

        private void LoadDataPaging()
        {
            maxRec = dtParent.Rows.Count;
            try
            {
                pageSize = int.Parse(combopageSize.Text);
            }
            catch
            {
                pageSize = dtParent.Rows.Count;
            }
            if (maxRec == 0)
            {
                pageCount = 1;
            }
            else
            {
                if (maxRec % pageSize == 0)
                {
                    pageCount = (int)(maxRec / pageSize);
                }
                else
                {
                    pageCount = (int)(maxRec / pageSize) + 1;
                }
            }
            currentPage = 1;
            recNo = 0;
            beginRec = 0;
            if (pageCount == 1)
            {
                UseButton(false, false, false, false);
            }
            else
            {
                UseButton(false, true, false, true);
            }
            LoadPage();
        }

        private void BestFitColumns()
        {
            gridview.BestFitColumns();
            foreach (var item in _lColFit)
            {
                gridview.Columns[item.FiledName.Trim()].Width += item.IncreaseWidth;
            }

        }

        /// <summary>
        ///     Đổ dữ liệu vào comboboxpagesize theo số dòng của DataTable
        /// </summary>
        /// <param name="row" type="int">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        private void LoadComboPageSize(int row)
        {
            _conditionload = true;
            combopageSize.Properties.Items.Clear();
            if (row > 0)
            {
                int i = _itemPerPage;
                while (i <= row)
                {
                    combopageSize.Properties.Items.Add(i);
                    i = i + _itemPerPage;
                }
                if (row % _itemPerPage != 0)
                {
                    combopageSize.Properties.Items.Add("Show All");
                }
            }
            else
            {
                combopageSize.Properties.Items.Add(0);
            }
            if (combopageSize.Properties.Items.Count > 0)
            {
                combopageSize.SelectedIndex = 0;
            }
            _conditionload = false;
        }

  
    #endregion


        #region "Control Event"


        private void gridview_RowCountChanged(object sender, EventArgs e)
        {
            GridView gv = sender as GridView;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString((gv.RowCount + 1 + pageSize * (currentPage - 1)).ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gridview_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (e.Info.IsRowIndicator)
            {
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                e.Info.DisplayText = (e.RowHandle + 1 + pageSize * (currentPage - 1)).ToString();
                e.Info.ImageIndex = -1;
            }
        }

        private void combopageSize_EditValueChanged(object sender, EventArgs e)
        {
            if (!_conditionload)
            {
                LoadDataPaging();
            }
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            PreviousPage();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            NextPage();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            LastPage();
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            FirstPage();
        }

        #endregion
    }
}
