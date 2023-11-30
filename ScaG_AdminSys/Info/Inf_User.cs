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
using CNY_Property;

namespace CNY_AdminSys.Info
{
    public class Inf_User
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        public string UpdatePasswordUser(Int64 userId, string newPass)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = userId };
            arrpara[1] = new SqlParameter("@NewPass", SqlDbType.NVarChar) { Value = newPass };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_UpdatePasswordUser", arrpara);
            return ProcessGeneral.GetSafeString(dt.Rows[0]["ChangePassDate"]);

        }
        public bool UserInGroup_Insert(string groupUserCode, Int64 userid)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userid };
            return _ac.BolExcuteSP("sp_CNY_UserInGroup_Insert", arrpara);
        }
        public bool UserInGroup_Delete(string groupUserCode, Int64 userid)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userid };
            return _ac.BolExcuteSP("sp_CNY_UserInGroup_Delete", arrpara);
        }

        public DataTable UserMemberLoadGrid(Int64 userid)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = userid };
            return _ac.TblReadDataSP("sp_CNY_UserMember_LoadGrid", arrpara);
        }
        public DataTable UserMemberLoadRoleSelected(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_CNY_UserMember_LoadRoleSelected", arrpara);
        }

        public DataTable GridViewData_Load()
        {
            return _ac.TblReadDataSP("sp_CNY_User_SelectAll", null);
        }
        public DataTable ListUser_CBDepartment()
        {
            return _ac.TblReadDataSP("sp_CNY_User_CBDepartment", null);
        }
        public DataTable ListUser_CBPositions()
        {
            return _ac.TblReadDataSP("sp_CNY_User_CBPositions", null);
        }
        public int Delete(Int64 userId)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userId };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_User_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int Insert(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[11];
            arrpara[0] = new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = cls.UserName };
            arrpara[1] = new SqlParameter("@FullName", SqlDbType.NVarChar) { Value = cls.FullName };
            arrpara[2] = new SqlParameter("@Password", SqlDbType.NVarChar) { Value = cls.Password };
            arrpara[3] = new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = cls.DateOfBirth };
            arrpara[4] = new SqlParameter("@Sex", SqlDbType.Bit) { Value = cls.Sex };
            arrpara[5] = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = cls.Email };
            arrpara[6] = new SqlParameter("@Signature", SqlDbType.Image) { Value = ProcessGeneral.ConvertImageToByteArray(cls.StrPath) };
            arrpara[7] = new SqlParameter("@PositionsCode", SqlDbType.NVarChar) { Value = cls.PositionsCode };
            arrpara[8] = new SqlParameter("@DepartmentCode", SqlDbType.NVarChar) { Value = cls.DepartmentCode };
            arrpara[9] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = cls.IsActive };
            arrpara[10] = new SqlParameter("@UserUpdate", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_User_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListUser_Update_NoneImage(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[9];
            arrpara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = cls.UserID };
            arrpara[1] = new SqlParameter("@FullName", SqlDbType.NVarChar) { Value = cls.FullName };
            arrpara[2] = new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = cls.DateOfBirth };
            arrpara[3] = new SqlParameter("@Sex", SqlDbType.Bit) { Value = cls.Sex };
            arrpara[4] = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = cls.Email };
            arrpara[5] = new SqlParameter("@PositionsCode", SqlDbType.NVarChar) { Value = cls.PositionsCode };
            arrpara[6] = new SqlParameter("@DepartmentCode", SqlDbType.NVarChar) { Value = cls.DepartmentCode };
            arrpara[7] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = cls.IsActive };
            arrpara[8] = new SqlParameter("@UserUpdate", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_User_Update_NoneImage", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListUser_Update_WithImage(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[10];
            arrpara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = cls.UserID };
            arrpara[1] = new SqlParameter("@FullName", SqlDbType.NVarChar) { Value = cls.FullName };
            arrpara[2] = new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = cls.DateOfBirth };
            arrpara[3] = new SqlParameter("@Sex", SqlDbType.Bit) { Value = cls.Sex };
            arrpara[4] = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = cls.Email };
            arrpara[5] = new SqlParameter("@PositionsCode", SqlDbType.NVarChar) { Value = cls.PositionsCode };
            arrpara[6] = new SqlParameter("@DepartmentCode", SqlDbType.NVarChar) { Value = cls.DepartmentCode };
            arrpara[7] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = cls.IsActive };
            arrpara[8] = new SqlParameter("@Signature", SqlDbType.Image) { Value = ProcessGeneral.ConvertImageToByteArray(cls.StrPath) };
            arrpara[9] = new SqlParameter("@UserUpdate", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_User_Update_WithImage", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        //================
        public DataTable LoadUserReceipientMail()
        {
            return _ac.TblReadDataSP("sp_User_LoadUserReceipientMail", null);
        }

        public DataTable LoadUserWizard(Int64 UserID)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@CNYCOM", SqlDbType.NVarChar) { Value = SystemProperty.SysCompanyCode };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = UserID };
            return _ac.TblReadDataSP("sp_User_LoadUser_Wizard", arrpara);
        }

        public DataTable LoadFunctionInUser(Int64 UserID,string UserName)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = UserID };
            arrpara[1] = new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = UserName };
            return _ac.TblReadDataSP("sp_User_LoadFunctionInUser", arrpara);
        }

        public DataTable GetListUser()
        {
            return _ac.TblReadDataSP("sp_User_LoadAllUser", null);
        }

        public DataTable GetListFunction()
        {
            return _ac.TblReadDataSP("sp_User_LoadAllFunction", null);
        }
        
        

        public Int64 User_InsertUpdateFunction(Int64 UserID, string UserName,string FunctionCode,int IsActive)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = UserID };
            arrpara[1] = new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = UserName };
            arrpara[2] = new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = FunctionCode };
            arrpara[3] = new SqlParameter("@IsActive", SqlDbType.Int) { Value = IsActive };
            DataTable dt = _ac.TblReadDataSP("sp_User_InsertUpdateFunction", arrpara);
            if (dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt64(dt.Rows[0]["CNYSYS11PK"]);
            return 0;
        }

        public bool User_DeleteFunction(Int64 UserID, string UserName, string FunctionCode)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = UserID };
            arrpara[1] = new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = UserName };
            arrpara[2] = new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = FunctionCode };
            return _ac.BolExcuteSP("sp_User_DeleteFunction", arrpara);
        }

        public int User_InsertUserReceipient(Int64 CNYSYS11PK, Int64 UserID)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@CNYSYS11PK", SqlDbType.BigInt) { Value = CNYSYS11PK };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = UserID };
            DataTable dt = _ac.TblReadDataSP("sp_User_InsertUserReceipient", arrpara);
            if (dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public DataTable LoadUserByFunction(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_User_LoadReceipientInFunction", arrpara);

        }
        public DataTable LoadFunctionByUser(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_User_LoadFunctionInUser", arrpara);

        }
        public bool User_DeleteUserReceipient(Int64 CNYSYS11PK, Int64 UserID)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@CNYSYS11PK", SqlDbType.BigInt) { Value = CNYSYS11PK };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = UserID };
            return _ac.BolExcuteSP("sp_User_DeleteUserReceipient", arrpara);
        }




        // New from 14/02/2019

        public DataTable FunctionLoadSelected(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_CNY_User_FunctionLoadSelected", arrpara);
        }
        public DataTable TabUserInfo_Load(string sFilter)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@where", SqlDbType.NVarChar) { Value = sFilter };
            return _ac.TblReadDataSP("sp_CNY_User_SelectAll_New", arrpara);
        }

        public DataTable StatusLoadSelected(DataTable dt, string WorkFunCode)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@WorkFunCode", SqlDbType.NVarChar) { Value = WorkFunCode };
            return _ac.TblReadDataSP("sp_CNY_User_StatusLoadSelected", arrpara);

        }

        public DataTable LoadSatusByFunction(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_CNY_User_LoadStatusInFunction", arrpara);

        }

        public DataTable ReceipientLoadSelected(DataTable dt,Int64 userID)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userID };
            return _ac.TblReadDataSP("sp_CNY_User_ReceipientLoadSelected", arrpara);
        }

        public DataTable LoadFunction(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_CNY_User_LoadFunctionInUser", arrpara);

        }

        public DataTable LoadReceipientByFunction(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_CNY_User_LoadReceipientInFunction", arrpara);

        }

        public Int64 InsertUser(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[11];
            arrpara[0] = new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = cls.UserName };
            arrpara[1] = new SqlParameter("@FullName", SqlDbType.NVarChar) { Value = cls.FullName };
            arrpara[2] = new SqlParameter("@Password", SqlDbType.NVarChar) { Value = cls.Password };
            arrpara[3] = new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = cls.DateOfBirth };
            arrpara[4] = new SqlParameter("@Sex", SqlDbType.Bit) { Value = cls.Sex };
            arrpara[5] = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = cls.Email };
            arrpara[6] = new SqlParameter("@Signature", SqlDbType.Image) { Value = ProcessGeneral.ConvertImageToByteArray(cls.StrPath) };
            arrpara[7] = new SqlParameter("@PositionsCode", SqlDbType.NVarChar) { Value = cls.PositionsCode };
            arrpara[8] = new SqlParameter("@DepartmentCode", SqlDbType.NVarChar) { Value = cls.DepartmentCode };
            arrpara[9] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = cls.IsActive };
            arrpara[10] = new SqlParameter("@UserUpdate", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_User_InsertNew", arrpara);
            if (dt!=null && dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt64(dt.Rows[0]["UserID"]);
            return 0;
        }

        public bool BolExcuteSqlText(string sql)
        {
            return _ac.BolExcuteSQL(sql, null);
        }

        public bool User_InsertUpdateFunction(Int64 CNYSYS11PK, Int64 UserID, string UserName, string FunctionCode, int IsActive, string RowState)
        {
            var arrpara = new SqlParameter[7];
            arrpara[0] = new SqlParameter("@CNYSYS11PK", SqlDbType.BigInt) { Value = CNYSYS11PK };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = UserID };
            arrpara[2] = new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = UserName };
            arrpara[3] = new SqlParameter("@FunctionCode", SqlDbType.NVarChar) { Value = FunctionCode };
            arrpara[4] = new SqlParameter("@IsActive", SqlDbType.Int) { Value = IsActive };
            arrpara[5] = new SqlParameter("@RowState", SqlDbType.NVarChar) { Value = RowState };
            arrpara[6] = new SqlParameter("@UserUpdate", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            return _ac.BolExcuteSP("sp_CNY_User_InsertUpdateFunction", arrpara);
            
        }

        public bool User_SaveStatus(DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@UserUpdate", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            return _ac.BolExcuteSP("sp_CNY_User_SaveStatus", arrpara);

        }
        public bool User_SaveReceipient(DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dt };
            arrpara[1] = new SqlParameter("@UserUpdate", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            return _ac.BolExcuteSP("sp_CNY_User_SaveReceipient", arrpara);

        }

        public DataTable TypeReceipient_Load()
        {
            return _ac.TblReadDataSP("sp_CNY_User_LoadTypeReceipient", null);
        }




        //Adjust 12/03/2020
        public DataTable LoadDataGridView_Module(Int64 userId)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userId };
            return _ac.TblReadDataSP("sp_ADMINSys_LoadDataGridView_Module", arrPara);
        }

        public DataTable LoadDataTreeList_Responsibility(Int64 cny00004Pk, string moduleCode)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@CNY00004PK", SqlDbType.BigInt) { Value = cny00004Pk };
            arrPara[1] = new SqlParameter("@moduleCode", SqlDbType.NVarChar) { Value = moduleCode };
            return _ac.TblReadDataSP("sp_MF_LoadDataTreeList_Responsibility", arrPara);
        }

        public DataTable InsertCNY00004(string userName, string fullName, string useModule, Int64 UserID)
        {
            var arrPara = new SqlParameter[5];
            arrPara[0] = new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = userName };
            arrPara[1] = new SqlParameter("@FullName", SqlDbType.NVarChar) { Value = fullName };
            arrPara[2] = new SqlParameter("@UseModule", SqlDbType.NVarChar) { Value = useModule };
            arrPara[3] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = UserID };
            arrPara[4] = new SqlParameter("@UserUpd", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            return _ac.TblReadDataSP("sp_MF_InsertCNY00004", arrPara);
        }

        public DataTable UpdateCNY00004(Int64 cny00004Pk, Int64 userId, string userName, string fullName, string useModule)
        {
            var arrPara = new SqlParameter[6];
            arrPara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = cny00004Pk };
            arrPara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userId };
            arrPara[2] = new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = userName };
            arrPara[3] = new SqlParameter("@FullName", SqlDbType.NVarChar) { Value = fullName };
            arrPara[4] = new SqlParameter("@UseModule", SqlDbType.NVarChar) { Value = useModule };
            arrPara[5] = new SqlParameter("@UserUpd", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };
            return _ac.TblReadDataSP("sp_MF_UpdateCNY00004", arrPara);
        }

        public int DeleteCNY00004(Int64 userId)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userId };
            DataTable dt = _ac.TblReadDataSP("sp_MF_DeleteCNY00004", arrPara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public DataTable LoadCNY00004(Int64 userId)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userId };
            return _ac.TblReadDataSP("sp_MF_LoadCNY00004", arrPara);
        }

        public bool Insert_Responsibility(DataTable dtResponsibility)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dtResponsibility };
            return _ac.BolExcuteSP("sp_ADMINSys_Insert_Responsibility", arrPara);
        }

        public bool Delete_ResponsibilityNEW(Int64 cny00004Pk, string strCny00001Pk, string module)
        {
            var arrPara = new SqlParameter[3];
            arrPara[0] = new SqlParameter("@CNY00004PK", SqlDbType.BigInt) { Value = cny00004Pk };
            arrPara[1] = new SqlParameter("@StrCny00001Pk", SqlDbType.NVarChar) { Value = strCny00001Pk };
            arrPara[2] = new SqlParameter("@Module", SqlDbType.NVarChar) { Value = module };
            return _ac.BolExcuteSP("sp_ADMINSys_Delete_ResponsibilityNEW", arrPara);
        }

        public bool DeleteModule(Int64 cny00004Pk, string strModule)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@CNY00004PK", SqlDbType.BigInt) { Value = cny00004Pk };
            arrPara[1] = new SqlParameter("@StrModule", SqlDbType.NVarChar) { Value = strModule };
            return _ac.BolExcuteSP("sp_ADMINSys_DeleteModule", arrPara);
        }

        public DataTable LoadModuleF4(DataTable dtCondition)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtCondition };
            return _ac.TblReadDataSP("sp_ADMINSys_LoadModule", arrPara);
        }

        public DataTable TblExcuteSqlText(string sql)
        {
            return _ac.TblReadDataSQL(sql, null);
        }

        public bool Update_Responsibility(Int64 cny00004Pk, DataTable dtResponsibility, string strModule)
        {
            var arrPara = new SqlParameter[3];
            arrPara[0] = new SqlParameter("@@CNY00004PK", SqlDbType.BigInt) { Value = cny00004Pk };
            arrPara[1] = new SqlParameter("@Type", SqlDbType.Structured) { Value = dtResponsibility };
            arrPara[2] = new SqlParameter("@StrModule", SqlDbType.NVarChar) { Value = strModule };
            return _ac.BolExcuteSP("sp_MF_Update_Responsibility", arrPara);
        }

        public bool Delete_Responsibility(Int64 cny00004Pk, string strCny00001Pk, string strModule)
        {
            var arrPara = new SqlParameter[3];
            arrPara[0] = new SqlParameter("@@CNY00004PK", SqlDbType.BigInt) { Value = cny00004Pk };
            arrPara[1] = new SqlParameter("@StrCny00001Pk", SqlDbType.NVarChar) { Value = strCny00001Pk };
            arrPara[2] = new SqlParameter("@StrModule", SqlDbType.NVarChar) { Value = strModule };
            return _ac.BolExcuteSP("sp_ADMINSys_Delete_Responsibility", arrPara);
        }
    }


    public class Cls_UserReceipient
    {
        public DataTable dt { get; set; }

    }
}
