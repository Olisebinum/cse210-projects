using System;

// CREATIVITY ADDITION: a "negative goal" for bad habits you're trying to
// stop, e.g. "Skipped Workout". Unlike every other goal type, recording
// this one costs you points instead of earning them, and it's never
// "complete" — the whole point is to record it as rarely as possible.
// Not part of the course's reference design — this is an "exceed
// requirements" addition.
public class NegativeGoal : Goal
{
    public NegativeGoal(string shortName, string description, int points)
        : base(shortName, description, points)
    {
    }

    public override void RecordEvent(int amount = 1)
    {
        // _points is stored as a positive number for readability when
        // creating the goal; the penalty is applied here.
        PointsEarnedLastEvent = -Math.Abs(_points) * amount;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetailsString()
    {
        return $"[ ] {_shortName} (habit to break, -{Math.Abs(_points)} pts each time)";
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal|{_shortName}|{_description}|{_points}";
    }
}