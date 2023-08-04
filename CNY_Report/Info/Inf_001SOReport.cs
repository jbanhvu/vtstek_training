using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using System.Data;
using System.Data.SqlClient;
using CNY_BaseSys.Class;


namespace CNY_Report.Info
{
    internal class Inf_001SOReport
    {

        public List<string> GetSortOrderAttribute(Dictionary<Int64, AttributeHeaderInfo> dicAttribute)
        {

            List<string> l = new List<string>();
            if (dicAttribute.Count <= 0) return l;
            var q1 = dicAttribute.Select(p => new
            {
                AttibutePK = p.Key,
                p.Value.AttibuteName
            }).CopyToDataTableNew();


            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = q1 };
            DataTable dt = _ac.TblReadDataSP("sp_SO_LoadListAttSort", arrpara);


            foreach (DataRow dr in dt.Rows)
            {
                l.Add(ProcessGeneral.GetSafeString(dr["AttibuteName"]));
            }

            return l;
        }
        public DataSet DisplaySoDetailNWhenEdit(Int64 pkHeader)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@SOHeaderPK", SqlDbType.BigInt) { Value = pkHeader };
            return _ac.DtsReadDataSP("sp_SO_DisplayDetailWhenEdit", arrpara);
        }


        private readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable LoadGrid_So_Line(string str)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@cus", SqlDbType.NVarChar) { Value = str };
            return _ac.TblReadDataSP("usp_SO_0006", arrPara);
        }
        public DataTable Get_Attribute_Insert(int pk)
        {
            var arr = new SqlParameter[1];
            arr[0] = new SqlParameter("@pk", SqlDbType.Int) { Value = pk };
            return _ac.TblReadDataSP("sp_SOReport_Get_Attribute_Insert", arr);
        }
        public DataTable Get_Attribute_Saved(int pk, int pkSoLine)
        {
            var arr = new SqlParameter[2];
            arr[0] = new SqlParameter("@pk", SqlDbType.Int) { Value = pk };
            arr[1] = new SqlParameter("@pkSoLine", SqlDbType.Int) { Value = pkSoLine };
            return _ac.TblReadDataSP("sp_SOReport_LoadGetAttribute_Saved", arr);
        }
        public DataTable LoadGrid_So_Line_Edit(int pk)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.Int) { Value = pk };
            return _ac.TblReadDataSP("sp_SOReport_LoadSOLine", arrPara);
        }
        public DataTable LoadHeaderEdit(Int64 pk)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.Int) { Value = pk };
            return _ac.TblReadDataSP("sp_SOReport_LoadHeader_New", arrPara);
        }
        public DataTable LoadAgreeMent()
        {
            return _ac.TblReadDataSQL("select -1 as CNY021PK, a.PK AS PKParent,a.CNY002 as Name, b.PK AS ChildPK,b.CNY003 as [Values],CAST (0 as bit ) as IsChecked " +
                "from CNYMF010 a inner join CNYMF011 b on a.PK = b.CNY001 ", null);
        }

        public DataTable LoadAgreeMent_Save(Int64 pk)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@SOPK", SqlDbType.Int) { Value = pk };
            return _ac.TblReadDataSP("sp_SO_LoadGridAgreeReport", arrPara);
            //return _ac.TblReadDataSQL(string.Format("select c.CNY021PK, a.PK AS PKParent,a.CNY002 as Name, b.PK AS ChildPK,b.CNY003 as [Values],c.CNY002 as IsChecked " +
            //    " from CNYMF010 a inner join CNYMF011 b on a.PK = b.CNY001  inner join CNY00021 c on c.CNY001 = b.PK where CNY021FK = {0}", pk), null);
        }
        public DataTable LoadSOLine(int pk)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.Int) { Value = pk };
            return _ac.TblReadDataSP("sp_SOReport_LoadSOLine", arrPara);
        }
        public DataTable Load_Attribute(string ItemPK)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@ItemPK", SqlDbType.VarChar) { Value = ItemPK };
            return _ac.TblReadDataSP("sp_SOReport_LoadAttribute", arrPara);
        }
        public DataTable Get_Attribute(int pk, string keydown)
        {
            var arr = new SqlParameter[1];
            arr[0] = new SqlParameter("@pk", SqlDbType.Int) { Value = pk };
            return _ac.TblReadDataSP("usp_SO_0017", arr);
        }
    }
}