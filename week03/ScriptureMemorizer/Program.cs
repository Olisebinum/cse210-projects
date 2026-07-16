using System;
using System.Collections.Generic;

/*
 * EXCEEDING REQUIREMENTS:
 * 1. Instead of a single hard-coded scripture, this program keeps a small
 *    library of scriptures and picks one at random each time it runs, so
 *    the memorization practice varies between sessions.
 * 2. HideRandomWords only selects from words that are NOT already hidden
 *    (see Scripture.cs), so no "hide" attempt is ever wasted on a word
 *    that's already gone -- this makes the hiding pace more consistent
 *    and avoids frustrating repeats.
 */

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> library = new List<Scripture>
        {
            new Scripture(new Reference("John", 3, 16),
                "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life"),

            new Scripture(new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart and lean not unto thine own understanding In all thy ways acknowledge him and he shall direct thy paths"),

            new Scripture(new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me"),

            new Scripture(new Reference("Joshua", 1, 9),
                "Have not I commanded thee Be strong and of a good courage be not afraid neither be thou dismayed for the Lord thy God is with thee whithersoever thou goest"),
        };

        Random random = new Random();
        Scripture scripture = library[random.Next(library.Count)];

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.IsCompletelyHidden())
            {
                break;
            }

            Console.Write("Press Enter to continue or type 'quit' to end: ");
            string input = Console.ReadLine();

            if (input != null && input.Trim().ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(3);
        }

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine();
        Console.WriteLine("Great work practicing! Goodbye.");
    }
}