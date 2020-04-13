using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class HeplerTags
    {
        public static string getTagName(string tagId)
        {
            string TagName = "";
            switch (tagId)
            {
                case "1":
                    TagName = "Recognition";
                    break;
                case "1.1":
                    TagName = "FootPrinting";
                    break;
                case "1.2":
                    TagName = "Enumrate";
                    break;
                case "1.3":
                    TagName = "Sniffing";
                    break;
                case "1.4":
                    TagName = "Scaning";
                    break;
                case "1.5":
                    TagName = "OtherRecognition";
                    break;
                case "2":
                    TagName = "PrivilingEsc";
                    break;
                case "2.1":
                    TagName = "PrivilingEsc_Root_Admin";
                    break;
                case "2.2":
                    TagName = "PrivilingEsc_User";
                    break;
                case "2.3":
                    TagName = "PrivilingEsc_Other";
                    break;
                case "3":
                    TagName = "Intrusion";
                    break;
                case "3.1":
                    TagName = "Intrusion_Root_Admin";
                    break;
                case "3.2":
                    TagName = "Intrusion_User";
                    break;
                case "3.3":
                    TagName = "Intrusion_Other";
                    break;
                case "4":
                    TagName = "Goal";
                    break;
                case "4.1":
                    TagName = "Goal_Corruption";
                    break;
                case "4.2":
                    TagName = "Goal_DOS";
                    break;
                case "4.3":
                    TagName = "Goal_Ethical";
                    break;
                case "4.4":
                    TagName = "Goal_Espionage";
                    break;
                case "4.5":
                    TagName = "Goal_Backdoor";
                    break;
                case "4.6":
                    TagName = "Goal_Pilfering";
                    break;
                case "4.7":
                    TagName = "Goal_Sniffing";
                    break;
                case "4.8":
                    TagName = "Goal_Other";
                    break;


                default:
                    break;
            }
            return TagName;
        }

    }
}
