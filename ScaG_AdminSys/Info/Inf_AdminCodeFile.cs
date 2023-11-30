using System;
using System.Data;
using CNY_BaseSys.Common;
using System.Data.SqlClient;
using CNY_AdminSys.Class;
using CNY_BaseSys;

namespace CNY_AdminSys.Info
{
    public class Inf_AdminCodeFile
    {
        readonly AccessData ac = new AccessData(DeclareSystem.SysConnectionString);

        //Menu Listing
        public int ListMenu_Delete(string menuCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = menuCode };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListMenu_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
        public int ListMenu_Update_WithImage(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[6];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = cls.MenuCode };
            arrpara[1] = new SqlParameter("@FormName", SqlDbType.NVarChar) { Value = cls.FormName };
            arrpara[2] = new SqlParameter("@FormCode", SqlDbType.NVarChar) { Value = cls.FormCode };
            arrpara[3] = new SqlParameter("@ProjectCode", SqlDbType.NVarChar) { Value = cls.ProjectCode };
            arrpara[4] = new SqlParameter("@FolderContainForm", SqlDbType.NVarChar) { Value = cls.FolderContainForm };
            arrpara[5] = new SqlParameter("@MenuImage", SqlDbType.Image) { Value = ProcessGeneral.ConvertImageToByteArray(cls.MenuImagePath) };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListMenu_Update_WithImage", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public bool ListMenu_Update_ProcessDocument(string menuCode, Byte[] data)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = menuCode };
            arrpara[1] = new SqlParameter("@ProcessDocument", SqlDbType.VarBinary) { Value = data };
            return ac.BolExcuteSP("usp_Scag_ListMenu_Update_ProcessDocument", arrpara);
        }

