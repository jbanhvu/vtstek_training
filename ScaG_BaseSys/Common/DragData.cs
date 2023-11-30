using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace CNY_BaseSys.Common
{
    public class DragData
    {
        private DataTable _sourceData;
        public DataTable sourceData { get { return this._sourceData; } set { this._sourceData = value; } }
        private GridView _gvDrag;
        public GridView gvDrag { get { return this._gvDrag; } set { this._gvDrag = value; } }
        private GridControl _gcDrag;
        public GridControl gcDrag { get { return this._gcDrag; } set { this._gcDrag = value; } }
    }
}
