using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using CNY_BaseSys.Info;
using CNY_BaseSys.Properties;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using TreeFixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;
using CNY_BaseSys.Class;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;
using GridSumCol = DevExpress.Data.SummaryItemType;
namespace CNY_BaseSys.WForm
{
	//2
	public partial class FrmDemandProductionOrderN : FrmBase
	{
		#region "Property"

		private Dictionary<string, ColorDemandPrInfoRpt> _dicColor;
		private readonly Inf_Progress _inf = new Inf_Progress();

		private Int64 _cny00019Pk = 0;

		public Int64 Cny00019Pk
		{
			get { return this._cny00019Pk; }
			set { this._cny00019Pk = value; }
		}


		public sealed override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

	
		private Int32 _bomSplitPos = 0;

		private Int64 _cny00012Pk = 0;
		private Int64 _cny00054Pk = 0;
		private bool _isSummaryClick = false;
		private readonly DataTable _dtShortCut;

		private Ctrl_MRPProgress _ctrl;
		private Dictionary<Int64, List<SoInfoBomCrossItem>> _dicCrossBOM = new Dictionary<Int64, List<SoInfoBomCrossItem>>();
	
		private Dictionary<Int64, PrNsoInfo> _dicSoLine = new Dictionary<Int64, PrNsoInfo>(); // Key PKCHild
		#endregion


		#region "Contructor"


		public FrmDemandProductionOrderN(Int64 cny00019Pk)
		{

			InitializeComponent();
			_cny00019Pk = cny00019Pk;
			_dtShortCut = _inf.BoM_LoadTableShortCut();

			xtraTabCenter.ShowTabHeader = DefaultBoolean.False;
			toolTipControllerMain.GetActiveObjectInfo += toolTipControllerMain_GetActiveObjectInfo;
			_dicColor = new Dictionary<string, ColorDemandPrInfoRpt>
			{
				
				{"GREEN", new ColorDemandPrInfoRpt
				{
					ColorSet = Color.Green,
					DescriptionSet = "<size=10> <b><color=green>*</color></b><backcolor=green>     </backcolor> Required Qty = Purchased Qty</size>"
                }},
				{"YELLOW", new ColorDemandPrInfoRpt
				{
					ColorSet = Color.Yellow,
					DescriptionSet = "<size=10> <b><color=yellow>*</color></b><backcolor=yellow>     </backcolor> Required Qty <> Purchased Qty</size>"
                }},
				{"", new ColorDemandPrInfoRpt
				{
					ColorSet = Color.White,
					DescriptionSet = "<size=10> <b><color=white>*</color></b><backcolor=white>     </backcolor> No Material Required</size>"
				}},
			};
		
	
	 
			//    this.Text = string.Format("{0} - (Order No. : {1})", this.Text, _soInfo.OrderNo);

			InitTreeList(tlMain);
			btnCheckAll.Click += BtnCheckAll_Click;
			this.Load += Frm_Load;






			txtCustCode.Properties.ReadOnly = true;
			txtCustName.Properties.ReadOnly = true;
			txtCustOrderNo.Properties.ReadOnly = true;
			txtOrderNo.Properties.ReadOnly = true;
			txtProductionOrderNo.Properties.ReadOnly = true;
			txtProjectNo.Properties.ReadOnly = true;
			txtProjectName.Properties.ReadOnly = true;
			
			_bomSplitPos = splitCCB.SplitterPosition;
			tgHideSplit.Toggled += TgHideSplit_Toggled;


			SetUpMainGrid();
			SetUpMainSummaryGrid();

            SetUpMainGridDetail();




        }


		private void TgHideSplit_Toggled(object sender, EventArgs e)
		{
			if (tgHideSplit.IsOn)
			{
				splitCCB.SplitterPosition = _bomSplitPos;
			}
			else
			{
				_bomSplitPos = splitCCB.SplitterPosition;
				splitCCB.SplitterPosition = 0;
			}
		}
		private void Frm_Load(object sender, EventArgs e)
		{
			ShowGridMain(true);
			DisplayPrData();





			AllowAdd = true;
			AllowEdit = false;
			AllowDelete = false;

			AllowCancel = false;
			AllowRefresh = true;

			AllowPrint = false;
			AllowBreakDown = false;
			AllowRevision = false;
			AllowRangeSize = false;
			AllowCopyObject = false;
			AllowGenerate = false;
			AllowCombine = false;
			AllowCheck = false;
			EnableFind = true;
			EnableRefresh = true;
			EnablePrint = true;
			EnableAdd = true;

			SetCaptionAdd = "Refresh";
			SetImageAdd = SetImageRefresh;

			SetCaptionRefresh = @"View";
			SetImageRefresh = Resources.viewonweb_32x32;
			AllowSave = false;

			AllowFind = false;
			SetCaptionFind = @"Export";
			SetImageFind = Resources.export_32x32;





		}
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                #region "System"
                case Keys.Escape:
                {
                    if (xtraTabCenter.SelectedTabPage == xtraTabDetail)
                    {
                        ShowGridMain(true);
                    }
                    return true;
                }

