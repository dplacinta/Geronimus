namespace Geronimus.DataObjects
{
    using System.Collections.Generic;

    public class ShortestPathsResult
    {
        public ShortestPathsResult(IList<IList<int>> shortestPaths, IList<IList<int>> neighbouringPaths)
        {
            ShortestPaths = shortestPaths;
            NeighbouringPaths = neighbouringPaths;
            AdditionalPaths = new List<IList<int>>();
            ConfirmedAdditionalPaths = new List<IList<int>>();
        }

        public IList<IList<int>> ShortestPaths { get; private set; }

        public IList<IList<int>> NeighbouringPaths { get; private set; }

        public IList<IList<int>> AdditionalPaths { get; private set; }

        public Node[][] ResultedNodesMatrix { get; set; }

        public double TotalTime { get; set; }

        public double[][] ResultedInitialMatrix { get; set; }

        public IList<IList<int>> ConfirmedAdditionalPaths { get; private set; }
    }
}