using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class TreeGenerator
    {


        public static DNode Regenerator(DNode newNode, Model.ListTemplateGuide tempGuid,List<DNode> savedDnodes)
        {
            DNode headerNode = new DNode(new SubCatInfo()) { };
            //  در اوردن پدرهای این نود که وارد شده و مشخص کردن پدر یا پدرهای این نود در الگوی راهنما
            var allGoalTemplate = tempGuid.template.Where(p => p.Child == newNode.data.tempInt).ToList();
            //در اوردن نودهایی که بسکتشون پر شده تا الان-- میخواد ببینه نود ورودی زیر کدوم پدرها می تونه بره
            //در اوردن نودهایی از بسکت های پرشده که بچشون برابر  پدر نود وارد شده باشه -- نودهای پدری که میتوانند این نود را به فرزندی بگیرند پی پرنت نود ورودی است
            if (allGoalTemplate.Count > 0)
            {//item12..Add(newNode);
                var mainNode = allGoalTemplate.FirstOrDefault();
                if (allGoalTemplate.Count > 1)
                {
                    //بر اساس لول ها عمیق ترین پدر ممکن در بین پدرهای موجود در درخت را پیدا میکد
                    var tf = tempGuid.template.Where(r => r.MaxLevel != null).GroupBy(
                     q => q.MaxLevel.level, q => q, (key, g) => new { maxLevel = key, tempGuides = g.ToList() }).OrderByDescending(r=>r.maxLevel).ToList();

                    if (tf.Count>0 )
                    {
                        //
                        var ppar = allGoalTemplate.Select(p => p.Parent).ToList();
                        //از بین پدرهای ممکن در پایش قبلی ، ون هایی که قابلیت داشتن فرزند در الگوی راهنما را داشته را بیاب
                        var tt = tf.Where(s => s.tempGuides.Any(h =>ppar.Contains( h.Child))).OrderByDescending(t=>t.maxLevel).ToList();
                        if (tt.Count > 0)
                        {
                            //ن. پیداشده را به مین اضافه کن
                            var hh = allGoalTemplate.Where(r => tt[0].tempGuides.Any(h => h.Child == r.Parent)).First();
                            mainNode = hh;
                        }
                    }
                    else
                    {
                        savedDnodes.Add(newNode);
                    }
                }
                mainNode.dnodes.Add(newNode);
            }
            else
            {
                //در غیر خالت بالا نود ورودی را به درخت اضافه کن
                tempGuid.template.Where(p => p.Child == newNode.data.tempInt).FirstOrDefault().dnodes.Add(newNode);
            }
            //شروع چیدن درخت
            var findAllTemplateHasNodes = new List<TemplateGuide>();
            //همه نودهایی که بسکت دار شدن رو پیدا میکنه
            findAllTemplateHasNodes.AddRange(tempGuid.template.Where(p => p.BasketCount > 0));
            findAllTemplateHasNodes.ForEach(p => p.dnodes.ForEach(q =>
            {
                q.next = new List<DNode>();
                q.prev = null;
            }));
            //شروع ساخت درخت اصلی
            var mainTemplateTree = new List<TemplateGuide>();
            bool findHeader = false;
            while (true)
            {
                //اون نودهایی که باباشون توی هبچ کدوم از نودهای پیدا شده تا به اینجا نباشه ینی 1.1 بچه کسی هست یتا نه
                var findAllTemplateHasNoParent = findAllTemplateHasNodes.Where(p => !findAllTemplateHasNodes.Any(q => q.Child == p.Parent && q.BasketCount > 0)).ToList();
                //این قسمت همیشه روت درخت را ایجاد می کند
                if (findAllTemplateHasNoParent.Count > 0 && !findHeader)
                {
                    //اگر هدر را پیدا نکرده بود تا اینجا، برای نود جاری یک پریویوس هدر میذارد
                    findAllTemplateHasNoParent.ForEach(t => t.dnodes.ForEach(r => r.prev = headerNode));
                    findHeader = true;
                    //-----------اضافه کردن روت اصلی به درخت
                    mainTemplateTree.AddRange(findAllTemplateHasNoParent);
                    //نود پیدا شده را بعد از هدر قرار بده
                    findAllTemplateHasNoParent.ForEach(p => headerNode.next.AddRange(p.dnodes));

                }
                else
                {
                    //لوپ برای نودهایی که مشخص شده در درخت هستند 
                    findAllTemplateHasNodes.ForEach(q =>
                    {
                        //اگر رابطه ای پدر فرزندی بین نودها پیدا کردی بیا فرزندان تون نود را زیرش قرار بده
                        var guid = tempGuid.template.Where(a => a.Child == q.Child).ToList();


                        //پرنت اون نودی رو پیدا میکند که قراره ثبت شه
                        var parent = mainTemplateTree.Where(p => p.Child == q.Parent).FirstOrDefault();
                      
                        if (parent != null)
                        {

                            var tt = parent.dnodes.FirstOrDefault();
                            tt.next.AddRange(q.dnodes);
                            tt.next.ForEach(r => r.prev = tt);

                        }
                        else
                        {
                            //var tt=guid.Where(r => r.BasketCount > 0).FirstOrDefault();
                            //if(tt==null)
                            //{

                            //}
                            //var tt2 = tt.dnodes.FirstOrDefault();
                            //tt2.next.AddRange(q.dnodes);
                            //tt2.next.ForEach(r => r.prev = tt2);
                        }

                    });

                }
                //این نودی که اضافه شده است را از لیست نودهای اصلی برای ساخت ادامه درخت  حذف کن
                findAllTemplateHasNoParent.ForEach(p => findAllTemplateHasNodes.Remove(p));
                mainTemplateTree.AddRange(findAllTemplateHasNoParent);
                mainTemplateTree = mainTemplateTree.Distinct().ToList();
                if (findAllTemplateHasNodes.Count == 0)
                    break;
            }
            return headerNode;
        }
        public static void GenTree(List<DNode> dnodes, DNode dnode1)
        {
            var levels = dnodes.Where(p => dnode1.data.tempInt <= p.data.tempInt).GroupBy(
                     q => q.level, q => q, (key, g) => new { level = key, node = g.ToList() }).OrderBy(p => p.level).ToList();

            if (levels.Count == 1)
            {

                dnode1.prev = levels[0].node.First();
                dnode1.prev.next.Add(dnode1);
                dnodes.Add(dnode1);
                return;
            }
            bool mainCheck = false;
            for (int i = levels.Count - 1; i >= 0; i--)
            {
                bool check = false;
                foreach (var inx in levels[i].node)
                {
                    //بر اساس یکسری معیار ها باید مقدار check  پر شود
                    if (dnode1.data.tempInt == inx.data.tempInt)
                    {

                        inx.prev.next.Add(dnode1);
                        dnode1.prev = inx.prev;
                        dnodes.Add(dnode1);
                        check = true;
                        break;
                    }
                    else if (dnode1.data.tempInt < inx.data.tempInt)
                    {
                        var tpt = inx.next.Where(p => p.data.tempInt < dnode1.data.tempInt).OrderBy(a => a.data.tempInt).ToList();
                        if (tpt.Count == 0)
                        {

                            dnode1.prev = inx;
                            inx.next.Add(dnode1);
                            dnodes.Add(dnode1);
                            check = true;
                            break;
                        }
                        else
                        {
                            var mainObj = tpt.First();
                            dnode1.prev = mainObj.prev;
                            mainObj.prev = dnode1;
                            dnode1.next.Add(mainObj);
                            dnode1.prev.next.Remove(mainObj);
                            dnode1.prev.next.Add(dnode1);

                            dnodes.Add(dnode1);
                            check = true;
                            break;
                        }
                    }
                    check = CheckPriorityTreeNode.CheckPriroty(inx.data.Temp, dnode1.data.Temp);

                    if (check)
                    {

                        inx.next.Add(dnode1);
                        dnodes.Add(dnode1);
                        break;
                    }
                }
                mainCheck = check;
                if (check)
                    break;
            }
            if (!mainCheck)
            {
                dnode1.prev = levels[0].node.First();
                levels[0].node.First().next.Add(dnode1);
                dnodes.Add(dnode1);
            }
        }

        public static void GetTreeFlatLine(DNode rootNode, List<List<DNode>> MainList)
        {
            //MainList = new List<List<DNode>>();
            Model.TreeGenerator.FindTreeLine(rootNode, new List<DNode>(), MainList);
            foreach (var item in MainList)
            {
                var sb = new List<string>();
                foreach (var ind in item)
                {
                    sb.Add(ind.id);
                }
                var bb = string.Join(",", sb.ToArray());
                // Debug.WriteLine(bb);
                Console.WriteLine(bb);
            }
        }

        public static void FindTreeLine(DNode node, List<DNode> line, List<List<DNode>> MainList)
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
                FindTreeLine(item, newLine, MainList);

            }

        }
    }
}
