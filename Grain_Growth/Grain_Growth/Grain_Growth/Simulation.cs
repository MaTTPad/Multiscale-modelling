using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
namespace Grain_Growth
{
    class Simulation
    {
        public List<Brush> brushesList;
        public int rows;
        public int columns;
        public int nextGrainState;
        public List<Grain> grains;
        public Graphics graphics;
        public List<List<int>> previousState;
        public List<List<int>> currentState;
        public List<List<int>> randedGrains;
        public List<List<double>> centersOfGravityX;
        public List<List<double>> centersOfGravityY;
        public int cellSize;
        public PictureBox lifeBox;
        public Bitmap bitmap;
        public bool isPeriodic;
        public String method;
        public String neighborhood;
        Random rand;
        public int radius;
        public double ktParameter;

        public Simulation(int rows, int columns, Graphics g, int cellSize, PictureBox lifeBox, Bitmap bitmap)
        {
            this.rows = rows;
            this.columns = columns;
            this.graphics = g;
            this.nextGrainState = 1;
            this.lifeBox = lifeBox;
            this.bitmap = bitmap;
            this.rand= new Random();

            grains = new List<Grain>();
            currentState = new List<List<int>>();
            previousState = new List<List<int>>();
            randedGrains = new List<List<int>>();
            centersOfGravityX = new List<List<double>>();
            centersOfGravityY = new List<List<double>>();

            for (int i = 0; i < rows; i++)
            {
                currentState.Add(Enumerable.Repeat<int>(0, columns).ToList());
                previousState.Add(Enumerable.Repeat<int>(0, columns).ToList());
                centersOfGravityX.Add(Enumerable.Repeat<double>(0, columns).ToList());
                centersOfGravityY.Add(Enumerable.Repeat<double>(0, columns).ToList());
                //randedGrains.Add(Enumerable.Repeat<int>(0, columns).ToList());
            }

            this.cellSize = cellSize;
            brushesList = new List<Brush>();
            Random randColor = new Random();
            for (int i = 0; i < 600; i++)
            {
                Color randomColor = Color.FromArgb(randColor.Next(256), randColor.Next(256), randColor.Next(256));
                brushesList.Add(new SolidBrush(randomColor));
            }
            setCentersOfGravity();
        }

        public void setCentersOfGravity()
        {
            for(int i=0;i<rows;i++)
                for(int j=0;j<columns;j++)
                {
                    centersOfGravityX[i][j] = getRandomNumber(j * 3, j * 3 + 3);
                    centersOfGravityY[i][j] = getRandomNumber(i * 3, i * 3 + 3);
                }
        }
        public double getRandomNumber(double minimum, double maximum)
        {
            return rand.NextDouble() * (maximum - minimum) + minimum;
        }

        public void setPeriodic(bool isPeriodic)
        {
            this.isPeriodic = isPeriodic;
        }

        public void setMethod(String method)
        {
            this.method = method;
        }

        public void setNeighborhood(String neighborhood)
        {
            this.neighborhood = neighborhood;
        }

        public void setRadius(int radius)
        {
            this.radius = radius;
        }

        public void setKtParameter(double kt)
        {
            this.ktParameter = kt;
        }

        public void RefreshBitmap()
        {
            lifeBox.Image = bitmap;
        }

