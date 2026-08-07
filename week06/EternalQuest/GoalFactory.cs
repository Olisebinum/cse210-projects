using System;

// Parses one line of the save file back into the correct Goal subclass.
// This is the other side of polymorphism from RecordEvent()/GetDetailsString():
// here we're deciding, at runtime, which concrete type to construct based
// on data read from a file.
public static class GoalFactory
{
    public static Goal CreateFromString(string line)
    {
        string[] parts = line.Split('|');
        string type = parts[0];

        switch (type)
        {
            case "SimpleGoal":
                return new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4]));

            case "EternalGoal":
                return new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));

            case "ChecklistGoal":
                return new ChecklistGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3]),
                    int.Parse(parts[4]),
                    int.Parse(parts[5]),
                    int.Parse(parts[6])
                );

            case "ProgressGoal":
                return new ProgressGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3]),
                    int.Parse(parts[4]),
                    int.Parse(parts[5]),
                    int.Parse(parts[6])
                );

            case "NegativeGoal":
                return new NegativeGoal(parts[1], parts[2], int.Parse(parts[3]));

            default:
                throw new FormatException($"Unrecognized goal type in save file: {type}");
        }
    }
}