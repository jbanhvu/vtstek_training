using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CNY_BaseSys.Class;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using CNY_BaseSys.Common;
using CNY_BaseSys.Properties;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraRichEdit;

namespace CNY_BaseSys.WForm
{
    public partial class FrmSendMailMultiAttach : XtraForm
    {
       

        
        #region "Property"

        public event SendMailHandler sendMailarg = null;

        private string sMailServer = "";
        private string sDomainName = "";
        private string sUserNane = "";
        private int sPort = 0;
        private string sEmailLogin = "";
        private string sPassLogin = "";
        private string sMailFrom = "";
        private string sMailto = "";
        private string sSubject = "";
        private string sContent = "";
        private bool sCheckSSL = false;
        private bool sUseContentHTML = true;
        private bool sEnableTextEditMailServer = true;
        private bool sEnableTextEditDomainName = true;
        private bool sEnableTextEditPort = true;
        private bool sEnableTextEditUserName = true;
        private bool sEnableTextEditMailFrom = true;
        private bool sEnableTextEditMailto = true;
        private bool sEnableTextEditPassLogin = true;
        private bool sEnableTextEditEmailLogin = true;
        private bool sEnableTextEditSubject = true;
        private bool sEnableTextEditContent = true;
        private bool sEnableTextEditCheckSSL = true;
        private bool sEnableButtonAddFile = true;
        private bool sEnableButtonRemoveFile = true;
        private bool sEnableButtonSendMail = true;
        private bool sEnableButtonClose = true;
        private DataTable sdtAttach = SetTableTemplateGirdView();
        /// <summary>
        ///  Gets or Sets Mail Server value
        /// </summary>
        public string MailServer { get { return this.sMailServer; } set { this.sMailServer = value; } }        
        /// <summary>
        ///  Gets or Sets Domain Name value
        /// </summary>
        public string DomainName { get { return this.sDomainName; } set {this.sDomainName=value ;} }
        /// <summary>
        ///  Gets or Sets User Name Login System value
        /// </summary>
        public string UserName { get { return this.sUserNane; } set {this.sUserNane=value ;} }
        /// <summary>
        ///  Gets or Sets Port value
        /// </summary>
        public int Port { get { return this.sPort; } set { this.sPort = value; } }
        /// <summary>
        ///  Gets or Sets (Email Login Mail Server For Send Mail) value
        /// </summary>
        public string EmailLogin { get { return this.sEmailLogin; } set { this.sEmailLogin = value; } }
        /// <summary>
        ///  Gets or Sets (Password Login Mail Server For Send Mail) value
        /// </summary>
        public string PassLogin { get { return this.sPassLogin; } set { this.sPassLogin = value; } }
        /// <summary>
        ///  Gets or Sets Sender Mail value
        /// </summary>
        public string MailFrom { get { return this.sMailFrom; } set { this.sMailFrom = value; } }
        /// <summary>
        ///  Gets or Sets List recipient mail value
        /// </summary>
        public string MailTo { get { return this.sMailto; } set { this.sMailto = value; } }
        /// <summary>
        ///  Gets or Sets Subject Mail value
        /// </summary>
        public string Subject { get { return this.sSubject; } set { this.sSubject = value; } }
        /// <summary>
        ///  Gets or Sets Content Mail value
        /// </summary>
        public string Content { get { return this.sContent; } set { this.sContent = value; } }
        /// <summary>
        ///  Gets or Sets SSL Mail value
        /// </summary>
        public bool CheckSSL { get { return this.sCheckSSL; } set { this.sCheckSSL = value; } }
        /// <summary>
        ///  Gets or Sets Content Mail allow HTML
        /// </summary>
        public bool UseContentHTML { get { return this.sUseContentHTML; } set { this.sUseContentHTML = value; } }        
        /// <summary>
        ///    Gets or Sets TextEditMailServer a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableTextEditMailServer
        {
            get { return sEnableTextEditMailServer; }
            set
            {
                txtMailServer.Enabled = value;
                sEnableTextEditMailServer = value;
            }
        }        
        /// <summary>
        ///    Gets or Sets TextEditDomainName a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableTextEditDomainName
        {
            get { return sEnableTextEditDomainName; }
            set
            {
                txtDomainName.Enabled = value;
                sEnableTextEditDomainName = value;
            }
        }        
        /// <summary>
        ///    Gets or Sets TextEditPort a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableTextEditPort
        {
            get { return sEnableTextEditPort; }
            set
            {
                txtPort.Enabled = value;
                sEnableTextEditPort = value;
            }
        }        
        /// <summary>
        ///    Gets or Sets TextEditUserName a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableTextEditUserName
        {
            get { return sEnableTextEditUserName; }
            set
            {
                txtUserName.Enabled = value;
                sEnableTextEditUserName = value;
            }
        }       
        /// <summary>
        ///    Gets or Sets TextEditEmailLogin a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableTextEditEmailLogin
        {
            get { return sEnableTextEditEmailLogin; }
            set
            {
                txtEmailLogin.Enabled = value;
                sEnableTextEditEmailLogin = value;
            }
        }        
        /// <summary>
        ///    Gets or Sets TextEditPassLogin a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableTextEditPassLogin
        {
            get { return sEnableTextEditPassLogin; }
            set
            {
                txtPassLogin.Enabled = value;
                sEnableTextEditPassLogin = value;
            }
        }
        /// <summary>
        ///    Gets or Sets TextEditMailFrom a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableTextEditMailFrom
        {
            get { return sEnableTextEditMailFrom; }
            set
            {
                txtMailFrom.Enabled = value;
                sEnableTextEditMailFrom = value;
            }
        }       
        /// <summary>
        ///    Gets or Sets TextEditMailto a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableTextEditMailto
        {
            get { return sEnableTextEditMailto; }
            set
            {
                tkMailTo.Enabled = value;
                sEnableTextEditMailto = value;
            }
        }        
        /// <summary>
        ///    Gets or Sets TextEditSubject a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableTextEditSubject
        {
            get { return sEnableTextEditSubject; }
            set
            {
                txtSubject.Enabled = value;
                sEnableTextEditSubject = value;
            }
        }     
        /// <summary>
        ///    Gets or Sets TextEditContent a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableTextEditContent
        {
            get { return sEnableTextEditContent; }
            set
            {
                txtContent.Enabled = value;
                sEnableTextEditContent = value;
            }
        }
        /// <summary>
        ///    Gets or Sets CheckSSL a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableCheckSSL
        {
            get { return sEnableTextEditCheckSSL; }
            set
            {
                chkEnabeSSL.Enabled = value;
                sEnableTextEditCheckSSL = value;
            }
        }        
        /// <summary>
        ///    Gets or Sets ButtonAddFile  a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableButtonAddFile 
        {
            get { return sEnableButtonAddFile; }
            set
            {
                btnAdd.Enabled = value;
                sEnableButtonAddFile = value;
            }
        }        
        /// <summary>
        ///    Gets or Sets ButtonRemoveFile a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableButtonRemoveFile
        {
            get { return sEnableButtonRemoveFile; }
            set
            {
                btnRemove.Enabled = value;
                sEnableButtonRemoveFile = value;
            }
        }   
        /// <summary>
        ///    Gets or Sets ButtonSendMail a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableButtonSendMail
        {
            get { return sEnableButtonSendMail; }
            set
            {
                btnSend.Enabled = value;
                sEnableButtonSendMail = value;
            }
        }       
        /// <summary>
        ///    Gets or Sets ButtonClose a value indicating whether the control can respond to user interaction
        /// </summary>    
        public bool EnableButtonClose
        {
            get { return sEnableButtonClose; }
            set
            {
                btnClose.Enabled = value;
                sEnableButtonClose = value;
            }
        }       
        /// <summary>
        ///      Gets or Sets Table Attach File value
        /// </summary>        
        public DataTable dtAttacch
        {
            get { return this.sdtAttach; }
            set
            {
                this.sdtAttach = value;
            }
        }

