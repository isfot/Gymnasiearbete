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
        private static string path = Environment.CurrentDirectory + @"\pic.png"; // Sökväg i hårdisken till kartan
        private static Bitmap map; // Kartan som en bitmap
        private static BitmapAVC mapAVC; // Kartan som en AVC bitmap
        private static List<Ant> ants = new List<Ant>(); // En lista med alla myror
        public readonly int hastighet_max = 1; // Maxhastighet för alla myror dvs hastighetsbegränsningen.
        private static List<Point>[] Start_Fields = new List<Point>[4] { new List<Point>(), new List<Point>(), new List<Point>(), new List<Point>() }; //Array av listor som indikerar startfält för myrorna
        /// <summary>
        /// Inititerar UI och bitmapen
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            map = new Bitmap(path);
            mapAVC = new BitmapAVC(map);
            startField_Finder();
        }
        /// <summary>
        /// Overload
        /// </summary>
        public Form1(string[] args)
        {
            InitializeComponent();
            map = new Bitmap(args[0]);
            mapAVC = new BitmapAVC(map);
            startField_Finder();
        }

        private void startField_Finder()
        {
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    //2: -1536
                    //3: -14336
                    //0: -27136
                    //1: -39936
                    switch (map.GetPixel(x, y).ToArgb())
                    {
                        case -1536:
                            Start_Fields[2].Add(new Point(x, y));
                            break;
                        case -14336:
                            Start_Fields[3].Add(new Point(x, y));
                            break;
                        case -27136:
                            Start_Fields[0].Add(new Point(x, y));
                            break;
                        case -39936:
                            Start_Fields[1].Add(new Point(x, y));
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Skalar upp kartan och sätter den i en picturebox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            mapAVC.Upscale(10); // Skala upp kartan
            pictureBox.Image = mapAVC.get(); // Sätter kartan i picturebox
        }
        //Sätter på timer eventet som körs med ett intervall
        private void Timer_button_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }
        /// <summary>
        /// Skapar myror i alla startfält :)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ant_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Start_Fields.Length; i++)
            {
                foreach (Point pos in Start_Fields[i])
                {
                    ants.Add(new Ant(pos, i, Color.Black));
                }
            }
            //Skriver ut antalet aktiva myror
            richTextBox3.Text = ants.Count.ToString();
            //Renderar alla myror
            foreach (Ant x in ants)
            {
                mapAVC.Setpixel(x.getPos(), x.Color);
            }
            pictureBox.Image = mapAVC.get();
        }
        static int counter;
        static Random rand = new Random();
        /// <summary>
        /// Startar myror ramdomiserat
        /// </summary>
        private void spawnrandom()
        {
            int index = rand.Next(0, 4);
            //Ha så kul med att försöka tyda detta :)
            ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, Color.Black));
        }
        /// <summary>
        /// Timer event som körs med ett fast intervall
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            antstep();
            richTextBox3.Text = ants.Count.ToString();
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
        private void Steg_Button_Click(object sender, EventArgs e)
        {
            antstep();
            richTextBox2.Text = ants[3]._dir.ToString();
        }
        /// <summary>
        /// Funktion som avgör ifall en int är större är 3 och sätter till 0 ifall det är så, eller ifall den är mindre en 0 och sätter till 3 ifall det är så
        /// </summary>
        /// <param name="_dir">Variabel för myrans riktning</param>
        /// <returns>Justerad riktning</returns>
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
                bool passthrough = antcheck(a);
                bool exists = true;
                switch (map.GetPixel(ants[a].getPosX(), ants[a].getPosY()).ToArgb())
                {
                    //Case röd har ihjäl myran
                    case -65536://Röd
                        passthrough = false;
                        ants.RemoveAt(a);
                        exists = false;
                        break;
                    //Blue
                    case -16776961:

                        ants[a]._dir++;
                        ants[a]._dir += 4;
                        ants[a]._dir %= 4;
                        // Total Kaos plz förklara.
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
            }
        }




        /// <summary>
        /// Tar bort alla myror och återställer 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_button_Click(object sender, EventArgs e)
        {
            ants.Clear();
        }



        #region KODSOM INTE SKA VARA MED I PUBLICERADE VERISIONEN
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
    #endregion
}
