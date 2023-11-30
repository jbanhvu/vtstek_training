using System;
using System.Data;
using System.Data.SqlClient;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_Property;

namespace CNY_Main
{
    public class inf_Main
    {
        readonly AccessData _ac = new AccessData(SystemProperty.SysConnectionString);
        public bool UpdateDatePasswordEmail(Int64 userId, string newPass)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = userId };
            arrpara[1] = new SqlParameter("@NewPass", SqlDbType.NVarChar) { Value = newPass };
            return _ac.BolExcuteSP("sp_CNY_UpdatePasswordEmail", arrpara);

        }
        public string UpdateDatePasswordWhenUserChangePass(Int64 userId, string newPass, string passTemp)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = userId };
            arrpara[1] = new SqlParameter("@NewPass", SqlDbType.NVarChar) { Value = newPass };
            arrpara[2] = new SqlParameter("@PassTemp", SqlDbType.NVarChar) { Value = passTemp };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_UpdatePasswordUser", arrpara);
            return ProcessGeneral.GetSafeString(dt.Rows[0]["ChangePassDate"]);

        }
        public DataTable LoadCbFuncionGroupWhenLogin(string groupUserCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            return _ac.TblReadDataSP("usp_Scag_LoadCBFuctionGroupWhenLogin", arrpara);

        }
        public DataTable LoadTreeViewMainMenu(Int64 idAuthorization, Int64 userInGroupId)
        {
            DataTable dtTemp = _ac.TblReadDataSQL("select IsUpdatePass from dbo.TblCheckUpdPassTemp", null);
            if (dtTemp.Rows.Count > 0 && !ProcessGeneral.GetSafeBool(dtTemp.Rows[0]["IsUpdatePass"]))
            {
                _ac.BolExcuteSQL("update dbo.TblCheckUpdPassTemp set IsUpdatePass=1", null);
                dtTemp = _ac.TblReadDataSQL("select UserID,Password from ListUser", null);
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    Int64 userIdTemp = ProcessGeneral.GetSafeInt64(drTemp["UserID"]);
                    string passwordTemp = EnDeCrypt.Decrypt(ProcessGeneral.GetSafeString(drTemp["Password"]), true);
                    UpdatePassTemp(userIdTemp, passwordTemp);
                }

              
            }


            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@IDAuthorization", SqlDbType.BigInt) { Value = idAuthorization };
            arrpara[1] = new SqlParameter("@UserInGroupID", SqlDbType.BigInt) { Value = userInGroupId };
            return _ac.TblReadDataSP("usp_Scag_LoadMenuTreeView_New_F1", arrpara);

        }
        private bool UpdatePassTemp(Int64 userId,string passTemp)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PassWord", SqlDbType.NVarChar) { Value = passTemp };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userId };
            return _ac.BolExcuteSP("usp_ListUser_UpdatePasswordTemp", arrpara);

        }
        public DataTable LoadTableTypeShowForm(string userGroupCode , Int64 usedId)
        {
            return _ac.TblReadDataSQL(string.Format("SELECT DISTINCT MenuCode,TypeFormShow FROM FormShowSetting WHERE UserID={0} AND GroupUserCode='{1}'", usedId, userGroupCode), null);

        }

    }
}
