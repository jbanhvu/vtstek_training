using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CNY_BaseSys.Bases;
using CNY_BaseSys.Common;
using CNY_BaseSys.Interfaces;

namespace CNY_BaseSys.Demo
{
    public partial class FrmDemoProgressBar : DevExpress.XtraEditors.XtraForm
    {
        Bitmap _bmp;
        public FrmDemoProgressBar()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _bmp = Bitmap.FromFile(dlg.FileName) as Bitmap;
                    pct.Image = _bmp;
                }
                catch
                {
                    MessageBox.Show(this, @"The file is not a valid Bitmap",@"Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNegative_Click(object sender, EventArgs e)
        {
            ProcessGeneral.StartProgressiveOperation(new NegativePO(_bmp), this);
            pct.Refresh();
        }

        private void btnGrayScale_Click(object sender, EventArgs e)
        {
            ProcessGeneral.StartProgressiveOperation(new GrayScalePO(_bmp), this);
            pct.Refresh();
        }

        private void btnAllEffects_Click(object sender, EventArgs e)
        {
            List<IProgressiveOperation> opList = new List<IProgressiveOperation>();
            opList.Add(new NegativePO(_bmp));
            opList.Add(new GrayScalePO(_bmp));

            ProcessGeneral.StartProgressiveOperation(new CompositePO(opList), this);
            pct.Refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}