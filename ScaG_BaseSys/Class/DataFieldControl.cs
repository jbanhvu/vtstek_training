using System;

namespace CNY_BaseSys.Class
{
    public class DataFieldControl
    {
        public int dfc_id { get; set; }
        public string dfc_name { get; set; }
        public string dfc_displayname { get; set; }
        public string vietnamese { get; set; }
        public string data_type { get; set; }
        public string reference_column { get; set; }
        public int TextLengthOnApp { get; set; }
        public int TextLengthReferenceOnApp { get; set; }
        public Boolean ShowOnApp { get; set; }
        public Boolean ShowOnAppAE { get; set; }
        public Boolean Editable { get; set; }
        public Boolean EditableWhenCase1 { get; set; }
        public Boolean Updateable { get; set; }
        public string AutoFillOnInsert { get; set; }
        public int Position { get; set; }
        public int GroupID { get; set; }
        public int SplitContainerID { get; set; }
        public Boolean Nullable { get; set; }
        public object value { get; set; }
        public string Table_name { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}
