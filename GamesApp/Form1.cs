using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace Spanzuratoarea
{
    public partial class Form1 : Form
    {
        private string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DateDB.mdf;Integrated Security=True";
        string pathCuv = $@"{Directory.GetCurrentDirectory()}\cuvinte.txt";
        string pathImg = $@"{Directory.GetCurrentDirectory()}\StadiiFloare";
        private System.Windows.Forms.Button[,] btnMat;
        private Label[] lblMat;
        private TableLayoutPanel tbl = null;
        private TableLayoutPanel tbl1 = null;
        private string lit = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int n, m;
        List<string> cuvinte = new List<string>();
        string cuvI;
        bool ales = false;
        int imgCnt = 1;
        int punctaj = 0;
        string nume;
        int cnt = 0, gasit = 0;
        Random rand = new Random();

        //pt artificii
        List<string> imageLoc = new List<string>();
        List<Firework> fireworkList = new List<Firework>();

        public Form1(string n)
        {
            InitializeComponent();
            LetterMatrix();
            ReadData();
            nume = n;
            picBox.Image = Image.FromFile(pathImg + @"\1.png");

            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
        }

        void ReadData()
        {
            StreamReader sr = new StreamReader(pathCuv);
            string line = null;

            while((line = sr.ReadLine()) != null)
            {
                cuvinte.Add(line);
            }
        }

        void LetterMatrix()
        {
            n = 3;
            m = (int)Math.Ceiling((double)lit.Length / n);
            int cnt = 0;
            btnMat = new System.Windows.Forms.Button[n, m];
            tbl = new TableLayoutPanel()
            {
                RowCount = n,
                ColumnCount = m,
                Dock = DockStyle.Fill
            };
            int L = Math.Min(panelBtnMatrix.Width, panelBtnMatrix.Height) / Math.Max(m, n);

            for (int j = 0; j < m; j++)
                tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, L));

            for (int i = 0; i < n; ++i)
            {
                tbl.RowStyles.Add(new RowStyle(SizeType.Percent, L));
                for (int j = 0; j < m; ++j)
                {
                    if (cnt < lit.Length)
                    {
                        btnMat[i, j] = new System.Windows.Forms.Button()
                        {
                            BackColor = Color.White,
                            Text = lit[cnt].ToString(),
                            Padding = new Padding(1),
                            Width = 50,
                            Height = 50,
                            Font = new Font("Arial", 11)
                        };

                        cnt++;
                        btnMat[i, j].Click += BtnPress;
                        tbl.Controls.Add(btnMat[i, j]);
                    }
                }
            }
            panelBtnMatrix.Controls.Add(tbl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }

        void WordSpace(int length)
        {
            lblMat = new Label[length];
            tbl1 = new TableLayoutPanel()
            {
                RowCount = 1,
                ColumnCount = length,
                AutoSize = true,
            };
            for(int i = 0; i < length; ++i)
            {
                lblMat[i] = new Label()
                {
                    Text = "_",
                    Padding = new Padding(3),
                    AutoSize = true,
                    Width = 50,
                    Height=50,
                    Font = new Font("Arial", 11)
                };
                tbl1.Controls.Add(lblMat[i]);
            }panelWord.Controls.Add(tbl1);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int x = random.Next(cuvinte.Count);
            cuvI = cuvinte[x];
            ales = true;
            btnStart.Enabled = false;
            WordSpace(cuvI.Length);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AlegeJoc f = new AlegeJoc(nume);
            f.Closed += (s, args) => this.Close();
            f.Show();
        }
        
        private void BtnPress(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            string t = btn.Text;
            btn.Enabled = false;
            bool found = false;

            if(ales == false)
            {
                MessageBox.Show("Cuvantul nu a fost ales");
                btn.Enabled = true;
                return;
            }

            for(int i = 0; i < cuvI.Length; ++i)
            {
                if (string.Equals(cuvI[i].ToString(), t, StringComparison.OrdinalIgnoreCase))
                {
                    lblMat[i].Text = cuvI[i].ToString();
                    gasit++;
                    if(imgCnt <= 6 && imgCnt != 6)
                        imgCnt++;
                    picBox.Image = Image.FromFile(pathImg + @"\" + imgCnt + ".png");
                    found = true;
                }
            }

            if (gasit == cuvI.Length)
            {
                punctaj = (100 - 4 * cnt);
                lblPunctaj.Text = "Punctaj: " + punctaj.ToString();
                UpdateDatabase(punctaj);
                if (punctaj > 50)
                {               
                    timerF.Start();
                    painted = true;
                }
                timerClose.Start();
                return;
            }

            if (found == false)
            {
                cnt++;
                if (imgCnt <= 6 && imgCnt > 0 && imgCnt != 1)
                    imgCnt--;
                picBox.Image = Image.FromFile(pathImg + @"\" + imgCnt + ".png");
            }
        }

        void UpdateDatabase(int s)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string query = "UPDATE Utilizatori SET ScorSpanzuratoarea = @s WHERE Nume = @n";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@s", s);
                cmd.Parameters.AddWithValue("@n", nume);
                 cmd.ExecuteNonQuery();                
            }
        }

        private void timerF_Tick(object sender, EventArgs e)
        {
            Point mousePosition = new Point();
            mousePosition.X = rand.Next(10, Math.Max(this.Width, this.Height));
            mousePosition.Y = rand.Next(10, Math.Max(this.Width, this.Height));

            Firework newFirework = new Firework();
            newFirework.position.X = mousePosition.X - (newFirework.width / 2);
            newFirework.position.Y = mousePosition.Y - (newFirework.height / 2);
            fireworkList.Add(newFirework);
            AnimationTimerEvent(sender, e);
        }

        bool painted = false;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            if (painted == true)
            {
                Brush b = new SolidBrush(Color.DarkBlue);
                e.Graphics.DrawString(punctaj.ToString(), new Font("Arial", 100), b, 
                    new Point(300, 150));
                foreach (Firework newFirework in fireworkList.ToList())
                {
                    if (newFirework.animationComplete == false)
                    {
                        e.Graphics.DrawImage(newFirework.firework, newFirework.position.X, newFirework.position.Y, newFirework.width, newFirework.height);
                    }
                }
            }
        }

        void HideAll()
        {
            picBox.Visible = false;
            btnStart.Visible = false;
            panelBtnMatrix.Visible = false;
            panelWord.Visible = false;
            lblPunctaj.Visible = false;
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            timerClose.Stop();
            this.Hide();
            AlegeJoc f = new AlegeJoc(nume);
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        public void AnimationTimerEvent(object sender, EventArgs e)
        {
            if (fireworkList != null)
            {
                foreach (Firework firework in fireworkList.ToList())
                {
                    if (firework.animationComplete == false)
                    {
                        firework.AnimateFireWork();
                    }
                    else
                    {
                        fireworkList.Remove(firework);
                    }
                }
            }
            HideAll();
            this.Invalidate();
        }
    }
}
