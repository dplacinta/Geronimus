namespace Geronimus.DataObjects
{
    public class GeronimusParameter
    {
        public double[] WaitingTime { get; set; }

        public double UnuniformityCoeficient { get; set; }

        public double TimeInterval { get; set; }

        public double PassangerArrivalCoeficient { get; set; }

        public double MaxInterval { get; set; }

        public double TransferTime { get; set; }

        public int Capacity { get; set; }

        public double[][] InitialMatrix { get; set; }

        public Node[][] NodesMatrix { get; set; }

        public double[][] PassengerFlow { get; set; }
    }
}