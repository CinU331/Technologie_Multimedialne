namespace MedicationTracker
{
    public class Settings
    {
        public string SecondsToRemoveOldReminders
        {
            get { return StaticSecondsToRemoveOldReminders.ToString(); }
            set { StaticSecondsToRemoveOldReminders = int.Parse(value); }
        }

        #region Data store
        public static int StaticSecondsToRemoveOldReminders { get; set; } = 10;
        #endregion
    }
}