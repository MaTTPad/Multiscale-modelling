using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cellular_Automaton
{
    class CellularAutomaton
    {

        char[] cells;
        char[] ruleSet;
        int size;
        int generation = 0;

        public char[] getCells()
        {
            return cells;
        }

        public int getSize()
        {
            return size; 
        }


        public CellularAutomaton(int width, int rule, int size)
        {
            this.size = size;
            cells = new char[width / size];
            cells[cells.Length / 2] = '1';
            string ruleString = decToBin(rule, 8);
            ruleSet = ruleString.ToCharArray();
            for (int i = 0; i < ruleSet.Length; i++)
            {
                if (ruleSet[i] != '1')
                {
                    ruleSet[i] = '\0';
                }
            }
        }

        public void createNextGeneration()
        {
            char leftNeighbor, cellToChange, rightNeighbor;
            char[] nextGeneration = new char[cells.Length];

            for (int i = 0; i < cells.Length; i++)
            {
                if (i - 1 < 0)
                    leftNeighbor = cells[cells.Length-1];
                else
                    leftNeighbor = cells[i - 1];

                cellToChange = cells[i];

                if (i+1>(cells.Length-1))
                    rightNeighbor = cells[0];
                else
                    rightNeighbor = cells[i + 1];

                nextGeneration[i] = Rules(leftNeighbor, cellToChange, rightNeighbor);
            }
            cells = nextGeneration;
            generation++;
        }

        public static string decToBin(int value, int length)
        {
            return (length > 1 ? decToBin(value >> 1, length - 1) : null) + "01"[value & 1];
        }

        private char Rules(char a, char b, char c)
        {
            if (a == '1' && b == '1' && c == '1')
                return ruleSet[0];
            else if (a == '1' && b == '1' && c == '\0')
                return ruleSet[1];
            else if (a == '1' && b == '\0' && c == '1')
                return ruleSet[2];
            else if (a == '1' && b == '\0' && c == '\0')
                return ruleSet[3];
            else if (a == '\0' && b == '1' && c == '1')
                return ruleSet[4];
            else if (a == '\0' && b == '1' && c == '\0')
                return ruleSet[5];
            else if (a == '\0' && b == '\0' && c == '1')
                return ruleSet[6];
            else // (a == '\0' && b == '\0' && c == '\0')
                return ruleSet[7];
        }
    }
}
