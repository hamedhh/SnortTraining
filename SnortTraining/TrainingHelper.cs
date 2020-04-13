using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SnortTraining
{
    public class TrainingHelper
    {
        #region Training
        public void NormalizaInput(string[] AllTxt)
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Dictionary<string, string> _dic = new Dictionary<string, string>();

            List<XDocument> xxx = new List<XDocument>();
            XDocument y = new XDocument();
            int c = 0;
            foreach (var item in AllTxt)
            {
                //if (c == 1000)
                //    return;

                try
                {
                    if (item != "")
                    {

                        if (!_dic.Keys.Contains("id"))
                        {
                            var firstTag = item.Substring(0, item.Length - 4);
                            char[] removeFisrt = { '[', ']', '*' };
                            var All = firstTag.TrimStart(removeFisrt);
                            var id = All.Substring(1, All.LastIndexOf(']'));
                            var desc = All.Remove(1, All.LastIndexOf(']'));
                            _dic.Add("id", id);
                            _dic.Add("Desc", desc);

                        }
                        if (!_dic.Keys.Contains("Priority"))
                        {
                            if (item.Contains("Priority"))
                            {
                                if (!item.Contains("Classification"))
                                {
                                    var priority = item.Replace('[', ' ').Replace(']', ' ').Trim();
                                    _dic.Add("Priority", priority);
                                    _dic.Add("Classification", "Nan");

                                }
                                else
                                {
                                    var priority = item.Remove(0, item.IndexOf(']') + 1);
                                    priority = priority.Replace('[', ' ').Replace(']', ' ').Trim();
                                    _dic.Add("Priority", priority);

                                }

                            }


                        }

                        if (!_dic.Keys.Contains("Classification"))
                        {
                            if (item.Contains("Classification"))
                            {
                                var Classification = item.Substring(1, item.IndexOf(']') - 1);
                                _dic.Add("Classification", Classification);

                            }



                        }

                        if (!_dic.Keys.Contains("time"))
                        {
                            if (item.Contains("->"))
                            {
                                char[] removeFisrt = { ' ' };
                                var time = item.Split(removeFisrt);
                                _dic.Add("time", time[0]);

                            }
                        }

                        if (!_dic.Keys.Contains("SrcIP"))
                        {
                            if (item.Contains("->"))
                            {
                                char[] removeFisrt = { ' ' };
                                var SrcIP = item.Split(removeFisrt);
                                _dic.Add("SrcIP", SrcIP[1]);
                                var geopLocationSrc = GeoLocation.GetUserCountryByIp(SrcIP[1].Split(':')[0]);
                                _dic.Add("LatLongIPSrc", geopLocationSrc.Loc);
                                _dic.Add("CountryIPSrc", geopLocationSrc.Country);
                            }
                        }

                        if (!_dic.Keys.Contains("DesIP"))
                        {
                            if (item.Contains("->"))
                            {
                                char[] removeFisrt = { ' ' };
                                var DesIP = item.Split(removeFisrt);
                                _dic.Add("DesIP", DesIP[3]);
                                var geopLocationDest = GeoLocation.GetUserCountryByIp(DesIP[3].Split(':')[0]);
                                _dic.Add("LatLongIpDest", geopLocationDest.Loc);
                                _dic.Add("CountryIpDest", geopLocationDest.Country);
                            }


                        }
                        if (!_dic.Keys.Contains("Xref"))
                        {
                            if (item.Contains("Xref"))
                            {
                                char[] removeTags = { '[', ']', ' ' };
                                var xrefs = item.Split(removeTags);
                                foreach (var xrefItem in xrefs)
                                {
                                    if (xrefItem != "")
                                    {
                                        if (xrefItem.Contains("securityfocus.com"))
                                        {
                                            try
                                            {
                                                WebClient webClient = new WebClient();
                                                string page = webClient.DownloadString(xrefItem);

                                                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                                doc.LoadHtml(page);
                                                var tblSecDes = doc.DocumentNode.SelectNodes("//span[@class='title']").Select(v => v.InnerHtml).ToList();
                                                var tablesec = doc.DocumentNode.SelectSingleNode("//table[@cellpadding='4']").Descendants("tr").Select(tr => tr.Elements("td").
                                                Select(td => td.InnerText.Trim()).ToList())
                                                            .ToList();
                                                var BugtraqID = tablesec[0][1];
                                                if (!_dic.Keys.Contains("BugtraqID"))
                                                    _dic.Add("BugtraqID", BugtraqID);
                                                var BugtraqClass = tablesec[1][1];
                                                if (!_dic.Keys.Contains("BugtraqClass"))
                                                    _dic.Add("BugtraqClass", BugtraqClass);
                                                var BugtraqRemote = tablesec[3][1];
                                                if (!_dic.Keys.Contains("BugtraqRemote"))
                                                    _dic.Add("BugtraqRemote", BugtraqRemote);
                                                var BugtraqLocal = tablesec[4][1];
                                                if (!_dic.Keys.Contains("BugtraqLocal"))
                                                    _dic.Add("BugtraqLocal", BugtraqLocal);
                                                var BugtraqDescription = tblSecDes.FirstOrDefault().ToString();
                                                if (!_dic.Keys.Contains("BugtraqDescription"))
                                                    _dic.Add("BugtraqDescription", BugtraqDescription);

                                            }
                                            catch (Exception)
                                            {
                                                continue;

                                            }


                                        }

                                        if (xrefItem.Contains("cve.mitre.org"))
                                        {
                                            var resAdd = xrefItem;
                                            WebClient webClient = new WebClient();
                                            if (!xrefItem.Split('?')[1].Contains("CVE"))
                                                resAdd = xrefItem.Split('?')[0].ToString() + "?name=CVE-" + xrefItem.Split('?')[1].ToString().Split('=')[1];
                                            string page = webClient.DownloadString(resAdd);

                                            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                            doc.LoadHtml(page);



                                            List<List<string>> table1 = doc.DocumentNode.SelectSingleNode("//table[@width='100%']")
                                                        .Descendants("tr").Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                                                        .ToList();
                                            var cveID = table1[1][0].ToString();
                                            var cveDes = table1[3][0].ToString();
                                            //if (!_dic.Keys.Contains("cveID"))
                                            _dic.Add("cveID", cveID);
                                            //if (!_dic.Keys.Contains("cveDescription"))
                                            _dic.Add("cveDescription", cveDes);
                                            if (table1[1][1].Contains("NVD"))
                                            {
                                                WebClient nvdSite = new WebClient();
                                                var address = "https://" + "nvd.nist.gov/vuln/detail/" + cveID + "#vulnCurrentDescriptionTitle";
                                                var nvdPage = nvdSite.DownloadString(address);

                                                HtmlAgilityPack.HtmlDocument docx = new HtmlAgilityPack.HtmlDocument();
                                                docx.LoadHtml(nvdPage);
                                                var table2 = docx.DocumentNode.Descendants("span").Where(a => a.InnerHtml.Contains("severityDetail")).ToList();
                                                var baseScore = table2[1].InnerText.Split(';')[1];
                                                if (!_dic.Keys.Contains("CVSS_Score"))
                                                    _dic.Add("CVSS_Score", baseScore);

                                                var tblVector = docx.DocumentNode.Descendants("span").Where(a => a.InnerHtml.Contains("tooltipCvss2NistMetrics")).ToList();
                                                var resVector = tblVector[0].InnerText.Split(';')[1].Split('/').ToList();
                                                var AttackVector = resVector[0].Split(':')[1];
                                                switch (AttackVector)
                                                {
                                                    case "L":
                                                        if (!_dic.Keys.Contains("AttackVector"))
                                                            _dic.Add("AttackVector", "local");
                                                        break;
                                                    case "A":
                                                        if (!_dic.Keys.Contains("AttackVector"))
                                                            _dic.Add("AttackVector", "Adjacent Network");
                                                        break;
                                                    case "N":
                                                        if (!_dic.Keys.Contains("AttackVector"))
                                                            _dic.Add("AttackVector", "Network");
                                                        break;
                                                    default:
                                                        _dic.Add("AttackVector", "");
                                                        break;
                                                }
                                                var AccessComplexity = resVector[1].Split(':')[1];
                                                switch (AccessComplexity)
                                                {
                                                    case "H":
                                                        if (!_dic.Keys.Contains("AccessComplexity"))
                                                            _dic.Add("AccessComplexity", "High");
                                                        break;
                                                    case "M":
                                                        if (!_dic.Keys.Contains("AccessComplexity"))
                                                            _dic.Add("AccessComplexity", "Medium");
                                                        break;
                                                    case "L":
                                                        if (!_dic.Keys.Contains("AccessComplexity"))
                                                            _dic.Add("AccessComplexity", "Low");
                                                        break;
                                                    default:
                                                        _dic.Add("AccessComplexity", "");
                                                        break;
                                                }
                                                var Authentication = resVector[2].Split(':')[1];
                                                switch (Authentication)
                                                {
                                                    case "M":
                                                        if (!_dic.Keys.Contains("Authentication"))
                                                            _dic.Add("Authentication", "Multiple");
                                                        break;
                                                    case "S":
                                                        if (!_dic.Keys.Contains("Authentication"))
                                                            _dic.Add("Authentication", "Singel");
                                                        break;
                                                    case "N":
                                                        if (!_dic.Keys.Contains("Authentication"))
                                                            _dic.Add("Authentication", "None");
                                                        break;
                                                    default:
                                                        _dic.Add("Authentication", "");
                                                        break;

                                                }
                                                var ConfidentialityImpact = resVector[3].Split(':')[1];
                                                switch (ConfidentialityImpact)
                                                {
                                                    case "N":
                                                        if (!_dic.Keys.Contains("ConfidentialityImpact"))
                                                            _dic.Add("ConfidentialityImpact", "None");
                                                        break;
                                                    case "P":
                                                        if (!_dic.Keys.Contains("ConfidentialityImpact"))
                                                            _dic.Add("ConfidentialityImpact", "Partial");
                                                        break;
                                                    case "C":
                                                        if (!_dic.Keys.Contains("ConfidentialityImpact"))
                                                            _dic.Add("ConfidentialityImpact", "Complete");
                                                        break;
                                                    default:
                                                        _dic.Add("ConfidentialityImpact", "");
                                                        break;

                                                }
                                                var IntegrityImpact = resVector[4].Split(':')[1];
                                                switch (IntegrityImpact)
                                                {
                                                    case "N":
                                                        if (!_dic.Keys.Contains("IntegrityImpact"))
                                                            _dic.Add("IntegrityImpact", "None");
                                                        break;
                                                    case "P":
                                                        if (!_dic.Keys.Contains("IntegrityImpact"))
                                                            _dic.Add("IntegrityImpact", "Partial");
                                                        break;
                                                    case "C":
                                                        if (!_dic.Keys.Contains("IntegrityImpact"))
                                                            _dic.Add("IntegrityImpact", "Complete");
                                                        break;
                                                    default:
                                                        _dic.Add("IntegrityImpact", "");
                                                        break;

                                                }
                                                var AvailabilityImpact = resVector[5].Split(':')[1].Split(')')[0];
                                                switch (AvailabilityImpact)
                                                {
                                                    case "N":
                                                        if (!_dic.Keys.Contains("AvailabilityImpact"))
                                                            _dic.Add("AvailabilityImpact", "None");
                                                        break;
                                                    case "P":
                                                        if (!_dic.Keys.Contains("AvailabilityImpact"))
                                                            _dic.Add("AvailabilityImpact", "Partial");
                                                        break;
                                                    case "C":
                                                        if (!_dic.Keys.Contains("AvailabilityImpact"))
                                                            _dic.Add("AvailabilityImpact", "Complete");
                                                        break;
                                                    default:
                                                        _dic.Add("AvailabilityImpact", "");
                                                        break;
                                                }


                                            }


                                        }

                                    }
                                }
                            }
                        }




                    }
                }
                catch (Exception)
                {

                    continue;
                }


                if (string.IsNullOrEmpty(item))
                {

                    xxx.Add(new XDocument(
     new XElement("rootTrack",
                              //new XElement("idDes",
                              new XElement("id", _dic.Keys.Contains("id") ? _dic["id"] : ""),
                              new XElement("Des", _dic.Keys.Contains("Desc") ? _dic["Desc"] : ""),
                              new XElement("Priority", _dic.Keys.Contains("Priority") ? _dic["Priority"] : ""),
                              new XElement("Classification", _dic.Keys.Contains("Classification") ? _dic["Classification"] : ""),
                              new XElement("time", _dic.Keys.Contains("time") ? _dic["time"] : ""),
                              new XElement("SrcIP", _dic.Keys.Contains("SrcIP") ? _dic["SrcIP"] : ""),
                              new XElement("DesIP", _dic.Keys.Contains("DesIP") ? _dic["DesIP"] : ""),
                              new XElement("cveID", _dic.Keys.Contains("cveID") ? _dic["cveID"] : ""),
                              new XElement("cveDescription", _dic.Keys.Contains("cveDescription") ? _dic["cveDescription"] : ""),
                              new XElement("AttackVector", _dic.Keys.Contains("AttackVector") ? _dic["AttackVector"] : ""),
                              new XElement("AccessComplexity", _dic.Keys.Contains("AccessComplexity") ? _dic["AccessComplexity"] : ""),
                              new XElement("Authentication", _dic.Keys.Contains("Authentication") ? _dic["Authentication"] : ""),
                              new XElement("ConfidentialityImpact", _dic.Keys.Contains("ConfidentialityImpact") ? _dic["ConfidentialityImpact"] : ""),
                              new XElement("IntegrityImpact", _dic.Keys.Contains("IntegrityImpact") ? _dic["IntegrityImpact"] : ""),
                              new XElement("AvailabilityImpact", _dic.Keys.Contains("AvailabilityImpact") ? _dic["AvailabilityImpact"] : ""),
                              new XElement("BugtraqID", _dic.Keys.Contains("BugtraqID") ? _dic["BugtraqID"] : ""),
                              new XElement("BugtraqClass", _dic.Keys.Contains("BugtraqClass") ? _dic["BugtraqClass"] : ""),
                              new XElement("BugtraqRemote", _dic.Keys.Contains("BugtraqRemote") ? _dic["BugtraqRemote"] : ""),
                              new XElement("BugtraqLocal", _dic.Keys.Contains("BugtraqLocal") ? _dic["BugtraqLocal"] : ""),
                              new XElement("BugtraqDescription", _dic.Keys.Contains("BugtraqDescription") ? _dic["BugtraqDescription"] : ""),
                              new XElement("Tags", ""),
                              new XElement("LatLongIPSrc", _dic.Keys.Contains("LatLongIPSrc") ? _dic["LatLongIPSrc"] : ""),
                              new XElement("LatLongIpDest", _dic.Keys.Contains("LatLongIpDest") ? _dic["LatLongIpDest"] : ""),
                              new XElement("CountryIPSrc", _dic.Keys.Contains("CountryIPSrc") ? _dic["CountryIPSrc"] : ""),
                              new XElement("CountryIpDest", _dic.Keys.Contains("CountryIpDest") ? _dic["CountryIpDest"] : "")

                 )
            ));
                    _dic.Clear();
                    c++;
                    InsertNormalizeDataIntoTxt(xxx);



                }

            }



            //Console.WriteLine("Almost Process of Normalize Data is Done....");
            //Console.ReadKey();

        }

        public static void InsertNormalizeDataIntoTxt(List<XDocument> xxx)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();

            string filePath = $@"{startupPath}\DataFolder\NormalaizeOutPut.txt";
            string str = "";
            foreach (var item in xxx)
            {
                str = str + (item.ToString()) +
                     System.Environment.NewLine + "#" + System.Environment.NewLine;
                xxx.Remove(item);
                if (xxx.Count == 0)
                    break;
            }
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                Directory.CreateDirectory(fileInfo.Directory.FullName);

            File.AppendAllText(filePath, str + Environment.NewLine);
            //File.WriteAllText($@"{startupPath}\DataFolder\NormalaizeOut_01062020.txt", str);
        }
        #endregion
    }
}
