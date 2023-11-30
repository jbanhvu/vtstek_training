using System;
using System.Data;

namespace CNY_AdminSys.Common
{
    public delegate void GetMultiUserInGroupSelectedHandler(object sender, GetMultiUserInGroupSelectedEventArgs e);
    public class GetMultiUserInGroupSelectedEventArgs : EventArgs
    {
        public DataTable DtUserInGroupId { get; set; }
    }
    public delegate void GetMultiSelectPermissionGroupHandler(object sender, GetMultiSelectPermissionGroupEventArgs e);
    public class GetMultiSelectPermissionGroupEventArgs : EventArgs
    {
        public string PermissionGroupCode { get; set; }
    }
    public delegate void GetMultiSelectAdvanceFunctionHandler(object sender, GetMultiSelectAdvanceFunctionEventArgs e);
    public class GetMultiSelectAdvanceFunctionEventArgs : EventArgs
    {
        public string AdvanceFunctionCode { get; set; }
    }
    public delegate void GetMultiUserSelectedHandler(object sender, GetMultiUserSelectedEventArgs e);
    public class GetMultiUserSelectedEventArgs : EventArgs
    {
        public DataTable dtUserID { get; set; }
    }
    public delegate void GetMultiRoleGroupSelectedHandler(object sender, GetMultiRoleGroupSelectedEventArgs e);
    public class GetMultiRoleGroupSelectedEventArgs : EventArgs
    {
        public DataTable DtRoleGroup { get; set; }
    }
}
