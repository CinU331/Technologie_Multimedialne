using System;

namespace MedicationTracker.Models
{
    public class Reminder
    {
        public string ID { get; set; }
        public Medicine Medicine { get; set; }
        public DateTime Date { get; set; }
        public string Portion { get; set; }
    }
}