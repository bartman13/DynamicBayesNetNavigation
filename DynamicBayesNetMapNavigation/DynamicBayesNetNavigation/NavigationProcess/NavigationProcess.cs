using DynamicBayesNetNavigation.Enums;
using DynamicBayesNetNavigation.ProblemRepresentation.Utils;

namespace DynamicBayesNetNavigation.NavigationProcess
{
    public static class NavigationProcess
    {
        public static IReadOnlyCollection<DefinedMoves> PerformNavigationProcess(
            int numberOfParticles,
            ProblemRepresentation.ProblemRepresentation problem,
            (int, int) startPoint,
            (int, int) destination,
            int maximumLoopSteps = 1000)
        {
            var moveSequence = new List<DefinedMoves>();
            var currentPosition = startPoint;
            int iterator = 0;
            NavigatingModule.NavigatingModule navigationModule = new NavigatingModule.NavigatingModule(
                numberOfParticles: numberOfParticles,
                n: problem.N,
                m: problem.M,
                outputModel: problem.OutputModel,
                destination: destination);
            
            while (iterator < maximumLoopSteps && currentPosition != destination)
            { 
                var currentMove = navigationModule.AlgorithmStep(problem.AttributeMatrix[currentPosition.Item1, currentPosition.Item2]
                    .GetHashForColorCollection());
                moveSequence.Add(currentMove);

                if (currentMove == DefinedMoves.Up && currentPosition.Item1 < problem.N -1)
                {
                    currentPosition.Item1++;
                }
                else if (currentMove == DefinedMoves.Down && currentPosition.Item1 > 0)
                {
                    currentPosition.Item1--;
                }
                else if (currentMove == DefinedMoves.Left && currentPosition.Item2 > 0)
                {
                    currentPosition.Item2--;
                }
                else
                {
                    currentPosition.Item2++;
                }

                iterator++;
            }

            return moveSequence;
        }
    }
}
