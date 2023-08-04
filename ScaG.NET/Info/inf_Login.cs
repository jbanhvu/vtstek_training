using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using CNY_Main.Class;
using CNY_BaseSys.Common;
using CNY_Property;

namespace CNY_Main
{
    public class inf_Login
    {
        readonly AccessData _ac = new AccessData(SystemProperty.SysConnectionString);


        public SystemLoginInfo GetSystemLoginInfo()
        {


            DataTable dt = _ac.TblReadDataSQL("SELECT HOST_NAME() AS HostName, SUSER_NAME() LoggedInUser,@@SERVERNAME as ServerName", null);



            var sl = new SystemLoginInfo
            {
                HostName = dt.Rows[0]["HostName"].ToString(),
                LoggedInUser = dt.Rows[0]["LoggedInUser"].ToString(),
                ServerSQL = dt.Rows[0]["ServerName"].ToString(),
                DomainName = IPGlobalProperties.GetIPGlobalProperties().DomainName
            };
            sl.FullDomainName = !sl.HostName.Contains(sl.DomainName) ? String.Format("{0}.{1}", sl.ServerSQL, sl.DomainName) : sl.HostName;
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {

                        sl.IpAddress = ip.ToString();
                        break;
                    }
                }

            }


            var q1 = NetworkInterface.GetAllNetworkInterfaces().SelectMany(p => p.GetIPProperties().GatewayAddresses, (p, t) => new { p, t })
                                   .Where(h => h.p.OperationalStatus == OperationalStatus.Up)
                                   .Select(h => h.t.Address.ToString().Trim()).Distinct().ToList();
            string defaultGateway = "";
            if (q1.Any())
            {
                defaultGateway = q1.First();
            }
            sl.DefaultGateway = defaultGateway;
            sl.UserLoginWindows = Environment.UserName;
            sl.MachineName = Environment.MachineName;
            sl.IsDomainUser = Environment.UserDomainName.ToUpper().Trim() != Environment.MachineName.ToUpper().Trim();



            return sl;



        }
      
        public DataTable LoadCbGroupCodeWhenLogin(Int64 userId)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userId };
            return _ac.TblReadDataSP("usp_Scag_LoadCBUserGroupWhenLogin", arrpara);
        }



        public DataTable GetGroupWhenLogin(Int64 userId)
        {
            var arrpara = new SqlParameter[1];
            arrpara[0] = new SqlParameter("@UserID", SqlDbType.BigInt) { Value = userId };
            return _ac.TblReadDataSP("usp_Scag_LoadGroupWhenLogin", arrpara);
        }

        public DataTable LoadTableCompanyCode()
        {
            return _ac.TblReadDataSP("sp_LoadListCompWhenLogin", null);
          
        }
        public DataTable LoadListUserWhenLogin()
        {
            return _ac.TblReadDataSP("sp_GetEmailInfoByAllDeparment", null);

        }


        public DataTable CheckUserLogin(string username, string password)
        {
            var arrpara = new SqlParameter[2];
            arrpara[0] = new SqlParameter("@Username", SqlDbType.NVarChar) { Value = username };
            arrpara[1] = new SqlParameter("@Password", SqlDbType.NVarChar) { Value = password };
            return _ac.TblReadDataSP("usp_Scag_LoginSystemNew", arrpara);
        }



    }
}
