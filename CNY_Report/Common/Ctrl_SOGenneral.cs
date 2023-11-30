using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;
using DevExpress.XtraTreeList;
using CNY_Report.Class;

namespace CNY_Report.Common
{
    public static class Ctrl_SOGenneral
    {

        public static DataTable TableAgreeTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("PKParent", typeof(Int64));
            dt.Columns.Add("IndexNo", typeof(String));
            dt.Columns.Add("Name", typeof(String));
            return dt;
        }
        public static string StrFormatSalePriceDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(SoContant.FormatSalePriceDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(SoContant.FormatSalePriceDecimal, isN);
        }
        public static string StrFormatOrderQtyDecimal(bool isN, bool isFooter)
        {
            if (isFooter)
                return ProcessGeneral.FormatNumberPatternFooter(SoContant.FormatOrderQtyDecimal, isN);
            return ProcessGeneral.FormatNumberPattern(SoContant.FormatOrderQtyDecimal, isN);
        }
        public static DataTable TableTreeviewTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("ItemType", typeof(string));
            dt.Columns.Add("SOLine", typeof(string));
            dt.Columns.Add("PKCode", typeof(Int64));
            dt.Columns.Add("RMCode_001", typeof(string));
            dt.Columns.Add("RMDescription_002", typeof(string));



            dt.Columns.Add("Reference", typeof(string));
            dt.Columns.Add("Dimension", typeof(string));


            dt.Columns.Add("MainMaterial", typeof(string));
            dt.Columns.Add("FinishingColor", typeof(string));

            dt.Columns.Add("OrderQty", typeof(Int32));
            dt.Columns.Add("SalePrice", typeof(double));

            dt.Columns.Add("Amount", typeof(double));
            dt.Columns.Add("SaleUnit", typeof(string));
            dt.Columns.Add("CustSOLine", typeof(string));

            dt.Columns.Add("CustETD", typeof(DateTime));
            dt.Columns.Add("DeliveryConf", typeof(DateTime));

            dt.Columns.Add("TypeOfFitting", typeof(string));
            dt.Columns.Add("BoMNo", typeof(string));
            dt.Columns.Add("Note", typeof(string));

            dt.Columns.Add("RowState", typeof(string));
            dt.Columns.Add("SortOrderNode", typeof(Int32));

            dt.Columns.Add("ChildPK", typeof(Int64));
            dt.Columns.Add("ParentPK", typeof(Int64));
            dt.Columns.Add("AllowUpdate", typeof(bool));
            dt.Columns.Add("CodeLevel", typeof(int));
            dt.Columns.Add("AllowChildSameLevel", typeof(bool));
            return dt;
        }

        public static DataTable TableFinishingTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("PK", typeof(Int64));
            dt.Columns.Add("ColorCode", typeof(string));
            dt.Columns.Add("ColorDesc", typeof(string));
            dt.Columns.Add("FinishingColor", typeof(string));
            dt.Columns.Add("RowState", typeof(string));
            dt.Columns.Add("AllowUpdate", typeof(bool));
            return dt;
        }



        public static DataTable TableFinishingSoLineTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("CNY00050PK", typeof(Int64));
            dt.Columns.Add("FinishingColor", typeof(string));
            dt.Columns.Add("PartNoDesc", typeof(string));
            dt.Columns.Add("PK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
            return dt;
        }

        public static DataTable TableBoMSoLineTemplate()
        {
            var dt = new DataTable();
            dt.Columns.Add("CNY00012PK", typeof(Int64));
            dt.Columns.Add("TDG00001PK", typeof(Int64));
            dt.Columns.Add("BoMNo", typeof(Int32));
            dt.Columns.Add("PK", typeof(Int64));
            dt.Columns.Add("RowState", typeof(string));
            return dt;
        }

        public static void VisibleTreeColumnSort(TreeList tl, List<string> arrAttribute)
        {
            List<VisibleColBomInfo> l = new List<VisibleColBomInfo>();


            l.Add(new VisibleColBomInfo("ItemType", false));
            l.Add(new VisibleColBomInfo("SOLine", false));
            l.Add(new VisibleColBomInfo("PKCode", false));
            l.Add(new VisibleColBomInfo("RMCode_001", false));
            l.Add(new VisibleColBomInfo("RMDescription_002", true));


            l.Add(new VisibleColBomInfo("Reference", true));

            foreach (string s in arrAttribute)
            {
                l.Add(new VisibleColBomInfo(s, true));
            }

            l.Add(new VisibleColBomInfo("Dimension", false));
            l.Add(new VisibleColBomInfo("MainMaterial", true));


            l.Add(new VisibleColBomInfo("FinishingColor", true));
            l.Add(new VisibleColBomInfo("TypeOfFitting", true));
            l.Add(new VisibleColBomInfo("OrderQty", true));

            l.Add(new VisibleColBomInfo("SalePrice", true));

            l.Add(new VisibleColBomInfo("Amount", true));

            l.Add(new VisibleColBomInfo("SaleUnit", true));
            l.Add(new VisibleColBomInfo("CustSOLine", false));



            l.Add(new VisibleColBomInfo("CustETD", false));


            l.Add(new VisibleColBomInfo("DeliveryConf", false));

        
            l.Add(new VisibleColBomInfo("BoMNo", false));

            l.Add(new VisibleColBomInfo("Note", true));
            l.Add(new VisibleColBomInfo("RowState", false));


            l.Add(new VisibleColBomInfo("SortOrderNode", false));
            l.Add(new VisibleColBomInfo("AllowUpdate", false));
            l.Add(new VisibleColBomInfo("CodeLevel", false));
            l.Add(new VisibleColBomInfo("AllowChildSameLevel", false));

            var qUnVisible = l.Where(p => !p.Visible).Select(p => p.FieldName).ToList();
            var qVisible = l.Where(p => p.Visible).Select(p => p.FieldName).ToList();

            for (int i = 0; i < qVisible.Count; i++)
            {
                tl.Columns[qVisible[i]].VisibleIndex = i;

            }
            foreach (string s in qUnVisible)
            {

                tl.Columns[s].Visible = false;


            }

        }
    }
}