        private string _delimiter;
        public string Delimiter
        {
            get { return this._delimiter; }
            set
            {
                this._delimiter = value;
            }
        }


        private DataTable _dtDepartment = ProcessGeneral.GetDefaultTableStringPk();
        public DataTable DtDepartment
        {
            get { return this._dtDepartment; }
            set
            {
                this._dtDepartment = value;
            }
        }

        public RichEditViewType RichTextViewType
        {
            get { return txtContent.RichTextViewType; }
            set
            {
                txtContent.RichTextViewType = value;
            }
        }

        private Dictionary<string, UserSystemEmailInfo> _dicEmailLogin;
        #endregion

        #region "Contructor"
        public FrmSendMailMultiAttach(string delimiter = ",")
        {
            InitializeComponent();

            _dicEmailLogin = DeclareSystem.DtUserListWhenLogin.AsEnumerable().Where(p=> ProcessGeneral.CheckFormatEmailAddress(p.Field<string>("Email")))
                .OrderBy(p=>p.Field<string>("FristName")).ThenBy(p=>p.Field<string>("Name")).GroupBy(p => new
            {
                Email = p.Field<string>("Email")}).Select(s => new
            {
                s.Key.Email,
                Data = s.Select(t=>new UserSystemEmailInfo
                {
                    Code = t.Field<string>("Code"),
                    Name = t.Field<string>("Name"),
                    Position = t.Field<string>("Position"),
                    Department = t.Field<string>("Department"),
                    FristName = t.Field<string>("FristName"),
                }).First()
            }).ToDictionary(s=>s.Email,s=>s.Data);


            txtContent.RichTextViewType = RichEditViewType.Simple;
            this._delimiter = delimiter;
          //  MaximizeBox = false;
            MinimizeBox = false;
            GridViewCustomInit();

            //var dt1 = ProcessGeneral.GetAllEmailAddressInTableListUser();

            //var dt2 = ProcessGeneral.GetAllInforUserInDomain(false);

            //string[] arrDefaultEmail = dt1.AsEnumerable().Select(p => p.Field<string>("Email").Trim()).Union(dt2.AsEnumerable().Select(p => p.Field<string>("Email").Trim()))
            //    .Where(ProcessGeneral.CheckFormatEmailAddress) .OrderBy(p => p).ToArray();

            //foreach (string email in arrDefaultEmail)
            //{
            //    var item = new TokenEditToken(email, email);
            //    tkMailTo.Properties.Tokens.Add(item);//}


            foreach (var item in _dicEmailLogin)
            {
     
                tkMailTo.Properties.Tokens.Add(new TokenEditToken(item.Key, item.Key));
            }



            tkMailTo.Properties.ShowDropDown = true;
            tkMailTo.Properties.DropDownRowCount = 8;
            tkMailTo.Properties.MaxExpandLines = 10;
            tkMailTo.Properties.PopupPanelOptions.ShowPopupPanel = false;
            tkMailTo.Properties.PopupPanelOptions.Location = TokenEditPopupPanelLocation.Default;
            tkMailTo.Properties.ShowSelectedItemsInPopup = false;
            tkMailTo.Properties.DropDownShowMode = TokenEditDropDownShowMode.Default;
            tkMailTo.Properties.EditMode = TokenEditMode.Default;
            tkMailTo.Properties.ShowRemoveTokenButtons = true;
            tkMailTo.Properties.PopupSizeable = true;
            char d = string.IsNullOrEmpty(_delimiter) ? ',' : _delimiter.ToCharArray()[0];
            tkMailTo.Properties.EditValueSeparatorChar = d;
            tkMailTo.Properties.CheckMode = TokenEditCheckMode.Default;
            tkMailTo.ValidateToken += tkMailTo_ValidateToken; ;
            tkMailTo.SizeChanged += tkMailTo_SizeChanged;

            txtMailServer.EditValue = DeclareSystem.SysMailServer;
            txtDomainName.EditValue = DeclareSystem.SysDomainName;          
            txtPort.EditValue = DeclareSystem.SysMailPort; 
            
            txtEmailLogin.EditValue = DeclareSystem.SysMailUserName;
            txtPassLogin.EditValue = DeclareSystem.SysMailUserPass;
            txtMailFrom.EditValue = DeclareSystem.SysMailUserName;
            btnAdvanceSearch.Click += BtnAdvanceSearch_Click;
            toolTipController1.GetActiveObjectInfo += ToolTipController1_GetActiveObjectInfo;

        }

