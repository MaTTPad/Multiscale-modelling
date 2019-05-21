using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
namespace Grain_Growth
{
    class Simulation
    {
        List<Brush> brushesList;
        public int rows;
        public int columns;
        public int nextGrainState;
        public List<Grain> grains;
        public Graphics graphics;
        public List<List<int>> previousState;
        public List<List<int>> currentState;
        public List<List<double>> centersOfGravity;
        public int cellSize;
        public PictureBox lifeBox;
        public Bitmap bitmap;
        public bool isPeriodic;
        public String method;
        Random rand;
        public int radius;

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
            centersOfGravity = new List<List<double>>();

            for (int i = 0; i < rows; i++)
            {
                currentState.Add(Enumerable.Repeat<int>(0, columns).ToList());
                previousState.Add(Enumerable.Repeat<int>(0, columns).ToList());
                centersOfGravity.Add(Enumerable.Repeat<double>(0, columns).ToList());
            }

            this.cellSize = cellSize;
            brushesList = new List<Brush>();
            Random randColor = new Random();
            for (int i = 0; i < 600; i++)
            {
                Color randomColor = Color.FromArgb(randColor.Next(256), randColor.Next(256), randColor.Next(256));
                brushesList.Add(new SolidBrush(randomColor));
            }
        }


        public void setPeriodic(bool isPeriodic)
        {
            this.isPeriodic = isPeriodic;
        }

        public void setMethod(String method)
        {
            this.method = method;
        }

        public void setRadius(int radius)
        {
            this.radius = radius;
        }

        public void RefreshBitmap()
        {
            lifeBox.Image = bitmap;
        }

        public void nextStep()
        {
            previousState = currentState;
            clearCurrentState();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (previousState[i][j] == 0)
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

            for(int i=0;i<rows;i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    
                }
            }


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
            grains.Add(new Grain(row, col, nextGrainState));
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
