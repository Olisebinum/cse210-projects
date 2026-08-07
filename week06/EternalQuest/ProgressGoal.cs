using System;

// CREATIVITY ADDITION: tracks incremental progress toward a large numeric
// target, e.g. "Run a Marathon" tracked in miles (target 26), rather than
// a simple yes/no or a fixed repeat count. Points are earned per unit of
// progress, plus a bonus when the target is reached. Not part of the
// course's reference design — this is one of the "exceed requirements"
// additions, which is why it takes advantage of the optional amount
// parameter on RecordEvent() that the required goal types don't need.
public class ProgressGoal : Goal
{
    private int _currentProgress;
    private int _targetProgress;
    private int _bonus;

    public ProgressGoal(string shortName, string description, int points, int targetProgress, int bonus, int currentProgress = 0)
        : base(shortName, description, points)
    {
        _targetProgress = targetProgress;
        _bonus = bonus;
        _currentProgress = currentProgress;
    }

    public override void RecordEvent(int amount = 1)
    {
        if (IsComplete())
        {
            Console.WriteLine("That goal is already complete.");
            PointsEarnedLastEvent = 0;
            return;
        }

        int remaining = _targetProgress - _currentProgress;
        int applied = Math.Min(amount, remaining);
        _currentProgress += applied;
        int earned = _points * applied;

        if (IsComplete())
        {
            earned += _bonus;
            Console.WriteLine($"Target reached! Bonus of {_bonus} points awarded.");
        }

        PointsEarnedLastEvent = earned;
    }

    public override bool IsComplete()
    {
        return _currentProgress >= _targetProgress;
    }

    public override string GetDetailsString()
    {
        string box = IsComplete() ? "[X]" : "[ ]";
        return $"{box} {_shortName} ({_currentProgress}/{_targetProgress})";
    }

    public override string GetStringRepresentation()
    {
        return $"ProgressGoal|{_shortName}|{_description}|{_points}|{_targetProgress}|{_bonus}|{_currentProgress}";
    }
}