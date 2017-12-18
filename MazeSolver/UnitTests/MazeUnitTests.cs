using NUnit.Framework;
using System.Drawing;
using System.IO;
using System.Linq;
namespace MazeSolver.UnitTests
{
    [TestFixture]
    class MazeUnitTests
    {
        [Test]        
        public void Test_ParseMazeParameters()
        {
            var resOne = MazePuzzle.ParseMazeParameters("10 10");
            Assert.AreEqual(new Point(10, 10), resOne);
            
            var resTwo = MazePuzzle.ParseMazeParameters("3 9");
            Assert.AreEqual(new Point(3, 9), resTwo);
        }

        [Test]
        public void Test_ParseMazeParameters_Error()
        {
            var msg = "Input string must contain only two words separated by a space";

            Assert.That(() => MazePuzzle.ParseMazeParameters("a b c"),
                Throws.ArgumentException.With.Property("Message").EqualTo(msg));
        }

        [Test]
        public void Test_LoadMaze()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\tiny_maze.txt");
            var lines = File.ReadAllLines(mazeFilePath);

            var mazeSize = MazePuzzle.ParseMazeParameters(lines[0]);
            var result = MazePuzzle.LoadMaze(lines.Skip(3).ToArray(), mazeSize.X, mazeSize.Y);
            var expected = new int[,]
            {
                { 1, 1, 1, 1, 1 },
                { 1, 0, 1, 0, 1 },
                { 1, 0, 1, 0, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1 }
            };

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void Test_LoadMaze_Incorrect_Width_Height()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\tiny_maze.txt");
            var lines = File.ReadAllLines(mazeFilePath);

            var mazeSize = MazePuzzle.ParseMazeParameters(lines[0]);

            Assert.That(() =>
                MazePuzzle.LoadMaze(lines.Skip(3).ToArray(), mazeSize.X, 1000),
                Throws.ArgumentException.With.Property("Message")
                    .EqualTo("Maze Height is incorrect"));

            Assert.That(() =>
                MazePuzzle.LoadMaze(lines.Skip(3).ToArray(), 1000, mazeSize.Y),
                Throws.ArgumentException.With.Property("Message")
                    .EqualTo("row 0 is invalid due to length"));
        }

        [Test]
        public void Test_LoadMaze_Negative_Width_Height()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\tiny_maze.txt");
            var lines = File.ReadAllLines(mazeFilePath);

            var mazeSize = MazePuzzle.ParseMazeParameters(lines[0]);

            Assert.That(() =>
                MazePuzzle.LoadMaze(lines.Skip(3).ToArray(), -5, mazeSize.Y),
                Throws.ArgumentException.With.Property("Message")
                    .EqualTo("Maze width must be greater than zero"));

            Assert.That(() =>
                MazePuzzle.LoadMaze(lines.Skip(3).ToArray(), mazeSize.X, -99),
                Throws.ArgumentException.With.Property("Message")
                    .EqualTo("Maze Height must be greater than zero"));
        }

        [Test]
        public void Test_MazePuzzle_Constructor()
        {
            var mazeFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestMazes\tiny_maze.txt");

            var mazePuzzle = new MazePuzzle(mazeFilePath);
            
            var expected = new int[,]
            {
                { 1, 1, 1, 1, 1 },
                { 1, 0, 1, 0, 1 },
                { 1, 0, 1, 0, 1 },
                { 1, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1 }
            };

            Assert.That(mazePuzzle.StartPoint, Is.EqualTo(new Point(1, 1)));
            Assert.That(mazePuzzle.EndPoint, Is.EqualTo(new Point(3, 3)));
            Assert.That(mazePuzzle.Maze, Is.EqualTo(expected));
        }
    }
}
