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

    public partial class XtraUCPanelControlValueString : DevExpress.XtraEditors.XtraUserControl
    {
        #region "Property"
        public MemoEdit txtStringValues
        {
            get
            {
                return txtStringValue;
            }

        }
        public int SetTabindextxtStringValues
        {
            set
            {
                txtStringValue.TabIndex = value;
            }

        }
        #endregion

        #region "Contructor"
        public XtraUCPanelControlValueString()
        {
            InitializeComponent();
        }
        #endregion
    }
}
