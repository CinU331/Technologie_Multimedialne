using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using MedicationTracker.Models;
using MedicationTracker.Views;

namespace MedicationTracker.ViewModels
{
    public class RemindersViewModel : BaseViewModel
    {
        public ObservableCollection<Reminder> Reminders { get; set; }

        public ICommand LoadRemindersCommand { get; private set; }

        public RemindersViewModel()
        {
            Title = "Reminders";
            Reminders = new ObservableCollection<Reminder>();

            MessagingCenter.Subscribe<RemindersPage, Reminder>(this, "AddReminder", async (obj, reminder) =>
            {
                Reminder newReminder = reminder as Reminder;
                Reminders.Add(newReminder);
                await DataStore.AddItemAsync(newReminder);
            });

            LoadRemindersCommand = new Command(async () => await ExecuteLoadRemindersCommand());
        }

        async Task ExecuteLoadRemindersCommand()
        {
            if (IsBusy) { return; }

            IsBusy = true;

            try
            {
                Reminders.Clear();

                IEnumerable<Reminder> reminders = await DataStore.GetItemsAsync(true);

                foreach (var r in reminders)
                {
                    Reminders.Add(r);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
            finally { IsBusy = false; }
        }
    }
}