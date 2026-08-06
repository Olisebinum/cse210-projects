using System;

// A goal that can be completed exactly once, e.g. "Run a Marathon" for 1000 points.
public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string shortName, string description, int points, bool isComplete = false)
        : base(shortName, description, points)
    {
        _isComplete = isComplete;
    }

    public override void RecordEvent(int amount = 1)
    {
        if (_isComplete)
        {
            Console.WriteLine("That goal is already complete.");
            PointsEarnedLastEvent = 0;
            return;
        }

        _isComplete = true;
        PointsEarnedLastEvent = _points;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetailsString()
    {
        string box = _isComplete ? "[X]" : "[ ]";
        return $"{box} {_shortName}";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{_shortName}|{_description}|{_points}|{_isComplete}";
    }
}