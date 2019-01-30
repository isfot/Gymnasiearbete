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
        public Dialogruta_CustomFlow()
        {
            InitializeComponent();
        }

        private void Return_button_Click(object sender, EventArgs e)
        {
             int[] flow = new int[16];
            for (int i=1;i<=flow.Length; i++)
            {
                try
                {
                    flow[i - 1] = Convert.ToInt32(this.Controls.Find("textbox" + i, true)[0].Text);
                }
               catch (Exception ex)
                {
                    flow[i - 1] = 1;
                }
                
            }
            for (int i=0; i < flow.Length; i++)
            {
                Form1.flow[i] = flow[i];
            }
            
            Close();
        }
    }
}
