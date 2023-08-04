using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CNY_BaseSys.WForm;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace CNY_BaseSys.Common
{
    public static class BoMFunctionGeneral
    {
     


        public static DataTable StandardDataBoMPrint(DataTable dtPara, Control ctrlParent, List<string> lPara, bool externalGroup, Dictionary<string, int> dicGlobal)
        {

            if (dtPara.Rows.Count <= 0) return dtPara;
            Int64 childPkNew = dtPara.AsEnumerable().Select(p => p.Field<Int64>("ChildPK")).Max() + 10;
            string[] arrFieldName = dtPara.Columns.Cast<DataColumn>().Select(p => p.ColumnName).ToArray();
            var qRmCode = dtPara.AsEnumerable().Where(p => p.Field<string>("ItemType") == "R").Where(p=> lPara.Any(t=>t== p.Field<string>("MainMaterialGroup"))).ToList();
            // if(!qRmCode.Any()) return dtPara;
            var qProCode = dtPara.AsEnumerable().Where(p => p.Field<string>("ItemType") != "R").ToList();
            string[] arrMainMaterial = qRmCode.Select(p => p.Field<string>("MainMaterialGroup")).Distinct().Join(dicGlobal,p=>p,t=>t.Key,(p,t)=>new
            {
                MainMaterialGroup = p,
                SortOrderNode = t.Value
            }).OrderBy(p=>p.SortOrderNode).Select(p=>p.MainMaterialGroup).ToArray();
            //   TreeList tlTempData = new TreeList();
            DataTable dtTreeSource = qProCode.Any() ? qProCode.CopyToDataTable() : dtPara.Clone();



            PanelControl panelControlTree = new PanelControl { Dock = DockStyle.Fill };
            TreeList tlTempData = new TreeList { Dock = System.Windows.Forms.DockStyle.Fill };
            panelControlTree.Controls.Add(tlTempData);
            ctrlParent.Controls.Add(panelControlTree);
            // 




            LoadDataTreeTest(tlTempData, dtTreeSource);


            DataTable dtResult = dtPara.Clone();
          
            Int32 sortIndexNew = 0;
            foreach (string strMainMaterial in arrMainMaterial)
            {
                Int64 rootPk = childPkNew;
                DataRow drMain = dtResult.NewRow();
                drMain["MainMaterialGroup"] = strMainMaterial;
                drMain["ChildPK"] = rootPk;
                drMain["ParentPK"] = 0;
                drMain["SortOrderNode"] = sortIndexNew;
                dtResult.Rows.Add(drMain);
                childPkNew++;
                sortIndexNew++;
                Dictionary<Int64, List<DataRow>> dicRm = qRmCode.AsEnumerable().Where(p => p.Field<string>("MainMaterialGroup") == strMainMaterial).GroupBy(p => p.Field<Int64>("ParentPK")).Select(myGroup => new
                {
                    KeyDic = myGroup.Key,
                    TempData = myGroup.Select(drGroup => drGroup).OrderBy(p => p.Field<Int32>("SortOrderNode")).ToList()
                }).ToDictionary(item => item.KeyDic, item => item.TempData);
                foreach (var itemRm in dicRm)
                {
                    Int64 parentKeyFind = itemRm.Key;
                    List<DataRow> lChildRow = itemRm.Value;
                    if (parentKeyFind == 0)
                    {
                        foreach (DataRow drAddTemp in lChildRow)
                        {
                            DataRow drAdd = dtResult.NewRow();
                            foreach (string sCol in arrFieldName)
                            {
                                drAdd[sCol] = drAddTemp[sCol];
                            }
                      
                            drAdd["ParentPK"] = rootPk;
                            drAdd["MainMaterialGroup"] = "";
                            dtResult.Rows.Add(drAdd);
                        }
                    }
                    else
                    {
                        TreeListNode nodeFind = tlTempData.FindNodeByKeyID(parentKeyFind);
                        if (nodeFind == null)
                        {
                            foreach (DataRow drAddTemp1 in lChildRow)
                            {
                                DataRow drAdd1 = dtResult.NewRow();
                                foreach (string sCol in arrFieldName)
                                {
                                    drAdd1[sCol] = drAddTemp1[sCol];
                                }
                                drAdd1["ParentPK"] = rootPk;
                                drAdd1["MainMaterialGroup"] = "";
                         
                                dtResult.Rows.Add(drAdd1);
                            }
                        }
                        else
                        {

                            if (nodeFind.ParentNode == null)
                            {
                                DataRow drNodeFind = ((DataRowView)tlTempData.GetDataRecordByNode(nodeFind)).Row;
                                DataRow drAdd2 = dtResult.NewRow();
                                foreach (string sCol in arrFieldName)
                                {
                                    drAdd2[sCol] = drNodeFind[sCol];
                                }
                                drAdd2["ParentPK"] = rootPk;
                                Int64 childPk1 = childPkNew;
                                drAdd2["ChildPK"] = childPk1;
                                childPkNew++;
                                drAdd2["MainMaterialGroup"] = "";
                       
                                dtResult.Rows.Add(drAdd2);
                                foreach (DataRow drAddTemp3 in lChildRow)
                                {
                                    DataRow drAdd3 = dtResult.NewRow();
                                    foreach (string sCol in arrFieldName)
                                    {
                                        drAdd3[sCol] = drAddTemp3[sCol];
                                    }
                                    drAdd3["ParentPK"] = childPk1;
                                    drAdd3["MainMaterialGroup"] = "";
                                  
                                    dtResult.Rows.Add(drAdd3);
                                }

                            }
                            else
                            {
                                TreeListNode[] lNodeRoot = nodeFind.GetAllParentNode().OrderBy(p => p.Level).ToArray();

                                int cRoot = lNodeRoot.Length;
                                Int64 parentPkFind = 0;
                                Int64[] arrPktTemp = new Int64[cRoot];
                                for (int i = 0; i < cRoot; i++)
                                {
                                    TreeListNode nodeTemp = lNodeRoot[i];
                                    DataRow drFindNode1 = ((DataRowView)tlTempData.GetDataRecordByNode(nodeTemp)).Row;
                                    DataRow drAdd6 = dtResult.NewRow();
                                    foreach (string sCol in arrFieldName)
                                    {
                                        drAdd6[sCol] = drFindNode1[sCol];
                                    }
                                    Int64 childPk4 = childPkNew;
                                    drAdd6["ChildPK"] = childPk4;
                                    childPkNew++;
                                    arrPktTemp[i] = childPk4;
                                    if (i == cRoot - 1)
                                    {
                                        parentPkFind = childPk4;
                                    }

                                    if (i == 0)
                                    {
                                        drAdd6["ParentPK"] = rootPk;


                                    }
                                    else
                                    {
                                        drAdd6["ParentPK"] = arrPktTemp[i - 1];
                                    }
                                    drAdd6["MainMaterialGroup"] = "";
                                  
                                    dtResult.Rows.Add(drAdd6);
                                }



                                DataRow drFindNode = ((DataRowView)tlTempData.GetDataRecordByNode(nodeFind)).Row;
                                DataRow drAdd5 = dtResult.NewRow();
                                foreach (string sCol in arrFieldName)
                                {
                                    drAdd5[sCol] = drFindNode[sCol];
                                }
                                drAdd5["ParentPK"] = parentPkFind;
                                Int64 childPk3 = childPkNew;
                                drAdd5["ChildPK"] = childPk3;
                                childPkNew++;
                                drAdd5["MainMaterialGroup"] = "";
                         
                                dtResult.Rows.Add(drAdd5);
                                foreach (DataRow drAddTemp7 in lChildRow)
                                {
                                    DataRow drAdd7 = dtResult.NewRow();
                                    foreach (string sCol in arrFieldName)
                                    {
                                        drAdd7[sCol] = drAddTemp7[sCol];
                                    }
                                    drAdd7["ParentPK"] = childPk3;

                                    drAdd7["MainMaterialGroup"] = "";
                               
                                    dtResult.Rows.Add(drAdd7);
                                }
                            }
                        }
                    }
                }

            }

            if (externalGroup)
            {
                var qChildPkNew = dtResult.AsEnumerable().Where(p => ProcessGeneral.GetSafeInt64(p["ChildPKNew"]) > 0).Select(p => p.Field<Int64>("ChildPKNew")).Distinct().ToList();

                var qProCode2 = qProCode.AsEnumerable().Where(p => qChildPkNew.All(t => t != p.Field<Int64>("ChildPK"))).ToList();

                if (qProCode2.Any())
                {

                    DataTable dtTest1 = qProCode2.CopyToDataTable();
                    LoadDataTreeTest(tlTempData, dtTest1);



                    var qCode1 = tlTempData.Nodes.Select(p => new
                    {
                        MainMaterialGroup = ProcessGeneral.GetSafeString(p.GetValue("MainMaterialGroup")),
                        Node = p
                    }).Distinct().ToList();
                    var qCode2 = dtResult.AsEnumerable().Where(p => p.Field<Int64>("ParentPK") == 0 && !string.IsNullOrEmpty(p.Field<string>("MainMaterialGroup"))).Select(p => new
                    {
                        ChildPK = p.Field<Int64>("ChildPK"),
                        MainMaterialGroup = p.Field<string>("MainMaterialGroup")
                    }).Distinct().ToList();

                    Dictionary<string, List<TreeListNode>> qExisCode = qCode2.Join(qCode1, p => p.MainMaterialGroup, t => t.MainMaterialGroup, (p, t) => new
                    {
                        p.MainMaterialGroup,
                        p.ChildPK,
                        t.Node
                    }).GroupBy(p => new
                    {
                        p.ChildPK,
                        p.MainMaterialGroup
                    }).Select(myGroup => new
                    {
                        KeyDic = string.Format("{0}%%%%%%%%%%{1}", myGroup.Key.ChildPK, myGroup.Key.MainMaterialGroup),
                        TempData = myGroup.Select(drGroup => drGroup.Node).ToList()
                    }).ToDictionary(item => item.KeyDic, item => item.TempData);



                    Dictionary<string, List<TreeListNode>> qNotExisCode = qCode1.Where(p => qCode2.All(t => t.MainMaterialGroup != p.MainMaterialGroup)).GroupBy(p => new
                    {
                        p.MainMaterialGroup
                    }).Select(myGroup => new
                    {
                        KeyDic = myGroup.Key.MainMaterialGroup,
                        TempData = myGroup.Select(drGroup => drGroup.Node).ToList()
                    }).ToDictionary(item => item.KeyDic, item => item.TempData);


                    foreach (var itemExists in qExisCode)
                    {
                        var qKey = itemExists.Key.Split(new string[] { "%%%%%%%%%%" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        if (qKey.Length != 2) continue;
                        Int64 pkRootEx = ProcessGeneral.GetSafeInt64(qKey[0].Trim());

                        foreach (TreeListNode node1 in itemExists.Value)
                        {

                            DataRow dr1 = ((DataRowView)tlTempData.GetDataRecordByNode(node1)).Row;
                            DataRow drA1 = dtResult.NewRow();
                            foreach (string sCol in arrFieldName)
                            {
                                drA1[sCol] = dr1[sCol];
                            }
                            drA1["ParentPK"] = pkRootEx;
                            drA1["MainMaterialGroup"] = "";
                            dtResult.Rows.Add(drA1);
                            TreeListNode[] lNodeEx = node1.GetAllChildNode().ToArray();
                            foreach (TreeListNode node2 in lNodeEx)
                            {
                                DataRow dr2 = ((DataRowView)tlTempData.GetDataRecordByNode(node2)).Row;
                                DataRow drA2 = dtResult.NewRow();
                                foreach (string sCol in arrFieldName)
                                {
                                    drA2[sCol] = dr2[sCol];
                                }

                                drA2["MainMaterialGroup"] = "";
                                dtResult.Rows.Add(drA2);
                            }
                        }







                    }
                    foreach (var itemNot in qNotExisCode)
                    {
                        Int64 rootPkNot = childPkNew;
                        DataRow drMain = dtResult.NewRow();
                        drMain["MainMaterialGroup"] = itemNot.Key;
                        drMain["ChildPK"] = rootPkNot;
                        drMain["ParentPK"] = 0;
                        drMain["SortOrderNode"] = sortIndexNew;
                        dtResult.Rows.Add(drMain);
                        childPkNew++;
                        sortIndexNew++;

                        foreach (TreeListNode node3 in itemNot.Value)
                        {

                            DataRow dr3 = ((DataRowView)tlTempData.GetDataRecordByNode(node3)).Row;
                            DataRow drA3 = dtResult.NewRow();
                            foreach (string sCol in arrFieldName)
                            {
                                drA3[sCol] = dr3[sCol];
                            }
                            drA3["ParentPK"] = rootPkNot;
                            drA3["MainMaterialGroup"] = "";
                            dtResult.Rows.Add(drA3);
                            TreeListNode[] lNodeExN = node3.GetAllChildNode().ToArray();
                            foreach (TreeListNode node4 in lNodeExN)
                            {
                                DataRow dr4 = ((DataRowView)tlTempData.GetDataRecordByNode(node4)).Row;
                                DataRow drA4 = dtResult.NewRow();
                                foreach (string sCol in arrFieldName)
                                {
                                    drA4[sCol] = dr4[sCol];
                                }

                                drA4["MainMaterialGroup"] = "";
                                dtResult.Rows.Add(drA4);
                            }
                        }
                    }

                }
            }


            dtResult.AcceptChanges();

            

            var qT1 = dtPara.AsEnumerable().Where(p => p.Field<Int64>("ParentPK") == 0).Select(p => new
            {
                ChildPK = p.Field<Int64>("ChildPK"),
                ParentPK = p.Field<Int64>("ParentPK"),
                SortOrderNode = p.Field<Int32>("SortOrderNode"),
            }).ToList();
            var qT2 = dtPara.AsEnumerable().Where(p => qT1.Any(t => t.ChildPK == p.Field<Int64>("ParentPK"))).Select(
                p => new
                {
                    ChildPK = p.Field<Int64>("ChildPK"),
                    ParentPK = p.Field<Int64>("ParentPK"),
                    SortOrderNode = p.Field<Int32>("SortOrderNode"),
                }).ToList();

            var qT3 = qT1.Union(qT2).ToList();
           
            if (qT3.Any())
            {
              
                LoadDataTreeTest( tlTempData, qT3.CopyToDataTableNew());

                Dictionary<string,Int32> dicSort = new Dictionary<string, int>();
                Int32 sortOrderNode = 0;
                foreach (TreeListNode node in tlTempData.Nodes)
                {
                    Int64 childPkT1 = ProcessGeneral.GetSafeInt64(node.GetValue("ChildPK"));
                    List<TreeListNode> lNode = node.Nodes.ToList();
                    if (lNode.Count > 0)
                    {
                        foreach (TreeListNode nodeC in lNode)
                        {
                            Int64 childPkT2 = ProcessGeneral.GetSafeInt64(nodeC.GetValue("ChildPK"));
                            dicSort.Add(string.Format("{0}-{1}", childPkT1, childPkT2), sortOrderNode);
                            sortOrderNode++;
                        }
                    }
                    else
                    {
                        dicSort.Add(string.Format("{0}", childPkT1), sortOrderNode);
                        sortOrderNode++;
                    }

                 
                }

             
                dtResult.Columns.Add("SortOrderNodeNew", typeof(string));
                LoadDataTreeTest(tlTempData, dtResult);


           
                foreach (TreeListNode nodeLv0 in tlTempData.Nodes)
                {
                    TreeListNodes lNodeLv1 = nodeLv0.Nodes;
                    foreach (TreeListNode nodeLv1 in lNodeLv1)
                    {
                        Int64 childPkL1 = ProcessGeneral.GetSafeInt64(nodeLv1.GetValue("ChildPKNew"));
                     
                        string keyL1 = "";
                        if (nodeLv1.Nodes.Count > 0)
                        {
                            Int64 childPkL2 = ProcessGeneral.GetSafeInt64(nodeLv1.Nodes[0].GetValue("ChildPKNew"));
                            keyL1 = string.Format("{0}-{1}", childPkL1, childPkL2);
                        }
                        else
                        {
                            keyL1 = string.Format("{0}", childPkL1);
                        }

                        Int32 sortN = 0;
                        if (dicSort.TryGetValue(keyL1, out sortN))
                        {
                            nodeLv1.SetValue("SortOrderNodeNew", sortN);
                        }
                    }
                }
                tlTempData.ClearSorting();
                var qL3 = dtResult.AsEnumerable().Where(p => ProcessGeneral.GetSafeString(p["SortOrderNodeNew"]) != "");
                foreach (DataRow dr3 in qL3)
                {
                    dr3["SortOrderNode"] = dr3["SortOrderNodeNew"];
                }
                dtResult.Columns.Remove("SortOrderNodeNew");
                dtResult.AcceptChanges();

            }

          
            
            //tlTempData.Columns.Clear();
            //tlTempData.DataSource = null;
            //tlTempData.DataSource = dtResult;
            //tlTempData.ParentFieldName = "ParentPK";
            //tlTempData.KeyFieldName = "ChildPK";


            //tlTempData.BeginUpdate();
            //tlTempData.ExpandAll();
            //tlTempData.ForceInitialize();
            //tlTempData.BeginSort();
            //tlTempData.Columns["SortOrderNode"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            //tlTempData.EndSort();
            //tlTempData.EndUpdate();

            ctrlParent.Controls.Remove(panelControlTree);




            return dtResult;
        }

        private static void LoadDataTreeTest(TreeList tlTempData, DataTable dt)
        {
            tlTempData.Columns.Clear();
            tlTempData.DataSource = null;
            tlTempData.DataSource = dt;
            tlTempData.ParentFieldName = "ParentPK";
            tlTempData.KeyFieldName = "ChildPK";


            tlTempData.BeginUpdate();
            tlTempData.ExpandAll();
            tlTempData.ForceInitialize();
            tlTempData.BeginSort();
            tlTempData.Columns["SortOrderNode"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
            tlTempData.EndSort();
            tlTempData.EndUpdate();
        }

        public static void UpdateParentNameHistory(DataTable dtCal, ref DataTable dtInsUpd, List<Int64> lParentPk)
        {
         //   List<Int64> lParentPkNew = dtInsUpd.AsEnumerable().Where(p => p.Field<Int64>("ParentPK") > 0).Select(p => p.Field<Int64>("ParentPK")).Distinct().ToList();
            if (lParentPk.Count <= 0) return;
            var qParent = dtCal.AsEnumerable().Where(p => lParentPk.Any(t => p.Field<Int64>("PK") == t)).Select(p => new
            {
                PK = p.Field<Int64>("PK"),
                RMDescription_002 = GetParentNameF1(p.Field<String>("RMDescription_Fi"), p.Field<String>("PosDesc_Fi")),
                ParentPKNew = p.Field<Int64>("ParentPK"),
            }).ToList();
            if (qParent.Count <= 0) return;
            var q2 = dtInsUpd.AsEnumerable().Join(qParent, p => p.Field<Int64>("ParentPK"), t => t.PK, (p, t) => new
            {
                SourceRow = p,
                t.ParentPKNew,
                RMDescription_002New = GetParentNameF2(p.Field<String>("RMDescription_Fi"), t.RMDescription_002),
            }).ToList();
            if (!q2.Any()) return;
            List<Int64> lParentPkNew = new List<Int64>();
    
            foreach (var itemQ2 in q2)
            {
                DataRow drQ2 = itemQ2.SourceRow;
                drQ2["RMDescription_Fi"] = itemQ2.RMDescription_002New;
                drQ2["ParentPK"] = itemQ2.ParentPKNew;
                lParentPkNew.Add(itemQ2.ParentPKNew);
            }
            dtInsUpd.AcceptChanges();
            lParentPkNew = lParentPkNew.Where(p => p > 0).Distinct().ToList();
            if (lParentPkNew.Count <= 0) return;
            UpdateParentNameHistory(dtCal, ref dtInsUpd, lParentPkNew);


         
        }

        private static string GetParentNameF2(string rmNameOld, string rmNameNew)
        {
            if (string.IsNullOrEmpty(rmNameOld))
                return "";
            if (string.IsNullOrEmpty(rmNameNew))
                return rmNameOld;
            return string.Format("{0} - {1}", rmNameOld, rmNameNew);
        }
        private static string GetParentNameF1(string rmNameNew, string posName)
        {
            if (string.IsNullOrEmpty(rmNameNew))
                return "";
            if (string.IsNullOrEmpty(posName))
                return string.Format("({0})", rmNameNew);
            return string.Format("({0} - {1})", rmNameNew, posName);
        }
























        public static DataTable StandardTablePrintInsUpdH(DataTable dtPara, int passCol)
        {

            
            const int beginCol = 4;
            //const int passCol = 5;
            dtPara.Columns.Add("ParentItem", typeof(string));
            dtPara.Columns.Add("PKItem", typeof(Int64));
            passCol = passCol + 1;
            int colNo = dtPara.Columns.Count;
         //   DataTable dtReturn = dtPara.Clone();
            if (dtPara.Rows.Count > 0)
            {
                Dictionary<Int64,Int32> dic = dtPara.AsEnumerable().Select(p => p.Field<Int64>("PK")).Distinct().Select((p, ind) => new
                {
                    PK = p,
                    Index = ind + 1
                }).ToDictionary(item => item.PK, item => item.Index);
                foreach (DataRow dr in dtPara.Rows)
                {
                    for (int i = beginCol; i < colNo - passCol; i++)
                    {
                        string value = ProcessGeneral.GetSafeString(dr[i]);
                        int lastIndex = value.LastIndexOf("-", StringComparison.Ordinal);
                        if (lastIndex >= 0)
                        {


                            string tempValue = value.SubStringNew(0, lastIndex);
                            string colAtt = dtPara.Columns[i].ColumnName;
                            if (colAtt.IndexOf("#", StringComparison.Ordinal) > 0)
                            {
                                dr[i] = tempValue;
                            }
                            else
                            {
                                string tempUnit = value.SubStringNew(lastIndex + 1, value.Length - (lastIndex + 1));
                                dr[i] = string.Format("{0}{1}", tempValue, tempUnit);
                            }

                        }
                    }
                    Int64 pk = ProcessGeneral.GetSafeInt64(dr["PK"]);
                    dr["PKItem"] = dr["PK"];
                    dr["PK"] = dic[pk];
                    dr["ParentItem"] = "";
                    //  dtReturn.ImportRow(dr);
                }
                dtPara.AcceptChanges();
            }
            return dtPara;
        }

        public static DataTable StandardTablePrintInsUpd(DataTable dtPara, int passCol)
        {


            const int beginCol = 4;
            //const int passCol = 5;
            dtPara.Columns.Add("ParentItem", typeof(string));
            dtPara.Columns.Add("PKItem", typeof(Int64));
            passCol = passCol + 1;
            int colNo = dtPara.Columns.Count;
            //   DataTable dtReturn = dtPara.Clone();
            if (dtPara.Rows.Count > 0)
            {
                Dictionary<Int64, Int32> dic = dtPara.AsEnumerable().Select(p => p.Field<Int64>("PK")).Distinct().Select((p, ind) => new
                {
                    PK = p,
                    Index = ind + 1
                }).ToDictionary(item => item.PK, item => item.Index);
                foreach (DataRow dr in dtPara.Rows)
                {
                    for (int i = beginCol; i < colNo - passCol; i++)
                    {
                       
                        //if(colAtt.IndexOf("#"))
                        string value = ProcessGeneral.GetSafeString(dr[i]);
                        int lastIndex = value.LastIndexOf("-", StringComparison.Ordinal);
                        if (lastIndex >= 0)
                        {
                            string tempValue = value.SubStringNew(0, lastIndex);
                            string tempUnit = value.SubStringNew(lastIndex + 1, value.Length - (lastIndex + 1));
                            dr[i] = string.Format("{0}{1}", tempValue, tempUnit);
                        }




                    }
                    Int64 pk = ProcessGeneral.GetSafeInt64(dr["PK"]);
                    dr["PKItem"] = dr["PK"];
                    dr["PK"] = dic[pk];
                    dr["ParentItem"] = "";
                    //  dtReturn.ImportRow(dr);
                }
                dtPara.AcceptChanges();
            }
            return dtPara;
        }

        public static DataTable StandardDataBoMPrintRmOnly(DataTable dtPara, List<string> lPara , int layout, string fieldNameAdd, Dictionary<string, int> dicGlobal)
        {

            if (dtPara.Rows.Count <= 0) return dtPara;
            Int64 childPkNew = dtPara.AsEnumerable().Select(p => p.Field<Int64>("ChildPK")).Max() + 10;
            var arrFieldName = dtPara.Columns.Cast<DataColumn>().Where(p => p.ColumnName != fieldNameAdd).Select(p => new
            {
                p.ColumnName,
                ContainPoint = p.ColumnName.Contains("^^^^^")
            }).ToList();
            List<DataRow> qRmCode;
            // if(!qRmCode.Any()) return dtPara;

            if (layout == 0)
            {
                qRmCode = dtPara.AsEnumerable().Where(p => p.Field<string>("ItemType") == "R").Where(p => lPara.Any(t => t == p.Field<string>("MainMaterialGroup"))).ToList();
            }
            else
            {
                qRmCode = dtPara.AsEnumerable().Where(p => lPara.Any(t => t == p.Field<string>("MainMaterialGroup"))).ToList();
            }



            Dictionary<string, Int64> arrMainMaterial = qRmCode.Select(p => p.Field<string>("MainMaterialGroup"))
                .Distinct().Join(dicGlobal, p => p, t => t.Key, (p, t) => new
                {
                    MainMaterialGroup = p,
                    SortIndex = t.Value
                }).OrderBy(p=>p.SortIndex)
                .Select((p, idx) => new
                {
                    Index = idx + childPkNew,
                    p.MainMaterialGroup
                }).ToDictionary(item => item.MainMaterialGroup, item => item.Index);



            DataTable dtResult = dtPara.Clone();
            if (!arrMainMaterial.Any()) return dtResult;
            foreach (var item in arrMainMaterial)
            {
                DataRow dr = dtResult.NewRow();
                dr["MainMaterialGroup"] = item.Key;
                dr["ChildPK"] = item.Value;
                dr["ParentPK"] = 0;
                dr["ChildPKNew"] = item.Value;
                dr["ParentPKNew"] = 0;
                dr["SortOrderNode"] = item.Value;
                dtResult.Rows.Add(dr);
            }
            foreach (DataRow drAddTemp in qRmCode)
            {
                DataRow drAdd = dtResult.NewRow();
                string mainGroup = ProcessGeneral.GetSafeString(drAddTemp["MainMaterialGroup"]);

                double value = 0;
                foreach (var itemT in arrFieldName)
                {
                    string sCol = itemT.ColumnName;
                    bool containPoint = itemT.ContainPoint;
                    drAdd[sCol] = drAddTemp[sCol];
                    if (fieldNameAdd != "" && containPoint)
                    {
                        value += ProcessGeneral.GetSafeDouble(drAddTemp[sCol]);
                    }
                }
                drAdd["ParentPK"] = arrMainMaterial[mainGroup];
                drAdd["MainMaterialGroup"] = "";
                if (fieldNameAdd != "")
                {
                    drAdd[fieldNameAdd] = value;
                }
                dtResult.Rows.Add(drAdd);
            }
            dtResult.AcceptChanges();
            return dtResult;
           
        }

        public static DataTable StandardDataBoMPrintRmOnly(DataTable dtPara, List<string> lPara, string fieldNameAdd, List<string> lMode, Dictionary<string, int> dicGlobal)
        {

            //if (dtPara.Rows.Count <= 0) return dtPara;
            
            //var arrFieldName = dtPara.Columns.Cast<DataColumn>().Where(p => p.ColumnName != fieldNameAdd).Select(p => new
            //{
            //    p.ColumnName,
            //    ContainPoint = p.ColumnName.Contains("^^^^^")
            //}).ToList();

            //// if(!qRmCode.Any()) return dtPara;


            //List<DataRow> qRmCode = dtPara.AsEnumerable().Where(p => lPara.Any(t => t == p.Field<string>("MainMaterialGroup"))).Where(p => lMode.Any(t => t == p.Field<string>("PurchaseType"))).ToList();




            //DataTable dtResult = dtPara.Clone();
       
            //foreach (DataRow drAddTemp in qRmCode)
            //{
            //    DataRow drAdd = dtResult.NewRow();


            //    double value = 0;
            //    foreach (var itemT in arrFieldName)
            //    {
            //        string sCol = itemT.ColumnName;
            //        bool containPoint = itemT.ContainPoint;
            //        drAdd[sCol] = drAddTemp[sCol];
            //        if (fieldNameAdd != "" && containPoint)
            //        {
            //            value += ProcessGeneral.GetSafeDouble(drAddTemp[sCol]);
            //        }
            //    }
            //    if (fieldNameAdd != "")
            //    {
            //        drAdd[fieldNameAdd] = value;
            //    }
            //    dtResult.Rows.Add(drAdd);
            //}
            //dtResult.AcceptChanges();
            //return dtResult;




            if (dtPara.Rows.Count <= 0) return dtPara;
            Int64 childPkNew = dtPara.AsEnumerable().Select(p => p.Field<Int64>("ChildPK")).Max() + 10;
            var arrFieldName = dtPara.Columns.Cast<DataColumn>().Where(p => p.ColumnName != fieldNameAdd).Select(p => new
            {
                p.ColumnName,
                ContainPoint = p.ColumnName.Contains("^^^^^")
            }).ToList();
            List<DataRow> qRmCode = dtPara.AsEnumerable().Where(p => lPara.Any(t => t == p.Field<string>("MainMaterialGroup"))).Where(p => lMode.Any(t => t == p.Field<string>("PurchaseType"))).ToList();
            // if(!qRmCode.Any()) return dtPara;

           


            Dictionary<string, Int64> arrMainMaterial = qRmCode.Select(p => p.Field<string>("MainMaterialGroup"))
                .Distinct().Join(dicGlobal, p => p, t => t.Key, (p, t) => new
                {
                    MainMaterialGroup = p,
                    SortIndex = t.Value
                }).OrderBy(p => p.SortIndex)
                .Select((p, idx) => new
                {
                    Index = idx + childPkNew,
                    p.MainMaterialGroup
                }).ToDictionary(item => item.MainMaterialGroup, item => item.Index);



            DataTable dtResult = dtPara.Clone();
            if (!arrMainMaterial.Any()) return dtResult;
            foreach (var item in arrMainMaterial)
            {
                DataRow dr = dtResult.NewRow();
                dr["MainMaterialGroup"] = item.Key;
                dr["RMName"] = item.Key;
                dr["RMNameT"] = item.Key;
                dr["ChildPK"] = item.Value;
                dr["ParentPK"] = 0;
                dr["ChildPKNew"] = item.Value;
                dr["ParentPKNew"] = 0;
                dr["SortOrderNode"] = item.Value;
                dtResult.Rows.Add(dr);
            }
            foreach (DataRow drAddTemp in qRmCode)
            {
                DataRow drAdd = dtResult.NewRow();
                string mainGroup = ProcessGeneral.GetSafeString(drAddTemp["MainMaterialGroup"]);

                double value = 0;
                foreach (var itemT in arrFieldName)
                {
                    string sCol = itemT.ColumnName;
                    bool containPoint = itemT.ContainPoint;
                    drAdd[sCol] = drAddTemp[sCol];
                    if (fieldNameAdd != "" && containPoint)
                    {
                        value += ProcessGeneral.GetSafeDouble(drAddTemp[sCol]);
                    }
                }
                drAdd["ParentPK"] = arrMainMaterial[mainGroup];
                drAdd["MainMaterialGroup"] = "";
                if (fieldNameAdd != "")
                {
                    drAdd[fieldNameAdd] = value;
                }
                dtResult.Rows.Add(drAdd);
            }
            dtResult.AcceptChanges();
            return dtResult;





            //foreach (DataRow drAdd in dtPara.Rows)
            //{



            //    double value = 0;
            //    foreach (var itemT in arrFieldName)
            //    {
            //        string sCol = itemT.ColumnName;
            //        bool containPoint = itemT.ContainPoint;
            //        drAdd[sCol] = drAdd[sCol];
            //        if (fieldNameAdd != "" && containPoint)
            //        {
            //            value += ProcessGeneral.GetSafeDouble(drAdd[sCol]);
            //        }
            //    }

            //    if (fieldNameAdd != "")
            //    {
            //        drAdd[fieldNameAdd] = value;
            //    }
            //}
            //dtPara.AcceptChanges();
            //return dtPara;

        }




        public static DataTable StandardDataAmountSummaryPrint(DataTable dtPara, List<string> lPara)
        {

            if (dtPara.Rows.Count <= 0) return dtPara;
            if(lPara.Count <= 0) return dtPara;
            List<DataRow> qRmCode = dtPara.AsEnumerable().Where(p => lPara.Any(t => t == p.Field<string>("MainMaterialGroup"))).ToList();
            if(!qRmCode.Any()) return dtPara;
            return qRmCode.CopyToDataTable();
        }







        private static Dictionary<Int64, string> LoadOpByReport(DataTable dtP)
        {
            AccessData ac = new AccessData(DeclareSystem.SysConnectionString);
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtP };
            DataTable dt= ac.TblReadDataSP("sp_BOM_LoadOPByReport", arrpara);
            Dictionary<Int64,string> q1 = dt.AsEnumerable().GroupBy(p => p.Field<Int64>("CNY00016PK")).Select(t => new
            {
                CNY00016PK = t.Key,
                OPDesc = string.Join(", ",
                    t.Select(s => s.Field<string>("OPDesc")).Distinct().OrderBy(m => m).ToArray())
            }).ToDictionary(s => s.CNY00016PK, s => s.OPDesc);
            return q1;
        }

        public static DataTable StandardDataBoMPrintNormal(DataTable dtPara)
        {

            var q1 = dtPara.AsEnumerable().Where(p => p.Field<String>("ItemType") != "R").Select(p => new
            {
                PK = p.Field<Int64>("ChildPK")
            }).Distinct().ToList();
            if (!q1.Any()) return dtPara;

            Dictionary<Int64, string> dicOp = LoadOpByReport(q1.CopyToDataTableNew());
            if (dicOp.Count <= 0) return dtPara;

            var q2 = dtPara.AsEnumerable().Join(dicOp, p => p.Field<Int64>("ChildPK"), t => t.Key, (p, t) => new
            {
                p,
                t.Value
            }).ToList();
            if (q2.Count <= 0) return dtPara;
            foreach (var item in q2)
            {
                item.p["OPName"] = item.Value;
            }


            dtPara.AcceptChanges();
            return dtPara;

        }


    }
}
