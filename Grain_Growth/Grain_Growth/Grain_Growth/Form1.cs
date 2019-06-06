using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grain_Growth
{
    public partial class Form1 : Form
    {
        Timer timer;
        Bitmap bitmap;
        Graphics graphics;
        int cellSize;
        Simulation simulation;
        Random rand;
        bool energized;
        double deltaTime = 0.001;
        double startTime = 0;
        double endTime = 0.2;
        double A, B;
        double criticalDislocation;
        private List<double> dislocations;


        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 40;
            timer.Tick += new EventHandler(timerTick);
            bitmap = new Bitmap(lifeBox.Width, lifeBox.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            lifeBox.Image = bitmap;
            cellSize = 3;
            energized = false;
            simulation = new Simulation(lifeBox.Height / cellSize, lifeBox.Width / cellSize, graphics, cellSize, lifeBox, bitmap);
            this.rand = new Random();
            dislocations = new List<Double>();

        }

        int counter = 0;
        private void timerTick(object sender, EventArgs e)
        {
            counter++;
            simulation.nextStep();
            RefreshBitmap();

            if (counter == 100) 
            {
                timer.Stop();
                counter = 0;
                MessageBox.Show("Generating finished.");
            }
           
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if (grainTypeCombo.SelectedItem == null)
            {
                MessageBox.Show("Wybierz typ ziarna przed klikaniem na siatkę!");
                return;
            }
            int grainType = System.Convert.ToInt32(grainTypeCombo.SelectedItem);
            MouseEventArgs clicked = (MouseEventArgs)e;
            Point clickCoordinates = clicked.Location;
            simulation.AddSeedOfSpecifiedType(clickCoordinates.Y / cellSize, clickCoordinates.X / cellSize, grainType);
        }


        private void AddRandomGrainsClick(object sender, EventArgs e)
        {
            timer.Stop();
            int grainsCount;
            if(randomGrainsBox.Text =="")
            {
                MessageBox.Show("Wprowadziłeś błędną liczbę ziaren do wygenerowania.");
                return;
            }
            grainsCount = System.Convert.ToInt32(randomGrainsBox.Text);
            if (grainsCount < 1 || grainsCount>599)
            {
                MessageBox.Show("Wprowadziłeś błędną liczbę ziaren do wygenerowania.");
                return;
            }
            simulation.generateRandomGrains(grainsCount);
        }

        public void RefreshBitmap()
        {
            lifeBox.Image = bitmap;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            String method="";
            simulation.setPeriodic(periodicButton.Checked);
            if (vonNeumanButton.Checked)
                method = "VonNeuman";
            else if (MooreButton.Checked)
                method = "Moore";
            else if (hexagonalLeftButton.Checked)
                method = "HexagonalLeft";
            else if (hexagonalRightButton.Checked)
                method = "HexagonalRight";
            else if (hexagonalRandomButton.Checked)
                method = "HexagonalRandom";
            else if (pentagonalRandomButton.Checked)
                method = "PentagonalRandom";
            else if (withRadiusButton.Checked)
            {
                method = "WithRadius";
                if(System.Convert.ToInt32(withRadiusBox.Text)> System.Convert.ToInt32(radiusBox.Text)+30)
                {
                    MessageBox.Show("Błędna wartość promienia.");
                    return;
                }
                simulation.setRadius(System.Convert.ToInt32(withRadiusBox.Text));
            }

            simulation.setMethod(method);
                if (timer.Enabled)
                    timer.Stop();
                else
                    timer.Start();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timer.Stop();

            int numberRows;
            numberRows = System.Convert.ToInt32(homogRowBox.Text);

            int numberCols;
            numberCols = System.Convert.ToInt32(homogColBox.Text);

            if(numberRows<1 || numberCols<1 || numberRows * numberCols >599 || numberRows > lifeBox.Height / 3 ||
                numberCols > lifeBox.Width / 3)
            {
                MessageBox.Show("Wprowadziłeś błędne wartości!");
                return;
            }

            int maxColumns = simulation.columns % 2 != 0 ? simulation.columns-1 : simulation.columns;
            int maxRows = simulation.rows % 2 != 0 ? simulation.rows-1 : simulation.rows;

            int colOffset = maxColumns / numberCols;
            int rowOffset = maxRows / numberRows;

            for (int i = 0; i < numberRows; ++i)
                for (int j = 0; j < numberCols; ++j)
                {
                    simulation.addGrain(i * rowOffset + rowOffset / 2, j * colOffset + colOffset / 2);
                }


            /*
            int maxInWidth = simulation.columns % 2 != 0 ? simulation.columns-1 : simulation.columns;
            int mexInHeight = simulation.rows % 2 != 0 ? simulation.rows-1 : simulation.rows;
            int rowOffset = mexInHeight / numberRows;
            int colOffset = maxInWidth/ numberCols;

            for (int i = 0; i < numberRows; i++)
            {
                for (int j = 0; j < numberCols; j++)
                {
                    simulation.addGrain((int)((i + 0.4) * rowOffset), (int)((j + 0.4) * colOffset));
                }
            }*/
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (System.Convert.ToInt32(newWidthBox.Text) < 1 || System.Convert.ToInt32(newHeightBox.Text) < 1)
            {
                MessageBox.Show("Musisz podać dodatnie rozmiary siatki!");
                return;
            }

            if (System.Convert.ToInt32(newWidthBox.Text) >600 || System.Convert.ToInt32(newHeightBox.Text) > 494)
            {
                MessageBox.Show("Przekroczyłeś maksymalne dopuszczalne wymiary siatki.");
                return;
            }
            lifeBox.Width = System.Convert.ToInt32(newWidthBox.Text);
            lifeBox.Height = System.Convert.ToInt32(newHeightBox.Text);


            cellSize = 3;
            bitmap = null;
            graphics = null;
            bitmap = new Bitmap(lifeBox.Width, lifeBox.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            RefreshBitmap();
            simulation.Resize(lifeBox.Height / 3, lifeBox.Width / 3, cellSize, bitmap, graphics);
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            counter = 0;
            timer.Stop();
            simulation.clearAll();
            graphics.Clear(Color.White);
            RefreshBitmap();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            timer.Stop();
            grainTypeCombo.Items.Clear();
            int noOfGrains;
            if (noOfGrainsBox.Text == "")
            {
                MessageBox.Show("Wprowadziłeś błędną liczbę ziaren do wygenerowania.");
                return;
            }
            noOfGrains = System.Convert.ToInt32(noOfGrainsBox.Text);
            if (noOfGrains < 1 || noOfGrains>599)
            {
                MessageBox.Show("Wprowadziłeś błędną liczbę ziaren do wygenerowania!");
                return;
            }
            simulation.nextGrainState += noOfGrains;
            for (int i = 1; i <= noOfGrains; i++)
            {
                grainTypeCombo.Items.Add(i);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            int maxRadius = lifeBox.Width / 2;
            int radius = Convert.ToInt32(radiusBox.Text);

            if (radius > maxRadius)
            {
                MessageBox.Show("Przekroczyłeś maksymalną wielkość promienia!");
                return;
            }
            int amountOfCells = Convert.ToInt32(radiusAmountBox.Text);
            var randomGenerator = new Random();

            if(radius<0 || amountOfCells<0)
            {
                MessageBox.Show("Złe dane wejściowe!");
                return;
            }

            int randed = 0;

            for (int i=0;i<amountOfCells;i++)
            {
                if (simulation.nextGrainState == 599)
                {
                    MessageBox.Show("Przekroczono maksymalną ilość ziaren możliwą do wygenerowania.");
                    return;
                }
                int row = randomGenerator.Next(0, lifeBox.Height /cellSize);
                int column = randomGenerator.Next(0, lifeBox.Width/cellSize);
                if (checkInRadius(row, column, radius))
                {
                    simulation.addGrain(row, column);
                }
                else
                    i--;

                randed++;
                if (randed > 10000)
                {
                    MessageBox.Show("Nie można wylosować większej ilości punktów!");
                    return;
                }
               
            }
            counter = 0;
        
        }

        private bool checkInRadius(int row, int column, int radius)
        {
            int count = 0;
            int index1, index2, index3, index4;
            for (int r = 0; r < radius; ++r)
                for (int p = 0; p < radius; ++p)
                    if (p + r <= radius)
                    {
                        if (row - p < 0)
                            index1 = lifeBox.Height / cellSize - 1;
                        else
                            index1 = row - p;

                        if (column - r < 0)
                            index2 = lifeBox.Width / cellSize - 1;
                        else
                            index2 = column - r;

                        if (column + r > lifeBox.Width / cellSize - 1)
                            index3 = 0;
                        else
                            index3 = column + r;

                        if (row + p > lifeBox.Height / cellSize - 1)
                            index4 = 0;
                        else
                            index4 = row+p;

                        if (simulation.currentState[index1] [index2] != 0) count++;
                        if (simulation.currentState[index1] [index3] != 0) count++;
                        if (simulation.currentState[index4] [index3] != 0) count++;
                        if (simulation.currentState[index4] [index2] != 0) count++;
                        if (count != 0)
                            return false;
                    }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void HomogRowBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void MonteCarloButton_Click(object sender, EventArgs e)
        {
            int loopCount = System.Convert.ToInt32(iterationCountBox.Text);
            double ktParam = System.Convert.ToDouble(ktParamBox.Text);

            if(loopCount>100 || loopCount<1)
            {
                MessageBox.Show("Maksymalna ilość iteracji to 100, minimalna to 1.");
                return;
            }
            else if (ktParam<0)
            {
                MessageBox.Show("Parametr k musi być większy od 0.");
                return;
            }


            String neighborhood;
            counter = 99 - loopCount;
            simulation.setMethod("Monte Carlo");
            if (vonNeumanButton.Checked)
                neighborhood = "VonNeuman";
            else if (MooreButton.Checked)
                neighborhood = "Moore";
            else if (hexagonalLeftButton.Checked)
                neighborhood = "HexagonalLeft";
            else if (hexagonalRightButton.Checked)
                neighborhood = "HexagonalRight";
            else if (hexagonalRandomButton.Checked)
                neighborhood = "HexagonalRandom";
            else if (pentagonalRandomButton.Checked)
                neighborhood = "PentagonalRandom";
            else //if (withRadiusButton.Checked)
            {
                MessageBox.Show("Wybierz sąsiedztwo!!!");
                return;
            }
            simulation.setNeighborhood(neighborhood);
            simulation.setKtParameter(ktParam);

            if (timer.Enabled)
                timer.Stop();
            else
                timer.Start();
        }

        private void EnergyButton_Click(object sender, EventArgs e)
        {
            simulation.setMethod("Energy");
            if (energized == false)
            {
                counter = 98;
                energized = true;
                if (timer.Enabled)
                    timer.Stop();
                else
                    timer.Start();
            }
            else
            {
                for(int i=0;i<simulation.rows;i++)
                    for(int j=0;j<simulation.columns;j++)
                    {
                        graphics.FillRectangle(simulation.brushesList[simulation.currentState[i][j]], j * cellSize, i * cellSize, cellSize, cellSize);
                    }

                energized = false;
                RefreshBitmap();

            }
        }

        private void IterationCountBox_TextChanged(object sender, EventArgs e)
        {

        }

        int iterationNumber = 0;

        private void DrxButton_Click(object sender, EventArgs e)
        {
            A = System.Convert.ToDouble(aDrxLabel.Text);
            B = System.Convert.ToDouble(bDrxLabel.Text);
            criticalDislocation = System.Convert.ToDouble(criticalDislocationBox.Text);


            String path = @"C:\Users\Mateusz\Desktop\Wieloskalowe\Grain_Growth\rekrystalizacja.txt";
            double time = startTime;


            StreamWriter streamWriter = new StreamWriter(path);
            streamWriter.Write("{0,-30}", "Time");
            streamWriter.Write("{0,-30}", "Ro");
            streamWriter.Write("{0,-30}\n", "Delta Ro");
            do
            {
                streamWriter.Write("{0,-30}", time);
                calculateDislocations(time, streamWriter);
                countAverageDislocation(time, streamWriter);
                time += deltaTime;

                iterationNumber++;
                if (time > startTime + deltaTime)
                {
                    //rozrostDrx();

                    RefreshBitmap();

                }
            } while (time <= endTime + deltaTime);

            streamWriter.Close();
        }

        double dislocation;
        double deltaDislocation;
        double averageDislocationPerCell;
        double dislocationPerCell;
        double dislocationPackage;

       private void drxNucleation()
        {
            for (int i = 0; i < simulation.rows; ++i)
                for (int j = 0; j < simulation.columns; ++j)
                {
                    if (simulation.grains[i][j].dislocationDensity > criticalDislocation && simulation.grains[i][j].energy>0)
                    {
                        simulation.addDrxGrain(i, j);
                        RefreshBitmap();
                    }
                }
        }

        private void rozrostDrx()
        {
            simulation.skrystalizowalWPOPRZEDNIM = simulation.skrystalizowalWOBECNYM;
            simulation.clearCurrentStateDrx();

            Parallel.For(0, simulation.rows, i =>
            {

                //for (int i = 0; i < simulation.rows; ++i)
                //{
                for (int j = 0; j < simulation.columns; ++j)
                {
                    simulation.drxGrowth(i, j);
                    simulation.countEnergy(i, j);
                }
            }


            );
        }
        private void calculateDislocations(double t, StreamWriter streamWriter)
        {
            dislocation = A / B + (1 - A / B) * Math.Exp(-B * t);
            streamWriter.Write("{0,-30}", dislocation);
            dislocations.Add(dislocation);
        }

        private void countAverageDislocation(double t, StreamWriter streamWriter)
        {
            if (t < startTime + deltaTime)
            {
                streamWriter.Write("{0,-30} \n", 0);
                return;
            }

            deltaDislocation = dislocations[iterationNumber] - dislocations[iterationNumber-1];
            streamWriter.Write("{0,-30} \n", deltaDislocation);
            averageDislocationPerCell = deltaDislocation / (simulation.rows * simulation.columns);
            dislocationPerCell = 0.3 * averageDislocationPerCell;
            assignStartDislocation(dislocationPerCell);
            asignRemainedDislocations(deltaDislocation - dislocationPerCell);
        }

        private void assignStartDislocation(double dislocationValue)
        {
            for (int i = 0; i < simulation.rows; ++i)
                for (int j = 0; j < simulation.columns; ++j)
                {
                    simulation.grains[i][j].dislocationDensity += dislocationValue;
                    if (simulation.grains[i][j].dislocationDensity > criticalDislocation && simulation.grains[i][j].energy > 0)
                    {
                        simulation.addDrxGrain(i, j);
                        //simulation.skrystalizowalWPOPRZEDNIM[row][column] = 1;
                    }
                    //graphics.FillRectangle(new SolidBrush(Color.Black), j * cellSize + cellSize / 2, i * cellSize + cellSize / 2, cellSize / 2, cellSize / 2);
                    //RefreshBitmap();
                }
        }

        private void asignRemainedDislocations(double dislocationValue)
        {
            int row;
            int column;
            do
            {
                dislocationPackage = rand.NextDouble() * dislocationValue;
                dislocationValue -= dislocationPackage;


                int prob = rand.Next(0, 100);
                if (prob <= 80)
                {
                    row = rand.Next(simulation.rows - 1);
                    column = rand.Next(simulation.columns - 1);
                    while(simulation.grains[row][column].energy < 1)
                    {
                        row = rand.Next(simulation.rows - 1);
                        column = rand.Next(simulation.columns - 1);
                    }


                    simulation.grains[row][column].dislocationDensity += dislocationPackage;
                    if (simulation.grains[row][column].dislocationDensity > criticalDislocation && simulation.grains[row][column].energy > 0)
                    {
                        simulation.addDrxGrain(row, column);
                        //simulation.skrystalizowalWPOPRZEDNIM[row][column] = 1;
                    }
                }

                else
                {
                    row = rand.Next(simulation.rows - 1);
                    column = rand.Next(simulation.columns - 1);
                    while (simulation.grains[row][column].energy !=0)
                    {
                        row = rand.Next(simulation.rows - 1);
                        column = rand.Next(simulation.columns - 1);
                    }

                    simulation.grains[row][column].dislocationDensity += dislocationPackage;
                }
            }
            while (dislocationValue > 0);

        }

        bool drxEnergized = false;
        Bitmap oldBitmap=null;
        private void DrxEnergy_Click(object sender, EventArgs e)
        {
            List<Brush> dislocBrushes = new List<Brush>();


            Color randomColor = Color.FromArgb(0, 40, 0);

            dislocBrushes.Add(new SolidBrush(Color.FromArgb(0, 0, 0)));
            dislocBrushes.Add(new SolidBrush(Color.FromArgb(0, 40, 0)));
            dislocBrushes.Add(new SolidBrush(Color.FromArgb(0, 70, 0)));
            dislocBrushes.Add(new SolidBrush(Color.FromArgb(0, 100, 0)));
            dislocBrushes.Add(new SolidBrush(Color.FromArgb(0, 130, 0)));
            dislocBrushes.Add(new SolidBrush(Color.FromArgb(0, 160, 0)));
            dislocBrushes.Add(new SolidBrush(Color.FromArgb(0, 190, 0)));
            dislocBrushes.Add(new SolidBrush(Color.FromArgb(0, 220, 0)));
            dislocBrushes.Add(new SolidBrush(Color.FromArgb(0, 240, 0)));



            if (drxEnergized == false)
            {
                drxEnergized = true;
                if (oldBitmap == null)
                    oldBitmap = new Bitmap(bitmap);

                for (int i = 0; i < simulation.rows; ++i)
                    for (int j = 0; j < simulation.columns; ++j)
                    {
                        if (simulation.grains[i][j].dislocationDensity > 1285317711.7)
                            graphics.FillRectangle(dislocBrushes[8], j * cellSize, i * cellSize, cellSize, cellSize);
                        else if (simulation.grains[i][j].dislocationDensity > 1285317711.7 / 2)
                            graphics.FillRectangle(dislocBrushes[7], j * cellSize, i * cellSize, cellSize, cellSize);
                        else if (simulation.grains[i][j].dislocationDensity > 1285317711.7 / 4)
                            graphics.FillRectangle(dislocBrushes[6], j * cellSize, i * cellSize, cellSize, cellSize);
                        else if (simulation.grains[i][j].dislocationDensity > 1285317711.7 / 8)
                            graphics.FillRectangle(dislocBrushes[5], j * cellSize, i * cellSize, cellSize, cellSize);
                        else if (simulation.grains[i][j].dislocationDensity > 1285317711.7 / 16)
                            graphics.FillRectangle(dislocBrushes[4], j * cellSize, i * cellSize, cellSize, cellSize);
                        else if (simulation.grains[i][j].dislocationDensity > 1285317711.7 / 32)
                            graphics.FillRectangle(dislocBrushes[3], j * cellSize, i * cellSize, cellSize, cellSize);
                        else if (simulation.grains[i][j].dislocationDensity > 1285317711.7 / 64)
                            graphics.FillRectangle(dislocBrushes[2], j * cellSize, i * cellSize, cellSize, cellSize);
                        else
                            graphics.FillRectangle(dislocBrushes[1], j * cellSize, i * cellSize, cellSize, cellSize);

                        RefreshBitmap();
                    }
            }
            else
            {
                lifeBox.Image = oldBitmap;
                drxEnergized = false;
            }
        }

        private void CriticalDislocationBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
