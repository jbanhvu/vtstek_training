using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNY_BaseSys.Class;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList;

namespace CNY_BaseSys.Common
{
    public class CheckDataChangeOnControl<T1, T2, T3, T4>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        List<ControlCheckInfo> _lCtrl = new List<ControlCheckInfo>();
        List<List<T1>> _lDelT1 = new List<List<T1>>();
        List<List<T2>> _lDelT2 = new List<List<T2>>();
        List<List<T3>> _lDelT3 = new List<List<T3>>();
        List<List<T4>> _lDelT4 = new List<List<T4>>();
        List<Dictionary<T1, List<BoMInputAttInfo>>> _lAttValueT1 = new List<Dictionary<T1, List<BoMInputAttInfo>>>();
        List<Dictionary<T2, List<BoMInputAttInfo>>> _lAttValueT2 = new List<Dictionary<T2, List<BoMInputAttInfo>>>();
        List<Dictionary<T3, List<BoMInputAttInfo>>> _lAttValueT3 = new List<Dictionary<T3, List<BoMInputAttInfo>>>();
        List<Dictionary<T4, List<BoMInputAttInfo>>> _lAttValueT4 = new List<Dictionary<T4, List<BoMInputAttInfo>>>();

        List<Dictionary<T1, DataTable>> _lTableT1 = new List<Dictionary<T1, DataTable>>();
        List<Dictionary<T2, DataTable>> _lTableT2 = new List<Dictionary<T2, DataTable>>();
        List<Dictionary<T3, DataTable>> _lTableT3 = new List<Dictionary<T3, DataTable>>();
        List<Dictionary<T4, DataTable>> _lTableT4 = new List<Dictionary<T4, DataTable>>();


        public void LoadControl(params Control[] arrCtrl)
        {
            _lCtrl.Clear();
            foreach (Control control in arrCtrl)
            {

                if (control == null) continue;
                if ((control is TreeList) || (control is GridControl))
                {
                    control.Tag = "";
                    _lCtrl.Add(new ControlCheckInfo
                    {
                        CtrEdit = control
                    });
                    continue;

                }


                if (control is BaseEdit)
                {
                    BaseEdit txtText = (BaseEdit)control;
                    _lCtrl.Add(new ControlCheckInfo
                    {
                        CtrEdit = txtText,
                        OldValue = txtText.EditValue

                    });
                }
            }

        }



        public void LoadListTable<T>(params Dictionary<T, DataTable>[] lTable) where T : struct
        {

            if (lTable.Length <= 0) return;
            Type type = typeof(T);
            if (type == typeof(T1))
            {
                _lTableT1.Clear();
                foreach (Dictionary<T, DataTable> dicTbl1 in lTable)
                {

                    _lTableT1.Add(dicTbl1 as Dictionary<T1, DataTable>);
                }
                return;
            }


            if (type == typeof(T2))
            {
                _lTableT2.Clear();
                foreach (Dictionary<T, DataTable> dicTbl2 in lTable)
                {

                    _lTableT2.Add(dicTbl2 as Dictionary<T2, DataTable>);
                }
                return;
            }

            if (type == typeof(T3))
            {
                _lTableT3.Clear();
                foreach (Dictionary<T, DataTable> dicTbl3 in lTable)
                {

                    _lTableT3.Add(dicTbl3 as Dictionary<T3, DataTable>);
                }
                return;
            }


            if (type == typeof(T4))
            {
                _lTableT4.Clear();
                foreach (Dictionary<T, DataTable> dicTbl4 in lTable)
                {

                    _lTableT4.Add(dicTbl4 as Dictionary<T4, DataTable>);
                }
                return;
            }
        }

