using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Ant_test
{
    public class Ant
    {
        private Point _pos;	//En en koordinat för myrans placering i bitmap
        public int X { get { return _pos.X; } }
        public int Y { get { return _pos.Y; } }
        public Point Pos { get { return _pos; } }
        private bool affect_Fields = true;
        public int _dir;        //Variabel för myrans riktning
        public Color Color;     //Variabel för myrans färg
        private readonly Color ORGcolor;
        private int hastighet = 0;
        private readonly bool RealAnt;
        /// <summary>
        /// Inizialerar en ny myra
        /// </summary>
        /// <param name="pos">Position</param>
        /// <param name="dir">Riktning 0-3</param>
        /// <param name="color">Färg</param>
        public Ant(Point pos, int dir, Color color, bool RF)
        {
            _pos = pos;
            _dir = dir;
            Color = color;
            ORGcolor = color;
            RealAnt = RF;
            if (RealAnt)
            {
                Form1.karta[pos.X, pos.Y] = true;
            }


        }
        /// <summary>
        ///  Inizialerar en ny myra
        /// </summary>
        /// <param name="X">X postition</param>
        /// <param name="Y">Y postition</param>
        /// <param name="dir">Riktning g0-3</param>
        /// <param name="color">Färg</param>
        public Ant(int X, int Y, int dir, Color color, bool RF)
        {
            _pos = new Point(X, Y);
            _dir = dir;
            Color = color;
            ORGcolor = color;
            RealAnt = RF;
            if (RealAnt)
            {
                Form1.karta[X, Y] = true;
            }

        }
        /// <summary>
        /// Resettar färgen på myran
        /// </summary>
        public void resetColor()
        {
            Color = ORGcolor;
        }
        /// <summary>
        /// Hämtar positionen
        /// </summary>
        /// <returns>Point med postion</returns>
        public Point getPos()
        {
            return _pos;
        }
        /// <summary>
        /// Hämtar postion som Y värde
        /// </summary>
        /// <returns>int med X position</returns>
        public int getPosX()
        {
            return _pos.X;
        }
        /// <summary>
        /// Hämtar postion som Y värde
        /// </summary>
        /// <returns>int med Y position</returns>
        public int getPosY()
        {
            return _pos.Y;
        }
        /// <summary>
        /// Sätter en position med en point som input
        /// </summary>
        /// <param name="input">Point som anger postion</param>
        public void setPos(Point input)
        {
            _pos = input;
        }
        /// <summary>
        /// Sätter en postion med ett y och x värde
        /// </summary>
        /// <param name="X">X postion</param>
        /// <param name="Y">Y postion</param>
        public void setPos(int X, int Y)
        {
            _pos = new Point(X, Y);
        }
        public bool getRealAnt()
        {
            return RealAnt;
        }

        //  int mod(int x, int m)
        //  {
        //      return (x % m + m) % m;
        //  }
        /// <summary>
        /// Ökar myrans postion i den riktning som riktnings variabeln anger
        /// </summary>
        public void step()  //påverkar myrorna (utanför huvudprogrammet) vid varje tidsenhet ska myrorna göra step
        {
            if (RealAnt)
            {
                Form1.karta[getPos().X, getPos().Y] = false; //gamla ruta blir false, nya blire true
            }

            switch (_dir % 4) // Switch med resten av dir mod 4.
            {
                case 0:
                    _pos.Y--;
                    break;
                case 1:
                    _pos.X++;
                    break;
                case 2:
                    _pos.Y++;
                    break;
                case 3:
                    _pos.X--;
                    break;
            }
            if (RealAnt && _pos.Y < Form1.map.Height - 1 && _pos.Y > 0 && _pos.X < Form1.map.Width - 1 && _pos.X > 0)  //håller myran innaför spelplanen
            {
                Form1.karta[getPos().X, getPos().Y] = true;
            }


        }
        public Point stepcalc()
        {
            switch (_dir % 4) // Switch med resten av dir mod 4.
            {
                case 0:
                    return new Point(_pos.X, _pos.Y - 1);

                case 1:
                    return new Point(_pos.X + 1, _pos.Y);

                case 2:
                    return new Point(_pos.X, _pos.Y + 1);

                case 3:
                    return new Point(_pos.X - 1, _pos.Y);

            }
            return _pos;
        }
        private bool trace_stop(Ant i, int step)  // Ska myran som räknar steg sluta bestämms av denna funktion.
        {
            if (step != 0)
            {
                try
                {
                    if (Form1.map_elements[i.X, i.Y] == 1 || Form1.map_elements[i.X, i.Y] == -1)
                    {
                        return false;
                    }
                    if (Form1.karta[i.X, i.Y]) // Om vi får true här är den nuvarande positionen okuperad av en myra
                    {
                        return false;
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        public void trace(out int step, out bool need_brake)
        {
            need_brake = true;
            Ant trace = new Ant(_pos.X, _pos.Y, _dir, Color.White, false); // Skapar en lokal myra som får springa till den stöter på något.
            for (step = 0; trace_stop(trace, step); step++) // En for-loop som ökar antalet steg tills logiken blir false se metod trace_stop ovan.
            {
                for (int i = 0; i < Form1.Turn_fields_Left.Length; i++)
                {
                    if (Form1.Turn_fields_Left[i].Contains(trace.Pos) && trace._dir == i)
                    {
                        trace._dir--;
                        trace._dir = dirOverFlowCorr(trace._dir);
                    }
                }
                for (int i = 0; i < Form1.Turn_fields_Right.Length; i++)
                {
                    if (Form1.Turn_fields_Right[i].Contains(trace.Pos) && trace._dir == i)
                    {
                        trace._dir++;
                        trace._dir = dirOverFlowCorr(trace._dir);
                    }
                }
                //Sväng diagonalt
                if (Form1.Turn_fields_Right_Diagonal.Contains(trace.getPos()))
                {
                    trace.step();
                    trace._dir = dirOverFlowCorr(trace._dir + 1);
                    trace.step();
                    trace._dir = dirOverFlowCorr(trace._dir - 1);
                }
                else// Gör tillsammans med if-sats i början att trace myra dör på killfields.
                {
                    trace.step();
                }
                Form1.mapAVC.Setpixel(trace.X, trace.Y, Color.Aqua);
            }
            if(Form1.map_elements[trace.X,trace.Y] == -1)
            {
                need_brake = false;
            }
        }
        /// <summary>
        /// Korrigerar överflöden i riktningen
        /// </summary>
        /// <param name="_dir"></param>
        /// <returns></returns>
        private int dirOverFlowCorr(int _dir) // endast riktningarna 0,1,2,3 är tillåtna
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
        /// <summary>
        /// En metod för att myran ej skall få en odefinierad riktning. dir är enbart definierad för 0-3.
        /// </summary>
        private void dirOverFlowCorr() //enbart riktningarna 0,1,2,3
        {
            if (_dir > 3)
            {
                _dir = 0;
            }
            else if (_dir < 0)
            {
                _dir = 3;
            }
        }
    }
}
