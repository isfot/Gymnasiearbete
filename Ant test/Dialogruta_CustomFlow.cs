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
    public partial class Dialogruta_CustomFlow : Form
    {
        private readonly Form1 initform;
        public Dialogruta_CustomFlow(Form1 caller)
        {
            initform = caller;
            InitializeComponent();
            for (int i = 0; i < Form1.flow.Length; i++)
            {
                this.Controls.Find("textbox" + (i + 1), true)[0].Text = Form1.flow[i].ToString();
            }
        }

        private void Return_button_Click(object sender, EventArgs e)
        {
            int[] flow = new int[16];
            for (int i = 0; i < flow.Length; i++)
            {
                try
                {
                    flow[i] = Convert.ToInt32(this.Controls.Find("textbox" + (i + 1), true)[0].Text);
                }
                catch
                {
                    flow[i] = 1;
                }

            }
            for (int i = 0; i < flow.Length; i++)
            {
                Form1.flow[i] = flow[i];
            }

            initform.add_ants();
        }

        //UI
        private void button_Close_MouseEnter(object sender, EventArgs e)
        {
            button_Close.BackgroundImage = Ant_test.Properties.Resources.Close_Button_Invert;
        }

        private void button_Close_MouseLeave(object sender, EventArgs e)
        {
            button_Close.BackgroundImage = Ant_test.Properties.Resources.Close_Button;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Dialogruta_CustomFlow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
