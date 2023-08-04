using CNY_BaseSys.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;
using GridSumCol = DevExpress.Data.SummaryItemType;
namespace CNY_WH.Common
{
    public static class Com_MaterialReleaseOrder
    {
        public static DataTable TablePackingLoadTemp()
        {
            var dt = new DataTable();
            dt.Columns.Add("ChildPK", typeof(Int64));
            dt.Columns.Add("Factor_CNY001", typeof(double));
            dt.Columns.Add("Note_CNY002", typeof(string));
            dt.Columns.Add("RowStateColor", typeof(string));
            dt.Columns.Add("BoMUnit", typeof(string));
            dt.Columns.Add("PurchaseUnit", typeof(string));
            dt.Columns.Add("Dimension", typeof(string));
            return dt;
        }
        public static DataRow GetPackingInfoConvert(DataTable dt)
        {
            int rC = dt.Rows.Count;
            if (rC == 1) return dt.Rows[0];
            if (rC <= 0) return null;


            DataRow drReturn = null;
            #region "Init Column"
            var lG = new List<GridViewTransferDataColumnInit>
            {

                new GridViewTransferDataColumnInit
                {
                    Caption = @"PK",
                    FieldName = "PK",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Factor",
                    FieldName = "Factor",
                    HorzAlign = HorzAlignment.Far,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 0,
                    FormatField = FormatType.Numeric,
                    FormatString = "#,0.##########",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },





                new GridViewTransferDataColumnInit
                {
                    Caption = @"Unit Code",
                    FieldName = "UnitCode",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = -1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Unit",
                    FieldName = "UnitName",
                    HorzAlign = HorzAlignment.Center,
                    FixStyle = GridFixedCol.None,
                    VisibleIndex = 1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = GridSumCol.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },

            };

            #endregion

            var f = new FrmTransferData
            {
                DtSource = dt,
                ListGvColFormat = lG,
                MinimizeBox = false,
                MaximizeBox = true,
                FormBorderStyle = FormBorderStyle.Sizable,
                Size = new Size(400, 400),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Packing Info Listing",
                StrFilter = "",
                IsMultiSelected = false,
                IsShowFindPanel = false,
                IsShowFooter = false,
                IsShowAutoFilterRow = true
            };

            f.OnTransferData += (s1, e1) =>
            {
                List<DataRow> lDr = e1.ReturnRowsSelected;


                drReturn = lDr[0];







            };
            f.ShowDialog();
            if (drReturn != null)
            {
                return drReturn;
            }

            return dt.Rows[0];
        }
        
        public static DataTable TableLookupType()
        {

        
            AccessData ac = new AccessData(DeclareSystem.SysConnectionString);
            return ac.TblReadDataSP("sp_PR_LoadPRType", null);

        }

        public static bool CheckApprovePr(Int64 pkHeader)
        {

            WaitDialogForm dlg = new WaitDialogForm();
            AccessData ac = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@PRHEADERPK", SqlDbType.BigInt) { Value = pkHeader };
            DataTable dt = ac.TblReadDataSP("sp_PR_CheckApprove", arrpara);
            dlg.Close();
            return dt.Rows.Count <= 0;


        }


        public static void PrintPr2(Int64 pkHeader)
        {

            WaitDialogForm dlg = new WaitDialogForm();
            AccessData ac = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = pkHeader };
            DataSet ds = ac.DtsReadDataSP("sp_PR_PrintFinalF2", arrpara);
            DataTable dtH = ds.Tables[1];

            DataTable dtTest = ds.Tables[0];

            ds.RemoveAllDataTableOnDataSet();



