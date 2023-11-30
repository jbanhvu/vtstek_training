using System;

namespace CNY_AdminSys.Class
{
    public class Cls_AdminCodeFile
    {
        public string SystemName { get; set; }
        public bool IsActiveMenu { get; set; }
        public string MenuCode { get; set; }
        public string FormName { get; set; }
        public string FormCode { get; set; }
        public string ProjectCode { get; set; }
        public string FolderContainForm { get; set; }
        public string MenuImagePath { get; set; }
        public string ShowCode { get; set; }
        public string WorkFunCode { get; set; }

        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Sex { get; set; }
        public string Email { get; set; }
        public string StrPath { get; set; }
        public string PositionsCode { get; set; }
        public string DepartmentCode { get; set; }
        public bool IsActive { get; set; }

        //Adjust 12/03/2020
        public Int64 Cny00004Pk { get; set; }
        public Int64 Cny00001Pk { get; set; }
        public string Module { get; set; }
        public string UserUpd { get; set; }
    }
}