        public bool ListMenu_Update_GuideDocument(string menuCode, Byte[] data)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = menuCode };
            arrpara[1] = new SqlParameter("@GuideDocument", SqlDbType.VarBinary) { Value = data };
            return ac.BolExcuteSP("usp_Scag_ListMenu_Update_GuideDocument", arrpara);
        }

        public int ListMenu_Update_NoneImage(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[5];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = cls.MenuCode };
            arrpara[1] = new SqlParameter("@FormName", SqlDbType.NVarChar) { Value = cls.FormName };
            arrpara[2] = new SqlParameter("@FormCode", SqlDbType.NVarChar) { Value = cls.FormCode };
            arrpara[3] = new SqlParameter("@ProjectCode", SqlDbType.NVarChar) { Value = cls.ProjectCode };
            arrpara[4] = new SqlParameter("@FolderContainForm", SqlDbType.NVarChar) { Value = cls.FolderContainForm };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListMenu_Update_NoneImage", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
        public int ListMenu_Insert(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[6];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = cls.MenuCode };
            arrpara[1] = new SqlParameter("@FormName", SqlDbType.NVarChar) { Value = cls.FormName };
            arrpara[2] = new SqlParameter("@FormCode", SqlDbType.NVarChar) { Value = cls.FormCode };
            arrpara[3] = new SqlParameter("@ProjectCode", SqlDbType.NVarChar) { Value = cls.ProjectCode };
            arrpara[4] = new SqlParameter("@FolderContainForm", SqlDbType.NVarChar) { Value = cls.FolderContainForm };
            arrpara[5] = new SqlParameter("@MenuImage", SqlDbType.Image) { Value = ProcessGeneral.ConvertImageToByteArray(cls.MenuImagePath) };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListMenu_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public string ListMenu_CreateMenuCode()
        {
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListMenu_CreateMenuCode", null);
            return ProcessGeneral.GetSafeString(dt.Rows[0]["MenuCode"]);
        }
        public DataTable ListMenu_Load()
        {
            return ac.TblReadDataSP("usp_Scag_ListMenu_SelectAll_New", null);
        }

        //Type Show Form Listing
        public int ListTypeShowForm_Delete(string showCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@ShowCode", SqlDbType.NVarChar) { Value = showCode };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListTypeShowForm_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListTypeShowForm_Update(string showCode, string showName, string showDescription)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@ShowCode", SqlDbType.NVarChar) { Value = showCode };
            arrpara[1] = new SqlParameter("@ShowName", SqlDbType.NVarChar) { Value = showName };
            arrpara[2] = new SqlParameter("@ShowDescription", SqlDbType.NVarChar) { Value = showDescription };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListTypeShowForm_Update", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListTypeShowForm_Insert(string showCode, string showName, string showDescription)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@ShowCode", SqlDbType.NVarChar) { Value = showCode };
            arrpara[1] = new SqlParameter("@ShowName", SqlDbType.NVarChar) { Value = showName };
            arrpara[2] = new SqlParameter("@ShowDescription", SqlDbType.NVarChar) { Value = showDescription };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListTypeShowForm_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public DataTable ListTypeShowForm_Load()
        {
            return ac.TblReadDataSP("usp_Scag_ListTypeShowForm_SelectAll", null);
        }

        //Permision Group Listing
        public int ListPermisionGroup_Delete(string permisionGroupCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListPermisionGroup_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListPermisionGroup_Update(string permisionGroupCode, string permisionGroupName, string permisionGroupDescription, int priority)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            arrpara[1] = new SqlParameter("@PermisionGroupName", SqlDbType.NVarChar) { Value = permisionGroupName };
            arrpara[2] = new SqlParameter("@PermisionGroupDescription", SqlDbType.NVarChar) { Value = permisionGroupDescription };
            arrpara[3] = new SqlParameter("@Priority", SqlDbType.NVarChar) { Value = priority };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListPermisionGroup_Update", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListPermisionGroup_Insert(string permisionGroupCode, string permisionGroupName, string permisionGroupDescription, int priority)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            arrpara[1] = new SqlParameter("@PermisionGroupName", SqlDbType.NVarChar) { Value = permisionGroupName };
            arrpara[2] = new SqlParameter("@PermisionGroupDescription", SqlDbType.NVarChar) { Value = permisionGroupDescription };
            arrpara[3] = new SqlParameter("@Priority", SqlDbType.NVarChar) { Value = priority };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListPermisionGroup_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public DataTable ListPermisionGroup_Load()
        {
            return ac.TblReadDataSP("usp_Scag_ListPermisionGroup_SelectAll", null);
        }

        //User Group Listing
        public int ListUserGroup_Delete(string groupUserCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListUserGroup_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListUserGroup_Update(string groupUserCode, string groupUserName, string groupUserDescription, int priority)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            arrpara[1] = new SqlParameter("@GroupUserName", SqlDbType.NVarChar) { Value = groupUserName };
            arrpara[2] = new SqlParameter("@GroupUserDescription", SqlDbType.NVarChar) { Value = groupUserDescription };
            arrpara[3] = new SqlParameter("@Priority", SqlDbType.NVarChar) { Value = priority };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListUserGroup_Update", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListUserGroup_Insert(string groupUserCode, string groupUserName, string groupUserDescription, int priority)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            arrpara[1] = new SqlParameter("@GroupUserName", SqlDbType.NVarChar) { Value = groupUserName };
            arrpara[2] = new SqlParameter("@GroupUserDescription", SqlDbType.NVarChar) { Value = groupUserDescription };
            arrpara[3] = new SqlParameter("@Priority", SqlDbType.NVarChar) { Value = priority };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListUserGroup_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public DataTable ListUserGroup_Load()
        {
            return ac.TblReadDataSP("usp_Scag_ListUserGroup_SelectAll", null);
        }
        //User Listing
        public int ListUser_Delete(Int64 userID)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userID };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListUser_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
        public int ListUser_Update_WithImage(Cls_AdminCodeFile cls)
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
            arrpara[8] = new SqlParameter("@Signature", SqlDbType.Image) { Value = ProcessGeneral.ConvertImageToByteArray(cls.StrPath) };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListUser_Update_WithImage", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
        public int ListUser_Update_NoneImage(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[8];
            arrpara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = cls.UserID };
            arrpara[1] = new SqlParameter("@FullName", SqlDbType.NVarChar) { Value = cls.FullName };
            arrpara[2] = new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = cls.DateOfBirth };
            arrpara[3] = new SqlParameter("@Sex", SqlDbType.Bit) { Value = cls.Sex };
            arrpara[4] = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = cls.Email };
            arrpara[5] = new SqlParameter("@PositionsCode", SqlDbType.NVarChar) { Value = cls.PositionsCode };
            arrpara[6] = new SqlParameter("@DepartmentCode", SqlDbType.NVarChar) { Value = cls.DepartmentCode };
            arrpara[7] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = cls.IsActive };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListUser_Update_NoneImage", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
        public int ListUser_Insert(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[10];
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
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListUser_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
        public DataTable ListUser_CBDepartment()
        {
            return ac.TblReadDataSP("usp_Scag_ListPositions_CBDepartment", null);
        }
        public DataTable ListUser_CBPositions()
        {
            return ac.TblReadDataSP("usp_Scag_ListPositions_CBPositions", null);
        }
        public DataTable ListUser_Load()
        {
            return ac.TblReadDataSP("usp_Scag_ListUser_SelectAll", null);
        }
        //Positions Listing
        public int ListPositions_Delete(string posCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PositionsCode", SqlDbType.NVarChar) { Value = posCode };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListPositions_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListPositions_Update(string posCode, string posName, string posDesc)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@PositionsCode", SqlDbType.NVarChar) { Value = posCode };
            arrpara[1] = new SqlParameter("@PositionsName", SqlDbType.NVarChar) { Value = posName };
            arrpara[2] = new SqlParameter("@PositionsDescription", SqlDbType.NVarChar) { Value = posDesc };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListPositions_Update", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListPositions_Insert(string posCode, string posName, string posDesc)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@PositionsCode", SqlDbType.NVarChar) { Value = posCode };
            arrpara[1] = new SqlParameter("@PositionsName", SqlDbType.NVarChar) { Value = posName };
            arrpara[2] = new SqlParameter("@PositionsDescription", SqlDbType.NVarChar) { Value = posDesc };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListPositions_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public DataTable ListPositions_Load()
        {
            return ac.TblReadDataSP("usp_Scag_ListPositions_SelectAll", null);
        }

        //Department listing

        public int ListDepartment_Delete(string departmentCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@DepartmentCode", SqlDbType.NVarChar) { Value = departmentCode };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListDepartment_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListDepartment_Update(string departmentCode, string departmentName, string companyCode)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@DepartmentCode", SqlDbType.NVarChar) { Value = departmentCode };
            arrpara[1] = new SqlParameter("@DepartmentName", SqlDbType.NVarChar) { Value = departmentName };
            arrpara[2] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = companyCode };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListDepartment_Update", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListDepartment_Insert(string departmentCode, string departmentName, string companyCode)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@DepartmentCode", SqlDbType.NVarChar) { Value = departmentCode };
            arrpara[1] = new SqlParameter("@DepartmentName", SqlDbType.NVarChar) { Value = departmentName };
            arrpara[2] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = companyCode };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListDepartment_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public DataTable ListDepartment_Load()
        {
            return ac.TblReadDataSP("usp_Scag_ListDepartment_SelectAll", null);
        }

        public DataTable ListDepartment_CBCompanyCode()
        {
            return ac.TblReadDataSP("usp_Scag_ListDepartment_CBCompanyCode", null);
        }
        //company listing
        public int ListCompany_Delete(string companyCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = companyCode };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListCompany_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListCompany_Update(string companyCode, string companyName, string adress, string phoneNumber, string fax, string personRepresent, string positions, string CNY001, string CNY002, string CNY003, string CNY004, string CNY005, string CNY006, string CNY007)
        {
            var arrpara = new SqlParameter[14];
            arrpara[0] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = companyCode };
            arrpara[1] = new SqlParameter("@CompanyName", SqlDbType.NVarChar) { Value = companyName };
            arrpara[2] = new SqlParameter("@Adrress", SqlDbType.NVarChar) { Value = adress };
            arrpara[3] = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = phoneNumber };
            arrpara[4] = new SqlParameter("@Fax", SqlDbType.NVarChar) { Value = fax };
            arrpara[5] = new SqlParameter("@PersonRepresent", SqlDbType.NVarChar) { Value = personRepresent };
            arrpara[6] = new SqlParameter("@Positions", SqlDbType.NVarChar) { Value = positions };
            arrpara[7] = new SqlParameter("@CNY001", SqlDbType.NVarChar) { Value = CNY001 };
            arrpara[8] = new SqlParameter("@CNY002", SqlDbType.NVarChar) { Value = CNY002 };
            arrpara[9] = new SqlParameter("@CNY003", SqlDbType.NVarChar) { Value = CNY003 };
            arrpara[10] = new SqlParameter("@CNY004", SqlDbType.NVarChar) { Value = CNY004 };
            arrpara[11] = new SqlParameter("@CNY005", SqlDbType.NVarChar) { Value = CNY005 };
            arrpara[12] = new SqlParameter("@CNY006", SqlDbType.NVarChar) { Value = CNY006 };
            arrpara[13] = new SqlParameter("@CNY007", SqlDbType.NVarChar) { Value = CNY007 };

            DataTable dt = ac.TblReadDataSP("usp_Scag_ListCompany_Update", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListCompany_Insert(string companyCode, string companyName, string adress, string phoneNumber, string fax, string personRepresent, string positions, string CNY001, string CNY002, string CNY003, string CNY004, string CNY005, string CNY006, string CNY007)
        {
            var arrpara = new SqlParameter[14];
            arrpara[0] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = companyCode };
            arrpara[1] = new SqlParameter("@CompanyName", SqlDbType.NVarChar) { Value = companyName };
            arrpara[2] = new SqlParameter("@Adrress", SqlDbType.NVarChar) { Value = adress };
            arrpara[3] = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = phoneNumber };
            arrpara[4] = new SqlParameter("@Fax", SqlDbType.NVarChar) { Value = fax };
            arrpara[5] = new SqlParameter("@PersonRepresent", SqlDbType.NVarChar) { Value = personRepresent };
            arrpara[6] = new SqlParameter("@Positions", SqlDbType.NVarChar) { Value = positions };
            arrpara[7] = new SqlParameter("@CNY001", SqlDbType.NVarChar) { Value = CNY001 };
            arrpara[8] = new SqlParameter("@CNY002", SqlDbType.NVarChar) { Value = CNY002 };
            arrpara[9] = new SqlParameter("@CNY003", SqlDbType.NVarChar) { Value = CNY003 };
            arrpara[10] = new SqlParameter("@CNY004", SqlDbType.NVarChar) { Value = CNY004 };
            arrpara[11] = new SqlParameter("@CNY005", SqlDbType.NVarChar) { Value = CNY005 };
            arrpara[12] = new SqlParameter("@CNY006", SqlDbType.NVarChar) { Value = CNY006 };
            arrpara[13] = new SqlParameter("@CNY007", SqlDbType.NVarChar) { Value = CNY007 };
            DataTable dt = ac.TblReadDataSP("usp_Scag_ListCompany_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public DataTable ListCompany_Load()
        {
            return ac.TblReadDataSP("usp_Scag_ListCompany_SelectAll", null);
        }
        public DataTable LoadCurrency()
        {
            return ac.TblReadDataSQL("SELECT CNY001 Code,CNY002 Name FROM dbo.CNY00005", null);
        }
        //
        public DataTable GetParentTableUserGroup()
        {
            return ac.TblReadDataSP("usp_Scag_ListUserGroup_LoadParent", null);
        }
        public DataTable GetChildTableUserGroup(DataTable dtCondition)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@TypeCondition", SqlDbType.Structured) { Value = dtCondition };
            return ac.TblReadDataSP("usp_Scag_ListUserGroup_LoadChild", arrpara);
        }
        public bool UserInGroup_Delete(string groupUserCode, Int64 userid)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userid };
            return ac.BolExcuteSP("usp_Scag_UserInGroup_Delete", arrpara);
        }
        public bool UserInGroup_Insert(string groupUserCode, Int64 userid)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            arrpara[1] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userid };
            return ac.BolExcuteSP("usp_Scag_UserInGroup_Insert", arrpara);
        }
        public DataTable GetTableUserSelected(string groupUserCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            return ac.TblReadDataSP("usp_Scag_UserInGroup_LoadUserSelected", arrpara);
        }
        public bool AuthorizationOnUserGroup_Delete(Int64 id)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@ID", SqlDbType.BigInt) { Value = id };
            return ac.BolExcuteSP("usp_AuthorizationOnUserGroup_Delete", arrpara);
        }
        public int AuthorizationOnUserGroup_Insert(string userGroupCode, string permisionGroupCode)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@UserGroupCode", SqlDbType.NVarChar) { Value = userGroupCode };
            arrpara[1] = new SqlParameter("@PermisionGroupCode", SqlDbType.NVarChar) { Value = permisionGroupCode };
            DataTable dt = ac.TblReadDataSP("usp_Scag_AuthorizationOnUserGroup_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return ProcessGeneral.GetSafeInt(dt.Rows[0]["ErrCode"]);
            return 0;
        }
        public DataTable AuthorizationOnUserGroup_LoadPermissionGroupByUC(string userGroupCode)
        {
            string strSql = "";
            strSql += " select PermisionGroupCode,PermisionGroupName,PermisionGroupDescription,[Priority] from ListPermisionGroup ";
            strSql += string.Format(" Where PermisionGroupCode not in (select PermisionGroupCode from AuthorizationOnUserGroup Where GroupUserCode='{0}') ", userGroupCode);
            return ac.TblReadDataSQL(strSql, null);
        }
        public DataTable ListRole_LoadUser(string groupUserCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@GroupUserCode", SqlDbType.NVarChar) { Value = groupUserCode };
            return ac.TblReadDataSP("usp_Scag_ListRole_LoadUser", arrpara);
        }
    }
}
