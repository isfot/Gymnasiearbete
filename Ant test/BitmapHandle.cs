using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ant_test
{
    public class BitmapAVC
    {
        private Bitmap _bitmap; // Kartan som en bitmap
        private readonly Bitmap _Resetmap; // Ursprungskartan för återställning
        private readonly Bitmap _Resetmap_Upscale; // Ursprungskartan för återställning
        private int MAPscale = 0; // Skalningsfaktor så att rätt antal pixlar ifylls i den nyskapade bitmapen
        public int scale { get { return MAPscale; } }
        //public Bitmap get { get { return _bitmap; } }

        /// <summary>
        /// Initierar en ny BitmapAVC
        /// </summary>
        /// <param name="input">Bitmap</param>
        public BitmapAVC(Bitmap input) //Konstruktor för en AVC Bitmap, dvs en bitmap med fler funktioner
        {
            _bitmap = input; // Skapar en AVC bitmap enligt den bitmap input vi angett
            _Resetmap = input; // Skapar en backup av Bitmap för att kunna återställa till ursprunlig bitmap.
            _Resetmap_Upscale = privUpscale(MAPscale, _bitmap);
            MAPscale = 1;
        }
        public BitmapAVC(Image input) // Overload, för att kunna göra som ovanstående konstruktor fast med annan input
        {
            _bitmap = new Bitmap(input);
            _Resetmap = new Bitmap(input);
            _Resetmap_Upscale = privUpscale(MAPscale, _bitmap);
            MAPscale = 1;
        }
        public BitmapAVC(string Path)//Overload, för att kunna göra som ovanstående konstruktor fast med annan input
        {
            _bitmap = new Bitmap(Image.FromFile(Path)); // Använder string som filnamn
            _Resetmap = new Bitmap(Image.FromFile(Path));
            _Resetmap_Upscale = privUpscale(MAPscale, _bitmap);
            MAPscale = 1;
        }
        /// <summary>
        /// Hämtar bitmapen från klassen
        /// </summary>
        /// <returns>Upskalad Bitmap</returns>




        public void render(List<Trafikljus>[] Trafic_lights, List<Ant> ants, List<Point> White_Field)
        {
            unsafe
            {
                BitmapData bitmapData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadWrite, _bitmap.PixelFormat);

                int bytesPerPixel = Bitmap.GetPixelFormatSize(_bitmap.PixelFormat) / 8;
                int heightInPixels = _bitmap.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                //Pointer till bitmapadressen
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                //Pointer [x] = Blue; [x + 1] = Green; [x + 2] = Red;

                Parallel.ForEach(White_Field, w =>
                {
                    for (int y_Scale = 0; y_Scale < MAPscale; y_Scale++)
                    {
                        //pointer till linen som ska fyllas i
                        byte* currentLine = PtrFirstPixel + (w.Y * MAPscale * bitmapData.Stride) + (y_Scale * bitmapData.Stride);
                        //För varje x kordinat
                        for (int x_Scale = 0; x_Scale < MAPscale; x_Scale++)
                        {
                            int x = bytesPerPixel * ((w.X) * MAPscale + x_Scale);
                            currentLine[x] = 255;
                            currentLine[x + 1] = 255;
                            currentLine[x + 2] = 255;
                        }
                    }
                });


                //Sets traficlight colors into the byte array
                Parallel.For(0, Trafic_lights.Length, i =>
                {
                    Parallel.ForEach(Trafic_lights[i], t =>
                    {
                        //1 är status där trafikljuset är rött
                        if (t.grönt == 1)
                        {
                            for (int y_Scale = 0; y_Scale < MAPscale; y_Scale++)
                            {
                                byte* currentLine = PtrFirstPixel + (t.pos.Y * MAPscale * bitmapData.Stride) + (y_Scale * bitmapData.Stride);
                                for (int x_Scale = 0; x_Scale < MAPscale; x_Scale++)
                                {
                                    int x = bytesPerPixel * ((t.pos.X) * MAPscale + x_Scale);
                                    currentLine[x] = 0;
                                    currentLine[x + 1] = 255;
                                    currentLine[x + 2] = 0;
                                }
                            }
                        }
                        //2 är status där trafikljuset är grönt
                        else if (t.grönt == 0)
                        {
                            for (int y_Scale = 0; y_Scale < MAPscale; y_Scale++)
                            {
                                byte* currentLine = PtrFirstPixel + (t.pos.Y * MAPscale * bitmapData.Stride) + (y_Scale * bitmapData.Stride);
                                for (int x_Scale = 0; x_Scale < MAPscale; x_Scale++)
                                {
                                    int x = bytesPerPixel * ((t.pos.X) * MAPscale + x_Scale);
                                    currentLine[x] = 0;
                                    currentLine[x + 1] = 0;
                                    currentLine[x + 2] = 255;
                                }
                            }
                        }
                        //2 är status där trafikljuset är gult
                        else 
                        {
                            for (int y_Scale = 0; y_Scale < MAPscale; y_Scale++)
                            {
                                byte* currentLine = PtrFirstPixel + (t.pos.Y * MAPscale * bitmapData.Stride) + (y_Scale * bitmapData.Stride);
                                for (int x_Scale = 0; x_Scale < MAPscale; x_Scale++)
                                {
                                    int x = bytesPerPixel * ((t.pos.X) * MAPscale + x_Scale);
                                    currentLine[x] = 0;
                                    currentLine[x + 1] = 255;
                                    currentLine[x + 2] = 255;
                                }
                            }
                        }
                    });
                });
                //Pointer [x] = Blue; [x + 1] = Green; [x + 2] = Red;
                Parallel.ForEach(ants, a =>
                {
                    for (int y_Scale = 0; y_Scale < MAPscale; y_Scale++)
                    {
                        byte* currentLine = PtrFirstPixel + (a.Y * MAPscale * bitmapData.Stride) + (y_Scale * bitmapData.Stride);
                        for (int x_Scale = 0; x_Scale < MAPscale; x_Scale++)
                        {
                            int x = bytesPerPixel * ((a.X) * MAPscale + x_Scale);
                            currentLine[x] = a.Color.B;
                            currentLine[x + 1] = a.Color.G;
                            currentLine[x + 2] = a.Color.R
                            ;
                        }
                    }
                });

                _bitmap.UnlockBits(bitmapData);
            }
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
        /// Resets bitmap to original upscaled bitmap for now that is 10. !!The bitmap size will not make a diffrence after new rendering method is in place!!
        /// </summary>
        public void reset_UP()
        {
            _bitmap = _Resetmap_Upscale;
        }
        /// <summary>
        /// Skalar upp bitmap med avseende på skalan som anges
        /// </summary>
        /// <param name="scale">Skalan som bitmapen ska skalas upp med</param>
        public void Upscale(int scale) // Metod för att skala upp bitmapen (endast för grafik)
        {
            if (scale > 0)
            {
                MAPscale *= scale;
                Bitmap output = new Bitmap(_bitmap.Width * scale, _bitmap.Height * scale);
                unsafe
                {
                    BitmapData bitmapData_org = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadWrite, _bitmap.PixelFormat);
                    BitmapData bitmapData_new = output.LockBits(new Rectangle(0, 0, output.Width, output.Height), ImageLockMode.ReadWrite, output.PixelFormat);
                    int width = _bitmap.Width;
                    int bytesPerPixel_org = Bitmap.GetPixelFormatSize(_bitmap.PixelFormat) / 8;
                    int heightInPixels_org = _bitmap.Height;
                    int widthInBytes_org = bitmapData_org.Width * bytesPerPixel_org;
                    //Pointer till bitmapadressen org
                    byte* PtrFirstPixel_org = (byte*)bitmapData_org.Scan0;

                    int bytesPerPixel_new = Bitmap.GetPixelFormatSize(output.PixelFormat) / 8;
                    int heightInPixels_new = output.Height;
                    int widthInBytes_new = bitmapData_new.Width * bytesPerPixel_new;
                    //Pointer till bitmapadressen new
                    byte* PtrFirstPixel_new = (byte*)bitmapData_new.Scan0;
                    Parallel.For(0, (width * heightInPixels_org), pixel =>
                    {
                        byte b = PtrFirstPixel_org[(pixel * bytesPerPixel_org)];
                        byte g = PtrFirstPixel_org[(pixel * bytesPerPixel_org) + 1];
                        byte r = PtrFirstPixel_org[(pixel * bytesPerPixel_org) + 2];
                        byte* pointer_to_set = PtrFirstPixel_new + pixel * bytesPerPixel_org * MAPscale + widthInBytes_new * (pixel / width) * 9;
                        Parallel.For(0, MAPscale, yy =>
                        {
                            Parallel.For(0, MAPscale, xx =>
                            {
                                pointer_to_set[xx * bytesPerPixel_new + yy * widthInBytes_new] = b;
                                pointer_to_set[xx * bytesPerPixel_new + 1 + yy * widthInBytes_new] = g;
                                pointer_to_set[xx * bytesPerPixel_new + 2 + yy * widthInBytes_new] = r;
                                pointer_to_set[xx * bytesPerPixel_new + 3 + yy * widthInBytes_new] = 255;
                            });
                        });
                    });

                    _bitmap.UnlockBits(bitmapData_org);
                    output.UnlockBits(bitmapData_new);
                }
                _bitmap = output;
            }
        }
        public Bitmap get() // Metod för att hämta bitmapen
        {
            return _bitmap;
        }
        /// <summary>
        /// Private upcaler for initialization !!this will be updated to new method of renering!!
        /// </summary>
        /// <param name="scale"></param>
        public Bitmap privUpscale(int scale, Bitmap input) // Metod för att skala upp bitmapen (endast för grafik)
        {
            if (scale > 0)
            {
                Bitmap output = new Bitmap(input.Width * scale, input.Height * scale);
                for (int x = 0; (x / scale) < input.Width; x++)
                {
                    for (int y = 0; (y / scale) < input.Height; y++)
                    {
                        output.SetPixel(x, y, input.GetPixel(x / scale, y / scale));
                    }
                }
                return output;
            }
            return null;
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
            return _bitmap.GetPixel(input.X * MAPscale, input.Y * MAPscale);
        }
        /// <summary>
        /// Hämtar en pixelfärg från bitmapen
        /// </summary>
        /// <param name="X">Pixels X position</param>
        /// <param name="Y">Pixels Y position</param>
        /// <returns></returns>
        public Color GetPixel(int X, int Y)
        {
            return _bitmap.GetPixel(X * MAPscale, Y * MAPscale);
        }

    }
}
