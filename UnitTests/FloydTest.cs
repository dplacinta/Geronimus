namespace Geronimus.UnitTests
{
    using System.Collections.Generic;
    using BusinessLogic;
    using DataObjects;
    using NUnit.Framework;

    public class FloydTest
    {
        private Node[][] expectedNodesMatrix;
        private Node[][] expectedNodesMatrix2;
        private double[][] inputMatrix;
        private double[][] inputMatrix2;

        [SetUp]
        public void SetUp()
        {
            const double infinity = double.PositiveInfinity;
            inputMatrix = new[]
                              {
                                  new[] {0D, infinity, 4D, 5D, infinity, infinity, 10D},
                                  new[] {infinity, 0D, 7D, 1D, infinity, infinity, infinity},
                                  new[] {4D, 7D, 0D, 4D, infinity, infinity, infinity},
                                  new[] {5D, 1D, infinity, 0D, infinity, infinity, 2D},
                                  new[] {infinity, infinity, infinity, 8D, 0D, 9D, infinity},
                                  new[] {infinity, infinity, infinity, infinity, 9D, 0D, 1D,},
                                  new[] {10D, infinity, infinity, 2D, infinity, 1D, 0D},
                              };

            inputMatrix2 = new[]
                               {
                                   new[] {infinity, infinity, infinity, 10, 9, 16, 13, infinity},
                                   new[] {infinity, infinity, infinity, infinity, infinity, 29, 7, infinity},
                                   new[] {infinity, infinity, infinity, infinity, 19, infinity, infinity, 19},
                                   new[] {10, infinity, infinity, infinity, 10, infinity, 20, infinity},
                                   new[] {9, infinity, 19, 10, infinity, infinity, infinity, 27},
                                   new[] {16, 29, infinity, infinity, infinity, infinity, 26, 6},
                                   new[] {13, 7, infinity, 20, infinity, 26, infinity, infinity},
                                   new[] {infinity, infinity, 19, infinity, 27, 6, infinity, infinity}
                               };

            expectedNodesMatrix = new[]
                                      {
                                          new[]
                                              {
                                                  new Node(0, 0), new Node(6, 3), new Node(4, 0), new Node(5, 0),
                                                  new Node(17, 6), new Node(8, 6), new Node(7, 3)
                                              },
                                          new[]
                                              {
                                                  new Node(6, 3), new Node(0, 0), new Node(7, 0), new Node(1, 0),
                                                  new Node(13, 6), new Node(4, 6), new Node(3, 3)
                                              },
                                          new[]
                                              {
                                                  new Node(4, 0), new Node(5, 3), new Node(0, 0), new Node(4, 0),
                                                  new Node(16, 6), new Node(7, 6), new Node(6, 3)
                                              },
                                          new[]
                                              {
                                                  new Node(5, 0), new Node(1, 0), new Node(8, 1), new Node(0, 0),
                                                  new Node(12, 6), new Node(3, 6), new Node(2, 0)
                                              },
                                          new[]
                                              {
                                                  new Node(13, 3), new Node(9, 3), new Node(16, 3), new Node(8, 0),
                                                  new Node(0, 0), new Node(9, 0), new Node(10, 3)
                                              },
                                          new[]
                                              {
                                                  new Node(8, 6), new Node(4, 6), new Node(11, 6), new Node(3, 6),
                                                  new Node(9, 0), new Node(0, 0), new Node(1, 0)
                                              },
                                          new[]
                                              {
                                                  new Node(7, 3), new Node(3, 3), new Node(10, 3), new Node(2, 0),
                                                  new Node(10, 5), new Node(1, 0), new Node(0, 0)
                                              },
                                      };

            expectedNodesMatrix2 = new[]
                                       {
                                           new[]
                                               {
                                                   new Node(0, 0), new Node(20, 6), new Node(28, 4), new Node(10, 0),
                                                   new Node(9, 0), new Node(16, 0), new Node(13, 0), new Node(22, 5)
                                               },
                                           new[]
                                               {
                                                   new Node(20, 6), new Node(0, 0), new Node(48, 6), new Node(27, 6),
                                                   new Node(29, 6), new Node(29, 0), new Node(7, 0), new Node(35, 5)
                                               },
                                           new[]
                                               {
                                                   new Node(28, 4), new Node(48, 6), new Node(0, 0), new Node(29, 4),
                                                   new Node(19, 0), new Node(25, 7), new Node(41, 4), new Node(19, 0)
                                               },
                                           new[]
                                               {
                                                   new Node(10, 0), new Node(27, 6), new Node(29, 4), new Node(0, 0),
                                                   new Node(10, 0), new Node(26, -1), new Node(20, 0), new Node(32, 5)
                                               },
                                           new[]
                                               {
                                                   new Node(9, 0), new Node(29, 6), new Node(19, 0), new Node(10, 0),
                                                   new Node(0, 0), new Node(25, -1), new Node(22, -1), new Node(27, 0)
                                               },
                                           new[]
                                               {
                                                   new Node(16, 0), new Node(29, 0), new Node(25, 7), new Node(26, -1),
                                                   new Node(25, -1), new Node(0, 0), new Node(26, 0), new Node(6, 0)
                                               },
                                           new[]
                                               {
                                                   new Node(13, 0), new Node(7, 0), new Node(41, 4), new Node(20, 0),
                                                   new Node(22, -1), new Node(26, 0), new Node(0, 0), new Node(32, 5)
                                               },
                                           new[]
                                               {
                                                   new Node(22, 5), new Node(35, 5), new Node(19, 0), new Node(32, 5),
                                                   new Node(27, 0), new Node(6, 0), new Node(32, 5), new Node(0, 0)
                                               },
                                       };
        }

        [Test]
        public void TestNodeCreation()
        {
            const double shortestPath = 5;
            var node = new Node(shortestPath);
            const int next = 2;
            node.Next = next;
            Assert.That(node.ShortestPath, Is.EqualTo(shortestPath));
            Assert.That(node.Next, Is.EqualTo(next));
        }

        [Test]
        public void TestFloydAlgorithmResult1()
        {
            Node[][] result = FloydAlgorithm.Process(inputMatrix);
            CollectionAssert.AreEqual(expectedNodesMatrix, result);
        }

        [Test]
        public void TestFloydAlgorithmResult2()
        {
            Node[][] result = FloydAlgorithm.Process(inputMatrix2);
            CollectionAssert.AreEqual(expectedNodesMatrix2, result);
        }

        [Test]
        public void TestCalculateShortestPath()
        {
            const int startPoint = 1;
            const int endPoint = 2;
            IList<int> expectedPath = new List<int> {startPoint, 6, 0, 4, endPoint};
            IList<int> path = FloydAlgorithm.CalculatePath(FloydAlgorithm.Process(inputMatrix2), startPoint, endPoint);

            CollectionAssert.AreEqual(expectedPath, path);
        }
    }
}