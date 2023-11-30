using DevExpress.XtraGrid.Views.Grid;

namespace CNY_BaseSys
{
    public class TransferDataGridView : GridView
    {
        public void FocusFindEdit()
        {
            if (FindPanel != null)
            {
                FindPanel.Focus();
                FindPanel.FocusFindEdit();
            }
        }
    }



  
}
