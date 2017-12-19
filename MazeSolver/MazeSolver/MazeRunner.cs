using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MazeSolver
{
    static class MazeRunner
    {
        internal enum Direction{North, East, South, West };


        /// <summary>
        /// Find a path through <paramref name="maze"/>, 
        /// starting at <paramref name="startPoint"/>,
        /// and ending at <paramref name="endPoint"/>
        /// </summary>
        /// <param name="maze">The current maze</param>
        /// <param name="startPoint">The starting point within the maze</param>
        /// <param name="endPoint">The destination point within the maze</param>
        /// <returns>An IEnumerable<Point> representing the path from <paramref name="startPoint"/> to <paramref name="endPoint"/></returns>
        internal static IEnumerable<Point> SolveMaze(int[,] maze, Point startPoint, Point endPoint)
        {
            var breadCrumbs = new Stack<Point>();
            breadCrumbs.Push(startPoint);

            if (startPoint == endPoint) return breadCrumbs;

            MarkCellAsVisited(maze, startPoint);

            while (breadCrumbs.Any())
            {
                var newPos = MoveInAnyValidDirection(maze, breadCrumbs.Peek());
                if(newPos == null)
                {
                    if (breadCrumbs.Any())
                    {
                        var currentPos = breadCrumbs.Pop();
                    }
                }
                else
                {
                    MarkCellAsVisited(maze, (Point)newPos);
                    breadCrumbs.Push((Point)newPos);
                }

                if (breadCrumbs.Any() && breadCrumbs.Peek() == endPoint)
                {
                    return new List<Point>(breadCrumbs.Reverse());
                }
            }
            return null;
        }

        /// <summary>
        /// In the context of <paramref name="maze"/>,
        /// starting at position <paramref name="currentPos"/>,
        /// Move in any available direction.
        /// </summary>
        /// <param name="maze">The current maze</param>
        /// <param name="currentPos">The current position within the maze</param>
        /// <returns>A Point representing the new location, or null if there where no legal moves</returns>
        internal static Point? MoveInAnyValidDirection(int[,] maze, Point currentPos)
        {
            if (!IsLegalPosition(maze, currentPos))
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

        /// <summary>
        /// In the context of <paramref name="maze"/>, 
        /// starting at position <paramref name="currentPos"/>,
        /// attempt to move in direction <paramref name="direction"/>
        /// </summary>
        /// <param name="maze">The current maze</param>
        /// <param name="currentPos">The position to move from</param>
        /// <param name="direction">The direction to move in</param>
        /// <returns>A Point representing the new location, or null if the move was not legal</returns>
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
            if (IsUnvisitedPosition(maze, newPos)) return newPos;

            return null;
        }


        /// <summary>
        /// Determines if the location given by 'pos' is a legal position.
        /// </summary>
        /// <param name="maze">The current maze</param>
        /// <param name="pos">The location in the maze that is to be checked</param>
        /// <returns>return true if pos is within the boundaries of the maze and pos is not a wall.
        /// False otherwise</returns>
        internal static bool IsLegalPosition(int[,] maze, Point pos)
        {
            if (pos.X < 0 || pos.Y < 0) return false;
            if (pos.Y >= maze.GetLength(0) || pos.X >= maze.GetLength(1)) return false;

            var val = maze[pos.Y, pos.X];
            if (val == 1) return false;//You are standing in a wall.

            return true;
        }

        /// <summary>
        /// Determines if the specified position 'pos' has not been visited in the maze.
        /// </summary>
        /// <param name="maze">the current maze</param>
        /// <param name="pos">the position to check</param>
        /// <returns>true if the value at pos is zero, false otherwise</returns>
        internal static bool IsUnvisitedPosition(int[,] maze, Point pos)
        {
            if (!IsLegalPosition(maze, pos)) return false;

            var val = maze[pos.Y, pos.X];
            if (val == 0) return true;
            return false;
        }


        /// <summary>
        /// Mark a position in the maze as visited by setting the value of that cell to 2.
        /// </summary>
        /// <param name="maze">the maze to be modified</param>
        /// <param name="pos">the position in the maze that is to be modified</param>
        /// <exception cref="ArgumentException">if the position to be marked is a wall</exception>
        internal static void MarkCellAsVisited(int[,] maze, Point pos)
        {
            if (maze[pos.Y, pos.X] == 1) throw new ArgumentException("Impossible to visit a wall");
            maze[pos.Y, pos.X] = 2;
        }
    }
}
