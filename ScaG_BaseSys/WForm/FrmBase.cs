using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CNY_BaseSys.Common;
using DevExpress.XtraBars;

namespace CNY_BaseSys.WForm
{
    
    public partial class FrmBase : DevExpress.XtraEditors.XtraForm
    {

        #region "Protected Field"
        
      
        protected bool PerIns = false;
        protected bool PerUpd = false;
        protected bool PerDel = false;
        protected bool PerViw = false;
        protected string StrAdvanceFunction = "";
        protected string MinStatus = "";
        protected string MaxStatus = "";

        protected DataTable DtPerFunction = new DataTable();
        protected string MenuCode = "";
        protected string StrSpecialFunction = "";
        protected DataTable DtSpecialFunction = new DataTable();
        protected bool PerCheckAdvanceFunction = false;


        public string MenuCodeDb
        {
            get { return this.MenuCode; }
        }


        private string _menuName = "";
        public string MenuName
        {
            get { return this._menuName; }
        }


        public string FixedColumnField1 { get; set; }
        public string FixedColumnField2 { get; set; }
        public string FixedColumnField3 { get; set; }
        public string FixedColumnField4 { get; set; }
        public string FixedColumnField5 { get; set; }

        #endregion

        #region "Property Button Bar Item"
        private BarButtonItem bbiAdd = new BarButtonItem();
        public BarButtonItem BbiMainAdd
        {
            get { return this.bbiAdd; }
            set { this.bbiAdd = value; }
        }

        private BarButtonItem bbiEdit = new BarButtonItem();
        public BarButtonItem BbiMainEdit
        {
            get { return this.bbiEdit; }
            set { this.bbiEdit = value; }
        }

        private BarButtonItem bbiDelete = new BarButtonItem();
        public BarButtonItem BbiMainDelete
        {
            get { return this.bbiDelete; }
            set { this.bbiDelete = value; }
        }

        private BarButtonItem bbiSave = new BarButtonItem();
        public BarButtonItem BbiMainSave
        {
            get { return this.bbiSave; }
            set { this.bbiSave = value; }
        }

        private BarButtonItem bbiCancel = new BarButtonItem();
        public BarButtonItem BbiMainCancel
        {
            get { return this.bbiCancel; }
            set { this.bbiCancel = value; }
        }

        private BarButtonItem bbiRefresh = new BarButtonItem();
        public BarButtonItem BbiMainRefresh
        {
            get { return this.bbiRefresh; }
            set { this.bbiRefresh = value; }
        }

        private BarButtonItem bbiFind = new BarButtonItem();
        public BarButtonItem BbiMainFind
        {
            get { return this.bbiFind; }
            set { this.bbiFind = value; }
        }

        private BarButtonItem bbiPrint = new BarButtonItem();
        public BarButtonItem BbiMainPrint
        {
            get { return this.bbiPrint; }
            set { this.bbiPrint = value; }
        }

        private BarButtonItem bbiPrintDown = new BarButtonItem();
        public BarButtonItem BbiMainPrintDown
        {
            get { return this.bbiPrintDown; }
            set { this.bbiPrintDown = value; }
        }



        private BarButtonItem bbiExpand = new BarButtonItem();
        public BarButtonItem BbiMainExpand
        {
            get { return this.bbiExpand; }
            set { this.bbiExpand = value; }
        }

        private BarButtonItem bbiCollapse = new BarButtonItem();
        public BarButtonItem BbiMainCollapse
        {
            get { return this.bbiCollapse; }
            set { this.bbiCollapse = value; }
        }

        private BarButtonItem bbiClose = new BarButtonItem();
        public BarButtonItem BbiMainClose
        {
            get { return this.bbiClose; }
            set { this.bbiClose = value; }
        }

        private BarButtonItem bbiRevision = new BarButtonItem();
        public BarButtonItem BbiMainRevision
        {
            get { return this.bbiRevision; }
            set { this.bbiRevision = value; }
        }

        private BarButtonItem bbiBreakDown = new BarButtonItem();
        public BarButtonItem BbiMainBreakDown
        {
            get { return this.bbiBreakDown; }
            set { this.bbiBreakDown = value; }
        }

        private BarButtonItem bbiRangSize = new BarButtonItem();
        public BarButtonItem BbiMainRangSize
        {
            get { return this.bbiRangSize; }
            set { this.bbiRangSize = value; }
        }

        private BarButtonItem bbiCopyObject = new BarButtonItem();
        public BarButtonItem BbiMainCopyObject
        {
            get { return this.bbiCopyObject; }
            set { this.bbiCopyObject = value; }
        }

        private BarButtonItem bbiGenerate = new BarButtonItem();
        public BarButtonItem BbiMainGenerate
        {
            get { return this.bbiGenerate; }
            set { this.bbiGenerate = value; }
        }

        private BarButtonItem bbiCombine = new BarButtonItem();
        public BarButtonItem BbiMainCombine
        {
            get { return this.bbiCombine; }
            set { this.bbiCombine = value; }
        }

        private BarButtonItem bbiCheck = new BarButtonItem();
        public BarButtonItem BbiMainCheck
        {
            get { return this.bbiCheck; }
            set { this.bbiCheck = value; }
        }
        #endregion



        #region "Popup Menu"

        private bool _isGetPopupPrintDownBeforPopup = false;
        public bool IsGetPopupPrintDownBeforPopup
        {
            get { return this._isGetPopupPrintDownBeforPopup; }
            set { this._isGetPopupPrintDownBeforPopup = value; }
        }

        private List<PopupMenuRibbonItemInfo> _lButtonPrintDropDown = new List<PopupMenuRibbonItemInfo>();
        public List<PopupMenuRibbonItemInfo> ListButtonPrintDropDown
        {
            get
            {
                if (_isGetPopupPrintDownBeforPopup)
                {
                    this._lButtonPrintDropDown = SetPrintDropDownMenuBeforPopup();
                }
                return this._lButtonPrintDropDown;

            }
            set { this._lButtonPrintDropDown = value; }
        }
        protected virtual List<PopupMenuRibbonItemInfo> SetPrintDropDownMenuBeforPopup()
        {
            return this._lButtonPrintDropDown;
        }



        protected void AddItemPrintDropDownMenu(params PopupMenuRibbonItemInfo[] arrBarPutton)
        {
            if (_lButtonPrintDropDown == null)
            {
                _lButtonPrintDropDown = new List<PopupMenuRibbonItemInfo>();
            }
            else
            {
                _lButtonPrintDropDown.Clear();
            }
       
            foreach (PopupMenuRibbonItemInfo item in arrBarPutton)
            {
                _lButtonPrintDropDown.Add(item);
            }
        }
        public void CommandPassPrintDropDownMenu(BarButtonItem barButtonItem)
        {
            PerformPrintDropDownMenu(barButtonItem);
        }
        protected virtual void PerformPrintDropDownMenu(BarButtonItem barButtonItem)
        {

        }
        #endregion


        #region "Button Function"



        #region "Created"
        private BarSubItem bbiGFN1 = new BarSubItem();
        public BarSubItem BbiMainGFN1
        {
            get { return this.bbiGFN1; }
            set { this.bbiGFN1 = value; }
        }


        private BarButtonItem bbiGFN1_F1 = new BarButtonItem();
        public BarButtonItem BbiMainGFN1_F1
        {
            get { return this.bbiGFN1_F1; }
            set { this.bbiGFN1_F1 = value; }
        }
        private BarButtonItem bbiGFN1_F2 = new BarButtonItem();
        public BarButtonItem BbiMainGFN1_F2
        {
            get { return this.bbiGFN1_F2; }
            set { this.bbiGFN1_F2 = value; }
        }
        private BarButtonItem bbiGFN1_F3 = new BarButtonItem();
        public BarButtonItem BbiMainGFN1_F3
        {
            get { return this.bbiGFN1_F3; }
            set { this.bbiGFN1_F3 = value; }
        }
        private BarButtonItem bbiGFN1_F4 = new BarButtonItem();
        public BarButtonItem BbiMainGFN1_F4
        {
            get { return this.bbiGFN1_F4; }
            set { this.bbiGFN1_F4 = value; }
        }
        private BarButtonItem bbiGFN1_F5 = new BarButtonItem();
        public BarButtonItem BbiMainGFN1_F5
        {
            get { return this.bbiGFN1_F5; }
            set { this.bbiGFN1_F5 = value; }
        }
        private BarButtonItem bbiGFN1_F6 = new BarButtonItem();
        public BarButtonItem BbiMainGFN1_F6
        {
            get { return this.bbiGFN1_F6; }
            set { this.bbiGFN1_F6 = value; }
        }







        private BarSubItem bbiGFN2 = new BarSubItem();
        public BarSubItem BbiMainGFN2
        {
            get { return this.bbiGFN2; }
            set { this.bbiGFN2 = value; }
        }


        private BarButtonItem bbiGFN2_F1 = new BarButtonItem();
        public BarButtonItem BbiMainGFN2_F1
        {
            get { return this.bbiGFN2_F1; }
            set { this.bbiGFN2_F1 = value; }
        }
        private BarButtonItem bbiGFN2_F2 = new BarButtonItem();
        public BarButtonItem BbiMainGFN2_F2
        {
            get { return this.bbiGFN2_F2; }
            set { this.bbiGFN2_F2 = value; }
        }
        private BarButtonItem bbiGFN2_F3 = new BarButtonItem();
        public BarButtonItem BbiMainGFN2_F3
        {
            get { return this.bbiGFN2_F3; }
            set { this.bbiGFN2_F3 = value; }
        }
        private BarButtonItem bbiGFN2_F4 = new BarButtonItem();
        public BarButtonItem BbiMainGFN2_F4
        {
            get { return this.bbiGFN2_F4; }
            set { this.bbiGFN2_F4 = value; }
        }
        private BarButtonItem bbiGFN2_F5 = new BarButtonItem();
        public BarButtonItem BbiMainGFN2_F5
        {
            get { return this.bbiGFN2_F5; }
            set { this.bbiGFN2_F5 = value; }
        }
        private BarButtonItem bbiGFN2_F6 = new BarButtonItem();
        public BarButtonItem BbiMainGFN2_F6
        {
            get { return this.bbiGFN2_F6; }
            set { this.bbiGFN2_F6 = value; }
        }




        private BarSubItem bbiGFN3 = new BarSubItem();
        public BarSubItem BbiMainGFN3
        {
            get { return this.bbiGFN3; }
            set { this.bbiGFN3 = value; }
        }


        private BarButtonItem bbiGFN3_F1 = new BarButtonItem();
        public BarButtonItem BbiMainGFN3_F1
        {
            get { return this.bbiGFN3_F1; }
            set { this.bbiGFN3_F1 = value; }
        }
        private BarButtonItem bbiGFN3_F2 = new BarButtonItem();
        public BarButtonItem BbiMainGFN3_F2
        {
            get { return this.bbiGFN3_F2; }
            set { this.bbiGFN3_F2 = value; }
        }
        private BarButtonItem bbiGFN3_F3 = new BarButtonItem();
        public BarButtonItem BbiMainGFN3_F3
        {
            get { return this.bbiGFN3_F3; }
            set { this.bbiGFN3_F3 = value; }
        }
        private BarButtonItem bbiGFN3_F4 = new BarButtonItem();
        public BarButtonItem BbiMainGFN3_F4
        {
            get { return this.bbiGFN3_F4; }
            set { this.bbiGFN3_F4 = value; }
        }
        private BarButtonItem bbiGFN3_F5 = new BarButtonItem();
        public BarButtonItem BbiMainGFN3_F5
        {
            get { return this.bbiGFN3_F5; }
            set { this.bbiGFN3_F5 = value; }
        }
        private BarButtonItem bbiGFN3_F6 = new BarButtonItem();
        public BarButtonItem BbiMainGFN3_F6
        {
            get { return this.bbiGFN3_F6; }
            set { this.bbiGFN3_F6 = value; }
        }





        private BarSubItem bbiGFN4 = new BarSubItem();
        public BarSubItem BbiMainGFN4
        {
            get { return this.bbiGFN4; }
            set { this.bbiGFN4 = value; }
        }


        private BarButtonItem bbiGFN4_F1 = new BarButtonItem();
        public BarButtonItem BbiMainGFN4_F1
        {
            get { return this.bbiGFN4_F1; }
            set { this.bbiGFN4_F1 = value; }
        }
        private BarButtonItem bbiGFN4_F2 = new BarButtonItem();
        public BarButtonItem BbiMainGFN4_F2
        {
            get { return this.bbiGFN4_F2; }
            set { this.bbiGFN4_F2 = value; }
        }
        private BarButtonItem bbiGFN4_F3 = new BarButtonItem();
        public BarButtonItem BbiMainGFN4_F3
        {
            get { return this.bbiGFN4_F3; }
            set { this.bbiGFN4_F3 = value; }
        }
        private BarButtonItem bbiGFN4_F4 = new BarButtonItem();
        public BarButtonItem BbiMainGFN4_F4
        {
            get { return this.bbiGFN4_F4; }
            set { this.bbiGFN4_F4 = value; }
        }
        private BarButtonItem bbiGFN4_F5 = new BarButtonItem();
        public BarButtonItem BbiMainGFN4_F5
        {
            get { return this.bbiGFN4_F5; }
            set { this.bbiGFN4_F5 = value; }
        }
        private BarButtonItem bbiGFN4_F6 = new BarButtonItem();
        public BarButtonItem BbiMainGFN4_F6
        {
            get { return this.bbiGFN4_F6; }
            set { this.bbiGFN4_F6 = value; }
        }





        private BarSubItem bbiGFN5 = new BarSubItem();
        public BarSubItem BbiMainGFN5
        {
            get { return this.bbiGFN5; }
            set { this.bbiGFN5 = value; }
        }


        private BarButtonItem bbiGFN5_F1 = new BarButtonItem();
        public BarButtonItem BbiMainGFN5_F1
        {
            get { return this.bbiGFN5_F1; }
            set { this.bbiGFN5_F1 = value; }
        }
        private BarButtonItem bbiGFN5_F2 = new BarButtonItem();
        public BarButtonItem BbiMainGFN5_F2
        {
            get { return this.bbiGFN5_F2; }
            set { this.bbiGFN5_F2 = value; }
        }
        private BarButtonItem bbiGFN5_F3 = new BarButtonItem();
        public BarButtonItem BbiMainGFN5_F3
        {
            get { return this.bbiGFN5_F3; }
            set { this.bbiGFN5_F3 = value; }
        }
        private BarButtonItem bbiGFN5_F4 = new BarButtonItem();
        public BarButtonItem BbiMainGFN5_F4
        {
            get { return this.bbiGFN5_F4; }
            set { this.bbiGFN5_F4 = value; }
        }
        private BarButtonItem bbiGFN5_F5 = new BarButtonItem();
        public BarButtonItem BbiMainGFN5_F5
        {
            get { return this.bbiGFN5_F5; }
            set { this.bbiGFN5_F5 = value; }
        }
        private BarButtonItem bbiGFN5_F6 = new BarButtonItem();
        public BarButtonItem BbiMainGFN5_F6
        {
            get { return this.bbiGFN5_F6; }
            set { this.bbiGFN5_F6 = value; }
        }





        private BarButtonItem bbiFN_1 = new BarButtonItem();
        public BarButtonItem BbiMainFN_1
        {
            get { return this.bbiFN_1; }
            set { this.bbiFN_1 = value; }
        }
        private BarButtonItem bbiFN_2 = new BarButtonItem();
        public BarButtonItem BbiMainFN_2
        {
            get { return this.bbiFN_2; }
            set { this.bbiFN_2 = value; }
        }
        private BarButtonItem bbiFN_3 = new BarButtonItem();
        public BarButtonItem BbiMainFN_3
        {
            get { return this.bbiFN_3; }
            set { this.bbiFN_3 = value; }
        }
        private BarButtonItem bbiFN_4 = new BarButtonItem();
        public BarButtonItem BbiMainFN_4
        {
            get { return this.bbiFN_4; }
            set { this.bbiFN_4 = value; }
        }
        private BarButtonItem bbiFN_5 = new BarButtonItem();
        public BarButtonItem BbiMainFN_5
        {
            get { return this.bbiFN_5; }
            set { this.bbiFN_5 = value; }
        }

        #endregion


