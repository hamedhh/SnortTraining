using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Northwoods.Go;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Testanimate
{
    public class MainForm : System.Windows.Forms.Form
    {
        private Northwoods.Go.GoView goView1;
        private Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private ListView listView1;
        private IContainer components;
        private static int selectedNode = 0;
        List<rootTrack> rootTracks;//= new IEnumerable<rootTrack>();
       
        //private Graphics gg;

         

        public MainForm()
        {
            InitializeComponent();
            // Customize the standard kind of link that is drawn.
            goView1.NewLinkPrototype = new AnimatedLink();
            // enable undo and redo
            goView1.Document.UndoManager = new GoUndoManager();


            var strPath = new System.IO.StreamReader(@"C:\Users\Hamed_201X\Desktop\ProjectMalek\ExportTracks\7.204.241.161_25.txt");
            var resStr = strPath.ReadToEnd();
            rootTracks = JsonHelper.JsonDeserialize<List<rootTrack>>(resStr);


            int[,] points = new int[,] {
            { 213,172 },
            { 222, 94 },
            { 261,236}
        };
            string[] Labels = { "Scanning", "Root_Admin", "Goal_Dos" };

            for (int i = 0; i < 3; i++)
            {

                InsertNode(new PointF(points[i, 0], points[i, 1]), true, Labels[i]);

            }
           
            //RectangleF f = new RectangleF()
            //{
            //    Height = 113,
            //    Location = new PointF(167, 151),
            //    Size = new SizeF(152, 113),
            //    Width = 28,
            //    X = 167,
            //    Y = 151
            //};

            //g.DpiX = 96;
            //g.DpiY = 96;
            //g.VisibleClipBounds = f;

            //GoLink link = new GoLink()
            //{
            //    AdjustingStyle = GoLinkAdjustingStyle.Calculate,
            //    AutoRescales = true,
            //    AvoidsNodes = false,
            //    Bottom = 221,
            //    Bounds = new RectangleF(223, 186, 26, 35),
            //    Brush = new SolidBrush(GetRandomColor(100)),
            //    BrushColor = Color.White,
            //    BrushFocusScales = new SizeF(0, 0),
            //    BrushStyle = GoBrushStyle.Solid,
            //    Center = new PointF(236, 203),
            //    Copyable = true,
            //    Curviness = 10,
            //    Deletable = true,
            //    DraggableOrthogonalSegments = false,
            //    FromArrow = false,
            //    Size = new SizeF(26, 35),
            //    ToArrow = true,
            //};
            //if (link != null)
            //{
            //    Color c = GetRandomColor(100);
            //    link.PenColor = c;
            //    link.PenWidth = 2;
            //    link.BrushColor = c;
            //}
            //GoDocument goDoc = new GoDocument();
            //goDoc.Bounds = new RectangleF(8, 48, 464, 336);
            //GoView goview = new GoView(goDoc);
            //goview.Bounds = new Rectangle(0, 0, 300, 248);

            //var res =link.FromArrowAnchorPoint;
            //link.Paint(gg, goview);





        }
        [DataContract]
        public class rootTrack
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string Des { get; set; }
            [DataMember]
            public string Classification { get; set; }
            [DataMember]
            public string Priority { get; set; }
            [DataMember]
            public string time { get; set; }
            [DataMember]
            public string SrcIP { get; set; }
            [DataMember]
            public string DesIP { get; set; }
            [DataMember]
            public string Tags { get; set; }
            [DataMember]
            public string TagName { get; set; }
        }
        public class JsonHelper
        {
            /// <summary>
            /// JSON Serialization
            /// </summary>
            public static string JsonSerializer<T>(T t)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, t);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            /// <summary>
            /// JSON Deserialization
            /// </summary>
            public static T JsonDeserialize<T>(string jsonString)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }


        }



        public void initListView(string tagid)
        {
         
            var res = rootTracks.Where(a => a.Tags.Contains("1.4")).ToList();

            listView1.View = View.Details;
            listView1.Columns.Add("id", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Des", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Classification", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Priority", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("time", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("SrcIP", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("DesIP", -2, HorizontalAlignment.Left);
            //listView1.Columns.Add("Tags", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("TagName", -2, HorizontalAlignment.Left);
            foreach (var rootitem in rootTracks)
            {
                var viewitem = new ListViewItem(new[]
                {
                    rootitem.id,
                    rootitem.Des,
                    rootitem.Classification,
                    rootitem.Priority,
                    rootitem.time,
                    rootitem.SrcIP,
                    rootitem.DesIP,
                    //rootitem.Tags,
                    rootitem.TagName,

                });
                viewitem.BackColor = Color.Red;
                viewitem.Font = new Font("Tahoma", 10, FontStyle.Bold);
                listView1.Items.Add(viewitem);
            }

            //var item1 = new ListViewItem(new[] { "id123", "Tom", "24" });
            //var item2 = new ListViewItem(new[] { "id3220", "cat", "sasd" });
            //item1.BackColor = Color.Red;
            //listView1.Items.Add(item1);
            //listView1.Items.Add(item2);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.goView1 = new Northwoods.Go.GoView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // goView1
            // 
            this.goView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.goView1.ArrowMoveLarge = 10F;
            this.goView1.ArrowMoveSmall = 1F;
            this.goView1.BackColor = System.Drawing.Color.Black;
            this.goView1.DragsRealtime = true;
            this.goView1.Location = new System.Drawing.Point(0, 0);
            this.goView1.Name = "goView1";
            this.goView1.SecondarySelectionColor = System.Drawing.Color.Chartreuse;
            this.goView1.Size = new System.Drawing.Size(443, 543);
            this.goView1.TabIndex = 0;
            this.goView1.Text = "goView1";
            this.goView1.LinkCreated += new Northwoods.Go.GoSelectionEventHandler(this.goView1_LinkCreated);
            this.goView1.BackgroundDoubleClicked += new Northwoods.Go.GoInputEventHandler(this.goView1_BackgroundDoubleClicked);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Location = new System.Drawing.Point(438, 0);
            this.listView1.Name = "listView1";
            this.listView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listView1.Size = new System.Drawing.Size(563, 543);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1001, 543);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.goView1);
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "مراحل ردگیری به تفکیک هر گام";
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Create a GoBasicNode with random colors and editable middle label,
        /// and add it to the document at the given point.
        /// </summary>
        /// <param name="pt">the location of the new node (the center of the shape)</param>
        /// <param name="rectangular">whether the node has a rectangle shape instead of elliptical</param>
        /// <returns>a GoBasicNode</returns>
        private GoBasicNode InsertNode(PointF pt, bool rectangular, string title)
        {
            GoDocument doc = goView1.Document;
            doc.StartTransaction();
            GoBasicNode n = new GoBasicNode();
            n.LabelSpot = GoObject.Middle;
            n.Text = title;//(++myNodeCounter).ToString();
                           //if (rectangular)
                           //n.Shape = new GoRectangle(); ;
                           // specify the position and colors
            n.Location = pt;
            n.Brush = new SolidBrush(GetRandomColor(100));
            n.Shape.PenColor = GetRandomColor(130);
            n.Shape.PenWidth = 3;
            // allow the user to edit the text in-place
            //n.Label.Editable = true;
            doc.Add(n);
            doc.FinishTransaction("inserted node");
            return n;
        }

        private Color GetRandomColor(int b)
        {
            return Color.FromArgb(b + NextRandom(220 - b), b + NextRandom(250 - b), b + NextRandom(250 - b));
        }

        private int NextRandom(int i)
        {
            return myRandom.Next(i);
        }

        private Random myRandom = new Random();
        private int myNodeCounter = 0;

        // When the user double-clicks in the background, create a new node there
        private void goView1_BackgroundDoubleClicked(object sender, Northwoods.Go.GoInputEventArgs e)
        {
            InsertNode(e.DocPoint, (myNodeCounter % 2 == 0), "1");
        }

        // When a link is drawn by the user, give it a random color
        private void goView1_LinkCreated(object sender, Northwoods.Go.GoSelectionEventArgs e)
        {
            initListView("1.2");
            var res = goView1.Document;
            GoLink link = e.GoObject as GoLink;
            if (link != null)
            {
                Color c = GetRandomColor(100);
                link.PenColor = c;
                link.PenWidth = 2;
                link.BrushColor = c;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            goView1.Document.SkipsUndoManager = true;
            foreach (GoObject obj in goView1.Document)
            {
                AnimatedLink link = obj as AnimatedLink;
                if (link != null) link.Step();
            }
            goView1.Document.SkipsUndoManager = false;
        }

        private void timer2_Tick(object sender, System.EventArgs e)
        {
            goView1.Document.SkipsUndoManager = true;
            foreach (GoObject obj in goView1.Document)
            {
                GoBasicNode n = obj as GoBasicNode;
                if (n != null)
                {
                    if (n.Shape.PenWidth == 2)
                        n.Shape.PenWidth = 4;
                    else
                        n.Shape.PenWidth = 2;
                }
            }
            goView1.Document.SkipsUndoManager = false;
        }
    }

    [Serializable]
    public class AnimatedLink : GoLink
    {
        public AnimatedLink()
        {
            this.Reshapable = false;
            this.HighlightWhenSelected = true;
            this.HighlightPenColor = Color.Chartreuse;
            this.HighlightPenWidth = 5;
            this.PenColor = Color.White;
            this.PenWidth = 2;
            this.BrushColor = Color.White;
            this.ToArrow = true;
        }



        public override void Paint(Graphics g, GoView view)
        {

            
            base.Paint(g, view);
            GoStroke s = this;
            if (mySeg >= s.PointsCount - 1)
                mySeg = 0;
            PointF a = s.GetPoint(mySeg);
            PointF b = s.GetPoint(mySeg + 1);
            float len = (float)Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
            float x = b.X;
            float y = b.Y;
            if (myDist >= len)
            {
                mySeg++;
                myDist = 0;
            }
            else if (len >= 1)
            {
                x = a.X + (b.X - a.X) * myDist / len;
                y = a.Y + (b.Y - a.Y) * myDist / len;
            }
            GoShape.DrawEllipse(g, view, null, Brushes.Red, x - 3, y - 3, 7, 7);
           
        }
        public void Step()
        {
            myDist += 3;
            this.InvalidateViews();
        }

        [NonSerialized]
        private int mySeg = 0;
        [NonSerialized]
        private float myDist = 0;
    }
}
