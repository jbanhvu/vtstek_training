using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using CNY_BaseSys.Class;

namespace CNY_BaseSys.Common
{
    public class SmtpSocketClient : IDisposable
    {



        #region "Property"
        private const int Port = 465;
        private readonly int _timeout = 100000;
        private readonly string _user;
        private readonly string _password;
        private readonly string _host;
        private readonly string _mailForm;
        private readonly List<string> _lMailTo;
        private readonly string _subject;
        private readonly string _body;
        private readonly List<string> _lFile;
        private readonly bool _isHtmLBody;
        private SmtpSocketConnection _con;

        #endregion

        #region cunstructor


        public SmtpSocketClient(string host, string username, string password, string mailForm, List<string> lMailTo, string subject, string body, List<string> lFile, bool isHtmLBody, int timeout = 100000)
        {

            _host = host;
            _user = username;
            _password = password;
            _timeout = timeout;
            _mailForm = mailForm;
            _lMailTo = lMailTo;
            this._subject = subject;
            this._body = body;
            _lFile = lFile;
            _isHtmLBody = isHtmLBody;




        }


        #endregion


        #region MessageSenders

        public bool SendEmail(out string errormsg, out string errorRecip)
        {
            errormsg = "";
            errorRecip = "";
            if (string.IsNullOrEmpty(_host))
            {
                errormsg = "There wasn't any host address found for the mail.";
                return false;
            }



            if (InCall)
            {
                errormsg = "Mailer is busy already, please try later";
                return false;
            }

            if (String.IsNullOrEmpty(_mailForm))
            {


                errormsg = "There wasn't any sender for the message";
                return false;
            }
            if (_lMailTo.Count == 0)
            {
                errormsg = "Please specifie at least one reciever for the message";
                return false;
            }
            MailAddress senderMail = new MailAddress(_mailForm, _mailForm);
            string sSubject =    string.Concat(_subject.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));



            MailMessage mailMessage = new MailMessage
            {
                
                Body = _body,
                IsBodyHtml = _isHtmLBody,
                From = senderMail,
                Sender = senderMail,
                SubjectEncoding = Encoding.UTF8,
                Subject = sSubject,

            };

            foreach (string sTo in _lMailTo)
            {
                MailAddress mailTo = new MailAddress(sTo, sTo);
                mailMessage.To.Add(mailTo);
            }


          


            InCall = true;
            //set up initial connection
            if (!EsablishSmtp(out errormsg)) return false;
            string response;
            int code;
            var buf = new StringBuilder { Length = 0 };
            buf.Append(SmtpCommands.Mail);
            if (!string.IsNullOrEmpty(mailMessage.From.DisplayName))
            {
                buf.Append("\"");
                buf.Append(mailMessage.From.DisplayName);
                buf.Append("\" ");
            }

            buf.Append("<");
            buf.Append(mailMessage.From.Address);
            buf.Append(">");

            _con.SendCommand(buf.ToString());
            _con.GetReply(out response, out code);
            if (!ParseMail(code, response, out errormsg)) return false;



            buf.Length = 0;
            //set up list of to addresses
            foreach (MailAddress recipient in mailMessage.To)
            {
                buf.Append(SmtpCommands.Recipient);
                buf.Append("<");

                buf.Append(recipient);
                buf.Append(">");
                _con.SendCommand(buf.ToString());
                _con.GetReply(out response, out code);
                if (!ParseRcpt(code, response, out errormsg))
                {
                    errorRecip = recipient.Address;
                    return false;
                }
                buf.Length = 0;
            }


            buf.Length = 0;
            //set headers
            _con.SendCommand(SmtpCommands.Data);
            _con.GetReply(out response, out code);
            if (!ParseData(code, response, out errormsg)) return false;
            _con.SendCommand("X-Mailer: AIM.MimeMailer");
            DateTime today = DateTime.UtcNow;
            buf.Append(SmtpCommands.Date);
            buf.Append(today.ToString("r"));
            _con.SendCommand(buf.ToString());
            buf.Length = 0;
            buf.Append(SmtpCommands.From);
            buf.Append(mailMessage.From);
            _con.SendCommand(buf.ToString());
            buf.Length = 0;
            buf.Append(SmtpCommands.To);
            buf.Append(mailMessage.To[0]);
            for (int x = 1; x < mailMessage.To.Count; ++x)
            {
                buf.Append(";");
                buf.Append(mailMessage.To[x]);
            }

