using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Model;
using static Model.ReadStrToXml;

namespace TagingOnNormalData
{


    public class TagToData
    {
        public static void InsertNormalizeDataIntoTxt(Model.rootTrack input)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            Serializer ser2 = new Serializer();
            startupPath = startupPath.Replace("TagingOnNormalData", "SnortTraining");
            string filePath = $@"{startupPath}\DataFolder\NormalaizeOutPutWithTag.txt";
            var xmlOutPutData = "";
            
                xmlOutPutData = xmlOutPutData + ser2.Serialize<rootTrack>(input) +
                     System.Environment.NewLine + "#" + System.Environment.NewLine;
                
            
            
            FileInfo fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                Directory.CreateDirectory(fileInfo.Directory.FullName);

            File.AppendAllText(filePath, xmlOutPutData + Environment.NewLine);



            //File.WriteAllText($@"{startupPath}\DataFolder\NormalaizeOut_01062020.txt", str);
        }


        public void taging()
        {
            XmlDocument[] xml = new XmlDocument[100000];

            //ReadStrToXml readxml = new ReadStrToXml();
            Serializer ser = new Serializer();
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            startupPath=startupPath.Replace("TagingOnNormalData", "SnortTraining");

            string filePath = $@"{startupPath}\DataFolder\NormalaizeOutPut.txt";

            var lstRootTrack = new List<Model.rootTrack>();
            var resxml = System.IO.File.ReadAllText(filePath).Split('#').ToList();
            for (int i = 0; i < resxml.Count; i++)
            {
                //XmlDocument nxml = new XmlDocument();
                var roottrack = new Model.rootTrack();
                var gg = resxml[i];
                if (!string.IsNullOrEmpty(gg.Trim()))
                {
                    roottrack = ser.Deserialize<Model.rootTrack>(gg);
                    lstRootTrack.Add(roottrack);
                }


                //nxml.LoadXml(gg);
                //xml[i] = nxml;
            }

            #region InsertTag
            foreach (var item in lstRootTrack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.cveDescription) && !string.IsNullOrEmpty(item.BugtraqRemote) && !string.IsNullOrEmpty(item.BugtraqLocal))
                    {
                        if (item.cveDescription.Contains("Buffer overflow") && item.BugtraqRemote.Contains("Yes") && item.BugtraqRemote.Contains("No"))
                        {
                            item.Tags = item.Tags + ";" + "3.3";
                        }

                        if ((item.cveDescription.Contains("Buffer overflow") && item.cveDescription.Contains("execute")) && item.BugtraqRemote.Contains("Yes") && item.BugtraqRemote.Contains("No"))
                        {
                            item.Tags = item.Tags + ";" + "4.4";
                        }

                        if (item.cveDescription.Contains("Buffer overflow") && item.BugtraqRemote.Contains("Yes") && item.BugtraqRemote.Contains("Yes"))
                        {
                            item.Tags = item.Tags + ";" + "3.3";
                        }
                        if (item.cveDescription.Contains("allows remote attackers to execute") && item.BugtraqRemote.Contains("Yes") && item.BugtraqRemote.Contains("No"))
                        {
                            item.Tags = item.Tags + ";" + "3.3";
                            item.Tags = item.Tags + ";" + "4.4";
                        }
                    }
                    if (item.cveDescription.Contains("Buffer overflow") && item.cveDescription.Contains("denial of service"))
                    {
                        item.Tags = item.Tags + ";" + "4.2";

                    }
                    if (item.cveDescription.Contains("brute force password attacks") || item.cveDescription.Contains("identify valid users"))
                    {
                        item.Tags = item.Tags + ";" + "2.2";
                        item.Tags = item.Tags + ";" + "3.2";
                    }
                    if (item.cveDescription.Contains("Buffer overflow") && item.cveDescription.Contains("remote attackers to  execute") && item.cveDescription.Contains("arbitrary code"))
                    {
                        item.Tags = item.Tags + ";" + "3.1";
                        item.Tags = item.Tags + ";" + "4.4";
                    }
                    if (item.cveDescription.Contains("remote attackers") && item.cveDescription.Contains("list directories") && item.cveDescription.Contains("web root"))
                    {
                        item.Tags = item.Tags + ";" + "4.4";
                    }
                    if (item.cveDescription.Contains("remote attackers") && item.cveDescription.Contains("list directories") && item.cveDescription.Contains("web root"))
                    {
                        item.Tags = item.Tags + ";" + "3.1";
                    }
                    if (item.cveDescription.Contains("remote attackers") && item.cveDescription.Contains("using a nonstandard URL"))
                    {
                        item.Tags = item.Tags + ";" + "3.1";
                    }
                    if (item.cveDescription.Contains("remote attackers execute") && item.cveDescription.Contains("arbitrary code") && item.cveDescription.Contains("one-byte buffer underflow"))
                    {
                        item.Tags = item.Tags + ";" + "4.4";
                    }
                    if (item.cveDescription.Contains("remote attackers") && item.cveDescription.Contains("with an upload parameter and specifying the file to copy"))
                    {
                        item.Tags = item.Tags + ";" + "3.3";
                    }
                    if (!string.IsNullOrEmpty(item.Des))
                    {
                        if (item.Des.Contains("ET SCAN NMAP"))
                        {
                            item.Tags = item.Tags + ";" + "1.4";
                        }
                        if (item.Des.Contains("SCAN NMAP"))
                        {
                            item.Tags = item.Tags + ";" + "1.4";
                        }
                        if (item.Des.Contains("ICMP Echo Reply"))
                        {
                            item.Tags = item.Tags + ";" + "1.2";
                        }
                        if (item.Des.Contains("PHP Remote File Inclusion"))
                        {
                            item.Tags = item.Tags + ";" + "4.1";
                        }
                        if (item.Des.Contains("WEB-FRONTPAGE rad fp30reg.dll access"))
                        {
                            item.Tags = item.Tags + ";" + "4.1";
                        }
                        if (item.Des.Contains("WEB-MISC http directory traversal"))
                        {
                            item.Tags = item.Tags + ";" + "1.4";
                            item.Tags = item.Tags + ";" + "4.1";
                        }
                        if (item.Des.Contains("WEB-MISC") && item.Des.Contains("access"))
                        {
                            item.Tags = item.Tags + ";" + "1.2";
                            item.Tags = item.Tags + ";" + "3.1";
                        }
                        if (item.Des.Contains("WEB-IIS cmd.exe access"))
                        {
                            item.Tags = item.Tags + ";" + "3.1";
                            item.Tags = item.Tags + ";" + "4.1";
                            item.Tags = item.Tags + ";" + "1.4";

                        }
                        if (item.Des.Contains("WEB-FRONTPAGE /_vti_bin/ access"))
                        {
                            item.Tags = item.Tags + ";" + "3.1";
                            item.Tags = item.Tags + ";" + "4.6";
                            item.Tags = item.Tags + ";" + "4.2";
                            item.Tags = item.Tags + ";" + "1.2";
                        }

                        if (item.Des.Contains("OVERSIZE REQUEST-URI DIRECTORY"))
                        {
                            item.Tags = item.Tags + ";" + "1.2";
                        }
                        if (item.Des.Contains("ET SCAN w3af User Agent"))
                        {
                            item.Tags = item.Tags + ";" + "1.2";
                        }
                        if (!string.IsNullOrEmpty(item.BugtraqLocal) && !string.IsNullOrEmpty(item.BugtraqRemote))
                        {
                            if (item.Des.Contains("WEB-PHP modules.php access") && item.BugtraqRemote.Contains("Yes") && item.BugtraqRemote.Contains("No"))
                            {
                                item.Tags = item.Tags + ";" + "1.4";
                                item.Tags = item.Tags + ";" + "4.1";
                            }
                        }

                        if (!string.IsNullOrEmpty(item.BugtraqLocal) && !string.IsNullOrEmpty(item.BugtraqRemote))
                        {
                            if (item.Des.Contains("WEB-MISC apache directory disclosure attempt") && item.BugtraqRemote.Contains("Yes") && item.BugtraqRemote.Contains("No"))
                            {
                                item.Tags = item.Tags + ";" + "1.4";
                                item.Tags = item.Tags + ";" + "3.3";
                                item.Tags = item.Tags + ";" + "2.1";
                                item.Tags = item.Tags + ";" + "4.3";

                            }
                        }
                        if (item.Des.Contains("WEB-PHP test.php access"))
                        {
                            item.Tags = item.Tags + ";" + "3.1";
                            item.Tags = item.Tags + ";" + "3.3";
                        }

                        if (item.Des.Contains("(http_inspect) OVERSIZE CHUNK ENCODING"))
                        {
                            item.Tags = item.Tags + ";" + "4.3";
                        }

                        if (item.Des.Contains("(http_inspect) BARE BYTE UNICODE ENCODING"))
                        {
                            item.Tags = item.Tags + ";" + "4.3";
                        }
                        if (item.Des.Contains("WEB-MISC cross site scripting attempt"))
                        {
                            item.Tags = item.Tags + ";" + "3.2";
                            item.Tags = item.Tags + ";" + "3.1";
                            item.Tags = item.Tags + ";" + "4.1";
                            item.Tags = item.Tags + ";" + "1.3";
                        }
                        if (item.Des.Contains("(spp_frag3) Fragmentation overlap"))
                        {
                            item.Tags = item.Tags + ";" + "1.1";
                            item.Tags = item.Tags + ";" + "4.2";
                        }
                        if (item.Des.Contains("SHELLCODE x86 inc ecx NOOP"))
                        {
                            item.Tags = item.Tags + ";" + "1.1";
                            item.Tags = item.Tags + ";" + "4.2";
                        }
                        if (item.Des.Contains("WEB-IIS /scripts/samples/ access"))
                        {
                            item.Tags = item.Tags + ";" + "1.1";
                        }
                        if (item.Des.Contains("ET SCAN Halberd Load Balanced Webserver Detection Scan"))
                        {
                            item.Tags = item.Tags + ";" + "1.4";
                        }
                        if (item.Des.Contains("WEB-IIS iissamples access"))
                        {
                            item.Tags = item.Tags + ";" + "1.2";
                        }


                    }
                    if (!string.IsNullOrEmpty(item.cveDescription))
                    {
                        if (item.cveDescription.Contains("allows remote") && (item.cveDescription.Contains("obtain") || item.cveDescription.Contains("access")))
                        {
                            item.Tags = item.Tags + ";" + "3.2";
                        }
                        if (item.cveDescription.Contains("SQL injection") && item.cveDescription.Contains("steal password hashes"))
                        {
                            item.Tags = item.Tags + ";" + "4.6";
                        }
                    }
                    if (!string.IsNullOrEmpty(item.BugtraqDescription))
                    {
                        if (item.BugtraqDescription.Contains("Input Validation Error") || item.BugtraqDescription.Contains("Access Validation Error"))
                        {
                            item.Tags = item.Tags + ";" + "1.4";
                            item.Tags = item.Tags + ";" + "1.1";
                        }
                        if (item.BugtraqDescription.Contains("phpBB Viewtopic.PHP SQL Injection Vulnerability"))
                        {
                            item.Tags = item.Tags + ";" + "4.4";
                        }
                    }
                    if (!string.IsNullOrEmpty(item.Classification))
                    {
                        if (item.Classification.Contains("Attempted Administrator Privilege Gain"))
                        {
                            item.Tags = item.Tags + ";" + "3.1";

                        }
                    }

                    InsertNormalizeDataIntoTxt(item);

                }
                catch (Exception)
                {

                    continue;
                }


            }
            #endregion





        }
    }
}
