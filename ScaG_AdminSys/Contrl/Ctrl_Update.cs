using System.Data;
using CNY_AdminSys.Info;

namespace CNY_AdminSys.Contrl
{
    public class Ctrl_Update
    {
        private readonly Inf_Update _inf = new Inf_Update();
        public bool Machine_UpdateDomain(DataTable dt)
        {
            return _inf.Machine_UpdateDomain(dt);
        }
        public bool Machine_Update(string strDel, DataTable dt)
        {
            return _inf.Machine_Update(strDel, dt);
        }
        public DataTable Machine_Load()
        {
            return _inf.Machine_Load();
        }
        public bool Report_Update(string strDel, DataTable dt)
        {
            return _inf.Report_Update(strDel, dt);
        }
        public DataTable Report_Load()
        {
            return _inf.Report_Load();
        }
        public bool Component_Update(string strDel, DataTable dt)
        {
            return _inf.Component_Update(strDel, dt);
        }
        public DataTable Component_Load()
        {
            return _inf.Component_Load();
        }
    }
}
