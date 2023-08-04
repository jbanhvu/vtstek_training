using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_BaseSys;
using CNY_BaseSys.Common;

namespace CNY_WH.Info
{
    class InfoWh00001
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        public DataTable LoadGvStockDocumentList00101(string sUser, string sType, string sWhere)
        {
            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@User", SqlDbType.NVarChar) { Value = sUser };
            arrpara[1] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[2] = new SqlParameter("@Where", SqlDbType.NVarChar) { Value = sWhere };
            return _ac.TblReadDataSP("usp_CNY_00101", arrpara);
        }

        #region Stock Document Generate

        public DataTable GenerateStkVoucherfrReq00102(string sType, string sPk
            , string sNo, long lfwh, long ltwh, int iShowAll)
        {
            var arrpara = new SqlParameter[6];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@sPk", SqlDbType.NVarChar) { Value = sPk };
            arrpara[2] = new SqlParameter("@sNo", SqlDbType.NVarChar) { Value = sNo };
            arrpara[3] = new SqlParameter("@FWH", SqlDbType.BigInt) { Value = lfwh };
            arrpara[4] = new SqlParameter("@TWH", SqlDbType.BigInt) { Value = ltwh };
            arrpara[5] = new SqlParameter("@iShow", SqlDbType.Int) { Value = iShowAll };

            return _ac.TblReadDataSP("usp_CNY_00102", arrpara);
        }

        public DataTable GenerateStkVoucherRecPo00102R(string sType, string sPk
            , string sNo, long lfwh, long ltwh, int iShowAll)
        {
            var arrpara = new SqlParameter[6];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@sPk", SqlDbType.NVarChar) { Value = sPk };
            arrpara[2] = new SqlParameter("@sNo", SqlDbType.NVarChar) { Value = sNo };
            arrpara[3] = new SqlParameter("@FWH", SqlDbType.BigInt) { Value = lfwh };
            arrpara[4] = new SqlParameter("@TWH", SqlDbType.BigInt) { Value = ltwh };
            arrpara[5] = new SqlParameter("@iShow", SqlDbType.Int) { Value = iShowAll };

            return _ac.TblReadDataSP("usp_CNY_00102R", arrpara);
        }

        public DataTable GenerateStkVoucherRecSo00102F(string sType, string sPk
            , string sNo, long lfwh, long ltwh, int iShowAll)
        {
            var arrpara = new SqlParameter[6];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@sPk", SqlDbType.NVarChar) { Value = sPk };
            arrpara[2] = new SqlParameter("@sNo", SqlDbType.NVarChar) { Value = sNo };
            arrpara[3] = new SqlParameter("@FWH", SqlDbType.BigInt) { Value = lfwh };
            arrpara[4] = new SqlParameter("@TWH", SqlDbType.BigInt) { Value = ltwh };
            arrpara[5] = new SqlParameter("@iShow", SqlDbType.Int) { Value = iShowAll };

            return _ac.TblReadDataSP("usp_CNY_00102F", arrpara);
        }

        public DataTable GenerateStkVoucherIssMo00103R(string sType, string sPk
            , string sNo, long lfwh, long ltwh, int iShowAll)
        {
            var arrpara = new SqlParameter[6];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@sPk", SqlDbType.NVarChar) { Value = sPk };
            arrpara[2] = new SqlParameter("@sNo", SqlDbType.NVarChar) { Value = sNo };
            arrpara[3] = new SqlParameter("@FWH", SqlDbType.BigInt) { Value = lfwh };
            arrpara[4] = new SqlParameter("@TWH", SqlDbType.BigInt) { Value = ltwh };
            arrpara[5] = new SqlParameter("@iShow", SqlDbType.Int) { Value = iShowAll };

            return _ac.TblReadDataSP("usp_CNY_00103R", arrpara);
        }
        public DataTable GenerateStkVoucherIssSo00103F(string sType, string sPk
            , string sNo, long lfwh, long ltwh, int iShowAll)
        {
            var arrpara = new SqlParameter[6];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@sPk", SqlDbType.NVarChar) { Value = sPk };
            arrpara[2] = new SqlParameter("@sNo", SqlDbType.NVarChar) { Value = sNo };
            arrpara[3] = new SqlParameter("@FWH", SqlDbType.BigInt) { Value = lfwh };
            arrpara[4] = new SqlParameter("@TWH", SqlDbType.BigInt) { Value = ltwh };
            arrpara[5] = new SqlParameter("@iShow", SqlDbType.Int) { Value = iShowAll };

            return _ac.TblReadDataSP("usp_CNY_00103F", arrpara);
        }