        public void nextStep()
        {
            previousState = currentState;
            clearCurrentState();
            int randedRow;
            int randedColumn;

            //randedGrains.Clear();
            randedGrains = new List<List<int>>();

            for (int l = 0; l < rows; l++)
            {
                randedGrains.Add(Enumerable.Repeat<int>(0, columns).ToList());
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (previousState[i][j] == 0 && method != "Monte Carlo")
                    {
                        if (method == "VonNeuman")
                            vonNeuman(i, j);
                        else if (method == "Moore")
                            moore(i, j);
                        else if (method == "HexagonalLeft")
                            hexagonalLeft(i, j);
                        else if (method == "HexagonalRight")
                            hexagonalRight(i, j);
                        else if (method == "HexagonalRandom")
                        {
                            if (rand.Next(0, 2) == 0)
                                hexagonalLeft(i, j);
                            else
                                hexagonalRight(i, j);
                        }
                        else if (method == "PentagonalRandom")
                        {
                            int randed = rand.Next(0, 5);
                            if (randed == 0)
                                pentagonal1(i, j);
                            else if (randed == 1)
                                pentagonal2(i, j);
                            else if (randed == 2)
                                pentagonal3(i, j);
                            else
                                pentagonal4(i, j);
                        }
                        else if (method == "WithRadius")
                            withRadius(i, j);
                    }
                    else if (method == "Monte Carlo")
                    {

                        randedRow = rand.Next(0, rows);
                        randedColumn = rand.Next(0, columns);

                        while (randedGrains[randedRow][randedColumn] == 1)
                        {
                            randedRow = rand.Next(0, rows);
                            randedColumn = rand.Next(0, columns);
                        }

                        randedGrains[randedRow][randedColumn] = 1;

                        monteCarlo(randedRow, randedColumn);
                        /*bool nextStep = false;
                        for (int k = 0; k < randedGrains.Count; k++)
                        {
                            if (randedGrains[k].Contains(0)) // (you use the word "contains". either equals or indexof might be appropriate)
                            {
                                nextStep = true;
                                break;
                            }
                        }
                        if (nextStep == false)
                            break;

                        */
                    }
                    else if (method == "Energy")
                    {
                        energy(i, j);

                    }
                    else
                    {
                        currentState[i][j] = previousState[i][j];
                    }
                }
            };
        }

        private void vonNeuman(int rowToCheck, int columnToCheck)
        {

            List<int> stateCounter = Enumerable.Repeat<int>(0, nextGrainState).ToList();

            int left = columnToCheck - 1;
            int right = columnToCheck + 1;
            int up = rowToCheck - 1;
            int down = rowToCheck + 1;
            if (!isPeriodic)
            {
                if (left < 0) left = 0;
                if (right >= columns) right = columns - 1;
                if (up < 0) up = 0;
                if (down >= rows) down = rows - 1;
            }
            else
            {
                if (left < 0) left = columns - 1;
                if (right >= columns) right = 0;
                if (up < 0) up = rows - 1;
                if (down >= rows) down = 0;
            }

            if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
            if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
            if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
            if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;

            int winningState = 0;
            for (int i = 1; i < stateCounter.Count; i++)
            {
                if (stateCounter[i] > 0 && stateCounter[i] > stateCounter[winningState])
                {
                    winningState = i;
                }
            }
            if (winningState > 0)
            {
                currentState[rowToCheck][columnToCheck] = winningState;
                graphics.FillRectangle(brushesList[winningState], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);
            }
       }

        private void moore(int rowToCheck, int columnToCheck)
        {

            List<int> stateCounter = Enumerable.Repeat<int>(0, nextGrainState).ToList();

            int left = columnToCheck - 1;
            int right = columnToCheck + 1;
            int up = rowToCheck - 1;
            int down = rowToCheck + 1;
            if (!isPeriodic)
            {
                if (left < 0) left = 0;
                if (right >= columns) right = columns - 1;
                if (up < 0) up = 0;
                if (down >= rows) down = rows - 1;
            }
            else
            {
                if (left < 0) left = columns - 1;
                if (right >= columns) right = 0;
                if (up < 0) up = rows - 1;
                if (down >= rows) down = 0;
            }

            if (previousState[up][left] != 0) stateCounter[previousState[up][left]]++;
            if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
            if (previousState[down][left] != 0) stateCounter[previousState[down][left]]++;

            if (previousState[up][right] != 0) stateCounter[previousState[up][right]]++;
            if (previousState[rowToCheck] [right] != 0) stateCounter[previousState[rowToCheck][right]]++;
            if (previousState[down][right] != 0) stateCounter[previousState[down][right]]++;

            if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
            if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;

            int winningState = 0;
            for (int i = 1; i < stateCounter.Count; i++)
            {
                if (stateCounter[i] > 0 && stateCounter[i] > stateCounter[winningState])
                {
                    winningState = i;
                }
            }
            if (winningState > 0)
            {
                currentState[rowToCheck][columnToCheck] = winningState;
                graphics.FillRectangle(brushesList[winningState], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);
            }
        }

