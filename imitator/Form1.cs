using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace imitator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            InputGrid33.Rows[e.RowIndex].ErrorText = "";
            double newDouble;

            if (InputGrid33.Rows[e.RowIndex].IsNewRow) { return; }
            if (double.TryParse(e.FormattedValue.ToString(),
                out newDouble) || string.IsNullOrWhiteSpace(e.FormattedValue.ToString())) return;
            e.Cancel = true;
            InputGrid33.Rows[e.RowIndex].ErrorText = "Необходимо ввести число.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            if (tabControl.SelectedIndex == 0)
            {
                var dotList = new ShineDot[InputGrid33.Rows.Count - 1];
                string[] dataRow = new string[14];

                for (int rows = 0; rows < InputGrid33.Rows.Count - 1; rows++)
                {
                    for (int col = 0; col < 14; col++)
                    {
                        if (InputGrid33.Rows[rows].Cells[col].Value == null)
                            dataRow[col] = "0";
                        else
                            dataRow[col] = InputGrid33.Rows[rows].Cells[col].Value.ToString();
                    }
                    dotList[rows] = toShineDot(dataRow);
                }



                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                double[,] OutArr = Imitator.StartWork(dotList);

                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value. 
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds/10);
                TimeLable.Text = elapsedTime;

                Bind33(OutArr);

                tabCntrl33.SelectTab(1);
            }
            else
            {
                var dotList = new Imit42.InputData[inputGrid42.Rows.Count - 1];
                string[] dataRow = new string[4];

                for (int rows = 0; rows < inputGrid42.Rows.Count - 1; rows++)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        if (inputGrid42.Rows[rows].Cells[col].Value == null)
                            dataRow[col] = "0";
                        else
                            dataRow[col] = inputGrid42.Rows[rows].Cells[col].Value.ToString();
                    }
                    dotList[rows] = toDataPack(dataRow);
                }

                double[,] OutArr = new double[dotList.Count(),5];

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                for (int i = 0; i < dotList.Count(); i++)
                {
                   double[] d=toDouble(Imit42.GeneralOperator(dotList[i]));
                    for (int j = 0; j < d.Length; j++)
                    {
                        OutArr[i, j] = d[j];
                    }
                }
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value. 
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                TimeLable.Text = elapsedTime;

                Bind42(OutArr);

                TabCntrl42.SelectTab(1);
            }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double[] toDouble(Imit42.OutputData data)
        {
            return new double[] { data.A,data.B,data.Delta,data.Ne,data.Nu};
        }

        private void Bind33(double[,] arrDoubles)
        {
            OutputGrid33.Columns.Clear();
            for (int i = 0; i < 180; i++)
            {
                OutputGrid33.Columns.Add(i.ToString(), i.ToString());
            }

            for (int j = 0; j < 32; j++)
            {
                DataGridViewRow row = new DataGridViewRow();

                for (int i = 0; i < 180; i++)
                {
                    
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value =Math.Round(arrDoubles[i, j],5).ToString();
                    row.Cells.Add(cell);
                }
                OutputGrid33.Rows.Add(row);
            }
        }
        private void Bind42(double[,] arrDoubles)
        {
            outputGrid42.Rows.Clear();

            for (int j = 0; j < arrDoubles.GetLength(0); j++)
            {
                DataGridViewRow row = new DataGridViewRow();

                for (int i = 0; i < arrDoubles.GetLength(1); i++)
                {

                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = arrDoubles[j, i].ToString();
                    row.Cells.Add(cell);
                }
                outputGrid42.Rows.Add(row);
            }
        }
        private ShineDot toShineDot(string[] data)
        {
            return new ShineDot()
            {
                Kf =Int32.Parse(data[0]),
                Xc = double.Parse(data[1]),
                Yc = double.Parse(data[2]),
                Omin = double.Parse(data[3]),
                Omax = double.Parse(data[4]),
                Rcf = double.Parse(data[5]),
                D1 = double.Parse(data[6]),
                D2 = double.Parse(data[7]),
                L = double.Parse(data[8]),
                Gamma = double.Parse(data[9]),
                D = double.Parse(data[10]),
                Kiz = double.Parse(data[11]),
                A = double.Parse(data[12]),
                C = double.Parse(data[13])
            };
        }
        private Imit42.InputData toDataPack(string[] data)
        {
            return new Imit42.InputData()
            {
                Rzatup = double.Parse(data[0]),
                V = double.Parse(data[1]),
                H = double.Parse(data[2]),
                Angle = double.Parse(data[3])
            };
        }
        

        
    }
}