        public DataTable GenerateStkVoucherfrReqFg00104(string sType, string sPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@sPk", SqlDbType.NVarChar) { Value = sPk };
            return _ac.TblReadDataSP("usp_CNY_00104", arrpara);
        }

        #endregion

        #region Load Batch Info
        
        public DataTable LoadBatchInfo00105(string sCode, string sType, string sWh
            , string sBatch, DataTable dtSelQty, string sCond)
        {
            var arrpara = new SqlParameter[7];
            arrpara[0] = new SqlParameter("@Comp", SqlDbType.NVarChar) { Value = DeclareSystem.SysCompanyCode };
            arrpara[1] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[2] = new SqlParameter("@sCode", SqlDbType.NVarChar) { Value = sCode };
            arrpara[3] = new SqlParameter("@sWh", SqlDbType.NVarChar) { Value = sWh };
            arrpara[4] = new SqlParameter("@sBatch", SqlDbType.NVarChar) { Value = sBatch };
            arrpara[5] = new SqlParameter("@TypeCNY00100PK", SqlDbType.Structured) {Value = dtSelQty};
            arrpara[6] = new SqlParameter("@Cond", SqlDbType.NVarChar) { Value = sCond };

            return _ac.TblReadDataSP("usp_CNY_00105", arrpara);
        }

        public DataTable LoadBatchInfo00105A(long lCny00104Pk, long lTdg00001Pk
            , long lCnymf004Pk, string sBatch)
        {
            var arrpara = new SqlParameter[5];
            arrpara[0] = new SqlParameter("@Comp", SqlDbType.NVarChar) { Value = DeclareSystem.SysCompanyCode };
            arrpara[1] = new SqlParameter("@CNY00104PK", SqlDbType.NVarChar) { Value = lCny00104Pk };
            arrpara[2] = new SqlParameter("@CODE_TDG00001PK", SqlDbType.NVarChar) { Value = lTdg00001Pk };
            arrpara[3] = new SqlParameter("@WH_CNYMF004PK", SqlDbType.NVarChar) { Value = lCnymf004Pk };
            arrpara[4] = new SqlParameter("@sBatch", SqlDbType.NVarChar) { Value = sBatch };
           
            return _ac.TblReadDataSP("usp_CNY_00105A", arrpara);
        }

        #endregion

        #region Save StkDoc