        private void hexagonalLeft(int rowToCheck, int columnToCheck)
        {

            List<int> stateCounter = Enumerable.Repeat<int>(0, nextGrainState).ToList();

            int left = columnToCheck - 1;
            int right = columnToCheck + 1;
            int up = rowToCheck - 1;
            int down = rowToCheck + 1;
            if (!isPeriodic)
            {
                if (left < 0) left = 0;
                if (right >= columns) right = columns - 1;
                if (up < 0) up = 0;
                if (down >= rows) down = rows - 1;
            }
            else
            {
                if (left < 0) left = columns - 1;
                if (right >= columns) right = 0;
                if (up < 0) up = rows - 1;
                if (down >= rows) down = 0;
            }

            if (previousState[up][left] != 0) stateCounter[previousState[up][left]]++;
            if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;

            if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
            if (previousState[down][right] != 0) stateCounter[previousState[down][right]]++;

            if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
            if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;

            int winningState = 0;
            for (int i = 1; i < stateCounter.Count; i++)
            {
                if (stateCounter[i] > 0 && stateCounter[i] > stateCounter[winningState])
                {
                    winningState = i;
                }
            }
            if (winningState > 0)
            {
                currentState[rowToCheck][columnToCheck] = winningState;
                graphics.FillRectangle(brushesList[winningState], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);
            }
        }

        private void hexagonalRight(int rowToCheck, int columnToCheck)
        {

            List<int> stateCounter = Enumerable.Repeat<int>(0, nextGrainState).ToList();

            int left = columnToCheck - 1;
            int right = columnToCheck + 1;
            int up = rowToCheck - 1;
            int down = rowToCheck + 1;
            if (!isPeriodic)
            {
                if (left < 0) left = 0;
                if (right >= columns) right = columns - 1;
                if (up < 0) up = 0;
                if (down >= rows) down = rows - 1;
            }
            else
            {
                if (left < 0) left = columns - 1;
                if (right >= columns) right = 0;
                if (up < 0) up = rows - 1;
                if (down >= rows) down = 0;
            }

            if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
            if (previousState[down][left] != 0) stateCounter[previousState[down][left]]++;

            if (previousState[up][right] != 0) stateCounter[previousState[up][right]]++;
            if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
    
            if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
            if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;

            int winningState = 0;
            for (int i = 1; i < stateCounter.Count; i++)
            {
                if (stateCounter[i] > 0 && stateCounter[i] > stateCounter[winningState])
                {
                    winningState = i;
                }
            }
            if (winningState > 0)
            {
                currentState[rowToCheck][columnToCheck] = winningState;
                graphics.FillRectangle(brushesList[winningState], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);
            }
        }

        private void pentagonal1(int rowToCheck, int columnToCheck)
        {

            List<int> stateCounter = Enumerable.Repeat<int>(0, nextGrainState).ToList();

            int left = columnToCheck - 1;
            int right = columnToCheck + 1;
            int up = rowToCheck - 1;
            int down = rowToCheck + 1;
            if (!isPeriodic)
            {
                if (left < 0) left = 0;
                if (right >= columns) right = columns - 1;
                if (up < 0) up = 0;
                if (down >= rows) down = rows - 1;
            }
            else
            {
                if (left < 0) left = columns - 1;
                if (right >= columns) right = 0;
                if (up < 0) up = rows - 1;
                if (down >= rows) down = 0;
            }

            if (previousState[up][left] != 0) stateCounter[previousState[up][left]]++;
            if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
            if (previousState[down][left] != 0) stateCounter[previousState[down][left]]++;

            if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
            if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;

            int winningState = 0;
            for (int i = 1; i < stateCounter.Count; i++)
            {
                if (stateCounter[i] > 0 && stateCounter[i] > stateCounter[winningState])
                {
                    winningState = i;
                }
            }
            if (winningState > 0)
            {
                currentState[rowToCheck][columnToCheck] = winningState;
                graphics.FillRectangle(brushesList[winningState], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);
            }
        }

        private void pentagonal2(int rowToCheck, int columnToCheck)
        {

            List<int> stateCounter = Enumerable.Repeat<int>(0, nextGrainState).ToList();

            int left = columnToCheck - 1;
            int right = columnToCheck + 1;
            int up = rowToCheck - 1;
            int down = rowToCheck + 1;
            if (!isPeriodic)
            {
                if (left < 0) left = 0;
                if (right >= columns) right = columns - 1;
                if (up < 0) up = 0;
                if (down >= rows) down = rows - 1;
            }
            else
            {
                if (left < 0) left = columns - 1;
                if (right >= columns) right = 0;
                if (up < 0) up = rows - 1;
                if (down >= rows) down = 0;
            }

            if (previousState[up][right] != 0) stateCounter[previousState[up][right]]++;
            if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
            if (previousState[down][right] != 0) stateCounter[previousState[down][right]]++;

            if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
            if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;

            int winningState = 0;
            for (int i = 1; i < stateCounter.Count; i++)
            {
                if (stateCounter[i] > 0 && stateCounter[i] > stateCounter[winningState])
                {
                    winningState = i;
                }
            }
            if (winningState > 0)
            {
                currentState[rowToCheck][columnToCheck] = winningState;
                graphics.FillRectangle(brushesList[winningState], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);
            }
        }

