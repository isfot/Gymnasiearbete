using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Ant_test
{
    public partial class Form1 : Form
    {
        static string path = Environment.CurrentDirectory + "pic.png"; // Sökväg i hårdisken till kartan
        static Bitmap map; // Kartan som en bitmap
        static BitmapAVC mapAVC; // Kartan som en AVC bitmap
        static List<Ant> ants = new List<Ant>(); // En lista med alla myror
        public Form1()
        {
            InitializeComponent();
            map = new Bitmap(path);
            mapAVC = new BitmapAVC(map);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mapAVC.Upscale(10); // Skala upp kartan
            pictureBox.Image = mapAVC.get(); // Sätter kartan i picturebox
        }

        private void Timer_button_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }

        private void Ant_button_Click(object sender, EventArgs e)
        {
            ants.Add(new Ant(9, 1, 2, Color.Red));
            ants.Add(new Ant(10, 1, 2, Color.Red));
            ants.Add(new Ant(11, 1, 2, Color.Red));
            ants.Add(new Ant(12, 1, 2, Color.Red));
            ants.Add(new Ant(15, 26, 0, Color.Pink));
            ants.Add(new Ant(16, 26, 0, Color.Pink));
            ants.Add(new Ant(17, 26, 0, Color.Pink));
            ants.Add(new Ant(18, 26, 0, Color.Pink));
            ants.Add(new Ant(1, 16, 1, Color.Black));
            ants.Add(new Ant(1, 17, 1, Color.Black));
            ants.Add(new Ant(1, 18, 1, Color.Black));
            ants.Add(new Ant(1, 19, 1, Color.Black));
            ants.Add(new Ant(26, 8, 3, Color.Chocolate));
            ants.Add(new Ant(26, 9, 3, Color.Chocolate));
            ants.Add(new Ant(26, 10, 3, Color.Chocolate));
            ants.Add(new Ant(26, 11, 3, Color.Chocolate));

            richTextBox3.Text = (ants.Count.ToString());
            richTextBox4.Clear();
            foreach (Ant x in ants)
            {
                richTextBox4.AppendText(x.getPosX() + " : " + x.getPosY() + "\n");
            }
            foreach (Ant x in ants)
            {
                mapAVC.Setpixel(x.getPos(), x.Color);
            }
            pictureBox.Image = mapAVC.get();
        }
        static int counter;
        static Random rand = new Random();
        private void spawnrandom()
        {
            switch (rand.Next(1, 13))
            {
                case 1: ants.Add(new Ant(10, 1, 2, Color.Red)); break;
                case 2: ants.Add(new Ant(11, 1, 2, Color.Red)); break;
                case 3: ants.Add(new Ant(12, 1, 2, Color.Red)); break;
                case 4: ants.Add(new Ant(15, 26, 0, Color.Pink)); break;
                case 5: ants.Add(new Ant(16, 26, 0, Color.Pink)); break;
                case 6: ants.Add(new Ant(17, 26, 0, Color.Pink)); break;
                case 7: ants.Add(new Ant(1, 18, 1, Color.Black)); break;
                case 8: ants.Add(new Ant(1, 16, 1, Color.Black)); break;
                case 9: ants.Add(new Ant(1, 17, 1, Color.Black)); break;
                case 10: ants.Add(new Ant(26, 10, 3, Color.Chocolate)); break;
                case 11: ants.Add(new Ant(26, 11, 3, Color.Chocolate)); break;
                case 12: ants.Add(new Ant(26, 9, 3, Color.Chocolate)); break;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            antstep();
            if (counter % 3 == 0 && checkBox1.Checked)
            {
                for (int i = 0; i < 5; i++)
                {
                    spawnrandom();
                }
            }

        }

        //Step
        static List<int> ClearList = new List<int>();
        private void button1_Click(object sender, EventArgs e)
        {
            antstep();
            richTextBox2.Text = ants[3]._dir.ToString();
        }

        private int dirOverFlowCorr(int _dir)
        {
            if (_dir > 3)
            {
                _dir = 0;
            }
            else if (_dir < 0)
            {
                _dir = 3;
            }
            return _dir;
        }
        private bool antcheck(int a)
        {
            bool output = true;
            foreach (Ant x in ants)
            {

                int xplus = 0;
                int yplus = 0;
                switch (ants[a]._dir)
                {
                    case 0:
                        yplus--;
                        break;
                    case 1:
                        xplus++;
                        break;
                    case 2:
                        yplus++;
                        break;
                    case 3:
                        xplus--;
                        break;
                }
                if (x.getPosX() == ants[a].getPosX() + xplus && x.getPosY() == ants[a].getPosY() + yplus)
                {
                    output = false;
                }
            }
            return output;
        }

        private void antstep()
        {

            mapAVC.reset();
            mapAVC.Upscale(3);
            for (int a = 0; a < ants.Count; a++)
            {
                bool passthrough = antcheck(a); ;
                bool exists = true;
                switch (map.GetPixel(ants[a].getPosX(), ants[a].getPosY()).ToArgb())
                {
                    //red
                    case -65536:
                        passthrough = false;
                        ants.RemoveAt(a);
                        exists = false;
                        break;
                    //Blue
                    case -16776961:

                        ants[a]._dir++;
                        ants[a]._dir += 4;
                        ants[a]._dir %= 4;

                        ants[a]._dir = dirOverFlowCorr(ants[a]._dir);
                        if (!antcheck(a))
                        {
                            passthrough = false;
                            ants[a]._dir--;
                            ants[a]._dir += 4;
                            ants[a]._dir %= 4;
                        }
                        break;


                    //Light blue
                    case -16711681:
                        //Ändrar riktning
                        ants[a]._dir--;
                        ants[a]._dir += 4;
                        ants[a]._dir %= 4;
                        //korrigerar riktning för att vara inom de tillåtna värdena
                        ants[a]._dir = dirOverFlowCorr(ants[a]._dir);
                        if (!antcheck(a))
                        {
                            passthrough = false;
                            ants[a]._dir++;
                            ants[a]._dir += 4;
                            ants[a]._dir %= 4;
                        }
                        break;


                    //Ljusare rosa
                    case -392966:
                        ants[a].step();

                        ants[a]._dir++;
                        ants[a]._dir += 4;
                        ants[a]._dir %= 4;

                        ants[a]._dir = dirOverFlowCorr(ants[a]._dir);
                        ants[a].step();
                        ants[a]._dir--;
                        // if (!antcheck(a))
                        // {
                        //     passthrough = false;
                        //     ants[a]._dir--;
                        //     ants[a]._dir += 4;
                        //     ants[a]._dir %= 4;
                        // }
                        break;


                    //Rosa/lila
                    case -393016:

                        ants[a]._dir--;
                        ants[a]._dir += 4;
                        ants[a]._dir %= 4;

                        ants[a]._dir = dirOverFlowCorr(ants[a]._dir);
                        //  if (!antcheck(a))
                        //  {
                        //      passthrough = false;
                        //      ants[a]._dir++;
                        //      ants[a]._dir += 4;
                        //      ants[a]._dir %= 4;
                        //  }
                        ants[a].step();


                        ants[a]._dir++;
                        ants[a]._dir += 4;
                        ants[a]._dir %= 4;

                        ants[a]._dir = dirOverFlowCorr(ants[a]._dir);
                        //  if (!antcheck(a))
                        //  {
                        //      passthrough = false;
                        //      ants[a]._dir--;
                        //      ants[a]._dir += 4;
                        //      ants[a]._dir %= 4;
                        //  }
                        break;

                }
                if (passthrough)
                {
                    ants[a].step();
                }
                if (exists)
                {
                    mapAVC.Setpixel(ants[a].getPos(), ants[a].Color);
                    pictureBox.Image = mapAVC.get();
                }
                richTextBox1.Text = ants[12]._dir.ToString();
            }





            //     richTextBox4.Clear();
            //
            //     textBox1.BackColor = Color.White;
            //     textBox2.BackColor = Color.White;
            //     mapAVC.reset();
            //     mapAVC.Upscale(3);
            //     for (int i = 0; i < ants.Count(); i++)
            //     {
            //         if (!ants[i].step(map, ants, i))
            //         {
            //             if (ants[i].Delete)
            //             {
            //                 ClearList.Add(i);
            //             }
            //         }
            //         mapAVC.Setpixel(ants[i].getPos(), ants[i].Color);
            //     }
            //     ClearList.Sort();
            //     for (int x = 0; x < ClearList.Count; x++)
            //     {
            //         try
            //         {
            //             ants.RemoveAt(ClearList[x]);
            //         }
            //         catch { }
            //     }
            //     ClearList.Clear();
            //     foreach (Ant x in ants)
            //     {
            //         richTextBox4.AppendText(x.getPosX() + " : " + x.getPosY() + "\n");
            //     }
            //     try
            //     {
            //
            //         richTextBox1.Text = map.GetPixel(ants[0].getPosX(), ants[0].getPosY()).ToArgb().ToString();
            //     }
            //     catch { }
            //     pictureBox.Image = mapAVC.get();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkant();
                richTextBox2.Clear();
            }
            catch (Exception exe)
            {
                richTextBox2.Text = exe.ToString();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                checkant();
                richTextBox2.Clear();
            }
            catch (Exception exe)
            {
                richTextBox2.Text = exe.ToString();
            }
        }

        private void checkant()
        {
            // if (Ant.Antonpixel(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text)))
            // {
            //     textBox1.BackColor = Color.Green;
            //     textBox2.BackColor = Color.Green;
            // }
            // else
            // {
            //     textBox1.BackColor = Color.Red;
            //     textBox2.BackColor = Color.Red;
            // }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ants.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ants.Add(new Ant(13, 1, 2, Color.Red));
        }

        static Ant independent;
        private void button8_Click(object sender, EventArgs e)
        {
            independent = new Ant(new Point(map.Height / 2, map.Width / 2), 0, Color.GreenYellow);
        }

        private void button_UP_Click(object sender, EventArgs e)
        {
            mapAVC.reset();
            mapAVC.Upscale(10);
            independent.setPos(new Point(independent.getPosX(), independent.getPosY() - 1));
            mapAVC.Setpixel(independent.getPos(), independent.Color);
            pictureBox.Image = mapAVC.get();
            colorcheck();
        }

        private void button_RIGHT_Click(object sender, EventArgs e)
        {
            mapAVC.reset();
            mapAVC.Upscale(10);
            independent.setPos(new Point(independent.getPosX() + 1, independent.getPosY()));
            mapAVC.Setpixel(independent.getPos(), independent.Color);
            pictureBox.Image = mapAVC.get();
            colorcheck();
        }

        private void button_LEFT_Click(object sender, EventArgs e)
        {
            mapAVC.reset();
            mapAVC.Upscale(10);
            independent.setPos(new Point(independent.getPosX() - 1, independent.getPosY()));
            mapAVC.Setpixel(independent.getPos(), independent.Color);
            pictureBox.Image = mapAVC.get();
            colorcheck();
        }

        private void button_DOWN_Click(object sender, EventArgs e)
        {
            mapAVC.reset();
            mapAVC.Upscale(10);
            independent.setPos(new Point(independent.getPosX(), independent.getPosY() + 1));
            mapAVC.Setpixel(independent.getPos(), independent.Color);
            pictureBox.Image = mapAVC.get();
            colorcheck();
        }
        private void colorcheck()
        {
            richTextBox2.Text = mapAVC.GetPixel(independent.getPos()).ToArgb().ToString();
        }
    }
}