        public bool InsertHeader_CNY00104_00106(Int64 iPk, Int64 iCNYMF026PK, string s001, DateTime d002
            , string s003, Int64 i004, Int64 i005, string s006, DateTime d007, string s008, DateTime d009
            , string s010, string s011, int i012, Int64 iCnyMf004Pks, Int64 iCnyMf004Pkd, string sComp)
        {
            var arrpara = new SqlParameter[17];

            arrpara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = iPk };
            arrpara[1] = new SqlParameter("@CNYMF026PK", SqlDbType.BigInt) { Value = iCNYMF026PK };
            arrpara[2] = new SqlParameter("@CNY001", SqlDbType.NVarChar) { Value = s001 };
            arrpara[3] = new SqlParameter("@CNY002", SqlDbType.DateTime) { Value = d002 };
            arrpara[4] = new SqlParameter("@CNY003", SqlDbType.NVarChar) { Value = s003 };
            arrpara[5] = new SqlParameter("@CNY004", SqlDbType.BigInt) { Value = i004 };
            arrpara[6] = new SqlParameter("@CNY005", SqlDbType.BigInt) { Value = i005 };
            arrpara[7] = new SqlParameter("@CNY006", SqlDbType.NVarChar) { Value = s006 };
            arrpara[8] = new SqlParameter("@CNY007", SqlDbType.DateTime) { Value = d007 };
            arrpara[9] = new SqlParameter("@CNY008", SqlDbType.NVarChar) { Value = s008 };
            arrpara[10] = new SqlParameter("@CNY009", SqlDbType.DateTime) { Value = d009 };
            arrpara[11] = new SqlParameter("@CNY010", SqlDbType.NVarChar) { Value = s010 };
            arrpara[12] = new SqlParameter("@CNY011", SqlDbType.NVarChar) { Value = s011 };
            arrpara[13] = new SqlParameter("@CNY012", SqlDbType.Int) { Value = i012 };
            arrpara[14] = new SqlParameter("@CNYMF004PKS", SqlDbType.BigInt) { Value = iCnyMf004Pks };
            arrpara[15] = new SqlParameter("@CNYMF004PKD", SqlDbType.BigInt) { Value = iCnyMf004Pkd };
            arrpara[16] = new SqlParameter("@Comp", SqlDbType.NVarChar) { Value = sComp };

