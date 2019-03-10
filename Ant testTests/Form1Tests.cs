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
    public class Form1Tests
    {
        [TestMethod()]
        public void Form1Test()
        {
            string[] args = new string[] { Environment.CurrentDirectory + @"\pic.png" };
            try
            {
                MainForm form = new MainForm(args);
            }
            catch(Exception ex)
            {
                Assert.Fail("Form1 import picture fail: " + ex.ToString());
            }
            try
            {
                MainForm form = new MainForm();
            }
            catch
            {
                Assert.Fail();
            }
            args = new string[] { Environment.CurrentDirectory + @"\picNOT_A_PICTURE_IN_PATH_HAHAH.png" };
            try
            {
                MainForm form = new MainForm(args);
                Assert.Fail("Form1 import picture fail: LET INVALID PICTURE THROUGH");
            }
            catch{    }
        }
    }
}