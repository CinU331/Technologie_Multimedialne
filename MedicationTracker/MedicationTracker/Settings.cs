using System.Windows.Input;

using Xamarin.Forms;

using MedicationTracker.Interfaces;

namespace MedicationTracker
{
    public class Settings
    {
        public string SecondsToRemoveOldReminders
        {
            get { return StaticSecondsToRemoveOldReminders.ToString(); }
            set { StaticSecondsToRemoveOldReminders = int.Parse(value); }
        }

        public string SelectedNotificationSound
        {
            get { return StaticSelectedNotificationSound; }
            set
            {
                if (StaticSelectedNotificationSound != value)
                {
                    StaticSelectedNotificationSound = value;
                    DependencyService.Get<IAudio>().PlayAudioFile(value);
                }
            }
        }

        public ICommand SetLightSoundCommand { get; private set; }
        public ICommand SetRoughSoundCommand { get; private set; }

        public Settings()
        {
            SetLightSoundCommand = new Command(() => { SelectedNotificationSound = "light.wav"; });
            SetRoughSoundCommand = new Command(() => { SelectedNotificationSound = "rough.wav"; });
        }

        #region Data store
        public static int StaticSecondsToRemoveOldReminders { get; set; } = 10;
        public static string StaticSelectedNotificationSound { get; set; } = "light.wav";
        #endregion
    }
}