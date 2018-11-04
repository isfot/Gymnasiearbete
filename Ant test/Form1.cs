﻿using System;
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
        public static readonly int hastighet_max = 3; // Maxhastighet för alla myror dvs hastighetsbegränsningen.
        private static List<Point>[] Start_Fields = new List<Point>[4] { new List<Point>(), new List<Point>(), new List<Point>(), new List<Point>() }; //Array av listor som indikerar startfält för myrorna
        private static Point[] Turn_fields = new Point[4];
        private static List<Point> Kill_Fields = new List<Point>();
        static int counter;
        static Random rand = new Random();
        public static bool[,] karta;// Initieraren skall ändras så att den matchar kartans storlek. 
        private static int[,] map_elements;

        /// <summary>
        /// Inititerar UI och bitmapen
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            map = new Bitmap(path);
            karta = new bool[map.Width, map.Height];
            map_elements = new int[map.Width, map.Height];
            startField_Finder();
            mapAVC = new BitmapAVC(map);
        }
        /// <summary>
        /// Overload
        /// </summary>
        public Form1(string[] args)
        {
            InitializeComponent();
            map = new Bitmap(args[0]);
            karta = new bool[map.Width, map.Height];
            map_elements = new int[map.Width, map.Height];
            startField_Finder();
            mapAVC = new BitmapAVC(map);
           
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
                        //Startfields
                        case -1536:
                            Start_Fields[2].Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        case -14336:
                            Start_Fields[3].Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        case -27136:
                            Start_Fields[0].Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        case -39936:
                            Start_Fields[1].Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        //Turnfields
                        case -16711681:
                            Turn_fields[0] = new Point(x, y);
                            hide_pixel(x, y);
                            break;
                        case -13434881:
                            Turn_fields[1] = new Point(x, y);
                            hide_pixel(x, y);
                            break;
                        case -10158081:
                            Turn_fields[2] = new Point(x, y);
                            hide_pixel(x, y);
                            break;
                        case -6881281:
                            Turn_fields[3] = new Point(x, y);
                            hide_pixel(x, y);
                            break;
                        //Killfields
                        case -65536:
                            map_elements[x, y] = -1;
                            hide_pixel(x, y);
                            break;
                        //Trafikljus
                        //0
                        case -39786:
                            hide_pixel(x, y);
                            break;
                        //1
                        case -39736:
                            hide_pixel(x, y);
                            break;
                        //2
                        case -39886:
                            hide_pixel(x, y);
                            break;
                        //3
                        case -39836:
                            hide_pixel(x, y);
                            break;

                    }
                }
            }
        }
        private void hide_pixel(int x, int y)
        {
            map.SetPixel(x, y, Color.White);
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
            for(int u = 0; u < 50; u++)
            {

                for (int i = 0; i < Start_Fields.Length - 1; i++)
                {
                    foreach (Point pos in Start_Fields[i])
                    {
                        ants.Add(new Ant(pos, i, Color.Black));
                    }
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

        /// <summary>
        /// Startar myror ramdomiserat
        /// </summary>
        private void spawnrandom()
        {
            int index = rand.Next(0, 4);
            //Ha så kul med att försöka tyda detta :)
            if (index == 0 && checkBox_Field_3.Checked)
            {
                ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, Color.Black));
            }
            if (index == 1 && checkBox_Field_4.Checked)
            {
                ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, Color.Black));
            }
            if (index == 2 && checkBox_Field_1.Checked)
            {
                ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, Color.Black));
            }
            if (index == 3 && checkBox_Field_2.Checked)
            {
                ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, Color.Black));
            }

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

        private bool is_ant_in_front(Ant greger)// metoden kan ersättas med en array som säger huruvida en rutan är okuperad.
        {
            bool output = false;
            switch (greger._dir)
            {
                case 0:
                    output = karta[greger.getPosX(), greger.getPosY() - 1];
                    break;
                case 1:
                    output = karta[greger.getPosX() + 1, greger.getPosY()];
                    break;
                case 2:
                    output = karta[greger.getPosX(), greger.getPosY() + 1];
                    break;
                case 3:
                    output = karta[greger.getPosX() - 1, greger.getPosY()];
                    break;
            }
            return output;
        }

        private void antstep()
        {
            mapAVC.reset();
            mapAVC.Upscale(3);

            for (int a = 0; a < ants.Count; a++)
            {
                bool passthrough = !is_ant_in_front(ants[a]);
                bool exists = true;


                if (ants[a]._dir == Array.IndexOf(Turn_fields, ants[a].getPos()))
                {
                    ants[a]._dir--;
                    ants[a]._dir = dirOverFlowCorr(ants[a]._dir);
                }
               
                //Kollar på elementen på kartan och utför en handling beroende på detta
                switch (map_elements[ants[a].getPosX(),ants[a].getPosY()])
                {
                    //Case röd har ihjäl myran
                    case -1://Röd
                        passthrough = false;
                        karta[ants[a].getPosX(), ants[a].getPosY()] = false;
                        ants.RemoveAt(a);
                        exists = false;
                        break;
                }

                if (passthrough)
                {
                    ants[a].step();
                }
                if (exists)
                {
                    mapAVC.Setpixel(ants[a].getPos(), ants[a].Color);
                }
            }
            pictureBox.Image = mapAVC.get();
        }
        public void anttoarray() // metod för att placera myrorna på en karta.
        {
            foreach (Ant a in ants)
            {
                karta[a.getPosX(), a.getPosY()] = true;
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
            karta = new bool[map.Width, map.Height];
            mapAVC.reset();
            mapAVC.Upscale(3);
            pictureBox.Image = mapAVC.get();
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
