using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CNY_BaseSys.Common
{
    public class SmtpSocketConnection : IDisposable
    {
        /// <summary>
        /// Get the client side certificate for ssl validation
        /// </summary>
	    public X509Certificate2 ClientCertificate2 { get; set; }

        //variables
        private TcpClient _socket;
        private StreamReader _reader;
        private StreamWriter _writer;
        private bool _connected;

        /// <summary>
        /// Connection status.
        /// </summary>
        public bool Connected
        {
            get { return _connected; }
        }

        /// <summary>
        /// Create a new connection.
        /// </summary>
        internal SmtpSocketConnection()
        {
            _socket = new TcpClient();
        }

        readonly RemoteCertificateValidationCallback _validationCallback =
  ServerValidationCallback;

        private static bool ServerValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
        {
            Console.Out.WriteLine("Validation Callback " + certificate + " ||| " + sslpolicyerrors);
            return true;
        }

        //   LocalCertificateSelectionCallback selectionCallback ;

        private X509Certificate ClientCertificateSelectionCallback(object sender, string targethost, X509CertificateCollection localcertificates, X509Certificate remotecertificate, string[] acceptableissuers)
        {
            return ClientCertificate2;
        }

        private const EncryptionPolicy EncryptionPolicy = System.Net.Security.EncryptionPolicy.AllowNoEncryption;




        public bool Open(string host, int port, int timeout, out string errormsg)
        {
            errormsg = "";
            try
            {
                _socket.Connect(host, port);
                _socket.SendTimeout = timeout;
                _socket.ReceiveTimeout = timeout;
                var sslStream = new SslStream(_socket.GetStream(),
                    true, _validationCallback, ClientCertificateSelectionCallback, EncryptionPolicy);
                sslStream.AuthenticateAsClient(host);
                _writer = new StreamWriter(sslStream, Encoding.ASCII);
                _reader = new StreamReader(sslStream, Encoding.ASCII);
                _connected = true;
                return true;

            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                return false;
            }


        }




        public void Close()
        {
            _reader.Close();
            _writer.Flush();
            _writer.Close();
            _reader = null;
            _writer = null;
            _socket.Close();
            _connected = false;
        }


        public void SendCommand(string cmd)
        {
            _writer.WriteLine(cmd);
            _writer.Flush();
        }



        public void SendData(char[] buf, int start, int length)
        {
            _writer.Write(buf, start, length);
        }


        public void GetReply(out string reply, out int code)
        {
            GetReply(_reader, out reply, out code);
        }


        public void GetReply(StreamReader reader, out string reply, out int code)
        {
            try
            {
                var s = reader.ReadLine();
                reply = s;
                while (s != null && s.Substring(3, 1).Equals("-"))
                {
                    s = reader.ReadLine();
                    if (s != null)
                    {
                        reply += s + "\r\n";
                    }
                }
                code = reply == null ? -1 : Int32.Parse(reply.Substring(0, 3));
            }
            catch (Exception err)
            {
                reply = "Error in reading response from server. " + err.Message;
                code = -1;
            }
        }


        public void Dispose()
        {
            _socket.Close();
            _socket = null;
        }


    }
}
