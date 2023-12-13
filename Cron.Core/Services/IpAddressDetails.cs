using static System.Net.WebRequestMethods;

namespace Cron.Core.Services
{
    public class IpAddressDetails
    {
        public static async Task<string> Get(string requestedIP)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                requestedIP = "https://ipinfo.io/" + requestedIP + "/geo";
                HttpResponseMessage responce = await httpclient.GetAsync(requestedIP);
                string json = await responce.Content.ReadAsStringAsync();
                return json;
            }
        }
    }

}

