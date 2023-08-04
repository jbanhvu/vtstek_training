using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CNY_BaseSys.Common;

namespace CNY_BaseSys.UControl
{
    public partial class XtraUCPanelControlNumberBetween : DevExpress.XtraEditors.XtraUserControl
    {
        #region "Property"
        public TextEdit txtNumberValueStartP
        {
            get
            {
                return txtNumberValueStart;
            }

        }
        public TextEdit txtNumberValueEndP
        {
            get
            {
                return txtNumberValueEnd;
            }

        }

        public int SetTabindextxtNumberValueStartP
        {
            set
            {
                txtNumberValueStart.TabIndex = value;
            }

        }

        public int SetTabindextxtNumberValueEndP
        {
            set
            {
                txtNumberValueEnd.TabIndex = value;
            }

        }
        #endregion

        #region "contructor"
        public XtraUCPanelControlNumberBetween()
        {
            InitializeComponent();
        }
        #endregion

        #region "compare Start Value And End Value"


        private void txtNumberValueEnd_Leave(object sender, EventArgs e)
        {
            if (txtNumberValueEnd.EditValue != null && txtNumberValueStart.EditValue != null)
            {
                if (!ProcessGeneral.CompareNumber(ProcessGeneral.GetSafeDouble(txtNumberValueStart.EditValue), ProcessGeneral.GetSafeDouble(txtNumberValueEnd.EditValue)))
                {
                    XtraMessageBox.Show("Start Value Is less than or equal to End Value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    txtNumberValueEnd.EditValue = txtNumberValueStart.EditValue;
                    txtNumberValueEnd.Focus();
                }

            }
        }

        private void txtNumberValueStart_Leave(object sender, EventArgs e)
        {

            if (txtNumberValueEnd.EditValue != null && txtNumberValueStart.EditValue != null)
            {
                if (!ProcessGeneral.CompareNumber(ProcessGeneral.GetSafeDouble(txtNumberValueStart.EditValue), ProcessGeneral.GetSafeDouble(txtNumberValueEnd.EditValue)))
                {
                    XtraMessageBox.Show("Start Value Is less than or equal to End Value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    txtNumberValueStart.EditValue = txtNumberValueEnd.EditValue;
                    txtNumberValueStart.Focus();
                }

            }
        }
        #endregion


    }
}