            _con.SendCommand(buf.ToString());


            buf.Length = 0;
            buf.Append(SmtpCommands.ReplyTo);
            buf.Append(mailMessage.From);
            _con.SendCommand(buf.ToString());
            buf.Length = 0;
            buf.Append(SmtpCommands.Subject);

            string strSub = GetEncodedSubject(mailMessage);
            buf.Append(strSub);
            _con.SendCommand(buf.ToString());
            SendMessageBody(mailMessage, buf);
            _con.GetReply(out response, out code);


            _con.SendCommand(SmtpCommands.Quit);
            _con.GetReply(out response, out code);

            _con.Close();
            InCall = false;

            errormsg = "Send Email Successful";
            return true;
        }


        private void SendMessageBody(MailMessage mailMessage, StringBuilder buf)
        {
            var encodingString = ProcessGeneral.GetContentTypeName(mailMessage.BodyEncoding);

            var encodingHtmlHeader = "Content-Type: text/html; charset=" + encodingString;
            var encodingPlainHeader = "Content-Type: text/plain; charset=" + encodingString;
            var encodingQuotedPrintable = mailMessage.BodyEncoding.Equals(Encoding.ASCII) ? "Content-Transfer-Encoding: quoted-printable\r\n" :
                "Content-Transfer-Encoding: base64\r\n";
            
            buf.Length = 0;
            //declare mime info for message
            _con.SendCommand("MIME-Version: 1.0");
            if (!mailMessage.IsBodyHtml ||
                (mailMessage.IsBodyHtml && _lFile.Count > 0))
            {
                _con.SendCommand("Content-Type: multipart/mixed; boundary=\"#SEPERATOR1#\"\r\n");
                _con.SendCommand("This is a multi-part message.\r\n\r\n--#SEPERATOR1#");
            }
            if (mailMessage.IsBodyHtml)
            {
                _con.SendCommand("Content-Type: multipart/related; boundary=\"#SEPERATOR2#\"");
                _con.SendCommand(encodingQuotedPrintable);
                _con.SendCommand("--#SEPERATOR2#");
            }
            if (mailMessage.IsBodyHtml)
            {
                _con.SendCommand(encodingHtmlHeader);
                _con.SendCommand(encodingQuotedPrintable);
                //     _con.SendCommand(" BODY=8BITMIME SMTPUTF8\r\n");


            }
            else
            {
                _con.SendCommand(encodingPlainHeader);
                _con.SendCommand(encodingQuotedPrintable);
                //   _con.SendCommand(" BODY=8BITMIME SMTPUTF8\r\n");
            }
            //_con.SendCommand(MailMessage.Body);
            _con.SendCommand(GetEncodedBody(mailMessage));
            if (mailMessage.IsBodyHtml)
            {
                _con.SendCommand("\r\n--#SEPERATOR2#--");
            }
            if (_lFile.Count > 0)
            {
                //send normal attachments
                SendAttachments(buf);
            }
            //finish up message
            _con.SendCommand("");
            if (_lFile.Count > 0)
            {
                _con.SendCommand("--#SEPERATOR1#--");
            }

            _con.SendCommand(".");
        }


        public bool InCall { get; private set; }



        private bool AuthenticateAsBase64(out string response, out int code)
        {
            _con.SendCommand(SmtpCommands.Auth + SmtpCommands.AuthLogin);
            _con.GetReply(out response, out code);
            if (code == (int)SmtpResponseCodes.SyntaxError)
            {
                return false;
            }

            _con.SendCommand(Convert.ToBase64String(Encoding.ASCII.GetBytes(_user)));
            _con.GetReply(out response, out code);

            _con.SendCommand(Convert.ToBase64String(Encoding.ASCII.GetBytes(_password)));

            _con.GetReply(out response, out code);


            if (code == (int)SmtpResponseCodes.AuthenticationSuccessfull)
                return true;
            return false;
        }



        private void QuiteConnection(out string response, out int code)
        {
            try
            {
                _con.SendCommand(SmtpCommands.Quit);
                _con.GetReply(out response, out code);
                _con.Close();
            }
            finally
            {
                InCall = false;
                _con = null;
            }
        }