        private void ToolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {if (e.SelectedControl != tkMailTo) return;
            TokenEditHitInfo hitInfo = tkMailTo.CalcHitInfo(e.ControlMousePosition);
            if (!hitInfo.InToken) return;
            TokenEditToken tokenInfo = hitInfo.Token;
            string email = tokenInfo.Description;
            UserSystemEmailInfo userInfo;
            if (!_dicEmailLogin.TryGetValue(email, out userInfo)) return;

            string content = "";

            if (!string.IsNullOrEmpty(userInfo.Code))
            {
                content = content+ "<size=10><b>Username : </b></size><size=10><color=102, 51, 204>" + userInfo.Code+ "</color></size><br>";
            }
            if (!string.IsNullOrEmpty(userInfo.Name))
            {
                content = content + "<size=10><b>Full Name : </b></size><size=10><color=102, 51, 204>" + userInfo.Name + "</color></size><br>";
            }

            if (!string.IsNullOrEmpty(userInfo.Department))
            {
                content = content + "<size=10><b>Department : </b></size><size=10><color=102, 51, 204>" + userInfo.Department + "</color></size><br>";
            }


            if (!string.IsNullOrEmpty(userInfo.Position))
            {
                content = content + "<size=10><b>Position : </b></size><size=10><color=102, 51, 204>" + userInfo.Position + "</color></size><br>";
            }

            if (string.IsNullOrEmpty(content)) return;


            SuperToolTip st = new SuperToolTip();
            SuperToolTipSetupArgs args = new SuperToolTipSetupArgs(st);
            args.AllowHtmlText = DefaultBoolean.True;
            args.Title.AllowHtmlText = DefaultBoolean.True;
            args.ShowFooterSeparator = false;
            args.Title.Text = string.Format("<color=102, 0, 51><u><b><size=12>{0}</size></b></u></color>", email);
            args.Contents.Text = content;st.Setup(args);
            ToolTipControlInfo info = new ToolTipControlInfo(tokenInfo, args.Contents.Text);
            info.SuperTip = st;
            info.ToolTipType = ToolTipType.SuperTip;
            e.Info = info;


          
        }