        public void LoadListDel<T>(params List<T>[] lDelList) where T : struct
        {

            if (lDelList.Length <= 0) return;
            Type type = typeof(T);
            if (type == typeof(T1))
            {
                _lDelT1.Clear();
                foreach (List<T> lObject1 in lDelList)
                {
                    _lDelT1.Add(lObject1 as List<T1>);

                }

                return;
            }


            if (type == typeof(T2))
            {
                _lDelT2.Clear();
                foreach (List<T> lObject2 in lDelList)
                {
                    _lDelT2.Add(lObject2 as List<T2>);

                }

                return;
            }


            if (type == typeof(T3))
            {
                _lDelT3.Clear();
                foreach (List<T> lObject3 in lDelList)
                {
                    _lDelT3.Add(lObject3 as List<T3>);

                }

                return;
            }


            if (type == typeof(T4))
            {
                _lDelT4.Clear();
                foreach (List<T> lObject4 in lDelList)
                {
                    _lDelT4.Add(lObject4 as List<T4>);

                }

                return;
            }
        }

        public void LoadListAtt<T>(params Dictionary<T, List<BoMInputAttInfo>>[] lAtt) where T : struct
        {
            if (lAtt.Length <= 0) return;
            Type type = typeof(T);
            if (type == typeof(T1))
            {
                _lAttValueT1.Clear();
                foreach (Dictionary<T, List<BoMInputAttInfo>> dicAtt1 in lAtt)
                {

                    _lAttValueT1.Add(dicAtt1 as Dictionary<T1, List<BoMInputAttInfo>>);
                }
                return;
            }

            if (type == typeof(T2))
            {
                _lAttValueT2.Clear();
                foreach (Dictionary<T, List<BoMInputAttInfo>> dicAtt2 in lAtt)
                {

                    _lAttValueT2.Add(dicAtt2 as Dictionary<T2, List<BoMInputAttInfo>>);
                }
                return;
            }

            if (type == typeof(T3))
            {
                _lAttValueT3.Clear();
                foreach (Dictionary<T, List<BoMInputAttInfo>> dicAtt3 in lAtt)
                {

                    _lAttValueT3.Add(dicAtt3 as Dictionary<T3, List<BoMInputAttInfo>>);
                }
                return;
            }

            if (type == typeof(T4))
            {
                _lAttValueT4.Clear();
                foreach (Dictionary<T, List<BoMInputAttInfo>> dicAtt4 in lAtt)
                {

                    _lAttValueT4.Add(dicAtt4 as Dictionary<T4, List<BoMInputAttInfo>>);
                }
                return;
            }


        }

        private bool ChecklistDel()
        {
            if (_lDelT1.Any(p => p.Count > 0)) return true;
            if (_lDelT2.Any(p => p.Count > 0)) return true;
            if (_lDelT3.Any(p => p.Count > 0)) return true;
            if (_lDelT4.Any(p => p.Count > 0)) return true;
            return false;
        }

        private bool ChecklistAtt()
        {
            foreach (Dictionary<T1, List<BoMInputAttInfo>> dicAtt1 in _lAttValueT1)
            {
                foreach (var dicItem1 in dicAtt1)
                {
                    List<BoMInputAttInfo> lInfo1 = dicItem1.Value;
                    if (lInfo1.Any(p => p.RowState != DataStatus.Unchange)) return true;
                }
            }
            foreach (Dictionary<T2, List<BoMInputAttInfo>> dicAtt2 in _lAttValueT2)
            {
                foreach (var dicItem2 in dicAtt2)
                {
                    List<BoMInputAttInfo> lInfo2 = dicItem2.Value;
                    if (lInfo2.Any(p => p.RowState != DataStatus.Unchange)) return true;
                }
            }
            foreach (Dictionary<T3, List<BoMInputAttInfo>> dicAtt3 in _lAttValueT3)
            {
                foreach (var dicItem3 in dicAtt3)
                {
                    List<BoMInputAttInfo> lInfo3 = dicItem3.Value;
                    if (lInfo3.Any(p => p.RowState != DataStatus.Unchange)) return true;
                }
            }
            foreach (Dictionary<T4, List<BoMInputAttInfo>> dicAtt4 in _lAttValueT4)
            {
                foreach (var dicItem4 in dicAtt4)
                {
                    List<BoMInputAttInfo> lInfo4 = dicItem4.Value;
                    if (lInfo4.Any(p => p.RowState != DataStatus.Unchange)) return true;
                }
            }
            return false;
        }



