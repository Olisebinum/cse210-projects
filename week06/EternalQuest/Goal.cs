using System;

// Abstract base class for all goal types. Holds the attributes every goal
// shares (short name, description, base point value) and declares the
// behaviors that make this program polymorphic: RecordEvent() and
// GetDetailsString(). Each derived class implements these differently, so
// GoalManager can call them on any Goal in the list without knowing or
// caring which subclass it's actually holding.
public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    public string ShortName => _shortName;
    public string Description => _description;
    public int Points => _points;

    // RecordEvent() returns void to match the course design, but the
    // manager still needs to know how many points a given event earned
    // (which varies — e.g. a checklist goal's final event also earns a
    // bonus). Each override sets this property as a side effect instead
    // of returning the value directly, so GoalManager can read it right
    // after calling RecordEvent() without ever needing to check which
    // concrete goal type it's holding.
    public int PointsEarnedLastEvent { get; protected set; }

    public Goal(string shortName, string description, int points)
    {
        _shortName = shortName;
        _description = description;
        _points = points;
    }

    // Records one occurrence of this goal being worked on / completed.
    // amount defaults to 1 so every required goal type can call it with
    // no argument; ProgressGoal (a creativity addition) can optionally
    // take a larger amount for one event.
    public abstract void RecordEvent(int amount = 1);

    // Whether this goal is fully complete. Eternal and negative goals are
    // never "complete" in this sense.
    public abstract bool IsComplete();

    // How this goal should be displayed in the goal list, e.g.
    // "[X] Run a Marathon" or "[ ] Attend the Temple (Completed 3/10 times)"
    public abstract string GetDetailsString();

    // How this goal should be written to the save file. Each subclass
    // prefixes its line with its own type name so GoalFactory can rebuild
    // the correct subclass when loading.
    public abstract string GetStringRepresentation();
}