using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TemplateGuide
    {
        public TemplateGuide(double parent, double child)
        {
            Parent = parent;
            Child = child;
        }
        public double Parent { get; set; }
        public double Child { get; set; }

        public List<Model.DNode> dnodes = new List<Model.DNode>();
        public DNode MaxLevel
        {
            get
            {
                var item = dnodes.OrderByDescending(p => p.level).FirstOrDefault();
                return item;

            }
        }
        public int BasketCount { get { return dnodes.Count(); } }
    }


    public class ListTemplateGuide
    {

        public ListTemplateGuide()
        {
            template.Add(new TemplateGuide(-1, 1.1));
            template.Add(new TemplateGuide(1.1, 1.4));
            template.Add(new TemplateGuide(1.4, 1.2));
            template.Add(new TemplateGuide(1.4, 3.2));
            template.Add(new TemplateGuide(1.4, 3.3));
            template.Add(new TemplateGuide(1.4, 3.1));
            template.Add(new TemplateGuide(1.4, 4.2));
            template.Add(new TemplateGuide(1.2, 3.2));
            template.Add(new TemplateGuide(1.2, 3.3));
            template.Add(new TemplateGuide(1.2, 3.1));
            template.Add(new TemplateGuide(1.2, 4.2));
            template.Add(new TemplateGuide(3.2, 2.2));
            template.Add(new TemplateGuide(3.2, 2.3));
            template.Add(new TemplateGuide(3.2, 2.1));
            template.Add(new TemplateGuide(3.2, 4.6));
            template.Add(new TemplateGuide(2.2, 2.3));//
            template.Add(new TemplateGuide(2.2, 4.6));
            template.Add(new TemplateGuide(2.2, 2.1));
            template.Add(new TemplateGuide(3.3, 3.2));
            template.Add(new TemplateGuide(3.3, 2.2));
            template.Add(new TemplateGuide(3.3, 2.3));//
            template.Add(new TemplateGuide(3.3, 2.1));
            template.Add(new TemplateGuide(3.3, 4.6));
            template.Add(new TemplateGuide(2.1, 4.4));
            template.Add(new TemplateGuide(2.1, 4.6));
            template.Add(new TemplateGuide(2.1, 4.1));
            template.Add(new TemplateGuide(2.1, 1.3));
            template.Add(new TemplateGuide(2.1, 4.5));
            template.Add(new TemplateGuide(3.1, 4.4));
            template.Add(new TemplateGuide(3.1, 4.6));
            template.Add(new TemplateGuide(3.1, 4.1));
            template.Add(new TemplateGuide(3.1, 1.3));
            template.Add(new TemplateGuide(2.3, 2.2));
            template.Add(new TemplateGuide(2.3, 2.1));
            template.Add(new TemplateGuide(2.3, 4.6));


        }
        public List<TemplateGuide> template = new List<TemplateGuide>();



    }
}