        private void pentagonal3(int rowToCheck, int columnToCheck)
        {

            List<int> stateCounter = Enumerable.Repeat<int>(0, nextGrainState).ToList();

            int left = columnToCheck - 1;
            int right = columnToCheck + 1;
            int up = rowToCheck - 1;
            int down = rowToCheck + 1;
            if (!isPeriodic)
            {
                if (left < 0) left = 0;
                if (right >= columns) right = columns - 1;
                if (up < 0) up = 0;
                if (down >= rows) down = rows - 1;
            }
            else
            {
                if (left < 0) left = columns - 1;
                if (right >= columns) right = 0;
                if (up < 0) up = rows - 1;
                if (down >= rows) down = 0;
            }

            if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
            if (previousState[down][left] != 0) stateCounter[previousState[down][left]]++;

            if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
            if (previousState[down][right] != 0) stateCounter[previousState[down][right]]++;

            if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;

            int winningState = 0;
            for (int i = 1; i < stateCounter.Count; i++)
            {
                if (stateCounter[i] > 0 && stateCounter[i] > stateCounter[winningState])
                {
                    winningState = i;
                }
            }
            if (winningState > 0)
            {
                currentState[rowToCheck][columnToCheck] = winningState;
                graphics.FillRectangle(brushesList[winningState], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);
            }
        }

        private void pentagonal4(int rowToCheck, int columnToCheck)
        {

            List<int> stateCounter = Enumerable.Repeat<int>(0, nextGrainState).ToList();

            int left = columnToCheck - 1;
            int right = columnToCheck + 1;
            int up = rowToCheck - 1;
            int down = rowToCheck + 1;
            if (!isPeriodic)
            {
                if (left < 0) left = 0;
                if (right >= columns) right = columns - 1;
                if (up < 0) up = 0;
                if (down >= rows) down = rows - 1;
            }
            else
            {
                if (left < 0) left = columns - 1;
                if (right >= columns) right = 0;
                if (up < 0) up = rows - 1;
                if (down >= rows) down = 0;
            }

            if (previousState[up][left] != 0) stateCounter[previousState[up][left]]++;
            if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;

            if (previousState[up][right] != 0) stateCounter[previousState[up][right]]++;
            if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;

            if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;

            int winningState = 0;
            for (int i = 1; i < stateCounter.Count; i++)
            {
                if (stateCounter[i] > 0 && stateCounter[i] > stateCounter[winningState])
                {
                    winningState = i;
                }
            }
            if (winningState > 0)
            {
                currentState[rowToCheck][columnToCheck] = winningState;
                graphics.FillRectangle(brushesList[winningState], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);
            }
        }

        private void withRadius(int rowToCheck, int columnToCheck)
        {

            List<int> stateCounter = Enumerable.Repeat<int>(0, nextGrainState).ToList();
            double d;

            Parallel.For(0, rows, i =>
            {
                //   for (int i=0;i<rows;i++)
                //{
                for (int j = 0; j < columns; j++)
                {
                    if ((i!=rowToCheck || j!=columnToCheck) && previousState[i][j] != 0)
                    {
                        d = Math.Sqrt(Math.Pow(centersOfGravityX[rowToCheck][columnToCheck]- centersOfGravityX[i][j], 2) +
                            Math.Pow(centersOfGravityY[rowToCheck][columnToCheck]- centersOfGravityY[i][j], 2));
                        if (d < radius)
                        {
                           // if (previousState[i][j] != 0)
                                stateCounter[previousState[i][j]]++;
                        }
                    }
                }
            });

            int winningState = 0;
            for (int i = 1; i < stateCounter.Count; i++)
            {
                if (stateCounter[i] > 0 && stateCounter[i] > stateCounter[winningState])
                {
                    winningState = i;
                }
            }
            if (winningState > 0)
            {
                //grains.Add(new Grain(rowToCheck, columnToCheck, winningState));
                currentState[rowToCheck][columnToCheck] = winningState;
                graphics.FillRectangle(brushesList[winningState], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);
            }
        }


