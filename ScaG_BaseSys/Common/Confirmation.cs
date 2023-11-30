using CNY_BaseSys.Class;
using CNY_BaseSys.Info;
using CNY_BaseSys.WForm;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNY_BaseSys.Common
{
    public class Confirmation
    {
        List<Tuple<LabelControl, SimpleButton, SimpleButton>> _listControlApprove = new List<Tuple<LabelControl, SimpleButton, SimpleButton>>();
        Inf_ApprovalHistory _inf_approval = new Inf_ApprovalHistory();
        DataTable _dtLevelFunctionApprove = new DataTable();
        PermissionFormInfo _qPer;
        string _menuCode;
        int _status;
        bool _isRejected = false;
        int numberOfPerson;
        Int32 _functionInApprovalPK;
        int _signPerRow; 
        GroupControl _groupControl;
        Int64 _pK;
        public bool needSave
        {
            get; set;
        }

        public Confirmation(Int32 functionInApprovalPK,Int64 PK ,int signPerRow, GroupControl groupControl, PermissionFormInfo qPer, string menuCode)
        {
            _functionInApprovalPK = functionInApprovalPK;
            _signPerRow = signPerRow;
            _groupControl = groupControl;
            _pK = PK;
            _qPer = qPer;
            _menuCode = menuCode;
            Inf_ApprovalHistory _inf_approval = new Inf_ApprovalHistory();
            needSave = false;
        }
        public void GenerateControlConfirm()
        {
            _dtLevelFunctionApprove = _inf_approval.sp_LevelFunctionApproval_SelectByFunctionInApprovalPK(_functionInApprovalPK);
            int numberOfPerson = _dtLevelFunctionApprove.Rows.Count;
            int totalHorizontal = GetSignHorizontalIndex(numberOfPerson);
            int personInLastHorizontal = GetSignVerticalIndex(numberOfPerson);
            if (personInLastHorizontal == _signPerRow)//Dong cuối có đủ người ký
            {
                GenerateControlConfirmDetail(_dtLevelFunctionApprove, _signPerRow, 0, _dtLevelFunctionApprove.Rows.Count - 1, _groupControl);
            }
            else
            {
                GenerateControlConfirmDetail(_dtLevelFunctionApprove, _signPerRow, 0, _signPerRow * (totalHorizontal - 1) - 1, _groupControl);
                GenerateControlConfirmDetail(_dtLevelFunctionApprove, personInLastHorizontal, _signPerRow * (totalHorizontal - 1), _dtLevelFunctionApprove.Rows.Count - 1, _groupControl);
            }
            LoadListControApprove(_dtLevelFunctionApprove, _groupControl);
        }

        public int GetSignHorizontalIndex(int rowPossition)
        {
            if (rowPossition % _signPerRow == 0)
            {
                return rowPossition / _signPerRow;
            }
            return rowPossition / _signPerRow + 1;
        }
        public  int GetSignVerticalIndex(int rowPossition)
        {
            if (rowPossition % _signPerRow == 0)
            {
                return _signPerRow;
            }
            return rowPossition % _signPerRow;
        }
        public  LabelControl GetLable(string LblName, GroupControl _groupControl)
        {
            foreach (Control control in _groupControl.Controls)
            {
                if (control is LabelControl button && button.Name == LblName)
                {
                    return button;
                }
            }

            return null;
        }

        public  SimpleButton GetSimpleButton(string LblName, GroupControl _groupControl)
        {
            foreach (Control control in _groupControl.Controls)
            {
                if (control is SimpleButton button && button.Name == LblName)
                {
                    return button;
                }
            }

            return null;
        }
        public  void GenerateControlConfirmDetail(DataTable dt, int __signPerRow, int fromRow, int toRow, GroupControl _groupControl)
        {
            int formWidth = 1200;
            for (int i = fromRow; i <= toRow; i++)
            {

                int Level = i + 1;
                int signHorizontalIndex = GetSignHorizontalIndex(Level);
                int signVerticalIndex = GetSignVerticalIndex(Level);
                int centerOfSign = (formWidth / (__signPerRow + 1)) * signVerticalIndex;
                Point _pointOfTitle = new Point(centerOfSign - 120, (signHorizontalIndex - 1) * 70 + 30);
                Point _pointOfSign = new Point(centerOfSign - 120, (signHorizontalIndex - 1) * 70 + 45);
                Point _pointOfRejectButton = new Point(centerOfSign - 48, (signHorizontalIndex - 1) * 70 + 45);
                CreatelblTitle(_pointOfTitle, dt.Rows[i]["note"].ToString(), _groupControl);
                CreatelblNotice(_pointOfSign, "lblNotice" + dt.Rows[i]["Level"].ToString(), _groupControl);
                CreatebtnConfirm(_pointOfSign, "btnConfirm" + dt.Rows[i]["Level"].ToString(), _groupControl);
                CreatebtnReject(_pointOfRejectButton, "btnReject" + dt.Rows[i]["Level"].ToString(), _groupControl);
            }
        }
        public  void CreatelblTitle(Point pointOfSign, string text, GroupControl _groupControl)
        {
            LabelControl lblTitle = new LabelControl();
            lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold);
            lblTitle.Appearance.Options.UseFont = true;
            lblTitle.Appearance.Options.UseTextOptions = true;
            lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            lblTitle.AutoSizeMode = LabelAutoSizeMode.None;
            lblTitle.Location = pointOfSign;
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(150, 15);
            lblTitle.Text = text;
            _groupControl.Controls.Add(lblTitle);
        }
        public  void CreatelblNotice(Point pointOfSign, string text, GroupControl _groupControl)
        {
            LabelControl lblNotice = new LabelControl();
            lblNotice.Appearance.Options.UseTextOptions = true;
            lblNotice.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblNotice.AutoSizeMode = LabelAutoSizeMode.None;
            lblNotice.Location = pointOfSign;
            lblNotice.Name = text;
            lblNotice.Size = new System.Drawing.Size(150, 45);
            lblNotice.Text = text;
            _groupControl.Controls.Add(lblNotice);
        }
        public  void CreatebtnConfirm(Point pointOfSign, string textName, GroupControl _groupControl)
        {
            SimpleButton btnConfirm = new SimpleButton();
            btnConfirm.ImageOptions.Image = Properties.Resources.apply_16x16;
            btnConfirm.Location = pointOfSign;
            btnConfirm.Name = textName;
            btnConfirm.Size = new Size(70, 25);
            btnConfirm.Text = "Xác nhận";
            btnConfirm.Click += BtnConfirm_Click;

            _groupControl.Controls.Add(btnConfirm);
        }
        public  void CreatebtnReject(Point pointOfSign, string textName, GroupControl _groupControl)
        {
            SimpleButton btnReject = new SimpleButton();
            btnReject.ImageOptions.Image = Properties.Resources.cancel_16x16;
            btnReject.Location = pointOfSign;
            btnReject.Name = textName;
            btnReject.Size = new Size(70, 25);
            btnReject.Text = "Từ chối";
            btnReject.Click += BtnConfirm_Click;
            _groupControl.Controls.Add(btnReject);

        }
        public  void BtnConfirm_Click(object sender, EventArgs e)
        {
            if(needSave)
            {
                XtraMessageBox.Show("Phiếu đã được cập nhật phải lưu lại trước khi xác nhận", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SimpleButton btn = sender as SimpleButton;
            int action = btn.Name.Substring(3, 7) == "Confirm" ? 1 : 0;
            string stringAction = btn.Name.Substring(3, 7);
            int level = ProcessGeneral.GetSafeInt(ProcessGeneral.RightString(btn.Name, 1));
            if (action == 1)
            {
                DialogResult dlResult = XtraMessageBox.Show("Xác nhận duyệt Phiếu yêu cầu vật tư số: " + _pK.ToString(), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dlResult == DialogResult.Yes)
                {
                    UpdateStatus(action, level, "");
                }
            }
            else
            {
                string _reason = "";
                using (var f2 = new FrmInput())
                {
                    f2.Text = @"Lý do từ chối";
                    f2.Size = new Size(340, f2.Height);
                    f2.CommandPromt = "Nhập lý do để xác nhận :";
                    f2.SetWidthLable = 160;
                    f2.MaskEdit = FunctionFormatModule.StrFormatFactorDecimal(true, false);
                    f2.UseSystemPasswordChar = false;
                    f2.CharacterCase = CharacterCasing.Normal;
                    f2.OnGetValue += (s, f) =>
                    {
                        _reason = ProcessGeneral.GetSafeString(f.Value);
                        UpdateStatus(action, level, _reason);
                    };

                    f2.ShowDialog();
                }
            }

        }

        #region LoadListControApprove
        public  void LoadListControApprove(DataTable dtLevelFunctionApprove, GroupControl _groupControl)
        {
            foreach (DataRow dr in dtLevelFunctionApprove.Rows)
            {
                Tuple<LabelControl, SimpleButton, SimpleButton> newItem =
                 new Tuple<LabelControl, SimpleButton, SimpleButton>(GetLable("lblNotice" + dr["Level"].ToString(),_groupControl), 
                 GetSimpleButton("btnConfirm" + dr["Level"].ToString(), _groupControl), 
                 GetSimpleButton("btnReject" + dr["Level"].ToString(), _groupControl));

                _listControlApprove.Add(newItem);

            }
        }
        #endregion

        #region RemoveControlConfirm
        public void RemoveControlConfirm()
        {
            _groupControl.Controls.Clear();
            _listControlApprove.Clear();
        }
        #endregion
        #region Generate Event Button Click

        public void UpdateStatus(Int32 Status, Int32 _currentLevel, string Note)
        {
            Cls_ApprovalHistory cls = new Cls_ApprovalHistory();
            cls.FunctionInApprovalPK = _functionInApprovalPK;
            cls.Level = _currentLevel;
            cls.ItemPKInFunction = _pK;
            cls.UserName = DeclareSystem.SysUserName;
            cls.Status = Status;
            cls.ApprovedDate = DateTime.Now;
            cls.Note = Note;
            DataTable dt = _inf_approval.sp_ApprovalHistory_Update(cls);
            if (dt.Rows.Count > 0)
            {
                DisplayConfirm();
            }
            //Cập nhật trạng thái
            int _status = 2;
            if (Status == 0)
            {
                _status = -1;
            }
            else
            {
                if (_currentLevel == numberOfPerson)
                    _status = 1;
            }
            //dt = _inf004.sp_MaterialRequirement_UpdateStatus(_pk, _status);

            //Gửi mail 
            if (Status == 1)// Gửi mail yêu cầu duyệt ước tiếp theo cho cấp cao hơn
            {
                //Lấy email cấp cao hơn
                dt = _inf_approval.sp_LevelFunctionApproval_GetUserApproval(_functionInApprovalPK, _menuCode, _currentLevel);
                foreach (DataRow dr in dt.Rows)
                {
                    //SendMailRequest(ProcessGeneral.GetSafeString(dr["Email"]));
                }
            }
            // Gửi mail thống báo tình trạng cho các cấp thấp hơn
            //Lấy email cấp thấp hơn

            dt = _inf_approval.sp_ApprovalHistory_GetUserApproved(_functionInApprovalPK, _pK);
            foreach (DataRow dr in dt.Rows)
            {
                //SendMailNotice(ProcessGeneral.GetSafeString(dr["Email"]), Status);
            }
            //DisplayDataForEditing();
        }
        #endregion

        #region CheckRight
        public void DisplayConfirmQuotationAdding()
        {
            foreach (Control ctr in _groupControl.Controls)
            {
                if (ctr is SimpleButton button && button.Name.Substring(0, 3) == "btn")
                {
                    ctr.Visible = false;
                }
            }
            foreach (Control ctr in _groupControl.Controls)
            {
                if (ctr is LabelControl button && button.Name.Substring(0, 4) == "lblN")
                {
                    ctr.Visible = false;
                }

            }
        }
        public void DisplayConfirm()
        {
            CheckStatus();
            CheckRight();
        }
        void CheckRight()
        {
            foreach (DataRow dr in _dtLevelFunctionApprove.Rows)
            {
                string Permission = dr["SystemRightCode"].ToString();
                if (!_qPer.StrSpecialFunction.Contains(Permission))
                {
                    SimpleButton btnConfirm = GetSimpleButton("btnConfirm" + dr["Level"].ToString());
                    if (btnConfirm != null)
                        btnConfirm.Visible = false;
                    SimpleButton btnReject = GetSimpleButton("btnReject" + dr["Level"].ToString());
                    if (btnReject != null)
                        btnReject.Visible = false;
                }
            }
        }
        void CheckStatus()
        {
            DataTable dtApproveHistory = _inf_approval.sp_ApprovalHistory_Select(_functionInApprovalPK, _pK);
            CheckApproveDetail(dtApproveHistory, 1);
            CheckApproveDetail(dtApproveHistory, 2);
            CheckApproveDetail(dtApproveHistory, 3);
            CheckApproveDetail(dtApproveHistory, 4);
            CheckApproveDetail(dtApproveHistory, 5);
            CheckApproveDetail(dtApproveHistory, 6);
            CheckApproveDetail(dtApproveHistory, 7);
            CheckApproveDetail(dtApproveHistory, 8);
            DisplayControlConfirm(_listControlApprove, _listControlApprove.Count, _status, _isRejected);
        }
        void CheckApproveDetail(DataTable dtApproveHistory, Int32 Level)
        {

            // Kiểm tra xem đã có những user nào approve bằng cách giá trị có tồn tại trong bảng lịch sử phê duyệt hay không bằng LINQ
            bool valueExists = dtApproveHistory.AsEnumerable().Any(row => row.Field<int>("Level") == Level);
            if (valueExists)
            {
                DataRow rowWithCondition = dtApproveHistory.AsEnumerable().FirstOrDefault(row => row.Field<int>("Level") == Level);
                _status = Level;
                DateTime ApprovedDate = ProcessGeneral.GetSafeDatetimeNullable(rowWithCondition["ApprovedDate"]);
                string ApprovedUse = ProcessGeneral.GetSafeString(rowWithCondition["UserName"]);
                int ActionID = ProcessGeneral.GetSafeInt(rowWithCondition["Status"]);
                string Note = ProcessGeneral.GetSafeString(rowWithCondition["Note"]);
                GetTextForLabelConfirm(_listControlApprove, Level, ActionID, ApprovedUse, ApprovedDate, Note);
            }

        }
        private void GetTextForLabelConfirm(List<Tuple<LabelControl, SimpleButton, SimpleButton>> _listControl, int Status, int ActionID, string userName, DateTime dateTime, string Note)
        {
            string Action = ActionID == 1 ? "Confirmed" : "Rejected";
            if (ActionID == 0)
            {
                _isRejected = true;
                _listControl[Status - 1].Item1.Appearance.ForeColor = Color.Red;
                _listControl[Status - 1].Item1.Appearance.Font = new System.Drawing.Font(_listControl[Status - 1].Item1.Appearance.Font, System.Drawing.FontStyle.Bold);
            }
            _listControl[Status - 1].Item1.Text = Action + " by " + userName + "\n at " + dateTime.ToString("dd/MM/yyyy hh:mm:ss") + "\n " + Note
                ;

        }
        #region DisplayControlConfirm
        private void DisplayControlConfirm(List<Tuple<LabelControl, SimpleButton, SimpleButton>> _listControl, int numberOfConfirm, int Status, bool Approved = false)
        {
            for (int controlIndex = 0; controlIndex < numberOfConfirm; controlIndex++)
            {
                if (Status > controlIndex)//Nếu trạng thái lớn hơn vị trí của control thì Label hiển thị thông báo hiện lên
                {
                    _listControl[controlIndex].Item1.Visible = true;
                }

                else
                {
                    _listControl[controlIndex].Item1.Visible = false;
                }

                if (Status == controlIndex && !_isRejected)//Nếu trạng thái bằng vị trí của control và không bị reject thì Button hiển thị nút xác nhận lên
                {
                    _listControl[controlIndex].Item2.Visible = true;
                    _listControl[controlIndex].Item3.Visible = true;
                }
                else
                {
                    _listControl[controlIndex].Item2.Visible = false;
                    _listControl[controlIndex].Item3.Visible = false;
                }
            }
        }
        #endregion
        private SimpleButton GetSimpleButton(string LblName)
        {
            foreach (Control control in _groupControl.Controls)
            {
                if (control is SimpleButton button && button.Name == LblName)
                {
                    return button;
                }
            }

            return null;
        }
        #endregion 
    }
}
