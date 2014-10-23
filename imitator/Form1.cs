using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
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

            if (InputGrid33.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
            if (double.TryParse(e.FormattedValue.ToString(),
                out newDouble) || string.IsNullOrWhiteSpace(e.FormattedValue.ToString())) return;
            e.Cancel = true;
            InputGrid33.Rows[e.RowIndex].ErrorText = "Необходимо ввести число.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                if (TabCntrl.SelectedIndex == 0)
                {
                    ShineDot[] dotList = new ShineDot[InputGrid33.Rows.Count - 1];
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

                    double[,] OutArr = Imit33.StartWork(dotList);

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
                else if (TabCntrl.SelectedIndex == 1)
                {
                    Imit42.InputData[] dotList = new Imit42.InputData[inputGrid42.Rows.Count - 1];
                    string[] dataRow = new string[5];

                    for (int rows = 0; rows < inputGrid42.Rows.Count - 1; rows++)
                    {
                        for (int col = 0; col < 5; col++)
                        {
                            if (inputGrid42.Rows[rows].Cells[col].Value == null)
                                dataRow[col] = "0";
                            else
                                dataRow[col] = inputGrid42.Rows[rows].Cells[col].Value.ToString();
                        }
                        dotList[rows] = toDataPack42(dataRow);
                    }

                    double[,] OutArr = new double[dotList.Count(), 8];

                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    for (int i = 0; i < dotList.Count(); i++)
                    {
                        double[] d = toDouble(Imit42.GeneralOperator(dotList[i]));
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
                        ts.Milliseconds/10);
                    TimeLable.Text = elapsedTime;

                    Bind42(OutArr);

                    TabCntrl42.SelectTab(1);
                }
                else if (TabCntrl.SelectedIndex == 2)
                {
                    Imit43.InputData[] dotList = new Imit43.InputData[inputGrid43.Rows.Count - 1];
                    string[] dataRow = new string[inputGrid43.Rows[0].Cells.Count];

                    for (int rows = 0; rows < inputGrid43.Rows.Count - 1; rows++)
                    {
                        for (int col = 0; col < inputGrid43.Rows[0].Cells.Count; col++)
                        {
                            if (inputGrid43.Rows[rows].Cells[col].Value == null)
                                dataRow[col] = "0";
                            else
                                dataRow[col] = inputGrid43.Rows[rows].Cells[col].Value.ToString();
                        }
                        dotList[rows] = toDataPack43(dataRow);
                    }

                    List<double[]> OutArr = new List<double[]>();

                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();

                    OutArr = Imit43.GeneralOperator(dotList);

                    stopWatch.Stop();

                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value. 
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds/10);
                    TimeLable.Text = elapsedTime;

                    Bind43(OutArr);

                    TabCntrl43.SelectTab(1);
                }
                else if (TabCntrl.SelectedIndex == 3)
                {
                    imit42_43.InputData[] dots = new imit42_43.InputData[input42_43.RowCount - 1];
                    string[] dataRow1 = new string[input42_43.ColumnCount];

                    for (int row = 0; row < input42_43.Rows.Count - 1; row++)
                    {
                        for (int col = 0; col < input42_43.ColumnCount; col++)
                        {
                            if (input42_43.Rows[row].Cells[col].Value == null)
                                dataRow1[col] = "0";
                            else
                                dataRow1[col] = input42_43.Rows[row].Cells[col].Value.ToString();
                        }

                        dots[row] = toDataPack42_43(dataRow1);
                    }

                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();

                    List<double[]> OutArr1 = imit42_43.GeneralOperator(dots);
                   
                    stopWatch.Stop();

                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value. 
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    TimeLable.Text = elapsedTime;

                    Bind42_43(OutArr1);

                    tabControl42_43.SelectTab(1);
                }
                else if (TabCntrl.SelectedIndex == 4)
                {
                    Imit44.InputData[] dotList = new Imit44.InputData[inputGrid44.Rows.Count - 1];
                    string[] dataRow = new string[inputGrid44.Rows[0].Cells.Count];

                    for (int row = 0; row < inputGrid44.Rows.Count - 1; row++)
                    {
                        for (int col = 0; col < 8; col++)
                        {
                            if (inputGrid44.Rows[row].Cells[col].Value == null)
                                dataRow[col] = "0";
                            else
                                dataRow[col] = inputGrid44.Rows[row].Cells[col].Value.ToString();
                        }
                        dotList[row] = toDataPack44(dataRow);
                    }

                    double[,] OutArr = new double[dotList.Count(), 15];

                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();

                    for (int i = 0; i < dotList.Count(); i++)
                    {
                        var Out = Imit44.GeneralOperator(dotList[i]);
                        //addToInput46(Out, dotList[i]);
                        var outData = toDouble(Out);
                        for (int j = 0; j < 15; j++)
                        {
                            OutArr[i, j] = outData[j];
                        }
                    }
                    stopWatch.Stop();

                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value. 
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    TimeLable.Text = elapsedTime;

                    Bind44(OutArr);

                    TabCntrl44.SelectTab(1);
                }
                else if (TabCntrl.SelectedIndex == 5)
                {
                    var dotList = new Imit46.InputData[inputGrid46.Rows.Count - 1];
                    string[] dataRow = new string[inputGrid46.Rows[0].Cells.Count];

                    for (int row = 0; row < inputGrid46.Rows.Count - 1; row++)
                    {
                        for (int col = 0; col < inputGrid46.ColumnCount; col++)
                        {
                            if (inputGrid46.Rows[row].Cells[col].Value == null)
                                dataRow[col] = "0";
                            else
                                dataRow[col] = inputGrid46.Rows[row].Cells[col].Value.ToString();
                        }
                        dotList[row] = toDataPack46(dataRow);
                    }

                    var OutArr = new String[dotList.Count(), 3];

                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();


                    Imit46.OutputData[] outData = Imit46.GeneralOperator(dotList);

                    for (int i = 0; i < dotList.Count(); i++)
                    {
                        var outStr= toString(outData[i]);
                        for (int j = 0; j < outStr.Count(); j++)
                        {
                            OutArr[i, j] = outStr[j];
                        }
                    }
                    stopWatch.Stop();

                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value. 
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    TimeLable.Text = elapsedTime;

                    Bind46(OutArr);

                    TabCntrl46.SelectTab(1);
                }
                else if (TabCntrl.SelectedIndex == 6)
                {
                    Imit44_46.InputData[] dotList = new Imit44_46.InputData[inputGrid44_46.Rows.Count - 1];
                    string[] dataRow = new string[inputGrid44_46.Rows[0].Cells.Count];

                    for (int row = 0; row < inputGrid44_46.Rows.Count - 1; row++)
                    {
                        for (int col = 0; col < 9; col++)
                        {
                            if (inputGrid44_46.Rows[row].Cells[col].Value == null)
                                dataRow[col] = "0";
                            else
                                dataRow[col] = inputGrid44_46.Rows[row].Cells[col].Value.ToString();
                        }
                        dotList[row] = toDataPack44_46(dataRow);
                    }

                    var OutArr = new String[dotList.Count(), 3];

                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();

                    var Out = Imit44_46.GeneralOperator(dotList);

                    for (int i = 0; i < dotList.Count(); i++)
                    {
                        var outStr = toString(Out[i]);
                        for (int j = 0; j < outStr.Count(); j++)
                        {
                            OutArr[i, j] = outStr[j];
                        }
                    }

                    stopWatch.Stop();

                    TimeSpan ts = stopWatch.Elapsed;

                    // Format and display the TimeSpan value. 
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    TimeLable.Text = elapsedTime;

                    Bind44_46(OutArr);

                    TabCntrl44_46.SelectTab(1);
                }


           // }
           // catch (Exception exception)
          //  {
          //      MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
           // }

        }

        private Imit43.InputData[] to43InputData(double[,] outArr1, DataGridViewCellCollection cells, Imit42.InputData[] dots)
        {
            var inp = new Imit43.InputData[20];

            foreach (DataGridViewCell cell in cells)
            {
                if (cell.Value==null)
                {
                    cell.Value = "0";
                }
            }
            for (int i = 0; i < 20; i++)
            {
                inp[i] = new Imit43.InputData() { Ageo = outArr1[0, 0],
                                                  Bgeo = outArr1[i, 1], 
                                                  Delta = outArr1[i, 2], 
                                                  Ne = outArr1[i, 3], 
                                                  Nu = outArr1[i, 4], 
                                                  NeKrit = outArr1[i, 5],
                                                  Kf =int.Parse( cells[0].Value.ToString()),
                                                  Xc = double.Parse(cells[1].Value.ToString()),
                                                  Yc = double.Parse(cells[2].Value.ToString()),
                                                  Omin = double.Parse(cells[3].Value.ToString()),
                                                  Omax = double.Parse(cells[4].Value.ToString()),
                                                  Rzatup = double.Parse(cells[5].Value.ToString()),
                                                  D1 = double.Parse(cells[6].Value.ToString()),
                                                  D2 = double.Parse(cells[7].Value.ToString()),
                                                  L = double.Parse(cells[8].Value.ToString()),
                                                  Gamma = double.Parse(cells[9].Value.ToString()),
                                                  D = double.Parse(cells[10].Value.ToString()),
                                                  Kiz = double.Parse(cells[11].Value.ToString()),
                                                  A = double.Parse(cells[12].Value.ToString()),
                                                  C = double.Parse(cells[13].Value.ToString()),
                                                  H = dots[i].H,
                                                  V = dots[i].V,
                                                  Angle = 0.2

                };
               
            }
            return inp;
        }

        private double[] toDouble(Imit42.OutputData data)
        {
            return new double[] { data.A,data.B,data.Delta,data.Ne,data.Nu,data.NeKrit,data.NuKrit,data.M};
        }
        private double[] toDouble(Imit44.OutputData data)
        {
            return new double[] { data.NeXkc, data.NeXkk, data.NeXkn, 
                data.NuXkc, data.NuXkk, data.NuXkn, 
                data.VXkc, data.VXkk, data.VXkn, 
                data.dXkc, data.dXkk, data.dXkn,
            data.Xkp,data.Xp,data.Hturb
            };
        }

        private void addToInput46(Imit44.OutputData data, Imit44.InputData dotList)
        {
            DataGridViewRow row = new DataGridViewRow();
            // rz v h angle fi
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = dotList.H });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = dotList.V });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.dXkn });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.dXkc});
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.dXkk});

            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.VXkn });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.VXkc });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.VXkk });

            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.NeXkn });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.NeXkc });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.NeXkk });

            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.NuXkn });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.NuXkc });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.NuXkk });

            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.Xp });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.Xkp });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = "0,2" });

            row.Cells.Add(new DataGridViewTextBoxCell() { Value = data.Hturb });

            row.Cells.Add(new DataGridViewTextBoxCell() { Value = dotList.Xkn });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = dotList.Xkc });
            row.Cells.Add(new DataGridViewTextBoxCell() { Value = dotList.Xkk });

            inputGrid46.Rows.Add(row);
        }

        private string[] toString(Imit46.OutputData data)
        {
            return new[] { data.Sk.ToString("0.###E+0", CultureInfo.InvariantCulture), data.Ssps.ToString("0.###E+0", CultureInfo.InvariantCulture), toOneValue(data.Skj)
            };
        }

        private string toOneValue(Imit46.VSpair[] doubles)
        {
            if (doubles!=null)
            {
                string s = "";
                s = doubles.Aggregate(s, (current, d) => 
                    current + (d.S.ToString("0.###E+0", CultureInfo.InvariantCulture) + string.Format("({0}), ", d.V.ToString("0.###E+0", CultureInfo.InvariantCulture))));
                return s;
            }
            return "";
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
                    cell.Value = arrDoubles[i, j].ToString("0.###E+0", CultureInfo.InvariantCulture);
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
                    cell.Value = arrDoubles[j, i].ToString("0.###E+0", CultureInfo.InvariantCulture);
                    row.Cells.Add(cell);
                }
                outputGrid42.Rows.Add(row);
            }
        }
        private void Bind43(List<double[]> arrDoubles)
        {
            outputGrid43.Columns.Clear();
            outputGrid43.Rows.Clear();

            outputGrid43.Columns.Add("col","S sum");
            foreach (var array in arrDoubles)
            {
                if (array.Length>1)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        outputGrid43.Columns.Add("col", "S "+i);
                    }
                    break;
                }
            }

            for (int j = 0; j < arrDoubles.Count; j++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int i = 0;
                if (arrDoubles[j].Length > 1)
                    i++;
                for (; i < arrDoubles[j].Length; i++)
                {
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = arrDoubles[j][i].ToString("0.###E+0", CultureInfo.InvariantCulture);
                    row.Cells.Add(cell);
                }
                outputGrid43.Rows.Add(row);
            }
        }
        private void Bind44(double[,] arrDoubles)
        {
            outputGrid44.Columns.Clear();
            outputGrid44.Rows.Clear();

            outputGrid44.Columns.Add("col1", "NeXkc");
            outputGrid44.Columns.Add("col2", "NeXkk");
            outputGrid44.Columns.Add("col3", "NeXkn");
            outputGrid44.Columns.Add("col4", "NuXkc");
            outputGrid44.Columns.Add("col5", "NuXkk");
            outputGrid44.Columns.Add("col6", "NuXkn");
            outputGrid44.Columns.Add("col7", "VXkc");
            outputGrid44.Columns.Add("col8", "VXkk");
            outputGrid44.Columns.Add("col9", "VXkn");
            outputGrid44.Columns.Add("col10", "dXkc");
            outputGrid44.Columns.Add("col11", "dXkk");
            outputGrid44.Columns.Add("col12", "dXkn");
            outputGrid44.Columns.Add("col13", "Xkp");
            outputGrid44.Columns.Add("col14", "Xp");
            outputGrid44.Columns.Add("col15", "Hturb");

            for (int j = 0; j < arrDoubles.GetLength(0); j++)
            {
                DataGridViewRow row = new DataGridViewRow();

                for (int i = 0; i < arrDoubles.GetLength(1); i++)
                {

                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = arrDoubles[j, i].ToString("0.###E+0", CultureInfo.InvariantCulture);
                    row.Cells.Add(cell);
                }
                outputGrid44.Rows.Add(row);
            }
        }
        private void Bind46(string[,] arr)
        {
            outputGrid46.Columns.Clear();
            outputGrid46.Rows.Clear();

            outputGrid46.Columns.Add("col1", "Sk");
            outputGrid46.Columns.Add("col2", "Ssps");
            outputGrid46.Columns.Add("col6", "Skj");
            
            for (int j = 0; j < arr.GetLength(0); j++)
            {
                DataGridViewRow row = new DataGridViewRow();

                for (int i = 0; i < 3; i++)
                {

                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = arr[j, i];
                    row.Cells.Add(cell);
                }
                outputGrid46.Rows.Add(row);
            }
        }
        private void Bind44_46(string[,] arr)
        {
            outputGrid44_46.Columns.Clear();
            outputGrid44_46.Rows.Clear();

            outputGrid44_46.Columns.Add("col1", "Sk");
            outputGrid44_46.Columns.Add("col2", "Ssps");
            outputGrid44_46.Columns.Add("col6", "Skj");

            for (int j = 0; j < arr.GetLength(0); j++)
            {
                DataGridViewRow row = new DataGridViewRow();

                for (int i = 0; i < 3; i++)
                {

                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = arr[j, i];
                    row.Cells.Add(cell);
                }
                outputGrid44_46.Rows.Add(row);
            }
        }
        private void FillDefaults44()
        {
            double H = 60000;
            if (inputGrid44.Rows[0].Cells[0].Value!=null)
            {
                H = double.TryParse(inputGrid44.Rows[0].Cells[0].Value.ToString(), out H) ? H : 60000;
            }
            
            
            inputGrid44.Rows.Clear();

            double[,] x = new double[20,3];
            for (int i = 0; i < 20; i++)
            {
                double k = i + 1;

                x[i, 0] = (k - 1) * 10 / Math.Cos(0.2);
                x[i, 1] = x[i, 0] + 5 / Math.Cos(0.2);
                x[i, 2] = x[i, 1] + 5 / Math.Cos(0.2);
            }

            for (int j = 0; j < 20; j++)
            {
                DataGridViewRow row = new DataGridViewRow();
               // h ne nu ro v 
                row.Cells.Add(new DataGridViewTextBoxCell() {Value = H.ToString()});
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = "1000000000000" });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = "100000000" });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = 0.001.ToString()});
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = "6000" });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = x[j, 0].ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = x[j, 1].ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = x[j, 2].ToString() });
                inputGrid44.Rows.Add(row);
            }
        }

        private void FillDefaults44_46()
        {
            double H = 60000;
            if (inputGrid44_46.Rows[0].Cells[0].Value != null)
            {
                H = double.TryParse(inputGrid44_46.Rows[0].Cells[0].Value.ToString(), out H) ? H : 60000;
            }


            inputGrid44_46.Rows.Clear();

            double[,] x = new double[20, 3];
            for (int i = 0; i < 20; i++)
            {
                double k = i + 1;

                x[i, 0] = (k - 0.5) * 10 / Math.Cos(0.2);
                x[i, 1] = x[i, 0] + 5 / Math.Cos(0.2);
                x[i, 2] = x[i, 1] + 5 / Math.Cos(0.2);
            }

            for (int j = 0; j < 20; j++)
            {
                DataGridViewRow row = new DataGridViewRow();
                // h ne nu ro v 
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = H.ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = "1000000000000" });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = "100000000" });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = 0.001.ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = "6000" });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = x[j, 0].ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = x[j, 1].ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = x[j, 2].ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = "0,2" });
                inputGrid44_46.Rows.Add(row);
            }
        }

        private void FillDefaults42(Imit42.InputData[] data)
        {
            inputGrid42.Rows.Clear();
            
            for (int j = 0; j < 20; j++)
            {
                DataGridViewRow row = new DataGridViewRow();
                // rz v h angle fi
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].Rzatup });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].V });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].H });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].Angle });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].Fi });
                
                inputGrid42.Rows.Add(row);
            }
        }
        private void FillDefaults42_43(imit42_43.InputData[] data)
        {
            input42_43.Rows.Clear();

            for (int j = 0; j < data.Length; j++)
            {
                DataGridViewRow row = new DataGridViewRow();
                // kf xc yc omin omax r d1 d2 l gamma d kiz a c 
                // H V angle
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].Type });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].SubType });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].H });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].V });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].Angle });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].Fi });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = data[j].Rzatup });

                input42_43.Rows.Add(row);
            }
        }


        private void FillDefaults46()
        {
            inputGrid46.Rows.Clear();

            double[,] x = new double[20, 3];
            for (int i = 0; i < 20; i++)
            {
                double k = i + 1;

                x[i, 0] = (k - 1) * 10 / Math.Cos(0.2);
                x[i, 1] = x[i, 0] + 5 / Math.Cos(0.2);
                x[i, 2] = x[i, 1] + 5 / Math.Cos(0.2);
            }

            for (int j = 0; j < 20; j++)
            {
                DataGridViewRow row = new DataGridViewRow();
                // h ne nu ro v 
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = "60000" });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = "1000000000000" });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = "100000000" });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = 0.001.ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = "6000" });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = x[j, 0].ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = x[j, 1].ToString() });
                row.Cells.Add(new DataGridViewTextBoxCell() { Value = x[j, 2].ToString() });
                inputGrid44.Rows.Add(row);
            }
        }
        private void Bind42_43(List<double[]> arrDoubles)
        {
            out42_43.Columns.Clear();
            out42_43.Rows.Clear();

            foreach (var array in arrDoubles)
            {
                if (array.Length > 1)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        out42_43.Columns.Add("col", "S " + i);
                    }
                    break;
                }
            }

            out42_43.Columns.Add("col", "S sum");

            for (int j = 0; j < arrDoubles.Count; j++)
            {
                DataGridViewRow row = new DataGridViewRow();

                for (int i = 0; i < arrDoubles[j].Length; i++)
                {

                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = arrDoubles[j][i].ToString("0.###E+0", CultureInfo.InvariantCulture);
                    row.Cells.Add(cell);
                }
                out42_43.Rows.Add(row);
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
                Rzatup = double.Parse(data[5]),
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
        private Imit42.InputData toDataPack42(string[] data)
        {
            return new Imit42.InputData()
            {
                Rzatup = double.Parse(data[0]),
                V = double.Parse(data[1]),
                H = double.Parse(data[2]),
                Angle = double.Parse(data[3]),
                Fi = double.Parse(data[4])
            };
        }
        private imit42_43.InputData toDataPack42_43(string[] data)
        {
            return new imit42_43.InputData()
            {
                Type = int.Parse(data[0]),
                SubType = int.Parse(data[1]),
                V = double.Parse(data[3]),
                H = double.Parse(data[2]),
                Angle = double.Parse(data[4]),
                Fi = double.Parse(data[5]),
                Rzatup = double.Parse(data[6])
            };
        }
        private Imit42.InputData[] GetDotList(double Rz,double Angle, double H, double Fi, double V)
        {
            var dots = new Imit42.InputData[20];
            double h = H;
            double alfa = 0.8 / (2 * Math.Sin(Fi));

            for (int m = 1; m < 21; m++)
            {
                double v = V * Math.Exp((-alfa) * (Math.Exp(-(1.5e-4) * h)));
                double s = Math.Sin(Fi);
                h = h - v*s;
                
                dots[m-1]=new Imit42.InputData(){Rzatup = Rz, V = v,Angle = Angle,H = h,Fi = Fi};
            }
            return dots;
        }
        private imit42_43.InputData[] GetDotList(double Rz, double Angle, double H, double Fi, double V,int Type,int SubType)
        {
            var dots = new imit42_43.InputData[20];
            double alfa = 0.8/(2*Math.Sin(Fi));

            double h = H;
            for (int m = 1; m < 21; m++)
            {
                double v = V * Math.Exp((-alfa) * (Math.Exp(-(1.5e-4) * h)));
                double s = Math.Sin(Fi);
                h = h - v * s;

                dots[m - 1] = new imit42_43.InputData() { Rzatup = Rz, V = v, Angle = Angle, H = h, Fi = Fi,Type = Type,SubType = SubType};
            }
            return dots;
        }
        private Imit43.InputData toDataPack43(string[] data)
        {
            return new Imit43.InputData()
            {
                //Kf =Int32.Parse(data[0]),
                //Xc = double.Parse(data[1]),
                //Yc = double.Parse(data[2]),
                //Omin = double.Parse(data[3]),
                //Omax = double.Parse(data[4]),
                //Rcf = double.Parse(data[5]),
                //D1 = double.Parse(data[6]),
                //D2 = double.Parse(data[7]),
                //L = double.Parse(data[8]),
                //Gamma = double.Parse(data[9]),
                //D = double.Parse(data[10]),
                //Kiz = double.Parse(data[11]),
                //A = double.Parse(data[12]),
                //C = double.Parse(data[13]),
                Type = Int32.Parse(data[0]),
                SubTupe = Int32.Parse(data[1]),
                Ne = double.Parse(data[2]),
                Nu= double.Parse(data[3]),
                Delta = double.Parse(data[4]),
                Ageo = double.Parse(data[5]),
                Bgeo = double.Parse(data[6]),
                Angle = double.Parse(data[7]),
                V = double.Parse(data[8]),
                Rzatup = double.Parse(data[9]),
                H = double.Parse(data[10]),
                NeKrit = double.Parse(data[11])
            
            };
        }

        private Imit44.InputData toDataPack44(string[] data)
        {
            return new Imit44.InputData()
            {
                H = Int32.Parse(data[0]),
                NeKrit = double.Parse(data[1]),
                NuKrit = double.Parse(data[2]),
                Ro = double.Parse(data[3]),
                V = double.Parse(data[4]),
                Xkn = double.Parse(data[5]),
                Xkc = double.Parse(data[6]),
                Xkk = double.Parse(data[7])
            };
        }
        private Imit44_46.InputData toDataPack44_46(string[] data)
        {
            return new Imit44_46.InputData()
            {
                H = Int32.Parse(data[0]),
                NeKrit = double.Parse(data[1]),
                NuKrit = double.Parse(data[2]),
                Ro = double.Parse(data[3]),
                V = double.Parse(data[4]),
                Xkn = double.Parse(data[5]),
                Xkc = double.Parse(data[6]),
                Xkk = double.Parse(data[7]),
                angle = double.Parse(data[8])
            };
        }
        private Imit46.InputData toDataPack46(string[] data)
        {
            return new Imit46.InputData()
            {
                H = Int32.Parse(data[0]),
                V = double.Parse(data[1]),
                dXkn = double.Parse(data[2]),
                dXkc = double.Parse(data[3]),
                dXkk = double.Parse(data[4]),
                VXkn = double.Parse(data[5]),
                VXkc = double.Parse(data[6]),
                VXkk = double.Parse(data[7]),
                NeXkn = double.Parse(data[8]),
                NeXkc = double.Parse(data[9]),
                NeXkk = double.Parse(data[10]),
                NuXkn = double.Parse(data[11]),
                NuXkc = double.Parse(data[12]),
                NuXkk = double.Parse(data[13]),

                Xp = double.Parse(data[14]),
                Xkp = double.Parse(data[15]),
                Angle = double.Parse(data[16]),
                Hturb = double.Parse(data[17]),

                Xkn = double.Parse(data[18]),
                Xkc = double.Parse(data[19]),
                Xkk = double.Parse(data[20])
            };

        }
        private void button1_Click(object sender, EventArgs e)
        {
            FillDefaults44();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Imit42.InputData[] dotList = new Imit42.InputData[inputGrid42.Rows.Count - 1];
            string[] dataRow = new string[5];

            for (int rows = 0; rows < inputGrid42.Rows.Count - 1; rows++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (inputGrid42.Rows[rows].Cells[col].Value == null)
                        dataRow[col] = "0";
                    else
                        dataRow[col] = inputGrid42.Rows[rows].Cells[col].Value.ToString();
                }
                dotList[rows] = toDataPack42(dataRow);
            }
            if (!dotList.Any())
            {
                dotList=new Imit42.InputData[]{new Imit42.InputData(){Angle = 0.1,Fi = 0.5,H=70000, Rzatup = 0.1,V=6600} };
            }
            dotList = GetDotList(dotList[0].Rzatup, dotList[0].Angle, dotList[0].H, dotList[0].Fi, dotList[0].V);
            FillDefaults42(dotList);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            imit42_43.InputData[] dotList = new imit42_43.InputData[input42_43.Rows.Count - 1];
            string[] dataRow = new string[7];

            for (int rows = 0; rows < input42_43.Rows.Count - 1; rows++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (input42_43.Rows[rows].Cells[col].Value == null)
                        dataRow[col] = "0";
                    else
                        dataRow[col] = input42_43.Rows[rows].Cells[col].Value.ToString();
                }
                dotList[rows] = toDataPack42_43(dataRow);
            }
            if (!dotList.Any())
            {
                dotList = new imit42_43.InputData[] { new imit42_43.InputData() { Angle = 0.1, Fi = 0.5, H = 70000, Rzatup = 0.1, V = 6600,SubType = 1,Type = 1} };
            }
            dotList = GetDotList(dotList[0].Rzatup, dotList[0].Angle, dotList[0].H, dotList[0].Fi, dotList[0].V, dotList[0].Type, dotList[0].SubType);
            FillDefaults42_43(dotList);
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var name= saveFileDialog1.FileName;

                DataGridView bs=new DataGridView(); 

                if (TabCntrl.SelectedIndex == 0)
                {
                    bs = InputGrid33;
                }
                else if (TabCntrl.SelectedIndex == 1)
                {
                    bs = inputGrid42;
                }
                else if (TabCntrl.SelectedIndex == 2)
                {
                    bs = inputGrid43;
                }
                else if (TabCntrl.SelectedIndex == 3)
                {
                    bs = input42_43;
                }
                else if (TabCntrl.SelectedIndex == 4)
                {
                    bs = inputGrid44;
                }
                else if (TabCntrl.SelectedIndex == 5)
                {
                    bs = inputGrid46;
                }

                DataTable dt = new DataTable();
                for (int i = 1; i < bs.Columns.Count + 1; i++)
                {
                    DataColumn column = new DataColumn(bs.Columns[i - 1].HeaderText);
                    dt.Columns.Add(column);
                }
                int ColumnCount = bs.Columns.Count;
                foreach (DataGridViewRow dr in bs.Rows)
                {
                    DataRow dataRow = dt.NewRow();
                    for (int i = 0; i < ColumnCount; i++)
                    {
                        dataRow[i] = dr.Cells[i].Value;
                    }
                    dt.Rows.Add(dataRow);
                }
                
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables[0].WriteXml(name); 
            }

            
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var name = openFileDialog1.FileName;

                DataGridView bs = new DataGridView();

                if (TabCntrl.SelectedIndex == 0)
                {
                    bs = InputGrid33;
                }
                else if (TabCntrl.SelectedIndex == 1)
                {
                    bs = inputGrid42;
                }
                else if (TabCntrl.SelectedIndex == 2)
                {
                    bs = inputGrid43;
                }
                else if (TabCntrl.SelectedIndex == 3)
                {
                    bs = input42_43;
                }
                else if (TabCntrl.SelectedIndex == 4)
                {
                    bs = inputGrid44;
                }
                else if (TabCntrl.SelectedIndex == 5)
                {
                    bs = inputGrid46;
                }

                DataTable dt = new DataTable();
                for (int i = 1; i < bs.Columns.Count + 1; i++)
                {
                    DataColumn column = new DataColumn(bs.Columns[i - 1].HeaderText);
                    dt.Columns.Add(column);
                }
                int ColumnCount = bs.Columns.Count;
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables[0].ReadXml(name);
                ds.Tables[0].Rows.RemoveAt(ds.Tables[0].Rows.Count-1);
                bs.DataSource = null;

                bs.Rows.Clear();
                bs.Columns.Clear();

                bs.DataSource = ds.Tables[0];

            }
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            DataGridView bs = new DataGridView();

            if (TabCntrl.SelectedIndex == 0)
            {
                bs = InputGrid33;
            }
            else if (TabCntrl.SelectedIndex == 1)
            {
                bs = inputGrid42;
            }
            else if (TabCntrl.SelectedIndex == 2)
            {
                bs = inputGrid43;
            }
            else if (TabCntrl.SelectedIndex == 3)
            {
                bs = input42_43;
            }
            else if (TabCntrl.SelectedIndex == 4)
            {
                bs = inputGrid44;
            }
            else if (TabCntrl.SelectedIndex == 5)
            {
                bs = inputGrid46;
            }
            else if (TabCntrl.SelectedIndex == 6)
            {
                bs = inputGrid44_46;
            }
            foreach (DataGridViewRow item in bs.SelectedRows)
             {
                 try
                 {
                     bs.Rows.RemoveAt(item.Index);
                 }
                 catch{}
                
             }
 
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            FillDefaults44_46();
        }

        #region Копирование/вставка ячеек
        
        #endregion

    }
}
