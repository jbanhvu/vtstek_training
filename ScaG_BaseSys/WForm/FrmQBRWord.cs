using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Office;
using DevExpress.XtraEditors;
using CNY_BaseSys.Common;

namespace CNY_BaseSys.WForm
{
    public partial class FrmQBRWord : DevExpress.XtraEditors.XtraForm
    {
        #region "Property"
      
        public event GetRtfTextQbrHandler OnGetText= null;

        public String GetSetRtFText
        {
            get { return this.richMain.RtfText; }
            set { this.richMain.RtfText = value; }
        }
        public String GetSetHtmlText
        {
            get { return this.richMain.HtmlText; }
            set { this.richMain.HtmlText = value; }
        }

        public String GetSetText
        {
            get { return this.richMain.Text; }
            set { this.richMain.Text = value; }
        }
        #endregion

        #region "Contructor"
        public FrmQBRWord()
        {
            InitializeComponent();

            ProcessGeneral.SetupDefaultMarginSettings(richMain.Document, DocumentUnit.Inch, PaperKind.A4, false, new MarginWord(0.5f, 0.5f, 0.5f, 0.5f));
            richMain.DocumentLoaded += richMain_DocumentLoaded;
            richMain.EmptyDocumentCreated += richMain_EmptyDocumentCreated;
            this.Closed += FrmQBRWord_Closed;



        }
        #endregion

        #region "Form Event"
        private void FrmQBRWord_Closed(object sender, EventArgs e)
        {
            OnGetText(this, new GetRtfTextQbrEventArgs
              {
                  RtfText = richMain.RtfText,
                  HtmlText = richMain.HtmlText,
                  Text = richMain.Text
              });
        }
        #endregion

        #region "Rich Text Event"

        private void richMain_EmptyDocumentCreated(object sender, EventArgs e)
        {
            ProcessGeneral.SetupDefaultMarginSettings(richMain.Document, DocumentUnit.Inch, PaperKind.A4, false, new MarginWord(0.5f, 0.5f, 0.5f, 0.5f));
        }

        private void richMain_DocumentLoaded(object sender, EventArgs e)
        {
            ProcessGeneral.SetupDefaultMarginSettings(richMain.Document, DocumentUnit.Inch, PaperKind.A4, false, new MarginWord(0.5f, 0.5f, 0.5f, 0.5f));
        }
        #endregion
    }
}