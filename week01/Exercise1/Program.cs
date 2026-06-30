using System;

class Program
{
    static void Main(string[] args)
    {
        // Prompt the user for their first and last name
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();

        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine($"Hello Your name is {lastName}, {firstName} {lastName}.");
    }
}