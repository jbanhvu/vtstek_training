using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using CNY_BaseSys.Common;

namespace CNY_BaseSys.WForm
{
    public partial class FrmConfirmCaptcha : DevExpress.XtraEditors.XtraForm
    {
         #region "Property"



        private readonly Int32 _lenghthText;
        private readonly bool _isNumberText;
        private string _value = "";
        public event OnConfirmCaptchaHandler OnConfirmCaptcha = null;
        private readonly String _fontName;
        private const string StrErrorCode = "Type Mismatch";
        #endregion

        #region "Contructor"

        public FrmConfirmCaptcha(Int32 lenghthText, bool isNumberText, string fontName = "")
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this._lenghthText = lenghthText;
            this._isNumberText = isNumberText;
            this._fontName = fontName;
            this.Load += Frm_Load;
            btnRefresh.Click += btnRefresh_Click;
            btnOK.Click += btnOK_Click;
            ProcessGeneral.SetTooltipControl(txtCode, StrErrorCode, "Error", ProcessGeneral.GetImageList(), 0, new Size(16, 16), DefaultBoolean.True, true, true);
            txtCode.EditValueChanged += txtCode_EditValueChanged;
            txtCode.Leave += txtCode_Leave;
            txtCode.MouseMove += txtCode_MouseMove;
            txtCode.KeyDown += txtCode_KeyDown;
            this.FormClosing += FrmConfirmCaptcha_FormClosing;
            
          
        }

     

        private void FrmConfirmCaptcha_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtCode.Text.Trim() != _value.Trim())
            {
                dXErrorMain.SetError(txtCode, StrErrorCode, ErrorType.Critical);
                e.Cancel = true;
            }
        }






        private void Frm_Load(object sender, EventArgs e)
        {
            this.CloseBox = false;
            LoadCaptCha();
            txtCode.Select();
        }
        #endregion


        #region "TextBox Event"
        private void txtCode_MouseMove(object sender, MouseEventArgs e)
        {
            var tE = sender as TextEdit;
            if (tE == null) return;
            if (tE.Text.Trim() != _value.Trim())
            {
                ToolTipController.DefaultController.ShowHint(ToolTipController.DefaultController.GetToolTip(tE), ToolTipController.DefaultController.GetTitle(tE),
                    tE.PointToScreen(e.Location));

            }
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            var tE = sender as TextEdit;
            if (tE == null) return;
            if (tE.Text.Trim() != _value.Trim())
            {
                dXErrorMain.SetError(tE, StrErrorCode, ErrorType.Critical);
            }
            else
            {
                dXErrorMain.SetError(tE, null);
            }
        }
        private void txtCode_EditValueChanged(object sender, EventArgs e)
        {
            var tE = sender as TextEdit;
            if (tE == null) return;
            if (tE.Text.Trim() != _value.Trim())
            {
                dXErrorMain.SetError(tE, StrErrorCode, ErrorType.Critical);
            }
            else
            {
                dXErrorMain.SetError(tE, null);
            }

        }


        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(sender, e);
            }
        }
        #endregion

        #region "Methold"

        private void LoadCaptCha()
        {
            //"Times New Roman"

            CaptchaImage ci;
            if (string.IsNullOrEmpty(_fontName))
            {
                ci = new CaptchaImage(_lenghthText, _isNumberText, picCaptcha.Width, picCaptcha.Height);
            }
            else
            {
                ci = new CaptchaImage(_lenghthText, _isNumberText, picCaptcha.Width, picCaptcha.Height, _fontName);
            }

           
            picCaptcha.Image = ci.Image;
            _value = ci.CaptchaText;
        }
        #endregion

        #region "Button Click Event"

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim() != _value.Trim())
            {
                dXErrorMain.SetError(txtCode, StrErrorCode, ErrorType.Critical);
                return;
            }
            dXErrorMain.SetError(txtCode, null);
            if (OnConfirmCaptcha == null) return;
            OnConfirmCaptcha(this, new OnConfirmCaptchaEventArgs
            {
                CaptchaText = _value,
            });
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
            LoadCaptCha();
            if (txtCode.Text.Trim() != _value.Trim())
            {
                dXErrorMain.SetError(txtCode, StrErrorCode, ErrorType.Critical);
            }
            else
            {
                dXErrorMain.SetError(txtCode, null);
            }
        }

        #endregion
    }
}