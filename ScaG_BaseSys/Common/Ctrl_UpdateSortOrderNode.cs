using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_BaseSys.Class;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace CNY_BaseSys.Common
{
   
    public class Ctrl_UpdateSortOrderNode
    {
        
       
      

        private Dictionary<Int64,Int32> _dicSort = new Dictionary<Int64, Int32>();
       

   

        private TreeListNode _parentNodeSource;
        public TreeListNode ParentNodeSource
        {
            get { return this._parentNodeSource; }
            set { this._parentNodeSource = value; }
        }

      
      
       




     



        private readonly string[] _arrColCrossTabFull;
        public Ctrl_UpdateSortOrderNode( 
            TreeListNode parentNodeSource,   string[] arrColCrossTabFull)
        {
            
            this._arrColCrossTabFull = arrColCrossTabFull;
            this._parentNodeSource = parentNodeSource;
        }
        public bool SetSortOrderNode()
        {
            _dicSort.Clear();
            if (_parentNodeSource == null) return false;
            SetSortOrderNode(_parentNodeSource);
            return true;


        }

        private void SetSortOrderNode(TreeListNode parentNode)
        {
            TreeListNodes lNode = parentNode.Nodes;
            if (lNode.Count > 0)
            {
                Int64 childPk = ProcessGeneral.GetSafeInt64(parentNode.GetValue("ChildPK"));
              
                
                if (!_dicSort.ContainsKey(childPk))
                {
                    int beginSort = ProcessGeneral.GetSafeInt(parentNode.GetValue("SortOrderNode")) + _arrColCrossTabFull.Length;
                    _dicSort.Add(childPk, beginSort);
                }

                foreach (TreeListNode node in lNode)
                {
                    foreach (string endColCross in _arrColCrossTabFull)
                    {
                        node.SetValue("SortOrderNode"+ endColCross, _dicSort[childPk]);
                        _dicSort[childPk] = _dicSort[childPk] + 1;
                        CalEditValueChangedEvent(node, endColCross);
                    }

                    SetSortOrderNode(node);
                }
            }
        }
        private void CalEditValueChangedEvent(TreeListNode node, string endColCross)
        {

            if (!string.IsNullOrEmpty(endColCross) && ProcessGeneral.GetSafeInt64(node.GetValue("PKCode" + endColCross)) <= 0) return;
            string rowState = ProcessGeneral.GetSafeString(node.GetValue("RowState" + endColCross));
            if (rowState == DataStatus.Unchange.ToString())
            {
                node.SetValue("RowState" + endColCross, DataStatus.Update.ToString());
            }
        }

     

       

    }
}
