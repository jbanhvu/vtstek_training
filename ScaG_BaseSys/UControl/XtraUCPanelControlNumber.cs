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

    public partial class XtraUCPanelControlNumber : DevExpress.XtraEditors.XtraUserControl
    {
        #region "Property"
        public TextEdit txtNumberValueP
        {
            get
            {
                return txtNumberValue;
            }

        }
        public int SetTabindextxtNumberValueP
        {
            set
            {
                txtNumberValue.TabIndex = value;
            }

        }
        #endregion

        #region "contructor"
        public XtraUCPanelControlNumber()
        {
            InitializeComponent();
        }
        #endregion
    }
}
