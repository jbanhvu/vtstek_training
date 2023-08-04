using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CNY_BaseSys;
using CNY_BaseSys.Common;
using CNY_BaseSys.WForm;
using CNY_WH.Class;
using CNY_WH.Common;
using CNY_WH.Info;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace CNY_WH.WForm
{

    public partial class Frm_StockDocumentRequest : FrmBase
    {
        #region "Property"


        XtraUC_StockDocumentRequestMain xtraUCMain;
        XtraUC_StockDocumentRequestMainAE xtraUCMainAE;
        GridView gvMain;

        readonly Inf_StockDocumentRequest _inf = new Inf_StockDocumentRequest();

     
        WaitDialogForm _dlg;

        private string _option = "";
        private const string _FunctionCode = "SR";
        private DataTable _dtHeader;
        TextEdit txtStatus;

        //SearchLookUpEdit searchStatus;


        private readonly WorkflowStatusManager _wm = null;
        private readonly PermissionFormInfo _perInfo = new PermissionFormInfo();
        //private readonly BarManager _barManagerMain;
      //  private readonly PopupMenu _popupMenuPrint;


        private Int64 _pk = 0;
        private Int64 _StatusPK = 0;
        private Int64 _StatusMaxPK = 0;
        private string _UserNameStatusMax = "";
        #endregion


        #region "Contructor"
        public Frm_StockDocumentRequest()
        {
            InitializeComponent();
         
            
            _wm = new WorkflowStatusManager(DeclareSystem.SysUserName.ToUpper(), "SR", DeclareSystem.SysConnectionString);

            Load += FrmMain_Load;

            //_barManagerMain = new BarManager
            //{
            //    Form = this,
            //    Images = InitImageListForBarManager(),
            //};



            //_popupMenuPrint = SetUpPopupMenu();

        }
        public bool GetVisibleMainControl()
        {
            if (xtraUCMain == null) return false;
            return xtraUCMain.Visible;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

            _perInfo.PerIns = PerIns;
            _perInfo.PerDel = PerDel;
            _perInfo.PerUpd = PerUpd;
            _perInfo.PerViw = PerViw;
            _perInfo.DtAdvanceFunc = DtPerFunction;
            _perInfo.StrAdvanceFunction = StrAdvanceFunction;
            _perInfo.PerCheckAdvanceFunction = PerCheckAdvanceFunction;
            _perInfo.StrSpecialFunction = StrSpecialFunction;
            _perInfo.DtSpecialFunction = DtSpecialFunction;
            _wm.PerInfo = _perInfo;
            // wm.PropertyChanged += Wm_PropertyChanged;

            
            LoadButtonWhenLoad();
            //SetCaptionRangSize = "Grouping";
            //SetCaptionBreakDown = "Allocate";
            //SetCaptionCheck = "Update Demand";
            xtraUCMain = new XtraUC_StockDocumentRequestMain("1201", "1401", "2201", "2401")
            {
                Dock = DockStyle.Fill,
                Name = "xtraUCM"
            };
            panelControlAdd.Controls.Add(xtraUCMain);
            gvMain = xtraUCMain.gvMainP;
            gvMain.DoubleClick += gvMain_DoubleClick;
            gvMain.KeyDown += GvMain_KeyDown;


        }


        #endregion

        #region "Popup Menu"


        #region "Create Button PopupMenu"
        /*
        private IEnumerable<String> CreateBarButtonPrint()
        {
            List<BarButtonPopupMenuInfo> lButton = new List<BarButtonPopupMenuInfo>();
            var item1 = new BarButtonPopupMenuInfo
            {
                Caption = "Print layout 1",
                Name = "bbiPrintLayout1",
                ImageIndex = 0,
                Index = 1
            };
            lButton.Add(item1);

            var item2 = new BarButtonPopupMenuInfo
            {
                Caption = "Print layout 2",
                Name = "bbiPrintLayout2",
                ImageIndex = 1,
                Index = 2
            };
            lButton.Add(item2);


            foreach (BarButtonPopupMenuInfo item in lButton)
            {
                var bbi = new BarButtonItem
                {
                    Name = item.Name,
                    Caption = item.Caption,
                    Id = item.Index,
                    ImageIndex = item.ImageIndex
                };
                bbi.ItemClick += bbi_ItemClick;
                _barManagerMain.Items.Add(bbi);
                yield return item.Name;
            }
        }

        private void bbi_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.Item as BarButtonItem;
            if (item == null) return;
            Int64 pkPrHeader = 0;

            if (xtraUCMain.Visible)
            {


                if (gvMain.RowCount <= 0)
                {
                    XtraMessageBox.Show("No row is selected to perform printing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int rH = gvMain.FocusedRowHandle;
                pkPrHeader = ProcessGeneral.GetSafeInt64(gvMain.GetRowCellValue(rH, "PK"));

            }
            else
            {
                pkPrHeader = xtraUCMainAE.PrHeaderPk;
               


            }


            switch (item.Name)
            {
                case "bbiPrintLayout1":
                    //Ctrl_PrGenneral.PrintPr(pkPrHeader,false);
                    break;
                case "bbiPrintLayout2":
                    //Ctrl_PrGenneral.PrintPr2(pkPrHeader);
                    break;
            }

        }
        */
        #endregion


        /*

        private PopupMenu SetUpPopupMenu()
        {

            PopupMenu popupMenuPrint = new PopupMenu(_barManagerMain)
            {
                MenuCaption = "Print PR",
                Name = "_popupMenuPrint",
                ShowCaption = true,
                MenuAppearance =
                {
                    MenuCaption =
                    {
                        Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0),
                        Options =
                        {
                            UseFont = true,
                        }
                    }
                }
            };

            IEnumerable<String> lB = CreateBarButtonPrint();
            foreach (String name in lB)
            {
                popupMenuPrint.ItemLinks.Add(_barManagerMain.Items[name]);
            }

            return popupMenuPrint;

            //  _popupMenuPrint.ItemLinks.Add(_barManagerMain.Items[1]);
            //finally set the context menu to the control or use the showpopup method on right click of control
            //  barManagerMain.SetPopupContextMenu(btnInsert, popupMenuPrint);

        }
        */


        #endregion


        #region "Image List"
        private ImageList InitImageListForBarManager()
        {
            var imgLt = new ImageList();
            //imgLt.Images.Add(Properties.Resources.print_15_item);
            //imgLt.Images.Add(Properties.Resources.print_4_item);
            return imgLt;

        }

        #endregion

        #region "Add UserControl On Panel"
        /// <summary>
        ///     Add UserControlAE When buton Add or Edit Click For Insert,Edit,Delete Data Contract OutSourcing
        /// </summary>
        /// <param name="paraoption" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        private void AddUserControlMainAE(string paraoption, DataTable dtAdvanceFunc, Int64 HeaderPk)
        {
            
            xtraUCMainAE = new XtraUC_StockDocumentRequestMainAE(paraoption, dtAdvanceFunc, HeaderPk)
            {
                Dock = DockStyle.Fill,
                Name = "xtraUCMAE",


            };

            //DataTable dtStatus=_inf.GetTableStatus("SR");
            //ProcessGeneral.GetSafeInt(dtStatus.Rows[0]["Code"]);
            //for (int i = 0; i <= dtStatus.Rows.Count; i++)
            //{
            //    new System.Tuple<string, TextEdit, TextEdit>(ProcessGeneral.GetSafeString(dtStatus.Rows[0]["Code"]), null, null),
            //}
            _wm.ArrIndexStatus = new[]
            {
            
                new System.Tuple<string, TextEdit, TextEdit>("1", null, null),
                //new System.Tuple<string, TextEdit, TextEdit>("2", xtraUCMainAE.txtReleasedBy,xtraUCMainAE.txtReleasedDate),
                //new System.Tuple<string, TextEdit, TextEdit>("3",xtraUCMainAE.txtConfirmBy,xtraUCMainAE.txtConfirmDate),
                //new System.Tuple<string, TextEdit, TextEdit>("4",xtraUCMainAE.txtApprovedBy,xtraUCMainAE.txtApprovedDate),
                new System.Tuple<string, TextEdit, TextEdit>("2", null,null),
                new System.Tuple<string, TextEdit, TextEdit>("3",null,null),
                new System.Tuple<string, TextEdit, TextEdit>("4",null,null),
                new System.Tuple<string, TextEdit, TextEdit>("5",null,null),
            };

            txtStatus = xtraUCMainAE.txtStatusP;
           
            //txtStatus.EditValueChanged += txtStatus_EditValueChanged;
            //txtStatus.KeyDown += txtStatus_KeyDown;

            //   Use2ButtonGenerateRefresh(xtraTabMain.SelectedTabPageIndex);


            panelControlAdd.Controls.Add(xtraUCMainAE);

        }


        /// <summary>
        ///     Remove UserControlAE When buton Cancel or Save Click For Complete Update data Contract OutSourcing
        /// </summary>
        /// <param name="root" type="System.Windows.Forms.Control">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="target" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        private void RemoveUserControlMainAE(Control root, string target)
        {
            var xtraUcMainAeRemove = ProcessGeneral.FindControl(root, target) as XtraUC_StockDocumentRequestMainAE;
            if (xtraUcMainAeRemove != null)
            {
                root.Controls.Remove(xtraUcMainAeRemove);

            }

        }
        #endregion


        #region "Methold"

        private void LoadButtonWhenLoad()
        {





            AllowCombine = false;
            AllowCheck = false;
            AllowCollapse = false;
            AllowExpand = false;

            AllowAdd = true;
            AllowEdit = true;
            AllowDelete = true;
            AllowSave = false;
            AllowCancel = false;
            AllowRefresh = true;
            AllowFind = true;
            AllowPrint = true;
            AllowRevision = false;
            AllowRangeSize = false;
            AllowCopyObject = false;
            AllowBreakDown = false;
            AllowGenerate = false;
            AllowClose = true;
            

            EnableAdd = _perInfo.PerIns;
            EnableEdit = _perInfo.PerUpd;
            EnableDelete = _perInfo.PerDel;
            EnableRefresh = true;
            EnableFind = true;
            EnablePrint = true;
            EnableClose = true;




        }


        private void LoadButtonWhenAddCopy()
        {




            AllowCombine = false;
            AllowCheck = false;
            AllowCollapse = false;
            AllowExpand = false;

            AllowAdd = false;

            AllowDelete = false;
         
            AllowCancel = false;
            AllowRefresh = true;
            AllowFind = false;
            AllowPrint = false;
      
            AllowCopyObject = false;
            AllowBreakDown = false;
            AllowRangeSize = false;
            //AllowGenerate = PerIns || PerUpd || PerDel;
            AllowGenerate = false;
            AllowClose = true;


            AllowEdit = false;





            EnableRefresh = true;
            EnableGenerate = PerIns || PerUpd || PerDel;
            EnableClose = true;
            EnableRangSize = true;

            _wm.Option = _option;
            _wm.Status = ProcessGeneral.GetSafeString(txtStatus.EditValue);
            //_wm.Status = ProcessGeneral.GetSafeString(ProcessGeneral.GetDataRowByEditValueKey(searchStatus)["Code"]);
            _wm.GetStatusSaveRevise();

            if (_wm.UseSave)
            {
                AllowSave = true;
                EnableSave = true;
            }
            else
            {
                AllowSave = false;
                EnableSave = false;
            }


            //if (_wm.UseRevision)
            //{
            //    AllowRevision = true;
            //    EnableRevision = true;
            //}
            //else
            //{
                AllowRevision = false;
                EnableRevision = false;

            //}
        }


        private void LoadButtonWhenEditRefresh()
        {



            AllowBreakDown = false;

            AllowCombine = false;
            AllowCheck = false;
            AllowCollapse = false;
            AllowExpand = false;

            AllowAdd = false;

            AllowDelete = false;
            AllowCancel = false;
            AllowRefresh = true;
            AllowFind = false;
            AllowPrint = true;
            AllowCopyObject = false;
            AllowRangeSize = false;
            //AllowGenerate = PerIns || PerUpd || PerDel;
            AllowGenerate = false;
            AllowClose = true;


            AllowEdit = false;


            EnablePrint = true;
            EnableRefresh = true;

            EnableClose = true;
            EnableGenerate = PerIns || PerUpd || PerDel;
            EnableRangSize = false;
            EnableCheck = false;
            _wm.Option = _option;
            _wm.Status = ProcessGeneral.GetSafeString(txtStatus.EditValue);
            //_wm.Status = ProcessGeneral.GetSafeString(ProcessGeneral.GetDataRowByEditValueKey(searchStatus)["Code"]);
            _wm.GetStatusSaveRevise();

            if (_wm.UseSave)
            {
                AllowSave = true;
                EnableSave = true;
            }
            else
            {
                AllowSave = false;
                EnableSave = false;
            }


            if (_wm.UseRevision)
            {
                AllowRevision = true;
                EnableRevision = true;
            }
            else
            {
                AllowRevision = false;
                EnableRevision = false;

            }

        }


        /// <summary>
        /// Use Button Save and Button Revision When Status Edit Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtStatus_EditValueChanged(object sender, EventArgs e)
        {

      
            //xtraUCMainAE.StatusDescriptionMain = _inf.GetStatusDescription(ProcessGeneral.GetSafeInt(txtStatus.EditValue));


        }


       


        private void txtStatus_KeyDown(object sender, KeyEventArgs e)
        {


            //if (e.KeyCode != Keys.F4) return;
            //string statusCheck = ProcessGeneral.GetSafeString(txtStatus.EditValue);

            //_wm.Status = statusCheck;

            //DataTable dtTest = _wm.GetTableStatus();
            //if (dtTest.Rows.Count <= 0)
            //{
            //    XtraMessageBox.Show("You are not authorized to change status \n You could contact with Admin System CNY.",
            //        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}


            //using (var frm = new FrmStatusListGeneral(dtTest))
            //{
            //    frm.getlistStatus += (s1, e1) =>
            //    {


            //        bool allowChange = true;
            //        if (e1.StatusCode == "4" && _option == "EDIT")
            //        {
            //            //allowChange = Ctrl_PrGenneral.CheckApprovePr(xtraUCMainAE.PrHeaderPk);
            //        }

            //        if (allowChange)
            //        {

            //            txtStatus.EditValue = e1.StatusCode;

            //bool isSet = _wm.SetTextAfterChangeStatus(e1.StatusCode);
            //            if (isSet)
            //            {
            //                _wm.Option = _option;
            //                _wm.Status = ProcessGeneral.GetSafeString(txtStatus.EditValue);
            //                _wm.GetStatusSaveRevise();
            //                if (_wm.UseSave)
            //                {
            //                    AllowSave = true;
            //                    EnableSave = true;
            //                }
            //                else
            //                {
            //                    AllowSave = false;
            //                    EnableSave = false;
            //                }


            //                if (_wm.UseRevision)
            //                {
            //                    AllowRevision = true;
            //                    EnableRevision = true;
            //                }
            //                else
            //                {
            //                    AllowRevision = false;
            //                    EnableRevision = false;

            //                }
            //            }
            //        }
            //        else
            //        {
            //            XtraMessageBox.Show("Approve BOM befor Approve SDR", "Error", MessageBoxButtons.OK,
            //                MessageBoxIcon.Error);
            //        }


            //    };
            //    frm.ShowDialog();
            //}
        }



        #endregion

        #region "gridview"

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (gv == null) return;
          
            if (gv.FocusedColumn == null) return;
            if (gv.RowCount <= 0) return;
            GridControl gc = gv.GridControl;


            GridHitInfo hi = gv.CalcHitInfo(gc.PointToClient(MousePosition));
            if (!hi.InRowCell) return;


            PerformEdit();
        }

        private void GvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformEdit();
            }
        }
     
        #endregion


        #region "override button menubar click"


        /// <summary>
        /// Perform when click delete button
        /// </summary>
        protected override void PerformDelete()
        {
            if (!_perInfo.PerDel)
            {
                XtraMessageBox.Show("You are not authorized to perform the function delete data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


          
            if (gvMain.RowCount <= 0)
            {
                XtraMessageBox.Show("No row is selected to perform delete data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int rH = gvMain.FocusedRowHandle;
           

            DialogResult dlResult = XtraMessageBox.Show("Do you want to delete this record ? (yes/No) \n Note:You could not restore this record!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlResult != DialogResult.Yes) return;

            Int64 pkPrHeader = ProcessGeneral.GetSafeInt64(gvMain.GetRowCellValue(rH, "PK"));
            _dlg = new WaitDialogForm("");
            int rs = _inf.DeleteAllData(pkPrHeader);
            _dlg.Close();

            if (rs == 1)
            {
                XtraMessageBox.Show("This SDR have been used - Can't delete SDR in the system", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                XtraMessageBox.Show("This record has been deleted from the database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                xtraUCMain.UpdateDataForGridView();
            }







        }


        /// <summary>
        ///    Perform when click Add button 
        /// </summary>
        protected override void PerformAdd()
        {
            if (!_perInfo.PerIns)
            {
                XtraMessageBox.Show("You are not authorized to perform the function add new data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        
            _dlg = new WaitDialogForm("");


            this.Text = String.Format("{0} - New", base.MenuName);



            _option = "ADD";
          
            xtraUCMain.Visible = false;
            AddUserControlMainAE(_option, DtPerFunction,0);



            xtraUCMainAE.LoadDataWhenAdd(true);



            LoadButtonWhenAddCopy();
            _dlg.Close();



        }

        protected override void PerformPrint()
        {
            /*
          
              */

            //if (xtraUCMain.Visible)
            //{


            //    if (gvMain.RowCount <= 0)
            //    {
            //        XtraMessageBox.Show("No row is selected to perform printing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //    int rH = gvMain.FocusedRowHandle;


            //    if (!gvMain.IsDataRow(rH) || gvMain.IsGroupRow(rH))
            //    {
            //        XtraMessageBox.Show("This Row is Invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    _popupMenuPrint.ShowPopup(MousePosition);


            //}
            //else
            //{
            //    _popupMenuPrint.ShowPopup(MousePosition);


            //}
        }

        protected override void PerformCheck()
        {



           
            xtraUCMainAE.PerformCheck();



        }


        /// <summary>
        /// Perform when click edit button
        /// </summary>
        protected override void PerformEdit()
        {


     
            if (gvMain.RowCount <= 0)
            {
                XtraMessageBox.Show("No row is selected to perform editing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int rH = gvMain.FocusedRowHandle;


            if (!gvMain.IsDataRow(rH) || gvMain.IsGroupRow(rH))
            {
                XtraMessageBox.Show("This Row is Invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dlg = new WaitDialogForm("");

            Int64 prHeaderPk = ProcessGeneral.GetSafeInt64(gvMain.GetRowCellValue(rH, "PK"));
            _dtHeader = _inf.DisplayHeaderWhenEdit(prHeaderPk,_FunctionCode);
            if (_dtHeader.Rows.Count <= 0)
            {
                _dlg.Close();
                XtraMessageBox.Show("This SDR No. not exist in database", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                xtraUCMain.UpdateDataForGridView();
                return;
            }


            this.Text = String.Format("{0} - Edit ({1})", base.MenuName,ProcessGeneral.GetSafeString(_dtHeader.Rows[0]["SDRNo"]));



            _option = "EDIT";
            xtraUCMain.Visible = false;
            AddUserControlMainAE(_option, DtPerFunction, prHeaderPk);
            xtraUCMainAE.LoadDataWhenEdit(_dtHeader, true);
            LoadButtonWhenEditRefresh();

            _dlg.Close();



        }



        /// <summary>
        /// Perform when click save button
        /// </summary>
        protected override void PerformSave()
        {
            Ctrl_SDRSave ctrl = xtraUCMainAE.SaveData();

            if (!ctrl.Result) return;
            _dlg = new WaitDialogForm("");
            _option = "EDIT";
            Int64 prHeaderPk = ctrl.PrHeaderPk;
            _dtHeader = _inf.DisplayHeaderWhenEdit(prHeaderPk, _FunctionCode);

            if (_dtHeader.Rows.Count <= 0)
            {
                _dlg.Close();
                XtraMessageBox.Show("This SDR has been deleted in system.\n Please, Recheck Data.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PerformCancel();
                return;
            }
            this.Text = String.Format("{0} - Edit ({1})", base.MenuName, ProcessGeneral.GetSafeString(_dtHeader.Rows[0]["SDRNo"]));



            xtraUCMainAE.LoadDataWhenEdit(_dtHeader,false);
            LoadButtonWhenEditRefresh();
            _pk = prHeaderPk;
            _dlg.Close();


            XtraMessageBox.Show("Record Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateGridMainAfterSave()
        {
            if (_pk <= 0)
            {
                _pk = 0;
                return;
            }
            xtraUCMain.UpdateDataForGridView();
            FindPkCodeAfterSave(_pk, "PK");
            _pk = 0;

        }
        private void FindPkCodeAfterSave(Int64 pk , string field)
        {

            int rHfocsued = gvMain.FindRowInGridByColumn(pk.ToString(), field);

            if (rHfocsued < 0) return;
            gvMain.SetFocusedRowOnGrid(rHfocsued);


        }

        /// <summary>
        /// Perform when click Revision button
        /// </summary>
        protected override void PerformRevision()
        {
            _StatusPK = xtraUCMainAE.StatusPK;
            _StatusMaxPK = xtraUCMainAE.StatusMaxPK;
            _UserNameStatusMax = xtraUCMainAE.UserNameStatusMax;

            if (_StatusPK != _StatusMaxPK || _UserNameStatusMax=="") return; // status nhỏ hơn status max hoặc chưa ký thì chưa dc Revision

            DialogResult dlResult = XtraMessageBox.Show("Do you want to revise??", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult != DialogResult.Yes) return;
            int VersionOld = ProcessGeneral.GetSafeInt(xtraUCMainAE.txtVersion.EditValue);
            // tăng version
            xtraUCMainAE.txtVersion.EditValue = ProcessGeneral.GetSafeInt(xtraUCMainAE.txtVersion.EditValue) + 1;
            // trả về status đầu tiên sau khi tăng vesion
            int VersionNew = ProcessGeneral.GetSafeInt(xtraUCMainAE.txtVersion.EditValue);
            if (VersionOld < VersionNew)
            {
                //Set Status về đầu tiên
                DataTable dt = new DataTable();
                dt = _inf.GetTableStatus(_FunctionCode, DtPerFunction);
                xtraUCMainAE.StatusPK = ProcessGeneral.GetSafeInt64(dt.Rows[0]["CNYMF015PK"]);
                xtraUCMainAE.txtStatus.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Code"]);
                xtraUCMainAE.txtStatusDescription.EditValue = ProcessGeneral.GetSafeString(dt.Rows[0]["Description"]);
                xtraUCMainAE.chkReject.Enabled = true;
                xtraUCMainAE.chkReject.Checked = false;
                xtraUCMainAE.chkReject.Text = "Rejected";
                xtraUCMainAE._RejectDescription = "Rejected";
                ////////////////////////
            }
           

        }

        public void PerformCancelTemp()
        {
            PerformCancel();
        }
        protected override void PerformCancel()
        {
            _option = "";
            //  xtraUCMainAE.ClearErrorOnControlButtonClick();
            RemoveUserControlMainAE(panelControlAdd, "xtraUCMAE");
            xtraUCMain.Visible = true;
            LoadButtonWhenLoad();
            UpdateGridMainAfterSave();
            this.Text = base.MenuName;
        }







        /// <summary>
        /// Perform when click find button
        /// </summary>
        protected override void PerformFind()
        {
            DataTable dtFiled = GetTableFieldSearch();
            var frm = new FrmSearch(dtFiled) { TitileForm = "Search SDR" };
            frm.searchEvent += (s, e) =>
            {
                xtraUCMain.StrFilter = e.filterexpression;
                xtraUCMain.UpdateDataForGridView();
            };
            frm.ShowDialog();

        }


        /// <summary>
        /// Perform when click Refresh button
        /// </summary>
        protected override void PerformRefresh()
        {

            if (!xtraUCMain.Visible)
            {
                DialogResult dlgRs = XtraMessageBox.Show("Do you want to refresh SDR ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgRs != DialogResult.Yes) return;

                _dlg = new WaitDialogForm("");
                if (_option.ToUpper() == "ADD")
                {
                    xtraUCMainAE.RefreshDataWhenAddOrCopy(false);
                }
                else
                {
                    int rs = xtraUCMainAE.RefreshDataWhenEditAndSave(false);

                    if (rs == 0)
                    {
                        LoadButtonWhenEditRefresh();

                    }
                    else
                    {
                        XtraMessageBox.Show("This SDR No. not exist in database ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        PerformCancel();

                    }

                }
            }
            else
            {
                _dlg = new WaitDialogForm("");
                xtraUCMain.StrFilter = "";
                xtraUCMain.TxtSearchp.EditValue = "";
                xtraUCMain.UpdateDataForGridView();
            }
            _dlg.Close();
        }


     

        /// <summary>
        /// Perform when click Close button
        /// </summary>
        protected override void PerformRangeSize()
        {
            xtraUCMainAE.GroupByItemCode();
        }

        protected override void PerformGenerate()
        {
            xtraUCMainAE.GenerateData("");
        }

        /// <summary>
        /// Perform when click Close button
        /// </summary>
        protected override void PerformClose()
        {
            if (xtraUCMain.Visible)
            {
                base.PerformClose();
            }
            else
            {
                PerformCancel();
            }
        }
        #endregion

        #region "methold search"

        /// <summary>
        ///     Create Template Table Search Into Search Form
        /// </summary>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        private DataTable GetTableFieldSearch()
        {
            var dtFiled = new DataTable();//number string datetime
            dtFiled.Columns.Add("FieldValue", typeof(string));
            dtFiled.Columns.Add("FieldDisplay", typeof(string));
            dtFiled.Columns.Add("FieldType", typeof(string));
            //  dtFiled.Rows.Add("","","");
            dtFiled.Rows.Add("[a].[CNY001]", "SDR No.", "string");
            dtFiled.Rows.Add("[h].[CNY001]", "SDR Type", "string");
            dtFiled.Rows.Add("[a].[CNY002]", "Description", "string");
            dtFiled.Rows.Add("[a].[CNY003]", "Sender", "string");
            dtFiled.Rows.Add("[a].[CNY004]", "Recipient", "string");

            //dtFiled.Rows.Add("k.CNY001", "Customer", "string");
            //dtFiled.Rows.Add("k.CNY003", "Search Name", "string");
            //dtFiled.Rows.Add("c.CNY002", "Cust. Order No.", "string");
            //dtFiled.Rows.Add("c.CNY004A", "Project No.", "string");
            //dtFiled.Rows.Add("c.CNY004", "Project Name", "string");
            //dtFiled.Rows.Add("c.CNY032", "Production Order", "string");

            dtFiled.Rows.Add("[a].[CNY006]", "Version", "number");
            dtFiled.Rows.Add("[b].[CodePer]", "Status", "number");
            dtFiled.Rows.Add("[a].[CNY008]", "Created Date", "datetime");
            dtFiled.Rows.Add("[a].[CNY007]", "Created By", "string");
            dtFiled.Rows.Add("[a].[CNY010]", "Adjusted Date", "datetime");
            dtFiled.Rows.Add("[a].[CNY009]", "Adjusted By", "string");
            dtFiled.Rows.Add("[a].[CNY011]", "Computer Adj", "string");

            //dtFiled.Rows.Add("[a].[CNY015]", "Released Date", "datetime");
            //dtFiled.Rows.Add("[a].[CNY014]", "Released By", "string");
            //dtFiled.Rows.Add("[a].[CNY020]", "Confirmed Date", "datetime");
            //dtFiled.Rows.Add("[a].[CNY019]", "Confirmed By", "string");
            //dtFiled.Rows.Add("[a].[CNY017]", "Approved Date", "datetime");
            //dtFiled.Rows.Add("[a].[CNY016]", "Approved By", "string");
            dtFiled.Rows.Add("[a].[PK]", "PK", "number");
            return dtFiled;


        }

        #endregion
    }
}
