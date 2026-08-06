using System;
using System.Collections.Generic;
using System.IO;

// Owns the list of goals and the running score, and now owns the menu loop
// itself (Start()) to match the course's reference design. This is where
// the polymorphism actually pays off: CreateGoal() constructs whichever
// concrete subclass the user asked for, but everywhere else in this class
// just treats every item in _goals as a plain Goal and calls the shared
// abstract methods — it never needs to know or check which subclass it's
// holding, even when reading back PointsEarnedLastEvent after RecordEvent().
public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score;

    // CREATIVITY ADDITION: a light leveling/title system built on top of
    // the score, similar in spirit to the "Level 13 Ninja Unicorn" idea
    // from the assignment prompt.
    private static readonly string[] _titles =
    {
        "Newcomer", "Apprentice", "Adventurer", "Journeyman",
        "Hero", "Champion", "Legend", "Eternal Champion"
    };

    public GoalManager()
    {
    }

    public void Start()
    {
        bool running = true;
        Console.WriteLine("Welcome to Eternal Quest!");

        while (running)
        {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("  1. Create a new goal");
            Console.WriteLine("  2. List goal names");
            Console.WriteLine("  3. List goal details");
            Console.WriteLine("  4. Record an event");
            Console.WriteLine("  5. Show player info");
            Console.WriteLine("  6. Save goals");
            Console.WriteLine("  7. Load goals");
            Console.WriteLine("  8. Quit");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoalNames();
                    break;
                case "3":
                    ListGoalDetails();
                    break;
                case "4":
                    RecordEvent();
                    break;
                case "5":
                    DisplayPlayerInfo();
                    break;
                case "6":
                    SaveGoals();
                    break;
                case "7":
                    LoadGoals();
                    break;
                case "8":
                    running = false;
                    Console.WriteLine("Keep questing!");
                    break;
                default:
                    Console.WriteLine("That's not a valid choice.");
                    break;
            }
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("\nWhat type of goal would you like to create?");
        Console.WriteLine("  1. Simple Goal (complete once)");
        Console.WriteLine("  2. Eternal Goal (never finishes, same points every time)");
        Console.WriteLine("  3. Checklist Goal (repeat a set number of times, bonus at the end)");
        Console.WriteLine("  4. Progress Goal (work toward a large numeric target, e.g. miles)");
        Console.WriteLine("  5. Negative Goal (a habit to break — costs points each time)");
        string choice = ReadLineNonEmpty("Choice: ");

        // Names and descriptions can't contain "|" — that character is the
        // field separator in the save file format, so letting it through
        // here would silently corrupt every field after it on load.
        string shortName = ReadLineWithoutPipe("Short name: ");
        string description = ReadLineWithoutPipe("Description: ");
        int points = ReadInt("Points per event: ");

        switch (choice)
        {
            case "1":
                _goals.Add(new SimpleGoal(shortName, description, points));
                break;

            case "2":
                _goals.Add(new EternalGoal(shortName, description, points));
                break;

            case "3":
                int target = ReadInt("How many times must it be completed? ");
                int bonus = ReadInt("Bonus points on final completion: ");
                _goals.Add(new ChecklistGoal(shortName, description, points, target, bonus));
                break;

            case "4":
                int targetProgress = ReadInt("Target amount (e.g. 26 for a 26-mile goal): ");
                int progressBonus = ReadInt("Bonus points when target is reached: ");
                _goals.Add(new ProgressGoal(shortName, description, points, targetProgress, progressBonus));
                break;

            case "5":
                _goals.Add(new NegativeGoal(shortName, description, points));
                break;

            default:
                Console.WriteLine("That's not a valid choice — no goal was created.");
                return;
        }

        Console.WriteLine($"Goal \"{shortName}\" created.");
    }

    public void RecordEvent()
    {
        if (!HasGoals()) return;

        ListGoalNames();
        int index = ReadInt("\nWhich goal did you accomplish? Enter the number: ");
        if (index < 1 || index > _goals.Count)
        {
            Console.WriteLine("That's not a valid goal number.");
            return;
        }

        Goal goal = _goals[index - 1];

        // Every goal type is asked the same question here, even though
        // SimpleGoal/EternalGoal ignore the amount entirely. That's a
        // deliberate tradeoff: GoalManager stays type-agnostic and never
        // has to check "if (goal is ProgressGoal)" before deciding what to
        // ask, which is the whole point of RecordEvent() being polymorphic.
        // The cost is that a user recording a simple/eternal goal can type
        // a number that has no visible effect — worth knowing, not a bug.
        int amount = ReadIntWithDefault("How much progress? (press Enter for 1): ", 1);

        int previousLevel = GetLevel();
        goal.RecordEvent(amount);
        int earned = goal.PointsEarnedLastEvent;
        _score += earned;

        if (earned > 0)
        {
            Console.WriteLine($"You earned {earned} points!");
        }
        else if (earned < 0)
        {
            Console.WriteLine($"That cost you {Math.Abs(earned)} points.");
        }

        if (GetLevel() > previousLevel)
        {
            Console.WriteLine($"*** Level up! You are now a Level {GetLevel()} {GetTitle()}. ***");
        }
    }

    public void ListGoalNames()
    {
        if (!HasGoals()) return;

        Console.WriteLine();
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].ShortName}");
        }
    }

    public void ListGoalDetails()
    {
        if (!HasGoals()) return;

        Console.WriteLine();
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()} — {_goals[i].Description}");
        }
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"\nScore: {_score} points");
        Console.WriteLine($"Level {GetLevel()}: {GetTitle()}");
    }

    // Centralizes the "no goals yet" check that RecordEvent(), ListGoalNames(),
    // and ListGoalDetails() all needed, instead of repeating the same
    // if-block three times.
    private bool HasGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You don't have any goals yet.");
            return false;
        }
        return true;
    }

    private int GetLevel()
    {
        int level = Math.Max(_score, 0) / 500 + 1;
        return Math.Min(level, _titles.Length);
    }

    private string GetTitle()
    {
        return _titles[GetLevel() - 1];
    }

    public void SaveGoals()
    {
        string filename = ReadLineNonEmpty("File name to save to: ");

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_score);
                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.WriteLine($"Saved to {filename}.");
        }
        catch (Exception)
        {
            Console.WriteLine("Couldn't save to that file — check the file name and try again.");
        }
    }

    public void LoadGoals()
    {
        string filename = ReadLineNonEmpty("File name to load from: ");

        if (!File.Exists(filename))
        {
            Console.WriteLine("That file doesn't exist.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);
            int loadedScore = int.Parse(lines[0]);
            List<Goal> loadedGoals = new List<Goal>();

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                loadedGoals.Add(GoalFactory.CreateFromString(lines[i]));
            }

            // Only replace the current state once the whole file has parsed
            // successfully — if something partway through was corrupted,
            // this keeps whatever goals the user already had instead of
            // leaving them with a half-loaded list.
            _score = loadedScore;
            _goals = loadedGoals;
            Console.WriteLine($"Loaded from {filename}.");
        }
        catch (Exception)
        {
            Console.WriteLine("That file couldn't be read — it may be corrupted or in an unexpected format.");
        }
    }

    // ---------- Input helpers ----------
    // Centralizing Console.ReadLine()/int.Parse() here means every prompt
    // in this class re-asks on bad input instead of crashing the whole
    // program on a non-numeric answer or an empty file name.

    private int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int value))
            {
                return value;
            }
            Console.WriteLine("Please enter a whole number.");
        }
    }

    private int ReadIntWithDefault(string prompt, int defaultValue)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return defaultValue;
            }
            if (int.TryParse(input, out int value))
            {
                return value;
            }
            Console.WriteLine("Please enter a whole number, or press Enter for the default.");
        }
    }

    private string ReadLineNonEmpty(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            Console.WriteLine("This can't be blank.");
        }
    }

    private string ReadLineWithoutPipe(string prompt)
    {
        while (true)
        {
            string input = ReadLineNonEmpty(prompt);
            if (input.Contains('|'))
            {
                Console.WriteLine("Sorry, the \"|\" character can't be used here — it's reserved by the save file format.");
                continue;
            }
            return input;
        }
    }
}