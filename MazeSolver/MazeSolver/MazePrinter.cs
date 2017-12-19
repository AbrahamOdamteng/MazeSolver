using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MazeSolver
{
    static class MazePrinter
    {
        /// <summary>
        /// Print the maze into the format described in README.txt
        /// </summary>
        /// <param name="maze">the maze to print</param>
        /// <param name="route">The route from the startPoint to the endPoint</param>
        /// <returns>A string representation of the array and the route taken through it</returns>
        static internal string  PrintMaze(int[,] maze, IEnumerable<Point> route)
        {
            var ylen = maze.GetLength(0);
            var xlen = maze.GetLength(1);
            var strMaze = new string[ylen, xlen];

            for (int y = 0; y < ylen; y++)
            {
                for (int x = 0; x < xlen; x++)
                {
                    var cell = maze[y, x];
                    if (cell == 1)strMaze[y, x] = "#";
                }
            }

            var startPoint = route.First();
            strMaze[startPoint.Y, startPoint.X] = "S";

            var endPoint = route.Last();
            strMaze[endPoint.Y, endPoint.X] = "E";

            foreach(var point in route)
            {
                if(strMaze[point.Y,point.X] == null)
                {
                    strMaze[point.Y, point.X] = "X";
                }
            }

            var sb = new StringBuilder();
            for (int y = 0; y < ylen; y++)
            {
                for (int x = 0; x < xlen; x++)
                {
                    var cellValue = strMaze[y, x];
                    if(cellValue == null)
                    {
                        sb.Append(" ");
                    }
                    else
                    {
                        sb.Append(cellValue);
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
