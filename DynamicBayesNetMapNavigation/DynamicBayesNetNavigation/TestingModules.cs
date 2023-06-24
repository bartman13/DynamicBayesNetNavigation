using System.Security.Cryptography.X509Certificates;
using DynamicBayesNetNavigation.ProblemRepresentation;
using DynamicBayesNetNavigation.ProblemRepresentation.FieldPropertyObtainingAlgorithms;
using System.IO;

namespace DynamicBayesNetNavigation
{
    public static class TestingModules
    {
        private const string ExamplesPath = @"..\..\..\PredefinedTestingExamples\";
        private const string Output = @"..\..\..\Output\";


        private const string csvHeader =
            @"LiczbaCzastek;Sukces;MaksymalnaLiczbaKrokówPetli;N;M;NajkrótszaLiczbaPrzejśćDoCelu;LiczbaWykonanychKrokow;ListaKrokow;";

        public static void ExecuteTests()
        {
            var prf1 = new ProblemRepresentationFactory(ExamplesPath + "1.png");
            var prf2 = new ProblemRepresentationFactory(ExamplesPath + "2.png");
            var prf3 = new ProblemRepresentationFactory(ExamplesPath + "3.png");
            var prf4 = new ProblemRepresentationFactory(ExamplesPath + "4.png");



            Console.WriteLine("Mapa 1 - mapa bitowa poddzieona na jednokolorwe kwadratowe bloki 10x10");
            Console.WriteLine("Test 1 - Prosta mapa składającaca się z Kwadratowych różnokolorowych  pól podzielona na kwadratowe bloki");
            PerformTestasenOnNumberOfParceles(
                "Example1",
                prf1.CreateProblemRepresentation(10, 10, new MostCommonColor()),
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

            using (StreamWriter sw = File.CreateText(Output + fielsName))
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
