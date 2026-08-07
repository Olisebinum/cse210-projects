using System;

// A goal that must be recorded a set number of times to be complete.
// Each event earns the base points; the final qualifying event also
// earns a bonus, e.g. "Attend the Temple" 10 times, 50 pts each,
// +500 bonus on the 10th.
public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string shortName, string description, int points, int target, int bonus, int amountCompleted = 0)
        : base(shortName, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = amountCompleted;
    }

    public override void RecordEvent(int amount = 1)
    {
        if (IsComplete())
        {
            Console.WriteLine("That goal is already complete.");
            PointsEarnedLastEvent = 0;
            return;
        }

        // Clip to what's actually remaining so a single large "amount"
        // can't pay out for more repetitions than the goal actually needed
        // (e.g. recording 5 at once when only 1 more was required to finish).
        int remaining = _target - _amountCompleted;
        int applied = Math.Min(amount, remaining);
        _amountCompleted += applied;
        int earned = _points * applied;

        if (IsComplete())
        {
            earned += _bonus;
            Console.WriteLine($"Checklist complete! Bonus of {_bonus} points awarded.");
        }

        PointsEarnedLastEvent = earned;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        string box = IsComplete() ? "[X]" : "[ ]";
        int shown = Math.Min(_amountCompleted, _target);
        return $"{box} {_shortName} (Completed {shown}/{_target} times)";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_shortName}|{_description}|{_points}|{_target}|{_bonus}|{_amountCompleted}";
    }
}