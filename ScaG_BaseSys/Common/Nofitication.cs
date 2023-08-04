using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
namespace CNY_BaseSys.Common
{
    public static class Nofitication
    {
        public static async void SendNotification(Int64 PK, string Func)
        {

            HttpClient client = new HttpClient();
            string address = $"http://{DeclareSystem.SysServerSql}:3000/TDGv1?DocNo={PK}&Func={Func}";
            try
            {
                HttpResponseMessage rp = await client.GetAsync(address);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }


        }
    }
}
