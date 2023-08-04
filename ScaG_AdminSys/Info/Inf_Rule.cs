using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_BaseSys;
using CNY_BaseSys.Common;

namespace CNY_AdminSys.Info
{
    public class Inf_Rule
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        public DataTable GridViewData_Load()
        {
            return _ac.TblReadDataSP("sp_CNY_Rule_SelectAll", null);
        }
        public int Delete(string permisionGroupCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Rule_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
        public int Update(string permisionGroupCode, string permisionGroupName, string permisionGroupDescription, int priority)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            arrpara[1] = new SqlParameter("@PermisionGroupName", SqlDbType.NVarChar) { Value = permisionGroupName };
            arrpara[2] = new SqlParameter("@PermisionGroupDescription", SqlDbType.NVarChar) { Value = permisionGroupDescription };
            arrpara[3] = new SqlParameter("@Priority", SqlDbType.NVarChar) { Value = priority };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Rule_Update", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int Insert(string permisionGroupCode, string permisionGroupName, string permisionGroupDescription, int priority)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            arrpara[1] = new SqlParameter("@PermisionGroupName", SqlDbType.NVarChar) { Value = permisionGroupName };
            arrpara[2] = new SqlParameter("@PermisionGroupDescription", SqlDbType.NVarChar) { Value = permisionGroupDescription };
            arrpara[3] = new SqlParameter("@Priority", SqlDbType.NVarChar) { Value = priority };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Rule_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }


        public DataTable RuleEdit_Load(string permisionGroupCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            return _ac.TblReadDataSP("sp_CNY_RuleEdit_Load", arrpara);
        }
        public DataTable RuleEdit_LoadAdvanceFunction(string menuCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = menuCode };
            return _ac.TblReadDataSP("sp_CNY_RuleEdit_LoadAdvanceFunction", arrpara);

        }

        public DataTable RuleEdit_LoadSpecialFunction(string formCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@FormCode", SqlDbType.NVarChar) { Value = formCode };
            return _ac.TblReadDataSP("sp_CNY_RuleEdit_LoadSpecialFunction", arrpara);
        }
        public DataTable RuleEdit_LoadMenu(string permisionGroupCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            return _ac.TblReadDataSP("sp_CNY_RuleEdit_LoadMenu", arrpara);
        }
     
        public bool RuleEdit_UpdateDB(DataTable dtIns,DataTable dtUpd,DataTable dtDel,bool overrideItem)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@typeInsert", SqlDbType.Structured) { Value = dtIns };
            arrpara[1] = new SqlParameter("@typeUpdate", SqlDbType.Structured) { Value = dtUpd };
            arrpara[2] = new SqlParameter("@typeDelete", SqlDbType.Structured) { Value = dtDel };
            arrpara[3] = new SqlParameter("@Override", SqlDbType.Bit) { Value = overrideItem };
            return _ac.BolExcuteSP("sp_CNY_RuleEdit_UpdateDB", arrpara);
        }


        public DataTable RuleEdit_LoadPermissionGroupByGC(string permisionGroupCode)
        {
            string sql = "";
            sql += " select a.PermisionGroupCode,a.PermisionGroupName,a.PermisionGroupDescription,a.[Priority] ";
            sql += " from ListPermisionGroup a ";
            sql += " Where  not exists(select b.PermisionGroupCode from MenuInPermision b Where b.PermisionGroupCode=a.PermisionGroupCode) ";
            sql += string.Format(" and PermisionGroupCode <> '{0}' ", permisionGroupCode);
            return _ac.TblReadDataSQL(sql, null);
        }


        public DataTable RuleEdit_LoadPermissionGroupCopy(string permisionGroupCode)
        {
            string sql = "";
            sql += " select a.PermisionGroupCode,a.PermisionGroupName,a.PermisionGroupDescription,a.[Priority] ";
            sql += " from ListPermisionGroup a ";
            sql += " Where  exists(select b.PermisionGroupCode from MenuInPermision b Where b.PermisionGroupCode=a.PermisionGroupCode) ";
            sql += string.Format(" and PermisionGroupCode <> '{0}' ", permisionGroupCode);
            return _ac.TblReadDataSQL(sql, null);
        }


        public int RuleEdit_DuplicatePermission(string permisionGroupCode, string permisionGroupCodeCopy)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            arrpara[1] = new SqlParameter("@PermisionGroupCodeCopy", SqlDbType.NVarChar) { Value = permisionGroupCodeCopy };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_RuleEdit_DuplicateDB", arrpara);
            if (dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public bool RuleEdit_CopyPermission(string permisionGroupCode, string permisionGroupCodeCopy)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            arrpara[1] = new SqlParameter("@PermisionGroupCodeCopy", SqlDbType.NVarChar) { Value = permisionGroupCodeCopy };
            return _ac.BolExcuteSP("sp_CNY_RuleEdit_CopyDB", arrpara);
       
        }
    }
}
