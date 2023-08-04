using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNY_BaseSys;
using CNY_BaseSys.Common;

namespace CNY_AdminSys.Info
{
    public class Inf_MasterFileList
    {
        private readonly AccessData _ac = new AccessData(DeclareSystem.SysConnectionString);

        public bool BolExcuteSql(string sql)
        {
            return _ac.BolExcuteSQL(sql, null);
        }

        public DataTable LoadModuleOnGridView()
        {
            return _ac.TblReadDataSP("sp_LoadModule", null);
        }

        public bool MasterFileList_Update(DataTable dtUpdate)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtUpdate };
            return _ac.BolExcuteSP("sp_MasterFileList_Update", arrPara);
        }

        public bool MasterFileList_Insert(DataTable dtInsert)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtInsert };
            return _ac.BolExcuteSP("sp_MasterFileList_Insert", arrPara);
        }

        public bool MasterFileList_Delete(DataTable dtPk)
        {
            var arrPara = new SqlParameter[1];
            arrPara[0] = new SqlParameter("@type", SqlDbType.Structured) { Value = dtPk };
            return _ac.BolExcuteSP("sp_MasterFileList_Delete", arrPara);
        }

        public DataTable MasterFileList_Load()
        {
            return _ac.TblReadDataSP("sp_MasterFileList_Load", null);
        }
    }
}
