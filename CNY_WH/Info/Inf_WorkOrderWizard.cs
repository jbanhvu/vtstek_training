using CNY_BaseSys;
using CNY_BaseSys.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CNY_WH.Info
{
    public class GroupInWizard
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public Int64 Index { get; set; }
    }

    public class Inf_WorkOrderWizard
    {

        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        public string CnyCom = DeclareSystem.SysCompanyCode; // location công ty
        public string UserName = DeclareSystem.SysUserName; // user đăng nhập
        public string ItemType { get; set; } // Loại sản phẩm, vật tư,...

        public string Conds { get; set; } //điều kiện lọc
        public DataTable LoadDepartment()
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNYCOM", SqlDbType.NVarChar) { Value = CnyCom };
            return _ac.TblReadDataSP("sp_BOM_Assign_Department_Wizard", arrpara);
        }
        
        
        public DataTable LoadGridSelectRmNormalWhenGenerate(DataTable dt, DataTable dtUser)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@CNYCOM", SqlDbType.NVarChar) { Value = CnyCom };
            arrpara[1] = new SqlParameter("@typeDepartmentCode", SqlDbType.Structured) { Value = dt };
            arrpara[2] = new SqlParameter("@typeUserAssign", SqlDbType.Structured) { Value = dtUser };
            return _ac.TblReadDataSP("sp_BOM_Assign_User_Wizard", arrpara);
        }

        public DataTable LoadProject()
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@CNYCOM", SqlDbType.NVarChar) { Value = CnyCom };
            arrpara[1] = new SqlParameter("@ItemType", SqlDbType.NVarChar) { Value = ItemType };
            arrpara[2] = new SqlParameter("@Conds", SqlDbType.NVarChar) { Value = Conds };

            return _ac.TblReadDataSP("sp_BOM_Assign_Project_Wizard", arrpara);
        }

        public DataTable LoadGridSelectProductWhenGenerate(DataTable dt, DataTable dtUser)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@CNYCOM", SqlDbType.NVarChar) { Value = CnyCom };
            arrpara[1] = new SqlParameter("@TYPE_ProjectCode", SqlDbType.Structured) { Value = dt };
            arrpara[2] = new SqlParameter("@typeUserAssign", SqlDbType.Structured) { Value = dtUser };
            return _ac.TblReadDataSP("sp_BOM_Assign_Product_Wizard", arrpara);
        }
    }
}
