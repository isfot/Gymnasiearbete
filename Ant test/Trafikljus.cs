using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ant_test
{
    //Klass för att sköta trafikljusen i korsningen och i och med det trafikflödet
    class Trafikljus
    {
        private Point pos;

        private readonly int dir;
        private readonly int v_max;


        /// <summary>
        /// Inizialerar ett nytt trafikljus
        /// </summary>
        /// <param name="pos">Position</param>
        /// <param name="dir">Riktning 0-3</param>
        /// <param name="color">Färg</param>
        public Trafikljus(Point pos, int dir, int v_max)
        {
            this.pos = pos;

            this.dir = dir;

        }
        /// <summary>
        ///  Inizialerar en ny trafikljus
        /// </summary>
        /// <param name="X">X postition</param>
        /// <param name="Y">Y postition</param>
        /// <param name="dir">Riktning g0-3</param>
        /// <param name="color">Färg</param>
        public Trafikljus(int X, int Y, int dir, int v_max)
        {
            pos = new Point(X, Y);

            this.dir = dir;

        }

        /// <summary>
        /// 
        /// </summary>
        public void Rödljus() // Metoden kräver kod från andra delar av programmet.
        {
            switch (dir)
            {
                case 0:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.karta[pos.X, pos.Y - i] = true;
                    }
                    break;
                case 1:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.karta[pos.X - i, pos.Y] = true;
                    }
                    break;
                case 2:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.karta[pos.X, pos.Y + i] = true;
                    }
                    break;
                case 3:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.karta[pos.X + i, pos.Y] = true;
                    }
                    break;
            }





        }
        public void Gröntljus() // Metoden kräver kod från andra delar av programmet.
        {

            switch (dir)
            {
                case 0:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.karta[pos.X, pos.Y - i] = false;
                    }
                    break;
                case 1:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.karta[pos.X - i, pos.Y] = false;
                    }
                    break;
                case 2:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.karta[pos.X, pos.Y + i] = false;
                    }
                    break;
                case 3:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.karta[pos.X + i, pos.Y] = false;
                    }
                    break;




            }
        }
    }
}
