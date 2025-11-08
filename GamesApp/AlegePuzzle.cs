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

namespace Spanzuratoarea
{
    public partial class AlegePuzzle : Form
    {
        string pathL = $@"{Directory.GetCurrentDirectory()}\ImaginiPuzzle1\";
        int piese = 0;
        public AlegePuzzle(string n)
        {
            InitializeComponent();
            
            LoadImg();
        }

        void LoadImg()
        {
            pictureBox1.Image = Image.FromFile(pathL + "1.jpg");
            pictureBox2.Image = Image.FromFile(pathL + "2.jpg");
            pictureBox3.Image = Image.FromFile(pathL + "3.jpg");
            pictureBox4.Image = Image.FromFile(pathL + "4.jpg");
            pictureBox5.Image = Image.FromFile(pathL + "5.jpg");
            pictureBox6.Image = Image.FromFile(pathL + "6.jpg");
        }

        private void AlegePuzzle_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (piese == 9)
            {
                if (radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 4, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 4, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
            else if (piese == 4)
            {
                if (radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 4, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 4, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            if (piese == 9)
            {
                if(radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 5, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 5, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
            else if (piese == 4)
            {
                if (radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 5, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 5, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (piese == 9)
            {
                if (radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 3, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 3, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
            else if (piese == 4)
            {
                if (radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 3, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 3, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (piese == 9)
            {
                if (radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 1, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 1, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
            else if (piese == 4)
            {
                if (radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 1, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 1, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (piese == 9)
            {
                if (radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 2, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 2, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
            else if (piese == 4)
            {
                if (radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 2, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 2, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (piese == 9)
            {
                if (radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 6, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle f = new Puzzle(piese, 6, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
            else if (piese == 4)
            {
                if (radioButton1.Checked == false)
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 6, 0);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                {
                    this.Hide();
                    Puzzle2 f = new Puzzle2(piese, 6, 1);
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            piese = int.Parse(comboBox1.Text);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            string r = $@"{Directory.GetCurrentDirectory()}\ImaginiPuzzle2\";
            pictureBox1.Image = Image.FromFile(r + "1.jpg");
            pictureBox2.Image = Image.FromFile(r + "2.jpg");
            pictureBox3.Image = Image.FromFile(r + "3.jpg");
            pictureBox4.Image = Image.FromFile(r + "4.jpg");
            pictureBox5.Image = Image.FromFile(r + "5.jpg");
            pictureBox6.Image = Image.FromFile(r + "6.jpg");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AlegeJoc f = new AlegeJoc("");
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LoadImg();
        }
    }
}
