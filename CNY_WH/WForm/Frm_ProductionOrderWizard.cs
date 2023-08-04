using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CNY_BaseSys.Class;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_WH.Class;
using CNY_WH.Common;
using CNY_WH.Info;
using CNY_WH.Properties;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using CellValueChangedEventArgs = DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs;

using DataRow = System.Data.DataRow;
using FocusedColumnChangedEventArgs = DevExpress.XtraTreeList.FocusedColumnChangedEventArgs;

using TreeFixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;

using GridFixedCol = DevExpress.XtraGrid.Columns.FixedStyle;
using SummaryItemType = DevExpress.Data.SummaryItemType;


namespace CNY_WH.WForm

{

    public partial class Frm_ProductionOrderWizard : XtraForm
    {
        #region "Property & Field"


        private readonly Inf_ProductionOrderWizard _inf = new Inf_ProductionOrderWizard();
        public Dictionary<Int64, DataTable> DicTableColorRoot { get; set; }





        private WaitDialogForm _dlg = null;
        private DataTable _dtFinalBoM = new DataTable();
        private DataTable _dtFinalSo = new DataTable();



        public event SDRWizardHandle OnSDRWizard = null;


        private const Int32 TotalStep = 3;
        private Int32 _step = 1;

        private Dictionary<Int64, AttributeHeaderInfo> _dicAttributeRm = new Dictionary<Int64, AttributeHeaderInfo>();//PKAtt + Name= Code + Desc

        Dictionary<Int64, List<BoMInputAttInfo>> _dicAttValueItemCode = new Dictionary<long, List<BoMInputAttInfo>>();


        private Dictionary<Int64, AttributeHeaderInfo> _dicAttributeSo = new Dictionary<Int64, AttributeHeaderInfo>();//PKAtt + Name= Code + Desc
        Dictionary<Int64, List<BoMInputAttInfo>> _dicAttValueSo = new Dictionary<long, List<BoMInputAttInfo>>();

        private readonly RepositoryItemSpinEdit _repositorySpinN0;

        //private DataTable _dtSelectedPr = Ctrl_PrGenneral.TableTempBomsoSelectedNpr();

        //public DataTable DtSelectedPr
        //{
        //    get { return this._dtSelectedPr; }


        //    set { this._dtSelectedPr = value; }

        //}
        private bool _performEditValueChangeEvent = true;
        public Dictionary<Int64, AttributeHeaderInfo> DicAttributeRoot { get; set; }

        public Dictionary<Int64, List<BoMInputAttInfo>> DicAttValueRoot { get; set; }
        public Dictionary<Int64, List<BoMInputAttInfo>> DicAttValueRmRoot { get; set; }
        public Dictionary<Int64, AttributeHeaderInfo> DicAttributeRMRoot { get; set; }
        public DataTable DtSourceTree { get; set; }

        private DateTime _etdDate = new DateTime(1900, 1, 1);

        public DateTime EtdDate { get { return this._etdDate; } set { this._etdDate = value; } }


        private DateTime _etaDate = new DateTime(1900, 1, 1);
        public DateTime EtaDate { get { return this._etaDate; } set { this._etaDate = value; } }


 
        private readonly string _productionOrderNo;
        #endregion


        #region "Contructor"

        public Frm_ProductionOrderWizard(string productionOrderNo)
        {
            InitializeComponent();
            this._productionOrderNo = productionOrderNo;
        
            _repositorySpinN0 = new RepositoryItemSpinEdit
            {
                AutoHeight = false,
                MinValue = 0,
                MaxValue = decimal.MaxValue,
                AllowMouseWheel = false,
                EditMask = "N0"
            };


            _repositorySpinN0.Buttons.Clear();

            InitTreeList(tlSO);
            InitTreeListItemCode(tlItemCode);

            InitTreeListFinal(tlFinal);
            //InitTreeListMain(tlMain);
            GridViewColorCustomInit();
            GridViewDetailCustomInit();
            GridViewProductionCustomInit();
            this.MinimizeBox = false;

            this.Load += Form_Load;

            btnCancel.Click += btnCancel_Click;
            btnNextFinish.Click += btnNextFinish_Click;
            btnBack.Click += btnBack_Click;
            btnSearch.Click += BtnSearch_Click;
            txtCust.KeyDown += TxtCust_KeyDown;
            txtProductionOrder.KeyDown += TxtProductionOrder_KeyDown;
            txtCust.Properties.CharacterCasing = CharacterCasing.Upper;
            txtProductionOrder.Properties.CharacterCasing = CharacterCasing.Upper;
        }




        #endregion

        #region "Form Event"


        private void Form_Load(object sender, EventArgs e)
        {

            _step = 1;

            VisibleTabPageByStep();
            SetupButtonNextFinished();



            txtCust.Select();

          

            if (!string.IsNullOrEmpty(_productionOrderNo))

            {
                txtProductionOrder.Text = _productionOrderNo;
                BtnSearch_Click(null, null);

            }

            splitContainerControl1.SplitterPosition = 0; // không hiện split bên phải ở tab thứ 2
            splitCCB.SplitterPosition = 0; // không hiện split bên phải ở tab finish

        }



        #endregion

        #region "Search SO"

        private void ShowListCustomerF4OnTree(TextEdit tE)
        {
            DataTable dtSource = _inf.LoadListCustomerW();

            #region "Init Column"



            var lG = new List<GridViewTransferDataColumnInit>

            {
                new GridViewTransferDataColumnInit


                {
                    Caption = @"Customer",

                    FieldName = "Customer",
                    HorzAlign = HorzAlignment.Near,

                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,
                    VisibleIndex = 0,

                    FormatField = FormatType.None,

                    FormatString = "",

                    IncreaseWdith = 10,
                    SummayType = DevExpress.Data.SummaryItemType.None,

                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
                },
                new GridViewTransferDataColumnInit

                {
                    Caption = @"Name",
                    FieldName = "CustomerName",
                    HorzAlign = HorzAlignment.Near,

                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,
                    VisibleIndex = 1,

                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 10,
                    SummayType = DevExpress.Data.SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center

                },


                new GridViewTransferDataColumnInit

                {
                    Caption = @"Search Name",
                    FieldName = "SearchName",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,

                    VisibleIndex = 2,

                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 10,
                    SummayType = DevExpress.Data.SummaryItemType.None,
                    SummaryFormatString = "",

                    SummaryHorzAlign = HorzAlignment.Center
                },

            };



            #endregion

            var f = new FrmTransferData
            {
                DtSource = dtSource,
                ListGvColFormat = lG,
                MinimizeBox = false,

                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedSingle,

                Size = new Size(600, 600),
                StartPosition = FormStartPosition.CenterScreen,


                WindowState = FormWindowState.Normal,

                Text = @"Customer Listing",

                StrFilter = "",

                IsMultiSelected = true,

                IsShowFindPanel = true,
                IsShowFooter = false,
                IsShowAutoFilterRow = false,
                MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
            };

            f.OnTransferData += (s1, e1) =>

            {
                List<DataRow> lDr = e1.ReturnRowsSelected;
                string strCode = ProcessGeneral.GetStringPkDataTransferForm(lDr, "Customer", ",", true);
                tE.EditValue = strCode;

            };

            f.ShowDialog();


        }

        private void ShowListProductionF4OnTree(TextEdit tE)
        {

            DataTable dtCust = new DataTable();

            dtCust.Columns.Add("Customer", typeof(string));



            var qCus = txtCust.Text.Trim().Replace("'", "")
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim().ToUpper())
                .Where(p => !string.IsNullOrEmpty(p)).Distinct().ToList();

            foreach (string sCus in qCus)
            {
                dtCust.Rows.Add(sCus);
            }

            DataTable dtSource = _inf.LoadListProductionOrderW(dtCust);

            #region "Init Column"

            var lG = new List<GridViewTransferDataColumnInit>

            {

                new GridViewTransferDataColumnInit
                {

                    Caption = @"Cust. Code",
                    FieldName = "Customer",

                    HorzAlign = HorzAlignment.Near,
                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,

                    VisibleIndex = 0,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = DevExpress.Data.SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
                },
                new GridViewTransferDataColumnInit
                {
                    Caption = @"Cust. Name",
                    FieldName = "CustomerName",

                    HorzAlign = HorzAlignment.Near,
                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,

                    VisibleIndex = 1,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = DevExpress.Data.SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },

                new GridViewTransferDataColumnInit

                {

                    Caption = @"Search Name",
                    FieldName = "SearchName",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,

                    VisibleIndex = 2,
                    FormatField = FormatType.None,
                    FormatString = "",

                    IncreaseWdith = 0,
                    SummayType = DevExpress.Data.SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },


                new GridViewTransferDataColumnInit

                {

                    Caption = @"Production Order",
                    FieldName = "ProductionOrder",

                    HorzAlign = HorzAlignment.Near,

                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,

                    VisibleIndex = 3,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = DevExpress.Data.SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Center
                },


                new GridViewTransferDataColumnInit

                {
                    Caption = @"Project No.",
                    FieldName = "ProjectNo",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,

                    VisibleIndex = 4,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = DevExpress.Data.SummaryItemType.None,

                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near

                },


                new GridViewTransferDataColumnInit

                {

                    Caption = @"Project Name",
                    FieldName = "ProjectName",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,

                    VisibleIndex = 5,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = DevExpress.Data.SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
                },


                new GridViewTransferDataColumnInit

                {
                    Caption = @"Cust. Order No.",
                    FieldName = "CustOrderNo",

                    HorzAlign = HorzAlignment.Near,
                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,

                    VisibleIndex = 6,
                    FormatField = FormatType.None,
                    FormatString = "",
                    IncreaseWdith = 0,
                    SummayType = DevExpress.Data.SummaryItemType.None,
                    SummaryFormatString = "",

                    SummaryHorzAlign = HorzAlignment.Near
                },
                new GridViewTransferDataColumnInit

                {
                    Caption = @"Order No.",
                    FieldName = "OrderNo",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,

                    VisibleIndex = 7,
                    FormatField = FormatType.None,
                    FormatString = "",

                    IncreaseWdith = 0,
                    SummayType = DevExpress.Data.SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near


                }, new GridViewTransferDataColumnInit
                {
                    Caption = @"Delivery Date",

                    FieldName = "DeliveryDate",
                    HorzAlign = HorzAlignment.Near,
                    FixStyle = DevExpress.XtraGrid.Columns.FixedStyle.None,


                    VisibleIndex = 8,

                    FormatField = FormatType.None,

                    FormatString = "",

                    IncreaseWdith = 0,
                    SummayType = DevExpress.Data.SummaryItemType.None,
                    SummaryFormatString = "",
                    SummaryHorzAlign = HorzAlignment.Near
                },

            };



            #endregion

            var f = new FrmTransferData

            {
                DtSource = dtSource,
                ListGvColFormat = lG,

                MinimizeBox = false,
                MaximizeBox = false,

                FormBorderStyle = FormBorderStyle.FixedSingle,
                Size = new Size(1024, 768),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Normal,
                Text = @"Production Order Listing",

                StrFilter = "",
                IsMultiSelected = true,
                IsShowFindPanel = true,

                IsShowFooter = false,
                IsShowAutoFilterRow = false,
                MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

            };


            f.OnTransferData += (s1, e1) =>

            {


                List<DataRow> lDr = e1.ReturnRowsSelected;
                string strCode = ProcessGeneral.GetStringPkDataTransferForm(lDr, "ProductionOrder", ",", true);
                tE.EditValue = strCode;


            };
            f.ShowDialog();
        }

        private void TxtProductionOrder_KeyDown(object sender, KeyEventArgs e)
        {
            TextEdit tE = sender as TextEdit;

            if (tE == null) return;
            if (e.KeyCode == Keys.F4)

            {
                ShowListProductionF4OnTree(tE);
                return;
            }
            if (e.KeyCode == Keys.Enter)

            {
                BtnSearch_Click(null, null);

                return;
            }

        }

        private void TxtCust_KeyDown(object sender, KeyEventArgs e)

