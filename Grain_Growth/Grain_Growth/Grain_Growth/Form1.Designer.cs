namespace Grain_Growth
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.lifeBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.homogRowBox = new System.Windows.Forms.TextBox();
            this.homogColBox = new System.Windows.Forms.TextBox();
            this.randomGrainsBox = new System.Windows.Forms.TextBox();
            this.addRandomGrains = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.newHeightBox = new System.Windows.Forms.TextBox();
            this.newWidthBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.periodicButton = new System.Windows.Forms.RadioButton();
            this.nonperiodicButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.noOfGrainsBox = new System.Windows.Forms.TextBox();
            this.grainTypeCombo = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.radiusAmountBox = new System.Windows.Forms.TextBox();
            this.radiusBox = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.withRadiusBox = new System.Windows.Forms.TextBox();
            this.pentagonalRandomButton = new System.Windows.Forms.RadioButton();
            this.withRadiusButton = new System.Windows.Forms.RadioButton();
            this.hexagonalRandomButton = new System.Windows.Forms.RadioButton();
            this.hexagonalRightButton = new System.Windows.Forms.RadioButton();
            this.hexagonalLeftButton = new System.Windows.Forms.RadioButton();
            this.vonNeumanButton = new System.Windows.Forms.RadioButton();
            this.MooreButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.monteCarloButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.iterationCountBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ktParamBox = new System.Windows.Forms.TextBox();
            this.energyButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.bDrxLabel = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.aDrxLabel = new System.Windows.Forms.TextBox();
            this.drxButton = new System.Windows.Forms.Button();
            this.criticalDislocationBox = new System.Windows.Forms.TextBox();
            this.drxEnergy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lifeBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lifeBox
            // 
            this.lifeBox.Location = new System.Drawing.Point(321, 12);
            this.lifeBox.Name = "lifeBox";
            this.lifeBox.Size = new System.Drawing.Size(600, 492);
            this.lifeBox.TabIndex = 0;
            this.lifeBox.TabStop = false;
            this.lifeBox.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(198, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 47);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add homogenous grains";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // homogRowBox
            // 
            this.homogRowBox.Location = new System.Drawing.Point(93, 195);
            this.homogRowBox.Name = "homogRowBox";
            this.homogRowBox.Size = new System.Drawing.Size(99, 20);
            this.homogRowBox.TabIndex = 2;
            this.homogRowBox.TextChanged += new System.EventHandler(this.HomogRowBox_TextChanged);
            // 
            // homogColBox
            // 
            this.homogColBox.Location = new System.Drawing.Point(93, 221);
            this.homogColBox.Name = "homogColBox";
            this.homogColBox.Size = new System.Drawing.Size(99, 20);
            this.homogColBox.TabIndex = 3;
            // 
            // randomGrainsBox
            // 
            this.randomGrainsBox.Location = new System.Drawing.Point(93, 25);
            this.randomGrainsBox.Name = "randomGrainsBox";
            this.randomGrainsBox.Size = new System.Drawing.Size(100, 20);
            this.randomGrainsBox.TabIndex = 4;
            // 
            // addRandomGrains
            // 
            this.addRandomGrains.Location = new System.Drawing.Point(199, 22);
            this.addRandomGrains.Name = "addRandomGrains";
            this.addRandomGrains.Size = new System.Drawing.Size(113, 23);
            this.addRandomGrains.TabIndex = 5;
            this.addRandomGrains.Text = "Add random grains";
            this.addRandomGrains.UseVisualStyleBackColor = true;
            this.addRandomGrains.Click += new System.EventHandler(this.AddRandomGrainsClick);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(53, 556);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // newHeightBox
            // 
            this.newHeightBox.Location = new System.Drawing.Point(93, 335);
            this.newHeightBox.Name = "newHeightBox";
            this.newHeightBox.Size = new System.Drawing.Size(100, 20);
            this.newHeightBox.TabIndex = 9;
            // 
            // newWidthBox
            // 
            this.newWidthBox.Location = new System.Drawing.Point(93, 309);
            this.newWidthBox.Name = "newWidthBox";
            this.newWidthBox.Size = new System.Drawing.Size(100, 20);
            this.newWidthBox.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(199, 306);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 49);
            this.button2.TabIndex = 7;
            this.button2.Text = "Change grid size";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // periodicButton
            // 
            this.periodicButton.AutoSize = true;
            this.periodicButton.Checked = true;
            this.periodicButton.Location = new System.Drawing.Point(6, 19);
            this.periodicButton.Name = "periodicButton";
            this.periodicButton.Size = new System.Drawing.Size(71, 17);
            this.periodicButton.TabIndex = 10;
            this.periodicButton.TabStop = true;
            this.periodicButton.Text = "Periodical";
            this.periodicButton.UseVisualStyleBackColor = true;
            // 
            // nonperiodicButton
            // 
            this.nonperiodicButton.AutoSize = true;
            this.nonperiodicButton.Location = new System.Drawing.Point(95, 19);
            this.nonperiodicButton.Name = "nonperiodicButton";
            this.nonperiodicButton.Size = new System.Drawing.Size(93, 17);
            this.nonperiodicButton.TabIndex = 11;
            this.nonperiodicButton.Text = "Non-periodical";
            this.nonperiodicButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.periodicButton);
            this.groupBox1.Controls.Add(this.nonperiodicButton);
            this.groupBox1.Location = new System.Drawing.Point(21, 376);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 45);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Boundary conditions";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(152, 556);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 13;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // noOfGrainsBox
            // 
            this.noOfGrainsBox.Location = new System.Drawing.Point(93, 65);
            this.noOfGrainsBox.Name = "noOfGrainsBox";
            this.noOfGrainsBox.Size = new System.Drawing.Size(100, 20);
            this.noOfGrainsBox.TabIndex = 14;
            // 
            // grainTypeCombo
            // 
            this.grainTypeCombo.FormattingEnabled = true;
            this.grainTypeCombo.Location = new System.Drawing.Point(93, 91);
            this.grainTypeCombo.Name = "grainTypeCombo";
            this.grainTypeCombo.Size = new System.Drawing.Size(100, 21);
            this.grainTypeCombo.TabIndex = 15;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(199, 65);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 47);
            this.button3.TabIndex = 16;
            this.button3.Text = "Generate grain types";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // radiusAmountBox
            // 
            this.radiusAmountBox.Location = new System.Drawing.Point(94, 159);
            this.radiusAmountBox.Name = "radiusAmountBox";
            this.radiusAmountBox.Size = new System.Drawing.Size(99, 20);
            this.radiusAmountBox.TabIndex = 19;
            // 
            // radiusBox
            // 
            this.radiusBox.Location = new System.Drawing.Point(94, 133);
            this.radiusBox.Name = "radiusBox";
            this.radiusBox.Size = new System.Drawing.Size(99, 20);
            this.radiusBox.TabIndex = 18;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(198, 133);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(113, 46);
            this.button4.TabIndex = 17;
            this.button4.Text = "Add grains with radius";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Number of grains";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Number of grains";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Radius";
            this.label4.Click += new System.EventHandler(this.Label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Number of rows";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Number of cols";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 338);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Height";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 312);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Width";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.withRadiusBox);
            this.groupBox2.Controls.Add(this.pentagonalRandomButton);
            this.groupBox2.Controls.Add(this.withRadiusButton);
            this.groupBox2.Controls.Add(this.hexagonalRandomButton);
            this.groupBox2.Controls.Add(this.hexagonalRightButton);
            this.groupBox2.Controls.Add(this.hexagonalLeftButton);
            this.groupBox2.Controls.Add(this.vonNeumanButton);
            this.groupBox2.Controls.Add(this.MooreButton);
            this.groupBox2.Location = new System.Drawing.Point(21, 427);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 114);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Neighborhood";
            // 
            // withRadiusBox
            // 
            this.withRadiusBox.Location = new System.Drawing.Point(150, 88);
            this.withRadiusBox.Name = "withRadiusBox";
            this.withRadiusBox.Size = new System.Drawing.Size(100, 20);
            this.withRadiusBox.TabIndex = 31;
            // 
            // pentagonalRandomButton
            // 
            this.pentagonalRandomButton.AutoSize = true;
            this.pentagonalRandomButton.Location = new System.Drawing.Point(151, 65);
            this.pentagonalRandomButton.Name = "pentagonalRandomButton";
            this.pentagonalRandomButton.Size = new System.Drawing.Size(117, 17);
            this.pentagonalRandomButton.TabIndex = 15;
            this.pentagonalRandomButton.TabStop = true;
            this.pentagonalRandomButton.Text = "Pentagonal random";
            this.pentagonalRandomButton.UseVisualStyleBackColor = true;
            // 
            // withRadiusButton
            // 
            this.withRadiusButton.AutoSize = true;
            this.withRadiusButton.Location = new System.Drawing.Point(6, 88);
            this.withRadiusButton.Name = "withRadiusButton";
            this.withRadiusButton.Size = new System.Drawing.Size(78, 17);
            this.withRadiusButton.TabIndex = 30;
            this.withRadiusButton.TabStop = true;
            this.withRadiusButton.Text = "With radius";
            this.withRadiusButton.UseVisualStyleBackColor = true;
            // 
            // hexagonalRandomButton
            // 
            this.hexagonalRandomButton.AutoSize = true;
            this.hexagonalRandomButton.Location = new System.Drawing.Point(6, 65);
            this.hexagonalRandomButton.Name = "hexagonalRandomButton";
            this.hexagonalRandomButton.Size = new System.Drawing.Size(114, 17);
            this.hexagonalRandomButton.TabIndex = 14;
            this.hexagonalRandomButton.TabStop = true;
            this.hexagonalRandomButton.Text = "Hexagonal random";
            this.hexagonalRandomButton.UseVisualStyleBackColor = true;
            // 
            // hexagonalRightButton
            // 
            this.hexagonalRightButton.AutoSize = true;
            this.hexagonalRightButton.Location = new System.Drawing.Point(151, 42);
            this.hexagonalRightButton.Name = "hexagonalRightButton";
            this.hexagonalRightButton.Size = new System.Drawing.Size(99, 17);
            this.hexagonalRightButton.TabIndex = 13;
            this.hexagonalRightButton.TabStop = true;
            this.hexagonalRightButton.Text = "Hexagonal right";
            this.hexagonalRightButton.UseVisualStyleBackColor = true;
            // 
            // hexagonalLeftButton
            // 
            this.hexagonalLeftButton.AutoSize = true;
            this.hexagonalLeftButton.Location = new System.Drawing.Point(6, 42);
            this.hexagonalLeftButton.Name = "hexagonalLeftButton";
            this.hexagonalLeftButton.Size = new System.Drawing.Size(93, 17);
            this.hexagonalLeftButton.TabIndex = 12;
            this.hexagonalLeftButton.TabStop = true;
            this.hexagonalLeftButton.Text = "Hexagonal left";
            this.hexagonalLeftButton.UseVisualStyleBackColor = true;
            // 
            // vonNeumanButton
            // 
            this.vonNeumanButton.AutoSize = true;
            this.vonNeumanButton.Checked = true;
            this.vonNeumanButton.Location = new System.Drawing.Point(6, 19);
            this.vonNeumanButton.Name = "vonNeumanButton";
            this.vonNeumanButton.Size = new System.Drawing.Size(87, 17);
            this.vonNeumanButton.TabIndex = 10;
            this.vonNeumanButton.TabStop = true;
            this.vonNeumanButton.Text = "Von Neuman";
            this.vonNeumanButton.UseVisualStyleBackColor = true;
            // 
            // MooreButton
            // 
            this.MooreButton.AutoSize = true;
            this.MooreButton.Location = new System.Drawing.Point(151, 19);
            this.MooreButton.Name = "MooreButton";
            this.MooreButton.Size = new System.Drawing.Size(55, 17);
            this.MooreButton.TabIndex = 11;
            this.MooreButton.Text = "Moore";
            this.MooreButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Number of grains";
            // 
            // monteCarloButton
            // 
            this.monteCarloButton.Location = new System.Drawing.Point(536, 515);
            this.monteCarloButton.Name = "monteCarloButton";
            this.monteCarloButton.Size = new System.Drawing.Size(125, 20);
            this.monteCarloButton.TabIndex = 29;
            this.monteCarloButton.Text = "Monte Carlo";
            this.monteCarloButton.UseVisualStyleBackColor = true;
            this.monteCarloButton.Click += new System.EventHandler(this.MonteCarloButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(323, 519);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Number of iterations";
            // 
            // iterationCountBox
            // 
            this.iterationCountBox.Location = new System.Drawing.Point(430, 515);
            this.iterationCountBox.Name = "iterationCountBox";
            this.iterationCountBox.Size = new System.Drawing.Size(100, 20);
            this.iterationCountBox.TabIndex = 30;
            this.iterationCountBox.TextChanged += new System.EventHandler(this.IterationCountBox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(353, 544);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "KT parameter";
            // 
            // ktParamBox
            // 
            this.ktParamBox.Location = new System.Drawing.Point(430, 541);
            this.ktParamBox.Name = "ktParamBox";
            this.ktParamBox.Size = new System.Drawing.Size(100, 20);
            this.ktParamBox.TabIndex = 32;
            // 
            // energyButton
            // 
            this.energyButton.Location = new System.Drawing.Point(536, 544);
            this.energyButton.Name = "energyButton";
            this.energyButton.Size = new System.Drawing.Size(125, 20);
            this.energyButton.TabIndex = 34;
            this.energyButton.Text = "Energy";
            this.energyButton.UseVisualStyleBackColor = true;
            this.energyButton.Click += new System.EventHandler(this.EnergyButton_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(675, 548);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "B";
            // 
            // bDrxLabel
            // 
            this.bDrxLabel.Location = new System.Drawing.Point(695, 544);
            this.bDrxLabel.Name = "bDrxLabel";
            this.bDrxLabel.Size = new System.Drawing.Size(100, 20);
            this.bDrxLabel.TabIndex = 38;
            this.bDrxLabel.Text = "9,41268203527779";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(675, 522);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 37;
            this.label12.Text = "A";
            // 
            // aDrxLabel
            // 
            this.aDrxLabel.Location = new System.Drawing.Point(695, 518);
            this.aDrxLabel.Name = "aDrxLabel";
            this.aDrxLabel.Size = new System.Drawing.Size(100, 20);
            this.aDrxLabel.TabIndex = 36;
            this.aDrxLabel.Text = "86710969050178,5";
            // 
            // drxButton
            // 
            this.drxButton.Location = new System.Drawing.Point(801, 544);
            this.drxButton.Name = "drxButton";
            this.drxButton.Size = new System.Drawing.Size(125, 20);
            this.drxButton.TabIndex = 35;
            this.drxButton.Text = "DRX";
            this.drxButton.UseVisualStyleBackColor = true;
            this.drxButton.Click += new System.EventHandler(this.DrxButton_Click);
            // 
            // criticalDislocationBox
            // 
            this.criticalDislocationBox.Location = new System.Drawing.Point(801, 518);
            this.criticalDislocationBox.Name = "criticalDislocationBox";
            this.criticalDislocationBox.Size = new System.Drawing.Size(125, 20);
            this.criticalDislocationBox.TabIndex = 40;
            this.criticalDislocationBox.Text = "1285317711,7";
            this.criticalDislocationBox.TextChanged += new System.EventHandler(this.CriticalDislocationBox_TextChanged);
            // 
            // drxEnergy
            // 
            this.drxEnergy.Location = new System.Drawing.Point(801, 570);
            this.drxEnergy.Name = "drxEnergy";
            this.drxEnergy.Size = new System.Drawing.Size(125, 20);
            this.drxEnergy.TabIndex = 41;
            this.drxEnergy.Text = "Energy";
            this.drxEnergy.UseVisualStyleBackColor = true;
            this.drxEnergy.Click += new System.EventHandler(this.DrxEnergy_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 597);
            this.Controls.Add(this.drxEnergy);
            this.Controls.Add(this.criticalDislocationBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.bDrxLabel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.aDrxLabel);
            this.Controls.Add(this.drxButton);
            this.Controls.Add(this.energyButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ktParamBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.iterationCountBox);
            this.Controls.Add(this.monteCarloButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radiusAmountBox);
            this.Controls.Add(this.radiusBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.grainTypeCombo);
            this.Controls.Add(this.noOfGrainsBox);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.newHeightBox);
            this.Controls.Add(this.newWidthBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.addRandomGrains);
            this.Controls.Add(this.randomGrainsBox);
            this.Controls.Add(this.homogColBox);
            this.Controls.Add(this.homogRowBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lifeBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lifeBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox lifeBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox homogRowBox;
        private System.Windows.Forms.TextBox homogColBox;
        private System.Windows.Forms.TextBox randomGrainsBox;
        private System.Windows.Forms.Button addRandomGrains;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox newHeightBox;
        private System.Windows.Forms.TextBox newWidthBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton periodicButton;
        private System.Windows.Forms.RadioButton nonperiodicButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.TextBox noOfGrainsBox;
        private System.Windows.Forms.ComboBox grainTypeCombo;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox radiusAmountBox;
        private System.Windows.Forms.TextBox radiusBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton vonNeumanButton;
        private System.Windows.Forms.RadioButton MooreButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton hexagonalLeftButton;
        private System.Windows.Forms.RadioButton hexagonalRightButton;
        private System.Windows.Forms.RadioButton hexagonalRandomButton;
        private System.Windows.Forms.RadioButton pentagonalRandomButton;
        private System.Windows.Forms.TextBox withRadiusBox;
        private System.Windows.Forms.RadioButton withRadiusButton;
        private System.Windows.Forms.Button monteCarloButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox iterationCountBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ktParamBox;
        private System.Windows.Forms.Button energyButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox bDrxLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox aDrxLabel;
        private System.Windows.Forms.Button drxButton;
        private System.Windows.Forms.TextBox criticalDislocationBox;
        private System.Windows.Forms.Button drxEnergy;
    }
}

