using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

/// <summary>
/// Base class for all mindfulness activities. Holds everything that is
/// common to every activity (the starting message, ending message,
/// animations, and duration tracking) so that each subclass only needs
/// to implement its own unique middle section in PerformActivity().
/// </summary>
public abstract class Activity
{
    // Private fields — only this class can touch them directly.
    // Subclasses interact with the activity through protected helpers below.
    private string _name;
    private string _description;
    private int _durationSeconds;

    // Shared Random instance so every subclass gets random behavior
    // without each one creating and managing its own Random object.
    private Random _random = new Random();

    // Supports the "no repeats until the whole list has been used"
    // bonus feature. Keyed by the list reference itself, so each
    // distinct prompt/question list gets its own shuffled queue.
    private Dictionary<List<string>, Queue<string>> _shuffledQueues =
        new Dictionary<List<string>, Queue<string>>();

    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    // Exposed so Program.cs can report on completed activities (used by
    // the session log bonus feature — see comment in Program.cs).
    public string Name => _name;
    public int DurationSeconds => _durationSeconds;

    /// <summary>
    /// The single public entry point every activity is run through.
    /// This "template method" guarantees the starting message, the
    /// activity-specific work, and the ending message always happen
    /// in that order, without each subclass having to repeat that logic.
    /// </summary>
    public void RunActivity()
    {
        DisplayStartingMessage();
        PerformActivity();
        DisplayEndingMessage();
    }

    /// <summary>
    /// Each subclass implements only its own unique activity logic here.
    /// </summary>
    protected abstract void PerformActivity();

    private void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name} Activity.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();

        _durationSeconds = PromptForDuration();

        Console.WriteLine();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
    }

    private void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed the {_name} Activity for {_durationSeconds} seconds.");
        ShowSpinner(3);
    }

    private int PromptForDuration()
    {
        int duration = 0;
        while (duration <= 0)
        {
            Console.Write("How long, in seconds, would you like for your session? ");
            string input = Console.ReadLine();
            int.TryParse(input, out duration);

            if (duration <= 0)
            {
                Console.WriteLine("Please enter a whole number greater than 0.");
            }
        }
        return duration;
    }

    /// <summary>
    /// Displays a simple spinner animation for the given number of seconds.
    /// Shared by all activities so none of them need their own copy.
    /// </summary>
    protected void ShowSpinner(int seconds)
    {
        string[] frames = { "|", "/", "-", "\\" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(frames[i % frames.Length]);
            Thread.Sleep(150);
            Console.Write("\b \b");
            i++;
        }
    }

    /// <summary>
    /// Displays a countdown timer for the given number of seconds.
    /// Shared by all activities so none of them need their own copy.
    /// </summary>
    protected void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
            if (i >= 10)
            {
                Console.Write("\b \b"); // erase the extra digit for two-digit numbers
            }
        }
    }

    /// <summary>
    /// Returns a random item from the given list. Bonus behavior: items
    /// are handed out in a shuffled order without repeating until every
    /// item in the list has been used once, then the list reshuffles.
    /// </summary>
    protected string GetRandomPrompt(List<string> options)
    {
        if (!_shuffledQueues.ContainsKey(options) || _shuffledQueues[options].Count == 0)
        {
            List<string> shuffled = options.OrderBy(_ => _random.Next()).ToList();
            _shuffledQueues[options] = new Queue<string>(shuffled);
        }
        return _shuffledQueues[options].Dequeue();
    }

    /// <summary>
    /// Returns true while the activity still has time remaining,
    /// given the time it started. Shared helper so each subclass's
    /// PerformActivity loop doesn't repeat this date-math.
    /// </summary>
    protected bool TimeRemaining(DateTime startTime)
    {
        return DateTime.Now < startTime.AddSeconds(_durationSeconds);
    }
}