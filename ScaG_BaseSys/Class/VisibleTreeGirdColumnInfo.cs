using System;

namespace CNY_BaseSys.Class
{
    public class VisibleTreeGirdColumnInfo
    {
        private string _fieldName = "";
        public string FieldName
        {
            get { return this._fieldName; }
            set { this._fieldName = value; }
        }
        private Int32 _visibleIndex = 0;
        public Int32 VisibleIndex
        {
            get { return this._visibleIndex; }
            set { this._visibleIndex = value; }
        }
        public VisibleTreeGirdColumnInfo()
        {
        }
        public VisibleTreeGirdColumnInfo(string fieldName, Int32 visibleIndex)
        {
            this._fieldName = fieldName;
            this._visibleIndex = visibleIndex;
        }
    }
}
