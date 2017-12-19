using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MazeSolver
{
    static class MazePrinter
    {

        static internal string  PrintMaze(int[,] maze, IEnumerable<Point> escapeRoute)
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

            var startPoint = escapeRoute.First();
            strMaze[startPoint.Y, startPoint.X] = "S";

            var endPoint = escapeRoute.Last();
            strMaze[endPoint.Y, endPoint.X] = "E";

            foreach(var point in escapeRoute)
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
