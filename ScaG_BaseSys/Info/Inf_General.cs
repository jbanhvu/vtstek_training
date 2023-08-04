using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;

namespace CNY_BaseSys.Info
{
    public class Inf_General
    {
        readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        public DataTable sp_ColumnInfomation_Select(string TableName)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@TableName", SqlDbType.NVarChar) { Value = TableName };
            return _ac.TblReadDataSP("sp_ColumnInfomation_Select", arrPara);
        }
        public DataSet PrintBoMAmount(Int64 bomHeaderPk)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@BOMHeaderPK", SqlDbType.BigInt) { Value = bomHeaderPk };
            return _ac.DtsReadDataSP("sp_BOM_AmountRpt_L1", arrpara);
        }


        public DataTable Report_LoadDataFinishingFinal(DataTable dt)
        {

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_Report_LoadListFinishingFinal", arrpara);

        }
        public DataTable Report_LoadDataCheckFi(DataTable dt, Int64 cny00012Pk)
        {

            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@CNY00012PK", SqlDbType.BigInt) { Value = cny00012Pk };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_Report_LoadListCheckFi", arrpara);

        }
        public DataTable Report_LoadDataCheckMain(DataTable dt)
        {

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_Report_LoadListCheckMain", arrpara);

        }
        public DataTable Report_LoadDataCheckMain_F1(DataTable dt, Int64 cny00012Pk, bool isCust)
        {

            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@FormatPrQtyDecimal", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[1] = new SqlParameter("@IsCust", SqlDbType.Bit) { Value = isCust };
            arrpara[2] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            DataTable dtNormal = _ac.TblReadDataSP("sp_Report_LoadListCheckMainF1", arrpara);

            arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@CNY00012PK", SqlDbType.BigInt) { Value = cny00012Pk };
            arrpara[1] = new SqlParameter("@FormatPrQtyDecimal", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[2] = new SqlParameter("@IsCust", SqlDbType.Bit) { Value = isCust };
            arrpara[3] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            DataTable dtFi = _ac.TblReadDataSP("sp_Report_LoadListCheckFi_F1", arrpara);
            int fC = dtFi.Rows.Count;
            int fN = dtNormal.Rows.Count;
            if (fC > 0 && fN > 0)
            {
                dtNormal.Merge(dtFi);
                return dtNormal;
            }

            if (fC > 0) return dtFi;
            return dtNormal;




        }
        public DataTable Report_LoadPrPoRpt_F1(DataTable dt)
        {

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_Report_LoadListPRPO_F1", arrpara);

        }
        public DataTable Report_LoadPrPoRpt(DataTable dt)
        {

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_Report_LoadListPRPO", arrpara);

        }
        public DataTable Report_LoadBoMAtt(DataTable dt)
        {

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_Report_LoadAtt", arrpara);

        }

        public DataTable Report_LoadDataCalMain(DataTable dt)
        {

            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@FormatPrQtyDecimal", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_Report_LoadListCalMain", arrpara);

        }

        public DataTable Report_LoadDataCalFi(DataTable dt,Int64 cny00012Pk)
        {

            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@FormatPrQtyDecimal", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[1] = new SqlParameter("@CNY00012PK", SqlDbType.BigInt) { Value = cny00012Pk };
            arrpara[2] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_Report_LoadListCalFi", arrpara);

        }

        public DataTable Report_LoadBoMPrPO_F1(DataTable dtPara, string mainGroup, Int64 cny00012Pk, bool isCust)
        {

            var arrpara = new SqlParameter[4];
            arrpara[0] = new SqlParameter("@FormatPrQtyDecimal", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[1] = new SqlParameter("@MainGroup", SqlDbType.NVarChar) { Value = mainGroup };
            arrpara[2] = new SqlParameter("@IsCust", SqlDbType.Bit) { Value = isCust };
            arrpara[3] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtPara };
            DataTable dt1 = _ac.TblReadDataSP("sp_Report_LoadListRMBOMFINAL_F1", arrpara);
            DataTable dtFinal = new DataTable();
            dtFinal.Columns.Add("RMCode_001", typeof(string));
            dtFinal.Columns.Add("RMDescription_002", typeof(string));
            dtFinal.Columns.Add("Dimenson", typeof(string));
            dtFinal.Columns.Add("CalDemand", typeof(decimal));
            dtFinal.Columns.Add("CalUnit", typeof(string));
            dtFinal.Columns.Add("BoMDemand", typeof(decimal));
            dtFinal.Columns.Add("BOMUnit", typeof(string));
            dtFinal.Columns.Add("PRQty", typeof(decimal));
            dtFinal.Columns.Add("PRUnit", typeof(string));
            dtFinal.Columns.Add("POQty", typeof(decimal));
            dtFinal.Columns.Add("POUnit", typeof(string));
            dtFinal.Columns.Add("TDG00004PK", typeof(Int64));
            if (dt1.Rows.Count > 0)
            {
                DataTable dtBoMDetail = dt1.AsEnumerable().Select(p => new
                {
                    PK = p.Field<Int64>("CNY00016PK"),
                }).Distinct().CopyToDataTableNew();

                DataTable dtAtt = Report_LoadBoMAtt(dtBoMDetail);

                var qDimenson = dtAtt.AsEnumerable().GroupBy(p => new
                {
                    CNY00016PK = p.Field<Int64>("CNY00016PK")
                }).Select(t => new
                {
                    t.Key.CNY00016PK,
                    Dimenson = string.Join(" - ", t.Select(s => new
                    {
                        AttibuteName = s.Field<string>("AttibuteName"),
                        AttibuteValue = s.Field<string>("AttibuteValue"),
                        SortIndex = s.Field<int>("SortIndex"),
                    }).Where(s => !string.IsNullOrEmpty(s.AttibuteName) && !string.IsNullOrEmpty(s.AttibuteValue))
                        .OrderBy(s => s.SortIndex)
                        .Select(s => string.Format("({0} - {1})", s.AttibuteName, s.AttibuteValue)).ToArray()).Trim()
                }).ToList();



                var q1 = dt1.AsEnumerable().GroupJoin(qDimenson,
                    p => new
                    {
                        CNY00016PK = p.Field<Int64>("CNY00016PK")
                    },
                    t => new
                    {
                        t.CNY00016PK
                    }, (p, j) => new { p, j })
                    .SelectMany(@t1 => @t1.j.DefaultIfEmpty(), (@t1, x) => new { @t1, x }) //  .Where(@t1 => @t1.x == null)
                    .Select(@t2 => new
                    {

                        CNY00020PK = @t2.@t1.p.Field<Int64>("CNY00020PK"),
                        TDG00001PK = @t2.@t1.p.Field<Int64>("TDG00001PK"),
                        CNY00016PK = @t2.@t1.p.Field<Int64>("CNY00016PK"),
                        CNY00055PK = @t2.@t1.p.Field<Int64>("CNY00055PK"),
                        RMCode_001 = @t2.@t1.p.Field<string>("RMCode_001"),
                        RMDescription_002 = @t2.@t1.p.Field<string>("RMDescription_002"),
                        CalDemand = @t2.@t1.p.Field<decimal>("CalDemand"),
                        CalUnit = @t2.@t1.p.Field<string>("CalUnit"),
                        BoMDemand = @t2.@t1.p.Field<decimal>("BoMDemand"),
                        BOMUnit = @t2.@t1.p.Field<string>("BOMUnit"),
                        PRQty = @t2.@t1.p.Field<decimal>("PRQty"),
                        PRUnit = @t2.@t1.p.Field<string>("PRUnit"),
                        //POQty = @t2.@t1.p.Field<decimal>("POQty"),
                        POUnit = @t2.@t1.p.Field<string>("POUnit"),
                        Dimenson = @t2.x == null ? "" : @t2.x.Dimenson,
                    }).GroupBy(p => new
                    {
                        p.CNY00020PK,
                        p.TDG00001PK,
                        p.CNY00055PK,
                        p.RMCode_001,
                        p.RMDescription_002,
                        p.Dimenson,
                        //BoMDemand = @t2.@t1.p.Field<decimal>("BoMDemand"),
                        p.BOMUnit,
                        //PRQty = @t2.@t1.p.Field<decimal>("PRQty"),
                        p.PRUnit,
                    //p.POQty,
                    p.POUnit,
                        p.CalUnit,

                    }).Select(t => new
                    {

                        t.Key.CNY00020PK,
                        t.Key.CNY00055PK,
                        t.Key.TDG00001PK,
                        t.Key.RMCode_001,
                        t.Key.RMDescription_002,
                        t.Key.Dimenson,
                        CalDemand = t.Sum(s => s.CalDemand),
                        t.Key.CalUnit,
                        BoMDemand = t.Sum(s => s.BoMDemand),
                        t.Key.BOMUnit,
                        PRQty = t.Sum(s => s.PRQty),
                        t.Key.PRUnit,
                    //t.Key.POQty,
                    t.Key.POUnit,
                    }).ToList();





                List<SOPRPOINFO> qFinal = q1.Where(p => p.CNY00055PK <= 0).GroupBy(p => new
                {
                    p.CNY00020PK,
                    p.RMCode_001,
                    p.RMDescription_002,
                    p.Dimenson,
                    p.BOMUnit,
                    p.PRUnit,
                    p.POUnit,
                    p.CalUnit,
                }).Select(s => new SOPRPOINFO
                {
                    CNY00020PK = s.Key.CNY00020PK,
                    RMCode_001 = s.Key.RMCode_001,
                    RMDescription_002 = s.Key.RMDescription_002,
                    Dimenson = s.Key.Dimenson,
                    CalDemand = s.Sum(t => t.CalDemand),
                    CalUnit = s.Key.CalUnit,
                    BoMDemand = s.Sum(t => t.BoMDemand),
                    BOMUnit = s.Key.BOMUnit,
                    PRQty = s.Sum(t => t.PRQty),
                    PRUnit = s.Key.PRUnit,
                    POQty = 0,
                    POUnit = s.Key.POUnit,

                }).ToList();

                foreach (var itemFinal in qFinal)
                {
                    dtFinal.Rows.Add(itemFinal.RMCode_001, itemFinal.RMDescription_002, itemFinal.Dimenson, itemFinal.CalDemand, itemFinal.CalUnit, itemFinal.BoMDemand, itemFinal.BOMUnit
                        , itemFinal.PRQty, itemFinal.PRUnit, itemFinal.POQty, itemFinal.POUnit,0);
                }
               

                var q2 = q1.Where(p => p.CNY00055PK > 0).ToList();
                if (q2.Any())
                {
                    DataTable dtPo = Report_LoadPrPoRpt_F1(q2.CopyToDataTableNew());
                    foreach (DataRow drPo in dtPo.Rows)
                    {
                        DataRow drFinal = dtFinal.NewRow();
                        drFinal["RMCode_001"] = drPo["RMCode_001"];
                        drFinal["RMDescription_002"] = drPo["RMDescription_002"];
                        drFinal["Dimenson"] = drPo["Dimenson"];
                        drFinal["CalDemand"] = drPo["CalDemand"];
                        drFinal["CalUnit"] = drPo["CalUnit"];
                        drFinal["BoMDemand"] = drPo["BoMDemand"];
                        drFinal["BOMUnit"] = drPo["BOMUnit"];
                        drFinal["PRQty"] = drPo["PRQty"];
                        drFinal["PRUnit"] = drPo["PRUnit"];
                        drFinal["POQty"] = drPo["POQty"];
                        drFinal["POUnit"] = drPo["POUnit"];
                        drFinal["TDG00004PK"] = 0;
                        dtFinal.Rows.Add(drFinal);
                    }
                }
            }


            arrpara = new SqlParameter[5];
            arrpara[0] = new SqlParameter("@FormatPrQtyDecimal", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[1] = new SqlParameter("@CNY00012PK", SqlDbType.BigInt) { Value = cny00012Pk };
            arrpara[2] = new SqlParameter("@MainGroup", SqlDbType.NVarChar) { Value = mainGroup };
            arrpara[3] = new SqlParameter("@IsCust", SqlDbType.Bit) { Value = isCust };
            arrpara[4] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtPara };

            DataTable dt2 = _ac.TblReadDataSP("sp_Report_LoadListRMBOMFINALFi_F1", arrpara);
            foreach (DataRow dr2 in dt2.Rows)
            {
                DataRow drFinal1 = dtFinal.NewRow();
                drFinal1["RMCode_001"] = dr2["RMCode_001"];
                drFinal1["RMDescription_002"] = dr2["RMDescription_002"];
                drFinal1["Dimenson"] = dr2["SupplierRef"];
                drFinal1["CalDemand"] = dr2["CalDemand"];
                drFinal1["CalUnit"] = dr2["CalUnit"];
                drFinal1["BoMDemand"] = dr2["BoMDemand"];
                drFinal1["BOMUnit"] = dr2["BOMUnit"];
                drFinal1["PRQty"] = dr2["PRQty"];
                drFinal1["PRUnit"] = dr2["PRUnit"];
                drFinal1["POQty"] = dr2["POQty"];
                drFinal1["POUnit"] = dr2["POUnit"];
                drFinal1["TDG00004PK"] = dr2["TDG00004PK"];
                dtFinal.Rows.Add(drFinal1);
            }

            if (dtFinal.Rows.Count <= 0)
            {
                dtFinal.Columns.Add("CalShow", typeof(string));
                dtFinal.Columns.Add("BOMShow", typeof(string));
                dtFinal.Columns.Add("PRShow", typeof(string));
                dtFinal.Columns.Add("POShow", typeof(string));
                return dtFinal;
            }
            var qTest = dtFinal.AsEnumerable().GroupBy(p => new
            {
                RMCode_001 = p.Field<string>("RMCode_001"),
                RMDescription_002 = p.Field<string>("RMDescription_002"),
                Dimenson = p.Field<string>("Dimenson"),
                CalUnit = p.Field<string>("CalUnit"),
                BOMUnit = p.Field<string>("BOMUnit"),
                PRUnit = p.Field<string>("PRUnit"),
                POUnit = p.Field<string>("POUnit"),
                TDG00004PK = p.Field<Int64>("TDG00004PK"),
            }).Select(t => new
            {
                t.Key.RMCode_001,
                t.Key.RMDescription_002,
                t.Key.Dimenson,
                CalDemand = t.Sum(s => s.Field<decimal>("CalDemand")),
                t.Key.CalUnit,
                BoMDemand = t.Sum(s => s.Field<decimal>("BoMDemand")),
                t.Key.BOMUnit,
                PRQty = t.Sum(s => s.Field<decimal>("PRQty")),
                t.Key.PRUnit,
                POQty = t.Sum(s => s.Field<decimal>("POQty")),
                t.Key.POUnit,
                CalShow = t.Sum(s => s.Field<decimal>("CalDemand")) > 0 ? "1" : "0",
                BOMShow = t.Sum(s => s.Field<decimal>("BoMDemand")) > 0 ? "1" : "0",
                PRShow = t.Sum(s => s.Field<decimal>("PRQty")) > 0 ? "1" : "0",
                POShow = t.Sum(s => s.Field<decimal>("POQty")) > 0 ? "1" : "0",

            }).OrderBy(p => p.RMCode_001).ThenBy(p => p.Dimenson).ToList();


            return qTest.CopyToDataTableNew();
        }


        public DataTable Report_LoadBoMPrPO(DataTable dt)
        {

            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@FormatPrQtyDecimal", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_Report_LoadListRMBOMFINAL", arrpara);

        }
        public DataTable Report_LoadBoMPrPOFi(DataTable dt, Int64 cny00012Pk)
        {

            var arrpara = new SqlParameter[3];
            arrpara[0] = new SqlParameter("@FormatPrQtyDecimal", SqlDbType.Int) { Value = ConstSystem.FormatPrQtyDecimal };
            arrpara[1] = new SqlParameter("@CNY00012PK", SqlDbType.BigInt) { Value = cny00012Pk };
            arrpara[2] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_Report_LoadListRMBOMFINALFi", arrpara);

        }
        public DataTable RMCode_LoadImage(Int64 pkCode)
        {

            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PKCode", SqlDbType.BigInt) { Value = pkCode };
            return _ac.TblReadDataSP("usp_RMCode_LoadGridImage", arrpara);

        }
        public DataTable RMCode_LoadGridAttributeGenerate(DataTable dt)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("usp_RMCode_LoadGridGenerateAttributeBoM", arrpara);
        }
        public DataTable RMCode_LoadGridAttributeGenerate(string rmGroup, DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@RmGroup", SqlDbType.NVarChar) { Value = rmGroup };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("usp_RMCode_LoadGridGenerateAttribute", arrpara);
        }
        public DataTable BOM_LoadGridAttributeGenerate(Int64 pkCode, DataTable dt)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@PKCode", SqlDbType.BigInt) { Value = pkCode };
            arrpara[1] = new SqlParameter("@type", SqlDbType.Structured) { Value = dt };
            return _ac.TblReadDataSP("sp_BOM_GenerateAttribute", arrpara);
        }
    }
}
