﻿namespace Geronimus.UnitTests
{
    using System.Collections.Generic;
    using BusinessLogic;
    using DataObjects;
    using NUnit.Framework;

    public class GeronimusTest
    {
        private readonly double[][] passengerFlow;
        private readonly TestSettings settings = new TestSettings();
        private Node[][] expectedNodesMatrix2;
        private double[][] inputMatrix2;

        public GeronimusTest()
        {
            passengerFlow = MatrixLoader.Load(settings.PassengerFlow);
        }

        [SetUp]
        public void SetUp()
        {
            const double infinity = double.PositiveInfinity;

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
        public void TestCalculateAllPaths()
        {
            string waitingTimeMatrixPath = settings.WaitingTimeMatrix;
            double[] watingTime = MatrixLoader.LoadVector(waitingTimeMatrixPath);

            var parameter = new GeronimusParameter
                                {
                                    WaitingTime = watingTime,
                                    PassengerFlow = passengerFlow,
                                    Capacity = 60,
                                    TransferTime = 2.5,
                                    MaxInterval = 12,
                                    PassangerArrivalCoeficient = 1.1,
                                    TimeInterval = 60,
                                    UnuniformityCoeficient = 0.5,
                                    InitialMatrix = inputMatrix2,
                                    NodesMatrix = expectedNodesMatrix2,
                                };

            IList<IList<int>> expectedPaths = new List<IList<int>>
                                                  {
                                                      new List<int> {2, 4, 0, 6, 1},
                                                      new List<int> {3, 6, 1}
                                                  };

            IList<IList<int>> expectedSmallPaths = new List<IList<int>>
                                                       {
                                                           new List<int> {0, 5},
                                                           new List<int> {4, 7},
                                                       };

            ShortestPathsResult result = Geronimus.CalculateShortestPaths(parameter);
            CollectionAssert.AreEqual(expectedPaths, result.ShortestPaths);
            CollectionAssert.AreEqual(expectedSmallPaths, result.NeighbouringPaths);
        }

        [Test]
        public void TestCalculateTheTimeSpentByAllPassengersInTrafic()
        {
            double[][] initialMatrix = MatrixLoader.Load(settings.ExpectedInitialMatrix);
            Node[][] nodesMatrix = FloydAlgorithm.Process(initialMatrix);
            double sum = Geronimus.CalculateOverallTimeInTrafic(nodesMatrix, passengerFlow);

            Assert.That(sum, Is.EqualTo(338786));
        }

        [Test]
        public void TestPassengerFlowMinMaxSums()
        {
            double proportion = Geronimus.CalculatePassengerFlowProportion(passengerFlow);

            Assert.That(proportion, Is.EqualTo(0.41314878892733564d));
        }

        [Test]
        public void CalcuateTotalWaitingTime()
        {
            const double expectedTime = 359135.34256055363d;

            const int unitCapacity = 60;
            const int routesNumber = 8;
            const double coeficient = 0.41314878892733564d;
            const double totalTime = 338786;
            double actualTime = Geronimus.CalculateTotalWaitingTime(unitCapacity, routesNumber, coeficient, totalTime);

            Assert.That(actualTime, Is.EqualTo(expectedTime));
        }

        [Test]
        public void TestStep5()
        {
            const double expectedTime = 359135.34256055363d;
            var initialPathsList = new List<IList<int>>
                                       {
                                           new List<int> {2, 4, 0, 6, 1},
                                           new List<int> {3, 6, 1},
                                           new List<int> {0, 5},
                                           new List<int> {4, 7},
                                       };

            double[][] initialMatrix = MatrixLoader.Load(settings.InitialMatrixPath);
            double[] waitingTime = MatrixLoader.LoadVector(settings.WaitingTimeMatrix);
            double[][] resultedInitialMatrix = MatrixLoader.BuildInitialMatrix(initialPathsList, initialMatrix);
            Node[][] nodesMatrix = FloydAlgorithm.Process(resultedInitialMatrix);

            double time = Geronimus.CalculateOverallTimeInTrafic(nodesMatrix, passengerFlow);
            double w = Geronimus.CalculatePassengerFlowProportion(passengerFlow);
            double totalTime = Geronimus.CalculateTotalWaitingTime(60, 8, w, time);

            Assert.That(totalTime, Is.EqualTo(expectedTime));
        }
    }
}