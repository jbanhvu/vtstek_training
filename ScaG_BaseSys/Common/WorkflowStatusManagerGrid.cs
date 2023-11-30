using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace CNY_BaseSys.Common
{
   
    public class WorkflowStatusManagerGrid 
    {
        private Dictionary<string, string> _dicAdvanceFuction;

        private readonly string _userName;
        private PermissionFormInfo _perInfo;
        public PermissionFormInfo PerInfo
        {
            get { return _perInfo; }
            set
            {
                _perInfo = value;
                _dicAdvanceFuction = _perInfo.DtAdvanceFunc.AsEnumerable().Select(t => t.Field<string>("CodePer").Trim()).Where(t => !string.IsNullOrEmpty(t)).Distinct().ToDictionary(t => t, t => t);
            }
        }


        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
            }
        }
        private Tuple<string, string, string>[] _arrIndexStatus;

        public Tuple<string, string, string>[] ArrIndexStatus
        {
            get { return this._arrIndexStatus; }
            set { this._arrIndexStatus = value; }
        }
        private readonly DataTable _dtStatus;

        public DataTable DtStatus
        {
            get { return this._dtStatus; }
        }


        private readonly AccessData _ac;



     

        private bool _allowReviseAllStatus = false;

        public bool AllowReviseAllStatus
        {
            get { return this._allowReviseAllStatus; }
            set { this._allowReviseAllStatus = value; }
        }


        public WorkflowStatusManagerGrid(string userName, string module, string connectString)
        {
            _dicAdvanceFuction = new Dictionary<string, string>();
            _ac = new AccessData(connectString);
            this._userName = userName;
            _dtStatus = TableStatusByModule(module);
            // ConstSystemStatus.STATUS_APPROVE

        }


        private DataTable TableTempStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CodePer", typeof(string));
            dt.Columns.Add("DescPer", typeof(string));
            return dt;
        }

        private DataTable TableStatusByModule(string module)
        {
            return _ac.TblReadDataSQL(string.Format("SELECT LTRIM(RTRIM([CNY001])) AS CodePer,LTRIM(RTRIM(CNY002)) as DescPer FROM [dbo].[CNYMF015] WHERE [CNY004]='{0}' ORDER BY CodePer", module), null);
        }

        public bool CheckStatusExistsInTable(DataTable dtStatusO , string statusO)
        {
            return dtStatusO.AsEnumerable().Any(p => p.Field<string>("CodePer") == statusO);
        }
        public DataTable GetTableStatus()
        {


            if (_userName == "ADMIN")
            {
                return _dtStatus;
            }

            if (!_dicAdvanceFuction.ContainsKey(_status))
            {
                return TableTempStatus();
            }
          

            int indexFind = -1;
            int count = _arrIndexStatus.Length;


            for (int i = 0; i < count; i++)
            {
                if (_arrIndexStatus[i].Item1 == _status)
                {
                    indexFind = i;
                    break;
                }
            }

            if (indexFind < 0) return TableTempStatus();

            string nextStatus = "";
           // string beforStatus = "";

            if (indexFind == 0)
            {
               // beforStatus = "";
                nextStatus = count == 1 ? "" : _arrIndexStatus[indexFind + 1].Item1;
            }
            else if (indexFind == count - 1)
            {
               // beforStatus = _arrIndexStatus[indexFind - 1].Item1;
                nextStatus = "";
            }
            else
            {
               // beforStatus = _arrIndexStatus[indexFind - 1].Item1;
                nextStatus = _arrIndexStatus[indexFind + 1].Item1;
            }
            List<string> l = new List<string>();
            //if (!string.IsNullOrEmpty(beforStatus) && _dicAdvanceFuction.ContainsKey(_status))
            //{
            //    l.Add(beforStatus);
            //}


            for (int i = 0; i < indexFind; i++)
            {
                l.Add(_arrIndexStatus[i].Item1);
            }

            l.Add(_status);
            if (!string.IsNullOrEmpty(nextStatus))
            {
                l.Add(nextStatus);
            }


            var qPer = l.Where(p => _dicAdvanceFuction.ContainsKey(p)).ToList();
            if (!qPer.Any()) return TableTempStatus();
            var qFinal = _dtStatus.AsEnumerable().Where(p => qPer.Contains(p.Field<string>("CodePer"))).ToList();
            if (!qFinal.Any()) return TableTempStatus();
            return qFinal.CopyToDataTable();
        }


        public bool CheckActionNextStatus(string nextStatus)
        {
            if (_userName == "ADMIN")
            {
                return true;
            }

            return _dicAdvanceFuction.ContainsKey(nextStatus);
        }
        public bool SetTextAfterChangeStatus(string newStatus ,out Dictionary<string,string> dicValue)
        {
            dicValue = new Dictionary<string, string>();
            if (newStatus == _status) return false;
            int indexFindNew = -1;
            int indexFindOld = -1;
            int count = _arrIndexStatus.Length;
            for (int i = 0; i < count; i++)
            {
                string item1 = _arrIndexStatus[i].Item1;
                if (item1 == newStatus)
                {
                    indexFindNew = i;
                }

                if (item1 == _status)
                {
                    indexFindOld = i;
                }
            }
            Tuple<string, string, string> tuple1;
            if (indexFindNew < 0 && indexFindOld < 0)
            {
                foreach (var itemT in _arrIndexStatus)
                {
                    tuple1 = itemT;
                    if (!string.IsNullOrEmpty(tuple1.Item2))
                    {
                        //tuple1.Item2.EditValue = @"N/A";
                        if (!dicValue.ContainsKey(tuple1.Item2))
                            dicValue.Add(tuple1.Item2, @"N/A");
                        else
                            dicValue[tuple1.Item2] = @"N/A";
                    }

                    if (!string.IsNullOrEmpty(tuple1.Item3))
                    {
                        //tuple1.Item3.EditValue = "";
                        if (!dicValue.ContainsKey(tuple1.Item3))
                            dicValue.Add(tuple1.Item3, @"01/01/1900");
                        else
                            dicValue[tuple1.Item3] = @"01/01/1900";
                    }
                }
                _status = newStatus;
                return true;
            }
            string dDate = ProcessGeneral.GetServerDate().ToString(ConstSystem.SysDateFormat);
            if (indexFindNew > indexFindOld)
            {
                if (indexFindOld < 0)
                    indexFindOld = 0;
                else
                    indexFindOld++;
                for (int j = indexFindOld; j <= indexFindNew; j++)
                {
                    tuple1 = _arrIndexStatus[j];
                    if (!string.IsNullOrEmpty(tuple1.Item2))
                    {
                        // tuple1.Item2.EditValue = _userName;
                        if (!dicValue.ContainsKey(tuple1.Item2))
                            dicValue.Add(tuple1.Item2, _userName);
                        else
                            dicValue[tuple1.Item2] = _userName;
                    }

                    if (!string.IsNullOrEmpty(tuple1.Item3))
                    {
                        //tuple1.Item3.EditValue = dDate;
                        if (!dicValue.ContainsKey(tuple1.Item3))
                            dicValue.Add(tuple1.Item3, dDate);
                        else
                            dicValue[tuple1.Item3] = dDate;
                    }
                }
                if (indexFindNew == count - 1)
                {
                    tuple1 = count == 1 ? _arrIndexStatus[indexFindNew] : _arrIndexStatus[indexFindNew - 1];
                    if (!string.IsNullOrEmpty(tuple1.Item2))
                    {
                        // tuple1.Item2.EditValue = @"N/A";
                        if (!dicValue.ContainsKey(tuple1.Item2))
                            dicValue.Add(tuple1.Item2, @"N/A");
                        else
                            dicValue[tuple1.Item2] = @"N/A";
                    }

                    if (!string.IsNullOrEmpty(tuple1.Item3))
                    {
                        // tuple1.Item3.EditValue = @"";
                        if (!dicValue.ContainsKey(tuple1.Item3))
                            dicValue.Add(tuple1.Item3, @"01/01/1900");
                        else
                            dicValue[tuple1.Item3] = @"01/01/1900";
                    }
                }
                _status = newStatus;
                return true;
            }

            bool setApprove = count - 2 == indexFindNew;
            if (indexFindNew < 0)
            {
                indexFindNew = 0;
            }
            else
            {
                indexFindNew++;
            }

            for (int k = indexFindOld; k >= indexFindNew; k--)
            {
                tuple1 = _arrIndexStatus[k];
                if (!string.IsNullOrEmpty(tuple1.Item2))
                {
                    // tuple1.Item2.EditValue = @"N/A";
                    if (!dicValue.ContainsKey(tuple1.Item2))
                        dicValue.Add(tuple1.Item2, @"N/A");
                    else
                        dicValue[tuple1.Item2] = @"N/A";
                }

                if (!string.IsNullOrEmpty(tuple1.Item3))
                {
                    // tuple1.Item3.EditValue = "";
                    if (!dicValue.ContainsKey(tuple1.Item3))
                        dicValue.Add(tuple1.Item3, @"01/01/1900");
                    else
                        dicValue[tuple1.Item3] = @"01/01/1900";
                }
            }

            if (indexFindNew == indexFindOld && setApprove)
            {
                tuple1 = _arrIndexStatus[indexFindNew - 1];
                if (!string.IsNullOrEmpty(tuple1.Item2))
                {
                    //  tuple1.Item2.EditValue = _userName;
                    if (!dicValue.ContainsKey(tuple1.Item2))
                        dicValue.Add(tuple1.Item2, _userName);
                    else
                        dicValue[tuple1.Item2] = _userName;
                }

                if (!string.IsNullOrEmpty(tuple1.Item3))
                {
                    //tuple1.Item3.EditValue = dDate;
                    if (!dicValue.ContainsKey(tuple1.Item3))
                        dicValue.Add(tuple1.Item3, dDate);
                    else
                        dicValue[tuple1.Item3] = dDate;
                }
            }


            _status = newStatus;
            return true;

        }

  



        public bool PerformRevision(out string statusOut, out Dictionary<string, string> dicValue)
        {
         
            statusOut = _status;
            dicValue = new Dictionary<string, string>();
            DialogResult dlResult = XtraMessageBox.Show("Do you want to revise??", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult != DialogResult.Yes) return false;
            int count = _arrIndexStatus.Length;
            if (count <= 0)
            {
                return false;
            }
            
            
            int indexFind = -1;


            for (int i = 0; i < count; i++)
            {
                if (_arrIndexStatus[i].Item1 == _status)
                {
                    indexFind = i;
                    break;
                }
            }

            if (indexFind < 0)
            {
                return false;
            }

            Tuple<string, string, string> tuple1;



            for (int j = 0; j < count; j++)
            {
                tuple1 = _arrIndexStatus[j];
                if (!string.IsNullOrEmpty(tuple1.Item2))
                {
                    // tuple1.Item2.EditValue = @"N/A";
                    if (!dicValue.ContainsKey(tuple1.Item2))
                        dicValue.Add(tuple1.Item2, @"N/A");
                    else
                        dicValue[tuple1.Item2] = @"N/A";
                }
                if (!string.IsNullOrEmpty(tuple1.Item3))
                {
                    // tuple1.Item3.EditValue = "";
                    if (!dicValue.ContainsKey(tuple1.Item3))
                        dicValue.Add(tuple1.Item3, @"01/01/1900");
                    else
                        dicValue[tuple1.Item3] = @"01/01/1900";
                }
            }

            statusOut = _arrIndexStatus[0].Item1;
            return true;





        }



        /*
     

    */



    }

 
}
