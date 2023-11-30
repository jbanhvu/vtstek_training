using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraPrinting;
using Microsoft.Office.Interop.Outlook;
using Attachment = System.Net.Mail.Attachment;

namespace CNY_BaseSys.Common
{
    public class ExportPdfSendEmail : ICommandHandler
    {
        private readonly string _emailSubject = "";
        private readonly string _emailBody = "";


        public ExportPdfSendEmail(string emailSubject, string emailBody)
        {
            this._emailSubject = emailSubject;
            this._emailBody = emailBody;
        }

        public virtual void HandleCommand(PrintingSystemCommand command,
            object[] args, IPrintControl printControl, ref bool handled)
        {
            if (!CanHandleCommand(command, printControl)) return;
            handled = true;
            MemoryStream stream = new MemoryStream();
            printControl.PrintingSystem.ExportToPdf(stream);
            Attachment att = new Attachment(stream, "PDF attachment");


            Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            oMailItem.BodyFormat = OlBodyFormat.olFormatHTML;
            oMailItem.Subject = _emailSubject;
           // oMailItem.Body = _emailBody;
            oMailItem.HTMLBody = _emailBody;
            oMailItem.Attachments.Add(att);
            oMailItem.Display(true);



            // your code
          
        }

        public virtual bool CanHandleCommand(PrintingSystemCommand command, IPrintControl printControl)
        {
            // This handler is used for the ExportGraphic command.
            return command == PrintingSystemCommand.SendPdf;
        }
    }
}
