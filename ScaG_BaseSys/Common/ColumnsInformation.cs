using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNY_BaseSys.Common
{
    public class ColumnsInformation
    {
        private string _columnName;
        private ColumnsDataType _columnType;
        public string ColumnName {
            get { return this._columnName; }
            set { this._columnName = value; }
        }

        public ColumnsDataType ColumnType
        {
            get { return this._columnType; }
            set { this._columnType = value; }
        }

        public ColumnsInformation(string columnName, ColumnsDataType columnType)
        {
            this._columnName = columnName;
            this._columnType = columnType;
        }

        public ColumnsInformation()
        {

        }

    }

    public enum ColumnsDataType
    {
        DateTimeType,
        Int32Type,
        Int64Type,
        DoubleType,
        BooleanType,
        DecimalType,
        StringType,
    }
}
