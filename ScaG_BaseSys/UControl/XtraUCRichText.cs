using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;

namespace CNY_BaseSys.UControl
{
    public partial class XtraUCRichText : DevExpress.XtraEditors.XtraUserControl
    {
        #region "Property"

        /// <summary>
        ///     Access RichTextBox Control In UserControl
        /// </summary>
        /// <value>
        ///     <para>
        ///         
        ///     </para>
        /// </value>
        /// <remarks>
        ///     
        /// </remarks>
        public RichEditControl rtxtContainp
        {
            get
            {
                return this.rtxtContain;
            }

        }

        /// <summary>
        ///     get text from rich text box
        /// </summary>
        /// <value>
        ///     <para>
        ///         
        ///     </para>
        /// </value>
        /// <remarks>
        ///     
        /// </remarks>
        public string GSText
        {
            get
            {

                return rtxtContain.Text;
            }
            set
            {
                rtxtContain.Text = value; ;
            }
        }
        /// <summary>
        ///      get rtf text from rich text box
        /// </summary>
        /// <value>
        ///     <para>
        ///         
        ///     </para>
        /// </value>
        /// <remarks>
        ///     
        /// </remarks>
        public string GSRTfText
        {
            get
            {
                return rtxtContain.RtfText;
            }
            set
            {
                rtxtContain.RtfText = value; ;
            }
        }

        /// <summary>
        ///      get html text from rich text box
        /// </summary>
        /// <value>
        ///     <para>
        ///         
        ///     </para>
        /// </value>
        /// <remarks>
        ///     
        /// </remarks>
        public string GSHTMLText
        {
            get
            {
                return rtxtContain.HtmlText;
            }
            set
            {
                rtxtContain.HtmlText = value; ;
            }
        }
        #endregion

        #region "contructor"
        public XtraUCRichText()
        {
            InitializeComponent();

        }

        #endregion


    }
}