            if (dtH.Rows.Count <= 0)
            {
                dlg.Close();
                XtraMessageBox.Show("No data display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }










            var qFieldAtt = dtTest.Columns.Cast<DataColumn>().Where(p => p.ColumnName.Contains("%%%%%")).Select(p => p.ColumnName).ToArray();

            Dictionary<string, string> dicUnit;
            DataTable dtFinal = ProcessGeneral.StandardUnitAttTableBeforPrint(dtTest, qFieldAtt, out dicUnit);

            if (dtFinal.Rows.Count > 1)
            {
                dtFinal = dtFinal.AsEnumerable().OrderBy(p => p.Field<string>("ProductionOrder")).ThenBy(p => p.Field<string>("Reference"))
                        .ThenBy(p => p.Field<string>("MainMaterialGroupC")).ThenBy(p => p.Field<string>("RMCode_001")).CopyToDataTable();
            }

            string mainGroup = string.Join(" - ", dtFinal.AsEnumerable().Select(p => new
                {
                    MainMaterialGroupC = p.Field<string>("MainMaterialGroupC"),
                    MainMaterialGroupD = p.Field<string>("MainMaterialGroupD"),
                }).Distinct().OrderBy(p => p.MainMaterialGroupC)
                .Select((p, idx) => string.Format("{0}.{1}", idx + 1, p.MainMaterialGroupD)).ToArray()).Trim();


            dtH.Rows[0]["MainMaterialGroup"] = mainGroup;
            dtH.AcceptChanges();




            DataSet dsPrint = new DataSet("Print");
            dtH.TableName = "dtH";
            dtFinal.TableName = "dtFinal";

            dsPrint.Tables.Add(dtH);
            dsPrint.Tables.Add(dtFinal);
            dlg.Close();


            //FrmRptPr f1 = new FrmRptPr(dsPrint, dicUnit);
            //f1.Show();



        }



        public static void PrintPr(Int64 pkHeader, bool isFinishing)
        {

            WaitDialogForm dlg = new WaitDialogForm();
            AccessData ac = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@HeaderPK", SqlDbType.BigInt) { Value = pkHeader };
            arrpara[1] = new SqlParameter("@IsFinishing", SqlDbType.Bit) { Value = isFinishing };
            DataSet ds = ac.DtsReadDataSP("sp_PR_PrintFinalF1", arrpara);
            DataTable dtH = ds.Tables[0];

            DataTable dtSo = ds.Tables[1];




            DataTable dtTest = ds.Tables[2];


            DataTable dtParent = ds.Tables[3];




            ds.RemoveAllDataTableOnDataSet();

            if (dtTest.Rows.Count <= 0)
            {
                dlg.Close();
                XtraMessageBox.Show("No data display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

           








            var qFieldAtt = dtTest.Columns.Cast<DataColumn>().Where(p => p.ColumnName.Contains("%%%%%")).Select(p => p.ColumnName).ToArray();

            Dictionary<string, string> dicUnit;
            DataTable dtFinal = ProcessGeneral.StandardUnitAttTableBeforPrint(dtTest, qFieldAtt, out dicUnit);




            int j = 1;
            for (int i = dtParent.Rows.Count - 1; i >= 0; i--)
            {

                Int64 childPk = ProcessGeneral.GetSafeInt64(dtParent.Rows[i]["ChildPK"]);
                string strGroup = ProcessGeneral.GetSafeString(dtParent.Rows[i]["MainMaterialGroupD"]);
                DataRow drTest = dtFinal.NewRow();

                drTest["STT"] = 0 - j;
                drTest["RMDescription_002"] = string.Format("{0}.{1}", childPk, strGroup);
                drTest["ChildPK"] = childPk;
                drTest["ParentPK"] = 0;
                dtFinal.Rows.Add(drTest);
                j++;
            }




            DataSet dsPrint = new DataSet("Print");
            dtH.TableName = "dtH";
            dtSo.TableName = "dtSo";
            dtFinal.TableName = "dtFinal";

            dsPrint.Tables.Add(dtH);
            dsPrint.Tables.Add(dtSo);
            dsPrint.Tables.Add(dtFinal);
            dsPrint.Tables.Add(dtParent);
            dlg.Close();


            //Frm005PurchaseRequisition f1 = new Frm005PurchaseRequisition(dsPrint, dicUnit);
            //f1.Print();




        }

        public static DataTable TableCNY00020PKTEMP()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            return dt;
        }
        public static Int64 GetPkPrTable()
        {
            return ProcessGeneral.GetNextId("SDRTABLE");
        }
        public static Int64 GetPkSDRTable(int row)
        {
            return ProcessGeneral.GetNextId("SDRTABLE", row);
        }
        public static DataTable TableTempBOMSOSelectedPR()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("CNY00016PK", typeof(Int64));
            dt.Columns.Add("PlanQty", typeof(Int32));
            return dt;
        }

        public static DataTable TableTempBomsoSelectedNpr()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("CNY00016PK", typeof(Int64));
            dt.Columns.Add("PRQty_CNY002B", typeof(double));
            return dt;
        }

