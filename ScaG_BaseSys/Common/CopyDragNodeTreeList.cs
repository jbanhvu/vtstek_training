using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys.Class;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace CNY_BaseSys.Common
{
    public class CopyDragNodeTreeList
    {
        public Dictionary<Int64, List<BoMInputAttInfo>> DicAttValueRoot { get; set; }
        public Dictionary<Int64, DataTable> DicTableProductionRoot = new Dictionary<Int64, DataTable>(); //PKCHild_PKCode
        private string[] _arrCol;
        public string[] ArrCol
        {
            get { return this._arrCol; }
            set { this._arrCol = value; }
        }

        private Int64 _beginChildPk;
        public Int64 BeginChildPk
        {
            get { return this._beginChildPk; }
            set { this._beginChildPk = value; }
        }
        private string _keyField;
        public string KeyField
        {
            get { return this._keyField; }
            set { this._keyField = value; }
        }

        private string _parentField;
        public string ParentField
        {
            get { return this._parentField; }
            set { this._parentField = value; }
        }

        private string _fieldRowState;
        public string FieldRowState
        {
            get { return this._fieldRowState; }
            set { this._fieldRowState = value; }
        }

        private string _fieldAllowUpdate;
        public string FieldAllowUpdate
        {
            get { return this._fieldAllowUpdate; }
            set { this._fieldAllowUpdate = value; }
        }

        private List<TreeListNode> _listSourceNode;
        public List<TreeListNode> ListSourceNode
        {
            get { return this._listSourceNode; }
            set { this._listSourceNode = value; }
        }

        private TreeListNode _parentNodeSource;
        public TreeListNode ParentNodeSource
        {
            get { return this._parentNodeSource; }
            set { this._parentNodeSource = value; }
        }

        private TreeList _tl;
        public TreeList TL
        {
            get { return this._tl; }
            set { this._tl = value; }
        }

        private string _fieldSortOrder;
        public string FieldSortOrder
        {
            get { return this._fieldSortOrder; }
            set { this._fieldSortOrder = value; }
        }

        private InsertType _dragInsertPos;
        public InsertType DragInsertPos
        {
            get { return this._dragInsertPos; }
            set { this._dragInsertPos = value; }
        }


        private TreeListNode _destNodeSource;
        public TreeListNode DestNodeSource
        {
            get { return this._destNodeSource; }
            set { this._destNodeSource = value; }
        }





        private List<TreeListNode> _listSelectedNode;
        public List<TreeListNode> ListSelectedNode
        {
            get { return this._listSelectedNode; }
            set { this._listSelectedNode = value; }
        }



        private bool _isSetAtt;
        public bool IsSetAtt
        {
            get { return this._isSetAtt; }
            set { this._isSetAtt = value; }
        }


        private bool _isBoolValueAllowUpdate = true;
        public bool IsBoolValueAllowUpdate
        {
            get { return this._isBoolValueAllowUpdate; }
            set { this._isBoolValueAllowUpdate = value; }
        }

        public CopyDragNodeTreeList()
        {
            
        }
        public CopyDragNodeTreeList(string[] arrCol, Int64 beginChildPk, string keyField, string parentField, string fieldRowState, string fieldAllowUpdate, string fieldSortOrder, List<TreeListNode> listSourceNode, 
            TreeListNode parentNodeSource, TreeList tl, InsertType dragInsertPos, TreeListNode destNodeSource, List<TreeListNode> listSelectedNode, bool isSetAtt)
        {
            this._isSetAtt = isSetAtt;
            this._arrCol = arrCol;
            this._beginChildPk = beginChildPk;
            this._keyField = keyField;
            this._parentField = parentField;
            this._fieldRowState = fieldRowState;
            this._fieldAllowUpdate = fieldAllowUpdate;
            this._listSourceNode = listSourceNode;
            this._parentNodeSource = parentNodeSource;
            this._fieldSortOrder = fieldSortOrder;
            this._tl = tl;
            this._dragInsertPos = dragInsertPos;
            this._destNodeSource = destNodeSource;
            this._listSelectedNode = listSelectedNode;
        }
        public void CopyNewListNode()
        {
            
            _tl.BeginUpdate();
            _tl.LockReloadNodes();
            bool expandEach = _parentNodeSource == null;

            List<Int64> lNodeEx = new List<Int64>();
            if (!expandEach)
            {
                lNodeEx.Add(ProcessGeneral.GetSafeInt64(_parentNodeSource.GetValue(_keyField)));
            }

            Int32 beginSortOrder = 0;
            if (_destNodeSource == null)
            {
                if (_parentNodeSource == null)
                {
                    beginSortOrder = GetMaxSortOrderValueOnNode(null);
                }
                else
                {
                    beginSortOrder = GetMaxSortOrderValueOnNode(_parentNodeSource);
                }
               
            }
            else
            {

                TreeListNodes lNodeLoop = _parentNodeSource == null ? _tl.Nodes : _parentNodeSource.Nodes;

                if (_dragInsertPos == InsertType.AsChild)
                {
                    if (lNodeLoop.Any())
                    {
                        beginSortOrder = lNodeLoop.Max(p => ProcessGeneral.GetSafeInt(p.GetValue(_fieldSortOrder)))+1;
                    }
                }
                else
                {
                    bool isFind = false;
                    List<TreeListNode> lNodeSort = new List<TreeListNode>();
                    if (_dragInsertPos == InsertType.Before)
                    {
                        lNodeSort.Add(_destNodeSource);
                    }
                    foreach (TreeListNode nodeSort in lNodeLoop)
                    {
                        if (nodeSort == _destNodeSource)
                        {
                            isFind = true;
                            continue;
                        }
                        if (isFind)
                        {
                            lNodeSort.Add(nodeSort);
                        }
                    }
                    if (lNodeSort.Count > 0)
                    {
                        beginSortOrder = lNodeSort.Min(p => ProcessGeneral.GetSafeInt(p.GetValue(_fieldSortOrder)));
                    }
                    else
                    {
                        beginSortOrder = lNodeLoop.Max(p => ProcessGeneral.GetSafeInt(p.GetValue(_fieldSortOrder))) + 1;
                    }
                
                    Int32 afterSortOrder = beginSortOrder + _listSourceNode.Count;

                    foreach (TreeListNode nodeS in lNodeSort)
                    {
                        nodeS.SetValue(_fieldSortOrder, afterSortOrder);
                        afterSortOrder++;
                        string rowState = ProcessGeneral.GetSafeString(nodeS.GetValue("RowState"));
                        if (rowState == DataStatus.Unchange.ToString())
                        {
                            nodeS.SetValue("RowState", DataStatus.Update.ToString());
                        }
                    }

                }


            }


            Int64 childPkTest = 0;
            foreach (TreeListNode node in _listSourceNode)
            {
                TreeListNode nodeAdd = AddNodeCopyTreeList(node, _parentNodeSource,true, beginSortOrder);
             
                AddAllNodeCopy(node, nodeAdd);
                childPkTest = ProcessGeneral.GetSafeInt64(nodeAdd.GetValue(_keyField));
                if (expandEach)
                {
                    lNodeEx.Add(childPkTest);

                }
             
                beginSortOrder++;



            }
          
            _tl.UnlockReloadNodes();
            _tl.EndUpdate();

            ((DataTable)_tl.DataSource).AcceptChanges();
            _tl.BeginUpdate();

            TreeListNode nodeFocusN = null;
            foreach (Int64 pkFind in lNodeEx)
            {
                TreeListNode nodeFinal = _tl.FindNodeByKeyID(pkFind);
                if (nodeFinal == null) continue;
                if (pkFind == childPkTest)
                    nodeFocusN = nodeFinal;


                if (!nodeFinal.HasChildren) continue;
                nodeFinal.ExpandAll();
            }


             

            if (nodeFocusN != null)
            {
                ProcessGeneral.SetFocusedCellOnTree(_tl, nodeFocusN, "RMCode_001");
            }
            else
            {
                nodeFocusN = _tl.FindNodeByKeyID(childPkTest);

                if (nodeFocusN != null)
                {
                    ProcessGeneral.SetFocusedCellOnTree(_tl, nodeFocusN, "RMCode_001");
                }
            }
            _tl.EndUpdate();
        }


        private Int32 GetMaxSortOrderValueOnNode(TreeListNode node)
        {
            var q1 = node == null ? _tl.Nodes : node.Nodes;
            if (q1.Count <= 0) return 1;
            return q1.Select(p => ProcessGeneral.GetSafeInt(p.GetValue(_fieldSortOrder))).Max() + 1;
        }

        private void AddAllNodeCopy(TreeListNode parentNodeOld, TreeListNode parentNodeNew)
        {
            List<TreeListNode> lParent = GetNodeChildCopyDrag(parentNodeOld);
            
            foreach (TreeListNode node in lParent)
            {

                TreeListNode nodeAdd = AddNodeCopyTreeList(node, parentNodeNew);
                //if (!parentNodeNew.Expanded)
                //    parentNodeNew.Expanded = true;

                List<TreeListNode> lChild = GetNodeChildCopyDrag(node);

                if (lChild.Count > 0)
                {
                    foreach (TreeListNode childNode in lChild)
                    {
                        TreeListNode nodeAddChild = AddNodeCopyTreeList(childNode, nodeAdd);
                        //if (!nodeAdd.Expanded)
                        //    nodeAdd.Expanded = true;
                        AddAllNodeCopy(childNode, nodeAddChild);
                    }
                }
            }
        }


        private List<TreeListNode> GetNodeChildCopyDrag(TreeListNode node)
        {
          //  
            List<TreeListNode> lSource = node.Nodes.ToList();
            if (lSource.Count <= 0) return lSource;
            if (_listSelectedNode.Count <= 0) return lSource;
            var q1 = _listSelectedNode.Join(lSource, p => ProcessGeneral.GetSafeInt64(p.GetValue(_keyField)), t => ProcessGeneral.GetSafeInt64(t.GetValue(_keyField)), (p, t) => new
                {
                    NodeSelected = p,
                    NodeSource = t
                }).ToList();
            if (!q1.Any()) return lSource;

            List<TreeListNode> l = new List<TreeListNode>();
            foreach (var item in q1)
            {
                _listSelectedNode.Remove(item.NodeSelected);
                l.Add(item.NodeSource);
            }
            return l;
        }
        private TreeListNode AddNodeCopyTreeList(TreeListNode destNode, TreeListNode parentNode, bool isSetSortOrder = false, int sortOrder = 0)
        {

            _beginChildPk++;

            DataRow dr = ((DataRowView)_tl.GetDataRecordByNode(destNode)).Row;

            object[] data = new object[_arrCol.Length];

            for (int i = 0; i < _arrCol.Length; i++)
            {
                string fieldName = _arrCol[i];
                if (fieldName == _keyField)
                {
                    data[i] = _beginChildPk;
                }
                else if (fieldName == _parentField)
                {
                    if (parentNode == null)
                    {
                        data[i] = 0;
                    }
                    else
                    {
                        data[i] = parentNode.GetValue(_parentField);
                    }
                }
                else if (fieldName == _fieldSortOrder)
                {
                    if (isSetSortOrder)
                    {
                        data[i] = sortOrder;
                    }
                    else
                    {
                        data[i] = dr[fieldName];
                    }
                }
                else if (fieldName == _fieldRowState)
                {
                    data[i] = DataStatus.Insert.ToString();
                }
                else if (fieldName == _fieldAllowUpdate)
                {
                    if (_isBoolValueAllowUpdate)
                    {
                        data[i] = true;
                    }
                    else
                    {
                        data[i] = RowEditType.Empty.ToString();
                    }
           
                }
                else
                {
                    data[i] = dr[fieldName];
                }
            }

            if (_isSetAtt)
            {
                Int64 childPkOld = ProcessGeneral.GetSafeInt64(dr[_keyField]);
                List<BoMInputAttInfo> lAttS;
                if (!DicAttValueRoot.TryGetValue(childPkOld, out lAttS))
                {
                    DicAttValueRoot.Add(_beginChildPk, new List<BoMInputAttInfo>());
                }
                else
                {
                    List<BoMInputAttInfo> lAttA = new List<BoMInputAttInfo>();
                    foreach (BoMInputAttInfo infoS in lAttS)
                    {
                        BoMInputAttInfo infoA = new BoMInputAttInfo
                        {
                            AttibutePK = infoS.AttibutePK,
                            AttibuteCode = infoS.AttibuteCode,
                            AttibuteName = infoS.AttibuteName,
                            AttibuteValueFull = infoS.AttibuteValueFull,
                            RowState = DataStatus.Insert,
                            PK = 0,
                            AttibuteUnit = infoS.AttibuteUnit,
                            AttibuteValueTemp = infoS.AttibuteValueTemp,
                            IsNumber = infoS.IsNumber,
                        };
                        lAttA.Add(infoA);

                    }
                    DicAttValueRoot.Add(_beginChildPk, lAttA);
                }


                string itemType = ProcessGeneral.GetSafeString(dr["ItemType"]);
                if (itemType == "R")
                {
                    DataTable dtPro;
                    if (DicTableProductionRoot.TryGetValue(childPkOld, out dtPro))
                    {
                        DataTable dtS = dtPro.Clone();
                        foreach (DataRow drPro in dtPro.Rows)
                        {
                            DataRow drS = dtS.NewRow();
                            drS["CNYMF012PK"] = drPro["CNYMF012PK"];
                            drS["PointCode"] = drPro["PointCode"];
                            drS["PointDesc"] = drPro["PointDesc"];
                            drS["UC"] = drPro["UC"];
                            drS["PK"] = 0;
                            drS["RowState"] = DataStatus.Insert.ToString();
                            dtS.Rows.Add(drS);






                        }
                        dtS.AcceptChanges();
                        DicTableProductionRoot.Add(_beginChildPk, dtS);

                    }


                }

            }



         

            TreeListNode nodeReturn = _tl.AppendNode(data, parentNode);
            return nodeReturn;
        }

    }
}