        private void BtnAdvanceSearch_Click(object sender, EventArgs e)
        {
            string strMailTo = ProcessGeneral.GetSafeString(tkMailTo.EditValue);
            string[] arrMailTo = strMailTo.Split(new[] { _delimiter }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).Where(p => !string.IsNullOrEmpty(p)).ToArray();
            FrmTreeEmail f = new FrmTreeEmail(_dtDepartment, arrMailTo);
            
            f.OnTransferData += (s1, e1) =>
            {

                Dictionary<string, UserSystemEmailInfo> dicEmail = e1.DicEmail;
                if (dicEmail.Count <= 0) return;
             //   string[] arrToKen = tkMailTo.Properties.Tokens.Select(p => p.Description).Distinct().ToArray();

                //var qAddToken = dicEmail.Where(p => !arrToKen.Contains(p.Key)).Select(p => p.Key).ToList();
                //foreach (string email in qAddToken)
                //{
                //    tkMailTo.Properties.Tokens.Add(new TokenEditToken(email, email));
                //}
                var qAddEmail = dicEmail.Where(p => !_dicEmailLogin.ContainsKey(p.Key)).ToList();
                foreach (var itemMail in qAddEmail)
                {
                    _dicEmailLogin.Add(itemMail.Key,itemMail.Value);
                    tkMailTo.Properties.Tokens.Add(new TokenEditToken(itemMail.Key, itemMail.Key));
                }



               

             

                List<string> qMailAdd = dicEmail.Where(p => !arrMailTo.Contains(p.Key)).Select(p => p.Key).ToList();
                string s2 = "";
                if (qMailAdd.Count > 0)
                {

                    foreach (string email in qMailAdd)
                    {
                        s2 = s2 + email + _delimiter;
                    }

                    s2 = s2.Substring(0, s2.Length - 1);

              
                }

                if (!string.IsNullOrEmpty(s2))
                {

                    string s3 = string.IsNullOrEmpty(strMailTo) ? s2 : strMailTo + "," + s2;
                    tkMailTo.EditValue = s3;
                }
              
                //  tkMailTo.EditValue = s1;
                // this.sMailto = s1;

            };
            f.ShowDialog();
         

        
        }




        #endregion

        #region "Form Event"


