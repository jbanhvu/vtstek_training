using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_AdminSys.Class;
using CNY_BaseSys;
using CNY_BaseSys.Common;

namespace CNY_AdminSys.Info
{
    public class Inf_Position
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable GridViewData_Load()
        {
            return _ac.TblReadDataSP("sp_CNY_Position_Load", null);
        }
      
        public int Insert(string code, string name, string description)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.VarChar) { Value = code };
            arrpara[1] = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name };
            arrpara[2] = new SqlParameter("@Description", SqlDbType.NVarChar) { Value = description };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Admin_Position_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }


        public int Update(string code, string name, string description)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.VarChar) { Value = code };
            arrpara[1] = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name };
            arrpara[2] = new SqlParameter("@Description", SqlDbType.NVarChar) { Value = description };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Admin_Position_Update", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int Delete(string code)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.VarChar) { Value = code };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Admin_Position_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
    }
}
