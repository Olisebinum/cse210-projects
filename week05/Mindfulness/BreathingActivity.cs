using System;

/// <summary>
/// Guides the user through a slow breathing exercise, alternating
/// between "Breathe in..." and "Breathe out..." with a countdown
/// after each, for the duration the user chose.
/// </summary>
public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing",
            "This activity will help you relax by walking your through breathing in and out slowly. " +
            "Clear your mind and focus on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        DateTime startTime = DateTime.Now;
        bool breatheIn = true;

        while (TimeRemaining(startTime))
        {
            Console.WriteLine();
            Console.Write(breatheIn ? "Breathe in..." : "Breathe out...");
            ShowCountDown(4);
            Console.WriteLine();
            breatheIn = !breatheIn;
        }
    }
}