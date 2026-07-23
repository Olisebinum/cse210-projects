using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Building a Budget in 10 Minutes", "NairaWise", 612);
        video1.AddComment(new Comment("Amaka O.", "This actually changed how I plan my paycheck!"));
        video1.AddComment(new Comment("David T.", "Wish I found this a year ago."));
        video1.AddComment(new Comment("Chioma B.", "Can you do one on savings goals next?"));

        Video video2 = new Video("Intro to C# Classes", "Code with Kendrick", 845);
        video2.AddComment(new Comment("Femi A.", "Finally understand encapsulation."));
        video2.AddComment(new Comment("Sarah K.", "Great pacing, not too fast."));
        video2.AddComment(new Comment("Musa I.", "More examples like this please."));
        video2.AddComment(new Comment("Grace N.", "Subscribed!"));

        Video video3 = new Video("Abuja Small Business Spotlight", "Chamber Media", 430);
        video3.AddComment(new Comment("Tunde O.", "Love seeing local businesses featured."));
        video3.AddComment(new Comment("Blessing E.", "Where is this shop located?"));
        video3.AddComment(new Comment("Ifeanyi C.", "Great production quality."));

        Video video4 = new Video("Forex Basics for Beginners", "TradeSimple", 723);
        video4.AddComment(new Comment("Peter U.", "Clear explanation of pips and lots."));
        video4.AddComment(new Comment("Ngozi A.", "Please cover risk management next."));
        video4.AddComment(new Comment("Samuel D.", "Best beginner video I've found."));

        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);
        videos.Add(video4);

        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  - {comment.GetName()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }
    }
}