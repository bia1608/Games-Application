using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spanzuratoarea
{
    public partial class Puzzle2 : Form
    {
        string pathL = $@"{Directory.GetCurrentDirectory()}\ImaginiPuzzle1\";
        string pathL2 = $@"{Directory.GetCurrentDirectory()}\ImaginiPuzzle2\";

        List<PictureBox> pictureBoxes = new List<PictureBox>();
        List<PictureBox> pictureBoxes1 = new List<PictureBox>();
        List<Bitmap> images = new List<Bitmap>();
        List<string> locations = new List<string>();
        List<string> current_locations = new List<string>();

        string currentP;
        Bitmap MainBitmap;
        string winPos;

        int Piese;
        int Avans, Img;

        public Puzzle2(int piese, int img, int avans)
        {
            InitializeComponent();
            Piese = piese;
            Avans = avans;
            Img = img;
            if (avans == 0)
            {
                
                MainBitmap = new Bitmap(Image.FromFile(pathL + $"{img}.jpg"), new Size(400, 400));
                //pictureBox1.Image = Image.FromFile(pathL + $"{img}.jpg");
            }
            else
            {
                MainBitmap = new Bitmap(Image.FromFile(pathL2 + $"{img}.jpg"), new Size(400, 400));
                //pictureBox1.Image = Image.FromFile(pathL2 + $"{img}.jpg");
            }
            current_locations.Clear();
            pictureBoxes.Clear();
            pictureBoxes1.Clear();
            CreatePB();
            AddImg(piese);

        }

        void CreatePB() //stabilim piesele puzzle-ului
        {
            for (int i = 0; i < 4; i++)
            {
                PictureBox p = new PictureBox()
                {
                    Size = new Size(200, 200),
                    Tag = i.ToString(),
                };
                pictureBoxes1.Add(p);
                p.Click += ClickPic;
                pictureBoxes.Add(p);
                
                locations.Add(p.Tag.ToString());
            }
        }
        PictureBox emptyCopy = new PictureBox();
        private void ClickPic(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            PictureBox empty = pictureBoxes.Find(x => x.Tag.ToString() == "0");
            emptyCopy.Image = empty.Image;
            empty.BackgroundImage = null;
            empty.BackColor = Color.White;
            Point point1 = pb.Location;
            Point point2 = empty.Location;

            int ind1 = this.Controls.IndexOf(pb);
            int ind2 = this.Controls.IndexOf(empty);

            if (pb.Right == empty.Left && pb.Location.Y == empty.Location.Y //una langa alta, aceeasi linie
                || pb.Left == empty.Right && pb.Location.Y == empty.Location.Y
                || pb.Top == empty.Bottom && pb.Location.X == empty.Location.X //una langa alta, aceeasi coloana
                || pb.Bottom == empty.Top && pb.Location.X == empty.Location.X)
            {
                //Le schimbam intre ele
                pb.Location = point2;
                empty.Location = point1;

                //Schimbam indicii
                this.Controls.SetChildIndex(pb, ind2);
                this.Controls.SetChildIndex(empty, ind1);
            }
            
            current_locations.Clear();
            CheckGame();
        }

        void CropImg(Bitmap main, int h, int w)
        {
            int x, y;
            x = 0;
            y = 0;
            for (int blocks = 0; blocks < 4; blocks++)
            {
                Bitmap cropped_image = new Bitmap(h, w);
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        cropped_image.SetPixel(i, j, main.GetPixel((i + x), (j + y)));
                    }
                }
                images.Add(cropped_image);
                x += 200;
                if (x == 400)
                {
                    x = 0;
                    y += 200;
                }
            }
        }

        void AddImg(int piese)
        {
            Bitmap b = new Bitmap(MainBitmap, new Size(400, 400));
            //CropImg(b, b.Height / (piese / 2), b.Width / (piese / 2), Piese);
            CropImg(b, 200, 200);
            for (int i = 0; i < pictureBoxes.Count; i++)
            {
                if (i == 0)
                    emptyCopy.BackgroundImage = (Image)images[i];
                pictureBoxes[i].BackgroundImage = (Image)images[i];
            }
                
            PlacePB();
        }

        void PlacePB()
        {
            Random r = new Random();
            var shuffle = pictureBoxes.OrderBy(a => Guid.NewGuid()).ToList();
            pictureBoxes = shuffle;
            int x = 10, y = 10;
            for (int i = 0; i < pictureBoxes.Count; ++i)
            {
                pictureBoxes[i].BackColor = Color.Black;
                if (i == 2)
                {
                    y += 200;
                    x = 10;
                }
                pictureBoxes[i].BorderStyle = BorderStyle.FixedSingle;
                pictureBoxes[i].Location = new Point(x, y);

                this.Controls.Add(pictureBoxes[i]);
                x += 200;

                winPos += locations[i];
            }

        }

        void CheckGame()
        {
            foreach (Control x in this.Controls)
                if (x is PictureBox)
                    current_locations.Add(x.Tag.ToString());
            currentP = string.Join("", current_locations);
            if (winPos == currentP)
                MessageBox.Show("Ai castigat!", "", MessageBoxButtons.OK);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AlegePuzzle f = new AlegePuzzle("");
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var shuffle = pictureBoxes1.OrderBy(a => a.Tag.ToString()).ToList();
            pictureBoxes1 = shuffle;
            int x = 10, y = 10;
            for (int i = 0; i < pictureBoxes1.Count; ++i)
            {
                if (pictureBoxes[i].Tag.ToString() == "0")
                    pictureBoxes[i].BackgroundImage = emptyCopy.BackgroundImage;
                pictureBoxes1[i].BackColor = Color.Black;
                if (i == 2)
                {
                    y += 200;
                    x = 10;
                }
                pictureBoxes1[i].BorderStyle = BorderStyle.FixedSingle;
                pictureBoxes1[i].Location = new Point(x, y);

                this.Controls.Add(pictureBoxes1[i]);
                x += 200;

                winPos += locations[i];
            }
        }

        private void Puzzle2_Paint(object sender, PaintEventArgs e)
        {
            if(Avans == 0)
            {
                Bitmap b = new Bitmap(Image.FromFile(pathL + $"{Img}.jpg"), 130, 130);
                e.Graphics.DrawImage(b, new Point(460, 10));
            }
            else
            {
                Bitmap b = new Bitmap(Image.FromFile(pathL2 + $"{Img}.jpg"), 130, 130);
                e.Graphics.DrawImage(b, new Point(460, 10));
            }
        }
    }
}
