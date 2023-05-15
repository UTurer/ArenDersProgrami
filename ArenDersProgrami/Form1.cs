using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArenZamanSayac
{
    public partial class Form1 : Form
    {

        private class CustomProgressBar : ProgressBar
        {
            
            //Property to hold the custom text
            public String Text_Left { get; set; }
            public String Text_Middle { get; set; }
            public String Text_Right { get; set; }

            public CustomProgressBar()
            {
                // Modify the ControlStyles flags
                //http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Rectangle rect = ClientRectangle;
                Graphics g = e.Graphics;

                ProgressBarRenderer.DrawHorizontalBar(g, rect);
                rect.Inflate(-3, -3);
                if (Value > 0)
                {
                    // As we doing this ourselves we need to draw the chunks on the progress bar
                    Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
                    ProgressBarRenderer.DrawHorizontalChunks(g, clip);
                }


                using (Font f = new Font(Form1.DefaultFont.FontFamily, Form1.DefaultFont.SizeInPoints))
                {
                    SizeF len = g.MeasureString(Text_Middle, f);
                    // Calculate the location of the text (the middle of progress bar)
                    // Point location = new Point(Convert.ToInt32((rect.Width / 2) - (len.Width / 2)), Convert.ToInt32((rect.Height / 2) - (len.Height / 2)));
                    Point location = new Point(Convert.ToInt32((Width / 2) - len.Width / 2), Convert.ToInt32((Height / 2) - len.Height / 2));
                    // The commented-out code will centre the text into the highlighted area only. This will centre the text regardless of the highlighted area.
                    // Draw the custom text
                    g.DrawString(Text_Middle, f, Brushes.Black, location);


                    len = g.MeasureString(Text_Left, f);
                    location = new Point(0, Convert.ToInt32((Height / 2) - len.Height / 2));
                    g.DrawString(Text_Left, f, Brushes.Black, location);

                    len = g.MeasureString(Text_Right, f);
                    location = new Point(Convert.ToInt32(Width-len.Width), Convert.ToInt32((Height / 2) - len.Height / 2));
                    g.DrawString(Text_Right, f, Brushes.Black, location);

                }
            }
        }

        private class Entry
        {
            public string Name = "";
            public System.DateTime Start = System.DateTime.MinValue;
            public System.DateTime End = System.DateTime.MaxValue;
        }

        private System.Collections.Generic.List<Entry> DersProgrami = null;
        private System.Collections.Generic.List<System.Windows.Forms.Label> LabelList = null;
        private System.Collections.Generic.List<CustomProgressBar> ProgressBarList = null;

        public Form1()
        {
            InitializeComponent();
        }

        private string baslik = "";
        private string zoom_id = "";
        private string zoom_pass = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.TopMost = true; //always on top yapmak için

            bool haftasonu = false;
            this.MinimumSize = new System.Drawing.Size(136, 154);

            DersProgrami = new System.Collections.Generic.List<Entry>();

            System.DateTime now = System.DateTime.Now;
            
            if(now.DayOfWeek == System.DayOfWeek.Monday)
            {
                baslik = "Pazartesi";
                zoom_id = "916 0255 6983";
                zoom_pass = "988215";
            }
            if (now.DayOfWeek == System.DayOfWeek.Tuesday)
            {
                baslik = "Salı";
                zoom_id = "916 0255 6983";
                zoom_pass = "988215";
            }
            if (now.DayOfWeek == System.DayOfWeek.Wednesday)
            {
                baslik = "Çarşamba";
                zoom_id = "916 0255 6983";
                zoom_pass = "988215";
            }
            if (now.DayOfWeek == System.DayOfWeek.Thursday)
            {
                baslik = "Perşembe";
                zoom_id = "916 0255 6983";
                zoom_pass = "988215";
            }
            if (now.DayOfWeek == System.DayOfWeek.Friday)
            {
                baslik = "Cuma";
                zoom_id = "916 0255 6983";
                zoom_pass = "988215";
            }
            if (now.DayOfWeek == System.DayOfWeek.Saturday)
            {
                baslik = "Cumartesi";
                zoom_id = "963 6546 4004";
                zoom_pass = "525408";
            }
            if (now.DayOfWeek == System.DayOfWeek.Sunday)
            {
                baslik = "Pazar";
                zoom_id = "963 6546 4004";
                zoom_pass = "525408";
            }

            baslik = baslik + " (Zoom ID: " + zoom_id + ", Şifre: " + zoom_pass + ")";
            this.Text = baslik;

            Entry entry;

            if (now.DayOfWeek == System.DayOfWeek.Saturday || now.DayOfWeek == System.DayOfWeek.Sunday)
            {
                haftasonu = true;
            }

            if(!haftasonu)
            {
                if (now.DayOfWeek == System.DayOfWeek.Monday || now.DayOfWeek == System.DayOfWeek.Tuesday || now.DayOfWeek == System.DayOfWeek.Wednesday || now.DayOfWeek == System.DayOfWeek.Thursday || now.DayOfWeek == System.DayOfWeek.Friday)
                {
                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 8, 20, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 8, 50, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 8, 50, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 9, 05, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 9, 05, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 9, 35, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 9, 35, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 9, 50, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 9, 50, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 10, 20, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 10, 20, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 10, 35, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 10, 35, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 11, 05, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 11, 05, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 11, 20, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 11, 20, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 11, 50, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 11, 50, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 12, 05, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 12, 05, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 12, 35, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 12, 35, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 13, 35, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 13, 35, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 14, 05, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 14, 05, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 14, 20, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 14, 20, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 14, 50, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 14, 50, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 15, 05, 0);
                    DersProgrami.Add(entry);

                    entry = new Entry();
                    entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 15, 05, 0);
                    entry.End = new System.DateTime(now.Year, now.Month, now.Day, 15, 35, 0);
                    DersProgrami.Add(entry);
                }

                if (now.DayOfWeek == System.DayOfWeek.Monday)
                {
                    DersProgrami[0].Name = "Türkçe";
                    DersProgrami[1].Name = "Teneffüs";
                    DersProgrami[2].Name = "Native";
                    DersProgrami[3].Name = "Teneffüs";
                    DersProgrami[4].Name = "Türkçe";
                    DersProgrami[5].Name = "Teneffüs";
                    DersProgrami[6].Name = "Müzik";
                    DersProgrami[7].Name = "Teneffüs";
                    DersProgrami[8].Name = "İngilizce";
                    DersProgrami[9].Name = "Teneffüs";
                    DersProgrami[10].Name = "Fen Bilimleri";
                    DersProgrami[11].Name = "Öğle Arası";
                    DersProgrami[12].Name = "Görsel Sanatlar";
                    DersProgrami[13].Name = "Teneffüs";
                    DersProgrami[14].Name = "Matematik";
                    DersProgrami[15].Name = "Teneffüs";
                    DersProgrami[16].Name = "İngilizce";

                }
                else if (now.DayOfWeek == System.DayOfWeek.Tuesday)
                {
                    DersProgrami[0].Name = "Türkçe";
                    DersProgrami[1].Name = "Teneffüs";
                    DersProgrami[2].Name = "Matematik";
                    DersProgrami[3].Name = "Teneffüs";
                    DersProgrami[4].Name = "İngilizce";
                    DersProgrami[5].Name = "Teneffüs";
                    DersProgrami[6].Name = "İngilizce";
                    DersProgrami[7].Name = "Teneffüs";
                    DersProgrami[8].Name = "Türkçe";
                    DersProgrami[9].Name = "Teneffüs";
                    DersProgrami[10].Name = "Türkçe";
                    DersProgrami[11].Name = "Öğle Arası";
                    DersProgrami[12].Name = "Beden Eğitimi";
                    DersProgrami[13].Name = "Tenefüs";
                    DersProgrami[14].Name = "Türkçe";
                    DersProgrami[15].Name = "Teneffüs";
                    DersProgrami[16].Name = "İngilizce";
                }
                else if (now.DayOfWeek == System.DayOfWeek.Wednesday)
                {
                    DersProgrami[0].Name = "İngilizce";
                    DersProgrami[1].Name = "Teneffüs";
                    DersProgrami[2].Name = "Hayat Bilgisi";
                    DersProgrami[3].Name = "Teneffüs";
                    DersProgrami[4].Name = "ETÜT Hayat Bilgisi";
                    DersProgrami[5].Name = "Teneffüs";
                    DersProgrami[6].Name = "Kodlama";
                    DersProgrami[7].Name = "Teneffüs";
                    DersProgrami[8].Name = "ETÜT Matematik";
                    DersProgrami[9].Name = "Teneffüs";
                    DersProgrami[10].Name = "Matematik";
                    DersProgrami[11].Name = "Öğle Arası";
                    DersProgrami[12].Name = "ETÜT Türkçe";
                    DersProgrami[13].Name = "Teneffüs";
                    DersProgrami[14].Name = "İngilizce";
                    DersProgrami[15].Name = "Teneffüs";
                    DersProgrami[16].Name = "Kulüp";
                }
                else if (now.DayOfWeek == System.DayOfWeek.Thursday)
                {
                    DersProgrami[0].Name = "Türkçe";
                    DersProgrami[1].Name = "Teneffüs";
                    DersProgrami[2].Name = "İngilizce";
                    DersProgrami[3].Name = "Teneffüs";
                    DersProgrami[4].Name = "Native";
                    DersProgrami[5].Name = "Teneffüs";
                    DersProgrami[6].Name = "Matematik";
                    DersProgrami[7].Name = "Teneffüs";
                    DersProgrami[8].Name = "Beden Eğitimi";
                    DersProgrami[9].Name = "Teneffüs";
                    DersProgrami[10].Name = "Fen Bilimleri";
                    DersProgrami[11].Name = "Öğle Arası";
                    DersProgrami[12].Name = "Hayat Bilgisi";
                    DersProgrami[13].Name = "Teneffüs";
                    DersProgrami[14].Name = "İngilizce";
                    DersProgrami[15].Name = "Teneffüs";
                    DersProgrami[16].Name = "Drama";
                }
                else if (now.DayOfWeek == System.DayOfWeek.Friday)
                {
                    DersProgrami[0].Name = "Türkçe";
                    DersProgrami[1].Name = "Teneffüs";
                    DersProgrami[2].Name = "Müzik";
                    DersProgrami[3].Name = "Teneffüs";
                    DersProgrami[4].Name = "Matematik";
                    DersProgrami[5].Name = "Teneffüs";
                    DersProgrami[6].Name = "İngilizce";
                    DersProgrami[7].Name = "Teneffüs";
                    DersProgrami[8].Name = "İngilizce";
                    DersProgrami[9].Name = "Teneffüs";
                    DersProgrami[10].Name = "Hayat Bilgisi";
                    DersProgrami[11].Name = "Öğle Arası";
                    DersProgrami[12].Name = "Fen Bilimleri";
                    DersProgrami[13].Name = "Teneffüs";
                    DersProgrami[14].Name = "İngilizce";
                    DersProgrami[15].Name = "Teneffüs";
                    DersProgrami[16].Name = "ETÜT Türkçe";
                }
            }
            else
            {
                entry = new Entry();
                entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 9, 0, 0);
                entry.End = new System.DateTime(now.Year, now.Month, now.Day, 9, 30, 0);
                DersProgrami.Add(entry);

                entry = new Entry();
                entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 9, 30, 0);
                entry.End = new System.DateTime(now.Year, now.Month, now.Day, 9, 50, 0);
                DersProgrami.Add(entry);

                entry = new Entry();
                entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 9, 50, 0);
                entry.End = new System.DateTime(now.Year, now.Month, now.Day, 10, 20, 0);
                DersProgrami.Add(entry);

                entry = new Entry();
                entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 10, 20, 0);
                entry.End = new System.DateTime(now.Year, now.Month, now.Day, 10, 30, 0);
                DersProgrami.Add(entry);

                entry = new Entry();
                entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 10, 30, 0);
                entry.End = new System.DateTime(now.Year, now.Month, now.Day, 11, 0, 0);
                DersProgrami.Add(entry);

                entry = new Entry();
                entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 11, 0, 0);
                entry.End = new System.DateTime(now.Year, now.Month, now.Day, 11, 10, 0);
                DersProgrami.Add(entry);

                entry = new Entry();
                entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 11, 10, 0);
                entry.End = new System.DateTime(now.Year, now.Month, now.Day, 11, 40, 0);
                DersProgrami.Add(entry);

                entry = new Entry();
                entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 11, 40, 0);
                entry.End = new System.DateTime(now.Year, now.Month, now.Day, 11, 50, 0);
                DersProgrami.Add(entry);

                entry = new Entry();
                entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 11, 50, 0);
                entry.End = new System.DateTime(now.Year, now.Month, now.Day, 12, 20, 0);
                DersProgrami.Add(entry);

                entry = new Entry();
                entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 12, 20, 0);
                entry.End = new System.DateTime(now.Year, now.Month, now.Day, 12, 30, 0);
                DersProgrami.Add(entry);

                entry = new Entry();
                entry.Start = new System.DateTime(now.Year, now.Month, now.Day, 12, 30, 0);
                entry.End = new System.DateTime(now.Year, now.Month, now.Day, 13, 0, 0);
                DersProgrami.Add(entry);

                if (now.DayOfWeek == System.DayOfWeek.Saturday)
                {
                    DersProgrami[0].Name = "Görsel Sanatlar";
                    DersProgrami[1].Name = "Teneffüs";
                    DersProgrami[2].Name = "Müzik";
                    DersProgrami[3].Name = "Teneffüs";
                    DersProgrami[4].Name = "İngilizce";
                    DersProgrami[5].Name = "Teneffüs";
                    DersProgrami[6].Name = "İngilizce";
                    DersProgrami[7].Name = "Teneffüs";
                    DersProgrami[8].Name = "Drama";
                    DersProgrami[9].Name = "Teneffüs";
                    DersProgrami[10].Name = "Satranç";
                }
            }

            LabelList = new System.Collections.Generic.List<System.Windows.Forms.Label>();
            ProgressBarList = new System.Collections.Generic.List<CustomProgressBar>();
            
            for (int i=0;i<DersProgrami.Count;i++)
            {
                System.Windows.Forms.Label label = new System.Windows.Forms.Label();
                label.Text = DersProgrami[i].Name;
                label.AutoSize = false;
                label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                label.Parent = this.splitContainer1.Panel1;
                label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                if(i == 0)
                {
                    label.Dock = System.Windows.Forms.DockStyle.Fill;
                }
                else
                {
                    label.Dock = System.Windows.Forms.DockStyle.Bottom;
                }
                
                LabelList.Add(label);

                CustomProgressBar customProgressBar = new CustomProgressBar();
                customProgressBar.Minimum = 0;
                customProgressBar.Maximum = 100;
                customProgressBar.Value = 0;
                customProgressBar.Parent = this.splitContainer1.Panel2;
                customProgressBar.Text_Left = DersProgrami[i].Start.ToShortTimeString();
                customProgressBar.Text_Right = DersProgrami[i].End.ToShortTimeString();
                customProgressBar.Text_Middle = "";
                if (i == 0)
                {
                    customProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
                }
                else
                {
                    customProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
                }
    
                ProgressBarList.Add(customProgressBar);

            }

            this.Width = ArenDersProgrami.Properties.Settings.Default.SettingFormWidth;
            this.Height = ArenDersProgrami.Properties.Settings.Default.SettingFormHeight;
            this.Left = ArenDersProgrami.Properties.Settings.Default.SettingFormPositionX;
            this.Top = ArenDersProgrami.Properties.Settings.Default.SettingFormPositionY;
            splitContainer1.SplitterDistance = ArenDersProgrami.Properties.Settings.Default.SettingSplitterDistance;
            
            Form1_Resize(this, null);

            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {         
            int h = splitContainer1.Height;

            for (int i=0;i<DersProgrami.Count;i++)
            {
                LabelList[i].Height = h / DersProgrami.Count;
                ProgressBarList[i].Height = h / DersProgrami.Count;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.DateTime now = System.DateTime.Now;

            int n = DersProgrami.Count;

            for (int i=0;i<DersProgrami.Count;i++)
            {
                if(now<DersProgrami[i].Start)
                {
                    n = i;
                    break;
                }
            }

        
            for (int i=0;i<n-1;i++)
            {
                ProgressBarList[i].Value = 100;
                ProgressBarList[i].Text_Middle = "0";
                ProgressBarList[i].Invalidate();
            }

            if (n > 0)
            {
                System.TimeSpan t1 = DersProgrami[n - 1].End - DersProgrami[n - 1].Start;
                System.TimeSpan t2 = now - DersProgrami[n - 1].Start;
                System.TimeSpan t3 = DersProgrami[n - 1].End - now;
                ProgressBarList[n - 1].Value = System.Math.Min((int)(t2.TotalSeconds / t1.TotalSeconds * 100), 100);
                ProgressBarList[n - 1].Text_Middle = System.Convert.ToInt32(t3.TotalMinutes).ToString();
                ProgressBarList[n - 1].Invalidate();
            }

            for (int i=n;i<DersProgrami.Count;i++)
            {
                ProgressBarList[i].Value = 0;
                System.TimeSpan t1 = DersProgrami[i].End - DersProgrami[i].Start;
                ProgressBarList[i].Text_Middle = System.Convert.ToInt32(t1.TotalMinutes).ToString();
                ProgressBarList[i].Invalidate();
            }

            this.Text = now.ToShortTimeString() + " " + baslik;

            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ArenDersProgrami.Properties.Settings.Default.SettingFormWidth = this.Width;
            ArenDersProgrami.Properties.Settings.Default.SettingFormHeight = this.Height;
            ArenDersProgrami.Properties.Settings.Default.SettingFormPositionX = this.Left;
            ArenDersProgrami.Properties.Settings.Default.SettingFormPositionY = this.Top;
            ArenDersProgrami.Properties.Settings.Default.SettingSplitterDistance = splitContainer1.SplitterDistance;
            ArenDersProgrami.Properties.Settings.Default.Save();

        }
    }
}
