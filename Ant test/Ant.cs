using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ant_test
{
    class Ant
    {
        private Point _pos;
        public int _dir;        // variabel för myrans riktning
        public Color Color;     // variabel för myrans färg
        private readonly Color ORGcolor; // myrans orginalfärg
        public Ant(Point pos, int dir, Color color) // Default Constructor 
        {
            _pos = pos;
            _dir = dir;
            Color = color;
            ORGcolor = color;
        }
        public Ant(int X, int Y, int dir, Color color) // Constructor med position som 2 int
        {
            _pos = new Point(X, Y);
            _dir = dir;
            Color = color;
            ORGcolor = color;
        }
        //getters
        public Point getPos()
        {
            return _pos;
        }
        public int getPosX()
        {
            return _pos.X;
        }
        public int getPosY()
        {
            return _pos.Y;
        }
        //setters
        public void setPos(Point input)
        {
            _pos = input;
        }
        public void setPos(int X, int Y)
        {
            _pos = new Point(X, Y);
        }

      //  int mod(int x, int m)
      //  {
      //      return (x % m + m) % m;
      //  }
        public void step()
        {
            switch (_dir % 4)
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
            
        }

        private void dirOverFlowCorr()
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

        private bool antPresent(List<Ant> ants, int dir, int number)
        {
            Point c = ants[number].getPos();
            switch (dir)
            {
                case 0:
                    c.Y--;
                    break;
                case 1:
                    c.X++;
                    break;
                case 2:
                    c.Y++;
                    break;
                case 3:
                    c.X--;
                    break;
            }
            foreach (Ant x in ants)
            {
                if (ants[number] != x)
                {
                    if (c == x.getPos())
                    {
                        return true;
                    }
                }
            }

            return false;
        }




    }
}
