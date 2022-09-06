using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace _7
{
    public partial class Form1 : Form
    {
        Chart[] A;
        int[] aaa = new int[] { 0, 0, 0, 0, 0 };
 
        public Form1()
        {
            A = new Chart[5];
            InitializeComponent();
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "txt files|*.txt";
            openFileDialog1.Title = "Открыть документ с данными";
            openFileDialog1.Multiselect = false;
            openFileDialog1.InitialDirectory = @"C:\Users\pvlkz\Downloads\практика пп\7";

            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "txt files|*.txt";
            saveFileDialog1.Title = "Сохранить документ с данными";
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = new Chart("", 0);
            }

        }
       
        static public double[] gol = new double[100];
        static public string[] sber = new string[100];
        static public int x;
        static public int[] y = new int [100];
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPage2_Paint(object sender, PaintEventArgs e)
        {
           
        }
        static public Double[] x2 = new Double[100];
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            
          
            double x,sum=0;            
            x = comboBox1.Items.Count;
            
            for (int о = 0; о < x; о++)
            {
                sber[о] = comboBox1.Items[о].ToString();
                if (comboBox1.Text == comboBox1.Items[о].ToString())
                {
                    gol[о]=gol[о] + 1 ;
                }    
                dataGridView1.Rows[о].Cells[0].Value = sber[о];
                dataGridView1.Rows[о].Cells[1].Value = gol[о];
               sum = sum + gol[о];
            }
            
            for (int о = 0; о < x; о++) {
                
                x2[о] = gol[о]/sum;
                
                dataGridView1.Rows[о].Cells[2].Value = Math.Round(x2[о]*100,2) + "%" ;
            }
            int i = comboBox1.SelectedIndex;
            aaa[i]++;
            A[i] = new Chart(comboBox1.SelectedItem.ToString(), aaa[i]);

            Diagram();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
           dataGridView1.ColumnCount = 3;
            dataGridView1.RowCount = 5;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Name = "Название";
            dataGridView1.Columns[1].Name = "Кол-во голосов:";
            dataGridView1.Columns[2].Name = "В процентах:";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 1)
                {
                    Chart.procent(A);
                    Chart.Show(A, dataGridView1);
                }
                if (tabControl1.SelectedIndex == 2)

                {
                    Diagram();
                }
            }
            catch
            {
                MessageBox.Show("Данные введены не полностью");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Chart.SaveToFile("Частота вирусных атак", A, saveFileDialog1.FileName);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Chart.OpenFile("Частота вирусных атак", ref A, openFileDialog1.FileName);
                comboBox1.Items.Clear();
                for (int i = 0; i < 5; i++)
                {
                    comboBox1.Items.Add(A[i].Nazv);
                }

                Chart.Show(A, dataGridView1);
            }
           

        }
        /// <summary>
        /// 
        /// </summary>
        private void Diagram()// метод рисует круговуюдиаграмму
        {
            chart1.Series.Clear();
            chart1.BackColor = Color.Gray;
            chart1.BackSecondaryColor = Color.WhiteSmoke;
            chart1.BackGradientStyle = GradientStyle.DiagonalRight;
            chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            chart1.BorderlineColor = Color.Gray;
            chart1.BorderSkin.SkinStyle = BorderSkinStyle.None;
            chart1.ChartAreas[0].BackColor = Color.OliveDrab;
            chart1.Titles.Clear();
            chart1.Titles.Add(label1.Text);
            chart1.Titles[0].Font = new Font("Utopia", 16);
            chart1.Series.Add(new Series("ColumnSeries") { ChartType = SeriesChartType.Pie });
            string[] xValues = new string[A.Length];
            double[] yValues = new double[A.Length];
            for (int i = 0; i < A.Length; i++)
            {
                xValues[i] = A[i].Nazv;
                yValues[i] = A[i].Prec;
            }
            chart1.Series["ColumnSeries"].Points.DataBindXY(xValues, yValues);
            chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
