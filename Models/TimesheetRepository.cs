using Xunit;
using System.Collections.Generic;
using TimesheetSystem.Models;


namespace TimesheetSystem
{
    public class TimesheetRepository
    {
        // A static list to store timesheet entries in memory
        private static readonly List<TimesheetEntry> _entries = new List<TimesheetEntry>();

        // Adds a new timesheet entry to the list.
        public void AddEntry(TimesheetEntry entry)
        {
            _entries.Add(entry); // Store the entry in the in-memory list
        }

        // Retrieves all timesheet entries stored in memory.
        public List<TimesheetEntry> GetEntries()
        {
            return _entries; // Return the stored timesheet entries
        }
    }

}
