using System;
using System.Collections.Generic;
using System.Threading;

namespace ConwayGameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            char[][] gameMatrix = new char[][] { 
                new char[] { '0', '1', '0', '0' },
                new char[] { '0', '0', '1', '0' },
                new char[] { '0', '1', '0', '0' },
                new char[] { '0', '0', '0', '0' }
            };

            for (int i = 0; i < 320; i++)
            {
                PrintMatrix(gameMatrix);
                Thread.Sleep(TimeSpan.FromSeconds(.5));
                gameMatrix = Step(gameMatrix);
                Console.Clear();
            }
        }

        static char[][] Step(char[][] matrix)
        {
            char[][] nextMatrix = new char[matrix.Length][];

            for(int i = 0; i < matrix.Length; i++)
            {
                nextMatrix[i] = new char[matrix.Length];
                for(int j = 0; j < matrix.Length; j++)
                {
                    nextMatrix[i][j] = NextState(i, j, matrix);
                }
            }

            return nextMatrix;
        }

        static char NextState(int row, int col, char[][] matrix)
        {
            List<char> liveNeighbours = new List<char>();
            char cell = matrix[row][col];

            var minRow = row - 1 < 0 ? row : row - 1;
            var maxRow = row + 1 < matrix[row].Length ? row + 1 : row;
            var minCol = col - 1 < 0 ? col : col - 1;
            var maxCol = col + 1 >= matrix[col].Length ? col : col + 1;

            for(int i = minRow; i <= maxRow; i++)
            {
                for(int j = minCol; j <= maxCol; j++)
                {
                    if (matrix[i][j] == '1' && (i != row || j != col))
                        liveNeighbours.Add(matrix[i][j]);
                }
            }

            var liveNeighboursCount = liveNeighbours.Count;

            if(cell == '1')
            {
                if (liveNeighboursCount < 2)
                    return '0';
                if (liveNeighboursCount >= 2 && liveNeighboursCount <= 3)
                    return '1';
                return '0';
            }
            else
            {
                if (liveNeighboursCount == 3)
                    return '1';

                return '0';
            }
        }

        static void PrintMatrix(char[][] matrix)
        {
            for(int i = 0; i < matrix.Length; i++)
            {
                Console.WriteLine(string.Join(' ', matrix[i])); 
            }
        }
    }
}
