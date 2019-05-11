
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Collections;
using System;
using System.Windows.Media;

namespace GameOfLife
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            LifeGrid.RowDefinitions.Clear();
            LifeGrid.ColumnDefinitions.Clear();
            LifeGrid.Children.Clear();
            int gridWidth = 600;
            int gridHeight = 600;

            for (int i = 0; i < gridHeight / 20; i++)
            {
                RowDefinition row = new RowDefinition();
                LifeGrid.RowDefinitions.Add(row);

            }

            for (int i = 0; i < gridWidth / 20; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                LifeGrid.ColumnDefinitions.Add(column);

            }


            for (int row = 0; row < LifeGrid.RowDefinitions.Count; row++)
            {
                for (int column = 0; column < LifeGrid.ColumnDefinitions.Count; column++)
                {
                    Cell cell = new Cell(row, column, false)
                    {
                        Width = LifeGrid.Width / LifeGrid.ColumnDefinitions.Count,
                        Height = LifeGrid.Height / LifeGrid.RowDefinitions.Count
                    };
                    cell.SetValue(Grid.ColumnProperty, column);
                    cell.SetValue(Grid.RowProperty, row);
                    cell.Opacity = 0;
                    LifeGrid.Children.Add(cell);
                }
            }

           
        }

        public void NextStep()
        {
           

            if (nonRadio.IsChecked == true)

            //nieperiodyczne
            {
                foreach (Cell cell in LifeGrid.Children)
                {
                    int count = 0;
                    for (int y = cell.row - 1; y <= cell.row + 1; y++)
                    {
                        if (y >= 0 && y <= LifeGrid.RowDefinitions.Count - 1)
                        {
                            for (int x = cell.column - 1; x <= cell.column + 1; x++)
                            {
                                if (x >= 0 && x <= LifeGrid.ColumnDefinitions.Count - 1)
                                {
                                    Cell neighbour = LifeGrid.Children[y * LifeGrid.ColumnDefinitions.Count + x] as Cell;
                                    if (neighbour.isAlive == true && (cell.column != x || cell.row != y))
                                        count++;
                                }
                            }
                        }
                    }
                    cell.NextStep(count);
                }
            }

            else
            {
                // periodyczne
                int gridWidth = (int)LifeGrid.Width;
                int gridHeight = (int)LifeGrid.Height;

                var maxRow = gridHeight / 20;
                var maxCol = gridWidth / 20;


                foreach (Cell cell in LifeGrid.Children)
                {
                    int count = 0;
                    int i = cell.row + maxRow;
                    int j = cell.column + maxCol;

                    /*

              int neighbour1ID;
              int neighbour2ID;
              int neighbour3ID;
              int neighbour4ID;
              int neighbour5ID;
              int neighbour6ID;
              int neighbour7ID;
              int neighbour8ID;
              int me = cell.row * maxCol + cell.column;


              if (me + 1 < maxCol*(cell.row+1))
                  neighbour1ID = me + 1;
              else
                  neighbour1ID = cell.row * maxCol;

              if (me + maxCol < maxRow*maxCol)
                  neighbour2ID = me+maxCol;
              else
                  neighbour2ID = cell.column;

              if (neighbour2ID + 1 < maxCol*(cell.row+1)+maxCol-1)
                  neighbour3ID = neighbour2ID + 1; 
              else
                  neighbour3ID = neighbour2ID -maxCol + 1;

              if (me - 1 >= cell.row * maxCol)
                  neighbour4ID = me - 1;
              else
                  neighbour4ID = me + maxCol - 1;

              if (neighbour2ID-1>=(cell.row+1)*maxCol)
                  neighbour5ID = me - 1;
              else
                  neighbour5ID = neighbour2ID+maxCol-1;

              if (cell.row==0)
                  neighbour6ID =(maxRow-1)*maxCol+cell.column;
              else
                  neighbour6ID = me-maxCol ;

              if (cell.row==0 && neighbour6ID+1<maxCol*maxRow)
                  neighbour7ID = neighbour6ID+1;
              else if (neighbour6ID+1<(cell.row-1)*maxCol+maxCol)
                  neighbour7ID = neighbour6ID+1;
              else
                  neighbour7ID = neighbour6ID - maxCol - 1;


              if (cell.row == 0 && neighbour6ID - 1 >0)
                  neighbour8ID = neighbour6ID - 1;
              else if (neighbour6ID - 1 < (cell.row - 1) * maxCol + maxCol)
                  neighbour7ID = neighbour6ID + 1;
              else
                  neighbour7ID = neighbour6ID - maxCol - 1;
                  */

                    for (int n = -1; n <= 1; n++)
                    {
                        for (int m = -1; m <= 1; m++)
                        {
                            if ((n == 0) && (m == 0)) continue;
                            int index = (i + m) % maxRow * LifeGrid.ColumnDefinitions.Count + (n + j) % maxCol;
                            Cell neighbour = LifeGrid.Children[index] as Cell;
                            if (neighbour.isAlive == true)
                                count++;
                        }
                    }
                    cell.NextStep(count);
                }
            }


        }
            
        private void UpdateCellAliveState()
        {
            foreach (Cell cell in LifeGrid.Children)
            {
                cell.isAlive = cell.isAliveInNextStep;
                if (cell.isAlive == true)
                {
                    cell.Opacity = 1;
                    cell.Background = new SolidColorBrush(Colors.Orange);
                }
                else
                    cell.Opacity = 0;
            }
        }


        private async void StartPressed(object sender, RoutedEventArgs e)
        {
            while (startButton.IsChecked == true)
            {
                int delay = (int)delaySlider.Value;
                NextStep();
                UpdateCellAliveState();
                await Task.Delay(delay);
            }
        }


        private void ResetClicked(object sender, RoutedEventArgs e)
        {
            foreach (Cell cell in LifeGrid.Children)
                cell.isAliveInNextStep = false;

            UpdateCellAliveState();
        }


        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            foreach (Cell cell in LifeGrid.Children)
                cell.isAliveInNextStep = false;
            UpdateCellAliveState();

            Random rnd = new Random();
            Cell[] cells = new Cell[LifeGrid.Children.Count / 5];
            for (int i = 0; i < LifeGrid.Children.Count / 5; i++)
            {
                int rand = rnd.Next(0, LifeGrid.Children.Count);
                cells[i] = LifeGrid.Children[rand] as Cell;
                cells[i].Opacity = 1;
                cells[i].isAliveInNextStep = true;
                cells[i].Background = new SolidColorBrush(Colors.Orange);

            }
        }

        private void GliderItem_Selected(object sender, RoutedEventArgs e)
        {
            foreach (Cell cell in LifeGrid.Children)
                cell.isAliveInNextStep = false;
            UpdateCellAliveState();



            int gridWidth = (int)LifeGrid.Width;
            int gridHeight = (int)LifeGrid.Height;

            var maxRow = gridHeight / 20;
            var maxCol = gridWidth / 20;

            int center = LifeGrid.Children.Count / 2+5;
            Cell cell1 = LifeGrid.Children[center] as Cell;
            Cell cell2 = LifeGrid.Children[center+1] as Cell; 
            Cell cell3 = LifeGrid.Children[center+maxCol] as Cell; 
            Cell cell4 = LifeGrid.Children[center+maxCol-1] as Cell;
            Cell cell5 = LifeGrid.Children[center+2*maxCol+1] as Cell; 


            cell1.Opacity = 1;
            cell1.Background = new SolidColorBrush(Colors.Orange);
            cell1.isAliveInNextStep = true;
            cell2.Opacity = 1;
            cell2.Background = new SolidColorBrush(Colors.Orange);
            cell2.isAliveInNextStep = true;
            cell3.isAliveInNextStep = true;
            cell3.Opacity = 1;
            cell3.Background = new SolidColorBrush(Colors.Orange);
            cell4.isAliveInNextStep = true;
            cell4.Opacity = 1;
            cell4.Background = new SolidColorBrush(Colors.Orange);
            cell5.isAliveInNextStep = true;
            cell5.Opacity = 1;
            cell5.Background = new SolidColorBrush(Colors.Orange);
        }

        private void OscylatorItem_Selected(object sender, RoutedEventArgs e)
        {

            foreach (Cell cell in LifeGrid.Children)
                cell.isAliveInNextStep = false;
            UpdateCellAliveState();

            int gridWidth = (int)LifeGrid.Width;
            int gridHeight = (int)LifeGrid.Height;

            var maxRow = gridHeight / 20;
            var maxCol = gridWidth / 20;

            int center = LifeGrid.Children.Count / 2 + 5;

            Cell cell1 = LifeGrid.Children[center] as Cell;
            cell1.Background = new SolidColorBrush(Colors.Orange);
            Cell cell2 = LifeGrid.Children[center+maxCol] as Cell; 
            cell2.Background = new SolidColorBrush(Colors.Orange);
            Cell cell3 = LifeGrid.Children[center+2*maxCol] as Cell;
            cell3.Background = new SolidColorBrush(Colors.Orange);

            cell1.Opacity = 1;
            cell1.isAliveInNextStep = true;
            cell2.Opacity = 1;
            cell2.isAliveInNextStep = true;
            cell3.isAliveInNextStep = true;
            cell3.Opacity = 1;
        }

        private void NiezmienneItem_Selected(object sender, RoutedEventArgs e)
        {
            foreach (Cell cell in LifeGrid.Children)
                cell.isAliveInNextStep = false;
            UpdateCellAliveState();
            int gridWidth = (int)LifeGrid.Width;
            int gridHeight = (int)LifeGrid.Height;

            var maxRow = gridHeight / 20;
            var maxCol = gridWidth / 20;

            int center = LifeGrid.Children.Count / 2 + 5;



            Cell cell1 = LifeGrid.Children[center] as Cell;
            Cell cell2 = LifeGrid.Children[center+1] as Cell;
            Cell cell3 = LifeGrid.Children[center+2*maxCol] as Cell;
            Cell cell4 = LifeGrid.Children[center + 2 * maxCol+1] as Cell;
            Cell cell5 = LifeGrid.Children[center+maxCol-1] as Cell;
            Cell cell6 = LifeGrid.Children[center + maxCol + 2] as Cell;

            cell1.Opacity = 1;
            cell1.isAliveInNextStep = true;
            cell2.Opacity = 1;
            cell2.isAliveInNextStep = true;
            cell3.isAliveInNextStep = true;
            cell3.Opacity = 1;
            cell4.isAliveInNextStep = true;
            cell4.Opacity = 1;
            cell5.isAliveInNextStep = true;
            cell5.Opacity = 1;
            cell6.isAliveInNextStep = true;
            cell6.Opacity = 1;

            cell1.Background = new SolidColorBrush(Colors.Orange);
            cell2.Background = new SolidColorBrush(Colors.Orange);
            cell3.Background = new SolidColorBrush(Colors.Orange);
            cell4.Background = new SolidColorBrush(Colors.Orange);
            cell5.Background = new SolidColorBrush(Colors.Orange);
            cell6.Background = new SolidColorBrush(Colors.Orange);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LifeGrid.RowDefinitions.Clear();
            LifeGrid.ColumnDefinitions.Clear();
            LifeGrid.Children.Clear();
            int gridWidth = System.Convert.ToInt32(heightBox.Text);
            int gridHeight = System.Convert.ToInt32(heightBox.Text);

            for (int i = 0; i < gridHeight / 20; i++)
            {
                RowDefinition row = new RowDefinition();
                LifeGrid.RowDefinitions.Add(row);
               
            }

            for (int i = 0; i < gridWidth / 20; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                LifeGrid.ColumnDefinitions.Add(column);

            }


            LifeGrid.Width = gridWidth;
            LifeGrid.Height = gridHeight;
            for (int row = 0; row < LifeGrid.RowDefinitions.Count; row++)
            {
                for (int column = 0; column < LifeGrid.ColumnDefinitions.Count; column++)
                {
                    Cell cell = new Cell(row, column, false)
                    {
                        Width = LifeGrid.Width / LifeGrid.ColumnDefinitions.Count,
                        Height = LifeGrid.Height / LifeGrid.RowDefinitions.Count
                    };
                    cell.SetValue(Grid.ColumnProperty, column);
                    cell.SetValue(Grid.RowProperty, row);
                    cell.Opacity = 0;
                    LifeGrid.Children.Add(cell);
                }
            }
        }
    }
}
