using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CNY_BaseSys.UControl
{
    public partial class XtraUCPanelControlBoolean : DevExpress.XtraEditors.XtraUserControl
    {

        #region "Property"
        public CheckEdit chkCheckValueP
        {
            get
            {
                return chkCheckValue;
            }

        }
        public int CheckValue
        {
            get
            {
                return chkCheckValue.Checked ? 1 : 0;
            }
        }
        public string CheckDisplay
        {
            get
            {
                return chkCheckValue.Checked ? "true" : "false";
            }
        }
        public int SetTabindexchkCheckValueP
        {
            set
            {
                chkCheckValue.TabIndex = value;
            }

        }
        #endregion

        #region"contructor"

        public XtraUCPanelControlBoolean()
        {

            InitializeComponent();
        }
        #endregion
    }
}