        public static DataTable TableTempBOMSOSelectedPRFi()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("CNY00016PK", typeof(Int64));
            dt.Columns.Add("QualityCode", typeof(string));
            dt.Columns.Add("CNY00050PKFi", typeof(Int64));
            dt.Columns.Add("PlanQty", typeof(Int32));
            return dt;
        }
        public static DataTable TablePackingFactorAttTemplate()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CNY00008PK", typeof(Int64));
            dt.Columns.Add("AttributeValue", typeof(string));
            dt.Columns.Add("AttributeUnit", typeof(string));
            return dt;
        }



        public static DataTable TableTreeviewTemplateSample()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TDG00001PK", typeof(Int64));
            dt.Columns.Add("RMCode_001", typeof(string));
            dt.Columns.Add("RMDescription_002", typeof(string));
            dt.Columns.Add("Color", typeof(string));
            dt.Columns.Add("MainMaterialGroup", typeof(string));
            dt.Columns.Add("CNY00002PK", typeof(Int64));
            dt.Columns.Add("Supplier", typeof(string));
            dt.Columns.Add("StockQty_CNY010", typeof(double));
            dt.Columns.Add("POQty_CNY003", typeof(double));
            dt.Columns.Add("UnitCode_CNY011", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("ETD", typeof(DateTime));
            dt.Columns.Add("ETA", typeof(DateTime));
            dt.Columns.Add("Note", typeof(string));
            dt.Columns.Add("ChildPK", typeof(Int64));
            dt.Columns.Add("ParentPK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
            dt.Columns.Add("CNY00050PK", typeof(Int64));
            dt.Columns.Add("CNY00004PK", typeof(Int64));
            dt.Columns.Add("Purchaser", typeof(string));
            dt.Columns.Add("AllowUpdate", typeof(bool));
            dt.Columns.Add("IsHasChild", typeof(bool));
            dt.Columns.Add("CNY00014PK", typeof(Int64));
            dt.Columns.Add("Dimension", typeof(string));
            dt.Columns.Add("SupplierRef", typeof(string));
            dt.Columns.Add("TDG00004PK", typeof(Int64));
            return dt;
        }

        public static DataTable TableTreeviewTemplate()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductionOrder", typeof(string));
            dt.Columns.Add("CusRef", typeof(string));
            dt.Columns.Add("ProductCode", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("ItemType", typeof(string));
            dt.Columns.Add("SortOrderNode", typeof(Int32));
            dt.Columns.Add("TDG00001PK", typeof(Int64));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("Reference", typeof(string));
            dt.Columns.Add("MainMaterialGroup", typeof(string));
            dt.Columns.Add("SourcePK", typeof(Int64));
            dt.Columns.Add("SourceName", typeof(string));
            dt.Columns.Add("DestinationPK", typeof(Int64));
            dt.Columns.Add("DestinationName", typeof(string));
            dt.Columns.Add("ProductionPointPK", typeof(Int64));
            dt.Columns.Add("ProductionPointName", typeof(string));
            dt.Columns.Add("UnitPK", typeof(Int64));
            dt.Columns.Add("UnitName", typeof(string));
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("CNY00016PK", typeof(Int64));
            dt.Columns.Add("PartnerPK", typeof(Int64));
            dt.Columns.Add("PartnerName", typeof(string));
            dt.Columns.Add("Quantity", typeof(double));
            dt.Columns.Add("Using", typeof(double));
            dt.Columns.Add("Tolerance", typeof(double));
            dt.Columns.Add("AmountUC", typeof(double));
            dt.Columns.Add("NeededQty", typeof(double));
            dt.Columns.Add("ActualQty", typeof(double));
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns.Add("DocumentRef", typeof(string));
            dt.Columns.Add("BomNo", typeof(string));
            dt.Columns.Add("DateUpate", typeof(DateTime));
            dt.Columns.Add("UserUpdate", typeof(string));
            dt.Columns.Add("ComputerUpdate", typeof(string));
            dt.Columns.Add("ChildPK", typeof(Int64));
            dt.Columns.Add("ParentPK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
            dt.Columns.Add("AllowUpdate", typeof(bool));

            return dt;
        }

        public static DataTable TableTreeviewN1Template()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TDG00001PK", typeof(Int64));
            dt.Columns.Add("RMCode_001", typeof(string));
            dt.Columns.Add("RMDescription_002", typeof(string));
            dt.Columns.Add("Color", typeof(string));
            dt.Columns.Add("MainMaterialGroup", typeof(string));
            dt.Columns.Add("CNY00002PK", typeof(Int64));
            dt.Columns.Add("Supplier", typeof(string));
            dt.Columns.Add("StockQty_CNY010", typeof(double));
            dt.Columns.Add("PRQty_CNY002", typeof(double));
            dt.Columns.Add("POQty_CNY003", typeof(double));
            dt.Columns.Add("UnitCode_CNY011", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
           
            dt.Columns.Add("ETD", typeof(DateTime));
            dt.Columns.Add("ETA", typeof(DateTime));
            dt.Columns.Add("Note", typeof(string));
          
            dt.Columns.Add("ChildPK", typeof(Int64));
            dt.Columns.Add("ParentPK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
            dt.Columns.Add("CNY00050PK", typeof(Int64));
            dt.Columns.Add("CNY00004PK", typeof(Int64));
            dt.Columns.Add("Purchaser", typeof(string));
            dt.Columns.Add("AllowUpdate", typeof(bool));
            dt.Columns.Add("IsHasChild", typeof(bool));


            dt.Columns.Add("CNY00014PK", typeof(Int64));

            dt.Columns.Add("Dimension", typeof(string));

            dt.Columns.Add("PRQty_CNY002B", typeof(double));
            dt.Columns.Add("POQty_CNY003B", typeof(double));
            dt.Columns.Add("UnitCode_CNY011B", typeof(string));
            dt.Columns.Add("UnitB", typeof(string));
            dt.Columns.Add("PackingFactor", typeof(double));

            dt.Columns.Add("SupplierRef", typeof(string));
            dt.Columns.Add("TDG00004PK", typeof(Int64));

            dt.Columns.Add("AllowChangeWaste", typeof(bool));
            dt.Columns.Add("IsGrouping", typeof(bool));
            dt.Columns.Add("IsPacking", typeof(bool));
            dt.Columns.Add("DifUC", typeof(bool));
            return dt;
        }

        public static DataTable TableGridMChildTemplate()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Reference", typeof(string));


            dt.Columns.Add("PRQty_CNY002", typeof(double));
            dt.Columns.Add("POQty_CNY003", typeof(double));
            dt.Columns.Add("PRQty_CNY002B", typeof(double));
            dt.Columns.Add("POQty_CNY003B", typeof(double));
            dt.Columns.Add("PlanQuantity", typeof(Int32));
          
 //           dt.Columns.Add("AssemblyComponent", typeof(string));
            dt.Columns.Add("RMCode_001", typeof(string));
            dt.Columns.Add("RMDescription_002", typeof(string));
            dt.Columns.Add("ProDimension", typeof(string));
       //     dt.Columns.Add("Count", typeof(Int32));
            dt.Columns.Add("UC", typeof(double));
            dt.Columns.Add("Tolerance", typeof(double));
            dt.Columns.Add("PercentUsing", typeof(double));



            dt.Columns.Add("ProjectName", typeof(string));

            dt.Columns.Add("ProductionOrder", typeof(string));
            dt.Columns.Add("RootSOQty", typeof(Int32));
            dt.Columns.Add("CNY00020PK", typeof(Int64));

            return dt;
        }



        public static DataTable TableGridMChildTemplateFi()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Reference", typeof(string));


            dt.Columns.Add("PRQty_CNY002", typeof(double));
            dt.Columns.Add("POQty_CNY003", typeof(double));
            dt.Columns.Add("PRQty_CNY002B", typeof(double));
            dt.Columns.Add("POQty_CNY003B", typeof(double));
            dt.Columns.Add("PlanQuantity", typeof(Int32));
            dt.Columns.Add("RMCode_001", typeof(string));
            dt.Columns.Add("RMDescription_002", typeof(string));
            dt.Columns.Add("ProDimension", typeof(string));
           
            dt.Columns.Add("QualityCode", typeof(string));
            dt.Columns.Add("Color", typeof(string));


            dt.Columns.Add("ProjectName", typeof(string));

            dt.Columns.Add("ProductionOrder", typeof(string));
            dt.Columns.Add("RootSOQty", typeof(Int32));
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("CNY00050PKFi", typeof(Int64));

















            return dt;
        }

        public static DataTable TableGridChildN1Template()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Reference", typeof(string));
       
        
            dt.Columns.Add("PRQty_CNY002", typeof(double));
            dt.Columns.Add("POQty_CNY003", typeof(double));
            dt.Columns.Add("PRQty_CNY002B", typeof(double));
            dt.Columns.Add("POQty_CNY003B", typeof(double));
            dt.Columns.Add("PlanQuantity", typeof(Int32));
            dt.Columns.Add("Position", typeof(string));
            dt.Columns.Add("RmDimension", typeof(string));
            dt.Columns.Add("AssemblyComponent", typeof(string));
            dt.Columns.Add("TDG00001PK", typeof(Int64));
            dt.Columns.Add("RMCode_001", typeof(string));
            dt.Columns.Add("RMDescription_002", typeof(string));
            dt.Columns.Add("ProDimension", typeof(string));


            dt.Columns.Add("Factor", typeof(double));
            dt.Columns.Add("UC", typeof(double));
            dt.Columns.Add("Tolerance", typeof(double));
            dt.Columns.Add("PercentUsing", typeof(double));


       
            dt.Columns.Add("ProjectName", typeof(string));
       
            dt.Columns.Add("ProductionOrder", typeof(string));
            dt.Columns.Add("RootSOQty", typeof(Int32));
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("CNY00016PK", typeof(Int64));
            dt.Columns.Add("ChildPK", typeof(Int64));
            dt.Columns.Add("ParentPK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
         
            dt.Columns.Add("AllowUpdate", typeof(bool));
            dt.Columns.Add("ToleranceBOM", typeof(double));
            dt.Columns.Add("CNY00019PK", typeof(Int64));

            dt.Columns.Add("TotalQuantity", typeof(double));
            dt.Columns.Add("UC_BOM", typeof(double));
            return dt;

          
        }

        public static DataTable TableStep1GroupTemplate()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Selected", typeof(bool));
            dt.Columns.Add("TDG00001PK", typeof(Int64));
            dt.Columns.Add("RMCode_001", typeof(string));
            dt.Columns.Add("RMDescription_002", typeof(string));
            dt.Columns.Add("Color", typeof(string));
            dt.Columns.Add("Supplier", typeof(string));
            dt.Columns.Add("SupplierRef", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("Purchaser", typeof(string));
            dt.Columns.Add("CNY00050PK", typeof(Int64));
            dt.Columns.Add("CNY00002PK", typeof(Int64));
            dt.Columns.Add("TDG00004PK", typeof(Int64));
            dt.Columns.Add("CNY00004PK", typeof(Int64));
            dt.Columns.Add("UnitCode_CNY011", typeof(string));
            dt.Columns.Add("ChildPK", typeof(string));
            dt.Columns.Add("ParentPK", typeof(string));
            return dt;

         


        
  


        }
        public static DataTable TableGridChildTemplateFi()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Reference", typeof(string));


            dt.Columns.Add("PRQty_CNY002", typeof(double));
            dt.Columns.Add("POQty_CNY003", typeof(double));
            dt.Columns.Add("PRQty_CNY002B", typeof(double));
            dt.Columns.Add("POQty_CNY003B", typeof(double));
            dt.Columns.Add("PlanQuantity", typeof(Int32));

  
            dt.Columns.Add("Route", typeof(string));
            dt.Columns.Add("Color", typeof(string));
            dt.Columns.Add("TDG00001PK", typeof(Int64));
            dt.Columns.Add("RMCode_001", typeof(string));
            dt.Columns.Add("RMDescription_002", typeof(string));
            dt.Columns.Add("ProDimension", typeof(string));


 
            dt.Columns.Add("UC", typeof(double));
            dt.Columns.Add("AreaPaint", typeof(double));
            dt.Columns.Add("RateGram", typeof(double));
            dt.Columns.Add("QualityCode", typeof(string));


            dt.Columns.Add("ProjectName", typeof(string));

            dt.Columns.Add("ProductionOrder", typeof(string));
            dt.Columns.Add("RootSOQty", typeof(Int32));
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("CNY00016PK", typeof(Int64));
            dt.Columns.Add("ChildPK", typeof(Int64));
            dt.Columns.Add("ParentPK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));

            dt.Columns.Add("AllowUpdate", typeof(bool));

            dt.Columns.Add("CNY00050PKFi", typeof(Int64));
            dt.Columns.Add("CNY00019PK", typeof(Int64));
            return dt;


        }
        public static void VisibleTreeColumnGroupBy(TreeList tl,bool visibleBoM, params string[] arrAttribute)
        {
            List<VisibleColBomInfo> l = new List<VisibleColBomInfo>();

            l.Add(new VisibleColBomInfo("TDG00001PK", false));//false
            l.Add(new VisibleColBomInfo("RMCode_001", true));
            l.Add(new VisibleColBomInfo("RMDescription_002", true));
            l.Add(new VisibleColBomInfo("Color", true));
            l.Add(new VisibleColBomInfo("MainMaterialGroup", true));
            foreach (string s in arrAttribute)
            {
                l.Add(new VisibleColBomInfo(s, true));
            }

            l.Add(new VisibleColBomInfo("StockQty_CNY010", true));

            l.Add(new VisibleColBomInfo("PRQty_CNY002", true));
            l.Add(new VisibleColBomInfo("POQty_CNY003", true));
            l.Add(new VisibleColBomInfo("Unit", true));
            l.Add(new VisibleColBomInfo("UnitCode_CNY011", false));//false

            l.Add(new VisibleColBomInfo("PackingFactor", visibleBoM));

            l.Add(new VisibleColBomInfo("PRQty_CNY002B", visibleBoM));
            l.Add(new VisibleColBomInfo("POQty_CNY003B", visibleBoM));
            l.Add(new VisibleColBomInfo("UnitB", visibleBoM));
            l.Add(new VisibleColBomInfo("UnitCode_CNY011B", false));//false




            l.Add(new VisibleColBomInfo("Supplier", true));

            l.Add(new VisibleColBomInfo("CNY00002PK", false));//false


            l.Add(new VisibleColBomInfo("SupplierRef", true));

            l.Add(new VisibleColBomInfo("TDG00004PK", false));//false


            l.Add(new VisibleColBomInfo("UC", true));


            l.Add(new VisibleColBomInfo("Purchaser", true));
            l.Add(new VisibleColBomInfo("ETD", true));
            l.Add(new VisibleColBomInfo("ETA", true));
            l.Add(new VisibleColBomInfo("Note", true));

            l.Add(new VisibleColBomInfo("RowState", false));//false
            l.Add(new VisibleColBomInfo("CNY00050PK", false));//false
            l.Add(new VisibleColBomInfo("CNY00004PK", false));//false

            l.Add(new VisibleColBomInfo("AllowUpdate", false));//false
            l.Add(new VisibleColBomInfo("IsHasChild", false));//false
            l.Add(new VisibleColBomInfo("CNY00014PK", false));//false
            l.Add(new VisibleColBomInfo("Dimension", false));
            l.Add(new VisibleColBomInfo("AllowChangeWaste", false));
            l.Add(new VisibleColBomInfo("IsGrouping", false));
            l.Add(new VisibleColBomInfo("IsPacking", false));
            l.Add(new VisibleColBomInfo("DifUC", false));
            // DataStatus.Unchange


            var qUnVisible = l.Where(p => !p.Visible).Select(p => p.FieldName).ToList();
            var qVisible = l.Where(p => p.Visible).Select(p => p.FieldName).ToList();

            for (int i = 0; i < qVisible.Count; i++)
            {
                TreeListColumn tColV = tl.Columns[qVisible[i]];
                if (tColV == null) continue;
                tColV.VisibleIndex = i;
            }
            foreach (string s in qUnVisible)
            {
                TreeListColumn tColU = tl.Columns[s];
                if (tColU == null) continue;
                tColU.Visible = false;

            }

        }


        public static void VisibleTreeColumnSortGenerate(TreeList tl, bool visibleBoM, List<string> arrAttribute)
        {
            List<VisibleColBomInfo> l = new List<VisibleColBomInfo>();

            l.Add(new VisibleColBomInfo("TDG00001PK", false));//false
            l.Add(new VisibleColBomInfo("RMCode_001", true));
            l.Add(new VisibleColBomInfo("RMDescription_002", true));
            l.Add(new VisibleColBomInfo("Color", true));
            l.Add(new VisibleColBomInfo("MainMaterialGroup", true));
            foreach (string s in arrAttribute)
            {
                l.Add(new VisibleColBomInfo(s, true));
            }

            l.Add(new VisibleColBomInfo("StockQty_CNY010", true));

            l.Add(new VisibleColBomInfo("PRQty_CNY002", true));
            l.Add(new VisibleColBomInfo("POQty_CNY003", true));
            l.Add(new VisibleColBomInfo("Unit", true));
            l.Add(new VisibleColBomInfo("UnitCode_CNY011", false));//false
            l.Add(new VisibleColBomInfo("PackingFactor", visibleBoM));
            l.Add(new VisibleColBomInfo("PRQty_CNY002B", visibleBoM));
            l.Add(new VisibleColBomInfo("POQty_CNY003B", visibleBoM));
            l.Add(new VisibleColBomInfo("UnitB", visibleBoM));
            l.Add(new VisibleColBomInfo("UnitCode_CNY011B", false));//false



        
            l.Add(new VisibleColBomInfo("Supplier", true));

            l.Add(new VisibleColBomInfo("CNY00002PK", false));//false


            l.Add(new VisibleColBomInfo("SupplierRef", true));

            l.Add(new VisibleColBomInfo("TDG00004PK", false));//false


            l.Add(new VisibleColBomInfo("UC", true));
    

            l.Add(new VisibleColBomInfo("Purchaser", true));
            l.Add(new VisibleColBomInfo("ETD", false));
            l.Add(new VisibleColBomInfo("ETA", false));
            l.Add(new VisibleColBomInfo("Note", false));

            l.Add(new VisibleColBomInfo("RowState", false));//false
            l.Add(new VisibleColBomInfo("CNY00050PK", false));//false
            l.Add(new VisibleColBomInfo("CNY00004PK", false));//false

            l.Add(new VisibleColBomInfo("AllowUpdate", false));//false
            l.Add(new VisibleColBomInfo("IsHasChild", false));//false
            l.Add(new VisibleColBomInfo("CNY00014PK", false));//false
            l.Add(new VisibleColBomInfo("Dimension", false));
            l.Add(new VisibleColBomInfo("AllowChangeWaste", false));
            l.Add(new VisibleColBomInfo("IsGrouping", false));
            l.Add(new VisibleColBomInfo("IsPacking", false));
            l.Add(new VisibleColBomInfo("DifUC", false));
            var qUnVisible = l.Where(p => !p.Visible).Select(p => p.FieldName).ToList();
            var qVisible = l.Where(p => p.Visible).Select(p => p.FieldName).ToList();

            for (int i = 0; i < qVisible.Count; i++)
            {
                TreeListColumn tColV = tl.Columns[qVisible[i]];
                if (tColV == null) continue;
                tColV.VisibleIndex = i;
            }
            foreach (string s in qUnVisible)
            {
                TreeListColumn tColU = tl.Columns[s];
                if (tColU == null) continue;
                tColU.Visible = false;

            }

        }
        public static void VisibleTreeColumnSort(TreeList tl, bool visibleBoM, List<string> arrAttribute)
        {
            List<VisibleColBomInfo> l = new List<VisibleColBomInfo>();
            
            l.Add(new VisibleColBomInfo("ItemType", true));
            l.Add(new VisibleColBomInfo("ProductionOrder", visibleBoM));
            l.Add(new VisibleColBomInfo("CusRef", visibleBoM));
            l.Add(new VisibleColBomInfo("ProductCode", visibleBoM));//false
            l.Add(new VisibleColBomInfo("ProductName", visibleBoM));
            l.Add(new VisibleColBomInfo("SortOrderNode", true));
            l.Add(new VisibleColBomInfo("TDG00001PK", false));//false
            l.Add(new VisibleColBomInfo("ItemCode", true));
            l.Add(new VisibleColBomInfo("ItemName", true));
            l.Add(new VisibleColBomInfo("Reference", true));
            l.Add(new VisibleColBomInfo("MainMaterialGroup", true));
            foreach (string s in arrAttribute)
            {
                l.Add(new VisibleColBomInfo(s, true));
            }
            l.Add(new VisibleColBomInfo("SourcePK", false));
            l.Add(new VisibleColBomInfo("SourceName", true));
            l.Add(new VisibleColBomInfo("DestinationPK", false));
            l.Add(new VisibleColBomInfo("DestinationName", true));
            l.Add(new VisibleColBomInfo("ProductionPointPK", false));
            l.Add(new VisibleColBomInfo("ProductionPointName", visibleBoM));
            l.Add(new VisibleColBomInfo("UnitPK", false));
            l.Add(new VisibleColBomInfo("UnitName", true));
            
            l.Add(new VisibleColBomInfo("CNY00020PK", false));
            l.Add(new VisibleColBomInfo("CNY00016PK", false));
            l.Add(new VisibleColBomInfo("PartnerPK", false));
            l.Add(new VisibleColBomInfo("PartnerName", true));
            l.Add(new VisibleColBomInfo("Quantity", visibleBoM));
            l.Add(new VisibleColBomInfo("Using", visibleBoM));
            l.Add(new VisibleColBomInfo("Tolerance", visibleBoM));
            l.Add(new VisibleColBomInfo("AmountUC", visibleBoM));
            l.Add(new VisibleColBomInfo("NeededQty", visibleBoM));
            l.Add(new VisibleColBomInfo("ActualQty", true));
            l.Add(new VisibleColBomInfo("Remark", true));
            l.Add(new VisibleColBomInfo("DocumentRef", true));
            l.Add(new VisibleColBomInfo("BomNo", visibleBoM));
            l.Add(new VisibleColBomInfo("DateUpate", false));//false
            l.Add(new VisibleColBomInfo("UserUpdate", false));//false
            l.Add(new VisibleColBomInfo("ComputerUpdate", false));//false
            l.Add(new VisibleColBomInfo("RowState", false));//false// DataStatus.Unchange
            l.Add(new VisibleColBomInfo("AllowUpdate", false));//false

            var qUnVisible = l.Where(p => !p.Visible).Select(p => p.FieldName).ToList();
            var qVisible = l.Where(p => p.Visible).Select(p => p.FieldName).ToList();

            for (int i = 0; i < qVisible.Count; i++)
            {
                TreeListColumn tColV = tl.Columns[qVisible[i]];
                if(tColV == null) continue;
                tColV.VisibleIndex = i;
            }
            foreach (string s in qUnVisible)
            {
                TreeListColumn tColU = tl.Columns[s];
                if (tColU == null) continue;
                tColU.Visible = false;

            }

        }
    
        public static void VisibleTreeColumnItemCode(TreeList tl, List<string> arrAttribute)
        {
            List<VisibleColBomInfo> l = new List<VisibleColBomInfo>();


            l.Add(new VisibleColBomInfo("MainMaterialGroup", true));
            l.Add(new VisibleColBomInfo("ItemType", true));
            l.Add(new VisibleColBomInfo("ProductionOrder", true));
            l.Add(new VisibleColBomInfo("ProductName", true));
            l.Add(new VisibleColBomInfo("Reference", true));
            l.Add(new VisibleColBomInfo("ProductCode", true));
            l.Add(new VisibleColBomInfo("ProductName", true));
            
            l.Add(new VisibleColBomInfo("CNYMF012PK", false));
            l.Add(new VisibleColBomInfo("PPCode", false));
            l.Add(new VisibleColBomInfo("ProductionPoint", true));
            l.Add(new VisibleColBomInfo("RMCode_001", true));
            l.Add(new VisibleColBomInfo("RMDescription_002", true));
            //l.Add(new VisibleColBomInfo("ColorReference", false));
            foreach (string s in arrAttribute)
            {
                l.Add(new VisibleColBomInfo(s, true));
            }
            //l.Add(new VisibleColBomInfo("PRQty_CNY002", true));
            l.Add(new VisibleColBomInfo("WOQty", true));
            l.Add(new VisibleColBomInfo("UCUnit", true));
            l.Add(new VisibleColBomInfo("Supplier", true));
            l.Add(new VisibleColBomInfo("SupplierRef", false));
            l.Add(new VisibleColBomInfo("CNY00002PK", false));
            l.Add(new VisibleColBomInfo("TDG00004PK", false));
            l.Add(new VisibleColBomInfo("BomUnitPK", false));
            l.Add(new VisibleColBomInfo("UCUnitCode", false));
            l.Add(new VisibleColBomInfo("CNY00020PK", false));
            l.Add(new VisibleColBomInfo("CNY00016PK", false));
            l.Add(new VisibleColBomInfo("StringRowIndex", false));
            l.Add(new VisibleColBomInfo("SortOrderNode", false));
            l.Add(new VisibleColBomInfo("TDG00001PK", false));
            l.Add(new VisibleColBomInfo("Check", false));

            l.Add(new VisibleColBomInfo("FormulaString", false));
            l.Add(new VisibleColBomInfo("FormulaStringDisplay", false));
            l.Add(new VisibleColBomInfo("PurchaseUnit", false));
            l.Add(new VisibleColBomInfo("DecimalRound", false));


            var qUnVisible = l.Where(p => !p.Visible).Select(p => p.FieldName).ToList();
            var qVisible = l.Where(p => p.Visible).Select(p => p.FieldName).ToList();

            for (int i = 0; i < qVisible.Count; i++)
            {
                if(tl.Columns[qVisible[i]]==null)continue;

                tl.Columns[qVisible[i]].VisibleIndex = i;
            }
            foreach (string s in qUnVisible)
            {
                if (tl.Columns[s] == null) continue;
                tl.Columns[s].Visible = false;

            }

        }
    }
}
