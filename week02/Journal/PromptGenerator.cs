using System;
using System.Collections.Generic;

// The PromptGenerator class is responsible for one thing only: handing
// out a random writing prompt whenever asked. The rest of the program
// never needs to know how many prompts exist, where they are stored, or
// how one gets chosen — it just calls GetRandomPrompt(). This is a good
// example of abstraction: all of that detail is hidden inside this class.
public class PromptGenerator
{
    private List<string> _prompts;
    private Random _random;

    public PromptGenerator()
    {
        _random = new Random();

        // At least five prompts are required. A few of these are the
        // examples given in the assignment, and a few are my own.
        _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What is something I learned today that I want to remember?",
            "What is one thing I am grateful for today?"
        };
    }

    // Returns one prompt at random. The caller does not need to know
    // anything about how the prompt list is stored or picked — that
    // detail is completely hidden inside this method.
    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}