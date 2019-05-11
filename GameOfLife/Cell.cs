using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLife
{
    public class Cell : Button
    {
        public int row;
        public int column;
        public bool isAlive;
        public bool isAliveInNextStep;

        public Cell(int row, int column, bool isAlive)
        {
            this.row = row;
            this.column = column;
            this.isAlive = isAlive;
        }

        protected override void OnClick()
        {
            if (isAliveInNextStep != true)
            {
                isAliveInNextStep = true;
                this.Background = new SolidColorBrush(Colors.Orange);
                this.Opacity = 1;
            }
            else
            {
                isAliveInNextStep = false;
                this.Opacity = 0;
            }                
            
            if (isAlive == true)
            {
                isAlive = false;
                this.Opacity = 0;
            }
            else
            {
                isAlive = true;
                this.Background = new SolidColorBrush(Colors.Orange);
                this.Opacity = 1;
            }
        }

        public void NextStep(int neighboursCount)
        {
            if (isAlive == false && neighboursCount == 3)
                isAliveInNextStep = true;
            else if ((isAlive == true && neighboursCount < 2) || neighboursCount > 3)
                isAliveInNextStep = false;
        }

    }
}
