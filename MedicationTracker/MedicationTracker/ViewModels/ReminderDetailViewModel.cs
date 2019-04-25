using System.Windows.Input;

using Xamarin.Forms;

using MedicationTracker.Models;

namespace MedicationTracker.ViewModels
{
    public class ReminderDetailViewModel : BaseViewModel
    {
        public Reminder Reminder { get; set; }

        public ICommand DeleteReminderCommand { get; private set; }

        public ReminderDetailViewModel(Reminder reminder = null)
        {
            Title = reminder?.Medicine.Name;

            Reminder = reminder;

            DeleteReminderCommand = new Command(() => ExecuteDeleteReminderCommand());
        }

        void ExecuteDeleteReminderCommand()
        {
            MessagingCenter.Send(this, "DeleteReminder", Reminder);
        }
    }
}