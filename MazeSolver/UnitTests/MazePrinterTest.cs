using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace MazeSolver.UnitTests
{
    [TestFixture]
    class MazePrinterTest
    {
        [Test]
        public void Test_PrintMaze()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);
            var escapeRoute = MazeRunner.SolveMaze(mazePuzzle.Maze, mazePuzzle.StartPoint, mazePuzzle.EndPoint);
            var strMaze = MazePrinter.PrintMaze(mazePuzzle.Maze, escapeRoute);
            Console.WriteLine(strMaze);
        }
    }
}
