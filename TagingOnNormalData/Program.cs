using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagingOnNormalData
{
    class Program
    {
        static void Main(string[] args)
        {
            TagToData tag = new TagToData();
            tag.taging();
            Console.WriteLine("Almost Process of Taging Data is Done....");
            Console.ReadKey();

        }
    }
}