                #endregion



            }
            return base.ProcessCmdKey(ref message, keys);



        }
        private void DisplayPrData()
		{
			WaitDialogForm dlg = new WaitDialogForm();
            //  _lCNY00056BPKDel.Clear();

            ResetDataPaintGrid();
            ResetDataSummaryGrid();

            DataTable dtSoHeader = _inf.GetSoHeaderInfo(_cny00019Pk);
			if (dtSoHeader.Rows.Count <= 0)
			{
	
				txtCustCode.EditValue = "";
				txtCustName.EditValue = "";
				txtOrderNo.EditValue = "";
				txtProductionOrderNo.EditValue = "";
				txtCustOrderNo.EditValue = "";
				txtProjectNo.EditValue = "";
				txtProjectName.EditValue = "";
				this.Text = @"Production Info";
             
				ResetSOTree();
				dlg.Close();
				return;
			}

			DataRow drSoHeader = dtSoHeader.Rows[0];
			string productionOrderNo = ProcessGeneral.GetSafeString(drSoHeader["ProductionOrder"]);
			string projectName = ProcessGeneral.GetSafeString(drSoHeader["ProjectName"]);
			txtCustCode.EditValue = ProcessGeneral.GetSafeString(drSoHeader["Customer"]);
			txtCustName.EditValue = ProcessGeneral.GetSafeString(drSoHeader["CustomerName"]);
			txtOrderNo.EditValue = ProcessGeneral.GetSafeString(drSoHeader["OrderNo"]);
			txtProductionOrderNo.EditValue = productionOrderNo;
			txtCustOrderNo.EditValue = ProcessGeneral.GetSafeString(drSoHeader["CustomerOrderNo"]);
			txtProjectNo.EditValue = ProcessGeneral.GetSafeString(drSoHeader["ProjectNo"]);
			txtProjectName.EditValue = projectName;
			this.Text = string.Format(@"Production Info (LSX : {0} - Project Name : {1})", productionOrderNo, projectName);
			_cny00012Pk = _inf.GetBomFinishingPk(_cny00019Pk);
			_cny00054Pk = _inf.GetPrHeaderPK(_cny00019Pk);

			
			_ctrl = new Ctrl_MRPProgress(_cny00019Pk, _cny00012Pk, _cny00054Pk, spMain.Document.FormulaEngine, _dtShortCut);
			DataSet ds = _ctrl.GetData();
			dlg.Close();
  
			DataTable dtErr = ds.Tables[0];
			DataTable dtCrossBoM = ds.Tables[1];



			_dicCrossBOM = dtCrossBoM.AsEnumerable().GroupBy(p => p.Field<Int64>("CNY00020PK")).Select(s => new
			{
				CNY00020PK = s.Key,
				Data = s.Select(t => new SoInfoBomCrossItem
				{
					BOMNo = t.Field<int>("BOMNo"),
					BOMStatus = t.Field<string>("BOMStatus"),
					BoMVersion = t.Field<int>("BoMVersion"),
					CreatedBy = t.Field<string>("CreatedBy"),
					CreatedDate = t.Field<string>("CreatedDate"),
					BOMStatusInt = t.Field<int>("BOMStatusInt")

				}).ToList()

			}).ToDictionary(p => p.CNY00020PK, p => p.Data);

			ds.RemoveAllDataTableOnDataSet();
			if (ProcessGeneral.GetSafeInt(dtErr.Rows[0]["ErrCode"]) == 0)
			{
			
				ResetSOTree();
				return;
			}
		
			_dicSoLine = _ctrl.DicSoLine;
            LoadDataSOTree();
           // LoadDataCheckMain(false);


        }





	  


		#endregion



		#region "Button Click Event"
		private void SetInfoCheckButton(bool enable, Image image, string text)
		{
			btnCheckAll.Image = image;
			btnCheckAll.ToolTip = text;
			btnCheckAll.Enabled = enable;
		}
		private void BtnCheckAll_Click(object sender, EventArgs e)
		{
			if (tlMain.AllNodesCount <= 0) return;
            if (xtraTabCenter.SelectedTabPage == xtraTabDetail) return;


            if (btnCheckAll.ToolTip == @"Check All")
			{
				foreach (TreeListNode node in tlMain.Nodes)
				{
					node.Checked = true;
				}

				SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");
			}
			else
			{
				foreach (TreeListNode node in tlMain.Nodes)
				{
					node.Checked = false;
				}

				SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
			}


		}

		#endregion



		#region "Init TreeView"

		private void ResetSOTree()
		{
            tlMain.BeginUpdate();
            tlMain.Bands.Clear();
            tlMain.Columns.Clear();
            tlMain.DataSource = null;
            tlMain.EndUpdate();
            btnCheckAll.Enabled = false;
            SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
        }


		private void LoadDataSOTree()
		{
			if (_dicSoLine.Count <= 0)
			{
				ResetSOTree();
				return;
			}




			int bomCount = 0;
			if (_dicCrossBOM.Count > 0)
			{
				bomCount = _dicCrossBOM.Select(p => p.Value.Count).Max();
			}


			DataTable dtGrid = new DataTable();

            //dtGrid.Columns.Add("Selected", typeof(string));
            dtGrid.Columns.Add("Reference", typeof(string));
			
			dtGrid.Columns.Add("OrderQuantity", typeof(int));

			for (int i = 0; i < bomCount; i++)
			{
				dtGrid.Columns.Add(string.Format("BOMNo-{0}", i), typeof(string));
				dtGrid.Columns.Add(string.Format("BOMStatus-{0}", i), typeof(string));
				dtGrid.Columns.Add(string.Format("BoMVersion-{0}", i), typeof(string));
				dtGrid.Columns.Add(string.Format("CreatedBy-{0}", i), typeof(string));
				dtGrid.Columns.Add(string.Format("CreatedDate-{0}", i), typeof(string));
			}
			dtGrid.Columns.Add("TDG00001PK", typeof(Int64));
			dtGrid.Columns.Add("CNY00020PK", typeof(Int64));
			dtGrid.Columns.Add("SortOrderNode", typeof(int));
            dtGrid.Columns.Add("ProductCode", typeof(string));
            dtGrid.Columns.Add("ProductName", typeof(string));
            dtGrid.Columns.Add("FactorProduct", typeof(int));
            dtGrid.Columns.Add("ChildPK", typeof(Int64));
            dtGrid.Columns.Add("ParentPK", typeof(Int64));
            foreach (var itemSo in _dicSoLine)
			{
				PrNsoInfo soInfo = itemSo.Value;
				Int64 cny00020pk = itemSo.Key;
				DataRow drGrid = dtGrid.NewRow();
                //drGrid["Selected"] = "";
                drGrid["ProductCode"] = soInfo.ProductCode;
				drGrid["ProductName"] = soInfo.ProductName;
				drGrid["Reference"] = soInfo.Reference;
				drGrid["FactorProduct"] = soInfo.FactorProduct;
				drGrid["OrderQuantity"] = soInfo.OrderQuantity;
				drGrid["TDG00001PK"] = soInfo.TDG00001PK;
				drGrid["CNY00020PK"] = cny00020pk;
				drGrid["SortOrderNode"] = soInfo.SortOrderNode;
                drGrid["ChildPK"] = cny00020pk;
                drGrid["ParentPK"] = 0;
                if (bomCount > 0)
				{


					int loop = 0;

					List<SoInfoBomCrossItem> lInfo;
					if (_dicCrossBOM.TryGetValue(cny00020pk, out lInfo) && lInfo.Count > 0)
					{
						foreach (SoInfoBomCrossItem crossInfo in lInfo)
						{
							drGrid[string.Format("BOMNo-{0}", loop)] = ProcessGeneral.GetSafeString(crossInfo.BOMNo);
							drGrid[string.Format("BOMStatus-{0}", loop)] = crossInfo.BOMStatus;
							drGrid[string.Format("BoMVersion-{0}", loop)] = ProcessGeneral.GetSafeString(crossInfo.BoMVersion);
							drGrid[string.Format("CreatedBy-{0}", loop)] = crossInfo.CreatedBy;
							drGrid[string.Format("CreatedDate-{0}", loop)] = crossInfo.CreatedDate;
							loop++;
						}
					}


					for (int j = loop; j < bomCount; j++)
					{
						drGrid[string.Format("BOMNo-{0}", j)] = "";
						drGrid[string.Format("BOMStatus-{0}", j)] = "";
						drGrid[string.Format("BoMVersion-{0}", j)] = "";
					}
				}
				dtGrid.Rows.Add(drGrid);
			}
			dtGrid.AcceptChanges();





            tlMain.BeginUpdate();
            tlMain.Bands.Clear();
            tlMain.Columns.Clear();
            tlMain.DataSource = null;
			CreateBandedTreeSO(dtGrid);
            tlMain.DataSource = dtGrid;
            tlMain.ParentFieldName = "ParentPK";
            tlMain.KeyFieldName = "ChildPK";
            if (!tlMain.OptionsView.ShowColumns)
                tlMain.OptionsView.ShowColumns = true;
            tlMain.BestFitColumns();

            if (dtGrid.Rows.Count > 0)
            {
                SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");
                foreach (TreeListNode node in tlMain.Nodes)
                {
                    node.Checked = true;
                }

            }
            else
            {
                SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
            }

            tlMain.EndUpdate();

            

            btnCheckAll.Enabled = dtGrid.Rows.Count > 0;

        }

		private void CreateBandedTreeSO(DataTable dtS)
		{

            TreeListBand[] arrBand = new TreeListBand[dtS.Columns.Count];
			Dictionary<string, Tuple<int, string>> dicColVisible = new Dictionary<string, Tuple<int, string>>();
			Dictionary<string, Tuple<int, string, string, string>> dicColChildVisible = new Dictionary<string, Tuple<int, string, string, string>>();
			int i = 0;

			foreach (DataColumn col in dtS.Columns)
            {
                string colName = col.ColumnName;
                if(colName == "ChildPK" || colName == "ParentPK") continue;
                TreeListColumn gCol = new TreeListColumn();
				gCol.AppearanceCell.Options.UseTextOptions = true;
				gCol.AppearanceHeader.Options.UseTextOptions = true;
				arrBand[i] = new TreeListBand();
				arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
				HorzAlignment hozAlignCol = HorzAlignment.Center;
				HorzAlignment hozAlignBan = HorzAlignment.Center;
			
				string bandCaption = "";
				string colCaption = "";
                if (colName == "ProductCode")
				{
					dicColVisible.Add(colName, new Tuple<int, string>(i, "HI"));
					colCaption = "Product Code";
					bandCaption = "Product Code";
					hozAlignCol = HorzAlignment.Center;
					hozAlignBan = HorzAlignment.Center;

				}
				else if (colName == "ProductName")
				{
					dicColVisible.Add(colName, new Tuple<int, string>(i, "HI"));
					colCaption = "Product Name";
					bandCaption = "Product Name";
					hozAlignCol = HorzAlignment.Near;
					hozAlignBan = HorzAlignment.Near;

				}
				else if (colName == "Reference")
				{
					dicColVisible.Add(colName, new Tuple<int, string>(i, ""));
					colCaption = "Reference";
					bandCaption = "Reference";
					hozAlignCol = HorzAlignment.Near;
					hozAlignBan = HorzAlignment.Near;

				}
				else if (colName == "FactorProduct")
				{
					dicColVisible.Add(colName, new Tuple<int, string>(i, "HI"));
					colCaption = "Factor";
					bandCaption = "Factor";
					hozAlignCol = HorzAlignment.Center;
					hozAlignBan = HorzAlignment.Center;
					gCol.Format.FormatType = FormatType.Numeric;
					gCol.Format.FormatString = "N0";

				}
				else if (colName == "OrderQuantity")
				{
					dicColVisible.Add(colName, new Tuple<int, string>(i, ""));
					colCaption = "Quantity";
					bandCaption = "Quantity";
					hozAlignCol = HorzAlignment.Center;
					hozAlignBan = HorzAlignment.Center;
					gCol.Format.FormatType = FormatType.Numeric;
					gCol.Format.FormatString = "N0";

				}
				else if (colName.Contains("-"))
				{
					string[] arrData = colName.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
					string strBefor = arrData[0];
					string strAfter = arrData[1];
					int idxDislplay = ProcessGeneral.GetSafeInt(strAfter) + 1;
					dicColVisible.Add(colName, new Tuple<int, string>(i, "ST"));
					dicColChildVisible.Add(colName, new Tuple<int, string, string, string>(i, idxDislplay.ToString(), "ST" + strAfter, "ST"));
					if (strBefor == "BOMNo")
					{
						colCaption = "No.";
						bandCaption = "No.";
					}
					else if (strBefor == "BOMStatus")
					{
						colCaption = "Status";
						bandCaption = "Status";
					}
					else if (strBefor == "CreatedBy")
					{
						colCaption = "Created By";
						bandCaption = "Created By";
					}
					else if (strBefor == "CreatedDate")
					{
						colCaption = "Created Date";
						bandCaption = "Created Date";
					}
					else
					{
						colCaption = "Version";
						bandCaption = "Version";
					}

					hozAlignCol = HorzAlignment.Center;
					hozAlignBan = HorzAlignment.Center;
					gCol.AppearanceHeader.Options.UseForeColor = true;
					gCol.AppearanceHeader.ForeColor = Color.DarkCyan;

				}
				else
				{
					dicColVisible.Add(colName, new Tuple<int, string>(i, "HI"));
					colCaption = colName;
					bandCaption = colName;
				}

				gCol.AppearanceCell.TextOptions.HAlignment = hozAlignCol;
				gCol.AppearanceHeader.TextOptions.HAlignment = hozAlignBan;

				arrBand[i].AppearanceHeader.TextOptions.HAlignment = hozAlignBan;
				arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
				arrBand[i].AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
				arrBand[i].AppearanceHeader.Options.UseFont = true;
				arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
				gCol.Caption = "<b>" + colCaption + "<b>";
				gCol.FieldName = colName;
				gCol.Visible = true;
				gCol.VisibleIndex = i;
				tlMain.Columns.Add(gCol);
				arrBand[i].Caption = bandCaption;
				arrBand[i].Columns.Add(gCol);
				//arrBand[i].Index = 0;
				arrBand[i].Width = 100;
				i++;
			}


			var q1 = dicColVisible.GroupBy(p => p.Value.Item2).Select(t => new
			{
				Str = t.Key,
				MinInd = t.Min(s => s.Value.Item1),
				MaxInd = t.Max(s => s.Value.Item1),
			}).OrderBy(p => p.MinInd).ToList();


			int h = 0;

			foreach (var item in q1)
			{
				string str = item.Str;
				int min = item.MinInd;
				int max = item.MaxInd;
				if (str == "")
				{
					TreeListBand gRoot = CreateParentBand("SO", 2);
					//gRoot.In = h;
					gRoot.Name = @"FirstBand";
					gRoot.AppearanceHeader.Options.UseForeColor = true;
					gRoot.AppearanceHeader.ForeColor = Color.DarkGreen;
					for (int j = min; j <= max; j++)
					{
                        TreeListColumn gCol = tlMain.Columns[j];
						arrBand[j].Columns.Clear();
						gRoot.Columns.Add(gCol);
					}
                    tlMain.Bands.Add(gRoot);
					h++;
				}
				else if (str == "ST")
				{
                    //       dicColChildVisible.Add(colName, new Tuple<int, string, string, string>(i, "Status", "ST1", "ST"));

                    TreeListBand gRoot = CreateParentBand("BOM", 1);
					//gRoot.VisibleIndex = h;
					gRoot.Name = @"STBand";
					gRoot.AppearanceHeader.Options.UseForeColor = true;
					gRoot.AppearanceHeader.ForeColor = Color.DarkCyan;



					var q2 = dicColChildVisible.Where(p => p.Value.Item4 == str).GroupBy(p => p.Value.Item3).Select(t => new
					{
						Str = t.Key,
						MinInd = t.Min(s => s.Value.Item1),
						MaxInd = t.Max(s => s.Value.Item1),
						Caption = t.Select(s => s.Value.Item2).First()
					}).OrderBy(p => p.MinInd).ToList();


					foreach (var itemQ2 in q2)
					{
                        TreeListBand gParent = CreateParentBand(itemQ2.Caption, 1);
						gParent.AppearanceHeader.Options.UseForeColor = true;
						gParent.AppearanceHeader.ForeColor = Color.DarkCyan;
						min = itemQ2.MinInd;
						max = itemQ2.MaxInd;
						for (int j = min; j <= max; j++)
						{

							arrBand[j].Columns.Clear();
							gParent.Columns.Add(tlMain.Columns[j]);
						}

						gRoot.Bands.Add(gParent);


                        

                    }



                    tlMain.Bands.Add(gRoot);
					h++;
				}
				else if (str == "HI")
				{
                    TreeListBand gRoot = CreateParentBand("Hide", 2);
					//gRoot.VisibleIndex = h;
					gRoot.Name = @"HIBand";
					for (int j = min; j <= max; j++)
					{
						arrBand[j].Columns.Clear();
						gRoot.Columns.Add(tlMain.Columns[j]);
					}
                    tlMain.Bands.Add(gRoot);
					h++;
				}
			}

			tlMain.Bands["HIBand"].Visible = false;
		}
        private TreeListBand CreateParentBand(string caption, int rowCount, bool isBold = true)
        {
            TreeListBand gParent = new TreeListBand();
            gParent.AppearanceHeader.Options.UseTextOptions = true;
            gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gParent.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            gParent.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
            if (isBold)
            {
                gParent.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);

            }


            gParent.AppearanceHeader.Options.UseFont = true;
            gParent.RowCount = rowCount;
            gParent.Caption = caption;

            gParent.Width = 100;
            return gParent;
        }


        private ImageList GetImageListDisplayTreeView()
		{
			var imgLt = new ImageList();
			imgLt.Images.Add(Resources.Product_BoM_16x16);
			imgLt.Images.Add(Resources.RawMaterial_BoM_16x16);
			return imgLt;
		}


		private void InitTreeList(TreeList treeList)
		{


		 
		  




			treeList.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
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

			treeList.OptionsBehavior.Editable = true;
			treeList.OptionsView.ShowColumns = true;
			treeList.OptionsView.ShowHorzLines = true;
			treeList.OptionsView.ShowVertLines = true;
			treeList.OptionsView.ShowIndicator = true;
			treeList.OptionsView.AutoWidth = false;
			treeList.OptionsView.EnableAppearanceEvenRow = false;
			treeList.OptionsView.EnableAppearanceOddRow = false;
			treeList.StateImageList = GetImageListDisplayTreeView();
			treeList.OptionsBehavior.AutoChangeParent = false;
			treeList.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
			treeList.OptionsBehavior.AutoNodeHeight = true;

			treeList.OptionsView.ShowSummaryFooter = false;



			treeList.OptionsView.AllowHtmlDrawHeaders = true;


			treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
			treeList.OptionsBehavior.KeepSelectedOnClick = true;

			treeList.OptionsBehavior.SmartMouseHover = false;
			treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;



			treeList.OptionsCustomization.AllowColumnResizing = true;

			treeList.OptionsCustomization.AllowQuickHideColumns = true;
			treeList.OptionsCustomization.AllowSort = true;
			treeList.OptionsCustomization.AllowFilter = true;



			treeList.OptionsCustomization.AllowColumnMoving = false;
			//treeList.OptionsMenu.EnableColumnMenu = true;



			treeList.OptionsBehavior.AllowExpandOnDblClick = true;

			treeList.OptionsView.ShowBandsMode = DefaultBoolean.True;


			treeList.AllowDrop = false;



			treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);



			treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
			treeList.OptionsView.ShowCheckBoxes = true;





			new TreeListMultiCellSelector(treeList, false)
			{
				AllowSort = false,
				FilterShowChild = true,

			};


			treeList.OptionsBehavior.ShowEditorOnMouseUp = false;

			
			treeList.ShowingEditor += TreeList_ShowingEditor;
			treeList.CustomDrawNodeIndicator += TreeList_CustomDrawNodeIndicator;
			treeList.NodeCellStyle += TreeList_NodeCellStyle;
			treeList.AfterCheckNode += TreeList_AfterCheckNode;
            treeList.GetStateImage += TreeList_GetStateImage;

            treeList.BeforeCheckNode += TreeList_BeforeCheckNode;

		}

        private void TreeList_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;

            e.NodeImageIndex = node.HasChildren ? 0 : 1;




        }
        private void TreeList_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            e.CanCheck = xtraTabCenter.SelectedTabPage != xtraTabDetail;
      
        }

        private void TreeList_AfterCheckNode(object sender, NodeEventArgs e)
		{
			TreeList tl = sender as TreeList;
			if (tl == null) return;
			List<TreeListNode> lCheckNode = tl.GetAllCheckedNodes().ToList();
			if (lCheckNode.Count == tl.AllNodesCount)
			{
				SetInfoCheckButton(true, Resources.chk_ch_24x24, @"UnCheck All");

			}
			else
			{
				SetInfoCheckButton(true, Resources.chk_un_24x24, @"Check All");
			}

		}


		private void TreeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
		{
			TreeList tl = sender as TreeList;
			if (tl == null) return;

			TreeListNode node = e.Node;
			if (node == null) return;
			TreeListColumn col = e.Column;
			if (col == null) return;
			string fieldName = col.FieldName;



			switch (fieldName)
			{
				case "OrderQuantity":
					{


						e.Appearance.ForeColor = Color.DarkOrange;
					}
					break;
				case "Reference":
					{

						e.Appearance.ForeColor = Color.DarkRed;
					}
					break;

			}

			if (fieldName == "Reference")
			{
				e.Appearance.GradientMode = LinearGradientMode.Vertical;
				e.Appearance.BackColor = Color.LavenderBlush;
				e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
			}
            else if (fieldName == "OrderQuantity")
            {
                if (e.Node.CheckState == CheckState.Checked)
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LavenderBlush;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
            }
			else if (e.Node.CheckState == CheckState.Checked)
            {

                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }



		}

		private void TreeList_ShowingEditor(object sender, CancelEventArgs e)
		{
            e.Cancel = true;


        }





		private void TreeList_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
		{
			var tl = sender as TreeList;
			if (tl == null) return;
			if (tl.GetDataRecordByNode(e.Node) == null) return;


			bool isCheck = e.Node.CheckState == CheckState.Checked;

			LinearGradientBrush backBrush;

			if (isCheck)
			{
				backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
			}
			else
			{
				backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
			}


			e.Graphics.FillRectangle(backBrush, e.Bounds);
			ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
			if (isCheck)
			{
				e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
				e.Appearance.ForeColor = e.Node.HasChildren ? Color.DarkMagenta : Color.DarkOrchid;
			}
			else
			{
				e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
				e.Appearance.ForeColor = Color.Black;
			}


			e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
			string value = (tl.GetVisibleIndexByNode(e.Node) + 1).ToString().Trim();
			e.Graphics.DrawString(value, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache),
				e.Bounds, e.Appearance.GetStringFormat());
			e.ImageIndex = -1;
			e.Handled = true;

		}

		//  private int updateRmSourceNo = 1;
		#endregion












		#region "Override Button Click Event"

		protected override void PerformRefresh()
		{
			LoadDataCheckMain();

		}

		protected override void PerformClose()
		{



			if (xtraTabCenter.SelectedTabPage == xtraTabDetail)
			{
				ShowGridMain(true);
				return;
			}

			base.PerformClose();
		}

		



		protected override void PerformAdd()
		{
			WaitDialogForm dlg = new WaitDialogForm("");
			DisplayPrData();
			dlg.Close();
		}


		#endregion


		#region "GridView Event && Init Grid"

		private void ShowGridMain(bool value)
		{
			//gcPaint.Visible = value;
			//pcInfo.Visible = !value;

			xtraTabGeneral.PageVisible = value;
			xtraTabDetail.PageVisible = !value;




			EnableAdd = value;
			EnableRefresh = value;
		}
		private void ResetDataPaintGrid()
		{
			gvPaint.BeginUpdate();
			gvPaint.Bands.Clear();
			gvPaint.Columns.Clear();
			gcPaint.DataSource = null;
			gvPaint.EndUpdate();
		}


		private void LoadDataCheckMain()
        {


            Dictionary<Int64,Tuple<string,int,int>> lCNY00020PK = tlMain.GetAllNodeTreeList().Select(p => new
            {
                CNY00020PK = ProcessGeneral.GetSafeInt64(p.GetValue("ChildPK")),
                Reference = ProcessGeneral.GetSafeString(p.GetValue("Reference")),
                SortOrderNode = ProcessGeneral.GetSafeInt(p.GetValue("SortOrderNode")),
                SOQty = ProcessGeneral.GetSafeInt(p.GetValue("OrderQuantity")),
            }).ToDictionary(p=>p.CNY00020PK,p=>new Tuple<string, int, int>(p.Reference,p.SortOrderNode,p.SOQty));


            

            if (lCNY00020PK.Count <= 0)
			{
                XtraMessageBox.Show("No Item Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
			}

			WaitDialogForm dlg = new WaitDialogForm();
            Dictionary<string, string> qLoop = new Dictionary<string, string>();
            DataTable dtFinal = null;
            DataTable dtSumary = null;
            bool rs = _ctrl.CalDataDisplayBySoSelected(lCNY00020PK, out dtSumary, out dtFinal ,out qLoop);
            dlg.Close();
          
			if (!rs)
			{
				ResetDataPaintGrid();
				ResetDataSummaryGrid();
	
				XtraMessageBox.Show("No Data Display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}








		



			




			

			gvPaint.BeginUpdate();
			gvPaint.Bands.Clear();
			gvPaint.Columns.Clear();
			gcPaint.DataSource = null;

			CreateBandedGridHeader(dtFinal, qLoop);
			gcPaint.DataSource = dtFinal;
			BestFitBandsGrid(gvPaint);

			gvPaint.EndUpdate();


			LoadDataGridSummary(dtSumary, qLoop);
			
		}

		private void CreateBandedGridHeader(DataTable dtS, Dictionary<string, string> dicCg)
		{

			GridBand[] arrBand = new GridBand[dtS.Columns.Count];

			int i = 0;
			foreach (DataColumn col in dtS.Columns)
			{
				BandedGridColumn gCol = new BandedGridColumn();
				gCol.AppearanceCell.Options.UseTextOptions = true;
				gCol.AppearanceHeader.Options.UseTextOptions = true;
				arrBand[i] = new GridBand();
				arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
				HorzAlignment hozAlign = HorzAlignment.Center;
				string colName = col.ColumnName;
				string displayText = "";
				if (colName == "CNY00020PK")
				{
					displayText = "CNY00020PK";
					arrBand[i].RowCount = 3;
				}
                else if (colName == "Reference")
				{
					displayText = "Reference";
					hozAlign = HorzAlignment.Near;
					arrBand[i].RowCount = 3;
				}
				else if (colName == "SOQty")
				{
					displayText = "Order Qty";
					gCol.DisplayFormat.FormatType = FormatType.Numeric;
					gCol.DisplayFormat.FormatString = FunctionFormatModule.StrFormatOrderQtyDecimal(false, false);
					arrBand[i].RowCount = 3;
				}
				else if (colName.StartsWith("FinishPR_"))
				{
					displayText = "PR";
					arrBand[i].RowCount = 1;
				}
				else if (colName.StartsWith("FinishPO_"))
				{
					displayText = "PO";
					arrBand[i].RowCount = 1;
				}
				else
				{
					displayText = colName;
				}



				//gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
				//gCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
				//arrBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;

				string[] arrTag = colName.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
				string tag = "";
				if (arrTag.Length == 2)
				{

					tag = arrTag[1].Trim();


				}



				gCol.AppearanceCell.TextOptions.HAlignment = hozAlign;
				gCol.AppearanceHeader.TextOptions.HAlignment = hozAlign;
				arrBand[i].AppearanceHeader.TextOptions.HAlignment = hozAlign;
				arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
				arrBand[i].AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
				arrBand[i].AppearanceHeader.Options.UseFont = true;
				arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
				arrBand[i].Tag = tag;

				gCol.Tag = tag;
				gCol.Caption = displayText;
				gCol.FieldName = colName;
				gCol.Visible = true;
				gCol.VisibleIndex = i;




				gvPaint.Columns.Add(gCol);
				arrBand[i].Caption = displayText;
				arrBand[i].Columns.Add(gCol);
				arrBand[i].VisibleIndex = 0;
				arrBand[i].Width = 100;
				i++;
			}
			gvPaint.Bands.AddRange(new[] { arrBand[0], arrBand[1], arrBand[2]});

			int t = 3;
			foreach (var itemTemp in dicCg)
			{

				string sName = itemTemp.Value;
				string sKey = itemTemp.Key;
				GridBand gParent = new GridBand();
				gParent.AppearanceHeader.Options.UseTextOptions = true;
				gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
				gParent.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
				gParent.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
				gParent.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
				gParent.AppearanceHeader.Options.UseFont = true;
				gParent.RowCount = 2;
				gParent.Caption = string.Format("{0}-{1}", sKey, sName);
				gParent.VisibleIndex = t;
				gParent.Width = 100;
				// gParent.RowCount = 2;
				var q2 = arrBand.Where(p => ProcessGeneral.GetSafeString(p.Tag) == sKey).ToArray();
				for (int h = 0; h < q2.Length; h++)
				{
					q2[h].VisibleIndex = h;
					gParent.Children.Add(q2[h]);
				}
				gvPaint.Bands.Add(gParent);
				t++;
			}


		}
		private void BestFitBandsGrid(BandedGridView view)
		{

			view.BeginUpdate();
			view.OptionsView.ShowColumnHeaders = true;


			foreach (BandedGridColumn col in view.Columns)
			{
				GridBand gBand = col.OwnerBand; string fieldName = col.FieldName;
				if (fieldName == "CNY00020PK")
				{
					gBand.Visible = false;
				}
				else
				{
					col.Caption = gBand.Caption;
					if (fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_"))
					{
						col.Width = 50;
					}
					else
					{
						col.Width = col.GetBestWidth() + 10;
					}

				}


			}

			// view.BestFitColumns();
			//   view.Columns["MenuCode"].Width += 20;
			view.OptionsView.ShowColumnHeaders = false;






			view.EndUpdate();



			//  view.Bands[1].Visible = false;


		}




		private void SetUpMainGrid()
		{


			gcPaint.UseEmbeddedNavigator = true;

			gcPaint.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
			gcPaint.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
			gcPaint.EmbeddedNavigator.Buttons.Append.Visible = false;
			gcPaint.EmbeddedNavigator.Buttons.Edit.Visible = false;
			gcPaint.EmbeddedNavigator.Buttons.Remove.Visible = false;
			gvPaint.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;

			gvPaint.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
			//   gridView1.OptionsBehavior.AutoPopulateColumns = false;
			gvPaint.OptionsBehavior.Editable = true;
			gvPaint.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
			gvPaint.OptionsCustomization.AllowColumnMoving = false;
			gvPaint.OptionsCustomization.AllowQuickHideColumns = true;
			gvPaint.OptionsCustomization.AllowSort = false;
			gvPaint.OptionsCustomization.AllowFilter = false;
			//     gvACQ.OptionsHint.ShowCellHints = true;
			gvPaint.OptionsView.ColumnAutoWidth = false;
			gvPaint.OptionsView.ShowGroupPanel = false;
			gvPaint.OptionsView.ShowIndicator = true;
			gvPaint.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
			gvPaint.OptionsView.ShowVerticalLines = DefaultBoolean.True;
			gvPaint.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			gvPaint.OptionsView.ShowAutoFilterRow = false;
			gvPaint.OptionsView.AllowCellMerge = false;
			gvPaint.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;



			gvPaint.Appearance.HeaderPanel.TextOptions.WordWrap = WordWrap.Wrap;
			//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

			gvPaint.OptionsNavigation.AutoFocusNewRow = true;
			gvPaint.OptionsNavigation.UseTabKey = true;

			gvPaint.OptionsSelection.MultiSelect = true;
			gvPaint.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
			gvPaint.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
			gvPaint.OptionsSelection.EnableAppearanceFocusedRow = false;
			gvPaint.OptionsSelection.EnableAppearanceFocusedCell = true;
			gvPaint.OptionsView.EnableAppearanceEvenRow = false;
			gvPaint.OptionsView.EnableAppearanceOddRow = false;

			gvPaint.OptionsView.ShowFooter = false;


			//   gridView1.RowHeight = 25;

			gvPaint.OptionsFind.AllowFindPanel = false;
			//gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
			gvPaint.OptionsFind.AlwaysVisible = false;
			gvPaint.OptionsFind.ShowCloseButton = true;
			gvPaint.OptionsFind.HighlightFindResults = true;



			gvPaint.OptionsPrint.PrintBandHeader = true;
			gvPaint.OptionsPrint.AllowCancelPrintExport = true;
			gvPaint.OptionsPrint.AllowMultilineHeaders = true;
			gvPaint.OptionsPrint.AutoResetPrintDocument = true;
			gvPaint.OptionsPrint.AutoWidth = false;
			gvPaint.OptionsPrint.PrintHeader = false;
			gvPaint.OptionsPrint.PrintPreview = true;
			gvPaint.OptionsPrint.PrintSelectedRowsOnly = false;
			gvPaint.OptionsPrint.UsePrintStyles = false;

			new MyFindPanelFilterHelper(gvPaint)
			{
				IsPerFormEvent = true,
				AllowSort = false,
				IsBestFitDoubleClick = true,
				IsDrawFilter = true,
			};
			gvPaint.OptionsMenu.EnableFooterMenu = false;
			gvPaint.OptionsMenu.EnableColumnMenu = false;




			gvPaint.RowCellStyle += gvPaint_RowCellStyle;

			gvPaint.LeftCoordChanged += gvPaint_LeftCoordChanged;
			gvPaint.MouseMove += gvPaint_MouseMove;
			gvPaint.TopRowChanged += gvPaint_TopRowChanged;
			gvPaint.FocusedColumnChanged += gvPaint_FocusedColumnChanged;
			gvPaint.FocusedRowChanged += gvPaint_FocusedRowChanged;
			gvPaint.RowCountChanged += gvPaint_RowCountChanged;
			gvPaint.CustomDrawRowIndicator += gvPaint_CustomDrawRowIndicator;

			gcPaint.Paint += gcPaint_Paint;
			gcPaint.KeyDown += gcPaint_KeyDown;
			gcPaint.EditorKeyDown += gcPaint_EditorKeyDown;
			gvPaint.ShowingEditor += gvPaint_ShowingEditor;
			gvPaint.CustomColumnDisplayText += GvPaint_CustomColumnDisplayText;
			gvPaint.DoubleClick += gvPaint_DoubleClick;




			gcPaint.ForceInitialize();



		}


		private void gvPaint_DoubleClick(object sender, EventArgs e)
		{
			var gv = sender as BandedGridView;
			if (gv == null) return;
			GridControl gc = gv.GridControl;
			BandedGridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(Control.MousePosition));
			if (!hi.InRowCell) return;
			BandedGridColumn gCol = hi.Column;
			if (gCol == null) return;
			_isSummaryClick = false;
			ShowDetalGroup(hi.RowHandle, gCol.FieldName);
		}

		private bool _checkKeyDown;
		private void gcPaint_EditorKeyDown(object sender, KeyEventArgs e)
		{
			if (!_checkKeyDown)
			{
				gcPaint_KeyDown(sender, e);
			}
			_checkKeyDown = false;
		}


		private void gcPaint_KeyDown(object sender, KeyEventArgs e)
		{
			var gc = (GridControl)sender;
			if (gc == null) return;
			var gv = (GridView)gc.FocusedView;
			if (gv == null) return;
			GridColumn gColF = gv.FocusedColumn;
			string fieldName = gColF.FieldName;

			int rH = gv.FocusedRowHandle;
			_checkKeyDown = true;


			#region "Process Enter key"

			if (e.KeyData == Keys.Enter)
			{
				_isSummaryClick = false;
				ShowDetalGroup(rH, fieldName);
				return;
			}

			#endregion







		}



		#region "Key Down"



		#endregion


		private void GvPaint_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			var gv = sender as GridView;
			if (gv == null) return;
			GridColumn gCol = e.Column;
			if (gCol == null) return;
			string fieldName = gCol.FieldName;

			if (fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_"))
			{
				e.DisplayText = "";
			}
		}
		private void gvPaint_ShowingEditor(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
		}
		private void gvPaint_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var gv = (GridView)sender;
			if (gv == null) return;
			int rH = e.RowHandle;
			if (rH < 0) return;
			GridColumn gCol = e.Column;
			if (gCol == null) return;
			int visibleIndex = gCol.VisibleIndex;
			if (visibleIndex < 0) return;
			string fieldName = gCol.FieldName;

			switch (fieldName)
			{
				case "OrderLine":
					e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
					e.Appearance.ForeColor = Color.DarkGreen;
					break;
				case "ProductCode":
					e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
					e.Appearance.ForeColor = Color.DarkRed;
					break;
				case "Reference":
					e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
					e.Appearance.ForeColor = Color.DarkRed;
					break;
				case "SOQty":
					e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
					e.Appearance.ForeColor = Color.DarkOrange;
					break;
			}

			if (gv.FocusedRowHandle == rH && gv.FocusedColumn != null && gv.FocusedColumn.FieldName == gCol.FieldName)
			{

				e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
				e.Appearance.BackColor = SystemCellColor.BackColorCellFocused;
				e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
				return;
			}
			if (gv.IsCellSelected(rH, gCol))
			{
				e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
				e.Appearance.BackColor = SystemCellColor.BackColorCellSelected;
				e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
				return;
			}


			if (fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_"))
			{
				string displayText = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, fieldName));
				ColorDemandPrInfoRpt infoColor;
				if (displayText != "" && _dicColor.TryGetValue(displayText, out infoColor))
				{
					//    e.Appearance.GradientMode = LinearGradientMode.Vertical;
					e.Appearance.BackColor = infoColor.ColorSet;
					//  e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
				}

			}
			else if (fieldName == "OrderLine" || fieldName == "SOQty")
			{
				e.Appearance.GradientMode = LinearGradientMode.Vertical;
				e.Appearance.BackColor = Color.WhiteSmoke;
				e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
			}
			else if (fieldName == "ProductCode" || fieldName == "Reference")
			{
				e.Appearance.GradientMode = LinearGradientMode.Vertical;
				e.Appearance.BackColor = Color.Cornsilk;
				e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
			}

		}

		private void gvPaint_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
		{
			GridView gv = (GridView)sender;
			if (gv == null) return;
			DrawRectangleSelection.RePaintGridView(((GridView)sender));
		}

		private void gvPaint_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
		{
			GridView gv = (GridView)sender;
			if (gv == null) return;
			DrawRectangleSelection.RePaintGridView(((GridView)sender));
		}

		private void gvPaint_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
		{
			GridView gv = (GridView)sender;
			if (gv == null) return;
			DrawRectangleSelection.RePaintGridView(((GridView)sender));
		}

		private void gvPaint_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
		{
			GridView gv = (GridView)sender;
			if (gv == null) return;
			DrawRectangleSelection.RePaintGridView(((GridView)sender));

		}
		private void gvPaint_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			GridView gv = (GridView)sender;
			if (gv == null) return;
			DrawRectangleSelection.RePaintGridView(((GridView)sender));

		}

		private void gcPaint_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
		{
			GridControl gc = (GridControl)sender;
			if (gc == null) return;
			DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
		}






		private void gvPaint_RowCountChanged(object sender, EventArgs e)
		{
			var gvP = sender as GridView;
			if (gvP == null) return;
			//  if (!gv.GridControl.IsHandleCreated) return;
			Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
			SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
			gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

			//GridPainter.Indicator.ImageSize.Width 
		}

		private void gvPaint_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
		{
			//GetStatusPriorityPaintOnRow(GridView gv, int rH)
			var gv = sender as GridView;
			if (gv == null) return;
			if (!gv.IsDataRow(e.RowHandle)) return;
			if (!e.Info.IsRowIndicator) return;

			e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
			e.Info.DisplayText = (e.RowHandle + 1).ToString();
			e.Info.ImageIndex = -1;
			e.Painter.DrawObject(e.Info);
			e.Handled = true;
			bool isSelected = gv.IsRowSelected(e.RowHandle);
			LinearGradientBrush backBrush;
			if (isSelected)
			{
				backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
			}
			else
			{
				backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
			}
			e.Graphics.FillRectangle(backBrush, e.Bounds);
			ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);

			if (isSelected)
			{
				e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
				e.Appearance.ForeColor = Color.DarkMagenta;
			}
			else
			{
				e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
				e.Appearance.ForeColor = Color.Black;
			}
			e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());



		}


		#endregion






		#region "GridView Event && Init Grid Summary"


		private void ResetDataSummaryGrid()
		{
			gvSumary.BeginUpdate();
			gvSumary.Bands.Clear();
			gvSumary.Columns.Clear();
			gcSumary.DataSource = null;
			gvSumary.EndUpdate();
		}


		private void LoadDataGridSummary(DataTable dtSumary, Dictionary<string, string> qLoop)
		{












			gvSumary.BeginUpdate();
			gvSumary.Bands.Clear();
			gvSumary.Columns.Clear();
			gcSumary.DataSource = null;

			CreateBandedGridSummaryHeader(dtSumary, qLoop);
			gcSumary.DataSource = dtSumary;
			BestFitBandsSummaryGrid(gvSumary);

			gvSumary.IndicatorWidth = gvPaint.IndicatorWidth;

			gvSumary.EndUpdate();



			//  LoadDataFinishing(spFinishing, dtLine);
		}

		private void CreateBandedGridSummaryHeader(DataTable dtS, Dictionary<string, string> dicCg)
		{

			GridBand[] arrBand = new GridBand[dtS.Columns.Count];

			int i = 0;
			foreach (DataColumn col in dtS.Columns)
			{
				BandedGridColumn gCol = new BandedGridColumn();
				gCol.AppearanceCell.Options.UseTextOptions = true;
				gCol.AppearanceHeader.Options.UseTextOptions = true;
				arrBand[i] = new GridBand();
				arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
				HorzAlignment hozAlign = HorzAlignment.Center;
				string colName = col.ColumnName;
				string displayText = "";
				if (colName == "ColorType")
				{
					displayText = "Color";
					arrBand[i].RowCount = 3;
				}
				else if (colName == "ColorDesc")
				{
					displayText = "Description";
					hozAlign = HorzAlignment.Near;
					//  arrBand[i].RowCount = 3;
				}
				else if (colName.StartsWith("FinishPR_"))
				{
					displayText = "PR";
					arrBand[i].RowCount = 1;
				}
				else if (colName.StartsWith("FinishPO_"))
				{
					displayText = "PO";
					arrBand[i].RowCount = 1;
				}
				else
				{
					displayText = colName;
				}



				//gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
				//gCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
				//arrBand[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;

				string[] arrTag = colName.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
				string tag = "";
				if (arrTag.Length == 2)
				{

					tag = arrTag[1].Trim();


				}



				gCol.AppearanceCell.TextOptions.HAlignment = hozAlign;
				gCol.AppearanceHeader.TextOptions.HAlignment = hozAlign;
				arrBand[i].AppearanceHeader.TextOptions.HAlignment = hozAlign;
				arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
				arrBand[i].AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
				arrBand[i].AppearanceHeader.Options.UseFont = true;
				arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
				arrBand[i].Tag = tag;

				gCol.Tag = tag;
				gCol.Caption = displayText;
				gCol.FieldName = colName;
				gCol.Visible = true;
				gCol.VisibleIndex = i;




				gvSumary.Columns.Add(gCol);
				arrBand[i].Caption = displayText;
				arrBand[i].Columns.Add(gCol);
				arrBand[i].VisibleIndex = 0;
				arrBand[i].Width = 100;
				i++;
			}
			gvSumary.Bands.AddRange(new[] { arrBand[0], arrBand[1] });

			int t = 2;
			foreach (var itemTemp in dicCg)
			{

				string sName = itemTemp.Value;
				string sKey = itemTemp.Key;
				GridBand gParent = new GridBand();
				gParent.AppearanceHeader.Options.UseTextOptions = true;
				gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
				gParent.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
				gParent.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
				gParent.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
				gParent.AppearanceHeader.Options.UseFont = true;
				gParent.RowCount = 2;
				gParent.Caption = string.Format("{0}-{1}", sKey, sName);
				gParent.VisibleIndex = t;
				gParent.Width = 100;
				// gParent.RowCount = 2;
				var q2 = arrBand.Where(p => ProcessGeneral.GetSafeString(p.Tag) == sKey).ToArray();
				for (int h = 0; h < q2.Length; h++)
				{
					q2[h].VisibleIndex = h;
					gParent.Children.Add(q2[h]);
				}
				gvSumary.Bands.Add(gParent);
				t++;
			}


		}
		private void BestFitBandsSummaryGrid(BandedGridView view)
		{

			view.BeginUpdate();
			view.OptionsView.ShowColumnHeaders = true;

			int widthCross = 0;
			int totalWidth = 0;
			foreach (BandedGridColumn col in gvPaint.VisibleColumns)
			{
				string fieldName = col.FieldName;
				if (fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_"))
				{
					view.Columns[fieldName].Width = col.Width;
					widthCross += col.Width;
				}

				totalWidth += col.Width;






			}
			int w1 = view.Columns["ColorType"].GetBestWidth() + 10;
			widthCross += w1;
			view.Columns["ColorType"].Width = w1;


			int leftWidth = totalWidth - widthCross;
			view.Columns["ColorDesc"].Width = leftWidth;
			view.OptionsView.ShowColumnHeaders = false;
			view.EndUpdate();


		}




		private void SetUpMainSummaryGrid()
		{


			gcSumary.UseEmbeddedNavigator = false;

			gcSumary.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
			gcSumary.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
			gcSumary.EmbeddedNavigator.Buttons.Append.Visible = false;
			gcSumary.EmbeddedNavigator.Buttons.Edit.Visible = false;
			gcSumary.EmbeddedNavigator.Buttons.Remove.Visible = false;
			gvSumary.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;

			gvSumary.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
			//   gridView1.OptionsBehavior.AutoPopulateColumns = false;
			gvSumary.OptionsBehavior.Editable = true;
			gvSumary.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
			gvSumary.OptionsCustomization.AllowColumnMoving = false;
			gvSumary.OptionsCustomization.AllowQuickHideColumns = true;
			gvSumary.OptionsCustomization.AllowSort = false;
			gvSumary.OptionsCustomization.AllowFilter = false;
			//     gvACQ.OptionsHint.ShowCellHints = true;
			gvSumary.OptionsView.ColumnAutoWidth = false;
			gvSumary.OptionsView.ShowGroupPanel = false;
			gvSumary.OptionsView.ShowIndicator = true;
			gvSumary.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
			gvSumary.OptionsView.ShowVerticalLines = DefaultBoolean.True;
			gvSumary.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			gvSumary.OptionsView.ShowAutoFilterRow = false;
			gvSumary.OptionsView.AllowCellMerge = false;
			gvSumary.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;



			gvSumary.Appearance.HeaderPanel.TextOptions.WordWrap = WordWrap.Wrap;
			//gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

			gvSumary.OptionsNavigation.AutoFocusNewRow = true;
			gvSumary.OptionsNavigation.UseTabKey = true;

			gvSumary.OptionsSelection.MultiSelect = true;
			gvSumary.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
			gvSumary.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
			gvSumary.OptionsSelection.EnableAppearanceFocusedRow = false;
			gvSumary.OptionsSelection.EnableAppearanceFocusedCell = true;
			gvSumary.OptionsView.EnableAppearanceEvenRow = false;
			gvSumary.OptionsView.EnableAppearanceOddRow = false;

			gvSumary.OptionsView.ShowFooter = false;


			//   gridView1.RowHeight = 25;

			gvSumary.OptionsFind.AllowFindPanel = false;
			//gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
			gvSumary.OptionsFind.AlwaysVisible = false;
			gvSumary.OptionsFind.ShowCloseButton = true;
			gvSumary.OptionsFind.HighlightFindResults = true;



			gvSumary.OptionsPrint.PrintBandHeader = true;
			gvSumary.OptionsPrint.AllowCancelPrintExport = true;
			gvSumary.OptionsPrint.AllowMultilineHeaders = true;
			gvSumary.OptionsPrint.AutoResetPrintDocument = true;
			gvSumary.OptionsPrint.AutoWidth = false;
			gvSumary.OptionsPrint.PrintHeader = false;
			gvSumary.OptionsPrint.PrintPreview = true;
			gvSumary.OptionsPrint.PrintSelectedRowsOnly = false;
			gvSumary.OptionsPrint.UsePrintStyles = false;

			new MyFindPanelFilterHelper(gvSumary)
			{
				IsPerFormEvent = true,
				AllowSort = false,
				IsBestFitDoubleClick = true,
				IsDrawFilter = true,
			};
			gvSumary.OptionsMenu.EnableFooterMenu = false;
			gvSumary.OptionsMenu.EnableColumnMenu = false;




			gvSumary.RowCellStyle += gvSumary_RowCellStyle;

			gvSumary.LeftCoordChanged += gvSumary_LeftCoordChanged;
			gvSumary.MouseMove += gvSumary_MouseMove;
			gvSumary.TopRowChanged += gvSumary_TopRowChanged;
			gvSumary.FocusedColumnChanged += gvSumary_FocusedColumnChanged;
			gvSumary.FocusedRowChanged += gvSumary_FocusedRowChanged;
			gvSumary.RowCountChanged += gvSumary_RowCountChanged;
			gvSumary.CustomDrawRowIndicator += gvSumary_CustomDrawRowIndicator;

			gcSumary.Paint += gcSumary_Paint;
			gcSumary.KeyDown += gcSumary_KeyDown;
			gcSumary.EditorKeyDown += gcSumary_EditorKeyDown;
			gvSumary.ShowingEditor += gvSumary_ShowingEditor;
			gvSumary.CustomColumnDisplayText += gvSumary_CustomColumnDisplayText;
			gvSumary.DoubleClick += gvSumary_DoubleClick;




			gcSumary.ForceInitialize();



		}


		private void gvSumary_DoubleClick(object sender, EventArgs e)
		{
			var gv = sender as BandedGridView;
			if (gv == null) return;
			GridControl gc = gv.GridControl;
			BandedGridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(Control.MousePosition));
			if (!hi.InRowCell) return;
			BandedGridColumn gCol = hi.Column;
			if (gCol == null) return;
			_isSummaryClick = true;
			ShowDetalGroup(hi.RowHandle, gCol.FieldName);
		}


		private void gcSumary_EditorKeyDown(object sender, KeyEventArgs e)
		{
			if (!_checkKeyDown)
			{
				gcSumary_KeyDown(sender, e);
			}
			_checkKeyDown = false;
		}


		private void gcSumary_KeyDown(object sender, KeyEventArgs e)
		{
			var gc = (GridControl)sender;
			if (gc == null) return;
			var gv = (GridView)gc.FocusedView;
			if (gv == null) return;
			GridColumn gColF = gv.FocusedColumn;
			string fieldName = gColF.FieldName;

			int rH = gv.FocusedRowHandle;
			_checkKeyDown = true;


			#region "Process Enter key"

			if (e.KeyData == Keys.Enter)
			{
				_isSummaryClick = true;
				ShowDetalGroup(rH, fieldName);
				return;
			}

			#endregion







		}



		#region "Key Down"



		#endregion


		private void gvSumary_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			var gv = sender as GridView;
			if (gv == null) return;
			GridColumn gCol = e.Column;
			if (gCol == null) return;
			string fieldName = gCol.FieldName;

			if (fieldName == "ColorType" || fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_"))
			{
				e.DisplayText = "";
			}
		}
		private void gvSumary_ShowingEditor(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
		}
		private void gvSumary_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var gv = (GridView)sender;
			if (gv == null) return;
			int rH = e.RowHandle;
			if (rH < 0) return;
			GridColumn gCol = e.Column;
			if (gCol == null) return;
			int visibleIndex = gCol.VisibleIndex;
			if (visibleIndex < 0) return;
			string fieldName = gCol.FieldName;

			switch (fieldName)
			{
				case "ColorDesc":
					e.Appearance.Font = new Font("Tahoma", 8.25F, (FontStyle.Bold), GraphicsUnit.Point, 0);
					e.Appearance.ForeColor = Color.DarkOrange;
					break;

			}

			if (gv.FocusedRowHandle == rH && gv.FocusedColumn != null && gv.FocusedColumn.FieldName == gCol.FieldName)
			{

				e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
				e.Appearance.BackColor = SystemCellColor.BackColorCellFocused;
				e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
				return;
			}
			if (gv.IsCellSelected(rH, gCol))
			{
				e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
				e.Appearance.BackColor = SystemCellColor.BackColorCellSelected;
				e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
				return;
			}


			if (fieldName == "ColorType" || fieldName.StartsWith("FinishPR_") || fieldName.StartsWith("FinishPO_"))
			{
				string displayText = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, fieldName));
				ColorDemandPrInfoRpt infoColor;
				if (displayText != "" && _dicColor.TryGetValue(displayText, out infoColor))
				{
					//    e.Appearance.GradientMode = LinearGradientMode.Vertical;
					e.Appearance.BackColor = infoColor.ColorSet;
					//  e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
				}

			}

			else
			{
				e.Appearance.GradientMode = LinearGradientMode.Vertical;
				e.Appearance.BackColor = Color.Cornsilk;
				e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
			}

		}

		private void gvSumary_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
		{
			GridView gv = (GridView)sender;
			if (gv == null) return;
			DrawRectangleSelection.RePaintGridView(((GridView)sender));
		}

		private void gvSumary_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
		{
			GridView gv = (GridView)sender;
			if (gv == null) return;
			DrawRectangleSelection.RePaintGridView(((GridView)sender));
		}

		private void gvSumary_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
		{
			GridView gv = (GridView)sender;
			if (gv == null) return;
			DrawRectangleSelection.RePaintGridView(((GridView)sender));
		}

		private void gvSumary_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
		{
			GridView gv = (GridView)sender;
			if (gv == null) return;
			DrawRectangleSelection.RePaintGridView(((GridView)sender));

		}
		private void gvSumary_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			GridView gv = (GridView)sender;
			if (gv == null) return;
			DrawRectangleSelection.RePaintGridView(((GridView)sender));

		}

		private void gcSumary_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
		{
			GridControl gc = (GridControl)sender;
			if (gc == null) return;
			DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
		}






		private void gvSumary_RowCountChanged(object sender, EventArgs e)
		{
			//var gvP = sender as GridView;
			//if (gvP == null) return;
			////  if (!gv.GridControl.IsHandleCreated) return;
			//Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
			//SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
			//gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

			//GridPainter.Indicator.ImageSize.Width 
		}

		private void gvSumary_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
		{
			//GetStatusPriorityPaintOnRow(GridView gv, int rH)
			var gv = sender as GridView;
			if (gv == null) return;
			if (!gv.IsDataRow(e.RowHandle)) return;
			if (!e.Info.IsRowIndicator) return;

			e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
			e.Info.DisplayText = (e.RowHandle + 1).ToString();
			e.Info.ImageIndex = -1;
			e.Painter.DrawObject(e.Info);
			e.Handled = true;
			bool isSelected = gv.IsRowSelected(e.RowHandle);
			LinearGradientBrush backBrush;
			if (isSelected)
			{
				backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
			}
			else
			{
				backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
			}
			e.Graphics.FillRectangle(backBrush, e.Bounds);
			ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);

			if (isSelected)
			{
				e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
				e.Appearance.ForeColor = Color.DarkMagenta;
			}
			else
			{
				e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
				e.Appearance.ForeColor = Color.Black;
			}
			e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());



		}


		#endregion





























		#region "Show Tooltip"



		private void toolTipControllerMain_GetActiveObjectInfo(object sender,
			ToolTipControllerGetActiveObjectInfoEventArgs e)
		{


			GridControl gc = null;

			if (e.SelectedControl == gcPaint)
			{
				gc = gcPaint;
			}
			else if (e.SelectedControl == gcSumary)
			{
				gc = gcSumary;
			}

			if (gc != null)
			{
				GridView gv = (GridView)gc.FocusedView;
				if (gv == null) return;
				GridHitInfo hi = gv.CalcHitInfo(e.ControlMousePosition);
				if (!hi.InRowCell) return;
				GridColumn gCol = hi.Column;
				if (gCol == null) return;
				string fieldName = gCol.FieldName;
				if (!fieldName.StartsWith("FinishPR_") && !fieldName.StartsWith("FinishPO_")) return;
				int rH = hi.RowHandle;
				if (!gv.IsDataRow(rH)) return;
				string displayText = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, fieldName));
				ColorDemandPrInfoRpt infoColor;
				if (!_dicColor.TryGetValue(displayText, out infoColor)) return;
				string text = infoColor.DescriptionSet;
				if (string.IsNullOrEmpty(text)) return;







				SuperToolTip sTooltip = new SuperToolTip();
				// Create an object to initialize the SuperToolTip.
				SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();
				args.Title.Text = @"<size=12> <color=blue><b>Info</b></color></size>";
				if (text.Contains("Missing PR") && fieldName.StartsWith("FinishPO_"))
				{
					text = text.Replace("Missing PR", "Missing PO");
				}
				args.Contents.Text = text;
				sTooltip.Setup(args);
				// Enable HTML formatting.
				sTooltip.AllowHtmlText = DefaultBoolean.True;

				e.Info = new ToolTipControlInfo();
				//<bold>Updated by John (DevExpress Support):</bold>
				//e.Info.Object = hitInfo.Column;
				e.Info.Object = hi.HitTest.ToString() + hi.RowHandle.ToString(); //NewLine
				//<bold>End Update</bold>
				e.Info.ToolTipType = ToolTipType.SuperTip;
				e.Info.SuperTip = sTooltip;
				// e.Info.SuperTip.Setup(toolTipArgs);

			}/*
			if (e.SelectedControl == gcPaint)
			{
			  
				var gv = gcPaint.FocusedView as GridView;
				if (gv == null) return;
				if (gv.RowCount <= 0) return;


				GridHitInfo hiG = gv.CalcHitInfo(gcPaint.PointToClient(MousePosition));
				if (!hiG.InRowCell) return;
				GridColumn colG = hiG.Column;
				if (colG == null) return;
				if (!colG.FieldName.StartsWith("AreaPaint")) return;

				int rH = hiG.RowHandle;

				string sFormular = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "FormulaStringPaint"));
				if (string.IsNullOrEmpty(sFormular)) return;

				object oG = hiG.HitTest.ToString() + rH.ToString(); //NewLine
				ToolTipControlInfo infoG = new ToolTipControlInfo(oG, sFormular, ToolTipIconType.Information) { AllowHtmlText = DefaultBoolean.True };
				e.Info = infoG;


		  

			}


			*/










		}

		#endregion


		#region "Show Detail Group"

		private void ShowDetalGroup(int rH, string fielName)
		{

			GridView gv = _isSummaryClick ? gvSumary : gvPaint;

			if (!gv.IsDataRow(rH)) return;
			if (!fielName.StartsWith("FinishPR_") && !fielName.StartsWith("FinishPO_")) return;
			string text = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, fielName));
			if (string.IsNullOrEmpty(text)) return;

            
			List<Int64> lCny00020Pk = new List<Int64>();
			if (!_isSummaryClick)
			{
				Int64 cny00020Pk = ProcessGeneral.GetSafeInt64(gv.GetRowCellValue(rH, "CNY00020PK"));
				if (cny00020Pk > 0)
				{
					lCny00020Pk.Add(cny00020Pk);
				}

			
			}
			else
			{
				


				for (int i = 0; i < gvPaint.RowCount; i++)
				{
					Int64 cny00020Pk = ProcessGeneral.GetSafeInt64(gvPaint.GetRowCellValue(i, "CNY00020PK"));
					if (cny00020Pk > 0)
					{
						lCny00020Pk.Add(cny00020Pk);
					}
				}
			}

            string mainGroup = ProcessGeneral.GetDescriptionInString(fielName, "_");


          




		
			ShowGridMain(false);



            WaitDialogForm dlg = new WaitDialogForm();
            DataTable dtFinal = _ctrl.CalDataDetailByGroup(mainGroup, lCny00020Pk);
            LoadDataGridMainDetail(dtFinal);
            dlg.Close();
        }


        #endregion


        #region "GridView Event && Init Grid"


       

       
        private void LoadDataGridMainDetail(DataTable dtGrid)
        {
            
            gvMain.BeginUpdate();
            gvMain.Bands.Clear();
            gvMain.Columns.Clear();
            gcMain.DataSource = null;
            Dictionary<string, string> dicAttCol;
            Dictionary<string, Tuple<string, string>> dicChange = CreateBandedGridHeader(dtGrid, out dicAttCol);
            gcMain.DataSource = dtGrid;
            gvMain.Columns["MainMaterialGroup"].GroupIndex = 0;
            gvMain.ExpandAllGroups();
            if (!gvMain.OptionsView.ShowColumnHeaders)
                gvMain.OptionsView.ShowColumnHeaders = true;
            gvMain.BestFitColumns();
            //gvMain.Columns["ImgStatus"].Width = 40;
            if (dicChange.Count > 0)
            {
                foreach (var itemChange in dicChange)
                {

                    gvMain.Columns[itemChange.Key].Caption = "<b>" + itemChange.Value.Item1 + "<b>";
                }
            }

           
            gvMain.Columns["RMCode_001"].OwnerBand.Fixed = GridFixedCol.Left;
            gvMain.Columns["RMDescription_002"].OwnerBand.Fixed = GridFixedCol.Left;


       


            gvMain.Columns["RMCode_001"].Image = Resources.reverssort_16x16;
            gvMain.Columns["RMCode_001"].ImageAlignment = StringAlignment.Near;
            gvMain.Columns["RMCode_001"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gvMain.Columns["RMCode_001"].Caption = "Item Code";

            gvMain.Columns["RMDescription_002"].Image = Resources.reverssort_16x16;
            gvMain.Columns["RMDescription_002"].ImageAlignment = StringAlignment.Near;
            gvMain.Columns["RMDescription_002"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gvMain.Columns["RMDescription_002"].Caption = "Item Name";

            gvMain.Columns["Supplier"].Image = Resources.reverssort_16x16;
            gvMain.Columns["Supplier"].ImageAlignment = StringAlignment.Near;
            gvMain.Columns["Supplier"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gvMain.Columns["Supplier"].Caption = "Supplier";




            


            foreach (var itemAtt in dicAttCol)
            {
                gvMain.Columns[itemAtt.Key].Image = Resources.reverssort_16x16;
                gvMain.Columns[itemAtt.Key].ImageAlignment = StringAlignment.Near;
                gvMain.Columns[itemAtt.Key].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
                gvMain.Columns[itemAtt.Key].Caption = itemAtt.Value;
            }

            //gvMain.Columns["IsRightRM"].Image = Resources.reverssort_16x16;
            //gvMain.Columns["RMCode_001"].ImageIndex = 0;
            //gvMain.Columns["RMCode_001"].ImageAlignment = StringAlignment.Near;

            gvMain.EndUpdate();


           

            
        }
    

       
        private Dictionary<string, Tuple<string, string>> CreateBandedGridHeader(DataTable dtS, out Dictionary<string, string> dicAttCol)
        {
            dicAttCol = new Dictionary<string, string>();
            GridBand[] arrBand = new GridBand[dtS.Columns.Count];
            Dictionary<string, Tuple<int, string>> dicColVisible = new Dictionary<string, Tuple<int, string>>();
            Dictionary<string, Tuple<int, string, string, string>> dicColChildVisible = new Dictionary<string, Tuple<int, string, string, string>>();
            int i = 0;
            Dictionary<string, Tuple<string, string>> dicChangeHeader = new Dictionary<string, Tuple<string, string>>();
            foreach (DataColumn col in dtS.Columns)
            {
                BandedGridColumn gCol = new BandedGridColumn();
                gCol.AppearanceCell.Options.UseTextOptions = true;
                gCol.AppearanceHeader.Options.UseTextOptions = true;
                arrBand[i] = new GridBand();
                arrBand[i].AppearanceHeader.Options.UseTextOptions = true;
                HorzAlignment hozAlignCol = HorzAlignment.Center;
                HorzAlignment hozAlignBan = HorzAlignment.Center;
                string colName = col.ColumnName;
                string bandCaption = "";
                string colCaption = "";
                if (colName == "MainMaterialGroup")
                {
                    dicColVisible.Add(colName, new Tuple<int, string>(i, "GR"));
                    colCaption = "Main Material Group";
                    bandCaption = "Main Material Group";
                    hozAlignCol = HorzAlignment.Near;
                    hozAlignBan = HorzAlignment.Near;

                }
                else if (colName == "RMCode_001")
                {
                    dicColVisible.Add(colName, new Tuple<int, string>(i, ""));
                    colCaption = "Item Code (F4)";
                    bandCaption = "Item Code (F4)";
                    hozAlignCol = HorzAlignment.Near;
                    hozAlignBan = HorzAlignment.Near;

                }
                else if (colName == "RMDescription_002")
                {
                    dicColVisible.Add(colName, new Tuple<int, string>(i, ""));
                    colCaption = "Item Name (F4)";
                    bandCaption = "Item Name (F4)";
                    hozAlignCol = HorzAlignment.Near;
                    hozAlignBan = HorzAlignment.Near;

                }
                else if (colName == "ColorReference")
                {
                    dicColVisible.Add(colName, new Tuple<int, string>(i, ""));
                    colCaption = "Color";
                    bandCaption = "Color";
                    hozAlignCol = HorzAlignment.Near;
                    hozAlignBan = HorzAlignment.Near;

                }
                else if (colName == "Supplier")
                {
                    dicColVisible.Add(colName, new Tuple<int, string>(i, ""));
                    colCaption = "Supplier (F4)";
                    bandCaption = "Supplier (F4)";
                    hozAlignCol = HorzAlignment.Near;
                    hozAlignBan = HorzAlignment.Near;

                }
                else if (colName == "SupplierRef")
                {
                    dicColVisible.Add(colName, new Tuple<int, string>(i, ""));
                    colCaption = "Supplier Ref (F4)";
                    bandCaption = "Supplier Ref (F4)";
                    hozAlignCol = HorzAlignment.Near;
                    hozAlignBan = HorzAlignment.Near;
                }
                else if (colName == "UCUnit")
                {
                    dicColVisible.Add(colName, new Tuple<int, string>(i, ""));
                    colCaption = "Unit";
                    bandCaption = "Unit";
                    hozAlignCol = HorzAlignment.Center;
                    hozAlignBan = HorzAlignment.Center;

                }
                else if (colName.IndexOf("-", StringComparison.OrdinalIgnoreCase) > 0)
                {
                    string[] arrAttValue = colName.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    dicColVisible.Add(colName, new Tuple<int, string>(i, ""));
                    colCaption = arrAttValue[1].Trim();
                    dicAttCol.Add(colName, colCaption);
                    bandCaption = colCaption;
                    hozAlignCol = HorzAlignment.Center;
                    hozAlignBan = HorzAlignment.Center;
                    gCol.AppearanceHeader.Options.UseForeColor = true;
                    gCol.AppearanceHeader.ForeColor = Color.DarkGreen;
                    gCol.Tag = ProcessGeneral.GetSafeInt64(arrAttValue[0].Trim());



                }
                
                else if (colName == "TotalDemandQty")
                {
                    dicColVisible.Add(colName, new Tuple<int, string>(i, "PO"));
                    colCaption = "BOM";
                    bandCaption = "BOM";
                    hozAlignCol = HorzAlignment.Far;
                    hozAlignBan = HorzAlignment.Center;
                    gCol.DisplayFormat.FormatType = FormatType.Numeric;
                    gCol.DisplayFormat.FormatString = FunctionFormatModule.StrFormatDefaultDecimal(false, false);
                    gCol.AppearanceHeader.Options.UseForeColor = true;
                    gCol.AppearanceHeader.ForeColor = Color.DarkMagenta;

                }
                else if (colName == "PRQty_PR")
                {
                    dicColVisible.Add(colName, new Tuple<int, string>(i, "PO"));
                    colCaption = "Require";
                    bandCaption = "Require";
                    hozAlignCol = HorzAlignment.Far;
                    hozAlignBan = HorzAlignment.Center;
                    gCol.DisplayFormat.FormatType = FormatType.Numeric;
                    gCol.DisplayFormat.FormatString = FunctionFormatModule.StrFormatDefaultDecimal(false, false);
                    gCol.AppearanceHeader.Options.UseForeColor = true;
                    gCol.AppearanceHeader.ForeColor = Color.DarkMagenta;

                }
                else if (colName == "POQty")
                {
                    dicColVisible.Add(colName, new Tuple<int, string>(i, "PO"));
                    colCaption = "Purchase";
                    bandCaption = "Purchase";
                    hozAlignCol = HorzAlignment.Far;
                    hozAlignBan = HorzAlignment.Center;
                    gCol.DisplayFormat.FormatType = FormatType.Numeric;
                    gCol.DisplayFormat.FormatString = FunctionFormatModule.StrFormatDefaultDecimal(false, false);
                    gCol.AppearanceHeader.Options.UseForeColor = true;
                    gCol.AppearanceHeader.ForeColor = Color.DarkMagenta;

                }
                else
                {
                    dicColVisible.Add(colName, new Tuple<int, string>(i, "HI"));
                    colCaption = colName;
                    bandCaption = colName;
                }

                gCol.AppearanceCell.TextOptions.HAlignment = hozAlignCol;
                gCol.AppearanceHeader.TextOptions.HAlignment = hozAlignBan;

                arrBand[i].AppearanceHeader.TextOptions.HAlignment = hozAlignBan;
                arrBand[i].AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
                arrBand[i].AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
                arrBand[i].AppearanceHeader.Options.UseFont = true;
                arrBand[i].AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                gCol.Caption = "<b>" + colCaption + "<b>";
                gCol.FieldName = colName;
                gCol.Visible = true;
                gCol.VisibleIndex = i;
                gvMain.Columns.Add(gCol);
                arrBand[i].Caption = bandCaption;
                arrBand[i].Columns.Add(gCol);
                arrBand[i].VisibleIndex = 0;
                arrBand[i].Width = 100;
                i++;
            }


            var q1 = dicColVisible.GroupBy(p => p.Value.Item2).Select(t => new
            {
                Str = t.Key,
                MinInd = t.Min(s => s.Value.Item1),
                MaxInd = t.Max(s => s.Value.Item1),
            }).OrderBy(p => p.MinInd).ToList();


            int h = 0;
            List<string> lBandHide = new List<string>();
            foreach (var item in q1)
            {
                string str = item.Str;
                int min = item.MinInd;
                int max = item.MaxInd;
                if (str == "GR")
                {
                    GridBand gRoot = CreateParentBandGrid("Group", 2);
                    gRoot.VisibleIndex = h;
                    gRoot.Name = @"GRBand";
                    for (int j = min; j <= max; j++)
                    {
                        gRoot.Children.Add(arrBand[j]);
                    }
                    gvMain.Bands.Add(gRoot);
                    h++;
                }
                else if (str == "")
                {

                    GridBand gRootAtt = null;



                    for (int j = min; j <= max; j++)
                    {
                        BandedGridColumn gCol = gvMain.Columns[j];
                        arrBand[j].Columns.Clear();
                        string fieldName = gCol.FieldName;
                        if (fieldName.Contains("-"))
                        {
                            if (gRootAtt == null)
                            {
                                gRootAtt = CreateParentBandGrid("Attributes", 1);
                                gRootAtt.VisibleIndex = h;
                                gRootAtt.Name = @"FirstBand-Att";
                                gRootAtt.AppearanceHeader.Options.UseForeColor = true;
                                gRootAtt.AppearanceHeader.ForeColor = Color.DarkGreen;
                                gvMain.Bands.Add(gRootAtt);
                                h++;
                            }
                            gRootAtt.Columns.Add(gCol);
                        }
                        else
                        {
                            string bandName = @"FirstBand-" + j;
                            GridBand gRoot = CreateParentBandGrid(".", 1);
                            gRoot.VisibleIndex = h;
                            gRoot.Name = bandName;
                            gRoot.AppearanceHeader.Options.UseForeColor = true;
                            gRoot.AppearanceHeader.ForeColor = Color.Transparent;
                            gRoot.Columns.Add(gCol);
                            gvMain.Bands.Add(gRoot);
                            h++;
                            /*
                            if (fieldName == "Supplier" || fieldName == "SupplierRef")
                            {
                                lBandHide.Add(bandName);
                            }*/
                        }



                    }

                }
                else if (str == "PO")
                {
                    GridBand gRoot = CreateParentBandGrid("Quantity", 1);
                    gRoot.VisibleIndex = h;
                    gRoot.Name = @"POBand";
                    gRoot.AppearanceHeader.Options.UseForeColor = true;
                    gRoot.AppearanceHeader.ForeColor = Color.DarkMagenta;
                    for (int j = min; j <= max; j++)
                    {
                        arrBand[j].Columns.Clear();
                        gRoot.Columns.Add(gvMain.Columns[j]);
                     
                    }
                    gvMain.Bands.Add(gRoot);
                    h++;
                }
                else if (str == "HI")
                {
                    GridBand gRoot = CreateParentBandGrid("Hide", 2);
                    gRoot.VisibleIndex = h;
                    gRoot.Name = @"HIBand";
                    for (int j = min; j <= max; j++)
                    {
                        arrBand[j].Columns.Clear();
                        gRoot.Columns.Add(gvMain.Columns[j]);
                    }
                    gvMain.Bands.Add(gRoot);
                    h++;
                }
            }

            gvMain.Bands["HIBand"].Visible = false;
            foreach (string bandName in lBandHide)
            {
                gvMain.Bands[bandName].Visible = false;
            }
            gvMain.Bands["GRBand"].Visible = false;
            return dicChangeHeader;
        }


        private GridBand CreateParentBandGrid(string caption, int rowCount, bool isBold = true)
        {
            GridBand gParent = new GridBand();
            gParent.AppearanceHeader.Options.UseTextOptions = true;
            gParent.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gParent.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            gParent.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
            if (isBold)
            {
                gParent.AppearanceHeader.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);

            }


            gParent.AppearanceHeader.Options.UseFont = true;
            gParent.RowCount = rowCount;
            gParent.Caption = caption;

            gParent.Width = 100;
            return gParent;
        }




        private void SetUpMainGridDetail()
        {


            gcMain.UseEmbeddedNavigator = true;

            gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gvMain.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;

            gvMain.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvMain.OptionsBehavior.Editable = true;
            gvMain.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvMain.OptionsCustomization.AllowColumnMoving = false;
            gvMain.OptionsCustomization.AllowQuickHideColumns = true;
            gvMain.OptionsCustomization.AllowSort = false;
            gvMain.OptionsCustomization.AllowFilter = false;
            gvMain.OptionsCustomization.AllowBandMoving = false;
            //     gvACQ.OptionsHint.ShowCellHints = true;
            gvMain.OptionsView.ColumnAutoWidth = false;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowIndicator = true;
            gvMain.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvMain.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.OptionsView.AllowCellMerge = false;
            gvMain.OptionsView.ShowBands = true;
            gvMain.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;

            gvMain.OptionsView.AllowHtmlDrawHeaders = true;
            gvMain.OptionsView.AllowHtmlDrawGroups = true;
            gvMain.Appearance.HeaderPanel.TextOptions.WordWrap = WordWrap.Wrap;
            //gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvMain.OptionsNavigation.AutoFocusNewRow = true;
            gvMain.OptionsNavigation.UseTabKey = true;

            gvMain.OptionsSelection.MultiSelect = true;
            gvMain.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvMain.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvMain.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvMain.OptionsView.EnableAppearanceEvenRow = false;
            gvMain.OptionsView.EnableAppearanceOddRow = false;

            gvMain.OptionsView.ShowFooter = false;
            gvMain.OptionsBehavior.EditorShowMode = EditorShowMode.MouseUp;

            //   gridView1.RowHeight = 25;

            gvMain.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.AlwaysVisible = false;
            gvMain.OptionsFind.ShowCloseButton = true;
            gvMain.OptionsFind.HighlightFindResults = true;



            gvMain.OptionsPrint.PrintBandHeader = true;
            gvMain.OptionsPrint.AllowCancelPrintExport = true;
            gvMain.OptionsPrint.AllowMultilineHeaders = true;
            gvMain.OptionsPrint.AutoResetPrintDocument = true;
            gvMain.OptionsPrint.AutoWidth = false;
            gvMain.OptionsPrint.PrintHeader = false;
            gvMain.OptionsPrint.PrintPreview = true;
            gvMain.OptionsPrint.PrintSelectedRowsOnly = false;
            gvMain.OptionsPrint.UsePrintStyles = false;

            new MyFindPanelFilterHelper(gvMain)
            {
                IsPerFormEvent = true,
                AllowSort = false,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };
            gvMain.OptionsMenu.EnableFooterMenu = false;
            gvMain.OptionsMenu.EnableColumnMenu = true;
            gvMain.RowCellStyle += gvMain_RowCellStyle;
            gvMain.LeftCoordChanged += gvMain_LeftCoordChanged;
            gvMain.MouseMove += gvMain_MouseMove;
            gvMain.TopRowChanged += gvMain_TopRowChanged;
            gvMain.FocusedColumnChanged += gvMain_FocusedColumnChanged;
            gvMain.FocusedRowChanged += gvMain_FocusedRowChanged;
            gvMain.RowCountChanged += gvMain_RowCountChanged;
            gvMain.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;
            gcMain.Paint += gcMain_Paint;
            gvMain.ShowingEditor += gvMain_ShowingEditor;
            gvMain.GroupLevelStyle += GvMain_GroupLevelStyle;
            gvMain.CustomDrawGroupRow += GvMain_CustomDrawGroupRow;
            gvMain.CustomColumnDisplayText += GvMain_CustomColumnDisplayText;
            gcMain.ForceInitialize();
        }



      
        private void gvMain_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

        }

      


      
      
        private void GvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;
            string fieldName = e.Column.FieldName;
            if (!e.IsForGroupRow)
            {
                if (fieldName == "TotalDemandQty" || fieldName == "PRQty_PR" || fieldName.EndsWith("POQty"))
                {
                    string text = e.DisplayText;
                    if (text == "0")
                    {
                        e.DisplayText = "";
                    }

                }
            }
        }

        private void GvMain_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;
            GridGroupRowInfo groupRowInfo = e.Info as GridGroupRowInfo;
            if (groupRowInfo != null)
            {
                Rectangle groupRowBounds = groupRowInfo.DataBounds;
                Rectangle expandButtonBounds = groupRowInfo.ButtonBounds;
                Rectangle textBounds = e.Bounds;
                textBounds.X = expandButtonBounds.Right + 4;


                Brush brush = e.Cache.GetGradientBrush(groupRowBounds, Color.LemonChiffon, Color.Tan, LinearGradientMode.Horizontal);

                Brush brushText = Brushes.Black, brushTextShadow = Brushes.White;
                if (e.RowHandle == gv.FocusedRowHandle)
                {
                    brush = brushTextShadow = Brushes.DarkBlue;
                    brushText = Brushes.White;
                }


                e.Graphics.FillRectangle(brush, groupRowBounds);

                Image img = gv.GetRowExpanded(e.RowHandle)
                    ? Properties.Resources._1450289438_bullet_toggle_minus
                    : Properties.Resources._1450289456_bullet_toggle_plus;
                e.Graphics.DrawImageUnscaled(img,
                    expandButtonBounds);

                string s = groupRowInfo.GroupValueText.Substring(2, groupRowInfo.GroupValueText.Length - 2).Trim();

                s = string.Format("{0} (count= {1})", s, gv.GetChildRowCount(e.RowHandle));
                e.Appearance.DrawString(e.Cache, s, new Rectangle(textBounds.X + 1, textBounds.Y + 1,
                    textBounds.Width, textBounds.Height), brushTextShadow);
                e.Appearance.DrawString(e.Cache, s, textBounds, brushText);
            }

            e.Handled = true;
        }

        private void GvMain_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            e.LevelAppearance.BackColor = Color.LemonChiffon;
        }


       


     

   
     

        #region "Key Down"



        #endregion

     


        private void gvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = (GridView)sender;
            if (gv == null) return;
            int rH = e.RowHandle;
            if (rH < 0) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;
            int visibleIndex = gCol.VisibleIndex;
            if (visibleIndex < 0) return;
            string fieldName = gCol.FieldName;
           


            


           
            if (gv.FocusedRowHandle == rH && gv.FocusedColumn != null && gv.FocusedColumn.FieldName == gCol.FieldName)
            {

                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellFocused;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }
            if (gv.IsCellSelected(rH, gCol))
            {
                e.Appearance.GradientMode = LinearGradientMode.ForwardDiagonal;
                e.Appearance.BackColor = SystemCellColor.BackColorCellSelected;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
                return;
            }

            if (fieldName == "TotalDemandQty" || fieldName == "PRQty_PR" || fieldName == "POQty")
            {
                e.Appearance.ForeColor = Color.DarkMagenta;
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.GhostWhite;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }


        }

        private void gvMain_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvMain_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvMain_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvMain_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(gv);
        
        }
        private void gvMain_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(gv);
        }



        private void gcMain_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvMain_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            if (!e.Info.IsRowIndicator) return;

            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
            e.Painter.DrawObject(e.Info);
            e.Handled = true;
            bool isSelected = gv.IsRowSelected(e.RowHandle);
           

            if (isSelected)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.DarkMagenta;
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                e.Appearance.ForeColor = Color.Black;
            }
            e.Graphics.DrawString(e.Info.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), e.Bounds, e.Appearance.GetStringFormat());



        }


        #endregion
    }

}