        /// <summary>
        /// Send any attachments.
        /// </summary>
        /// <param name="buf">String work area.</param>
        /// <param name="type">Attachment type to send.</param>
        private void SendAttachments(StringBuilder buf)
        {

            //declare mime info for attachment
            var fbuf = new byte[2048];
            string seperator = "\r\n--#SEPERATOR1#";
            buf.Length = 0;
            foreach (string pathName in _lFile)
            {


                Attachment attachment = new Attachment(pathName);
                Stream stream = new FileStream(pathName, FileMode.Open, FileAccess.Read, FileShare.Read);
                string fileName = Path.GetFileName(pathName);


                var cs = new CryptoStream(stream, new ToBase64Transform(), CryptoStreamMode.Read);
                _con.SendCommand(seperator);
                var escapedFileName = fileName.Replace(@"\", @"\\").Replace(@"""", @"\""");
                buf.Append("Content-Type: ");
                buf.Append(attachment.ContentType);
                buf.Append("; name=\"");
                buf.Append(escapedFileName);
                buf.Append("\"");
                _con.SendCommand(buf.ToString());
                _con.SendCommand("Content-Transfer-Encoding: base64");
                buf.Length = 0;
                buf.Append("Content-Disposition: attachment; filename=\"");
                buf.Append(escapedFileName);
                buf.Append("\"");
                _con.SendCommand(buf.ToString());
                buf.Length = 0;
                buf.Append("Content-ID: ");
                var escapedContentId = Path.GetFileNameWithoutExtension(fileName).Replace(" ", "-");
                buf.Append(escapedContentId);
                buf.Append("\r\n");
                _con.SendCommand(buf.ToString());
                buf.Length = 0;
                int num = cs.Read(fbuf, 0, 2048);
                while (num > 0)
                {
                    _con.SendData(Encoding.ASCII.GetChars(fbuf, 0, num), 0, num);
                    num = cs.Read(fbuf, 0, 2048);
                }
                cs.Close();
                _con.SendCommand("");
            }
        }

        #endregion


        private bool EsablishSmtp(out string errormsg)
        {
            errormsg = "";

            _con = new SmtpSocketConnection();
            bool connect = _con.Open(_host, Port, _timeout, out errormsg);
            if (!connect)
            {
                Dispose();
                return false;
            }
            string response;
            int code;
            //read greeting
            _con.GetReply(out response, out code);


            //Code 220 means that service is up and working

            if (!ParseGreeting(code, response, out errormsg)) return false;
            var buf = new StringBuilder();
            buf.Append(SmtpCommands.EHello);
            buf.Append(_host);
            _con.SendCommand(buf.ToString());
            _con.GetReply(out response, out code);

            //Handle Errors
            if (!ParseEHello(code, response, out errormsg)) return false;



            if (!AuthenticateAsBase64(out response, out code))
            {
                if (code == (int)SmtpResponseCodes.SyntaxError)
                {
                    errormsg = "Service Does not support Base64 Encoding. Please check authentification type";

                }
                if (code == (int)SmtpResponseCodes.AuthenticationFailed)
                {
                    errormsg = "SMTP client authenticates but the username or password is incorrect";

                }
                else if (code == (int)SmtpResponseCodes.Error)
                {
                    errormsg = "A general Error happened";

                }

                else
                {
                    errormsg = "Authenticiation Failed";

                }
                QuiteConnection(out response, out code);
                return false;
            }

            return true;
        }

        #region Parsers

        private bool ParseRcpt(int code, string response, out string errormsg)
        {
            errormsg = "";
            if (code == (int)SmtpResponseCodes.RequestCompleted) return true;
            //There is something wrong

            switch (code)
            {
                case (int)SmtpResponseCodes.ServiceNotAvailable:
                    errormsg = "Service not available, closing transmission channel";
                    break;
                case (int)SmtpResponseCodes.MailNotAccepted:
                    errormsg = "does not accept mail [rfc1846]";
                    break;
                case (int)SmtpResponseCodes.NotImplemented:
                    errormsg = "Requested action not taken: mailbox unavailable";
                    break;
                case (int)SmtpResponseCodes.BadSequence:
                    errormsg = "Bad sequence of commands";
                    break;
                case (int)SmtpResponseCodes.MailBoxUnavailable:
                    errormsg = "Requested mail action not taken: mailbox unavailable";
                    break;
                case (int)SmtpResponseCodes.MailBoxNameNotValid:
                    errormsg = "Requested action not taken: mailbox name not allowed";
                    break;
                case (int)SmtpResponseCodes.UserNotLocalBad:
                    errormsg = "User not local";
                    break;
                case (int)SmtpResponseCodes.ExceededStorage:
                    errormsg = "Requested mail action aborted: exceeded storage allocation";
                    break;
                case (int)SmtpResponseCodes.RequestAborted:
                    errormsg = "Requested action aborted: local error in processing";
                    break;
                case (int)SmtpResponseCodes.InsufficientStorage:
                    errormsg = "Requested action not taken: insufficient system storage";
                    break;
                case (int)SmtpResponseCodes.SyntaxError:
                    errormsg = "Syntax error in parameters or arguments";
                    break;
                case (int)SmtpResponseCodes.Error:
                    errormsg = "Syntax error, command unrecognised";
                    break;
            }
            QuiteConnection(out response, out code);
            return false;

        }


        private bool ParseMail(int code, string response, out string errormsg)
        {
            errormsg = "";
            if (code == (int)SmtpResponseCodes.RequestCompleted) return true;
            //There is something wrong

            switch (code)
            {
                case (int)SmtpResponseCodes.ServiceNotAvailable:
                    errormsg = "Service not available, closing transmission channel";
                    break;
                case (int)SmtpResponseCodes.ExceededStorage:
                    errormsg = "Requested mail action aborted: exceeded storage allocation";
                    break;
                case (int)SmtpResponseCodes.RequestAborted:
                    errormsg = "Requested action aborted: local error in processing";
                    break;
                case (int)SmtpResponseCodes.InsufficientStorage:
                    errormsg = "Requested action not taken: insufficient system storage";
                    break;
                case (int)SmtpResponseCodes.SyntaxError:
                    errormsg = "Syntax error in parameters or arguments";
                    break;
                case (int)SmtpResponseCodes.Error:
                    errormsg = "Syntax error, command unrecognised";
                    break;
            }
            QuiteConnection(out response, out code);
            return false;

        }



        private bool ParseEHello(int code, string response, out string errormsg)
        {
            errormsg = "";
            if (code == (int)SmtpResponseCodes.RequestCompleted) return true;
            switch (code)
            {
                case (int)SmtpResponseCodes.ServiceNotAvailable:
                    errormsg = "Service not available, closing transmission channel";
                    break;
                case (int)SmtpResponseCodes.NotImplemented:
                    errormsg = "Not Implemented";
                    break;
                case (int)SmtpResponseCodes.CommandParameterNotImplemented:
                    errormsg = "Command parameter not implemented";
                    break;
                case (int)SmtpResponseCodes.SyntaxError:
                    errormsg = "Syntax error in parameters or arguments";
                    break;
                case (int)SmtpResponseCodes.Error:
                    errormsg = "Syntax error, command unrecognised";
                    break;
                default:
                    errormsg = response;
                    break;
            }
            QuiteConnection(out response, out code);
            return false;
        }


        private bool ParseGreeting(int code, string response, out string errormsg)
        {
            errormsg = "";
            if (code == (int)SmtpResponseCodes.Ready) return true;
            //There is something wrong
            switch (code)
            {
                case (int)SmtpResponseCodes.ServiceNotAvailable:
                    errormsg = "Service not available, closing transmission channel";
                    break;
                default:
                    errormsg = "We couldn't connect to server, server is clossing";
                    break;
            }
            QuiteConnection(out response, out code);
            return false;
        }

        private bool ParseData(int code, string response, out string errormsg)
        {
            errormsg = "";
            if (code == (int)SmtpResponseCodes.StartInput || code == (int)SmtpResponseCodes.RequestCompleted) return true;
            switch (code)
            {
                case (int)SmtpResponseCodes.RequestAborted:
                    errormsg = "Requested action aborted: local error in processing";
                    break;
                case (int)SmtpResponseCodes.TransactionFailed:
                    errormsg = "Transaction failed";
                    break;
                case (int)SmtpResponseCodes.SyntaxError:
                    errormsg = "Syntax error, command unrecognised";
                    break;
                case (int)SmtpResponseCodes.Error:
                    errormsg = "Syntax error in parameters or arguments";
                    break;
                case (int)SmtpResponseCodes.BadSequence:
                    errormsg = "Bad sequence of commands";
                    break;
                case (int)SmtpResponseCodes.ServiceNotAvailable:
                    errormsg = "Service not available, closing transmission channel received data";
                    break;

                case (int)SmtpResponseCodes.ExceededStorage:
                    errormsg = "Requested mail action aborted: exceeded storage allocation";
                    break;
                case (int)SmtpResponseCodes.InsufficientStorage:
                    errormsg = "Requested action aborted: Insufficiant System Storage";
                    break;
                default:
                    errormsg = "We couldn't connect to server, server is clossing";
                    break;
            }
            QuiteConnection(out response, out code);
            return false;
        }

        #endregion


        private string GetEncodedSubject(MailMessage mailMessage)
        {
          
          ////  return ProcessGeneral.ToBase64(mailMessage.SubjectEncoding.GetBytes(mailMessage.Subject));

          return mailMessage.Subject;
            //var subjectEncoding = mailMessage.SubjectEncoding ?? Encoding.UTF8;
            //if (Encoding.ASCII.Equals(subjectEncoding))
            //{
            //    return mailMessage.Subject;
            //}
            //else
            //{
            //    var encodingName = subjectEncoding.BodyName.ToLower();
            //    return "=?" + encodingName + "?B?" + ProcessGeneral.ToBase64(subjectEncoding.GetBytes(mailMessage.Subject)) + "?=";
            //}
        }

        private string GetEncodedBody(MailMessage mailMessage)
        {
            if (mailMessage.BodyEncoding.Equals(Encoding.ASCII))
            {
                return BodyToQuotedPrintable(mailMessage);
            }
            else
            {
                return ProcessGeneral.ToBase64(mailMessage.BodyEncoding.GetBytes(mailMessage.Body));
            }
        }
        /// <summary>
		/// Encode the body as in quoted-printable format.
		/// Adapted from PJ Naughter's quoted-printable encoding code.
		/// For more information see RFC 2045.
		/// </summary>
		/// <returns>The encoded body.</returns>
		private string BodyToQuotedPrintable(MailMessage mailMessage)
        {
            //         var ENCODED = Encoding.UTF8.GetString(MailMessage.Body);
            var stringBuilder = new StringBuilder();
            sbyte currentByte;
            foreach (char t in mailMessage.Body)
            {
                currentByte = (sbyte)t;
                //is this a valid ascii character?
                if (((currentByte >= 33) && (currentByte <= 60)) || ((currentByte >= 62) && (currentByte <= 126)) || (currentByte == '\r') || (currentByte == '\n') || (currentByte == '\t') || (currentByte == ' '))
                {
                    stringBuilder.Append(t);
                }
                else
                {
                    stringBuilder.Append('=');
                    stringBuilder.Append(((sbyte)((currentByte & 0xF0) >> 4)).ToString("X"));
                    stringBuilder.Append(((sbyte)(currentByte & 0x0F)).ToString("X"));
                }
            }
            //format data so that lines don't end with spaces (if so, add a trailing '='), etc.
            //for more detail see RFC 2045.
            int start = 0;
            string encodedString = stringBuilder.ToString();
            stringBuilder.Length = 0;
            for (int x = 0; x < encodedString.Length; ++x)
            {
                currentByte = (sbyte)encodedString[x];
                if (currentByte == '\n' || currentByte == '\r' || x == (encodedString.Length - 1))
                {
                    stringBuilder.Append(encodedString.Substring(start, x - start + 1));
                    start = x + 1;
                    continue;
                }
                if ((x - start) > 76)
                {
                    bool inWord = true;
                    while (inWord)
                    {
                        inWord = (!char.IsWhiteSpace(encodedString, x) && encodedString[x - 2] != '=');
                        if (inWord)
                        {
                            --x;
                            //							currentByte = (sbyte) encodedString[x];
                        }
                        if (x == start)
                        {
                            x = start + 76;
                            break;
                        }
                    }
                    stringBuilder.Append(encodedString.Substring(start, x - start + 1));
                    stringBuilder.Append("=\r\n");
                    start = x + 1;
                }
            }
            return stringBuilder.ToString();
        }

        public void Dispose()
        {
            if (_con != null && _con.Connected)
            {
                _con.Close();
            }
        }




    }
}
