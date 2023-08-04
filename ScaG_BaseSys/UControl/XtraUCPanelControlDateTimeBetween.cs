using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CNY_BaseSys.Common;
using DevExpress.Utils;

namespace CNY_BaseSys.UControl
{

    public partial class XtraUCPanelControlDateTimeBetween : DevExpress.XtraEditors.XtraUserControl
    {
        #region "Property"
        public DateEdit txtdatetimeValueStartP
        {
            get
            {
                return txtdatetimeValueStart;
            }

        }

        public DateEdit txtdatetimeValueEndP
        {
            get
            {
                return txtdatetimeValueEnd;
            }

        }

        public int SetTabindextxtdatetimeValueStartP
        {
            set
            {
                txtdatetimeValueStart.TabIndex = value;
            }

        }

        public int SetTabindextxtdatetimeValueEndP
        {
            set
            {
                txtdatetimeValueEnd.TabIndex = value;
            }

        }
        #endregion
        #region "contructor"
        public XtraUCPanelControlDateTimeBetween()
        {
            InitializeComponent();
            ProcessGeneral.SetDateEditFormat(txtdatetimeValueStart, @"dd\/MM\/yyyy", false, true, DefaultBoolean.Default);
            ProcessGeneral.SetDateEditFormat(txtdatetimeValueEnd, @"dd\/MM\/yyyy", false, true, DefaultBoolean.Default);
        }
        #endregion
        #region "compare Start Date And End Date"
        private void txtdatetimeValueStart_EditValueChanged(object sender, EventArgs e)
        {
            if (txtdatetimeValueStart.EditValue != null && txtdatetimeValueEnd.EditValue != null)
            {
                if (!ProcessGeneral.CompareDate(txtdatetimeValueStart.DateTime, txtdatetimeValueEnd.DateTime))
                {
                    XtraMessageBox.Show("Start Date Is less than or equal to End Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    txtdatetimeValueStart.EditValue = txtdatetimeValueEnd.EditValue;
                }

            }
        }

        private void txtdatetimeValueEnd_EditValueChanged(object sender, EventArgs e)
        {
            if (txtdatetimeValueStart.EditValue != null && txtdatetimeValueEnd.EditValue != null)
            {
                if (!ProcessGeneral.CompareDate(txtdatetimeValueStart.DateTime, txtdatetimeValueEnd.DateTime))
                {
                    XtraMessageBox.Show("Start Date Is less than or equal to End Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    txtdatetimeValueEnd.EditValue = txtdatetimeValueStart.EditValue;
                }

            }
        }

        #endregion
    }
}
