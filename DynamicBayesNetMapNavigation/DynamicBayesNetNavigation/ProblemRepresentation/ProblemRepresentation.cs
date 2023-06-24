namespace DynamicBayesNetNavigation.ProblemRepresentation
{
    public class ProblemRepresentation
    {
        public int N { get; }
        public int M { get; }
        public IReadOnlyCollection<uint>[,] AttributeMatrix { get; }
        public IReadOnlyDictionary<uint, HashSet<(int, int)>> OutputModel { get; }

        public ProblemRepresentation(
            int n,
            int m,
            IReadOnlyCollection<uint>[,] attributeMatrix,
            IReadOnlyDictionary<uint, HashSet<(int, int)>> outputModel)
        {
            N = n; 
            M = m; 
            AttributeMatrix = attributeMatrix;
            OutputModel = outputModel;
        }
    }
}
