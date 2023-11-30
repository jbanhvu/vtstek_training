using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.Utils.Win;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ColorWheel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Scrolling;
using CNY_Main.Properties;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.Demo;
using CNY_BaseSys.WForm;
using CNY_Property;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Utils.Paint;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Nodes;
using Settings = CNY_Main.Properties.Settings;
using SortOrder = System.Windows.Forms.SortOrder;
using TreeFixedStyle = DevExpress.XtraTreeList.Columns.FixedStyle;
using System.Windows.Threading;
using CNY_BaseSys.Class;
using DevExpress.XtraPrinting.Preview;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management;
using Microsoft.SqlServer.Management.Common;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using Quobject.SocketIoClientDotNet.Client;
using System.Threading;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using Newtonsoft.Json;

namespace CNY_Main
{
    public partial class FrmMain : RibbonForm
    {

        #region "Property And Field"
        private bool _performEventCombobox = true;
        private bool minimizedToTray = false;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip TrayIconContextMenu;
        private ToolStripMenuItem CloseMenuItem;
        readonly Ctr_Main _ctrl = new Ctr_Main();
        private WaitDialogForm _dlg = null;
        private bool _isConfigSql = false;
        private bool _isLogout = false;
        private DateTime _currentTimeLogin;
        private int _pProc = 0;

        private string _lastCriteria;
        private ExpressionEvaluator _lastEvaluator;
        private bool _isLoad = false;
        private readonly RepositoryItemMemoEdit _repositoryTextGrid;
        private readonly List<TreeListNode> _lNodeDelete = new List<TreeListNode>();
        AccessData ac = new AccessData(DeclareSystem.SysConnectionString);
        BackgroundWorker bw = new BackgroundWorker();
        DataTable dt = new DataTable();
        bool CheckDB = true;
        DataTable dt3 = new DataTable();
        Socket io;
        bool checkNewNoti = false;
        
        #endregion

        #region "Contructor"

