using MedicationTracker.Models;

namespace MedicationTracker.ViewModels
{
    public class ReminderDetailViewModel : BaseViewModel
    {
        public Reminder Reminder { get; set; }

        public ReminderDetailViewModel(Reminder reminder = null)
        {
            Title = reminder?.Medicine.Name;

            Reminder = reminder;
        }
    }
}