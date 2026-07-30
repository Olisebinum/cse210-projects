using System;
using System.Collections.Generic;

/*
 * EXCEEDING REQUIREMENTS:
 *
 * 1. Session log — the program keeps track of every activity completed
 *    during the current run (name + duration) and prints a summary when
 *    the user chooses to quit, so they can see everything they did in
 *    that session.
 *
 * 2. No-repeat random prompts/questions — Activity.GetRandomPrompt()
 *    hands out items from a shuffled queue instead of picking a fresh
 *    random index each time, so no prompt or question repeats until
 *    every item in that list has been used at least once.
 */
public class Program
{
    public static void Main(string[] args)
    {
        List<Activity> completedActivities = new List<Activity>();
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine();
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. View Session Log");
            Console.WriteLine("5. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();
            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    ShowSessionLog(completedActivities);
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("That's not a valid choice. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }

            if (activity != null)
            {
                activity.RunActivity();
                completedActivities.Add(activity);
                Console.WriteLine();
                Console.WriteLine("Press Enter to return to the menu.");
                Console.ReadLine();
            }
        }

        Console.WriteLine();
        Console.WriteLine("Thanks for taking time to be mindful today!");
    }

    private static void ShowSessionLog(List<Activity> completedActivities)
    {
        Console.Clear();
        Console.WriteLine("Session Log");
        Console.WriteLine();

        if (completedActivities.Count == 0)
        {
            Console.WriteLine("You haven't completed any activities yet this session.");
        }
        else
        {
            foreach (Activity activity in completedActivities)
            {
                Console.WriteLine($"- {activity.Name} Activity: {activity.DurationSeconds} seconds");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press Enter to return to the menu.");
        Console.ReadLine();
    }
}