        private void monteCarlo(int rowToCheck, int columnToCheck)
        {
            List<int> stateCounter = Enumerable.Repeat<int>(0, nextGrainState).ToList();

            int left = columnToCheck - 1;
            int right = columnToCheck + 1;
            int up = rowToCheck - 1;
            int down = rowToCheck + 1;
            if (!isPeriodic)
            {
                if (left < 0) left = 0;
                if (right >= columns) right = columns - 1;
                if (up < 0) up = 0;
                if (down >= rows) down = rows - 1;
            }
            else
            {
                if (left < 0) left = columns - 1;
                if (right >= columns) right = 0;
                if (up < 0) up = rows - 1;
                if (down >= rows) down = 0;
            }



            if (neighborhood == "Moore")
            {
                if (previousState[up][left] != 0) stateCounter[previousState[up][left]]++;
                if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
                if (previousState[down][left] != 0) stateCounter[previousState[down][left]]++;

                if (previousState[up][right] != 0) stateCounter[previousState[up][right]]++;
                if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
                if (previousState[down][right] != 0) stateCounter[previousState[down][right]]++;

                if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
                if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;
            }
            else if (neighborhood == "VonNeuman")
            {
                if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
                if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
                if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
                if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;
            }
            else if (neighborhood == "HexagonalLeft")
            {
                if (previousState[up][left] != 0) stateCounter[previousState[up][left]]++;
                if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;

                if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
                if (previousState[down][right] != 0) stateCounter[previousState[down][right]]++;

                if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
                if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;
            }
            else if (neighborhood == "HexagonalRight")
            {
                if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
                if (previousState[down][left] != 0) stateCounter[previousState[down][left]]++;

                if (previousState[up][right] != 0) stateCounter[previousState[up][right]]++;
                if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;

                if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
                if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;
            }
            else if (neighborhood == "HexagonalRandom")
            {
                if (rand.Next(0, 2) == 0)
                {
                    if (previousState[up][left] != 0) stateCounter[previousState[up][left]]++;
                    if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;

                    if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
                    if (previousState[down][right] != 0) stateCounter[previousState[down][right]]++;

                    if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
                    if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;
                }
                else
                {
                    if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
                    if (previousState[down][left] != 0) stateCounter[previousState[down][left]]++;

                    if (previousState[up][right] != 0) stateCounter[previousState[up][right]]++;
                    if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;

                    if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
                    if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;
                }
            }
            else if (neighborhood == "PentagonalRandom")
            {
                int randed = rand.Next(0, 5);
                if (randed == 0)
                {
                    if (previousState[up][left] != 0) stateCounter[previousState[up][left]]++;
                    if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
                    if (previousState[down][left] != 0) stateCounter[previousState[down][left]]++;
                    if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
                    if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;
                }
                else if (randed == 1)
                {
                    if (previousState[up][right] != 0) stateCounter[previousState[up][right]]++;
                    if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
                    if (previousState[down][right] != 0) stateCounter[previousState[down][right]]++;
                    if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
                    if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;
                }
                else if (randed == 2)
                {
                    if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
                    if (previousState[down][left] != 0) stateCounter[previousState[down][left]]++;
                    if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
                    if (previousState[down][right] != 0) stateCounter[previousState[down][right]]++;
                    if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;
                }
                else
                {
                    if (previousState[up][left] != 0) stateCounter[previousState[up][left]]++;
                    if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
                    if (previousState[up][right] != 0) stateCounter[previousState[up][right]]++;
                    if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
                    if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
                }
            }

            int myState = previousState[rowToCheck][columnToCheck];


            int energy = 0;
            for (int i = 0; i < stateCounter.Count; i++)
            {
                if (stateCounter[i] > 0 && i != myState)
                {
                    energy += stateCounter[i];
                }
            }

            int newState = myState;

                for (int i = 0; i < stateCounter.Count; i++)
                {
                    int newEnergy = 0;
                    int tempState = i;
                    if (stateCounter[tempState] != 0)
                    {
                        for (int j = 0; j < stateCounter.Count; j++)
                        {
                            if (stateCounter[j] > 0 && j != tempState)
                            {
                                newEnergy+= stateCounter[j];
                            }
                        }

                        if ((newEnergy - energy) <= 0)
                        {
                            energy = newEnergy;
                            newState = i;
                        }
                        else if ((newEnergy - energy) > 0)
                        {
                            double probability = Math.Exp(-(newEnergy - energy) / ktParameter)*100;
                            double randedNumber = rand.NextDouble()*100;
                            bool trueFalse = (randedNumber < probability);

                            if(trueFalse)
                            {
                                energy = newEnergy;
                                newState = i;
                            }
                        }
                        
                    }
                }


          
            
                //previousState[rowToCheck][columnToCheck] = newState;
                currentState[rowToCheck][columnToCheck] = newState;
                graphics.FillRectangle(brushesList[newState], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);
            
        }

