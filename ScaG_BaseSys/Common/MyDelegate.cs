using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CNY_BaseSys.Class;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace CNY_BaseSys.Common
{

    public delegate void TransferDataOnFormHandler(object sender, TransferDataOnFormEventArgs e);
    public class TransferDataOnFormEventArgs : EventArgs
    {
        public bool IsEvent { get; set; }
        public Dictionary<string, decimal> DicValue { get; set; }
        public Dictionary<Int64, Tuple<decimal, List<PrAllocateF4Info>>> DicAllocateInfo { get; set; }
    }

    public delegate void OnGetDataTreeHandler(object sender, OnGetDataTreeEventArgs e);
    public class OnGetDataTreeEventArgs : EventArgs
    {
        public List<TreeListNode> LNode { get; set; }
    }

    public delegate void RestoreCancelListHandler(object sender, RestoreCancelListEventArgs e);
    public class RestoreCancelListEventArgs : EventArgs
    {
        public Int64 ChildPK { get; set; }
    }
    public delegate void InputAttHiddenFormHandler(object sender, InputAttHiddenFormEventArgs e);
    public class InputAttHiddenFormEventArgs : EventArgs
    {
        public Int64 Key { get; set; }
        public Object Value { get; set; }

        public string FieldName { get; set; }
    }

    public delegate void OnGetBomFinishingFinalHandler(object sender, OnGetBomFinishingFinalEventArgs e);
    public class OnGetBomFinishingFinalEventArgs : EventArgs
    {
      
       
        public TreeList TreeReturn { get; set; }

      
    }

    public delegate void SearchHandlerCode(object sender, SearchCodeEventArgs e);
    public class SearchCodeEventArgs : EventArgs
    {
        public string StrNormal { get; set; }
        public string StrAnd { get; set; }
        public string StrOr { get; set; }
        public int CountAnd { get; set; }
        public bool IsAtt { get; set; }
    }

    public delegate void PrintBoMOptionHandler(object sender, PrintBoMOptionEventArgs e);
    public class PrintBoMOptionEventArgs : EventArgs
    {
        public List<string> LMode{ get; set; }
        public bool IsNormal { get; set; }
        public bool OnlyRm { get; set; }
        public List<string> LMainMaterial { get; set; }
        public bool IsAll { get; set; }
        public bool ExternalGroup { get; set; }
        public int LayOut { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string ProductionOrder { get; set; }
        public bool ShowItemCode { get; set; }
        public List<string> LColumnHide { get; set; }
        public bool ShowHistory { get; set; }

        public  DataTable DtFiQlt { get; set; }


        public bool IsChildNode { get; set; }

        public string MenuCaption { get; set; }

        public Dictionary<Int64, Int64> DicCny00050Pk { get; set; }
        public bool IsCheckAll { get; set; }
        public DataTable DtItemCode { get; set; }
        public DataTable DtSOPK { get; set; }


        public List<Int64> lChildPK { get; set; }

        public bool IsHighLightHis { get; set; }

        public bool IsPrintCancelItem { get; set; }

        public DataTable DtDate { get; set; }

        public DataTable DtMainMaterial { get; set; }
    }
    public delegate void OnLoadDataWhenCloseFormHandle(object sender, EventArgs e);
    public delegate void UpdateDisplayFilterHandler(object sender, DisplayFilterEventArgs e);
    public class DisplayFilterEventArgs : EventArgs
    {
        string filterText;
        public DisplayFilterEventArgs(string filterText)
        {
            this.filterText = filterText;
        }
        public string FilterText
        {
            get { return filterText; }
            set
            {
                if (filterText != value)
                    filterText = value;
            }
        }
    }

    public delegate void OnWizardAtrributeHandler(object sender, OnWizardAtrributeEventArgs e);
    public class OnWizardAtrributeEventArgs : EventArgs
    {
        public Dictionary<string, CellAttributeInfo> LCellInfo { get; set; }
        public Int32 RowHandle { get; set; }
    }
    public delegate void GetCodeGenerateHandler(object sender, GetCodeGenerateHEventArgs e);
    public class GetCodeGenerateHEventArgs : EventArgs
    {
        public DataTable DtReturn { get; set; }
    }

    public delegate void GetListStatusHandler(object sender, GetListStatusEventArgs e);
    public class GetListStatusEventArgs : EventArgs
    {
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
    public delegate void GetRtfTextQbrHandler(object sender, GetRtfTextQbrEventArgs e);
    public class GetRtfTextQbrEventArgs : EventArgs
    {
        public string RtfText { get; set; }
        public string HtmlText { get; set; }
        public string Text { get; set; }
    }

    public delegate void OnConfirmCaptchaHandler(object sender, OnConfirmCaptchaEventArgs e);
    public class OnConfirmCaptchaEventArgs : EventArgs
    {
        public string CaptchaText { get; set; }
    }

    public delegate void OnGetValueHandler(object sender, OnGetValueEventArgs e);
    public class OnGetValueEventArgs : EventArgs
    {
       public object Value { get; set; }
       public string Text { get; set; }


       public ActionDialog DialogResult { get; set; }

       public List<DateTime> LDate { get; set; }
       public DataTable DtTemp { get; set; }
    }

    public delegate void GetListEmailDomainHandler(object sender, GetListEmailDomainEventArgs e);
    public class GetListEmailDomainEventArgs : EventArgs
    {
        public string StrUserName { get; set; }
        public string StrDisplayName { get; set; }
        public string StrEmail { get; set; }
     
    }
    public delegate void GetListEmailHandler(object sender, GetListEmailEventArgs e);
    public class GetListEmailEventArgs : EventArgs
    {
        public string ListEmail { get; set; }
        public string ListFullName { get; set; }
    }
    public delegate void SendMailHandler(object sender, SendMailEventArgs e);
    public class SendMailEventArgs : EventArgs
    {
        public bool sendMailresult { get; set; }
    }

    public delegate void SearchHandler(object sender, SearchEventArgs e);
    public class SearchEventArgs : EventArgs
    {
        public string filterexpression { get; set; }
        public DataTable DtSearch { get; set; }
    }
    public delegate void AfterExportCustomReportHandler(object sender, EventArgs e);
    public delegate void TransferDataOnGridViewHandler(object sender, TransferDataOnGridViewEventArgs e);
    public class TransferDataOnGridViewEventArgs : EventArgs
    {
        public List<DataRow> ReturnRowsSelected { get; set; }
        public Dictionary<string, UserSystemEmailInfo> DicEmail { get; set; }
    }


    public delegate void TransferIdentityHandler(object sender, TransferIdentityEventArgs e);
    public class TransferIdentityEventArgs : EventArgs
    {
        public Int32 NewIdentity { get; set; }
    }

    public delegate void GetFixedColumnFieldNameHandler(object sender, GetFixedColumnFieldNameEventArgs e);
    public class GetFixedColumnFieldNameEventArgs : EventArgs
    {
        public string FieldName { get; set; }
    }


    public delegate void OnInputValueAttributeHandler(object sender, OnInputValueAttributeEventArgs e);
    public class OnInputValueAttributeEventArgs : EventArgs
    {
        public object Value { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
    }
    public delegate void OnInputValueMemoHandler(object sender, OnInputValueMemoEventArgs e);
    public class OnInputValueMemoEventArgs : EventArgs
    {
        public string Value { get; set; }
        public bool IsSave { get; set; }

        public string ValueRtf { get; set; }

        public string ValueHtml { get; set; }
    }
}
