using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Ant_test
{
    public partial class DataForm : Form
    {
        public DataForm()
        {
            InitializeComponent();
            dt = new DataTable();
            dt.Columns.Add("X", typeof(double));
            dt.Columns.Add("Y_Value_Speed_AVG", typeof(double));
            dt.Columns.Add("Value_Count", typeof(double));
            dt.Columns.Add("Y_Value_Speed_N", typeof(double));
            dt.Columns.Add("Y_Value_Speed_S", typeof(double));
            dt.Columns.Add("Y_Value_Speed_V", typeof(double));
            dt.Columns.Add("Y_Value_Speed_Ö", typeof(double));
        }
        private List<Ant> local_Ants;
        public void updateData(List<Ant> ants)
        {
            local_Ants = ants; // Tar ants från huvudformen 
            updater();
            //new Thread(updater).Start(); // En ny thread för parallisering dvs att dessa uträkningar sker som en process oberoende av programkörningen.
        }
        private DataTable dt;
        private double avg_speed; // variabel för att räkna ut medelhastighet av alla bilar
        private double avg_speed_n;// variabel för att räkna ut medelhastighet av alla bilar som kör åt norr
        private double avg_speed_s;// variabel för att räkna ut medelhastighet av alla bilar som kör åt söder
        private double avg_speed_v;// variabel för att räkna ut medelhastighet av alla bilar som kör åt väster
        private double avg_speed_ö;// variabel för att räkna ut medelhastighet av alla bilar som kör åt öster
        private double num; // Antal bilar 
        private double num_n;// Antal bilar som kör åt norr
        private double num_s;// Antal bilar som kör åt söder
        private double num_v;// Antal bilar som kör åt väster
        private double num_ö;// Antal bilar som kör åt öster
        private double flow; // Andel av bilar i rörelse
        private double flow_n;
        private double flow_s;
        private double flow_v;
        private double flow_ö;


        private Bitmap graph; // en bitmap för att rita ut en graf
        private int counter = 0;
        private void graphUpdateValue(double[] value_speed, double[] value_ants)
        {
            dt.Rows.Add(counter, value_speed[0], value_ants[0] / 100.0, value_speed[1], value_speed[2], value_speed[3], value_speed[4]);//HÄR
            chart1.DataSource = dt;
            chart1.Series["Avrage Speed"].XValueMember = "X";
            chart1.Series["Avrage Speed"].YValueMembers = "Y_Value_Speed_AVG";
            chart1.Series["Avrage Speed"].ChartType = SeriesChartType.Line;

            chart1.Series["Avrage Speed N"].XValueMember = "X";
            chart1.Series["Avrage Speed N"].YValueMembers = "Y_Value_Speed_N";
            chart1.Series["Avrage Speed N"].ChartType = SeriesChartType.Line;
            chart1.Series["Avrage Speed S"].XValueMember = "X";
            chart1.Series["Avrage Speed S"].YValueMembers = "Y_Value_Speed_S";
            chart1.Series["Avrage Speed S"].ChartType = SeriesChartType.Line;
            chart1.Series["Avrage Speed V"].XValueMember = "X";
            chart1.Series["Avrage Speed V"].YValueMembers = "Y_Value_Speed_V";
            chart1.Series["Avrage Speed V"].ChartType = SeriesChartType.Line;
            chart1.Series["Avrage Speed Ö"].XValueMember = "X";
            chart1.Series["Avrage Speed Ö"].YValueMembers = "Y_Value_Speed_Ö";
            chart1.Series["Avrage Speed Ö"].ChartType = SeriesChartType.Line;
            chart1.Series["Count"].XValueMember = "X";
            chart1.Series["Count"].YValueMembers = "Value_Count";
            chart1.Series["Count"].ChartType = SeriesChartType.Line;
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "";

            counter++;
        }

        private void updater()
        {
            avg_speed = 0;
            avg_speed_n = 0;
            avg_speed_s = 0;
            avg_speed_v = 0;
            avg_speed_ö = 0;
            num = 0;
            num_n = 0;
            num_s = 0;
            num_v = 0;
            num_ö = 0;
            foreach (Ant a in local_Ants)
            {
                switch (a._dir)
                {
                    case 0:
                        avg_speed_n += a.v;
                        if (a.v > 0)
                        {
                            flow_n++;
                        }
                        num_n++;
                        break;
                    case 1:
                        avg_speed_ö += a.v;
                        if (a.v > 0)
                        {
                            flow_ö++;
                        }
                        num_ö++;
                        break;
                    case 2:
                        avg_speed_s += a.v;
                        if (a.v > 0)
                        {
                            flow_s++;
                        }
                        num_s++;
                        break;
                    case 3:
                        avg_speed_v += a.v;
                        if (a.v > 0)
                        {
                            flow_v++;
                        }
                        num_v++;
                        break;
                }
                num++;
                avg_speed += a.v;
                if (a.v > 0)
                {
                    flow++;
                }
            }
            avg_speed = avg_speed / local_Ants.Count;
            avg_speed_n = avg_speed_n / num_n;
            avg_speed_s = avg_speed_s / num_s;
            avg_speed_v = avg_speed_v / num_v;
            avg_speed_ö = avg_speed_ö / num_ö;
            flow /= num;
            flow_n /= num_n;
            flow_s /= num_s;
            flow_v /= num_v;
            flow_ö /= num_ö;
            double[] speed = new double[] { avg_speed, avg_speed_n, avg_speed_s, avg_speed_v, avg_speed_ö };
            double[] cocosnötter = new double[] { local_Ants.Count, num_n, num_s, num_v, num_ö };
            double[] flows = new double[] { flow, flow_n, flow_s, flow_v, flow_ö };
            updateForm(speed, cocosnötter, flows);
            graphUpdateValue(speed, cocosnötter);
        }
        private void updateForm(double[] speed, double[] ants, double[] flows) // updaterar rutor i DataForm
        {
            label_speed_avg.Text = speed[0].ToString();
            label_speed_N.Text = speed[1].ToString();
            label_speed_S.Text = speed[2].ToString();
            label_speed_V.Text = speed[3].ToString();
            label_speed_Ö.Text = speed[4].ToString();
            label_ant_count.Text = ants[0].ToString();
            label_ant_count_n.Text = ants[1].ToString();
            label_ant_count_s.Text = ants[2].ToString();
            label_ant_count_v.Text = ants[3].ToString();
            label_ant_count_ö.Text = ants[4].ToString();
            label_flow.Text = flows[0].ToString();
            label_flow_N.Text = flows[1].ToString();
            label_flow_S.Text = flows[2].ToString();
            label_flow_V.Text = flows[3].ToString();
            label_flow_Ö.Text = flows[4].ToString();
        }

        private void DataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.dataFormActive = false;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void DataForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
