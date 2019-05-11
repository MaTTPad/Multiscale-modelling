using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace Cellular_Automaton
{

    public partial class MainWindow : Window
    {
        CellularAutomaton automaton1D;
        IntegerUpDown rule;
        IntegerUpDown cellSize;
        IntegerUpDown gridHeight;
        IntegerUpDown gridWidth;

        int x;
        int y;
        int automatonCellSize;

        public MainWindow()
        {
            InitializeComponent();

            gridHeight = new IntegerUpDown();
            gridHeight.AllowSpin = true;
            gridHeight.Maximum = 400;
            gridHeight.Minimum = 100;
            gridHeight.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            gridHeight.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            gridHeight.Margin = new Thickness(220, 25, 0, 0);
            gridHeight.Value = 400;

            gridWidth = new IntegerUpDown();
            gridWidth.AllowSpin = true;
            gridWidth.Maximum = 800;
            gridWidth.Minimum = 100;
            gridWidth.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            gridWidth.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            gridWidth.Margin = new Thickness(220, 55, 0, 0);
            gridWidth.Value = 799;

            rule = new IntegerUpDown();
            rule.AllowSpin = true;
            rule.Maximum = 255;
            rule.Minimum = 0;
            rule.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            rule.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            rule.Margin = new Thickness(90, 25, 0, 0);
            rule.Value = 90;

            cellSize = new IntegerUpDown();
            cellSize.AllowSpin = true;
            cellSize.Maximum = 20;
            cellSize.Minimum = 1;
            cellSize.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            cellSize.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            cellSize.Margin = new Thickness(90, 55, 0, 0);
            cellSize.Value = 10;

            this.mainGrid.Children.Add(gridHeight);
            this.mainGrid.Children.Add(gridWidth);
            this.mainGrid.Children.Add(rule);
            this.mainGrid.Children.Add(cellSize);
            y = 0;
        }


        private void fillCells(int canvasHeight, int canvasWidth)
        {
            for (x = 0; x < (canvasWidth / automatonCellSize)&& y< canvasHeight; x++)
            {
                char[] currentCells = automaton1D.getCells();
                Rectangle rectangle = new Rectangle();
                rectangle.Width = automatonCellSize;
                rectangle.Height = automatonCellSize;
                if (currentCells[x] == '1')  
                    rectangle.Fill = new SolidColorBrush(Colors.White);
                else
                     rectangle.Fill = new SolidColorBrush(Colors.Orange);

                Canvas.SetTop(rectangle, y);
                Canvas.SetLeft(rectangle, x * automatonCellSize);
                myCanvas.Children.Add(rectangle);
            }
            automaton1D.createNextGeneration();
            y += automatonCellSize;
        }

        private void drawCA(object sender, RoutedEventArgs e)
        {
            myCanvas.Children.Clear();

            this.y = 0;
            int ruleNumber = (int)rule.Value;
            int canvasHeight = (int)gridHeight.Value;
            int canvasWidth = (int)gridWidth.Value;

            automaton1D = new CellularAutomaton(canvasWidth, ruleNumber, (int)cellSize.Value);
            automatonCellSize = automaton1D.getSize();

            for(int i=0;i< canvasHeight; i++)
                fillCells(canvasHeight, canvasWidth);

        }
    }
}
