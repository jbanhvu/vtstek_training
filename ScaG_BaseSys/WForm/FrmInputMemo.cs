using System;
using System.Windows.Forms;
using CNY_BaseSys.Common;

namespace CNY_BaseSys.WForm
{
    public partial class FrmInputMemo : DevExpress.XtraEditors.XtraForm
    {

        #region "Property"
        public event OnInputValueMemoHandler OnInputValue = null;
        private readonly string _data = "";
        private bool _isSave = true;
        #endregion

        #region "Contructor"
        public FrmInputMemo(string data)
        {
            InitializeComponent();
            this._data = data;
            if (!string.IsNullOrEmpty(data))
            {
                memoMain.EditValue = data;
            }

            btnNextFinish.Click += BtnNextFinish_Click;
            this.Load += FrmInputMemo_Load;
            this.Closed += FrmInputMemo_Closed;

        }

        private void FrmInputMemo_Closed(object sender, EventArgs e)
        {
            string value = ProcessGeneral.GetSafeString(memoMain.EditValue);
            bool isSave = true;
            if (!_isSave)
            {
                isSave = false;
            }
            else
            {
                if (value != _data)
                {
                    isSave = true;
                }
                else
                {
                    isSave = false;
                }
            }
            if (OnInputValue != null)
            {

                OnInputValue(this, new OnInputValueMemoEventArgs
                {
                    Value = value,
                    IsSave = isSave
                });
            }
        }

        private void FrmInputMemo_Load(object sender, EventArgs e)
        {

            memoMain.SelectNextControl(ActiveControl, true, true, true, true);
            memoMain.Focus();
            if (!string.IsNullOrEmpty(memoMain.Text))
            {
                memoMain.Select(memoMain.Text.Length, 0);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const UInt32 WM_NCACTIVATE = 0x0086;

            if (m.Msg == WM_NCACTIVATE && m.WParam.ToInt32() == 0)
            {
                this.Close();
            }

            base.WndProc(ref m);
        }



        #endregion


        #region  "Button Click"


        private void BtnNextFinish_Click(object sender, EventArgs e)
        {



            if (!btnNextFinish.Focused)
            {
              
                btnNextFinish.SelectNextControl(ActiveControl, true, true, true, true);
                btnNextFinish.Focus();
                

            }



          
            this.Close();
        }

        #endregion

    

        #region "hotkey"
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Escape:
                    {
                        _isSave = false;
                        this.Close();
                        return true;
                    }
                case Keys.Control | Keys.S:
                    {
                        BtnNextFinish_Click(null, null);
                        return true;
                    }


            }
            return base.ProcessCmdKey(ref message, keys);
        }

        #endregion

    }
}
