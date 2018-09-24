using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ant_test
{
    class BitmapAVC
    {
        private Bitmap _bitmap;
        private readonly Bitmap _Resetmap;
        private int MAPscale = 0;
        /// <summary>
        /// Inisializer
        /// </summary>
        /// <param name="input"></param>
        public BitmapAVC(Bitmap input)
        {
            _bitmap = input;
            _Resetmap = input;
        }
        public BitmapAVC(Image input)
        {
            _bitmap = new Bitmap(input);
            _Resetmap = new Bitmap(input);
        }
        public BitmapAVC(string Path)
        {
            _bitmap = new Bitmap(Image.FromFile(Path));
            _Resetmap = new Bitmap(Image.FromFile(Path));
        }
        /// <summary>
        /// Getters
        /// </summary>
        /// <returns></returns>
        public Bitmap get()
        {
            return _bitmap;
        }
        /// <summary>
        /// Setters
        /// </summary>
        /// <param name="input"></param>
        public void set(Bitmap input)
        {
            _bitmap = input;
        }
        public void set(Image input)
        {
            _bitmap = new Bitmap(input);
        }
        public void set(string Path)
        {
            _bitmap = new Bitmap(Image.FromFile(Path));
        }
        public void reset()
        {
            _bitmap = _Resetmap;
        }

        public void Upscale(int scale)
        {
            if (scale > 0)
            {
                MAPscale = scale;
                Bitmap output = new Bitmap(_bitmap.Width * scale, _bitmap.Height * scale);
                for (int x = 0; (x / scale) < _bitmap.Width; x++)
                {
                    for (int y = 0; (y / scale) < _bitmap.Height; y++)
                    {
                        output.SetPixel(x, y, _bitmap.GetPixel(x / scale, y / scale));
                    }
                }
                _bitmap = output;
            }
        }
        public void Upscale(string scaleSTR)
        {
            int scale = Convert.ToInt32(scaleSTR);
            if (scale > 0)
            {
                MAPscale = scale;
                Bitmap output = new Bitmap(_bitmap.Width * scale, _bitmap.Height * scale);
                for (int x = 0; (x / scale) < _bitmap.Width; x++)
                {
                    for (int y = 0; (y / scale) < _bitmap.Height; y++)
                    {
                        output.SetPixel(x, y, _bitmap.GetPixel(x / scale, y / scale));
                    }
                }
                _bitmap = output;
            }
        }

        public void Setpixel(Point pos, Color col)
        {
            for (int X = pos.X * MAPscale; X < pos.X * MAPscale + MAPscale; X++)
            {
                for (int Y = pos.Y * MAPscale; Y < pos.Y * MAPscale + MAPscale; Y++)
                {
                    try
                    { 
                        _bitmap.SetPixel(X, Y, col);
                    }
                    catch
                    {

                    }
                }
            }
        }
        public void Setpixel(int posX, int posY, Color col)
        {
            for (int X = posX * MAPscale; X < posX * MAPscale + MAPscale; X++)
            {
                for (int Y = posY * MAPscale; Y < posY * MAPscale + MAPscale; Y++)
                {
                    _bitmap.SetPixel(X, Y, col);
                }
            }
        }

        public Color GetPixel(Point input)
        {
            return _Resetmap.GetPixel(input.X, input.Y);
        }
        public Color GetPixel(int X, int Y)
        {
            return _bitmap.GetPixel(X, Y);
        }

    }
}
