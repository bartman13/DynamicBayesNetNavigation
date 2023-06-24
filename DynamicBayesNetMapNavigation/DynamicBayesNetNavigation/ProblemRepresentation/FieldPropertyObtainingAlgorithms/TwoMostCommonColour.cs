using System.Drawing;
using DynamicBayesNetNavigation.ProblemRepresentation.Utils;

namespace DynamicBayesNetNavigation.ProblemRepresentation.FieldPropertyObtainingAlgorithms;

public class TwoMostCommonColor : IObtainingAlgorithm
{
    public IReadOnlyCollection<uint> GenerateVectorOfAttributeForBlock(IReadOnlyCollection<Color> colorsInTheBlock) => colorsInTheBlock
            .GroupBy(x => x)
            .Select(g => new { Key = g.Key, Count = g.Count() })
            .OrderByDescending(el => el.Count).Take(2)
            .Select(x => x.Key.ToARGB()).ToArray();
        }

