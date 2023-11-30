using System;
using System.Drawing;
using System.Windows.Forms;
using CNY_BaseSys.Common;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace CNY_BaseSys.WForm
{
    public partial class FrmInputRichText : DevExpress.XtraEditors.XtraForm
    {

        #region "Property"
        public event OnInputValueMemoHandler OnInputValue = null;
        private readonly string _data = "";
        private bool _isSave = true;

        private bool _isInsertEnterKey = false;

        public bool IsInsertEnterKey
        {
            get { return this._isInsertEnterKey; }
            set { this._isInsertEnterKey = value; }
        }

        public BarDockStyle MenuDockStyle
        {
            get { return this.fontBar1.DockStyle; }
            set { this.fontBar1.DockStyle = value; }
        }


        public RichEditViewType RichTextViewType
        {
            get { return memoMain.ActiveViewType; }
            set
            {
                memoMain.ActiveViewType = value;
                memoMain.Options.HorizontalScrollbar.Visibility = memoMain.ActiveViewType == RichEditViewType.Simple ? RichEditScrollbarVisibility.Hidden : RichEditScrollbarVisibility.Auto;
            }
        }

        #endregion

        #region "Contructor"

        public FrmInputRichText(string data, DocumentTypeRichText type = DocumentTypeRichText.Normal,bool isInsertEnterKey = false)
        {
            InitializeComponent();
         
            //  
            memoMain.ActiveViewType = RichEditViewType.Simple;
            memoMain.Options.HorizontalScrollbar.Visibility = RichEditScrollbarVisibility.Hidden;

            _isInsertEnterKey = isInsertEnterKey;
            this._data = data;
            if (!string.IsNullOrEmpty(data))
            {
                switch(type)
                {
                    case DocumentTypeRichText.Html:
                        memoMain.HtmlText = data;
                        break;
                    case DocumentTypeRichText.Rtf:
                        memoMain.RtfText = data;
                        break;
                    default:
                        memoMain.Text = data;
                        break;
                }
              
            }
            SectionMargins margin = memoMain.Document.Sections[0].Margins;
            margin.Top = 0;
            margin.Left = 0;
            margin.Right = 0;
            margin.Bottom = 0;
            

            btnNextFinish.Click += BtnNextFinish_Click;
            this.Load += Frm_Load;
            this.Closed += Frm_Closed;
            

            EditorButton btnChangeFont = repositoryItemColorPickEdit3.Buttons[1];
            btnChangeFont.Click += BtnChangeFont_Click;
            EditorButton btnChangeBackground = repositoryItemColorPickEdit2.Buttons[1];
            btnChangeBackground.Click += BtnChangeBackground_Click;
            
        }

      

        private void Frm_Closed(object sender, EventArgs e)
        {
            string value = ProcessGeneral.GetSafeString(memoMain.Text);
            bool isSave = true;
            if (!_isSave)
            {
                isSave = false;
            }
            else
            {
                if (value != _data)
                {
                    isSave = true;}
                else
                {
                    isSave = false;
                }
            }
            if (OnInputValue != null)
            {
                if (_isInsertEnterKey)
                {
                   // DocumentPosition pos = memoMain.Document.CaretPosition;
                    //  DocumentRange range =

                    DocumentPosition pos = memoMain.Document.Range.End;
                    memoMain.Document.InsertText(pos, "\n");
                }
           
                OnInputValue(this, new OnInputValueMemoEventArgs
                {
                    Value = value,
                    IsSave = isSave,
                    ValueHtml = memoMain.HtmlText,
                    ValueRtf = memoMain.RtfText

                });
            }
        }

        private void Frm_Load(object sender, EventArgs e)
        {


          



            memoMain.SelectNextControl(ActiveControl, true, true, true, true);
            memoMain.Focus();
            memoMain.Select();
            string text = memoMain.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {

                memoMain.Document.CaretPosition = memoMain.Document.Range.End;
                //DocumentPosition pos = memoMain.Document.CaretPosition;
                ////  DocumentRange range =
                //memoMain.Document.InsertText(pos, "\n");
                //DocumentPosition myStart = memoMain.Document.CreatePosition(text.Length);
                //DocumentRange myRange = memoMain.Document.CreateRange(myStart, 1);
                //memoMain.Document.Selection = myRange;
                //  memoMain.Document.CaretPosition = myStart;


                // memoMain.Select(memoMain.Text.Length, 0);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const UInt32 WM_NCACTIVATE = 0x0086;

            if (m.Msg == WM_NCACTIVATE && m.WParam.ToInt32() == 0)
            {
                this.Close();
            }

            base.WndProc(ref m);
        }



        #endregion


        #region  "Button Click"


        private void BtnNextFinish_Click(object sender, EventArgs e)
        {



            if (!btnNextFinish.Focused)
            {

                btnNextFinish.SelectNextControl(ActiveControl, true, true, true, true);
                btnNextFinish.Focus();


            }




            this.Close();
        }

        #endregion



        #region "hotkey"
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.Escape:
                    {
                        _isSave = false;
                        this.Close();
                        return true;
                    }
                case Keys.Control | Keys.S:
                    {
                        BtnNextFinish_Click(null, null);
                        return true;
                    }


            }
            return base.ProcessCmdKey(ref message, keys);
        }


        #endregion

        private void biChangeFontColor_EditValueChanged(object sender, EventArgs e)
        {
            Color color = (Color)biChangeFontColor.EditValue;
            if (color == null) return;
            ChangeFontColor(color, false);
            //  biChangeFontColor.Edit


        }
        private void BtnChangeFont_Click(object sender, EventArgs e)
        {
            Color color = (Color)biChangeFontColor.EditValue;
            if (color == null) return;
            ChangeFontColor(color, false);
        }


        private void biChangeBackgroudColor_EditValueChanged(object sender, EventArgs e)
        {
            Color color = (Color)biChangeBackgroudColor.EditValue;
            if (color == null) return;
            ChangeFontColor(color, true);
        }
        private void BtnChangeBackground_Click(object sender, EventArgs e)
        {
            Color color = (Color)biChangeBackgroudColor.EditValue;
            if (color == null) return;
            ChangeFontColor(color, true);
        }
        private void ChangeFontColor(Color color, bool isBackColor)
        {
            Document document = memoMain.Document;
            DocumentRange range = document.Selection;



            CharacterProperties cp = document.BeginUpdateCharacters(range);
            if (isBackColor)
            {
                cp.BackColor = color;

            }
            else
            {
                cp.ForeColor = color;
            }




            document.EndUpdateCharacters(cp);
        }


    }
}
