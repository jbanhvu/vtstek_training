using CNY_BaseSys.Common;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_Report.Info;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using CNY_BaseSys.Class;
using CNY_Report.Class;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using CNY_Report.Common;
using CNY_Report.Properties;
using DevExpress.XtraTreeList.Nodes;

namespace CNY_Report
{
    public partial class Frm001SOReport : Form
    {
        private readonly RepositoryItemMemoEdit _repositoryTextNormal;
        private readonly RepositoryItemMemoEdit _repositoryTextGrid;
        private  RepositoryItemRichTextEdit _repositoryRichTextEdit;
        private readonly RepositoryItemPictureEdit _repositoryPictureEdit;
     
        private Int64 _soHeaderPk;
        private Inf_001SOReport _inf = new Inf_001SOReport();
        DataTable tb_Att = new DataTable();
        private readonly DataSet _dsData;
        private readonly Dictionary<string, string> _dicUnit;public Frm001SOReport(Int64 _PK, DataSet dsDataPara, Dictionary<string, string> dicUnit)
        {
            InitializeComponent();
            _dsData = dsDataPara;
            _soHeaderPk = _PK;
            _dicUnit = dicUnit;
            //_repositoryPictureEdit = new RepositoryItemPictureEdit { AutoHeight = false, PictureAlignment = ContentAlignment.MiddleCenter,SizeMode=DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze  };
            //_repositoryPictureEdit = new RepositoryItemPictureEdit { AutoHeight = false, PictureAlignment = ContentAlignment.MiddleCenter,SizeMode=DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch  };
            _repositoryPictureEdit = new RepositoryItemPictureEdit { AutoHeight = false, PictureAlignment = ContentAlignment.MiddleCenter,SizeMode=DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze ,
            
            };
            _repositoryTextGrid = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            _repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            _repositoryRichTextEdit = new RepositoryItemRichTextEdit { AutoHeight = false, MaxHeight = 1000, VerticalIndent=10,HorizontalIndent= 10}; //CustomHeight = 500 };


            InitTreeList(tlMain);

            if(_dsData.Tables[0].Rows.Count>0)
            {
                LoadDataTreeList(tlMain, _dsData.Tables[0], dsDataPara.Tables[1]);
            }
           

            InitTreeListServices(tlServices);
            if(_dsData.Tables[2].Rows.Count>0)
            {
                LoadDataTreeListServices(tlServices, _dsData.Tables[2], dsDataPara.Tables[1]);
            }
        }



        #region "Init TreeList"




     

        private void LoadDataTreeList(TreeList tl, DataTable dtS,DataTable dtSHeader)
        {
            // Int64 pkColor key, Int64 cny00050Pk value

           


            tl.BeginUpdate();


            tl.Bands.Clear();
            tl.Columns.Clear();
            tl.DataSource = null;
          
          
            CreateBandedTreeHeader(tl, dtS, dtSHeader);
            tl.DataSource = dtS;
            //_repositoryRichTextEdit = new OrderRowFreeTextRepositoryItemFactory(tl).Create();
            //tl.Columns["MainMaterial"].ColumnEdit = _repositoryRichTextEdit;
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
            if (tl.Columns["RMDescription_002"].Visible && tl.Columns["RMDescription_002"].Width > 160)
                tl.Columns["RMDescription_002"].Width = 160;
            if (tl.Columns["MainMaterial"].Visible && tl.Columns["MainMaterial"].Width > 250)
                tl.Columns["MainMaterial"].Width = 260;
            tl.RepositoryItems.Add(_repositoryRichTextEdit);
            tl.Columns["MainMaterial"].ColumnEdit = _repositoryRichTextEdit;
            //tl.Columns["MainMaterial"].AppearanceCell.Options.UseTextOptions = true;
            //tl.Columns["MainMaterial"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            //tl.Columns["MainMaterial"].AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
            //tl.Columns["MainMaterial"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            //tl.Columns["MainMaterial"].AppearanceCell.TextOptions.Trimming = Trimming.None;
            //((MyTreeListNode)tl.Nodes[6]).Height = 100;
            if (tl.Columns["ImageCode"].Visible && tl.Columns["ImageCode"].Width > 100)
                tl.Columns["ImageCode"].Width = 100;
           
            //tl.Columns["MainMaterial"].ColumnEdit =;
            if (tl.Columns["FinishingColor"].Visible && tl.Columns["FinishingColor"].Width > 110)
                tl.Columns["FinishingColor"].Width = 110;
            //if (tl.Columns["Amount"].Visible && tl.Columns["Amount"].Width > 100)
                tl.Columns["Amount"].Width = 100;
            tl.Columns["OrderQty"].Width = 75;
            tl.Columns["SalePrice"].Width = 75;
            if (tl.Columns["Note"].Visible && tl.Columns["Note"].Width > 100)
                tl.Columns["Note"].Width = 100;



            tl.Columns["Amount"].SummaryFooter = SummaryItemType.Sum;
            tl.Columns["Amount"].SummaryFooterStrFormat = Ctrl_SOGenneral.StrFormatSalePriceDecimal(false,true);
            tl.Columns["SalePrice"].SummaryFooter = SummaryItemType.Count;
            tl.Columns["SalePrice"].SummaryFooterStrFormat = @"Total :";
            tl.Columns["OrderQty"].SummaryFooter = SummaryItemType.Sum;
            tl.Columns["OrderQty"].SummaryFooterStrFormat = Ctrl_SOGenneral.StrFormatSalePriceDecimal(false, true);
            //tl.Columns["Currency"].SummaryFooter = SummaryItemType.Count;
            //tl.Columns["Currency"].SummaryFooterStrFormat = string.Format("{0}",ProcessGeneral.GetSafeString(dtS.Rows[0]["Currency"])) ;
            tl.OptionsView.ShowBandsMode = DefaultBoolean.True;
            tl.OptionsView.ShowColumns = false;
            
            tl.EndUpdate();

            

            //RepositoryItemMemoEdit repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true };

            //tl.Columns["Note"].ColumnEdit = repositoryTextNormal;






        }


