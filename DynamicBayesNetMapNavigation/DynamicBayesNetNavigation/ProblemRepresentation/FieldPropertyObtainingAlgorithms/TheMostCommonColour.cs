using System.Drawing;
using DynamicBayesNetNavigation.ProblemRepresentation.Utils;

namespace DynamicBayesNetNavigation.ProblemRepresentation.FieldPropertyObtainingAlgorithms;

    public class TheMostCommonColor : IObtainingAlgorithm
    {
        public IReadOnlyCollection<uint> GenerateVectorOfAttributeForBlock(IReadOnlyCollection<Color> colorsInTheBlock) => 
            new[] { colorsInTheBlock
                .GroupBy(x => x)
                .Select(g => new { Key = g.Key, Count = g.Count() }).OrderByDescending(el => el.Count).First().Key
                .ToARGB() };
    }

