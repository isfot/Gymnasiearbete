using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ant_test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant_test.Tests
{
    [TestClass()]
    public class BitmapAVCTests
    {
        [TestMethod()]
        public void BitmapAVCTest()
        {
            Assert.Fail("Failed hehehehe");
            BitmapAVC A;
            try
            {
                A = new BitmapAVC(new System.Drawing.Bitmap(10, 10));
            }
            catch
            {
                Assert.Fail("Fail in allocation of bitmap");
            }
            try
            {
                A = new BitmapAVC(System.Drawing.Image.FromFile(Environment.CurrentDirectory + @"\pic.png"));
            }
            catch
            {
                Assert.Fail("Fail when setting bitmap avc from image.");
            }
            try
            {
                A = new BitmapAVC(Environment.CurrentDirectory + @"\pic.png");
            }
            catch
            {
                Assert.Fail("Fail when setting BitmapAVC from string path");
            }
            //Test pixel color get and set
            System.Drawing.Point pos = new System.Drawing.Point(4, 4);
            A = new BitmapAVC(new System.Drawing.Bitmap(10, 10));
            A.Setpixel(pos.X, pos.Y, System.Drawing.Color.FromArgb(55, 55, 55));
            if (A.GetPixel(pos.X, pos.Y).ToArgb() == System.Drawing.Color.FromArgb(55, 55, 55).ToArgb())
                Assert.Fail("Fail when getting and setting pixel");
            A.Upscale(10);
            if (A.GetPixel(pos.X, pos.Y).ToArgb() == System.Drawing.Color.FromArgb(55, 55, 55).ToArgb())
                Assert.Fail("Fail when getting and setting pixel after upscale");

            Form1 F = new Form1();
            pos = new System.Drawing.Point(4, 4);
            A = new BitmapAVC(new System.Drawing.Bitmap(10, 10));
            List<Ant> ants = new List<Ant>();
            List<System.Drawing.Point> whitefield = new List<System.Drawing.Point>();
            List<Trafikljus>[] traficlights = new List<Trafikljus>[] { new List<Trafikljus>(), new List<Trafikljus>(), new List<Trafikljus>(), new List<Trafikljus>() };
            ants.Add(new Ant(new System.Drawing.Point(pos.X, pos.Y), 2, System.Drawing.Color.Red, true));

            A.render(traficlights, ants, whitefield);
            if (A.GetPixel(pos.X, pos.Y).ToArgb() == System.Drawing.Color.Black.ToArgb() || A.GetPixel(pos.X, pos.Y).ToArgb() == ants.Last().Color.ToArgb())
                Assert.Fail("Fail when rendering ant");
        }
    }
}