        public void AddEmailToTokenEdit(params string [] arrEmail)
        {

            Dictionary<string,string> arrDefaultEmail = tkMailTo.Properties.Tokens.Select(p => ProcessGeneral.GetSafeString(p.Value)).ToDictionary(p=>p,p=>p);


            var q1 = arrEmail.Where(p => !arrDefaultEmail.ContainsKey(p) && ProcessGeneral.CheckFormatEmailAddress(p)).ToArray();

           
            foreach (string email in q1)
            {
                var item = new TokenEditToken(email, email);
                tkMailTo.Properties.Tokens.Add(item);
            }

        }

        private void FrmSendMailMultiAttach_Load(object sender, EventArgs e)
        {

            if (this.sMailServer != string.Empty)
            {
                txtMailServer.EditValue = this.sMailServer;
            }
            if (this.sDomainName != string.Empty)
            {
                txtDomainName.EditValue = this.sDomainName;
            }          
            if (this.sPort!= 0)
            {
                txtPort.EditValue = this.sPort;
            }
            if (this.sEmailLogin!= string.Empty)
            {
                txtEmailLogin.EditValue = this.sEmailLogin;
            }
            if (this.sPassLogin != string.Empty)
            {
                txtPassLogin.EditValue = this.sPassLogin;
            }
            if (this.sMailFrom != string.Empty){
                txtMailFrom.EditValue = this.sMailFrom;
            }
            if (this.sMailto != string.Empty)
            {
                string[] arrMailTo = this.sMailto.Split(new[] { _delimiter }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim())
                    .Where(p=>_dicEmailLogin.ContainsKey(p))
                    .Distinct().ToArray();

                Dictionary<string,string> arrToKen = tkMailTo.Properties.Tokens.Select(p => p.Description).Distinct().ToDictionary(p=>p,p=>p);

                string[] qMailAdd = arrMailTo.Where(p=> arrToKen.ContainsKey(p)).ToArray();

           

                string s1 = "";
                if (qMailAdd.Length > 0)
                {
                  
                    foreach (string email in qMailAdd)
                    {
                    
                        s1 = s1 + email + _delimiter;
                    }

                    s1 = s1.Substring(0, s1.Length - 1);

                    tkMailTo.EditValue = s1;
                }
                this.sMailto = s1;
                
            }
            txtUserName.EditValue = this.sUserNane;
            chkEnabeSSL.Checked = this.sCheckSSL;
            txtSubject.EditValue = this.sSubject;
            txtContent.GSHTMLText =this.sContent;            
            this.Text = string.Format("Send Mail {0}", txtSubject.EditValue);
            gcAttachFile.DataSource = this.sdtAttach;
            BestFitColumnsGv();
            txtMailFrom.Select();


            Rectangle screen = Screen.FromControl(this).WorkingArea;
            this.Height = screen.Height;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(this.Location.X, 0);
        }

    
        #endregion

        #region "Init GridView"
        private void BestFitColumnsGv()
        {
            gvAttachFile.BestFitColumns();
            gvAttachFile.Columns["FileName"].Width += 30;
        }
        /// <summary>
        ///     Get Template Table For GridView Init
        /// </summary>
        /// <returns>
        ///     A System.Data.DataTable value...
        /// </returns>
        private static DataTable SetTableTemplateGirdView()
        {
            var dtTest = new DataTable();
            dtTest.Columns.Add("FileName", typeof(string));
            dtTest.Columns.Add("FileSize", typeof(string));
            dtTest.Columns.Add("Path", typeof(string));            
            return dtTest;
        }
        
