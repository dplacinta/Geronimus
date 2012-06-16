namespace Geronimus.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using DataObjects;

    public class Geronimus
    {
        private static string logFilePath;
        private static bool isLoggingEnabled;

        public Geronimus(string filePath)
        {
            logFilePath = filePath;
            isLoggingEnabled = logFilePath != null;
            if (isLoggingEnabled)
            {
                File.Delete(logFilePath);
            }
        }

        public static ShortestPathsResult CalculateShortestPaths(GeronimusParameter parameter)
        {
            IList<IList<int>> pathsList = BuildAllPossiblePaths(parameter.NodesMatrix);
            IList<IList<int>> smallPaths = GetSmallRoutes(pathsList);
            var shortestPathsResult = new ShortestPathsResult(pathsList, smallPaths);

            LogText("Pasul 2: stabilirea retelei initiale:");
            // Timpul minim se calculeaza doar din puncte intermediare
            ProcessRemainingPaths(shortestPathsResult.ShortestPaths, parameter, shortestPathsResult.AdditionalPaths);
            RemoveContainingPaths(shortestPathsResult.ShortestPaths);
            RemoveContainingRoutes(shortestPathsResult.ShortestPaths, shortestPathsResult.NeighbouringPaths);

            IList<IList<int>> neighbouringPathsCopy = new List<IList<int>>(shortestPathsResult.NeighbouringPaths);

            LogText("Pasul 3: rutele scurte");
            ProcessRemainingNeighbouringPaths(shortestPathsResult, parameter);
            RemoveSubincludedPaths(shortestPathsResult.NeighbouringPaths);


            CheckNodesAccessibility(parameter, shortestPathsResult, neighbouringPathsCopy);

            //RemoveSubincludedPaths(shortestPathsResult.AdditionalPaths);

            LogText("Pasul 4: stabilirea eficientei adaugarii de rute aditionale:");
            BuildAdditionalPathsList(shortestPathsResult, parameter);
            RemoveContainingRoutes(shortestPathsResult.ShortestPaths, shortestPathsResult.AdditionalPaths);
            //WriteToFile(shortestPathsResult.NeighbouringPaths);
            LogText("Pasul 5:");
            ProcessStep5(parameter, shortestPathsResult);

            return shortestPathsResult;
        }

        private static void CheckNodesAccessibility(GeronimusParameter parameter, ShortestPathsResult shortestPathsResult, IList<IList<int>> neighbouringPathsCopy)
        {
            IList<IList<int>> shortestPaths = shortestPathsResult.ShortestPaths.Union(shortestPathsResult.NeighbouringPaths).ToList();
            bool areNodesIsolated = !AreNodesAccessible(shortestPaths, parameter);

            if (areNodesIsolated)
            {
                LogText("Pasul 3b: verificarea nodurilor izolate");

                IEnumerable<IList<int>> remailningList = ProcessRemainingNeighbouringPaths(parameter, shortestPathsResult.ShortestPaths, neighbouringPathsCopy);
                foreach (IList<int> path in remailningList.Union(neighbouringPathsCopy))
                {
                    shortestPathsResult.NeighbouringPaths.Add(path);
                }
            }

            RemoveSubincludedPaths(shortestPathsResult.NeighbouringPaths);
        }

        private static void ProcessStep5(GeronimusParameter parameter, ShortestPathsResult shortestPathsResult)
        {
            IList<IList<int>> shortestPaths = shortestPathsResult.ShortestPaths.Union(shortestPathsResult.NeighbouringPaths).ToList();
            IList<IList<int>> additionalPath = new List<IList<int>>(shortestPathsResult.AdditionalPaths);
            Step5Cycle(shortestPathsResult, parameter, shortestPaths, additionalPath, true);
        }

        private static void Step5Cycle(ShortestPathsResult shortestPathsResult, GeronimusParameter parameter,
                                       ICollection<IList<int>> shortestPaths, IList<IList<int>> additionalPath, bool saveData)
        {
            double totalTime = Step5FirstTime(shortestPaths, parameter, saveData, shortestPathsResult);
            shortestPathsResult.TotalTime = totalTime;

            LogText(string.Format("Tipul retelei initiale: {0}", totalTime));
            IDictionary<IList<int>, double> totalTimeMap = new Dictionary<IList<int>, double>();

            if (additionalPath.Count == 0)
            {
                return;
            }

            for (int i = 0; i < additionalPath.Count; i++)
            {
                IList<int> path = additionalPath[i];
                var pathsList = new List<IList<int>>(shortestPaths) {path};
                LogText("Calculam timpul total pentru ruta aditionala: " + BuildPathString(path));
                double currentTotalTime = Step5FirstTime(pathsList, parameter, false, shortestPathsResult);
                totalTimeMap[path] = currentTotalTime;
                LogText("Timpul total = " + currentTotalTime);

                if (totalTime >= currentTotalTime)
                {
                    continue;
                }

                LogText(string.Format("Excludem ruta ({0}) din lista rutelor aditionale, deoarece timpul "
                                      + "schemei precedente ({1}) e mai mic decit timpul schemei curente ({2})",
                                      BuildPathString(path), totalTime, currentTotalTime));
                additionalPath.RemoveAt(i--);
            }

            double minTime = totalTimeMap.Values.Min();

            if (minTime >= totalTime)
            {
                return;
            }

            IList<int> pathToInclude = (totalTimeMap.Where(d => d.Value == minTime).Select(d => d.Key)).FirstOrDefault();
            if (pathToInclude != null)
            {
                LogText(string.Format("Includem ruta ({0}) in schema initiala", BuildPathString(pathToInclude)));
                shortestPaths.Add(pathToInclude);
                additionalPath.Remove(pathToInclude);
                shortestPathsResult.TotalTime = minTime;
                shortestPathsResult.ConfirmedAdditionalPaths.Add(pathToInclude);
            }
            Step5Cycle(shortestPathsResult, parameter, shortestPaths, additionalPath, false);
        }

        private static double Step5FirstTime(IEnumerable<IList<int>> shortestPaths, GeronimusParameter parameter, bool saveData, ShortestPathsResult shortestPathsResult)
        {
            double[][] resultedInitialMatrix = MatrixLoader.BuildInitialMatrix(shortestPaths, parameter.InitialMatrix);
            Node[][] resultedNodesMatrix = FloydAlgorithm.Process(resultedInitialMatrix, parameter.WaitingTime, shortestPaths);


            double time = CalculateOverallTimeInTrafic(resultedNodesMatrix, parameter.PassengerFlow);
            double w = CalculatePassengerFlowProportion(parameter.PassengerFlow);
            double totalWaitingTime = CalculateTotalWaitingTime(parameter.Capacity, shortestPaths.Count(), w, time);

            PersistData(shortestPathsResult, resultedInitialMatrix, resultedNodesMatrix, saveData);

            return totalWaitingTime;
        }

        private static void PersistData(ShortestPathsResult shortestPathsResult, double[][] resultedInitialMatrix,
                                        Node[][] resultedNodesMatrix, bool saveData)
        {
            if (!saveData)
            {
                return;
            }
            shortestPathsResult.ResultedInitialMatrix = resultedInitialMatrix;
            shortestPathsResult.ResultedNodesMatrix = resultedNodesMatrix;
        }

        private static void LogText(string text)
        {
            if (isLoggingEnabled)
            {
                File.AppendAllLines(logFilePath, new List<string> {text});
            }
        }

        private static void ProcessRemainingNeighbouringPaths(ShortestPathsResult shortPathsParam, GeronimusParameter parameter)
        {
            IList<IList<int>> pathsList = shortPathsParam.NeighbouringPaths;
            IDictionary<int, IList<int>> remainingList = new Dictionary<int, IList<int>>(pathsList.Count);
            IDictionary<int, double> calculatedValues = new Dictionary<int, double>(pathsList.Count);
            IList<int> shortestPathsUnion = GetPathsUnion(shortPathsParam.ShortestPaths);

            for (int i = 0; i < pathsList.Count; i++)
            {
                IList<int> path = pathsList[i];
                double max = Math.Max(parameter.PassengerFlow[path[0]][path[path.Count - 1]],
                                      parameter.PassengerFlow[path[path.Count - 1]][path[0]]);
                double actualTime = parameter.Capacity*parameter.TimeInterval/max;

                int node = path[shortestPathsUnion.Contains(path[0]) ? 1 : 0];
                string pathToString = BuildPathString(pathsList[i]);
                if (actualTime > parameter.MaxInterval)
                {
                    pathsList.RemoveAt(i--);

                    bool checkIntersection = CheckIntersection(shortestPathsUnion, path); //&& AreNodesAccessible(shortestPaths, parameter, path);
                    if (!checkIntersection)
                    {
                        if (calculatedValues.ContainsKey(node))
                        {
                            if (calculatedValues[node] > actualTime)
                            {
                                remainingList[node] = path;
                                calculatedValues[node] = actualTime;
                            }
/*                            else if(!calculatedValues.ContainsKey(path[1]))
                            {
                                remainingList[node] = path;
                                calculatedValues[node] = actualTime;
                            }*/
                        }
                        else
                        {
                            remainingList[node] = path;
                            calculatedValues[node] = actualTime;
                        }
                    }

                    LogText(string.Format("Ruta {0} se exclude, timpul necesar ({1}) > timpul admisibil ({2})",
                                          pathToString, actualTime, parameter.MaxInterval));
                }
                else
                {
                    shortestPathsUnion.Add(node);
                    LogText(string.Format("Ruta {0} se include, timpul necesar ({1}) < timpul admisibil ({2})",
                                          pathToString, actualTime, parameter.MaxInterval));
                }
            }

            foreach (var path in remainingList.Values)
            {
                pathsList.Add(path);
                LogText(string.Format("Ruta {0} se include, pentru a nu avea noduri izolate", BuildPathString(path)));
            }
        }

        private static IEnumerable<IList<int>> ProcessRemainingNeighbouringPaths(GeronimusParameter parameter, IEnumerable<IList<int>> shortestPathsParam, IList<IList<int>> neighbouringPaths)
        {
            IList<IList<int>> pathsList = neighbouringPaths;
            IDictionary<int, IList<int>> remainingList = new Dictionary<int, IList<int>>(pathsList.Count);
            IDictionary<int, double> calculatedValues = new Dictionary<int, double>(pathsList.Count);
            IList<int> shortestPathsUnion = GetPathsUnion(shortestPathsParam);

            for (int i = 0; i < pathsList.Count; i++)
            {
                IList<int> path = pathsList[i];
                double max = Math.Max(parameter.PassengerFlow[path[0]][path[path.Count - 1]],
                                      parameter.PassengerFlow[path[path.Count - 1]][path[0]]);
                double actualTime = parameter.Capacity*parameter.TimeInterval/max;

                int node = path[shortestPathsUnion.Contains(path[0]) ? 1 : 0];
                string pathToString = BuildPathString(pathsList[i]);
                if (actualTime > parameter.MaxInterval)
                {
                    pathsList.RemoveAt(i--);

                    IEnumerable<IList<int>> shortestPaths = shortestPathsParam.Union(pathsList);
                    bool checkIntersection = AreNodesAccessible(shortestPaths, parameter, path);
                    if (!checkIntersection)
                    {
                        if (calculatedValues.ContainsKey(node))
                        {
                            if (calculatedValues[node] > actualTime)
                            {
                                remainingList[node] = path;
                                calculatedValues[node] = actualTime;
                            }
                        }
                        else
                        {
                            remainingList[node] = path;
                            calculatedValues[node] = actualTime;
                        }
                    }

                    LogText(string.Format("Ruta {0} se exclude, timpul necesar ({1}) > timpul admisibil ({2})",
                                          pathToString, actualTime, parameter.MaxInterval));
                }
                else
                {
                    shortestPathsUnion.Add(node);
                    LogText(string.Format("Ruta {0} se include, timpul necesar ({1}) < timpul admisibil ({2})",
                                          pathToString, actualTime, parameter.MaxInterval));
                }
            }

            foreach (var path in remainingList.Values)
            {
                LogText(string.Format("Ruta {0} se include, pentru a nu avea noduri izolate", BuildPathString(path)));
            }

            return remainingList.Values;
        }

        private static bool AreNodesAccessible(IEnumerable<IList<int>> shortestPaths, GeronimusParameter parameter, IList<int> path)
        {
            if (shortestPaths.Count() == 0)
            {
                return true;
            }
            //shortestPaths = shortestPaths.Except(new List<IList<int>> {path});
            double[][] resultedInitialMatrix = MatrixLoader.BuildInitialMatrix(shortestPaths, parameter.InitialMatrix);
            Node[][] resultedNodesMatrix = FloydAlgorithm.Process(resultedInitialMatrix, parameter.WaitingTime, shortestPaths);

            for (int i = 0; i < resultedNodesMatrix.Length; i++)
            {
                if (double.IsInfinity(resultedNodesMatrix[path[0]][i].ShortestPath) || double.IsInfinity(resultedNodesMatrix[i][path[0]].ShortestPath))
                {
                    return false;
                }
                if (double.IsInfinity(resultedNodesMatrix[path[1]][i].ShortestPath) || double.IsInfinity(resultedNodesMatrix[i][path[1]].ShortestPath))
                {
                    return false;
                }
            }

            return true;
        }
        private static bool AreNodesAccessible(IEnumerable<IList<int>> shortestPaths, GeronimusParameter parameter)
        {
            double[][] resultedInitialMatrix = MatrixLoader.BuildInitialMatrix(shortestPaths, parameter.InitialMatrix);
            Node[][] resultedNodesMatrix = FloydAlgorithm.Process(resultedInitialMatrix, parameter.WaitingTime, shortestPaths);

            for (int i = 0; i < resultedNodesMatrix.Length; i++)
            {
                for (int j = 0; j < resultedNodesMatrix.Length; j++)
                {
                    if (double.IsInfinity(resultedNodesMatrix[i][j].ShortestPath))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static IList<int> GetPathsUnion(IEnumerable<IList<int>> shortestPaths)
        {
            IList<int> allNodes = new List<int>();

            foreach (int node in 
                        from path in shortestPaths 
                            from node in path 
                            where !allNodes.Contains(node) 
                            select node)
            {
                allNodes.Add(node);
            }

            return allNodes;
        }

        private static bool CheckIntersection(IEnumerable<int> nodesList, ICollection<int> smallPath)
        {
            int count = nodesList.Intersect(smallPath).Count();
            return count == smallPath.Count;
        }

        private static IList<IList<int>> GetSmallRoutes(IList<IList<int>> pathsList)
        {
            IList<IList<int>> smallRoutes = new List<IList<int>>();
            for (int i = 0; i < pathsList.Count; i++)
            {
                if (pathsList[i].Count != 2)
                {
                    continue;
                }

                smallRoutes.Add(pathsList[i]);
                pathsList.RemoveAt(i);
                i--;
            }
            return smallRoutes;
        }

        private static void RemoveSubincludedPaths(IList<IList<int>> pathsList)
        {
            for (int i = 0; i < pathsList.Count; i++)
            {
                for (int j = i + 1; j < pathsList.Count; j++)
                {
                    int commonElementsCount = pathsList[i].Intersect(pathsList[j]).Count();

                    if (commonElementsCount != pathsList[j].Count)
                    {
                        continue;
                    }

                    pathsList.RemoveAt(j);
                    break;
                }
            }
        }

        private static void RemoveContainingRoutes(IList<IList<int>> sourceList, IList<IList<int>> destinationList)
        {
            for (int i = 0; i < sourceList.Count; i++)
            {
                for (int j = 0; j < destinationList.Count; j++)
                {
                    int count = sourceList[i].Intersect(destinationList[j]).Count();
                    if (count != destinationList[j].Count)
                    {
                        continue;
                    }

                    destinationList.RemoveAt(j);
                    j--;
                }
            }
        }

        private static bool CheckSubinclusion(IEnumerable<IList<int>> sourceList, IEnumerable<int> path)
        {
            return sourceList.Select(t => t.Intersect(path).Count()).Any(count => count == path.Count());
        }

        /// <summary>
        /// Removes the paths that don't verify the relationship in step #2
        /// </summary>
        /// <param name="pathsList">Initial path list</param>
        /// <param name="parameter">The input parameter</param>
        /// <param name="excludedPaths"></param>
        /// <returns></returns>
        private static void ProcessRemainingPaths(IList<IList<int>> pathsList, GeronimusParameter parameter,
                                                  ICollection<IList<int>> excludedPaths)
        {
            for (int i = 0; i < pathsList.Count; i++)
            {
                IList<int> path = pathsList[i];
                string pathToString = BuildPathString(path);

                double max = GetMaxFlowInExtremities(parameter, path);

                double minimumTime = GetMinimumWaitingTimeForInnerNodes(path, parameter.WaitingTime);


                double actualTime = parameter.UnuniformityCoeficient*parameter.Capacity*parameter.TimeInterval/
                                    (parameter.PassangerArrivalCoeficient*max);
                if (actualTime > minimumTime)
                {
                    LogText(string.Format("Ruta {0} se exclude, timpul necesar ({1}) > timpul admisibil ({2})",
                                          pathToString, actualTime, minimumTime));
                    excludedPaths.Add(pathsList[i]);
                    pathsList.RemoveAt(i--);
                }
                else
                {
                    LogText(string.Format("Ruta {0} se include, timpul necesar ({1}) < timpul admisibil ({2})",
                                          pathToString, actualTime, minimumTime));
                }
            }
        }

        private static double GetMinimumWaitingTimeForInnerNodes(ICollection<int> path, IList<double> waitingTime)
        {
            IEnumerable<int> innerNodes =
                path.Except(new Collection<int> {path.ElementAt(0), path.ElementAt(path.Count - 1)});
            return innerNodes.Select(t => waitingTime[t]).Min();
        }

        /// <summary>
        /// Removes the paths that don't verify the relationship in step #2
        /// </summary>
        /// <param name="shortestPathsResult"></param>
        /// <param name="parameter">The input parameter</param>
        /// <returns></returns>
        private static void BuildAdditionalPathsList(ShortestPathsResult shortestPathsResult, GeronimusParameter parameter)
        {
            double minimumTime = parameter.MaxInterval;
            var additionalPaths = shortestPathsResult.AdditionalPaths;
            double divider = parameter.Capacity * parameter.TimeInterval;

            for (int i = 0; i < additionalPaths.Count; i++)
            {
                IList<int> path = additionalPaths[i];
                string pathToString = BuildPathString(path);

                double max = GetOverallMaxFlow(parameter, path, shortestPathsResult);

                double actualTime = divider / max;
                LogText(string.Format("I = {0} / {1} = {2}", divider, max, actualTime));

                if (actualTime > minimumTime)
                {
                    LogText(string.Format("Ruta {0} se exclude, timpul necesar ({1}) > timpul admisibil ({2})",
                                          pathToString, actualTime, minimumTime));
                    //excludedPaths.Add(pathsList[i]);
                    additionalPaths.RemoveAt(i--);
                }
                else
                {
                    LogText(string.Format("Ruta {0} se include, timpul necesar ({1}) < timpul admisibil ({2})",
                                          pathToString, actualTime, minimumTime));
                }
            }
        }

        private static string BuildPathString(IList<int> path)
        {
            if (!isLoggingEnabled)
            {
                return null;
            }

            var sb = new StringBuilder();

            for (int j = 0; j < path.Count; j++)
            {
                sb.Append(path[j] + 1);
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        private static double GetMaxFlowInExtremities(GeronimusParameter parameter, IList<int> path)
        {
            return Math.Max(parameter.PassengerFlow[path[0]][path[path.Count - 1]],
                            parameter.PassengerFlow[path[path.Count - 1]][path[0]]);
        }

        private static double GetOverallMaxFlow(GeronimusParameter parameter, IList<int> path, ShortestPathsResult shortestPaths)
        {
            //TODO: Check if the small route is not included in the initial network, if so, we shouldn't sum the passenger flw values
            double forwardSum = 0;
            double backwardSum = 0;

            if (path.Count > 2)
            {
                forwardSum += parameter.PassengerFlow[path[0]][path[path.Count - 1]];
                backwardSum += parameter.PassengerFlow[path[path.Count - 1]][path[0]];
                LogText(string.Format("{0} / {1}", forwardSum, backwardSum));
            }

            for (int i = 0; i < path.Count - 1; i++)
            {
                var list = new List<int>
                               {
                                   path[i],
                                   path[i + 1]
                               };
                if (CheckSubinclusion(shortestPaths.ShortestPaths, list) || CheckSubinclusion(shortestPaths.NeighbouringPaths, list))
                {
                    continue;
                }

                forwardSum += parameter.PassengerFlow[path[i]][path[i + 1]];
                backwardSum += parameter.PassengerFlow[path[i + 1]][path[i]];

                LogText(string.Format("{0} / {1}", parameter.PassengerFlow[path[i]][path[i + 1]],
                                      parameter.PassengerFlow[path[i + 1]][path[i]]));
            }

            LogText(string.Format("Sumele finale: {0} / {1}", forwardSum, backwardSum));

            return Math.Max(forwardSum, backwardSum);
        }

        private static void RemoveContainingPaths(IList<IList<int>> pathsList)
        {
            for (int i = 0; i < pathsList.Count; i++)
            {
                for (int j = i + 1; j < pathsList.Count; j++)
                {
                    int commonElementsCount = pathsList[i].Intersect(pathsList[j]).Count();

                    if (commonElementsCount == pathsList[i].Count)
                    {
                        pathsList.RemoveAt(i);
                        i--;
                        break;
                    }

                    if (commonElementsCount != pathsList[j].Count)
                    {
                        continue;
                    }

                    pathsList.RemoveAt(j);
                    j--;
                }
            }

            return;
        }

        private static IList<IList<int>> BuildAllPossiblePaths(Node[][] nodesMatrix)
        {
            IList<IList<int>> pathsList = new List<IList<int>>();
            for (int i = 0; i < nodesMatrix.LongLength; i++)
            {
                for (int j = 0; j < nodesMatrix.Length; j++)
                {
                    if (i == j /* || initialMatrix[i][j] < double.PositiveInfinity*/)
                    {
                        continue;
                    }
                    pathsList.Add(FloydAlgorithm.CalculatePath(nodesMatrix, i, j));
                }
            }
            return pathsList;
        }

        public static double CalculateOverallTimeInTrafic(Node[][] initialMatrix, double[][] passengerFlow)
        {
            double sum = 0;

            for (int i = 0; i < initialMatrix.Length; i++)
            {
                for (int j = i + 1; j < initialMatrix[i].Length; j++)
                {
                    sum += initialMatrix[i][j].ShortestPath*passengerFlow[i][j];
                    sum += initialMatrix[j][i].ShortestPath*passengerFlow[j][i];

                    if (sum == double.PositiveInfinity)
                    {
                        LogText("Suma a ajuns la infinit");
                        return sum;
                    }
                }
            }

            return sum;
        }

        public static double CalculatePassengerFlowProportion(double[][] passwengerFlow)
        {
            double minSum = 0;
            double maxSum = 0;

            for (int i = 0; i < passwengerFlow.Length; i++)
            {
                for (int j = i + 1; j < passwengerFlow.Length; j++)
                {
                    minSum += Math.Min(passwengerFlow[i][j], passwengerFlow[j][i]);
                    maxSum += Math.Max(passwengerFlow[i][j], passwengerFlow[j][i]);
                }
            }

            return minSum/maxSum;
        }

        public static double CalculateTotalWaitingTime(int unitCapacity, int routesNumber, double coeficient, double totalTime)
        {
            return 30*unitCapacity*routesNumber*(1 + coeficient) + totalTime;
        }
    }
}