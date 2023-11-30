using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using System.Runtime.Serialization;
using DevExpress.XtraGrid.Columns;
using System.Drawing;
using DevExpress.Utils;
using System.Drawing.Drawing2D;

namespace CNY_BaseSys.Common
{
   public class GridViewControlPaging
    {
        #region "Property"
        public int pageCount { get; set; }
        public int maxRec { get; set; }
        public int beginRec { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int recNo { get; set; }
        public DataTable dtSource { get; set; }
        public GridControl gridcontrol { get; set; }
        public GridView gridview { get; set; }
        public LabelControl labelDisplay { get; set; }
        public LabelControl labelCurrentRecord { get; set; }
        public ComboBoxEdit combopageSize { get; set; }
        public SimpleButton btnLastPage { get; set; }
        public SimpleButton btnNextPage { get; set; }
        public SimpleButton btnPreviousPage { get; set; }
        public SimpleButton btnFirstPage { get; set; }
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
        int endRec=0 ;
        DataTable dtTemp = dtSource.Clone();
      
     
        if (currentPage == pageCount) 
        {
            endRec = maxRec;
        }
        else
        {
            endRec = pageSize * currentPage;
        }
        
        for (int i = startRec ;i< endRec ;i++)
        {
            if (i < dtSource.Rows.Count )
            {
                dtTemp.ImportRow(dtSource.Rows[i]);
                recNo = recNo + 1;
            }
        }
        gridcontrol.DataSource = dtTemp;
      
        DisplayPageInfo();
        LoadCurrentRecord();
        
    }

    /// <summary>
    ///  display page info   
    /// </summary>
    public void DisplayPageInfo()
    {
        labelDisplay.Text = String.Format("Page {0}/{1} - Record ({2}-{3})/{4}", currentPage, pageCount, beginRec, recNo,maxRec);
    }

    /// <summary>
    ///     display current record selected
    /// </summary>
    public void LoadCurrentRecord()
    {
        if (gridview.IsDataRow(gridview.FocusedRowHandle))
        {
            labelCurrentRecord.Text = maxRec == 0 ? string.Empty : string.Format("Current Record Selected: {0}", pageSize * (currentPage - 1) + (gridview.FocusedRowHandle + 1));
        }
        else
        {
            labelCurrentRecord.Text = maxRec == 0 ? string.Empty : string.Format("Current Record Selected: {0}", pageSize * (currentPage - 1) + 1);
        }
    }

    /// <summary>
    ///     Load Last Page Gridview
    /// </summary>
    public void LastPage()
    {
        UseButton(true,false,true,false);
        currentPage = pageCount;
        recNo = pageSize * (currentPage - 1);
        LoadPage();
    }

    /// <summary>
    ///     Load First Page Gridview
    /// </summary>
    public void FirstPage()
    {
        UseButton(false,true, false, true);
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
        if (currentPage < 1 )
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
            UseButton(true,false,true,false);
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

    /// <summary>
    ///     init gridview data
    /// </summary>
    /// <param name="GridControlData" type="DevExpress.XtraGrid.GridControl">
    ///     <para>
    ///         
    ///     </para>
    /// </param>
    /// <param name="gvData" type="DevExpress.XtraGrid.Views.Grid.GridView">
    ///     <para>
    ///         
    ///     </para>
    /// </param>
    /// <param name="LabelDisplyPage" type="DevExpress.XtraEditors.LabelControl">
    ///     <para>
    ///         
    ///     </para>
    /// </param>
    /// <param name="labelCurrentRec" type="DevExpress.XtraEditors.LabelControl">
    ///     <para>
    ///         
    ///     </para>
    /// </param>
    /// <param name="DroppageSize" type="DevExpress.XtraEditors.ComboBoxEdit">
    ///     <para>
    ///         
    ///     </para>
    /// </param>
    /// <param name="dt" type="System.Data.DataTable">
    ///     <para>
    ///         
    ///     </para>
    /// </param>
    /// <param name="btnFirst" type="DevExpress.XtraEditors.SimpleButton">
    ///     <para>
    ///         
    ///     </para>
    /// </param>
    /// <param name="btnPrevious" type="DevExpress.XtraEditors.SimpleButton">
    ///     <para>
    ///         
    ///     </para>
    /// </param>
    /// <param name="btnNext" type="DevExpress.XtraEditors.SimpleButton">
    ///     <para>
    ///         
    ///     </para>
    /// </param>
    /// <param name="btnLast" type="DevExpress.XtraEditors.SimpleButton">
    ///     <para>
    ///         
    ///     </para>
    /// </param>
    public void Innitial(GridControl GridControlData,GridView gvData, LabelControl LabelDisplyPage, LabelControl labelCurrentRec, ComboBoxEdit DroppageSize, DataTable dt, SimpleButton btnFirst,
    SimpleButton btnPrevious,SimpleButton btnNext,SimpleButton btnLast)
    {
        gridcontrol = GridControlData;
        gridview = gvData;
        gridview.RowCountChanged += gridview_RowCountChanged;
        gridview.CustomDrawRowIndicator += gridview_CustomDrawRowIndicator;
        labelDisplay = LabelDisplyPage;
        labelCurrentRecord = labelCurrentRec;
        combopageSize = DroppageSize;
        btnFirstPage = btnFirst;
        btnLastPage = btnLast;
        btnPreviousPage = btnPrevious;
        btnNextPage = btnNext;        
        dtSource = dt;
        maxRec = dtSource.Rows.Count;
        try
        {
            pageSize = int.Parse(combopageSize.Text);
        }
        catch
        {
            pageSize = dtSource.Rows.Count;
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
            UseButton(false,true,false,true);
        }
        LoadPage();
        
    }


    private void gridview_RowCountChanged(object sender, EventArgs e)
    {
        var gv = sender as GridView;
      //  if (!gv.GridControl.IsHandleCreated) return;
        Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
        SizeF size = gr.MeasureString((gv.RowCount+1 + pageSize * (currentPage - 1)).ToString(), gv.PaintAppearance.Row.GetFont());
        gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
        //GridPainter.Indicator.ImageSize.Width 
    }

    private void gridview_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
    {
        var gv = sender as GridView;
        if (!e.Info.IsRowIndicator) return;
        if (!gv.IsDataRow(e.RowHandle)) return;
        e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
        e.Info.DisplayText = (e.RowHandle + 1 + pageSize * (currentPage - 1)).ToString();
        e.Info.ImageIndex = -1;
    }
    #endregion
    }
}
