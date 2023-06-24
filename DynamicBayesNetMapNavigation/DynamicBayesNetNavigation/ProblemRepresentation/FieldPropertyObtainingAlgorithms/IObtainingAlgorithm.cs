using System.Drawing;


namespace DynamicBayesNetNavigation.ProblemRepresentation.FieldPropertyObtainingAlgorithms
{
    public interface IObtainingAlgorithm
    {
        public IReadOnlyCollection<uint> GenerateVectorOfAttributeForBlock(IReadOnlyCollection<Color> colorsInTheBlock);
    }
}
