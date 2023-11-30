using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;

namespace CNY_BaseSys.WForm
{
    public partial class FrmCustomDialog : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"



        public string CommandPromt
        {
            get { return this.lblInput.Text; }
            set { this.lblInput.Text = value; }
        }


        


        public event OnGetValueHandler OnGetValue = null;
        #endregion

        #region "Contructor"

        public FrmCustomDialog()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Load += FrmInput_Load;
            btnClose.Click += btn_Click;
            btnOK.Click += btn_Click;

        }


      



        private void FrmInput_Load(object sender, EventArgs e)
        {
            int tag = ProcessGeneral.GetSafeInt(this.Tag);
            if (tag == 0)
            {
                
                btnOK.Focus();
                btnOK.Select();
            }
            else
            {
              
                btnClose.Focus();
                btnClose.Select();
            }
        }
        #endregion

        #region "Button Click Event"



        private void ProcessEvent(string btnName)
        {
           
        


            if (OnGetValue != null)
            {
                ActionDialog actionDialog = btnOK.Name == btnName ? ActionDialog.Yes : ActionDialog.No;
                OnGetValue(this, new OnGetValueEventArgs
                {
                    DialogResult = actionDialog,
                });
            }
            this.Close();
        }

        private void btn_Click(object sender, EventArgs e)
        {

            SimpleButton button = sender as SimpleButton;
            if (button == null) return;
            ProcessEvent(button.Name);
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {

                case Keys.Enter:
                {
                    if (btnOK.Focused)
                    {
                        ProcessEvent(btnOK.Name);
                    }
                    else
                    {
                        ProcessEvent(btnClose.Name);
                        }

                    return true;
                }
                case Keys.Escape:
                {

                    this.Close();
                    return true;
                }

            }
            return base.ProcessCmdKey(ref message, keys);



        }

        #endregion
    }
}