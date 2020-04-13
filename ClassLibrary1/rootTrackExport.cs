using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
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
        public string LatLongIPSrc { get; set; }
        public string LatLongIpDest { get; set; }
        public string CountryIPSrc { get; set; }
        public string CountryIpDest { get; set; }

    }

    public class rootTrackExport
    {
        public string id { get; set; }
        public string Des { get; set; }
        public string Priority { get; set; }
        public string Classification { get; set; }
        public string time { get; set; }
        public string SrcIP { get; set; }
        public string DesIP { get; set; }
        public string Tags { get; set; }
        public string TagName { get; set; }
        public string EstimatedTime { get; set; }
        public string EstimatedValue { get; set; }



    }


    public class SubCatInfo
    {
        public List<rootTrackExport> EventFeatures { get; set; }
        public string EstimatedAvgTime { get; set; }
        public string Temp { get; set; }
        public double tempInt
        {
            get {return Convert.ToDouble(Temp); }
        }
        

    }
}
