using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MazeSolver
{
    class MazeRunner
    {
        internal enum Direction{North, East, South, West };

        internal static IEnumerable<Point> SolveMaze(int[,] maze, Point startPoint, Point endPoint)
        {
            var breadCrumbs = new Stack<Point>();
            if (startPoint == endPoint) return breadCrumbs;

            breadCrumbs.Push(startPoint);
            while (breadCrumbs.Any())
            {
                var newPos = MoveInAnyValidDirection(maze, breadCrumbs.Peek());
                if(newPos == null)
                {
                    if (breadCrumbs.Any())
                    {
                        var currentPos = breadCrumbs.Pop();
                        MarkCellAsVisited(maze, currentPos);
                    }
                }
                else
                {
                    breadCrumbs.Push((Point)newPos);
                }

                if (breadCrumbs.Any() && breadCrumbs.Peek() == endPoint) return breadCrumbs;
            }
            return null;
        }

        internal static Point? MoveInAnyValidDirection(int[,] maze, Point currentPos)
        {
            if (!IsValidPosition(maze, currentPos))
            {
                var msg = string.Format("Current position {0} is invalid for the current maze", currentPos);
                throw new ArgumentException(msg);
            }

            foreach(Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var point = MazeRunner.Move(maze, currentPos, direction);
                if (point != null) return point;
            }
            return null;
        }

        internal static Point? Move(int[,] maze, Point currentPos, Direction direction)
        {
            Point newPos = new Point(currentPos.X, currentPos.Y);
            
            switch (direction)
            {
                case Direction.North:
                    newPos.Y--;
                    break;
                case Direction.East:
                    newPos.X++;
                    break;
                case Direction.South:
                    newPos.Y++;
                    break;
                case Direction.West:
                    newPos.X--;
                    break;
                default:
                    throw new ArgumentException(string.Format("Direction {0} is not recognised)", direction));
            }
            if (IsValidPosition(maze, newPos)) return newPos;

            return null;
        }

        internal static bool IsValidPosition(int[,] maze, Point pos)
        {
            if (pos.X < 0 || pos.Y < 0) return false;
            if (pos.Y >= maze.GetLength(0) || pos.X >= maze.GetLength(1)) return false;

            var val = maze[pos.Y, pos.X];
            if (val == 0) return true;
            return false;
        }

        internal static void MarkCellAsVisited(int[,] maze, Point pos)
        {
            if (maze[pos.Y, pos.X] == 1) throw new ArgumentException("Impossible to visit a wall");
            maze[pos.Y, pos.X] = 2;
        }
    }
}
