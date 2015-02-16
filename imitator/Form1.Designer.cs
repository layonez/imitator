namespace imitator
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TabCntrl = new System.Windows.Forms.TabControl();
            this.ImitPage = new System.Windows.Forms.TabPage();
            this.ImitTabCtrl = new System.Windows.Forms.TabControl();
            this.tabPage23 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.ImitTypeComboBox = new System.Windows.Forms.ComboBox();
            this.InputView = new System.Windows.Forms.DataGridView();
            this.tabPage24 = new System.Windows.Forms.TabPage();
            this.OutputView = new System.Windows.Forms.DataGridView();
            this.EditorPage = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ConstantsView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.AimSubtypeComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AimTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.dV = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.K0 = new System.Windows.Forms.TextBox();
            this.Ksv3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Ksv2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.K4 = new System.Windows.Forms.TextBox();
            this.K3 = new System.Windows.Forms.TextBox();
            this.K2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.K1 = new System.Windows.Forms.TextBox();
            this.FillDefaultsButon = new System.Windows.Forms.Button();
            this.TimeLable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteCtrlVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.TabCntrl.SuspendLayout();
            this.ImitPage.SuspendLayout();
            this.ImitTabCtrl.SuspendLayout();
            this.tabPage23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InputView)).BeginInit();
            this.tabPage24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutputView)).BeginInit();
            this.EditorPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConstantsView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TabCntrl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.FillDefaultsButon);
            this.splitContainer1.Panel2.Controls.Add(this.TimeLable);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Size = new System.Drawing.Size(1018, 390);
            this.splitContainer1.SplitterDistance = 885;
            this.splitContainer1.TabIndex = 0;
            // 
            // TabCntrl
            // 
            this.TabCntrl.Controls.Add(this.ImitPage);
            this.TabCntrl.Controls.Add(this.EditorPage);
            this.TabCntrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCntrl.Location = new System.Drawing.Point(0, 0);
            this.TabCntrl.Name = "TabCntrl";
            this.TabCntrl.SelectedIndex = 0;
            this.TabCntrl.Size = new System.Drawing.Size(885, 390);
            this.TabCntrl.TabIndex = 1;
            this.TabCntrl.SelectedIndexChanged += new System.EventHandler(this.TabCntrl_SelectedIndexChanged);
            // 
            // ImitPage
            // 
            this.ImitPage.Controls.Add(this.ImitTabCtrl);
            this.ImitPage.Location = new System.Drawing.Point(4, 22);
            this.ImitPage.Name = "ImitPage";
            this.ImitPage.Padding = new System.Windows.Forms.Padding(3);
            this.ImitPage.Size = new System.Drawing.Size(877, 364);
            this.ImitPage.TabIndex = 8;
            this.ImitPage.Text = "Модуль ЧИ";
            this.ImitPage.UseVisualStyleBackColor = true;
            // 
            // ImitTabCtrl
            // 
            this.ImitTabCtrl.Controls.Add(this.tabPage23);
            this.ImitTabCtrl.Controls.Add(this.tabPage24);
            this.ImitTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImitTabCtrl.Location = new System.Drawing.Point(3, 3);
            this.ImitTabCtrl.Name = "ImitTabCtrl";
            this.ImitTabCtrl.SelectedIndex = 0;
            this.ImitTabCtrl.Size = new System.Drawing.Size(871, 358);
            this.ImitTabCtrl.TabIndex = 0;
            // 
            // tabPage23
            // 
            this.tabPage23.Controls.Add(this.label14);
            this.tabPage23.Controls.Add(this.ImitTypeComboBox);
            this.tabPage23.Controls.Add(this.InputView);
            this.tabPage23.Location = new System.Drawing.Point(4, 22);
            this.tabPage23.Name = "tabPage23";
            this.tabPage23.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage23.Size = new System.Drawing.Size(863, 332);
            this.tabPage23.TabIndex = 0;
            this.tabPage23.Text = "Входные данные";
            this.tabPage23.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(7, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "ЧИ";
            // 
            // ImitTypeComboBox
            // 
            this.ImitTypeComboBox.FormattingEnabled = true;
            this.ImitTypeComboBox.Items.AddRange(new object[] {
            "33",
            "42",
            "43",
            "44",
            "46",
            "42+43",
            "44+46",
            "42-46"});
            this.ImitTypeComboBox.Location = new System.Drawing.Point(38, 6);
            this.ImitTypeComboBox.Name = "ImitTypeComboBox";
            this.ImitTypeComboBox.Size = new System.Drawing.Size(100, 21);
            this.ImitTypeComboBox.TabIndex = 1;
            this.ImitTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.TypeComboBox_SelectedIndexChanged);
            // 
            // InputView
            // 
            this.InputView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InputView.Location = new System.Drawing.Point(6, 33);
            this.InputView.Name = "InputView";
            this.InputView.Size = new System.Drawing.Size(851, 293);
            this.InputView.TabIndex = 0;
            // 
            // tabPage24
            // 
            this.tabPage24.Controls.Add(this.OutputView);
            this.tabPage24.Location = new System.Drawing.Point(4, 22);
            this.tabPage24.Name = "tabPage24";
            this.tabPage24.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage24.Size = new System.Drawing.Size(863, 332);
            this.tabPage24.TabIndex = 1;
            this.tabPage24.Text = "Выходные данные";
            this.tabPage24.UseVisualStyleBackColor = true;
            // 
            // OutputView
            // 
            this.OutputView.AllowUserToOrderColumns = true;
            this.OutputView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OutputView.Location = new System.Drawing.Point(6, 6);
            this.OutputView.Name = "OutputView";
            this.OutputView.Size = new System.Drawing.Size(511, 260);
            this.OutputView.TabIndex = 1;
            // 
            // EditorPage
            // 
            this.EditorPage.Controls.Add(this.splitContainer2);
            this.EditorPage.Location = new System.Drawing.Point(4, 22);
            this.EditorPage.Name = "EditorPage";
            this.EditorPage.Padding = new System.Windows.Forms.Padding(3);
            this.EditorPage.Size = new System.Drawing.Size(877, 364);
            this.EditorPage.TabIndex = 7;
            this.EditorPage.Text = "Редактор констант";
            this.EditorPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label15);
            this.splitContainer2.Panel2.Controls.Add(this.label16);
            this.splitContainer2.Panel2.Controls.Add(this.dV);
            this.splitContainer2.Panel2.Controls.Add(this.label12);
            this.splitContainer2.Panel2.Controls.Add(this.label13);
            this.splitContainer2.Panel2.Controls.Add(this.label9);
            this.splitContainer2.Panel2.Controls.Add(this.K0);
            this.splitContainer2.Panel2.Controls.Add(this.Ksv3);
            this.splitContainer2.Panel2.Controls.Add(this.label10);
            this.splitContainer2.Panel2.Controls.Add(this.label11);
            this.splitContainer2.Panel2.Controls.Add(this.Ksv2);
            this.splitContainer2.Panel2.Controls.Add(this.label8);
            this.splitContainer2.Panel2.Controls.Add(this.K4);
            this.splitContainer2.Panel2.Controls.Add(this.K3);
            this.splitContainer2.Panel2.Controls.Add(this.K2);
            this.splitContainer2.Panel2.Controls.Add(this.label7);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.K1);
            this.splitContainer2.Size = new System.Drawing.Size(871, 358);
            this.splitContainer2.SplitterDistance = 177;
            this.splitContainer2.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ConstantsView);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.AimSubtypeComboBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.AimTypeComboBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(871, 177);
            this.panel1.TabIndex = 6;
            // 
            // ConstantsView
            // 
            this.ConstantsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConstantsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConstantsView.Location = new System.Drawing.Point(17, 36);
            this.ConstantsView.Name = "ConstantsView";
            this.ConstantsView.Size = new System.Drawing.Size(841, 131);
            this.ConstantsView.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Подтип цели";
            // 
            // AimSubtypeComboBox
            // 
            this.AimSubtypeComboBox.FormattingEnabled = true;
            this.AimSubtypeComboBox.Location = new System.Drawing.Point(277, 9);
            this.AimSubtypeComboBox.Name = "AimSubtypeComboBox";
            this.AimSubtypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.AimSubtypeComboBox.TabIndex = 4;
            this.AimSubtypeComboBox.SelectedIndexChanged += new System.EventHandler(this.AimSubtypeComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Тип цели";
            // 
            // AimTypeComboBox
            // 
            this.AimTypeComboBox.FormattingEnabled = true;
            this.AimTypeComboBox.Location = new System.Drawing.Point(73, 9);
            this.AimTypeComboBox.Name = "AimTypeComboBox";
            this.AimTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.AimTypeComboBox.TabIndex = 3;
            this.AimTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.AimTypeComboBox_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(500, 57);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(278, 26);
            this.label15.TabIndex = 19;
            this.label15.Text = "Разрешающая способность дальностно-скоростного\r\nпортрета по скорости";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(500, 90);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "dV";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // dV
            // 
            this.dV.Location = new System.Drawing.Point(536, 87);
            this.dV.Name = "dV";
            this.dV.Size = new System.Drawing.Size(100, 20);
            this.dV.TabIndex = 17;
            this.dV.TextChanged += new System.EventHandler(this.K_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(500, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(324, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Размерность дальностно-скоростного портрета по дальности";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(500, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "K0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(187, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(307, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Коэффициенты калибровки ЭПР сверхкритического следа";
            // 
            // K0
            // 
            this.K0.Location = new System.Drawing.Point(536, 27);
            this.K0.Name = "K0";
            this.K0.Size = new System.Drawing.Size(100, 20);
            this.K0.TabIndex = 14;
            this.K0.TextChanged += new System.EventHandler(this.K_TextChanged);
            // 
            // Ksv3
            // 
            this.Ksv3.Location = new System.Drawing.Point(223, 57);
            this.Ksv3.Name = "Ksv3";
            this.Ksv3.Size = new System.Drawing.Size(100, 20);
            this.Ksv3.TabIndex = 12;
            this.Ksv3.TextChanged += new System.EventHandler(this.K_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(187, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Kсв3";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(187, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Kсв2";
            // 
            // Ksv2
            // 
            this.Ksv2.Location = new System.Drawing.Point(223, 27);
            this.Ksv2.Name = "Ksv2";
            this.Ksv2.Size = new System.Drawing.Size(100, 20);
            this.Ksv2.TabIndex = 9;
            this.Ksv2.TextChanged += new System.EventHandler(this.K_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Калибровочные коэффициенты";
            // 
            // K4
            // 
            this.K4.Location = new System.Drawing.Point(40, 114);
            this.K4.Name = "K4";
            this.K4.Size = new System.Drawing.Size(100, 20);
            this.K4.TabIndex = 7;
            this.K4.TextChanged += new System.EventHandler(this.K_TextChanged);
            // 
            // K3
            // 
            this.K3.Location = new System.Drawing.Point(40, 87);
            this.K3.Name = "K3";
            this.K3.Size = new System.Drawing.Size(100, 20);
            this.K3.TabIndex = 6;
            this.K3.TextChanged += new System.EventHandler(this.K_TextChanged);
            // 
            // K2
            // 
            this.K2.Location = new System.Drawing.Point(40, 57);
            this.K2.Name = "K2";
            this.K2.Size = new System.Drawing.Size(100, 20);
            this.K2.TabIndex = 5;
            this.K2.TextChanged += new System.EventHandler(this.K_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "K4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "K2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "K3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "K1";
            // 
            // K1
            // 
            this.K1.Location = new System.Drawing.Point(40, 27);
            this.K1.Name = "K1";
            this.K1.Size = new System.Drawing.Size(100, 20);
            this.K1.TabIndex = 0;
            this.K1.TextChanged += new System.EventHandler(this.K_TextChanged);
            // 
            // FillDefaultsButon
            // 
            this.FillDefaultsButon.Location = new System.Drawing.Point(3, 56);
            this.FillDefaultsButon.Name = "FillDefaultsButon";
            this.FillDefaultsButon.Size = new System.Drawing.Size(123, 38);
            this.FillDefaultsButon.TabIndex = 4;
            this.FillDefaultsButon.Text = "Заполнить стандартными";
            this.FillDefaultsButon.UseVisualStyleBackColor = true;
            this.FillDefaultsButon.Click += new System.EventHandler(this.Fill_Click);
            // 
            // TimeLable
            // 
            this.TimeLable.AutoSize = true;
            this.TimeLable.Location = new System.Drawing.Point(3, 137);
            this.TimeLable.Name = "TimeLable";
            this.TimeLable.Size = new System.Drawing.Size(0, 13);
            this.TimeLable.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Время выполнения:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 38);
            this.button2.TabIndex = 1;
            this.button2.Text = "Расчёт";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // pasteCtrlVToolStripMenuItem
            // 
            this.pasteCtrlVToolStripMenuItem.Name = "pasteCtrlVToolStripMenuItem";
            this.pasteCtrlVToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 390);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Программный модуль имитации РЛХ ЭСБЦ";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.TabCntrl.ResumeLayout(false);
            this.ImitPage.ResumeLayout(false);
            this.ImitTabCtrl.ResumeLayout(false);
            this.tabPage23.ResumeLayout(false);
            this.tabPage23.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InputView)).EndInit();
            this.tabPage24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OutputView)).EndInit();
            this.EditorPage.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConstantsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label TimeLable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteCtrlVToolStripMenuItem;
        private System.Windows.Forms.Button FillDefaultsButon;
        private System.Windows.Forms.TabControl TabCntrl;
        private System.Windows.Forms.TabPage EditorPage;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView ConstantsView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox AimSubtypeComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox AimTypeComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox K0;
        private System.Windows.Forms.TextBox Ksv3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Ksv2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox K4;
        private System.Windows.Forms.TextBox K3;
        private System.Windows.Forms.TextBox K2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox K1;
        private System.Windows.Forms.TabPage ImitPage;
        private System.Windows.Forms.TabControl ImitTabCtrl;
        private System.Windows.Forms.TabPage tabPage23;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox ImitTypeComboBox;
        private System.Windows.Forms.DataGridView InputView;
        private System.Windows.Forms.TabPage tabPage24;
        private System.Windows.Forms.DataGridView OutputView;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox dV;
    }
}