        private Dictionary<string, BoMPrintTextInfo> GetColCaptionCrossTab(DataTable dtS)
        {
            Dictionary<string, BoMPrintTextInfo> dicCol = new Dictionary<string, BoMPrintTextInfo>();

            var qCol = dtS.Columns.Cast<DataColumn>().Select(p => new
            {
                p.ColumnName,
                TypeCol = p.ColumnName.Contains("%%%%%") ? "%%%%%" : "",
            }).Where(p => p.TypeCol != "").ToList();




            foreach (var item in qCol)
            {
                string colName = item.ColumnName;
                string typeCol = item.TypeCol;
                string displayText = "";


                string[] arrTag = colName.Split(new[] { typeCol }, StringSplitOptions.RemoveEmptyEntries);

                string unit = "";
                _dicUnit.TryGetValue(colName, out unit);
                if (string.IsNullOrEmpty(unit))
                {
                    displayText = arrTag[1];
                }
                else
                {

                    unit = string.Format("({0})", unit);
                    displayText = string.Format("{0} {1}", arrTag[1], unit);

                }



                List<String> qSplit = displayText.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Where(p => !string.IsNullOrEmpty(p.Trim())).Select(p => p.Trim()).ToList();
                int count = qSplit.Count;
                List<String> lFinal = new List<string>();
                string text = "";
                string caption = "";
                for (int i = 0; i < count; i++)
                {
                    string sSplit = qSplit[i];
                    if (sSplit.Length > caption.Length)
                    {
                        caption = sSplit;
                    }
                    if (i == count - 1)
                    {
                        text = sSplit;
                    }
                    else
                    {
                        lFinal.Add(sSplit);
                    }
                }
                BoMPrintTextInfo info = new BoMPrintTextInfo
                {
                    Caption = caption,
                    DisplayText = text,
                    Tag = typeCol,
                    BandCount = count,
                    ListStrHeader = lFinal
                };
                dicCol.Add(colName, info);



            }

            return dicCol;




        }


        private void CreateBandedTreeHeader(TreeList tl, DataTable dtS,DataTable dtSHeader)
        {
            string sCurency = ProcessGeneral.GetSafeString(dtSHeader.Rows[0]["Currency_Des"]);

            Dictionary<string, BoMPrintTextInfo> dicCol = GetColCaptionCrossTab(dtS);
            string ServicesCaption =  "(1) Product Items";
            TreeListBand gParentTop = new TreeListBand();

            //gParentTop.AppearanceHeader.Font.Bold = Font.Bold;

            gParentTop.Caption = ServicesCaption;
            gParentTop.AppearanceHeader.Options.UseFont = true;
            gParentTop.AppearanceHeader.Options.UseTextOptions = true;
            gParentTop.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gParentTop.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            gParentTop.AppearanceHeader.Font = new Font("Times New Roman", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));



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
                FormatType formatType = FormatType.Custom;
                string tag = "";
                string colCaption = "";


