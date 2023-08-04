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

namespace CNY_BaseSys.Common
{
    public class WorkflowStatusManager : INotifyPropertyChanged
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
                _dicAdvanceFuction = _perInfo.DtAdvanceFunc.AsEnumerable().Select(t => t.Field<string>("CodePer").Trim()).Where(t=>!string.IsNullOrEmpty(t)).Distinct().ToDictionary(t => t, t => t);
            }
        }


        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }
        private Tuple<string, TextEdit, TextEdit> [] _arrIndexStatus;

        public Tuple<string, TextEdit, TextEdit>[] ArrIndexStatus
        {
            get { return this._arrIndexStatus; }
            set { this._arrIndexStatus = value; }
        }
        private readonly DataTable _dtStatus;

        public DataTable DtStatus
        {
            get { return this._dtStatus; }
        }


        private readonly  AccessData _ac;



        private string _option = "";

        public string Option
        {
            get { return this._option; }
            set { this._option = value; }
        }


        private bool _allowReviseAllStatus = false;

        public bool AllowReviseAllStatus
        {
            get { return this._allowReviseAllStatus; }
            set { this._allowReviseAllStatus = value; }
        }


        public WorkflowStatusManager(string userName, string module, string connectString)
        {
            _dicAdvanceFuction = new Dictionary<string, string>();
            _ac = new AccessData(connectString);
            this._userName = userName;
            _dtStatus = TableStatusByModule(module);
            // ConstSystemStatus.STATUS_APPROVE

        }
       

        private DataTable TableTempStatus()
        {
            DataTable dt  = new DataTable();
            dt.Columns.Add("CodePer", typeof(string));
            dt.Columns.Add("DescPer", typeof(string));
            return dt;
        }

        private DataTable TableStatusByModule(string module)
        {
            return _ac.TblReadDataSQL(string.Format("SELECT LTRIM(RTRIM([CNY001])) AS CodePer,LTRIM(RTRIM(CNY002)) as DescPer FROM [dbo].[CNYMF015] WHERE [CNY004]='{0}' ORDER BY CodePer", module), null);
        }

        public DataTable GetTableStatus()
        {


            if (_userName == "ADMIN")
            {
                return _dtStatus;
            }

            if (_option == "ADD")
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
            string beforStatus = "";
        
            if (indexFind == 0)
            {
                beforStatus = "";
                nextStatus = count == 1 ? "" : _arrIndexStatus[indexFind + 1].Item1;
            }
            else if (indexFind == count - 1)
            {
                beforStatus = _arrIndexStatus[indexFind -1].Item1;
                nextStatus = "";
            }
            else
            {
                beforStatus = _arrIndexStatus[indexFind - 1].Item1;
                nextStatus = _arrIndexStatus[indexFind + 1].Item1;
            }
            List<string> l = new List<string>();
            if (!string.IsNullOrEmpty(beforStatus) && _dicAdvanceFuction.ContainsKey(_status))
            {
                l.Add(beforStatus);
            }
            l.Add(_status);
            if (!string.IsNullOrEmpty(nextStatus))
            {
                l.Add(nextStatus);
            }


            var qPer = l.Where(p => _dicAdvanceFuction.Any(t => t.Key == p)).ToList();
            if (!qPer.Any()) return TableTempStatus();
            var qFinal = _dtStatus.AsEnumerable().Where(p => qPer.AsEnumerable().Any(t => t == p.Field<string>("CodePer"))).ToList();
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
        public bool SetTextAfterChangeStatus(string newStatus)
        {

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
            Tuple<string, TextEdit, TextEdit> tuple1;
            if (indexFindNew < 0 && indexFindOld < 0)
            {
                foreach (var itemT in _arrIndexStatus)
                {
                    tuple1 = itemT;
                    if (tuple1.Item2 != null)
                        tuple1.Item2.EditValue = @"N/A";
                    if (tuple1.Item3 != null)
                        tuple1.Item3.EditValue = "";
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
                    if (tuple1.Item2 != null)
                        tuple1.Item2.EditValue = _userName;
                    if (tuple1.Item3 != null)
                        tuple1.Item3.EditValue = dDate;
                }
                if (indexFindNew == count - 1)
                {
                    tuple1 = count == 1 ? _arrIndexStatus[indexFindNew] : _arrIndexStatus[indexFindNew - 1];
                    if (tuple1.Item2 != null)
                        tuple1.Item2.EditValue = @"N/A";
                    if (tuple1.Item3 != null)
                        tuple1.Item3.EditValue = @"";
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
                if (tuple1.Item2 != null)
                    tuple1.Item2.EditValue = @"N/A";
                if (tuple1.Item3 != null)
                    tuple1.Item3.EditValue = "";
            }

            if (indexFindNew == indexFindOld && setApprove)
            {
                tuple1 = _arrIndexStatus[indexFindNew - 1];
                if (tuple1.Item2 != null)
                    tuple1.Item2.EditValue = _userName;
                if (tuple1.Item3 != null)
                    tuple1.Item3.EditValue = dDate;
            }
         
             
            _status = newStatus;
            return true;
            
        }

        private bool _useSave = false;
        public bool UseSave
        {
            get { return this._useSave; }
            set { this._useSave = value; }
        }



        private bool _useRevision = false;
        public bool UseRevision
        {
            get { return this._useRevision; }
            set { this._useRevision = value; }
        }


        public void GetStatusSaveRevise()
        {
            if (DeclareSystem.SysUserName == "ADMIN")
            {
                _useSave = true;
                _useRevision = true;
                return;
            }

            if (_option == "")
            {
                _useRevision = false;
                _useSave = false;
                return;
            }
            int count = _arrIndexStatus.Length;
            string beginStatus = count <= 0 ? "" : _arrIndexStatus[0].Item1;

            if (_option == "ADD")
            {
                _useRevision = false;

                if (!_perInfo.PerIns)
                {
                    _useSave = false;
                }
                else
                {
                    _useSave = string.IsNullOrEmpty(_status) || _dicAdvanceFuction.ContainsKey(beginStatus);
                }
               
                return;
            }


            if (!_perInfo.PerUpd)
            {
                _useRevision = false;
                _useSave = false;
                return;
            }
          
            if (count <= 0)
            {
                _useRevision = false;
                _useSave = true;
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
                _useRevision = false;
                _useSave = false;
                return;
            }

            if (count == 1)
            {
                _useRevision = false;
                _useSave = true;
                return;
            }


            if (indexFind == count - 1)
            {
                _useSave = _dicAdvanceFuction.ContainsKey(_status);
                _useRevision = _dicAdvanceFuction.ContainsKey(beginStatus);
                return;
            }


            string nextStatus = "";
            if (count > 1)
            {
                nextStatus = _arrIndexStatus[indexFind + 1].Item1;
            }
            List<string> l = new List<string>();

            if (!string.IsNullOrEmpty(nextStatus) && nextStatus != _status)
            {
                l.Add(nextStatus);
            }
            if (!string.IsNullOrEmpty(_status))
                l.Add(_status);
            _useSave = l.Count > 0 && _dicAdvanceFuction.Any(p => l.Any(t => t == p.Key));





            if (indexFind > 0 && _status != beginStatus)
            {

                



                if (_allowReviseAllStatus && _dicAdvanceFuction.ContainsKey(beginStatus))
                {

                    //_useRevision = !_dicAdvanceFuction.ContainsKey(_status);
                    _useRevision = true;

                }
                else
                {

                    if (indexFind < count - 2 && _dicAdvanceFuction.ContainsKey(_status))
                    {
                        string beforStatus = _arrIndexStatus[indexFind - 1].Item1;
                        _useRevision = !_dicAdvanceFuction.ContainsKey(beforStatus);
                    }
                    else
                    {
                        _useRevision = false;
                    }



                }


            }
            else
            {
                _useRevision = false;
            }


           





        }


        public void PerformRevision(TextEdit txtVersion, TextEdit txtStatus)
        {
            DialogResult dlResult = XtraMessageBox.Show("Do you want to revise??", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlResult != DialogResult.Yes) return;
            int count = _arrIndexStatus.Length;
            _useSave = true;

            int version = ProcessGeneral.GetSafeInt(txtVersion.EditValue);
            if (_status == "4" || _status == "5" || _userName.ToUpper() == "ADMIN")
            {
                version = version + 1;
            }


            if (count <= 0)
            {
                txtVersion.EditValue = version;
                txtStatus.EditValue = @"";
                _useRevision = true;
                return;
            }
            _useRevision = _userName.ToUpper() == "ADMIN";


         

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

                txtStatus.EditValue = _arrIndexStatus[0].Item1;
                txtVersion.EditValue = version;
                return;
            }

            Tuple<string, TextEdit, TextEdit> tuple1;
            if (indexFind == count - 1 || indexFind == 0)
            {
                for (int j = 0; j < count; j++)
                {
                    tuple1 = _arrIndexStatus[j];
                    if (tuple1.Item2 != null)
                        tuple1.Item2.EditValue = @"N/A";
                    if (tuple1.Item3 != null)
                        tuple1.Item3.EditValue = "";
                }

                txtStatus.EditValue = _arrIndexStatus[0].Item1;
                txtVersion.EditValue = version;
                return;
            }


            if (_allowReviseAllStatus && (_status == "4" || _status == "5"))
            {
                for (int j = 0; j < count; j++)
                {
                    tuple1 = _arrIndexStatus[j];
                    if (tuple1.Item2 != null)
                        tuple1.Item2.EditValue = @"N/A";
                    if (tuple1.Item3 != null)
                        tuple1.Item3.EditValue = "";
                }

                txtStatus.EditValue = _arrIndexStatus[0].Item1;
                txtVersion.EditValue = version;
            }
            else
            {
                tuple1 = _arrIndexStatus[indexFind];
                if (tuple1.Item2 != null)
                    tuple1.Item2.EditValue = @"N/A";
                if (tuple1.Item3 != null)
                    tuple1.Item3.EditValue = "";
                string beforStatus = _arrIndexStatus[indexFind - 1].Item1;
                txtStatus.EditValue = beforStatus;
                if (_userName.ToUpper() != "ADMIN")
                {
                    _useSave = _dicAdvanceFuction.ContainsKey(beforStatus);
                }
            }

          

      


        }



        /*
     

    */



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class StatusDataInfo
    {
        public  Int32 StatusCode { get; set; }
        public  TextEdit TxtBy { get; set; }

        public TextEdit TxtDate { get; set; }
    }
}