        {
            TextEdit tE = sender as TextEdit;
            if (tE == null) return;
            if (e.KeyCode == Keys.F4)
            {
                ShowListCustomerF4OnTree(tE);

                return;

            }
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch_Click(null, null);
                return;
            }
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {

            _dlg = new WaitDialogForm();

            DataTable dtCust = new DataTable();
            dtCust.Columns.Add("Customer", typeof(string));
            DataTable dtProduction = new DataTable();
            dtProduction.Columns.Add("ProductionOrderNo_CNY032", typeof(string));

            var qCus = txtCust.Text.Trim().Replace("'", "")
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim().ToUpper())
                .Where(p => !string.IsNullOrEmpty(p)).Distinct().ToList();
            var qPro = txtProductionOrder.Text.Trim().Replace("'", "")
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim().ToUpper())
                .Where(p => !string.IsNullOrEmpty(p)).Distinct().ToList();
            foreach (string sCus in qCus)
            {
                dtCust.Rows.Add(sCus);
            }
            foreach (string sPro in qPro)
            {
                dtProduction.Rows.Add(sPro);

            }


            DataSet ds = _inf.LoadDataGridViewSoWhenGenerateNormal(dtCust, dtProduction);



            DataTable dtAtemp = ds.Tables[0];
            DataTable dtBom = ds.Tables[1];
            DataTable dtBi = ds.Tables[2];


            if (dtBom.Rows.Count <= 0)
            {
                _dlg.Close();
                XtraMessageBox.Show("No Data Display", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (!string.IsNullOrEmpty(_productionOrderNo))
                {
                    this.Close();
                }


                return;

            }

            DataTable dtFinal = StandardTreeTableSo(dtBom, dtAtemp, dtBi);


            LoadDataTreeView(tlSO, dtFinal);



            _dlg.Close();



            tlSO.SelectNextControl(ActiveControl, true, true, true, true);
            tlSO.Focus();
            if (tlSO.AllNodesCount > 0)

            {
                tlSO.SetFocusedCellTreeList(tlSO.Nodes[0], tlSO.VisibleColumns[0]);
                var qEx = tlSO.Nodes.Where(p => p.Expanded).ToList();
                foreach (TreeListNode nodeEx in qEx)
                {
                    nodeEx.Expanded = false;
                }


            }

            if (!string.IsNullOrEmpty(_productionOrderNo))
            {
                foreach (TreeListNode node in tlSO.Nodes)
                {
                    node.CheckAll();
                }
                btnNextFinish_Click(null, null);
            }

        }



        private DataTable StandardTreeTableSo(DataTable dtTreeTemp, DataTable dtAtemp, DataTable dtBi)
        {

            Int64 beginParentPk = dtTreeTemp.Rows.Count + 10;

            Dictionary<Int64, PrSOGenerateInfoStep1> qP1 = dtTreeTemp.AsEnumerable().GroupBy(p => new
            {
                Customer = p.Field<string>("Customer"),
                CustomerOrderNo = p.Field<string>("CustomerOrderNo"),
                ProjectNo = p.Field<string>("ProjectNo"),
                ProjectName = p.Field<string>("ProjectName"),
                DeliveryDate = p.Field<string>("DeliveryDate"),
                ProductionOrder = p.Field<string>("ProductionOrder"),
                OrderNo = p.Field<string>("OrderNo"),
                CNY00019PK = p.Field<Int64>("CNY00019PK"),
            }).Select((t, ind) => new
            {

                KeyDic = t.Key.CNY00019PK,
                TempData = new PrSOGenerateInfoStep1
                {
                    Customer = t.Key.Customer,
                    CustomerOrderNo = t.Key.CustomerOrderNo,
                    ProjectNo = t.Key.ProjectNo,
                    ProjectName = t.Key.ProjectName,
                    DeliveryDate = t.Key.DeliveryDate,
                    ProductionOrder = t.Key.ProductionOrder,
                    OrderNo = t.Key.OrderNo,
                    OrderLine = "",
                    MainMaterialGroup = "",
                    ProductCode = "",
                    Reference = "",
                    ProductName = "",
                    OrderQuantity = t.Sum(s => s.Field<Int32>("OrderQuantity")),
                    PlanQuantity = 0,
                    //PlanQuantity = t.Sum(s => s.Field<Int32>("OrderQuantity")),
                    //BalanceQuantity = 0,
                    BalanceQuantity = t.Sum(s => s.Field<Int32>("OrderQuantity")),

                    FinishingColor = "",
                    TDG00001PK = (Int64)0,
                    CNY00020PK = (Int64)0,
                    CNY00019PK = t.Key.CNY00019PK,
                    ChildPK = beginParentPk + ind,
                    ParentPK = (Int64)0,
                    ProDimension = ""
                },

            }).ToDictionary(item => item.KeyDic, item => item.TempData);


            _dicAttValueSo.Clear();
            _dicAttValueSo = dtAtemp.AsEnumerable().GroupBy(f => new
            {
                CNY00020PK = f.Field<Int64>("CNY00020PK"),
            }).Where(myGroup => myGroup.Any())
                .Select(myGroup => new
                {
                    KeyDic = myGroup.Key.CNY00020PK,

                    TempData = myGroup.GroupBy(f => new
                    {
                        AttibuteCode = f.Field<String>("AttibuteCode"),
                        AttibuteName = f.Field<String>("AttibuteName"),

                        AttibuteValueFull = f.Field<String>("AttibuteValueFull"),
                        AttibutePK = f.Field<Int64>("AttibutePK"),
                        AttibuteValueTemp = f.Field<String>("AttibuteValueTemp"),

                        AttibuteUnit = f.Field<String>("AttibuteUnit"),
                        IsNumber = f.Field<Boolean>("IsNumber"),
                        PK = f.Field<Int64>("PK"),

                        RowState = ProcessGeneral.GetDataStatus(f.Field<String>("RowState"))
                    }).Select(m => new BoMInputAttInfo
                    {
                        AttibuteCode = m.Key.AttibuteCode,
                        AttibuteName = m.Key.AttibuteName,
                        AttibuteValueFull = m.Key.AttibuteValueFull,
                        AttibutePK = m.Key.AttibutePK,

                        AttibuteValueTemp = m.Key.AttibuteValueTemp,
                        AttibuteUnit = m.Key.AttibuteUnit,

                        IsNumber = m.Key.IsNumber,
                        PK = m.Key.PK,

                        RowState = m.Key.RowState,
                    }).Where(n => !string.IsNullOrEmpty(n.AttibuteCode)).ToList()
                }).ToDictionary(item => item.KeyDic, item => item.TempData);


            var qAd = dtAtemp.AsEnumerable().Select(p => new
            {
                AttibutePK = ProcessGeneral.GetSafeInt64(p["AttibutePK"]),
                AttibuteName = string.Format("{0}-{1}", ProcessGeneral.GetSafeString(p["AttibuteCode"]), ProcessGeneral.GetSafeString(p["AttibuteName"])),

                DataType = ProcessGeneral.GetDataAttType(ProcessGeneral.GetSafeString(p["DataAttType"]))
            }).Distinct().Select(p => new BoMCompactAttInfo
            {
                AttibutePK = p.AttibutePK,
                AttibuteName = p.AttibuteName,
                DataType = p.DataType
            }).ToList();

            Dictionary<Int64, PRBOMIDNOINFO> qF2 = dtBi.AsEnumerable().GroupBy(p => p.Field<Int64>("CNY00020PK")).Select(t => new
            {
                KeyDic = t.Key,
                TempData = new PRBOMIDNOINFO
                {
                    StrBOMID = string.Join(",", t.Select(s => ProcessGeneral.GetSafeString(s["BOMID"])).Where(m => !string.IsNullOrEmpty(m)).Distinct().ToArray()),
                    StrBoMNo = string.Join(",", t.Select(s => ProcessGeneral.GetSafeString(s["BoMNo"])).Where(m => !string.IsNullOrEmpty(m)).Distinct().ToArray())
                },

            }).ToDictionary(item => item.KeyDic, item => item.TempData);


            _dicAttributeSo.Clear();

            foreach (var itemAd in qAd)
            {


                string attName = itemAd.AttibuteName;
                Int64 attPkT = itemAd.AttibutePK;
                _dicAttributeSo.Add(attPkT, new AttributeHeaderInfo

                {
                    AttibuteName = attName,
                    DataType = itemAd.DataType
                });

                dtTreeTemp.Columns.Add(attName, typeof(string));
            }


            foreach (DataRow drSource in dtTreeTemp.Rows)

            {

                Int64 cny00020Pk = ProcessGeneral.GetSafeInt64(drSource["CNY00020PK"]);
                Int64 cny00019Pk = ProcessGeneral.GetSafeInt64(drSource["CNY00019PK"]);


                List<BoMInputAttInfo> lBomInfoLoop;

                if (_dicAttValueSo.TryGetValue(cny00020Pk, out lBomInfoLoop))
                {

                    foreach (BoMInputAttInfo t1 in lBomInfoLoop)

                    {
                        String attibuteCode = t1.AttibuteCode;

                        String attibuteName = t1.AttibuteName;
                        String attibuteValueFull = t1.AttibuteValueFull;
                        drSource[string.Format("{0}-{1}", attibuteCode, attibuteName)] = attibuteValueFull;


                    }

                }


                //drSource["Customer"] = "";
                //drSource["CustomerOrderNo"] = "";
                //drSource["ProjectNo"] = "";
                //drSource["ProjectName"] = "";
                //drSource["DeliveryDate"] = "";
                //drSource["ProductionOrder"] = "";
                //drSource["OrderNo"] = "";

                PrSOGenerateInfoStep1 tempInfo1;

                if (qP1.TryGetValue(cny00019Pk, out tempInfo1))
                {
                    drSource["ParentPK"] = tempInfo1.ChildPK;
                }


                PRBOMIDNOINFO itemBoM;
                if (qF2.TryGetValue(cny00020Pk, out itemBoM))
                {
                    drSource["BoMNo"] = itemBoM.StrBoMNo;
                    drSource["BOMID"] = itemBoM.StrBOMID;

                }



            }
            _dtFinalSo = dtTreeTemp;



            foreach (var item in qP1)
            {
                PrSOGenerateInfoStep1 tempInfo2 = item.Value;
                DataRow drAddTree = dtTreeTemp.NewRow();
                drAddTree["Customer"] = tempInfo2.Customer;

                drAddTree["CustomerOrderNo"] = tempInfo2.CustomerOrderNo;

                drAddTree["ProjectNo"] = tempInfo2.ProjectNo;
                drAddTree["ProjectName"] = tempInfo2.ProjectName;
                drAddTree["DeliveryDate"] = tempInfo2.DeliveryDate;
                drAddTree["ProductionOrder"] = tempInfo2.ProductionOrder;
                drAddTree["OrderNo"] = tempInfo2.OrderNo;
                drAddTree["OrderLine"] = tempInfo2.OrderLine;
                drAddTree["MainMaterialGroup"] = tempInfo2.MainMaterialGroup;
                drAddTree["ProductCode"] = tempInfo2.ProductCode;
                drAddTree["Reference"] = tempInfo2.Reference;
                drAddTree["ProductName"] = tempInfo2.ProductName;

                drAddTree["OrderQuantity"] = tempInfo2.OrderQuantity;
                drAddTree["PlanQuantity"] = tempInfo2.PlanQuantity;

                drAddTree["BalanceQuantity"] = tempInfo2.BalanceQuantity;
                drAddTree["FinishingColor"] = tempInfo2.FinishingColor;
                drAddTree["BOMID"] = "";
                drAddTree["BoMNo"] = "";
                drAddTree["TDG00001PK"] = tempInfo2.TDG00001PK;
                drAddTree["CNY00020PK"] = tempInfo2.CNY00020PK;
                drAddTree["CNY00019PK"] = tempInfo2.CNY00019PK;
                drAddTree["ChildPK"] = tempInfo2.ChildPK;
                drAddTree["ParentPK"] = tempInfo2.ParentPK;
                drAddTree["ProDimension"] = tempInfo2.ProDimension;


                dtTreeTemp.Rows.Add(drAddTree);
            }
            dtTreeTemp.AcceptChanges();
            return dtTreeTemp;

        }

        private void LoadDataTreeView(TreeList tl, DataTable dt)
        {

            tl.Columns.Clear();
            tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";



            tl.BeginUpdate();

            ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "TDG00001PK", "CNY00020PK", "CNY00019PK", "BOMID", "ProDimension", "CustomerOrderNo", "ProjectNo", "OrderNo", "OrderLine",
                "MainMaterialGroup");



            int indexCode = tl.Columns["ProductCode"].VisibleIndex;

            tl.Columns["ProductCode"].VisibleIndex = tl.Columns["Reference"].VisibleIndex;
            tl.Columns["Reference"].VisibleIndex = indexCode;






            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Customer"], "Customer", false, HorzAlignment.Center,
                TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["CustomerOrderNo"], "Cust Order No.", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProjectNo"], "Project No.", false, HorzAlignment.Center,
                TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProjectName"], "Project Name", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["DeliveryDate"], "Delivery Date", false, HorzAlignment.Center,

                TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProductionOrder"], "Production Order", false,
                HorzAlignment.Center, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["OrderNo"], "Order No.", false, HorzAlignment.Center,
                TreeFixedStyle.None, "");





            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["OrderLine"], "Order Line", true, HorzAlignment.Center,
                TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["MainMaterialGroup"], "Main Material Group", true, HorzAlignment.Near,
                TreeFixedStyle.None, "");



            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProductCode"], "Product Code", false, HorzAlignment.Center,
                TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Reference"], "Reference", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProductName"], "Product Name", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["OrderQuantity"], "Order Quantity", false, HorzAlignment.Center,

                TreeFixedStyle.None, "");
            tl.Columns["OrderQuantity"].Format.FormatType = FormatType.Numeric;
            tl.Columns["OrderQuantity"].Format.FormatString = "N0";


            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["PlanQuantity"], "Plan Quantity", false, HorzAlignment.Center,

                TreeFixedStyle.None, "");
            tl.Columns["PlanQuantity"].Format.FormatType = FormatType.Numeric;
            tl.Columns["PlanQuantity"].Format.FormatString = "N0";

            tl.Columns["PlanQuantity"].ImageIndex = 0;

            tl.Columns["PlanQuantity"].ImageAlignment = StringAlignment.Far;


            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["BalanceQuantity"], "Balance Quantity", false, HorzAlignment.Center,
                TreeFixedStyle.None, "");
            tl.Columns["BalanceQuantity"].Format.FormatType = FormatType.Numeric;
            tl.Columns["BalanceQuantity"].Format.FormatString = "N0";


            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["FinishingColor"], "Finishing Color", false, HorzAlignment.Near,

                TreeFixedStyle.None, "");







            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["BOMID"], "BOM ID", false, HorzAlignment.Center,
                TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["BoMNo"], "BOM No.", false, HorzAlignment.Center,
                TreeFixedStyle.None, "");


            foreach (var item in _dicAttributeSo)
            {
                string s = item.Value.AttibuteName;

                TreeListColumn tColS = tl.Columns[s];
                if (tColS == null) continue;
                ProcessGeneral.SetTreeListColumnHeader(tColS, s, false, HorzAlignment.Near, TreeFixedStyle.None, item.Key);
                tColS.ImageIndex = 0;


                tColS.ImageAlignment = StringAlignment.Near;

            }

            //    tl.ExpandAll();



            tl.BestFitColumns();

            if (tl.Columns["FinishingColor"].Width > 200)

            {
                tl.Columns["FinishingColor"].Width = 200;
            }
            tl.ForceInitialize();


            tl.EndUpdate();
        }
        #endregion





        #region "Methold"



        private void VisibleTabPageByStep()
        {
            foreach (XtraTabPage page in xtraTabMain.TabPages)

            {
                Int32 tag = ProcessGeneral.GetSafeInt(page.Tag);
                page.PageVisible = tag == _step;
            }
        }

        private void SetupButtonNextFinished()
        {
            btnBack.Enabled = _step != 1;
            if (_step < TotalStep)
            {

                btnNextFinish.Text = @"Next";
                btnNextFinish.Image = Resources.forward_24x24_W;
                btnNextFinish.ToolTip = @"Press (Ctrl+Shift+N)";
            }
            else
            {
                btnNextFinish.Text = @"Finish";
                btnNextFinish.Image = Resources.apply_24x24_W;

                btnNextFinish.ToolTip = @"Press (Ctrl+Shift+F)";

            }

        }




        private DataTable GetTableSoLinePk()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductionOrder", typeof(string));
            dt.Columns.Add("CNY00012PK", typeof(Int64));
            dt.Columns.Add("CNY00020PK", typeof(Int64));
            dt.Columns.Add("PlanQty", typeof(Int32));
            dt.Columns.Add("WOQty", typeof(Int32));
            dt.Columns.Add("Reference", typeof(string));
            dt.Columns.Add("ProductCode", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));

            var q1 = tlSO.GetAllCheckedNodes().Where(p => !p.HasChildren).Select(p => new
            {

                StrCNY00012PK = ProcessGeneral.GetSafeString(p.GetValue("BOMID")),
                ProductionOrder = ProcessGeneral.GetSafeString(p.GetValue("ProductionOrder")),

                CNY00020PK = ProcessGeneral.GetSafeInt64(p.GetValue("CNY00020PK")),

                PlanQty = ProcessGeneral.GetSafeInt(p.GetValue("PlanQuantity")),
                WOQty = ProcessGeneral.GetSafeInt(p.GetValue("OrderQuantity")),

                Reference = ProcessGeneral.GetSafeString(p.GetValue("Reference")),

                ProductCode = ProcessGeneral.GetSafeString(p.GetValue("ProductCode")),
                ProductName = ProcessGeneral.GetSafeString(p.GetValue("ProductName")),

            }).Where(p => //p.PlanQty <= p.SOQty && 
                          p.WOQty > 0 && p.StrCNY00012PK != "" && p.CNY00020PK > 0).Distinct().ToList();



            foreach (var item in q1)
            {
                string strCny00012Pk = item.StrCNY00012PK;

                Int64[] arrBomId = strCny00012Pk.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => ProcessGeneral.GetSafeInt64(p.Trim())).Where(p => p > 0).Distinct().ToArray();

                foreach (Int64 cny00012Pk in arrBomId)


                {
                    dt.Rows.Add(item.ProductionOrder, cny00012Pk, item.CNY00020PK, item.PlanQty, item.WOQty, item.Reference, item.ProductCode, item.ProductName);
                }
            }
            return dt;

        }



        private List<TreeListNode> GetTableItemSelected()
        {


            return tlItemCode.GetAllCheckedNodes().Where(p => !p.HasChildren).ToList();


        }



        #endregion

        #region "Button Click Event"


        private void btnBack_Click(object sender, EventArgs e)
        {

            _step = _step - 1;
            VisibleTabPageByStep();
            SetupButtonNextFinished();


        }

        private DataTable dtAa04Pk = new DataTable();
        private List<TreeListNode> lNodeSelected = new List<TreeListNode>();
        //private DataTable dtCategoyFull = new DataTable();
        //private DataTable dtCategoyCompact = new DataTable();
        //private DataTable dtBoMFinal = new DataTable();







        private void btnNextFinish_Click(object sender, EventArgs e)
        {

            txtFocus.SelectNextControl(ActiveControl, true, true, true, true);

            _step = _step + 1;



            if (_step == 2)
            {
                dtAa04Pk = GetTableSoLinePk();

                if (dtAa04Pk.Rows.Count <= 0)
                {
                    XtraMessageBox.Show("No Row Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _step = _step - 1;
                    return;
                }







            }

            else if (_step == 3)
            {
                //DataTable dtGlobal = GetTableGlobalGroupSelected();
                lNodeSelected = GetTableItemSelected();
                if (tlItemCode.AllNodesCount <= 0 || lNodeSelected.Count <= 0)
                {
                    XtraMessageBox.Show("No Row Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _step = _step - 1;
                    return;
                }
            }











            switch (_step)
            {
                case 2:
                    {

                       
                        VisibleTabPageByStep();

                        SetupButtonNextFinished();
                        LoadDataTreeViewItemCode();
                        tlItemCode.SelectNextControl(ActiveControl, true, true, true, true);
                        tlItemCode.Focus();

                        if (tlItemCode.AllNodesCount > 0)
                        {
                            tlItemCode.SetFocusedCellTreeList(tlItemCode.Nodes[0], tlItemCode.VisibleColumns[0]);
                        }
                    }

                    break;
                case 3:
                    {
                        VisibleTabPageByStep();
                        SetupButtonNextFinished();
                        //GenerateTemp();

                        LoadDataTreeViewItemFinal(tlFinal);
                        tlFinal.SelectNextControl(ActiveControl, true, true, true, true);
                        tlFinal.Focus();
                        if (tlFinal.AllNodesCount > 0)
                        {
                            tlFinal.SetFocusedCellTreeList(tlFinal.Nodes[0], tlFinal.VisibleColumns[0]);
                        }

                    }

                    break;
                default:
                    {
                        GenerateFinal();

                    }
                    break;



            }

        }



        private Dictionary<Int64, AttributeHeaderInfo> _DicAttributeRootTemp = new Dictionary<Int64, AttributeHeaderInfo>();


        private Dictionary<Int64, List<BoMInputAttInfo>> _DicAttValueRootTemp = new Dictionary<Int64, List<BoMInputAttInfo>>();

        Dictionary<Int64, DataTable> _dicTableColorTemp = new Dictionary<Int64, DataTable>(); //PKCHild
        private void GenerateTemp()
        {

            _dlg = new WaitDialogForm();
            _DicAttributeRootTemp.Clear();
            _DicAttValueRootTemp.Clear();
            _dicTableColorTemp.Clear();
            LoadDataTreeViewItemFinal(tlFinal);

            DataTable dtTemp = DtSourceTree.Clone();




            var qAttJoin = _dicAttributeRm.Select(p => new

            {

                AttibutePK = p.Key,
                p.Value.AttibuteName,
                p.Value.DataType
            }).ToList();

            var qAdR = qAttJoin.Where(p => DicAttributeRoot.All(t => t.Key != p.AttibutePK)).ToList();





            if (qAdR.Any())
            {


                foreach (var itemAdR in qAdR)
                {
                    string attName = itemAdR.AttibuteName;
                    dtTemp.Columns.Add(attName, typeof(string));
                    _DicAttributeRootTemp.Add(itemAdR.AttibutePK, new AttributeHeaderInfo
                    {

                        AttibuteName = attName,
                        DataType = itemAdR.DataType

                    });
                }

                dtTemp.AcceptChanges();
            }







            var qNodeParent = tlFinal.GetAllNodeTreeList().Where(p =>
                !p.HasChildren && ProcessGeneral.GetSafeString(p.GetValue("StringRowIndex")) != "").ToList();
            int pCount = qNodeParent.Count;
            Int64 pPk = Com_StockDocumentRequest.GetPkSDRTable(pCount);



            var qParent = qNodeParent.Select((p, idp) => new
            {



                TDG00001PK = ProcessGeneral.GetSafeInt64(p.GetValue("TDG00001PK")),
                RMCode_001 = ProcessGeneral.GetSafeString(p.GetValue("RMCode_001")),

                RMDescription_002 = ProcessGeneral.GetSafeString(p.GetValue("RMDescription_002")),
                Color = ProcessGeneral.GetSafeString(p.GetValue("ColorReference")),

                MainMaterialGroup = ProcessGeneral.GetSafeString(p.GetValue("MainMaterialGroup")),


                CNY00002PK = ProcessGeneral.GetSafeInt64(p.GetValue("CNY00002PK")),

                Supplier = ProcessGeneral.GetSafeString(p.GetValue("Supplier")),



                TDG00004PK = ProcessGeneral.GetSafeInt64(p.GetValue("TDG00004PK")),

                SupplierRef = ProcessGeneral.GetSafeString(p.GetValue("SupplierRef")),

                StockQty_CNY010 = Convert.ToDecimal(0),
                PRQty_CNY002 = Convert.ToDecimal(0),
                POQty_CNY003 = Convert.ToDecimal(0),



                UnitCode_CNY011 = ProcessGeneral.GetSafeString(p.GetValue("UCUnitCode")),
                Unit = ProcessGeneral.GetSafeString(p.GetValue("UCUnit")),


                ETD = _etdDate,

                ETA = _etaDate,
                Note = "",


                ChildPK = Convert.ToInt64(idp + pPk),

                ParentPK = Convert.ToInt64(0),

                RowState = DataStatus.Insert.ToString(),
                StringRowIndex = ProcessGeneral.GetSafeString(p.GetValue("StringRowIndex")),
                CNY00050PK = ProcessGeneral.GetSafeInt64(p.GetValue("CNY00050PK")),
                CNY00004PK = Convert.ToInt64(0),
                Purchaser = "",
                AllowUpdate = true,
                FormulaString = ProcessGeneral.GetSafeString(p.GetValue("FormulaString")),
                FormulaStringDisplay = ProcessGeneral.GetSafeString(p.GetValue("FormulaStringDisplay")),
                PurchaseUnit = ProcessGeneral.GetSafeString(p.GetValue("PurchaseUnit")),
            }).ToList();




            DataTable qItemCode = qParent.Select(p => new
            {
                p.TDG00001PK

            }).Distinct().CopyToDataTableNew();


            DataTable dtPurchaser = _inf.LoadListPurchaserByItemCode(qItemCode);

            Dictionary<Int64, PurchaserGenerateInfo> dicPur = new Dictionary<long, PurchaserGenerateInfo>();
            foreach (DataRow drPur in dtPurchaser.Rows)

            {
                dicPur.Add(ProcessGeneral.GetSafeInt64(drPur["TDG00001PK"]), new PurchaserGenerateInfo
                {

                    CNY00004PK = ProcessGeneral.GetSafeInt64(drPur["CNY00004PK"]),
                    Purchaser = ProcessGeneral.GetSafeString(drPur["Purchaser"])
                });
            }


            Dictionary<Int64, bool> dicChangeWaste = _inf.LoadListAllowChangeWasteItemCode(qItemCode);


            var lChildTemp = qParent.SelectMany(
                p => p.StringRowIndex.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(s => !string.IsNullOrEmpty(s.Trim())).Select(s => ProcessGeneral.GetSafeInt64(s.Trim()))

                    .Distinct().ToArray(),
                (m, n) => new
                {
                    ParentPK = m.ChildPK,

                    RowIndex = n,
                }).ToList();





            Int64 cPk = Com_StockDocumentRequest.GetPkSDRTable(lChildTemp.Count);



            var lChildFinal = lChildTemp.Select((p, idc) => new
            {

                ChildPK = Convert.ToInt64(idc + cPk),
                p.ParentPK,


                p.RowIndex
            }).ToList();



            var qC = lChildFinal.Join(_dtFinalBoM.AsEnumerable(), p => p.RowIndex,
                t => t.Field<Int64>("RowIndex"), (p, t) => new

                {

                    RmDimension = t.Field<string>("RmDimension"),
                    Position = t.Field<string>("Position"),

                    PRQty_CNY002 = Math.Round((t.Field<decimal>("UC") * t.Field<Int32>("PlanQty") *
                                   (1 + t.Field<decimal>("Waste") / 100) * t.Field<decimal>("PercentUsing") / 100), ConstSystem.FormatPrQtyDecimal),
                    POQty_CNY003 = Math.Round((t.Field<decimal>("UC") * t.Field<Int32>("PlanQty") *
                                   (1 + t.Field<decimal>("Waste") / 100) * t.Field<decimal>("PercentUsing") / 100), ConstSystem.FormatPoQtyDecimal),

                    PlanQuantity = t.Field<Int32>("PlanQty"),
                    Factor = t.Field<decimal>("Factor"),
                    UC = t.Field<decimal>("UC"),

                    Tolerance = t.Field<decimal>("Waste"),
                    PercentUsing = t.Field<decimal>("PercentUsing"),

                    RootSOQty = t.Field<Int32>("SOQty"),
                    CNY00020PK = t.Field<Int64>("CNY00020PK"),
                    CNY00016PK = t.Field<Int64>("CNY00016PK"),
                    p.ChildPK,

                    p.ParentPK,
                    RowState = DataStatus.Insert.ToString(),

                }).Join(_dtFinalSo.AsEnumerable(), m => m.CNY00020PK, n => n.Field<Int64>("CNY00020PK"), (m, n) =>


                new

                {
                    ProDimension = n.Field<string>("ProDimension"),
                    m.RmDimension,
                    m.Position,

                    m.PRQty_CNY002,
                    m.POQty_CNY003,



                    TDG00001PK = n.Field<Int64>("TDG00001PK"),
                    RMCode_001 = n.Field<String>("ProductCode"),
                    RMDescription_002 = n.Field<String>("ProductName"),
                    Reference = n.Field<String>("Reference"),


                    m.PlanQuantity,

                    m.Factor,
                    m.UC,

                    m.Tolerance,
                    m.PercentUsing,


                    ProjectName = n.Field<String>("ProjectName"),

                    ProductionOrder = n.Field<String>("ProductionOrder"),
                    m.RootSOQty,
                    m.CNY00020PK,
                    m.CNY00016PK,
                    m.ChildPK,
                    m.ParentPK,
                    m.RowState,

                    AllowUpdate = true,

                    CNY00019PK = n.Field<Int64>("CNY00019PK"),

                }).ToList();

            var qCGroup = qC.GroupBy(p => p.ParentPK).Select(t => new

            {
                ParentPK = t.Key,

                PRQty_CNY002 = t.Sum(s => s.PRQty_CNY002),
                POQty_CNY003 = t.Sum(s => s.POQty_CNY003),
                CNY00016PK = t.Min(s => s.CNY00016PK)
            }).ToList();

            var qP = qParent.Join(qCGroup, p => p.ChildPK, t => t.ParentPK, (p, t) => new
            {
                p.TDG00001PK,
                p.RMCode_001,
                p.RMDescription_002,
                p.Color,
                p.MainMaterialGroup,
                p.CNY00002PK,
                p.Supplier,
                p.StockQty_CNY010,
                t.PRQty_CNY002,
                t.POQty_CNY003,

                p.UnitCode_CNY011,
                p.Unit,
                //    t.PlanQuantity,
                p.ETD,
                p.ETA,
                p.Note,
                p.ChildPK,
                p.ParentPK,

                p.RowState,
                p.CNY00050PK,
                CNY00016PKAtt = t.CNY00016PK,

                p.CNY00004PK,

                p.Purchaser,
                p.AllowUpdate,
                p.TDG00004PK,
                p.SupplierRef,
                p.FormulaString,
                p.FormulaStringDisplay,
                p.PurchaseUnit
            }).ToList();

            var qCny00016Pk = qP.Select(p => p.CNY00016PKAtt).Distinct().ToList();

            Dictionary<Int64, List<BoMInputAttInfo>> dicVCode = _dicAttValueItemCode
                .Where(p => qCny00016Pk.Any(t => t == p.Key)).ToDictionary(item => item.Key, item => item.Value);


            //  var qCny00020Pk = qC.Select(p => p.CNY00020PK).Distinct().ToList();


            //      Dictionary<Int64, List<BoMInputAttInfo>> dicVSo = _dicAttValueSo.Where(p => qCny00020Pk.Any(t => t == p.Key)).ToDictionary(item => item.Key, item => item.Value);



            DataTable dtAssComp = _inf.LoadListAssCompByItemCode(qC.Select(p => new
            {
                p.CNY00016PK

            }).Distinct().CopyToDataTableNew());
            Dictionary<Int64, string> dicAssComp = new Dictionary<long, string>();


            foreach (DataRow drAssComp in dtAssComp.Rows)
            {

                dicAssComp.Add(ProcessGeneral.GetSafeInt64(drAssComp["CNY00016PK"]), ProcessGeneral.GetSafeString(drAssComp["AssemblyComponent"]));
            }

            Dictionary<Int64, string> dicDimension = _dtFinalBoM.AsEnumerable().Where(p => qCny00016Pk.Any(t => t == p.Field<Int64>("CNY00016PK"))).Select(p => new
            {
                CNY00016PK = p.Field<Int64>("CNY00016PK"),

                RmDimension = p.Field<string>("RmDimension"),
            }).Distinct().ToDictionary(s => s.CNY00016PK, s => s.RmDimension);

            //qC


            foreach (var itemP in qP)
            {
                DataRow drp = dtTemp.NewRow();

                Int64 childPkP = itemP.ChildPK;
                Int64 cny00016PkAtt = itemP.CNY00016PKAtt;


                Int64 pkCodeP = itemP.TDG00001PK;

                drp["TDG00001PK"] = itemP.TDG00001PK;

                drp["RMCode_001"] = itemP.RMCode_001;

                drp["RMDescription_002"] = itemP.RMDescription_002;
                drp["Color"] = itemP.Color;
                drp["MainMaterialGroup"] = itemP.MainMaterialGroup;


                drp["CNY00002PK"] = itemP.CNY00002PK;
                drp["Supplier"] = itemP.Supplier;


                drp["TDG00004PK"] = itemP.TDG00004PK;
                drp["SupplierRef"] = itemP.SupplierRef;



                drp["StockQty_CNY010"] = itemP.StockQty_CNY010;
                drp["PRQty_CNY002"] = itemP.PRQty_CNY002;

                drp["POQty_CNY003"] = itemP.POQty_CNY003;
                drp["UnitCode_CNY011"] = itemP.UnitCode_CNY011;

                drp["Unit"] = itemP.Unit;

                drp["PRQty_CNY002B"] = itemP.PRQty_CNY002;
                drp["POQty_CNY003B"] = itemP.POQty_CNY003;
                drp["UnitCode_CNY011B"] = itemP.UnitCode_CNY011;
                drp["UnitB"] = itemP.Unit;


                drp["ETD"] = itemP.ETD;

                drp["ETA"] = itemP.ETA;
                drp["Note"] = itemP.Note;


                drp["ChildPK"] = childPkP;

                drp["ParentPK"] = itemP.ParentPK;
                drp["RowState"] = itemP.RowState;
                drp["CNY00050PK"] = itemP.CNY00050PK;
                drp["PackingFactor"] = 1;
                PurchaserGenerateInfo purInfo;
                if (dicPur.TryGetValue(pkCodeP, out purInfo))
                {
                    drp["CNY00004PK"] = purInfo.CNY00004PK;


                    drp["Purchaser"] = purInfo.Purchaser;


                }

                else


                {
                    drp["CNY00004PK"] = itemP.CNY00004PK;
                    drp["Purchaser"] = itemP.Purchaser;
                }





                bool allowChangeWaste;
                if (!dicChangeWaste.TryGetValue(pkCodeP, out allowChangeWaste))
                {



                    allowChangeWaste = false;
                }
                drp["AllowChangeWaste"] = allowChangeWaste;
                drp["IsGrouping"] = false;
                drp["IsPacking"] = false;

                drp["DifUC"] = false;



                string dimension = "";
                dicDimension.TryGetValue(cny00016PkAtt, out dimension);
                drp["Dimension"] = dimension;
                drp["PurchaseUnit"] = itemP.PurchaseUnit;

                drp["FormulaString"] = itemP.FormulaString;
                drp["FormulaStringDisplay"] = itemP.FormulaStringDisplay;

                drp["IsFormula"] = false;


                drp["AllowUpdate"] = itemP.AllowUpdate;
                drp["IsHasChild"] = false;
                drp["CNY00014PK"] = 0;
                List<BoMInputAttInfo> lBomInfoP = new List<BoMInputAttInfo>();
                List<BoMInputAttInfo> lDataP;
                if (dicVCode.TryGetValue(cny00016PkAtt, out lDataP))

                {
                    foreach (var tP in lDataP)
                    {
                        String attibuteCodeP = tP.AttibuteCode;
                        String attibuteNameP = tP.AttibuteName;
                        String attibuteValueFullP = tP.AttibuteValueFull;
                        drp[string.Format("{0}-{1}", attibuteCodeP, attibuteNameP)] = attibuteValueFullP;
                        lBomInfoP.Add(new BoMInputAttInfo
                        {

                            AttibuteCode = attibuteCodeP,
                            AttibuteName = attibuteNameP,

                            AttibuteValueFull = attibuteValueFullP,
                            AttibutePK = tP.AttibutePK,

                            IsNumber = tP.IsNumber,
                            AttibuteUnit = tP.AttibuteUnit,
                            AttibuteValueTemp = tP.AttibuteValueTemp,

                            RowState = DataStatus.Insert,
                            PK = -1,
                        });
                    }
                }

                _DicAttValueRootTemp.Add(childPkP, lBomInfoP);

                dtTemp.Rows.Add(drp);

                var qCf = qC.Where(p => p.ParentPK == childPkP).ToList();


                DataTable dtCf = Com_StockDocumentRequest.TableGridChildN1Template();





                foreach (var itemC in qCf)
                {

                    DataRow drc = dtCf.NewRow();
                    Int64 cny00020Pk = itemC.CNY00020PK;


                    drc["RmDimension"] = itemC.RmDimension;
                    drc["ProDimension"] = itemC.ProDimension;

                    drc["Position"] = itemC.Position;
                    drc["TDG00001PK"] = itemC.TDG00001PK;
                    drc["RMCode_001"] = itemC.RMCode_001;
                    drc["RMDescription_002"] = itemC.RMDescription_002;

                    drc["Reference"] = itemC.Reference;

                    drc["PRQty_CNY002"] = itemC.PRQty_CNY002;
                    drc["POQty_CNY003"] = itemC.POQty_CNY003;
                    drc["PRQty_CNY002B"] = itemC.PRQty_CNY002;
                    drc["POQty_CNY003B"] = itemC.POQty_CNY003;
                    drc["PlanQuantity"] = itemC.PlanQuantity;
                    drc["Factor"] = itemC.Factor;
                    drc["TotalQuantity"] = itemC.Factor * itemC.PlanQuantity;


                    drc["UC"] = itemC.UC;
                    drc["UC_BOM"] = itemC.UC;
                    drc["Tolerance"] = itemC.Tolerance;
                    drc["ToleranceBOM"] = itemC.Tolerance;

                    drc["PercentUsing"] = itemC.PercentUsing;

                    drc["ProjectName"] = itemC.ProjectName;

                    drc["ProductionOrder"] = itemC.ProductionOrder;
                    drc["RootSOQty"] = itemC.RootSOQty;

                    drc["CNY00020PK"] = cny00020Pk;
                    Int64 cny00016PkT = itemC.CNY00016PK;
                    drc["CNY00016PK"] = cny00016PkT;
                    drc["ChildPK"] = itemC.ChildPK;
                    drc["ParentPK"] = itemC.ParentPK;
                    drc["RowState"] = itemC.RowState;
                    drc["StockQty_CNY010"] = 0;

                    string assComp = "";
                    if (dicAssComp.TryGetValue(cny00016PkT, out assComp))

                    {
                        drc["AssemblyComponent"] = assComp;
                    }
                    else
                    {
                        drc["AssemblyComponent"] = "";
                    }

                    drc["CNY00019PK"] = itemC.CNY00019PK;




                    drc["AllowUpdate"] = itemC.AllowUpdate;


                    dtCf.Rows.Add(drc);
                }



                dtCf.AcceptChanges();
                _dicTableColorTemp.Add(childPkP, dtCf);



            }







            dtTemp.AcceptChanges();
            //LoadDataTreeView(tlMain, dtTemp, true);
            _dlg.Close();







        }

        private void GenerateFinal()
        {
            _dlg = new WaitDialogForm();
            bool isEvent = false;
            DataTable dtTemp = DtSourceTree;

            if (tlFinal.AllNodesCount > 0)
            {
                isEvent = true;

                var qNodeParent = tlFinal.GetAllNodeTreeList().Where(p => !p.HasChildren).ToList();
                int pCount = qNodeParent.Count;
                Int64 pPk = Com_StockDocumentRequest.GetPkSDRTable(pCount);
                // lấy giá trị lớn nhất của cột thứ tự bên tree cần Genarate để chuẩn bị tăng thêm
                int maxSort = ProcessGeneral.GetSafeInt(DtSourceTree.AsEnumerable().Max(row => row["SortOrderNode"]));
                maxSort = maxSort == 0 ? 1:maxSort;

                var qParent = qNodeParent.Select((p, idp) => new
                {
                    ChildPK = Convert.ToInt64(idp + pPk),
                    ParentPK = Convert.ToInt64(0),
                    ProductionOrder = ProcessGeneral.GetSafeString(p.GetValue("ProductionOrder")),
                    CusRef = ProcessGeneral.GetSafeString(p.GetValue("CusRef")),
                    ProductCode = ProcessGeneral.GetSafeString(p.GetValue("ProductCode")),
                    ProductName = ProcessGeneral.GetSafeString(p.GetValue("ProductName")),
                    ItemType = ProcessGeneral.GetSafeString(p.GetValue("ItemType")),
                    SortOrderNode = ProcessGeneral.GetSafeInt(idp + maxSort),
                    TDG00001PK = ProcessGeneral.GetSafeInt64(p.GetValue("TDG00001PK")),
                    ItemCode = ProcessGeneral.GetSafeString(p.GetValue("RMCode_001")),
                    ItemName = ProcessGeneral.GetSafeString(p.GetValue("RMDescription_002")),
                    Reference = ProcessGeneral.GetSafeString(p.GetValue("Reference")),
                    MainMaterialGroup = ProcessGeneral.GetSafeString(p.GetValue("MainMaterialGroup")),
                    SourcePK = 0,
                    SourceName ="",
                    DestinationPK = 0,
                    DestinationName = "",
                    ProductionPointPK = ProcessGeneral.GetSafeInt64(p.GetValue("CNYMF012PK")),
                    ProductionPointName = ProcessGeneral.GetSafeString(p.GetValue("ProductionPoint")),
                    UnitPK = ProcessGeneral.GetSafeInt64(p.GetValue("BomUnitPK")),
                    UnitName = ProcessGeneral.GetSafeString(p.GetValue("UCUnit")),
                    CNY00020PK = ProcessGeneral.GetSafeInt64(p.GetValue("CNY00020PK")),
                    CNY00016PK = ProcessGeneral.GetSafeInt64(p.GetValue("CNY00016PK")),
                    PartnerPK = ProcessGeneral.GetSafeInt64(p.GetValue("CNY00002PK")),
                    PartnerName = ProcessGeneral.GetSafeString(p.GetValue("Supplier")),
                    Quantity = ProcessGeneral.GetSafeDecimal(p.GetValue("WOQty")),
                    Using = ProcessGeneral.GetSafeDecimal(p.GetValue("Using")),
                    Tolerance = ProcessGeneral.GetSafeDecimal(p.GetValue("Tolerance")),
                    AmountUC = ProcessGeneral.GetSafeDecimal(p.GetValue("AmountUC")),
                    NeededQty = ProcessGeneral.GetSafeDecimal(p.GetValue("NeededQty")),
                    ActualQty = ProcessGeneral.GetSafeDecimal(p.GetValue("NeededQty")),
                    Remark = "",
                    DocumentRef = "",
                    BomNo = ProcessGeneral.GetSafeString(p.GetValue("BomNo")),
                    RowState = DataStatus.Insert.ToString(),
                    AllowUpdate = 1,
                }).ToList();


                var CNY00016PK = qParent.Select(p => p.CNY00016PK).Distinct().ToList();
                Dictionary<Int64, List<BoMInputAttInfo>> dicVCode = _dicAttValueItemCode
                    .Where(p => CNY00016PK.Any(t => t == p.Key)).ToDictionary(item => item.Key, item => item.Value);

                if (_dicAttributeRm.Any())
                {
                    List<Int64> qDel = dicVCode.SelectMany(p => p.Value.Select(t => t.AttibutePK), (m, n) => n).Distinct().ToList();


                    var qAttJoin = _dicAttributeRm.Where(p => qDel.Any(t => t == p.Key)).Select(p => new
                    {
                        AttibutePK = p.Key,
                        p.Value.AttibuteName,
                        p.Value.DataType
                    }).ToList();

                    var qAdR = qAttJoin.Where(p => DicAttributeRoot.All(t => t.Key != p.AttibutePK)).ToList();
                    if (qAdR.Any())
                    {

                        foreach (var itemAdR in qAdR)
                        {
                            string attName = itemAdR.AttibuteName;
                            dtTemp.Columns.Add(attName, typeof(string));
                            DicAttributeRoot.Add(itemAdR.AttibutePK, new AttributeHeaderInfo
                            {
                                AttibuteName = attName,
                                DataType = itemAdR.DataType
                            });
                        }
                        dtTemp.AcceptChanges();
                    }
                }





                foreach (var itemP in qParent)
                {

                    DataRow drp = dtTemp.NewRow();
                    Int64 childPkP = itemP.ChildPK;
                    Int64 CNY00016PKAtt = itemP.CNY00016PK;
                    drp["ProductionOrder"] = itemP.ProductionOrder;
                    drp["CusRef"] = itemP.CusRef;
                    drp["ProductCode"] = itemP.ProductCode;
                    drp["ProductName"] = itemP.ProductName;
                    drp["ItemType"] = itemP.ItemType;
                    drp["SortOrderNode"] = itemP.SortOrderNode;
                    drp["TDG00001PK"] = itemP.TDG00001PK;
                    drp["ItemCode"] = itemP.ItemCode;
                    drp["ItemName"] = itemP.ItemName;
                    drp["Reference"] = itemP.Reference;
                    drp["MainMaterialGroup"] = itemP.MainMaterialGroup;
                    drp["SourcePK"] = itemP.SourcePK;
                    drp["SourceName"] = itemP.SourceName;
                    drp["DestinationPK"] = itemP.DestinationPK;
                    drp["DestinationName"] = itemP.DestinationName;
                    drp["ProductionPointPK"] = itemP.ProductionPointPK;
                    drp["ProductionPointName"] = itemP.ProductionPointName;
                    drp["UnitPK"] = itemP.UnitPK;
                    drp["UnitName"] = itemP.UnitName;
                    drp["CNY00020PK"] = itemP.CNY00020PK;
                    drp["CNY00016PK"] = itemP.CNY00016PK;
                    drp["PartnerPK"] = itemP.PartnerPK;
                    drp["PartnerName"] = itemP.PartnerName;
                    drp["Quantity"] = itemP.Quantity;
                    drp["Using"] = itemP.Using;
                    drp["Tolerance"] = itemP.Tolerance;
                    drp["AmountUC"] = itemP.AmountUC;
                    drp["NeededQty"] = itemP.NeededQty;
                    drp["ActualQty"] = itemP.ActualQty;
                    drp["Remark"] = itemP.Remark;
                    drp["DocumentRef"] = itemP.DocumentRef;
                    drp["BoMNo"] = itemP.BomNo;
                    drp["ChildPK"] = itemP.ChildPK;
                    drp["ParentPK"] = itemP.ParentPK;
                    drp["RowState"] = itemP.RowState;
                    drp["AllowUpdate"] = itemP.AllowUpdate;

                    dtTemp.Rows.Add(drp);

                    List<BoMInputAttInfo> lBomInfoP = new List<BoMInputAttInfo>();
                    List<BoMInputAttInfo> lDataP;
                    if (dicVCode.TryGetValue(CNY00016PKAtt, out lDataP))
                    {
                        foreach (var tP in lDataP)
                        {
                            String attibuteCodeP = tP.AttibuteCode;
                            String attibuteNameP = tP.AttibuteName;
                            String attibuteValueFullP = tP.AttibuteValueFull;
                            drp[string.Format("{0}-{1}", attibuteCodeP, attibuteNameP)] = attibuteValueFullP;
                            lBomInfoP.Add(new BoMInputAttInfo
                            {
                                AttibuteCode = attibuteCodeP,
                                AttibuteName = attibuteNameP,
                                AttibuteValueFull = attibuteValueFullP,
                                AttibutePK = tP.AttibutePK,
                                IsNumber = tP.IsNumber,
                                AttibuteUnit = tP.AttibuteUnit,
                                AttibuteValueTemp = tP.AttibuteValueTemp,
                                RowState = DataStatus.Insert,
                                PK = -1,

                            });

                        }
                    }

                    DicAttValueRoot.Add(childPkP, lBomInfoP);
                }

                dtTemp.AcceptChanges();

            }
            if (OnSDRWizard != null)
            {

                OnSDRWizard(this, new SDRWizardEventArgs
                {
                    DtGenerate = dtTemp,
                    IsEvent = isEvent
                });

            }
            _dlg.Close();
            this.Close();



        }


        private void LoadDataTreeViewItemFinal(TreeList tl)
        {

            DataTable dtI = tlItemCode.DataSource as DataTable;
            DataTable dt = dtI.Clone();

            List<TreeListNode> lNodeParent = lNodeSelected.Select(p => p.ParentNode).Distinct().ToList();


            foreach (TreeListNode parentNode in lNodeParent)
            {
                DataRowView pData = (DataRowView)tlItemCode.GetDataRecordByNode(parentNode);
                DataRow drP = pData.Row;
                dt.ImportRow(drP);

            }


            foreach (TreeListNode childNode in lNodeSelected)
            {
                DataRowView cData = (DataRowView)tlItemCode.GetDataRecordByNode(childNode);
                DataRow drC = cData.Row;
                dt.ImportRow(drC);
            }

            dt.AcceptChanges();

            // TreeList tl, DataTable dt

            tl.Columns.Clear();
            tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";

            List<string> arr = _inf.GetSortOrderAttribute(_dicAttributeRm);
            tl.BeginUpdate();
            Com_StockDocumentRequest.VisibleTreeColumnItemCode(tl, arr);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["MainMaterialGroup"], "Main Material Group", false, HorzAlignment.Near,TreeFixedStyle.None, "");


            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProductionOrder"], "Pro.Order", false, HorzAlignment.Center, TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RMCode_001"], "Item Code", false, HorzAlignment.Center, TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RMDescription_002"], "Item Name", false, HorzAlignment.Near, TreeFixedStyle.None, "");

            //ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ColorReference"], "Color", false, HorzAlignment.Near, TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Supplier"], "Supplier", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["SupplierRef"], "Supplier Ref", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["UCUnit"], "UC Unit", false, HorzAlignment.Center, TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["NeededQty"], "Needed Qty", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["NeededQty"].Format.FormatType = FormatType.Numeric;
            tl.Columns["NeededQty"].Format.FormatString = "#,0.######";


            foreach (var item in _dicAttributeRm)

            {
                string s = item.Value.AttibuteName;

                TreeListColumn tColS = tl.Columns[s];
                if (tColS == null) continue;
                ProcessGeneral.SetTreeListColumnHeader(tColS, s, false, HorzAlignment.Near, TreeFixedStyle.None, item.Key);
                tColS.ImageIndex = 0;
                tColS.ImageAlignment = StringAlignment.Near;
            }
            var qExpand = tl.Nodes.Where(p => p.Expanded).ToList();
            foreach (TreeListNode nodeColl in qExpand)
            {


                nodeColl.Expanded = false;
            }

            tl.ExpandAll();
            tl.BestFitColumns();

            tl.ForceInitialize();


            tl.EndUpdate();











        }



        private void LoadDataTreeViewItemCode(TreeList tl, DataTable dt)
        {




            tl.Columns.Clear();
            tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";

            List<string> arr = _inf.GetSortOrderAttribute(_dicAttributeRm);
            tl.BeginUpdate();
            Com_StockDocumentRequest.VisibleTreeColumnItemCode(tl, arr);




            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["MainMaterialGroup"], "Main Material Group", false, HorzAlignment.Near,
                TreeFixedStyle.None, "");


            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ProductionOrder"], "Pro.Order", false, HorzAlignment.Center, TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RMCode_001"], "Item Code", false, HorzAlignment.Center, TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RMDescription_002"], "Item Name", false, HorzAlignment.Near, TreeFixedStyle.None, "");

            //ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ColorReference"], "Color", false, HorzAlignment.Near, TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Supplier"], "Supplier", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["SupplierRef"], "Supplier Ref", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            //ProcessGeneral.SetTreeListColumnHeader(tl.Columns["UC"], "Quantity", false, HorzAlignment.Center, DevExpress.XtraTreeList.Columns.FixedStyle.None, "");
            //tl.Columns["UC"].Format.FormatType = FormatType.Numeric;
            //tl.Columns["UC"].Format.FormatString = "#,0.#####";

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["UCUnit"], "UC Unit", false, HorzAlignment.Center, TreeFixedStyle.None, "");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["NeededQty"], "Needed Qty", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["NeededQty"].Format.FormatType = FormatType.Numeric;
            tl.Columns["NeededQty"].Format.FormatString = "#,0.######";

            foreach (var item in _dicAttributeRm)

            {
                string s = item.Value.AttibuteName;

                TreeListColumn tColS = tl.Columns[s];
                if (tColS == null) continue;
                ProcessGeneral.SetTreeListColumnHeader(tColS, s, false, HorzAlignment.Near, TreeFixedStyle.None, item.Key);
                tColS.ImageIndex = 0;
                tColS.ImageAlignment = StringAlignment.Near;
            }
            var qExpand = tl.Nodes.Where(p => p.Expanded).ToList();
            foreach (TreeListNode nodeColl in qExpand)
            {


                nodeColl.Expanded = false;
            }

            tl.ExpandAll();
            tl.BestFitColumns();

            tl.ForceInitialize();



            tl.EndUpdate();

            //Mặc định hiển thị check khi giá trị Check=1
            tl.BeginUpdate();

            foreach (TreeListNode node in tl.Nodes)
            {
                TreeListNodes lNode = node.Nodes;
                var q1 = lNode.Where(p => ProcessGeneral.GetSafeBool(p.GetValue("Check"))).ToList();
                if (q1.Count == lNode.Count)
                {
                    node.CheckAll();
                }
                else
                {
                    foreach (TreeListNode nodeChild in q1)
                    {
                        nodeChild.CheckState = CheckState.Checked;
                    }
                }




            }



            tl.EndUpdate();
        }




        private void LoadDataTreeViewItemCode()
        {


            _dlg = new WaitDialogForm();
            DataTable dtBoM = _inf.LoadGridSelectRmNormalWhenGenerateN(dtAa04Pk);

            if (dtBoM.Rows.Count <= 0)

            {
                tlItemCode.Columns.Clear();
                tlItemCode.DataSource = null;
                _dlg.Close();
                return;

            }

            //List<DataRow> qFinal = dtBoM.AsEnumerable().Where(p => _dtSelectedPr.AsEnumerable().Any(t =>

            //             p.Field<Int64>("CNY00020PK") == t.Field<Int64>("CNY00020PK") &&
            //             p.Field<Int64>("CNY00016PK") == t.Field<Int64>("CNY00016PK") &&
            //             p.Field<int>("LeftPlanQty") < t.Field<int>("PlanQty")

            //             )).ToList();

            //if (qFinal.Any())
            //{

            //    foreach (DataRow drDel in qFinal)
            //    {
            //        dtBoM.Rows.Remove(drDel);
            //    }
            //    dtBoM.AcceptChanges();
            //    if (dtBoM.Rows.Count <= 0)
            //    {
            //        tlItemCode.Columns.Clear();
            //        tlItemCode.DataSource = null;
            //        _dlg.Close();
            //        return;
            //    }


            //}
            var qRm = dtBoM.AsEnumerable().Select(p => p.Field<Int64>("TDG00001PK")).Distinct().ToList();


            DataTable dtCategoyFull = _inf.LoadGridSelectCategoryWhenGenerate(qRm.CopyToDataTableNew());


            if (dtCategoyFull.Rows.Count <= 0)
            {
                tlItemCode.Columns.Clear();

                tlItemCode.DataSource = null;

                _dlg.Close();
                return;
            }
            var q16Pk = dtBoM.AsEnumerable().Select(p => p.Field<Int64>("CNY00016PK")).Distinct().ToList();

            DataTable dtAtemp = _inf.LoadAttributeWhenGenerate(q16Pk.CopyToDataTableNew());


            DataTable dtFinal = StandardTreeTableItemCode(dtBoM, dtAtemp, dtCategoyFull);
            LoadDataTreeViewItemCode(tlItemCode, dtFinal);



            _dlg.Close();
        }



        private DataTable StandardTreeTableItemCode(DataTable dtTreeTemp, DataTable dtAtemp, DataTable dtCategoyFull)
        {



            Int64 beginParentPk = dtTreeTemp.Rows.Count + 10;


            var qP0 = dtCategoyFull.AsEnumerable().Select(p => p.Field<string>("Code")).Distinct().Select(

                (p, inx) => new
                {
                    Index = inx + beginParentPk,
                    Code = p.ToString()
                }).ToList();


            Dictionary<Int64, PrGlobalCategoryInfo> qP1 = qP0.Join(dtCategoyFull.AsEnumerable(), p => p.Code, t => t.Field<string>("Code"), (p, t) => new
            {
                KeyDic = t.Field<Int64>("TDG00001PK"),
                TempData = new PrGlobalCategoryInfo
                {
                    Code = p.Code,
                    Description = t.Field<string>("Description"),

                    Index = p.Index,

                    FormulaString = t.Field<string>("FormulaString"),
                    FormulaStringDisplay = t.Field<string>("FormulaStringDisplay"),
                    PurchaseUnit = t.Field<string>("PurchaseUnit"),
                },

            }).ToDictionary(item => item.KeyDic, item => item.TempData);






            _dicAttValueItemCode.Clear();
            _dicAttValueItemCode = dtAtemp.AsEnumerable().GroupBy(f => new
            {
                CNY00016PK = f.Field<Int64>("CNY00016PK"),
            }).Where(myGroup => myGroup.Any())
                .Select(myGroup => new
                {
                    KeyDic = myGroup.Key.CNY00016PK,
                    TempData = myGroup.GroupBy(f => new
                    {
                        AttibuteCode = f.Field<String>("AttibuteCode"),
                        AttibuteName = f.Field<String>("AttibuteName"),
                        AttibuteValueFull = f.Field<String>("AttibuteValueFull"),
                        AttibutePK = f.Field<Int64>("AttibutePK"),
                        AttibuteValueTemp = f.Field<String>("AttibuteValueTemp"),
                        AttibuteUnit = f.Field<String>("AttibuteUnit"),
                        IsNumber = f.Field<Boolean>("IsNumber"),
                        PK = f.Field<Int64>("PK"),
                        RowState = ProcessGeneral.GetDataStatus(f.Field<String>("RowState"))
                    }).Select(m => new BoMInputAttInfo
                    {
                        AttibuteCode = m.Key.AttibuteCode,
                        AttibuteName = m.Key.AttibuteName,
                        AttibuteValueFull = m.Key.AttibuteValueFull,
                        AttibutePK = m.Key.AttibutePK,
                        AttibuteValueTemp = m.Key.AttibuteValueTemp,
                        AttibuteUnit = m.Key.AttibuteUnit,
                        IsNumber = m.Key.IsNumber,
                        PK = m.Key.PK,
                        RowState = m.Key.RowState,
                    }).Where(n => !string.IsNullOrEmpty(n.AttibuteCode)).ToList()
                }).ToDictionary(item => item.KeyDic, item => item.TempData);


            var qAd = dtAtemp.AsEnumerable().Select(p => new
            {
                AttibutePK = ProcessGeneral.GetSafeInt64(p["AttibutePK"]),
                AttibuteName = string.Format("{0}-{1}", ProcessGeneral.GetSafeString(p["AttibuteCode"]), ProcessGeneral.GetSafeString(p["AttibuteName"])),
                DataType = ProcessGeneral.GetDataAttType(ProcessGeneral.GetSafeString(p["DataAttType"]))
            }).Distinct().Select(p => new BoMCompactAttInfo
            {
                AttibutePK = p.AttibutePK,
                AttibuteName = p.AttibuteName,
                DataType = p.DataType
            }).ToList();






            _dicAttributeRm.Clear();
            foreach (var itemAd in qAd)
            {

                string attName = itemAd.AttibuteName;
                Int64 attPkT = itemAd.AttibutePK;
                _dicAttributeRm.Add(attPkT, new AttributeHeaderInfo
                {
                    AttibuteName = attName,
                    DataType = itemAd.DataType
                });

                dtTreeTemp.Columns.Add(attName, typeof(string));
            }
            dtTreeTemp.Columns.Add("ChildPK", typeof(Int64));
            dtTreeTemp.Columns.Add("ParentPK", typeof(Int64));
            dtTreeTemp.Columns.Add("SortOrderNode", typeof(Int64));
            dtTreeTemp.Columns.Add("FormulaString", typeof(string));
            dtTreeTemp.Columns.Add("FormulaStringDisplay", typeof(string));
            dtTreeTemp.Columns.Add("PurchaseUnit", typeof(string));
            int iLoop = 1;
            foreach (DataRow drSource in dtTreeTemp.Rows)
            {
                Int64 cny00016Pk = ProcessGeneral.GetSafeInt64(drSource["CNY00016PK"]);
                Int64 tdg00001Pk = ProcessGeneral.GetSafeInt64(drSource["TDG00001PK"]);


                List<BoMInputAttInfo> lBomInfoLoop;

                if (_dicAttValueItemCode.TryGetValue(cny00016Pk, out lBomInfoLoop))
                {
                    foreach (BoMInputAttInfo t1 in lBomInfoLoop)
                    {
                        String attibuteCode = t1.AttibuteCode;
                        String attibuteName = t1.AttibuteName;
                        String attibuteValueFull = t1.AttibuteValueFull;
                        drSource[string.Format("{0}-{1}", attibuteCode, attibuteName)] = attibuteValueFull;


                    }

                }

                drSource["ChildPK"] = iLoop;
                drSource["SortOrderNode"] = iLoop;
                //drSource["RowIndex"] = iLoop;

                string globalGrTemp = "";
                string formulaString = "";
                string formulaStringDisplay = "";
                string purchaseUnit="";
                PrGlobalCategoryInfo tempInfo1;
                if (qP1.TryGetValue(tdg00001Pk, out tempInfo1))
                {
                    drSource["ParentPK"] = tempInfo1.Index;
                    globalGrTemp = String.Format("{0}-{1}", tempInfo1.Code, tempInfo1.Description);
                    formulaString = tempInfo1.FormulaString;
                    formulaStringDisplay = tempInfo1.FormulaStringDisplay;
                    purchaseUnit = tempInfo1.PurchaseUnit;
                }
                drSource["FormulaString"] = formulaString;
                drSource["FormulaStringDisplay"] = formulaStringDisplay;
                drSource["MainMaterialGroup"] = globalGrTemp;
                drSource["PurchaseUnit"] = purchaseUnit;
                iLoop++;



            }

            /*

            List<DataRowIndexSort> qS1 = dtTreeTemp.AsEnumerable().OrderBy(p => p.Field<Int64>("ParentPK"))
                                                                .ThenBy(p => p.Field<Int64>("TDG00001PK"))
                                                                .ThenBy(p => p.Field<Int64>("CNY00002PK"))
                                                                .ThenBy(p => p.Field<string>("UCUnitCode"))
                                                                .ThenBy(p => p.Field<decimal>("UC"))
                                                                .ThenBy(p => p.Field<decimal>("Waste"))
                                                                .ThenBy(p => p.Field<decimal>("PercentUsing"))
                                                                .Select((p, i) => new DataRowIndexSort {
                                                                    RowData = p,
                                                                    RowIndex = i
                                                                }).ToList();
                                                                
             
            string[] arrStrSort = _dicAttributeRm.Select(p=>p.Value.AttibuteName).ToArray();
            string strFieldAtt = "";
            foreach (string sS in arrStrSort)
            {
                qS1 = qS1.OrderBy(p => p.RowIndex).ThenBy(p => p.RowData.Field<string>(sS)).Select((p, j) => new DataRowIndexSort
                {
                    RowData = p.RowData,
                    RowIndex = j
                }).ToList();
                strFieldAtt = string.Format("{0}[{1}],", strFieldAtt, sS);
            }
            _dtFinalBoM = dtTreeTemp.Clone();

            qS1.ForEach(t =>
            {
                DataRow dr = t.RowData;
                dr["RowIndex"] = t.RowIndex;
                _dtFinalBoM.ImportRow(dr);
            });
             
             */
            _dtFinalBoM = dtTreeTemp;
            string tableName = ProcessGeneral.GetRandomTableName();



            //   strFieldAttWhere = strFieldAttWhere.Trim();




            _inf.CreateTableSqlServer(_dtFinalBoM, tableName);
            _inf.CopyDataFromDataTableToSqlTable(_dtFinalBoM, tableName);




            string[] arrStrSort = _dicAttributeRm.Select(p => p.Value.AttibuteName.Trim()).ToArray();
            string strFieldAttSelGrp = "";
            string strFieldAttWhere = "";
            string sqlUpdAtt = "";
            foreach (string sS in arrStrSort)
            {

                strFieldAttSelGrp = strFieldAttSelGrp + "[a].[" + sS + "],";
                strFieldAttWhere = strFieldAttWhere + " [a].[" + sS + "] = [b].[" + sS + "] and ";
                sqlUpdAtt = sqlUpdAtt + "[" + sS + "]=isnull([" + sS + "],''),";
            }
            sqlUpdAtt = sqlUpdAtt.Trim();
            // string strFieldAtt = "";


            if (!string.IsNullOrEmpty(sqlUpdAtt))
            {
                sqlUpdAtt = sqlUpdAtt.Substring(0, sqlUpdAtt.Length - 1);
                sqlUpdAtt = "UPDATE " + tableName + " set " + sqlUpdAtt;
                _inf.BolExcuteSqlText(sqlUpdAtt);
            }









            DataTable dtFinal = _inf.LoadTablePrGenerateFinal(tableName, strFieldAttSelGrp.Trim(), strFieldAttWhere.Trim());

            //_inf.DropSqlTable(tableName);



            var qP2 = qP1.Select(p => new
            {
                Code = string.Format("{0}-{1}", p.Value.Code, p.Value.Description),
                Index = p.Value.Index,
            }).Distinct().OrderBy(p => p.Code).ToList();


            //var q1 = dtFinal.AsEnumerable().Select(p => new
            //{
            //    MinRow = Convert.ToInt32(p.Field<Int64>("MinRow")),
            //    MaxRow = Convert.ToInt32(p.Field<Int64>("MaxRow") - p.Field<Int64>("MinRow") + 1),
            //}).Select(p=>Enumerable.Range(p.MinRow, p.MaxRow).ToArray()).SelectMany(t=>t,(m,n)=>n).ToList();

            foreach (var item in qP2)
            {


                DataRow drAddTree = dtFinal.NewRow();

                drAddTree["MainMaterialGroup"] = item.Code;
                drAddTree["ChildPK"] = item.Index;
                drAddTree["ParentPK"] = 0;


                //drAddTree["TDG00001PK"]=0;
                //drAddTree["RMCode_001"]="";
                //drAddTree["RMDescription_002"] = "";
                //drAddTree["ColorReference"] = "";
                //drAddTree["CNY00002PK"] = 0;
                //drAddTree["Supplier"] = "";


                //foreach (var itemAd in qAd)
                //{

                //    string attName = itemAd.AttibuteName;
                //    drAddTree[attName] = "";


                //}


                //drAddTree["UC"] = 0;
                //drAddTree["UCUnitCode"] = "";

                //drAddTree["UCUnit"] = "";


                //drAddTree["Waste"] = 0;
                //drAddTree["PercentUsing"] = 0;


                //drAddTree["CNY00020PK"] = 0;
                //drAddTree["CNY00016PK"] = 0;
                //drAddTree["PlanQty"] = 0;


                //drAddTree["SOQty"] = 0;





                dtFinal.Rows.Add(drAddTree);
            }
            dtFinal.AcceptChanges();







            return dtFinal;




        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion




        #region "Proccess Treeview"








        private ImageList GetImageListDisplayTreeView()
        {
            var imgLt = new ImageList();
            imgLt.Images.Add(Resources.Product_BoM_16x16);
            imgLt.Images.Add(Resources.RawMaterial_BoM_16x16);
            return imgLt;
        }


        private void InitTreeList(TreeList treeList)
        {

            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
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

            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = true;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;

            treeList.AllowDrop = false;

            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsView.ShowCheckBoxes = true;


            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,

            };







            treeList.ShowingEditor += TreeList_ShowingEditor;



            treeList.CustomDrawNodeIndicator += TreeList_CustomDrawNodeIndicator;






            treeList.NodeCellStyle += TreeList_NodeCellStyle;

            treeList.GetStateImage += TreeList_GetStateImage;

            treeList.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;

            treeList.ShownEditor += TreeList_ShownEditor;
            treeList.CellValueChanged += TreeList_CellValueChanged;
            treeList.GetNodeDisplayValue += TreeList_GetNodeDisplayValue;



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
            switch (fieldName)
            {
                case "Customer":
                case "CustomerOrderNo":
                case "ProjectNo":
                case "ProjectName":
                case "DeliveryDate":
                case "ProductionOrder":
                case "OrderNo":
                    {
                        if (node.ParentNode != null)
                        {
                            e.Value = "";
                        }
                    }
                    break;
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
            bool isParent = node.ParentNode == null;
            bool isCheck = e.Node.CheckState == CheckState.Checked;

            if (isParent)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Regular), GraphicsUnit.Point, 0);
            }
            switch (fieldName)
            {
                case "OrderQuantity":
                    {


                        e.Appearance.ForeColor = Color.DarkOrange;
                    }
                    break;
                case "PlanQuantity":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;
                case "BalanceQuantity":
                    {

                        e.Appearance.ForeColor = Color.DarkGreen;
                    }
                    break;

            }
            if (fieldName.IndexOf("-", StringComparison.Ordinal) > 0)
            {



                if (!isParent)
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LightGoldenrodYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else
                {

                    if (isCheck)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }

                }



            }
            else
            {
                if (fieldName == "Customer" || fieldName == "CustomerOrderNo" || fieldName == "ProjectNo" || fieldName == "ProjectName" || fieldName == "DeliveryDate"
                    || fieldName == "ProductionOrder" || fieldName == "OrderNo")
                {
                    if (isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LavenderBlush;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;

                    }
                    else
                    {
                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }

                }
                else if (fieldName == "BOMID" || fieldName == "BoMNo")
                {
                    if (!isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LavenderBlush;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {

                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }

                    }
                }
                else if (fieldName == "OrderQuantity")
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.GhostWhite;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else if (fieldName == "PlanQuantity")
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.Lavender;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else if (fieldName == "BalanceQuantity")
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.WhiteSmoke;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else
                {
                    if (isCheck)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                }
            }





        }


        private void TreeList_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (!_performEditValueChangeEvent) return;
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            if (e.Column == null) return;
            string fieldName = e.Column.FieldName;


            if (fieldName == "PlanQuantity" && !node.HasChildren)
            {

                TreeListNode parentNode = node.ParentNode;
                node.SetValue("BalanceQuantity", ProcessGeneral.GetSafeInt(node.GetValue("OrderQuantity")) - ProcessGeneral.GetSafeInt(e.Value));
                List<TreeListNode> lNode = parentNode.GetAllChildNode().ToList();
                Int32 sumPlan = 0;
                Int32 sumBalance = 0;
                foreach (TreeListNode cNode in lNode)
                {
                    sumPlan += ProcessGeneral.GetSafeInt(cNode.GetValue("PlanQuantity"));
                    sumBalance += ProcessGeneral.GetSafeInt(cNode.GetValue("BalanceQuantity"));
                }
                parentNode.SetValue("PlanQuantity", sumPlan);
                parentNode.SetValue("BalanceQuantity", sumBalance);

            }






        }

        private void TreeList_ShownEditor(object sender, EventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;
            switch (fieldName)
            {
                case "PlanQuantity":
                    if (!node.HasChildren)
                    {
                        var editor = tl.ActiveEditor as SpinEdit;
                        if (editor != null)
                        {
                            editor.Properties.MinValue = 0;
                            editor.Properties.MaxValue = ProcessGeneral.GetSafeInt(node.GetValue("OrderQuantity"));
                        }
                    }

                    break;
            }

        }

        private void TreeList_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            string fieldName = e.Column.FieldName;

            switch (fieldName)
            {
                case "PlanQuantity":
                    e.RepositoryItem = _repositorySpinN0;
                    break;


            }

        }

        private void TreeList_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;
            e.NodeImageIndex = e.Node.HasChildren ? 0 : 1;
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
        private void TreeList_ShowingEditor(object sender, CancelEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;
            switch (fieldName)
            {
                case "PlanQuantity":
                    e.Cancel = node.HasChildren;

                    break;
                default:
                    e.Cancel = true;
                    break;
            }


        }


        #endregion


        #region "Proccess Treeview ItemCode"









        private void InitTreeListItemCode(TreeList treeList)
        {

            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
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

            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = true;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;

            treeList.AllowDrop = false;

            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsView.ShowCheckBoxes = true;


            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,

            };







            treeList.ShowingEditor += TreeListItemCode_ShowingEditor;



            treeList.CustomDrawNodeIndicator += TreeListItemCode_CustomDrawNodeIndicator;






            treeList.NodeCellStyle += TreeListItemCode_NodeCellStyle;

            treeList.GetStateImage += TreeListItemCode_GetStateImage;


            treeList.GetNodeDisplayValue += TreeListItemCode_GetNodeDisplayValue;


            treeList.FocusedNodeChanged += TreeListItemCode_FocusedNodeChanged;

            treeList.AfterCheckNode += TreeListItemCode_AfterCheckNode;

        }

        private void TreeListItemCode_AfterCheckNode(object sender, NodeEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;

            TreeListNode parentNode = node.ParentNode == null ? node : node.ParentNode;
            if (node.HasChildren)
            {
                List<TreeListNode> q1 = node.Nodes.Where(p => !ProcessGeneral.GetSafeBool(p.GetValue("Check"))).ToList();
                foreach (TreeListNode tlNode in q1)
                {
                    tlNode.Checked = false;
                }
            }

        }
        private void TreeListItemCode_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            //SetReloadDataSourceRm(e.OldNode, e.Node);
            TreeListNode node = tl.FocusedNode;
            LoadDataGridViewProductionFrist(node);





        }


        private void TreeListItemCode_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
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
                case "MainMaterialGroup":
                    {
                        if (node.ParentNode != null)
                        {
                            e.Value = "";
                        }
                    }
                    break;
            }
        }

        private void TreeListItemCode_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;
            e.NodeImageIndex = e.Node.HasChildren ? 0 : 1;
        }






        private void TreeListItemCode_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            bool isParent = node.ParentNode == null;
            bool isCheck = e.Node.CheckState == CheckState.Checked;

            if (isParent)
            {
                e.Appearance.Font = new Font("Tahoma", 9F, (FontStyle.Bold), GraphicsUnit.Point, 0);
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Regular), GraphicsUnit.Point, 0);
            }
            switch (fieldName)
            {
                case "MainMaterialGroup":
                    {


                        e.Appearance.ForeColor = Color.MediumVioletRed;
                    }
                    break;
                case "UC":
                    {


                        e.Appearance.ForeColor = Color.DarkOrange;
                    }
                    break;
                case "Waste":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;
                case "PercentUsing":
                    {

                        e.Appearance.ForeColor = Color.DarkGreen;
                    }
                    break;

            }
            if (fieldName.IndexOf("-", StringComparison.Ordinal) > 0)
            {



                if (!isParent)
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LightGoldenrodYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else
                {

                    if (isCheck)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }

                }



            }
            else
            {
                if (fieldName == "MainMaterialGroup")
                {
                    if (isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LavenderBlush;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {

                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }

                    }
                }
                else if (fieldName == "Waste")
                {
                    if (!isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.GhostWhite;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {
                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }

                }
                else if (fieldName == "UC")
                {

                    if (!isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.Lavender;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {
                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }
                }
                else if (fieldName == "PercentUsing")
                {

                    if (!isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.WhiteSmoke;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    else
                    {
                        if (isCheck)
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                    }
                }
                else
                {
                    if (isCheck)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorSelectedRow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                }
            }





        }




        private void TreeListItemCode_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
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
        private void TreeListItemCode_ShowingEditor(object sender, CancelEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            //  string fieldName = col.FieldName;
            e.Cancel = true;

        }


        #endregion



        #region "Proccess Treeview Final"









        private void InitTreeListFinal(TreeList treeList)
        {

            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
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

            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = true;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;

            treeList.AllowDrop = false;

            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsView.ShowCheckBoxes = false;


            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,

            };







            treeList.ShowingEditor += TreeListFinal_ShowingEditor;



            treeList.CustomDrawNodeIndicator += TreeListFinal_CustomDrawNodeIndicator;






            treeList.NodeCellStyle += TreeListFinal_NodeCellStyle;

            treeList.GetStateImage += TreeListFinal_GetStateImage;


            treeList.GetNodeDisplayValue += TreeListFinal_GetNodeDisplayValue;






        }
        private void TreeListFinal_GetNodeDisplayValue(object sender, GetNodeDisplayValueEventArgs e)
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
                case "MainMaterialGroup":
                    {
                        if (node.ParentNode != null)
                        {
                            e.Value = "";
                        }
                    }
                    break;
            }
        }

        private void TreeListFinal_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;
            e.NodeImageIndex = e.Node.HasChildren ? 0 : 1;
        }






        private void TreeListFinal_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            bool isParent = node.ParentNode == null;


            if (isParent)
            {
                e.Appearance.Font = new Font("Tahoma", 9F, (FontStyle.Bold), GraphicsUnit.Point, 0);
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Regular), GraphicsUnit.Point, 0);
            }
            switch (fieldName)
            {
                case "MainMaterialGroup":
                    {


                        e.Appearance.ForeColor = Color.MediumVioletRed;
                    }
                    break;
                case "UC":
                    {


                        e.Appearance.ForeColor = Color.DarkOrange;
                    }
                    break;
                case "Waste":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;
                case "PercentUsing":
                    {

                        e.Appearance.ForeColor = Color.DarkGreen;
                    }
                    break;

            }
            if (fieldName.IndexOf("-", StringComparison.Ordinal) > 0)
            {



                if (!isParent)
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LightGoldenrodYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }



            }
            else
            {
                if (fieldName == "MainMaterialGroup")
                {
                    if (isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LavenderBlush;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                }
                else if (fieldName == "Waste")
                {
                    if (!isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.GhostWhite;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }

                }
                else if (fieldName == "UC")
                {

                    if (!isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.Lavender;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                }
                else if (fieldName == "PercentUsing")
                {

                    if (!isParent)
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.WhiteSmoke;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                }
            }




        }




        private void TreeListFinal_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;


            bool isCheck = e.Node.Selected;

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
        private void TreeListFinal_ShowingEditor(object sender, CancelEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = tl.FocusedNode;
            if (node == null) return;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null) return;
            //  string fieldName = col.FieldName;
            e.Cancel = true;

        }


        #endregion


        #region "hotkey"


        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {


                #region "Command"
                case Keys.Control | Keys.Shift | Keys.B:
                    {
                        if (btnBack.Enabled)
                        {
                            btnBack_Click(null, null);
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.C:
                    {
                        this.Close();
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.N:
                    {

                        if (btnNextFinish.Enabled && btnNextFinish.Text == @"Next")
                        {
                            btnNextFinish_Click(null, null);
                        }
                        return true;
                    }

                case Keys.Control | Keys.Shift | Keys.F:
                    {
                        if (btnNextFinish.Enabled && btnNextFinish.Text == @"Finish")
                        {
                            btnNextFinish_Click(null, null);
                        }
                        return true;
                    }

                    #endregion

            }
            return base.ProcessCmdKey(ref message, keys);



        }


        #endregion




        #region "Proccess Treeview"

        private void LoadDataTreeView(TreeList tl, DataTable dt, bool isBestFit)
        {
            bool isShowFind = false;
            string findPanelText = "";
            if (tl.FindPanelVisible)
            {
                isShowFind = true;
                findPanelText = tl.FindFilterText;
                tl.HideFindPanel();
            }
            Dictionary<string, Int32> dicWidth = new Dictionary<string, int>();
            if (!isBestFit)
            {
                foreach (TreeListColumn colWidth in tl.VisibleColumns)
                {
                    dicWidth.Add(colWidth.FieldName, colWidth.Width);


                }
            }



            tl.Columns.Clear();
            tl.DataSource = null;

            tl.DataSource = dt;
            tl.ParentFieldName = "ParentPK";
            tl.KeyFieldName = "ChildPK";
            Dictionary<Int64, AttributeHeaderInfo> dicSort = DicAttributeRoot.Union(_DicAttributeRootTemp).ToDictionary(t => t.Key, t => t.Value);
            List<string> arr = _inf.GetSortOrderAttribute(dicSort);
            tl.BeginUpdate();

            Com_StockDocumentRequest.VisibleTreeColumnSortGenerate(tl, false, arr);


            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RMCode_001"], "Item Code (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tl.Columns["RMCode_001"].ImageIndex = 0;
            tl.Columns["RMCode_001"].ImageAlignment = StringAlignment.Near;
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["RMDescription_002"], "Item Name", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            //ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Color"], "Color (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            //tl.Columns["Color"].ImageIndex = 0;
            //tl.Columns["Color"].ImageAlignment = StringAlignment.Near;
            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["MainMaterialGroup"], "Main Material Group", false, HorzAlignment.Near, TreeFixedStyle.None, "");


            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Supplier"], "Supplier (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tl.Columns["Supplier"].ImageIndex = 0;
            tl.Columns["Supplier"].ImageAlignment = StringAlignment.Near;

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["SupplierRef"], "Supplier Ref (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tl.Columns["SupplierRef"].ImageIndex = 0;
            tl.Columns["SupplierRef"].ImageAlignment = StringAlignment.Near;

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["StockQty_CNY010"], "Stock Qty", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["StockQty_CNY010"].Format.FormatType = FormatType.Numeric;
            tl.Columns["StockQty_CNY010"].Format.FormatString = FunctionFormatModule.StrFormatStockQtyDecimal(false, false);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["PRQty_CNY002"], "Demand Qty", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["PRQty_CNY002"].Format.FormatType = FormatType.Numeric;
            tl.Columns["PRQty_CNY002"].Format.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["POQty_CNY003"], "Purchase Qty", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["POQty_CNY003"].Format.FormatType = FormatType.Numeric;
            tl.Columns["POQty_CNY003"].Format.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Unit"], "Unit", false, HorzAlignment.Center, TreeFixedStyle.None, "");


            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["PRQty_CNY002B"], "Demand Qty (BoM)", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["PRQty_CNY002B"].Format.FormatType = FormatType.Numeric;
            tl.Columns["PRQty_CNY002B"].Format.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["POQty_CNY003B"], "Purchase Qty (BoM)", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["POQty_CNY003B"].Format.FormatType = FormatType.Numeric;
            tl.Columns["POQty_CNY003B"].Format.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["UnitB"], "Unit (BoM)", false, HorzAlignment.Center, TreeFixedStyle.None, "");




            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["PackingFactor"], "Factor", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            tl.Columns["PackingFactor"].Format.FormatType = FormatType.Numeric;
            tl.Columns["PackingFactor"].Format.FormatString = FunctionFormatModule.StrFormatPackingFactorDecimal(false, false);


            //ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ETD"], "ETD", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            //tl.Columns["ETD"].Format.FormatType = FormatType.DateTime;
            //tl.Columns["ETD"].Format.FormatString = ConstSystem.SysDateFormat;


            //ProcessGeneral.SetTreeListColumnHeader(tl.Columns["ETA"], "ETA", false, HorzAlignment.Center, TreeFixedStyle.None, "");
            //tl.Columns["ETA"].Format.FormatType = FormatType.DateTime;
            //tl.Columns["ETA"].Format.FormatString = ConstSystem.SysDateFormat;
            //ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Note"], "Note", false, HorzAlignment.Near, TreeFixedStyle.None, "");





            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["Purchaser"], "Purchaser (F4)", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tl.Columns["Purchaser"].ImageIndex = 0;
            tl.Columns["Purchaser"].ImageAlignment = StringAlignment.Near;




            foreach (var item in dicSort)
            {
                string s = item.Value.AttibuteName;

                TreeListColumn tColS = tl.Columns[s];
                if (tColS == null) continue;
                ProcessGeneral.SetTreeListColumnHeader(tColS, s, false, HorzAlignment.Near, TreeFixedStyle.None, item.Key);

                tColS.ImageIndex = 0;
                tColS.ImageAlignment = StringAlignment.Near;
            }


            tl.ExpandAll();


            if (isBestFit)
            {
                tl.BestFitColumns();
                //tl.Columns["ETD"].Width = 80;
                //tl.Columns["ETA"].Width = 80;
                if (tl.Columns["Color"].Width > 200)
                {
                    tl.Columns["Color"].Width = 200;
                }

            }
            else
            {
                foreach (var itemWidth in dicWidth)
                {
                    TreeListColumn colWidthSet = tl.Columns[itemWidth.Key];
                    if (colWidthSet == null) continue;
                    colWidthSet.Width = itemWidth.Value;
                }
            }

            tl.ForceInitialize();
            tl.EndUpdate();

            if (isShowFind)
            {
                tl.ShowFindPanelTreeList(findPanelText);
            }
        }






        private void InitTreeListMain(TreeList treeList)
        {

            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
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

            treeList.OptionsBehavior.Editable = true; treeList.OptionsView.ShowColumns = true;
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

            treeList.OptionsBehavior.CloseEditorOnLostFocus = true;
            treeList.OptionsBehavior.KeepSelectedOnClick = true;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;






            treeList.ColumnsImageList = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);
            new TreeListMultiCellSelector(treeList, true)
            {
                AllowSort = false,
                FilterShowChild = true,

            };







            treeList.ShowingEditor += TreeListMain_ShowingEditor;
            treeList.FocusedNodeChanged += TreeList_FocusedNodeChanged;
            treeList.GetStateImage += TreeListMain_GetStateImage;

            treeList.CustomDrawNodeIndicator += TreeListMain_CustomDrawNodeIndicator;
            treeList.NodeCellStyle += TreeListMain_NodeCellStyle;





            //    LoadDataTreeView(treeList, Ctrl_PrGenneral.TableTreeviewTemplate(), true);




        }
        private void TreeListMain_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {

            TreeList tl = sender as TreeList;
            if (tl == null) return;

            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;
            string fieldName = col.FieldName;
            bool isParent = node.HasChildren;

            if (isParent)
            {
                e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
            }
            switch (fieldName)
            {
                case "PRQty_CNY002":
                case "PRQty_CNY002B":
                    {


                        e.Appearance.ForeColor = Color.DarkOrange;
                    }
                    break;
                case "POQty_CNY003":
                case "POQty_CNY003B":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;
                case "PackingFactor":
                    {

                        e.Appearance.ForeColor = Color.Blue;
                    }
                    break;

            }

            if (fieldName.IndexOf("-", StringComparison.Ordinal) > 0)
            {
                string valueAtt = ProcessGeneral.GetSafeString(node.GetValue(fieldName));
                if (string.IsNullOrEmpty(valueAtt))
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LightGoldenrodYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.GreenYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }


            }
            else
            {
                switch (fieldName)
                {

                    case "RMCode_001":
                        {
                            if (ProcessGeneral.GetSafeBool(node.GetValue("AllowChangeWaste")))
                            {
                                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                e.Appearance.BackColor = Color.Ivory;
                                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                            }
                            else
                            {
                                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                            }

                        }
                        break;
                    case "RMDescription_002":
                    {
                        if (ProcessGeneral.GetSafeBool(node.GetValue("DifUC")))
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.Yellow;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        else
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }

                    }
                        break;
                    case "Color":
                    case "MainMaterialGroup":
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        break;

                    case "Supplier":
                    case "SupplierRef":
                    case "StockQty_CNY010":
                    case "Purchaser":
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        break;
                    case "PRQty_CNY002":
                    case "PRQty_CNY002B":
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.Cornsilk;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        break;
                    case "POQty_CNY003":
                    case "POQty_CNY003B":
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LightPink;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        break;
                    //case "PlanQuantity":
                    //    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    //    e.Appearance.BackColor = Color.PaleGoldenrod;
                    //    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    //    break;
                    case "Unit":
                    case "UnitB":
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        break;
                    case "PackingFactor":
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        break;

                }



            }



        }
        private void TreeListMain_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            if (tl.GetDataRecordByNode(e.Node) == null) return;

            LinearGradientBrush backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);




            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);
            if (tl.Selection.Contains(e.Node))
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

        private void TreeListMain_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;

            if (node.HasChildren)
            {
                e.NodeImageIndex = 0;
            }
            else
            {
                e.NodeImageIndex = 1;
            }
        }

        private void TreeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            //SetReloadDataSourceRm(e.OldNode, e.Node);


            if (tl.AllNodesCount <= 0) return;
            TreeListNode focusedNode = tl.FocusedNode;
            if (focusedNode == null) return;


            Int64 pkChild = ProcessGeneral.GetSafeInt64(focusedNode.GetValue("ChildPK"));
            DataTable dtColor;
            bool findColor = _dicTableColorTemp.TryGetValue(pkChild, out dtColor);
            if (!findColor)
            {
                dtColor = Com_StockDocumentRequest.TableGridChildN1Template();
                _dicTableColorTemp.Add(pkChild, dtColor);
            }


            if (dtColor.Rows.Count > 0)
            {
                LoadDataGridViewColorFrist(dtColor.AsEnumerable().OrderBy(p => p.Field<string>("Reference")).ThenBy(p => p.Field<string>("RmDimension")).ThenBy(p => p.Field<string>("Position")).CopyToDataTable());
            }
            else
            {
                LoadDataGridViewColorFrist(dtColor);
            }

            LoadDataGridViewDetailFrist(dtColor);




        }

        private void LoadDataGridViewColorFrist(DataTable dt)
        {

            gvColor.BeginUpdate();





            gcColor.DataSource = null;
            gvColor.Columns.Clear();

            gcColor.DataSource = dt;
            Dictionary<string, bool> dicCol = new Dictionary<string, bool>
            {
                {"Reference", true},
                {"PRQty_CNY002", true},
                {"StockQty_CNY010", true},
                {"POQty_CNY003", true},
                { "PRQty_CNY002B", false},
                {"POQty_CNY003B", false},
                {"PlanQuantity", true},
                {"UC", true},
                {"Factor", true},
                {"TotalQuantity", true},
                {"Position", true},
                { "AssemblyComponent", true},
                {"RmDimension", false},


            };


            dicCol.Add("Tolerance", true);
            dicCol.Add("PercentUsing", true);
            dicCol.Add("RootSOQty", true);
            dicCol.Add("RMDescription_002", true);
            dicCol.Add("ProductionOrder", true);
            dicCol.Add("ProjectName", true);
            dicCol.Add("ProDimension", true);
            dicCol.Add("CNY00020PK", false);
            dicCol.Add("RMCode_001", false);
            dicCol.Add("TDG00001PK", false);
            dicCol.Add("CNY00016PK", false);
            dicCol.Add("ChildPK", false);
            dicCol.Add("ParentPK", false);
            dicCol.Add("RowState", false);
            dicCol.Add("AllowUpdate", false);
            dicCol.Add("ToleranceBOM", false);
            dicCol.Add("CNY00019PK", false);
            dicCol.Add("UC_BOM", false);
            gvColor.VisibleAndSortGridColumn(dicCol);

            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["PlanQuantity"], "Pro. Order Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["PlanQuantity"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gvColor.Columns["PlanQuantity"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["PlanQuantity"].DisplayFormat.FormatString = "N0";
            gvColor.Columns["PlanQuantity"].ImageIndex = 0;
            gvColor.Columns["PlanQuantity"].ImageAlignment = StringAlignment.Near;
            //gvColor.Columns["PlanQuantity"].SummaryItem.SummaryType = GridSumCol.Sum;
            //gvColor.Columns["PlanQuantity"].SummaryItem.DisplayFormat = @"{0:N0}";



            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["UC"], "Amount", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["UC"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["UC"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatUcDecimal(false, false);

            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["Tolerance"], "Waste (%)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["Tolerance"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["Tolerance"].DisplayFormat.FormatString = "#,0.#####";

            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["PercentUsing"], "Using (%)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["PercentUsing"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["PercentUsing"].DisplayFormat.FormatString = "#,0.#####";


            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["Factor"], "Quantity", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["Factor"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["Factor"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatFactorDecimal(false, false);


            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["TotalQuantity"], "Total Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["TotalQuantity"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["TotalQuantity"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatFactorDecimal(false, false);


            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["PRQty_CNY002"], "Demand Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["PRQty_CNY002"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["PRQty_CNY002"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);
            //gvColor.Columns["PRQty_CNY002"].SummaryItem.SummaryType = GridSumCol.Sum;
            //gvColor.Columns["PRQty_CNY002"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";



            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["POQty_CNY003"], "Purchase Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["POQty_CNY003"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["POQty_CNY003"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);
            //gvColor.Columns["POQty_CNY003"].SummaryItem.SummaryType = GridSumCol.Sum;
            //gvColor.Columns["POQty_CNY003"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";


            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["StockQty_CNY010"], "Stock Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["StockQty_CNY010"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["StockQty_CNY010"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);


            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["PRQty_CNY002B"], "Demand Qty  (BoM)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["PRQty_CNY002B"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["PRQty_CNY002B"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);
            //gvColor.Columns["PRQty_CNY002"].SummaryItem.SummaryType = GridSumCol.Sum;
            //gvColor.Columns["PRQty_CNY002"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";



            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["POQty_CNY003B"], "Purchase Qty (BoM)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["POQty_CNY003B"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["POQty_CNY003B"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);
            //gvColor.Columns["POQty_CNY003"].SummaryItem.SummaryType = GridSumCol.Sum;
            //gvColor.Columns["POQty_CNY003"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";




            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["ProjectName"], "Project Name", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);


            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["ProductionOrder"], "Production Order", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["RootSOQty"], "Order Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvColor.Columns["RootSOQty"].DisplayFormat.FormatType = FormatType.Numeric;
            gvColor.Columns["RootSOQty"].DisplayFormat.FormatString = "N0";
            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["Position"], "Position", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["Reference"], "Reference", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);
            //gvColor.Columns["Reference"].SummaryItem.SummaryType = GridSumCol.Sum;
            //gvColor.Columns["Reference"].SummaryItem.DisplayFormat = @"Total:";

            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["AssemblyComponent"], "Ass - Comp", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);

            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["RMCode_001"], "Item Code", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);

            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["RMDescription_002"], "Item Name", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);







            //ProcessGeneral.SetGridColumnHeader(gvColor.Columns["ColorCode"], "Color", DefaultBoolean.False, HorzAlignment.Near,
            //    GridFixedCol.None);


            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["RmDimension"], "Dimension (RM)", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvColor.Columns["ProDimension"], "Dimension (PRO.)", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);
            gvColor.BestFitColumns();

            gvColor.EndUpdate();



        }
        private void GridViewColorCustomInit()
        {



            gcColor.UseEmbeddedNavigator = true;

            gcColor.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcColor.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcColor.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcColor.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcColor.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvColor.OptionsBehavior.Editable = true;
            gvColor.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvColor.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvColor.OptionsCustomization.AllowColumnMoving = false;
            gvColor.OptionsCustomization.AllowQuickHideColumns = true;

            gvColor.OptionsCustomization.AllowSort = false;

            gvColor.OptionsCustomization.AllowFilter = false;
            gvColor.OptionsView.BestFitMaxRowCount = 10;

            gvColor.OptionsView.ShowGroupPanel = false;
            gvColor.OptionsView.ShowIndicator = true;
            gvColor.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvColor.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvColor.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvColor.OptionsView.ShowAutoFilterRow = false;
            gvColor.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvColor.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvColor.OptionsNavigation.AutoFocusNewRow = true;
            gvColor.OptionsNavigation.UseTabKey = true;

            gvColor.OptionsSelection.MultiSelect = true;
            gvColor.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvColor.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvColor.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvColor.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvColor.OptionsView.EnableAppearanceEvenRow = false;
            gvColor.OptionsView.EnableAppearanceOddRow = false;
            gvColor.OptionsView.ShowFooter = false;
            gvColor.OptionsView.RowAutoHeight = true;
            gvColor.OptionsHint.ShowFooterHints = false;
            gvColor.OptionsHint.ShowCellHints = true;
            //   gridView1.RowHeight = 25;

            gvColor.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gvColor.OptionsFind.AllowFindPanel = false;


            gvColor.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);

            new MyFindPanelFilterHelper(gvColor)
            {
                AllowSort = false,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };




            gvColor.ShowingEditor += gvColor_ShowingEditor;
            gvColor.RowCountChanged += gvColor_RowCountChanged;
            gvColor.CustomDrawRowIndicator += gvColor_CustomDrawRowIndicator;



            gvColor.LeftCoordChanged += gvColor_LeftCoordChanged;
            gvColor.MouseMove += gvColor_MouseMove;
            gvColor.TopRowChanged += gvColor_TopRowChanged;
            gvColor.FocusedColumnChanged += GvColor_FocusedColumnChanged;
            gvColor.FocusedRowChanged += gvColor_FocusedRowChanged;
            gcColor.Paint += gcColor_Paint;

            gvColor.RowCellStyle += gvColor_RowCellStyle;


            gcColor.ForceInitialize();



        }
        private void gvColor_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
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











            // bool allowUpdate = ProcessGeneral.GetSafeBool(gv.GetRowCellValue(rH,"AllowUpdate"));
            string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "RowState"));
            switch (fieldName)
            {
                case "PRQty_CNY002":
                case "PRQty_CNY002B":
                    {


                        e.Appearance.ForeColor = Color.DarkOrange;
                    }
                    break;
                case "POQty_CNY003":
                case "POQty_CNY003B":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;
                case "RootSOQty":
                    {

                        e.Appearance.ForeColor = Color.DarkBlue;
                    }
                    break;
                case "PlanQuantity":
                    {

                        e.Appearance.ForeColor = Color.DarkGreen;
                    }
                    break;
                case "Position":
                    {
                        e.Appearance.Font = new Font("Tahoma", 8F, (FontStyle.Bold), GraphicsUnit.Point, 0);
                        e.Appearance.ForeColor = Color.DarkGreen;
                    }
                    break;

            }

            if (fieldName.IndexOf("-", StringComparison.Ordinal) > 0)
            {
                if (rowState == DataStatus.Insert.ToString() && ProcessGeneral.GetSafeString(e.CellValue) != "")
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.GreenYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
                else
                {
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LightGoldenrodYellow;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                }
            }
            else
            {





                switch (fieldName)
                {
                    case "Tolerance":
                        {
                            if (object.Equals(e.CellValue, gv.GetRowCellValue(rH, "ToleranceBOM")))
                            {
                                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                e.Appearance.BackColor = Color.WhiteSmoke;
                                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                            }
                            else
                            {
                                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                e.Appearance.BackColor = Color.LightSalmon;
                                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                            }

                        }
                        break;
                    case "UC":
                    {
                        if (Equals(e.CellValue, gv.GetRowCellValue(rH, "UC_BOM")))
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.WhiteSmoke;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        else
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.LightSalmon;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }

                    }
                        break;
                    case "TotalQuantity":
                    case "Factor":
                    case "PercentUsing":
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.WhiteSmoke;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        break;
                    case "RMCode_001":
                    case "RMDescription_002":
                    case "Reference":
                    case "StockQty_CNY010":
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        break;
                    case "ProjectName":
                    case "AssemblyComponent":
                    case "ProductionOrder":
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        break;
                    case "PRQty_CNY002":
                    case "PRQty_CNY002B":
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.Cornsilk;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        break;
                    case "POQty_CNY003":
                    case "POQty_CNY003B":
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LightPink;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        break;
                    case "PlanQuantity":
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.PaleGoldenrod;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        break;
                    case "RootSOQty":
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.Khaki;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        break;
                    case "Position":
                        {
                            e.Appearance.GradientMode = LinearGradientMode.Vertical;
                            e.Appearance.BackColor = Color.PaleGoldenrod;
                            e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                        }
                        break;
                    case "RmDimension":
                        {
                            if (rowState == DataStatus.Insert.ToString() && ProcessGeneral.GetSafeString(e.CellValue) != "")
                            {
                                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                e.Appearance.BackColor = Color.GreenYellow;
                                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                            }
                            else
                            {
                                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                e.Appearance.BackColor = Color.LightGoldenrodYellow;
                                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                            }
                        }
                        break;
                    case "ProDimension":
                        {
                            if (rowState == DataStatus.Insert.ToString() && ProcessGeneral.GetSafeString(e.CellValue) != "")
                            {
                                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                e.Appearance.BackColor = Color.GreenYellow;
                                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                            }
                            else
                            {
                                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                                e.Appearance.BackColor = Color.LightYellow;
                                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                            }
                        }
                        break;

                }

            }








        }
        private void GvColor_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        #region "GridView Event"




        private void gvColor_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }



        private void gvColor_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvColor_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvColor_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }


        private void gvColor_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }

        private void gcColor_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvColor_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvColor_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

            LinearGradientBrush backBrush;
            string rowState = ProcessGeneral.GetSafeString(gv.GetRowCellValue(e.RowHandle, "RowState"));
            bool allowUpdate = ProcessGeneral.GetSafeBool(gv.GetRowCellValue(e.RowHandle, "AllowUpdate"));







            if (!allowUpdate)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Yellow, Color.Azure, 90);
            }
            else
            {
                if (rowState == DataStatus.Insert.ToString())
                {
                    backBrush = new LinearGradientBrush(e.Bounds, Color.GreenYellow, Color.Azure, 90);
                }
                else
                {
                    if (rowState == DataStatus.Update.ToString())
                    {
                        backBrush = new LinearGradientBrush(e.Bounds, Color.Aquamarine, Color.Azure, 90);
                    }
                    else
                    {
                        backBrush = new LinearGradientBrush(e.Bounds, Color.Silver, Color.Azure, 90);
                    }
                }
            }


            e.Graphics.FillRectangle(backBrush, e.Bounds);
            ControlPaint.DrawBorder3D(e.Graphics, e.Bounds, Border3DStyle.RaisedInner);

            if (gv.IsRowSelected(e.RowHandle))
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

        private void TreeListMain_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;


        }
















        #endregion


        #region "Process Grid Detail"


        private void LoadDataGridViewDetailFrist(DataTable dtColor)
        {
            DataTable dt;
            if (dtColor == null || dtColor.Rows.Count <= 0)
            {
                dt = Com_StockDocumentRequest.TableGridMChildTemplate();


            }
            else
            {
                var q1 = dtColor.AsEnumerable().GroupBy(p => new
                {
                    Reference = p.Field<string>("Reference"),
                    PlanQuantity = p.Field<Int32>("PlanQuantity"),
                    RMCode_001 = p.Field<string>("RMCode_001"),
                    RMDescription_002 = p.Field<string>("RMDescription_002"),
                    ProDimension = p.Field<string>("ProDimension"),
                    //  UC = p.Field<decimal>("UC"),
                    Tolerance = p.Field<decimal>("Tolerance"),
                    PercentUsing = p.Field<decimal>("PercentUsing"),
                    ProjectName = p.Field<string>("ProjectName"),
                    ProductionOrder = p.Field<string>("ProductionOrder"),
                    RootSOQty = p.Field<Int32>("RootSOQty"),
                    CNY00020PK = p.Field<Int64>("CNY00020PK"),

                }).Select(t => new
                {
                    t.Key.Reference,
                    PRQty_CNY002 = t.Sum(s => s.Field<decimal>("PRQty_CNY002")),
                    POQty_CNY003 = t.Sum(s => s.Field<decimal>("POQty_CNY003")),
                    PRQty_CNY002B = t.Sum(s => s.Field<decimal>("PRQty_CNY002B")),
                    POQty_CNY003B = t.Sum(s => s.Field<decimal>("POQty_CNY003B")),
                    t.Key.PlanQuantity,
                    // t.Key.AssemblyComponent,
                    t.Key.RMCode_001,
                    t.Key.RMDescription_002,
                    t.Key.ProDimension,
                    //  Count = t.Count(),
                    UC = t.Sum(s => s.Field<decimal>("UC")),
                    t.Key.Tolerance,
                    t.Key.PercentUsing,
                    t.Key.ProjectName,
                    t.Key.ProductionOrder,
                    t.Key.RootSOQty,
                    t.Key.CNY00020PK,
                }).ToList();

                dt = q1.CopyToDataTableNew();

            }




            gvDetail.BeginUpdate();





            gcDetail.DataSource = null;
            gvDetail.Columns.Clear();

            gcDetail.DataSource = dt;

            Dictionary<string, bool> dicCol = new Dictionary<string, bool>
            {
                {"Reference", true},
                {"PRQty_CNY002", true},
                {"POQty_CNY003", true},
                { "PRQty_CNY002B", false},
                {"POQty_CNY003B", false},
                {"PlanQuantity", true},
                {"UC", true},
                {"RMCode_001", false},
                {"RMDescription_002", true},
                {"ProductionOrder", true},
                {"ProjectName", true},
                {"ProDimension", true},
                {"Tolerance", true},
                {"PercentUsing", true},

                {"RootSOQty", false},
                {"CNY00020PK", false},
            };
            gvDetail.VisibleAndSortGridColumn(dicCol);






            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["PlanQuantity"], "Pro. Order Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvDetail.Columns["PlanQuantity"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gvDetail.Columns["PlanQuantity"].DisplayFormat.FormatType = FormatType.Numeric;
            gvDetail.Columns["PlanQuantity"].DisplayFormat.FormatString = "N0";
            gvDetail.Columns["PlanQuantity"].ImageIndex = 0;
            gvDetail.Columns["PlanQuantity"].ImageAlignment = StringAlignment.Near;
            //gvMain.Columns["PlanQuantity"].SummaryItem.SummaryType = GridSumCol.Sum;
            //gvMain.Columns["PlanQuantity"].SummaryItem.DisplayFormat = @"{0:N0}";



            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["UC"], "Amount", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvDetail.Columns["UC"].DisplayFormat.FormatType = FormatType.Numeric;
            gvDetail.Columns["UC"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatUcDecimal(false, false);

            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["Tolerance"], "Waste (%)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvDetail.Columns["Tolerance"].DisplayFormat.FormatType = FormatType.Numeric;
            gvDetail.Columns["Tolerance"].DisplayFormat.FormatString = "#,0.#####";

            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["PercentUsing"], "Using (%)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvDetail.Columns["PercentUsing"].DisplayFormat.FormatType = FormatType.Numeric;
            gvDetail.Columns["PercentUsing"].DisplayFormat.FormatString = "#,0.#####";







            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["PRQty_CNY002"], "Demand Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvDetail.Columns["PRQty_CNY002"].DisplayFormat.FormatType = FormatType.Numeric;
            gvDetail.Columns["PRQty_CNY002"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);
            //gvMain.Columns["PRQty_CNY002"].SummaryItem.SummaryType = GridSumCol.Sum;
            //gvMain.Columns["PRQty_CNY002"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";



            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["POQty_CNY003"], "Purchase Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvDetail.Columns["POQty_CNY003"].DisplayFormat.FormatType = FormatType.Numeric;
            gvDetail.Columns["POQty_CNY003"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);
            //gvMain.Columns["POQty_CNY003"].SummaryItem.SummaryType = GridSumCol.Sum;
            //gvMain.Columns["POQty_CNY003"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";

            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["PRQty_CNY002B"], "Demand Qty (BoM)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvDetail.Columns["PRQty_CNY002B"].DisplayFormat.FormatType = FormatType.Numeric;
            gvDetail.Columns["PRQty_CNY002B"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPrQtyDecimal(false, false);
            //gvMain.Columns["PRQty_CNY002"].SummaryItem.SummaryType = GridSumCol.Sum;
            //gvMain.Columns["PRQty_CNY002"].SummaryItem.DisplayFormat = @"{0:#,0.#####}";



            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["POQty_CNY003B"], "Purchase Qty (BoM)", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvDetail.Columns["POQty_CNY003B"].DisplayFormat.FormatType = FormatType.Numeric;
            gvDetail.Columns["POQty_CNY003B"].DisplayFormat.FormatString = FunctionFormatModule.StrFormatPoQtyDecimal(false, false);




            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["ProDimension"], "Dimension (PRO.)", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);


            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["ProjectName"], "Project Name", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);


            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["ProductionOrder"], "Production Order", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["RootSOQty"], "Order Qty", DefaultBoolean.Default, HorzAlignment.Center, GridFixedCol.None);
            gvDetail.Columns["RootSOQty"].DisplayFormat.FormatType = FormatType.Numeric;
            gvDetail.Columns["RootSOQty"].DisplayFormat.FormatString = "N0";

            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["Reference"], "Reference", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);
            //gvMain.Columns["Reference"].SummaryItem.SummaryType = GridSumCol.Sum;
            //gvMain.Columns["Reference"].SummaryItem.DisplayFormat = @"Total:";


            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["RMCode_001"], "Item Code", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);

            ProcessGeneral.SetGridColumnHeader(gvDetail.Columns["RMDescription_002"], "Item Name", DefaultBoolean.Default, HorzAlignment.Near, GridFixedCol.None);











            //ProcessGeneral.SetGridColumnHeader(gvMain.Columns["ColorCode"], "Color", DefaultBoolean.False, HorzAlignment.Near,
            //    GridFixedCol.None);


            //var qColAtt = gvMain.Columns.Where(p => p.FieldName.IndexOf("-", StringComparison.Ordinal) > 0).ToList();

            //foreach (GridColumn gColD in qColAtt)
            //{

            //    ProcessGeneral.SetGridColumnHeader(gColD, gColD.Caption, DefaultBoolean.False, HorzAlignment.Near,
            //        GridFixedCol.None);
            //    gColD.ImageIndex = 0;
            //    gColD.ImageAlignment = StringAlignment.Near;
            //}

            gvDetail.BestFitColumns();

            gvDetail.EndUpdate();


        }



        private void GridViewDetailCustomInit()
        {



            gcDetail.UseEmbeddedNavigator = true;

            gcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvDetail.OptionsBehavior.Editable = true;
            gvDetail.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvDetail.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvDetail.OptionsCustomization.AllowColumnMoving = false;
            gvDetail.OptionsCustomization.AllowQuickHideColumns = true;

            gvDetail.OptionsCustomization.AllowSort = false;

            gvDetail.OptionsCustomization.AllowFilter = false;


            gvDetail.OptionsView.ShowGroupPanel = false;
            gvDetail.OptionsView.ShowIndicator = true;
            gvDetail.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvDetail.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvDetail.OptionsView.ShowAutoFilterRow = false;
            gvDetail.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvDetail.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvDetail.OptionsNavigation.AutoFocusNewRow = true;
            gvDetail.OptionsNavigation.UseTabKey = true;

            gvDetail.OptionsSelection.MultiSelect = true;
            gvDetail.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvDetail.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvDetail.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvDetail.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvDetail.OptionsView.EnableAppearanceEvenRow = false;
            gvDetail.OptionsView.EnableAppearanceOddRow = false;
            gvDetail.OptionsView.ShowFooter = false;
            gvDetail.OptionsView.RowAutoHeight = true;
            gvDetail.OptionsHint.ShowFooterHints = false;
            gvDetail.OptionsHint.ShowCellHints = true;
            //   gridView1.RowHeight = 25;

            gvDetail.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gvDetail.OptionsFind.AllowFindPanel = false;

            gvDetail.OptionsView.BestFitMaxRowCount = 10;
            gvDetail.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);

            new MyFindPanelFilterHelper(gvDetail)
            {
                AllowSort = false,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };




            gvDetail.ShowingEditor += gvDetail_ShowingEditor;
            gvDetail.RowCountChanged += gvDetail_RowCountChanged;
            gvDetail.CustomDrawRowIndicator += gvDetail_CustomDrawRowIndicator;

            gvDetail.RowCellStyle += gvDetail_RowCellStyle;

            gvDetail.LeftCoordChanged += gvDetail_LeftCoordChanged;
            gvDetail.MouseMove += gvDetail_MouseMove;
            gvDetail.TopRowChanged += gvDetail_TopRowChanged;
            gvDetail.FocusedColumnChanged += gvDetail_FocusedColumnChanged;
            gvDetail.FocusedRowChanged += gvDetail_FocusedRowChanged;
            gcDetail.Paint += gcDetail_Paint;





            gcDetail.ForceInitialize();



        }

        #region "GridView Event"




        private void gvDetail_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

        }


        private void gvDetail_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            int rH = e.RowHandle;
            if (rH < 0) return;
            GridColumn gCol = e.Column;
            if (gCol == null) return;

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












            switch (fieldName)
            {
                case "PRQty_CNY002":
                case "PRQty_CNY002B":
                    {


                        e.Appearance.ForeColor = Color.DarkOrange;
                    }
                    break;
                case "POQty_CNY003":
                case "POQty_CNY003B":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;
                case "RootSOQty":
                    {

                        e.Appearance.ForeColor = Color.DarkBlue;
                    }
                    break;
                case "PlanQuantity":
                    {

                        e.Appearance.ForeColor = Color.DarkGreen;
                    }
                    break;

            }

            switch (fieldName)
            {

                case "Count":
                case "UC":
                case "Tolerance":
                case "PercentUsing":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.WhiteSmoke;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "RMCode_001":
                case "RMDescription_002":
                case "Reference":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "ProjectName":
                case "AssemblyComponent":
                case "ProductionOrder":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = SystemCellColor.BackColorReadonly;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "PRQty_CNY002":
                case "PRQty_CNY002B":
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.Cornsilk;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    break;
                case "POQty_CNY003":
                case "POQty_CNY003B":
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.LightPink;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    break;
                case "PlanQuantity":
                    e.Appearance.GradientMode = LinearGradientMode.Vertical;
                    e.Appearance.BackColor = Color.PaleGoldenrod;
                    e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    break;
                case "RootSOQty":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.Khaki;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;
                case "ProDimension":
                    {
                        e.Appearance.GradientMode = LinearGradientMode.Vertical;
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
                    }
                    break;

            }











        }

        private void gvDetail_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDetail_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDetail_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvDetail_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }
        private void gvDetail_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }

        private void gcDetail_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvDetail_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvDetail_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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

            if (gv.IsRowSelected(e.RowHandle))
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

        #endregion






        #region "Process Grid Production"




        private void GridViewProductionCustomInit()
        {



            gcProduction.UseEmbeddedNavigator = true;

            gcProduction.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcProduction.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcProduction.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcProduction.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcProduction.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvProduction.OptionsBehavior.Editable = true;
            gvProduction.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvProduction.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvProduction.OptionsCustomization.AllowColumnMoving = false;
            gvProduction.OptionsCustomization.AllowQuickHideColumns = true;

            gvProduction.OptionsCustomization.AllowSort = false;

            gvProduction.OptionsCustomization.AllowFilter = false;


            gvProduction.OptionsView.ShowGroupPanel = false;
            gvProduction.OptionsView.ShowIndicator = true;
            gvProduction.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvProduction.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvProduction.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvProduction.OptionsView.ShowAutoFilterRow = false;
            gvProduction.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvProduction.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvProduction.OptionsNavigation.AutoFocusNewRow = true;
            gvProduction.OptionsNavigation.UseTabKey = true;

            gvProduction.OptionsSelection.MultiSelect = true;
            gvProduction.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvProduction.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvProduction.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvProduction.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvProduction.OptionsView.EnableAppearanceEvenRow = false;
            gvProduction.OptionsView.EnableAppearanceOddRow = false;
            gvProduction.OptionsView.ShowFooter = true;

            gvProduction.OptionsHint.ShowFooterHints = false;
            gvProduction.OptionsHint.ShowCellHints = false;
            //   gridView1.RowHeight = 25;

            gvProduction.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gvProduction.OptionsFind.AllowFindPanel = false;


            gvProduction.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);

            new MyFindPanelFilterHelper(gvProduction)
            {
                AllowSort = false,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };






            gvProduction.ShowingEditor += gvProduction_ShowingEditor;

            gvProduction.CustomRowCellEdit += gvProduction_CustomRowCellEdit;
            gvProduction.LeftCoordChanged += gvProduction_LeftCoordChanged;
            gvProduction.MouseMove += gvProduction_MouseMove;
            gvProduction.TopRowChanged += gvProduction_TopRowChanged;
            gvProduction.FocusedColumnChanged += gvProduction_FocusedColumnChanged;
            gvProduction.FocusedRowChanged += gvProduction_FocusedRowChanged;
            gcProduction.Paint += gcProduction_Paint;

            gvProduction.RowCountChanged += gvProduction_RowCountChanged;
            gvProduction.CustomDrawRowIndicator += gvProduction_CustomDrawRowIndicator;

            gvProduction.RowCellStyle += gvProduction_RowCellStyle;

            gvProduction.CellValueChanged += GvProduction_CellValueChanged;

            gvProduction.ShownEditor += GvProduction_ShownEditor;
            gcProduction.ForceInitialize();



        }

        private void GvProduction_ShownEditor(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

         
            GridColumn col = gv.FocusedColumn;
            if (col == null) return;
            string fieldName = col.FieldName;
            switch (fieldName)
            {
                case "PlanQty":
                    var editor = gv.ActiveEditor as SpinEdit;
                    if (editor != null)
                    {
                        editor.Properties.MinValue = 0;
                        editor.Properties.MaxValue = ProcessGeneral.GetSafeInt(gv.GetRowCellValue(gv.FocusedRowHandle, "LeftPlanQty"));
                    }

                    break;
            }
        }

        private void gvProduction_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;

            string fieldName = e.Column.FieldName;
            switch (fieldName)
            {
                case "PlanQty":
                    e.RepositoryItem = _repositorySpinN0;
                    break;

            }

        }


        private void gvProduction_ShowingEditor(object sender, CancelEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GridColumn gCol = gv.FocusedColumn;
            if (gCol == null) return;
            string fieldName = gCol.FieldName;
            switch (fieldName)
            {
                case "PlanQty":
                {
                    TreeListNode node = tlItemCode.FocusedNode;

                    if (node != null && node.Checked)
                        e.Cancel = false;
                    else
                    {
                        e.Cancel = true;
                    }
                }
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }


        private void gvProduction_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var gv = sender as GridView;
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

            
            switch (fieldName)
            {
                case "LeftPlanQty":
                case "SOQty":
                    {


                        e.Appearance.ForeColor = Color.DarkOrange;
                    }
                    break;
                case "PlanQty":
                    {

                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;
                case "BalanceQty":
                    {

                        e.Appearance.ForeColor = Color.DarkGreen;
                    }
                    break;

            }
            if (fieldName == "ProjectNo" || fieldName == "ProjectName"|| fieldName == "ProductionOrder" )
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.LavenderBlush;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;

            }
            else if (fieldName == "LeftPlanQty" || fieldName == "SOQty")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.GhostWhite;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "PlanQty")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.Lavender;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else if (fieldName == "BalanceQty")
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = Color.WhiteSmoke;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2Readonly;
            }
            else
            {
                e.Appearance.GradientMode = LinearGradientMode.Vertical;
                e.Appearance.BackColor = SystemCellColor.BackColorShowEditor;
                e.Appearance.BackColor2 = SystemCellColor.BackColor2ShowEditor;
            }





        }

        private void gvProduction_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvProduction_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvProduction_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvProduction_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));

        }


        private void gcProduction_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }






        private void gvProduction_RowCountChanged(object sender, EventArgs e)
        {
            var gvP = sender as GridView;
            if (gvP == null) return;
            //  if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gvP.GridControl.Handle);
            SizeF size = gr.MeasureString((gvP.RowCount + 1).ToString(), gvP.PaintAppearance.Row.GetFont());
            gvP.IndicatorWidth = Convert.ToInt32(size.Width) + 10;

            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvProduction_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
          
           

            if (gv.IsRowSelected(e.RowHandle))
            {
                Rectangle rect = e.Bounds;
                Brush brush = new LinearGradientBrush(rect, Color.GreenYellow, Color.Azure, 90);
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
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






        private void GvProduction_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;
            switch (e.Column.FieldName)
            {
                case "PlanQty":
                {
                    int rH = e.RowHandle;
                    int planQty = ProcessGeneral.GetSafeInt(e.Value);
                    int balanceQty = ProcessGeneral.GetSafeInt(gv.GetRowCellValue(rH, "LeftPlanQty")) - planQty;
                    gv.SetRowCellValue(rH, "BalanceQty", balanceQty);
                    Dictionary<Int64, Int64> dicRow = ProcessGeneral.GetSafeString(gv.GetRowCellValue(rH, "RowIndex"))
                        .Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(ProcessGeneral.GetSafeInt64).ToDictionary(p => p, p => p);

                    var q1 = _dtFinalBoM.AsEnumerable().Where(p => dicRow.ContainsKey(p.Field<Int64>("RowIndex"))).ToList();
                    foreach (DataRow dr in q1)
                    {
                        dr["PlanQty"] = planQty;
                    }
                    _dtFinalBoM.AcceptChanges();

                }
                    break;
            }
        }
      


        private void gvProduction_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(gv);



        }
      
        

        private void LoadDataGridViewProductionFrist(TreeListNode node)
        {
            string strRowIndex = "";
            if (node != null && !node.HasChildren)
            {
                strRowIndex = ProcessGeneral.GetSafeString(node.GetValue("StringRowIndex"));
            }

            Dictionary<Int64, Int64> dicIndex = strRowIndex.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(ProcessGeneral.GetSafeInt64).ToDictionary(p => p, p => p);

            DataTable dt = null;


            if (dicIndex.Count > 0)
            {
                var q1 = _dtFinalBoM.AsEnumerable().Where(p => dicIndex.ContainsKey(p.Field<Int64>("RowIndex"))).Select(p => new
                {
                    CNY00020PK = p.Field<Int64>("CNY00020PK"),
                    PlanQty = p.Field<int>("PlanQty"),
                    SOQty = p.Field<int>("SOQty"),
                    BalanceQty = p.Field<int>("LeftPlanQty"),
                    LeftPlanQty = p.Field<int>("LeftPlanQty")+ p.Field<int>("PlanQty"),
                    RowIndex = p.Field<Int64>("RowIndex"),
                }).ToList();

                DataTable dtSo = (DataTable) tlSO.DataSource;

                dt = q1.Join(dtSo.AsEnumerable(),p=>new
                {
                    p.CNY00020PK
                },t=>new
                {
                    CNY00020PK = t.Field<Int64>("CNY00020PK"),
                },(p,t)=>new
                {
                    p.PlanQty,
                    p.BalanceQty,
                    p.LeftPlanQty,
                    p.SOQty,
                    Reference = t.Field<string>("Reference"),
                    ProjectNo = t.Field<string>("ProjectNo"),
                    ProjectName = t.Field<string>("ProjectName"),
                    ProductionOrder = t.Field<string>("ProductionOrder"),
                    FinishingColor = t.Field<string>("FinishingColor"),
                    ProDimension = t.Field<string>("ProDimension"),
                    p.CNY00020PK,
                    p.RowIndex,
                }).GroupBy(p=>new{
                    p.PlanQty,
                    p.BalanceQty,
                    p.LeftPlanQty,
                    p.SOQty,
                    p.Reference,
                    p.ProjectNo,
                    p.ProjectName,
                    p.ProductionOrder,
                    p.FinishingColor,
                    p.ProDimension,
                    p.CNY00020PK
                }).Select(t=>new{
                    t.Key.PlanQty,
                    t.Key.BalanceQty,
                    t.Key.LeftPlanQty,
                    t.Key.SOQty,
                    t.Key.Reference,
                    t.Key.ProjectNo,
                    t.Key.ProjectName,
                    t.Key.ProductionOrder,
                    t.Key.FinishingColor,
                    t.Key.ProDimension,
                    t.Key.CNY00020PK,
                    RowIndex = string.Join(",", t.Select(s=>s.RowIndex.ToString()).ToArray())
                }).CopyToDataTableNew();

                

            }






            gvProduction.BeginUpdate();



            gcProduction.DataSource = null;
            gvProduction.Columns.Clear();

            gcProduction.DataSource = dt;

            if (dt != null)
            {
                ProcessGeneral.HideVisibleColumnsGridView(gvProduction, false, "RowIndex", "CNY00020PK");


                ProcessGeneral.SetGridColumnHeader(gvProduction.Columns["PlanQty"], "Plan Qty", DefaultBoolean.False, HorzAlignment.Center, GridFixedCol.None);
                gvProduction.Columns["PlanQty"].DisplayFormat.FormatType = FormatType.Numeric;
                gvProduction.Columns["PlanQty"].DisplayFormat.FormatString = "N0";


                ProcessGeneral.SetGridColumnHeader(gvProduction.Columns["BalanceQty"], "Balance Qty", DefaultBoolean.False, HorzAlignment.Center, GridFixedCol.None);
                gvProduction.Columns["BalanceQty"].DisplayFormat.FormatType = FormatType.Numeric;
                gvProduction.Columns["BalanceQty"].DisplayFormat.FormatString = "N0";

                ProcessGeneral.SetGridColumnHeader(gvProduction.Columns["LeftPlanQty"], "Allow Qty", DefaultBoolean.False, HorzAlignment.Center, GridFixedCol.None);
                gvProduction.Columns["LeftPlanQty"].DisplayFormat.FormatType = FormatType.Numeric;
                gvProduction.Columns["LeftPlanQty"].DisplayFormat.FormatString = "N0";

                ProcessGeneral.SetGridColumnHeader(gvProduction.Columns["SOQty"], "SO Qty", DefaultBoolean.False, HorzAlignment.Center, GridFixedCol.None);
                gvProduction.Columns["SOQty"].DisplayFormat.FormatType = FormatType.Numeric;
                gvProduction.Columns["SOQty"].DisplayFormat.FormatString = "N0";

                ProcessGeneral.SetGridColumnHeader(gvProduction.Columns["Reference"], "Reference", DefaultBoolean.False, HorzAlignment.Near,GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gvProduction.Columns["ProjectNo"], "Project No.", DefaultBoolean.False, HorzAlignment.Near,GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gvProduction.Columns["ProjectName"], "Project Name", DefaultBoolean.False, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gvProduction.Columns["ProductionOrder"], "Production Order No.", DefaultBoolean.False, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gvProduction.Columns["FinishingColor"], "Color", DefaultBoolean.False, HorzAlignment.Near, GridFixedCol.None);
                ProcessGeneral.SetGridColumnHeader(gvProduction.Columns["ProDimension"], "Dimension", DefaultBoolean.False, HorzAlignment.Near, GridFixedCol.None);


            }
           



            gvProduction.BestFitColumns();
            gvProduction.EndUpdate();

        }




        #endregion








    }


}

