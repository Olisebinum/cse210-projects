using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job();
        job1._jobTitle = "Software Engineer";
        job1._company = "Microsoft";
        job1._startYear = 2019;
        job1._endYear = 2022;

        Job job2 = new Job();
        job2._jobTitle = "Manager";
        job2._company = "Apple";
        job2._startYear = 2022;
        job2._endYear = 2023;

        Resume resume1 = new Resume();
        resume1._name = "Olise Ebinum";
        resume1._jobs.Add(job1);
        resume1._jobs.Add(job2);

        resume1.Display();

        // Second resume to prove abstraction works with multiple people
        Job job3 = new Job();
        job3._jobTitle = "Data Analyst";
        job3._company = "Maybeach Tech";
        job3._startYear = 2020;
        job3._endYear = 2023;

        Resume resume2 = new Resume();
        resume2._name = "Jane Doe";
        resume2._jobs.Add(job3);

        Console.WriteLine();
        resume2.Display();
    }
}