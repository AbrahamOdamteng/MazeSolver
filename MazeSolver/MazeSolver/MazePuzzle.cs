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

        /// <summary>
        /// Load an instance of the maze puzzle from mazeFilePath.
        /// </summary>
        /// <param name="mazeFilePath">The filepath of the maze to load</param>
        public MazePuzzle(string mazeFilePath)
        {
            var lines = File.ReadAllLines(mazeFilePath);
            var mazeSize = ParseMazeParameters(lines[0]);
            StartPoint = ParseMazeParameters(lines[1]);
            EndPoint = ParseMazeParameters(lines[2]);

            Maze = LoadMaze(lines.Skip(3).ToArray(), mazeSize.X, mazeSize.Y);
        }

        /// <summary>
        /// Convert the single dimension string array (string[]) representation of the maze, 
        /// into a multidemensional int (int[,]) representation.
        /// </summary>
        /// <param name="mazeArray"></param>
        /// <param name="mazeWidth">the width of the integer array </param>
        /// <param name="mazeHeight">the height of the integer array</param>
        /// <returns>A multidimensional integer array that represents the maze</returns>
        /// <exception cref="ArgumentException">If mazeWidth or mazeHeight is less than 1</exception>  
        internal static int[,] LoadMaze(string[] mazeArray, int mazeWidth, int mazeHeight)
        {
            if(mazeWidth < 1) throw new ArgumentException("Maze width must be greater than zero");
            if(mazeHeight < 1) throw new ArgumentException("Maze Height must be greater than zero");

            if (mazeArray.Length != mazeHeight)
            {
                throw new ArgumentException("Maze Height is incorrect");
            }

            var maze = new int[mazeHeight, mazeWidth];

            for (int y = 0; y < mazeHeight; y++)
            {
                var rowString = mazeArray[y];
                var rowArray = rowString.Split();
                if (rowArray.Length != mazeWidth)
                {
                    throw new ArgumentException(string.Format("row {0} is invalid due to length", y));
                }

                for (int x = 0; x < mazeWidth; x++)
                {
                    maze[y, x] = Convert.ToInt32(rowArray[x]);
                }
            }
            return maze;
        }

        /// <summary>
        /// Parses the parameters in the maze text files.
        /// </summary>
        /// <param name="input">A string of the form '# #' where each # is an integer</param>
        /// <returns>A point containing the two values in the input string</returns>
        /// <exception cref="ArgumentException">If the input string is not in the required format</exception> 
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
