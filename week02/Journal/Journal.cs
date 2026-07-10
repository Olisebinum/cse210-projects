using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// The Journal class is responsible for managing the full collection of
// journal entries: adding new ones, displaying them, and saving/loading
// them to and from a file. The rest of the program interacts with the
// journal only through these methods (AddEntry, DisplayAll, SaveToFile,
// LoadFromFile) and never touches the internal _entries list directly.
// This is abstraction in practice: the internal list is a private detail,
// and everyone else uses the simple public methods instead.
public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    // Adds a single entry to the journal.
    // The caller does not need to know this is backed by a List<Entry> —
    // it just calls AddEntry() and trusts the Journal to handle it.
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    // Displays every entry currently stored in the journal.
    // Notice that Journal does not know how to format a single entry —
    // it delegates that responsibility to each Entry's own Display()
    // method. This keeps formatting logic out of Journal entirely.
    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("The journal is currently empty.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    // Saves the current journal to a file, using JSON format.
    //
    // NOTE (exceeding requirements): instead of using a simple
    // character-separated text format, I chose to store entries as JSON.
    // This keeps the saved data structured and human-readable, avoids any
    // issues with separator characters accidentally appearing inside a
    // user's written response, and can be opened and inspected in any
    // text editor or re-used by other programs that understand JSON.
    public void SaveToFile(string filename)
    {
        // IncludeFields = true is required here because Entry stores its
        // data in public fields (_date, _promptText, _entryText) rather
        // than properties. System.Text.Json only serializes properties
        // by default, so without this flag every saved entry would come
        // out as an empty JSON object.
        string json = JsonSerializer.Serialize(_entries, new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        });

        File.WriteAllText(filename, json);
        Console.WriteLine($"Journal saved to {filename}.");
    }

    // Loads a journal from a file, replacing any entries currently held
    // in memory. If the file does not exist, the current journal is left
    // unchanged and a message is shown instead of crashing the program.
    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"No file found at {filename}. Nothing was loaded.");
            return;
        }

        string json = File.ReadAllText(filename);
        List<Entry> loadedEntries = JsonSerializer.Deserialize<List<Entry>>(json, new JsonSerializerOptions
        {
            IncludeFields = true
        });

        _entries = loadedEntries ?? new List<Entry>();
        Console.WriteLine($"Journal loaded from {filename}. {_entries.Count} entries found.");
    }
}