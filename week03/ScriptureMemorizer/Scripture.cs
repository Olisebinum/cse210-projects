using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private static Random _random = new Random();

    // Takes a plain string of text, not a List<Word> -- Scripture keeps
    // full control over how it stores and splits the words internally.
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        foreach (string wordText in text.Split(' '))
        {
            _words.Add(new Word(wordText));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        // Stretch challenge: only select from words that are not already hidden,
        // so we don't waste a "hide" on a word that's already gone.
        List<Word> visibleWords = _words.Where(w => !w.IsHidden()).ToList();

        int amountToHide = Math.Min(numberToHide, visibleWords.Count);

        for (int i = 0; i < amountToHide; i++)
        {
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public string GetDisplayText()
    {
        string wordsText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()}\n{wordsText}";
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }
}