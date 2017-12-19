using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Drawing;
using System.IO;

namespace MazeSolver.UnitTests
{
    [TestFixture]
    class MazeRunnerUnitTests
    {

        [Test]
        public void Test_IsValidPosition_True()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);


            //Empty Space========================================================
            Assert.That(
                () => MazeRunner.IsValidPosition(mazePuzzle.Maze, new Point(1, 1)),
                Is.EqualTo(true));

            Assert.That(
                () => MazeRunner.IsValidPosition(mazePuzzle.Maze, new Point(3, 4)),
                Is.EqualTo(true));

        }

        [Test]
        public void Test_IsValidPosition_False()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);

            //Negative Numbers=====================================================
            Assert.That(
                () => MazeRunner.IsValidPosition(mazePuzzle.Maze, new Point(-1, 3)),
                Is.EqualTo(false));

            Assert.That(
                () => MazeRunner.IsValidPosition(mazePuzzle.Maze, new Point(3, -1)),
                Is.EqualTo(false));

            Assert.That(
                () => MazeRunner.IsValidPosition(mazePuzzle.Maze, new Point(-5, -9)),
                Is.EqualTo(false));

            //Large Numbers=====================================================
            Assert.That(
                () => MazeRunner.IsValidPosition(mazePuzzle.Maze, new Point(0, 6)),
                Is.EqualTo(false));

            Assert.That(
                () => MazeRunner.IsValidPosition(mazePuzzle.Maze, new Point(5, 0)),
                Is.EqualTo(false));

            Assert.That(
                () => MazeRunner.IsValidPosition(mazePuzzle.Maze, new Point(5, 6)),
                Is.EqualTo(false));

            //Walls================================================================
            Assert.That(
                () => MazeRunner.IsValidPosition(mazePuzzle.Maze, new Point(0, 0)),
                Is.EqualTo(false));
        }

        [Test]
        public void Test_Move()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);

            var currentPos = new Point(1, 1);

            //North----------------------------------
            Assert.That(
                () => MazeRunner.Move(mazePuzzle.Maze, currentPos, MazeRunner.Direction.North),
                Is.EqualTo(null));

            //East----------------------------------
            Assert.That(
                () => MazeRunner.Move(mazePuzzle.Maze, currentPos, MazeRunner.Direction.East),
                Is.EqualTo(new Point(2,1)));

            //South----------------------------------
            Assert.That(
                () => MazeRunner.Move(mazePuzzle.Maze, currentPos, MazeRunner.Direction.South),
                Is.EqualTo(new Point(1, 2)));

            //West----------------------------------
            Assert.That(
                () => MazeRunner.Move(mazePuzzle.Maze, currentPos, MazeRunner.Direction.West),
                Is.EqualTo(null));
        }


        [Test]
        public void Test_MoveInAnyValidDirection_Exception()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);

            Assert.That(
                () => MazeRunner.MoveInAnyValidDirection(mazePuzzle.Maze, new Point(0, 0)),
                Throws.ArgumentException.With.Property("Message")
                .EqualTo("Current position {X=0,Y=0} is invalid for the current maze"));
        }

        [Test]
        public void Test_MoveInAnyValidDirection()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);

            var result = MazeRunner.MoveInAnyValidDirection(mazePuzzle.Maze, new Point(1, 1));
            Assert.That(result, Is.EqualTo(new Point(2,1)));
        }

        [Test]
        public void Test_MarkCellAsVisited()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);

            Assert.That(
                () => MazeRunner.MarkCellAsVisited(mazePuzzle.Maze, new Point(0, 0)),
                Throws.ArgumentException.With.Property("Message").EqualTo("Impossible to visit a wall"));

            Assert.That(mazePuzzle.Maze[1, 1], Is.EqualTo(0));
            MazeRunner.MarkCellAsVisited(mazePuzzle.Maze, new Point(1, 1));
            Assert.That(mazePuzzle.Maze[1, 1], Is.EqualTo(2));
        }
    }
}
