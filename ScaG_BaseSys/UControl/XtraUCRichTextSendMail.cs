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

    public partial class XtraUCRichTextSendMail : DevExpress.XtraEditors.XtraUserControl
    {


        #region "Property"
        public RichEditViewType RichTextViewType
        {
            get { return ricTxtSendMail.ActiveViewType; }
            set
            {
                ricTxtSendMail.ActiveViewType = value;
                ricTxtSendMail.Options.HorizontalScrollbar.Visibility = ricTxtSendMail.ActiveViewType == RichEditViewType.Simple ? RichEditScrollbarVisibility.Hidden : RichEditScrollbarVisibility.Auto;
            }
        }


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
                return this.ricTxtSendMail;
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

                return ricTxtSendMail.Text;
            }
            set
            {
                ricTxtSendMail.Text = value; ;
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
                return ricTxtSendMail.RtfText;
            }
            set
            {
                ricTxtSendMail.RtfText = value; ;
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
                return ricTxtSendMail.HtmlText;
            }
            set
            {
                ricTxtSendMail.HtmlText = value; ;
            }
        }
        #endregion

        #region "contructor"
        public XtraUCRichTextSendMail()
        {
            InitializeComponent();
        }
        #endregion
    }
}
