using CNY_BaseSys;
using CNY_BaseSys.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CNY_WH.Class;

namespace CNY_WH.Info
{
    class InfoWhMf
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        public DataTable LoadListCustSupp106(string sCode, int iSupp)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = sCode };
            arrpara[1] = new SqlParameter("@iSupp", SqlDbType.NVarChar) { Value = iSupp };
            return _ac.TblReadDataSP("usp_CNY_0106", arrpara);
        }
        public DataTable LoadWoList109(string sCustomer, string sType)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Customer", SqlDbType.NVarChar) { Value = sCustomer };
            arrpara[1] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            return _ac.TblReadDataSP("usp_CNY_0109", arrpara);
        }
        public DataTable LoadF4List110(string sSelect, string sFrom, string sWhere)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@Fields", SqlDbType.NVarChar) { Value = sSelect };
            arrpara[1] = new SqlParameter("@Tables", SqlDbType.NVarChar) { Value = sFrom };
            arrpara[2] = new SqlParameter("@Criteria", SqlDbType.NVarChar) { Value = sWhere };
            return _ac.TblReadDataSP("usp_CNY_0110", arrpara);
        }
        public DataTable LoadF4AttributeValues(Int64 iCny08Pk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00008PK", SqlDbType.BigInt) { Value = iCny08Pk };
            return _ac.TblReadDataSP("usp_RMCode_LoadAttributeValueF4_Input_New", arrpara);
        }

        public DataTable LoadF4AttributeValues00162(Int64 iCny08Pk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = iCny08Pk };
            return _ac.TblReadDataSP("usp_CNY_00162", arrpara);
        }

        public DataTable LoadCodeAttributeValues(Int64 iAttPk, string sCode, string sWh, string sBatch, string sBin)
        {
            var arrpara = new SqlParameter[5];
            arrpara[0] = new SqlParameter("@Attid", SqlDbType.BigInt) { Value = iAttPk };
            arrpara[1] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = sCode };
            arrpara[2] = new SqlParameter("@Wh", SqlDbType.NVarChar) { Value = sWh };
            arrpara[3] = new SqlParameter("@Batch", SqlDbType.NVarChar) { Value = sBatch };
            arrpara[4] = new SqlParameter("@Bin", SqlDbType.NVarChar) { Value = sBin };
            return _ac.TblReadDataSP("usp_CNY_0138", arrpara);
        }

        public DataTable LoadAlterCode139(string sCode)
        {
            var arrPara= new SqlParameter[1];
            arrPara[0] = new SqlParameter("@Code", SqlDbType.NVarChar) {Value = sCode};
            return _ac.TblReadDataSP("usp_CNY_0139", arrPara);
        }
        public DataTable LoadItemCodeInfo0140(string sCode)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@ItemCona", SqlDbType.NVarChar) { Value = sCode };
            return _ac.TblReadDataSP("usp_CNY_0140", arrPara);
        }

        public DataTable LoadItemCodeInfo00166(string sType, string sCode)
        {
            var arrPara = new SqlParameter[2];
            arrPara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrPara[1] = new SqlParameter("@ItemCona", SqlDbType.NVarChar) { Value = sCode };
            return _ac.TblReadDataSP("usp_CNY_00166", arrPara);
        }

        public DataTable LoadBatchInfo129(string sCode, string sWh)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@sCode", SqlDbType.NVarChar) { Value = sCode };
            arrpara[1] = new SqlParameter("@sWh", SqlDbType.NVarChar) { Value = sWh };
            return _ac.TblReadDataSP("usp_CNY_0129", arrpara);
        }

        public DataTable LoadItemInfo132(string sCode)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Code", SqlDbType.NVarChar) { Value = sCode };
          
            return _ac.TblReadDataSP("usp_CNY_0132", arrpara);
        }

        public string GetStockDocType(string sMenu)
        {
            string sType = "";
            switch (sMenu)
            {
                case ClsWhConstant.ConMenuCodeStkRmRecPo:
                    sType = ClsWhConstant.ConStkRmRecPo;
                    break;
                case ClsWhConstant.ConMenuCodeStkRmRecRe:
                    sType = ClsWhConstant.ConStkRmRecRe;
                    break;
                case ClsWhConstant.ConMenuCodeStkFgRecSo:
                    sType = ClsWhConstant.ConStkFgRecSo;
                    break;
                case ClsWhConstant.ConMenuCodeStkFgRecRe:
                    sType = ClsWhConstant.ConStkFgRecRe;
                    break;

                case ClsWhConstant.ConMenuCodeStkRmIssMo:
                    sType = ClsWhConstant.ConStkRmIssMo;
                    break;
                case ClsWhConstant.ConMenuCodeStkRmIssRe:
                    sType = ClsWhConstant.ConStkRmIssRe;
                    break;
                case ClsWhConstant.ConMenuCodeStkFgIssSo:
                    sType = ClsWhConstant.ConStkFgIssSo;
                    break;
                case ClsWhConstant.ConMenuCodeStkFgIssRe:
                    sType = ClsWhConstant.ConStkFgIssRe;
                    break;
            }
            return sType;
        }

        #region Import FG Balance File

        public DataTable UploadFgBalanceFile(DataTable dtData, Int64 lCny00104Pk)
        {
            var arr = new SqlParameter[3];

            arr[0] = new SqlParameter("@TblData", SqlDbType.Structured) { Value = dtData };
            arr[1] = new SqlParameter("@Cny00104Pk", SqlDbType.BigInt) { Value = lCny00104Pk };
            arr[2] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = DeclareSystem.SysUserName };

            return _ac.TblReadDataSP("usp_Apparel_SQ_040", arr);
        }

        #endregion

        #region Master File

        public DataTable GetRateFile()
        {
            return _ac.TblReadDataSQL("usp_CNY_Apparel_GetRate", null);
        }

        public DataTable LoadImageLogo()
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@CompanyCode", SqlDbType.NVarChar) { Value = DeclareSystem.SysCompanyCode };
            return _ac.TblReadDataSP("sp_PO_LoadImagePrint", arrPara);
        }

        #endregion
    }
}