         /// <summary>
        ///     Khởi tạo cấu trúc của girdview
        /// </summary>
        private void GridViewCustomInit()
        {


            gcAttachFile.UseEmbeddedNavigator = true;

            gcAttachFile.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcAttachFile.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcAttachFile.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcAttachFile.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcAttachFile.EmbeddedNavigator.Buttons.Remove.Visible = false;
            
            
        //   gridView1.OptionsBehavior.AutoPopulateColumns = false;
            gvAttachFile.OptionsBehavior.Editable = false;
            gvAttachFile.OptionsBehavior.AllowAddRows = DefaultBoolean.True;
            gvAttachFile.OptionsBehavior.AllowDeleteRows = DefaultBoolean.True;
            gvAttachFile.OptionsCustomization.AllowColumnMoving = false;
            gvAttachFile.OptionsCustomization.AllowQuickHideColumns = true;
            gvAttachFile.OptionsCustomization.AllowSort = true;
            gvAttachFile.OptionsCustomization.AllowFilter = true;


            gvAttachFile.OptionsView.ShowGroupPanel = false;
            gvAttachFile.OptionsView.ShowIndicator = true;
            gvAttachFile.OptionsView.ShowHorizontalLines = DefaultBoolean.True;
            gvAttachFile.OptionsView.ShowVerticalLines = DefaultBoolean.True;
            gvAttachFile.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            gvAttachFile.OptionsView.ShowAutoFilterRow = false;
            gvAttachFile.OptionsView.AllowCellMerge = false;
            gvAttachFile.HorzScrollVisibility = ScrollVisibility.Auto;
            gvAttachFile.OptionsView.ColumnAutoWidth = false;

          //  gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            gvAttachFile.OptionsSelection.MultiSelect = true;
            gvAttachFile.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            gvAttachFile.OptionsNavigation.AutoFocusNewRow = true;
            gvAttachFile.OptionsNavigation.UseTabKey = true;

            gvAttachFile.FocusRectStyle = DrawFocusRectStyle.CellFocus;
            gvAttachFile.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvAttachFile.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvAttachFile.OptionsView.EnableAppearanceEvenRow = true;
            gvAttachFile.OptionsView.EnableAppearanceOddRow = true;

            gvAttachFile.OptionsView.ShowFooter = false;

            //   gridView1.RowHeight = 25;

            gvAttachFile.OptionsFind.AllowFindPanel = true;
            //gridView1.OptionsFind.AlwaysVisible = true;//==>false==>gridView1.OptionsFind.ShowCloseButton = true;
            gvAttachFile.OptionsFind.AlwaysVisible = false;
            gvAttachFile.OptionsFind.ShowCloseButton = true;
            gvAttachFile.OptionsFind.HighlightFindResults = true;
            new MyFindPanelFilterHelper(gvAttachFile)
            {
                IsPerFormEvent = true,
            };

            var gridColumn1 = new GridColumn();
            gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            gridColumn1.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn1.Caption = @"File Name";
            gridColumn1.FieldName = "FileName";
            gridColumn1.Name = "FileName";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            gridColumn1.Fixed = FixedStyle.Left;
            gvAttachFile.Columns.Add(gridColumn1);

            
            var gridColumn2 = new GridColumn();
            gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            gridColumn2.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridColumn2.Caption = @"File Size";
            gridColumn2.FieldName = "FileSize";
            gridColumn2.Name = "FileSize";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            gridColumn2.Fixed = FixedStyle.Left;
            gvAttachFile.Columns.Add(gridColumn2);

            
            var gridColumn3 = new GridColumn();
            gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            gridColumn3.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridColumn3.Caption = @"Path";
            gridColumn3.FieldName = "Path";
            gridColumn3.Name = "Path";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex =2;
            gridColumn3.Fixed = FixedStyle.None;
            gvAttachFile.Columns.Add(gridColumn3);

            gvAttachFile.RowStyle += gvAttachFile_RowStyle;
            gvAttachFile.CustomDrawCell += gvAttachFile_CustomDrawCell;
            gvAttachFile.RowCountChanged += gvAttachFile_RowCountChanged;
            gvAttachFile.CustomDrawRowIndicator += gvAttachFile_CustomDrawRowIndicator;
            gcAttachFile.DataSource = SetTableTemplateGirdView();
            gcAttachFile.ForceInitialize();
          
        }


        private void gvAttachFile_RowCountChanged(object sender, EventArgs e)
        {
            var gv = sender as GridView;
            if (!gv.GridControl.IsHandleCreated) return;
            Graphics gr = Graphics.FromHwnd(gv.GridControl.Handle);
            SizeF size = gr.MeasureString(gv.RowCount.ToString(), gv.PaintAppearance.Row.GetFont());
            gv.IndicatorWidth = Convert.ToInt32(size.Width) + 10;
            //GridPainter.Indicator.ImageSize.Width 
        }