        private bool ChecklistTable()
        {
            foreach (Dictionary<T1, DataTable> dicTbl1 in _lTableT1)
            {
                foreach (var lInfo1 in dicTbl1)
                {
                    DataTable dt1 = lInfo1.Value;
                    if (dt1.Columns.Cast<DataColumn>().Any(p => p.ColumnName == "RowState"))
                    {
                        if (dt1.AsEnumerable().Any(p => p.Field<string>("RowState") != DataStatus.Unchange.ToString())) return true;
                    }
                    else
                    {
                        if (dt1.AsEnumerable().Any(p => p.RowState != DataRowState.Unchanged)) return true;
                    }

                }
            }


            foreach (Dictionary<T2, DataTable> dicTbl2 in _lTableT2)
            {
                foreach (var lInfo2 in dicTbl2)
                {
                    DataTable dt2 = lInfo2.Value;
                    if (dt2.Columns.Cast<DataColumn>().Any(p => p.ColumnName == "RowState"))
                    {
                        if (dt2.AsEnumerable().Any(p => p.Field<string>("RowState") != DataStatus.Unchange.ToString())) return true;
                    }
                    else
                    {
                        if (dt2.AsEnumerable().Any(p => p.RowState != DataRowState.Unchanged)) return true;
                    }

                }
            }


            foreach (Dictionary<T3, DataTable> dicTbl3 in _lTableT3)
            {
                foreach (var lInfo3 in dicTbl3)
                {
                    DataTable dt3 = lInfo3.Value;
                    if (dt3.Columns.Cast<DataColumn>().Any(p => p.ColumnName == "RowState"))
                    {
                        if (dt3.AsEnumerable().Any(p => p.Field<string>("RowState") != DataStatus.Unchange.ToString())) return true;
                    }
                    else
                    {
                        if (dt3.AsEnumerable().Any(p => p.RowState != DataRowState.Unchanged)) return true;
                    }

                }
            }


            foreach (Dictionary<T4, DataTable> dicTbl4 in _lTableT4)
            {
                foreach (var lInfo4 in dicTbl4)
                {
                    DataTable dt4 = lInfo4.Value;
                    if (dt4.Columns.Cast<DataColumn>().Any(p => p.ColumnName == "RowState"))
                    {
                        if (dt4.AsEnumerable().Any(p => p.Field<string>("RowState") != DataStatus.Unchange.ToString())) return true;
                    }
                    else
                    {
                        if (dt4.AsEnumerable().Any(p => p.RowState != DataRowState.Unchanged)) return true;
                    }

                }
            }

            return false;
        }


        public bool CheckControlIsChangedValue()
        {
            if (ChecklistDel()) return true;
            if (ChecklistAtt()) return true;
            if (ChecklistTable()) return true;




            foreach (ControlCheckInfo item in _lCtrl)
            {
                Control control = item.CtrEdit;

                if ((control is TreeList) || (control is GridControl))
                {
                    DataTable dtS;
                    if (control is TreeList)
                    {
                        TreeList tl = (TreeList)control;
                        dtS = (DataTable)tl.DataSource;
                    }
                    else
                    {
                        GridControl gc = (GridControl)control;
                        dtS = (DataTable)gc.DataSource;
                    }

                    if (dtS != null)
                    {

                        if (dtS.Columns.Cast<DataColumn>().Any(p => p.ColumnName == "RowState"))
                        {
                            if (dtS.AsEnumerable().Any(p => p.Field<string>("RowState") != DataStatus.Unchange.ToString())) return true;
                        }
                        else
                        {
                            if (dtS.AsEnumerable().Any(p => p.RowState != DataRowState.Unchanged)) return true;
                        }
                    }
                    continue;
                }

                object oldValue = item.OldValue;
                if (control is BaseEdit)
                {
                    BaseEdit txtText = (BaseEdit)control;
                    object newValue = txtText.EditValue;

                    if (newValue == null && oldValue == null) continue;

                    if (newValue == null)
                    {
                        if (ProcessGeneral.GetSafeString(oldValue) != "")
                            return true;
                    }
                    else if (oldValue == null)
                    {
                        if (ProcessGeneral.GetSafeString(newValue) != "")
                            return true;
                    }
                    else
                    {
                        
                        if (!oldValue.Equals(newValue))
                            return true;
                    }

                }
            }
            return false;
        }





    }

}
