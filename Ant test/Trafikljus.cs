using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ant_test
{
    //Klass för att sköta trafikljusen i korsningen och i och med det trafikflödet
    public class Trafikljus
    {
        public Point pos;
        public int X { get { return pos.X; } }
        public int Y { get { return pos.Y; } }
        public int grönt;
        private readonly int dir;   //vilket håll den ska släppa igenom bilar
        public bool stop = false;
        private bool is_left_turn = false;

        /// <summary>
        /// Inizialerar ett nytt trafikljus
        /// </summary>
        /// <param name="pos">Position</param>
        /// <param name="dir">Riktning 0-3</param>
        /// <param name="color">Färg</param>
        public Trafikljus(Point pos, int dir, int v_max) //När programmet ser en ruta med en viss färg intieras ett nytt trafikljus
        {
            this.pos = pos;
            this.dir = dir;
            is_left_turn = false;
        }
        public Trafikljus(Point pos, int dir, int v_max, bool is_left) //När programmet ser en ruta med en viss färg intieras ett nytt trafikljus
        {
            this.pos = pos;
            this.dir = dir;
            is_left_turn = is_left;
        }
        public Trafikljus(int x, int y, int dir, int v_max, bool is_left) //När programmet ser en ruta med en viss färg intieras ett nytt trafikljus
        {
            this.pos = new Point(x, y);
            this.dir = dir;
            is_left_turn = is_left;
        }
        /// <summary>
        ///  Inizialerar en ny trafikljus
        /// </summary>
        /// <param name="X">X postition</param>
        /// <param name="Y">Y postition</param>
        /// <param name="dir">Riktning g0-3</param>
        /// <param name="color">Färg</param>
        public Trafikljus(int X, int Y, int dir, int v_max) //Ett annat sätt att skapa ett trafikljus-.
        {
            pos = new Point(X, Y);

            this.dir = dir;

        }

        public void tick(int time)
        {
            switch (time % 144)
            {
                //Rött Vänster 1 3
                //Grönt 0 2
                case 0:
                    if ((this.dir == 0 | this.dir == 2) && !this.is_left_turn)
                        this.Gröntljus();
                    if ((this.dir == 1 || this.dir == 3) && this.is_left_turn)
                        this.Rödljus();
                    break;
                //Gult 0 2
                case 30:
                    if ((this.dir == 0 | this.dir == 2) && !this.is_left_turn)
                        this.Gultljus();
                    break;
                //Rött 0 2
                //Grönt vänster 0 2
                case 36:
                    if ((this.dir == 0 | this.dir == 2) && !this.is_left_turn)
                        this.Rödljus();
                    if ((this.dir == 0 | this.dir == 2) && this.is_left_turn)
                        this.Gröntljus();
                    break;
                //Gult vänster 0 2
                case 66:
                    if ((this.dir == 0 | this.dir == 2) && this.is_left_turn)
                        this.Gultljus();
                    break;
                //Rött vänster 0 2
                //Grönt 1 3
                case 72:
                    if ((this.dir == 0 | this.dir == 2) && this.is_left_turn)
                        this.Rödljus();
                    if ((this.dir == 1 | this.dir == 3) && !this.is_left_turn)
                        this.Gröntljus();
                    break;
                //Gult 1 3
                case 102:
                    if ((this.dir == 1 | this.dir == 3) && !this.is_left_turn)
                        this.Gultljus();
                    break;
                //Rött 1 3
                //Grönt vänster 1 3
                case 108:
                    if ((this.dir == 1 | this.dir == 3) && !this.is_left_turn)
                        this.Rödljus();
                    if ((this.dir == 1 | this.dir == 3) && this.is_left_turn)
                        this.Gröntljus();
                    break;
                //Gult vänster 1 3
                case 138:
                    if ((this.dir == 1 | this.dir == 3) && this.is_left_turn)
                        this.Gultljus();
                    break;

            }
        }

        public void Rödljus()
        {
            grönt = 0;
            MainForm.map_elements[X, Y] = 1;
        }
        public void Gröntljus() // Metoden kräver kod från andra delar av programmet.
        {
            grönt = 1;
            MainForm.map_elements[X, Y] = 2;
        }
        public void Gultljus()
        {
            stop = true;
            grönt = 2;
            MainForm.map_elements[X, Y] = 3;
        }

        public bool slaom()
        {
            Point p = pos;
            while (true)
            {
                for (int i = 0; i < MainForm.ants.Count; i++)
                {
                    if (MainForm.ants[i].Pos == p)
                    {

                        if (MainForm.ants[i].t_ljus)
                        {
                            return true;
                        }
                        return false;
                    }
                }
                if (MainForm.Start_Fields[dir].Contains(p))
                {
                    return true;
                }

                switch ((dir + 2) % 4) // Switch med resten av dir mod 4.
                {
                    case 0:
                        p.Y--;
                        break;
                    case 1:
                        p.X++;
                        break;
                    case 2:
                        p.Y++;
                        break;
                    case 3:
                        p.X--;
                        break;
                }
            }
        }
    }
}
