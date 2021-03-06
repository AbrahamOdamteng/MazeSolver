﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace MazeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var mazeFilePath = Path.Combine(Environment.CurrentDirectory, @"TestMazes\large_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);

            var escapeRoute = MazeRunner.SolveMaze(mazePuzzle.Maze, mazePuzzle.StartPoint, mazePuzzle.EndPoint);
            var strMaze = MazePrinter.PrintMaze(mazePuzzle.Maze, escapeRoute);
            Console.WriteLine(strMaze);
            Console.Read();
        }
    }
}
