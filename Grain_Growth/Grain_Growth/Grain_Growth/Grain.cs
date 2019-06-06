using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grain_Growth
{
    class Grain
    {
        public int row;
        public int col;
        public int state;
        public int energy;
        public double centerOfGravityX;
        public double centerOfGravityY;
        public double dislocationDensity;
        public bool recrystalized;
        public Random rand= new Random();

        public Grain(int row, int col, int state)
        {
            this.row = row;
            this.col = col;
            this.state = state;
            centerOfGravityX = getRandomNumber(col * 3, col * 3 + 3);
            centerOfGravityY = getRandomNumber(row * 3, row * 3 + 3);
            recrystalized = false;
            dislocationDensity = 0;
            energy = -1;
        }  


        public void setDislocationDensity(double dens)
        {
            dislocationDensity = dens;
        }
        public double getRandomNumber(double minimum, double maximum)
        {
            return rand.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
