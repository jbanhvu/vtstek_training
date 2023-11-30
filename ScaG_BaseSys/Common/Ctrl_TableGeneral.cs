using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CNY_BaseSys.Common
{
    public static class Ctrl_TableGeneral
    {
        public static DataTable TableAttributeTempGenerate()
        {
            var dt = new DataTable();
            dt.Columns.Add("AttibutePK", typeof(Int64));
            dt.Columns.Add("AttibuteCode", typeof(String));
            dt.Columns.Add("AttibuteName", typeof(String));
            dt.Columns.Add("AttibuteValue", typeof(String));
            dt.Columns.Add("PK", typeof(Int64));
            dt.Columns.Add("CNY00010PK", typeof(Int64));

            dt.Columns.Add("AttibuteValueTemp", typeof(String));
            dt.Columns.Add("AttibuteUnit", typeof(String));
            dt.Columns.Add("IsNumber", typeof(bool));
            return dt;
        }
        public static DataTable TableAttributeTempBoM()
        {
            var dt = new DataTable();
            dt.Columns.Add("AttibutePK", typeof(Int64));
            dt.Columns.Add("AttibuteCode", typeof(String));
            dt.Columns.Add("AttibuteName", typeof(String));
            dt.Columns.Add("AttibuteValue", typeof(String));
            dt.Columns.Add("PK", typeof(Int64));
            dt.Columns.Add("CNY00010PK", typeof(Int64));


            dt.Columns.Add("ColumnIndex", typeof(Int32));


            dt.Columns.Add("CNY00016PK", typeof(Int64));
            dt.Columns.Add("PKCode", typeof(Int64));
            dt.Columns.Add("RowState", typeof(String));

            dt.Columns.Add("AttibuteValueTemp", typeof(String));
            dt.Columns.Add("AttibuteUnit", typeof(String));

            dt.Columns.Add("IsNumber", typeof(bool));
            return dt;
        }
    
    }
}
