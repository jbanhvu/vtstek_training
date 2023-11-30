using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNY_BaseSys.WForm
{
    public partial class FrmViewImage : DevExpress.XtraEditors.XtraForm
    {
        public FrmViewImage(Image img)
        {
            InitializeComponent();
            picProduct.Image = img;
        }


        public FrmViewImage(String path)
        {
            InitializeComponent();
            Image img = Image.FromFile(path);
            picProduct.Image = img;
        }
    }
}
