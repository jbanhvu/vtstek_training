using System;
using System.Data;
using CNY_Main.Class;

namespace CNY_Main
{
    public class Ctr_Login
    {
        readonly inf_Login _inf = new inf_Login();

        public SystemLoginInfo GetSystemLoginInfo()
        {



            return _inf.GetSystemLoginInfo();



        }

        public DataTable LoadListUserWhenLogin()
        {
            return _inf.LoadListUserWhenLogin();

        }


        public DataTable LoadCbGroupCodeWhenLogin(Int64 userId)
        {
            return _inf.LoadCbGroupCodeWhenLogin(userId);
        }
        public DataTable GetGroupWhenLogin(Int64 userId)
        {
            return _inf.GetGroupWhenLogin(userId);
        }
     
        public DataTable CheckUserLogin(string username, string password)
        {
            return _inf.CheckUserLogin(username, password);
        }



        public DataTable LoadTableCompanyCode()
        {
            return _inf.LoadTableCompanyCode();
        }
    }
}
