using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Windows;

namespace CNY_BaseSys.WForm
{
    public partial class FrmHelpFinal : DevExpress.XtraEditors.XtraForm
    {
      
        #region "Contructor"

        public FrmHelpFinal()
        {
            InitializeComponent();
            this.Load += FrmHelp_Load;
            pdfViewerMain.PopupMenuShowing += pdfViewer1_PopupMenuShowing;



        }
        private void pdfViewer1_PopupMenuShowing(object sender, DevExpress.XtraPdfViewer.PdfPopupMenuShowingEventArgs e)
        {
            e.ItemLinks[8].Visible = false;
        }
        private void FrmHelp_Load(object sender, EventArgs e)
        {
            
        }
        #endregion


        #region "Load Document"

        public void LoadDocument(string path)
        {
            pdfViewerMain.LoadDocument(path);
        }
        public void LoadDocument(Stream stream)
        {
            pdfViewerMain.LoadDocument(stream);
        }
        #endregion

        #region "hotkey"
        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {

                case Keys.Escape:
                    {
                        this.Close();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        #endregion
    }
}