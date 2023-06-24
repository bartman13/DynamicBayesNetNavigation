using DynamicBayesNetNavigation.ProblemRepresentation;
using DynamicBayesNetNavigation.ProblemRepresentation.FieldPropertyObtainingAlgorithms;

namespace DynamicBayesNetNavigation
{
    public static class TestingModules
    {
        private const string ExamplesPath = @".\PredefinedTestingExamples\";
        private const string Output = @".\Output\";


        private const string csvHeader =
            @"LiczbaCzastek;Sukces;MaksymalnaLiczbaKrokówPetli;N;M;NajkrótszaLiczbaPrzejśćDoCelu;LiczbaWykonanychKrokow;ListaKrokow;";

        public static void ExecuteTests()
        {
            var prf1 = new ProblemRepresentationFactory(ExamplesPath + "1.png");
            var prf2 = new ProblemRepresentationFactory(ExamplesPath + "2.png");
            var prf3 = new ProblemRepresentationFactory(ExamplesPath + "3.png");
            var mostCommonColor = new MostCommonColor();
            var twoMostCommonColor = new TwoMostCommonColor();


            Console.WriteLine("Mapa 1 - mapa bitowa poddzieona na jednokolorwe kwadratowe bloki 10x10");
            Console.WriteLine("\tTest 1.1 - MostCommonColor, bloki 10x10, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example1.1",
                prf1.CreateProblemRepresentation(10, 10, mostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);
            Console.WriteLine("\tTest 1.2 - MostCommonColor, bloki 100x100, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example1.2",
                prf1.CreateProblemRepresentation(100, 100, mostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);
            Console.WriteLine("\tTest 1.3 - TwoMostCommonColor, bloki 10x10, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example1.3",
                prf1.CreateProblemRepresentation(10, 10, twoMostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);
            Console.WriteLine("\tTest 1.4 - TwoMostCommonColor, bloki 100x100, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example1.4",
                prf1.CreateProblemRepresentation(100, 100, twoMostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);

            Console.WriteLine("Mapa 2 - mapa Mazowsza");
            Console.WriteLine("\tTest 2.1 - MostCommonColor, bloki 10x10, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example2.1",
                prf2.CreateProblemRepresentation(10, 10, mostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);
            Console.WriteLine("\tTest 2.2 - MostCommonColor, bloki 100x100, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example2.2",
                prf2.CreateProblemRepresentation(100, 100, mostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);
            Console.WriteLine("\tTest 2.3 - TwoMostCommonColor, bloki 10x10, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example2.3",
                prf2.CreateProblemRepresentation(10, 10, twoMostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);
            Console.WriteLine("\tTest 2.4 - TwoMostCommonColor, bloki 100x100, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example2.4",
                prf2.CreateProblemRepresentation(100, 100, twoMostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);

            Console.WriteLine("Mapa 3 - wielokolorowa mapa");
            Console.WriteLine("\tTest 3.1 - MostCommonColor, bloki 10x10, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example3.1",
                prf2.CreateProblemRepresentation(10, 10, mostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);
            Console.WriteLine("\tTest 3.2 - MostCommonColor, bloki 100x100, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example3.2",
                prf2.CreateProblemRepresentation(100, 100, mostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);
            Console.WriteLine("\tTest 3.3 - TwoMostCommonColor, bloki 10x10, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example3.3",
                prf2.CreateProblemRepresentation(10, 10, twoMostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);
            Console.WriteLine("\tTest 3.4 - TwoMostCommonColor, bloki 100x100, dokładność 100% ");
            PerformTestasenOnNumberOfParceles(
                "Example3.4",
                prf2.CreateProblemRepresentation(100, 100, twoMostCommonColor),
                (0, 0),
                (5, 5),
                100,
                8);
        }

        private static void PerformTestasenOnNumberOfParceles(string fielsName, ProblemRepresentation.ProblemRepresentation problem, (int,int) startPoint, (int,int) endPoint, int maxStepNumber, int shortestPossibleNumberOfMoves)
        {
            var outputModel = Enumerable.Range(1, 300).Select(x => new
            {
                NumberOfParcels = x,
                Result = NavigationProcess.NavigationProcess.PerformNavigationProcess(x, problem, startPoint, endPoint,
                    maxStepNumber)
            }).Select(y => 
                    $"{y.NumberOfParcels};{y.Result.Count < maxStepNumber};{maxStepNumber};{problem.N};{problem.M};{shortestPossibleNumberOfMoves};{y.Result.Count};{y.Result.ToString()};")
                .ToArray();
            
            if (! System.IO.Directory.Exists(Output))
            {
                System.IO.Directory.CreateDirectory(Output);
            }

            using (StreamWriter sw = File.CreateText(Output + fielsName + ".csv"))
            {
                sw.WriteLine(csvHeader);
                foreach (var s in outputModel)
                {
                    sw.WriteLine(s);
                }
            }
        }
    }
}
