using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys.Class;
using CNY_BaseSys.WForm;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

namespace CNY_BaseSys.Common
{
    public static class Ctrl_FinishingGenneral
    {



        public static void LoadReportAmountBomInfo(Int64 cny00019Pk, FrmBase fCurrent)
        {
            if (cny00019Pk <= 0) return;



            if (fCurrent == null) return;
            Form fParent = fCurrent.ParentForm;
            if (fParent == null) return;

            var qForm = fParent.MdiChildren.Select((p, i) => new
            {
                Index = i,
                FrmMdi = p,
            }).Where(t => t.FrmMdi is FrmFiAmountProductionOrder).Select(p => new
            {
                p.Index,
                FrmMdi = (FrmFiAmountProductionOrder)p.FrmMdi,
            }).Select(p => new
            {
                p.Index,
                p.FrmMdi,
                p.FrmMdi.Cny00019Pk
            }).ToList();

            if (!qForm.Any())
            {
                DisplayFormReportAmountBomOnSreen(cny00019Pk, fCurrent, fParent);
                return;
            }


            var q3 = qForm.Where(p => p.Cny00019Pk == cny00019Pk).Distinct().ToList();
            if (q3.Any())
            {
                foreach (var item in q3)
                {
                    fParent.MdiChildren[item.Index].Activate();
                    return;
                }
            }





            DisplayFormReportAmountBomOnSreen(cny00019Pk, fCurrent, fParent);





        }


        private static void DisplayFormReportAmountBomOnSreen(Int64 cny00019Pk, FrmBase fCurrent, Form fParent)
        {
            WaitDialogForm dlg = new WaitDialogForm();
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019Pk };
            DataSet ds = accData.DtsReadDataSP("sp_LoadSOInfoWhenCheckReport", arrpara);

