using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Buddies.App_Class
{
    public static class HelperClass
    {
        public static string GetData(string Url)
        {
            string res = "";
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(Url).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    res = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                }
            }

            return res;
        }
    }
}
