using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Model
{
    public class DNode
    {
        [DataMember(Name = "data")]
        [JsonProperty("data")]
        public SubCatInfo data;

        [DataMember(Name = "id")]
        [JsonProperty("id")]

        public string id { get; set; }
        [JsonIgnore]
        public DNode prev;

        [DataMember(Name = "next")]
        [JsonProperty("next")]
        public List<DNode> next = new List<DNode>();
        public int level
        {
            get
            {

                if (prev == null)
                    return 0;
                else
                    return prev.level + 1;
            }
            set { }
        }
        public DNode(SubCatInfo d)
        {
            
            data = d;
            prev = null;

            //next = null;
        }
}
}
