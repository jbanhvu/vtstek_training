using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Paint;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

namespace CNY_BaseSys.Common
{
    public class TreeListLookupFilterEdit
    {
        #region "Property"

        private readonly string _searchFieldName;
        private string _strFilter = "";

        #endregion


        #region "Contructor"
        public TreeListLookupFilterEdit(TreeListLookUpEdit treeLookUpEdit, DataTable dtS, string keyField, string parentField, string displayField, string searchFieldName)
        {
            this._searchFieldName = searchFieldName;
            TreeList treeList = treeLookUpEdit.Properties.TreeList;

            string tag = ProcessGeneral.GetSafeString(treeLookUpEdit.Tag);
            if (tag.ToUpper() == "LOAD") goto finished;
            treeLookUpEdit.Tag = "LOAD";
            treeLookUpEdit.Properties.PopupFilterMode = PopupFilterMode.Contains;

            treeLookUpEdit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            treeLookUpEdit.Properties.AutoComplete = true;
            treeLookUpEdit.Properties.ImmediatePopup = true;
            treeLookUpEdit.Properties.ShowFooter = true;
            treeLookUpEdit.Properties.PopupSizeable = true;










            treeList.OptionsView.ShowAutoFilterRow = true;

            treeList.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;

            treeList.OptionsFilter.AllowFilterEditor = false;
            treeList.OptionsBehavior.EnableFiltering = true;
            treeList.OptionsFilter.FilterMode = FilterMode.Smart;
            treeList.OptionsFind.AllowFindPanel = false;

            treeList.OptionsFind.HighlightFindResults = true;

            treeList.OptionsClipboard.AllowCopy = DefaultBoolean.True;
            treeList.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            treeList.OptionsClipboard.CopyCollapsedData = DefaultBoolean.False;


            treeList.OptionsView.AutoWidth = false;
            treeList.OptionsView.EnableAppearanceEvenRow = true;
            treeList.OptionsView.EnableAppearanceOddRow = true;
            treeList.OptionsView.AllowHtmlDrawHeaders = true;
            treeList.OptionsCustomization.AllowBandMoving = false;
            treeList.OptionsCustomization.AllowColumnMoving = false;

            treeLookUpEdit.Popup += TreeEdit_Popup;
            treeLookUpEdit.CloseUp += TreeLookUpEdit_CloseUp;
            treeList.CustomDrawNodeCell += treeList_CustomDrawNodeCell;

            treeLookUpEdit.KeyPress += TreeLookUpEdit_KeyPress;
            finished:
            treeList.KeyFieldName = keyField;
            treeList.ParentFieldName = parentField;
            treeLookUpEdit.Properties.DisplayMember = displayField;
            treeLookUpEdit.Properties.KeyMember = keyField;

            treeList.DataSource = dtS;
            treeList.BestFitColumns();
            foreach (TreeListColumn col in treeList.VisibleColumns)
            {
                col.Width += 15;
            }
            treeList.ExpandAll();



        }

        private void TreeLookUpEdit_CloseUp(object sender, CloseUpEventArgs e)
        {
            _strFilter = "";
        }

        private void TreeLookUpEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isInput = false;
            _strFilter = e.KeyChar.CheckIsKeyInputText(out isInput);
            if (!isInput)
            {
                _strFilter = "";
            }

        }






















        #endregion




        #region "Filter Node"


        private void TreeEdit_Popup(object sender, EventArgs e)
        {
            //  MessageBox.Show(_strFilter);
            TreeListLookUpEdit treeLookUpEdit = (TreeListLookUpEdit)sender;
            TreeList treeList = treeLookUpEdit.Properties.TreeList;

            treeList.Nodes.AutoFilterNode.SetValue(_searchFieldName, _strFilter);
            treeList.FocusedNode = treeList.Nodes.AutoFilterNode;
            treeList.FocusedColumn = treeList.Columns[_searchFieldName];
            treeList.ShowEditor();
            if (!string.IsNullOrEmpty(_strFilter))
            {
                var editor = treeList.ActiveEditor as TextEdit;
                if (editor != null)
                {
                    editor.SelectionStart = 1;
                }
            }
            //IPopupControl c = sender as IPopupControl;



            //var controls = c.PopupWindow.Controls.Find("FindPanel", true);
            //if (controls != null)
            //{
            //    FindPanel panel = controls[0] as FindPanel;
            //    panel.Focus();
            //}


            //BeginInvoke(new MethodInvoker(() => {
            //    if (Equals(nodeByTheNameField, nodeByTheIdField)) //this line is only intended to show that the node is found by using values of the both fields
            //        treeList.FocusedNode = nodeByTheNameField;
            //}));
        }




        private void treeList_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            TreeList tl = sender as TreeList;
            if (tl == null) return;
            //TreeListNode node = e.Node;
            //if (node == null) return;
            //TreeListColumn col = e.Column;
            //if (col == null) return;

            if (tl.OptionsView.ShowAutoFilterRow)
            {
                object valueSearch = e.Column.FilterInfo.AutoFilterRowValue;
                if (valueSearch != null)
                {

                    string filterCellText = valueSearch.ToString();
                    if (!String.IsNullOrEmpty(filterCellText))
                    {
                        string textCon = e.Node.GetDisplayText(e.Column);
                        int filterTextIndex = textCon.IndexOf(filterCellText, StringComparison.CurrentCultureIgnoreCase);
                        if (filterTextIndex != -1)
                        {
                            XPaint.Graphics.DrawMultiColorString(e.Cache, e.Bounds, textCon, filterCellText,
                                e.Appearance, Color.Black, Color.Gold, false,
                                filterTextIndex);
                            e.Handled = true;
                            return;
                        }
                    }

                }
            }











        }






        #endregion



    }
}
