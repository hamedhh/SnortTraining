using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    public class ReadStrToXml
    {
        string resStr = "";
        public string read(int path)
        {
            switch (path)
            {
                case 1:
                    return System.IO.File.ReadAllText(@"C:\Users\Hamed_201X\Desktop\ProjectMalek\NormalaizeOut_01062020.txt");
                case 2:
                    return System.IO.File.ReadAllText(@"C:\Users\Hamed_201X\Desktop\ProjectMalek\NormalaizeWithTagsOut_01062020.txt");
                case 3:
                    return System.IO.File.ReadAllText(@"C:\Users\Hamed_201X\Desktop\ProjectMalek\out_9415_ALLTag.txt");
                case 4:
                    string startupPath = System.IO.Directory.GetCurrentDirectory();
                    return System.IO.File.ReadAllText($@"e:\SourceFileRecord\records.txt");
                   // return System.IO.File.ReadAllText(@"D:\pro\records.txt");
                default:
                    break;

            }
            return "";
            //return System.IO.File.ReadAllText(@"C:\Users\Hamed_201X\Desktop\ProjectMalek\out002.txt");


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
 
      

        //public class rootTrackExport
        //{
        //    public string id { get; set; }
        //    public string Des { get; set; }
        //    public string Priority { get; set; }
        //    public string Classification { get; set; }
        //    public string time { get; set; }
        //    public string SrcIP { get; set; }
        //    public string DesIP { get; set; }
        //    public string Tags { get; set; }
        //    public string TagName { get; set; }


        //}

        //public class rootTrackExport
        //{
        //    public string id { get; set; }
        //    public string Des { get; set; }
        //    public string Classification { get; set; }
        //    public string time { get; set; }
        //    public string SrcIP { get; set; }
        //    public string DesIP { get; set; }
        //    public string cveID { get; set; }
        //    public string cveDescription { get; set; }
        //    public string Tags { get; set; }


        //}
    }
}
