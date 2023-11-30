using System;
using System.Windows.Forms;
using DevExpress.XtraEditors.Mask;
using CNY_BaseSys.Common;

namespace CNY_BaseSys.WForm
{
    public partial class FrmInput : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"
      
     

        public string CommandPromt
        {
            get { return this.lblInput.Text; }
            set { this.lblInput.Text = value; }
        }

        public string GetText
        {
            get { return this.txtInput.Text; }
        }
        public object GetValue
        {
            get { return this.txtInput.EditValue; }
        }

        public Int32 SetWidthLable
        {
            set { this.pcTopAfterLeft.Width = value; }
        }

        private MaskType _maskTypeInput = MaskType.None;
        public MaskType MaskTypeInput
        {
            get { return this._maskTypeInput; }
            set { this._maskTypeInput = value; }
        }
        private string _maskEdit = "";
        public string MaskEdit
        {
            get { return this._maskEdit; }
            set { this._maskEdit = value; }
        }
        
        public object DefaultValue { get; set; }

        private bool _useSystemPasswordChar = false;
        public bool UseSystemPasswordChar
        {
            get { return this._useSystemPasswordChar; }
            set { this._useSystemPasswordChar = value; }
        }
        private CharacterCasing _characterCase = CharacterCasing.Normal;
        public CharacterCasing CharacterCase
        {
            get { return this._characterCase; }
            set { this._characterCase = value; }
        }
        public event OnGetValueHandler OnGetValue = null;
        #endregion

        #region "Contructor"
   
        public FrmInput()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Load += FrmInput_Load;
            btnClose.Click += btnClose_Click;
            btnOK.Click += btnOK_Click;
          
        }



     


        private void FrmInput_Load(object sender, EventArgs e)
        {
            txtInput.EditValue = DefaultValue;
            txtInput.Properties.UseSystemPasswordChar = _useSystemPasswordChar;
            if (_maskTypeInput != MaskType.None)
            {
                txtInput.Properties.Mask.MaskType = _maskTypeInput;
                if (!string.IsNullOrEmpty(_maskEdit))
                {
                    txtInput.Properties.Mask.EditMask = _maskEdit;
                }
                txtInput.Properties.Mask.UseMaskAsDisplayFormat = true;
            }
            txtInput.Properties.CharacterCasing = _characterCase;
            txtInput.Focus();
        }
        #endregion

        #region "Button Click Event"

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (OnGetValue != null)
            {
                OnGetValue(this, new OnGetValueEventArgs
                {
                    Value = txtInput.EditValue,
                    Text = txtInput.Text.Trim(),
                });
            }
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        #endregion
    }



}