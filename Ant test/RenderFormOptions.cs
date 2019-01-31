using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ant_test
{
    public partial class RenderFormOptions : Form
    {
        RenderForm initform;
        public RenderFormOptions(RenderForm caller)
        {
            initform = caller;
            InitializeComponent();
        }

        private void button_Fullscreen_Click(object sender, EventArgs e)
        {
            initform.fullscreen();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            initform.close();
            this.Close();
        }

        private void button_Normal_Click(object sender, EventArgs e)
        {
            initform.normal();
        }
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void RenderFormOptions_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
