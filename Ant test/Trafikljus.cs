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
        public Point pos;
        public bool grönt;
        private readonly int dir;   //vilket håll den ska släppa igenom bilar
        private readonly int v_max; // Globalt hastighetsmaximum


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

       
        public void Rödljus() 
        {
            grönt = false;
            switch (dir) //utifrån vilken riktning måste den stänga av olika rutor
            {
                case 2:
                    for (int i = 0; i <= 2; i++) // Stänger 2 rutor innan sin egen position.
                    {
                        Form1.map_elements[pos.X, pos.Y - i] = 1; 
                    }
                    break;
                case 1:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.map_elements[pos.X - i, pos.Y] = 1;
                    }
                    break;
                case 0:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.map_elements[pos.X, pos.Y + i] = 1;
                    }
                    break;
                case 3:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.map_elements[pos.X + i, pos.Y] = 1;
                    }
                    break;
            }





        }
        public void Gröntljus() // Metoden kräver kod från andra delar av programmet.
        {
            grönt = true; 
            switch (dir)  //tar bort det som rött gjorde 
            {
                case 2:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.map_elements[pos.X, pos.Y - i] = 2;
                    }
                    break;
                case 1:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.map_elements[pos.X - i, pos.Y] = 2;
                    }
                    break;
                case 0:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.map_elements[pos.X, pos.Y + i] = 2;
                    }
                    break;
                case 3:
                    for (int i = 0; i <= 2; i++)
                    {
                        Form1.map_elements[pos.X + i, pos.Y] = 2;
                    }
                    break;
            }
        }
    }
}
