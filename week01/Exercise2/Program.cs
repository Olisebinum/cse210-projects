using System;

class Program
{
    static void Main(string[] args)
    {
        // Prompt the user for their grade percentage
        Console.Write("What is your grade percentage? ");
        int grade = int.Parse(Console.ReadLine());

        string letter = "";
        string sign = "";

        // Determine the letter grade
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determine the + or - sign
        int lastDigit = grade % 10;

        if (letter != "A" && letter != "F")
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }
        else if (letter == "A")
        {
            // Only A- is possible, not A+
            if (lastDigit < 3 && grade < 100)
            {
                sign = "-";
            }
        }

        // Display the final grade
        Console.WriteLine($"Your grade is {letter}{sign}.");

        // Determine pass/fail
        if (grade >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Don't give up! Keep working hard and you'll do better next time.");
        }
    }
}