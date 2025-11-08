using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spanzuratoarea
{
    public partial class AlegeJoc : Form
    {
        string nu;
        private string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DateDB.mdf;Integrated Security=True";
        public AlegeJoc(string n)
        {
            InitializeComponent();
            
            nu = n;
            SelectData();
            
        }


        //public AlegeJoc()
        //{
        //    InitializeComponent();
        //    string nu = Program.
        //    SelectData();
        //}
        //+

        void SelectData()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string query = "SELECT Nume, ScorSpanzuratoarea, ScorPuzzle FROM Utilizatori WHERE Nume = @e";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@e", nu);
                    using (var dr = cmd.ExecuteReader())
                    {

                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        dataGridView1.DataSource = dt;
                        //dataGridView1.HorizontalScrollingOffset = 0;
                        for (int i = 0; i < dt.Columns.Count; i++)
                            dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
        }

        private void AlegeJoc_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1(nu);
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AlegePuzzle f = new AlegePuzzle(nu);
            f.Closed += (s, args) => this.Close();
            f.Show();
        }
    }
}
