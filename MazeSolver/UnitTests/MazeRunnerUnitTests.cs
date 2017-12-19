using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MazeSolver.UnitTests
{
    [TestFixture]
    class MazeRunnerUnitTests
    {
        //IsLegalPosition#############################################################
        [Test]
        public void Test_IsLegalPosition_True()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);

            //Unvisited empty cells are legal cell positions.
            Assert.That(mazePuzzle.Maze[1, 1], Is.EqualTo(0));

            Assert.That(
                () => MazeRunner.IsLegalPosition(mazePuzzle.Maze, new Point(1, 1)),
                Is.EqualTo(true));

            MazeRunner.MarkCellAsVisited(mazePuzzle.Maze, new Point(1, 1));
            Assert.That(mazePuzzle.Maze[1, 1], Is.EqualTo(2));

            //Visited Cells are legal cell positions.
            Assert.That(
                () => MazeRunner.IsLegalPosition(mazePuzzle.Maze, new Point(1, 1)),
                Is.EqualTo(true));
        }


        [Test]
        public void Test_IsLegalPosition_False()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);

            //Wall Test
            Assert.That(
                () => MazeRunner.IsLegalPosition(mazePuzzle.Maze, new Point(0, 0)),
                Is.EqualTo(false));

            //Out of bounds Negative X value
            Assert.That(
                () => MazeRunner.IsLegalPosition(mazePuzzle.Maze, new Point(-1, 0)),
                Is.EqualTo(false));

            //Out of bounds Negative Y value
            Assert.That(
                () => MazeRunner.IsLegalPosition(mazePuzzle.Maze, new Point(0, -1)),
                Is.EqualTo(false));

            //Out of bounds Y too large
            Assert.That(
                () => MazeRunner.IsLegalPosition(mazePuzzle.Maze, new Point(0, 6)),
                Is.EqualTo(false));

            //Out of bounds X too large
            Assert.That(
                () => MazeRunner.IsLegalPosition(mazePuzzle.Maze, new Point(5, 0)),
                Is.EqualTo(false));
        }


        //IsValidUnvisitedPosition#############################################################
        [Test]
        public void Test_IsValidUnvisitedPosition_True()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);


            //Empty Space-------------------------
            Assert.That(
                () => MazeRunner.IsUnvisitedPosition(mazePuzzle.Maze, new Point(1, 1)),
                Is.EqualTo(true));

            Assert.That(
                () => MazeRunner.IsUnvisitedPosition(mazePuzzle.Maze, new Point(3, 4)),
                Is.EqualTo(true));

        }

        
        [Test]
        public void Test_IsValidUnvisitedPosition_False()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);

            //Negative Numbers-------------------------
            Assert.That(
                () => MazeRunner.IsUnvisitedPosition(mazePuzzle.Maze, new Point(-1, 3)),
                Is.EqualTo(false));

            Assert.That(
                () => MazeRunner.IsUnvisitedPosition(mazePuzzle.Maze, new Point(3, -1)),
                Is.EqualTo(false));

            Assert.That(
                () => MazeRunner.IsUnvisitedPosition(mazePuzzle.Maze, new Point(-5, -9)),
                Is.EqualTo(false));

            //Large Numbers-------------------------
            Assert.That(
                () => MazeRunner.IsUnvisitedPosition(mazePuzzle.Maze, new Point(0, 6)),
                Is.EqualTo(false));

            Assert.That(
                () => MazeRunner.IsUnvisitedPosition(mazePuzzle.Maze, new Point(5, 0)),
                Is.EqualTo(false));

            Assert.That(
                () => MazeRunner.IsUnvisitedPosition(mazePuzzle.Maze, new Point(5, 6)),
                Is.EqualTo(false));

            //Walls-------------------------
            Assert.That(
                () => MazeRunner.IsUnvisitedPosition(mazePuzzle.Maze, new Point(0, 0)),
                Is.EqualTo(false));

            //Visited Cell-------------------------
            var visitedCell = new Point(3, 4);
            MazeRunner.MarkCellAsVisited(mazePuzzle.Maze, visitedCell);
            Assert.That(mazePuzzle.Maze[visitedCell.Y, visitedCell.X], Is.EqualTo(2));

            Assert.That(
                () => MazeRunner.IsUnvisitedPosition(mazePuzzle.Maze, visitedCell),
                Is.EqualTo(false));
        }


        //Move Tests##################################################
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


        //MoveInAnyValidDirection Tests##################################################
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

        //MarkCellAsVisited Tests##################################################
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


        //SolveMaze Tests##################################################
        [Test]
        public void Test_SolveMaze_TrivialSolution()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);

            var point = new Point(1, 1);
            var result = MazeRunner.SolveMaze(mazePuzzle.Maze, point, point);

            var expected = new List<Point>()
            {
                point,
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Test_SolveMaze()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\small_maze.txt");
            var mazePuzzle = new MazePuzzle(mazeFilePath);

            var startPoint = new Point(1, 1);
            var endPoint = new Point(3, 4);
            var result = MazeRunner.SolveMaze(mazePuzzle.Maze, startPoint, endPoint);

            var expected = new List<Point>()
            {
                startPoint,
                new Point(2, 1),
                new Point(3, 1),
                new Point(3, 2),
                new Point(3, 3),
                endPoint
            };
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