        private void gvAttachFile_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            var gv = sender as GridView;
            if (!e.Info.IsRowIndicator) return;
            if (!gv.IsDataRow(e.RowHandle)) return;
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
            e.Info.ImageIndex = -1;
        }
        private void gvAttachFile_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.VisibleIndex == 0)
            {
                Image icon = Resources.Upload_Information_icon;
                e.Graphics.DrawImage(icon, new Rectangle(e.Bounds.X, e.Bounds.Y, 17, 17));
                e.Appearance.DrawString(e.Cache, e.DisplayText, new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height));
                e.Handled = true;
            }
        }

        private void gvAttachFile_RowStyle(object sender, RowStyleEventArgs e)
        {
            var gv = sender as GridView;
            if (gv.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Assign(gv.PaintAppearance.SelectedRow);
                e.HighPriority = true;
                e.Appearance.BackColor = Color.FromArgb(169, 249, 108);
                e.Appearance.BackColor2 = Color.FromArgb(246, 248, 247);
                e.Appearance.GradientMode = LinearGradientMode.Horizontal;


            }


        }

        #endregion


        #region "Get Attach File"

  

        /// <summary>
        ///     Selected File Add File Information Into GridView
        /// </summary>
        private void AddFileSelectedToGridView()
        {
            var openFile = new OpenFileDialog() 
             { 
                Title = @"Select File",
                RestoreDirectory = true, 
                Multiselect = true 
            };
            if (openFile.ShowDialog() != DialogResult.OK) return;
            ((DataTable)gcAttachFile.DataSource).AcceptChanges();
            var dtFile = gcAttachFile.DataSource as DataTable;
            foreach (string s in openFile.FileNames)
            {
                if (!ProcessGeneral.CheckFileSelected(dtFile, s))
                {
                    var fi = new FileInfo(s);
                    gvAttachFile.AddNewRow();
                    int newRowHandle = gvAttachFile.FocusedRowHandle;
                    gvAttachFile.SetRowCellValue(newRowHandle, gvAttachFile.Columns["FileName"], fi.Name);
                    gvAttachFile.SetRowCellValue(newRowHandle, gvAttachFile.Columns["FileSize"], ProcessGeneral.ConvertFileSizeToString(fi.Length));
                    gvAttachFile.SetRowCellValue(newRowHandle, gvAttachFile.Columns["Path"], s);
                    gvAttachFile.UpdateCurrentRow();
                }
            }
            BestFitColumnsGv();
        }

        #endregion

        #region "Check Control Input"

        /// <summary>
        ///     Check Values Input Empty String
        /// </summary>
        /// <returns>
        ///     A bool value...
        /// </returns>
        private bool CheckValuesInput()
        {
            if (ProcessGeneral.GetSafeString(txtEmailLogin.EditValue) == string.Empty || ProcessGeneral.GetSafeString(txtPassLogin.EditValue) == string.Empty ||
                ProcessGeneral.GetSafeString(txtMailFrom.EditValue) == string.Empty || ProcessGeneral.GetSafeString(tkMailTo.EditValue) == string.Empty)
                return false;
            return true;
        }

        #endregion

        #region "Button Click Event"
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddFileSelectedToGridView();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            gvAttachFile.DeleteSelectedRows();
            ((DataTable)gcAttachFile.DataSource).AcceptChanges();
            BestFitColumnsGv();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
             string contentSend = "";
            if (!CheckValuesInput())
            {
                XtraMessageBox.Show("You enter not enough to the information input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return;
            }
            contentSend = this.sUseContentHTML ? txtContent.GSHTMLText : txtContent.GSText;
            int port = ProcessGeneral.GetSafeInt(txtPort.EditValue);
            if (sendMailarg != null)
            {
                
                bool result;


                if (port == 465)
                {
                    result = ProcessGeneral.SendMail465(ProcessGeneral.GetSafeString(txtMailServer.EditValue), ProcessGeneral.GetSafeString(txtEmailLogin.EditValue),
                        ProcessGeneral.GetSafeString(txtPassLogin.EditValue), 
                        ProcessGeneral.GetSafeString(txtMailFrom.EditValue), ProcessGeneral.GetSafeString(tkMailTo.EditValue), contentSend,
                        ProcessGeneral.GetSafeString(txtSubject.EditValue), (DataTable)gcAttachFile.DataSource, true, _delimiter);
                }
                else
                {
                    result = ProcessGeneral.SendMail(ProcessGeneral.GetSafeString(txtMailServer.EditValue), ProcessGeneral.GetSafeString(txtEmailLogin.EditValue),
                        ProcessGeneral.GetSafeString(txtPassLogin.EditValue), port, chkEnabeSSL.Checked,
                        ProcessGeneral.GetSafeString(txtMailFrom.EditValue), ProcessGeneral.GetSafeString(tkMailTo.EditValue), contentSend,
                        ProcessGeneral.GetSafeString(txtSubject.EditValue), (DataTable)gcAttachFile.DataSource, true, _delimiter);
                }



                sendMailarg(this, new SendMailEventArgs
                {
                    sendMailresult = result,
                });

                if (result)
                {
                    this.Close();
                }
                else
                {
                    DialogResult kq = XtraMessageBox.Show("Delivery to the following recipient failed permanently \n Do you want to send again this email??", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    if (kq == DialogResult.No)
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                if (port == 465)
                {
                    ProcessGeneral.SendMail465(ProcessGeneral.GetSafeString(txtMailServer.EditValue), ProcessGeneral.GetSafeString(txtEmailLogin.EditValue),
                        ProcessGeneral.GetSafeString(txtPassLogin.EditValue), 
                        ProcessGeneral.GetSafeString(txtMailFrom.EditValue), ProcessGeneral.GetSafeString(tkMailTo.EditValue), contentSend,
                        ProcessGeneral.GetSafeString(txtSubject.EditValue), (DataTable)gcAttachFile.DataSource, true, _delimiter);
                }
                else
                {
                    ProcessGeneral.SendMail(ProcessGeneral.GetSafeString(txtMailServer.EditValue), ProcessGeneral.GetSafeString(txtEmailLogin.EditValue),
                        ProcessGeneral.GetSafeString(txtPassLogin.EditValue), port, chkEnabeSSL.Checked,
                        ProcessGeneral.GetSafeString(txtMailFrom.EditValue), ProcessGeneral.GetSafeString(tkMailTo.EditValue), contentSend,
                        ProcessGeneral.GetSafeString(txtSubject.EditValue), (DataTable)gcAttachFile.DataSource, true, _delimiter);
                }

            }

        }
        #endregion


    
        #region "textbox event"


        private void tkMailTo_SizeChanged(object sender, EventArgs e)
        {
            Size newSize = ((TokenEdit)sender).Size;
            pcLabelTo.Height = newSize.Height + 4;
            pcMailTo.Height = newSize.Height + 4;
        }

        private void tkMailTo_ValidateToken(object sender, TokenEditValidateTokenEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Description)) return;
            e.IsValid = ProcessGeneral.CheckFormatEmailAddress(e.Description);
        }
       

        private void txtSubject_TextChanged(object sender, EventArgs e)
        {
            this.Text = string.Format("Send Mail {0}", txtSubject.EditValue);
            if (ProcessGeneral.GetSafeString(txtSubject.EditValue) != string.Empty)
            {

                ProcessGeneral.SetTooltipControl(txtSubject, ProcessGeneral.GetSafeString(txtSubject.EditValue), "Mail Subject", ProcessGeneral.GetImageList(), 0, new Size(16, 16), DefaultBoolean.True, true, true);

            }
          
        }

        

        private void txtSubject_MouseMove(object sender, MouseEventArgs e)
        {
            if (ProcessGeneral.GetSafeString(txtSubject.EditValue) != string.Empty)
            {
                ToolTipController.DefaultController.ShowHint(ToolTipController.DefaultController.GetToolTip(txtSubject), ToolTipController.DefaultController.GetTitle(txtSubject), txtSubject.PointToScreen(e.Location));
            }
        }

        private void txtEmailLogin_Validating(object sender, CancelEventArgs e)
        {
            if (!ProcessGeneral.CheckFormatEmailAddress(ProcessGeneral.GetSafeString(txtEmailLogin.EditValue)))
            {
                e.Cancel = true;
            }
        }

        private void txtMailFrom_Validating(object sender, CancelEventArgs e)
        {
            if (!ProcessGeneral.CheckFormatEmailAddress(ProcessGeneral.GetSafeString(txtMailFrom.EditValue)))
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region "hotkey"
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {

                case Keys.Alt | Keys.A:
                    {

                        btnAdd_Click(null,null);
                        return true;
                    }
                case Keys.Alt | Keys.R:
                    {

                       btnRemove_Click(null,null);
                        return true;
                    }
                case Keys.Alt | Keys.S:
                    {
                        btnSend_Click(null,null);
                        return true;
                    }

            }
            return base.ProcessCmdKey(ref message, keys);
        }

        #endregion
    }
}