namespace Geronimus.DataObjects
{
    public class Node
    {
        public Node(double shortestPath)
        {
            ShortestPath = shortestPath;
        }

        public Node(double shortestPath, int nextNode)
        {
            ShortestPath = shortestPath;
            Next = nextNode;
        }

        public int Next { get; set; }

        public double ShortestPath { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (Node) && Equals(obj as Node);
        }

        private bool Equals(Node other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Next == Next && other.ShortestPath.Equals(ShortestPath);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Next*397) ^ ShortestPath.GetHashCode();
            }
        }

        public override string ToString()
        {
            return string.Format("Node(Path={0}, Nex={1})", ShortestPath, Next);
        }
    }
}