            return _ac.BolExcuteSP("usp_CNY_00106", arrpara);
        }

        public bool InsertDetails_CNY00105_00107(Int64 iPk, Int64 iCny00104Pk, Int64 iTdg00001Pk
            , Int64 iCny00102Pk, Int64 iCnyMf004Pk, DateTime d001, string s002, double l003, double l004
            , string s005, string s006, decimal d007, int i008, double l009, double l010, string s011
            , string s012, Int64 iCny00105Pk, Int64 iCny00105APk)
        {
            var arrpara = new SqlParameter[20];

            arrpara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = iPk };
            arrpara[1] = new SqlParameter("@CompCode", SqlDbType.NVarChar) { Value = DeclareSystem.SysCompanyCode };
            arrpara[2] = new SqlParameter("@CNY00104PK", SqlDbType.BigInt) { Value = iCny00104Pk };
            arrpara[3] = new SqlParameter("@CNY00105PK", SqlDbType.BigInt) { Value = iCny00105Pk };
            arrpara[4] = new SqlParameter("@CNY00105APK", SqlDbType.BigInt) { Value = iCny00105APk };
            arrpara[5] = new SqlParameter("@TDG00001PK", SqlDbType.BigInt) { Value = iTdg00001Pk };
            arrpara[6] = new SqlParameter("@CNY00102PK", SqlDbType.BigInt) { Value = iCny00102Pk };
            arrpara[7] = new SqlParameter("@CNYMF004PK", SqlDbType.BigInt) { Value = iCnyMf004Pk };
            arrpara[8] = new SqlParameter("@CNY001", SqlDbType.DateTime) { Value = d001 };
            arrpara[9] = new SqlParameter("@CNY002", SqlDbType.NVarChar) { Value = s002 };
            arrpara[10] = new SqlParameter("@CNY003", SqlDbType.Decimal) { Value = l003 };
            arrpara[11] = new SqlParameter("@CNY004", SqlDbType.Decimal) { Value = l004 };
            arrpara[12] = new SqlParameter("@CNY005", SqlDbType.NVarChar) { Value = s005 };
            arrpara[13] = new SqlParameter("@CNY006", SqlDbType.NVarChar) { Value = s006 };
            arrpara[14] = new SqlParameter("@CNY007", SqlDbType.Decimal) { Value = d007 };
            arrpara[15] = new SqlParameter("@CNY008", SqlDbType.Int) { Value = i008 };
            arrpara[16] = new SqlParameter("@CNY009", SqlDbType.Decimal) { Value = l009 };
            arrpara[17] = new SqlParameter("@CNY010", SqlDbType.Decimal) { Value = l010 };
            arrpara[18] = new SqlParameter("@CNY011", SqlDbType.NVarChar) { Value = s011 };
            arrpara[19] = new SqlParameter("@CNY012", SqlDbType.NVarChar) { Value = s012 };

            return _ac.BolExcuteSP("usp_CNY_00107", arrpara);
        }

        public bool InsertAttributes_CNY00106_00108(Int64 iPk, Int64 iCNY00105PK, Int64 iCNY00008PK
            , string s001, string s002)
        {
            var arrpara = new SqlParameter[5];

            arrpara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = iPk };
            arrpara[1] = new SqlParameter("@CNY00105PK", SqlDbType.BigInt) { Value = iCNY00105PK };
            arrpara[2] = new SqlParameter("@CNY00008PK", SqlDbType.BigInt) { Value = iCNY00008PK };
            arrpara[3] = new SqlParameter("@CNY001", SqlDbType.NVarChar) { Value = s001 };
            arrpara[4] = new SqlParameter("@CNY002", SqlDbType.NVarChar) { Value = s002 };

            return _ac.BolExcuteSP("usp_CNY_00108", arrpara);
        }

        public bool DeleteDetails_CNY1056_00109(string sPkl)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Pkl", SqlDbType.NVarChar) { Value = sPkl };

            return _ac.BolExcuteSP("usp_CNY_00109", arrpara);
        }

        public bool InsertBatchDetails_CNY00100_00157(Int64 iPk, Int64 iTdg00001Pk, Int64 iCnymf004Pk
            , string sCny00050Pk, Int64 i001, double l002, double l003, double l005, string sComp)
        {
            var arrpara = new SqlParameter[9];

            arrpara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = iPk };
            arrpara[1] = new SqlParameter("@TDG00001PK", SqlDbType.BigInt) { Value = iTdg00001Pk };
            arrpara[2] = new SqlParameter("@CNYMF004PK", SqlDbType.BigInt) { Value = iCnymf004Pk };
            arrpara[3] = new SqlParameter("@CNY00050PK", SqlDbType.NVarChar) { Value = sCny00050Pk };
            arrpara[4] = new SqlParameter("@CNY001", SqlDbType.BigInt) { Value = i001 };
            arrpara[5] = new SqlParameter("@CNY002", SqlDbType.Decimal) { Value = l002 };
            arrpara[6] = new SqlParameter("@CNY003", SqlDbType.Decimal) { Value = l003 };
            arrpara[7] = new SqlParameter("@CNY005", SqlDbType.Decimal) { Value = l005 };
            arrpara[8] = new SqlParameter("@CompCode", SqlDbType.NVarChar) { Value = sComp };

            return _ac.BolExcuteSP("usp_CNY_00157", arrpara);
        }

        public bool InsertBatchAttributes_CNY00100B_00158(Int64 iCny00100Pk, Int64 iCny00008Pk, string s001, string s002)
        {
            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@CNY00100PK", SqlDbType.BigInt) { Value = iCny00100Pk };
            arrpara[1] = new SqlParameter("@CNY00008PK", SqlDbType.BigInt) { Value = iCny00008Pk };
            arrpara[2] = new SqlParameter("@CNY001", SqlDbType.NVarChar) { Value = s001 };
            arrpara[3] = new SqlParameter("@CNY002", SqlDbType.NVarChar) { Value = s002 };

            return _ac.BolExcuteSP("usp_CNY_00158", arrpara);
        }

        public bool UpdateBalanceStockQty_CNY00100BC_App_00157(Int64 iCny00104Pk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00104PK", SqlDbType.BigInt) { Value = iCny00104Pk };

            return _ac.BolExcuteSP("usp_CNY_Apparel_00157", arrpara);
        }

        public bool InsertCarton_CNY00105A_App_00107(Int64 iPk, Int64 iCny00104Pk
            , string s001, string s002, string s003, DateTime t004, string s005, decimal m006)
        {
            var arrpara = new SqlParameter[8];

            arrpara[0] = new SqlParameter("@PK", SqlDbType.BigInt) { Value = iPk };
            arrpara[1] = new SqlParameter("@CNY00104PK", SqlDbType.BigInt) { Value = iCny00104Pk };
            arrpara[2] = new SqlParameter("@CNY001", SqlDbType.NVarChar) { Value = s001 };
            arrpara[3] = new SqlParameter("@CNY002", SqlDbType.NVarChar) { Value = s002 };
            arrpara[4] = new SqlParameter("@CNY003", SqlDbType.NVarChar) { Value = s003 };
            arrpara[5] = new SqlParameter("@CNY004", SqlDbType.DateTime) { Value = t004 };
            arrpara[6] = new SqlParameter("@CNY005", SqlDbType.NVarChar) { Value = s005 };
            arrpara[7] = new SqlParameter("@CNY006", SqlDbType.Decimal) { Value = m006 };

            return _ac.BolExcuteSP("usp_CNY_Apparel_00107", arrpara);
        }

        #endregion

        #region Load StkDoc after Saving

        public DataTable LoadGvStockDocDetails00110(string sType, long lPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = lPk };
            return _ac.TblReadDataSP("usp_CNY_00110", arrpara);
        }

        public DataTable LoadGvStockDocDetails00110R(string sType, long lPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = lPk };
            return _ac.TblReadDataSP("usp_CNY_00110R", arrpara);
        }

        public DataTable LoadGvStockDocDetails00110F(string sType, long lPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = lPk };
            return _ac.TblReadDataSP("usp_CNY_00110F", arrpara);
        }

        #endregion

        #region Import iScala

        public DataTable LoadGvImportiScala00111(string sType, string sPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.NVarChar) { Value = sPk };
            return _ac.TblReadDataSP("usp_CNY_00111", arrpara);
        }

        public DataTable LoadGvImportiScala00111R(string sType, string sPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.NVarChar) { Value = sPk };
            return _ac.TblReadDataSP("usp_CNY_00111R", arrpara);
        }

        public DataTable LoadGvImportiScala00111G(string sType, string sPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.NVarChar) { Value = sPk };
            return _ac.TblReadDataSP("usp_CNY_00111G", arrpara);
        }

        public DataTable LoadGvImportiScala00111F(string sType, string sPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.NVarChar) { Value = sPk };
            return _ac.TblReadDataSP("usp_CNY_00111F", arrpara);
        }

        public DataTable LoadGvImportiScala00111I(string sType, string sPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.NVarChar) { Value = sPk };
            return _ac.TblReadDataSP("usp_CNY_00111I", arrpara);
        }

        public DataTable LoadGvImportiScala00111I01F(string sType, string sPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.NVarChar) { Value = sPk };
            return _ac.TblReadDataSP("usp_CNY_00111I01F", arrpara);
        }

        public DataTable LoadGvImportiScala00111I01R(string sType, string sPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.NVarChar) { Value = sPk };
            return _ac.TblReadDataSP("usp_CNY_00111I01R", arrpara);
        }

        public DataTable LoadGvImportiScala00111I06(string sType, string sPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.NVarChar) { Value = sPk };
            return _ac.TblReadDataSP("usp_CNY_00111I06", arrpara);
        }

        public DataTable LoadGvImportiScala00111I09(string sType, string sPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.NVarChar) { Value = sPk };
            return _ac.TblReadDataSP("usp_CNY_00111I09", arrpara);
        }

        #endregion

        #region iScala Transaction-File Path

        public DataTable UpdateCny00104FilePathIns108(string sPk, string sFileName)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PK", SqlDbType.NVarChar) { Value = sPk };
            arrpara[1] = new SqlParameter("@FileName", SqlDbType.NVarChar) { Value = sFileName };
            return _ac.TblReadDataSP("usp_CNYG_00151", arrpara);
        }

        #endregion

        public DataTable GeneratetblTempBatchInfo00136(string sCode, string sWh, string sBatch, string sBin)
        {
            // load thong tin batch sau==>> chinh nha
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Pk", SqlDbType.NVarChar) { Value = sCode };
            return _ac.TblReadDataSP("usp_CNY_00111", arrpara);
        }

        #region Return PO Status

        public bool UpdateGrninPoStatus_CNY061_00165(string sPk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Pk", SqlDbType.NVarChar) { Value = sPk };

            return _ac.BolExcuteSP("usp_CNY_00165", arrpara);
        }

        #endregion

        #region Apparel

        public DataTable GenerateStkVoucherIssSo_App_00103F(string sType, string sPk
            , string sNo, long lfwh, long ltwh, int iShowAll)
        {
            var arrpara = new SqlParameter[6];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@sPk", SqlDbType.NVarChar) { Value = sPk };
            arrpara[2] = new SqlParameter("@sNo", SqlDbType.NVarChar) { Value = sNo };
            arrpara[3] = new SqlParameter("@FWH", SqlDbType.BigInt) { Value = lfwh };
            arrpara[4] = new SqlParameter("@TWH", SqlDbType.BigInt) { Value = ltwh };
            arrpara[5] = new SqlParameter("@iShow", SqlDbType.Int) { Value = iShowAll };

            return _ac.TblReadDataSP("usp_CNY_Apparel_00103F", arrpara);
        }

        public DataTable LoadBatchInfo_App_00105(string sCode, string sType, string sWh
            , string sBatch, DataTable dtSelQty, string sCond)
        {
            var arrpara = new SqlParameter[7];
            arrpara[0] = new SqlParameter("@Comp", SqlDbType.NVarChar) { Value = DeclareSystem.SysCompanyCode };
            arrpara[1] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[2] = new SqlParameter("@sCode", SqlDbType.NVarChar) { Value = sCode };
            arrpara[3] = new SqlParameter("@sWh", SqlDbType.NVarChar) { Value = sWh };
            arrpara[4] = new SqlParameter("@sBatch", SqlDbType.NVarChar) { Value = sBatch };
            arrpara[5] = new SqlParameter("@TypeCnyAppWh001", SqlDbType.Structured) { Value = dtSelQty };
            arrpara[6] = new SqlParameter("@Cond", SqlDbType.NVarChar) { Value = sCond };

            return _ac.TblReadDataSP("usp_CNY_Apparel_00105", arrpara);
        }

        public DataTable LoadGvStockDocDetails_App_00110(string sType, long lPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = lPk };
            return _ac.TblReadDataSP("usp_CNY_Apparel_00110", arrpara);
        }

        public DataTable LoadGvStockDocDetails_App_00110F(string sType, long lPk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Type", SqlDbType.NVarChar) { Value = sType };
            arrpara[1] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = lPk };
            return _ac.TblReadDataSP("usp_CNY_Apparel_00110F", arrpara);
        }

        public DataSet LoadBatchInfo_App_00105A(long lCny00104Pk)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Comp", SqlDbType.NVarChar) { Value = DeclareSystem.SysCompanyCode };
            arrpara[1] = new SqlParameter("@CNY00104PK", SqlDbType.NVarChar) { Value = lCny00104Pk };

            return _ac.DtsReadDataSP("usp_CNY_Apparel_00105A", arrpara);
        }

        public DataTable LoadLocationInfo()
        {
            return _ac.TblReadDataSP("usp_CNY_Apparel_GetLocationInfo", null);
        }

        public DataTable DtCheckBalanceQtylessthanIssuedQty_App_00002(long lPk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = lPk };

            return _ac.TblReadDataSP("usp_CNY_Apparel_00002", arrpara);
        }

        public bool DeleteDetails_App_CNY1056_00109(long lPk, int iDelVoucher)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Pk", SqlDbType.BigInt) { Value = lPk };
            arrpara[1] = new SqlParameter("@DeleteVoucher", SqlDbType.Int) { Value = iDelVoucher };

            return _ac.BolExcuteSP("usp_CNY_Apparel_00109", arrpara);
        }

        #endregion
    }
}
