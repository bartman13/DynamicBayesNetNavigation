using DynamicBayesNetNavigation.ProblemRepresentation.FieldPropertyObtainingAlgorithms;
using DynamicBayesNetNavigation.ProblemRepresentation.Utils;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DynamicBayesNetNavigation.ProblemRepresentation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1416", Justification = "Academic purpose code")]
    public class ProblemRepresentationFactory
    {
        private readonly Bitmap _bitmap;

        public ProblemRepresentationFactory(string path)
        {
            _bitmap = new Bitmap(path);
        }

        public ProblemRepresentation CreateProblemRepresentation(int n, int m, IObtainingAlgorithm algorithm)
        {
            var attributeMatrix = GenerateAttributeMatrix(n, m, algorithm);
            var hashedValues = GenerateHashTable(attributeMatrix);

            return new ProblemRepresentation(
                n:n,
                m:m,
                attributeMatrix: attributeMatrix, 
                outputModel: hashedValues);
        }

        // TODO: Performance test if it is good idea to implement Parallel computation over BITMAP
        private IReadOnlyCollection<uint>[,] GenerateAttributeMatrix(int n, int m, IObtainingAlgorithm algorithm)
        {
            var heightStep = _bitmap.Width / n;
            var widthStep = _bitmap.Height / m;
            var resultMatrix = new IReadOnlyCollection<uint>[n,m];

            for (var x = 0; x < n; x++)
            {
                for (var y = 0; y < m; y++)
                {
                    var setOfPixels = new List<Color>();
                    for (var i = y*heightStep; i < (y+1) * heightStep; i++)
                    {
                        for (var j = x*widthStep; j < (x+1) * widthStep; j++)
                        {
                            setOfPixels.Add(_bitmap.GetPixel(i, j));
                        }
                    }

                    resultMatrix[x,y] = algorithm.GenerateVectorOfAttributeForBlock(setOfPixels);
                }
            }

            return resultMatrix;
        }

        private IReadOnlyDictionary<uint,HashSet<(int n,int m)>> GenerateHashTable(IReadOnlyCollection<uint>[,] attributeMatrix)
        {
            var outputDictionary = new Dictionary<uint, HashSet<(int, int)>>();
            var height = attributeMatrix.GetLength(0); 
            var width = attributeMatrix.GetLength(1);

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    var hash = attributeMatrix[i, j].GetHashForColorCollection();
                    if (outputDictionary.ContainsKey(hash)) outputDictionary[hash].Add((i, j));
                    else outputDictionary.Add(hash, new HashSet<(int, int)> {(i,j)});

                }
            }

            return outputDictionary;
        }
    }
}
