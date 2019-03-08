using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Ant_test
{
    class PictureExport
    {
        private readonly Bitmap input;
        private readonly int scale;
        private readonly string basePath;
        private int frame;
        public PictureExport(Bitmap input_, int scale_, int frame_, string basePath_)
        {
            input = input_;
            scale = scale_;
            frame = frame_;
            basePath = basePath_;
            //Export();
            Thread export = new Thread(Export);
            export.Start();
        }

        private void Export()
        {
            Thread.Sleep(100);
            Bitmap output_bitmap = new Bitmap(input.Width * scale, input.Height * scale);
            for (int x = 0; (x / scale) < input.Width; x++)
            {
                for (int y = 0; (y / scale) < input.Height; y++)
                {
                    output_bitmap.SetPixel(x, y, input.GetPixel(x / scale, y / scale));
                }
            }
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            output_bitmap.Save(basePath + frame + ".png");
        }

    }
}