            if (ds.Tables[2].Rows.Count <= 0)
            {
                dlg.Close();
                XtraMessageBox.Show("No Data Display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var f = new FrmFiAmountProductionOrder(ds, cny00019Pk)
            {
                MdiParent = fParent,
                WindowState = FormWindowState.Normal,
                StartPosition = FormStartPosition.CenterScreen
            };
            f.SetDefaultCommandAndPermission(fCurrent);
            f.Show();
            dlg.Close();
        }




    

        public static void LoadProductionDetailInfo(Int64 cny00019Pk, FrmBase fCurrent)
        {
            if (cny00019Pk <= 0) return;
          


            if (fCurrent == null) return;
            Form fParent = fCurrent.ParentForm;
            if (fParent == null) return;

            DataTable dtPr = GetPrInfoBySoPK(cny00019Pk);
            bool isNew = true;
            if (dtPr.Rows.Count > 0)
            {
                isNew = ProcessGeneral.GetSafeBool(dtPr.Rows[0]["IsNew"]);
            }

            

            if (!isNew)
            {
                var qForm = fParent.MdiChildren.Select((p, i) => new
                {
                    Index = i,
                    FrmMdi = p,
                }).Where(t => t.FrmMdi is FrmDemandProductionOrder).Select(p => new
                {
                    p.Index,
                    FrmMdi = (FrmDemandProductionOrder)p.FrmMdi,
                }).Select(p => new
                {
                    p.Index,
                    p.FrmMdi,
                    p.FrmMdi.Cny00019Pk
                }).ToList();

                if (!qForm.Any())
                {
                    DisplayFormDetailProductOnSreen(cny00019Pk, fCurrent, fParent);
                    return;
                }


                var q3 = qForm.Where(p => p.Cny00019Pk == cny00019Pk).Distinct().ToList();
                if (q3.Any())
                {
                    foreach (var item in q3)
                    {
                        fParent.MdiChildren[item.Index].Activate();
                        return;
                    }
                }
                DisplayFormDetailProductOnSreen(cny00019Pk, fCurrent, fParent);
                return;
            }



            var qFormN = fParent.MdiChildren.Select((p, i) => new
            {
                Index = i,
                FrmMdi = p,
            }).Where(t => t.FrmMdi is FrmDemandProductionOrderN).Select(p => new
            {
                p.Index,
                FrmMdi = (FrmDemandProductionOrderN)p.FrmMdi,
            }).Select(p => new
            {
                p.Index,
                p.FrmMdi,
                p.FrmMdi.Cny00019Pk
            }).ToList();

            if (!qFormN.Any())
            {
                DisplayFormDetailProductOnSreenN(cny00019Pk, fCurrent, fParent);
                return;
            }


            var q3N = qFormN.Where(p => p.Cny00019Pk == cny00019Pk).Distinct().ToList();
            if (q3N.Any())
            {
                foreach (var item in q3N)
                {
                    fParent.MdiChildren[item.Index].Activate();
                    return;
                }
            }
            DisplayFormDetailProductOnSreenN(cny00019Pk, fCurrent, fParent);




        }
        private static void DisplayFormDetailProductOnSreenN(Int64 cny00019Pk, FrmBase fCurrent, Form fParent)
        {
            
           
            var f = new FrmDemandProductionOrderN(cny00019Pk)
            {
                MdiParent = fParent,
                WindowState = FormWindowState.Normal,
                StartPosition = FormStartPosition.CenterScreen
            };
            f.SetDefaultCommandAndPermission(fCurrent);
            f.Show();
         
        }
        private static DataTable GetPrInfoBySoPK(Int64 cny00019Pk)
        {
          
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019Pk };
             return accData.TblReadDataSP("usp_MRP_GetPRNo", arrpara);

          
        }
        private static void DisplayFormDetailProductOnSreen(Int64 cny00019Pk, FrmBase fCurrent, Form fParent)
        {
            WaitDialogForm dlg = new WaitDialogForm();
            AccessData accData = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@CNY00019PK", SqlDbType.BigInt) { Value = cny00019Pk };
            DataSet ds = accData.DtsReadDataSP("sp_LoadSOInfoWhenCheckReport", arrpara);

            if (ds.Tables[2].Rows.Count <= 0)
            {
                dlg.Close();
                XtraMessageBox.Show("No Data Display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var f = new FrmDemandProductionOrder(ds, cny00019Pk)
            {
                MdiParent = fParent,
                WindowState = FormWindowState.Normal,
                StartPosition = FormStartPosition.CenterScreen
            };
            f.SetDefaultCommandAndPermission(fCurrent);
            f.Show();
            dlg.Close();
        }





    
        public static void ShowFormBoMFinishingByCny019Pk(SOParaBoMFinishingInfo soInfo, FrmBase fCurrent)
        {

            //Int64 cny00019Pk = soInfo.CNY00019PK;
            //if (cny00019Pk <= 0) return;
            //if (fCurrent == null) return;
            //Form fParent = fCurrent.ParentForm;
            //if (fParent == null) return;

            //var qForm = fParent.MdiChildren.Select((p, i) => new
            //{
            //    Index = i,
            //    FrmMdi = p,
            //}).Where(t => t.FrmMdi is FrmBOMFinishing).Select(p => new
            //{
            //    p.Index,
            //    FrmMdi = (FrmBOMFinishing)p.FrmMdi,
            //}).Select(p => new
            //{
            //    p.Index,
            //    p.FrmMdi,
            //    p.FrmMdi.Cny00019Pk
            //}).ToList();

            //if (!qForm.Any())
            //{
            //    DisplayFormBoMFinshingOnSreen(soInfo, fCurrent, fParent);
            //    return;
            //}

            //var q3 = qForm.Where(p => p.Cny00019Pk == cny00019Pk).Distinct().ToList();
            //if (q3.Any())
            //{
            //    foreach (var item in q3)
            //    {
            //        fParent.MdiChildren[item.Index].Activate();
            //        return;
            //    }
            //}



            //DisplayFormBoMFinshingOnSreen(soInfo, fCurrent, fParent);





        }


        public static DataTable TableTreeviewFinishingTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("CNYMF016PK", typeof(Int64));
            dt.Columns.Add("StepWork", typeof(string));
            dt.Columns.Add("PKCode", typeof(Int64));
            dt.Columns.Add("RMCode_001", typeof(string));
            dt.Columns.Add("RMDescription_002", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("RMGroup_066", typeof(string));
            dt.Columns.Add("QualityCode", typeof(string));
            dt.Columns.Add("RateGram", typeof(decimal));
            dt.Columns.Add("RatePercent", typeof(decimal));
            dt.Columns.Add("UC", typeof(decimal));
            dt.Columns.Add("Tolerance", typeof(decimal));
            dt.Columns.Add("Note", typeof(string));
            dt.Columns.Add("RowState", typeof(string));
            dt.Columns.Add("ItemLevel", typeof(int));
            dt.Columns.Add("SortOrderNode", typeof(Int32));
            dt.Columns.Add("ChildPK", typeof(Int64));
            dt.Columns.Add("ParentPK", typeof(Int64));
            dt.Columns.Add("Supplier", typeof(string));
            dt.Columns.Add("SupplierPK", typeof(Int64));
            dt.Columns.Add("SupplierRef", typeof(string));
            dt.Columns.Add("TDG00004PK", typeof(Int64));
            dt.Columns.Add("AllowUpdate", typeof(bool));
            dt.Columns.Add("TableCode", typeof(string));
            dt.Columns.Add("PurchaseType", typeof(string));
            return dt;
        }

        public static void VisibleTreeColumnSortPaint(TreeList tl)
        {
            List<VisibleColBomInfo> l = new List<VisibleColBomInfo>
            {
                new VisibleColBomInfo("CNYMF016PK", false),
                new VisibleColBomInfo("StepWork", true),
                new VisibleColBomInfo("PKCode", false),
                new VisibleColBomInfo("RMCode_001", true),
                new VisibleColBomInfo("RMDescription_002", true),
                new VisibleColBomInfo("Unit", true),
                new VisibleColBomInfo("RMGroup_066", true),
                new VisibleColBomInfo("QualityCode", true),
                new VisibleColBomInfo("RateGram", true),
                new VisibleColBomInfo("RatePercent", true),
                new VisibleColBomInfo("UC", true),
                new VisibleColBomInfo("Tolerance", true),
                new VisibleColBomInfo("PurchaseType", true),
                new VisibleColBomInfo("Supplier", true),
                new VisibleColBomInfo("SupplierRef", true),
                new VisibleColBomInfo("Note", true),
                new VisibleColBomInfo("RowState", false),
                new VisibleColBomInfo("ItemLevel", false),
                new VisibleColBomInfo("SortOrderNode", false),

              
                new VisibleColBomInfo("SupplierPK", false),
                new VisibleColBomInfo("TDG00004PK", false),
                new VisibleColBomInfo("AllowUpdate", false),
                new VisibleColBomInfo("TableCode", false),

        };














            var qUnVisible = l.Where(p => !p.Visible).Select(p => p.FieldName).ToList();
            var qVisible = l.Where(p => p.Visible).Select(p => p.FieldName).ToList();

            for (int i = 0; i < qVisible.Count; i++)
            {
                TreeListColumn colV = tl.Columns[qVisible[i]];
                if(colV==null)continue;
                colV.VisibleIndex = i;
            }
            foreach (string s in qUnVisible)
            {
                TreeListColumn colU = tl.Columns[s];
                if (colU == null) continue;
                colU.Visible = false;

            }

        }


    }
}
