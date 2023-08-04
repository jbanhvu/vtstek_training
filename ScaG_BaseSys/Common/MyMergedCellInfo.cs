using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Columns;

namespace CNY_BaseSys.Common
{
    public class MyMergedCellInfo
    {
        List<GridColumn> columns_;
        string displayText_;
        int rowHandle_;

        public List<GridColumn> Columns
        {
            get { return columns_; }
        }

        public string DisplayText
        {
            get { return displayText_; }
        }

        public int RowHandle
        {
            get { return rowHandle_; }
        }

        public MyMergedCellInfo(string sDisplayText, int iRowHandle)
        {
            columns_ = new List<GridColumn>();
            displayText_ = sDisplayText;
            rowHandle_ = iRowHandle;
        }
    }
}
