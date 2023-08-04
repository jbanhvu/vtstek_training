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
    public class Inf_Role
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable LoadTreeViewMainMenu(Int64 idAuthorization, Int64 userInGroupId)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@IDAuthorization", SqlDbType.BigInt) { Value = idAuthorization };
            arrpara[1] = new SqlParameter("@UserInGroupID", SqlDbType.BigInt) { Value = userInGroupId };
            return _ac.TblReadDataSP("usp_Scag_LoadMenuTreeView_New_F1", arrpara);

        }
        public bool AuthorizationOnUser_Copy(Int64 userInGroupId, Int64 iDAuthorization, Int64 userInGroupIdCopy)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@UserInGroupID", SqlDbType.BigInt) { Value = userInGroupId };
            arrpara[1] = new SqlParameter("@IDAuthorization", SqlDbType.BigInt) { Value = iDAuthorization };
            arrpara[2] = new SqlParameter("@UserInGroupIDCopy", SqlDbType.BigInt) { Value = userInGroupIdCopy };
            return _ac.BolExcuteSP("usp_Scag_AuthorizationOnUserGroup_Copy", arrpara);
        }

        public DataTable AuthorizationOnUser_LoadGridUserCopy(string groupUserCode, Int64 id)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            arrpara[1] = new SqlParameter("@ID", SqlDbType.BigInt) { Value = id };
            return _ac.TblReadDataSP("sp_CNY_RoleEdit_LoadGridUserCopy", arrpara);
        }

        public bool AuthorizationOnUser_Update(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@TableUpdate", SqlDbType.Structured) { Value = dt };
            return _ac.BolExcuteSP("sp_CNY_RoleEdit_Update", arrpara);
        }
        public DataTable AuthorizationOnUser_LoadSpecialFunctionByID(string strSpecialFunction)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@StrSpecialFunction", SqlDbType.NVarChar) { Value = strSpecialFunction };
            return _ac.TblReadDataSP("sp_CNY_RoleEdit_LoadSpecialFunctionByID", arrpara);
        }
        public DataTable AuthorizationOnUser_LoadAdvanceFunctionByID(string strAdvanceFunction)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@StrAdvanceFunction", SqlDbType.NVarChar) { Value = strAdvanceFunction };
            return _ac.TblReadDataSP("sp_CNY_RoleEdit_LoadAdvanceFunctionByID", arrpara);
        }
        public DataTable AuthorizationOnUser_LoadTreeListPermission(DataTable dtUserid, Int64 iDAuthorization)
        {
            var arrpara = new SqlParameter[2];
           
            arrpara[0] = new SqlParameter("@IDAuthorization", SqlDbType.BigInt) { Value = iDAuthorization };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtUserid };
            return _ac.TblReadDataSP("sp_CNY_RoleEdit_LoadTreeListPermission_All", arrpara);
        }
        public DataTable AuthorizationOnUser_LoadGridUser(string groupUserCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            return _ac.TblReadDataSP("sp_CNY_RoleEdit_LoadGridUser", arrpara);
        }
        public Int64 Role_GetAuthorizationOnUserGroupID(string userGroupCode, string permisionGroupCode)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@UserGroupCode", SqlDbType.NVarChar) { Value = userGroupCode };
            arrpara[1] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Role_AuthorizationOnUserGroupID", arrpara);
            if (dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt64(dt.Rows[0]["AuthorizationOnUserGroupID"]);
            return 0;
        }

        public bool Role_DeleteRule(string userGroupCode, string permisionGroupCode)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@UserGroupCode", SqlDbType.NVarChar) { Value = userGroupCode };
            arrpara[1] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            return _ac.BolExcuteSP("sp_CNY_Role_DeleteRule", arrpara);
        }

        public bool Role_DeleteUser(string groupUserCode, Int64 userid)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userid };
            return _ac.BolExcuteSP("sp_CNY_Role_DeleteUser", arrpara);
        }
        public bool Role_InsertUser(string groupUserCode, Int64 userid)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userid };
            return _ac.BolExcuteSP("sp_CNY_Role_InsertUser", arrpara);
        }

        public int Role_InsertRule(string userGroupCode, string permisionGroupCode)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@UserGroupCode", SqlDbType.NVarChar) { Value = userGroupCode };
            arrpara[1] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Role_InsertRule", arrpara);
            if (dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt(dt.Rows[0]["ErrCode"]);
            return 0;
        }
        public DataTable LoadRuleByRole(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_CNY_Role_LoadRule", arrpara);

        }
        public DataTable LoadUserByRole(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_CNY_Role_LoadUser", arrpara);
       
        }
        public DataTable GetListRule()
        {
            return _ac.TblReadDataSP("sp_CNY_Role_LoadAllRule", null);
        }

        public DataTable GetListUser()
        {
            return _ac.TblReadDataSP("sp_CNY_Role_LoadAllUser", null);
        }

        public DataTable GridViewData_Load()
        {
            return _ac.TblReadDataSP("sp_CNY_Role_SelectAll", null);
        }
        public int Delete(string permisionGroupCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Role_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int Update(string groupCode, string groupName, string groupDescription, int priority)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@UserGroupCode", SqlDbType.NVarChar) { Value = groupCode };
            arrpara[1] = new SqlParameter("@UserGroupName", SqlDbType.NVarChar) { Value = groupName };
            arrpara[2] = new SqlParameter("@UserGroupDescription", SqlDbType.NVarChar) { Value = groupDescription };
            arrpara[3] = new SqlParameter("@Priority", SqlDbType.NVarChar) { Value = priority };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Role_Update", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int Insert(string groupCode, string groupName, string groupDescription, int priority)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@UserGroupCode", SqlDbType.NVarChar) { Value = groupCode };
            arrpara[1] = new SqlParameter("@UserGroupName", SqlDbType.NVarChar) { Value = groupName };
            arrpara[2] = new SqlParameter("@UserGroupDescription", SqlDbType.NVarChar) { Value = groupDescription };
            arrpara[3] = new SqlParameter("@Priority", SqlDbType.NVarChar) { Value = priority };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Role_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
    }
}
