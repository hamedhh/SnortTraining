using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SnortTraining
{
    public class ReadStrToXml
    {
        string resStr = "";
        public string read()
        {
             return System.IO.File.ReadAllText(@"C:\Users\Hamed_201X\Desktop\ProjectMalek\sampel02.txt");

        }

        public class Serializer
        {
            public T Deserialize<T>(string input) where T : class
            {
                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

                using (StringReader sr = new StringReader(input))
                {
                    return (T)ser.Deserialize(sr);
                }
            }

            public string Serialize<T>(T ObjectToSerialize)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                    return textWriter.ToString();
                }
            }
        }
 
        public class rootTrack
        {
            public string id { get; set; }
            public string Des { get; set; }
            public string Priority { get; set; }
            public string Classification { get; set; }
            public string time { get; set; }
            public string SrcIP { get; set; }
            public string DesIP { get; set; }
            public string cveID { get; set; }
            public string cveDescription { get; set; }
            public string AttackVector { get; set; }
            public string AccessComplexity { get; set; }
            public string Authentication { get; set; }
            public string ConfidentialityImpact { get; set; }
            public string IntegrityImpact { get; set; }
            public string BugtraqID { get; set; }
            public string BugtraqClass { get; set; }
            public string BugtraqRemote { get; set; }
            public string BugtraqLocal { get; set; }
            public string BugtraqDescription { get; set; }
            public string Tags { get; set; }
        }
    }
}
