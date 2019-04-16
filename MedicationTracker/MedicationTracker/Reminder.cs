using System;

namespace MedicationTracker
{
    public class Reminder
    {
        public string Medicine { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Interval { get; set; }
        public string Portion { get; set; }
    }
}