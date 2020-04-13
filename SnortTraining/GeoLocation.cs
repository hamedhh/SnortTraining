using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SnortTraining
{
    class GeoLocation
    {
        public static IpInfo GetUserCountryByIp(string ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.Name;
            }
            catch (Exception)
            {
                Random rand = new Random();
                var res = rand.Next(0, 3);
                switch (res)
                {
                    case 0:
                        return new IpInfo() { Country = "KR", Loc = "40.3399,127.5101"};
                    case 1:
                        return new IpInfo() { Country = "RU", Loc = "61.5240,105.3188"};
                    case 2:
                        return new IpInfo() { Country = "CA", Loc = "56.1304,106.3468"};
                    case 3:
                        return new IpInfo() { Country = "BR", Loc = "14.2350,51.9253"};
                }
            }

            return ipInfo;
        }



        public class IpInfo
        {

            [JsonProperty("ip")]
            public string Ip { get; set; }

            [JsonProperty("hostname")]
            public string Hostname { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("region")]
            public string Region { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("loc")]
            public string Loc { get; set; }

            [JsonProperty("org")]
            public string Org { get; set; }

            [JsonProperty("postal")]
            public string Postal { get; set; }
        }


        public void TestIPLocation()
        {
            var API_PATH_IP_API = "http://ip-api.com/json/14.99.228.232";
            var API_PATH_Free_Geo_IP = "http://freegeoip.net/json/116.73.154.132";


            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //For IP-API
                client.BaseAddress = new Uri(API_PATH_IP_API);
                HttpResponseMessage response = client.GetAsync(API_PATH_IP_API).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var locationDetails = response.Content.ReadAsAsync<IP2Location_IP_API>().GetAwaiter().GetResult();
                    if (locationDetails != null)
                    {
                        Console.WriteLine("Country: " + locationDetails.country);
                        Console.WriteLine("Region: " + locationDetails.regionName);
                        Console.WriteLine("City: " + locationDetails.city);
                    }
                }
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //For Free-GEO-IP API
                client.BaseAddress = new Uri(API_PATH_Free_Geo_IP);
                HttpResponseMessage response = client.GetAsync(API_PATH_Free_Geo_IP).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var locationDetails = response.Content.ReadAsAsync<IP2Location_FreeGeoIP>().GetAwaiter().GetResult();
                    if (locationDetails != null)
                    {
                        Console.WriteLine("Country: " + locationDetails.country_name);
                        Console.WriteLine("Region: " + locationDetails.region_name);
                        Console.WriteLine("City: " + locationDetails.city);
                    }
                }
            }
        }

        public class IP2Location_IP_API
        {
            public string @as { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string countryCode { get; set; }
            public string isp { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            public string org { get; set; }
            public string query { get; set; }
            public string region { get; set; }
            public string regionName { get; set; }
            public string status { get; set; }
            public string timezone { get; set; }
            public string zip { get; set; }
        }

        public class IP2Location_FreeGeoIP
        {
            public string ip { get; set; }
            public string country_code { get; set; }
            public string country_name { get; set; }
            public string region_code { get; set; }
            public string region_name { get; set; }
            public string city { get; set; }
            public string zip_code { get; set; }
            public string time_zone { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public int metro_code { get; set; }
        }
    }
}
