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
        private static readonly string path = Environment.CurrentDirectory + @"\pic.png"; // Sökväg i hårdisken till kartan
        public static Bitmap map; // Kartan som en bitmap   VArje pixel
        public static BitmapAVC mapAVC; // Kartan som en AVC bitmap
        public static List<Ant> ants = new List<Ant>(); // En lista med alla myror
        public static readonly int hastighet_max = 3; // Maxhastighet för alla myror dvs hastighetsbegränsningen.
        private static List<Point>[] Start_Fields = new List<Point>[4] { new List<Point>(), new List<Point>(), new List<Point>(), new List<Point>() }; //Array av listor som indikerar startfält för myrorna
        public static List<Point>[] Turn_fields_Left = new List<Point>[4] { new List<Point>(), new List<Point>(), new List<Point>(), new List<Point>() }; //Array av listor som indikerar var myrorna svänger vänster 
        public static List<Point>[] Turn_fields_Right = new List<Point>[4] { new List<Point>(), new List<Point>(), new List<Point>(), new List<Point>() }; //Array av listor som indikerar var myrorna svänger höger
        public static List<Point>[] Turn_fields_Left_Diagonal = new List<Point>[4] { new List<Point>(), new List<Point>(), new List<Point>(), new List<Point>() }; //Array av listor som indikerar var myrorna svänger höger
        public static List<Point> Turn_fields_Right_Diagonal = new List<Point>(); //Array av listor som indikerar var myrorna svänger diagonalt höger
        private List<Point> White_Fields = new List<Point>();
        private static readonly int v_max = 5;
        public static int[] flow = new int[16];
        private static double occupiable_fields;
        private static double density;
        private static int car_in_motion = 0;
        private static List<Point> Kill_Fields = new List<Point>(); //ha ihjäl myror vid rätt rutor
        static Random rand = new Random();
        public static bool[,] karta;// Initieraren skall ändras så att den matchar kartans storlek.  MAP
        public static int[,] map_elements; //POSITIONERAR TRAFIKLJUS OCH KILLFIELDS
        private int tid = 0; // Variabel för tid- I programmet motsvarar 1 tidsenhet=1 sekund
        private List<Trafikljus>[] TraficLights = new List<Trafikljus>[4] { new List<Trafikljus>(), new List<Trafikljus>(), new List<Trafikljus>(), new List<Trafikljus>() };
        private List<Trafikljus>[] TraficLights_Left_Turn = new List<Trafikljus>[4] { new List<Trafikljus>(), new List<Trafikljus>(), new List<Trafikljus>(), new List<Trafikljus>() };
        private readonly string baseSavePath = @"C:\ANTS\" + Convert.ToString(DateTime.Now.ToString("MM-dd-yyyy__HH_mm")) + @"\";

        /// <summary>
        /// Forms
        /// </summary>
        public bool renderFormActive = false;
        private RenderForm renderForm;

        public static bool dataFormActive = false;
        private DataForm dataForm;
        /// <summary>
        /// Inititerar UI och bitmapen
        /// </summary>
        public Form1() //Skapar grafiska förutsättningar
        {
            InitializeComponent();
            map = new Bitmap(path); //Nytt fönster
            karta = new bool[map.Width, map.Height];//Gör en karta som är ett koordinatsystem där varje ruta är true-false
            map_elements = new int[map.Width, map.Height]; //Gör en karta som håller koll på trafikljus, svängfält och grejer
            startField_Finder(); //Läser in vad som ska vara på kartan
            mapAVC = new BitmapAVC(map); //
            foreach (List<Trafikljus> y in TraficLights) // Alla vanliga trafikljus
            {
                foreach (Trafikljus a in y)
                {
                    a.Rödljus(); // Gör alla trafikljus till röda vid programmets start
                    mapAVC.Setpixel(a.pos, Color.Red); // Gör så att dem visas röda i bilden.
                }
            }
            foreach (List<Trafikljus> y in TraficLights_Left_Turn) // Alla trafikljus som svänger vänster
            {
                foreach (Trafikljus a in y)
                {
                    a.Rödljus();// Gör alla trafikljus till röda vid programmets start
                    mapAVC.Setpixel(a.pos, Color.Red);// Gör så att dem visas röda i bilden.
                }
            }
        }
        /// <summary>
        /// Overload
        /// </summary>
        public Form1(string[] args) // Samma sak som ovan för att FILIP VILL DET
        {
            InitializeComponent();
            map = new Bitmap(args[0]);
            karta = new bool[map.Width, map.Height];
            map_elements = new int[map.Width, map.Height];
            startField_Finder();
            mapAVC = new BitmapAVC(map);
            foreach (List<Trafikljus> y in TraficLights)
            {
                foreach (Trafikljus a in y)
                {
                    a.Rödljus();
                    mapAVC.Setpixel(a.pos, Color.Red);
                }
            }
            foreach (List<Trafikljus> y in TraficLights_Left_Turn)
            {
                foreach (Trafikljus a in y)
                {
                    a.Rödljus();
                    mapAVC.Setpixel(a.pos, Color.Red);
                }
            }
        }


        private void startField_Finder()  //LOOP SOM KOLLAR TILL ALLA PIXLAR INNAN PROGRAMMET VISAR NÅT OCH VAD SOM SKA VARA PÅ DOM
        {                                   // blonderar också pixlar så att do minte syns
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    //2: -1536  ALLA FÄRGER FÅR EN SIFFRA
                    //3: -14336
                    //0: -27136
                    //1: -39936

                    // Se i bilden vilken rikning som motsvarar vilken siffra
                    switch (map.GetPixel(x, y).ToArgb()) // Tittar på varje pixel vilket Argb-värde den har(dvs vilken färg den har).
                    {
                        //Startfields
                        case -1536:
                            Start_Fields[2].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        case -14336:
                            Start_Fields[3].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        case -27136:
                            Start_Fields[0].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        case -39936:
                            Start_Fields[1].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //Turnfields left
                        case -16711681:
                            Turn_fields_Left[0].Add(new Point(x, y));
                            hide_pixel(x, y);
                            break;
                        case -13434881:
                            Turn_fields_Left[1].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        case -10158081:
                            Turn_fields_Left[2].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        case -6881281:
                            Turn_fields_Left[3].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
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
                            occupiable_fields++;
                            break;
                        //1
                        case -39736:
                            hide_pixel(x, y);
                            TraficLights[1].Add(new Trafikljus(x, y, 1, v_max));
                            occupiable_fields++;
                            break;
                        //2
                        case -39886:
                            hide_pixel(x, y);
                            TraficLights[2].Add(new Trafikljus(x, y, 2, v_max));
                            occupiable_fields++;
                            break;
                        //3
                        case -39836:
                            hide_pixel(x, y);
                            TraficLights[3].Add(new Trafikljus(x, y, 3, v_max));
                            occupiable_fields++;
                            break;
                        //Trafikljus vänstersväng
                        //0
                        case -26986:
                            TraficLights_Left_Turn[0].Add(new Trafikljus(x, y, 0, v_max));
                            occupiable_fields++;
                            hide_pixel(x, y);
                            break;
                        //1
                        case -26936:
                            TraficLights_Left_Turn[1].Add(new Trafikljus(x, y, 1, v_max));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //2
                        case -27086:
                            TraficLights_Left_Turn[2].Add(new Trafikljus(x, y, 2, v_max));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //3
                        case -27036:
                            TraficLights_Left_Turn[3].Add(new Trafikljus(x, y, 3, v_max));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //Högersvängar
                        //Lila
                        case -65281:
                            Turn_fields_Right_Diagonal.Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //Turnfields left diagonal
                        //0
                        case -16776961:
                            Turn_fields_Left_Diagonal[0].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //1
                        case -6908161:
                            Turn_fields_Left_Diagonal[1].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //2
                        case -6895361:
                            Turn_fields_Left_Diagonal[2].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //3
                        case -3618561:
                            Turn_fields_Left_Diagonal[3].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //Grön
                        case -16711936:
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //Turnfields right
                        //0
                        case -16711836:
                            Turn_fields_Right[0].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //1
                        case -13435036:
                            Turn_fields_Right[1].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //2
                        case -10158236:
                            Turn_fields_Right[2].Add(new Point(x, y));
                            hide_pixel(x, y);
                            occupiable_fields++;
                            break;
                        //3
                        case -6881436:
                            Turn_fields_Right[3].Add(new Point(x, y));
                            hide_pixel(x, y);

                            occupiable_fields++;
                            break;
                        case -1: // Hittar alla körbara fält.
                            occupiable_fields++;
                            White_Fields.Add(new Point(x, y));
                            break;
                    }
                }
            }
        }

        private void hide_pixel(int x, int y)  //GÖR VÄGARNA I GRAFIKEN VITA (TAR INFO FRÅN STARTFIELDFINDER)
        {
            map.SetPixel(x, y, Color.White);
            White_Fields.Add(new Point(x, y));
        }
        /// <summary>
        /// Skalar upp kartan och sätter den i en picturebox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.IKON; // Filip ville  prompt ha en ikon i programmet ......
            pictureBox1.Image = Properties.Resources.IKON.ToBitmap(); // Ikonen skulle också finnas i Form1 ......
            mapAVC.Upscale(10);
            pictureBox.Image = mapAVC.get(); // Sätter kartan i picturebox
        }
        private void Timer_button_Click(object sender, EventArgs e)//Sätter på timer eventet som körs med ett intervall
        {
            timer1.Enabled = !timer1.Enabled;
        }
        /// <summary>
        /// Skapar myror i alla startfält :)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static readonly Color[] colors = new Color[] { Color.Yellow, Color.HotPink, Color.Indigo, Color.DarkRed }; //Beroende på var dom börjhar får dom en viss färg
        private void Ant_button_Click(object sender, EventArgs e) // Metod som skapar myror på alla startfält
        {
            //for (int a = 0; a < 50; a++)
            for (int i = 0; i < Start_Fields.Length; i++)
            {
                //SÄTTER MYRA PÅ ALLA STATRFÄLT 
                foreach (Point pos in Start_Fields[i])
                {
                    ants.Add(new Ant(pos, i, colors[i], true));
                }
            }
        }
        public void add_ants()
        {
            for (int i = 0; i < flow.Length; i++) // flow är en array där informationen hur många bilar man vill skicka ut sparas.
            {
                switch (i % 4)
                {
                    case 1:
                        for (int h = 0; h < flow[i]; h++) // flow[i] är hur många bilar som skall skickas ut.
                        {
                            ants.Add(new Ant(Start_Fields[1][(int)(i / 4)], 1, colors[i % 4], true));
                        }
                        break;
                    case 2:
                        for (int h = 0; h < flow[i]; h++)
                        {
                            ants.Add(new Ant(Start_Fields[2][(int)(i / 4)], 2, colors[i % 4], true));
                        }
                        break;
                    case 3:
                        for (int h = 0; h < flow[i]; h++)
                        {
                            ants.Add(new Ant(Start_Fields[3][(int)(i / 4)], 3, colors[i % 4], true));
                        }
                        break;
                    case 0:
                        for (int h = 0; h < flow[i]; h++)
                        {
                            ants.Add(new Ant(Start_Fields[0][(int)(i / 4)], 0, colors[i % 4], true));
                        }
                        break;
                }

            }
        }

        private void button_Custom_Flow_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////////////////////////
            // Har lagt till ytterligare funktionalitet för att spawna myror:
            // CustomFlow_checkbox (heh filip var här och tog bort den :)) ger valet att spawna en myra på varje startfält eller att välja hur många på varje startfält man vill ha, genom att välja i en dialogruta. se nedan.
            //////////////////////////////////////////////////////////////////////////////////////
            Form f = new Dialogruta_CustomFlow(this); // Skapar en dialogruta enligt filen CustomFlow.cs filen
            f.Show(); // Öppnar dialogrutan

            //Skriver ut antalet aktiva myror
            richTextBox3.Text = ants.Count.ToString();
            //renderar skärmen
            render_To_Screen();
        }

        /// <summary>
        /// Startar myror ramdomiserat
        /// </summary>
        private void spawnrandom()
        {
            int index = rand.Next(0, 4); // Random INT-variabel mellan 0 och 4
            //Ha så kul med att försöka tyda detta :)
            try
            {
                if (index == 0 && checkBox_Field_3.Checked)
                {
                    ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, colors[index], true));
                }
                if (index == 1 && checkBox_Field_4.Checked)
                {
                    ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, colors[index], true));
                }
                if (index == 2 && checkBox_Field_1.Checked)
                {
                    ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, colors[index], true));
                }
                if (index == 3 && checkBox_Field_2.Checked)
                {
                    ants.Add(new Ant(Start_Fields[index][rand.Next(0, Start_Fields[index].Count)], index, colors[index], true));
                }
            }
            catch { }


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
        private void gul_fram(int dir)
        {
            foreach (Trafikljus a in TraficLights[dir])
            {
                a.Gultljus();
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
        private void gul_turn(int dir)
        {
            foreach (Trafikljus a in TraficLights_Left_Turn[dir])
            {
                a.Gultljus();
            }
        }
        static int counter;
        /// <summary>
        /// Timer event som körs med ett fast intervall
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        System.Diagnostics.Stopwatch watch;
        private void timer1_Tick(object sender, EventArgs e)
        {
            watch = System.Diagnostics.Stopwatch.StartNew();
            counter++;
            //antstep();
            v_step();
            richTextBox3.Text = ants.Count.ToString();
            richTextBox4.Text = tid.ToString();
            if (counter % 5 == 0 && checkBox1.Checked)
            {
                for (int i = 0; i < 5; i++)
                {
                    spawnrandom();
                }
            }
            double cykel = 200;
            if (tid % cykel == 0)
            {
                TraficLight_Toggle_Forward(0, true);
                TraficLight_Toggle_Forward(2, true);
                TraficLight_Toggle_Forward(1, false);
                TraficLight_Toggle_Forward(3, false);
                //  TraficLight_Toggle_Turn(0, true);
                //  TraficLight_Toggle_Turn(3, false);
            }
            if (tid % cykel == 2)
            {
                gul_fram(1); // tbd
                gul_fram(2);
                gul_fram(3);
                gul_fram(0);
            }
            if (tid % cykel == (0.25 * cykel)-2)
            {
                //gul_fram(1); // tbd
            }
            if (tid % cykel == (0.25 * cykel))
            {
                TraficLight_Toggle_Forward(0, false);
                TraficLight_Toggle_Forward(2, false);
                TraficLight_Toggle_Forward(1, false);
                TraficLight_Toggle_Forward(3, false);
                //  TraficLight_Toggle_Turn(1, true);
                //  TraficLight_Toggle_Turn(0, false);
            }
            if (tid % cykel == (0.27 * cykel))
            {
                TraficLight_Toggle_Turn(0, true);
                TraficLight_Toggle_Turn(2, true);
            }
            if (tid % cykel == (0.47 * cykel))
            {
                TraficLight_Toggle_Turn(0, false);
                TraficLight_Toggle_Turn(2, false);
            }
            if (tid % cykel == (0.5 * cykel))
            {
                TraficLight_Toggle_Forward(0, false);
                TraficLight_Toggle_Forward(2, false);
                TraficLight_Toggle_Forward(1, true);
                TraficLight_Toggle_Forward(3, true);
                //TraficLight_Toggle_Turn(2, true);
                //TraficLight_Toggle_Turn(1, false);
            }
            if (tid % cykel == (0.75 * cykel))
            {
                TraficLight_Toggle_Forward(0, false);
                TraficLight_Toggle_Forward(2, false);
                TraficLight_Toggle_Forward(1, false);
                TraficLight_Toggle_Forward(3, false);
                //TraficLight_Toggle_Turn(3, true);
                //TraficLight_Toggle_Turn(2, false);
            }
            if (tid % cykel == (0.77 * cykel))
            {
                TraficLight_Toggle_Turn(1, true);
                TraficLight_Toggle_Turn(3, true);
            }
            if (tid % cykel == (0.97 * cykel))
            {
                TraficLight_Toggle_Turn(1, false);
                TraficLight_Toggle_Turn(3, false);
            }
            density = ants.Count / occupiable_fields;
            Densitet_Textbox.Text = density.ToString() + "   " + car_in_motion.ToString();

            tid++;
            watch.Stop();
            richTextBox2.Text = watch.ElapsedTicks.ToString() + "\n" + timer1.Interval;
            if (watch.Elapsed.Ticks < 20000)
            {
                timer1.Interval = 70;
            }
            else
            {
                timer1.Interval = 1;
            }
        }

        //Step
        static List<int> ClearList = new List<int>();
        private void Steg_Button_Click(object sender, EventArgs e)
        {
            v_step();
            richTextBox2.Text = ants[3]._dir.ToString();
            richTextBox4.Text = tid.ToString();
            Densitet_Textbox.Text = density.ToString() + "   " + car_in_motion.ToString();
            #region kommentar
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
            #endregion
            tid++;
        }
        public void updateText(string text)
        {
            richTextBox2.Text = text;
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
                        if (true == karta[greger.X, greger.Y - 1])
                        {
                            output = true;
                        }
                        break;
                    case 1:
                        if (karta[greger.X + 1, greger.Y] == true)
                        {
                            output = true;
                        }
                        break;
                    case 2:
                        if (true == karta[greger.X, greger.Y + 1])
                        {
                            output = true;
                        }
                        break;
                    case 3:
                        if (karta[greger.X - 1, greger.Y] == true)
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

        static public bool is_ant_to_side_right(Ant Orvar)
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
        static public bool is_ant_to_side_left(Ant Orvar)
        {
            if (Orvar.X < map.Width && Orvar.X > 0 && Orvar.Y < map.Height && Orvar.Y > 0)
            {
                switch (Orvar._dir)
                {
                    case 0:
                        return karta[Orvar.X - 1, Orvar.Y - 1];
                    case 1:
                        return karta[Orvar.X + 1, Orvar.Y - 1];
                    case 2:
                        return karta[Orvar.X + 1, Orvar.Y + 1];
                    case 3:
                        return karta[Orvar.X - 1, Orvar.Y + 1];
                    default:
                        return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        private void ant_check_Turn_Fields_Left(int a)
        {
            for (int i = 0; i < Turn_fields_Left.Length; i++)
            {
                if (Turn_fields_Left[i].Contains(ants[a].getPos()) && ants[a]._dir == i)
                {
                    ants[a]._dir--;
                    ants[a]._dir = dirOverFlowCorr(ants[a]._dir);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        private void ant_check_Turn_Fields_Right(int a)
        {
            for (int i = 0; i < Turn_fields_Right.Length; i++)
            {
                if (Turn_fields_Right[i].Contains(ants[a].getPos()) && ants[a]._dir == i)
                {
                    ants[a]._dir++;
                    ants[a]._dir = dirOverFlowCorr(ants[a]._dir);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="passthrough"></param>
        private void ant_check_Turn_Diagonal(int a, bool passthrough)
        {
            if (Turn_fields_Right_Diagonal.Contains(ants[a].getPos()) && !is_ant_to_side_right(ants[a]))
            {
                ants[a].step();
                ants[a]._dir = dirOverFlowCorr(ants[a]._dir + 1);
                ants[a].step();
                ants[a]._dir = dirOverFlowCorr(ants[a]._dir - 1);
                passthrough = false;
            }
            //Sväng höger
            else if (Turn_fields_Left_Diagonal[ants[a]._dir].Contains(ants[a].getPos()) && !is_ant_to_side_right(ants[a]))
            {
                ants[a].step();
                ants[a]._dir = dirOverFlowCorr(ants[a]._dir - 1);
                ants[a].step();
                ants[a]._dir = dirOverFlowCorr(ants[a]._dir + 1);
                passthrough = false;
            }
            //Tar steg ifall den får
            else if (passthrough && ants[a].getPosX() < map.Width && ants[a].getPosX() > 0 && ants[a].getPosY() < map.Height && ants[a].getPosY() > 0 && !Turn_fields_Right_Diagonal.Contains(ants[a].getPos()) && map_elements[ants[a].getPos().X, ants[a].getPos().Y] != 1)
            {
                ants[a].step();
                ants[a].resetColor();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ants"></param>
        /// <param name="ants_"></param>
        static List<Ant> Remove;
        private void ant_check_remove(List<Ant> ants)
        {
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
            //Tar bort alla myror

        }

        private void antstep(Ant s)
        {
            //mapAVC.reset();
            //mapAVC.Upscale(1);

            int a = ants.IndexOf(s);
            //Kollar ifall myran står på vänstersväng
            ant_check_Turn_Fields_Left(a);
            //Kollar ifall myran står på en högersväng
            ant_check_Turn_Fields_Right(a);
            //Sväng diagonalt
            ant_check_Turn_Diagonal(a, !is_ant_in_front(ants[a]));

            //Tar bort myror på dödsrutor
            //  ant_check_remove(ants);
            //renderar allt till skärmen
            // render_To_Screen();
        }

        /// <summary>
        /// Funktion som tar gör att alla myror tar ett steg med sin acceleration
        /// </summary>
        private void v_step()
        {
            Remove = new List<Ant>();
            //mapAVC.reset();
            //mapAVC.Upscale(1);
            foreach (Ant a in ants)
            {
                a.trace(out int step, out bool brake, out int Ant_V, this);

                if (step <= Convert.ToDouble(a.v * a.v + a.v) / 2.0 - Convert.ToDouble(Ant_V * Ant_V + Ant_V) / 2.0)
                {
                    Console.WriteLine("nu blev det fel");
                }

                if (step <= Convert.ToDouble(a.v * a.v + 1.5 * a.v - Ant_V * Ant_V - Ant_V) / 2.0 && brake)
                {
                    for (int x = 1; x <= a.v; x++)
                    {

                        antstep(a);
                    }
                    if (0 < a.v)
                    {
                        a.Acc = -1;

                    }

                }
                else if (step > Convert.ToDouble(a.v * a.v - Ant_V * Ant_V - Ant_V) / 2.0 + 2.5 * a.v + 1 | !brake)
                {
                    for (int x = 1; x <= a.v; x++)
                    {
                        if (map_elements[a.X, a.Y] == -1)
                        {
                            a.v = 0;
                        }
                        antstep(a);
                    }
                    if (!brake && a.v != 0)
                    {
                        antstep(a);
                    }

                    if (a.v < v_max && map_elements[a.X, a.Y] != -1)
                    {
                        a.Acc = 1;


                    }
                }
                else
                {
                    a.Acc = 0;
                    for (int x = 1; x <= a.v; x++)
                    {

                        antstep(a);
                    }
                }
            }
            foreach (Ant a in ants)
            {
                if (a.v == 0 && a.Acc == 1)
                {
                    car_in_motion++;
                }
                a.v += a.Acc;
                if (a.v == 0)
                {
                    car_in_motion--;
                }
            }
            ant_check_remove(ants);
            //Tar bort alla myror
            for (int i = 0; i < Remove.Count; i++)
            {
                ants.Remove(Remove[i]);
            }

            render_To_Screen();
        }

        private void render_To_Screen()
        {
            List<Trafikljus>[] concatList = new List<Trafikljus>[4];
            for (int i = 0; i < concatList.Length; i++)
            {
                var all_products = TraficLights[i].Concat(TraficLights_Left_Turn[i]).ToList();
                concatList[i] = all_products;
            }
            mapAVC.render(concatList, ants, White_Fields);

            pictureBox.Image = mapAVC.get();
            if (renderFormActive)
            {
                renderForm.set(mapAVC.get());
            }
            if (dataFormActive)
            {
                dataForm.updateData(ants);
            }
            //new PictureExport(mapAVC.get(), 10, tid, baseSavePath);
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
            List<Trafikljus>[] concatList = new List<Trafikljus>[4];
            for (int i = 0; i < concatList.Length; i++)
            {
                var all_products = TraficLights[i].Concat(TraficLights_Left_Turn[i]).ToList();
                concatList[i] = all_products;
            }
            mapAVC.render(concatList, ants, White_Fields);
            pictureBox.Image = mapAVC.get();
            tid = 0;
        }


        #region KOD SOM INTE SKA VARA MED I PUBLICERADE VERISIONEN
        static Ant independent;
        private void button8_Click(object sender, EventArgs e)
        {
            independent = new Ant(new Point(map.Height / 2, map.Width / 2), 0, Color.GreenYellow, false);
        }

        private void button_UP_Click(object sender, EventArgs e)
        {
            mapAVC.reset();
            mapAVC.Upscale(2);
            independent._dir = 0;
            independent.setPos(new Point(independent.getPosX(), independent.getPosY() - 1));
            mapAVC.Setpixel(independent.getPos(), independent.Color);
            pictureBox.Image = mapAVC.get();
            colorcheck();
        }

        private void button_RIGHT_Click(object sender, EventArgs e)
        {
            mapAVC.reset();
            mapAVC.Upscale(2);
            independent._dir = 1;
            independent.setPos(new Point(independent.getPosX() + 1, independent.getPosY()));
            mapAVC.Setpixel(independent.getPos(), independent.Color);
            pictureBox.Image = mapAVC.get();
            colorcheck();
        }

        private void button_LEFT_Click(object sender, EventArgs e)
        {
            mapAVC.reset();
            mapAVC.Upscale(2);
            independent._dir = 3;
            independent.setPos(new Point(independent.getPosX() - 1, independent.getPosY()));
            mapAVC.Setpixel(independent.getPos(), independent.Color);
            pictureBox.Image = mapAVC.get();
            colorcheck();
        }

        private void button_DOWN_Click(object sender, EventArgs e)
        {
            mapAVC.reset();
            mapAVC.Upscale(2);
            independent._dir = 2;
            independent.setPos(new Point(independent.getPosX(), independent.getPosY() + 1));
            mapAVC.Setpixel(independent.getPos(), independent.Color);
            pictureBox.Image = mapAVC.get();
            colorcheck();
        }
        private void colorcheck()
        {
            //  richTextBox2.Text = mapAVC.get();
            //  Pixel(independent.getPos()).ToArgb().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mapAVC.reset();
            mapAVC.Upscale(2);
            bool is_ant;
            int length;
            //Kolla länken ifall man inte förstår https://www.dotnetperls.com/multiple-return-values kollade upp detta för en sekund sedan
            independent.trace(out length, out is_ant, out int Ant_V, this);
            richTextBox1.Text = length.ToString() + "     " + is_ant.ToString();
            mapAVC.Setpixel(independent.getPos(), independent.Color);
            pictureBox.Image = mapAVC.get();
        }
        public void Update()
        {
            pictureBox.Image = mapAVC.get();
        }
        #endregion     

        private void button_render_form_Click(object sender, EventArgs e)
        {
            if (!renderFormActive)
                renderForm = new RenderForm(this);
            renderForm.Show();
        }

        private void button_Data_Form_Click(object sender, EventArgs e)
        {
            dataForm = new DataForm();
            dataFormActive = true;
            dataForm.Show();
        }
    }

}
