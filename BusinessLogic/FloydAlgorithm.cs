namespace Geronimus.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataObjects;

    public static class FloydAlgorithm
    {
        public static Node[][] Process(double[][] inputMatrix)
        {
            Node[][] result = InitMatrix(inputMatrix);
            
            for (int k = 0; k < result.Length; k++)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    for (int j = 0; j < result.LongLength; j++)
                    {
                        if (i == j ||
                            result[i][j].ShortestPath <= result[i][k].ShortestPath + result[k][j].ShortestPath)
                        {
                            continue;
                        }
                        result[i][j].ShortestPath = result[i][k].ShortestPath + result[k][j].ShortestPath;
                        result[i][j].Next = k == 0 ? -1 : k;
                    }
                }
            }
            return result;
        }

        private static IList<int> GetPath(IList<Node[]> nodes, int startPoint, int endPoint, IList<int> paths)
        {
            if (nodes[startPoint][endPoint].ShortestPath == double.PositiveInfinity ||
                nodes[startPoint][endPoint].Next == 0)
            {
                return paths;
            }

            int intermediate = nodes[startPoint][endPoint].Next == -1 ? 0 : nodes[startPoint][endPoint].Next;

            GetPath(nodes, startPoint, intermediate, paths);

            paths.Add(intermediate == -1 ? 0 : intermediate);

            return GetPath(nodes, intermediate, endPoint, paths);
        }

        public static IList<int> CalculatePath(Node[][] nodes, int startPoint, int endPoint)
        {
            IList<int> paths = GetPath(nodes, startPoint, endPoint, new List<int>());

            paths.Insert(0, startPoint);
            paths.Insert(paths.Count, endPoint);

            return paths;
        }

        public static Node[][] Process(double[][] inputMatrix, double[] waitingTime, IEnumerable<IList<int>> shortestPaths)
        {
            Node[][] result = InitMatrix(inputMatrix);
            for (int l = 0; l < 2; l++)
            {
                for (int k = 0; k < result.Length; k++)
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        for (int j = 0; j < result.LongLength; j++)
                        {
                            if (i == j)
                            {
                                continue;
                            }

                            var pathLength = GetPathLength(result, waitingTime[k], k, i, j, shortestPaths);
                            if (result[i][j].ShortestPath <= pathLength)
                            {
                                continue;
                            }
                            result[i][j].ShortestPath = pathLength;
                            result[i][j].Next = k == 0 ? -1 : k;
                        }
                    }
                }
            }
            return result;
        }

        private static double GetPathLength(Node[][] nodes, double waitingTime, int k, int i, int j, IEnumerable<IList<int>> shortestPaths)
        {
            if (shortestPaths.Any(path => path.Contains(i) && path.Contains(j) && path.Contains(k)))
            {
                return nodes[i][k].ShortestPath + nodes[k][j].ShortestPath;
            }

            return nodes[i][k].ShortestPath + waitingTime + nodes[k][j].ShortestPath;
        }

        private static Node[][] InitMatrix(double[][] inputMatrix)
        {
            var result = new Node[inputMatrix.Length][];

            for (int i = 0; i < inputMatrix.Length; i++)
            {
                result[i] = new Node[inputMatrix.Length];
                for (int j = 0; j < inputMatrix.Length; j++)
                {
                    result[i][j] = new Node(i == j ? 0 : inputMatrix[i][j]);
                }
            }
            return result;
        }
    }
}