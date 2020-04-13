using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Diagnostics;

namespace Model
{
    public class TreeLinkList
    {
       


        public static void Test()
        {
            int i = 13;
            MainList = new List<List<DNode>>();
            var dnode9 = new DNode(new SubCatInfo()) { id = "dnode9" };
            var dnode10 = new DNode(new SubCatInfo()) { id = "dnode10" };
            var dnode5 = new DNode(new SubCatInfo()) { id = "dnode5", next = new List<DNode>() { dnode9, dnode10 } };
            var dnode6 = new DNode(new SubCatInfo()) { id = "dnode6" };
            var dnode2 = new DNode(new SubCatInfo()) { id = "dnode2", next=new List<DNode>() {dnode5,dnode6 } };
            var dnode11 = new DNode(new SubCatInfo()) { id = "dnode11" };
            var dnode12 = new DNode(new SubCatInfo()) { id = "dnode12" };
            var dnode7 = new DNode(new SubCatInfo()) { id = "dnode7", next = new List<DNode>() { dnode11, dnode12 } };
            var dnode8 = new DNode(new SubCatInfo()) { id = "dnode8" };
            var dnode3 = new DNode(new SubCatInfo()) { id = "dnode3", next = new List<DNode>() { dnode7, dnode8 } };
            var dnode4 = new DNode(new SubCatInfo()) { id = "dnode4" };
            var dnode1 = new DNode(new SubCatInfo()) { id = "dnode1",next=new List<DNode>() {dnode2,dnode3,dnode4 } };

            GetTreeFlatLine(dnode1);

        }
        public static List<List<DNode>> MainList = new List<List<DNode>>();
        public static void GetTreeFlatLine(DNode rootNode)
        {
            MainList = new List<List<DNode>>();
            FindTreeLine(rootNode,new List<DNode>());
            foreach(var item in MainList)
            {
                var sb = new List<string>();
                foreach(var ind in item)
                {
                    sb.Add(ind.id);
                }
                var bb = string.Join(",", sb.ToArray());
               // Debug.WriteLine(bb);
                Console.WriteLine(bb);
            }
        }

        public static void  FindTreeLine(DNode node, List<DNode> line)
        {
            
            if (node.next.Count == 0)
            {
                line.Add(node);
                MainList.Add(line);
                return;
            }
            
            foreach (var item in node.next)
            {
                var newLine = new List<DNode>();
                newLine.AddRange(line);
                newLine.Add(node);
                FindTreeLine(item,newLine);
                
            }
            
        }

            
    }
}
