using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CNY_BaseSys.Class;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace CNY_BaseSys.Common
{
    public class RemoveAttributeColumnOnTree
    {

        private readonly List<BoMCompactDelAttInfo> _lCol;

        public RemoveAttributeColumnOnTree(List<BoMCompactDelAttInfo> lCol, TreeList tl, string fieldNameCheck, bool valueCheck)
        {
            _lCol = lCol;
            CheckTreeRemoveAttCol(tl, fieldNameCheck, valueCheck);
        }
        private void CheckTreeRemoveAttCol(TreeList tl, string fieldNameCheck, bool valueCheck)
        {

            foreach (TreeListNode node in tl.Nodes)
            {
                CheckTreeRemoveAttCol(node, fieldNameCheck, valueCheck);
               
            }

        }

        public List<BoMCompactDelAttInfo> CheckRemove()
        {
            return _lCol;
        }
       

        private  void CheckTreeRemoveAttCol(TreeListNode tlNode, string fieldNameCheck, bool valueCheck)
        {
         
            if (_lCol.Count <= 0) return;
            bool tempValue = ProcessGeneral.GetSafeBool(tlNode.GetValue(fieldNameCheck)) != valueCheck;
            if (!tempValue)
            {
                List<Int32> lRemove = new List<int>();
                for (int i = 0; i < _lCol.Count; i++)
                {
                    BoMCompactDelAttInfo colInfo = _lCol[i];
                    if (ProcessGeneral.GetSafeString(tlNode.GetValue(colInfo.FieldName)) != "")
                    {
                        lRemove.Add(i);
                     
                    }
                }
                if (lRemove.Count > 0)
                {
                    List<Int32> qDel = lRemove.OrderByDescending(p => p).ToList();
                    foreach (Int32 ind in qDel)
                    {
                        _lCol.RemoveAt(ind);
                    }
                }

                if (_lCol.Count <= 0) return;
            }
            if (tlNode.Nodes.Count > 0)
            {
                foreach (TreeListNode node in tlNode.Nodes)
                {
                    CheckTreeRemoveAttCol(node, fieldNameCheck, valueCheck);
                }
            }
        }
    }
}
