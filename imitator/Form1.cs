using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TextBox = System.Windows.Forms.TextBox;

namespace imitator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            OutputView.DefaultCellStyle.Format = "0.###E+0";
            InputView.DefaultCellStyle.Format = "0.###E+0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var bs = InputView.DataSource;
            var sourse=bs as BindingSource;
            if (sourse != null)
            {
                var type = sourse.DataSource.GetType().ToString();

                switch (type)
                {
                    case "System.Collections.Generic.List`1[imitator.Imit42+InputData]":
                        var i42 = sourse.DataSource as List<Imit42.InputData>;
                        var o42 = from inputItem in i42 select Imit42.Exec(inputItem);
                        var s42 = new BindingSource {DataSource = o42};
                        OutputView.DataSource = s42;
                        break;
                    case "System.Collections.Generic.List`1[imitator.Imit43+InputData]":
                        var i43 = sourse.DataSource as List<Imit43.InputData>;
                        var o43 = Imit43.Exec(i43);
                        var s43 = new BindingSource {DataSource = o43};
                        OutputView.DataSource = s43;
                        break;
                    case "System.Collections.Generic.List`1[imitator.Imit44+InputData]":
                        var i44 = sourse.DataSource as List<Imit44.InputData>;
                        var o44 = from inputItem in i44 select Imit44.Exec(inputItem);
                        var s44 = new BindingSource {DataSource = o44};
                        OutputView.DataSource = s44;
                        break;
                    case "System.Collections.Generic.List`1[imitator.Imit46+InputData]":
                        var i46 = sourse.DataSource as List<Imit46.InputData>;
                        var o46 = Imit46.Exec(i46);
                        var s46 = new BindingSource { DataSource = o46 };
                        OutputView.DataSource = s46;
                        break;
                    case "System.Collections.Generic.List`1[imitator.imit42_43+InputData]":
                        var i4243 = sourse.DataSource as List<Imit42.InputData>;
                        var o4243 = Imit42_43.Exec(i4243);
                        var s4243 = new BindingSource {DataSource = o4243};
                        OutputView.DataSource = s4243;
                        break;
                    case "System.Collections.Generic.List`1[imitator.Imit44_46+InputData]":
                        var i4446 = sourse.DataSource as List<Imit44_46.InputData>;
                        var o4446 = Imit44_46.Exec(i4446);
                        var s4446 = new BindingSource {DataSource = o4446};
                        OutputView.DataSource = s4446;
                        break;
                    case "System.Collections.Generic.List`1[imitator.Imit42_46+InputData]":
                        var i4246 = sourse.DataSource as List<Imit42_46.InputData>;
                        var o4246 = Imit42_46.Exec(i4246.First());
                        var s4246 = new BindingSource { DataSource = o4246 };
                        OutputView.DataSource = s4246;
                        break;

                }
                ImitTabCtrl.SelectTab(1);;
            }
        }

        private void FillDefaults44()
        {
            var source = InputView.DataSource as BindingSource;
            if (source == null)
            {
                return;
            }
            var items = source.DataSource as List<Imit44.InputData>;
            if (items == null)
            {
                return;
            }
            var firstItem = !items.Any() ? new Imit44.InputData() { H = 60000, NeKrit = 1000000000000, NuKrit = 100000000, V = 6000, Type = 1, SubType = 1 } : items.First();

            for (int i = 0; i < 20; i++)
            {
                var index = (Imit44.InputData) source.AddNew();

                index.H = firstItem.H;
                index.NeKrit = firstItem.NeKrit;
                index.NuKrit = firstItem.NuKrit;
                index.V = firstItem.V;
                index.Type = firstItem.Type;
                index.SubType = firstItem.SubType;

                double k = i;

                index.Xkn = 5 + (k) * 10 / Math.Cos(0.2);
                index.Xkc = index.Xkn + 5 / Math.Cos(0.2);
                index.Xkk = index.Xkc + 5 / Math.Cos(0.2);
            }
        }

        private void FillDefaults44_46()
        {
            var source = InputView.DataSource as BindingSource;
            if (source == null)
            {
                return;
            }
            var items = source.DataSource as List<Imit44_46.InputData>;
            if (items == null)
            {
                return;
            }
            var firstItem = !items.Any() ? new Imit44_46.InputData() { H = 40000, NeKrit = 1e13, NuKrit = 100000000, V = 6000, Type = 1, SubType = 1 , angle = 0.2} : items.First();
            for (int i = 0; i < 20; i++)
            {
                var index = (Imit44_46.InputData)source.AddNew();

                index.H = firstItem.H;
                index.NeKrit = firstItem.NeKrit;
                index.NuKrit = firstItem.NuKrit;
                index.V = firstItem.V;
                index.Type = firstItem.Type;
                index.SubType = firstItem.SubType;
                index.angle = firstItem.angle;
                double k = i;

                index.Xkn = 5 + (k) * 10 / Math.Cos(0.2);
                index.Xkc = index.Xkn + 5 / Math.Cos(0.2);
                index.Xkk = index.Xkc + 5 / Math.Cos(0.2);
            }

        }

        private void FillDefaults42()
        {
            var source=InputView.DataSource as BindingSource;
            if (source == null)
            {
                return;
            }
            var items = source.DataSource as List<Imit42.InputData>;
            if (items == null)
            {
                return;
            }
            var firstItem = !items.Any() ? new Imit42.InputData() { Angle = 0.1, Fi = 0.5, H = 70000, Rzatup = 0.1, V = 6600,Type = 1,SubType = 1} : items.First();
            
            var dots = new Imit42.InputData[19];
            double h = firstItem.H;
            double alfa = 0.8 / (2 * Math.Sin(firstItem.Fi));

            for (int m = 1; m < 20; m++)
            {
                double v = firstItem.V * Math.Exp((-alfa) * (Math.Exp(-(1.5e-4) * h)));
                double s = Math.Sin(firstItem.Fi);
                h = h - v * s;

                dots[m - 1] = new Imit42.InputData() { Rzatup = firstItem.Rzatup, V = v, Angle = firstItem.Angle, H = h, Fi = firstItem.Fi };
            }
            foreach (var dot in dots)
            {
                source.Add(dot);
            }
            InputView.Refresh();
        }
        
        private void FillDefaults46()
        {
           
        }

        private void FillDefaults43()
        {
        }

        private void FillDefaults33()
        {
        }
        
        private void TabCntrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabCntrl.SelectedIndex == 7)
            {
                var i = 0;
                var types = new ObservableCollection<int>();
                foreach (var dot in Const.Objects)
                {
                    types.Add(i++);
                }
                AimTypeComboBox.DataSource = types;

                K1.Text = Const.K1.ToString();
                K2.Text = Const.K2.ToString();
                K3.Text = Const.K3.ToString();
                K4.Text = Const.K4.ToString();

                Ksv2.Text = Const.Ksv2.ToString();
                Ksv3.Text = Const.Ksv3.ToString();

                K0.Text = Const.K0.ToString();
            }
        }

        private void AimTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var box = sender as ComboBox;

            if (box != null)
            {
                var obj = Const.Objects[box.SelectedIndex];
                var i = 0;
                var subTypes = new ObservableCollection<int>();
                foreach (var dot in obj.ShineDots)
                {
                    subTypes.Add(i++);
                }

                AimSubtypeComboBox.DataSource = subTypes;
            }
        }
        
        private void AimSubtypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConstantsView.AutoGenerateColumns = true;
            ConstantsView.DataSource =Const.GetFlyingObject(AimTypeComboBox.SelectedIndex+1,AimSubtypeComboBox.SelectedIndex+1).ShineDots;
            ConstantsView.Refresh();
        }

        private void K_TextChanged(object sender, EventArgs e)
        {
            var tBox = sender as TextBox;
            double val;
            if (tBox != null && double.TryParse(tBox.Text, out val))
            {
                switch ( tBox.Name)
                {
                    case "K0":
                        Const.K0 = val;
                        break;
                    case "K1":
                        Const.K1 = val;
                        break;
                    case "K2":
                        Const.K2 = val;
                        break;
                    case "K3":
                        Const.K3 = val;
                        break;
                    case "K4":
                        Const.K4 = val;
                        break;
                    case "Ksv2":
                        Const.Ksv2 = val;
                        break;
                    case "Ksv3":
                        Const.Ksv3 = val;
                        break;
                }
                tBox.BackColor = Color.White;
            }
            else
            {
                if (tBox != null) tBox.BackColor = Color.Red;
            }
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var source = new BindingSource();
            switch ((string)ImitTypeComboBox.SelectedItem)
            {
                case "33":
                    var list33 = new List<Imit33.InputData> { };
                    source.DataSource = list33;
                    InputView.DataSource = source;
                    break;
                case "42":
                    var list42 = new List<Imit42.InputData> {};
                    source.DataSource = list42;
                    InputView.DataSource = source;
                    break;
                case "43":
                    var list43 = new List<Imit43.InputData> { };
                    source.DataSource = list43;
                    InputView.DataSource = source;
                    break;
                case "44":
                    var list44 = new List<Imit44.InputData> { };
                    source.DataSource = list44;
                    InputView.DataSource = source;
                    break;
                case "46":
                    var list46 = new List<Imit46.InputData> { };
                    source.DataSource = list46;
                    InputView.DataSource = source;
                    break;
                case "42+43":
                    var list4243 = new List<Imit42.InputData> { };
                    source.DataSource = list4243;
                    InputView.DataSource = source;
                    break;
                case "44+46":
                    var list4446 = new List<Imit44_46.InputData> { };
                    source.DataSource = list4446;
                    InputView.DataSource = source;
                    break;
                case "42-46":
                    var list4246 = new List<Imit42_46.InputData> { };
                    source.DataSource = list4246;
                    InputView.DataSource = source;
                    break;
            }
        }

        private void Fill_Click(object sender, EventArgs e)
        {
            switch ((string)ImitTypeComboBox.SelectedItem)
            {
                case "33":
                    FillDefaults33();
                    break;
                case "42":
                    FillDefaults42();
                    break;
                case "43":
                    FillDefaults43();
                    break;
                case "44":
                    FillDefaults44();
                    break;
                case "46":
                    FillDefaults46();
                    break;
                case "42+43":
                    //FillDefaults42_43();
                    break;
                case "44+46":
                    FillDefaults44_46();
                    break;
            }
        }


        
    }
}
