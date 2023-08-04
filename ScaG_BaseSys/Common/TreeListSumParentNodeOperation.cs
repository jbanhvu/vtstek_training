using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;

namespace CNY_BaseSys.Common
{

    public class TreeListSumParentNodeOperation : TreeListOperation
    {
        private readonly string _fieldName;
        private double _result;

        public TreeListSumParentNodeOperation(string fieldName)
        {
            this._fieldName = fieldName;
            _result = 0;
        }
        // incrementing the counter if the node's value exceeds the limit
        public override void Execute(TreeListNode node)
        {
            if (node.ParentNode == null)
            {
                double sum = ProcessGeneral.GetSafeDouble(node[_fieldName]);
                _result = _result + sum;
            }
        }
        public double Result
        {
            get { return _result; }
        }
    }
    public class TreeListCountParentNodeOperation : TreeListOperation
    {
        private readonly string _fieldName;
        private int _result;

        public TreeListCountParentNodeOperation(string fieldName)
        {
            this._fieldName = fieldName;
            _result = 0;
        }
        // incrementing the counter if the node's value exceeds the limit
        public override void Execute(TreeListNode node)
        {
            if (node.ParentNode == null)
            {
                _result = _result + 1;
            }
        }
        public int Result
        {
            get { return _result; }
        }
    }



   
}
