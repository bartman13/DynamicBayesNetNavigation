// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using DynamicBayesNetNavigation;
using DynamicBayesNetNavigation.Enums;
using DynamicBayesNetNavigation.NavigatingModule;
using DynamicBayesNetNavigation.NavigationProcess;
using DynamicBayesNetNavigation.ProblemRepresentation;
using DynamicBayesNetNavigation.ProblemRepresentation.FieldPropertyObtainingAlgorithms;

var work = true;
while (work)
{
    Console.WriteLine("Program Obsługi Autonomicznej Nawigacji Kursorem po zdefiniowanym obrazie");
    Console.WriteLine("Opcje:");
    Console.WriteLine("O: Uruchom własny Test");
    Console.WriteLine("M: Uruchom zdwfiniowany moduł testowy");
    Console.WriteLine("Dowolny: Zakończ");
    Console.Write("->");
    var key = Console.ReadKey();
    Console.WriteLine();
    switch (key.Key)
    {
        case ConsoleKey.O:
            break;
        case ConsoleKey.M:
            TestingModules.ExecuteTests();
            break;
        case ConsoleKey.C:
            Console.Clear();
            break;
        default:
            work = false;
            break;
    }
}

Console.WriteLine("End");