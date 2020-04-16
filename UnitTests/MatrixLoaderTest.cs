namespace Geronimus.UnitTests
{
    using System.Collections.Generic;
    using BusinessLogic;
    using NUnit.Framework;

    public class MatrixLoaderTest
    {
        private TestSettings settings;
        private MatrixLoader matrixLoader;

        [SetUp]
        public void Init()
        {
            settings = new TestSettings();
            matrixLoader = new MatrixLoader(TestContext.CurrentContext.TestDirectory);
        }

        [Test]
        public void TestInitialMatrixLoading()
        {
            double[][] initialMatrix = matrixLoader.Load(settings.InitialMatrixPath);
            const double infinity = double.PositiveInfinity;
            var expectedInitialMatrix = new[]
                                            {
                                                new[] {infinity, infinity, infinity, 10, 9, 16, 13, infinity},
                                                new[]
                                                    {infinity, infinity, infinity, infinity, infinity, 29, 7, infinity},
                                                new[]
                                                    {infinity, infinity, infinity, infinity, 19, infinity, infinity, 19}
                                                ,
                                                new[] {10, infinity, infinity, infinity, 10, infinity, 20, infinity},
                                                new[] {9, infinity, 19, 10, infinity, infinity, infinity, 27},
                                                new[] {16, 29, infinity, infinity, infinity, infinity, 26, 6},
                                                new[] {13, 7, infinity, 20, infinity, 26, infinity, infinity},
                                                new[] {infinity, infinity, 19, infinity, 27, 6, infinity, infinity}
                                            };
            CollectionAssert.AreEqual(expectedInitialMatrix, initialMatrix);
        }

        [Test]
        public void TestExpectedMatrixLoading()
        {
            double[][] shortestPaths = matrixLoader.Load(settings.ShortestPathsMatrix);
            var expectedShortestPathsMatrix = new[]
                                                  {
                                                      new double[] {0, 20, 28, 10, 9, 16, 13, 22},
                                                      new double[] {20, 0, 48, 27, 29, 29, 7, 35},
                                                      new double[] {28, 48, 0, 29, 19, 25, 41, 19},
                                                      new double[] {10, 27, 29, 0, 10, 26, 20, 32},
                                                      new double[] {9, 29, 19, 10, 0, 25, 22, 27},
                                                      new double[] {16, 29, 25, 26, 25, 0, 26, 6},
                                                      new double[] {13, 7, 41, 20, 22, 26, 0, 32},
                                                      new double[] {22, 35, 19, 32, 27, 6, 32, 0}
                                                  };
            CollectionAssert.AreEqual(expectedShortestPathsMatrix, shortestPaths);
        }

        [Test]
        public void TestExpectedVectorLoading()
        {
            double[] shortestPaths = matrixLoader.LoadVector(settings.WaitingTimeMatrix);
            var expectedShortestPathsMatrix = new[] {2, 4, 3, 3, 5, 2, 3, 2};
            CollectionAssert.AreEqual(expectedShortestPathsMatrix, shortestPaths);
        }

        [Test]
        public void TestPlainTextLoading()
        {
            const string expectedText = "*	*	*	10	9	16	13	*\r\n" +
                                        "*	*	*	*	*	29	7	*\r\n" +
                                        "*	*	*	*	19	*	*	19\r\n" +
                                        "10	*	*	*	10	*	20	*\r\n" +
                                        "9	*	19	10	*	*	*	27\r\n" +
                                        "16	29	*	*	*	*	26	6\r\n" +
                                        "13	7	*	20	*	26	*	*\r\n" +
                                        "*	*	19	*	27	6	*	*";

            string fileContent = matrixLoader.ReadAllText(settings.InitialMatrixPath);

            Assert.That(fileContent, Is.EqualTo(expectedText));
        }

        [Test]
        public void BuildInitialMatrixFromPathsList()
        {
            var initialPathsList = new List<IList<int>>
                                       {
                                           new List<int> {2, 4, 0, 6, 1},
                                           new List<int> {3, 6, 1},
                                           new List<int> {0, 5},
                                           new List<int> {4, 7},
                                       };

            double[][] expectedInitialMatrix = matrixLoader.Load(settings.ExpectedInitialMatrix);
            double[][] initialMatrix = matrixLoader.Load(settings.InitialMatrixPath);
            double[] waitingTime = matrixLoader.LoadVector(settings.WaitingTimeMatrix);
            double[][] actualInitialMatrix = matrixLoader.BuildInitialMatrix(initialPathsList, initialMatrix);
            Assert.That(actualInitialMatrix, Is.EqualTo(expectedInitialMatrix));
        }
    }
}