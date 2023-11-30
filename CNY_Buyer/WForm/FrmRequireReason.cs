using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNY_Buyer.WForm
{
    public partial class FrmRequireReason : Form
    {
        //Tạo một property để lưu giá trị nhập liệu.
    public string EnteredText { get; private set; }
        public bool _isOk { get; private set; }
        public FrmRequireReason()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        { // Lưu giá trị từ TextBox vào biến EnteredText.
            EnteredText = txtReason.Text;
            _isOk = true;
            this.Close(); // Đóng form PopupForm sau khi nhấn OK.
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