        private void energy(int rowToCheck, int columnToCheck)
        {
            List<int> stateCounter = Enumerable.Repeat<int>(0, nextGrainState).ToList();

            int left = columnToCheck - 1;
            int right = columnToCheck + 1;
            int up = rowToCheck - 1;
            int down = rowToCheck + 1;
            if (!isPeriodic)
            {
                if (left < 0) left = 0;
                if (right >= columns) right = columns - 1;
                if (up < 0) up = 0;
                if (down >= rows) down = rows - 1;
            }
            else
            {
                if (left < 0) left = columns - 1;
                if (right >= columns) right = 0;
                if (up < 0) up = rows - 1;
                if (down >= rows) down = 0;
            }

            if (previousState[up][left] != 0) stateCounter[previousState[up][left]]++;
            if (previousState[rowToCheck][left] != 0) stateCounter[previousState[rowToCheck][left]]++;
            if (previousState[down][left] != 0) stateCounter[previousState[down][left]]++;

            if (previousState[up][right] != 0) stateCounter[previousState[up][right]]++;
            if (previousState[rowToCheck][right] != 0) stateCounter[previousState[rowToCheck][right]]++;
            if (previousState[down][right] != 0) stateCounter[previousState[down][right]]++;

            if (previousState[up][columnToCheck] != 0) stateCounter[previousState[up][columnToCheck]]++;
            if (previousState[down][columnToCheck] != 0) stateCounter[previousState[down][columnToCheck]]++;

            int myState = previousState[rowToCheck][columnToCheck];
            int energy = 0;
            for (int i = 0; i < stateCounter.Count; i++)
            {
                if (stateCounter[i] > 0 && i != myState)
                {
                    energy += stateCounter[i];
                }
            }


            currentState[rowToCheck][columnToCheck] = myState;
            if(energy>0)
                graphics.FillRectangle(brushesList[4], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);
            else
                graphics.FillRectangle(brushesList[100], columnToCheck * cellSize, rowToCheck * cellSize, cellSize, cellSize);

        }

        public void generateRandomGrains(int count)
        {
            var randomGenerator = new Random();
            for (int i = 0; i < count; i++)
            {
                int x = randomGenerator.Next(rows-1);
                int y = randomGenerator.Next(columns-1);
                if (currentState[x][y] == 0)
                    addGrain(x, y);
                else
                    i--;
            }
        }

        public void addGrain(int row, int col)
        {
            currentState[row][col] = nextGrainState;
            graphics.FillRectangle(brushesList[nextGrainState], col * cellSize, row * cellSize, cellSize, cellSize);
            Grain newGrain = new Grain(row, col, nextGrainState);
            //centersOfGravityX[row][col] = newGrain.centerOfGravityX;
            //centersOfGravityY[row][col] = newGrain.centerOfGravityY;
            grains.Add(newGrain);
            nextGrainState++;
            RefreshBitmap();
        }


        public void AddSeedOfSpecifiedType(int row, int col, int type)
        {
            currentState[row][col] = type;
            graphics.FillRectangle(brushesList[type], col * cellSize, row * cellSize, cellSize, cellSize);
            RefreshBitmap();
        }

        public void Resize(int rows, int cols, int cellSize, Bitmap bitmap, Graphics graphics)
        {
            this.rows = rows;
            this.columns = cols;
            this.cellSize = cellSize;
            this.bitmap = bitmap;
            this.graphics = graphics;
            previousState= new List<List<int>>();
            for (int i = 0; i < rows; i++)
            {
                currentState.Add(Enumerable.Repeat<int>(0, columns).ToList());
            }
            clearAll();
        }

        public void clearAll()
        {
            nextGrainState = 1;
            grains = new List<Grain>();
            clearCurrentState();
        }

        private void clearCurrentState()
        {
            currentState = new List<List<int>>();
            for (int i = 0; i < rows; i++)
            {
                currentState.Add(Enumerable.Repeat<int>(0, columns).ToList());
            }

        }
    }

}
