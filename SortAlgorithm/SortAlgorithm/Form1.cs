using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SortAlgorithm
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();

            Thread t = new Thread(new ThreadStart(Set));
            t.Start();
        }

        private List<int> RandomValues = new List<int>();

        private void StartSort()
        {
            int i = 1;
            int[] List = RandomValues.ToArray();

            while (true)
            {
                UpdataChart(List);

                bool Check = true;

                while (true)
                {
                    if (i + 2 > RandomValues.Count)
                    {
                        break;
                    }

                    int c = ComparisonValue(List[i], List[i + 1]);

                    int x1 = List[i];
                    int x2 = List[i + 1];

                    if (c == 1)
                    {
                        List[i] = x2;
                        List[i + 1] = x1;
                        Check = false;
                    }

                    i += 1;

                }

                i = RandomValues.Count - 1;

                while (true)
                {
                    if (i - 2 < 0)
                    {
                        break;
                    }

                    System.Diagnostics.Debug.WriteLine(i);
                    int c = ComparisonValue(List[i], List[i - 1]);

                    int x1 = List[i];
                    int x2 = List[i - 1];

                    if (c == 2)
                    {
                        List[i] = x2;
                        List[i - 1] = x1;
                        Check = false;
                    }

                    i -= 1;
                }

                i = 0;
                if(Check)
                {
                    UpdataChart(List);
                    break;
                }
            }

        }

        private void UpdataChart(int[] list)  
        {
            Invoke((MethodInvoker)(() => chart1.Series.Clear()));
            Invoke((MethodInvoker)(() => chart1.ChartAreas.Clear()));

            string chart_area1 = "Area1";
            Invoke((MethodInvoker)(() => chart1.ChartAreas.Add(new ChartArea(chart_area1))));
            string legend1 = "Graph1";
            Invoke((MethodInvoker)(() => chart1.Series.Add(legend1)));
            Invoke((MethodInvoker)(() => chart1.Series[legend1].ChartType = SeriesChartType.Column));

            for (int i = 0; i < list.Length; i++)
            {
                Invoke((MethodInvoker)(() => chart1.Series[legend1].Points.AddY(list[i])));
            }
        }

        private int ComparisonValue(int x1,int x2)
        {
            System.Diagnostics.Debug.WriteLine("X1:" + x1.ToString() + " X2:" + x2.ToString());
            int x = 0;

            if(x1 > x2)
            {
                x = 1;
            }
            else if(x1 < x2)
            {
                x = 2;
            }
            else
            {
                x = 0;
            }

            return x;
        }

        private Random rnd = new Random();

        private void Set()
        {
            try
            {
                RandomValues = new List<int>();

                Invoke((MethodInvoker)(() => chart1.Series.Clear()));
                Invoke((MethodInvoker)(() => chart1.ChartAreas.Clear()));

                string chart_area1 = "Area1";
                Invoke((MethodInvoker)(() => chart1.ChartAreas.Add(new ChartArea(chart_area1))));
                string legend1 = "Graph1";
                Invoke((MethodInvoker)(() => chart1.Series.Add(legend1)));
                Invoke((MethodInvoker)(() => chart1.Series[legend1].ChartType = SeriesChartType.Column));

                for (int i = 0; i < Size; i++)
                {
                    int x = rnd.Next(1, 2000);
                    Invoke((MethodInvoker)(() => chart1.Series[legend1].Points.AddY(x)));
                    RandomValues.Add(x);
                }
                Invoke((MethodInvoker)(() => button1.Enabled = true));
            }
            catch { Set(); };

        }
        private int Size = 200;
        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            try
            {
                Size = Convert.ToInt32(textBox1.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            Thread t = new Thread(new ThreadStart(Set));
            t.Start();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
           Thread t = new Thread(new ThreadStart(StartSort));
            t.Start();
        }
    }
}
