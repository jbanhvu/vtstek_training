using CNY_BaseSys.Common;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CNY_Report
{
    public partial class Frm003ProductionOrder : Form
    {
        #region "Property"
        private readonly RepositoryItemMemoEdit _repositoryTextNormal;
        private readonly RepositoryItemMemoEdit _repositoryTextGrid;
        #endregion
        private Int64 pk;
        //DataTable _dtPrint;

        public Frm003ProductionOrder(Int64 _PK)
        {
          
            InitializeComponent();
            pk = _PK;
            //_dtPrint = dtPrint;

            _repositoryTextGrid = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            _repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            //LoadDataTreeList(tlMain, _dtPrint);

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Print();
        }

        public void Print()
        {
            Rpt003ProductionOrder report = new Rpt003ProductionOrder(pk);
            ReportPrintTool pt = new ReportPrintTool(report);
            Form form = pt.PreviewForm;
            form.MdiParent = this.ParentForm;
            pt.ShowPreview();
            form.Show();
        }

        private void LoadDataTreeList(TreeList tl, DataTable dtS)
        {
            // Int64 pkColor key, Int64 cny00050Pk value




            tl.BeginUpdate();


            tl.Bands.Clear();
            tl.Columns.Clear();
            tl.DataSource = null;
            CreateBandedTreeHeader(tl, dtS);
            tl.DataSource = dtS;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";





            tl.ExpandAll();

            tl.ForceInitialize();


            tl.BeginSort();
            tl.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
            tl.EndSort();
            tl.EndUpdate();

            tl.BeginUpdate();
            tl.OptionsView.ShowColumns = true;
            tl.OptionsView.ShowBandsMode = DefaultBoolean.False;
            tl.BestFitColumns();
            if (tl.Columns["RMDescription_002"].Visible && tl.Columns["RMDescription_002"].Width > 150)
                tl.Columns["RMDescription_002"].Width = 150;
            if (tl.Columns["MainMaterial"].Visible && tl.Columns["MainMaterial"].Width > 190)
                tl.Columns["MainMaterial"].Width = 190;
            if (tl.Columns["FinishingColor"].Visible && tl.Columns["FinishingColor"].Width > 190)
                tl.Columns["FinishingColor"].Width = 190;
            if (tl.Columns["Note"].Visible && tl.Columns["Note"].Width > 190)
                tl.Columns["Note"].Width = 190;

            tl.OptionsView.ShowBandsMode = DefaultBoolean.True;
            tl.OptionsView.ShowColumns = false;
            tl.EndUpdate();



            //RepositoryItemMemoEdit repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true };
            //tl.Columns["RMName"].ColumnEdit = repositoryTextNormal;
            //tl.Columns["Note"].ColumnEdit = repositoryTextNormal;






        }
        private void CreateBandedTreeHeader(TreeList tl, DataTable dtS)
        {
            TreeListBand[] arrBand = new TreeListBand[dtS.Columns.Count - 2];

            int i = 0;
            foreach (DataColumn col in dtS.Columns)
            {

                string colName = col.ColumnName;
                if (colName == "ChildPK" || colName == "ParentPK") continue;
                TreeListColumn gCol = new TreeListColumn();
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i] = new TreeListBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;

                HorzAlignment hoz = HorzAlignment.Near;
                string displayText = "";
                string displayFormat = "";
                FormatType formatType = FormatType.None;
                string tag = "";
                string colCaption = "";


             
                    if (colName == "STT")
                    {
                        displayText = "STT";
                        colCaption = "STT";
                        hoz = HorzAlignment.Center;
                        // gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("MaHang"))
                    {
                        displayText = "Mã Hàng";
                        colCaption = "Mã Hàng";
                        hoz = HorzAlignment.Center;
                    }
                    else if (colName.Equals("MaKhacHang"))
                    {
                        displayText = "Mã Khách Hàng";
                        colCaption = "Mã Khách Hàng";
                        hoz = HorzAlignment.Near;
                        //  gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("TenSP"))
                    {
                        displayText = @"Tên SP";
                        colCaption = @"Tên SP";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("Reference"))
                    {
                        displayText = "Mã KH";
                        colCaption = "Mã KH";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("HinhSP"))
                    {
                        displayText = "Hình SP";
                        colCaption = "Hình SP";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("DVT"))
                    {
                        displayText = "DVT";
                        colCaption = "DVT";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("SoLuong"))
                    {
                        displayText = "SoLuong";
                        colCaption = "SoLuong";
                        hoz = HorzAlignment.Center;
                    formatType = FormatType.Numeric;
                    displayFormat = "N0";
                    //gCol.ColumnEdit = _repositoryTextNormal;
                }
                    else if (colName.Equals("SoLuongMauSale"))
                    {
                        displayText = "SoLuongMauSale";
                        colCaption = "SoLuongMauSale";
                        hoz = HorzAlignment.Center;
                        formatType = FormatType.Numeric;
                        displayFormat = "N0";
                    }
                    else if (colName.Equals("NguyenLieu"))
                    {
                        displayText = "NguyenLieu";
                        colCaption = "NguyenLieu";
                        hoz = HorzAlignment.Center;
                      
                    }
                    else if (colName.Equals("MauHoanThien"))
                    {
                        displayText = "MauHoanThien";
                        colCaption = "MauHoanThien";
                        hoz = HorzAlignment.Center;
                    }
                    else if (colName.Equals("GhiChu"))
                    {
                        displayText = "GhiChu";
                        colCaption = "GhiChu";
                        hoz = HorzAlignment.Center;
                    }
                    else if (colName.Equals("MauHoanThienBK"))
                    {
                        displayText = "MauHoanThienBK";
                        colCaption = "MauHoanThienBK";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;

                    }
                else if (colName.Equals("NguyenLieuBK"))
                {
                    displayText = "NguyenLieuBK";
                    colCaption = "NguyenLieuBK";
                    hoz = HorzAlignment.Center;
                }
                else if (colName.Equals("SoLuongMauSaleBK"))
                {
                    displayText = "SoLuongMauSaleBK";
                    colCaption = "SoLuongMauSaleBK";
                    hoz = HorzAlignment.Center;
                }
                else if (colName.Equals("GhiChuBK"))
                {
                    displayText = "GhiChuBK";
                    colCaption = "GhiChuBK";
                    hoz = HorzAlignment.Center;
                }


                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceHeader.TextOptions.HAlignment = hoz;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = hoz;
                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                gCol.AppearanceHeader.TextOptions.HAlignment = hoz;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = hoz;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                arrBand[i].Name = tag;
                // arrBand[i].RowCount = 2;
                gCol.OptionsColumn.ReadOnly = false;
                gCol.Tag = tag;
                gCol.Caption = string.Format("<b>{0}</b>", colCaption);
                gCol.FieldName = colName;
                gCol.Visible = true;
                gCol.VisibleIndex = i;
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                if (displayFormat != "")
                {
                    gCol.Format.FormatType = formatType;
                    gCol.Format.FormatString = displayFormat;
                }



                tl.Columns.Add(gCol);
                arrBand[i].Caption = string.Format("<b>{0}</b>", displayText);
                arrBand[i].Columns.Add(gCol);
                arrBand[i].Visible = true;

                // arrBand[i].Width = 100;

                i++;
            }


            const int fristBandNo = 5;


            for (int e = 0; e < fristBandNo; e++)
            {
                tl.Bands.Add(arrBand[e]);
            }




            //List<string> l = new List<string>();
            //l.Add("%%%%%");

            //int sumPaint = 0;


            //Dictionary<string, BoMPrintTextInfo> dicColFinal = dicCol.Where(p => p.Value.BandCount > 1).ToDictionary(s => s.Key, s => s.Value);

            //foreach (string tagLoop in l)
            //{
            //    string sName = "Product Size";

            //    TreeListBand gParent = new TreeListBand();
            //    gParent.AppearanceHeader.Options.UseTextOptions = true;
            //    gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //    gParent.Caption = string.Format("<b>{0}</b>", sName);
            //    //  gParent.Width = 100;
            //    var q2 = arrBand.Where(p => ProcessGeneral.GetSafeString(p.Name) == tagLoop).ToArray();
            //    int len = q2.Length;
            //    sumPaint += len;
            //    if (len > 0)
            //    {
            //        foreach (TreeListBand t1 in q2)
            //        {
            //            string fieldNamG = t1.Columns[0].FieldName;
            //            BoMPrintTextInfo infoG;
            //            if (dicColFinal.TryGetValue(fieldNamG, out infoG))
            //            {
            //                TreeListBand parentBand = gParent;
            //                List<string> lStr = infoG.ListStrHeader;
            //                foreach (string t in lStr)
            //                {
            //                    parentBand = AddChildBand(parentBand, t);
            //                }
            //                parentBand.Bands.Add(t1);
            //            }
            //            else
            //            {
            //                gParent.Bands.Add(t1);
            //            }

            //            // t1.Columns.Add();

            //            //  t1.RowCount = bandRowCount;

            //        }
            //        tl.Bands.Add(gParent);
            //    }


            //}

            //int beginPaint = sumPaint + fristBandNo;


            //for (int m = beginPaint; m < arrBand.Length; m++)
            //{
            //    tl.Bands.Add(arrBand[m]);
            //}
            //List<string> lHide = new List<string>();
            //lHide.Add("ItemType");
            //lHide.Add("RMCode_001");
            //lHide.Add("SortOrderNode");
            //var qHideBand = tl.Bands.Where(p => !p.HasChildren).Where(p => lHide.Any(s => s == p.Columns[0].FieldName)).OrderByDescending(p => p.Index).ToList();
            //foreach (TreeListBand bandHide in qHideBand)
            //{
            //    bandHide.Visible = false;
            //}


        }

    }
}
