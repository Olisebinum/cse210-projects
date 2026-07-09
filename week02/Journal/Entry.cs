using System;

// The Entry class represents a single journal entry: the date it was
// written, the prompt the user responded to, and their written response.
// This class has no behaviors beyond holding data — it is intentionally
// simple, since its only responsibility is to represent one entry.
public class Entry
{
    public string _date;
    public string _promptText;
    public string _entryText;

    // Builds a readable, single-line summary of this entry.
    // Callers do not need to know how the formatting is put together —
    // they just call Display() and get a clean result. This is
    // abstraction: the formatting details are hidden inside this method.
    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_promptText}");
        Console.WriteLine($"Response: {_entryText}");
        Console.WriteLine(new string('-', 40));
    }
}