        #region "Hide Or Visible"
        private bool _allowGFN1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN1
        {
            get { return _allowGFN1; }
            set
            {
                _allowGFN1 = value;
                bbiGFN1.Visibility = !_allowGFN1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN1_F1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN1_F1
        {
            get { return _allowGFN1_F1; }
            set
            {
                _allowGFN1_F1 = value;
                bbiGFN1_F1.Visibility = !_allowGFN1_F1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN1_F2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN1_F2
        {
            get { return _allowGFN1_F2; }
            set
            {
                _allowGFN1_F2 = value;
                bbiGFN1_F2.Visibility = !_allowGFN1_F2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN1_F3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN1_F3
        {
            get { return _allowGFN1_F3; }
            set
            {
                _allowGFN1_F3 = value;
                bbiGFN1_F3.Visibility = !_allowGFN1_F3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }


        private bool _allowGFN1_F4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN1_F4
        {
            get { return _allowGFN1_F4; }
            set
            {
                _allowGFN1_F4 = value;
                bbiGFN1_F4.Visibility = !_allowGFN1_F4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN1_F5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN1_F5
        {
            get { return _allowGFN1_F5; }
            set
            {
                _allowGFN1_F5 = value;
                bbiGFN1_F5.Visibility = !_allowGFN1_F5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN1_F6 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN1_F6
        {
            get { return _allowGFN1_F6; }
            set
            {
                _allowGFN1_F6 = value;
                bbiGFN1_F6.Visibility = !_allowGFN1_F6 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private bool _allowGFN2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN2
        {
            get { return _allowGFN2; }
            set
            {
                _allowGFN2 = value;
                bbiGFN2.Visibility = !_allowGFN2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN2_F1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN2_F1
        {
            get { return _allowGFN2_F1; }
            set
            {
                _allowGFN2_F1 = value;
                bbiGFN2_F1.Visibility = !_allowGFN2_F1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN2_F2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN2_F2
        {
            get { return _allowGFN2_F2; }
            set
            {
                _allowGFN2_F2 = value;
                bbiGFN2_F2.Visibility = !_allowGFN2_F2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN2_F3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN2_F3
        {
            get { return _allowGFN2_F3; }
            set
            {
                _allowGFN2_F3 = value;
                bbiGFN2_F3.Visibility = !_allowGFN2_F3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }


        private bool _allowGFN2_F4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN2_F4
        {
            get { return _allowGFN2_F4; }
            set
            {
                _allowGFN2_F4 = value;
                bbiGFN2_F4.Visibility = !_allowGFN2_F4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN2_F5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN2_F5
        {
            get { return _allowGFN2_F5; }
            set
            {
                _allowGFN2_F5 = value;
                bbiGFN2_F5.Visibility = !_allowGFN2_F5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN2_F6 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN2_F6
        {
            get { return _allowGFN2_F6; }
            set
            {
                _allowGFN2_F6 = value;
                bbiGFN2_F6.Visibility = !_allowGFN2_F6 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private bool _allowGFN3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN3
        {
            get { return _allowGFN3; }
            set
            {
                _allowGFN3 = value;
                bbiGFN3.Visibility = !_allowGFN3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN3_F1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN3_F1
        {
            get { return _allowGFN3_F1; }
            set
            {
                _allowGFN3_F1 = value;
                bbiGFN3_F1.Visibility = !_allowGFN3_F1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN3_F2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN3_F2
        {
            get { return _allowGFN3_F2; }
            set
            {
                _allowGFN3_F2 = value;
                bbiGFN3_F2.Visibility = !_allowGFN3_F2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN3_F3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN3_F3
        {
            get { return _allowGFN3_F3; }
            set
            {
                _allowGFN3_F3 = value;
                bbiGFN3_F3.Visibility = !_allowGFN3_F3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }


        private bool _allowGFN3_F4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN3_F4
        {
            get { return _allowGFN3_F4; }
            set
            {
                _allowGFN3_F4 = value;
                bbiGFN3_F4.Visibility = !_allowGFN3_F4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN3_F5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN3_F5
        {
            get { return _allowGFN3_F5; }
            set
            {
                _allowGFN3_F5 = value;
                bbiGFN3_F5.Visibility = !_allowGFN3_F5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN3_F6 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN3_F6
        {
            get { return _allowGFN3_F6; }
            set
            {
                _allowGFN3_F6 = value;
                bbiGFN3_F6.Visibility = !_allowGFN3_F6 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private bool _allowGFN4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN4
        {
            get { return _allowGFN4; }
            set
            {
                _allowGFN4 = value;
                bbiGFN4.Visibility = !_allowGFN4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN4_F1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN4_F1
        {
            get { return _allowGFN4_F1; }
            set
            {
                _allowGFN4_F1 = value;
                bbiGFN4_F1.Visibility = !_allowGFN4_F1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN4_F2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN4_F2
        {
            get { return _allowGFN4_F2; }
            set
            {
                _allowGFN4_F2 = value;
                bbiGFN4_F2.Visibility = !_allowGFN4_F2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN4_F3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN4_F3
        {
            get { return _allowGFN4_F3; }
            set
            {
                _allowGFN4_F3 = value;
                bbiGFN4_F3.Visibility = !_allowGFN4_F3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }


        private bool _allowGFN4_F4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN4_F4
        {
            get { return _allowGFN4_F4; }
            set
            {
                _allowGFN4_F4 = value;
                bbiGFN4_F4.Visibility = !_allowGFN4_F4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN4_F5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN4_F5
        {
            get { return _allowGFN4_F5; }
            set
            {
                _allowGFN4_F5 = value;
                bbiGFN4_F5.Visibility = !_allowGFN4_F5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN4_F6 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN4_F6
        {
            get { return _allowGFN4_F6; }
            set
            {
                _allowGFN4_F6 = value;
                bbiGFN4_F6.Visibility = !_allowGFN4_F6 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private bool _allowGFN5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN5
        {
            get { return _allowGFN5; }
            set
            {
                _allowGFN5 = value;
                bbiGFN5.Visibility = !_allowGFN5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN5_F1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN5_F1
        {
            get { return _allowGFN5_F1; }
            set
            {
                _allowGFN5_F1 = value;
                bbiGFN5_F1.Visibility = !_allowGFN5_F1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN5_F2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN5_F2
        {
            get { return _allowGFN5_F2; }
            set
            {
                _allowGFN5_F2 = value;
                bbiGFN5_F2.Visibility = !_allowGFN5_F2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN5_F3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN5_F3
        {
            get { return _allowGFN5_F3; }
            set
            {
                _allowGFN5_F3 = value;
                bbiGFN5_F3.Visibility = !_allowGFN5_F3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }


        private bool _allowGFN5_F4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN5_F4
        {
            get { return _allowGFN5_F4; }
            set
            {
                _allowGFN5_F4 = value;
                bbiGFN5_F4.Visibility = !_allowGFN5_F4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN5_F5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN5_F5
        {
            get { return _allowGFN5_F5; }
            set
            {
                _allowGFN5_F5 = value;
                bbiGFN5_F5.Visibility = !_allowGFN5_F5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowGFN5_F6 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowGFN5_F6
        {
            get { return _allowGFN5_F6; }
            set
            {
                _allowGFN5_F6 = value;
                bbiGFN5_F6.Visibility = !_allowGFN5_F6 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        
        private bool _allowFN1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowFN1
        {
            get { return _allowFN1; }
            set
            {
                _allowFN1 = value;
                bbiFN_1.Visibility = !_allowFN1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }



      

        private bool _allowFN2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowFN2
        {
            get { return _allowFN2; }
            set
            {
                _allowFN2 = value;
                bbiFN_2.Visibility = !_allowFN2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowFN3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowFN3
        {
            get { return _allowFN3; }
            set
            {
                _allowFN3 = value;
                bbiFN_3.Visibility = !_allowFN3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }


        private bool _allowFN4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowFN4
        {
            get { return _allowFN4; }
            set
            {
                _allowFN4 = value;
                bbiFN_4.Visibility = !_allowFN4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        private bool _allowFN5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool AllowFN5
        {
            get { return _allowFN5; }
            set
            {
                _allowFN5 = value;
                bbiFN_5.Visibility = !_allowFN5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        #endregion


        #region "Enable"
        private bool _enableGFN1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN1
        {
            get { return _enableGFN1; }
            set
            {
                _enableGFN1 = value;
                bbiGFN1.Enabled = value;
            }
        }

        private bool _enableGFN1_F1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN1_F1
        {
            get { return _enableGFN1_F1; }
            set
            {
                _enableGFN1_F1 = value;
                bbiGFN1_F1.Enabled = value;
            }
        }

        private bool _enableGFN1_F2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN1_F2
        {
            get { return _enableGFN1_F2; }
            set
            {
                _enableGFN1_F2 = value;
                bbiGFN1_F2.Enabled = value;
            }
        }

        private bool _enableGFN1_F3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN1_F3
        {
            get { return _enableGFN1_F3; }
            set
            {
                _enableGFN1_F3 = value;
                bbiGFN1_F3.Enabled = value;
            }
        }


        private bool _enableGFN1_F4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN1_F4
        {
            get { return _enableGFN1_F4; }
            set
            {
                _enableGFN1_F4 = value;
                bbiGFN1_F4.Enabled = value;
            }
        }

        private bool _enableGFN1_F5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN1_F5
        {
            get { return _enableGFN1_F5; }
            set
            {
                _enableGFN1_F5 = value;
                bbiGFN1_F5.Enabled = value;
            }
        }

        private bool _enableGFN1_F6 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN1_F6
        {
            get { return _enableGFN1_F6; }
            set
            {
                _enableGFN1_F6 = value;
                bbiGFN1_F6.Enabled = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private bool _enableGFN2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN2
        {
            get { return _enableGFN2; }
            set
            {
                _enableGFN2 = value;
                bbiGFN2.Enabled = value;
            }
        }

        private bool _enableGFN2_F1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN2_F1
        {
            get { return _enableGFN2_F1; }
            set
            {
                _enableGFN2_F1 = value;
                bbiGFN2_F1.Enabled = value;
            }
        }

        private bool _enableGFN2_F2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN2_F2
        {
            get { return _enableGFN2_F2; }
            set
            {
                _enableGFN2_F2 = value;
                bbiGFN2_F2.Enabled = value;
            }
        }

        private bool _enableGFN2_F3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN2_F3
        {
            get { return _enableGFN2_F3; }
            set
            {
                _enableGFN2_F3 = value;
                bbiGFN2_F3.Enabled = value;
            }
        }


        private bool _enableGFN2_F4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN2_F4
        {
            get { return _enableGFN2_F4; }
            set
            {
                _enableGFN2_F4 = value;
                bbiGFN2_F4.Enabled = value;
            }
        }

        private bool _enableGFN2_F5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN2_F5
        {
            get { return _enableGFN2_F5; }
            set
            {
                _enableGFN2_F5 = value;
                bbiGFN2_F5.Enabled = value;
            }
        }

        private bool _enableGFN2_F6 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN2_F6
        {
            get { return _enableGFN2_F6; }
            set
            {
                _enableGFN2_F6 = value;
                bbiGFN2_F6.Enabled = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private bool _enableGFN3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN3
        {
            get { return _enableGFN3; }
            set
            {
                _enableGFN3 = value;
                bbiGFN3.Enabled = value;
            }
        }

        private bool _enableGFN3_F1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN3_F1
        {
            get { return _enableGFN3_F1; }
            set
            {
                _enableGFN3_F1 = value;
                bbiGFN3_F1.Enabled = value;
            }
        }

        private bool _enableGFN3_F2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN3_F2
        {
            get { return _enableGFN3_F2; }
            set
            {
                _enableGFN3_F2 = value;
                bbiGFN3_F2.Enabled = value;
            }
        }

        private bool _enableGFN3_F3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN3_F3
        {
            get { return _enableGFN3_F3; }
            set
            {
                _enableGFN3_F3 = value;
                bbiGFN3_F3.Enabled = value;
            }
        }


        private bool _enableGFN3_F4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN3_F4
        {
            get { return _enableGFN3_F4; }
            set
            {
                _enableGFN3_F4 = value;
                bbiGFN3_F4.Enabled = value;
            }
        }

        private bool _enableGFN3_F5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN3_F5
        {
            get { return _enableGFN3_F5; }
            set
            {
                _enableGFN3_F5 = value;
                bbiGFN3_F5.Enabled = value;
            }
        }

        private bool _enableGFN3_F6 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN3_F6
        {
            get { return _enableGFN3_F6; }
            set
            {
                _enableGFN3_F6 = value;
                bbiGFN3_F6.Enabled = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private bool _enableGFN4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN4
        {
            get { return _enableGFN4; }
            set
            {
                _enableGFN4 = value;
                bbiGFN4.Enabled = value;
            }
        }

        private bool _enableGFN4_F1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN4_F1
        {
            get { return _enableGFN4_F1; }
            set
            {
                _enableGFN4_F1 = value;
                bbiGFN4_F1.Enabled = value;
            }
        }

        private bool _enableGFN4_F2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN4_F2
        {
            get { return _enableGFN4_F2; }
            set
            {
                _enableGFN4_F2 = value;
                bbiGFN4_F2.Enabled = value;
            }
        }

        private bool _enableGFN4_F3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN4_F3
        {
            get { return _enableGFN4_F3; }
            set
            {
                _enableGFN4_F3 = value;
                bbiGFN4_F3.Enabled = value;
            }
        }


        private bool _enableGFN4_F4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN4_F4
        {
            get { return _enableGFN4_F4; }
            set
            {
                _enableGFN4_F4 = value;
                bbiGFN4_F4.Enabled = value;
            }
        }

        private bool _enableGFN4_F5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN4_F5
        {
            get { return _enableGFN4_F5; }
            set
            {
                _enableGFN4_F5 = value;
                bbiGFN4_F5.Enabled = value;
            }
        }

        private bool _enableGFN4_F6 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN4_F6
        {
            get { return _enableGFN4_F6; }
            set
            {
                _enableGFN4_F6 = value;
                bbiGFN4_F6.Enabled = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private bool _enableGFN5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN5
        {
            get { return _enableGFN5; }
            set
            {
                _enableGFN5 = value;
                bbiGFN5.Enabled = value;
            }
        }

        private bool _enableGFN5_F1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN5_F1
        {
            get { return _enableGFN5_F1; }
            set
            {
                _enableGFN5_F1 = value;
                bbiGFN5_F1.Enabled = value;
            }
        }

        private bool _enableGFN5_F2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN5_F2
        {
            get { return _enableGFN5_F2; }
            set
            {
                _enableGFN5_F2 = value;
                bbiGFN5_F2.Enabled = value;
            }
        }

        private bool _enableGFN5_F3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN5_F3
        {
            get { return _enableGFN5_F3; }
            set
            {
                _enableGFN5_F3 = value;
                bbiGFN5_F3.Enabled = value;
            }
        }


        private bool _enableGFN5_F4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN5_F4
        {
            get { return _enableGFN5_F4; }
            set
            {
                _enableGFN5_F4 = value;
                bbiGFN5_F4.Enabled = value;
            }
        }

        private bool _enableGFN5_F5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN5_F5
        {
            get { return _enableGFN5_F5; }
            set
            {
                _enableGFN5_F5 = value;
                bbiGFN5_F5.Enabled = value;
            }
        }

        private bool _enableGFN5_F6 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGFN5_F6
        {
            get { return _enableGFN5_F6; }
            set
            {
                _enableGFN5_F6 = value;
                bbiGFN5_F6.Enabled = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        private bool _enableFN1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableFN1
        {
            get { return _enableFN1; }
            set
            {
                _enableFN1 = value;
                bbiFN_1.Enabled = value;
            }
        }





        private bool _enableFN2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableFN2
        {
            get { return _enableFN2; }
            set
            {
                _enableFN2 = value;
                bbiFN_2.Enabled = value;
            }
        }

        private bool _enableFN3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableFN3
        {
            get { return _enableFN3; }
            set
            {
                _enableFN3 = value;
                bbiFN_3.Enabled = value;
            }
        }


        private bool _enableFN4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableFN4
        {
            get { return _enableFN4; }
            set
            {
                _enableFN4 = value;
                bbiFN_4.Enabled = value;
            }
        }
        private bool _enableFN5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableFN5
        {
            get { return _enableFN5; }
            set
            {
                _enableFN5 = value;
                bbiFN_5.Enabled = value;
            }
        }
        #endregion


        #region "Caption"
        private string _captionGFN1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN1
        {
            get { return _captionGFN1; }
            set
            {
                _captionGFN1 = value;
                bbiGFN1.Caption = value;
            }
        }

        private string _captionGFN1_F1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN1_F1
        {
            get { return _captionGFN1_F1; }
            set
            {
                _captionGFN1_F1 = value;
                bbiGFN1_F1.Caption = value;
            }
        }

        private string _captionGFN1_F2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN1_F2
        {
            get { return _captionGFN1_F2; }
            set
            {
                _captionGFN1_F2 = value;
                bbiGFN1_F2.Caption = value;
            }
        }

        private string _captionGFN1_F3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN1_F3
        {
            get { return _captionGFN1_F3; }
            set
            {
                _captionGFN1_F3 = value;
                bbiGFN1_F3.Caption = value;
            }
        }


        private string _captionGFN1_F4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN1_F4
        {
            get { return _captionGFN1_F4; }
            set
            {
                _captionGFN1_F4 = value;
                bbiGFN1_F4.Caption = value;
            }
        }

        private string _captionGFN1_F5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN1_F5
        {
            get { return _captionGFN1_F5; }
            set
            {
                _captionGFN1_F5 = value;
                bbiGFN1_F5.Caption = value;
            }
        }

        private string _captionGFN1_F6 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN1_F6
        {
            get { return _captionGFN1_F6; }
            set
            {
                _captionGFN1_F6 = value;
                bbiGFN1_F6.Caption = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private string _captionGFN2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN2
        {
            get { return _captionGFN2; }
            set
            {
                _captionGFN2 = value;
                bbiGFN2.Caption = value;
            }
        }

        private string _captionGFN2_F1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN2_F1
        {
            get { return _captionGFN2_F1; }
            set
            {
                _captionGFN2_F1 = value;
                bbiGFN2_F1.Caption = value;
            }
        }

        private string _captionGFN2_F2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN2_F2
        {
            get { return _captionGFN2_F2; }
            set
            {
                _captionGFN2_F2 = value;
                bbiGFN2_F2.Caption = value;
            }
        }

        private string _captionGFN2_F3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN2_F3
        {
            get { return _captionGFN2_F3; }
            set
            {
                _captionGFN2_F3 = value;
                bbiGFN2_F3.Caption = value;
            }
        }


        private string _captionGFN2_F4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN2_F4
        {
            get { return _captionGFN2_F4; }
            set
            {
                _captionGFN2_F4 = value;
                bbiGFN2_F4.Caption = value;
            }
        }

        private string _captionGFN2_F5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN2_F5
        {
            get { return _captionGFN2_F5; }
            set
            {
                _captionGFN2_F5 = value;
                bbiGFN2_F5.Caption = value;
            }
        }

        private string _captionGFN2_F6 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN2_F6
        {
            get { return _captionGFN2_F6; }
            set
            {
                _captionGFN2_F6 = value;
                bbiGFN2_F6.Caption = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private string _captionGFN3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN3
        {
            get { return _captionGFN3; }
            set
            {
                _captionGFN3 = value;
                bbiGFN3.Caption = value;
            }
        }

        private string _captionGFN3_F1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN3_F1
        {
            get { return _captionGFN3_F1; }
            set
            {
                _captionGFN3_F1 = value;
                bbiGFN3_F1.Caption = value;
            }
        }

        private string _captionGFN3_F2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN3_F2
        {
            get { return _captionGFN3_F2; }
            set
            {
                _captionGFN3_F2 = value;
                bbiGFN3_F2.Caption = value;
            }
        }

        private string _captionGFN3_F3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN3_F3
        {
            get { return _captionGFN3_F3; }
            set
            {
                _captionGFN3_F3 = value;
                bbiGFN3_F3.Caption = value;
            }
        }


        private string _captionGFN3_F4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN3_F4
        {
            get { return _captionGFN3_F4; }
            set
            {
                _captionGFN3_F4 = value;
                bbiGFN3_F4.Caption = value;
            }
        }

        private string _captionGFN3_F5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN3_F5
        {
            get { return _captionGFN3_F5; }
            set
            {
                _captionGFN3_F5 = value;
                bbiGFN3_F5.Caption = value;
            }
        }

        private string _captionGFN3_F6 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN3_F6
        {
            get { return _captionGFN3_F6; }
            set
            {
                _captionGFN3_F6 = value;
                bbiGFN3_F6.Caption = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private string _captionGFN4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN4
        {
            get { return _captionGFN4; }
            set
            {
                _captionGFN4 = value;
                bbiGFN4.Caption = value;
            }
        }

        private string _captionGFN4_F1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN4_F1
        {
            get { return _captionGFN4_F1; }
            set
            {
                _captionGFN4_F1 = value;
                bbiGFN4_F1.Caption = value;
            }
        }

        private string _captionGFN4_F2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN4_F2
        {
            get { return _captionGFN4_F2; }
            set
            {
                _captionGFN4_F2 = value;
                bbiGFN4_F2.Caption = value;
            }
        }

        private string _captionGFN4_F3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN4_F3
        {
            get { return _captionGFN4_F3; }
            set
            {
                _captionGFN4_F3 = value;
                bbiGFN4_F3.Caption = value;
            }
        }


        private string _captionGFN4_F4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN4_F4
        {
            get { return _captionGFN4_F4; }
            set
            {
                _captionGFN4_F4 = value;
                bbiGFN4_F4.Caption = value;
            }
        }

        private string _captionGFN4_F5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN4_F5
        {
            get { return _captionGFN4_F5; }
            set
            {
                _captionGFN4_F5 = value;
                bbiGFN4_F5.Caption = value;
            }
        }

        private string _captionGFN4_F6 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN4_F6
        {
            get { return _captionGFN4_F6; }
            set
            {
                _captionGFN4_F6 = value;
                bbiGFN4_F6.Caption = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private string _captionGFN5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN5
        {
            get { return _captionGFN5; }
            set
            {
                _captionGFN5 = value;
                bbiGFN5.Caption = value;
            }
        }

        private string _captionGFN5_F1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN5_F1
        {
            get { return _captionGFN5_F1; }
            set
            {
                _captionGFN5_F1 = value;
                bbiGFN5_F1.Caption = value;
            }
        }

        private string _captionGFN5_F2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN5_F2
        {
            get { return _captionGFN5_F2; }
            set
            {
                _captionGFN5_F2 = value;
                bbiGFN5_F2.Caption = value;
            }
        }

        private string _captionGFN5_F3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN5_F3
        {
            get { return _captionGFN5_F3; }
            set
            {
                _captionGFN5_F3 = value;
                bbiGFN5_F3.Caption = value;
            }
        }


        private string _captionGFN5_F4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN5_F4
        {
            get { return _captionGFN5_F4; }
            set
            {
                _captionGFN5_F4 = value;
                bbiGFN5_F4.Caption = value;
            }
        }

        private string _captionGFN5_F5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN5_F5
        {
            get { return _captionGFN5_F5; }
            set
            {
                _captionGFN5_F5 = value;
                bbiGFN5_F5.Caption = value;
            }
        }

        private string _captionGFN5_F6 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionGFN5_F6
        {
            get { return _captionGFN5_F6; }
            set
            {
                _captionGFN5_F6 = value;
                bbiGFN5_F6.Caption = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        private string _captionFN1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionFN1
        {
            get { return _captionFN1; }
            set
            {
                _captionFN1 = value;
                bbiFN_1.Caption = value;
            }
        }





        private string _captionFN2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionFN2
        {
            get { return _captionFN2; }
            set
            {
                _captionFN2 = value;
                bbiFN_2.Caption = value;
            }
        }

        private string _captionFN3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionFN3
        {
            get { return _captionFN3; }
            set
            {
                _captionFN3 = value;
                bbiFN_3.Caption = value;
            }
        }


        private string _captionFN4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionFN4
        {
            get { return _captionFN4; }
            set
            {
                _captionFN4 = value;
                bbiFN_4.Caption = value;
            }
        }
        private string _captionFN5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;

        public string SetCaptionFN5
        {
            get { return _captionFN5; }
            set
            {
                _captionFN5 = value;
                bbiFN_5.Caption = value;
            }
        }
        #endregion


     


        #region "Image"
        private Image _imageGFN1 = CommandBarDefaultSetting.ImageButtonFunction;

        public Image SetImageGFN1
        {
            get { return _imageGFN1; }
            set
            {
                _imageGFN1 = value;
                bbiGFN1.ImageOptions.LargeImage = value;
            }
        }

        private Image _imageGFN1_F1 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN1_F1
        {
            get { return _imageGFN1_F1; }
            set
            {
                _imageGFN1_F1 = value;
                bbiGFN1_F1.Glyph = value;
            }
        }

        private Image _imageGFN1_F2 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN1_F2
        {
            get { return _imageGFN1_F2; }
            set
            {
                _imageGFN1_F2 = value;
                bbiGFN1_F2.Glyph = value;
            }
        }

        private Image _imageGFN1_F3 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN1_F3
        {
            get { return _imageGFN1_F3; }
            set
            {
                _imageGFN1_F3 = value;
                bbiGFN1_F3.Glyph = value;
            }
        }


        private Image _imageGFN1_F4 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN1_F4
        {
            get { return _imageGFN1_F4; }
            set
            {
                _imageGFN1_F4 = value;
                bbiGFN1_F4.Glyph = value;
            }
        }

        private Image _imageGFN1_F5 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN1_F5
        {
            get { return _imageGFN1_F5; }
            set
            {
                _imageGFN1_F5 = value;
                bbiGFN1_F5.Glyph = value;
            }
        }

        private Image _imageGFN1_F6 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN1_F6
        {
            get { return _imageGFN1_F6; }
            set
            {
                _imageGFN1_F6 = value;
                bbiGFN1_F6.Glyph = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private Image _imageGFN2 = CommandBarDefaultSetting.ImageButtonFunction;

        public Image SetImageGFN2
        {
            get { return _imageGFN2; }
            set
            {
                _imageGFN2 = value;
                bbiGFN2.ImageOptions.LargeImage = value;
            }
        }

        private Image _imageGFN2_F1 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN2_F1
        {
            get { return _imageGFN2_F1; }
            set
            {
                _imageGFN2_F1 = value;
                bbiGFN2_F1.Glyph = value;
            }
        }

        private Image _imageGFN2_F2 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN2_F2
        {
            get { return _imageGFN2_F2; }
            set
            {
                _imageGFN2_F2 = value;
                bbiGFN2_F2.Glyph = value;
            }
        }

        private Image _imageGFN2_F3 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN2_F3
        {
            get { return _imageGFN2_F3; }
            set
            {
                _imageGFN2_F3 = value;
                bbiGFN2_F3.Glyph = value;
            }
        }


        private Image _imageGFN2_F4 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN2_F4
        {
            get { return _imageGFN2_F4; }
            set
            {
                _imageGFN2_F4 = value;
                bbiGFN2_F4.Glyph = value;
            }
        }

        private Image _imageGFN2_F5 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN2_F5
        {
            get { return _imageGFN2_F5; }
            set
            {
                _imageGFN2_F5 = value;
                bbiGFN2_F5.Glyph = value;
            }
        }

        private Image _imageGFN2_F6 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN2_F6
        {
            get { return _imageGFN2_F6; }
            set
            {
                _imageGFN2_F6 = value;
                bbiGFN2_F6.Glyph = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private Image _imageGFN3 = CommandBarDefaultSetting.ImageButtonFunction;

        public Image SetImageGFN3
        {
            get { return _imageGFN3; }
            set
            {
                _imageGFN3 = value;
                bbiGFN3.ImageOptions.LargeImage = value;
            }
        }

        private Image _imageGFN3_F1 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN3_F1
        {
            get { return _imageGFN3_F1; }
            set
            {
                _imageGFN3_F1 = value;
                bbiGFN3_F1.Glyph = value;
            }
        }

        private Image _imageGFN3_F2 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN3_F2
        {
            get { return _imageGFN3_F2; }
            set
            {
                _imageGFN3_F2 = value;
                bbiGFN3_F2.Glyph = value;
            }
        }

        private Image _imageGFN3_F3 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN3_F3
        {
            get { return _imageGFN3_F3; }
            set
            {
                _imageGFN3_F3 = value;
                bbiGFN3_F3.Glyph = value;
            }
        }


        private Image _imageGFN3_F4 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN3_F4
        {
            get { return _imageGFN3_F4; }
            set
            {
                _imageGFN3_F4 = value;
                bbiGFN3_F4.Glyph = value;
            }
        }

        private Image _imageGFN3_F5 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN3_F5
        {
            get { return _imageGFN3_F5; }
            set
            {
                _imageGFN3_F5 = value;
                bbiGFN3_F5.Glyph = value;
            }
        }

        private Image _imageGFN3_F6 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN3_F6
        {
            get { return _imageGFN3_F6; }
            set
            {
                _imageGFN3_F6 = value;
                bbiGFN3_F6.Glyph = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private Image _imageGFN4 = CommandBarDefaultSetting.ImageButtonFunction;

        public Image SetImageGFN4
        {
            get { return _imageGFN4; }
            set
            {
                _imageGFN4 = value;
                bbiGFN4.ImageOptions.LargeImage = value;
            }
        }

        private Image _imageGFN4_F1 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN4_F1
        {
            get { return _imageGFN4_F1; }
            set
            {
                _imageGFN4_F1 = value;
                bbiGFN4_F1.Glyph = value;
            }
        }

        private Image _imageGFN4_F2 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN4_F2
        {
            get { return _imageGFN4_F2; }
            set
            {
                _imageGFN4_F2 = value;
                bbiGFN4_F2.Glyph = value;
            }
        }

        private Image _imageGFN4_F3 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN4_F3
        {
            get { return _imageGFN4_F3; }
            set
            {
                _imageGFN4_F3 = value;
                bbiGFN4_F3.Glyph = value;
            }
        }


        private Image _imageGFN4_F4 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN4_F4
        {
            get { return _imageGFN4_F4; }
            set
            {
                _imageGFN4_F4 = value;
                bbiGFN4_F4.Glyph = value;
            }
        }

        private Image _imageGFN4_F5 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN4_F5
        {
            get { return _imageGFN4_F5; }
            set
            {
                _imageGFN4_F5 = value;
                bbiGFN4_F5.Glyph = value;
            }
        }

        private Image _imageGFN4_F6 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN4_F6
        {
            get { return _imageGFN4_F6; }
            set
            {
                _imageGFN4_F6 = value;
                bbiGFN4_F6.Glyph = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private Image _imageGFN5 = CommandBarDefaultSetting.ImageButtonFunction;

        public Image SetImageGFN5
        {
            get { return _imageGFN5; }
            set
            {
                _imageGFN5 = value;
                bbiGFN5.ImageOptions.LargeImage = value;
            }
        }

        private Image _imageGFN5_F1 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN5_F1
        {
            get { return _imageGFN5_F1; }
            set
            {
                _imageGFN5_F1 = value;
                bbiGFN5_F1.Glyph = value;
            }
        }

        private Image _imageGFN5_F2 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN5_F2
        {
            get { return _imageGFN5_F2; }
            set
            {
                _imageGFN5_F2 = value;
                bbiGFN5_F2.Glyph = value;
            }
        }

        private Image _imageGFN5_F3 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN5_F3
        {
            get { return _imageGFN5_F3; }
            set
            {
                _imageGFN5_F3 = value;
                bbiGFN5_F3.Glyph = value;
            }
        }


        private Image _imageGFN5_F4 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN5_F4
        {
            get { return _imageGFN5_F4; }
            set
            {
                _imageGFN5_F4 = value;
                bbiGFN5_F4.Glyph = value;
            }
        }

        private Image _imageGFN5_F5 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN5_F5
        {
            get { return _imageGFN5_F5; }
            set
            {
                _imageGFN5_F5 = value;
                bbiGFN5_F5.Glyph = value;
            }
        }

        private Image _imageGFN5_F6 = CommandBarDefaultSetting.ImageButtonFunctionDown;

        public Image SetImageGFN5_F6
        {
            get { return _imageGFN5_F6; }
            set
            {
                _imageGFN5_F6 = value;
                bbiGFN5_F6.Glyph = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        private Image _imageFN1 = CommandBarDefaultSetting.ImageButtonFunction;

        public Image SetImageFN1
        {
            get { return _imageFN1; }
            set
            {
                _imageFN1 = value;
                bbiFN_1.ImageOptions.LargeImage = value;
            }
        }





        private Image _imageFN2 = CommandBarDefaultSetting.ImageButtonFunction;

        public Image SetImageFN2
        {
            get { return _imageFN2; }
            set
            {
                _imageFN2 = value;
                bbiFN_2.ImageOptions.LargeImage = value;
            }
        }

        private Image _imageFN3 = CommandBarDefaultSetting.ImageButtonFunction;

        public Image SetImageFN3
        {
            get { return _imageFN3; }
            set
            {
                _imageFN3 = value;
                bbiFN_3.ImageOptions.LargeImage = value;
            }
        }


        private Image _imageFN4 = CommandBarDefaultSetting.ImageButtonFunction;

        public Image SetImageFN4
        {
            get { return _imageFN4; }
            set
            {
                _imageFN4 = value;
                bbiFN_4.ImageOptions.LargeImage = value;
            }
        }
        private Image _imageFN5 = CommandBarDefaultSetting.ImageButtonFunction;

        public Image SetImageFN5
        {
            get { return _imageFN5; }
            set
            {
                _imageFN5 = value;
                bbiFN_5.ImageOptions.LargeImage = value;
            }
        }
        #endregion
        #endregion

        #region "Hide or Visible Button"


        private bool _allowAdd = CommandBarDefaultSetting.AllowButtonAddDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Thêm hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowAdd
        {
            get { return _allowAdd; }
            set
            {
                _allowAdd = value;
                bbiAdd.Visibility = !_allowAdd ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }


        private bool _allowEdit = CommandBarDefaultSetting.AllowButtonEditDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Sửa hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowEdit
        {
            get { return _allowEdit; }
            set
            {
                _allowEdit = value;
                bbiEdit.Visibility = !_allowEdit ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowDelete = CommandBarDefaultSetting.AllowButtonDeleteDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Xoá hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowDelete
        {
            get { return _allowDelete; }
            set
            {
                _allowDelete = value;
                bbiDelete.Visibility = !_allowDelete ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowSave = CommandBarDefaultSetting.AllowButtonSaveDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Lưu hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowSave
        {
            get { return _allowSave; }
            set
            {
                _allowSave = value;
                bbiSave.Visibility = !_allowSave ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowCancel = CommandBarDefaultSetting.AllowButtonCancelDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Huỷ hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowCancel
        {
            get { return _allowCancel; }
            set
            {
                _allowCancel = value;
                bbiCancel.Visibility = !_allowCancel ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowRefresh = CommandBarDefaultSetting.AllowButtonRefreshDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Cập nhật hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowRefresh
        {
            get { return _allowRefresh; }
            set
            {
                _allowRefresh = value;
                bbiRefresh.Visibility = !_allowRefresh ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }





        private bool _allowFind = CommandBarDefaultSetting.AllowButtonFindDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Tìm hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowFind
        {
            get { return _allowFind; }
            set
            {
                _allowFind = value;
                bbiFind.Visibility = !_allowFind ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowPrint = CommandBarDefaultSetting.AllowButtonPrintDefaultCommamd;
        /// <summary>
        /// Hiển thị nút In ấn hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowPrint
        {
            get { return _allowPrint; }
            set
            {
                _allowPrint = value;
                bbiPrint.Visibility = !_allowPrint ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }


        private bool _allowPrintDown = CommandBarDefaultSetting.AllowButtonPrintDownDefaultCommamd;
        /// <summary>
        /// Hiển thị nút In ấn hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowPrintDown
        {
            get { return _allowPrintDown; }
            set
            {
                _allowPrintDown = value;
                bbiPrintDown.Visibility = !_allowPrintDown ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowBreakDown = CommandBarDefaultSetting.AllowButtonBreakDownDefaultCommamd;
        /// <summary>
        /// Hiển thị nút break down hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowBreakDown
        {
            get { return _allowBreakDown; }
            set
            {
                _allowBreakDown = value;
                bbiBreakDown.Visibility = !_allowBreakDown ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        private bool _allowRevision = CommandBarDefaultSetting.AllowButtonRevisionDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Revision hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowRevision
        {
            get { return _allowRevision; }
            set
            {
                _allowRevision = value;
                bbiRevision.Visibility = !_allowRevision ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        private bool _allowRangeSize = CommandBarDefaultSetting.AllowButtonRangeSizeDefaultCommamd;
        /// <summary>
        /// Hiển thị nút RangeSize hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowRangeSize
        {
            get { return _allowRevision; }
            set
            {
                _allowRangeSize = value;
                bbiRangSize.Visibility = !_allowRangeSize ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowCopyObject = CommandBarDefaultSetting.AllowButtonCopyDefaultCommamd;
        /// <summary>
        /// Hiển thị nút CopyObject hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowCopyObject
        {
            get { return _allowCopyObject; }
            set
            {
                _allowCopyObject = value;
                bbiCopyObject.Visibility = !_allowCopyObject ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        private bool _allowGenerate = CommandBarDefaultSetting.AllowButtonGenerateDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Generate hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowGenerate
        {
            get { return _allowGenerate; }
            set
            {
                _allowGenerate = value;
                bbiGenerate.Visibility = !_allowGenerate ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        private bool _allowCombine = CommandBarDefaultSetting.AllowButtonCombineDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Combine hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowCombine
        {
            get { return _allowCombine; }
            set
            {
                _allowCombine = value;
                bbiCombine.Visibility = !_allowCombine ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        private bool _allowCheck = CommandBarDefaultSetting.AllowButtonCheckDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Check hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowCheck
        {
            get { return _allowCheck; }
            set
            {
                _allowCheck = value;
                bbiCheck.Visibility = !_allowCheck ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }

        private bool _allowCollapse = CommandBarDefaultSetting.AllowButtonCollosepandDefaultCommamd;
        /// <summary>
        /// Thu gọn tất cả cây
        /// </summary>
        public bool AllowCollapse
        {
            get { return _allowCollapse; }
            set
            {
                _allowCollapse = value;
                bbiCollapse.Visibility = value ? BarItemVisibility.Always : BarItemVisibility.Never;
              
            }
        }

        private bool _allowExpand = CommandBarDefaultSetting.AllowButtonExpandDefaultCommamd;
        /// <summary>
        /// Mở rộng tất cả cây
        /// </summary>
        public bool AllowExpand
        {
            get { return _allowExpand; }
            set
            {
                _allowExpand = value;
                bbiExpand.Visibility = value ? BarItemVisibility.Always : BarItemVisibility.Never;
              
            }
        }


        private bool _allowClose = CommandBarDefaultSetting.AllowButtonCloseDefaultCommamd;
        /// <summary>
        /// Hiển thị nút Sửa hay không
        /// </summary>
        [DefaultValue(false)]
        public bool AllowClose
        {
            get { return _allowClose; }
            set
            {
                _allowClose = value;
                bbiClose.Visibility = !_allowClose ? BarItemVisibility.Never : BarItemVisibility.Always;
            }
        }
        #endregion

        #region "Enable Button"

        
         /// <summary>
        /// Enable Print button on command bar manager
        /// </summary>
        private bool _enablePrint = CommandBarDefaultSetting.EnableButtonPrintDefaultCommamd;
        [DefaultValue(false)]
        public bool EnablePrint
        {
            get { return _enablePrint; }
            set
            {
                bbiPrint.Enabled = value;
                _enablePrint = value;
            }
        }

        /// <summary>
        /// Enable Print Down button on command bar manager
        /// </summary>
        private bool _enablePrintDown = CommandBarDefaultSetting.EnableButtonPrintDownDefaultCommamd;
        [DefaultValue(false)]
        public bool EnablePrintDown
        {
            get { return _enablePrintDown; }
            set
            {
                bbiPrintDown.Enabled = value;
                _enablePrintDown = value;
            }
        }


        /// <summary>
        /// Enable Add button on command bar manager
        /// </summary>
        private bool _enableAdd = CommandBarDefaultSetting.EnableButtonAddDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableAdd
        {
            get { return _enableAdd; }
            set
            {
                bbiAdd.Enabled = value;
                _enableAdd = value;
            }
        }

        /// <summary>
        /// Enable Edit button on command bar manager
        /// </summary>
        private bool _enableEdit = CommandBarDefaultSetting.EnableButtonEditDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableEdit
        {
            get { return _enableEdit; }
            set
            {
                bbiEdit.Enabled = value;
                _enableEdit = value;
            }
        }

        /// <summary>
        /// Enable Delete button on command bar manager
        /// </summary>
        private bool _enableDelete = CommandBarDefaultSetting.EnableButtonDeleteDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableDelete
        {
            get { return _enableDelete; }
            set
            {
                bbiDelete.Enabled = value;
                _enableDelete = value;
            }
        }

        /// <summary>
        /// Enable Save button on command bar manager
        /// </summary>
        private bool _enableSave = CommandBarDefaultSetting.EnableButtonSaveDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableSave
        {
            get { return _enableSave; }
            set
            {
                bbiSave.Enabled = value;
                _enableSave = value;
            }
        }

        /// <summary>
        /// Enable Cancel button on command bar manager
        /// </summary>
        private bool _enableCancel = CommandBarDefaultSetting.EnableButtonCancelDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableCancel
        {
            get { return _enableCancel; }
            set
            {
                bbiCancel.Enabled = value;
                _enableCancel = value;
            }
        }

        /// <summary>
        /// Enable Refresh button on command bar manager
        /// </summary>
        private bool _enableRefresh = CommandBarDefaultSetting.EnableButtonRefreshDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableRefresh
        {
            get { return _enableRefresh; }
            set
            {
                bbiRefresh.Enabled = value;
                _enableRefresh = value;
            }
        }

        /// <summary>
        /// Enable Find button on command bar manager
        /// </summary>
        private bool _enableFind = CommandBarDefaultSetting.EnableButtonFindDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableFind
        {
            get { return _enableFind; }
            set
            {
                bbiFind.Enabled = value;
                _enableFind = value;
            }
        }

        /// <summary>
        /// Enable BreakDown button on command bar manager
        /// </summary>
        private bool _enableBreakDown = CommandBarDefaultSetting.EnableButtonBreakDownDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableBreakDown
        {
            get { return _enableBreakDown; }
            set
            {
                bbiBreakDown.Enabled = value;
                _enableBreakDown = value;
            }
        }

        /// <summary>
        /// Enable Check button on command bar manager
        /// </summary>
        private bool _enableCheck = CommandBarDefaultSetting.EnableButtonCheckDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableCheck
        {
            get { return _enableCheck; }
            set
            {
                bbiCheck.Enabled = value;
                _enableCheck = value;
            }
        }


        /// <summary>
        /// Enable Close button on command bar manager
        /// </summary>
        private bool _enableClose = CommandBarDefaultSetting.EnableButtonCloseDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableClose
        {
            get { return _enableClose; }
            set
            {
                bbiClose.Enabled = value;
                _enableClose = value;
            }
        }

        /// <summary>
        /// Enable Expand button on command bar manager
        /// </summary>
        private bool _enableExpand = CommandBarDefaultSetting.EnableButtonExpandDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableExpand
        {
            get { return _enableExpand; }
            set
            {
                bbiExpand.Enabled = value;
                _enableExpand = value;
            }
        }


        /// <summary>
        /// Enable Collapse button on command bar manager
        /// </summary>
        private bool _enableCollapse = CommandBarDefaultSetting.EnableButtonCollosepandDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableCollapse
        {
            get { return _enableCollapse; }
            set
            {
                bbiCollapse.Enabled = value;
                _enableCollapse = value;
            }
        }

        /// <summary>
        /// Enable Revision button on command bar manager
        /// </summary>
        private bool _enableRevision = CommandBarDefaultSetting.EnableButtonRevisionDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableRevision
        {
            get { return _enableRevision; }
            set
            {
                bbiRevision.Enabled = value;
                _enableRevision = value;
            }
        }

        /// <summary>
        /// Enable Combine button on command bar manager
        /// </summary>
        private bool _enableCombine = CommandBarDefaultSetting.EnableButtonCombineDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableCombine
        {
            get { return _enableCombine; }
            set
            {
                bbiCombine.Enabled = value;
                _enableCombine = value;
            }
        }

        /// <summary>
        /// Enable Generate button on command bar manager
        /// </summary>
        private bool _enableGenerate = CommandBarDefaultSetting.EnableButtonGenerateDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableGenerate
        {
            get { return _enableGenerate; }
            set
            {
                bbiGenerate.Enabled = value;
                _enableGenerate = value;
            }
        }

       
        /// <summary>
        /// Enable Copy button on command bar manager
        /// </summary>
        private bool _enableCopyObject = CommandBarDefaultSetting.EnableButtonCopyDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableCopyObject
        {
            get { return _enableCopyObject; }
            set
            {
                bbiCopyObject.Enabled = value;
                _enableCopyObject = value;
            }
        }
        /// <summary>
        /// Enable RangSize button on command bar manager
        /// </summary>
        private bool _enableRangeSize = CommandBarDefaultSetting.EnableButtonRangeSizeDefaultCommamd;
        [DefaultValue(false)]
        public bool EnableRangSize
        {
            get { return _enableRangeSize; }
            set
            {
                bbiRangSize.Enabled = value;
                _enableRangeSize = value;
            }
        }    
       

        #endregion

        #region "Set Caption Button Menu"

        private string _captionPrint = CommandBarDefaultSetting.CaptionButtonPrintDefaultCommamd;
         /// <summary>
        /// Set Caption Print button on command bar manager
        /// </summary>
        public string SetCaptionPrint
        {
            get { return this._captionPrint; }
            set
            {
                _captionPrint = value;
                bbiPrint.Caption = value;
            }
        }


         private string _captionPrintDown = CommandBarDefaultSetting.CaptionButtonPrintDownDefaultCommamd;
         /// <summary>
         /// Set Caption Print button on command bar manager
         /// </summary>
         public string SetCaptionPrintDown
         {
             get { return this._captionPrintDown; }
             set
             {
                 _captionPrintDown = value;
                 bbiPrintDown.Caption = value;
             }
         }
        private string _captionAdd = CommandBarDefaultSetting.CaptionButtonAddDefaultCommamd;
        /// <summary>
        /// Set Caption  Add button on command bar manager
        /// </summary>
        public string SetCaptionAdd
        {
            get { return this._captionAdd; }
            set
            {
                this._captionAdd = value;
                bbiAdd.Caption = value;
            }
        }

        private string _captionEdit = CommandBarDefaultSetting.CaptionButtonEditDefaultCommamd;
        /// <summary>
        /// Set Caption Edit button on command bar manager
        /// </summary>
        public string SetCaptionEdit
        {
            get { return this._captionEdit; }
            set
            {
                this._captionEdit = value;
                bbiEdit.Caption = value;
            }
        }

        private string _captionDelete = CommandBarDefaultSetting.CaptionButtonDeleteDefaultCommamd;
        /// <summary>
        /// Set Caption Delete button on command bar manager
        /// </summary>
        public string SetCaptionDelete
        {
            get { return this._captionDelete; }
            set
            {
                this._captionDelete = value;
                bbiDelete.Caption = value;
            }
        }


        private string _captionSave = CommandBarDefaultSetting.CaptionButtonSaveDefaultCommamd;
        /// <summary>
        /// Set Caption Save button on command bar manager
        /// </summary>
        public string SetCaptionSave
        {
            get { return this._captionSave; }
            set
            {
                this._captionSave = value;
                bbiSave.Caption = value;
            }
        }

        private string _captionCancel = CommandBarDefaultSetting.CaptionButtonCancelDefaultCommamd;
        /// <summary>
        /// Set Caption Cancel button on command bar manager
        /// </summary>
        public string SetCaptionCancel
        {
            get { return this._captionCancel; }
            set
            {
                this._captionCancel = value;
                bbiCancel.Caption = value;
            }
        }

        private string _captionRefresh = CommandBarDefaultSetting.CaptionButtonRefreshDefaultCommamd;
        /// <summary>
        /// Set Caption Refresh button on command bar manager
        /// </summary>
        public string SetCaptionRefresh
        {
            get { return this._captionRefresh; }
            set
            {
                this._captionRefresh = value;
                bbiRefresh.Caption = value;
            }
        }

        private string _captionFind = CommandBarDefaultSetting.CaptionButtonFindDefaultCommamd;
        /// <summary>
        /// Set Caption Find button on command bar manager
        /// </summary>
        public string SetCaptionFind
        {
            get { return this._captionFind; }
            set
            {
                this._captionFind = value;
                bbiFind.Caption = value;
            }
        }

        private string _captionBreakDown = CommandBarDefaultSetting.CaptionButtonBreakDownDefaultCommamd;
        /// <summary>
        /// Set Caption BreakDown button on command bar manager
        /// </summary>
        public string SetCaptionBreakDown
        {
            get { return this._captionBreakDown; }
            set
            {
                this._captionBreakDown = value;
                bbiBreakDown.Caption = value;
            }
        }

        private string _captionCheck = CommandBarDefaultSetting.CaptionButtonCheckDefaultCommamd;
        /// <summary>
        /// Set Caption Check button on command bar manager
        /// </summary>
        public string SetCaptionCheck
        {
            get { return this._captionCheck; }
            set
            {
                this._captionCheck = value;
                bbiCheck.Caption = value;
            }
        }

        private string _captionClose = CommandBarDefaultSetting.CaptionButtonCloseDefaultCommamd;
        /// <summary>
        /// Set Caption Close button on command bar manager
        /// </summary>
        public string SetCaptionClose
        {
            get { return this._captionClose; }
            set
            {
                this._captionClose = value;
                bbiClose.Caption = value;
            }
        }

        private string _captionExpand = CommandBarDefaultSetting.CaptionButtonExpandDefaultCommamd;
        /// <summary>
        ///  Set Caption Expand button on command bar manager
        /// </summary>
        public string SetCaptionExpand
        {
            get { return this._captionExpand; }
            set
            {
                this._captionExpand = value;
                bbiExpand.Caption = value;
            }
        }

        private string _captionCollapse = CommandBarDefaultSetting.CaptionButtonCollosepandDefaultCommamd;
        /// <summary>
        /// Set Caption Collapse button on command bar manager
        /// </summary>
        public string SetCaptionCollapse
        {
            get { return this._captionCollapse; }
            set
            {
                this._captionCollapse = value;
                bbiCollapse.Caption = value;
            }
        }
        private string _captionRevision = CommandBarDefaultSetting.CaptionButtonRevisionDefaultCommamd;
        /// <summary>
        /// Set Caption Revision button on command bar manager
        /// </summary>
        public string SetCaptionRevision
        {
            get { return this._captionRevision; }
            set
            {
                this._captionRevision = value;
                bbiRevision.Caption = value;
            }
        }
        private string _captionCombine = CommandBarDefaultSetting.CaptionButtonCombineDefaultCommamd;
        /// <summary>
        /// Set Caption Combine button on command bar manager
        /// </summary>
        public string SetCaptionCombine
        {
            get { return this._captionCombine; }
            set
            {
                this._captionCombine = value;
                bbiCombine.Caption = value;
            }
        }
        private string _captionGenerate = CommandBarDefaultSetting.CaptionButtonGenerateDefaultCommamd;
        /// <summary>
        /// Set Caption Generate button on command bar manager
        /// </summary>
        public string SetCaptionGenerate
        {
            get { return this._captionGenerate; }
            set
            {
                this._captionGenerate = value;
                bbiGenerate.Caption = value;
            }
        }


        private string _captionCopyObject = CommandBarDefaultSetting.CaptionButtonCopyDefaultCommamd;
        /// <summary>
        /// Set Caption Copy button on command bar manager
        /// </summary>
        public string SetCaptionCopyObject
        {
            get { return this._captionCopyObject; }
            set
            {
                this._captionCopyObject = value;
                bbiCopyObject.Caption = value;
            }
        }

        private string _captionRangeSize = CommandBarDefaultSetting.CaptionButtonRangeSizeDefaultCommamd;
        /// <summary>
        /// Set Caption RangSize button on command bar manager
        /// </summary>
        public string SetCaptionRangSize
        {
            get { return this._captionRangeSize; }
            set
            {
                this._captionRangeSize = value;
                bbiRangSize.Caption = value;
            }
        }    
       
        #endregion

        #region "Set Image Button Menu"

        private Image _imagePrint = CommandBarDefaultSetting.ImageButtonPrint;
        /// <summary>
        /// Set Image Print button on command bar manager
        /// </summary>
        public Image SetImagePrint
        {
            get { return this._imagePrint; }
            set
            {
                this._imagePrint = value;
                bbiPrint.Glyph = value;
            }
        }


        private Image _imagePrintDown = CommandBarDefaultSetting.ImageButtonPrintDown;
        /// <summary>
        /// Set Image Print Down button on command bar manager
        /// </summary>
        public Image SetImagePrintDown
        {
            get { return this._imagePrintDown; }
            set
            {
                this._imagePrintDown = value;
                bbiPrintDown.Glyph = value;
            }
        }



        private Image _imageAdd = CommandBarDefaultSetting.ImageButtonAdd;
        /// <summary>
        /// Set Image  Add button on command bar manager
        /// </summary>
        public Image SetImageAdd
        {
            get { return this._imageAdd; }
            set
            {
                this._imageAdd = value;
                bbiAdd.LargeGlyph = value;
            }
        }

        private Image _imageEdit = CommandBarDefaultSetting.ImageButtonEdit;
        /// <summary>
        /// Set Image Edit button on command bar manager
        /// </summary>
        public Image SetImageEdit
        {
            get { return this._imageEdit; }
            set
            {
                this._imageEdit = value;
                bbiEdit.LargeGlyph = value;
            }
        }

        private Image _imageDelete = CommandBarDefaultSetting.ImageButtonDelete;
        /// <summary>
        /// Set Image Delete button on command bar manager
        /// </summary>
        public Image SetImageDelete
        {
            get { return this._imageDelete; }
            set
            {
                this._imageDelete = value;
                bbiDelete.LargeGlyph = value;
            }
        }

        private Image _imageSave = CommandBarDefaultSetting.ImageButtonSave;
        /// <summary>
        /// Set Image Save button on command bar manager
        /// </summary>
        public Image SetImageSave
        {
            get { return this._imageSave; }
            set
            {
                this._imageSave = value;
                bbiSave.LargeGlyph = value;
            }
        }

        private Image _imageCancel = CommandBarDefaultSetting.ImageButtonCancel;
        /// <summary>
        /// Set Caption Cancel button on command bar manager
        /// </summary>
        public Image SetImageCancel
        {
            get { return this._imageCancel; }
            set
            {
                this._imageCancel = value;
                bbiCancel.LargeGlyph = value;
            }
        }

        private Image _imageRefresh = CommandBarDefaultSetting.ImageButtonRefresh;
        /// <summary>
        /// Set Image Refresh button on command bar manager
        /// </summary>
        public Image SetImageRefresh
        {
            get { return this._imageRefresh; }
            set
            {
                this._imageRefresh = value;
                bbiRefresh.LargeGlyph = value;
            }
        }

        private Image _imageFind = CommandBarDefaultSetting.ImageButtonFind;
        /// <summary>
        /// Set CapImagetion Find button on command bar manager
        /// </summary>
        public Image SetImageFind
        {
            get { return this._imageFind; }
            set
            {
                this._imageFind = value;
                bbiFind.LargeGlyph = value;
            }
        }

        private Image _imageBreakDown = CommandBarDefaultSetting.ImageButtonBreakDown;
        /// <summary>
        /// Set Image BreakDown button on command bar manager
        /// </summary>
        public Image SetImageBreakDown
        {
            get { return this._imageBreakDown; }
            set
            {
                this._imageBreakDown = value;
                bbiBreakDown.LargeGlyph = value;
            }
        }
        private Image _imageCheck = CommandBarDefaultSetting.ImageButtonCheck;
        /// <summary>
        /// Set Image Check button on command bar manager
        /// </summary>
        public Image SetImageCheck
        {
            get { return this._imageCheck; }
            set
            {
                _imageCheck = value;
                bbiCheck.LargeGlyph = value;
            }
        }

        private Image _imageClose = CommandBarDefaultSetting.ImageButtonClose;
        /// <summary>
        /// Set Image Close button on command bar manager
        /// </summary>
        public Image SetImageClose
        {
            get { return this._imageClose; }
            set
            {
                this._imageClose = value;
                bbiClose.LargeGlyph = value;
            }
        }
        private Image _imageExpand = CommandBarDefaultSetting.ImageButtonExpand;
        /// <summary>
        ///  Set Image Expand button on command bar manager
        /// </summary>
        public Image SetImageExpand
        {
            get { return this._imageExpand; }
            set
            {
                this._imageExpand = value;
                bbiExpand.LargeGlyph = value;
            }
        }

        private Image _imageCollapse = CommandBarDefaultSetting.ImageButtonCollosepand;
        /// <summary>
        /// Set Image Collapse button on command bar manager
        /// </summary>
        public Image SetImageCollapse
        {
            get { return this._imageCollapse; }
            set
            {
                this._imageCollapse = value;
                bbiCollapse.LargeGlyph = value;
            }
        }

        private Image _imageRevision = CommandBarDefaultSetting.ImageButtonRevision;
        /// <summary>
        /// Set Image Revision button on command bar manager
        /// </summary>
        public Image SetImageRevision
        {
            get { return this._imageRevision; }
            set
            {
                this._imageRevision = value;
                bbiRevision.LargeGlyph = value;
            }
        }
        private Image _imageCombine = CommandBarDefaultSetting.ImageButtonCombine;
        /// <summary>
        /// Set Image Combine button on command bar manager
        /// </summary>
        public Image SetImageCombine
        {
            get { return this._imageCombine; }
            set
            {
                this._imageCombine = value;
                bbiCombine.LargeGlyph = value;
            }
        }
        private Image _imageGenerate = CommandBarDefaultSetting.ImageButtonGenerate;
        /// <summary>
        /// Set Image Generate button on command bar manager
        /// </summary>
        public Image SetImageGenerate
        {
            get { return this._imageGenerate; }
            set
            {
                this._imageGenerate = value;
                bbiGenerate.LargeGlyph = value;
            }
        }


        private Image _imageCopy = CommandBarDefaultSetting.ImageButtonCopy;
        /// <summary>
        /// Set Image Copy button on command bar manager
        /// </summary>
        public Image SetImageCopyObject
        {
            get { return this._imageCopy; }
            set
            {
                this._imageCopy = value;
                bbiCopyObject.LargeGlyph = value;
            }
        }

        private Image _imageRangeSize = CommandBarDefaultSetting.ImageButtonRangeSize;
        /// <summary>
        /// Set Image RangSize button on command bar manager
        /// </summary>
        public Image SetImageRangSize
        {
            get { return this._imageRangeSize; }
            set
            {
                this._imageRangeSize = value;
                bbiRangSize.LargeGlyph = value;
            }
        }    
       
        #endregion

        #region "Contructor"

        public FrmBase()
        {
            InitializeComponent();
            //
        }

        
        #endregion

       

        #region "Get Right On Form"
        public void AddRightWhenShowForm(string menuCode, bool roleInsert, bool roleUpdate, bool roleDelete, bool roleView,
          string advanceFunction, string specialFunction, bool checkAdvanceFunction , string formName)
        {
            this.MenuCode = menuCode;
            this.PerIns = roleInsert;
            this.PerUpd = roleUpdate;
            this.PerDel = roleDelete;
            this.PerViw = roleView;

            this.StrAdvanceFunction = advanceFunction;
            this.DtPerFunction = ProcessGeneral.GetAdvanceFunctionWhenOpenForm(advanceFunction);

            this.StrSpecialFunction = specialFunction;
            this.DtSpecialFunction = ProcessGeneral.GetSpecialFunctionWhenOpenForm(specialFunction);

            this.PerCheckAdvanceFunction = checkAdvanceFunction;

            this.MinStatus = ProcessGeneral.FindMinValueInStringNumber(advanceFunction, ',');
            this.MaxStatus = ProcessGeneral.FindMaxValueInStringNumber(advanceFunction, ',');
            this._menuName = formName;
            bbiSave.Enabled = false;
            bbiCancel.Enabled = false;
            if (!roleInsert)
            {
                bbiAdd.Enabled = false;
                AllowAdd = false;
               
            }
            else
            {
                bbiAdd.Enabled = true;
                AllowSave = true;
            }

            if (!roleUpdate)
            {
                bbiEdit.Enabled = false;
                bbiDelete.Enabled = false;
                AllowEdit = false;
                AllowDelete = false;
                
            }
            else
            {
                bbiEdit.Enabled = true;
                bbiDelete.Enabled = true;
                AllowEdit = true;
                AllowDelete = true;
            }

        }
        #endregion

        #region "Command Parse"

        public void CommandPass(string commandName)
        {
            switch (commandName)
            {
                case CommandBarDefaultSetting.CaptionButtonAddDefaultCommamd:
                    PerformAdd();
                    break;
                case CommandBarDefaultSetting.CaptionButtonEditDefaultCommamd:
                    PerformEdit();
                    break;
                case CommandBarDefaultSetting.CaptionButtonDeleteDefaultCommamd:
                    PerformDelete();
                    break;
                case CommandBarDefaultSetting.CaptionButtonSaveDefaultCommamd:
                    PerformSave();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCancelDefaultCommamd:
                    PerformCancel();
                    break;
                case CommandBarDefaultSetting.CaptionButtonRefreshDefaultCommamd:
                    PerformRefresh();
                    break;
                case CommandBarDefaultSetting.CaptionButtonFindDefaultCommamd:
                    PerformFind();
                    break;
                case CommandBarDefaultSetting.CaptionButtonPrintDefaultCommamd:
                    PerformPrint();
                    break;
                case CommandBarDefaultSetting.CaptionButtonExpandDefaultCommamd:
                    PerformExpand();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCollosepandDefaultCommamd:
                    PerformCollapse();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCloseDefaultCommamd:
                    PerformClose();
                    break;
                case CommandBarDefaultSetting.CaptionButtonRevisionDefaultCommamd:
                    PerformRevision();
                    break;
                case CommandBarDefaultSetting.CaptionButtonBreakDownDefaultCommamd:
                    PerformBreakDown();
                    break;
                case CommandBarDefaultSetting.CaptionButtonRangeSizeDefaultCommamd:
                    PerformRangeSize();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCopyDefaultCommamd:
                    PerformCopy();
                    break;
                case CommandBarDefaultSetting.CaptionButtonGenerateDefaultCommamd:
                    PerformGenerate();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCombineDefaultCommamd:
                    PerformCombine();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCheckDefaultCommamd:
                    PerformCheck();
                    break;

                #region "Function"

                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F1:
                    PerformGFN1_F1();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F2:
                    PerformGFN1_F2();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F3:
                    PerformGFN1_F3();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F4:
                    PerformGFN1_F4();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F5:
                    PerformGFN1_F5();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F6:
                    PerformGFN1_F6();
                    break;
                //----------------------------------------------------------------------------------------------------
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F1:
                    PerformGFN2_F1();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F2:
                    PerformGFN2_F2();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F3:
                    PerformGFN2_F3();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F4:
                    PerformGFN2_F4();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F5:
                    PerformGFN2_F5();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F6:
                    PerformGFN2_F6();
                    break;
                //----------------------------------------------------------------------------------------------------
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F1:
                    PerformGFN3_F1();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F2:
                    PerformGFN3_F2();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F3:
                    PerformGFN3_F3();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F4:
                    PerformGFN3_F4();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F5:
                    PerformGFN3_F5();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F6:
                    PerformGFN3_F6();
                    break;
                //----------------------------------------------------------------------------------------------------
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F1:
                    PerformGFN4_F1();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F2:
                    PerformGFN4_F2();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F3:
                    PerformGFN4_F3();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F4:
                    PerformGFN4_F4();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F5:
                    PerformGFN4_F5();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F6:
                    PerformGFN4_F6();
                    break;
                //----------------------------------------------------------------------------------------------------
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F1:
                    PerformGFN5_F1();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F2:
                    PerformGFN5_F2();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F3:
                    PerformGFN5_F3();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F4:
                    PerformGFN5_F4();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F5:
                    PerformGFN5_F5();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F6:
                    PerformGFN5_F6();
                    break;
                //----------------------------------------------------------------------------------------------------
                case CommandBarDefaultSetting.CaptionButtonbbiFN_1:
                    PerformFN_1();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiFN_2:
                    PerformFN_2();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiFN_3:
                    PerformFN_3();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiFN_4:
                    PerformFN_4();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiFN_5:
                    PerformFN_5();
                    break;
                    #endregion
            }
        }



       
        public void CommandPassRightCLick(string commandName)
        {
            switch (commandName)
            {
                case CommandBarDefaultSetting.CaptionButtonAddDefaultCommamd:
                    PerformAddRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonEditDefaultCommamd:
                    PerformEditRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonDeleteDefaultCommamd:
                    PerformDeleteRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonSaveDefaultCommamd:
                    PerformSaveRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCancelDefaultCommamd:
                    PerformCancelRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonRefreshDefaultCommamd:
                    PerformRefreshRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonFindDefaultCommamd:
                    PerformFindRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonPrintDefaultCommamd:
                    PerformPrintRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonExpandDefaultCommamd:
                    PerformExpandRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCollosepandDefaultCommamd:
                    PerformCollapseRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCloseDefaultCommamd:
                    PerformCloseRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonRevisionDefaultCommamd:
                    PerformRevisionRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonBreakDownDefaultCommamd:
                    PerformBreakDownRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonRangeSizeDefaultCommamd:
                    PerformRangeSizeRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCopyDefaultCommamd:
                    PerformCopyRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonGenerateDefaultCommamd:
                    PerformGenerateRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCombineDefaultCommamd:
                    PerformCombineRc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonCheckDefaultCommamd:
                    PerformCheckRc();
                    break;
                #region "Function"

                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F1:
                    PerformGFN1_F1Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F2:
                    PerformGFN1_F2Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F3:
                    PerformGFN1_F3Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F4:
                    PerformGFN1_F4Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F5:
                    PerformGFN1_F5Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN1_F6:
                    PerformGFN1_F6Rc();
                    break;
                //----------------------------------------------------------------------------------------------------
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F1:
                    PerformGFN2_F1Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F2:
                    PerformGFN2_F2Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F3:
                    PerformGFN2_F3Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F4:
                    PerformGFN2_F4Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F5:
                    PerformGFN2_F5Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN2_F6:
                    PerformGFN2_F6Rc();
                    break;
                //----------------------------------------------------------------------------------------------------
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F1:
                    PerformGFN3_F1Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F2:
                    PerformGFN3_F2Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F3:
                    PerformGFN3_F3Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F4:
                    PerformGFN3_F4Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F5:
                    PerformGFN3_F5Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN3_F6:
                    PerformGFN3_F6Rc();
                    break;
                //----------------------------------------------------------------------------------------------------
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F1:
                    PerformGFN4_F1Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F2:
                    PerformGFN4_F2Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F3:
                    PerformGFN4_F3Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F4:
                    PerformGFN4_F4Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F5:
                    PerformGFN4_F5Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN4_F6:
                    PerformGFN4_F6Rc();
                    break;
                //----------------------------------------------------------------------------------------------------
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F1:
                    PerformGFN5_F1Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F2:
                    PerformGFN5_F2Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F3:
                    PerformGFN5_F3Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F4:
                    PerformGFN5_F4Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F5:
                    PerformGFN5_F5Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiGFN5_F6:
                    PerformGFN5_F6Rc();
                    break;
                //----------------------------------------------------------------------------------------------------
                case CommandBarDefaultSetting.CaptionButtonbbiFN_1:
                    PerformFN_1Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiFN_2:
                    PerformFN_2Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiFN_3:
                    PerformFN_3Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiFN_4:
                    PerformFN_4Rc();
                    break;
                case CommandBarDefaultSetting.CaptionButtonbbiFN_5:
                    PerformFN_5Rc();
                    break;
                    #endregion
            }
        }



        #region "Button Function Click"


        protected virtual void PerformGFN1_F1()
        {

        }
        protected virtual void PerformGFN1_F2()
        {

        }
        protected virtual void PerformGFN1_F3()
        {

        }
        protected virtual void PerformGFN1_F4()
        {

        }
        protected virtual void PerformGFN1_F5()
        {

        }
        protected virtual void PerformGFN1_F6()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------
        protected virtual void PerformGFN2_F1()
        {

        }
        protected virtual void PerformGFN2_F2()
        {

        }
        protected virtual void PerformGFN2_F3()
        {

        }
        protected virtual void PerformGFN2_F4()
        {

        }
        protected virtual void PerformGFN2_F5()
        {

        }
        protected virtual void PerformGFN2_F6()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------
        protected virtual void PerformGFN3_F1()
        {

        }
        protected virtual void PerformGFN3_F2()
        {

        }
        protected virtual void PerformGFN3_F3()
        {

        }
        protected virtual void PerformGFN3_F4()
        {

        }
        protected virtual void PerformGFN3_F5()
        {

        }
        protected virtual void PerformGFN3_F6()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------
        protected virtual void PerformGFN4_F1()
        {

        }
        protected virtual void PerformGFN4_F2()
        {

        }
        protected virtual void PerformGFN4_F3()
        {

        }
        protected virtual void PerformGFN4_F4()
        {

        }
        protected virtual void PerformGFN4_F5()
        {

        }
        protected virtual void PerformGFN4_F6()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------
        protected virtual void PerformGFN5_F1()
        {

        }
        protected virtual void PerformGFN5_F2()
        {

        }
        protected virtual void PerformGFN5_F3()
        {

        }
        protected virtual void PerformGFN5_F4()
        {

        }
        protected virtual void PerformGFN5_F5()
        {

        }
        protected virtual void PerformGFN5_F6()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------
        protected virtual void PerformFN_1()
        {

        }
        protected virtual void PerformFN_2()
        {

        }
        protected virtual void PerformFN_3()
        {

        }
        protected virtual void PerformFN_4()
        {

        }
        protected virtual void PerformFN_5()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------




        protected virtual void PerformGFN1_F1Rc()
        {

        }
        protected virtual void PerformGFN1_F2Rc()
        {

        }
        protected virtual void PerformGFN1_F3Rc()
        {

        }
        protected virtual void PerformGFN1_F4Rc()
        {

        }
        protected virtual void PerformGFN1_F5Rc()
        {

        }
        protected virtual void PerformGFN1_F6Rc()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------
        protected virtual void PerformGFN2_F1Rc()
        {

        }
        protected virtual void PerformGFN2_F2Rc()
        {

        }
        protected virtual void PerformGFN2_F3Rc()
        {

        }
        protected virtual void PerformGFN2_F4Rc()
        {

        }
        protected virtual void PerformGFN2_F5Rc()
        {

        }
        protected virtual void PerformGFN2_F6Rc()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------
        protected virtual void PerformGFN3_F1Rc()
        {

        }
        protected virtual void PerformGFN3_F2Rc()
        {

        }
        protected virtual void PerformGFN3_F3Rc()
        {

        }
        protected virtual void PerformGFN3_F4Rc()
        {

        }
        protected virtual void PerformGFN3_F5Rc()
        {

        }
        protected virtual void PerformGFN3_F6Rc()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------
        protected virtual void PerformGFN4_F1Rc()
        {

        }
        protected virtual void PerformGFN4_F2Rc()
        {

        }
        protected virtual void PerformGFN4_F3Rc()
        {

        }
        protected virtual void PerformGFN4_F4Rc()
        {

        }
        protected virtual void PerformGFN4_F5Rc()
        {

        }
        protected virtual void PerformGFN4_F6Rc()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------
        protected virtual void PerformGFN5_F1Rc()
        {

        }
        protected virtual void PerformGFN5_F2Rc()
        {

        }
        protected virtual void PerformGFN5_F3Rc()
        {

        }
        protected virtual void PerformGFN5_F4Rc()
        {

        }
        protected virtual void PerformGFN5_F5Rc()
        {

        }
        protected virtual void PerformGFN5_F6Rc()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------
        protected virtual void PerformFN_1Rc()
        {

        }
        protected virtual void PerformFN_2Rc()
        {

        }
        protected virtual void PerformFN_3Rc()
        {

        }
        protected virtual void PerformFN_4Rc()
        {

        }
        protected virtual void PerformFN_5Rc()
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------------

        #endregion



        /// <summary>
        /// Perform when right click add button
        /// </summary>
        protected virtual void PerformCloseRc()
        {
            this.Close();
        }
        /// <summary>
        /// Perform when right click add button
        /// </summary>
        protected virtual void PerformAddRc()
        {

        }

        /// <summary>
        /// Perform when right click edit button
        /// </summary>
        protected virtual void PerformEditRc()
        {

        }

        /// <summary>
        /// Perform when right click delete button
        /// </summary>
        protected virtual void PerformDeleteRc() { }

        /// <summary>
        /// Perform when right click save button
        /// </summary>
        protected virtual void PerformSaveRc() { }

        /// <summary>
        /// Perform when right click cancel button
        /// </summary>
        protected virtual void PerformCancelRc()
        {

        }

        /// <summary>
        /// Load data or perform when right click refresh button
        /// </summary>
        protected virtual void PerformRefreshRc() { }

        /// <summary>
        /// Perform when right click find button
        /// </summary>
        protected virtual void PerformFindRc() { }

        /// <summary>
        /// Perform when right click print button
        /// </summary>
        protected virtual void PerformPrintRc() { }

        /// <summary>
        /// Perform when right click break down
        /// </summary>
        protected virtual void PerformBreakDownRc() { }

        /// <summary>
        /// Perform when right click break down
        /// </summary>
        protected virtual void PerformRevisionRc() { }

        /// <summary>
        /// Perform when right click collapse button
        /// </summary>
        protected virtual void PerformCollapseRc() { }



        /// <summary>
        /// Perform when right click expand button
        /// </summary>
        protected virtual void PerformExpandRc() { }

        /// <summary>
        /// Perform when right click range size button
        /// </summary>
        protected virtual void PerformRangeSizeRc() { }

        /// <summary>
        /// Perform when right click Copy button
        /// </summary>
        protected virtual void PerformCopyRc() { }

        /// <summary>
        /// Perform when right click Generate button
        /// </summary>
        protected virtual void PerformGenerateRc() { }

        /// <summary>
        /// Perform when right click Combine button
        /// </summary>
        protected virtual void PerformCombineRc() { }

        /// <summary>
        /// Perform when right click Check button
        /// </summary>
        protected virtual void PerformCheckRc() { }











        /// <summary>
        /// Perform when click add button
        /// </summary>
        protected virtual void PerformClose()
        {
            this.Close();
        }
        /// <summary>
        /// Perform when click add button
        /// </summary>
        protected virtual void PerformAdd()
        {
          
        }

        /// <summary>
        /// Perform when click edit button
        /// </summary>
        protected virtual void PerformEdit()
        {
          
        }

        /// <summary>
        /// Perform when click delete button
        /// </summary>
        protected virtual void PerformDelete() { }

        /// <summary>
        /// Perform when click save button
        /// </summary>
        protected virtual void PerformSave() { }

        /// <summary>
        /// Perform when click cancel button
        /// </summary>
        protected virtual void PerformCancel()
        {
         
        }

        /// <summary>
        /// Load data or perform when click refresh button
        /// </summary>
        protected virtual void PerformRefresh() { }

        /// <summary>
        /// Perform when click find button
        /// </summary>
        protected virtual void PerformFind() { }

        /// <summary>
        /// Perform when click print button
        /// </summary>
        protected virtual void PerformPrint() { }

        /// <summary>
        /// Perform when click break down
        /// </summary>
        protected virtual void PerformBreakDown() { }

        /// <summary>
        /// Perform when click break down
        /// </summary>
        protected virtual void PerformRevision() { }

        /// <summary>
        /// Perform when click collapse button
        /// </summary>
        protected virtual void PerformCollapse() { }



        /// <summary>
        /// Perform when click expand button
        /// </summary>
        protected virtual void PerformExpand() { }

        /// <summary>
        /// Perform when click range size button
        /// </summary>
        protected virtual void PerformRangeSize() { }

        /// <summary>
        /// Perform when click Copy button
        /// </summary>
        protected virtual void PerformCopy() { }

        /// <summary>
        /// Perform when click Generate button
        /// </summary>
        protected virtual void PerformGenerate() { }

        /// <summary>
        /// Perform when click Combine button
        /// </summary>
        protected virtual void PerformCombine() { }

        /// <summary>
        /// Perform when click Check button
        /// </summary>
        protected virtual void PerformCheck() { }

        #endregion


        #region "Tooltip"
        private string _hintPrint = CommandBarDefaultSetting.HintButtonPrintDefaultCommamd;
        public string HintPrint
        {
            get { return _hintPrint; }
            set
            {
                bbiPrint.Hint = value;
                _hintPrint = value;
            }
        }
        private string _hintPrintDown = CommandBarDefaultSetting.HintButtonPrintDownDefaultCommamd;
        public string HintPrintDown
        {
            get { return _hintPrintDown; }
            set
            {
                bbiPrintDown.Hint = value;
                _hintPrintDown = value;
            }
        }
        private string _hintAdd = CommandBarDefaultSetting.HintButtonAddDefaultCommamd;
        public string HintAdd
        {
            get { return _hintAdd; }
            set
            {
                bbiAdd.Hint = value;
                _hintAdd = value;
            }
        }
        private string _hintEdit = CommandBarDefaultSetting.HintButtonEditDefaultCommamd;
        public string HintEdit
        {
            get { return _hintEdit; }
            set
            {
                bbiEdit.Hint = value;
                _hintEdit = value;
            }
        }

     
        private string _hintDelete = CommandBarDefaultSetting.HintButtonDeleteDefaultCommamd;
        public string HintDelete
        {
            get { return _hintDelete; }
            set
            {
                bbiDelete.Hint = value;
                _hintDelete = value;
            }
        }

       
        private string _hintSave = CommandBarDefaultSetting.HintButtonSaveDefaultCommamd;
        public string HintSave
        {
            get { return _hintSave; }
            set
            {
                bbiSave.Hint = value;
                _hintSave = value;
            }
        }

     
        private string _hintCancel = CommandBarDefaultSetting.HintButtonCancelDefaultCommamd;
        public string HintCancel
        {
            get { return _hintCancel; }
            set
            {
                bbiCancel.Hint = value;
                _hintCancel = value;
            }
        }

        private string _hintRefresh = CommandBarDefaultSetting.HintButtonRefreshDefaultCommamd;
        public string HintRefresh
        {
            get { return _hintRefresh; }
            set
            {
                bbiRefresh.Hint = value;
                _hintRefresh = value;
            }
        }

      
        private string _hintFind = CommandBarDefaultSetting.HintButtonFindDefaultCommamd;
        public string HintFind
        {
            get { return _hintFind; }
            set
            {
                bbiFind.Hint = value;
                _hintFind = value;
            }
        }

        private string _hintBreakDown = CommandBarDefaultSetting.HintButtonBreakDownDefaultCommamd;
        public string HintBreakDown
        {
            get { return _hintBreakDown; }
            set
            {
                bbiBreakDown.Hint = value;
                _hintBreakDown = value;
            }
        }

     
        private string _hintCheck = CommandBarDefaultSetting.HintButtonCheckDefaultCommamd;
        public string HintCheck
        {
            get { return _hintCheck; }
            set
            {
                bbiCheck.Hint = value;
                _hintCheck = value;
            }
        }


     
        private string _hintClose = CommandBarDefaultSetting.HintButtonCloseDefaultCommamd;
        public string HintClose
        {
            get { return _hintClose; }
            set
            {
                bbiClose.Hint = value;
                _hintClose = value;
            }
        }

     
        private string _hintExpand = CommandBarDefaultSetting.HintButtonExpandDefaultCommamd;
        public string HintExpand
        {
            get { return _hintExpand; }
            set
            {
                bbiExpand.Hint = value;
                _hintExpand = value;
            }
        }


        private string _hintCollapse = CommandBarDefaultSetting.HintButtonCollosepandDefaultCommamd;
        public string HintCollapse
        {
            get { return _hintCollapse; }
            set
            {
                bbiCollapse.Hint = value;
                _hintCollapse = value;
            }
        }

      
        private string _hintRevision = CommandBarDefaultSetting.HintButtonRevisionDefaultCommamd;
        public string HintRevision
        {
            get { return _hintRevision; }
            set
            {
                bbiRevision.Hint = value;
                _hintRevision = value;
            }
        }

        private string _hintCombine = CommandBarDefaultSetting.HintButtonCombineDefaultCommamd;
        public string HintCombine
        {
            get { return _hintCombine; }
            set
            {
                bbiCombine.Hint = value;
                _hintCombine = value;
            }
        }

      
        private string _hintGenerate = CommandBarDefaultSetting.HintButtonGenerateDefaultCommamd;
        public string HintGenerate
        {
            get { return _hintGenerate; }
            set
            {
                bbiGenerate.Hint = value;
                _hintGenerate = value;
            }
        }


        private string _hintCopyObject = CommandBarDefaultSetting.HintButtonCopyDefaultCommamd;
        public string HintCopyObject
        {
            get { return _hintCopyObject; }
            set
            {
                bbiCopyObject.Hint = value;
                _hintCopyObject = value;
            }
        }
        private string _hintRangeSize = CommandBarDefaultSetting.HintButtonRangeSizeDefaultCommamd;
        public string HintRangSize
        {
            get { return _hintRangeSize; }
            set
            {
                bbiRangSize.Hint = value;
                _hintRangeSize = value;
            }
        }

        #region "Hint Function"
        private string _hintGFN1 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN1
        {
            get { return _hintGFN1; }
            set
            {
                _hintGFN1 = value;
                bbiGFN1.Hint = value;
            }
        }

        private string _hintGFN1_F1 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN1_F1
        {
            get { return _hintGFN1_F1; }
            set
            {
                _hintGFN1_F1 = value;
                bbiGFN1_F1.Hint = value;
            }
        }

        private string _hintGFN1_F2 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN1_F2
        {
            get { return _hintGFN1_F2; }
            set
            {
                _hintGFN1_F2 = value;
                bbiGFN1_F2.Hint = value;
            }
        }

        private string _hintGFN1_F3 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN1_F3
        {
            get { return _hintGFN1_F3; }
            set
            {
                _hintGFN1_F3 = value;
                bbiGFN1_F3.Hint = value;
            }
        }


        private string _hintGFN1_F4 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN1_F4
        {
            get { return _hintGFN1_F4; }
            set
            {
                _hintGFN1_F4 = value;
                bbiGFN1_F4.Hint = value;
            }
        }

        private string _hintGFN1_F5 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN1_F5
        {
            get { return _hintGFN1_F5; }
            set
            {
                _hintGFN1_F5 = value;
                bbiGFN1_F5.Hint = value;
            }
        }

        private string _hintGFN1_F6 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN1_F6
        {
            get { return _hintGFN1_F6; }
            set
            {
                _hintGFN1_F6 = value;
                bbiGFN1_F6.Hint = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private string _hintGFN2 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN2
        {
            get { return _hintGFN2; }
            set
            {
                _hintGFN2 = value;
                bbiGFN2.Hint = value;
            }
        }

        private string _hintGFN2_F1 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN2_F1
        {
            get { return _hintGFN2_F1; }
            set
            {
                _hintGFN2_F1 = value;
                bbiGFN2_F1.Hint = value;
            }
        }

        private string _hintGFN2_F2 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN2_F2
        {
            get { return _hintGFN2_F2; }
            set
            {
                _hintGFN2_F2 = value;
                bbiGFN2_F2.Hint = value;
            }
        }

        private string _hintGFN2_F3 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN2_F3
        {
            get { return _hintGFN2_F3; }
            set
            {
                _hintGFN2_F3 = value;
                bbiGFN2_F3.Hint = value;
            }
        }


        private string _hintGFN2_F4 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN2_F4
        {
            get { return _hintGFN2_F4; }
            set
            {
                _hintGFN2_F4 = value;
                bbiGFN2_F4.Hint = value;
            }
        }

        private string _hintGFN2_F5 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN2_F5
        {
            get { return _hintGFN2_F5; }
            set
            {
                _hintGFN2_F5 = value;
                bbiGFN2_F5.Hint = value;
            }
        }

        private string _hintGFN2_F6 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN2_F6
        {
            get { return _hintGFN2_F6; }
            set
            {
                _hintGFN2_F6 = value;
                bbiGFN2_F6.Hint = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private string _hintGFN3 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN3
        {
            get { return _hintGFN3; }
            set
            {
                _hintGFN3 = value;
                bbiGFN3.Hint = value;
            }
        }

        private string _hintGFN3_F1 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN3_F1
        {
            get { return _hintGFN3_F1; }
            set
            {
                _hintGFN3_F1 = value;
                bbiGFN3_F1.Hint = value;
            }
        }

        private string _hintGFN3_F2 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN3_F2
        {
            get { return _hintGFN3_F2; }
            set
            {
                _hintGFN3_F2 = value;
                bbiGFN3_F2.Hint = value;
            }
        }

        private string _hintGFN3_F3 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN3_F3
        {
            get { return _hintGFN3_F3; }
            set
            {
                _hintGFN3_F3 = value;
                bbiGFN3_F3.Hint = value;
            }
        }


        private string _hintGFN3_F4 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN3_F4
        {
            get { return _hintGFN3_F4; }
            set
            {
                _hintGFN3_F4 = value;
                bbiGFN3_F4.Hint = value;
            }
        }

        private string _hintGFN3_F5 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN3_F5
        {
            get { return _hintGFN3_F5; }
            set
            {
                _hintGFN3_F5 = value;
                bbiGFN3_F5.Hint = value;
            }
        }

        private string _hintGFN3_F6 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN3_F6
        {
            get { return _hintGFN3_F6; }
            set
            {
                _hintGFN3_F6 = value;
                bbiGFN3_F6.Hint = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private string _hintGFN4 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN4
        {
            get { return _hintGFN4; }
            set
            {
                _hintGFN4 = value;
                bbiGFN4.Hint = value;
            }
        }

        private string _hintGFN4_F1 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN4_F1
        {
            get { return _hintGFN4_F1; }
            set
            {
                _hintGFN4_F1 = value;
                bbiGFN4_F1.Hint = value;
            }
        }

        private string _hintGFN4_F2 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN4_F2
        {
            get { return _hintGFN4_F2; }
            set
            {
                _hintGFN4_F2 = value;
                bbiGFN4_F2.Hint = value;
            }
        }

        private string _hintGFN4_F3 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN4_F3
        {
            get { return _hintGFN4_F3; }
            set
            {
                _hintGFN4_F3 = value;
                bbiGFN4_F3.Hint = value;
            }
        }


        private string _hintGFN4_F4 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN4_F4
        {
            get { return _hintGFN4_F4; }
            set
            {
                _hintGFN4_F4 = value;
                bbiGFN4_F4.Hint = value;
            }
        }

        private string _hintGFN4_F5 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN4_F5
        {
            get { return _hintGFN4_F5; }
            set
            {
                _hintGFN4_F5 = value;
                bbiGFN4_F5.Hint = value;
            }
        }

        private string _hintGFN4_F6 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN4_F6
        {
            get { return _hintGFN4_F6; }
            set
            {
                _hintGFN4_F6 = value;
                bbiGFN4_F6.Hint = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
        private string _hintGFN5 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN5
        {
            get { return _hintGFN5; }
            set
            {
                _hintGFN5 = value;
                bbiGFN5.Hint = value;
            }
        }

        private string _hintGFN5_F1 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN5_F1
        {
            get { return _hintGFN5_F1; }
            set
            {
                _hintGFN5_F1 = value;
                bbiGFN5_F1.Hint = value;
            }
        }

        private string _hintGFN5_F2 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN5_F2
        {
            get { return _hintGFN5_F2; }
            set
            {
                _hintGFN5_F2 = value;
                bbiGFN5_F2.Hint = value;
            }
        }

        private string _hintGFN5_F3 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN5_F3
        {
            get { return _hintGFN5_F3; }
            set
            {
                _hintGFN5_F3 = value;
                bbiGFN5_F3.Hint = value;
            }
        }


        private string _hintGFN5_F4 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN5_F4
        {
            get { return _hintGFN5_F4; }
            set
            {
                _hintGFN5_F4 = value;
                bbiGFN5_F4.Hint = value;
            }
        }

        private string _hintGFN5_F5 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN5_F5
        {
            get { return _hintGFN5_F5; }
            set
            {
                _hintGFN5_F5 = value;
                bbiGFN5_F5.Hint = value;
            }
        }

        private string _hintGFN5_F6 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintGFN5_F6
        {
            get { return _hintGFN5_F6; }
            set
            {
                _hintGFN5_F6 = value;
                bbiGFN5_F6.Hint = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------

        private string _hintFN1 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintFN1
        {
            get { return _hintFN1; }
            set
            {
                _hintFN1 = value;
                bbiFN_1.Hint = value;
            }
        }





        private string _hintFN2 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintFN2
        {
            get { return _hintFN2; }
            set
            {
                _hintFN2 = value;
                bbiFN_2.Hint = value;
            }
        }

        private string _hintFN3 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintFN3
        {
            get { return _hintFN3; }
            set
            {
                _hintFN3 = value;
                bbiFN_3.Hint = value;
            }
        }


        private string _hintFN4 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintFN4
        {
            get { return _hintFN4; }
            set
            {
                _hintFN4 = value;
                bbiFN_4.Hint = value;
            }
        }
        private string _hintFN5 = CommandBarDefaultSetting.HintButtonFunctionDefaultCommamd;
        public string HintFN5
        {
            get { return _hintFN5; }
            set
            {
                _hintFN5 = value;
                bbiFN_5.Hint = value;
            }
        }
        #endregion
        #endregion


        #region "Re-Set Main Menu On Main Form When Change Tab"

        public void ReDisplayMainMenu()
        {
            #region "Set Image"
            
            bbiAdd.LargeGlyph = this._imageAdd;
            bbiEdit.LargeGlyph = this._imageEdit;
            bbiDelete.LargeGlyph = this._imageDelete;
            bbiSave.LargeGlyph = this._imageSave;
            bbiCancel.LargeGlyph = this._imageCancel;
            bbiRefresh.LargeGlyph = this._imageRefresh;
            bbiFind.LargeGlyph = this._imageFind;
            bbiPrint.LargeGlyph = this._imagePrint;
            bbiPrintDown.LargeGlyph = this._imagePrintDown;
            bbiExpand.LargeGlyph = this._imageExpand;
            bbiCollapse.LargeGlyph = this._imageCollapse;
            bbiClose.LargeGlyph = this._imageClose;
            bbiRevision.LargeGlyph = this._imageRevision;
            bbiBreakDown.LargeGlyph = this._imageBreakDown;
            bbiRangSize.LargeGlyph = this._imageRangeSize;
            bbiCopyObject.LargeGlyph = this._imageCopy;
            bbiGenerate.LargeGlyph = this._imageGenerate;
            bbiCombine.LargeGlyph = this._imageCombine;
            bbiCheck.LargeGlyph = this._imageCheck;

            #region "Function"
            bbiGFN1.ImageOptions.LargeImage = this._imageGFN1;
            bbiGFN1_F1.Glyph = this._imageGFN1_F1;
            bbiGFN1_F2.Glyph = this._imageGFN1_F2;
            bbiGFN1_F3.Glyph = this._imageGFN1_F3;
            bbiGFN1_F4.Glyph = this._imageGFN1_F4;
            bbiGFN1_F5.Glyph = this._imageGFN1_F5;
            bbiGFN1_F6.Glyph = this._imageGFN1_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN2.ImageOptions.LargeImage = this._imageGFN2;
            bbiGFN2_F1.Glyph = this._imageGFN2_F1;
            bbiGFN2_F2.Glyph = this._imageGFN2_F2;
            bbiGFN2_F3.Glyph = this._imageGFN2_F3;
            bbiGFN2_F4.Glyph = this._imageGFN2_F4;
            bbiGFN2_F5.Glyph = this._imageGFN2_F5;
            bbiGFN2_F6.Glyph = this._imageGFN2_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN3.ImageOptions.LargeImage = this._imageGFN3;
            bbiGFN3_F1.Glyph = this._imageGFN3_F1;
            bbiGFN3_F2.Glyph = this._imageGFN3_F2;
            bbiGFN3_F3.Glyph = this._imageGFN3_F3;
            bbiGFN3_F4.Glyph = this._imageGFN3_F4;
            bbiGFN3_F5.Glyph = this._imageGFN3_F5;
            bbiGFN3_F6.Glyph = this._imageGFN3_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN4.ImageOptions.LargeImage = this._imageGFN4;
            bbiGFN4_F1.Glyph = this._imageGFN4_F1;
            bbiGFN4_F2.Glyph = this._imageGFN4_F2;
            bbiGFN4_F3.Glyph = this._imageGFN4_F3;
            bbiGFN4_F4.Glyph = this._imageGFN4_F4;
            bbiGFN4_F5.Glyph = this._imageGFN4_F5;
            bbiGFN4_F6.Glyph = this._imageGFN4_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN5.ImageOptions.LargeImage = this._imageGFN5;
            bbiGFN5_F1.Glyph = this._imageGFN5_F1;
            bbiGFN5_F2.Glyph = this._imageGFN5_F2;
            bbiGFN5_F3.Glyph = this._imageGFN5_F3;
            bbiGFN5_F4.Glyph = this._imageGFN5_F4;
            bbiGFN5_F5.Glyph = this._imageGFN5_F5;
            bbiGFN5_F6.Glyph = this._imageGFN5_F6;
            //-----------------------------------------------------------------------------------------
            bbiFN_1.ImageOptions.LargeImage = this._imageFN1;
            bbiFN_2.ImageOptions.LargeImage = this._imageFN2;
            bbiFN_3.ImageOptions.LargeImage = this._imageFN3;
            bbiFN_4.ImageOptions.LargeImage = this._imageFN4;
            bbiFN_5.ImageOptions.LargeImage = this._imageFN5;
            #endregion

            #endregion


            #region "Set Caption"
            bbiAdd.Caption = this._captionAdd;
            bbiEdit.Caption = this._captionEdit;
            bbiDelete.Caption = this._captionDelete;
            bbiSave.Caption = this._captionSave;
            bbiCancel.Caption = this._captionCancel;
            bbiRefresh.Caption = this._captionRefresh;
            bbiFind.Caption = this._captionFind;
            bbiPrint.Caption = this._captionPrint;
            bbiPrintDown.Caption = this._captionPrintDown;
            bbiExpand.Caption = this._captionExpand;
            bbiCollapse.Caption = this._captionCollapse;
            bbiClose.Caption = this._captionClose;
            bbiRevision.Caption = this._captionRevision;
            bbiBreakDown.Caption = this._captionBreakDown;
            bbiRangSize.Caption = this._captionRangeSize;
            bbiCopyObject.Caption = this._captionCopyObject;
            bbiGenerate.Caption = this._captionGenerate;
            bbiCombine.Caption = this._captionCombine;
            bbiCheck.Caption = this._captionCheck;
            #region "Function"
            bbiGFN1.Caption = this._captionGFN1;
            bbiGFN1_F1.Caption = this._captionGFN1_F1;
            bbiGFN1_F2.Caption = this._captionGFN1_F2;
            bbiGFN1_F3.Caption = this._captionGFN1_F3;
            bbiGFN1_F4.Caption = this._captionGFN1_F4;
            bbiGFN1_F5.Caption = this._captionGFN1_F5;
            bbiGFN1_F6.Caption = this._captionGFN1_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN2.Caption = this._captionGFN2;
            bbiGFN2_F1.Caption = this._captionGFN2_F1;
            bbiGFN2_F2.Caption = this._captionGFN2_F2;
            bbiGFN2_F3.Caption = this._captionGFN2_F3;
            bbiGFN2_F4.Caption = this._captionGFN2_F4;
            bbiGFN2_F5.Caption = this._captionGFN2_F5;
            bbiGFN2_F6.Caption = this._captionGFN2_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN3.Caption = this._captionGFN3;
            bbiGFN3_F1.Caption = this._captionGFN3_F1;
            bbiGFN3_F2.Caption = this._captionGFN3_F2;
            bbiGFN3_F3.Caption = this._captionGFN3_F3;
            bbiGFN3_F4.Caption = this._captionGFN3_F4;
            bbiGFN3_F5.Caption = this._captionGFN3_F5;
            bbiGFN3_F6.Caption = this._captionGFN3_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN4.Caption = this._captionGFN4;
            bbiGFN4_F1.Caption = this._captionGFN4_F1;
            bbiGFN4_F2.Caption = this._captionGFN4_F2;
            bbiGFN4_F3.Caption = this._captionGFN4_F3;
            bbiGFN4_F4.Caption = this._captionGFN4_F4;
            bbiGFN4_F5.Caption = this._captionGFN4_F5;
            bbiGFN4_F6.Caption = this._captionGFN4_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN5.Caption = this._captionGFN5;
            bbiGFN5_F1.Caption = this._captionGFN5_F1;
            bbiGFN5_F2.Caption = this._captionGFN5_F2;
            bbiGFN5_F3.Caption = this._captionGFN5_F3;
            bbiGFN5_F4.Caption = this._captionGFN5_F4;
            bbiGFN5_F5.Caption = this._captionGFN5_F5;
            bbiGFN5_F6.Caption = this._captionGFN5_F6;
            //-----------------------------------------------------------------------------------------
            bbiFN_1.Caption = this._captionFN1;
            bbiFN_2.Caption = this._captionFN2;
            bbiFN_3.Caption = this._captionFN3;
            bbiFN_4.Caption = this._captionFN4;
            bbiFN_5.Caption = this._captionFN5;
            #endregion
            #endregion


            #region "Set Enable"
            bbiAdd.Enabled = this._enableAdd;
            bbiEdit.Enabled = this._enableEdit;
            bbiDelete.Enabled = this._enableDelete;
            bbiSave.Enabled = this._enableSave;
            bbiCancel.Enabled = this._enableCancel;
            bbiRefresh.Enabled = this._enableRefresh;
            bbiFind.Enabled = this._enableFind;
            bbiPrint.Enabled = this._enablePrint;
            bbiPrintDown.Enabled = this._enablePrintDown;
            bbiExpand.Enabled = this._enableExpand;
            bbiCollapse.Enabled = this._enableCollapse;
            bbiClose.Enabled = this._enableClose;
            bbiRevision.Enabled = this._enableRevision;
            bbiBreakDown.Enabled = this._enableBreakDown;
            bbiRangSize.Enabled = this._enableRangeSize;
            bbiCopyObject.Enabled = this._enableCopyObject;
            bbiGenerate.Enabled = this._enableGenerate;
            bbiCombine.Enabled = this._enableCombine;
            bbiCheck.Enabled = this._enableCheck;


            #region "Function"
            bbiGFN1.Enabled = this._enableGFN1;
            bbiGFN1_F1.Enabled = this._enableGFN1_F1;
            bbiGFN1_F2.Enabled = this._enableGFN1_F2;
            bbiGFN1_F3.Enabled = this._enableGFN1_F3;
            bbiGFN1_F4.Enabled = this._enableGFN1_F4;
            bbiGFN1_F5.Enabled = this._enableGFN1_F5;
            bbiGFN1_F6.Enabled = this._enableGFN1_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN2.Enabled = this._enableGFN2;
            bbiGFN2_F1.Enabled = this._enableGFN2_F1;
            bbiGFN2_F2.Enabled = this._enableGFN2_F2;
            bbiGFN2_F3.Enabled = this._enableGFN2_F3;
            bbiGFN2_F4.Enabled = this._enableGFN2_F4;
            bbiGFN2_F5.Enabled = this._enableGFN2_F5;
            bbiGFN2_F6.Enabled = this._enableGFN2_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN3.Enabled = this._enableGFN3;
            bbiGFN3_F1.Enabled = this._enableGFN3_F1;
            bbiGFN3_F2.Enabled = this._enableGFN3_F2;
            bbiGFN3_F3.Enabled = this._enableGFN3_F3;
            bbiGFN3_F4.Enabled = this._enableGFN3_F4;
            bbiGFN3_F5.Enabled = this._enableGFN3_F5;
            bbiGFN3_F6.Enabled = this._enableGFN3_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN4.Enabled = this._enableGFN4;
            bbiGFN4_F1.Enabled = this._enableGFN4_F1;
            bbiGFN4_F2.Enabled = this._enableGFN4_F2;
            bbiGFN4_F3.Enabled = this._enableGFN4_F3;
            bbiGFN4_F4.Enabled = this._enableGFN4_F4;
            bbiGFN4_F5.Enabled = this._enableGFN4_F5;
            bbiGFN4_F6.Enabled = this._enableGFN4_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN5.Enabled = this._enableGFN5;
            bbiGFN5_F1.Enabled = this._enableGFN5_F1;
            bbiGFN5_F2.Enabled = this._enableGFN5_F2;
            bbiGFN5_F3.Enabled = this._enableGFN5_F3;
            bbiGFN5_F4.Enabled = this._enableGFN5_F4;
            bbiGFN5_F5.Enabled = this._enableGFN5_F5;
            bbiGFN5_F6.Enabled = this._enableGFN5_F6;
            //-----------------------------------------------------------------------------------------
            bbiFN_1.Enabled = this._enableFN1;
            bbiFN_2.Enabled = this._enableFN2;
            bbiFN_3.Enabled = this._enableFN3;
            bbiFN_4.Enabled = this._enableFN4;
            bbiFN_5.Enabled = this._enableFN5;
            #endregion
            #endregion


            #region "Set Enable"
            bbiAdd.Visibility = !this._allowAdd ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiEdit.Visibility = !this._allowEdit ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiDelete.Visibility = !this._allowDelete ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiSave.Visibility = !this._allowSave ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiCancel.Visibility = !this._allowCancel ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiRefresh.Visibility = !this._allowRefresh ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiFind.Visibility = !this._allowFind ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiPrint.Visibility = !this._allowPrint ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiPrintDown.Visibility = !this._allowPrintDown ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiExpand.Visibility = !this._allowExpand ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiCollapse.Visibility = !this._allowCollapse ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiClose.Visibility = !this._allowClose ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiRevision.Visibility = !this._allowRevision ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiBreakDown.Visibility = !this._allowBreakDown ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiRangSize.Visibility = !this._allowRangeSize ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiCopyObject.Visibility = !this._allowCopyObject ? BarItemVisibility.Never : BarItemVisibility.Always;

            bbiGenerate.Visibility = !this._allowGenerate ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiCombine.Visibility = !this._allowCombine ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiCheck.Visibility = !this._allowCheck ? BarItemVisibility.Never : BarItemVisibility.Always;


            #region "Function"
            bbiGFN1.Visibility = !this._allowGFN1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN1_F1.Visibility = !this._allowGFN1_F1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN1_F2.Visibility = !this._allowGFN1_F2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN1_F3.Visibility = !this._allowGFN1_F3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN1_F4.Visibility = !this._allowGFN1_F4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN1_F5.Visibility = !this._allowGFN1_F5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN1_F6.Visibility = !this._allowGFN1_F6 ? BarItemVisibility.Never : BarItemVisibility.Always;
            //-----------------------------------------------------------------------------------------
            bbiGFN2.Visibility = !this._allowGFN2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN2_F1.Visibility = !this._allowGFN2_F1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN2_F2.Visibility = !this._allowGFN2_F2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN2_F3.Visibility = !this._allowGFN2_F3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN2_F4.Visibility = !this._allowGFN2_F4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN2_F5.Visibility = !this._allowGFN2_F5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN2_F6.Visibility = !this._allowGFN2_F6 ? BarItemVisibility.Never : BarItemVisibility.Always;
            //-----------------------------------------------------------------------------------------
            bbiGFN3.Visibility = !this._allowGFN3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN3_F1.Visibility = !this._allowGFN3_F1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN3_F2.Visibility = !this._allowGFN3_F2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN3_F3.Visibility = !this._allowGFN3_F3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN3_F4.Visibility = !this._allowGFN3_F4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN3_F5.Visibility = !this._allowGFN3_F5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN3_F6.Visibility = !this._allowGFN3_F6 ? BarItemVisibility.Never : BarItemVisibility.Always;
            //-----------------------------------------------------------------------------------------
            bbiGFN4.Visibility = !this._allowGFN4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN4_F1.Visibility = !this._allowGFN4_F1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN4_F2.Visibility = !this._allowGFN4_F2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN4_F3.Visibility = !this._allowGFN4_F3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN4_F4.Visibility = !this._allowGFN4_F4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN4_F5.Visibility = !this._allowGFN4_F5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN4_F6.Visibility = !this._allowGFN4_F6 ? BarItemVisibility.Never : BarItemVisibility.Always;
            //-----------------------------------------------------------------------------------------
            bbiGFN5.Visibility = !this._allowGFN5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN5_F1.Visibility = !this._allowGFN5_F1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN5_F2.Visibility = !this._allowGFN5_F2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN5_F3.Visibility = !this._allowGFN5_F3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN5_F4.Visibility = !this._allowGFN5_F4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN5_F5.Visibility = !this._allowGFN5_F5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiGFN5_F6.Visibility = !this._allowGFN5_F6 ? BarItemVisibility.Never : BarItemVisibility.Always;
            //-----------------------------------------------------------------------------------------
            bbiFN_1.Visibility = !this._allowFN1 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiFN_2.Visibility = !this._allowFN2 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiFN_3.Visibility = !this._allowFN3 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiFN_4.Visibility = !this._allowFN4 ? BarItemVisibility.Never : BarItemVisibility.Always;
            bbiFN_5.Visibility = !this._allowFN5 ? BarItemVisibility.Never : BarItemVisibility.Always;
            #endregion
            #endregion
        }


        #endregion

        #region "Set Default Button And Permission"



        public void SetDefaultCommandAndPermission(FrmBase fBase)
        {
            #region "Set Permission"
            
            this.PerIns = fBase.PerIns;
            this.PerUpd = fBase.PerUpd;
            this.PerDel = fBase.PerDel;
            this.PerViw = fBase.PerViw;
            this.StrAdvanceFunction = fBase.StrAdvanceFunction;
            this.MinStatus = fBase.MinStatus;
            this.MaxStatus = fBase.MaxStatus;
            this.DtPerFunction = fBase.DtPerFunction;
            this.MenuCode = fBase.MenuCode;
            this.StrSpecialFunction = fBase.StrSpecialFunction;
            this.DtSpecialFunction = fBase.DtSpecialFunction;
            this.PerCheckAdvanceFunction = fBase.PerCheckAdvanceFunction;
            #endregion

            #region "Set Button"

            this.bbiAdd = fBase.bbiAdd;
            this.bbiEdit = fBase.bbiEdit;
            this.bbiDelete = fBase.bbiDelete;
            this.bbiSave = fBase.bbiSave;
            this.bbiCancel = fBase.bbiCancel;
            this.bbiRefresh = fBase.bbiRefresh;
            this.bbiFind = fBase.bbiFind;
            this.bbiPrint = fBase.bbiPrint;
            this.bbiPrintDown = fBase.bbiPrintDown;
            this.bbiExpand = fBase.bbiExpand;
            this.bbiCollapse = fBase.bbiCollapse;
            this.bbiClose = fBase.bbiClose;
            this.bbiRevision = fBase.bbiRevision;
            this.bbiBreakDown = fBase.bbiBreakDown;
            this.bbiRangSize = fBase.bbiRangSize;
            this.bbiCopyObject = fBase.bbiCopyObject;
            this.bbiGenerate = fBase.bbiGenerate;
            this.bbiCombine = fBase.bbiCombine;
            this.bbiCheck = fBase.bbiCheck;


            #region "Function"
            this.bbiGFN1 = fBase.bbiGFN1;
            this.bbiGFN1_F1 = fBase.bbiGFN1_F1;
            this.bbiGFN1_F2 = fBase.bbiGFN1_F2;
            this.bbiGFN1_F3 = fBase.bbiGFN1_F3;
            this.bbiGFN1_F4 = fBase.bbiGFN1_F4;
            this.bbiGFN1_F5 = fBase.bbiGFN1_F5;
            this.bbiGFN1_F6 = fBase.bbiGFN1_F6;
            //-------------------------------------------------------------------------------
            this.bbiGFN2 = fBase.bbiGFN2;
            this.bbiGFN2_F1 = fBase.bbiGFN2_F1;
            this.bbiGFN2_F2 = fBase.bbiGFN2_F2;
            this.bbiGFN2_F3 = fBase.bbiGFN2_F3;
            this.bbiGFN2_F4 = fBase.bbiGFN2_F4;
            this.bbiGFN2_F5 = fBase.bbiGFN2_F5;
            this.bbiGFN2_F6 = fBase.bbiGFN2_F6;
            //-------------------------------------------------------------------------------
            this.bbiGFN3 = fBase.bbiGFN3;
            this.bbiGFN3_F1 = fBase.bbiGFN3_F1;
            this.bbiGFN3_F2 = fBase.bbiGFN3_F2;
            this.bbiGFN3_F3 = fBase.bbiGFN3_F3;
            this.bbiGFN3_F4 = fBase.bbiGFN3_F4;
            this.bbiGFN3_F5 = fBase.bbiGFN3_F5;
            this.bbiGFN3_F6 = fBase.bbiGFN3_F6;
            //-------------------------------------------------------------------------------
            this.bbiGFN4 = fBase.bbiGFN4;
            this.bbiGFN4_F1 = fBase.bbiGFN4_F1;
            this.bbiGFN4_F2 = fBase.bbiGFN4_F2;
            this.bbiGFN4_F3 = fBase.bbiGFN4_F3;
            this.bbiGFN4_F4 = fBase.bbiGFN4_F4;
            this.bbiGFN4_F5 = fBase.bbiGFN4_F5;
            this.bbiGFN4_F6 = fBase.bbiGFN4_F6;
            //-------------------------------------------------------------------------------
            this.bbiGFN5 = fBase.bbiGFN5;
            this.bbiGFN5_F1 = fBase.bbiGFN5_F1;
            this.bbiGFN5_F2 = fBase.bbiGFN5_F2;
            this.bbiGFN5_F3 = fBase.bbiGFN5_F3;
            this.bbiGFN5_F4 = fBase.bbiGFN5_F4;
            this.bbiGFN5_F5 = fBase.bbiGFN5_F5;
            this.bbiGFN5_F6 = fBase.bbiGFN5_F6;
            //-------------------------------------------------------------------------------
            this.bbiFN_1 = fBase.bbiFN_1;
            this.bbiFN_2 = fBase.bbiFN_2;
            this.bbiFN_3 = fBase.bbiFN_3;
            this.bbiFN_4 = fBase.bbiFN_4;
            this.bbiFN_5 = fBase.bbiFN_5;
            #endregion
            #endregion

            #region "Set Caption"
            this.SetCaptionPrint = CommandBarDefaultSetting.CaptionButtonPrintDefaultCommamd;
            this.SetCaptionPrintDown = CommandBarDefaultSetting.CaptionButtonPrintDownDefaultCommamd;
            this.SetCaptionAdd = CommandBarDefaultSetting.CaptionButtonAddDefaultCommamd;
            this.SetCaptionEdit = CommandBarDefaultSetting.CaptionButtonEditDefaultCommamd;
            this.SetCaptionDelete = CommandBarDefaultSetting.CaptionButtonDeleteDefaultCommamd;
            this.SetCaptionSave = CommandBarDefaultSetting.CaptionButtonSaveDefaultCommamd;
            this.SetCaptionCancel = CommandBarDefaultSetting.CaptionButtonCancelDefaultCommamd;
            this.SetCaptionRefresh = CommandBarDefaultSetting.CaptionButtonRefreshDefaultCommamd;
            this.SetCaptionFind = CommandBarDefaultSetting.CaptionButtonFindDefaultCommamd;
            this.SetCaptionBreakDown = CommandBarDefaultSetting.CaptionButtonBreakDownDefaultCommamd;
            this.SetCaptionCheck = CommandBarDefaultSetting.CaptionButtonCheckDefaultCommamd;
            this.SetCaptionClose = CommandBarDefaultSetting.CaptionButtonCloseDefaultCommamd;
            this.SetCaptionExpand = CommandBarDefaultSetting.CaptionButtonExpandDefaultCommamd;
            this.SetCaptionCollapse = CommandBarDefaultSetting.CaptionButtonCollosepandDefaultCommamd;
            this.SetCaptionRevision = CommandBarDefaultSetting.CaptionButtonRevisionDefaultCommamd;
            this.SetCaptionCombine = CommandBarDefaultSetting.CaptionButtonCombineDefaultCommamd;
            this.SetCaptionGenerate = CommandBarDefaultSetting.CaptionButtonGenerateDefaultCommamd;
            this.SetCaptionCopyObject = CommandBarDefaultSetting.CaptionButtonCopyDefaultCommamd;
            this.SetCaptionRangSize = CommandBarDefaultSetting.CaptionButtonRangeSizeDefaultCommamd;

            
            #region "Function"
            this.SetCaptionGFN1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN1_F1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN1_F2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN1_F3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN1_F4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN1_F5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN1_F6 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.SetCaptionGFN2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN2_F1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN2_F2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN2_F3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN2_F4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN2_F5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN2_F6 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.SetCaptionGFN3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN3_F1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN3_F2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN3_F3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN3_F4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN3_F5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN3_F6 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.SetCaptionGFN4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN4_F1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN4_F2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN4_F3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN4_F4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN4_F5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN4_F6 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.SetCaptionGFN5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN5_F1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN5_F2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN5_F3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN5_F4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN5_F5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionGFN5_F6 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.SetCaptionFN1 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionFN2 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionFN3 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionFN4 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            this.SetCaptionFN5 = CommandBarDefaultSetting.CaptionButtonFunctionDefaultCommamd;
            #endregion
            #endregion

            #region "Set Image"
            this.SetImagePrint = CommandBarDefaultSetting.ImageButtonPrint;
            this.SetImagePrintDown = CommandBarDefaultSetting.ImageButtonPrintDown;
            this.SetImageAdd = CommandBarDefaultSetting.ImageButtonAdd;
            this.SetImageEdit = CommandBarDefaultSetting.ImageButtonEdit;
            this.SetImageDelete = CommandBarDefaultSetting.ImageButtonDelete;
            this.SetImageSave = CommandBarDefaultSetting.ImageButtonSave;
            this.SetImageCancel = CommandBarDefaultSetting.ImageButtonCancel;
            this.SetImageRefresh = CommandBarDefaultSetting.ImageButtonRefresh;
            this.SetImageFind = CommandBarDefaultSetting.ImageButtonFind;
            this.SetImageBreakDown = CommandBarDefaultSetting.ImageButtonBreakDown;
            this.SetImageCheck = CommandBarDefaultSetting.ImageButtonCheck;
            this.SetImageClose = CommandBarDefaultSetting.ImageButtonClose;
            this.SetImageExpand = CommandBarDefaultSetting.ImageButtonExpand;
            this.SetImageCollapse = CommandBarDefaultSetting.ImageButtonCollosepand;
            this.SetImageRevision = CommandBarDefaultSetting.ImageButtonRevision;
            this.SetImageCombine = CommandBarDefaultSetting.ImageButtonCombine;
            this.SetImageGenerate = CommandBarDefaultSetting.ImageButtonGenerate;
            this.SetImageCopyObject = CommandBarDefaultSetting.ImageButtonCopy;
            this.SetImageRangSize = CommandBarDefaultSetting.ImageButtonRangeSize;

            #region "Function"
            this.SetImageGFN1 = CommandBarDefaultSetting.ImageButtonFunction;
            this.SetImageGFN1_F1 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN1_F2 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN1_F3 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN1_F4 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN1_F5 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN1_F6 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            //-------------------------------------------------------------------------------
            this.SetImageGFN2 = CommandBarDefaultSetting.ImageButtonFunction;
            this.SetImageGFN2_F1 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN2_F2 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN2_F3 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN2_F4 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN2_F5 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN2_F6 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            //-------------------------------------------------------------------------------
            this.SetImageGFN3 = CommandBarDefaultSetting.ImageButtonFunction;
            this.SetImageGFN3_F1 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN3_F2 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN3_F3 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN3_F4 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN3_F5 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN3_F6 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            //-------------------------------------------------------------------------------
            this.SetImageGFN4 = CommandBarDefaultSetting.ImageButtonFunction;
            this.SetImageGFN4_F1 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN4_F2 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN4_F3 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN4_F4 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN4_F5 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN4_F6 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            //-------------------------------------------------------------------------------
            this.SetImageGFN5 = CommandBarDefaultSetting.ImageButtonFunction;
            this.SetImageGFN5_F1 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN5_F2 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN5_F3 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN5_F4 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN5_F5 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            this.SetImageGFN5_F6 = CommandBarDefaultSetting.ImageButtonFunctionDown;
            //-------------------------------------------------------------------------------
            this.SetImageFN1 = CommandBarDefaultSetting.ImageButtonFunction;
            this.SetImageFN2 = CommandBarDefaultSetting.ImageButtonFunction;
            this.SetImageFN3 = CommandBarDefaultSetting.ImageButtonFunction;
            this.SetImageFN4 = CommandBarDefaultSetting.ImageButtonFunction;
            this.SetImageFN5 = CommandBarDefaultSetting.ImageButtonFunction;
            #endregion

            #endregion

            #region "Set Enable"
            this.EnablePrint = CommandBarDefaultSetting.EnableButtonPrintDefaultCommamd;
            this.EnablePrintDown = CommandBarDefaultSetting.EnableButtonPrintDownDefaultCommamd;
            this.EnableAdd = CommandBarDefaultSetting.EnableButtonAddDefaultCommamd;
            this.EnableEdit = CommandBarDefaultSetting.EnableButtonEditDefaultCommamd;
            this.EnableDelete = CommandBarDefaultSetting.EnableButtonDeleteDefaultCommamd;
            this.EnableSave = CommandBarDefaultSetting.EnableButtonSaveDefaultCommamd;
            this.EnableCancel = CommandBarDefaultSetting.EnableButtonCancelDefaultCommamd;
            this.EnableRefresh = CommandBarDefaultSetting.EnableButtonRefreshDefaultCommamd;
            this.EnableFind = CommandBarDefaultSetting.EnableButtonFindDefaultCommamd;
            this.EnableBreakDown = CommandBarDefaultSetting.EnableButtonBreakDownDefaultCommamd;
            this.EnableCheck = CommandBarDefaultSetting.EnableButtonCheckDefaultCommamd;
            this.EnableClose = CommandBarDefaultSetting.EnableButtonCloseDefaultCommamd;
            this.EnableExpand = CommandBarDefaultSetting.EnableButtonExpandDefaultCommamd;
            this.EnableCollapse = CommandBarDefaultSetting.EnableButtonCollosepandDefaultCommamd;
            this.EnableRevision = CommandBarDefaultSetting.EnableButtonRevisionDefaultCommamd;
            this.EnableCombine = CommandBarDefaultSetting.EnableButtonCombineDefaultCommamd;
            this.EnableGenerate = CommandBarDefaultSetting.EnableButtonGenerateDefaultCommamd;
            this.EnableCopyObject = CommandBarDefaultSetting.EnableButtonCopyDefaultCommamd;
            this.EnableRangSize = CommandBarDefaultSetting.EnableButtonRangeSizeDefaultCommamd;
            #region "Function"
            this.EnableGFN1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN1_F1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN1_F2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN1_F3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN1_F4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN1_F5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN1_F6 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.EnableGFN2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN2_F1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN2_F2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN2_F3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN2_F4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN2_F5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN2_F6 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.EnableGFN3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN3_F1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN3_F2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN3_F3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN3_F4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN3_F5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN3_F6 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.EnableGFN4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN4_F1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN4_F2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN4_F3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN4_F4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN4_F5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN4_F6 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.EnableGFN5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN5_F1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN5_F2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN5_F3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN5_F4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN5_F5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableGFN5_F6 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.EnableFN1 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableFN2 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableFN3 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableFN4 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            this.EnableFN5 = CommandBarDefaultSetting.EnableButtonFunctionDefaultCommamd;
            #endregion
            #endregion

            #region "Set Visible"
            this.AllowAdd = CommandBarDefaultSetting.AllowButtonAddDefaultCommamd;
            this.AllowEdit = CommandBarDefaultSetting.AllowButtonEditDefaultCommamd;
            this.AllowDelete = CommandBarDefaultSetting.AllowButtonDeleteDefaultCommamd;
            this.AllowSave = CommandBarDefaultSetting.AllowButtonSaveDefaultCommamd;
            this.AllowCancel = CommandBarDefaultSetting.AllowButtonCancelDefaultCommamd;
            this.AllowRefresh = CommandBarDefaultSetting.AllowButtonRefreshDefaultCommamd;
            this.AllowFind = CommandBarDefaultSetting.AllowButtonFindDefaultCommamd;
            this.AllowPrint = CommandBarDefaultSetting.AllowButtonPrintDefaultCommamd;
            this.AllowPrintDown = CommandBarDefaultSetting.AllowButtonPrintDownDefaultCommamd;
            this.AllowBreakDown = CommandBarDefaultSetting.AllowButtonBreakDownDefaultCommamd;
            this.AllowRevision = CommandBarDefaultSetting.AllowButtonRevisionDefaultCommamd;
            this.AllowRangeSize = CommandBarDefaultSetting.AllowButtonRangeSizeDefaultCommamd;
            this.AllowCopyObject = CommandBarDefaultSetting.AllowButtonCopyDefaultCommamd;
            this.AllowGenerate = CommandBarDefaultSetting.AllowButtonGenerateDefaultCommamd;
            this.AllowCombine = CommandBarDefaultSetting.AllowButtonCombineDefaultCommamd;
            this.AllowCheck = CommandBarDefaultSetting.AllowButtonCheckDefaultCommamd;
            this.AllowCollapse = CommandBarDefaultSetting.AllowButtonCollosepandDefaultCommamd;
            this.AllowExpand = CommandBarDefaultSetting.AllowButtonExpandDefaultCommamd;
            this.AllowClose = CommandBarDefaultSetting.AllowButtonCloseDefaultCommamd;
            #region "Function"
            this.AllowGFN1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN1_F1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN1_F2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN1_F3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN1_F4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN1_F5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN1_F6 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.AllowGFN2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN2_F1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN2_F2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN2_F3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN2_F4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN2_F5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN2_F6 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.AllowGFN3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN3_F1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN3_F2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN3_F3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN3_F4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN3_F5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN3_F6 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.AllowGFN4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN4_F1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN4_F2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN4_F3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN4_F4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN4_F5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN4_F6 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.AllowGFN5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN5_F1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN5_F2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN5_F3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN5_F4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN5_F5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowGFN5_F6 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            //-------------------------------------------------------------------------------
            this.AllowFN1 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowFN2 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowFN3 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowFN4 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            this.AllowFN5 = CommandBarDefaultSetting.AllowButtonFunctionDefaultCommamd;
            #endregion

            #endregion

            #region "Set Tooltip"
            bbiAdd.Hint = this._hintAdd;
            bbiEdit.Hint = this._hintEdit;
            bbiDelete.Hint = this._hintDelete;
            bbiSave.Hint = this._hintSave;
            bbiCancel.Hint = this._hintCancel;
            bbiRefresh.Hint = this._hintRefresh;
            bbiFind.Hint = this._hintFind;
            bbiPrint.Hint = this._hintPrint;
            bbiPrintDown.Hint = this._hintPrintDown;
            bbiExpand.Hint = this._hintExpand;
            bbiCollapse.Hint = this._hintCollapse;
            bbiClose.Hint = this._hintClose;
            bbiRevision.Hint = this._hintRevision;
            bbiBreakDown.Hint = this._hintBreakDown;
            bbiRangSize.Hint = this._hintRangeSize;
            bbiCopyObject.Hint = this._hintCopyObject;
            bbiGenerate.Hint = this._hintGenerate;
            bbiCombine.Hint = this._hintCombine;
            bbiCheck.Hint = this._hintCheck;
            #region "Function"
            bbiGFN1.Hint = this._hintGFN1;
            bbiGFN1_F1.Hint = this._hintGFN1_F1;
            bbiGFN1_F2.Hint = this._hintGFN1_F2;
            bbiGFN1_F3.Hint = this._hintGFN1_F3;
            bbiGFN1_F4.Hint = this._hintGFN1_F4;
            bbiGFN1_F5.Hint = this._hintGFN1_F5;
            bbiGFN1_F6.Hint = this._hintGFN1_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN2.Hint = this._hintGFN2;
            bbiGFN2_F1.Hint = this._hintGFN2_F1;
            bbiGFN2_F2.Hint = this._hintGFN2_F2;
            bbiGFN2_F3.Hint = this._hintGFN2_F3;
            bbiGFN2_F4.Hint = this._hintGFN2_F4;
            bbiGFN2_F5.Hint = this._hintGFN2_F5;
            bbiGFN2_F6.Hint = this._hintGFN2_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN3.Hint = this._hintGFN3;
            bbiGFN3_F1.Hint = this._hintGFN3_F1;
            bbiGFN3_F2.Hint = this._hintGFN3_F2;
            bbiGFN3_F3.Hint = this._hintGFN3_F3;
            bbiGFN3_F4.Hint = this._hintGFN3_F4;
            bbiGFN3_F5.Hint = this._hintGFN3_F5;
            bbiGFN3_F6.Hint = this._hintGFN3_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN4.Hint = this._hintGFN4;
            bbiGFN4_F1.Hint = this._hintGFN4_F1;
            bbiGFN4_F2.Hint = this._hintGFN4_F2;
            bbiGFN4_F3.Hint = this._hintGFN4_F3;
            bbiGFN4_F4.Hint = this._hintGFN4_F4;
            bbiGFN4_F5.Hint = this._hintGFN4_F5;
            bbiGFN4_F6.Hint = this._hintGFN4_F6;
            //-----------------------------------------------------------------------------------------
            bbiGFN5.Hint = this._hintGFN5;
            bbiGFN5_F1.Hint = this._hintGFN5_F1;
            bbiGFN5_F2.Hint = this._hintGFN5_F2;
            bbiGFN5_F3.Hint = this._hintGFN5_F3;
            bbiGFN5_F4.Hint = this._hintGFN5_F4;
            bbiGFN5_F5.Hint = this._hintGFN5_F5;
            bbiGFN5_F6.Hint = this._hintGFN5_F6;
            //-----------------------------------------------------------------------------------------
            bbiFN_1.Hint = this._hintFN1;
            bbiFN_2.Hint = this._hintFN2;
            bbiFN_3.Hint = this._hintFN3;
            bbiFN_4.Hint = this._hintFN4;
            bbiFN_5.Hint = this._hintFN5;
            #endregion
            #endregion
        }


        #endregion







    }
}