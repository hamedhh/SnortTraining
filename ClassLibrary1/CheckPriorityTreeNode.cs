using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class CheckPriorityTreeNode
    {
        public static bool CheckPriroty(string currentTag, string lastTag)
        {

            if (lastTag == "1.1" && currentTag == "1.4")
                return true;
            if (lastTag == "1.4" && (currentTag == "1.2" || currentTag == "3.2" || currentTag == "3.3" || currentTag == "3.1" || currentTag == "4.2"))
                return true;
            if (lastTag == "1.2" && (currentTag == "3.2" || currentTag == "3.2" || currentTag == "3.1" || currentTag == "4.2"))
                return true;
            if (lastTag == "3.2" && (currentTag == "3.3" || currentTag == "2.2" || currentTag == "2.3" || currentTag == "2.1" || currentTag == "4.6"))
                return true;
            if (lastTag == "2.1" && (currentTag == "4.4" || currentTag == "4.6" || currentTag == "4.1" || currentTag == "1.3" || currentTag == "4.5"))
                return true;
            if (lastTag == "2.1" && (currentTag == "4.4" || currentTag == "4.6" || currentTag == "4.1" || currentTag == "1.3" || currentTag == "4.5"))
                return true;
            if (lastTag == "3.3" && (currentTag == "3.2" || currentTag == "2.2" || currentTag == "2.3" || currentTag == "2.1" || currentTag == "4.6"))
                return true;
            if (lastTag == "2.2" && (currentTag == "2.3" || currentTag == "4.6" || currentTag == "2.1"))
                return true;
            if (lastTag == "3.1" && (currentTag == "4.4" || currentTag == "4.6" || currentTag == "4.1" || currentTag == "1.3" || currentTag == "4.5"))
                return true;
            if (lastTag == "2.3" && (currentTag == "2.2" || currentTag == "2.1" || currentTag == "4.6"))
                return true;
            return false;
        }
    }
}
