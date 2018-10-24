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
        public Color color;
        private readonly int dir;
        private readonly int v_max;

        /// <summary>
        /// Inizialerar ett nytt trafikljus
        /// </summary>
        /// <param name="pos">Position</param>
        /// <param name="dir">Riktning 0-3</param>
        /// <param name="color">Färg</param>
        public Trafikljus(Point pos, int dir,  Color color)
        {
            this.pos = pos;
           this.color = color;
            this.dir = dir;
        }
        /// <summary>
        ///  Inizialerar en ny trafikljus
        /// </summary>
        /// <param name="X">X postition</param>
        /// <param name="Y">Y postition</param>
        /// <param name="dir">Riktning g0-3</param>
        /// <param name="color">Färg</param>
        public Trafikljus(int X, int Y, int dir, Color color)
        {
            pos = new Point(X, Y);
            this.color = color;
            this.dir = dir;
        }

        public void Rödljus() // Metoden kräver kod från andra delar av programmet.
        {
            int[] redlights = new int[2];

            for (int i=0; i<Math.Pow(v_max,2)*v_max/2; i++)
            {
                switch (dir)
                {
                    case 0: break;
                    redlights.SetValue
                    case 1: break;
                    case 2: break;
                    case 3: break;
                }
                
            }
            

                 
        }
    }
}
