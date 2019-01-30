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
    public partial class RenderForm : Form
    {
        Form1 initform;
        public RenderForm(Form1 caller)
        {
            initform = caller;
            InitializeComponent();
            initform.renderFormActive = true;
            RenderFormOptions R = new RenderFormOptions(this);
            R.Show();
        }

        public void close()
        {
            initform.renderFormActive = false;
            this.Close();
        }
        public void set(Bitmap input)
        {
            pictureBox1.Image = input;
        }
        
        public void fullscreen()
        {
            this.WindowState = FormWindowState.Maximized;
        }

        public void normal()
        {
            this.WindowState = FormWindowState.Normal;
        }

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
