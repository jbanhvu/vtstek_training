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
    public class Inf_Menu
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public DataTable Menu_LoadCBShowType()
        {
            return _ac.TblReadDataSP("sp_CNY_Menu_CBShowType", null);
        }
        public DataTable Menu_LoadCBModule()
        {
            return _ac.TblReadDataSP("sp_CNY_Menu_CBModule", null);
        }
        public DataTable GridViewData_Load()
        {
            return _ac.TblReadDataSP("sp_CNY_Menu_SelectAll_New", null);
        }
        public bool ListMenu_Update_ProcessDocument(string menuCode, Byte[] data)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = menuCode };
            arrpara[1] = new SqlParameter("@ProcessDocument", SqlDbType.VarBinary) { Value = data };
            return _ac.BolExcuteSP("sp_CNY_Menu_Update_ProcessDocument", arrpara);
        }

        public bool ListMenu_Update_GuideDocument(string menuCode, Byte[] data)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = menuCode };
            arrpara[1] = new SqlParameter("@GuideDocument", SqlDbType.VarBinary) { Value = data };
            return _ac.BolExcuteSP("sp_CNY_Menu_Update_GuideDocument", arrpara);
        }
        public int Delete(string menuCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = menuCode };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Menu_Delete", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
        public string ListMenu_CreateMenuCode()
        {
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Menu_CreateMenuCode", null);
            return ProcessGeneral.GetSafeString(dt.Rows[0]["MenuCode"]);
        }

        public int ListMenu_Update_WithImage(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[10];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = cls.MenuCode };
            arrpara[1] = new SqlParameter("@FormName", SqlDbType.NVarChar) { Value = cls.FormName };
            arrpara[2] = new SqlParameter("@FormCode", SqlDbType.NVarChar) { Value = cls.FormCode };
            arrpara[3] = new SqlParameter("@ProjectCode", SqlDbType.NVarChar) { Value = cls.ProjectCode };
            arrpara[4] = new SqlParameter("@FolderContainForm", SqlDbType.NVarChar) { Value = cls.FolderContainForm };
            arrpara[5] = new SqlParameter("@MenuImage", SqlDbType.Image) { Value = ProcessGeneral.ConvertImageToByteArray(cls.MenuImagePath) };
            arrpara[6] = new SqlParameter("@ShowCode", SqlDbType.NVarChar) { Value = cls.ShowCode };
            arrpara[7] = new SqlParameter("@WorkFunCode", SqlDbType.NVarChar) { Value = cls.WorkFunCode };
            arrpara[8] = new SqlParameter("@IsActive", SqlDbType.NVarChar) { Value = cls.IsActiveMenu };
            arrpara[9] = new SqlParameter("@SystemName", SqlDbType.NVarChar) { Value = cls.SystemName };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Menu_WithImage", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }

        public int ListMenu_Update_NoneImage(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[9];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = cls.MenuCode };
            arrpara[1] = new SqlParameter("@FormName", SqlDbType.NVarChar) { Value = cls.FormName };
            arrpara[2] = new SqlParameter("@FormCode", SqlDbType.NVarChar) { Value = cls.FormCode };
            arrpara[3] = new SqlParameter("@ProjectCode", SqlDbType.NVarChar) { Value = cls.ProjectCode };
            arrpara[4] = new SqlParameter("@FolderContainForm", SqlDbType.NVarChar) { Value = cls.FolderContainForm };
            arrpara[5] = new SqlParameter("@ShowCode", SqlDbType.NVarChar) { Value = cls.ShowCode };
            arrpara[6] = new SqlParameter("@WorkFunCode", SqlDbType.NVarChar) { Value = cls.WorkFunCode };
            arrpara[7] = new SqlParameter("@IsActive", SqlDbType.NVarChar) { Value = cls.IsActiveMenu };
            arrpara[8] = new SqlParameter("@SystemName", SqlDbType.NVarChar) { Value = cls.SystemName };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Menu_NoneImage", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
        public int Insert(Cls_AdminCodeFile cls)
        {
            var arrpara = new SqlParameter[10];
            arrpara[0] = new SqlParameter("@MenuCode", SqlDbType.NVarChar) { Value = cls.MenuCode };
            arrpara[1] = new SqlParameter("@FormName", SqlDbType.NVarChar) { Value = cls.FormName };
            arrpara[2] = new SqlParameter("@FormCode", SqlDbType.NVarChar) { Value = cls.FormCode };
            arrpara[3] = new SqlParameter("@ProjectCode", SqlDbType.NVarChar) { Value = cls.ProjectCode };
            arrpara[4] = new SqlParameter("@FolderContainForm", SqlDbType.NVarChar) { Value = cls.FolderContainForm };
            arrpara[5] = new SqlParameter("@MenuImage", SqlDbType.Image) { Value = ProcessGeneral.ConvertImageToByteArray(cls.MenuImagePath) };
            arrpara[6] = new SqlParameter("@ShowCode", SqlDbType.NVarChar) { Value = cls.ShowCode };
            arrpara[7] = new SqlParameter("@WorkFunCode", SqlDbType.NVarChar) { Value = cls.WorkFunCode };
            arrpara[8] = new SqlParameter("@IsActive", SqlDbType.NVarChar) { Value = cls.IsActiveMenu };
            arrpara[9] = new SqlParameter("@SystemName", SqlDbType.NVarChar) { Value = cls.SystemName };
            DataTable dt = _ac.TblReadDataSP("sp_CNY_Menu_Insert", arrpara);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ErrCode"]);
            return 0;
        }
    }
}
