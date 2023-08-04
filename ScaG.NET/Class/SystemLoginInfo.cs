using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNY_Main.Class
{
    public class SystemLoginInfo
    {
        public string HostName { get; set; }
        public string LoggedInUser { get; set; }
        public string ServerSQL { get; set; }
        public string DomainName { get; set; }
        public string FullDomainName { get; set; }
        public string IpAddress { get; set; }
        public string DefaultGateway { get; set; }
        public string UserLoginWindows { get; set; }
        public string MachineName { get; set; }
        public bool IsDomainUser { get; set; }
    }
}
