using System;
using System.Collections.Generic;

/// <summary>
/// Guides the user to think broadly about a single prompt by having
/// them list as many items as they can before the chosen duration runs out.
/// </summary>
public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt happy this month?",
        "Who are some of your personal heroes?",
    };

    public ListingActivity()
        : base(
            "Listing",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void PerformActivity()
    {
        Console.WriteLine();
        Console.WriteLine(GetRandomPrompt(_prompts));
        Console.WriteLine();
        Console.Write("You will have a few seconds to think of an answer for each. ");
        ShowCountDown(5);
        Console.WriteLine();
        Console.WriteLine("Start listing items. Press Enter after each one.");
        Console.WriteLine();

        DateTime startTime = DateTime.Now;
        List<string> items = new List<string>();

        while (TimeRemaining(startTime))
        {
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
            {
                items.Add(item);
            }
        }

        Console.WriteLine();
        Console.WriteLine($"You listed {items.Count} items!");
    }
}