        public FrmMain()
        {
            

            InitializeComponent();
            
            this.FormClosing += FrmMain_FormClosing;

            string testHeader = "";
            if (SystemProperty.SysWorkingServer == "TEST")
            {
                bStaticI_Machine.Appearance.BackColor = Color.Red;
                bsiRAM.Appearance.BackColor = Color.Red;
                bbItemNum.Appearance.BackColor = Color.Red;
                bbItemCAP.Appearance.BackColor = Color.Red;

                barStaticI_User.Appearance.BackColor = Color.Red;
                barStaticI_Date.Appearance.BackColor = Color.Red;
                barStaticI_Time.Appearance.BackColor = Color.Red;
                barStaticI_GridAgg.Appearance.BackColor = Color.Red;
                bbiTL.Appearance.BackColor = Color.Red;
                bbiTR.Appearance.BackColor = Color.Red;
                testHeader = " ::::: (TEST)";

            }
            else if (SystemProperty.SysWorkingServer == "TRIAL")
            {
                bStaticI_Machine.Appearance.BackColor = Color.Yellow;
                bsiRAM.Appearance.BackColor = Color.Yellow;
                bbItemNum.Appearance.BackColor = Color.Yellow;
                bbItemCAP.Appearance.BackColor = Color.Yellow;

                barStaticI_User.Appearance.BackColor = Color.Yellow;
                barStaticI_Date.Appearance.BackColor = Color.Yellow;
                barStaticI_Time.Appearance.BackColor = Color.Yellow;
                barStaticI_GridAgg.Appearance.BackColor = Color.Yellow;
                bbiTL.Appearance.BackColor = Color.Yellow;
                bbiTR.Appearance.BackColor = Color.Yellow;
                testHeader = " ::::: (TRIAL)";

            }
            else
            {
                bbiTL.Visibility =BarItemVisibility.Never;
                bbiTR.Visibility = BarItemVisibility.Never;
            }


            _repositoryTextGrid = new RepositoryItemMemoEdit { WordWrap = true, AutoHeight = false };
            _isLoad = true;

            spinSum.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            spinSum.Properties.DisplayFormat.FormatString = @"#,0.###############";

            spinAVG.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            spinAVG.Properties.DisplayFormat.FormatString = @"#,0.###############";

            spinCount.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            spinCount.Properties.DisplayFormat.FormatString = @"#,0.###############";

            spinSum.Visible = false;
            spinAVG.Visible = false;
            spinCount.Visible = false;
            this.Text = string.Format("::::: [{0}] ::::: Version [{1}]"+ " ::::: DB-Server [{2}] ::::: Finance Year [{3}] ::::: Company [{4}] :::::",
                this.Text.Trim(), SystemProperty.SysVersion, SystemProperty.SysServerSql, SystemProperty.SysFinanceYear, DeclareSystem.SysCompanyCode);

            //this.Text = string.Format("::::: [{0}] ::::: Version [{1}] ::::: DB-Server [{2}] ::::: Finance Year [{3}] :::::",
            // this.Text.Trim(), SystemProperty.SysVersion, SystemProperty.SysServerSql, SystemProperty.SysFinanceYear);


            SkinHelper.InitSkinGallery(rgbMain, true);




            spinPaging.Properties.MinValue = 10;
            spinPaging.Properties.MaxValue = decimal.MaxValue;
            spinPaging.Properties.AllowMouseWheel = false;
            spinPaging.Properties.Mask.MaskType = MaskType.Numeric;
            spinPaging.Properties.Mask.EditMask = @"N0";
            spinPaging.Properties.Mask.UseMaskAsDisplayFormat = true;
            spinPaging.EditValue = Settings.Default.ItemPerPage; ;
            SystemProperty.SysItemsPaging = Settings.Default.ItemPerPage;
            //  ProcessGeneral.ConvertFileSizeToString(GetRAMCounter(myProcess));
            InitTreeView(treeList1);
            notifyIcon = new NotifyIcon();
            notifyIcon.DoubleClick += NotifyIconClick;
            notifyIcon.Icon = this.Icon;
            notifyIcon.Text = @"CNY";
            notifyIcon.BalloonTipText = @"CNY Is Minimized Windows.";
            notifyIcon.BalloonTipTitle = @"CNY Notify";
            TrayIconContextMenu = new ContextMenuStrip();
            CloseMenuItem = new ToolStripMenuItem();
            TrayIconContextMenu.SuspendLayout();
            this.TrayIconContextMenu.Items.AddRange(new ToolStripItem[] {
            this.CloseMenuItem});
            this.TrayIconContextMenu.Name = "TrayIconContextMenu";
            this.TrayIconContextMenu.Size = new Size(101, 70);
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new Size(100, 22);
            this.CloseMenuItem.Text = @"Exit program";
            this.CloseMenuItem.Click += CloseMenuItem_Click;
            btnSavePaging.Click += btnSavePaging_Click;
            TrayIconContextMenu.ResumeLayout(false);
            notifyIcon.ContextMenuStrip = TrayIconContextMenu;

            printDropDownMenu.BeforePopup += PrintDropDownMenu_BeforePopup;
            printDropDownMenu.CloseUp += PrintDropDownMenu_CloseUp;
            bbItemCAP.ItemDoubleClick += bbItemCAP_ItemDoubleClick;
            bbItemNum.ItemDoubleClick += bbItemNum_ItemDoubleClick;
            LoadSumOnLableStatusBar(0, 0, 0);

            tmmMain.SelectedPageChanged += tmmMain_SelectedPageChanged;
            gvNotify.RowCellClick += GvNotify_RowCellClick;


            foreach (RibbonPageGroup currentGroup in rpgMainCommand.Groups)
            {
                foreach (BarItemLink currenLink in currentGroup.ItemLinks)
                {
                    BarButtonItem barButtonItem = currenLink.Item as BarButtonItem;
                    if (barButtonItem != null)
                    {
                        barButtonItem.ItemClick += barButtonItemCommand_ItemClick;
                    }
                    else
                    {
                        BarSubItem barSubItem = currenLink.Item as BarSubItem;
                        if (barSubItem != null)
                        {
                            foreach (LinkPersistInfo subItem in barSubItem.LinksPersistInfo)
                            {
                                BarButtonItem barButtonSub = subItem.Item as BarButtonItem;
                                if (barButtonSub != null)
                                {
                                    barButtonSub.ItemClick += barButtonItemCommand_ItemClick;
                                }
                            }

                            barSubItem.Visibility = BarItemVisibility.Never;

                        }
                    }
                }
            }
            bbiFN_1.Visibility = BarItemVisibility.Never;
            bbiFN_2.Visibility = BarItemVisibility.Never;
            bbiFN_3.Visibility = BarItemVisibility.Never;
            bbiFN_4.Visibility = BarItemVisibility.Never;
            bbiFN_5.Visibility = BarItemVisibility.Never;
            rpgConfig.Visible = DeclareSystem.SysUserName.ToUpper().Trim() == "ADMIN";
            rpgDemo.Visible = DeclareSystem.SysUserName.ToUpper().Trim() == "ADMIN";
            ribbon.ShowCustomizationMenu += ribbon_ShowCustomizationMenu;

            btnConfirmEmailPass.Click += btnConfirmEmailPass_Click;
            txtSearchMain.EditValueChanged += TxtSearchMain_EditValueChanged;
            dockPanel1.SizeChanged += DockPanel1_SizeChanged;
            dockManagerMain.StartDocking += DockManagerMain_StartDocking;
            GridViewWBOMCustomInit(TableBOMDetailTemp());

            btnGetItemCode.Click += BtnGetItemCode_Click;


            DeclareSystem.GridControlBomAssignInfo = gcNMTBOM;
            DeclareSystem.GridViewBomAssignInfo = gvNMTBOM;

            dockNotify.Collapsed += DockNotify_Collapsed;
            gvNotify.ShowingEditor += GvNotify_ShowingEditor;

            btnNotify2.ItemClick += btnNotify_ItemClick;
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;

            //---- Listen Notification


            //--------------------------------------------------------------
            btnNotify2.Visibility = BarItemVisibility.Never;


        }

      
        private void PrintDropDownMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            XtraMdiTabPage selectedPage = tmmMain.SelectedPage;
            if (selectedPage == null) return;
            var mdiForm = selectedPage.MdiChild;
            var fBase = mdiForm as FrmBase;
            if (fBase == null) return;
            List<PopupMenuRibbonItemInfo> listButtonPrintDropDown = fBase.ListButtonPrintDropDown;
            if (listButtonPrintDropDown == null || listButtonPrintDropDown.Count <= 0)
            {
                e.Cancel = true;
                return;
            }
            printDropDownMenu.ItemLinks.Clear();
            foreach (PopupMenuRibbonItemInfo itemPrint in listButtonPrintDropDown)
            {
                BarButtonItem itemPrintDropDownItem = new BarButtonItem
                {
                    Name = itemPrint.ItemName, Caption = itemPrint.ItemCaption
                };
                if (itemPrint.ItemImage != null)
                {
                    itemPrintDropDownItem.ImageOptions.Image = itemPrint.ItemImage;
                }
                itemPrintDropDownItem.ItemClick += ItemPrintDropDownItem_ItemClick;
                printDropDownMenu.ItemLinks.Add(itemPrintDropDownItem).BeginGroup= itemPrint.BeginGroup;
            }


        }
        private void PrintDropDownMenu_CloseUp(object sender, EventArgs e)
        {
            if (printDropDownMenu.ItemLinks.Count <= 0) return;
            foreach (BarButtonItemLink itemPrint in printDropDownMenu.ItemLinks)
            {
                BarButtonItem itemPrintDropDownItem = itemPrint.Item;
                if(itemPrintDropDownItem == null) continue;
                itemPrintDropDownItem.ItemClick -= ItemPrintDropDownItem_ItemClick;
            }
            printDropDownMenu.ItemLinks.Clear();

        }
        private void ItemPrintDropDownItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarButtonItem item = e.Item as BarButtonItem;
            if (item == null) return;
            XtraMdiTabPage selectedPage = tmmMain.SelectedPage;
            if (selectedPage == null) return;
            var mdiForm = selectedPage.MdiChild;
            var fBase = mdiForm as FrmBase;
            if (fBase == null) return;
            fBase.CommandPassPrintDropDownMenu(item);
        }

        private void SetTaskBarOverlay(string numNoti)
        {
            string notificationCount = numNoti; //To do: Add this as a parameter
            int noticount = notificationCount.Count();
            int sizefont = 20 - noticount;
            var bmp = new Bitmap(32 + noticount, 32 + noticount);
            //using (Graphics g = Graphics.FromImage(bmp))
            //{
            //    g.FillEllipse(Brushes.Red, new Rectangle(Point.Empty, bmp.Size));
            //    g.DrawString(notificationCount, new Font("Sans serif", 15, GraphicsUnit.Point),
            //        Brushes.White, new Rectangle(Point.Empty, bmp.Size));
            //}

            Graphics g = Graphics.FromImage(bmp);
            g.FillEllipse(Brushes.Red, new Rectangle(0, 0, 32 + noticount, 32 + noticount));
            g.DrawString(notificationCount, new Font("San serif", sizefont, GraphicsUnit.Pixel), Brushes.White, new Rectangle((32 - sizefont) / 4, (32 - sizefont) / 2, 32 + noticount, 32 + noticount));



            var overlay = Icon.FromHandle(bmp.GetHicon());
            try
            {
                TaskbarManager.Instance.SetOverlayIcon(overlay, "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

        }

        private void RemoveTaskBarOverlay()
        {
            try
            {
                TaskbarManager.Instance.SetOverlayIcon(null, "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (CheckDB)
            {
                int numNoti = ProcessGeneral.GetSafeInt(dt3.Rows[0][0]);
                if (numNoti > 0)
                {
                    btnNotify.Caption = $"You have {numNoti} new notification";





                    btnNotify2.Caption = $"You have {numNoti} new notification";



                    if (btnNotify.Enabled == false)
                        btnNotify.Enabled = true;
                    if (btnNotify2.Enabled == false)
                        btnNotify2.Enabled = true;

                    SetTaskBarOverlay(numNoti.ToString());
                    checkNewNoti = true;

                }
                else
                {
                    btnNotify.Caption = "No news notification";
                    btnNotify.ItemAppearance.Normal.BackColor = Color.Transparent;

                    btnNotify2.Caption = "No news notification";
                    btnNotify2.ItemAppearance.Normal.BackColor = Color.Transparent;
                    if (btnNotify.Enabled == false)
                        btnNotify.Enabled = true;
                    if (btnNotify2.Enabled == false)
                        btnNotify2.Enabled = true;

                    RemoveTaskBarOverlay();
                    checkNewNoti = false;
                }
            }
            else
            {
                btnNotify.Caption = "Lost Connect from Server";
                btnNotify.ItemAppearance.Normal.BackColor = Color.Transparent;

                btnNotify2.Caption = "Lost Connect from Server";
                btnNotify2.ItemAppearance.Normal.BackColor = Color.Transparent;
                btnNotify.Enabled = false;
                btnNotify2.Enabled = false;
            }
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            SqlConnection cnn = new SqlConnection(DeclareSystem.SysConnectionString);

            try
            {


                string cmd = $"SELECT Count(*) FROM CNYNoti WHERE UserID={DeclareSystem.SysUserId} AND CNY003=1";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd, cnn);
                dt3.Clear();
                adapter.Fill(dt3);
                CheckDB = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                CheckDB = false;


            }

        }

        private void GvNotify_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Dismiss")
            {
                int PK = ProcessGeneral.GetSafeInt(gvNotify.GetRowCellValue(gvNotify.FocusedRowHandle, "PK"));
                gvNotify.DeleteRow(gvNotify.FocusedRowHandle);
                bool t = ac.BolExcuteSQL($"UPDATE CNYNoti SET CNY003={0} WHERE PK={PK}", null);

                if (!bw.IsBusy)
                    bw.RunWorkerAsync();



            }
            else if (e.Column.FieldName == "ViewDocumment")
            {
                MessageBox.Show("The feature is being develop and be comming soon", "Infomation");
            }
        }

        private void GvNotify_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void DockNotify_Collapsed(object sender, DockPanelEventArgs e)
        {
            dockNotify.Visibility = DockVisibility.Hidden;
        }

        private void DockManagerMain_StartDocking(object sender, DockPanelCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void DockPanel1_SizeChanged(object sender, EventArgs e)
        {
            if (_isLoad) return;
            int width = dockPanel1.Width;
            //if (width < _dockWidth)
            //{
            //    width = _dockWidth;
            //}

            int vSrollWidth = GetScrollWidthInTree();
            int colWidth = width - vSrollWidth - 10;
            treeList1.Columns["FormName"].Width = colWidth;

            //   MessageBox.Show(dockPanel1.Width.ToString());
        }

        private void TxtSearchMain_EditValueChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchMain.Text.Trim();
            treeList1.ApplyFindFilter(searchText);

        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }



        private void ShowFormLogin()
        {
            this.Close();
            var fShow = new FrmLogin();
            Program.OpenFormByThread(fShow, false);
        }
        private void ShowFormConfigSql()
        {
            this.Close();
            var fShow = new FrmConfigSQLServer();
            Program.OpenFormByThread(fShow, false);

        }
        #endregion

        #region "Tray Icon"
        private void MinimizeToTray()
        {
            notifyIcon.Visible = true;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            notifyIcon.ShowBalloonTip(1000);
            minimizedToTray = true;
        }

        private void NotifyIconClick(Object sender, EventArgs e)
        {
            ShowWindow();
        }
        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                DialogResult dlResult = XtraMessageBox.Show("Do you want to exit program ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlResult == DialogResult.Yes)
                {
                    Application.ExitThread();
                    Application.Exit();
                }
            }
            else
            {
                Application.ExitThread();
                Application.Exit();
            }
        }

        #endregion

        #region "Show Multi Instance"

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == SingleInstance.WM_SHOWFIRSTINSTANCE)
            {
                ShowWindow();
            }
            base.WndProc(ref message);

        }
        public void ShowWindow()
        {
            if (minimizedToTray)
            {
                minimizedToTray = false;
                notifyIcon.Visible = false;
                this.Show();
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                WinApi.ShowToFrontMaximized(this.Handle);
            }
        }
        #endregion


        #region "Process Ribbon"

        private void SettingRibbonWhenLoad()
        {
            ribPageGroupMain.Enabled = false;
            ribbon.SelectedPage = rpgSystem;
        }

        private void SettingRibbonWhenOpenForm()
        {
            ribPageGroupMain.Enabled = true;
            ribbon.SelectedPage = rpgMainCommand;
        }
        #endregion
        #region "Form Event"
        private void FrmMain_Load(object sender, EventArgs e)
        {
          
           
          
            this.TopMost = false;
            //ribbon.Minimized = true;
            ribbon.Minimized = false;
            SettingRibbonWhenLoad();
            LoadInfoAfterLogon();
            _isLoad = false;
            LoadDataGridViewBOM();

           


            io = IO.Socket($"http://{DeclareSystem.SysServerSql}:3000");
            io.On("CheckNotify", (data) =>
            {
                //String msg = JsonConvert.DeserializeAnonymousType()
                Thread.Sleep(1000);
                if (!bw.IsBusy)
                    bw.RunWorkerAsync();
            }
                );

            if (!bw.IsBusy)
                bw.RunWorkerAsync();


          


        }

     
        private void BtnGetItemCode_Click(object sender, EventArgs e)
        {
            LoadDataGridViewBOM();

        }





        private void LoadDataGridViewBOM()
        {
            ProcessGeneral.LoadBomAssignInfo(SystemProperty.SysUserName, SystemProperty.SysConnectionString);
            gvNMTBOM.BeginUpdate();
            gcNMTBOM.DataSource = DeclareSystem.DtBoMAssignMain;
            gvNMTBOM.BestFitColumns();
            gvNMTBOM.EndUpdate();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isConfigSql && !_isLogout)
            {
                if (this.MdiChildren.Length > 0)
                {
                    DialogResult dlResult = XtraMessageBox.Show("Do you want to exit program ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlResult == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
            _isConfigSql = false;
            _isLogout = false;
        }

       
       

        #endregion

        #region "Transfer Command"

        private void ribbon_ShowCustomizationMenu(object sender, RibbonCustomizationMenuEventArgs e)
        {
            e.ShowCustomizationMenu = false;
            if (e.HitInfo.HitTest != RibbonHitTest.Item) return;
            BarItemLink linkItem = e.HitInfo.Item;
            if (linkItem == null) return;
            BarButtonItem item = linkItem.Item as BarButtonItem;
            if (item == null) return;
            if (item.Visibility == BarItemVisibility.Never) return;
            if (!item.Enabled) return;
            string commandName = GetCommandName(item.Name);
            if (string.IsNullOrEmpty(commandName)) return;
            XtraMdiTabPage selectedPage = tmmMain.SelectedPage;
            if (selectedPage == null) return;
            var mdiForm = selectedPage.MdiChild;
            var fBase = mdiForm as FrmBase;
            if (fBase == null)
            {
                switch (commandName)
                {
                    case CommandBarDefaultSetting.CaptionButtonCloseDefaultCommamd:
                        mdiForm.Close();
                        break;
                }
            }
            else
            {
                fBase.CommandPassRightCLick(commandName);
            }
        }

        private void barButtonItemCommand_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarButtonItem item = e.Item as BarButtonItem;
            if (item == null) return;
            string commandName = GetCommandName(item.Name);
            XtraMdiTabPage selectedPage = tmmMain.SelectedPage;
            if (selectedPage == null) return;
            var mdiForm = selectedPage.MdiChild;
            var fBase = mdiForm as FrmBase;
            if (fBase == null)
            {
                switch (commandName)
                {
                    case CommandBarDefaultSetting.CaptionButtonCloseDefaultCommamd:
                        mdiForm.Close();
                        break;
                }
            }
            else
            {
                fBase.CommandPass(commandName);
            }

        }

        private string GetCommandName(string buttonName)
        {
            string commandName = "";
            switch (buttonName)
            {
                case "bbiAdd":
                    commandName = CommandBarDefaultSetting.CaptionButtonAddDefaultCommamd;
                    break;
                case "bbiEdit":
                    commandName = CommandBarDefaultSetting.CaptionButtonEditDefaultCommamd;
                    break;
                case "bbiDelete":
                    commandName = CommandBarDefaultSetting.CaptionButtonDeleteDefaultCommamd;
                    break;
                case "bbiSave":
                    commandName = CommandBarDefaultSetting.CaptionButtonSaveDefaultCommamd;
                    break;
                case "bbiCancel":
                    commandName = CommandBarDefaultSetting.CaptionButtonCancelDefaultCommamd;
                    break;
                case "bbiRefresh":
                    commandName = CommandBarDefaultSetting.CaptionButtonRefreshDefaultCommamd;
                    break;
                case "bbiFind":
                    commandName = CommandBarDefaultSetting.CaptionButtonFindDefaultCommamd;
                    break;
                case "bbiPrint":
                    commandName = CommandBarDefaultSetting.CaptionButtonPrintDefaultCommamd;
                    break;
                case "bbiExpand":
                    commandName = CommandBarDefaultSetting.CaptionButtonExpandDefaultCommamd;
                    break;
                case "bbiCollapse":
                    commandName = CommandBarDefaultSetting.CaptionButtonCollosepandDefaultCommamd;
                    break;
                case "bbiClose":
                    commandName = CommandBarDefaultSetting.CaptionButtonCloseDefaultCommamd;
                    break;
                case "bbiRevision":
                    commandName = CommandBarDefaultSetting.CaptionButtonRevisionDefaultCommamd;
                    break;
                case "bbiBreakDown":
                    commandName = CommandBarDefaultSetting.CaptionButtonBreakDownDefaultCommamd;
                    break;
                case "bbiRangSize":
                    commandName = CommandBarDefaultSetting.CaptionButtonRangeSizeDefaultCommamd;
                    break;
                case "bbiCopyObject":
                    commandName = CommandBarDefaultSetting.CaptionButtonCopyDefaultCommamd;
                    break;
                case "bbiGenerate":
                    commandName = CommandBarDefaultSetting.CaptionButtonGenerateDefaultCommamd;
                    break;
                case "bbiCombine":
                    commandName = CommandBarDefaultSetting.CaptionButtonCombineDefaultCommamd;
                    break;
                case "bbiCheck":
                    commandName = CommandBarDefaultSetting.CaptionButtonCheckDefaultCommamd;
                    break;

                #region "Function"

                case "bbiFN_1":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiFN_1;
                    break;
                case "bbiFN_2":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiFN_2;
                    break;
                case "bbiFN_3":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiFN_3;
                    break;
                case "bbiFN_4":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiFN_4;
                    break;
                case "bbiFN_5":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiFN_5;
                    break;
                ///////////////////////////-------------------------------------------------------------------------------
                case "bbiGFN1_F1":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN1_F1;
                    break;
                case "bbiGFN1_F2":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN1_F2;
                    break;
                case "bbiGFN1_F3":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN1_F3;
                    break;
                case "bbiGFN1_F4":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN1_F4;
                    break;
                case "bbiGFN1_F5":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN1_F5;
                    break;
                case "bbiGFN1_F6":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN1_F6;
                    break;
                ///////////////////////////-------------------------------------------------------------------------------
                case "bbiGFN2_F1":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN2_F1;
                    break;
                case "bbiGFN2_F2":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN2_F2;
                    break;
                case "bbiGFN2_F3":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN2_F3;
                    break;
                case "bbiGFN2_F4":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN2_F4;
                    break;
                case "bbiGFN2_F5":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN2_F5;
                    break;
                case "bbiGFN2_F6":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN2_F6;
                    break;
                ///////////////////////////-------------------------------------------------------------------------------
                case "bbiGFN3_F1":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN3_F1;
                    break;
                case "bbiGFN3_F2":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN3_F2;
                    break;
                case "bbiGFN3_F3":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN3_F3;
                    break;
                case "bbiGFN3_F4":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN3_F4;
                    break;
                case "bbiGFN3_F5":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN3_F5;
                    break;
                case "bbiGFN3_F6":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN3_F6;
                    break;
                ///////////////////////////-------------------------------------------------------------------------------
                case "bbiGFN4_F1":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN4_F1;
                    break;
                case "bbiGFN4_F2":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN4_F2;
                    break;
                case "bbiGFN4_F3":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN4_F3;
                    break;
                case "bbiGFN4_F4":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN4_F4;
                    break;
                case "bbiGFN4_F5":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN4_F5;
                    break;
                case "bbiGFN4_F6":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN4_F6;
                    break;
                ///////////////////////////-------------------------------------------------------------------------------
                case "bbiGFN5_F1":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN5_F1;
                    break;
                case "bbiGFN5_F2":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN5_F2;
                    break;
                case "bbiGFN5_F3":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN5_F3;
                    break;
                case "bbiGFN5_F4":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN5_F4;
                    break;
                case "bbiGFN5_F5":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN5_F5;
                    break;
                case "bbiGFN5_F6":
                    commandName = CommandBarDefaultSetting.CaptionButtonbbiGFN5_F6;
                    break;
                ///////////////////////////-------------------------------------------------------------------------------
                    #endregion



            }

            return commandName;
        }

        private void HideMainMenuNotBaseForm()
        {
            bbiAdd.Visibility = BarItemVisibility.Never;
            bbiEdit.Visibility = BarItemVisibility.Never;
            bbiDelete.Visibility = BarItemVisibility.Never;
            bbiSave.Visibility = BarItemVisibility.Never;
            bbiCancel.Visibility = BarItemVisibility.Never;
            bbiRefresh.Visibility = BarItemVisibility.Never;
            bbiFind.Visibility = BarItemVisibility.Never;
            bbiPrint.Visibility = BarItemVisibility.Never;
            bbiPrintDown.Visibility = BarItemVisibility.Never;
            bbiExpand.Visibility = BarItemVisibility.Never;
            bbiCollapse.Visibility = BarItemVisibility.Never;
            bbiRevision.Visibility = BarItemVisibility.Never;
            bbiBreakDown.Visibility = BarItemVisibility.Never;
            bbiRangSize.Visibility = BarItemVisibility.Never;
            bbiCopyObject.Visibility = BarItemVisibility.Never;
            bbiGenerate.Visibility = BarItemVisibility.Never;
            bbiCombine.Visibility = BarItemVisibility.Never;
            bbiCheck.Visibility = BarItemVisibility.Never;
       







            bbiClose.Visibility = BarItemVisibility.Always;
            bbiClose.Enabled = true;


            #region "Function"
            bbiGFN1.Visibility = BarItemVisibility.Never;
            bbiGFN1_F1.Visibility = BarItemVisibility.Never;
            bbiGFN1_F2.Visibility = BarItemVisibility.Never;
            bbiGFN1_F3.Visibility = BarItemVisibility.Never;
            bbiGFN1_F4.Visibility = BarItemVisibility.Never;
            bbiGFN1_F5.Visibility = BarItemVisibility.Never;
            bbiGFN1_F6.Visibility = BarItemVisibility.Never;
            //---------------------------------------------------------------------------------------------------
            bbiGFN2.Visibility = BarItemVisibility.Never;
            bbiGFN2_F1.Visibility = BarItemVisibility.Never;
            bbiGFN2_F2.Visibility = BarItemVisibility.Never;
            bbiGFN2_F3.Visibility = BarItemVisibility.Never;
            bbiGFN2_F4.Visibility = BarItemVisibility.Never;
            bbiGFN2_F5.Visibility = BarItemVisibility.Never;
            bbiGFN2_F6.Visibility = BarItemVisibility.Never;
            //---------------------------------------------------------------------------------------------------
            bbiGFN3.Visibility = BarItemVisibility.Never;
            bbiGFN3_F1.Visibility = BarItemVisibility.Never;
            bbiGFN3_F2.Visibility = BarItemVisibility.Never;
            bbiGFN3_F3.Visibility = BarItemVisibility.Never;
            bbiGFN3_F4.Visibility = BarItemVisibility.Never;
            bbiGFN3_F5.Visibility = BarItemVisibility.Never;
            bbiGFN3_F6.Visibility = BarItemVisibility.Never;
            //---------------------------------------------------------------------------------------------------
            bbiGFN4.Visibility = BarItemVisibility.Never;
            bbiGFN4_F1.Visibility = BarItemVisibility.Never;
            bbiGFN4_F2.Visibility = BarItemVisibility.Never;
            bbiGFN4_F3.Visibility = BarItemVisibility.Never;
            bbiGFN4_F4.Visibility = BarItemVisibility.Never;
            bbiGFN4_F5.Visibility = BarItemVisibility.Never;
            bbiGFN4_F6.Visibility = BarItemVisibility.Never;
            //---------------------------------------------------------------------------------------------------
            bbiGFN5.Visibility = BarItemVisibility.Never;
            bbiGFN5_F1.Visibility = BarItemVisibility.Never;
            bbiGFN5_F2.Visibility = BarItemVisibility.Never;
            bbiGFN5_F3.Visibility = BarItemVisibility.Never;
            bbiGFN5_F4.Visibility = BarItemVisibility.Never;
            bbiGFN5_F5.Visibility = BarItemVisibility.Never;
            bbiGFN5_F6.Visibility = BarItemVisibility.Never;
            //---------------------------------------------------------------------------------------------------
            bbiFN_1.Visibility = BarItemVisibility.Never;
            bbiFN_2.Visibility = BarItemVisibility.Never;
            bbiFN_3.Visibility = BarItemVisibility.Never;
            bbiFN_4.Visibility = BarItemVisibility.Never;
            bbiFN_5.Visibility = BarItemVisibility.Never;
            #endregion
        }
        #endregion

        #region "Tab MDI ,Panel Control"
        private void tmmMain_SelectedPageChanged(object sender, EventArgs e)
        {
            XtraMdiTabPage selectedPage = tmmMain.SelectedPage;
            if (selectedPage == null)
            {
                SettingRibbonWhenLoad();
                return;
            }
            SettingRibbonWhenOpenForm();

            var mdiForm = selectedPage.MdiChild as FrmBase;
            if (mdiForm == null)
            {
                HideMainMenuNotBaseForm();

                //if (!ribbon.Minimized)
                //{
                //    ribbon.Minimized = true;
                //}

            }
            else
            {
                mdiForm.ReDisplayMainMenu();

                //if (ribbon.Minimized)
                //{
                //    ribbon.Minimized = false;
                //}

                CreateEventSelectionChangedAllGrid(mdiForm);
                //SplitGroupPanel a = new SplitGroupPanel(this.spinPaging);
                //GroupControl grpC = new GroupControl();
                //XtraScrollableControl x = new XtraScrollableControl();
                // SplitContainerControl spl = new SplitContainerControl();

                List<PanelControl> lPanel = mdiForm.FindAllChildrenByType<PanelControl>().ToList();
                if (lPanel.Count <= 0) return;

                foreach (PanelControl pcAdd in lPanel)
                {
                    pcAdd.ControlAdded += pcAdd_ControlAdded;
                    pcAdd.ControlRemoved += pcAdd_ControlRemoved;
                }
            }






        }

        private void pcAdd_ControlRemoved(object sender, ControlEventArgs e)
        {
            var pc = sender as PanelControl;
            if (pc == null) return;
            CreateEventSelectionChangedAllGrid(pc);
        }

        private void pcAdd_ControlAdded(object sender, ControlEventArgs e)
        {
            var pc = sender as PanelControl;
            if (pc == null) return;
            CreateEventSelectionChangedAllGrid(pc);
        }

        #endregion


        #region "Process GridControl, PivotGrid, SpeardSheet, TreeList Aggreate"




        private void CreateEventSelectionChangedAllGrid(Control f)
        {


            bool isSetDefault = false;




            List<SpreadsheetControl> lSpread = f.FindAllChildrenByType<SpreadsheetControl>().ToList();

            if (lSpread.Any())
            {
                List<SpreadsheetControl> lSpreadF = new List<SpreadsheetControl>();
                foreach (SpreadsheetControl spread in lSpread)
                {
                    if (spread.Visible)
                    {
                        lSpreadF.Add(spread);
                    }

                    spread.SelectionChanged += spread_SelectionChanged;
                    //   spread.LostFocus += spread_LostFocus;
                }
                if (lSpreadF.Any())
                {
                    SetSumSpreadsheet(lSpreadF[0]);
                    isSetDefault = true;
                }

            }


            List<PivotGridControl> lPivot = f.FindAllChildrenByType<PivotGridControl>().ToList();
            if (lPivot.Any())
            {
                List<PivotGridControl> lPivotF = new List<PivotGridControl>();
                foreach (PivotGridControl pivot in lPivot)
                {
                    if (pivot.Visible && pivot.OptionsSelection.MultiSelect)
                    {
                        lPivotF.Add(pivot);
                    }
                    pivot.CellSelectionChanged += pivot_CellSelectionChanged;
                    // pivot.LostFocus += pivot_LostFocus;
                }
                if (lPivotF.Any())
                {
                    SetSumPivotGrid(lPivotF[0]);
                    isSetDefault = true;
                }

            }

            List<TreeList> lTree = f.FindAllChildrenByType<TreeList>().ToList();

            if (lTree.Any())
            {
                List<TreeList> lTreeF = new List<TreeList>();
                foreach (TreeList tree in lTree)
                {
                    if (tree.Visible && tree.OptionsSelection.MultiSelect && tree.GetSelectedCells().Count > 0)
                    {
                        lTreeF.Add(tree);
                    }
                    if (!tree.OptionsSelection.MultiSelect) continue;
                    tree.SelectionChanged += tree_SelectionChanged;

                    // tree.LostFocus += tree_LostFocus;

                }
                if (lTreeF.Any())
                {
                    SetSumTreeList(lTreeF[0]);
                    isSetDefault = true;
                }

            }



            List<GridControl> lGrid = f.FindAllChildrenByType<GridControl>().ToList();

            if (lGrid.Any())
            {
                List<GridView> lGridF = new List<GridView>();




                foreach (GridControl grid in lGrid)
                {
                    var focusedView = grid.FocusedView as GridView;
                    if (grid.Visible && focusedView != null && focusedView.SelectedRowsCount > 0 && focusedView.OptionsSelection.MultiSelect && focusedView.IsVisible)
                    {
                        lGridF.Add(focusedView);
                    }

                    grid.FocusedViewChanged += grid_FocusedViewChanged;
                    var mainView = grid.MainView as GridView;
                    if (mainView == null) continue;
                    if (!mainView.OptionsSelection.MultiSelect) continue;
                    mainView.SelectionChanged += mainView_SelectionChanged;
                    //  mainView.LostFocus += mainView_LostFocus;
                }
                if (lGridF.Any())
                {
                    GetValueAggreateAllGridView(lGridF[0]);
                    isSetDefault = true;
                }
            }

            if (!isSetDefault)
            {
                LoadSumOnLableStatusBar(0, 0, 0);
            }






        }

        #region "Tree List"

        private void tree_SelectionChanged(object sender, EventArgs e)
        {
            var tl = sender as TreeList;
            if (tl == null) return;
            SetSumTreeList(tl);
        }




        private void SetSumTreeList(TreeList tl)
        {
            decimal sum = 0;
            int count = 0;
            decimal avg = 0;

            var q1 = tl.GetSelectedCells();
            if (q1.Count <= 0)
                goto loadinfo;
            TreeListColumn col = tl.FocusedColumn;
            if (col == null)
            {
                col = q1.Select(p => p.Column).Distinct().First();
            }
            if (col == null)
                goto loadinfo;
            Type t = col.ColumnType;
            bool isNumericType = t.IsNumericType();

            int tagSum = ProcessGeneral.GetSafeInt(col.Tag);


            var q2 = q1.Select(p => p.Node).Distinct().ToList();
            count = q2.Count;
            if (!isNumericType || count <= 0) goto loadinfo;

            bool qRoot = q2.All(p => p.ParentNode == null);
            bool qChild = q2.All(p => p.ParentNode != null);

            if (qRoot || qChild) goto sumNormal;
            if (tagSum == ConstSystem.SumRootNode)
            {
                var qRootS = q2.Where(p => p.ParentNode == null).ToList();
                count = qRootS.Count;
                if (count <= 0) goto loadinfo;
                sum = qRootS.Sum(p => ProcessGeneral.GetSafeDecimal(p.GetValue(col)));
                avg = Math.Round(sum / count, 5);
                goto loadinfo;
            }
            if (tagSum == ConstSystem.SumChildNode)
            {
                var qChildS = q2.Where(p => p.ParentNode != null).ToList();
                count = qChildS.Count;
                if (count <= 0) goto loadinfo;
                sum = qChildS.Sum(p => ProcessGeneral.GetSafeDecimal(p.GetValue(col)));
                avg = Math.Round(sum / count, 5);
                goto loadinfo;
            }

            sumNormal:
            sum = q2.Sum(p => ProcessGeneral.GetSafeDecimal(p.GetValue(col)));

            avg = Math.Round(sum / count, 5);

            loadinfo:
            LoadSumOnLableStatusBar(count, sum, avg);
        }

        #endregion



        /*
        private void tree_LostFocus(object sender, EventArgs e)
        {
            LoadSumOnLableStatusBar(0, 0, 0);
        }
         private void spread_LostFocus(object sender, EventArgs e)
        {
            LoadSumOnLableStatusBar(0, 0, 0);
        }
         
        private void pivot_LostFocus(object sender, EventArgs e)
        {

            LoadSumOnLableStatusBar(0, 0, 0);
        }
          private void gvDetail_LostFocus(object sender, EventArgs e)
        {
            LoadSumOnLableStatusBar(0, 0, 0);
        }
         
           private void mainView_LostFocus(object sender, EventArgs e)
        {
           LoadSumOnLableStatusBar(0, 0, 0);
        }
 
         */



        #region "Spread Sheet"
        private void spread_SelectionChanged(object sender, EventArgs e)
        {
            var spread = sender as SpreadsheetControl;
            if (spread == null) return;
            SetSumSpreadsheet(spread);

        }


        private void SetSumSpreadsheet(SpreadsheetControl spread)
        {
            var ws = spread.ActiveWorksheet;

            decimal sum = 0;
            int count = 0;
            decimal avg = 0;




            int columnIndex = ws.SelectedCell.LeftColumnIndex;

            CellRange range = ws.Selection;

            if (columnIndex < 0)
            {
                columnIndex = range.LeftColumnIndex;
                if (columnIndex < 0)
                {
                    columnIndex = 0;
                }
            }

            int beginRow = range.TopRowIndex;
            int endRow = range.BottomRowIndex;

            if (endRow > 100000)
            {
                CellRange dataRange = ws.GetDataRange();
                int endRow1 = dataRange.BottomRowIndex;
                if (endRow > endRow1)
                {
                    endRow = endRow1;
                }
            }

            if (beginRow > endRow) goto loadinfo;

            for (int rowIndex = beginRow; rowIndex <= endRow; rowIndex++)
            {
                count++;
                Cell cell = ws[rowIndex, columnIndex];
                string value = cell.DisplayText;
                sum += ProcessGeneral.GetSafeDecimal(value);
            }


            if (count > 0)
            {
                avg = Math.Round(sum / count, 5);
            }
            loadinfo:
            LoadSumOnLableStatusBar(count, sum, avg);

        }

        #endregion


        #region "Pivot Grid"



        private void pivot_CellSelectionChanged(object sender, EventArgs e)
        {
            var pivot = sender as PivotGridControl;
            if (pivot == null) return;
            SetSumPivotGrid(pivot);
        }

        private void SetSumPivotGrid(PivotGridControl pivot)
        {

            PivotGridCells cells = pivot.Cells;

            // Get the coordinates of the selected cells.
            Rectangle cellSelection = cells.Selection;
            int maxRow = cellSelection.Y + cellSelection.Height - 1;

            PivotCellEventArgs cellInfo = cells.GetFocusedCellInfo();
            int colIndex = cellInfo.ColumnIndex;
            Type t = cellInfo.DataField.DataType;
            bool isNumericType = t.IsNumericType();



            decimal sum = 0;
            int count = 0;
            decimal avg = 0;
            // Iterate through the selected cells.
            for (int rowIndex = cellSelection.Y; rowIndex <= maxRow; rowIndex++)
            {
                count++;
                PivotCellEventArgs cellInfoAgg = cells.GetCellInfo(colIndex, rowIndex);

                if (isNumericType)
                {
                    sum += ProcessGeneral.GetSafeDecimal(cellInfoAgg.Value);
                }

            }

            if (count > 0)
            {
                avg = Math.Round(sum / count, 5);
            }


            LoadSumOnLableStatusBar(count, sum, avg);
        }


        #endregion
        #region "GridView"



        private void grid_FocusedViewChanged(object sender, ViewFocusEventArgs e)
        {
            var gvDetail = e.View as GridView;
            if (gvDetail == null) return;
            if (!gvDetail.IsDetailView) return;
            gvDetail.SelectionChanged += gvDetail_SelectionChanged;
            // gvDetail.LostFocus += gvDetail_LostFocus;
        }



        private void gvDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GetValueAggreateAllGridView(gv);
        }



        private void mainView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
            GetValueAggreateAllGridView(gv);
        }


        private void GetValueAggreateAllGridView(GridView gv)
        {
            decimal sum = 0;
            int count = gv.SelectedRowsCount;
            decimal avg = 0;

            if (count <= 0)
                goto loadinfo;

            if (!gv.OptionsSelection.MultiSelect) goto loadinfo;

            GridColumn col = gv.FocusedColumn;
            if (gv.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CellSelect)
            {
                if (col == null)
                {
                    var qCol = gv.GetSelectedCells().Select(p => p.Column).Distinct().ToList();
                    if (qCol.Count <= 0) goto loadinfo;
                    col = qCol.First();
                }

            }
            else
            {
                if (col == null) goto loadinfo;
            }



            try
            {
                Type t = col.ColumnType;
                bool isNumericType = t.IsNumericType();
                if (!isNumericType) goto loadinfo;

                sum = gv.GetSelectedRows().Select(p => ProcessGeneral.GetSafeDecimal(gv.GetRowCellValue(p, col))).Sum();
                avg = Math.Round(sum / count, 5);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }





            loadinfo:
            LoadSumOnLableStatusBar(count, sum, avg);

        }

        #endregion



        private void LoadSumOnLableStatusBar(int count, decimal sum, decimal avg)
        {
            spinSum.EditValue = sum;
            spinCount.EditValue = count;
            spinAVG.EditValue = avg;
            barStaticI_GridAgg.Caption = string.Format("Count = [{0}] | Sum = [{1}] | Average = [{2}]", spinCount.Text.Trim(), spinSum.Text.Trim(), spinAVG.Text.Trim());
        }
        #endregion

        #region "Process Combobox"
        private DataTable TableTemplateCbGroupUserCode()
        {
            var dt = new DataTable();
            dt.Columns.Add("UserInGroupID", typeof(Int64));
            dt.Columns.Add("GroupUserCode", typeof(string));
            dt.Columns.Add("GroupUserName", typeof(string));
            return dt;
        }
        private void LoadComboUserGroup(DataTable dt)
        {
            lookupUserGroup.Properties.DataSource = dt;
            lookupUserGroup.Properties.DisplayMember = "GroupUserName";
            lookupUserGroup.Properties.ValueMember = "UserInGroupID";
            lookupUserGroup.Properties.Columns.Clear();
            var column0 = new LookUpColumnInfo
            {
                FieldName = "UserInGroupID",
                Caption = @"UserInGroupID",
                Alignment = HorzAlignment.Near,
                Visible = false
            };
            lookupUserGroup.Properties.Columns.Add(column0);
            var column1 = new LookUpColumnInfo
            {
                FieldName = "GroupUserCode",
                Caption = @"User Group Code",
                Alignment = HorzAlignment.Near,
                Visible = true
            };
            lookupUserGroup.Properties.Columns.Add(column1);
            var column2 = new LookUpColumnInfo
            {
                FieldName = "GroupUserName",
                Caption = @"User Group Name",
                Alignment = HorzAlignment.Near,
                Visible = true
            };
            lookupUserGroup.Properties.Columns.Add(column2);
            lookupUserGroup.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            lookupUserGroup.Properties.ForceInitialize();
        }
        private DataTable TableTemplateCbGroupFunctionCode()
        {
            var dt = new DataTable();
            dt.Columns.Add("IDAuthorization", typeof(Int64));
            dt.Columns.Add("PermisionGroupCode", typeof(string));
            dt.Columns.Add("PermisionGroupName", typeof(string));
            return dt;
        }
        private void LoadComboFunctionGroup(DataTable dt)
        {
            lookUpFuntionGroup.Properties.DataSource = dt;
            lookUpFuntionGroup.Properties.DisplayMember = "PermisionGroupName";
            lookUpFuntionGroup.Properties.ValueMember = "IDAuthorization";
            lookUpFuntionGroup.Properties.Columns.Clear();
            var column0 = new LookUpColumnInfo
            {
                FieldName = "IDAuthorization",
                Caption = @"IDAuthorization",
                Alignment = HorzAlignment.Near,
                Visible = false
            };
            lookUpFuntionGroup.Properties.Columns.Add(column0);
            var column1 = new LookUpColumnInfo
            {
                FieldName = "PermisionGroupCode",
                Caption = @"Function Group Code",
                Alignment = HorzAlignment.Near,
                Visible = true
            };
            lookUpFuntionGroup.Properties.Columns.Add(column1);
            var column2 = new LookUpColumnInfo
            {
                FieldName = "PermisionGroupName",
                Caption = @"Function Group Name",
                Alignment = HorzAlignment.Near,
                Visible = true
            };
            lookUpFuntionGroup.Properties.Columns.Add(column2);
            lookUpFuntionGroup.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            lookUpFuntionGroup.Properties.ForceInitialize();
        }
        private void lookupUserGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (!_performEventCombobox)
                return;
            if (lookupUserGroup.Properties.DataSource == null)
            {
                return;
            }
            var drv = lookupUserGroup.Properties.GetDataSourceRowByKeyValue(lookupUserGroup.EditValue) as DataRowView;
            if (drv != null)
            {
                SystemProperty.SysDtCbPermission = _ctrl.LoadCbFuncionGroupWhenLogin(ProcessGeneral.GetSafeString(drv.Row["GroupUserCode"]));
                LoadComboFunctionGroup(SystemProperty.SysDtCbPermission);

            }
            else
            {
                LoadComboFunctionGroup(TableTemplateCbGroupFunctionCode());
            }
        }



        private void lookupUserGroup_Popup(object sender, EventArgs e)
        {
            var edit = sender as UserLookUpEdit;
            var f = ((IPopupControl)edit).PopupWindow as UserPopupLookUpEditForm;
            var dtHeight = (DataTable)edit.Properties.DataSource;
            int dropDownRow = edit.Properties.DropDownRows;
            // f.Width = edit.Width;
            if (dtHeight.Rows.Count < dropDownRow)
            {
                f.Height = (dtHeight.Rows.Count + 1) * edit.Properties.DropDownItemHeight + 7;
            }
        }

        private void lookUpFuntionGroup_Popup(object sender, EventArgs e)
        {
            var edit = sender as UserLookUpEdit;
            var f = ((IPopupControl)edit).PopupWindow as UserPopupLookUpEditForm;
            var dtHeight = (DataTable)edit.Properties.DataSource;
            int dropDownRow = edit.Properties.DropDownRows;
            //  f.Width = edit.Width;
            if (dtHeight.Rows.Count < dropDownRow)
            {
                f.Height = (dtHeight.Rows.Count + 1) * edit.Properties.DropDownItemHeight + 7;
            }
        }
        #endregion

        #region "Init Treeview Template"






        /// <summary>
        ///     For Load icon Into Treeview
        /// </summary>
        /// <returns>
        ///     A System.Windows.Forms.ImageList value...
        /// </returns>
        private ImageList GetImageListDisplayTreeView()
        {
            var imgLt = new ImageList();
            imgLt.Images.Add(Resources.folder_yellow_Close_icon);
            imgLt.Images.Add(Resources.folder_yellow_open_icon);
            imgLt.Images.Add(Resources.Document_txt_icon);
            imgLt.Images.Add(Resources.knowledgebasearticle_16x16);
            imgLt.Images.Add(Resources.sendbehindtext_16x16);
            imgLt.Images.Add(Resources.suggestion_16x16);
            return imgLt;
        }
        /// <summary>
        ///     Init Treeview template
        /// </summary>
        private void InitTreeView(TreeList treeList)
        {
            treeList.AllowDrop = false;




            treeList.OptionsDragAndDrop.CanCloneNodesOnDrop = true;

            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.AllowFilterEditor = true;
            treeList.OptionsFilter.AllowMRUFilterList = true;
            treeList.OptionsFilter.AllowColumnMRUFilterList = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = false;
            treeList.OptionsFind.AlwaysVisible = false;
            treeList.OptionsFind.ShowCloseButton = false;
            treeList.OptionsFind.HighlightFindResults = true;
            treeList.OptionsFind.ShowFindButton = false;
            treeList.OptionsFind.ShowClearButton = false;
            treeList.OptionsView.ShowAutoFilterRow = false;

            treeList.OptionsBehavior.Editable = false;
            treeList.OptionsView.ShowColumns = false;
            treeList.OptionsView.ShowHorzLines = false;
            treeList.OptionsView.ShowVertLines = false;
            treeList.OptionsView.ShowIndicator = false;
            treeList.OptionsView.AutoWidth = false;
            treeList.OptionsView.EnableAppearanceEvenRow = false;
            treeList.OptionsView.EnableAppearanceOddRow = false;
            treeList.StateImageList = GetImageListDisplayTreeView();
            treeList.OptionsBehavior.AutoChangeParent = false;
            treeList.Appearance.Row.TextOptions.WordWrap = WordWrap.Wrap;
            treeList.OptionsBehavior.AutoNodeHeight = true;

            treeList.OptionsView.ShowSummaryFooter = false;

            treeList.OptionsBehavior.CloseEditorOnLostFocus = false;
            treeList.OptionsBehavior.KeepSelectedOnClick = true;
            treeList.OptionsBehavior.ShowEditorOnMouseUp = true;
            treeList.OptionsBehavior.SmartMouseHover = false;
            treeList.VertScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Always;

            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsView.ShowCheckBoxes = false;


            treeList.OptionsBehavior.ExpandNodesOnFiltering = true;

            treeList.OptionsSelection.MultiSelect = false;
            treeList.OptionsSelection.MultiSelectMode = TreeListMultiSelectMode.RowSelect;
            treeList.OptionsSelection.SelectNodesOnRightClick = false;
            treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
            treeList.OptionsSelection.EnableAppearanceFocusedRow = false;



            treeList.OptionsMenu.EnableNodeMenu = false;


            treeList.GetStateImage += TreeList_GetStateImage;
            treeList.ShowingEditor += TreeList_ShowingEditor;









            treeList.NodeCellStyle += TreeList_NodeCellStyle;







            treeList.DoubleClick += TreeList_DoubleClick;
            treeList.KeyDown += TreeList_KeyDown;

            treeList.CustomDrawNodeCell += treeList_CustomDrawNodeCell;

            treeList.FilterNode += treeList_FilterNode;
            treeList.CustomNodeCellEdit += TreeList_CustomNodeCellEdit;

        }

        private void TreeList_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {
            if (e.Column.FieldName == "FormName")
            {
                e.RepositoryItem = _repositoryTextGrid;
            }
        }

        private void treeList_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;
            TreeListColumn col = e.Column;
            if (col == null) return;

            //  TreeListCell
            string findText = tl.FindFilterText.Trim();
            if (tl.OptionsFind.HighlightFindResults && !string.IsNullOrEmpty(findText))
            {




                string searchText = findText.Replace("\"", "");

                string[] arrSearchText = findText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Replace("\"", "").Trim()).ToArray();
                int searchCount = arrSearchText.Length;


                string cellText = e.CellText;
                int lengthDisp = cellText.Length;

                if (searchCount == 1)
                {


                    int res = cellText.AccentInsensitiveIndexOf(searchText);
                    if (res >= 0)
                    {

                        e.EditViewInfo.MatchedString = cellText.Substring(res, searchText.Length);
                    }
                }
                else

                {
                    if (lengthDisp >= searchText.Length)
                    {
                        string tmpStr = cellText;
                        int cutOutSymbolsCount = 0;
                        List<Indexes> searchIndexesList = new List<Indexes>();
                        foreach (string sSearchTemp in arrSearchText)
                        {
                            int resIndex = tmpStr.AccentInsensitiveIndexOf(sSearchTemp);

                            if (resIndex < 0) continue;
                            int sSearchTempLength = sSearchTemp.Length;

                            searchIndexesList.Add(new Indexes
                            {
                                Start = resIndex + cutOutSymbolsCount,
                                End = resIndex + cutOutSymbolsCount + sSearchTempLength
                            });
                            //  tmpStr = tmpStr.Remove(resIndex, sSearchTempLength);
                            tmpStr = tmpStr.Substring(resIndex + sSearchTempLength,
                                tmpStr.Length - resIndex - sSearchTempLength);
                            cutOutSymbolsCount = cutOutSymbolsCount + sSearchTempLength + resIndex;
                        }
                        int countListIndex = searchIndexesList.Count;

                        if (countListIndex > 0)
                        {
                            e.Handled = true;
                            XPaint paint = new XPaint();
                            MultiColorDrawStringParams drawParams = new MultiColorDrawStringParams(e.Appearance);
                            drawParams.Bounds = e.Bounds;
                            drawParams.Text = cellText;

                            drawParams.Ranges = new CharacterRangeWithFormat[countListIndex * 2 + 1];
                            int currentStrIndex = 0;
                            int i = 0;
                            while (i < countListIndex)
                            {
                                Indexes ind = searchIndexesList[i];
                                int start = ind.Start;
                                int end = ind.End;
                                drawParams.Ranges[i * 2] = new CharacterRangeWithFormat(currentStrIndex,
                                    start - currentStrIndex, e.Appearance.ForeColor, e.Appearance.BackColor);
                                drawParams.Ranges[i * 2 + 1] = new CharacterRangeWithFormat(start, end - start,
                                    e.Appearance.ForeColor, System.Drawing.Color.FromArgb(255, 210, 0));
                                currentStrIndex = end;
                                i++;
                            }
                            drawParams.Ranges[i * 2] = new CharacterRangeWithFormat(currentStrIndex,
                                lengthDisp - currentStrIndex, e.Appearance.ForeColor, e.Appearance.BackColor);
                            drawParams.Ranges = drawParams.Ranges.Where(val => val.Length != 0).ToArray();


                            paint.MultiColorDrawString(new DevExpress.Utils.Drawing.GraphicsCache(e.Graphics),
                                drawParams);
                        }
                    }
                }



            }








            //TreeListColumn focusCol = tl.FocusedColumn;
            //if (focusCol != null)
            //{
            //    if (e.Node.Focused && focusCol.FieldName == e.Column.FieldName)
            //    {
            //        Brush backBrush = new LinearGradientBrush(e.Bounds, SystemCellColor.BackColorCellFocused, SystemCellColor.BackColor2ShowEditor, LinearGradientMode.ForwardDiagonal);
            //        //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);

            //        e.Graphics.FillRectangle(backBrush, e.Bounds);
            //        ControlGraphicsInfoArgs c = new ControlGraphicsInfoArgs(e.EditViewInfo, e.Cache, e.EditViewInfo.Bounds);
            //        e.EditPainter.Draw(c);
            //        e.Handled = true;
            //        return;
            //    }
            //}

            //if (tl.IsCellSelected(e.Node, e.Column))
            //{
            //    Brush backBrush = new LinearGradientBrush(e.Bounds, SystemCellColor.BackColorCellSelected, SystemCellColor.BackColor2ShowEditor, LinearGradientMode.ForwardDiagonal);
            //    e.Graphics.FillRectangle(backBrush, e.Bounds);
            //    ControlGraphicsInfoArgs c = new ControlGraphicsInfoArgs(e.EditViewInfo, e.Cache, e.EditViewInfo.Bounds);
            //    e.EditPainter.Draw(c);
            //    e.Handled = true;
            //}







        }




        private ExpressionEvaluator GetExpressionEvaluator(CriteriaOperator criteria, bool isReload = false)
        {


            if (!isReload)
            {
                if (criteria.ToString() == _lastCriteria)
                    return _lastEvaluator;
            }

            _lastCriteria = criteria.ToString();
            DataTable dtFt = treeList1.DataSource as DataTable;

            if (dtFt != null)
            {
                PropertyDescriptorCollection pdc = ((ITypedList)dtFt.DefaultView).GetItemProperties(null);
                _lastEvaluator = new ExpressionEvaluator(pdc, criteria, false);
            }
            return _lastEvaluator;
        }

        private CriteriaOperator GetFindPanelCriteria(bool isAndSearchNew)
        {
            return FilterCriteriaHelperTreeList.MyConvertFindPanelTextToCriteriaOperator(treeList1, isAndSearchNew);
        }


        private void treeList_FilterNode(object sender, FilterNodeEventArgs e)
        {

            TreeList tl = (TreeList)sender;
            if (tl == null) return;
            TreeListNode node = e.Node;
            if (node == null) return;


            TreeListNode parentNode = node.ParentNode;

            if (!string.IsNullOrEmpty(tl.FindFilterText))
            {

                CriteriaOperator criteria = FilterCriteriaHelperTreeList.ReplaceFindPanelCriteria(treeList1.ActiveFilterCriteria, tl, GetFindPanelCriteria(true), true);
                ExpressionEvaluator evaluator = GetExpressionEvaluator(criteria);

                e.Handled = true;
                DataTable dtS = (DataTable)tl.DataSource;
                if (dtS != null)
                {

                    DataView dv = dtS.DefaultView;
                    int id = node.Id;
                    object data = dv[id];
                    bool visible = false;

                    try
                    {
                        visible = evaluator.Fit(data);
                    }
                    catch (Exception ex)
                    {
                        evaluator = GetExpressionEvaluator(criteria, true);
                        visible = evaluator.Fit(data);
                        Console.WriteLine(ex.ToString());
                    }
                    finally
                    {

                        string tag = "false";
                        if (visible)
                        {
                            tag = "true";
                        }
                        node.Tag = tag;


                        if (parentNode != null && parentNode.Visible && !visible && ProcessGeneral.GetSafeString(parentNode.Tag) == "true")
                        {
                            visible = true;
                        }

                        node.Visible = visible;
                        if (visible && parentNode != null && !parentNode.Visible)
                        {
                            parentNode.Visible = true;
                        }
                    }



                }


            }











            //if (e.IsFitDefaultFilter) return;
            //if (node.Visible) return;
            //string s = node.GetValue(1).ToString();
            //if (!node.HasChildren) return;
            //bool visibleParent = false;
            //foreach (TreeListNode childNode in node.Nodes)
            //{
            //    if (childNode.Visible)
            //    {
            //        visibleParent = true;
            //        break;
            //    }
            //    else
            //    {
            //        int c = 1;
            //    }
            //}

            //if (!visibleParent) return;
            //node.Visible = true;













        }

        private void TreeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            TreeListNode node = e.Node;
            if (node == null) return;


            if (node.HasChildren)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                if (node.Expanded)
                {

                    e.Appearance.BackColor = Color.BlanchedAlmond;
                    e.Appearance.BackColor2 = Color.Azure;
                }
            }



            if (treeList1.FocusedNode == e.Node && treeList1.FocusedColumn == e.Column)
            {
                e.Appearance.BackColor = Color.BlanchedAlmond;
                e.Appearance.BackColor2 = Color.SkyBlue;
            }
        }

        private void TreeList_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;



        }
        private void TreeList_DoubleClick(object sender, EventArgs e)
        {
            var tl = (TreeList)sender;
            if (tl == null) return;
            TreeListHitInfo hi = tl.CalcHitInfo(tl.PointToClient(Control.MousePosition));
            if (hi.HitInfoType != HitInfoType.Cell) return;
            TreeListNode node = hi.Node;
            if (node == null) return;
            DisplayDataOnClick(node);
        }





        private void TreeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TreeList_DoubleClick(sender, e);
            }

            if (e.KeyData == (Keys.Shift | Keys.F5))
            {
                LoadTreeViewMenuFinal(treeList1);
            }
        }



        private void DisplayDataOnClick(TreeListNode node)
        {
            if (node == null) return;
            if (node.HasChildren) return;
            //var drv = (DataRowView)treeList1.GetDataRecordByNode(treeList1.FocusedNode);
            //if (drv == null) return;





            var qCheck = Application.OpenForms.Cast<Form>().OfType<WaitDialogForm>().ToList();
            foreach (var dlgClose in qCheck)
            {
                dlgClose.Close();
            }





            string sMenuCode = ProcessGeneral.GetSafeString(node.GetValue("MenuCode"));
            string sFormCode = ProcessGeneral.GetSafeString(node.GetValue("FormCode"));


            //  string sProjectCode = DeclareSystem.SysCompanyCode == "02" ? "ScaG_SFC" : ProcessGeneral.GetSafeString(drv["ProjectCode"]);

            string sProjectCode = ProcessGeneral.GetSafeString(node.GetValue("ProjectCode"));

            string sFolderContainForm = ProcessGeneral.GetSafeString(node.GetValue("FolderContainForm"));
            bool bRoleInsert = ProcessGeneral.GetSafeBool(node.GetValue("RoleInsert"));
            bool bRoleUpdate = ProcessGeneral.GetSafeBool(node.GetValue("RoleUpdate"));
            bool bRoleDelete = ProcessGeneral.GetSafeBool(node.GetValue("RoleDelete"));
            bool bRoleView = ProcessGeneral.GetSafeBool(node.GetValue("RoleView"));
            string sAdvanceFunction = ProcessGeneral.GetSafeString(node.GetValue("AdvanceFunction"));
            string sSpecialFunction = ProcessGeneral.GetSafeString(node.GetValue("SpecialFunction"));
            bool bCheckAdvanceFunction = ProcessGeneral.GetSafeBool(node.GetValue("CheckAdvanceFunction"));


            if (sFormCode == "" || sProjectCode == "") return;
            TypeShowForm typeShowForm = ProcessGeneral.GetTypeShowForm(ProcessGeneral.GetSafeString(node.GetValue("ShowCode")));
            string formName = ProcessGeneral.GetSafeString(node.GetValue("FormName"));


            if (typeShowForm == TypeShowForm.ShowMdi)
            {
                ShowFormMdiWhenTreeClick(sFormCode, sProjectCode, sFolderContainForm, sMenuCode, bRoleInsert, bRoleUpdate, bRoleDelete, bRoleView,
                    sAdvanceFunction, sSpecialFunction, bCheckAdvanceFunction, formName);
            }
            else if (typeShowForm == TypeShowForm.ShowNormal)
            {
                ShowFormNormalWhenTreeClick(sFormCode, sProjectCode, sFolderContainForm, sMenuCode, bRoleInsert, bRoleUpdate, bRoleDelete, bRoleView,
                    sAdvanceFunction, sSpecialFunction, bCheckAdvanceFunction, formName);
            }
            else
            {
                ShowFormDialogWhenTreeClick(sFormCode, sProjectCode, sFolderContainForm, sMenuCode, bRoleInsert, bRoleUpdate, bRoleDelete, bRoleView,
                    sAdvanceFunction, sSpecialFunction, bCheckAdvanceFunction, formName);
            }
        }

        private void TreeList_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            int isGuide = ProcessGeneral.GetSafeInt(e.Node.GetValue("GuideDocument"));
            int isProcess = ProcessGeneral.GetSafeInt(e.Node.GetValue("ProcessDocument"));
            if (isGuide == 1 && isProcess == 1)
            {
                e.NodeImageIndex = 5;
                return;
            }
            if (isGuide == 1)
            {
                e.NodeImageIndex = 3;
                return;
            }
            if (isProcess == 1)
            {
                e.NodeImageIndex = 4;
                return;
            }
            if (e.Node.HasChildren)//|| e.Node.ParentNode == null
            {
                e.NodeImageIndex = e.Node.Expanded ? 1 : 0;
            }
            else
            {
                e.NodeImageIndex = 2;
            }
        }








        private void BestFitCol(TreeList tl)
        {
            tl.BestFitColumns();
            tl.Columns["FormName"].Width += 10;
        }

        private int _dockWidth = 0;

        private Int32 GetScrollWidthInTree()
        {
            FieldInfo fi = typeof(TreeList).GetField("scrollInfo", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fi == null)
            {
                return 0;
            }

            var sInfo = (ScrollInfo)fi.GetValue(treeList1);
            Rectangle rect = ((sInfo.VScroll as IScrollBar).ViewInfo.VisibleDecButtonBounds);
            int vSrollWidth = rect.Width;
            return vSrollWidth;
        }
        /// <summary>     Fit Width DockPanel With TreeList
        /// </summary>
        private void ResizeDockPanel()
        {
            dockManagerMain.ForceInitialize();

            int vSrollWidth = GetScrollWidthInTree();
            if (dockPanel1.Visibility == DockVisibility.AutoHide)
            {
                dockPanel1.SetWidthInternal(treeList1.Columns["FormName"].Width + vSrollWidth + 10);
            }
            else
            {
                dockPanel1.Size = new Size(treeList1.Columns["FormName"].Width + vSrollWidth + 10, 0);
            }

            _dockWidth = dockPanel1.Width;
        }
        #endregion




        #region "Show Form TreeList"
        private void ShowFormMdiWhenTreeClick(string sFormCode, string sProjectCode, string sFolderContainForm, string sMenuCode, bool bRoleInsert,
           bool bRoleUpdate, bool bRoleDelete, bool bRoleView, string sAdvanceFunction, string sSpecialFunction, bool bCheckAdvanceFunction, string formName)
        {


            var qForm = this.MdiChildren.Select((p, i) => new
            {
                Index = i,
                FrmMdi = p,
                p.Name,
                p.Text,
                Tag = ProcessGeneral.GetSafeString(p.Tag)
            }).ToList();

            var qCheck = qForm.Where(p => p.Name.Trim().ToUpper() == sFormCode.Trim().ToUpper()).ToList();
            if (!qCheck.Any()) goto finishShow;

            var qBase = qCheck.Where(p => p.FrmMdi is FrmBase).Select(p => new
            {
                p.Index,
                FBase = (FrmBase)p.FrmMdi,
                p.Name,
                p.Text,
            }).ToList();
            if (qBase.Any())
            {
                var qBaseCheck = qBase.Where(p => p.FBase.MenuCodeDb.Trim().ToUpper() == sMenuCode.Trim().ToUpper()).ToList();
                if (!qBaseCheck.Any()) goto finishShow;
                //    ?this.MdiChildren[qBaseCheck.First().Index].Enabled
                //false

                int indexActive = qBaseCheck.First().Index;
                if (this.MdiChildren[indexActive].Enabled)
                    this.MdiChildren[indexActive].Activate();
                return;

            }
            var qNormal = qCheck.Where(p => p.Tag.ToUpper().Trim() == sMenuCode.Trim().ToUpper()).ToList();
            if (!qNormal.Any()) goto finishShow;
            this.MdiChildren[qNormal.First().Index].Activate();
            return;

            finishShow:
            try
            {
                _dlg = new WaitDialogForm("");
                var fFrom = CreateInstanceFormByProject(sProjectCode, sFolderContainForm, sFormCode);
                var fBase = fFrom as FrmBase;
                if (fBase == null)
                {
                    fFrom.Tag = sMenuCode;
                    fFrom.MdiParent = this;
                    fFrom.WindowState = FormWindowState.Normal;
                    fFrom.StartPosition = FormStartPosition.CenterScreen;
                    fFrom.Show();
                }
                else
                {
                    fBase.Tag = sMenuCode;
                    fBase.BbiMainAdd = bbiAdd;
                    fBase.BbiMainEdit = bbiEdit;
                    fBase.BbiMainDelete = bbiDelete;
                    fBase.BbiMainSave = bbiSave;
                    fBase.BbiMainCancel = bbiCancel;
                    fBase.BbiMainRefresh = bbiRefresh;
                    fBase.BbiMainFind = bbiFind;
                    fBase.BbiMainPrint = bbiPrint;
                    fBase.BbiMainPrintDown = bbiPrintDown;
                    fBase.BbiMainExpand = bbiExpand;
                    fBase.BbiMainCollapse = bbiCollapse;
                    fBase.BbiMainClose = bbiClose;
                    fBase.BbiMainRevision = bbiRevision;
                    fBase.BbiMainBreakDown = bbiBreakDown;
                    fBase.BbiMainRangSize = bbiRangSize;
                    fBase.BbiMainCopyObject = bbiCopyObject;
                    fBase.BbiMainGenerate = bbiGenerate;
                    fBase.BbiMainCombine = bbiCombine;
                    fBase.BbiMainCheck = bbiCheck;
                    #region "Function"
                    fBase.BbiMainGFN1 = bbiGFN1;
                    fBase.BbiMainGFN1_F1 = bbiGFN1_F1;
                    fBase.BbiMainGFN1_F2 = bbiGFN1_F2;
                    fBase.BbiMainGFN1_F3 = bbiGFN1_F3;
                    fBase.BbiMainGFN1_F4 = bbiGFN1_F4;
                    fBase.BbiMainGFN1_F5 = bbiGFN1_F5;
                    fBase.BbiMainGFN1_F6 = bbiGFN1_F6;
                    //--------------------------------------------------------------------------------------------------
                    fBase.BbiMainGFN2 = bbiGFN2;
                    fBase.BbiMainGFN2_F1 = bbiGFN2_F1;
                    fBase.BbiMainGFN2_F2 = bbiGFN2_F2;
                    fBase.BbiMainGFN2_F3 = bbiGFN2_F3;
                    fBase.BbiMainGFN2_F4 = bbiGFN2_F4;
                    fBase.BbiMainGFN2_F5 = bbiGFN2_F5;
                    fBase.BbiMainGFN2_F6 = bbiGFN2_F6;
                    //--------------------------------------------------------------------------------------------------
                    fBase.BbiMainGFN3 = bbiGFN3;
                    fBase.BbiMainGFN3_F1 = bbiGFN3_F1;
                    fBase.BbiMainGFN3_F2 = bbiGFN3_F2;
                    fBase.BbiMainGFN3_F3 = bbiGFN3_F3;
                    fBase.BbiMainGFN3_F4 = bbiGFN3_F4;
                    fBase.BbiMainGFN3_F5 = bbiGFN3_F5;
                    fBase.BbiMainGFN3_F6 = bbiGFN3_F6;
                    //--------------------------------------------------------------------------------------------------
                    fBase.BbiMainGFN4 = bbiGFN4;
                    fBase.BbiMainGFN4_F1 = bbiGFN4_F1;
                    fBase.BbiMainGFN4_F2 = bbiGFN4_F2;
                    fBase.BbiMainGFN4_F3 = bbiGFN4_F3;
                    fBase.BbiMainGFN4_F4 = bbiGFN4_F4;
                    fBase.BbiMainGFN4_F5 = bbiGFN4_F5;
                    fBase.BbiMainGFN4_F6 = bbiGFN4_F6;
                    //--------------------------------------------------------------------------------------------------
                    fBase.BbiMainGFN5 = bbiGFN5;
                    fBase.BbiMainGFN5_F1 = bbiGFN5_F1;
                    fBase.BbiMainGFN5_F2 = bbiGFN5_F2;
                    fBase.BbiMainGFN5_F3 = bbiGFN5_F3;
                    fBase.BbiMainGFN5_F4 = bbiGFN5_F4;
                    fBase.BbiMainGFN5_F5 = bbiGFN5_F5;
                    fBase.BbiMainGFN5_F6 = bbiGFN5_F6;
                    //--------------------------------------------------------------------------------------------------
                    fBase.BbiMainFN_1 = bbiFN_1;
                    fBase.BbiMainFN_2 = bbiFN_2;
                    fBase.BbiMainFN_3 = bbiFN_3;
                    fBase.BbiMainFN_4 = bbiFN_4;
                    fBase.BbiMainFN_5 = bbiFN_5;
                    #endregion

                    fBase.AddRightWhenShowForm(sMenuCode, bRoleInsert, bRoleUpdate, bRoleDelete, bRoleView, sAdvanceFunction, sSpecialFunction, bCheckAdvanceFunction, formName);
                    fBase.MdiParent = this;
                    fBase.WindowState = FormWindowState.Normal;
                    fBase.StartPosition = FormStartPosition.CenterScreen;
                    fBase.Show();
                    fBase.Text = formName;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(string.Format("(Form : {0} -- Project Code : {1}) \n{2}", sFormCode, sProjectCode, ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }
            finally
            {
                _dlg.Close();
            }
        }

        private void ShowFormNormalWhenTreeClick(string sFormCode, string sProjectCode, string sFolderContainForm, string sMenuCode, bool bRoleInsert,
          bool bRoleUpdate, bool bRoleDelete, bool bRoleView, string sAdvanceFunction, string sSpecialFunction, bool bCheckAdvanceFunction, string formName)
        {
            var q1 = Application.OpenForms.Cast<Form>().Where(p => p.Name.Trim().ToUpper() == sFormCode.Trim().ToUpper() && !p.IsMdiChild).ToList();
            if (q1.Any())
            {
                Form f = q1.FirstOrDefault();
                if (f != null) f.Activate();
            }
            else
            {
                try
                {
                    _dlg = new WaitDialogForm("");
                    var fFrom = CreateInstanceFormByProject(sProjectCode, sFolderContainForm, sFormCode);
                    var fBase = fFrom as FrmBase;
                    if (fBase == null)
                    {
                        fFrom.WindowState = FormWindowState.Normal;
                        fFrom.StartPosition = FormStartPosition.CenterScreen;
                        _dlg.Close();
                        fFrom.Show();
                    }
                    else
                    {
                        fBase.BbiMainAdd = bbiAdd;
                        fBase.BbiMainEdit = bbiEdit;
                        fBase.BbiMainDelete = bbiDelete;
                        fBase.BbiMainSave = bbiSave;
                        fBase.BbiMainCancel = bbiCancel;
                        fBase.BbiMainRefresh = bbiRefresh;
                        fBase.BbiMainFind = bbiFind;
                        fBase.BbiMainPrint = bbiPrint;
                        fBase.BbiMainPrintDown = bbiPrintDown;
                        fBase.BbiMainExpand = bbiExpand;
                        fBase.BbiMainCollapse = bbiCollapse;
                        fBase.BbiMainClose = bbiClose;
                        fBase.BbiMainRevision = bbiRevision;
                        fBase.BbiMainBreakDown = bbiBreakDown;
                        fBase.BbiMainRangSize = bbiRangSize;
                        fBase.BbiMainCopyObject = bbiCopyObject;
                        fBase.BbiMainGenerate = bbiGenerate;
                        fBase.BbiMainCombine = bbiCombine;
                        fBase.BbiMainCheck = bbiCheck;
                        #region "Function"
                        fBase.BbiMainGFN1 = bbiGFN1;
                        fBase.BbiMainGFN1_F1 = bbiGFN1_F1;
                        fBase.BbiMainGFN1_F2 = bbiGFN1_F2;
                        fBase.BbiMainGFN1_F3 = bbiGFN1_F3;
                        fBase.BbiMainGFN1_F4 = bbiGFN1_F4;
                        fBase.BbiMainGFN1_F5 = bbiGFN1_F5;
                        fBase.BbiMainGFN1_F6 = bbiGFN1_F6;
                        //--------------------------------------------------------------------------------------------------
                        fBase.BbiMainGFN2 = bbiGFN2;
                        fBase.BbiMainGFN2_F1 = bbiGFN2_F1;
                        fBase.BbiMainGFN2_F2 = bbiGFN2_F2;
                        fBase.BbiMainGFN2_F3 = bbiGFN2_F3;
                        fBase.BbiMainGFN2_F4 = bbiGFN2_F4;
                        fBase.BbiMainGFN2_F5 = bbiGFN2_F5;
                        fBase.BbiMainGFN2_F6 = bbiGFN2_F6;
                        //--------------------------------------------------------------------------------------------------
                        fBase.BbiMainGFN3 = bbiGFN3;
                        fBase.BbiMainGFN3_F1 = bbiGFN3_F1;
                        fBase.BbiMainGFN3_F2 = bbiGFN3_F2;
                        fBase.BbiMainGFN3_F3 = bbiGFN3_F3;
                        fBase.BbiMainGFN3_F4 = bbiGFN3_F4;
                        fBase.BbiMainGFN3_F5 = bbiGFN3_F5;
                        fBase.BbiMainGFN3_F6 = bbiGFN3_F6;
                        //--------------------------------------------------------------------------------------------------
                        fBase.BbiMainGFN4 = bbiGFN4;
                        fBase.BbiMainGFN4_F1 = bbiGFN4_F1;
                        fBase.BbiMainGFN4_F2 = bbiGFN4_F2;
                        fBase.BbiMainGFN4_F3 = bbiGFN4_F3;
                        fBase.BbiMainGFN4_F4 = bbiGFN4_F4;
                        fBase.BbiMainGFN4_F5 = bbiGFN4_F5;
                        fBase.BbiMainGFN4_F6 = bbiGFN4_F6;
                        //--------------------------------------------------------------------------------------------------
                        fBase.BbiMainGFN5 = bbiGFN5;
                        fBase.BbiMainGFN5_F1 = bbiGFN5_F1;
                        fBase.BbiMainGFN5_F2 = bbiGFN5_F2;
                        fBase.BbiMainGFN5_F3 = bbiGFN5_F3;
                        fBase.BbiMainGFN5_F4 = bbiGFN5_F4;
                        fBase.BbiMainGFN5_F5 = bbiGFN5_F5;
                        fBase.BbiMainGFN5_F6 = bbiGFN5_F6;
                        //--------------------------------------------------------------------------------------------------
                        fBase.BbiMainFN_1 = bbiFN_1;
                        fBase.BbiMainFN_2 = bbiFN_2;
                        fBase.BbiMainFN_3 = bbiFN_3;
                        fBase.BbiMainFN_4 = bbiFN_4;
                        fBase.BbiMainFN_5 = bbiFN_5;
                        #endregion
                        fBase.AddRightWhenShowForm(sMenuCode, bRoleInsert, bRoleUpdate, bRoleDelete, bRoleView, sAdvanceFunction, sSpecialFunction, bCheckAdvanceFunction, formName);
                        fBase.WindowState = FormWindowState.Normal;
                        fBase.StartPosition = FormStartPosition.CenterScreen;
                        _dlg.Close();
                        fBase.Show();
                        fBase.Text = formName;
                    }

                }
                catch (Exception ex)
                {
                    _dlg.Close();
                    XtraMessageBox.Show(string.Format("(Form : {0} -- Project Code : {1}) \n{2}", sFormCode, sProjectCode, ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                }
            }

        }

        private void ShowFormDialogWhenTreeClick(string sFormCode, string sProjectCode, string sFolderContainForm, string sMenuCode, bool bRoleInsert,
         bool bRoleUpdate, bool bRoleDelete, bool bRoleView, string sAdvanceFunction, string sSpecialFunction, bool bCheckAdvanceFunction, string formName)
        {

            try
            {
                _dlg = new WaitDialogForm("");
                var fFrom = CreateInstanceFormByProject(sProjectCode, sFolderContainForm, sFormCode);
                var fBase = fFrom as FrmBase;
                if (fBase == null)
                {
                    fFrom.WindowState = FormWindowState.Normal;
                    fFrom.StartPosition = FormStartPosition.CenterScreen;
                    _dlg.Close();
                    fFrom.ShowDialog();
                }
                else
                {
                    fBase.BbiMainAdd = bbiAdd;
                    fBase.BbiMainEdit = bbiEdit;
                    fBase.BbiMainDelete = bbiDelete;
                    fBase.BbiMainSave = bbiSave;
                    fBase.BbiMainCancel = bbiCancel;
                    fBase.BbiMainRefresh = bbiRefresh;
                    fBase.BbiMainFind = bbiFind;
                    fBase.BbiMainPrint = bbiPrint;
                    fBase.BbiMainPrintDown = bbiPrintDown;
                    fBase.BbiMainExpand = bbiExpand;
                    fBase.BbiMainCollapse = bbiCollapse;
                    fBase.BbiMainClose = bbiClose;
                    fBase.BbiMainRevision = bbiRevision;
                    fBase.BbiMainBreakDown = bbiBreakDown;
                    fBase.BbiMainRangSize = bbiRangSize;
                    fBase.BbiMainCopyObject = bbiCopyObject;
                    fBase.BbiMainGenerate = bbiGenerate;
                    fBase.BbiMainCombine = bbiCombine;
                    fBase.BbiMainCheck = bbiCheck;
                    #region "Function"
                    fBase.BbiMainGFN1 = bbiGFN1;
                    fBase.BbiMainGFN1_F1 = bbiGFN1_F1;
                    fBase.BbiMainGFN1_F2 = bbiGFN1_F2;
                    fBase.BbiMainGFN1_F3 = bbiGFN1_F3;
                    fBase.BbiMainGFN1_F4 = bbiGFN1_F4;
                    fBase.BbiMainGFN1_F5 = bbiGFN1_F5;
                    fBase.BbiMainGFN1_F6 = bbiGFN1_F6;
                    //--------------------------------------------------------------------------------------------------
                    fBase.BbiMainGFN2 = bbiGFN2;
                    fBase.BbiMainGFN2_F1 = bbiGFN2_F1;
                    fBase.BbiMainGFN2_F2 = bbiGFN2_F2;
                    fBase.BbiMainGFN2_F3 = bbiGFN2_F3;
                    fBase.BbiMainGFN2_F4 = bbiGFN2_F4;
                    fBase.BbiMainGFN2_F5 = bbiGFN2_F5;
                    fBase.BbiMainGFN2_F6 = bbiGFN2_F6;
                    //--------------------------------------------------------------------------------------------------
                    fBase.BbiMainGFN3 = bbiGFN3;
                    fBase.BbiMainGFN3_F1 = bbiGFN3_F1;
                    fBase.BbiMainGFN3_F2 = bbiGFN3_F2;
                    fBase.BbiMainGFN3_F3 = bbiGFN3_F3;
                    fBase.BbiMainGFN3_F4 = bbiGFN3_F4;
                    fBase.BbiMainGFN3_F5 = bbiGFN3_F5;
                    fBase.BbiMainGFN3_F6 = bbiGFN3_F6;
                    //--------------------------------------------------------------------------------------------------
                    fBase.BbiMainGFN4 = bbiGFN4;
                    fBase.BbiMainGFN4_F1 = bbiGFN4_F1;
                    fBase.BbiMainGFN4_F2 = bbiGFN4_F2;
                    fBase.BbiMainGFN4_F3 = bbiGFN4_F3;
                    fBase.BbiMainGFN4_F4 = bbiGFN4_F4;
                    fBase.BbiMainGFN4_F5 = bbiGFN4_F5;
                    fBase.BbiMainGFN4_F6 = bbiGFN4_F6;
                    //--------------------------------------------------------------------------------------------------
                    fBase.BbiMainGFN5 = bbiGFN5;
                    fBase.BbiMainGFN5_F1 = bbiGFN5_F1;
                    fBase.BbiMainGFN5_F2 = bbiGFN5_F2;
                    fBase.BbiMainGFN5_F3 = bbiGFN5_F3;
                    fBase.BbiMainGFN5_F4 = bbiGFN5_F4;
                    fBase.BbiMainGFN5_F5 = bbiGFN5_F5;
                    fBase.BbiMainGFN5_F6 = bbiGFN5_F6;
                    //--------------------------------------------------------------------------------------------------
                    fBase.BbiMainFN_1 = bbiFN_1;
                    fBase.BbiMainFN_2 = bbiFN_2;
                    fBase.BbiMainFN_3 = bbiFN_3;
                    fBase.BbiMainFN_4 = bbiFN_4;
                    fBase.BbiMainFN_5 = bbiFN_5;
                    #endregion
                    fBase.AddRightWhenShowForm(sMenuCode, bRoleInsert, bRoleUpdate, bRoleDelete, bRoleView, sAdvanceFunction, sSpecialFunction, bCheckAdvanceFunction, formName);

                    fBase.WindowState = FormWindowState.Normal;
                    fBase.StartPosition = FormStartPosition.CenterScreen;
                    _dlg.Close();
                    fBase.ShowDialog();
                    fBase.Text = formName;
                }

            }
            catch (Exception ex)
            {
                _dlg.Close();
                XtraMessageBox.Show(string.Format("(Form : {0} -- Project Code : {1}) \n{2}", sFormCode, sProjectCode, ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            }



        }
        #endregion

        #region "button click event"

        private void bbiConfigSQLServer_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                DialogResult dlResult = XtraMessageBox.Show("Do you want to perform config sql server \nfor setting database working program?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlResult == DialogResult.Yes)
                {
                    _isConfigSql = true;
                    ShowFormConfigSql();
                }

            }
            else
            {
                ShowFormConfigSql();
            }
        }
        private void bbiBackupDatabase_ItemClick(object sender, ItemClickEventArgs e)
        {
            var f = new SaveFileDialog()
            {
                Title = @"Select file for saving backup",
                Filter = @"Backup Files (*.bak)|*.bak",
                RestoreDirectory = true
            };



            if (f.ShowDialog() != DialogResult.OK) return;
            string pathBackup = f.FileName;
            if (string.IsNullOrEmpty(pathBackup)) return;



            _dlg = new WaitDialogForm("");




            bool isSuccess = true;

            SqlConnection sqlConn = new SqlConnection(SystemProperty.SysConnectionString);
            Backup dbBackup = null;
            Server sqlServer = new Server(new ServerConnection(sqlConn));
            try
            {


                string pathTemp = SystemProperty.UseFTPServer ? SystemProperty.BackupDatabasePathFtp : SystemProperty.BackupDatabasePath;
                string fileName = string.Format("{0}{1:ddMMyyyyhhmmss}.bak", SystemProperty.SysDataBaseName, DateTime.Now);
                string pathBackupNew = string.Format("{0}\\{1}", pathTemp, fileName);

                dbBackup = new Backup
                {
                    Action = BackupActionType.Database,
                    Database = SystemProperty.SysDataBaseName,
                    BackupSetName = string.Format("{0} backup set.", SystemProperty.SysDataBaseName),
                    BackupSetDescription = string.Format("Database: {0}. Date: {1:ddMMyyyyhhmmss}.", SystemProperty.SysDataBaseName, DateTime.Now),
                    MediaDescription = "Disk",
                    Initialize = true,
                    Checksum = true,
                };



                //      dbBackup.De


                BackupDeviceItem device = new BackupDeviceItem(pathBackupNew, DeviceType.File);
                dbBackup.Devices.Add(device);
                string scriptBackup = dbBackup.Script(sqlServer);
                Console.WriteLine(scriptBackup);
                dbBackup.SqlBackup(sqlServer);

                if (!SystemProperty.UseFTPServer)
                {
                    CopyFileAfterBackup(pathBackupNew, pathBackup);

                }
                else
                {
                    CopyFileFormFtpServer(fileName, pathBackup);
                }

            }
            catch (Exception ex)
            {
                isSuccess = false;
                if (dbBackup != null)
                    dbBackup.Abort();
                if (sqlConn.State != ConnectionState.Closed) { sqlConn.Close(); }
                _dlg.Close();
                XtraMessageBox.Show(string.Format("Exception occured.\nMessage: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!isSuccess) return;

            sqlConn.Close();
            _dlg.Close();

            XtraMessageBox.Show("Backup complete.", "Information", MessageBoxButtons.OK,
                MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            //      progBar.Visible = false;
        }



        private void CopyFileFormFtpServer(string fileName, string newFile)
        {

            try
            {
                FtpWebRequest requestFile = (FtpWebRequest)WebRequest.Create(SystemProperty.PathFtpBackupDatabaseTemp + "\\" + fileName);
                requestFile.Credentials = new NetworkCredential(SystemProperty.FtpUser, SystemProperty.FtpPass);


                requestFile.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse responseFileDownload = (FtpWebResponse)requestFile.GetResponse();



                Stream responseStream = responseFileDownload.GetResponseStream();
                if (responseStream == null) return;


                if (File.Exists(newFile))
                {
                    File.Delete(newFile);
                }



                FileStream writeStream = new FileStream(newFile, FileMode.Create);

                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);

                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }

                responseStream.Close();
                writeStream.Close();




                DeleteFileFromFtpServer(fileName);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(string.Format("Exception occured.\nMessage: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }









            //requestFileDownload = null;
            //responseFileDownload = null;
        }


        private void DeleteFileFromFtpServer(string fileName)
        {
            try
            {

                FtpWebRequest requestFile = (FtpWebRequest)WebRequest.Create(SystemProperty.PathFtpBackupDatabaseTemp + "\\" + fileName);
                requestFile.Credentials = new NetworkCredential(SystemProperty.FtpUser, SystemProperty.FtpPass);
                requestFile.Method = WebRequestMethods.Ftp.DeleteFile;

                requestFile.GetResponse();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(string.Format("Exception occured.\nMessage: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void CopyFileAfterBackup(string oldFile, string newFile)
        {
            FileInfo f1 = new FileInfo(oldFile);
            if (!f1.Exists) return;
            try
            {
                if (File.Exists(newFile))
                {
                    File.Delete(newFile);
                }
                f1.MoveTo(newFile);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(string.Format("Exception occured.\nMessage: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void bbiLogout_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (((DataTable)treeList1.DataSource).Rows.Count > 0 && this.MdiChildren.Length > 0)
            {
                DialogResult dlResult = XtraMessageBox.Show("Are you sure to logout ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlResult == DialogResult.Yes)
                {
                    _isLogout = true;
                    ShowFormLogin();
                }
            }
            else
            {
                _isLogout = true;
                ShowFormLogin();
            }

        }
        private void bbiHide_ItemClick(object sender, ItemClickEventArgs e)
        {
            MinimizeToTray();
        }

        private void bbiCloseAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                DialogResult dlResult = XtraMessageBox.Show("Do you want to close all opening forms ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlResult != DialogResult.Yes) return;
                foreach (var frmChild in this.MdiChildren)
                {
                    frmChild.Close();
                }
            }
        }

        private void bbiExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        private void bbiColorMixer_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var showColorMixer = new ColorWheelForm())
            {
                showColorMixer.StartPosition = FormStartPosition.CenterScreen; showColorMixer.ShowDialog(this);
            }
        }
        private void bvcmdExit_ItemClick(object sender, BackstageViewItemEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }
        private void btnApplyPermission_Click(object sender, EventArgs e)
        {
            if (lookupUserGroup.Properties.DataSource == null || lookUpFuntionGroup.Properties.DataSource == null)
            {
                return;
            }
            var drvUg = lookupUserGroup.Properties.GetDataSourceRowByKeyValue(lookupUserGroup.EditValue) as DataRowView;
            var drvPg = lookUpFuntionGroup.Properties.GetDataSourceRowByKeyValue(lookUpFuntionGroup.EditValue) as DataRowView;

            if (drvUg == null || drvPg == null) return;
            if (ProcessGeneral.GetSafeInt64(drvUg.Row["UserInGroupID"]) <= 0 || ProcessGeneral.GetSafeInt64(drvPg.Row["IDAuthorization"]) <= 0) return;
            SystemProperty.SysUserInGroupId = ProcessGeneral.GetSafeInt64(drvUg.Row["UserInGroupID"]);
            SystemProperty.SysIdAuthorization = ProcessGeneral.GetSafeInt64(drvPg.Row["IDAuthorization"]);
            SystemProperty.SysUserGroupCode = ProcessGeneral.GetSafeString(drvUg.Row["GroupUserCode"]);
            SystemProperty.SysPermissionGroupCode = ProcessGeneral.GetSafeString(drvPg.Row["PermisionGroupCode"]);
            LoadTreeViewMenuFinal(treeList1);
            XtraMessageBox.Show("You have changed the right in system successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (ProcessGeneral.GetSafeString(txtCurrentPass.EditValue) == "")
            {
                XtraMessageBox.Show("TextBox Current Pass Could not empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCurrentPass.Select();
                return;
            }
            if (ProcessGeneral.GetSafeString(txtNewPass.EditValue) == "")
            {
                XtraMessageBox.Show("TextBox New Pass Could not empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNewPass.Select();
                return;
            }
            if (ProcessGeneral.GetSafeString(txtConfirmNewPass.EditValue) == "")
            {
                XtraMessageBox.Show("TextBox Confirm New Pass Could not empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfirmNewPass.Select();
                return;
            }
            if (ProcessGeneral.GetSafeString(txtCurrentPass.EditValue) != SystemProperty.SysPassword)
            {
                XtraMessageBox.Show("Current Pass Incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCurrentPass.Select();
                return;
            }
            if (ProcessGeneral.GetSafeString(txtNewPass.EditValue) != ProcessGeneral.GetSafeString(txtConfirmNewPass.EditValue))
            {
                XtraMessageBox.Show("TextBox Confirm New Pass does not match with TextBox New Pass.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfirmNewPass.Select();
                return;
            }
            SystemProperty.SysChangePassDate = _ctrl.UpdateDatePasswordWhenUserChangePass(SystemProperty.SysUserId, EnDeCrypt.Encrypt(ProcessGeneral.GetSafeString(txtConfirmNewPass.Text.Trim()), true), txtConfirmNewPass.Text.Trim());
            lblChangePassDate.Text = SystemProperty.SysChangePassDate;
            XtraMessageBox.Show("Change Pass Successfull.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtCurrentPass.EditValue = "";
            txtNewPass.EditValue = "";
            txtConfirmNewPass.EditValue = "";

        }
        private void btnSavePaging_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("Config Successfull.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SystemProperty.SysItemsPaging = Convert.ToInt32(spinPaging.EditValue);
            Settings.Default.ItemPerPage = Convert.ToInt32(spinPaging.EditValue);
            Settings.Default.Save();


        }

        private void bbiHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;
            if (focusedNode == null) return;
            string menuCode = ProcessGeneral.GetSafeString(focusedNode.GetValue("MenuCode"));
            ProcessGeneral.OpenHelpForm(menuCode, true);
        }



        private void bbiAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            var f = new FrmAboutForm();
            f.ShowDialog();
        }


        private void bbiProgressBar_ItemClick(object sender, ItemClickEventArgs e)
        {
            var f = new FrmDemoProgressBar();
            f.ShowDialog();
        }

        #endregion

        #region "Login"




        private void LoadTreeViewMenuFinal(TreeList tl)
        {
            tl.OptionsView.ShowColumns = true;
            DataTable dtTree = _ctrl.LoadTreeViewMainMenu(SystemProperty.SysIdAuthorization, SystemProperty.SysUserInGroupId);



            DataTable dtTemp = dtTree.Clone();
            foreach (DataRow drTree in dtTree.Rows)
            {
                dtTemp.ImportRow(drTree);
            }

            SystemProperty.SysTablePermissionMainForm = dtTemp;

            tl.Columns.Clear();
            tl.DataSource = null;
            tl.DataSource = dtTree;
            tl.ParentFieldName = "MenuParent";
            tl.KeyFieldName = "MenuChild";



            tl.BeginUpdate();
            ProcessGeneral.HideVisibleColumnsTreeList(tl, false, "MenuCode", "FormCode", "ProjectCode", "FolderContainForm", "RoleInsert", "RoleUpdate", "RoleDelete",
                "RoleView", "AdvanceFunction", "Level", "SortOrder", "Visible"
                , "SpecialFunction", "CheckAdvanceFunction", "ProcessDocument", "GuideDocument", "ShowCode");

            ProcessGeneral.SetTreeListColumnHeader(tl.Columns["FormName"], "Form Name", false, HorzAlignment.Near, TreeFixedStyle.None, "");
            tl.ExpandAll();

            tl.ForceInitialize();
            tl.BeginSort();
            tl.Columns["SortOrder"].SortOrder = SortOrder.Ascending;
            tl.EndSort();
            tl.EndUpdate();
            tl.OptionsView.ShowColumns = false;
            GetNodeDelete(tl);


        }


        private void GetNodeDelete(TreeList tl)
        {

            tl.BeginUpdate();
            tl.LockReloadNodes();
            _lNodeDelete.Clear();
            foreach (TreeListNode node in tl.Nodes)
            {

                GetNodeGoDown(node);
            }

            if (_lNodeDelete.Count > 0)
            {
                List<TreeListNode> lNode = new List<TreeListNode>();
                foreach (TreeListNode nodeDel in _lNodeDelete)
                {
                    TreeListNode nodeAdd = GetNodeDelete(nodeDel);
                    if (nodeAdd == null || nodeAdd == nodeDel) continue;
                    lNode.Add(nodeAdd);
                }
                foreach (TreeListNode nodeD in _lNodeDelete)
                {
                    tl.DeleteNode(nodeD);
                }
                _lNodeDelete.Clear();
                foreach (TreeListNode nodeF in lNode)
                {
                    tl.DeleteNode(nodeF);
                }
            }
            tl.UnlockReloadNodes();
            BestFitCol(tl);
            tl.EndUpdate();



        }


        private TreeListNode GetNodeDelete(TreeListNode nodeDel)
        {

            TreeListNode parentNode = nodeDel.ParentNode;
            if (parentNode == null) return nodeDel;
            string check = ProcessGeneral.GetSafeString(parentNode.Tag);
            if (check == "Check")
            {
                return null;
            }
            parentNode.Tag = "Check";
            bool beforValue1 = ProcessGeneral.GetSafeBool(parentNode.GetValue("Visible"));
            if (!beforValue1)
            {
                return GetNodeDelete(parentNode);
            }
            return nodeDel;




        }


        private void GetNodeGoDown(TreeListNode tlNode)
        {

            bool visibleL1 = ProcessGeneral.GetSafeBool(tlNode.GetValue("Visible"));

            if (tlNode.Nodes.Count > 0)
            {
                foreach (TreeListNode node in tlNode.Nodes)
                {
                    bool visibleL2 = ProcessGeneral.GetSafeBool(node.GetValue("Visible"));
                    if (visibleL2)
                        tlNode.SetValue("Visible", true);

                    if (node.Nodes.Count > 0)
                    {
                        foreach (TreeListNode childNode in node.Nodes)
                        {
                            bool visibleL3 = ProcessGeneral.GetSafeBool(childNode.GetValue("Visible"));
                            if (visibleL3)
                            {
                                node.SetValue("Visible", true);
                            }
                            if (childNode.Nodes.Count > 0)
                            {
                                GetNodeGoDown(childNode);
                            }
                            else
                            {


                                if (!visibleL3)
                                    _lNodeDelete.Add(childNode);
                                GetNodeGoUp(childNode.ParentNode, visibleL3);
                            }




                        }
                    }
                    else
                    {
                        if (!visibleL2)
                            _lNodeDelete.Add(node);
                        GetNodeGoUp(node.ParentNode, visibleL2);
                    }

                }
            }
            else
            {
                if (!visibleL1)
                    _lNodeDelete.Add(tlNode);
                GetNodeGoUp(tlNode.ParentNode, visibleL1);
            }


        }
        private void GetNodeGoUp(TreeListNode tlNode, bool beforValue)
        {
            if (tlNode == null) return;
            bool currentValue = ProcessGeneral.GetSafeBool(tlNode.GetValue("Visible"));
            string currentTag = ProcessGeneral.GetSafeString(tlNode.Tag);
            if (currentValue && currentTag == "Visible") return;
            TreeListNode parentNode = tlNode.ParentNode;
            if (parentNode == null) return;
            if (beforValue)
            {
                parentNode.SetValue("Visible", true);
                parentNode.Tag = "Visible";
            }
            bool beforValue1 = ProcessGeneral.GetSafeBool(parentNode.GetValue("Visible"));
            GetNodeGoUpDetail(parentNode.ParentNode, beforValue1);


        }

        private void GetNodeGoUpDetail(TreeListNode currentNode, bool beforValue)
        {
            if (currentNode == null) return;
            bool currentValue = ProcessGeneral.GetSafeBool(currentNode.GetValue("Visible"));
            string currentTag = ProcessGeneral.GetSafeString(currentNode.Tag);
            if (currentValue && currentTag == "Visible") return;



            if (beforValue)
            {
                currentNode.SetValue("Visible", true);
                currentNode.Tag = "Visible";
            }
            bool beforValue1 = ProcessGeneral.GetSafeBool(currentNode.GetValue("Visible"));
            GetNodeGoUpDetail(currentNode.ParentNode, beforValue1);


        }        /// <summary>
                 /// Sau khi đăng nhập, hiện menu, thay đổi nút đăng nhập -> đăng xuất
                 /// </summary>
        private void LoadInfoAfterLogon()
        {
            _performEventCombobox = false;
            ProcessGeneral.VisibleMenuParentForm(this);
            dockPanel1.Visibility = DockVisibility.Visible;
            LoadTreeViewMenuFinal(treeList1);

            ResizeDockPanel();
            this.lblChangePassDate.Text = SystemProperty.SysChangePassDate;
            this.lblUsername.Text = SystemProperty.SysUserName;
            this.lblFullname.Text = SystemProperty.SysFullName;
            this.lblEmail.Text = SystemProperty.SysEmail;
            txtEmailPass.EditValue = SystemProperty.SysDefaultSendMailPass;
            this.lblDepartMent.Text = SystemProperty.SysDepartmentName;
            this.lblPositions.Text = SystemProperty.SysPositionsName;
            this.barStaticI_User.Caption = String.Format("User [{0}]", SystemProperty.SysUserName);
            this.bStaticI_Machine.Caption = String.Format("Machine [{0}]", SystemProperty.SysMachineName);
            this.barStaticI_Date.Caption = String.Format("[{0}]", DateTime.Now.Date.ToLongDateString());
            LoadComboUserGroup(SystemProperty.SysDtCbUserGroup);
            if (((DataTable)lookupUserGroup.Properties.DataSource).Rows.Count > 0)
            {
                lookupUserGroup.EditValue = SystemProperty.SysUserInGroupId;
            }
            SystemProperty.SysDtCbPermission = _ctrl.LoadCbFuncionGroupWhenLogin(SystemProperty.SysUserGroupCode);
            LoadComboFunctionGroup(SystemProperty.SysDtCbPermission);
            if (SystemProperty.SysDtCbPermission.Rows.Count > 0)
            {
                lookUpFuntionGroup.EditValue = SystemProperty.SysIdAuthorization;
            }
            _currentTimeLogin = DateTime.Now;
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
            }
            _performEventCombobox = true;







        }





        #endregion

        #region "Process Grid BOM"




        private void GridViewWBOMCustomInit(DataTable dtInit)
        {



            gcNMTBOM.UseEmbeddedNavigator = true;

            gcNMTBOM.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcNMTBOM.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcNMTBOM.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcNMTBOM.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcNMTBOM.EmbeddedNavigator.Buttons.Remove.Visible = false;


            //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvNMTBOM.OptionsBehavior.Editable = true;
            gvNMTBOM.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            gvNMTBOM.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            gvNMTBOM.OptionsCustomization.AllowColumnMoving = false;
            gvNMTBOM.OptionsCustomization.AllowQuickHideColumns = true;
            gvNMTBOM.OptionsView.RowAutoHeight = true;
            gvNMTBOM.OptionsCustomization.AllowSort = true;

            gvNMTBOM.OptionsCustomization.AllowFilter = false;


            gvNMTBOM.OptionsView.ShowGroupPanel = false;
            gvNMTBOM.OptionsView.ShowIndicator = true;
            gvNMTBOM.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvNMTBOM.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvNMTBOM.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gvNMTBOM.OptionsView.ShowAutoFilterRow = false;
            gvNMTBOM.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            gvNMTBOM.OptionsView.ColumnAutoWidth = false;

            //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            gvNMTBOM.OptionsNavigation.AutoFocusNewRow = true;
            gvNMTBOM.OptionsNavigation.UseTabKey = true;

            gvNMTBOM.OptionsSelection.MultiSelect = true;
            gvNMTBOM.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            gvNMTBOM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            gvNMTBOM.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvNMTBOM.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvNMTBOM.OptionsView.EnableAppearanceEvenRow = false;
            gvNMTBOM.OptionsView.EnableAppearanceOddRow = false;
            gvNMTBOM.OptionsView.ShowFooter = false;

            gvNMTBOM.OptionsHint.ShowFooterHints = false;
            gvNMTBOM.OptionsHint.ShowCellHints = false;
            //   gridView1.RowHeight = 25;

            gvNMTBOM.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;

            gvNMTBOM.OptionsFind.AllowFindPanel = false;


            // gvNMTBOM.Images = ProcessGeneral.SetUpImageList(new Size(16, 16), Resources.reverssort_16x16);

            new MyFindPanelFilterHelper(gvNMTBOM)
            {
                AllowSort = true,
                IsPerFormEvent = true,
                IsBestFitDoubleClick = true,
                IsDrawFilter = true,
            };



            GridColumn[] arrGridCol = new GridColumn[dtInit.Columns.Count];
            int ind = -1;

            #region "Init Column"


            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Item Type",
                FieldName = "ItemType",
                Name = "ItemType",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };
            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Item Code",
                FieldName = "RMCode_001",
                Name = "RMCode_001",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                ColumnEdit = _repositoryTextGrid,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };
            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Item Name",
                FieldName = "RMDescription_002",
                Name = "RMDescription_002",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };
            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Reference",
                FieldName = "Reference",
                Name = "Reference",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },//  ColumnEdit = repositoryText,
                //    DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };


            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Customer",
                FieldName = "SearchName",
                Name = "SearchName",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //    DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };










            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Group",
                FieldName = "Group",
                Name = "Group",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Near, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };


            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Target Date",
                FieldName = "TargetDate",
                Name = "TargetDate",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };



            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Effect Date",
                FieldName = "EffectiveDate",
                Name = "EffectiveDate",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };


            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Expire Date",
                FieldName = "ExpireDate",
                Name = "ExpireDate",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };




            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Created Date",
                FieldName = "CreatedDate",
                Name = "CreatedDate",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                // DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };

            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Created By",
                FieldName = "CreatedUser",
                Name = "CreatedUser",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };
            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Adjusted Date",
                FieldName = "UpdatedDate",
                Name = "UpdatedDate",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };
            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Adjusted By",
                FieldName = "UpdatedUser",
                Name = "UpdatedUser",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = true, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = "", FormatType = FormatType.Numeric, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };

            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"TDG00001PK",
                FieldName = "TDG00001PK",
                Name = "TDG00001PK",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = false, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };


            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Priority",
                FieldName = "Priority",
                Name = "Priority",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = false, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };



            arrGridCol[++ind] = new GridColumn
            {
                Caption = @"Main Response",
                FieldName = "MainResponse",
                Name = "MainResponse",
                Visible = true,
                VisibleIndex = ind,
                Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None,
                OptionsColumn = { AllowMerge = DefaultBoolean.False, ShowInCustomizationForm = false, },
                OptionsFilter = { AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains },
                //  ColumnEdit = repositoryText,
                //  DisplayFormat = { FormatString = ConstSystem.SysDateFormat, FormatType = FormatType.DateTime, },
                AppearanceHeader = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                AppearanceCell = { Options = { UseTextOptions = true, }, TextOptions = { HAlignment = HorzAlignment.Center, }, },
                //SummaryItem = { SummaryType = SummaryItemType.Count, DisplayFormat = @"Sum :", },
            };



            #endregion

            gvNMTBOM.Columns.AddRange(arrGridCol);
            //     
            ProcessGeneral.HideVisibleColumnsGridView(gvNMTBOM, false, "TDG00001PK", "Priority");


            gvNMTBOM.ShowingEditor += gvNMTBOM_ShowingEditor;

            gvNMTBOM.LeftCoordChanged += gvNMTBOM_LeftCoordChanged;
            gvNMTBOM.MouseMove += gvNMTBOM_MouseMove;
            gvNMTBOM.TopRowChanged += gvNMTBOM_TopRowChanged;
            gvNMTBOM.FocusedColumnChanged += GvNMTBOM_FocusedColumnChanged;
            gcNMTBOM.Paint += gcNMTBOM_Paint;


            gvNMTBOM.RowCellStyle += gvNMTBOM_RowCellStyle;

            gvNMTBOM.CustomColumnDisplayText += GvNMTBOM_CustomColumnDisplayText;

            gcNMTBOM.ForceInitialize();



        }

        private void GvNMTBOM_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void GvNMTBOM_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "TargetDate":
                case "EffectiveDate":
                case "ExpireDate":
                case "CreatedDate":
                case "UpdatedDate":
                    if (e.DisplayText.Contains("1900"))
                    {
                        e.DisplayText = "";
                    }
                    break;
            }
        }





        #region "GridView Event"



        private void gvNMTBOM_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }


        private void gvNMTBOM_RowCellStyle(object sender, RowCellStyleEventArgs e)
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


                case "Reference":
                    {


                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                    break;
                case "EffectiveDate":
                case "ExpireDate":
                    {

                        e.Appearance.ForeColor = Color.DarkBlue;
                    }
                    break;
            }






        }

        private void gvNMTBOM_LeftCoordChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvNMTBOM_MouseMove(object sender, MouseEventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }

        private void gvNMTBOM_TopRowChanged(object sender, EventArgs e)//draw rectangle cell secltion
        {
            GridView gv = (GridView)sender;
            if (gv == null) return;
            DrawRectangleSelection.RePaintGridView(((GridView)sender));
        }




        private void gcNMTBOM_Paint(object sender, PaintEventArgs e)//draw rectangle cell secltion
        {
            GridControl gc = (GridControl)sender;
            if (gc == null) return;
            DrawRectangleSelection.PaintGridViewSelectionRect(gc, e);
        }

















        #endregion

        private DataTable TableBOMDetailTemp()
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("ItemType", typeof(string));
            dtTemp.Columns.Add("RMCode_001", typeof(string));
            dtTemp.Columns.Add("RMDescription_002", typeof(string));
            dtTemp.Columns.Add("Reference", typeof(string));
            dtTemp.Columns.Add("SearchName", typeof(string));
            dtTemp.Columns.Add("Group", typeof(string));
            dtTemp.Columns.Add("TargetDate", typeof(string));
            dtTemp.Columns.Add("EffectiveDate", typeof(string));
            dtTemp.Columns.Add("ExpireDate", typeof(string));
            dtTemp.Columns.Add("CreatedDate", typeof(string));
            dtTemp.Columns.Add("CreatedUser", typeof(string));
            dtTemp.Columns.Add("UpdatedDate", typeof(string));
            dtTemp.Columns.Add("UpdatedUser", typeof(string));
            dtTemp.Columns.Add("TDG00001PK", typeof(Int64));
            dtTemp.Columns.Add("Priority", typeof(int));
            dtTemp.Columns.Add("MainResponse", typeof(bool));
            return dtTemp;
        }







        #endregion


        #region "Timer Event"

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_pProc == 100)
            {
                _pProc = 0;
            }
            else
            {
                beiProcess.EditValue = _pProc;
            }
            _pProc++;

            //   this.barStaticI_CPU.Caption = string.Format("CPU Usage: {0}%",Convert.ToInt32(GetCPUCounter()));
            if (!SystemProperty.SysOsName.Trim().ToUpper().Contains("WINDOWS XP"))
            {
                this.bsiRAM.Caption = string.Format("Ram [{0}]", ProcessGeneral.ConvertFileSizeToString(Program.GetRAMCounter(Program.GetProgramProcess())));
            }
            TimeSpan diff = DateTime.Now.Subtract(_currentTimeLogin);
            this.lblTimeUsage.Text = string.Format("{0}", Program.GetUsageTimeProgram(Convert.ToInt32(diff.TotalSeconds)));
            this.barStaticI_Time.Caption = String.Format("[{0:T}]", DateTime.Now);
            // this.Text = this.Text.Substring(1, this.Text.Length - 1) + this.Text.Substring(0, 1);




            bbItemCAP.ItemAppearance.Normal.ForeColor = IsKeyLocked(Keys.CapsLock) ? Color.Black : Color.Transparent;
            bbItemNum.ItemAppearance.Normal.ForeColor = IsKeyLocked(Keys.NumLock) ? Color.Black : Color.Transparent;

            if (checkNewNoti)
            {
                if (btnNotify.ItemAppearance.Normal.BackColor == Color.Transparent)
                    btnNotify.ItemAppearance.Normal.BackColor = Color.Red;
                else if (btnNotify.ItemAppearance.Normal.BackColor == Color.Red)
                    btnNotify.ItemAppearance.Normal.BackColor = Color.Transparent;
                if (btnNotify2.ItemAppearance.Normal.BackColor == Color.Transparent)
                    btnNotify2.ItemAppearance.Normal.BackColor = Color.Red;
                else if (btnNotify2.ItemAppearance.Normal.BackColor == Color.Red)
                    btnNotify2.ItemAppearance.Normal.BackColor = Color.Transparent;

                if (btnNotify.Enabled == false)
                    btnNotify.Enabled = true;
                if (btnNotify2.Enabled == false)
                    btnNotify2.Enabled = true;
            }
            else
            {

                btnNotify.ItemAppearance.Normal.BackColor = Color.Transparent;


                btnNotify2.ItemAppearance.Normal.BackColor = Color.Transparent;

                if (btnNotify.Enabled == false)
                    btnNotify.Enabled = true;
                if (btnNotify2.Enabled == false)
                    btnNotify2.Enabled = true;

            }




        }





        #endregion

        #region "show form"

        public Form CreateInstanceFormByProject(string projectName, string folderContainForm, string formName)
        {
            Type t;
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            if (projectName != appName)
            {
                Assembly assemblyProject = Assembly.LoadFile(string.Format(@"{0}\{1}.dll", Application.StartupPath, projectName));
                if (string.IsNullOrEmpty(folderContainForm))
                {
                    t = assemblyProject.GetType(string.Format("{0}.{1}", projectName, formName), false, true);
                }
                else
                {
                    t = assemblyProject.GetType(string.Format("{0}.{1}.{2}", projectName, folderContainForm, formName), false, true);
                }

            }
            else
            {
                t = Type.GetType(string.IsNullOrEmpty(folderContainForm) ? formName : string.Format("{0}.{1}", folderContainForm, formName), false, true);
            }
            object objForm = Activator.CreateInstance(t);
            return objForm as Form;
        }

        #endregion

        #region "hotkey"


        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                #region "System"
                case Keys.Shift | Keys.F1:
                    {

                        TreeListNode focusedNode = treeList1.FocusedNode;
                        if (focusedNode != null)
                        {
                            string menuCode = ProcessGeneral.GetSafeString(focusedNode.GetValue("MenuCode"));
                            ProcessGeneral.OpenHelpForm(menuCode, false);
                        }
                        return true;
                    }
                case Keys.F1:
                    {

                        bbiHelp_ItemClick(null, null);
                        return true;
                    }
                case Keys.Control | Keys.Alt | Keys.P:
                    {

                        bbiProgressBar_ItemClick(null, null);
                        return true;
                    }

                case Keys.Control | Keys.Alt | Keys.L:
                    {

                        bbiLogout_ItemClick(null, null);
                        return true;
                    }
                case Keys.Control | Keys.Alt | Keys.H:
                    {

                        bbiHide_ItemClick(null, null);
                        return true;
                    }
                case Keys.Control | Keys.Alt | Keys.S:
                    {
                        bbiConfigSQLServer_ItemClick(null, null);
                        return true;
                    }
                case Keys.Control | Keys.Alt | Keys.B:
                    {
                        bbiBackupDatabase_ItemClick(null, null);
                        return true;
                    }
                #endregion

                #region "Command"
                case Keys.Control | Keys.Shift | Keys.A:
                    {
                        if (bbiAdd.Visibility == BarItemVisibility.Always && bbiAdd.Enabled)
                        {
                            PerformCommandHotKey("bbiAdd");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.E:
                    {
                        if (bbiEdit.Visibility == BarItemVisibility.Always && bbiEdit.Enabled)
                        {
                            PerformCommandHotKey("bbiEdit");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.D:
                    {
                        if (bbiDelete.Visibility == BarItemVisibility.Always && bbiDelete.Enabled)
                        {
                            PerformCommandHotKey("bbiDelete");
                        }
                        return true;
                    }

                case Keys.Control | Keys.Shift | Keys.S:
                    {
                        if (bbiSave.Visibility == BarItemVisibility.Always && bbiSave.Enabled)
                        {
                            PerformCommandHotKey("bbiSave");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.X:
                    {
                        if (bbiCancel.Visibility == BarItemVisibility.Always && bbiCancel.Enabled)
                        {
                            PerformCommandHotKey("bbiCancel");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.F5:
                    {
                        if (bbiRefresh.Visibility == BarItemVisibility.Always && bbiRefresh.Enabled)
                        {
                            PerformCommandHotKey("bbiRefresh");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.F:
                    {
                        if (bbiFind.Visibility == BarItemVisibility.Always && bbiFind.Enabled)
                        {
                            PerformCommandHotKey("bbiFind");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.M:
                    {
                        if (bbiCollapse.Visibility == BarItemVisibility.Always && bbiCollapse.Enabled)
                        {
                            PerformCommandHotKey("bbiCollapse");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.L:
                    {
                        if (bbiExpand.Visibility == BarItemVisibility.Always && bbiExpand.Enabled)
                        {
                            PerformCommandHotKey("bbiExpand");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.P:
                    {
                        if (bbiPrint.Visibility == BarItemVisibility.Always && bbiPrint.Enabled)
                        {
                            PerformCommandHotKey("bbiPrint");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.B:
                    {
                        if (bbiBreakDown.Visibility == BarItemVisibility.Always && bbiBreakDown.Enabled)
                        {
                            PerformCommandHotKey("bbiBreakDown");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.R:
                    {
                        if (bbiRevision.Visibility == BarItemVisibility.Always && bbiRevision.Enabled)
                        {
                            PerformCommandHotKey("bbiRevision");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.K:
                    {
                        if (bbiRangSize.Visibility == BarItemVisibility.Always && bbiRangSize.Enabled)
                        {
                            PerformCommandHotKey("bbiRangSize");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.C:
                    {
                        if (bbiCopyObject.Visibility == BarItemVisibility.Always && bbiCopyObject.Enabled)
                        {
                            PerformCommandHotKey("bbiCopyObject");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.Q:
                    {
                        if (bbiClose.Visibility == BarItemVisibility.Always && bbiClose.Enabled)
                        {
                            PerformCommandHotKey("bbiClose");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.G:
                    {
                        if (bbiGenerate.Visibility == BarItemVisibility.Always && bbiGenerate.Enabled)
                        {
                            PerformCommandHotKey("bbiGenerate");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.H:
                    {
                        if (bbiCombine.Visibility == BarItemVisibility.Always && bbiCombine.Enabled)
                        {
                            PerformCommandHotKey("bbiCombine");
                        }
                        return true;
                    }
                case Keys.Control | Keys.Shift | Keys.T:
                    {
                        if (bbiCheck.Visibility == BarItemVisibility.Always && bbiCheck.Enabled)
                        {
                            PerformCommandHotKey("bbiCheck");
                        }
                        return true;
                    }
                    #endregion

            }
            return base.ProcessCmdKey(ref message, keys);



        }

        private void PerformCommandHotKey(string buttonName)
        {

            XtraMdiTabPage selectedPage = tmmMain.SelectedPage;
            if (selectedPage == null) return;
            string commandName = GetCommandName(buttonName);
            var mdiForm = selectedPage.MdiChild;
            var fBase = mdiForm as FrmBase;
            if (fBase == null)
            {
                switch (commandName)
                {
                    case CommandBarDefaultSetting.CaptionButtonCloseDefaultCommamd:
                        mdiForm.Close();
                        break;
                }
            }
            else
            {
                fBase.CommandPass(commandName);
            }


        }
        #endregion


        #region "Process CAPS,INSERT STATUS"


        private void btnConfirmEmailPass_Click(object sender, EventArgs e)
        {
            string emailPass = ProcessGeneral.GetSafeString(txtEmailPass.EditValue);
            if (ProcessGeneral.GetSafeString(emailPass) == "")
            {
                XtraMessageBox.Show("Password Could not empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmailPass.Select();
                return;
            }


            _ctrl.UpdateDatePasswordEmail(SystemProperty.SysUserId, EnDeCrypt.Encrypt(emailPass, true));
            XtraMessageBox.Show("Update Pass Successfull.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SystemProperty.SysDefaultSendMailPass = emailPass;


        }



        private void bbItemNum_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            PressKeyboardButton(Keys.NumLock);
        }

        private void bbItemCAP_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            PressKeyboardButton(Keys.CapsLock);
        }






        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        /// <summary>
        /// Simulate the Key Press Event
        /// </summary>
        /// <param name="keyCode">The code of the Key to be simulated</param>
        private void PressKeyboardButton(Keys keyCode)
        {
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            keybd_event((byte)keyCode, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event((byte)keyCode, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }


        #endregion

        #region "Save Skin"
        private void rgbMain_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
        {
            Settings.Default.SkinName = e.Item.Caption;
            Settings.Default.Save();
        }





        private void btnNotify_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dockNotify.Visibility == DockVisibility.Hidden || dockNotify.Visibility == DockVisibility.AutoHide)
            {


                dockNotify.Visibility = DockVisibility.Visible;
                DataTable tb = ac.TblReadDataSQL($"SELECT '' as Notification,*,'' as ViewDocumment,'' as Dismiss FROM CNYNoti WHERE UserID='{DeclareSystem.SysUserId}' AND CNY003=1", null);
                tb.AsEnumerable().ToList().ForEach(x =>
                {
                    x["Notification"] = x["CNY001"].ToString();
                });

                gcNotify.DataSource = tb;
                AddRepository();
                // gvNotify.OptionsView.ColumnAutoWidth = false;         
                gvNotify.Columns["PK"].Visible = false;
                gvNotify.Columns["UserID"].Visible = false;
                gvNotify.Columns["CNY001"].Visible = false;
                gvNotify.Columns["CNY002"].Visible = false;
                gvNotify.Columns["CNY003"].Visible = false;
                gvNotify.Columns["CNY004"].Visible = false;
                gvNotify.Columns["CNY005"].Visible = false;
                gvNotify.Columns["CNY006"].Visible = false;
                gvNotify.Columns["CNY007"].Visible = false;
                gvNotify.Columns["Notification"].Width = 400;
                gvNotify.OptionsView.ShowGroupPanel = false;
                gvNotify.OptionsView.ShowColumnHeaders = false;
            }

            else
                dockNotify.Visibility = DockVisibility.Hidden;
            // dockNotify.ShowSliding();






        }


        private void AddRepository()
        {
            RepositoryItemButtonEdit makeread = new RepositoryItemButtonEdit();
            RepositoryItemButtonEdit viewdoc = new RepositoryItemButtonEdit();
            RepositoryItemButtonEdit dismiss = new RepositoryItemButtonEdit();
            RepositoryItemRichTextEdit edittext = new RepositoryItemRichTextEdit();






            viewdoc.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            viewdoc.Buttons[0].Caption = "View Document";
            viewdoc.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            viewdoc.Buttons[0].ImageOptions.Image = Properties.Resources.document_view;
            viewdoc.Buttons[0].ToolTip = "View Documment";
            gvNotify.Columns["ViewDocumment"].ColumnEdit = viewdoc;


            dismiss.TextEditStyle = TextEditStyles.HideTextEditor;
            dismiss.Buttons[0].Caption = "View Document";

            dismiss.Buttons[0].Kind = ButtonPredefines.Glyph;
            dismiss.Buttons[0].ImageOptions.Image = Properties.Resources.exitnoti;
            dismiss.Buttons[0].ToolTip = "Dismiss Notification";
            gvNotify.Columns["Dismiss"].ColumnEdit = dismiss;

            gvNotify.Columns["Notification"].ColumnEdit = edittext;

        }

       













        #endregion

        /*
        private bool _isSpam = true;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _isSpam = true;
            while (true)
            {if (!_isSpam) break;
                ProcessGeneral.SendMail("smtp.office365.com", "MinhThuan.Nguyen@scavi.com.vn", "1234567",587,true, "MinhThuan.Nguyen@scavi.com.vn", "AnhVu.Do@scavi.com.vn", "V1","V1",null,false);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            _isSpam = false;
        }*/
    }
}