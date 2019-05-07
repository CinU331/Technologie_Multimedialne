namespace MedicationTracker.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public Settings Settings { get; set; }

        public SettingsViewModel()
        {
            Settings = new Settings();
        }
    }
}