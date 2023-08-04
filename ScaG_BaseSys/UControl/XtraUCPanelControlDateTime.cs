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
    public partial class XtraUCPanelControlDateTime : DevExpress.XtraEditors.XtraUserControl
    {
        #region "Property"
        public DateEdit txtdatetimeValueP
        {
            get
            {
                return txtdatetimeValue;
            }

        }

        public int SetTabindextxtdatetimeValueP
        {
            set
            {
                txtdatetimeValue.TabIndex = value;
            }

        }
        #endregion

        #region "contructor"
        public XtraUCPanelControlDateTime()
        {
            InitializeComponent();
            ProcessGeneral.SetDateEditFormat(txtdatetimeValue, @"dd\/MM\/yyyy", false, true, DefaultBoolean.Default);
        }
        #endregion
    }
}
