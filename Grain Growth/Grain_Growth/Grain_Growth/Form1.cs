using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            simulation = new Simulation(lifeBox.Height / cellSize, lifeBox.Width / cellSize, graphics, cellSize, lifeBox, bitmap);

        }
        private void timerTick(object sender, EventArgs e)
        {
            simulation.nextStep();
            RefreshBitmap();
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
            int maxRadius = lifeBox.Width;
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
    }
}
