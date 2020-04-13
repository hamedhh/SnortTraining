using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TrackApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var client = new WebClient();
            //client.Headers.Add("User-Agent", "C# console program");

            //string url = "http://cve.mitre.org/cgi-bin/cvename.cgi?name=2006-3602";//
            //string content = client.DownloadString(url);


            //string[] values = content.Split(new string[] { "<tr>", "</tr>", "<td>", "</td>" }, StringSplitOptions.RemoveEmptyEntries);

            //List<string> list = new List<string>(values);


            WebClient webClient = new WebClient();
            string page = webClient.DownloadString("http://cve.mitre.org/cgi-bin/cvename.cgi?name=2006-3602");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);



            List<List<string>> table1 = doc.DocumentNode.SelectSingleNode("//table[@width='100%']")
                        .Descendants("tr").Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                        .ToList();
            var cveID = table1[1][0].ToString();
            var cveDes = table1[3][0].ToString();

            return View();
        }


    }
}