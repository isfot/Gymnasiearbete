using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Trafikdata_Kompilering
{
    public partial class TrafikForm : Form
    {
        private string filepath;
        private bool file_valid;
        public TrafikForm()
        {
            InitializeComponent();
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            //Sökväg till fil
            filepath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            //Kollar ifall filen är okej
            file_valid = (File.Exists(filepath) && Path.GetExtension(filepath) == ".txt");
            //Sätter textbox
            textBox_filename.Text = Path.GetFileName(filepath);
            //Sätter Färg
            if (file_valid)
                panel1.BackColor = Color.Green;
            else
                panel1.BackColor = Color.Red;

            compile();
        }

        double[] procent;

        private void compile()
        {
            if (file_valid)
            {
                string[] lines = removefirst(File.ReadAllLines(filepath));
                int[] lines_int = convert(lines);
                double total = sum(lines);
                procent = new double[lines_int.Length];
                clear();
                for (int i = 0; i < lines_int.Length; i++)
                {
                    procent[i] = Convert.ToDouble(lines_int[i]) / total;
                    print(procent[i] * Convert.ToDouble(textBox_total.Text));
                }

            }
        }

        private void clear()
        {
            richTextBox1.Text = "";
        }

        private void print(object input)
        {
            if (input.GetType() == typeof(String) || input.GetType() == typeof(Double))
            {
                richTextBox1.AppendText(Convert.ToString(input) + "\n");
            }

        }

        private int[] convert(string[] input)
        {
            int[] output = new int[input.Length];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = Convert.ToInt32(input[i].Split(' ')[1]);
            }
            return output;
        }

        private int sum(string[] input)
        {
            int output = 0;
            foreach (string x in input)
            {
                output += Convert.ToInt32(x.Split(' ')[1]);
            }
            return output;
        }

        private string[] removefirst(string[] input)
        {
            string[] output = new string[input.Length - 1];
            for (int i = 1; i < input.Length; i++)
            {
                output[i - 1] = input[i];
            }
            return output;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
            for (int i = 0; i < procent.Length; i++)
            {
                print(procent[i] * Convert.ToDouble(textBox_total.Text));
            }
        }
    }
}