                BoMPrintTextInfo infoItem;
                if (dicCol.TryGetValue(colName, out infoItem))
                {
                    tag = infoItem.Tag;
                    displayText = infoItem.DisplayText;
                    colCaption = infoItem.Caption;
                    hoz = HorzAlignment.Center;
                }
                else
                {
                    if (colName == "STT")
                    {
                        displayText = "No.";
                        colCaption = "No.";
                        hoz = HorzAlignment.Center;
                       // gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("ItemType"))
                    {
                        displayText = "Item Type";
                        colCaption = "Item Type";
                        hoz = HorzAlignment.Center;
                    }
                    else if (colName.Equals("RMCode_001"))
                    {
                        displayText = "Item Code";
                        colCaption = "Item Code";
                        hoz = HorzAlignment.Near;
                      //  gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("RMDescription_002"))
                    {
                        displayText = "Description";
                        colCaption = "Description";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("Reference"))
                    {
                        displayText = "Reference";
                        colCaption = "Reference";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;

                    }
                    else if (colName.Equals("ImageCode"))
                    {
                        displayText = "Image";
                        colCaption = "Image";
                        hoz = HorzAlignment.Center;
                        gCol.ColumnEdit = _repositoryPictureEdit;
                        //gCol.AppearanceCell.Image=Image.
                    }
                    else if (colName.Equals("MainMaterial"))
                    {
                        displayText = "Main Material";
                        colCaption = "Main Material";
                        hoz = HorzAlignment.Near;
                        
                        gCol.ColumnEdit = _repositoryRichTextEdit;

                    }
                    else if (colName.Equals("FinishingColor"))
                    {
                        displayText = "Finishing Color";
                        colCaption = "Finishing Color";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("TypeOfFitting"))
                    {
                        displayText = "Type Of Fitting";
                        colCaption = "Type";
                        hoz = HorzAlignment.Center;
                        //gCol.ColumnEdit = _repositoryTextNormal;
                    }
                    else if (colName.Equals("OrderQty"))
                    {
                        displayText = "Qty";
                        colCaption = "Qty";
                        hoz = HorzAlignment.Far;
                        formatType = FormatType.Numeric;
                        displayFormat = "N0";
                    }
                    else if (colName.Equals("SalePrice"))
                    {
                        displayText = string.Format("Price \n({0})", sCurency);
                        colCaption = "Price";
                        hoz = HorzAlignment.Far;
                        formatType = FormatType.Numeric;
                        displayFormat = "#,0.##########";
                    }
                    else if (colName.Equals("Amount"))
                    {
                        displayText = string.Format("Amount \n({0})", sCurency);
                        colCaption = "USD";
                        hoz = HorzAlignment.Far;
                        formatType = FormatType.Numeric;
                        displayFormat = "#,0.##########";
                    }
                    //else if (colName.Equals("Currency"))
                    //{
                    //    displayText = "Currency";
                    //    colCaption = "Currency";
                    //    hoz = HorzAlignment.Center;
                    //}
                    else if (colName.Equals("SaleUnit"))
                    {
                        displayText = "Unit";
                        colCaption = "Unit";
                        hoz = HorzAlignment.Near;
                    }
                    else if (colName.Equals("Note"))
                    {
                        displayText = "Remark";
                        colCaption = "Remark";
                        hoz = HorzAlignment.Near;
                        gCol.ColumnEdit = _repositoryRichTextEdit;

                    }
                    else if (colName.Equals("SortOrderNode"))
                    {
                        displayText = "SortOrderNode";
                        colCaption = "SortOrderNode";
                        hoz = HorzAlignment.Center;
                    }
                }
               

                gCol.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                gCol.AppearanceHeader.TextOptions.HAlignment = hoz;
              
                gCol.AppearanceCell.TextOptions.HAlignment = hoz;
                gCol.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                gCol.AppearanceCell.TextOptions.Trimming = Trimming.None;
                arrBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

                arrBand[i].AppearanceHeader.Options.UseFont = true;
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                //arrBand[i].AppearanceHeader.TextOptions.HAlignment = hoz;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
               
                arrBand[i].Name = tag;
                arrBand[i].AppearanceHeader.Font = new Font("Times New Roman", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                // arrBand[i].RowCount = 2;
                gCol.OptionsColumn.ReadOnly = false;
                gCol.Tag = tag;
                gCol.Caption = string.Format("<b>{0}</b>", colCaption);
                gCol.FieldName = colName;
                gCol.Visible = true;
                gCol.VisibleIndex = i;
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                gCol.AppearanceCell.TextOptions.Trimming = Trimming.None;
                if (displayFormat != "")
                {
                    gCol.Format.FormatType = formatType;
                    gCol.Format.FormatString = displayFormat;
                }
                //if (colName.Equals("Amount"))
                //{
                //    tl.Columns["Amount"].SummaryFooterStrFormat = Ctrl_SOGenneral.StrFormatOrderQtyDecimal(false, true);
                //}


                    tl.Columns.Add(gCol);
                arrBand[i].Caption = string.Format("<b>{0}</b>", displayText);
                arrBand[i].Columns.Add(gCol);
                arrBand[i].Visible = true;

                // arrBand[i].Width = 100;

                i++;
            }


            const int fristBandNo = 6;


            for (int e = 0; e < fristBandNo; e++)
            {
                ////tl.Bands.Add(arrBand[e]);
                gParentTop.Bands.Add(arrBand[e]);
            }




            List<string> l = new List<string>();
            l.Add("%%%%%");

            int sumPaint = 0;


            Dictionary<string, BoMPrintTextInfo> dicColFinal = dicCol.Where(p => p.Value.BandCount > 1).ToDictionary(s => s.Key, s => s.Value);

            foreach (string tagLoop in l)
            {
                string sName = "Product Size";
              
                TreeListBand gParent = new TreeListBand();
                gParent.AppearanceHeader.Options.UseTextOptions = true;
                gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gParent.Caption = string.Format("<b>{0}</b>", sName);
                gParent.AppearanceHeader.Options.UseFont = true;
                gParent.AppearanceHeader.Options.UseTextOptions = true;
                gParent.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                
                gParent.AppearanceHeader.Font = new Font("Times New Roman", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //  gParent.Width = 100;
                var q2 = arrBand.Where(p => ProcessGeneral.GetSafeString(p.Name) == tagLoop).ToArray();
                int len = q2.Length;
                sumPaint += len;
                if (len > 0)
                {


                    //1

                    foreach (TreeListBand t1 in q2)
                    {
                        string fieldNamG = t1.Columns[0].FieldName;
                        BoMPrintTextInfo infoG;
                        if (dicColFinal.TryGetValue(fieldNamG, out infoG))
                        {
                            TreeListBand parentBand = gParent;
                            parentBand.AppearanceHeader.Options.UseFont = true;
                            parentBand.AppearanceHeader.Options.UseTextOptions = true;
                            parentBand.AppearanceHeader.Font = new Font("Times New Roman", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                            List<string> lStr = infoG.ListStrHeader;
                            foreach (string t in lStr)
                            {
                                parentBand = AddChildBand(parentBand, t.Substring(0,1));
                            }
                            parentBand.Bands.Add(t1);
                        }
                        else
                        {
                            gParent.Bands.Add(t1);
                        }

                        // t1.Columns.Add();

                        //  t1.RowCount = bandRowCount;

                    }
                    gParentTop.Bands.Add(gParent);
                    //tl.Bands.Add(gParent);
                }


            }

            int beginPaint = sumPaint + fristBandNo;


            for (int m = beginPaint; m < arrBand.Length; m++)
            {
                //tl.Bands.Add(arrBand[m]);
                gParentTop.Bands.Add(arrBand[m]);
            }
            tl.Bands.Add(gParentTop);
            List<string> lHide = new List<string>();
            lHide.Add("ItemType");
            lHide.Add("RMCode_001");
            lHide.Add("SortOrderNode");
            var qHideBand = gParentTop.Bands.Where(p => !p.HasChildren).Where(p => lHide.Any(s => s == p.Columns[0].FieldName)).OrderByDescending(p => p.Index).ToList();
            foreach (TreeListBand bandHide in qHideBand)
            {
                bandHide.Visible = false;
            }

           
        }





        private TreeListBand AddChildBand(TreeListBand rootBand, string sItem)
        {
            TreeListBand gChild = new TreeListBand();
            gChild.AppearanceHeader.Options.UseTextOptions = true;
            gChild.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gChild.Caption = string.Format("<b>{0}</b>", sItem);
            return rootBand.Bands.Add(gChild);
        }


        private void InitTreeList(TreeList treeList)
        {


            treeList.OptionsView.ShowBandsMode = DefaultBoolean.True;

            treeList.OptionsView.AllowHtmlDrawHeaders = true;

            treeList.OptionsView.AllowBandColumnsMultiRow = false;

            treeList.OptionsView.ShowColumns = false;
            treeList.OptionsView.AutoWidth = false;
           
           treeList.OptionsPrint.UsePrintStyles = false;



            treeList.OptionsPrint.PrintAllNodes = true;
            treeList.OptionsPrint.PrintBandHeader = true;
            treeList.OptionsPrint.PrintCheckBoxes = false;
            treeList.OptionsPrint.PrintFilledTreeIndent = false;
            treeList.OptionsPrint.PrintHorzLines = true;
            treeList.OptionsPrint.PrintImages = true;
            treeList.OptionsPrint.PrintPageHeader = false;
            treeList.OptionsPrint.AutoRowHeight = true;
            treeList.OptionsPrint.AutoWidth = true;
            treeList.OptionsPrint.PrintTree = true;
            treeList.OptionsPrint.PrintTreeButtons = false;
            treeList.OptionsPrint.PrintVertLines = true;

            treeList.OptionsPrint.PrintReportFooter = true;
            treeList.OptionsPrint.PrintRowFooterSummary = true;
            treeList.AppearancePrint.Row.TextOptions.WordWrap = WordWrap.Wrap;
            //   treeList.AppearancePrint.Lines.BackColor = Color.Red;

            //tlMain.AppearancePrint.Row.Options.UseTextOptions = true;
            //tlMain.AppearancePrint.Row.Options.UseFont = true;
            //tlMain.AppearancePrint.Row.Options.UseBackColor = true;
            //tlMain.AppearancePrint.Row.TextOptions.WordWrap = WordWrap.Wrap;

            treeList.OptionsBehavior.AutoNodeHeight = true;
            treeList.OptionsBehavior.Editable = false;
            treeList.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

           


            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = true;
            treeList.OptionsFind.AlwaysVisible = false;
            treeList.OptionsFind.ShowCloseButton = true;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsView.ShowAutoFilterRow = false;



            treeList.OptionsView.ShowHorzLines = true;
            treeList.OptionsView.ShowVertLines = true;
            treeList.OptionsView.ShowIndicator = true;

            treeList.OptionsView.EnableAppearanceEvenRow = false;
            treeList.OptionsView.EnableAppearanceOddRow = false;

            treeList.OptionsBehavior.AutoChangeParent = false;



            treeList.OptionsView.ShowSummaryFooter = false;

            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = false;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;







            //new TreeListMultiCellSelector(treeList, true)
            //{
            //    AllowSort = false,
            //    FilterShowChild = true,

            //};








            treeList.NodeCellStyle += tlMain_NodeCellStyle;
            //     treeList.CalcNodeHeight += TreeList_CalcNodeHeight;
            treeList.GetNodeDisplayValue += TreeList_GetNodeDisplayValue;
            //  treeList.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;
       








        }



        private void TreeList_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
        {

            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            if (!col.Visible) return;
            if (fieldName != "STT") return;
            string value = (tl.GetVisibleIndexByNode(node) + 1).ToString().Trim();
            e.Value = value;
        }




        private void tlMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
           

            if(node.ParentNode == null)
            {
                if(node.HasChildren)
                {
                    e.Appearance.Font = new Font("Times New Roman", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkOrange;
                }
                else
                {
                    e.Appearance.Font = new Font("Times New Roman", 8F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.Black;
                }
              
            }
            else
            {
                e.Appearance.Font = new Font("Times New Roman", 7F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }



          
        }

        #endregion

        #region "Init TreeList Services"






        private void LoadDataTreeListServices(TreeList tl, DataTable dtS, DataTable dtSHeader)
        {
            // Int64 pkColor key, Int64 cny00050Pk value




            tl.BeginUpdate();


            tl.Bands.Clear();
            tl.Columns.Clear();
            tl.DataSource = null;
            tl.DataSource = dtS;
            CreateBandedTreeHeaderServices(tl, dtS, dtSHeader);
        

            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";

            tl.ExpandAll();

            tl.ForceInitialize();


            tl.BeginSort();
            //tl.Columns["SortOrderNode"].SortOrder = SortOrder.Ascending;
            tl.EndSort();
            tl.EndUpdate();

            tl.BeginUpdate();
            tl.OptionsView.ShowColumns = true;
            tl.OptionsView.ShowBandsMode = DefaultBoolean.False;
            tl.BestFitColumns();
            if (tl.Columns["STT"].Visible && tl.Columns["STT"].Width > 40)
                tl.Columns["STT"].Width = 40;
            if (tl.Columns["RMCode_001"].Visible && tl.Columns["RMCode_001"].Width > 105)
                tl.Columns["RMCode_001"].Width = 105;
            if (tl.Columns["RMDescription_002"].Visible && tl.Columns["RMDescription_002"].Width > 150)
                tl.Columns["RMDescription_002"].Width = 150;
        

            if (tl.Columns["Price"].Visible && tl.Columns["Price"].Width > 70)
                tl.Columns["Price"].Width = 70;


            if (tl.Columns["Note"].Visible && tl.Columns["Note"].Width > 150)
                tl.Columns["Note"].Width = 150;
            if (tl.Columns["Amount"].Visible && tl.Columns["Amount"].Width > 100)
                tl.Columns["Amount"].Width = 100;

            tl.Columns["Amount"].SummaryFooter = SummaryItemType.Sum;
            tl.Columns["Amount"].SummaryFooterStrFormat = Ctrl_SOGenneral.StrFormatSalePriceDecimal(false, true);
            tl.Columns["Price"].SummaryFooter = SummaryItemType.Count;
            tl.Columns["Price"].SummaryFooterStrFormat = @"Total :";

            tl.OptionsView.ShowBandsMode = DefaultBoolean.True;
            tl.OptionsView.ShowColumns = false;
            tl.PostEditor();
            tl.LayoutChanged();
            tl.ShowEditor();
            tl.EndUpdate();



            //RepositoryItemMemoEdit repositoryTextNormal = new RepositoryItemMemoEdit { WordWrap = true };

            //tl.Columns["Note"].ColumnEdit = repositoryTextNormal;






        }





        private void CreateBandedTreeHeaderServices(TreeList tl, DataTable dtS, DataTable dtSHeader)
        {
            string sCurency = ProcessGeneral.GetSafeString(dtSHeader.Rows[0]["Currency_Des"]);

            string ServicesCaption = "(2) Services";
            TreeListBand gParent = new TreeListBand();
            gParent.AppearanceHeader.Options.UseTextOptions = true;
            gParent.AppearanceHeader.TextOptions.HAlignment = gParent.AppearanceHeader.TextOptions.HAlignment;
            gParent.Caption = ServicesCaption;
            gParent.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Top;
            gParent.AppearanceHeader.Options.UseFont = true;
            gParent.AppearanceHeader.Options.UseTextOptions = true;
            gParent.AppearanceHeader.Font = new Font("Times New Roman", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            //gParent.Bands.Add(gParent);
            tl.Bands.Add(gParent);


            TreeListBand[] arrBand = new TreeListBand[dtS.Columns.Count - 2];

            int i = 0;
            foreach (DataColumn col in dtS.Columns)
            {

                string colName = col.ColumnName;
                if (colName == "ChildPK" || colName == "ParentPK") continue;
                TreeListColumn gCol = new TreeListColumn();
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                gCol.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                arrBand[i] = new TreeListBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                HorzAlignment hoz = HorzAlignment.Near;
                string displayText = "";
                string displayFormat = "";
                FormatType formatType = FormatType.Custom;
                string tag = "";
                string colCaption = "";

                if (colName == "STT")
                {
                    displayText = "No.";
                    colCaption = "STT";
                    hoz = HorzAlignment.Center;
                    // gCol.ColumnEdit = _repositoryTextNormal;
                }
                else if (colName.Equals("RMCode_001"))
                {
                    displayText = @"Code";
                    colCaption = @"Mã hàng";
                    hoz = HorzAlignment.Center;
                }
                else if (colName.Equals("RMDescription_002"))
                {
                    displayText = @"Description";
                    colCaption = @"Tên hàng";
                    hoz = HorzAlignment.Near;
                    //  gCol.ColumnEdit = _repositoryTextNormal;

                }
                else if (colName.Equals("Qty"))
                {
                    displayText = @"Quantity";
                    colCaption = @"Số lượng";
                    hoz = HorzAlignment.Near;
                    //gCol.ColumnEdit = _repositoryTextNormal;

                }
                else if (colName.Equals("Price"))
                {
                    displayText = @"Unit price";
                    colCaption = @"Unit price";
                    hoz = HorzAlignment.Near;
                    //gCol.ColumnEdit = _repositoryTextNormal;
                }
                else if (colName.Equals("Amount"))
                {
                    displayText = string.Format(@"Amount ({0})", sCurency);
                    colCaption = string.Format(@"Amount ({0})", sCurency);
                    hoz = HorzAlignment.Center;
                    //gCol.ColumnEdit = _repositoryTextNormal;
                }
                else if (colName.Equals("Note"))
                {
                    displayText = @"Remark";
                    colCaption = @"Ghi chú";
                    hoz = HorzAlignment.Near;
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
                arrBand[i].AppearanceHeader.Options.UseFont = true;
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i].AppearanceHeader.Font = new Font("Times New Roman", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                // arrBand[i].RowCount = 2;
                gCol.OptionsColumn.ReadOnly = false;
                gCol.Tag = tag;
                gCol.Caption = colCaption;
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
                arrBand[i].Caption = displayText;
                arrBand[i].Columns.Add(gCol);
                arrBand[i].Visible = true;
                i++;
            }
            gParent.Bands.AddRange(arrBand);

            //List<string> lHide = new List<string>();
            ////lHide.Add("ItemType");
            ////lHide.Add("RMCode_001");
            ////lHide.Add("SortOrderNode");
            //var qHideBand = tl.Bands.Where(p => !p.HasChildren).Where(p => lHide.Any(s => s == p.Columns[0].FieldName)).OrderByDescending(p => p.Index).ToList();
            //foreach (TreeListBand bandHide in qHideBand)
            //{
            //    bandHide.Visible = false;
            //}


        }





        private TreeListBand AddChildBandServices(TreeListBand rootBand, string sItem)
        {
            TreeListBand gChild = new TreeListBand();
            gChild.AppearanceHeader.Options.UseTextOptions = true;
            gChild.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gChild.Caption = string.Format("<b>{0}</b>", sItem);
            return rootBand.Bands.Add(gChild);
        }


        private void InitTreeListServices(TreeList treeList)
        {


            treeList.OptionsView.ShowBandsMode = DefaultBoolean.True;


            treeList.OptionsView.AllowBandColumnsMultiRow = false;

            treeList.OptionsView.ShowColumns = false;
            treeList.OptionsView.AutoWidth = false;

            treeList.OptionsPrint.UsePrintStyles = false;


            treeList.OptionsPrint.PrintPageHeader = false;
            treeList.OptionsPrint.PrintAllNodes = true;
            treeList.OptionsPrint.PrintBandHeader = true;
            treeList.OptionsPrint.PrintCheckBoxes = false;
            treeList.OptionsPrint.PrintFilledTreeIndent = false;
            treeList.OptionsPrint.PrintHorzLines = true;
            treeList.OptionsPrint.PrintImages = true;
            // treeList.OptionsPrint.PrintPageHeader = false;
            treeList.OptionsPrint.AutoRowHeight = true;
            treeList.OptionsPrint.AutoWidth = true;
            treeList.OptionsPrint.PrintTree = true;
            treeList.OptionsPrint.PrintTreeButtons = false;
            treeList.OptionsPrint.PrintVertLines = true;

            treeList.OptionsPrint.PrintReportFooter = true;
            treeList.OptionsPrint.PrintRowFooterSummary = true;

            //   treeList.AppearancePrint.Lines.BackColor = Color.Red;

            //tlMain.AppearancePrint.Row.Options.UseTextOptions = true;
            //tlMain.AppearancePrint.Row.Options.UseFont = true;
            //tlMain.AppearancePrint.Row.Options.UseBackColor = true;
            //tlMain.AppearancePrint.Row.TextOptions.WordWrap = WordWrap.Wrap;

            treeList.OptionsBehavior.AutoNodeHeight = true;
            treeList.OptionsBehavior.Editable = false;
            treeList.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            treeList.OptionsView.AllowHtmlDrawHeaders = true;



            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = true;
            treeList.OptionsFind.AlwaysVisible = false;
            treeList.OptionsFind.ShowCloseButton = true;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsView.ShowAutoFilterRow = false;



            treeList.OptionsView.ShowHorzLines = true;
            treeList.OptionsView.ShowVertLines = true;
            treeList.OptionsView.ShowIndicator = true;

            treeList.OptionsView.EnableAppearanceEvenRow = false;
            treeList.OptionsView.EnableAppearanceOddRow = false;

            treeList.OptionsBehavior.AutoChangeParent = false;



            treeList.OptionsView.ShowSummaryFooter = false;

            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = false;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;







            //new TreeListMultiCellSelector(treeList, true)
            //{
            //    AllowSort = false,
            //    FilterShowChild = true,

            //};








            treeList.NodeCellStyle += TreeListServices_NodeCellStyle;
            //     treeList.CalcNodeHeight += TreeList_CalcNodeHeight;
            treeList.GetNodeDisplayValue += TreeListServices_GetNodeDisplayValue;
            //  treeList.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;








        }



        private void TreeListServices_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
        {

            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            if (!col.Visible) return;
            //if (fieldName != "STT") return;
            //string value = (tl.GetVisibleIndexByNode(node) + 1).ToString().Trim();
            //e.Value = value;
        }




        private void TreeListServices_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;


            if (node.ParentNode == null)
            {
                if (node.HasChildren)
                {
                    e.Appearance.Font = new Font("Times New Roman", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.DarkOrange;
                }
                else
                {
                    e.Appearance.Font = new Font("Times New Roman", 8F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                    e.Appearance.ForeColor = Color.Black;
                }

            }
            else
            {
                e.Appearance.Font = new Font("Times New Roman", 7F, (FontStyle.Regular), GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }




        }

        #endregion

        public void Print()
        {
            //this.Show();

            rpt001SOReport rpt = new rpt001SOReport(_soHeaderPk, tlMain, _dsData.Tables[1], tlServices);
            ReportPrintTool pt = new ReportPrintTool(rpt);
        
            Form form = pt.PreviewForm;
            form.MdiParent = this.ParentForm;
            pt.ShowPreview();
          
            form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = string.Format("Print SO No.: {0}", ProcessGeneral.GetSafeString(_dsData.Tables[1].Rows[0]["OrderNOT"]));
            form.Show();
        }
  










        private void simpleButton1_Click(object sender, EventArgs e)
        {
        
        }


     



        private void gvMain_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            e.Appearance.BackColor = Color.Pink;
        }

        public class ChildItemAgreement
        {
            public string Col { get; set; }
            public Int32 pk { get; set; }
            public Int32 sav { get; set; }
            public bool chk { get; set; }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            rpt001SOReportSub1 report = new rpt001SOReportSub1(ProcessGeneral.GetSafeInt(txtPK.Text));
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowPreviewDialog();
        }
    }

    class MyTreeList : TreeList
    {
        public MyTreeList() : base()
        {
            OptionsBehavior.AutoNodeHeight = false;
        }
        protected override TreeListNode CreateNode(int nodeID, TreeListNodes owner, object tag)
        {
            return new MyTreeListNode(nodeID, owner);
        }
        
        protected override void RaiseCalcNodeHeight(TreeListNode node, ref int nodeHeight)
        {
            MyTreeListNode myNode = node as MyTreeListNode;
            if (myNode != null)
                nodeHeight = myNode.Height;
            else
                base.RaiseCalcNodeHeight(node, ref nodeHeight);
        }
        public virtual int DefaultNodesHeight { get { return 18; } }
    }
    public class MyTreeListNode : TreeListNode
    {
        const int minHeight = 5;
        int height;
        public MyTreeListNode(int id, TreeListNodes owner) : base(id, owner)
        {
            this.height = (owner.TreeList as MyTreeList).DefaultNodesHeight;
        }
        public int Height
        {
            get { return height; }
            set
            {
                if (Height == value || value < minHeight) return;
                height = value;
                Changed(NodeChangeTypeEnum.User1);
            }
        }
    }
}
