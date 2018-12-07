using System.Drawing;

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
            if (affect_Fields)
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
            if (affect_Fields && _pos.Y < Form1.map.Height - 1 && _pos.Y > 0 && _pos.X < Form1.map.Width - 1 && _pos.X > 0)  //håller myran innaför spelplanen
            {
                Form1.karta[getPos().X, getPos().Y] = true;
            }

        }
        private static bool trace_stop(Ant i)  //har med hastighet att göra
        {
            if (Form1.map.Width > i.X && -1 < i.X && Form1.map.Height > i.Y && -1 < i.Y)
            {
                switch (Form1.map_elements[i.X, i.Y])
                {
                    case 1:
                    case -1:
                        return false;
                }
                if (Form1.karta[i.X, i.Y])
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        public static int trace(Ant in_)
        {
            int step;
            Ant trace = new Ant(in_._pos, in_._dir, Color.White, false);
            trace.affect_Fields = false;
            for (step = 0; trace_stop(trace); step++)
            {
                Form1.mapAVC.Setpixel(trace.X, trace.Y, Color.Aqua);
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
                else if (Form1.Turn_fields_Left_Diagonal[trace._dir].Contains(trace.getPos()) && !Form1.is_ant_to_side_right(trace))
                {
                    trace.step();
                    trace._dir = dirOverFlowCorr(trace._dir - 1);
                    trace.step();
                    trace._dir = dirOverFlowCorr(trace._dir + 1);
                }
                else// Gör tillsammans med if-sats i början att trace myra dör på killfields.
                {
                    trace.step();
                }
            }
            return step;
        }
        /// <summary>
        /// Korrigerar överflöden i riktningen
        /// </summary>
        /// <param name="_dir"></param>
        /// <returns></returns>
        private static int dirOverFlowCorr(int _dir) // endast riktningarna 0,1,2,3 är tillåtna
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
