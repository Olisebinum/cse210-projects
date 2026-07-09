using System;

// =====================================================================
// W02 Project: Journal Program
// Author: Olise Ebinum
//
// EXCEEDING REQUIREMENTS:
// Instead of saving the journal using a simple character-separated text
// format (e.g. splitting fields with a symbol like "~"), I chose to save
// and load the journal using JSON (via System.Text.Json). This avoids
// any risk of a user's written response accidentally containing the
// separator character, keeps the saved file structured and readable,
// and demonstrates working with a real-world data format used broadly
// in software development, similar to how I already use JSON for data
// storage in my other coursework.
// =====================================================================

class Program
{
    static void Main(string[] args)
    {
        // The Program class only talks to Journal and PromptGenerator
        // through their public methods. It never needs to know how a
        // prompt is chosen or how entries are stored internally — that
        // is the abstraction principle at work across the whole program.
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        bool running = true;

        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry(journal, promptGenerator);
                    break;
                case "2":
                    journal.DisplayAll();
                    break;
                case "3":
                    Console.Write("Enter a filename to save to: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    break;
                case "4":
                    Console.Write("Enter a filename to load from: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("That is not a valid option. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    // Displays the menu options to the user.
    static void DisplayMenu()
    {
        Console.WriteLine("Journal Menu");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal to a file");
        Console.WriteLine("4. Load the journal from a file");
        Console.WriteLine("5. Quit");
        Console.Write("Choose an option: ");
    }

    // Handles the "write a new entry" flow: get a prompt, get the
    // user's response, build an Entry, and add it to the journal.
    static void WriteNewEntry(Journal journal, PromptGenerator promptGenerator)
    {
        string prompt = promptGenerator.GetRandomPrompt();
        Console.WriteLine(prompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Entry entry = new Entry();
        entry._date = DateTime.Now.ToShortDateString();
        entry._promptText = prompt;
        entry._entryText = response;

        journal.AddEntry(entry);
        Console.WriteLine("Entry added.");
    }
}