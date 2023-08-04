using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CNY_BaseSys.Common
{
    public class CodeRuleFieldData
    {
        public string TableFieldSql { get; set; }
        public string StringValue { get; set; }
    }
    public class SystemGetCodeRule
    {


        #region "Property"

        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);
        private Int64 _pkHeader = 0;

        public Int64 PkHeader
        {
            get { return this._pkHeader; }
            set
            {
                this._pkHeader = value;
                DataTable dtTemp = RMCode_LoadDefineHeader_Version();
                if (dtTemp.Rows.Count > 0)
                {
                    _versionDefine = ProcessGeneral.GetSafeInt(dtTemp.Rows[0]["Version"]);
                    _itemCode = ProcessGeneral.GetSafeString(dtTemp.Rows[0]["ItemCode"]);
                }
                else
                {
                    _versionDefine = 0;
                    _itemCode = "";
                }

            }
        }


        private string _tableCode = "";

        public string TableCode
        {
            get { return this._tableCode; }
            set
            {
                this._tableCode = value;
            }
        }


        private Int32 _versionDefine = 0;
        private string _itemCode = "";

        private CodeRuleContainData _codeRuleData = new CodeRuleContainData();
        public CodeRuleContainData CodeRuleData
        {
            get { return this._codeRuleData; }
            set
            {
                this._codeRuleData = value;
            }
        }
        private List<CodeRuleFieldData> _arrData = new List<CodeRuleFieldData>();
        public List<CodeRuleFieldData> ArrData
        {
            get { return this._arrData; }
            set
            {
                this._arrData = value;
            }
        }
       
        #endregion


        #region "Contructor"

        public SystemGetCodeRule()
        {

        }

        public SystemGetCodeRule(Int64 pkHeader, string tableCode)
        {
            this._pkHeader = pkHeader;
            this._tableCode = tableCode;
            DataTable dtTemp = RMCode_LoadDefineHeader_Version();
            if (dtTemp.Rows.Count > 0)
            {
                _versionDefine = ProcessGeneral.GetSafeInt(dtTemp.Rows[0]["Version"]);
                _itemCode =  ProcessGeneral.GetSafeString(dtTemp.Rows[0]["ItemCode"]);
            }
            else
            {
                _versionDefine = 0;
                _itemCode = "";
            }
            

          
        }

        #endregion

        #region "Create Code"

       

        public void CreateCodeString()
        {
         //sdasd
            DataTable dtEmp = RMCode_LoadDefineDetail();
            var qD = dtEmp.AsEnumerable().Where(p => p.Field<Int32>("InputType") == ConstSystem.SysInputTypeDateTime).ToList();

            if (qD.Any())
            {
                foreach (DataRow dr in qD)
                {
                    dr["Value"] = ExcuteSqlTextStr(ProcessGeneral.GetSafeString(dr["SqlQueryReturnData"]));
                }
                dtEmp.AcceptChanges();
            }
            int cB = _arrData.Count;
            if (cB > 0)
            {
                
                Dictionary<Int32,List<Int32>> dicInputType = new Dictionary<int, List<int>>();
                dicInputType.Add(0, new List<int> { ConstSystem.SysInputTypeGetField });
                dicInputType.Add(1, new List<int> { ConstSystem.SysInputTypeAllowInput, ConstSystem.SysInputTypeSelectedSource });

                foreach (var item in dicInputType)
                {
                    List<int> lType = item.Value;
                    var qN = dtEmp.AsEnumerable().Where(p => lType.Any(t => t == p.Field<Int32>("InputType"))).ToList();
                    bool isGetField = lType.Any(p => p == ConstSystem.SysInputTypeGetField);
                    List<CodeRuleFieldData> arrTemp;
                    if (isGetField)
                    {
                        qN = qN.AsEnumerable().Where(p => ProcessGeneral.GetSafeString(p["GetFieldCode"]) != "").ToList();
                        arrTemp = _arrData.Where(p => ProcessGeneral.GetSafeString(p.TableFieldSql) != "").ToList();
                    }
                    else
                    {
                        arrTemp = _arrData.Where(p => ProcessGeneral.GetSafeString(p.TableFieldSql) == "").ToList();
                    }




                    int cA = arrTemp.Count;
                    int cN = qN.Count;
                    if (cA > 0 && cN > 0)
                    {
                        if (isGetField)
                        {
                            for (int j = 0; j < cN; j++)
                            {

                                DataRow drA = qN[j];
                                string fieldCode = ProcessGeneral.GetSafeString(drA["GetFieldCode"]);
                                string valueTempA = ProcessGeneral.GetSafeString(arrTemp.Where(p => p.TableFieldSql == fieldCode).Select(p => p.StringValue).First());
                                if(string.IsNullOrEmpty(valueTempA))continue;

                                


                                string valueA = "";
                                Int32 lengthA = ProcessGeneral.GetSafeInt(drA["CharLength"]);
                                Int32 startA = ProcessGeneral.GetSafeInt(drA["StratIndexStrDataSource"]);

                                if (startA <= 1)
                                {
                                    valueA = valueTempA;
                                }
                                else
                                {
                                    int lengthT = valueTempA.Length;
                                    if (lengthT >= startA)
                                    {
                                        int right = lengthT - startA + 1;
                                        valueA = ProcessGeneral.RightString(valueTempA, right);
                                    }

                                }
                                drA["Value"] = valueA.SubStringNew(0, lengthA);
                            }
                        }
                        else
                        {
                            int loop = cA >= cN ? cN : cA;
                            if (loop > 0)
                            {
                                for (int i = 0; i < loop; i++)
                                {
                                    string valueTemp = arrTemp[i].StringValue;
                                    DataRow dr = qN[i];
                                    string value = "";
                                    Int32 length = ProcessGeneral.GetSafeInt(dr["CharLength"]);
                                    Int32 start = ProcessGeneral.GetSafeInt(dr["StratIndexStrDataSource"]);

                                    if (start <= 1)
                                    {
                                        value = valueTemp;
                                    }
                                    else
                                    {
                                        int lengthT = valueTemp.Length;
                                        if (lengthT >= start)
                                        {
                                            int right = lengthT - start + 1;
                                            value = ProcessGeneral.RightString(valueTemp, right);
                                        }

                                    }
                                    dr["Value"] = value.SubStringNew(0, length);
                                }

                            }
                        }

                        dtEmp.AcceptChanges();
                    }
                    



                   
                   
                }

                

            }


            var qA = dtEmp.AsEnumerable().Where(p => p.Field<Int32>("InputType") == ConstSystem.SysInputTypeAutoIncrease).ToList();
            if (qA.Any())
            {
                DataRow drA = qA.First();
                
                string fieldSelected = string.Format("ColDefine_{0:00}", ProcessGeneral.GetSafeInt(drA["ID"]));
                int lenSelected = ProcessGeneral.GetSafeInt(drA["CharLength"]);
               
                string sql = string.Format("SELECT ltrim(rtrim({0})) as {0} FROM dbo.SystemGetCodeRule ", fieldSelected);

                var q3 = dtEmp.AsEnumerable().Where(p => p.Field<Int32>("InputType") != ConstSystem.SysInputTypeAutoIncrease).Select(p => string.Format("ColDefine_{0:00} = '{1}{2}{3}'",
                    ProcessGeneral.GetSafeInt(p["ID"]), ProcessGeneral.GetSafeString(p["S"]), ProcessGeneral.GetSafeString(p["Value"]), ProcessGeneral.GetSafeString(p["E"]))).ToArray();
                string sqlWhere = "";
                if (q3.Any())
                {
                    sqlWhere = string.Format(" Where TableCode ='{0}' and ItemCode = '{1}' and {2} and len(ltrim(rtrim({3})))={4}", _tableCode, _itemCode, string.Join(" And ", q3), fieldSelected, lenSelected);
                }
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sql = string.Format("{0} {1}", sql, sqlWhere);
                }
                else
                {
                    sql = string.Format("{0} Where TableCode ='{1}' and ItemCode = '{2}'  and len(ltrim(rtrim({3})))={4} ", sql, _tableCode, _itemCode, fieldSelected, lenSelected);
                }

                DataTable dtIncrease = TblExcuteSqlText(sql);
                if (dtIncrease.Rows.Count <= 0)
                {
                    string s = string.Format("{0}1", ProcessGeneral.SetStringDuplicate(lenSelected, "0"));
                    s = ProcessGeneral.RightString(s, lenSelected);
                    drA["Value"] = s;
                }
                else
                {
                    var q4 = dtIncrease.AsEnumerable().Where(p => !ProcessGeneral.IsNumber(p[0].ToString())).Select(p => p[0].ToString()).ToList();
                    if (q4.Any())
                    {
                        string s1 = q4.Max();
                        sql = string.Format("SELECT  dbo.ufn_GetNextNumberAndStringFinal ('{0}',{1}) as Code", s1, lenSelected);
                        s1 = ProcessGeneral.GetSafeString(TblExcuteSqlText(sql).Rows[0][0]);
                        drA["Value"] = s1;
                       
                    }
                    else
                    {
                        int q5 = dtIncrease.AsEnumerable().Select(p => ProcessGeneral.GetSafeInt(p[0])).Max();
                        Int64 maxQ5 = lenSelected.GetMaxNumber();
                        if (q5 == maxQ5)
                        {
                            sql = string.Format("SELECT  dbo.ufn_GetNextNumberAndStringFinal ('{0}',{1}) as Code", q5, lenSelected);
                            drA["Value"] = ProcessGeneral.GetSafeString(TblExcuteSqlText(sql).Rows[0][0]);
                        }
                        else
                        {

                            string s2 = string.Format("{0}{1}", ProcessGeneral.SetStringDuplicate(lenSelected, "0"), q5+1);
                            s2 = ProcessGeneral.RightString(s2, lenSelected);
                            drA["Value"] = s2;
                        }



                    }
                 
                }

                dtEmp.AcceptChanges();


            }
            string code = GetCodeFinalDisplay(dtEmp);
            _codeRuleData.DtCode = dtEmp;
            _codeRuleData.StrCode = code;

        }



        private void GetAutoIncreaseCodeWhenSave()
        {

            DataTable dtEmp = _codeRuleData.DtCode;
          
            var qA = dtEmp.AsEnumerable().Where(p => p.Field<Int32>("InputType") == ConstSystem.SysInputTypeAutoIncrease).ToList();
            if (qA.Any())
            {
                DataRow drA = qA.First();

                string fieldSelected = string.Format("ColDefine_{0:00}", ProcessGeneral.GetSafeInt(drA["ID"]));
                int lenSelected = ProcessGeneral.GetSafeInt(drA["CharLength"]);

                string sql = string.Format("SELECT ltrim(rtrim({0})) as {0} FROM dbo.SystemGetCodeRule ", fieldSelected);

                var q3 = dtEmp.AsEnumerable().Where(p => p.Field<Int32>("InputType") != ConstSystem.SysInputTypeAutoIncrease).Select(p => string.Format("ColDefine_{0:00} = '{1}{2}{3}'",
                    ProcessGeneral.GetSafeInt(p["ID"]), ProcessGeneral.GetSafeString(p["S"]), ProcessGeneral.GetSafeString(p["Value"]), ProcessGeneral.GetSafeString(p["E"]))).ToArray();
                string sqlWhere = "";
                if (q3.Any())
                {
                    sqlWhere = string.Format(" Where TableCode ='{0}' and ItemCode = '{1}'  and {2} and len(ltrim(rtrim({3})))={4}", _tableCode, _itemCode, string.Join(" And ", q3), fieldSelected, lenSelected);
                }
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sql = string.Format("{0} {1}", sql, sqlWhere);
                }
                else
                {
                    sql = string.Format("{0} Where TableCode ='{1}' and ItemCode = '{2}'  and len(ltrim(rtrim({3})))={4} ", sql, _tableCode, _itemCode, fieldSelected, lenSelected);
                }

                DataTable dtIncrease = TblExcuteSqlText(sql);
                if (dtIncrease.Rows.Count <= 0)
                {
                    string s = string.Format("{0}1", ProcessGeneral.SetStringDuplicate(lenSelected, "0"));
                    s = ProcessGeneral.RightString(s, lenSelected);
                    drA["Value"] = s;
                }
                else
                {
                    var q4 = dtIncrease.AsEnumerable().Where(p => !ProcessGeneral.IsNumber(p[0].ToString())).Select(p => p[0].ToString()).ToList();
                    if (q4.Any())
                    {
                        string s1 = q4.Max();
                        sql = string.Format("SELECT  dbo.ufn_GetNextNumberAndStringFinal ('{0}',{1}) as Code", s1, lenSelected);
                        s1 = ProcessGeneral.GetSafeString(TblExcuteSqlText(sql).Rows[0][0]);
                        drA["Value"] = s1;

                    }
                    else
                    {
                        int q5 = dtIncrease.AsEnumerable().Select(p => ProcessGeneral.GetSafeInt(p[0])).Max();
                        Int64 maxQ5 = lenSelected.GetMaxNumber();
                        if (q5 == maxQ5)
                        {
                            sql = string.Format("SELECT  dbo.ufn_GetNextNumberAndStringFinal ('{0}',{1}) as Code", q5, lenSelected);
                            drA["Value"] = ProcessGeneral.GetSafeString(TblExcuteSqlText(sql).Rows[0][0]);
                        }
                        else
                        {

                            string s2 = string.Format("{0}{1}", ProcessGeneral.SetStringDuplicate(lenSelected, "0"), q5 + 1);
                            s2 = ProcessGeneral.RightString(s2, lenSelected);
                            drA["Value"] = s2;
                        }



                    }

                }

                dtEmp.AcceptChanges();


            }
            string code = GetCodeFinalDisplay(dtEmp);
            _codeRuleData.DtCode = dtEmp;
            _codeRuleData.StrCode = code;

        }


        private string GetCodeFinalDisplay(DataTable dtEmp)
        {


            var q1 = dtEmp.AsEnumerable().Select(p => string.Format("{0}{1}{2}", ProcessGeneral.GetSafeString(p["S"]),
                ProcessGeneral.GetSafeString(p["Value"]), ProcessGeneral.GetSafeString(p["E"])).Trim()).Where(p => !string.IsNullOrEmpty(p)).ToArray();
            string s = "";
            if (q1.Length > 0) s = string.Join("", q1);
            return s;
        }
        #endregion


        #region  "Save Data"

        public CodeRuleReturnSave SaveCodeData()
        {
            CodeRuleReturnSave rs = new CodeRuleReturnSave();
            if (string.IsNullOrEmpty(_codeRuleData.StrCode))
            {
                XtraMessageBox.Show("Code could not blank!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rs.IsSusccess = false;
                rs.StrCode = "";
                return rs;
            }




            List<CodeRuleSaveData> lSave = StandardGridBeforSave();

            bool contiCreateCode = lSave.Any(p => p.InputType == ConstSystem.SysInputTypeAutoIncrease);
            while (true)
            {
                string code = _codeRuleData.StrCode;
                bool existsCode = IsCodeExitsInSystem(code);
                if (!existsCode)
                    break;
                if (!contiCreateCode)
                {
                    XtraMessageBox.Show(string.Format("Code {0} exists in system!", code), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rs.IsSusccess = false;
                    rs.StrCode = "";
                    return rs;
                }
                DialogResult dlResult = XtraMessageBox.Show(string.Format("Code {0} exists in system! \n Do you want the system automatically to generate code for function saving data continue?",
                    code), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlResult != DialogResult.Yes)
                {
                    rs.IsSusccess = false;
                    rs.StrCode = "";
                    return rs;
                }
                GetAutoIncreaseCodeWhenSave();
                lSave = StandardGridBeforSave();
            }
            DataTable dtSave = GetDataBeforSave(lSave);
            rs.IsSusccess = CodeRule_UpdateData(dtSave);
            rs.StrCode = _codeRuleData.StrCode;
            return rs;

        }

        private DataTable TempTableSave()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ColDefine_01", typeof(string));
            dt.Columns.Add("ColDefine_02", typeof(string));
            dt.Columns.Add("ColDefine_03", typeof(string));
            dt.Columns.Add("ColDefine_04", typeof(string));
            dt.Columns.Add("ColDefine_05", typeof(string));
            dt.Columns.Add("ColDefine_06", typeof(string));
            dt.Columns.Add("ColDefine_07", typeof(string));
            dt.Columns.Add("ColDefine_08", typeof(string));
            dt.Columns.Add("ColDefine_09", typeof(string));
            dt.Columns.Add("ColDefine_10", typeof(string));
            dt.Columns.Add("RMCode_001", typeof(string));
            dt.Columns.Add("PKHeader", typeof(Int64));
            dt.Columns.Add("VersionDefine", typeof(Int32));
            dt.Columns.Add("TableCode", typeof(string));
            dt.Columns.Add("ItemCode", typeof(string));

            return dt;
        }

        private List<CodeRuleSaveData> StandardGridBeforSave()
        {
            DataTable dtEmp = _codeRuleData.DtCode;
            var lSave = dtEmp.AsEnumerable().Select((p, index) => new CodeRuleSaveData
            {
                RowHandle = index,
                ID = ProcessGeneral.GetSafeInt(p["ID"]),
                Value = string.Format("{0}{1}{2}", ProcessGeneral.GetSafeString(p["S"]), ProcessGeneral.GetSafeString(p["Value"]), ProcessGeneral.GetSafeString(p["E"])).Trim(),
                CharLength = ProcessGeneral.GetSafeInt(p["CharLength"]) + ProcessGeneral.GetSafeString(p["S"]).Length + ProcessGeneral.GetSafeString(p["E"]).Length,
                InputType = ProcessGeneral.GetSafeInt(p["InputType"]),
                ColName = string.Format("ColDefine_{0:00}", ProcessGeneral.GetSafeInt(p["ID"])),
            }).ToList();
            return lSave;
        }


        private DataTable GetDataBeforSave(List<CodeRuleSaveData> lSave)
        {
            DataTable dtSave = TempTableSave();
            DataRow drSave = dtSave.NewRow();


            foreach (var item in lSave)
            {
                drSave[item.ColName] = item.Value;
            }

            var q1 = dtSave.Columns.OfType<DataColumn>()
                .Select(p => p.ColumnName)
                .Where(p => p.StartsWith("ColDefine_"))
                .ToList();

            var q2 = q1.Where(p => lSave.All(t => t.ColName.Trim().ToUpper() != p.Trim().ToUpper())).ToList();
            foreach (string colName in q2)
            {
                drSave[colName] = "";
            }



            drSave["RMCode_001"] = _codeRuleData.StrCode;







            drSave["PKHeader"] = _pkHeader;
            drSave["VersionDefine"] = _versionDefine;
            drSave["TableCode"] = _tableCode;
            drSave["ItemCode"] = _itemCode;
            dtSave.Rows.Add(drSave);
            return dtSave;
        }

        #endregion






        #region "Sql Methold"

        private bool IsCodeExitsInSystem(string rmCode)
        {
            string sqlCheck = string.Format("SELECT RMCode_001 FROM dbo.SystemGetCodeRule WHERE LTRIM(RTRIM(RMCode_001))='{0}' and TableCode = '{1}'", rmCode, _tableCode);
            return IsRecordExistExt(sqlCheck);
        }





        private bool CodeRule_UpdateData(DataTable dtIns)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtIns };
            return _ac.BolExcuteSP("usp_CodeRule_insertCode", arrpara);
       
        }


        private bool IsRecordExistExt(string sql)
        {
            return _ac.TblReadDataSQL(sql, null).Rows.Count > 0;
        }
        private DataTable TblExcuteSqlText(string sql)
        {
            return _ac.TblReadDataSQL(sql, null);
        }
        private string ExcuteSqlTextStr(string sql)
        {
            DataTable dt = _ac.TblReadDataSQL(sql, null);
            if (dt.Rows.Count <= 0) return "";
            return ProcessGeneral.GetSafeString(dt.Rows[0][0]);
        }
        private DataTable RMCode_LoadDefineHeader_Version()
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PKHeader", SqlDbType.BigInt) { Value = this._pkHeader };
            return _ac.TblReadDataSP("usp_RMCode_LoadDefineHeader", arrpara);
   
        }
        private DataTable RMCode_LoadDefineDetail()
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PKHeader", SqlDbType.BigInt) { Value = this._pkHeader };
            arrpara[1] = new SqlParameter("@VersionDefine", SqlDbType.Int) { Value = this._versionDefine };
            return _ac.TblReadDataSP("usp_RMCode_LoadDefineDetailF1", arrpara);
        }

        #endregion
    }



    public class CodeRuleReturnSave
    {
        public bool IsSusccess { get; set; }
        public string StrCode { get; set; }
    }


    public class CodeRuleSaveData
    {
        public Int32 RowHandle { get; set; }
        public Int32 ID { get; set; }
        public string Value { get; set; }
        public Int32 CharLength { get; set; }
        public Int32 InputType { get; set; }
        public string ColName { get; set; }
    }
    public class CodeRuleContainData
    {
        private DataTable _dtCode = new DataTable();
        public DataTable DtCode { get { return this._dtCode; } set { this._dtCode = value; } }
        private string _strCode = "";
        public string StrCode { get { return this._strCode; } set { this._strCode = value; } }

        public CodeRuleContainData()
        {
            
        }
        public CodeRuleContainData(string strCode, DataTable dtCode)
        {
            this._strCode = strCode;
            this._dtCode = dtCode;
        }
    }
   
}
