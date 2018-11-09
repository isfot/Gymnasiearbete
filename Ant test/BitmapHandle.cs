using System;
using System.Drawing;

namespace Ant_test
{
    public class BitmapAVC
    {
        private Bitmap _bitmap; // Kartan som en bitmap
        private readonly Bitmap _Resetmap; // Ursprungskartan för återställning
        private int MAPscale = 0; // Skalningsfaktor så att rätt antal pixlar ifylls i den nyskapade bitmapen
        /// <summary>
        /// Initierar en ny BitmapAVC
        /// </summary>
        /// <param name="input">Bitmap</param>
        public BitmapAVC(Bitmap input) //Konstruktor för en AVC Bitmap, dvs en bitmap med fler funktioner
        {
            _bitmap = input; // Skapar en AVC bitmap enligt den bitmap input vi angett
            _Resetmap = input; // Skapar en backup av Bitmap för att kunna återställa till ursprunlig bitmap.
        }
        public BitmapAVC(Image input) // Overload, för att kunna göra som ovanstående konstruktor fast med annan input
        {
            _bitmap = new Bitmap(input);
            _Resetmap = new Bitmap(input);
        }
        public BitmapAVC(string Path)//Overload, för att kunna göra som ovanstående konstruktor fast med annan input
        {
            _bitmap = new Bitmap(Image.FromFile(Path)); // Använder string som filnamn
            _Resetmap = new Bitmap(Image.FromFile(Path));
        }
        /// <summary>
        /// Hämtar bitmapen från klassen
        /// </summary>
        /// <returns>Upskalad Bitmap</returns>
        public Bitmap get() // Metod för att hämta bitmapen
        {
            return _bitmap;
        }
        /// <summary>
        /// Setters
        /// </summary>
        /// <param name="input">Ny bitmap</param>
        public void set(Bitmap input) // För att ändra en redan existerande bitmap
        {
            _bitmap = input;
        }
        public void set(Image input)// För att ändra en redan existerande bitmap
        {
            _bitmap = new Bitmap(input);
        }
        public void set(string Path)// För att ändra en redan existerande bitmap
        {
            _bitmap = new Bitmap(Image.FromFile(Path));
        }
        /// <summary>
        /// Ställer om bitmap till det som det ställdes in till orginellt
        /// </summary>
        public void reset()
        {
            _bitmap = _Resetmap;
        }
        /// <summary>
        /// Skalar upp bitmap med avseende på skalan som anges
        /// </summary>
        /// <param name="scale"></param>
        public void Upscale(int scale) // Metod för att skala upp bitmapen (endast för grafik)
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
        public void Upscale(string scaleSTR)//Overload som tar en string istället för en siffra
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
        /// <summary>
        /// Sätter en pixel i den potentiellt uppskalade bitmapen
   
            /// </summary>
        /// <param name="pos">Vilket pixel som ska sättas</param>
        /// <param name="col">Vilken färg denna pixel ska vara</param>
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
                    catch // För ev felhantering
                    {

                    }
                }
            }
        }
        /// <summary>
        /// Sätter en pixel i den potentiellt uppskalade bitmapen
        /// </summary>
        /// <param name="posX">Pixels X position</param>
        /// <param name="posY">Pixels Y position</param>
        /// <param name="col">Pixelns färger</param>
        public void Setpixel(int posX, int posY, Color col)
        {
            for (int X = posX * MAPscale; X < posX * MAPscale + MAPscale; X++)
            {
                for (int Y = posY * MAPscale; Y < posY * MAPscale + MAPscale; Y++)
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
        /// <summary>
        /// Hämtar en pixelfärg från bitmapen
        /// </summary>
        /// <param name="input">Pixelns position</param>
        /// <returns></returns>
        public Color GetPixel(Point input)
        {
            return _Resetmap.GetPixel(input.X, input.Y);
        }
        /// <summary>
        /// Hämtar en pixelfärg från bitmapen
        /// </summary>
        /// <param name="X">Pixels X position</param>
        /// <param name="Y">Pixels Y position</param>
        /// <returns></returns>
        public Color GetPixel(int X, int Y)
        {
            return _bitmap.GetPixel(X, Y);
        }

    }
}
