using static System.Net.WebRequestMethods;

namespace Cron.Core.Services
{
    public class IpAddressDetails
    {
        public static async Task<HttpResponseMessage> Get(string requestedIP)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                requestedIP = "https://ipinfo.io/" + requestedIP + "/geo";
                HttpResponseMessage responce = await httpclient.GetAsync(requestedIP);
                return responce;         
            }
        }
    }

}

