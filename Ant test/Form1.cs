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
        public static readonly int hastighet_max = 3; // Maxhastighet för alla myror dvs hastighetsbegränsningen.
        private static List<Point>[] Start_Fields = new List<Point>[4] { new List<Point>(), new List<Point>(), new List<Point>(), new List<Point>() }; //Array av listor som indikerar startfält för myrorna
        public static List<Point>[] Turn_fields_Left = new List<Point>[4] { new List<Point>(), new List<Point>(), new List<Point>(), new List<Point>() }; //Array av listor som indikerar startfält för myrorna
        public static List<Point>[] Turn_fields_Right = new List<Point>[4] { new List<Point>(), new List<Point>(), new List<Point>(), new List<Point>() }; //Array av listor som indikerar startfält för myrorna
        public static List<Point> Turn_fields_Right_Diagonal = new List<Point>(); //Array av listor som indikerar startfält för myrorna
        private static readonly int v_max = 1;

        private static List<Point> Kill_Fields = new List<Point>();
        static int counter;
        static Random rand = new Random();
        public static bool[,] karta;// Initieraren skall ändras så att den matchar kartans storlek. 
        public static int[,] map_elements;
        private int tid = 0;
        private List<Trafikljus>[] TraficLights = new List<Trafikljus>[4] { new List<Trafikljus>(), new List<Trafikljus>(), new List<Trafikljus>(), new List<Trafikljus>() };
        private List<Trafikljus>[] TraficLights_Left_Turn = new List<Trafikljus>[4] { new List<Trafikljus>(), new List<Trafikljus>(), new List<Trafikljus>(), new List<Trafikljus>() };
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
                        //Turnfields left
                        case -16711681:
                            Turn_fields_Left[0].Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        case -13434881:
                            Turn_fields_Left[1].Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        case -10158081:
                            Turn_fields_Left[2].Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        case -6881281:
                            Turn_fields_Left[3].Add(new Point(x, y));
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
                            TraficLights[0].Add(new Trafikljus(x, y, 0, v_max));
                            break;
                        //1
                        case -39736:
                            hide_pixel(x, y);
                            TraficLights[1].Add(new Trafikljus(x, y, 1, v_max));
                            break;
                        //2
                        case -39886:
                            hide_pixel(x, y);
                            TraficLights[2].Add(new Trafikljus(x, y, 2, v_max));
                            break;
                        //3
                        case -39836:
                            hide_pixel(x, y);
                            TraficLights[3].Add(new Trafikljus(x, y, 3, v_max));
                            break;
                        //Trafikljus vänstersväng
                        //0
                        case -26986:
                            TraficLights_Left_Turn[0].Add(new Trafikljus(x, y, 0, v_max));
                            hide_pixel(x, y);
                            break;
                        //1
                        case -26936:
                            TraficLights_Left_Turn[1].Add(new Trafikljus(x, y, 1, v_max));
                            hide_pixel(x, y);
                            break;
                        //2
                        case -27086:
                            TraficLights_Left_Turn[2].Add(new Trafikljus(x, y, 2, v_max));
                            hide_pixel(x, y);
                            break;
                        //3
                        case -27036:
                            TraficLights_Left_Turn[3].Add(new Trafikljus(x, y, 3, v_max));
                            hide_pixel(x, y);
                            break;

                        //Högersvängar
                        //Lila
                        case -65281:
                            Turn_fields_Right_Diagonal.Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        //Grön
                        case -16711936:
                            hide_pixel(x, y);
                            break;
                        //Turnfields right
                        //0
                        case -16711836:
                            Turn_fields_Right[0].Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        //1
                        case -13435036:
                            Turn_fields_Right[1].Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        //2
                        case -10158236:
                            Turn_fields_Right[2].Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        //3
                        case -6881436:
                            Turn_fields_Right[3].Add(new Point(x, y));
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
        private static Color[] colors = new Color[] { Color.Blue, Color.HotPink, Color.Indigo, Color.Turquoise };
        private void Ant_button_Click(object sender, EventArgs e) // Metod som skapar myror på alla startfält
        {
            //for (int a = 0; a < 50; a++)
            for (int i = 0; i < Start_Fields.Length; i++)
            {
                foreach (Point pos in Start_Fields[i])
                {
                    ants.Add(new Ant(pos, i, colors[i]));
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
            int index = rand.Next(0, 4); // Random variabel mellan 0 och 4
            //Ha så kul med att försöka tyda detta :)
            if (index == 0 && checkBox_Field_3.Checked)
            {
                ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, colors[index]));
            }
            if (index == 1 && checkBox_Field_4.Checked)
            {
                ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, colors[index]));
            }
            if (index == 2 && checkBox_Field_1.Checked)
            {
                ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, colors[index]));
            }
            if (index == 3 && checkBox_Field_2.Checked)
            {
                ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, colors[index]));
            }

        }

        private void TraficLight_Toggle_Forward(int dir, bool on)
        {
            foreach (Trafikljus a in TraficLights[dir])
            {
                if (on)
                {
                    a.Gröntljus();
                }
                else
                {
                    a.Rödljus();
                }
            }
        }
        private void TraficLight_Toggle_Turn(int dir, bool on)
        {
            foreach (Trafikljus a in TraficLights_Left_Turn[dir])
            {
                if (on)
                {
                    a.Gröntljus();
                }
                else
                {
                    a.Rödljus();
                }
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
            richTextBox4.Text = tid.ToString();
            if (counter % 3 == 0 && checkBox1.Checked)
            {
                for (int i = 0; i < 5; i++)
                {
                    spawnrandom();
                }
            }
            double cycel = 200;
            if (tid % cycel == 0)
            {
                TraficLight_Toggle_Forward(0, true);
                TraficLight_Toggle_Forward(2, true);
                TraficLight_Toggle_Forward(1, false);
                TraficLight_Toggle_Forward(3, false);
                //  TraficLight_Toggle_Turn(0, true);
                //  TraficLight_Toggle_Turn(3, false);
            }
            if (tid % cycel == (0.25 * cycel))
            {
                TraficLight_Toggle_Forward(0, false);
                TraficLight_Toggle_Forward(2, false);
                TraficLight_Toggle_Forward(1, false);
                TraficLight_Toggle_Forward(3, false);
                //  TraficLight_Toggle_Turn(1, true);
                //  TraficLight_Toggle_Turn(0, false);
            }
            if (tid % cycel == (0.5 * cycel))
            {
                TraficLight_Toggle_Forward(0, false);
                TraficLight_Toggle_Forward(2, false);
                TraficLight_Toggle_Forward(1, true);
                TraficLight_Toggle_Forward(3, true);
                //TraficLight_Toggle_Turn(2, true);
                //TraficLight_Toggle_Turn(1, false);
            }
            if (tid % cycel == (0.75 * cycel))
            {
                TraficLight_Toggle_Forward(0, false);
                TraficLight_Toggle_Forward(2, false);
                TraficLight_Toggle_Forward(1, false);
                TraficLight_Toggle_Forward(3, false);
                //TraficLight_Toggle_Turn(3, true);
                //TraficLight_Toggle_Turn(2, false);
            }
            tid++;
        }

        //Step
        static List<int> ClearList = new List<int>();
        private void Steg_Button_Click(object sender, EventArgs e)
        {
            antstep();
            richTextBox2.Text = ants[3]._dir.ToString();
            richTextBox4.Text = tid.ToString();

            //          if (tid % 100 == 0)
            //          {
            //              TraficLights[2, 0].Gröntljus();
            //              TraficLights[2, 1].Gröntljus();
            //              TraficLights[0, 1].Gröntljus();
            //              TraficLights[0, 2].Gröntljus();
            //
            //              TraficLights[1, 1].Rödljus();
            //              TraficLights[1, 2].Rödljus();
            //              TraficLights[3, 0].Rödljus();
            //              TraficLights[3, 1].Rödljus();
            //
            //          }
            //          if (tid % 100 == 25)
            //          {
            //              TraficLights[2, 0].Rödljus();
            //              TraficLights[2, 1].Rödljus();
            //              TraficLights[0, 1].Rödljus();
            //              TraficLights[0, 2].Rödljus();
            //
            //              TraficLights[1, 1].Rödljus();
            //              TraficLights[1, 2].Rödljus();
            //              TraficLights[3, 0].Rödljus();
            //              TraficLights[3, 1].Rödljus();
            //
            //
            //          }
            //          if (tid % 100 == 50)
            //          {
            //              TraficLights[2, 0].Rödljus();
            //              TraficLights[2, 1].Rödljus();
            //              TraficLights[0, 1].Rödljus();
            //              TraficLights[0, 2].Rödljus();
            //
            //              TraficLights[1, 1].Gröntljus();
            //              TraficLights[1, 2].Gröntljus();
            //              TraficLights[3, 0].Gröntljus();
            //              TraficLights[3, 1].Gröntljus();
            //          }
            //          if (tid % 100 == 75)
            //          {
            //              TraficLights[2, 0].Rödljus();
            //              TraficLights[2, 1].Rödljus();
            //              TraficLights[0, 1].Rödljus();
            //              TraficLights[0, 2].Rödljus();
            //
            //              TraficLights[1, 1].Rödljus();
            //              TraficLights[1, 2].Rödljus();
            //              TraficLights[3, 0].Rödljus();
            //              TraficLights[3, 1].Rödljus();
            //
            //
            //          }

            tid++;
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

        private bool is_ant_in_front(Ant greger)// säger huruvida en rutan är okuperad.
        {
            try
            {
                bool output = false;
                switch (greger._dir)
                {
                    case 0:
                        if (true == karta[greger.getPosX(), greger.getPosY() - 1])
                        {
                            output = true;
                        }
                        break;
                    case 1:
                        if (karta[greger.getPosX() + 1, greger.getPosY()] == true)
                        {
                            output = true;
                        }
                        break;
                    case 2:
                        if (true == karta[greger.getPosX(), greger.getPosY() + 1])
                        {
                            output = true;
                        }
                        break;
                    case 3:
                        if (karta[greger.getPosX() - 1, greger.getPosY()] == true)
                        {
                            output = true;
                        }
                        break;
                }
                return output;
            }
            catch
            {
                return true;
            }
        }

        static public bool is_ant_to_side(Ant Orvar)
        {
            bool output = false;
            switch (Orvar._dir)
            {
                case 0:
                    output = karta[Orvar.getPosX() + 1, Orvar.getPosY() - 1];
                    break;
                case 1:
                    output = karta[Orvar.getPosX() + 1, Orvar.getPosY() + 1];
                    break;
                case 2:
                    output = karta[Orvar.getPosX() - 1, Orvar.getPosY() + 1];
                    break;
                case 3:
                    output = karta[Orvar.getPosX() - 1, Orvar.getPosY() - 1];
                    break;
            }

            return output;
        }

        private void antstep()
        {
            mapAVC.reset();
            mapAVC.Upscale(1);

            for (int a = 0; a < ants.Count; a++)
            {
                bool turn = false;
                bool passthrough = !is_ant_in_front(ants[a]);
                for (int i = 0; i < Turn_fields_Left.Length; i++)
                {
                    if (Turn_fields_Left[i].Contains(ants[a].getPos()) && ants[a]._dir == i)
                    {
                        ants[a]._dir--;
                        ants[a]._dir = dirOverFlowCorr(ants[a]._dir);
                    }
                }
                for (int i = 0; i < Turn_fields_Right.Length; i++)
                {
                    if (Turn_fields_Right[i].Contains(ants[a].getPos()) && ants[a]._dir == i)
                    {
                        ants[a]._dir++;
                        ants[a]._dir = dirOverFlowCorr(ants[a]._dir);
                    }
                }
                //Sväng diagonalt
                if (Turn_fields_Right_Diagonal.Contains(ants[a].getPos()) && !is_ant_to_side(ants[a]))
                {
                    ants[a].step();
                    ants[a]._dir = dirOverFlowCorr(ants[a]._dir + 1);
                    ants[a].step();
                    ants[a]._dir = dirOverFlowCorr(ants[a]._dir - 1);
                    passthrough = false;
                    turn = true;
                }
                //Tar steg ifall den får
                if (passthrough && ants[a].getPosX() < map.Width && ants[a].getPosX() > 0 && ants[a].getPosY() < map.Height && ants[a].getPosY() > 0 && !Turn_fields_Right_Diagonal.Contains(ants[a].getPos()) && map_elements[ants[a].getPos().X, ants[a].getPos().Y] != 1)
                {
                    ants[a].step();
                    ants[a].resetColor();
                }
                else if (!turn)
                {
                    ants[a].Color = Color.Orange;
                }
            }
            //Tar bort myror på dödsrutor
            List<Ant> Remove = new List<Ant>();
            foreach (Ant a in ants)
            {
                switch (map_elements[a.getPosX(), a.getPosY()])
                {
                    //Case röd har ihjäl myran
                    case -1://Röd
                        karta[a.getPosX(), a.getPosY()] = false;
                        Remove.Add(a);
                        break;
                }
                if (a.getPosX() > map.Width || a.getPosX() < 0 || a.getPosY() > map.Height || a.getPosY() < 0)
                {
                    Remove.Add(a);
                }
            }
            for (int i = 0; i < Remove.Count; i++)
            {
                ants.Remove(Remove[i]);
            }
            for (int i = 0; i < TraficLights.Length; i++)
            {
                foreach (Trafikljus t in TraficLights[i])
                {
                    if (t.grönt)
                    {
                        mapAVC.Setpixel(t.pos, Color.Green);
                    }
                    else
                    {
                        mapAVC.Setpixel(t.pos, Color.Red);
                    }
                }
            }

            //Renderar myrorna
            foreach (Ant a in ants)
            {
                mapAVC.Setpixel(a.getPos(), a.Color);
            }
            pictureBox.Image = mapAVC.get();
        }
        private void v_step()
        {
            for (int i = 0; i < ants.Count; i++)
            {

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
            mapAVC.Upscale(10);
            pictureBox.Image = mapAVC.get();
            tid = 0;
        }



        #region KOD SOM INTE SKA VARA MED I PUBLICERADE VERISIONEN
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
