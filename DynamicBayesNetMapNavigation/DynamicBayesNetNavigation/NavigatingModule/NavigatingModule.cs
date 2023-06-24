using DynamicBayesNetNavigation.Enums;

namespace DynamicBayesNetNavigation.NavigatingModule
{
    public class NavigatingModule
    {
        private readonly int N;
        private readonly int M;
        private (int n,int m)[] _currentParceleState;
        private readonly IReadOnlyDictionary<uint, HashSet<(int, int)>> _OutputModel;
        private readonly (int n, int m) _destination;

        public NavigatingModule(
            int numberOfParticles,
            int n,
            int m,
            IReadOnlyDictionary<uint, HashSet<(int, int)>> outputModel,
            (int,int) destination)
        {
            N = n;
            M = m;
            _OutputModel = outputModel;
            _destination = destination;
            
            var probabilityStep = 1.0 / (N * M);
            var startingDistribution = Enumerable.Range(0, N * M)
                .Select(x => (Probability: (x + 1) * probabilityStep, Coordinate: (x / N, x % M)));

            var random = new Random();
            _currentParceleState = new (int,int)[numberOfParticles];

            for (var i = 0; i < numberOfParticles; i++)
            {
                var realRoll = random.NextDouble();
                var chosenCoordinate = startingDistribution
                    .FirstOrDefault(element => element.Probability > realRoll)
                    .Coordinate;

                _currentParceleState[i] = chosenCoordinate;
            }
        }

        public DefinedMoves AlgorithmStep(uint observation)
        {
            ParcelFilteringAlgorithm(observation);
            return MakeMoveDecision();
        }

        private void ParcelFilteringAlgorithm(uint observation)
        {
            var weighTVector = new Double[_currentParceleState.Length];
            var selectedSet = _OutputModel[observation];
            var setOfPossibleCoordinates = _currentParceleState
                .Where(x => selectedSet.Contains(x))
                .ToArray();

            var tmpParceleState = _currentParceleState = Enumerable.Range(1,_currentParceleState.Length).Select(x => setOfPossibleCoordinates.Length !=0 ? TakeRandomValueFromSet<(int,int)>(setOfPossibleCoordinates) : TakeRandomValueFromSet<(int,int)>(_currentParceleState)).ToArray();
            _currentParceleState = tmpParceleState;
        }

        static T TakeRandomValueFromSet<T>(T[] set)
        {
            Random random = new Random();
            int randomIndex = random.Next(set.Length);
            return set[randomIndex];
        }


        // The move is defined by hipotetical position which is weighted average of all positions in vector.
        // Based on it we choose action which will  
        private DefinedMoves MakeMoveDecision()
        {
            var averageY = _currentParceleState.Sum(x => x.n) / _currentParceleState.Length;
            var averageX = _currentParceleState.Sum(x => x.m) / _currentParceleState.Length;

            var Ygap = _destination.n - averageY;
            var Xgap = _destination.m - averageX;
            DefinedMoves result;
            if (Math.Abs(Ygap) > Math.Abs(Xgap))
            {
                result = Ygap >= 0 ? DefinedMoves.Up : DefinedMoves.Down;
            }
            else
            {
                result = Xgap >= 0 ? DefinedMoves.Right : DefinedMoves.Left;
            }

            for (var i = 0; i < _currentParceleState.Length; i++)
            {
                switch (result)
                {
                    case DefinedMoves.Up when _currentParceleState[i].n < N-1:
                        _currentParceleState[i].n++;
                        break;
                    case DefinedMoves.Down when _currentParceleState[i].n > 0:
                        _currentParceleState[i].n--;
                        break;
                    case DefinedMoves.Left when _currentParceleState[i].m > 0:
                        _currentParceleState[i].m--;
                        break;
                    case DefinedMoves.Right when _currentParceleState[i].m < M - 1:
                        _currentParceleState[i].m++;
                        break;
                }
            }

            return result;
        }


    }
}
