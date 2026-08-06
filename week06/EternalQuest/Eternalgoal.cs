using System;

// A goal that is never "finished" — you earn the same points every time
// you record it, e.g. "Read Scriptures" for 100 points, indefinitely.
public class EternalGoal : Goal
{
    public EternalGoal(string shortName, string description, int points)
        : base(shortName, description, points)
    {
    }

    public override void RecordEvent(int amount = 1)
    {
        PointsEarnedLastEvent = _points;
    }

    public override bool IsComplete()
    {
        // Eternal goals have no finish line by definition.
        return false;
    }

    public override string GetDetailsString()
    {
        return $"[X] {_shortName}";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{_shortName}|{_description}|{_points}";
    }
}