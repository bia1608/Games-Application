using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spanzuratoarea
{

    public partial class Login : Form
    {
        
        private string resPath = $@"{Directory.GetCurrentDirectory()}\";
        private string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DateDB.mdf;Integrated Security=True";
        public string nume;

        public Login()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
        }

        public string Nume
        {
            get { return nume; }
            set { nume = value; }
        }

        public string GetNume(string em)
        {
            string e = null;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string query = "SELECT Nume FROM Utilizatori WHERE Nume = @e";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@e", em);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            Nume = dr.GetString(0);
                        }
                    }
                }
            }
            return e;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string par = textBox2.Text;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string query = "SELECT * FROM Utilizatori WHERE Nume = @e AND Parola = @p";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@e", textBox1.Text);
                cmd.Parameters.AddWithValue("@p", par);

                var dr = cmd.ExecuteReader();
                if (dr.HasRows == false)
                    MessageBox.Show("Eroare autentificare!", "Eroare", MessageBoxButtons.OK);
                else
                {
                    nume = textBox1.Text;
                    AlegeJoc formM = new AlegeJoc(Nume); 
                    this.Hide();
                    formM.Closed += (s, args) => this.Close();
                    formM.Show(); //deschide form-ul nou
                }
            }
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button1.PerformClick();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button1.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.TextLength==0)
            {
                MessageBox.Show("Introduceti un nume de utilizator!", "Eroare", MessageBoxButtons.OK);
            }
            else
            {
                nume = textBox1.Text;
                AlegeJoc formM = new AlegeJoc(Nume);
                this.Hide();
                formM.Closed += (s, args) => this.Close();
                formM.Show(); //deschide form-ul nou
            }
        }
    }
}
