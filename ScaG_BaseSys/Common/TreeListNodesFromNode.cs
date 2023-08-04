using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;

namespace CNY_BaseSys.Common
{
    public class TreeListNodesFromNode : TreeListOperation
    {

        private readonly List<TreeListNode> _lNode;
        private bool _isFind = false;
        private readonly TreeListNode _fromNode;
        private readonly int _count = 0;
        private readonly bool _getAll = false;
        public TreeListNodesFromNode(List<TreeListNode> lNode, TreeListNode fromNode, int count, bool getAll = false)
        {
            _lNode = lNode;
            _fromNode = fromNode;
            _count = count;
            _getAll = getAll;
        }

        public override void Execute(TreeListNode node)
        {
            if (!_isFind)
            {
                if (_fromNode == node)
                {
                    _isFind = true;
                }
            }
            if (_isFind)
            {
                if (_getAll)
                {
                    _lNode.Add(node);
                }
                else if (_lNode.Count < _count)
                {
                    _lNode.Add(node);
                }
            }


        }


    }
}
