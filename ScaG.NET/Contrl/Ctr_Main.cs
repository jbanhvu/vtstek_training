using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CNY_Main
{
    public class Ctr_Main
    {
        readonly inf_Main _inf = new inf_Main();
        public bool UpdateDatePasswordEmail(Int64 userId, string newPass)
        {
            return _inf.UpdateDatePasswordEmail(userId, newPass);

        }
        public string UpdateDatePasswordWhenUserChangePass(Int64 userId, string newPass , string passTemp)
        {
            return _inf.UpdateDatePasswordWhenUserChangePass(userId, newPass, passTemp);

        }

        public DataTable LoadCbFuncionGroupWhenLogin(string groupUserCode)
        {
            return _inf.LoadCbFuncionGroupWhenLogin(groupUserCode);

        }
        public DataTable LoadTreeViewMainMenu(Int64 idAuthorization, Int64 userInGroupId)
        {
            return _inf.LoadTreeViewMainMenu(idAuthorization, userInGroupId);

        }
        public DataTable LoadTableTypeShowForm(string userGroupCode, Int64 usedId)
        {
            return _inf.LoadTableTypeShowForm(userGroupCode, usedId);

        }

        
    }
}
