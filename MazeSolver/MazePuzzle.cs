using System;
using System.Drawing;
using System.IO;
using System.Linq;
namespace MazeSolver
{
    public class MazePuzzle
    {
        internal Point StartPoint { get; set; }
        internal Point EndPoint { get; set; }
        internal int[,] Maze { get; set; }

        public MazePuzzle(string mazeFilePath)
        {
            var lines = File.ReadAllLines(mazeFilePath);
            var mazeSize = ParseMazeParameters(lines[0]);
            StartPoint = ParseMazeParameters(lines[1]);
            EndPoint = ParseMazeParameters(lines[2]);

            Maze = LoadMaze(lines.Skip(3).ToArray(), mazeSize.X, mazeSize.Y);
        }

        internal static int[,] LoadMaze(string[] mazeArray, int mazeWidth, int mazeHeight)
        {
            if(mazeWidth < 1) throw new ArgumentException("Maze width must be greater than zero");
            if(mazeHeight < 1) throw new ArgumentException("Maze Height must be greater than zero");

            if (mazeArray.Length != mazeHeight)
            {
                throw new ArgumentException("Maze Height is incorrect");
            }

            var maze = new int[mazeWidth, mazeHeight];

            for(int x = 0; x < mazeWidth; x++)
            {
                var rowString = mazeArray[x];
                var rowArray = rowString.Split();
                if (rowArray.Length != mazeWidth)
                {
                    throw new ArgumentException(string.Format("row {0} is invalid due to length", x));
                }

                for (int y = 0; y < mazeHeight; y++)
                {
                    maze[x, y] = Convert.ToInt32(rowArray[y]);
                }
            }
            return maze;
        }

        internal static Point ParseMazeParameters(string input)
        {
            var inputArray = input.Split();
            if (inputArray.Length != 2)
            {
                throw new ArgumentException("Input string must contain only two words separated by a space");
            }

            var xValue = Convert.ToInt32(inputArray[0]);
            var yValue = Convert.ToInt32(inputArray[1]);

            return new Point(xValue, yValue);
        }
    }
}
